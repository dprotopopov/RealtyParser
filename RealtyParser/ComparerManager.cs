using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RealtyParser
{
    public class ComparerManager
    {
        public ComparerManager()
        {
            ModuleNamespace = GetType().Namespace;
        }

        public string ModuleNamespace { get; set; }

        /// <summary>
        ///     Создание экземпляра указанного класса, реализующего интерфейс IComparer
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public IComparer<string> CreatePublicationComparer(string className)
        {
            string fullClassName = string.Format("{0}.Comparer.{1}", ModuleNamespace, className);
            Debug.WriteLine(fullClassName);
            Type type = Type.GetType(fullClassName);
            if (type != null)
                return
                    Activator.CreateInstance(type) as
                        IComparer<string>;
            throw new NotImplementedException();
        }
    }
}