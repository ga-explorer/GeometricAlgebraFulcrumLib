using System;

using System.IO;
using System.Linq;
using DataStructuresLib;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicShapes.Triangles.Immutable;
using NumericalGeometryLib.Borders.Space2D.Immutable;
using GraphicsComposerLib.Geometry.ParametricShapes.Surfaces;
using GraphicsComposerLib.Geometry.ParametricShapes.Surfaces.Sampled;
using GraphicsComposerLib.Geometry.Primitives;
using GraphicsComposerLib.Geometry.Primitives.Lines;
using GraphicsComposerLib.Geometry.Primitives.Triangles;
using GraphicsComposerLib.Rendering.Svg.DrawingBoard;
using GraphicsComposerLib.Rendering.Xeogl;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Samples.Geometry.ParametricShapes
{
    public static class Sample2
    {
        private static string Generate(GrVertexListTriangleGeometry3D trianglesGeometry, string htmlFilePath = "")
        {
            trianglesGeometry.VertexColorsEnabled = false;
            trianglesGeometry.VertexNormalsEnabled = true;

            var linesGeometryComposer = new GrLineGeometryComposer3D();
            //linesGeometryComposer.AddLines(trianglesGeometry.GetTriangleEdges());
            linesGeometryComposer.AddLines(trianglesGeometry.GetDisplacedTriangleEdges(-0.0005d));
            linesGeometryComposer.AddLines(trianglesGeometry.GetDisplacedTriangleEdges(0.0005d));
            
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
        
        private static double Sinc(double x, double y)
        {
            var v = Math.Sqrt(x * x + y * y) * 6;

            return v.IsAlmostZero() ? 1d : (Math.Sin(v) / v);
        }


        public static SvgDrawingBoardLayer DrawLeafTopEdges(this SvgDrawingBoardLayer drawingLayer, GrParametricSurfaceTree3D surfaceTree, double gridSegmentLength)
        {
            drawingLayer.PenPixelsWidth = 5;
            drawingLayer.PenColor = Color.DarkBlue;

            foreach (var leafNode in surfaceTree.LeafNodesList)
            {
                var edgePoints = 
                    leafNode.Edge1X.Select(f => 
                        (ITuple2D) new Tuple2D(
                            f.GridIndex.Item1 * gridSegmentLength,
                            f.GridIndex.Item2 * gridSegmentLength
                        )
                    ).ToArray();

                drawingLayer.DrawPolyline(edgePoints);

                foreach (var point in edgePoints)
                    drawingLayer.DrawSquareMarker(point, 8);
            }

            return drawingLayer;
        }

        public static SvgDrawingBoardLayer DrawLeafTriangles(this SvgDrawingBoardLayer drawingLayer, GrParametricSurfaceTree3D surfaceTree, double gridSegmentLength)
        {
            drawingLayer.PenPixelsWidth = 1;
            drawingLayer.PenColor = Color.Green;

            var triangleIndexTriplets = 
                surfaceTree
                    .LeafNodesList
                    .SelectMany(n => n.GetTriangleIndexTriplets());
            
            foreach (var (i1, i2, i3) in triangleIndexTriplets)
            {
                if (i1 == i2 || i2 == i3 || i3 == i1)
                    throw new InvalidOperationException();

                var gridIndex1 = surfaceTree.GetCorner(i1).GridIndex;
                var gridIndex2 = surfaceTree.GetCorner(i2).GridIndex;
                var gridIndex3 = surfaceTree.GetCorner(i3).GridIndex;

                var p1 = new Tuple2D(
                    gridIndex1.Item1 * gridSegmentLength,
                    gridIndex1.Item2 * gridSegmentLength
                );

                var p2 = new Tuple2D(
                    gridIndex2.Item1 * gridSegmentLength,
                    gridIndex2.Item2 * gridSegmentLength
                );

                var p3 = new Tuple2D(
                    gridIndex3.Item1 * gridSegmentLength,
                    gridIndex3.Item2 * gridSegmentLength
                );

                drawingLayer.DrawTriangle(Triangle2D.Create(p1, p2, p3));

                //drawingLayer.DrawLineSegment(p1, p2);
                //drawingLayer.DrawLineSegment(p2, p3);
                //drawingLayer.DrawLineSegment(p3, p1);
            }

            return drawingLayer;
        }

        public static SvgDrawingBoardLayer DrawTree(this SvgDrawingBoardLayer drawingLayer, GrParametricSurfaceTree3D surfaceTree, double gridSegmentLength)
        {
            drawingLayer.PenPixelsWidth = 1;
            drawingLayer.PenColor = Color.Black;

            drawingLayer.DrawRectangle(
                0, 
                0, 
                surfaceTree.GridSegmentCount * gridSegmentLength,
                surfaceTree.GridSegmentCount * gridSegmentLength
            );

            foreach (var node in surfaceTree.BranchNodes)
            {
                var (bottomMidGridIndex1, bottomMidGridIndex2) = node.MidGridIndexX0;
                var (topMidGridIndex1, topMidGridIndex2) = node.MidGridIndexX1;
                var (leftMidGridIndex1, leftMidGridIndex2) = node.MidGridIndex0X;
                var (rightMidGridIndex1, rightMidGridIndex2) = node.MidGridIndex1X;

                drawingLayer.DrawLineSegment(
                    bottomMidGridIndex1 * gridSegmentLength,
                    bottomMidGridIndex2 * gridSegmentLength,
                    topMidGridIndex1 * gridSegmentLength,
                    topMidGridIndex2 * gridSegmentLength
                );
                
                drawingLayer.DrawLineSegment(
                    leftMidGridIndex1 * gridSegmentLength,
                    leftMidGridIndex2 * gridSegmentLength,
                    rightMidGridIndex1 * gridSegmentLength,
                    rightMidGridIndex2 * gridSegmentLength
                );
            }

            return drawingLayer;
        }

        public static SvgDrawingBoardLayer DrawFrames(this SvgDrawingBoardLayer drawingLayer, GrParametricSurfaceTree3D surfaceTree, double gridSegmentLength)
        {
            drawingLayer.PenPixelsWidth = 2;
            drawingLayer.PenColor = Color.Blue;
            
            foreach (var frame in surfaceTree.CornerList)
            {
                var (gridIndex1, gridIndex2) = frame.GridIndex;

                drawingLayer.DrawCircleMarker(
                    gridIndex1 * gridSegmentLength,
                    gridIndex2 * gridSegmentLength,
                    5
                );
            }

            return drawingLayer;
        } 

        public static void Execute()
        {
            var parameterValueRange = 
                BoundingBox2D.Create(0, 0, 1, 1);

            var options = new GrParametricSurfaceTreeOptions3D(
                6.DegreesToAngle(),
                0,
                30
            )
            {
                ForceBalancedTree = true
            };

            var sampledSurface =
                GrParametricSurfaceFactory3D
                    .CreateMathSurface3D(Sinc)
                    .CreateSampledSurface3D(parameterValueRange, options);

            //var sampledSurface =
            //    new Tuple3D(1, 1, 1)
            //        .ToUnitVector()
            //        .CreateCircle3D(10d)
            //        .CreateTube3D(2d)
            //        .CreateSampledSurface3D(parameterValueRange, options);

            //var sampledSurface =
            //    AxisPair3D.Xy
            //        .CreateSphere3D(2)
            //        .CreateSampledSurface3D(parameterValueRange, options);

            var time1 = DateTime.Now;

            sampledSurface.GenerateTree(options);

            var timeSpan = DateTime.Now - time1;
            
            var maxDistance = 
                sampledSurface
                    .LeafNodesList
                    .Min(n => n.MaxEdgeFramesDistance());

            var maxAngle = 
                sampledSurface
                    .LeafNodesList
                    .Max(n => n.MaxEdgeFramesAngle().Degrees);

            Console.WriteLine($"Tree Levels: {sampledSurface.TreeLevelCount}");
            Console.WriteLine($"Number of samples: {sampledSurface.CornerCount}");
            Console.WriteLine($"Number of leaf nodes: {sampledSurface.LeafNodeCount}");
            Console.WriteLine($"Max leaf edge frames distance: {maxDistance:G}");
            Console.WriteLine($"Max leaf edge frames angle: {maxAngle:G}");
            Console.WriteLine($"Tree size: {sampledSurface.SizeInBytes():C0}");
            Console.WriteLine($"Sampling time: {timeSpan.TotalMilliseconds} ms");
            
            //foreach (var leafNode in surfaceTree.LeafNodes)
            //{
            //    Console.WriteLine($"Cell <00: ({leafNode.GridIndex00.Item1}, {leafNode.GridIndex00.Item2}), 01: ({leafNode.GridIndex01.Item1}, {leafNode.GridIndex01.Item2}), 10: ({leafNode.GridIndex10.Item1}, {leafNode.GridIndex10.Item2}), 11: ({leafNode.GridIndex11.Item1}, {leafNode.GridIndex11.Item2})>");

            //    var edgeText =
            //        leafNode
            //            .BottomEdge
            //            .Select(f => $"({f.GridIndex.Item1}, {f.GridIndex.Item2})")
            //            .Concatenate(" -> ");

            //    Console.WriteLine($"Bottom Edge: {edgeText}");

            //    edgeText =
            //        leafNode
            //            .TopEdge
            //            .Select(f => $"({f.GridIndex.Item1}, {f.GridIndex.Item2})")
            //            .Concatenate(" -> ");

            //    Console.WriteLine($"Top Edge: {edgeText}");
                
            //    edgeText =
            //        leafNode
            //            .LeftEdge
            //            .Select(f => $"({f.GridIndex.Item1}, {f.GridIndex.Item2})")
            //            .Concatenate(" -> ");

            //    Console.WriteLine($"Left Edge: {edgeText}");

            //    edgeText =
            //        leafNode
            //            .RightEdge
            //            .Select(f => $"({f.GridIndex.Item1}, {f.GridIndex.Item2})")
            //            .Concatenate(" -> ");

            //    Console.WriteLine($"Right Edge: {edgeText}");
            //    Console.WriteLine();
            //    Console.WriteLine();
            //}

            time1 = DateTime.Now;

            var trianglesGeometry = 
                sampledSurface.GenerateGeometry();

            timeSpan = DateTime.Now - time1;
            
            Console.WriteLine($"Triangles count: {trianglesGeometry.Count:C0}");
            Console.WriteLine($"Triangles size: {trianglesGeometry.SizeInBytes():C0}");
            Console.WriteLine($"Triangles Generation time: {timeSpan.TotalMilliseconds} ms");

            //sampledSurface.ClearLeafEdgeData();

            const string fileName = "Surface1";
            Generate(trianglesGeometry, @$"D:\Projects\Study\Xeogl\MyWork\{fileName}.html");

            //Draw parameter value quad tree
            const int resolutionLevel = 11;

            if (sampledSurface.TreeLevelCount > resolutionLevel - 4) 
                return;
            
            var gridSegmentLength = 
                (1 << (resolutionLevel - sampledSurface.TreeLevelCount)) * 
                sampledSurface.GridSegmentCount;

            var drawingBoard = SvgDrawingBoard.Create(
                1 << resolutionLevel,
                1 << resolutionLevel,
                gridSegmentLength * sampledSurface.GridSegmentCount / 2d,
                gridSegmentLength * sampledSurface.GridSegmentCount / 2d,
                gridSegmentLength * (2 + sampledSurface.GridSegmentCount)
            );

            drawingBoard
                .ActiveLayer
                .DrawTree(sampledSurface, gridSegmentLength)
                .DrawLeafTriangles(sampledSurface, gridSegmentLength)
                .DrawFrames(sampledSurface, gridSegmentLength);

            drawingBoard.SaveToSvgFile(@$"D:\Projects\Study\Xeogl\MyWork\{fileName}.svg");
        }
    }
}