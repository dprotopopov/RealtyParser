using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.Egent
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "EgentParser")]
    public sealed class EgentParser : ParserModule
    {
        public EgentParser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}