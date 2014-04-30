using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.Arenda66
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "Arenda66Parser")]
    public sealed class Arenda66Parser : ParserModule
    {
        public Arenda66Parser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}