using System.Collections.Generic;

namespace RealtyParser.Comparer
{
    public interface IPublicationComparer : IEqualityComparer<string>, IComparer<string>
    {
        bool IsValid(string s);
    }
}