using System.IO;

namespace RealtyParser.Compression
{
    public interface ICompression
    {
        void Decompress(Stream input, Stream output);
    }
}