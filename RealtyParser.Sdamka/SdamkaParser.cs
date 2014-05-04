using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.Sdamka
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "SdamkaParser")]
    public sealed class SdamkaParser : ParserModule
    {
        public SdamkaParser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}