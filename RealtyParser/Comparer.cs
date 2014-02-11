using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RealtyParser
{
    public static class Comparer
    {
        /// <summary>
        ///     Создание экземпляра указанного класса, реализующего интерфейс IComparer
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public static IComparer<string> CreatePublicationComparer(string className)
        {
            try
            {
                string moduleNamespace = typeof (RealtyParserParsingModule).Namespace;
                string fullClassName = System.String.Format("{0}.PublicationComparer.{1}", moduleNamespace, className);
                Debug.WriteLine(fullClassName);
                Type type = Type.GetType(fullClassName);
                if (type != null)
                    return
                        Activator.CreateInstance(type) as
                            IComparer<string>;
                throw new NotImplementedException();
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}