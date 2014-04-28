using MyLibrary.Attribute;

namespace RealtyParser.Collections
{
    /// <summary>
    ///     Не входит в техническое задание
    /// </summary>
    public class HierarchicalItem : IValueable
    {
        [Value]
        public string Key { get; set; }

        [Value]
        public string Value { get; set; }

        [Value]
        public string ParentId { get; set; }

        [Value]
        public int Level { get; set; }

        public Values ToValues()
        {
            return new Values(this);
        }
    }
}