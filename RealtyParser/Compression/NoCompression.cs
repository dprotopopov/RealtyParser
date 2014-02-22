using System.IO;

namespace RealtyParser.Compression
{
    public class NoCompression : ICompression
    {
        public void Decompress(Stream input, Stream output)
        {
            input.CopyTo(output);
        }
    }
}