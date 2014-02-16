namespace RealtyParser
{
    public static class Type
    {
        public static bool IsKindOf(System.Type[] types, System.Type[] profile)
        {
            if (types.Length != profile.Length) return false;
            for (int i = 0; i < types.Length && i < profile.Length; i++)
                if (!profile[i].IsAssignableFrom(types[i]))
                    return false;
            return true;
        }
    }
}