﻿using System;
using System.Diagnostics;
using RealtyParser.Compression;

namespace RealtyParser.Managers
{
    public class CompressionManager
    {
        public CompressionManager()
        {
            ModuleNamespace = GetType().Namespace;
        }

        public string ModuleNamespace { get; set; }

        /// <summary>
        ///     Создание экземпляра указанного класса, реализующего интерфейс ICompression
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public ICompression CreateCompression(string className)
        {
            string fullClassName = string.Format("{0}.Compression.{1}", ModuleNamespace, className);
            Debug.WriteLine(fullClassName);
            Type type = Type.GetType(fullClassName);
            if (type != null)
                return
                    Activator.CreateInstance(type) as
                        ICompression;
            throw new NotImplementedException();
        }
    }
}