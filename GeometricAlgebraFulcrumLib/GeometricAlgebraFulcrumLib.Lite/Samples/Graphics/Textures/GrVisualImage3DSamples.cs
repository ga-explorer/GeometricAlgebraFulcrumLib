using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Images;
using SixLabors.ImageSharp.Formats.Bmp;

namespace GeometricAlgebraFulcrumLib.Lite.Samples.Graphics.Textures
{
    public static class GrVisualImage3DSamples
    {
        public static string WorkingPath { get; }
            = @"D:\Projects\Study\Babylon.js\Textures\";


        public static void Example1()
        {
            var composer = new GrVisualComputedImage3D("")
            {
                Width = 256,
                Height = 1,
                ColorFunc = (i, j) => Color.FromRgba(
                    (byte) (255 - i), 
                    (byte) (255 - i), 
                    (byte) (255 - i), 
                    (byte) (255 - i)
                )
            };

            composer.SaveImage(
                Path.Combine(WorkingPath, @"opacityTexture1.bmp"), 
                new BmpEncoder()
            );


            composer = new GrVisualComputedImage3D("")
            {
                Width = 1,
                Height = 256,
                ColorFunc = (i, j) => Color.FromRgba(
                    (byte) (255 - j), 
                    (byte) (255 - j), 
                    (byte) (255 - j), 
                    (byte) (255 - j)
                )
            };

            composer.SaveImage(
                Path.Combine(WorkingPath, @"opacityTexture2.bmp"), 
                new BmpEncoder()
            );
        }

        public static void Example2()
        {
            var composer = new GrVisualLineGridImage3D("")
            {
                BaseSquareColor = Color.LightYellow,
                BaseLineColor = Color.BurlyWood,
                MidLineColor = Color.SandyBrown,
                BorderLineColor = Color.SaddleBrown,
                BaseSquareCount = 3,
                BaseSquareSize = 64,
                BaseLineWidth = 2,
                MidLineWidth = 0,
                BorderLineWidth = 3
            };

            var filePath = Path.Combine(
                WorkingPath, 
                @"gridTexture4.bmp"
            );

            composer.SaveImage(
                filePath, 
                new BmpEncoder()
            );
        }
        
        public static void Example3()
        {
            var colorArray = new Color[3, 3];

            const int v1 = 63;
            const int v2 = 159;
            const int v3 = 255;

            colorArray[0, 0] = Color.FromRgb(v1, 0, 0);
            colorArray[1, 0] = Color.FromRgb(v2, 0, 0);
            colorArray[2, 0] = Color.FromRgb(v3, 0, 0);

            colorArray[0, 1] = Color.FromRgb(0, v1, 0);
            colorArray[1, 1] = Color.FromRgb(0, v2, 0);
            colorArray[2, 1] = Color.FromRgb(0, v3, 0);

            colorArray[0, 2] = Color.FromRgb(0, 0, v1);
            colorArray[1, 2] = Color.FromRgb(0, 0, v2);
            colorArray[2, 2] = Color.FromRgb(0, 0, v3);

            var composer = new GrVisualColorGridImage3D("")
            {
                ColorArray = colorArray,
                SquareSize = 1
            };
            
            composer.SaveImage(
                Path.Combine(WorkingPath, @"colorGridTexture1.bmp"), 
                new BmpEncoder()
            );
        }

        //public static void Example4()
        //{
        //    const string workingPath = @"D:\Projects\Study\Babylon.js\Textures\";

        //    @"\frac23"
        //        .CreateVisualLaTeXImage3D("LaTeX1")
        //        .SaveImage(Path.Combine(workingPath, "LaTeX1.png"));

        //    @"This $ e_1 + e_2 $ is an inline equation, while this: $$ e_1 + e_2 $$ is a display equation."
        //        .CreateVisualLaTeXImage3D(
        //            "LaTeX2",
        //            new GrCSharpMathLaTeXImageComposer()
        //            {
        //                RendererType = GrCSharpMathLaTeXRendererType.TextPainter,
        //                TextColor = Color.DarkBlue
        //            }
        //        ).SaveImage(Path.Combine(workingPath, "LaTeX2.png"));
        //}
    }
}
