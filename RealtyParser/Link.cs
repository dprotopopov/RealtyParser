namespace RealtyParser
{
    public class Link : MyParser.Link, IValueable
    {
        public Link(string s) : base(s)
        {
        }

        public new Values ToValues()
        {
            return new Values(this);
        }
    }
}