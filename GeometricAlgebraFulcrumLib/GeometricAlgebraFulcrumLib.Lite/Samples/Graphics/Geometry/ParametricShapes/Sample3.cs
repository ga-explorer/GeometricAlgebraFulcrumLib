﻿using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Graphics.ParametricShapes.Volumes;
using GeometricAlgebraFulcrumLib.Lite.Graphics.ParametricShapes.Volumes.Sampled;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Lines;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl;
using GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry.Primitives;

namespace GeometricAlgebraFulcrumLib.Lite.Samples.Graphics.Geometry.ParametricShapes;

public static class Sample3
{
    private static string Generate(GrParametricVolumeTree3D tree, string htmlFilePath = "")
    {
        var edgeLineSegments = 
            tree.GetEdgePointPairs().Select(p => 
                LineSegment3D.Create(p.Item1, p.Item2)
            );


        var linesGeometryComposer = new GrLineGeometryComposer3D();
        linesGeometryComposer.AddLines(edgeLineSegments);

        var scriptGenerator = new XeoglHtmlComposer();
        scriptGenerator.IncludesList.Add("js/xeogl.js");
        scriptGenerator.IncludesList.Add("js/generation/geometryBuilder.js");

        scriptGenerator.AddLinesGeometry(linesGeometryComposer, @"new xeogl.PhongMaterial({ emissive: [Math.random() + 0.5, Math.random() + 0.5, Math.random() + 0.5] })");

        var scriptCode = scriptGenerator.GenerateHtmlPage();

        if (string.IsNullOrEmpty(htmlFilePath))
            return scriptCode;

        File.WriteAllText(htmlFilePath, scriptCode);

        return scriptCode;
    }
        
    public static void Execute()
    {
        var parameterValueRange = 
            BoundingBox3D.Create(0, 0, 0, 1.2, 1.2, 1.2);

        var options = new GrParametricVolumeTreeOptions3D(
            0.02d,
            0,
            30
        );

        var sampledSurface =
            new SdfRoundBox3D()
                .CreateSampledVolume3D(parameterValueRange, options);
            
        var time1 = DateTime.Now;

        sampledSurface.GenerateTree(options);

        var timeSpan = DateTime.Now - time1;
            
        var maxDistance = 
            sampledSurface
                .LeafNodesList
                .Min(n => n.MaxEdgeFramesDistance());
            
        Console.WriteLine($"Tree Levels: {sampledSurface.TreeLevelCount}");
        Console.WriteLine($"Number of samples: {sampledSurface.CornerCount}");
        Console.WriteLine($"Number of leaf nodes: {sampledSurface.LeafNodeCount}");
        Console.WriteLine($"Max leaf edge frames distance: {maxDistance:G}");
        Console.WriteLine($"Tree size: {sampledSurface.SizeInBytes():C0}");
        Console.WriteLine($"Sampling time: {timeSpan.TotalMilliseconds} ms");

        const string fileName = "Volume1";

        Generate(sampledSurface, @$"D:\Projects\Study\Xeogl\MyWork\{fileName}.html");
    }
}