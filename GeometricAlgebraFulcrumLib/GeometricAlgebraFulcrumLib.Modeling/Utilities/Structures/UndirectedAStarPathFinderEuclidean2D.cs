using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Undirected;

namespace GeometricAlgebraFulcrumLib.Modeling.Utilities.Structures;

public class UndirectedAStarPathFinderEuclidean2D :
    UndirectedAStarPathFinder<LinFloat64Vector2D>
{
    public UndirectedAdjacencyGraphEuclidean2D EuclideanGraph { get; }


    public UndirectedAStarPathFinderEuclidean2D(UndirectedAdjacencyGraphEuclidean2D graph) 
        : base(graph, UndirectedAdjacencyGraphEuclidean2D.DefaultDistanceFunction, UndirectedAdjacencyGraphEuclidean2D.DefaultDistanceFunction)
    {
        EuclideanGraph = graph;
    }


    public Float64Polyline2D FindPath(double startX, double startY, double goalX, double goalY, Float64LineSegment2DBvh obstacles, double safetyDistance)
    {
        return FindPath(
            LinFloat64Vector2D.Create(startX, startY),
            LinFloat64Vector2D.Create(goalX, goalY),
            obstacles, 
            safetyDistance
        );
    }

    public Float64Polyline2D FindPath(LinFloat64Vector2D start, LinFloat64Vector2D goal, Float64LineSegment2DBvh obstacles, double safetyDistance)
    {
        var (found1, p1) = 
            EuclideanGraph.TryGetNearestPoint(start, obstacles, safetyDistance);

        if (!found1) 
            return new Float64Polyline2D(start);
        
        var (found2, p2) = 
            EuclideanGraph.TryGetNearestPoint(goal, obstacles, safetyDistance);
        
        if (!found2) 
            return new Float64Polyline2D(start);

        var (path, _) = 
            base.FindPath(p1, p2);

        if (start.IsNearEqual(p1))
            path[0] = start;
        else
            path.Insert(0, start);

        if (goal.IsNearEqual(p2))
            path[^1] = goal;
        else
            path.Add(goal);

        return path.ToPolyline();
    }
}