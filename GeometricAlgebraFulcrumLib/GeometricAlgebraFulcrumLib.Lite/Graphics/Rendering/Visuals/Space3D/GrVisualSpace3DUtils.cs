using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Images;
using WebComposerLib.LaTeX.ImageComposers;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D
{
    public static class GrVisualSpace3DUtils
    {
        public static GrVisualLaTeXImage3D CreateVisualLaTeXImage3D(this string latexCode, string name = "")
        {
            return new GrVisualLaTeXImage3D(
                name,
                latexCode,
                WclCSharpMathLaTeXImageComposer.DefaultMathComposer
            );
        }

        public static GrVisualLaTeXImage3D CreateVisualLaTeXImage3D(this string latexCode, string name, IWclLaTeXImageComposer imageComposer)
        {
            return new GrVisualLaTeXImage3D(
                name, 
                latexCode, 
                imageComposer
            );
        }

        public static GrVisualLaTeXImage3D CreateVisualLaTeXImage3D(this IWclLaTeXImageComposer imageComposer, string latexCode, string name)
        {
            return new GrVisualLaTeXImage3D(
                name, 
                latexCode, 
                imageComposer
            );
        }

    }
}
