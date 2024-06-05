using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.ImageComposers;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.GraphViz.Dot.Image;

public sealed class DotGraphLaTeXImage : 
    IDotGraphImage
{
    public static WclLaTeXImageComposer LaTeXRenderer { get; }
        = new WclLaTeXImageComposer()
        {
            Resolution = 150
        };

    public string ImageId { get; }

    public string ImageFileName
        => ImageId + ".png";

    public bool IsGenerated 
        => true;

    public double Resolution { get; set; } = 96;

    public string LaTeXCode { get; set; }


    internal DotGraphLaTeXImage(string imageId)
    {
        ImageId = imageId;
    }


    public SixLabors.ImageSharp.Image GetImage()
    {
        return LaTeXRenderer.RenderToPngImage(LaTeXCode);
    }


    public override string ToString()
    {
        return LaTeXCode;
    }
}