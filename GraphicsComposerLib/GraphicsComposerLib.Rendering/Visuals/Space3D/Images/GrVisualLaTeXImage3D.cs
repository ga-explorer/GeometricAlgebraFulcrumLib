using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.LaTeX.ImageComposers;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Images;

public class GrVisualLaTeXImage3D :
    GrVisualImage3D
{
    public IGrLaTeXImageComposer ImageComposer { get; }

    public string LaTeXCode { get; }
    

    public GrVisualLaTeXImage3D(string name, [NotNull] string latexCode, [NotNull] IGrLaTeXImageComposer imageComposer)
        : base(name)
    {
        LaTeXCode = latexCode;
        ImageComposer = imageComposer;
    }


    public override Pair<int> GetSize()
    {
        var image = GetImage();

        return new Pair<int>(image.Width, image.Height);
    }

    public override Image GetImage()
    {
        return ImageComposer.RenderToPngImage(LaTeXCode);
    }
}