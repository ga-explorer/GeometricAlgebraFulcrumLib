using GraphicsComposerLib.Rendering.LaTeX.ImageComposers;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Images;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D
{
    public static class GrVisualSpace3DUtils
    {
        public static GrVisualLaTeXImage3D CreateVisualLaTeXImage3D(this string latexCode, string name = "")
        {
            return new GrVisualLaTeXImage3D(
                name,
                latexCode,
                GrCSharpMathLaTeXImageComposer.DefaultMathComposer
            );
        }

        public static GrVisualLaTeXImage3D CreateVisualLaTeXImage3D(this string latexCode, string name, IGrLaTeXImageComposer imageComposer)
        {
            return new GrVisualLaTeXImage3D(
                name, 
                latexCode, 
                imageComposer
            );
        }

        public static GrVisualLaTeXImage3D CreateVisualLaTeXImage3D(this IGrLaTeXImageComposer imageComposer, string latexCode, string name)
        {
            return new GrVisualLaTeXImage3D(
                name, 
                latexCode, 
                imageComposer
            );
        }

    }
}
