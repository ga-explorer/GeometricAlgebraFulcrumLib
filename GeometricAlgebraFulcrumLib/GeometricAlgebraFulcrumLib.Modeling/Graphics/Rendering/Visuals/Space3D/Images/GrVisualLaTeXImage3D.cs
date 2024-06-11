using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.ImageComposers;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Images;

public class GrVisualLaTeXImage3D :
    GrVisualImage3D
{
    public IWclLaTeXImageComposer ImageComposer { get; }

    public string LaTeXCode { get; }
    

    public GrVisualLaTeXImage3D(string name, string latexCode, IWclLaTeXImageComposer imageComposer)
        : base(name)
    {
        LaTeXCode = latexCode;
        ImageComposer = imageComposer;
    }

        
    public override bool IsValid()
    {
        throw new NotImplementedException();
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