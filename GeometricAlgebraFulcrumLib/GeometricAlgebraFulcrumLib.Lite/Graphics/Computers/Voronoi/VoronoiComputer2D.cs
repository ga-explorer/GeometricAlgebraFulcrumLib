using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Computers.Voronoi
{
    public class VoronoiComputer2D
    {
        public DelaunayTriangulation2D ComputeDelaunayTriangulation(IEnumerable<IFloat64Vector2D> points)
        {
            var pointsList = new VoronoiPointsList(points);
            var trianglesList = new List<VoronoiTriangle2D>
            {
                pointsList.BoundingTriangle
            };

            for (var pointIndex = 0; pointIndex < pointsList.DataPointsCount; pointIndex++)
            {
                var point = pointsList[pointIndex];
                var edgesList = new List<VoronoiEdge2D>();

                foreach (var triangle in trianglesList)
                {
                    if (!triangle.CircumcircleContainsVa(point))//(point.X, point.Y))
                        continue;

                    triangle.IsBad = true;
                    edgesList.AddRange(triangle.Edges);
                }

                trianglesList.RemoveAll(t => t.IsBad);

                for (var edgeIndex1 = 0; edgeIndex1 < edgesList.Count; edgeIndex1++)
                {
                    var edge1 = edgesList[edgeIndex1];

                    for (var edgeIndex2 = edgeIndex1 + 1; edgeIndex2 < edgesList.Count; edgeIndex2++)
                    {
                        var edge2 = edgesList[edgeIndex2];

                        if (!edge1.IsSameEdge(edge2))
                            continue;

                        edge1.IsBad = true;
                        edge2.IsBad = true;
                    }
                }

                trianglesList.AddRange(
                    edgesList
                        .Where(e => !e.IsBad)
                        .Select(
                            edge => new VoronoiTriangle2D(edge, pointIndex)
                        )
                );
            }

            trianglesList.RemoveAll(t => 
                t.ContainsBoundingTrianglePoint()
            );

            return new DelaunayTriangulation2D(
                pointsList,
                trianglesList
            );
        }
    }
}