namespace RealtyParser
{
    public class Link
    {
        public Link(string s)
        {
            Url = s;
        }

        private string Url { get; set; }

        public override string ToString()
        {
            return Url;
        }
    }
}