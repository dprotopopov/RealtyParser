namespace RealtyParser
{
    public class Resource : RequestProperties
    {
        public Resource(string s) : base(s)
        {
            Text = s;
        }

        private string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}