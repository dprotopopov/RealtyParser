using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.Rosrealt
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "RosrealtParser")]
    public sealed class RosrealtParser : ParserModule
    {
        public RosrealtParser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}