using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.Kvadrat66
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "Kvadrat66Parser")]
    public sealed class Kvadrat66Parser : ParserModule
    {
        public Kvadrat66Parser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}