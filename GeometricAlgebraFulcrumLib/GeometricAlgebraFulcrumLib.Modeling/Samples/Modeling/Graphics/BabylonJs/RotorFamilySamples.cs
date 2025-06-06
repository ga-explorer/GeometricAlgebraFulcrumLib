﻿using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics.BabylonJs;

public static class RotorFamilySamples
{
    public static void BabylonJsExample()
    {
        const int frameCount = 101;

        var sourceVector = LinFloat64Vector3D.E3;
        var targetVector = LinFloat64Vector3D.Create(1, 1, 1).ToUnitLinVector3D();


        const double thetaEpsilon = 5e-5d;
        var thetaValues =
            (-90d + thetaEpsilon).GetCosRange(90d - thetaEpsilon, frameCount, 1, false)
            .Select(t => (LinFloat64Angle)t.DegreesToDirectedAngle())
            .ToImmutableArray();

        var visualizer = new GrBabylonJsRotorFamilyVisualizer(
            @"D:\Projects\Study\Web\Babylon.js\",
            Float64SamplingSpecs.CreateFromSamplingRate(frameCount, 50), 
            sourceVector,
            targetVector,
            thetaValues
        )
        {
            ShowCopyright = true,
            ShowGuiLayer = true,

            SceneTitle = "Rotor Family of Two Vectors",
            HostUrl = "http://127.0.0.1:5500/", //"http://localhost:5200/",
            //LiveReloadWebServer "D:/Projects/Study/Babylon.js/" --port 5200 --UseSsl False --LiveReloadEnabled False --OpenBrowser True

            GridUnitCount = 20,
            LaTeXScalingFactor = 1d / 72d,
            DrawRotorTrace = true,

            ComposeSceneFilesEnabled = true,
            SceneRenderMethod = GrVisualSceneSequenceComposer.RenderImageFilesMethod.PerScene,
            RenderGifFileEnabled = false,
            RenderVideoFileEnabled = false
        };

        visualizer.SetCanvas(
            1280, // 3840 - 1080,
            720 //2160,
        );

        visualizer.SetCamera(
            120d.DegreesToRadians(),
            75d.DegreesToRadians(),
            20
        );

        visualizer.ComposeSceneSequence();
    }
    
    public static void Example2()
    {
        const string imageFolderIn1 =
            @"D:\Projects\Study\Web\Babylon.js\Animations\Images\Rotor Family of Two Vectors\00\";

        const string imageFolderIn2 =
            @"D:\Projects\Study\Web\Babylon.js\Animations\Images\Rotor Family of Two Vectors\01\";

        const string imageFolderIn3 =
            @"D:\Projects\Study\Web\Babylon.js\Animations\Images\Rotor Family of Two Vectors\10\";

        const string imageFolderOut =
            @"D:\Projects\Study\Web\Babylon.js\Animations\Images\";

        const int imageCount = 1001;

        for (var i = 0; i < imageCount; i++)
        {
            var imageFileName = @$"Frame-{i:D6}.png";

            var imageIn1 = Image.Load(Path.Combine(imageFolderIn1, imageFileName));
            var imageIn2 = Image.Load(Path.Combine(imageFolderIn2, imageFileName));
            var imageIn3 = Image.Load(Path.Combine(imageFolderIn3, imageFileName));

            //imageIn1.Mutate(c=>
            //    c.Resize(imageIn1.Width / 2, imageIn1.Height / 2)
            //);

            //imageIn2.Mutate(c=>
            //    c.Resize(imageIn2.Width / 2, imageIn2.Height / 2)
            //);

            var imageOut = new Image<Rgba32>(
                imageIn3.Width + imageIn1.Width,
                imageIn3.Height
            );

            imageOut.Mutate(c =>
                c.DrawImage(imageIn1, new Point(imageIn3.Width, 0), 1f)
            );

            imageOut.Mutate(c =>
                c.DrawImage(imageIn2, new Point(imageIn3.Width, imageIn1.Height), 1f)
            );

            imageOut.Mutate(c =>
                c.DrawImage(imageIn3, new Point(0, 0), 1f)
            );

            imageOut.Save(Path.Combine(imageFolderOut, imageFileName));

            if (i % 10 == 0)
            {
                Console.WriteLine($"Finished processing frame {i}");
            }
        }
    }

    public static void Example3()
    {
        const string imageFolderIn1 =
            @"D:\Projects\Study\Web\Babylon.js\Animations\Images\Rotor Family of Two Vectors\00\";

        const string imageFolderIn2 =
            @"D:\Projects\Study\Web\Babylon.js\Animations\Images\Rotor Family of Two Vectors\01\";

        const string imageFolderIn3 =
            @"D:\Projects\Study\Web\Babylon.js\Animations\Images\Rotor Family of Two Vectors\10\";

        const string imageFolderOut =
            @"D:\Projects\Study\Web\Babylon.js\Animations\Images\";

        const int imageCount = 1001;

        for (var i = 0; i < imageCount; i++)
        {
            var imageFileName = @$"Frame-{i:D6}.png";

            var imageIn1 = Image.Load(Path.Combine(imageFolderIn1, imageFileName));
            var imageIn2 = Image.Load(Path.Combine(imageFolderIn2, imageFileName));
            var imageIn3 = Image.Load(Path.Combine(imageFolderIn3, imageFileName));

            imageIn1.Mutate(c =>
                c.Resize(imageIn3.Width / 2, imageIn3.Width / 2)
            );

            imageIn2.Mutate(c =>
                c.Resize(imageIn3.Width / 2, imageIn3.Width / 2)
            );

            var imageOut = new Image<Rgba32>(
                imageIn3.Width,
                imageIn3.Height + imageIn1.Height
            );

            imageOut.Mutate(c =>
                c.DrawImage(imageIn1, new Point(0, imageIn3.Height), 1f)
            );

            imageOut.Mutate(c =>
                c.DrawImage(imageIn2, new Point(imageIn1.Width, imageIn3.Height), 1f)
            );

            imageOut.Mutate(c =>
                c.DrawImage(imageIn3, new Point(0, 0), 1f)
            );

            imageOut.Save(Path.Combine(imageFolderOut, imageFileName));

            if (i % 10 == 0)
            {
                Console.WriteLine($"Finished processing frame {i}");
            }
        }
    }
}