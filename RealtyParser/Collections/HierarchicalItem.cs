namespace RealtyParser.Collections
{
    /// <summary>
    ///     Не входит в техническое задание
    /// </summary>
    public class HierarchicalItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string ParentId { get; set; }
        public int Level { get; set; }
    }
}