using PdfSharp.Fonts;
using System.IO;

namespace EsteknikCRM1.Services.Pdf
{
    public class WindowsFontResolver : IFontResolver
    {
        public byte[] GetFont(string faceName)
        {
            string fontPath = @"C:\Windows\Fonts\arial.ttf";

            if (faceName == "Arial#Bold")
                fontPath = @"C:\Windows\Fonts\arialbd.ttf";

            return File.ReadAllBytes(fontPath);
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (isBold)
                return new FontResolverInfo("Arial#Bold");

            return new FontResolverInfo("Arial#Regular");
        }
    }
}