using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingBoard;
using GeometricAlgebraFulcrumLib.Modeling.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Research.UAVs;

public class VoronoiMazeSolver2D
{
    public sealed record SolverOptions
    {
        public double Tolerance { get; init; } = Float64Utils.DefaultTolerance;

        public double MaxVoronoiSiteDistance { get; init; }

        public double MaxEdgeLength { get; init; }
        
        public double MinObstacleDistance { get; init; }

        public double MinLeafEdgeLength { get; init; }
    }


    public static VoronoiMazeSolver2D Solve(Float64LineSegment2DBvh mazeSegments, double maxVoronoiSiteDistance, double maxEdgeLength, double minObstacleDistance, double minLeafEdgeLength)
    {
        var solver = new VoronoiMazeSolver2D(
            mazeSegments, 
            new SolverOptions
            {
                MaxVoronoiSiteDistance = maxVoronoiSiteDistance,
                MaxEdgeLength = maxEdgeLength,
                MinObstacleDistance = minObstacleDistance,
                MinLeafEdgeLength = minLeafEdgeLength
            }
        );

        return solver;
    }


    public LinFloat64Vector2D.ToleranceComparer PointEqualityComparer { get; }

    public SolverOptions Options { get; }

    public Float64LineSegment2DBvh MazeSegments { get; }

    public IReadOnlyList<LinFloat64Vector2D> MazePoints { get; }

    public Float64BoundingBox2D BoundingBox { get; }

    public IReadOnlyCollection<LinFloat64Vector2D> VoronoiSites { get; private set; }
    
    public IReadOnlyList<IReadOnlyList<LinFloat64Vector2D>> FreeEdgeChains { get; private set; }
    
    public IReadOnlyList<Float64LineSegment2D> FreeEdges { get; private set; }

    public UndirectedAdjacencyGraphEuclidean2D AdjacencyGraph { get; private set; }


    public VoronoiMazeSolver2D(Float64LineSegment2DBvh mazeSegments, SolverOptions options)
    {
        Options = options;

        PointEqualityComparer = 
            new LinFloat64Vector2D.ToleranceComparer(options.Tolerance);

        MazeSegments = mazeSegments;
            
        MazePoints = mazeSegments.SelectMany(
            s => s.GetItems()
        ).ToHashSet(PointEqualityComparer).ToArray();

        BoundingBox = Float64BoundingBoxComposer2D
            .Containing(mazeSegments)
            .UpdateSizeByFactor(1.05)
            .GetBoundingBox();
            
        // Step 1: Construct Voronoi sites from maze segments
        VoronoiSites = ConstructVoronoiSites();

        // Step 2: Build free path edges of obstacle-avoiding Voronoi
        var freeEdges =
            ObstacleAvoidingVoronoiComposer
                .BuildVoronoiAvoidingObstacles(
                    VoronoiSites, 
                    MazeSegments, 
                    BoundingBox
                ).Where(e => 
                    e.Item1.GetDistanceToPoint(e.Item2) < Options.MaxEdgeLength &&
                    MazeSegments.ValidateSegmentClearance(e, Options.MinObstacleDistance)
                ).ToArray();

        // Step 3: Build undirected simple adjacency graph from free edges
        var adjGraph = 
            ConstructAdjacencyGraph(freeEdges, options.MinLeafEdgeLength);

        // Step 4: Create and simplify polylines of connected path edges
        //FreeEdgeChains = adjGraph.GetEdgeChains();
        FreeEdgeChains =
            adjGraph
                .GetEdgeChains()
                .Select(c =>
                    c.SimplifyPolyline(mazeSegments, Options.MinObstacleDistance)
                ).ToList();
        //FreeEdgeChains =
        //    adjGraph
        //        .GetEdgeChains()
        //        .Select(c =>
        //            c.SimplifyZigzagPolyline(mazeSegments, 0, Options.MinObstacleDistance)
        //        ).ToList();

        // Create finalized free edges from simplified polyline paths
        FreeEdges = FreeEdgeChains.SelectMany(
            c => c.ToLineSegments(false)
        ).ToArray();

        // Re-create adjacency graph on finalized free edges
        AdjacencyGraph = ConstructAdjacencyGraph(FreeEdges, 0);
    }


    public ImageSharpDrawingBoard CreateDrawingBoard(int width, Color backgroundColor)
    {
        return ImageSharpDrawingBoard.Create(
            WorldToScreenMap2D.FromScreenWidth(
                BoundingBox, 
                width, 
                0
            ),
            backgroundColor
        );
    }
    
    public ImageSharpDrawingBoard CreateDrawingBoard(int width, int screenMargin, Color backgroundColor)
    {
        return ImageSharpDrawingBoard.Create(
            WorldToScreenMap2D.FromScreenWidth(
                BoundingBox, 
                width, 
                screenMargin
            ),
            backgroundColor
        );
    }

    /// <summary>
    /// Define the sites (centers) of Voronoi cells (regions).
    /// Each line segment is divided into one or more sites. The
    /// distance between two sites on a single line segment is made
    /// close to maxSegmentLength
    /// </summary>
    private HashSet<LinFloat64Vector2D> ConstructVoronoiSites()
    {
        var voronoiSites = new HashSet<LinFloat64Vector2D>(
            PointEqualityComparer
        );

        foreach (var (p1, p2) in MazeSegments)
        {
            var length = p2.GetDistanceToPoint(p1);

            if (length <= Options.MaxVoronoiSiteDistance)
            {
                voronoiSites.Add(p1);
                voronoiSites.Add(p2);
                continue;
            }

            var n = (int)Math.Ceiling(length / Options.MaxVoronoiSiteDistance);

            voronoiSites.AddRange(
                0d.GetLinearRange(1d, n + 1).Lerp(p1, p2)
            );
        }

        return voronoiSites;
    }

    /// <summary>
    /// Build an undirected adjacency graph from the free path edges of obstacle-avoiding Voronoi roadmap
    /// </summary>
    /// <param name="freeEdges"></param>
    /// <param name="minLeafEdgeLength"></param>
    /// <returns></returns>
    private UndirectedAdjacencyGraphEuclidean2D ConstructAdjacencyGraph(IReadOnlyList<Float64LineSegment2D> freeEdges, double minLeafEdgeLength)
    {
        var adjGraph = 
            UndirectedAdjacencyGraphEuclidean2D.Create(
                PointEqualityComparer,
                freeEdges
            );

        if (minLeafEdgeLength > 0)
            adjGraph.RemoveLeafEdges(
                (p1, p2) => 
                    p1.GetDistanceToPoint(p2) < minLeafEdgeLength
            );

        return adjGraph;
    }



}