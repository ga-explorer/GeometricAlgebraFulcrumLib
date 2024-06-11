using System;
using System.Collections.Generic;
using System.IO;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves.Bezier;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.LatticeShapes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.LatticeShapes.Surfaces;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Graphics.Geometry.BezierShapes;

public static class Sample1
{
    private static string Generate(IReadOnlyList<ParametricCurveLocalFrame3D> sampledCurve, string htmlFilePath = "")
    {
        var composer1 = new GrLineGeometryComposer3D();
        var composer2 = new GrLineGeometryComposer3D();
        var composer3 = new GrLineGeometryComposer3D();

        foreach (var frame in sampledCurve)
        {
            composer1.AddLine(frame.Point, frame.Point + frame.Normal1.ToLinVector3D());
            composer2.AddLine(frame.Point, frame.Point + frame.Normal2.ToLinVector3D());
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
        var parameterValueRange = Float64ScalarRange.Create(0, 1);

        var options = new AdaptiveCurveSamplingOptions3D(
            3.DegreesToDirectedAngle(),
            3,
            30
        );

        // Create a bezier curve
        //var curve = new BezierCurve3Degree3D(
        //    Float64Vector3D.Create(10, 0, 0),
        //    Float64Vector3D.Create(10, 30, 0),
        //    Float64Vector3D.Create(5, 1, 1),
        //    Float64Vector3D.Create(0, 0, 10)
        //);

        var curve = new BezierCurve3Degree3D(
            LinFloat64Vector3D.Create(0, 0, 0),
            LinFloat64Vector3D.Create(10, 0, 0),
            LinFloat64Vector3D.Create(5, 20, 20),
            LinFloat64Vector3D.Create(0, 20, 0)
        );

        var sampledCurve =
            new AdaptiveCurve3D(curve, parameterValueRange)
            {
                FrameSamplingMethod = ParametricCurveLocalFrameSamplingMethod.SimpleRotation,
                FrameInterpolationMethod = ParametricCurveLocalFrameInterpolationMethod.SphericalLinearInterpolation
            };

        sampledCurve.GenerateTree(options);

        var localFramesList =
            sampledCurve.GenerateTree(options);

        Console.WriteLine($"Number of samples: {localFramesList.CornerCount}");

        var surfaceList = new GrLatticeSurfaceList3D();

        surfaceList
            .AddTubeSurface(localFramesList, 0.2, 36, false);

        surfaceList
            .AddZSphereSurface(36, 18)
            .ScalePointsBy(0.4)
            .TranslatePointsBy(localFramesList[0].Point);

        surfaceList
            .AddZSphereSurface(36, 18)
            .ScalePointsBy(0.4)
            .TranslatePointsBy(localFramesList[^1].Point);

        surfaceList.FinalizeSet();

        Generate(surfaceList, @"D:\Projects\Study\Xeogl\MyWork\Curve1.html");
    }
}