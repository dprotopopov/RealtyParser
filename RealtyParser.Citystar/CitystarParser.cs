using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.Citystar
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "CitystarParser")]
    public sealed class CitystarParser : ParserModule
    {
        public CitystarParser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}