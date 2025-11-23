using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingStyles;
using GeometricAlgebraFulcrumLib.Modeling.Research.UAVs;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;
using SixLabors.ImageSharp.Drawing;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Samples.Research
{
    public static class UavTrajectoryOptimization
    {
        
        public static void Example1()
        {
            const bool showMaze = true;
            const string workingFolder = @"D:\";
            const int degree = 5;
            
            var randGen = new Random(Seed: 10);

            // Construct/Read PH Curve C2 Cache
            var c2Cache =
                PhBernsteinCurveC2Cache2D.CreateOrRead(
                    degree: degree, 
                    sideLength: 1, 
                    jsonFilePath: System.IO.Path.Combine(
                        path1: workingFolder,
                        path2: $"PhBernsteinCurveC2Cache2D_{degree}-1.json.gz"
                    ), 
                    useCompression: true
                );

            // We assume environment size in km, and UAV speed around 20 m/s = 0.02 km/s

            // Initialize maze composer and generate maze segments
            var mazeComposer =
                new PolygonMazeComposer(
                    baseOuterRadius: 1,
                    sideCount: 6,
                    levelCount: 12,
                    maxDoorsPerLevel: 15
                );

            var obstacles =
                mazeComposer.GenerateMaze(randomGenerator: randGen);
            
            obstacles.SaveSegmentsToJson(
                filePath: System.IO.Path.Combine(
                    path1: workingFolder,
                    path2: $"Maze_{mazeComposer.SideCount}-{mazeComposer.LevelCount}.json.gz"
                ), 
                compress: true
            );

            // Initialize maze solver and drawing board
            var time1 = DateTime.Now;

            var mazeSolver = VoronoiMazeSolver2D.Solve(
                mazeSegments: obstacles, 
                maxVoronoiSiteDistance: 1, 
                maxEdgeLength: 100, 
                minObstacleDistance: 0.15, 
                minLeafEdgeLength: 0.5
            );

            var time2 = DateTime.Now;

            var freePolylines =
                mazeSolver.FreeEdgeChains;

            var adjGraph =
                mazeSolver.AdjacencyGraph;

            var freeEdges =
                mazeSolver.FreeEdges;

            Console.WriteLine(value: $"Built {freePolylines.Count} polyline chains containing {freeEdges.Count} free-space Voronoi edges in {time2 - time1} seconds.");

            // Build complete roadmap once using A* search
            var pathFinder = adjGraph.GetAStarPathFinder();

            // Now we can handle multiple optimal path queries
            var queries =
                new (LinFloat64Vector2D, LinFloat64Vector2D)[]
                {
                    ((0, 0), (-10, 0.5))
                }.Select(adjGraph.GetNearestNodePair).ToArray();

            var pathIndex = 0;
            foreach (var (start, goal) in queries)
            {
                pathIndex++;

                Console.WriteLine(value: $"Solving path {pathIndex} of {queries.Length}");

                var path =
                    pathFinder.FindPath(start, goal, obstacles, 0.1);

                if (path.Count == 0)
                {
                    Console.WriteLine(value: "No path found");
                }
                else
                {
                    Console.WriteLine(value: $"Path found with {path.Count} points");

                    //foreach (var p in path) 
                    //    Console.WriteLine($"   {p.X:F1},{p.Y:F1}");

                    // Create a random path from the solution path
                    var bestPath = 
                        path.CreateRandom(obstacles, 0.1, randGen);
                    
                    // TODO: Replace this random search loop with an optimization method
                    var itrCount = 10000;
                    while (itrCount > 0)
                    {
                        itrCount--;

                        var gene = 
                            randGen.GetFloat64Array1D(size: 2 * (path.Count - 2));

                        var candidatePath = 
                            path.CreateFromGene(
                                gene: gene, 
                                obstacles: obstacles, 
                                safetyDistance: 0.1
                            );

                        var candidatePathMetrics = 
                            candidatePath.GetMetrics(obstacles);

                        if (candidatePathMetrics.GetMinObstacleDistance() < 0.1)
                            continue;

                        if (candidatePathMetrics.Length < bestPath.Length) 
                            bestPath = candidatePath;
                    }

                    //randomPath = randomPath.IncreaseVertexAngles(obstacles, 0.1);

                    // Convert the path into a PH poly-curve trajectory
                    time1 = DateTime.Now;
                    var polyCurve =
                        PhBernsteinPolyCurve2D.FromPolyline(polyline: bestPath, c2Cache: c2Cache, randGen: randGen);
                    time2 = DateTime.Now;
                    Console.WriteLine(value: $"Poly-curve found in {time2 - time1} seconds");

                    // Compute and display physical properties of solution trajectory
                    // Each 1 curve length unit = 1000 meters, with 20 m/s UAV speed
                    polyCurve.SetPhysicalProperties(physicalLengthScale: 1000, physicalSpeed: 20);

                    var rangeSummary =
                        polyCurve.GetPhysicalQuantitiesSummary(timeResolution: 0.1, medianFilterSamples: 5);

                    foreach (var (quantity, range) in rangeSummary) 
                        Console.WriteLine(value: $"{quantity}: {range}");

                    //Console.WriteLine($"Length: {polyCurve.TotalLength}");
                    //Console.WriteLine($"Max Curvature: {polyCurve.MaxCurvature}");
                    //Console.WriteLine($"Energy: {polyCurve.Energy}");
                    //Console.WriteLine($"Position error: {polyCurve.PositionError}");
                    //Console.WriteLine($"Tangent error: {polyCurve.TangentError}");
                    //Console.WriteLine($"Acceleration error: {polyCurve.AccelerationError}");

                    // Draw the maze and solution
                    using var drawingBoard =
                        mazeSolver.CreateDrawingBoard(width: 1600*2, backgroundColor: Color.Beige);

                    // Draw world border rectangle
                    drawingBoard.DrawRectangle(
                        center: LinFloat64Vector2D.Zero,
                        sizeX: drawingBoard.WorldSizeX,
                        sizeY: drawingBoard.WorldSizeY,
                        lineStyle: new ImageSharpLineDrawingStyle(strokeColor: Color.Black, strokeWidth: 3)
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
                            pointPairs: obstacles,
                            style: new ImageSharpLineDrawingStyle(strokeColor: Color.Blue, strokeWidth: 7)
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
                        points: polyCurve.ToAdaptivePolyline(),
                        lineStyle: new ImageSharpLineDrawingStyle(strokeColor: color, strokeWidth: 3)
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
                        filePath: System.IO.Path.Combine(path1: workingFolder, path2: $"Maze Path {pathIndex}.png")
                    );

                    //polyCurve.PlotCurveMetrics(workingFolder);
                    //polyCurve.PlotPhysicalMetricsVsTime(workingFolder);
                    //polyCurve.PlotPhysicalMetricsVsDistance(workingFolder);

                }
            }

        }

        public static void Example2()
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
                    System.IO.Path.Combine(
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
                System.IO.Path.Combine(
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
                        System.IO.Path.Combine(workingFolder, $"Maze Path {pathIndex}.png")
                    );

                    //polyCurve.PlotCurveMetrics(workingFolder);
                    //polyCurve.PlotPhysicalMetricsVsTime(workingFolder);
                    //polyCurve.PlotPhysicalMetricsVsDistance(workingFolder);

                }
            }

        }
    }
}
