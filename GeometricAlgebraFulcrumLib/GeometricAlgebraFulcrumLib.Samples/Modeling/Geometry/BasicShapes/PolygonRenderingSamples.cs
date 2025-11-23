using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingBoard;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingStyles;
using GeometricAlgebraFulcrumLib.Modeling.Research.UAVs;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Formats.Png;
using Path = System.IO.Path;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Geometry.BasicShapes
{
    public static class PolygonRenderingSamples
    {
        //public static void Example1()
        //{
        //    using var image = new Image<Rgba32>(1000, 1000, Color.White);

        //    // Example polygon: a star {7/2}
        //    var polygon = Float64RegularPolygon2D.Create(7, 1);
        //    polygon.Transform = Float64SimilarityAffineMap2D.Create(
        //        400, 
        //        LinFloat64PolarAngle.Angle0, 
        //        500, 
        //        500
        //    );

        //    var renderer = new RegularPolygonRenderer2D(polygon)

        //        // -----------------------------
        //        // Geometry layers
        //        // -----------------------------
        //        .BeginGroup("geometry")
        //            .AddFill(s => s.FillColor = Color.LightGoldenrodYellow)
        //            .AddOutline(s =>
        //            {
        //                s.StrokeColor = Color.Black;
        //                s.StrokeWidth = 4;
        //            })
        //            .AddVertices(s =>
        //            {
        //                s.Color = Color.DarkRed;
        //                s.Radius = 8;
        //            })
        //            .AddDiagonals(s =>
        //            {
        //                s.StrokeColor = Color.LightGray;
        //                s.StrokeWidth = 1;
        //            })
        //            .AddNormals(s =>
        //            {
        //                s.StrokeColor = Color.SteelBlue;
        //                s.StrokeWidth = 2;
        //                s.Length = 50;
        //            })
        //        .EndGroup()

        //        // -----------------------------
        //        // Annotation layers
        //        // -----------------------------
        //        .BeginGroup("annotations")
        //            .AddAngles(s =>
        //            {
        //                s.StrokeColor = Color.OrangeRed;
        //                s.StrokeWidth = 2;
        //                s.ArcRadius = 60;
        //                s.ShowLabels = true;
        //                s.LabelFont = SystemFonts.CreateFont("Arial", 16, FontStyle.Bold);
        //                s.LabelColor = Color.DarkBlue;
        //            })
        //            .AddLabels(s =>
        //            {
        //                s.Color = Color.DarkGreen;
        //                s.Font = SystemFonts.CreateFont("Arial", 18, FontStyle.Bold);
        //                s.Prefix = "V";
        //            })
        //            .AddCustom((ctx, vertices, layer) =>
        //            {
        //                // Highlight the first vertex with a large purple dot
        //                var v0 = vertices.First().ToImageSharpPointF();
        //                ctx.Fill(Color.Purple, new EllipsePolygon(v0, 15));
        //            })
        //        .EndGroup();

        //    const string workingFolder = @"D:\";

        //    // -----------------------------
        //    // 1) Render with all layers
        //    // -----------------------------
        //    image.Mutate(ctx => renderer.Render(ctx));
        //    image.SaveAsPng(
        //        System.IO.Path.Combine(
        //            workingFolder, 
        //            "polygon_all_layers.png"
        //        )
        //    );

        //    // -----------------------------
        //    // 2) Disable annotations and render again
        //    // -----------------------------
        //    renderer.EnableGroup("annotations", false);
        //    using var image2 = new Image<Rgba32>(1000, 1000, Color.White);
        //    image2.Mutate(ctx => renderer.Render(ctx));
        //    image2.SaveAsPng(
        //        System.IO.Path.Combine(
        //            workingFolder, 
        //            "polygon_geometry_only.png"
        //        )
        //    );

        //    // -----------------------------
        //    // 3) Re-enable annotations but override their styles globally
        //    // -----------------------------
        //    renderer.EnableGroup("annotations", true);
        //    renderer.SetGroupOverride("annotations", o =>
        //    {
        //        o.StrokeColor = Color.Green;
        //        o.LabelColor = Color.Green;
        //    });

        //    using var image3 = new Image<Rgba32>(1000, 1000, Color.White);
        //    image3.Mutate(ctx => renderer.Render(ctx));
        //    image3.SaveAsPng(
        //        System.IO.Path.Combine(
        //            workingFolder, 
        //            "polygon_annotations_overridden.png"
        //        )
        //    );

        //    // Clear overrides for later
        //    renderer.ClearAllOverrides();
        //}

        public static void Example1()
        {
            const int n = 8;
            const string workingFolder = @"D:\";

            using var drawingBoard = ImageSharpDrawingBoard.Create(
                new WorldToScreenMap2D(
                    Float64BoundingBox2D.Around(
                        LinFloat64Vector2D.Zero,
                        (n + 1) / 0.75,
                        (n + 1)
                    ),
                    1024,
                    768,
                    16
                ),
                Color.Beige
            );

            for (var i = n; i <= n; i++)
            {
                var polygon = Float64RegularPolygon2D.Create(n, 1);

                polygon.Transform = Float64SimilarityAffineMap2D.Scaling(i);

                drawingBoard.DrawDiagonals(
                    polygon,
                    new ImageSharpLineDrawingStyle(Color.SandyBrown, 1)
                    {
                        StrokePattern = [3f, 1f]
                    }
                ).DrawSideNormals(
                    polygon,
                    2,
                    new ImageSharpLineDrawingStyle(Color.DarkSalmon, 2),
                    new ImageSharpArrowHeadDrawingStyle(Color.DarkSalmon, Color.LightSalmon, 2)
                    {
                        SizeX = 20,
                        SizeY = 15,
                    }
                ).DrawSides(
                    polygon,
                    new ImageSharpLineDrawingStyle(Color.Black, 3)
                ).DrawVertexPoints(
                    polygon,
                    new ImageSharpPointMarkerDrawingStyle(Color.Black, Color.CadetBlue, 1)
                    {
                        Size = 15,
                        Shape = PointMarkerShape.Circle
                    }
                ).DrawSideMidPoints(
                    polygon,
                    new ImageSharpPointMarkerDrawingStyle(Color.Black, Color.LightSalmon, 1)
                    {
                        Size = 7,
                        Shape = PointMarkerShape.Circle
                    }
                );
            }

            drawingBoard.Image.SaveAsPng(
                Path.Combine(workingFolder, "Polygon.png"),
                new PngEncoder()
                {
                    ColorType = PngColorType.RgbWithAlpha,
                    CompressionLevel = PngCompressionLevel.NoCompression,
                    FilterMethod = PngFilterMethod.None,
                    TransparentColorMode = PngTransparentColorMode.Preserve
                }
            );
        }

        public static void Example2()
        {
            const int sideCount = 8;
            const int levelCount = 12;
            const int maxDoorsPerLevel = 6; // must be <= sideCount

            const string workingFolder = @"D:\";

            var polygonList =
                levelCount.GetRange(lvlIdx =>
                    {
                        var p =
                            Float64RegularPolygon2D.Convex(sideCount);

                        p.Transform = Float64SimilarityAffineMap2D.Create(
                            lvlIdx + 1,
                            p.CentralAngle.HalfPolarAngle()
                        );

                        return p;
                    }
                ).ToArray();

            var perimeterList =
                levelCount.GetRange(lvlIdx =>
                    polygonList[lvlIdx].GetPerimeterPoints(lvlIdx).ToArray()
                ).ToList();

            var flatSegmentList = new List<Pair<LinFloat64Vector2D>[]>(levelCount);

            for (var levelIdx = 0; levelIdx < perimeterList.Count; levelIdx++)
            {
                var perimeterPoints =
                    perimeterList[levelIdx];

                var perimeterPointsCount =
                    perimeterPoints.Length;

                var flatSegments =
                    new Pair<LinFloat64Vector2D>[perimeterPointsCount];

                flatSegmentList.Add(flatSegments);

                for (var i = 0; i < perimeterPointsCount; i++)
                {
                    var j = (i + 1).Mod(perimeterPointsCount);

                    flatSegments[i] =
                        new Pair<LinFloat64Vector2D>(
                            perimeterPoints[i],
                            perimeterPoints[j]
                        );
                }
            }

            var randGen = new Random(10);
            for (var levelIdx = 0; levelIdx < levelCount - 1; levelIdx++)
            {
                var level = levelIdx + 1;

                var flatSegments1 =
                    flatSegmentList[levelIdx];

                var flatSegments2 =
                    flatSegmentList[levelIdx + 1];

                var maxDoorCount = Math.Min(level, maxDoorsPerLevel);
                var doorCount = randGen.GetInt32(1, maxDoorCount + 1);

                var segmentIndexList =
                    randGen.GetDistinctIndices(
                        doorCount,
                        sideCount
                    );

                foreach (var segmentIndex in segmentIndexList)
                {
                    var offset = randGen.GetInt32(-1, 2);

                    var segmentIndex1 = segmentIndex * level;
                    var segmentIndex2 = segmentIndex * (level + 1);

                    flatSegments1[segmentIndex1] =
                        new Pair<LinFloat64Vector2D>(
                            flatSegments1[segmentIndex1].Item1,
                            flatSegments2[(segmentIndex2 + offset).Mod(flatSegments2.Length)].Item1
                        );
                }
            }

            using var drawingBoard = ImageSharpDrawingBoard.Create(
                new WorldToScreenMap2D(
                    Float64BoundingBox2D.Around(
                        LinFloat64Vector2D.Zero,
                        levelCount,
                        levelCount
                    ),
                    1600,
                    1600,
                    24
                ),
                Color.Beige
            );

            drawingBoard.DrawRectangle(
                LinFloat64Vector2D.Zero,
                drawingBoard.WorldSizeX,
                drawingBoard.WorldSizeY,
                new ImageSharpLineDrawingStyle(Color.Black, 3)
            ).DrawLines(
                flatSegmentList.SelectMany(a => a),
                new ImageSharpLineDrawingStyle(Color.Blue, 3)
                {
                    EndCapStyle = EndCapStyle.Round
                }
            );

            //for (var i = 0; i < levelCount; i++)
            //{
            //    var polygon = polygonList[i];

            //    drawingBoard.DrawSides(
            //        polygon,
            //        new ImageSharpLineDrawingStyle(Color.Black, 1)
            //        {
            //            StrokePattern = [3f, 1f, 1f, 1f]
            //        }
            //    ).DrawPoints(
            //        polygon.GetPerimeterPoints(i),
            //        new ImageSharpPointMarkerDrawingStyle(Color.Black, Color.LightSalmon, 1)
            //        {
            //            Size = 7,
            //            Shape = PointMarkerShape.Circle
            //        }
            //    );
            //}

            drawingBoard.Image.SaveAsPng(
                Path.Combine(workingFolder, $"Maze{sideCount}By{levelCount}.png"),
                new PngEncoder()
                {
                    ColorType = PngColorType.RgbWithAlpha,
                    CompressionLevel = PngCompressionLevel.NoCompression,
                    FilterMethod = PngFilterMethod.None,
                    TransparentColorMode = PngTransparentColorMode.Preserve
                }
            );
        }


        public static void Example3()
        {
            const bool showMaze = true;
            const string workingFolder = @"D:\";
            const int degree = 5;
            
            var randGen = new Random(10);

            // Construct/Read PH Curve C2 Cache
            var c2Cache =
                PhBernsteinCurveC2Cache2D.CreateOrRead(
                    degree, 
                    1, 
                    Path.Combine(
                        workingFolder,
                        $"PhBernsteinCurveC2Cache2D_{degree}-1.json.gz"
                    ), 
                    true
                );

            // We assume environment size in km, and UAV speed around 20 m/s = 0.02 km/s

            // Initialize maze composer and generate maze segments
            var mazeComposer =
                new PolygonMazeComposer(
                    1,
                    6,
                    12,
                    15
                );

            var obstacles =
                mazeComposer.GenerateMaze(randGen);
            
            obstacles.SaveSegmentsToJson(
                Path.Combine(
                    workingFolder,
                    $"Maze_{mazeComposer.SideCount}-{mazeComposer.LevelCount}.json.gz"
                ), 
                true
            );

            // Initialize maze solver and drawing board
            var time1 = DateTime.Now;

            var mazeSolver =
                new VoronoiMazeSolver2D(
                    obstacles,
                    new VoronoiMazeSolver2D.SolverOptions()
                    {
                        MaxVoronoiSiteDistance = 1,
                        MaxEdgeLength = 100,
                        MinLeafEdgeLength = 0.5,
                        MinObstacleDistance = 0.15
                    }
                );

            var time2 = DateTime.Now;

            var freePolylines =
                mazeSolver.FreeEdgeChains;

            var adjGraph =
                mazeSolver.AdjacencyGraph;

            var freeEdges =
                mazeSolver.FreeEdges;

            Console.WriteLine($"Built {freePolylines.Count} polyline chains containing {freeEdges.Count} free-space Voronoi edges in {time2 - time1} seconds.");

            // Example polyline: take the longest one
            var longestPolyline =
                freePolylines.OrderByDescending(
                    p => p.GetPolylineLength()
                ).First();

            // Build complete roadmap once using A* search
            var pathFinder = adjGraph.GetAStarPathFinder(
                (p1, p2) => p1.GetDistanceToPoint(p2),
                (p1, p2) => p1.GetDistanceToPoint(p2)
            );

            // Now we can handle multiple optimal path queries
            var queries =
                adjGraph.LeafNodes.Select(goal =>
                    (LinFloat64Vector2D.Zero, goal)
                ).ToArray();

            var pathIndex = 0;
            foreach (var (start, goal) in queries)
            {
                pathIndex++;

                Console.WriteLine($"Solving path {pathIndex} of {queries.Length}");

                var node1 =
                    adjGraph.GetNearestNode(
                        start,
                        (p1, p2) => p1.GetDistanceToPoint(p2)
                    );

                var node2 =
                    adjGraph.GetNearestNode(
                        goal,
                        (p1, p2) => p1.GetDistanceToPoint(p2)
                    );

                var (path, _) =
                    pathFinder.FindPath(node1, node2);

                if (path.Count == 0)
                {
                    Console.WriteLine("No path found");
                }
                else
                {
                    Console.WriteLine($"Path found with {path.Count} points");
                    
                    //foreach (var p in path) 
                    //    Console.WriteLine($"   {p.X:F1},{p.Y:F1}");

                    // Create a random path from the solution path
                    //var randomPath = path.ToPolyline();
                    var randomPath = 
                        path.ToPolyline().CreateRandom(obstacles, 0.1, randGen);
                    
                    // TODO: Replace this random search loop with an optimization method
                    var itrCount = 10000;
                    while (itrCount > 0)
                    {
                        itrCount--;

                        var randomPath1 = 
                            path.ToPolyline().CreateRandom(obstacles, 0.1, randGen);

                        if (randomPath1.GetShortestDistanceTo(obstacles) < 0.1)
                            continue;

                        if (randomPath1.Length < randomPath.Length) 
                            randomPath = randomPath1;
                    }

                    //randomPath = randomPath.IncreaseVertexAngles(obstacles, 0.1);

                    // Convert the path into a PH poly-curve trajectory
                    time1 = DateTime.Now;
                    var polyCurve =
                        PhBernsteinPolyCurve2D.FromPolyline(randomPath, c2Cache, randGen);
                    time2 = DateTime.Now;
                    Console.WriteLine($"Poly-curve found in {time2 - time1} seconds");

                    // Compute and display physical properties of solution trajectory
                    // Each 1 curve length unit = 1000 meters, with 20 m/s UAV speed
                    polyCurve.SetPhysicalProperties(1000, 20);

                    var rangeSummary =
                        polyCurve.GetPhysicalQuantitiesSummary(0.1, 5);

                    foreach (var (quantity, range) in rangeSummary) 
                        Console.WriteLine($"{quantity}: {range}");

                    //Console.WriteLine($"Length: {polyCurve.TotalLength}");
                    //Console.WriteLine($"Max Curvature: {polyCurve.MaxCurvature}");
                    //Console.WriteLine($"Energy: {polyCurve.Energy}");
                    //Console.WriteLine($"Position error: {polyCurve.PositionError}");
                    //Console.WriteLine($"Tangent error: {polyCurve.TangentError}");
                    //Console.WriteLine($"Acceleration error: {polyCurve.AccelerationError}");

                    // Draw the maze and solution
                    using var drawingBoard =
                        mazeSolver.CreateDrawingBoard(1600*2, Color.Beige);

                    // Draw world border rectangle
                    drawingBoard.DrawRectangle(
                        LinFloat64Vector2D.Zero,
                        drawingBoard.WorldSizeX,
                        drawingBoard.WorldSizeY,
                        new ImageSharpLineDrawingStyle(Color.Black, 3)
                    );

                    if (showMaze)
                    {
                        // Draw maze levels
                        //for (var i = 0; i < composer.LevelCount; i++)
                        //{
                        //    var polygon = composer.LevelPolygons[i];

                        //    drawingBoard.DrawSides(
                        //        polygon,
                        //        new ImageSharpLineDrawingStyle(Color.Black, 1)
                        //        {
                        //            StrokePattern = [4f, 2f, 1f, 2f]
                        //        }
                        //    );
                        //}

                        // Draw maze segments
                        drawingBoard.DrawLines(
                            obstacles,
                            new ImageSharpLineDrawingStyle(Color.Blue, 7)
                            {
                                EndCapStyle = EndCapStyle.Round
                            }
                        );

                        // Draw maze segment points
                        //drawingBoard.DrawPoints(
                        //    mazeSolver.MazePoints,
                        //    new ImageSharpPointMarkerDrawingStyle(Color.Black, Color.LightSalmon, 1)
                        //    {
                        //        Size = 7,
                        //        Shape = PointMarkerShape.Circle
                        //    }
                        //);

                        // Draw all nodes on adjacency graph
                        //drawingBoard.DrawPoints(
                        //    adjGraph.Nodes,
                        //    new ImageSharpPointMarkerDrawingStyle(Color.Black, Color.LightSalmon, 1)
                        //    {
                        //        Size = 7,
                        //        Shape = PointMarkerShape.Circle
                        //    }
                        //);
                    
                        // Draw all points on simplified polyline paths
                        //drawingBoard.DrawPoints(
                        //    freePolylines.SelectMany(pl => pl),
                        //    new ImageSharpPointMarkerDrawingStyle(Color.Black, Color.LightSalmon, 1)
                        //    {
                        //        Size = 7,
                        //        Shape = PointMarkerShape.Circle
                        //    }
                        //);
                    }

                    //drawingBoard.DrawPolyLine(
                    //    path,
                    //    new ImageSharpLineDrawingStyle(Color.Green, 1)
                    //);

                    var color = Color.DarkBlue; // randGen.GetColor();

                    //drawingBoard.DrawPolyLine(
                    //    randomPath,
                    //    new ImageSharpLineDrawingStyle(color, 1)
                    //);

                    drawingBoard.DrawPolyLine(
                        polyCurve.ToAdaptivePolyline(),
                        new ImageSharpLineDrawingStyle(color, 3)
                    );
                    
                    //drawingBoard.DrawLines(
                    //    freeEdges,
                    //    new ImageSharpLineDrawingStyle(Color.LightGreen, 7)
                    //    {
                    //        EndCapStyle = EndCapStyle.Round
                    //    }
                    //);

                    //drawingBoard.DrawLines(
                    //    longestPolyline,
                    //    new ImageSharpLineDrawingStyle(Color.Green, 7)
                    //    {
                    //        EndCapStyle = EndCapStyle.Round
                    //    }
                    //);

                    //drawingBoard.DrawLines(
                    //    freeEdges,
                    //    new ImageSharpLineDrawingStyle(Color.DarkRed, 2)
                    //    {
                    //        EndCapStyle = EndCapStyle.Round
                    //    }
                    //);

                    drawingBoard.SavePngImage(
                        Path.Combine(workingFolder, $"Maze Path {pathIndex}.png")
                    );

                    //polyCurve.PlotCurveMetrics(workingFolder);
                    //polyCurve.PlotPhysicalMetricsVsTime(workingFolder);
                    //polyCurve.PlotPhysicalMetricsVsDistance(workingFolder);

                }
            }

        }

    }

}
