﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MyLibrary;
using MyLibrary.LastError;
using MyParser.Comparer;
using MyParser.Managers;
using RealtyParser.Collections;
using RT.ParsingLibs;
using RT.ParsingLibs.Models;
using RT.ParsingLibs.Requests;
using RT.ParsingLibs.Responses;
using Boolean = MyLibrary.Types.Boolean;
using String = MyLibrary.Types.String;
using Uri = MyLibrary.Types.Uri;

namespace RealtyParser
{
    public class ParserModule : IParsingModule, IValueable, ILastError
    {
        public ParserModule()
        {
            ModuleClassname = typeof (ParserModule).Namespace;
            ModuleNamespace = typeof (ParserModule).Namespace;
            Database = new Database {ModuleClassname = ModuleClassname};
            ComparerManager = new ComparerManager();
            CompressionManager = new CompressionManager();
            Converter = new Converter();
            Transformation = new Transformation();
            Parser = new Parser {Transformation = Transformation, Database = Database, Converter = Converter};
            LookupCrawler = new Crawler {CompressionManager = CompressionManager};
            PublicationCrawler = new Crawler {CompressionManager = CompressionManager};
            LinkComparer = new LinkComparer();
            ObjectComparer = new ObjectComparer();
            PublicationHash = new Dictionary<Link, WebPublication>(LinkComparer);
        }

        private UriBuilder BaseBuilder { get; set; }
        public object LastError { get; set; }

        /// <summary>
        ///     Задача на парсинг
        /// </summary>
        /// <param name="request">Запрос на парсинг</param>
        /// <returns>Ответ от парсера</returns>
        public async Task<ParseResponse> Result(ParseRequest request)
        {
            int responseLevel = 4;
            var responseCode = new StackListQueue<ParseResponseCode>
            {
                ParseResponseCode.NotAvailableResource, // Неправильные параметры
                ParseResponseCode.NotFoundId, // Нет никаких данных
                ParseResponseCode.NotFoundId, // Данные есть, но последний Id не найден
                ParseResponseCode.ContentEmpty, // Последний Id найден, но нечего возвращать
                ParseResponseCode.Success // Последний Id найден и есть что вернуть
            };

            Database.Connect();
            Mapping =
                Database.GetList(Database.MappingTable, Database.TableNameColumn).Select(item => item.ToString());

            Debug.WriteLine("-------------------------------------------------------------------");
            Debug.WriteLine("Переданы параметры:");
            Debug.WriteLine("request.RegionId -> {0}", request.RegionId);
            Debug.WriteLine("request.RubricId -> {0}", request.RubricId);
            Debug.WriteLine("request.ActionId -> {0}", request.ActionId);
            Debug.WriteLine(string.Format("request.LastPublicationId -> '{0}'", request.LastPublicationId));
            Debug.WriteLine("-------------------------------------------------------------------");
            Debug.WriteLine(string.Empty);
            Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!   ВНИМАНИЮ ТЕСТИРОВЩИКОВ  !!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Debug.WriteLine("Наличие сообщений в логе отладки и трассировки не является признаком ошибки");
            Debug.WriteLine("Ошибка – это несоответствие программы требованиям");
            Debug.WriteLine("Тестирование – проверка соответствия программы требованиям");
            Debug.WriteLine("В случае ошибки, то есть несоответствия программы требованиям, предоставить,");
            Debug.WriteLine("для исправления ошибки, информацию о параметрах, при которых осуществлялся запуск,");
            Debug.WriteLine("чтобы можно было воспроизвести ошибку, и описание в чём заключается ошибка");
            Debug.WriteLine("Так же желательно приложить лог отладки программы на момент ошибки");
            Debug.WriteLine("Например:");
            Debug.WriteLine("Запускалось с параметрами сайт=www..., RegionId=..., RubricId=..., ActionId=..");
            Debug.WriteLine("на экран выдалось сообщение ... об ..., программа не вернула результаты");
            Debug.WriteLine("Например:");
            Debug.WriteLine("Запускалось с параметрами сайт=www..., RegionId=..., RubricId=..., ActionId=..");
            Debug.WriteLine("в результатах отсутствует поле ... для объявления ... по адресу http://....");
            Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!   ВНИМАНИЮ ТЕСТИРОВЩИКОВ  !!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Debug.WriteLine(string.Empty);

            var requestId = new RequestProperties
            {
                {
                    Database.SiteTable,
                    Database.GetScalar(ModuleClassname, Database.SiteIdColumn, Database.ModuleClassnameColumn,
                        Database.SiteTable)
                },
            };

            SiteProperties = Database.GetSiteProperties(requestId.Site);

            PublicationComparer =
                ComparerManager.CreatePublicationComparer(SiteProperties.PublicationComparerClassName.ToString());
            ResourceComparer = new ResourceComparer {PublicationComparer = PublicationComparer, Mapping = Mapping};
            ReturnFieldInfos = Database.GetReturnFieldInfos(requestId.Site);
            LookupCrawler.Method = SiteProperties.LookupMethod.ToString();
            LookupCrawler.Encoding = SiteProperties.LookupEncoding.ToString();
            LookupCrawler.Compression = SiteProperties.LookupCompression.ToString();
            LookupCrawler.Edition = (int) MyDatabase.Database.ConvertTo<long>(SiteProperties.LookupEdition);
            PublicationCrawler.Method = SiteProperties.PublicationMethod.ToString();
            PublicationCrawler.Encoding = SiteProperties.PublicationEncoding.ToString();
            PublicationCrawler.Compression = SiteProperties.PublicationCompression.ToString();
            PublicationCrawler.Edition = (int) MyDatabase.Database.ConvertTo<long>(SiteProperties.PublicationEdition);

            var loggingDays = MyDatabase.Database.ConvertTo<long>(SiteProperties.LoggingDays);
            if (loggingDays > 0)
            {
                Database.Wait(Database.Connection);
                Database.Delete(
                    new Logging
                    {
                        SiteId = SiteProperties.SiteId,
                        RequestTimestamp = DateTime.Now.AddDays(1 - loggingDays).ToString("yyyy-MM-dd 00:00:00")
                    }, "<");
                Database.Release(Database.Connection);
            }

            var lastResources = new StackListQueue<Resource>(
                (string.IsNullOrWhiteSpace(request.LastPublicationId) ? string.Empty : request.LastPublicationId).Split(
                    '&')
                    .Select(s => s.Trim())
                    .Where(String.IsNotNullAndNotEmpty)
                    .Where(s => PublicationComparer.IsValid(s))
                    .Select(
                        s =>
                            new Resource(s, SiteProperties.ResourceTemplate.ToString())
                            {
                                Transformation = Transformation
                            })
                );

            Debug.WriteLine("lastResources.Count = " + lastResources.Count);

            try
            {
                foreach (string table in Mapping)
                {
                    PropertyInfo propertyInfo = request.GetType().GetProperty(table + "Id");
                    requestId.Add(table, Database.GetScalar(propertyInfo.GetValue(request), table));
                }


                Dictionary<RequestProperties, Resource> lastResourceDictionary =
                    lastResources.Select(item => new RequestProperties(item, Mapping))
                        .Distinct().ToDictionary(item => item,
                            item =>
                                lastResources.Last(
                                    r => ObjectComparer.Equals(r, item, Mapping)));

                if (requestId.Select(pair => pair.Value == null).Aggregate(Boolean.Or))
                    throw new NotAvailableDetectedException();

                Debug.WriteLine("requestId {0}", requestId);
                Debug.WriteLine("properties {0}", SiteProperties);
                Debug.WriteLine("returnFieldInfos {0}", ReturnFieldInfos);


                Dictionary<string, List<object>> mappedEnum = Mapping.ToDictionary(s => s,
                    s => Database.GetList(requestId[s], s, requestId.Site).ToList());

                Dictionary<string, List<object>> childrenEnum = mappedEnum.ToDictionary(table => table.Key,
                    table => new List<object>());
                foreach (var pair in mappedEnum)
                    foreach (object item in pair.Value)
                        childrenEnum[pair.Key].AddRange(Database.GetList(pair.Key, item, requestId.Site).ToList());
                childrenEnum = childrenEnum.ToDictionary(pair => pair.Key, pair => pair.Value.Distinct().ToList());

                var includeList = new StackListQueue<RequestProperties>
                {
                    from action in mappedEnum["Action"]
                    from rubric in mappedEnum["Rubric"]
                    from region in mappedEnum["Region"]
                    select
                        new RequestProperties {Action = action, Rubric = rubric, Region = region, Site = requestId.Site}
                };

                if (!includeList.Any())
                {
                    responseLevel = Math.Min(0, responseLevel);
                    return new ParseResponse
                    {
                        ResponseCode = responseCode[responseLevel],
                        LastPublicationId = string.Join("&", lastResources.Select(item => item.ToString())),
                        ModuleName = GetType().Name,
                        Publications = new StackListQueue<WebPublication>()
                    };
                }

                var excludeList = new StackListQueue<RequestProperties>
                {
                    from action in mappedEnum["Action"]
                    from rubric in childrenEnum["Rubric"]
                    from region in mappedEnum["Region"]
                    select
                        new RequestProperties {Action = action, Rubric = rubric, Region = region, Site = requestId.Site}
                };

                BaseBuilder = new UriBuilder(SiteProperties.Url.ToString())
                {
                    UserName = SiteProperties.UserName.ToString(),
                    Password = SiteProperties.Password.ToString(),
                };

                // Устанавливаем uri используемый по-умолчанию при преобразовании строки
                Uri.Default = BaseBuilder.Uri;
                MyLibrary.Types.DateTime.Default = DateTime.Now;

                var returnFieldInfos = new ReturnFieldInfos();
                foreach (
                    string key in
                        ReturnFieldInfos.GetType().GetProperties()
                            .Select(propertyInfo => propertyInfo.Name)
                            .Where(key => ReturnFieldInfos.ContainsKey(key)))
                    returnFieldInfos.Add(key, ReturnFieldInfos[key]);

                var dictionary = new Dictionary<Resource, KeyValuePair<Link, Values>>(ResourceComparer);
                var resources = new SortedStackListQueue<Resource> {Comparer = ResourceComparer};
                bool available = false;

                foreach (RequestProperties mappedId in includeList)
                {
                    var mappedLevel = new RequestProperties
                    {
                        mappedId.Where(pair => Mapping.Contains(pair.Key)).ToDictionary(pair => pair.Key,
                            pair =>
                                Database.GetScalar(pair.Value, Database.LevelColumn, pair.Key, requestId.Site))
                    };

                    Values siteValues = Parser.BuildValues(requestId, mappedId, mappedLevel);

                    if ((from name in new[] {"RubricAction", "RegionRubric"}
                        let property = siteValues.GetType().GetProperty(name)
                        let propertyLock = SiteProperties.GetType().GetProperty(string.Format("{0}Lock", name))
                        let enumerable = property.GetValue(siteValues, null) as IEnumerable<string>
                        let value = MyDatabase.Database.ConvertTo<long>(propertyLock.GetValue(SiteProperties, null))
                        select !enumerable.Any() && value != 0).Aggregate(Boolean.Or)) continue;

                    available = true;

                    var requestProperties = new RequestProperties(mappedId, Mapping);
                    var currentResources = new StackListQueue<Resource>();
                    try
                    {
                        for (long pageId = 1;
                            currentResources.Count < MyDatabase.Database.ConvertTo<long>(SiteProperties.CountAd);
                            pageId++)
                        {
                            var pageValues = new Values(siteValues)
                            {
                                Page =
                                    new StackListQueue<string>
                                    {
                                        ((pageId > 1) ? pageId.ToString(CultureInfo.InvariantCulture) : string.Empty)
                                    },
                                PageStart =
                                    new StackListQueue<string>
                                    {
                                        string.Format("{0}",
                                            (pageId - 1)*
                                            MyDatabase.Database.ConvertTo<long>(SiteProperties.PageSize))
                                    }
                            };
                            pageValues.Add("Page-1", (pageId - 1).ToString(CultureInfo.InvariantCulture));
                            pageValues.Add("Page||1", pageId.ToString(CultureInfo.InvariantCulture));
                            if (pageId > 1) pageValues.Add("&p=Page-1", string.Format("&p={0}", pageId - 1));

                            Debug.WriteLine(pageValues.ToString());
                            IEnumerable<string> urls =
                                Transformation.ParseTemplate(SiteProperties.LookupTemplate.ToString(), pageValues);
                            IEnumerable<string> reqs =
                                Transformation.ParseTemplate(SiteProperties.LookupRequestTemplate.ToString(),
                                    pageValues);
                            for (int index = 0; index < pageValues.MaxCount; index++)
                            {
                                Debug.WriteLine(urls.ElementAt(index) + ":" + reqs.ElementAt(index));
                                try
                                {
                                    LookupCrawler.Request = reqs.ElementAt(index);
                                    var builder =
                                        new UriBuilder(Uri.Combine(BaseBuilder.Uri.ToString(), urls.ElementAt(index)))
                                        {
                                            UserName = BaseBuilder.UserName,
                                            Password = BaseBuilder.Password,
                                        };
                                    var urlValues = new Values(pageValues)
                                    {
                                        Url = new StackListQueue<string> {builder.Uri.ToString()}
                                    };
                                    Uri.Default = builder.Uri;

                                    IEnumerable<MemoryStream> documents =
                                        await LookupCrawler.WebRequest(builder.Uri);

                                    if (!documents.Any())
                                        throw new EndOfSearchDetectedException();

                                    ReturnFields returnFields = Parser.BuildReturnFields(documents,
                                        urlValues, returnFieldInfos.ToList());

                                    if (!returnFields.PublicationLink.Any())
                                        throw new EndOfSearchDetectedException();

                                    returnFields.Add(urlValues);
                                    int linkCount = returnFields.PublicationLink.Count();
                                    Debug.WriteLine("linkCount = " + linkCount);

                                    var returnValues = new Values(returnFields);
                                    IEnumerable<Link> links =
                                        Transformation.ParseTemplate(SiteProperties.PublicationTemplate.ToString(),
                                            returnValues)
                                            .ToList()
                                            .GetRange(0, linkCount)
                                            .Select(s => new Link(Uri.Combine(builder.Uri.ToString(),
                                                s).ToString()));
                                    Debug.WriteLine("links.Count = " + links.Count());

                                    Debug.WriteLine(string.Join(Environment.NewLine,
                                        siteValues.Where(pair => pair.Value == null).Select(pair => pair.Key)));
                                    foreach (
                                        var pair in
                                            siteValues.Where(pair => pair.Value != null).Where(pair => pair.Value.Any())
                                        )
                                    {
                                        returnValues.Add(pair.Key,
                                            Enumerable.Repeat(pair.Value.First(), linkCount).ToList());
                                    }
                                    foreach (string item in Mapping)
                                    {
                                        returnValues[item] =
                                            Enumerable.Repeat(mappedId[item].ToString(), linkCount).ToList();
                                    }

                                    IEnumerable<string> keys =
                                        Transformation.ParseTemplate(SiteProperties.ResourceTemplate.ToString(),
                                            returnValues);

                                    for (int i = 0; i < linkCount; i++)
                                    {
                                        if (PublicationHash.ContainsKey(links.ElementAt(i)))
                                            throw new LoopDetectedException();

                                        if (PublicationComparer.IsValid(keys.ElementAt(i)))
                                        {
                                            var resource = new Resource(keys.ElementAt(i),
                                                SiteProperties.ResourceTemplate.ToString())
                                            {
                                                Transformation = Transformation
                                            };
                                            if (dictionary.ContainsKey(resource))
                                                throw new LoopDetectedException();
                                            if (loggingDays > 0)
                                                if (
                                                    Database.Exists(new Logging
                                                    {
                                                        SiteId = SiteProperties.SiteId,
                                                        ResponceResource = resource.ToString()
                                                    })) continue;

                                            dictionary.Add(resource,
                                                new KeyValuePair<Link, Values>(links.ElementAt(i),
                                                    returnValues.Slice(i)));
                                            Debug.WriteLine("Enqueue " + resource + " " +
                                                            PublicationComparer.GetType().Name);
                                            currentResources.Enqueue(resource);
                                            continue;
                                        }

                                        try
                                        {
                                            var builder1 =
                                                new UriBuilder(Uri.Combine(BaseBuilder.Uri.ToString(),
                                                    links.ElementAt(i).ToString()))
                                                {
                                                    UserName = BaseBuilder.UserName,
                                                    Password = BaseBuilder.Password,
                                                };
                                            var values = new Values(returnValues.Slice(i))
                                            {
                                                Url = new StackListQueue<string>(builder1.Uri.ToString()),
                                            };
                                            Uri.Default = builder.Uri;

                                            IEnumerable<MemoryStream> streams =
                                                await PublicationCrawler.WebRequest(builder1.Uri);
                                            Thread.Sleep(0);
                                            ReturnFields fields = Parser.BuildReturnFields(streams,
                                                values, ReturnFieldInfos.ToList());
                                            var values1 = new Values(fields) {values};
                                            IEnumerable<Resource> list =
                                                Transformation.ParseTemplate(
                                                    SiteProperties.ResourceTemplate.ToString(),
                                                    values1)
                                                    .Where(String.IsNotNullAndNotEmpty)
                                                    .Where(s => PublicationComparer.IsValid(s))
                                                    .Select(
                                                        s =>
                                                            new Resource(s,
                                                                SiteProperties.ResourceTemplate.ToString())
                                                            {
                                                                Transformation = Transformation
                                                            });

                                            Debug.WriteLine("Enqueue " + list.First() + " " +
                                                            PublicationComparer.GetType().Name);

                                            if (dictionary.ContainsKey(list.First()))
                                                throw new LoopDetectedException();

                                            if (loggingDays > 0)
                                                if (
                                                    Database.Exists(new Logging
                                                    {
                                                        SiteId = SiteProperties.SiteId,
                                                        ResponceResource = list.First().ToString()
                                                    })) continue;

                                            currentResources.Enqueue(list.First());
                                            dictionary.Add(list.First(),
                                                new KeyValuePair<Link, Values>(links.ElementAt(i), values));
                                            PublicationHash.Add(links.ElementAt(i),
                                                Parser.CreateWebPublication(fields, requestId));

                                            if (!lastResourceDictionary.ContainsKey(requestProperties))
                                            {
                                                Debug.WriteLine("No requestProperties in lastResourceDictionary " +
                                                                requestProperties);
                                                throw new EndOfSearchDetectedException();
                                            }
                                        }
                                        catch (EndOfSearchDetectedException exception)
                                        {
                                            Debug.WriteLine(exception.ToString());
                                            throw;
                                        }
                                        catch (LoopDetectedException exception)
                                        {
                                            Debug.WriteLine(exception.ToString());
                                            throw;
                                        }
                                        catch (Exception exception)
                                        {
                                            Debug.WriteLine(exception.ToString());
                                            LastError = exception;
                                        }
                                    }
                                }
                                catch (EndOfSearchDetectedException exception)
                                {
                                    Debug.WriteLine(exception.ToString());
                                    throw;
                                }
                                catch (LoopDetectedException exception)
                                {
                                    Debug.WriteLine(exception.ToString());
                                    throw;
                                }
                                catch (Exception exception)
                                {
                                    Debug.WriteLine(exception.ToString());
                                    LastError = exception;
                                }
                            }

                            currentResources.Sort(ResourceComparer);
                            Debug.WriteLine("currentResources.Count = " + currentResources.Count);
                            if (!lastResourceDictionary.ContainsKey(requestProperties))
                                throw new EndOfSearchDetectedException();

                            if (ResourceComparer.Compare(
                                currentResources.DefaultIfEmpty(lastResourceDictionary[requestProperties])
                                    .FirstOrDefault(),
                                lastResourceDictionary[requestProperties]) < 0)
                                throw new EndOfSearchDetectedException();
                        }
                    }
                    catch (EndOfSearchDetectedException exception)
                    {
                        Debug.WriteLine(exception.ToString());
                    }
                    catch (LoopDetectedException exception)
                    {
                        Debug.WriteLine(exception.ToString());
                    }
                    Debug.WriteLine("resources.Count = " + resources.Count());
                    resources.AddRangeExcept(currentResources);
                    Debug.WriteLine("resources.Count = " + resources.Count());
                }

                if (!available) throw new NotAvailableDetectedException();

                var nextResources = new StackListQueue<Resource>(lastResources) {resources};
                nextResources = new StackListQueue<Resource>(nextResources.Select(
                    item => new RequestProperties(item, Mapping))
                    .Distinct().Select(
                        item =>
                            nextResources.Last(
                                r => ObjectComparer.Equals(r, item, Mapping))));

                if (loggingDays > 0)
                {
                    Database.Wait(Database.Connection);
                    Database.InsertOrReplace(
                        resources.Select(
                            r =>
                                new Logging
                                {
                                    SiteId = SiteProperties.SiteId,
                                    RequestTimestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                    ResponceResource = r.ToString()
                                }));
                    Database.Release(Database.Connection);
                }
                try
                {
                    if (lastResourceDictionary.Any() && loggingDays == 0)
                    {
                        //Одним из условий работы парсера является требование к оптимизации алгоритма
                        //получения новых объявлений, т.е. необходимо делать минимальное количество запросов
                        //к ресурсу. Для этого парсер должен использовать форму расширенного поиска, чтобы
                        //получать выборку нужных объявлений. Затем отсеивать объявления, которые были до
                        //IdRes и использовать дальнейшие ссылки только для новых объявлений.


                        Dictionary<RequestProperties, IEnumerable<Resource>> pairs =
                            resources.Select(
                                item =>
                                    new RequestProperties(item, Mapping))
                                .Distinct().ToDictionary(item => item,
                                    item =>
                                        resources.Where(
                                            r => ObjectComparer.Equals(r, item, Mapping)));

                        var value = new StackListQueue<Resource>();
                        foreach (var pair in pairs)
                            if (lastResourceDictionary.ContainsKey(pair.Key))
                                value.AddRange(
                                    pair.Value.Where(
                                        item => ResourceComparer.Compare(item, lastResourceDictionary[pair.Key]) >= 0));
                            else value.Add(pair.Value.Last());

                        resources.ReplaceAll(new StackListQueue<Resource> {value});
                        resources.Sort(ResourceComparer);
                    }

                    if (!resources.Any())
                    {
                        responseLevel = Math.Min(1, responseLevel);
                    }


                    Dictionary<RequestProperties, IEnumerable<Resource>> resourceDictionary =
                        resources.Select(
                            item => new RequestProperties(item, Mapping))
                            .Distinct().ToDictionary(item => item,
                                item =>
                                    resources.Where(
                                        r => ObjectComparer.Equals(r, item, Mapping)));

                    if (lastResourceDictionary.Any() && resources.Any() &&
                        lastResourceDictionary.Select(pair => resourceDictionary.ContainsKey(pair.Key) &&
                                                              ResourceComparer.Compare(
                                                                  resourceDictionary[pair.Key].First(), pair.Value) > 0)
                            .Aggregate(Boolean.And))
                    {
                        responseLevel = Math.Min(2, responseLevel);
                    }

                    if (lastResourceDictionary.Any() && resources.Any() &&
                        lastResourceDictionary.Select(pair => resourceDictionary.ContainsKey(pair.Key) &&
                                                              ResourceComparer.Equals(
                                                                  resourceDictionary[pair.Key].Last(),
                                                                  pair.Value)).Aggregate(Boolean.And))
                    {
                        responseLevel = Math.Min(3, responseLevel);
                    }

                    responseLevel = Math.Min(4, responseLevel);

                    if (!lastResourceDictionary.Any() && resources.Count > 1)
                    {
                        //При первом вызове метода парсера из DLL не известно о IdRes объявления внутри ресурса, поэтому он
                        //передает значение NULL. В этом случае парсер должен выбрать самое последнее объявление и
                        //вернуть его IdRes ресурса.
                        Debug.WriteLine("Remove {0} items", (resources.Count - 1));
                        resources.RemoveRange(0, resources.Count - 1);
                    }

                    if (lastResourceDictionary.Any() && resources.Any() &&
                        lastResourceDictionary.Select(pair => resourceDictionary.ContainsKey(pair.Key) &&
                                                              ResourceComparer.Equals(
                                                                  resourceDictionary[pair.Key].Last(),
                                                                  pair.Value)).Aggregate(Boolean.And))
                    {
                        Debug.WriteLine("Remove all items");
                        resources.Clear();
                    }


                    if (lastResourceDictionary.Any() &&
                        (resources.Count > MyDatabase.Database.ConvertTo<long>(SiteProperties.CountAd)) &&
                        lastResourceDictionary.Select(pair => resourceDictionary.ContainsKey(pair.Key) &&
                                                              ResourceComparer.Compare(
                                                                  resourceDictionary[pair.Key].First(), pair.Value) > 0)
                            .Aggregate(Boolean.And))
                    {
                        //Если IdRes не найден, то парсер возвращает последнии CountAD
                        //объявлений (такая ситуация может получится, если размещенное объявление к следующей
                        //итерации будет удалено на ресурсе)
                        Debug.WriteLine("Remove {0} items",
                            (int) (resources.Count - MyDatabase.Database.ConvertTo<long>(SiteProperties.CountAd)));
                        resources.RemoveRange(0,
                            (int) (resources.Count - MyDatabase.Database.ConvertTo<long>(SiteProperties.CountAd)));
                    }

                    if (lastResourceDictionary.Any() && resources.Any() &&
                        lastResourceDictionary.Select(pair => resourceDictionary.ContainsKey(pair.Key) &&
                                                              ResourceComparer.Equals(
                                                                  resourceDictionary[pair.Key].First(), pair.Value))
                            .Aggregate(Boolean.And))
                    {
                        Debug.WriteLine("Remove first item");
                        resources.Dequeue();
                    }

                    if (lastResourceDictionary.Any() && loggingDays > 0
                        && lastResourceDictionary.All(pair => Database.Exists(new Logging
                        {
                            SiteId = SiteProperties.SiteId,
                            ResponceResource = pair.Value.ToString()
                        })))
                    {
                        responseLevel = Math.Max(resources.Any() ? 4 : 3, responseLevel);
                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception.ToString());
                    LastError = exception;
                }
                Debug.WriteLine("resources.Count = " + resources.Count());
                var publications = new StackListQueue<WebPublication>();
                foreach (Resource resource in resources)
                {
                    Link url = dictionary[resource].Key;
                    if (PublicationHash.ContainsKey(url))
                    {
                        publications.Add(PublicationHash[url]);
                        continue;
                    }
                    try
                    {
                        var builder = new UriBuilder(Uri.Combine(BaseBuilder.Uri.ToString(), url.ToString()))
                        {
                            UserName = BaseBuilder.UserName,
                            Password = BaseBuilder.Password,
                        };
                        var urlValues = new Values(dictionary[resource].Value)
                        {
                            Url = new StackListQueue<string>(builder.Uri.ToString())
                        };
                        Uri.Default = builder.Uri;
                        IEnumerable<MemoryStream> streams =
                            await PublicationCrawler.WebRequest(builder.Uri);
                        Thread.Sleep(0);
                        ReturnFields returnFields = Parser.BuildReturnFields(streams,
                            urlValues, ReturnFieldInfos.ToList());
                        publications.Add(Parser.CreateWebPublication(returnFields, requestId));
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception.ToString());
                        LastError = exception;
                    }
                }

                if (!publications.Any()) responseLevel = Math.Min(3, responseLevel);

                PublicationHash.Clear();

                return new ParseResponse
                {
                    ResponseCode = responseCode[responseLevel],
                    LastPublicationId = string.Join("&", nextResources.Select(item => item.ToString())),
                    ModuleName = GetType().Name,
                    Publications = publications
                };
            }
            catch (NotAvailableDetectedException exeption)
            {
                Debug.WriteLine(exeption.ToString());
                responseLevel = Math.Min(0, responseLevel);
                return new ParseResponse
                {
                    ResponseCode = responseCode[responseLevel],
                    LastPublicationId = string.Join("&", lastResources.Select(item => item.ToString())),
                    ModuleName = GetType().Name,
                    Publications = new StackListQueue<WebPublication>()
                };
            }
        }

        /// <summary>
        ///     Получить названия ресурсов, обрабатываемая библиотекой
        ///     Sources – метод получает «рубрика, категория, регион», проверяет обрабатывается ли
        ///     такой набор исходных данных данной библиотекой и возвращает коллекцию названий
        ///     ресурсов (сайтов) в случае возможной обработки.
        /// </summary>
        /// <param name="bind">Бинд запроса</param>
        /// <returns> Коллекция названий ресурсов (сайтов)</returns>
        public IList<string> Sources(Bind bind)
        {
            Database.Connect();
            Mapping = Database.GetList(Database.MappingTable, Database.TableNameColumn).Select(item => item.ToString());

            var requestId = new RequestProperties();
            foreach (string table in Mapping)
            {
                PropertyInfo propertyInfo = bind.GetType().GetProperty(table + "Id");
                requestId.Add(table, Database.GetScalar(propertyInfo.GetValue(bind), table));
            }

            if (!requestId.Select(pair => pair.Value != null).Aggregate(Boolean.And))
                return new StackListQueue<string>();

            Mapping sites = Database.GetMapping(Database.SiteTable);

            return new MyLibrary.Collections.StackListQueue<string>(from site in sites
                where
                    requestId.Select(pair => Database.GetScalar(pair.Value, pair.Key, site.Key) != null)
                        .Aggregate(Boolean.And)
                select site.Value.ToString());
        }

        /// <summary>
        ///     Получить информацию о разработчике
        ///     About — возвращает информацию и координаты разработчика, а так же информацию о
        ///     передаче исключительных прав. Текст о передаче исключительных прав будет выслан
        ///     дополнительно.
        /// </summary>
        /// <returns>Информация о разработчике</returns>
        public AboutResponse About()
        {
            return new AboutResponse
            {
                Info = "Dmitry Protopopov",
                Contacts = "dmitry@protopopov.ru",
                CopyRight = "All reserved"
            };
        }

        /// <summary>
        ///     KeysRubrics – возвращает коллекцию «ИД рубрик» в структуре сервиса, которые умеет
        ///     обрабатывать библиотека
        /// </summary>
        /// <returns></returns>
        public IList<int> KeysRubrics()
        {
            Database.Connect();

            object siteId = Database.GetScalar(ModuleClassname, Database.SiteIdColumn,
                Database.ModuleClassnameColumn,
                Database.SiteTable);

            Mappings mappings = Database.GetMappings(siteId);

            return new MyLibrary.Collections.StackListQueue<int>(from rubric in mappings.Rubric.Keys
                select MyDatabase.Database.ConvertTo<int>(rubric));
        }

        /// <summary>
        ///     KeysRegions – возвращает коллекцию «ИД регионов» в структуре сервиса, которые умеет
        ///     обрабатывать библиотека
        /// </summary>
        /// <returns></returns>
        public IList<int> KeysRegions()
        {
            Database.Connect();

            object siteId = Database.GetScalar(ModuleClassname, Database.SiteIdColumn,
                Database.ModuleClassnameColumn,
                Database.SiteTable);

            Mappings mappings = Database.GetMappings(siteId);

            return new MyLibrary.Collections.StackListQueue<int>(from region in mappings.Region.Keys
                select MyDatabase.Database.ConvertTo<int>(region));
        }

        /// <summary>
        ///     KeysActions – возвращает коллекцию «ИД действий» в структуре сервиса, которые умеет
        ///     обрабатывать библиотека
        /// </summary>
        /// <returns></returns>
        public IList<int> KeysActions()
        {
            Database.Connect();

            object siteId = Database.GetScalar(ModuleClassname, Database.SiteIdColumn,
                Database.ModuleClassnameColumn,
                Database.SiteTable);

            Mappings mappings = Database.GetMappings(siteId);

            return new MyLibrary.Collections.StackListQueue<int>(from action in mappings.Action.Keys
                select MyDatabase.Database.ConvertTo<int>(action));
        }

        public Values ToValues()
        {
            return new Values(this);
        }

        #region

        public string ModuleClassname { get; set; }
        public string ModuleNamespace { get; set; }
        public Database Database { get; set; }
        public Transformation Transformation { get; set; }
        public Parser Parser { get; set; }
        public ComparerManager ComparerManager { get; set; }
        public CompressionManager CompressionManager { get; set; }
        public Crawler LookupCrawler { get; set; }
        public Crawler PublicationCrawler { get; set; }

        private ReturnFieldInfos ReturnFieldInfos { get; set; }
        private IPublicationComparer PublicationComparer { get; set; }
        private SiteProperties SiteProperties { get; set; }
        private Converter Converter { get; set; }
        private ResourceComparer ResourceComparer { get; set; }
        private LinkComparer LinkComparer { get; set; }
        private Dictionary<Link, WebPublication> PublicationHash { get; set; }
        private ObjectComparer ObjectComparer { get; set; }
        private IEnumerable<string> Mapping { get; set; }

        #endregion

        /// <summary>
        ///     Получить список биндов, обрабатываемая библиотекой
        /// </summary>
        /// <returns>Коллекция биндов</returns>
        public IList<Bind> Keys()
        {
            Database.Connect();

            object siteId = Database.GetScalar(ModuleClassname, Database.SiteIdColumn,
                Database.ModuleClassnameColumn,
                Database.SiteTable);

            Mappings mappings = Database.GetMappings(siteId);

            return new MyLibrary.Collections.StackListQueue<Bind>(from region in mappings.Region.Keys
                from rubric in mappings.Rubric.Keys
                from action in mappings.Action.Keys
                select new Bind
                {
                    RegionId = MyDatabase.Database.ConvertTo<int>(region),
                    RubricId = MyDatabase.Database.ConvertTo<int>(rubric),
                    ActionId = MyDatabase.Database.ConvertTo<int>(action)
                });
        }

        private class EndOfSearchDetectedException : Exception
        {
        }

        private class LoopDetectedException : Exception
        {
        }

        private class NotAvailableDetectedException : Exception
        {
        }
    }
}