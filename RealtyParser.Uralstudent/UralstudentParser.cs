using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.Uralstudent
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "UralstudentParser")]
    public sealed class UralstudentParser : ParserModule
    {
        public UralstudentParser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}