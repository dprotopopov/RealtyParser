using System.Linq;

namespace RealtyParser.Types
{
    public static class Type
    {
        public static bool IsKindOf(System.Type[] types, System.Type[] profile)
        {
            return (types.Length == profile.Length) &&
                   types.Select((type, index) => IsKindOf(type, profile[index])).Aggregate((i, j) => i && j);
        }

        public static bool IsKindOf(System.Type type, System.Type profile)
        {
            return profile.IsAssignableFrom(type);
        }
    }
}