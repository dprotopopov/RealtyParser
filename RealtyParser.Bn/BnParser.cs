using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.Bn
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "BnParser")]
    public sealed class BnParser : ParserModule
    {
        public BnParser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}