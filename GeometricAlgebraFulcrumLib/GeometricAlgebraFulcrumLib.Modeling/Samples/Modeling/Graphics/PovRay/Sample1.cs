using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using Instances;
using SixLabors.ImageSharp;
using Xabe.FFmpeg;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics.PovRay
{
    public static class Sample1
    {
        public static void Example1()
        {
            var angleList = 
                0d.GetLinearPeriodicRange(
                    2 * Math.PI, 72
                ).ToImmutableArray();

            var frameCount = angleList.Length;

            var v1 = LinFloat64Vector3D.Create(2, 5, -10);
            
            for (var frameIndex = 0; frameIndex < angleList.Length; frameIndex++)
            {
                var angle = angleList[frameIndex];

                var filePath = @$"D:\Projects\Study\POV-Ray\Samples\Frame-{frameIndex:D6}.pov";

                Console.WriteLine($"Generating code for frame {frameIndex} of {frameCount}");

                var scene = new GrPovRayScene($"Frame-{frameIndex:D6}");

                scene.AtmosphericBackground(Color.Beige);

                scene.GlobalSettings(
                    new GrPovRayGlobalSettingsProperties
                    {
                        AmbientLight = Color.DarkGray
                    }
                );

                //var location = v1.YRotateBy(angle.RadiansToPolarAngle());
                
                scene.Camera = GrPovRayPerspectiveCamera.ArcRotatePerspective(
                    angle.RadiansToPolarAngle(), 
                    LinFloat64PolarAngle.Angle60,
                    1,
                    LinFloat64Vector3D.E2 * 1.001,
                    67.DegreesToPolarAngle(),
                    4d/3d
                );
                
                //camera.Transforms.Translate()

                scene.AddStatement(
                    GrPovRayLightSource.PointLight(
                        LinFloat64Vector3D.Create(0, 0, 5),
                        Color.White
                    )
                );

                scene.AddStatement(
                    GrPovRayLightSource.PointLight(
                        LinFloat64Vector3D.Create(5, 5, 5),
                        Color.White
                    )
                );

                //scene.Plane(
                //    LinFloat64Vector3D.E2, 
                //    0
                //);

                scene.Statements.DeclareFinish(
                    "baseFinish",
                    new GrPovRayFinishProperties
                    {
                        DiffuseAmount = 1,
                        SpecularHighlightAmount = 1
                    }
                );

                var box = GrPovRayObject.Box(
                    LinFloat64Vector3D.Create(-1, -1, -1),
                    LinFloat64Vector3D.Create(1, 1, 1)
                ).SetMaterial(Color.OrangeRed, "baseFinish");

                scene.AddStatement(box);

                var xAxis = GrPovRayObject.Cylinder(
                    LinFloat64Vector3D.Zero,
                    LinFloat64Vector3D.E1 * 3,
                    0.075,
                    false
                ).SetMaterial(Color.DarkRed, "baseFinish");

                scene.AddStatement(xAxis);

                var yAxis = GrPovRayObject.Cylinder(
                    LinFloat64Vector3D.Zero,
                    LinFloat64Vector3D.E2 * 3,
                    0.075,
                    false
                ).SetMaterial(Color.DarkGreen, "baseFinish");

                scene.AddStatement(yAxis);

                var zAxis = GrPovRayObject.Cylinder(
                    LinFloat64Vector3D.Zero,
                    LinFloat64Vector3D.E3 * 3,
                    0.075,
                    false
                ).SetMaterial(Color.DarkBlue, "baseFinish");

                scene.AddStatement(zAxis);

                var code = scene.GetPovRayCode();

                File.WriteAllText(
                    filePath, 
                    code
                );

                var options = new GrPovRayRenderingOptions().GetPovRayCode();

                File.WriteAllText(
                    filePath.Replace(".pov", ".ini"), 
                    options
                );
            }

            Console.WriteLine();
            
            for (var frameIndex = 0; frameIndex < frameCount; frameIndex++)
            {
                var sceneFilePath = @$"D:\Projects\Study\POV-Ray\Samples\Frame-{frameIndex:D6}.pov";
                var optionsFilePath = @$"D:\Projects\Study\POV-Ray\Samples\Frame-{frameIndex:D6}.ini";
                var imageFilePath = @$"D:\Projects\Study\POV-Ray\Samples\Frame-{frameIndex:D6}.png";

                if (File.Exists(imageFilePath))
                    File.Delete(imageFilePath);

                Console.Write($"Rendering frame {frameIndex} of {frameCount}.. ");

                using var instance = Instance.Start(
                    @"C:\Program Files\POV-Ray\v3.7\bin\pvengine.exe", 
                    @$"/NORESTORE ""{optionsFilePath}"" /EXIT /RENDER ""{sceneFilePath}"""
                );

                var result = instance.WaitForExit();

                Console.WriteLine("done");
            }

            var fileList = new List<string>(
                frameCount.MapRange(i => 
                    @$"D:\Projects\Study\POV-Ray\Samples\Frame-{i:D6}.png"
                )
            );

            FFmpeg.SetExecutablesPath(@"D:\ffmpeg\bin");

            var videoFilePath = @"D:\Projects\Study\POV-Ray\Samples\Scene.mp4";

            if (File.Exists(videoFilePath))
                File.Delete(videoFilePath);

            new Conversion()
                .SetInputFrameRate(frameCount / 4d)
                .BuildVideoFromImages(fileList)
                //.SetFrameRate(1)
                .SetPixelFormat(PixelFormat.yuv420p)
                .SetOutput(videoFilePath)
                .Start();

            Console.WriteLine();
        }

        public static void Example2()
        {
            var fileList = new List<string>(
                360.MapRange(i => 
                    @$"D:\Projects\Study\POV-Ray\Samples\Frame-{i:D6}.png"
                )
            );

            FFmpeg.SetExecutablesPath(@"D:\ffmpeg\bin");

            new Conversion()
                .SetInputFrameRate(18)
                .BuildVideoFromImages(fileList)
                .SetFrameRate(18)
                .SetPixelFormat(PixelFormat.yuv420p)
                .SetOutput(@"D:\Projects\Study\POV-Ray\Samples\Scene.mp4")
                .Start();
        }

        public class GrPovRay : 
            GrPovRaySceneSequenceComposer
        {
            public GrPovRay(string workingFolder, Float64SamplingSpecs samplingSpecs) 
                : base(workingFolder, samplingSpecs)
            {
            }

            protected override void InitializeTextureSet()
            {
                
            }
            
            protected override void InitializeTemporalValues()
            {

            }

            protected override void InitializeSceneComposers(int frameIndex)
            {
                
            }

            protected override void AddGuiLayer(int frameIndex)
            {
                
            }

            protected override void ComposeFrame(int frameIndex)
            {
                base.ComposeFrame(frameIndex);

                // Sunlight
                ActiveSceneObject.AddStatement(
                    GrPovRayLightSource.PointLight(
                        LinFloat64Vector3D.Create(-1500, 2500, -2500),
                        GrPovRayColorValue.Rgb(0.9)
                    )
                );

                if (ActiveSceneObject.Camera.FlashLightColor is not null)
                    ActiveSceneObject.AddStatement(
                        GrPovRayLightSource.PointLight(
                            ActiveSceneObject.Camera.Position,
                            ActiveSceneObject.Camera.FlashLightColor
                        )
                    );

                //scene.Plane(
                //    LinFloat64Vector3D.E2, 
                //    0
                //);

                //ActiveSceneObject.Statements.DeclareFinish(
                //    "baseFinish",
                //    new GrPovRayFinishProperties
                //    {
                //        DiffuseAmount = 1,
                //        SpecularAmount = 1
                //    }
                //);

                //var box = ActiveSceneObject.Box(
                //    LinFloat64Vector3D.Create(-1, -1, -1),
                //    LinFloat64Vector3D.Create(1, 1, 1)
                //).SetMaterial(Color.OrangeRed, "baseFinish");

                //var xAxis = ActiveSceneObject.Cylinder(
                //    LinFloat64Vector3D.Zero,
                //    LinFloat64Vector3D.E1 * 3,
                //    0.075,
                //    false
                //).SetMaterial(Color.DarkRed, "baseFinish");
                
                //var yAxis = ActiveSceneObject.Cylinder(
                //    LinFloat64Vector3D.Zero,
                //    LinFloat64Vector3D.E2 * 3,
                //    0.075,
                //    false
                //).SetMaterial(Color.DarkGreen, "baseFinish");
                
                //var zAxis = ActiveSceneObject.Cylinder(
                //    LinFloat64Vector3D.Zero,
                //    LinFloat64Vector3D.E3 * 3,
                //    0.075,
                //    false
                //).SetMaterial(Color.DarkBlue, "baseFinish");
            }
        }

        public static void Example3()
        {
            var samplingSpecs = 
                Float64SamplingSpecs.CreateFromTimeLength(36, 5);

            var composer = new GrPovRay(
                @"D:\Projects\Study\POV-Ray\Samples", samplingSpecs)
            {
                DefaultRenderingOptions =
                {
                    Width = 1280,
                    Height = 720,
                    AntiAlias = true,
                    AntiAliasDepth = 6,
                    MaxImageBufferMemory = 10240,
                    Quality = 9,
                    OutputFileType = GrPovRayOutputFileTypeValue.Png
                },
                
                GridUnitCount = 8,

                ShowAxes = true,
                ShowGrid = true,
                ShowCopyright = false,
                ShowGuiLayer = false,
                
                ComposeSceneFilesEnabled = true,
                RenderImageFilesEnabled = true,
                RenderGifFileEnabled = false,
                RenderVideoFileEnabled = true
                
            };
            

            var alpha = 
                TemporalFloat64Scalar
                    .FullCos(-120, 120)
                    .DegreesToRadians();

            var beta = 
                60.DegreesToRadians();

            composer.SetCameraAlphaBetaDistance(alpha, beta, 15);

            composer.RenderFiles();
        }
    }
}
