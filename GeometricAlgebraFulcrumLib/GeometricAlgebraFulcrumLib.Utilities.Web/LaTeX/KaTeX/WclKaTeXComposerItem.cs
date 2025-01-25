using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX;

public sealed class WclKaTeXComposerItem
{
    public string Key { get; }

    public string LaTeXCode { get; }

    public string HtmlCode { get; internal set; }

    public  Image PngImage { get; internal set; }

    public string ImageFileName 
        => Key + ".png";

    public bool ContainsImage 
        => PngImage.Width > 1 && PngImage.Height > 1;


    internal WclKaTeXComposerItem(string key, string laTeXCode)
    {
        Key = key;
        LaTeXCode = laTeXCode;
        PngImage = new Image<Rgba32>(1, 1);
    }


    public void ClearImage()
    {
        PngImage = new Image<Rgba32>(1, 1);
    }
}