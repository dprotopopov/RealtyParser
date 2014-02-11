namespace RealtyParser
{
    public static class Regex
    {
        public static string Escape(string text)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"(\{|\[|\]|\})");
            return regex.Replace(text, @"\$&").Trim();
        }
    }
}