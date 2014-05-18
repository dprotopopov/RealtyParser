using System.ComponentModel.Composition;
using RT.ParsingLibs;

namespace RealtyParser.Mail
{
    [Export(typeof (IParsingModule))]
    [ExportMetadata("Name", "MailParser")]
    public sealed class MailParser : ParserModule
    {
        public MailParser()
        {
            ModuleClassname = GetType().Name;
            Database.ModuleClassname = ModuleClassname;
        }
    }
}