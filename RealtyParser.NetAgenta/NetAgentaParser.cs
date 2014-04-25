using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.NetAgenta
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "NetAgentaParser")]
    public sealed class NetAgentaParser : ParserModule
    {
        public NetAgentaParser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}