namespace RealtyParser.Collections
{
    /// <summary>
    ///     Не входит в техническое задание
    /// </summary>
    public class HierarchicalItem : IValueable
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string ParentId { get; set; }
        public int Level { get; set; }

        public Values ToValues()
        {
            return new Values(this);
        }
    }
}