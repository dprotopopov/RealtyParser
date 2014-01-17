﻿using System.Collections.Generic;
using System.Reflection;

namespace RealtyParser
{
    public class Arguments : Dictionary<string, string>
    {
        public Arguments(Arguments args)
        {
            InsertOrReplaceArguments(args);
        }

        public Arguments()
        {
        }

        public Arguments InsertOrReplaceArguments(Dictionary<string, string> dictionary)
        {
            foreach (var arg in dictionary)
                if (ContainsKey(arg.Key))
                    this[arg.Key] = arg.Value;
                else
                    Add(arg.Key, arg.Value);
            return this;
        }

        public string RegionId
        {
            get
            {
                string propertyName = @"\{\{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"\}\}";
                if (!ContainsKey(propertyName)) Add(propertyName, "");
                return this[propertyName];
            }
            set
            {
                string propertyName = @"\{\{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"\}\}";
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }
        public string RubricId
        {
            get
            {
                string propertyName = @"\{\{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"\}\}";
                if (!ContainsKey(propertyName)) Add(propertyName, "");
                return this[propertyName];
            }
            set
            {
                string propertyName = @"\{\{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"\}\}";
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }
        public string ActionId
        {
            get
            {
                string propertyName = @"\{\{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"\}\}";
                if (!ContainsKey(propertyName)) Add(propertyName, "");
                return this[propertyName];
            }
            set
            {
                string propertyName = @"\{\{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"\}\}";
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }
        public string PublicationId
        {
            get
            {
                string propertyName = @"\{\{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"\}\}";
                if (!ContainsKey(propertyName)) Add(propertyName, "");
                return this[propertyName];
            }
            set
            {
                string propertyName = @"\{\{" + MethodBase.GetCurrentMethod().Name.Substring(4) + @"\}\}";
                if (ContainsKey(propertyName))
                    this[propertyName] = value;
                else
                    Add(propertyName, value);
            }
        }
    }
}
