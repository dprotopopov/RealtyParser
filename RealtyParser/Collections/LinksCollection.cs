﻿using System.Collections.Generic;
using RealtyParser.Types;

namespace RealtyParser.Collections
{
    /// <summary>
    ///     Вспомогательный класс
    ///     Используется для доступа к значениям словаря по ключу
    /// </summary>
    public class LinksCollection : Dictionary<string, string>, IValueable
    {
        public Values ToValues()
        {
            return new Values(this);
        }

        public override string ToString()
        {
            return String.Parse(new Transformation().ParseTemplate(new Values(Keys, Values)));
        }
    }
}