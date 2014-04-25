using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.BeBoss
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "BeBossParser")]
    public sealed class BeBossParser : ParserModule
    {
        public BeBossParser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}