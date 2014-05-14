using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.Gdeetotdom
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "GdeetotdomParser")]
    public sealed class GdeetotdomParser : ParserModule
    {
        public GdeetotdomParser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}