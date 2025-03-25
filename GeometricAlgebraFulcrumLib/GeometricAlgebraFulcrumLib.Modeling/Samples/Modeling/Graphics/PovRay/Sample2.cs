using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics.PovRay;

public static class Sample2
{
    public static void Example1()
    {
        var workingFolder = @"D:\Projects\Study\POV-Ray\Samples";

        var imageWidth = 1024;
        var imageHeight = 768;
        var gridWidth = 10;
        var cameraDistance = 10;

        var widthToHeightRatio = imageWidth / (double)imageHeight;
        var heightToWidthRatio = imageHeight / (double)imageWidth;

        var composer = new GrPovRaySceneComposer(workingFolder)
        {
            RenderingOptions =
            {
                AntiAlias = true,
                AntiAliasDepth = 6,
                Width = imageWidth,
                Height = imageHeight,
                Quality = 9,
                MaxImageBufferMemory = 10240,
                OutputFileType = GrPovRayOutputFileTypeValue.Png,
                Display = true
            }
        };

        composer
            .AddDefaultSkySphere()
            .AddDefaultAxes(LinFloat64Vector3D.Zero)
            .AddDefaultGridZx(gridWidth, 1, 0, 1);

        composer.AddDefaultOrthographicCamera(
            cameraDistance,
            LinFloat64PolarAngle.Angle0,
            LinFloat64PolarAngle.Angle0,
            gridWidth, 
            gridWidth * heightToWidthRatio
        );

        composer.AddStatement(
            GrPovRayLightSource.PointLight(
                cameraDistance * LinFloat64Vector3D.E2, 
                Color.NavajoWhite
            )
        );


        var imageFileName = "thetaRotorPlaneText.png"; // "phiMinRotorText.png";

        var image = Image.Load<Rgba32>(
            workingFolder.GetFilePath(imageFileName)
        );

        var baseHeight = 32;
        var rectHeight = 0.5;

        var width = image.Width;
        var height = image.Height;

        var heightRatio = rectHeight * (height / (double)baseHeight);

        var pigment = new GrPovRayImageMapPigment(
            imageFileName, 
            GrPovRayImageMapBitmapType.Png
        );

        pigment.AffineMap.Translate(-0.5, -0.5, 0);
        pigment.Properties.Once = true;
        
        var rect = 
            GrPovRayObject.RectanglePolygonZx(1, 1);

        rect.SetMaterial(pigment);

        rect.AffineMap.Scale(1, 1, width / (double) height).Scale(heightRatio);

        rect.AffineMap.TranslateY(0.5);

        composer.AddStatement(rect);

        var sceneCode = composer.SceneObject.GetPovRayCode();
        var optionsCode = composer.RenderingOptions.GetPovRayCode();

        File.WriteAllText(
            workingFolder.GetFilePath("Sample2.pov"), 
            sceneCode
        );
        
        File.WriteAllText(
            workingFolder.GetFilePath("Sample2.ini"), 
            optionsCode
        );

        Console.WriteLine(sceneCode);
        Console.WriteLine();
    }
}