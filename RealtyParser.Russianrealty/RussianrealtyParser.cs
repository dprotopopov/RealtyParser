using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.Russianrealty
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "RussianrealtyParser")]
    public sealed class RussianrealtyParser : ParserModule
    {
        public RussianrealtyParser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}