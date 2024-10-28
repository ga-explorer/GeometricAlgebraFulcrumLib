using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics.SkiaSharp;

public static class LaTeXSamples
{
    public static void SaveStream(this Stream data, string filePath)
    {
        using var fileStream = new FileStream(
            filePath,
            FileMode.Create,
            FileAccess.Write
        );

        data.CopyTo(fileStream);
    }

    public static void Example1()
    {
        @"\frac23"
            .CreateVisualLaTeXImage3D()
            .SaveImage(
                @"D:\Projects\Study\Babylon.js\Textures\LaTeX1.png"
            );

        @"This $ e_1 + e_2 $ is an inline equation, while this: $$ e_1 + e_2 $$ is a display equation."
            .CreateVisualLaTeXImage3D()
            .SaveImage(
                @"D:\Projects\Study\Babylon.js\Textures\LaTeX2.png"
            );
    }
}