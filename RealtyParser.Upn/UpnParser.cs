using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.Upn
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "UpnParser")]
    public sealed class UpnParser : ParserModule
    {
        public UpnParser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}