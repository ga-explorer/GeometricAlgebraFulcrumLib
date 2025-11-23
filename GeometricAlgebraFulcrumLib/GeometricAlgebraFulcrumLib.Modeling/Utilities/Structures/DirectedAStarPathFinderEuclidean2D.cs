using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Directed;

namespace GeometricAlgebraFulcrumLib.Modeling.Utilities.Structures
{
    public class DirectedAStarPathFinderEuclidean2D :
        DirectedAStarPathFinder<LinFloat64Vector2D>
    {
        public DirectedAdjacencyGraphEuclidean2D EuclideanGraph { get; }
        

        public DirectedAStarPathFinderEuclidean2D(DirectedAdjacencyGraphEuclidean2D graph) 
            : base(graph, DirectedAdjacencyGraphEuclidean2D.DefaultDistanceFunction, DirectedAdjacencyGraphEuclidean2D.DefaultDistanceFunction)
        {
            EuclideanGraph = graph;
        }

        
        public Float64Polyline2D FindPathPolyline(double startX, double startY, double goalX, double goalY)
        {
            return FindPathPolyline(
                LinFloat64Vector2D.Create(startX, startY),
                LinFloat64Vector2D.Create(goalX, goalY)
            );
        }

        public Float64Polyline2D FindPathPolyline(LinFloat64Vector2D start, LinFloat64Vector2D goal)
        {
            var p1 = EuclideanGraph.GetNearestNode(start);
            var p2 = EuclideanGraph.GetNearestNode(goal);

            var (path, _) = base.FindPath(p1, p2);

            if (!start.IsNearEqual(p1))
                path.Insert(0, start);

            if (!goal.IsNearEqual(p2))
                path.Add(goal);

            return path.ToPolyline();
        }
    }
}
