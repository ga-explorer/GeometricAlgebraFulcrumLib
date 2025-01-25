using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics.PovRay;

public static class RotorFamilySamples
{
    public static void PovRayExample()
    {
        const int frameCount = 501;
        const double timeLength = 20; // Seconds

        var frameSpecs = Float64SamplingSpecs.CreateFromTimeLength(frameCount, timeLength);

        var sourceVector = LinFloat64Vector3D.E3;
        var targetVector = LinFloat64Vector3D.Create(1, 1, 1).ToUnitLinVector3D();

        const double thetaEpsilon = 5e-5d;
        
        var visualizer = new GrPovRayRotorFamilyVisualizer(
            @"D:\Projects\Study\POV-Ray\RotorFamily\",
            frameSpecs,
            sourceVector,
            targetVector
        )
        {
            DefaultRenderingOptions =
            {
                Width = 1280 / 4, //800
                Height = 720 / 4, //450
                AntiAlias = true,
                AntiAliasDepth = 6,
                MaxImageBufferMemory = 10240,
                Quality = 9,
                OutputFileType = GrPovRayOutputFileTypeValue.Png
            },

            ShowGrid = false,
            ShowCopyright = false,
            ShowGuiLayer = false,

            Title = "Rotor Family of Two Vectors",
            
            AxesOrigin = LinFloat64Vector3D.Create(0, -7, 0),
            GridUnitCount = 20,
            LaTeXScalingFactor = 1d / 60d,
            DrawRotorTrace = false,

            ComposeSceneFilesEnabled = true,
            RenderImageFilesEnabled = true,
            RenderGifFileEnabled = false,
            RenderVideoFileEnabled = true
        };

        
        var cameraAlphaValues =
            TemporalFloat64Scalar
                .Ramp()
                .Repeat(
                    1, 
                    0, 
                    1, 
                    0, 
                    360.DegreesToRadians()
                );

        var cameraBetaValues =
            TemporalFloat64Scalar
                .SmoothRectangle()
                .Repeat(
                    4, 
                    0, 
                    1, 
                    20.DegreesToRadians(), 
                    70.DegreesToRadians()
                );
        
        //var cameraDistanceValues =
        //    TemporalFloat64Scalar
        //        .Constant(16, 0, 1);

        visualizer.SetCameraAlphaBetaDistance(
            cameraAlphaValues, 
            cameraBetaValues, 
            16
        );

        visualizer.RenderFiles();
    }

}