using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.Mirkvartir
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "MirkvartirParser")]
    public sealed class MirkvartirParser : ParserModule
    {
        public MirkvartirParser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}