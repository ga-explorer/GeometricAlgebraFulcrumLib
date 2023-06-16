using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.LatticeShapes;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.LatticeShapes.Surfaces;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives.Lines;
using GeometricAlgebraFulcrumLib.MathBase.Parametric;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Space3D.Frames;
using GraphicsComposerLib.Rendering.Xeogl;

namespace GraphicsComposerLib.Samples.Geometry.ParametricShapes
{
    public static class Sample1
    {
        private static string Generate(IReadOnlyList<ParametricCurveLocalFrame3D> sampledCurve, string htmlFilePath = "")
        {
            var composer1 = new GrLineGeometryComposer3D();
            var composer2 = new GrLineGeometryComposer3D();
            var composer3 = new GrLineGeometryComposer3D();

            foreach (var frame in sampledCurve)
            {
                composer1.AddLine(frame.Point, frame.Point + frame.Normal1.ToTuple3D());
                composer2.AddLine(frame.Point, frame.Point + frame.Normal2.ToTuple3D());
                composer3.AddLine(frame.Point, frame.Point + frame.Tangent);
            }

            var scriptGenerator = new XeoglHtmlComposer();
            scriptGenerator.IncludesList.Add("js/xeogl.js");
            scriptGenerator.IncludesList.Add("js/generation/geometryBuilder.js");

            scriptGenerator.AddLinesGeometry(composer1, @"new xeogl.PhongMaterial({ emissive: [1, 0, 0] })");
            scriptGenerator.AddLinesGeometry(composer2, @"new xeogl.PhongMaterial({ emissive: [0, 1, 0] })");
            scriptGenerator.AddLinesGeometry(composer3, @"new xeogl.PhongMaterial({ emissive: [0, 0, 1] })");

            var scriptCode = scriptGenerator.GenerateHtmlPage();

            if (string.IsNullOrEmpty(htmlFilePath))
                return scriptCode;

            File.WriteAllText(htmlFilePath, scriptCode);

            return scriptCode;
        }

        private static string Generate(GrLatticeSurfaceList3D surfaceList, string htmlFilePath = "")
        {
            var trianglesGeometry = surfaceList.GetTriangleGeometry();

            trianglesGeometry.VertexColorsEnabled = false;
            trianglesGeometry.VertexNormalsEnabled = true;

            var linesGeometryComposer = new GrLineGeometryComposer3D();
            //linesGeometryComposer.AddLines(trianglesGeometry.GetTriangleEdges());
            linesGeometryComposer.AddLines(trianglesGeometry.GetDisplacedTriangleEdges(-0.005d));
            linesGeometryComposer.AddLines(trianglesGeometry.GetDisplacedTriangleEdges(0.005d));
            
            var normalLinesGeometryComposer = new GrLineGeometryComposer3D();
            normalLinesGeometryComposer.AddLines(trianglesGeometry.GetNormalLines(0, 0.2));

            var scriptGenerator = new XeoglHtmlComposer();
            scriptGenerator.IncludesList.Add("js/xeogl.js");
            scriptGenerator.IncludesList.Add("js/generation/geometryBuilder.js");

            scriptGenerator.AddTrianglesGeometry(trianglesGeometry, @"new xeogl.PhongMaterial({ diffuse: [0.6, 0.6, 1.0] })");
            //scriptGenerator.AddLinesGeometry(linesGeometryComposer, @"new xeogl.PhongMaterial({ emissive: [Math.random() + 0.5, Math.random() + 0.5, Math.random() + 0.5] })");
            //scriptGenerator.AddLinesGeometry(normalLinesGeometryComposer, @"new xeogl.PhongMaterial({ emissive: [Math.random() + 0.5, Math.random() + 0.5, Math.random() + 0.5] })");

            var scriptCode = scriptGenerator.GenerateHtmlPage();

            if (string.IsNullOrEmpty(htmlFilePath))
                return scriptCode;

            File.WriteAllText(htmlFilePath, scriptCode);

            return scriptCode;
        }

        public static void Execute()
        {
            var options = new AdaptiveCurveSamplingOptions3D(
                3.DegreesToAngle(),
                0,
                30
            );

            var curve = new ComputedParametricCurve3D(
                t => 
                    new Float64Tuple3D(
                        Math.Exp(-0.1 * t) * Math.Cos(t),
                        Math.Exp(-0.1 * t) * Math.Sin(t),
                        0.5 * t
                    )
            );
            
            var composer = new AdaptiveCurve3D(curve, 0, 8 * Math.PI)
            {
                FrameSamplingMethod = ParametricCurveLocalFrameSamplingMethod.SimpleRotation,
                FrameInterpolationMethod = ParametricCurveLocalFrameInterpolationMethod.SphericalLinearInterpolation
            };


            var time1 = DateTime.Now;

            var sampledCurve1 = 
                composer.GenerateTree(options);

            var timeSpan = DateTime.Now - time1;

            var maxDistance = 
                sampledCurve1
                    .LeafNodesList
                    .Min(n => n.EdgeFrameDistance());

            var maxAngle = 
                sampledCurve1
                    .LeafNodesList
                    .Max(n => n.EdgeFrameMaxAngle().Degrees);

            Console.WriteLine($"Tree Levels: {sampledCurve1.TreeLevelCount}");
            Console.WriteLine($"Number of samples: {sampledCurve1.CornerCount}");
            Console.WriteLine($"Number of leaf nodes: {sampledCurve1.LeafNodeCount}");
            Console.WriteLine($"Max leaf edge frames distance: {maxDistance:G}");
            Console.WriteLine($"Max leaf edge frames angle: {maxAngle:G}");
            Console.WriteLine($"Tree size: {sampledCurve1.SizeInBytes():C0}");
            Console.WriteLine($"Sampling time: {timeSpan.TotalMilliseconds} ms");

            var sampledCurve2 =
                sampledCurve1.GetSubCurveByLength(0, 2, options);

            var sampledCurve3 =
                sampledCurve1.GetSubCurveByLength(4, 6, options);

            var sampledCurve4 =
                sampledCurve1.GetSubCurveByLength(8, 10, options);

            var sampledCurve5 =
                sampledCurve1.GetSubCurveByLength(12, 14, options);

            var sampledCurve6 =
                sampledCurve1.GetSubCurveByLength(16, sampledCurve1.Length, options);

            var surfaceList = new GrLatticeSurfaceList3D();
            
            surfaceList
                .AddTubeSurface(sampledCurve1, 0.2, 36, false);

            surfaceList
                .AddTubeSurface(sampledCurve2, 0.2, 36, false)
                .TranslatePointsBy(5, 0, 0);

            surfaceList
                .AddTubeSurface(sampledCurve3, 0.2, 36, false)
                .TranslatePointsBy(5, 0, 0);

            surfaceList
                .AddTubeSurface(sampledCurve4, 0.2, 36, false)
                .TranslatePointsBy(5, 0, 0);

            surfaceList
                .AddTubeSurface(sampledCurve5, 0.2, 36, false)
                .TranslatePointsBy(5, 0, 0);

            surfaceList
                .AddTubeSurface(sampledCurve6, 0.2, 36, false)
                .TranslatePointsBy(5, 0, 0);

            surfaceList
                .AddZSphereSurface(36, 18)
                .ScalePointsBy(0.4)
                .TranslatePointsBy(sampledCurve1[0].Point);

            surfaceList
                .AddZSphereSurface(36, 18)
                .ScalePointsBy(0.4)
                .TranslatePointsBy(sampledCurve1[^1].Point);

            surfaceList.FinalizeSet();

            Generate(surfaceList, @"D:\Projects\Study\Xeogl\MyWork\Curve1.html");
        }
    }
}
