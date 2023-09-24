namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Computers.Voronoi
{
    public class DelaunayTriangulation2D
    {
        public VoronoiPointsList Points { get; }

        public VoronoiTriangle2D[] Triangles { get; }

        public VoronoiEdge2D[] Edges { get; }


        internal DelaunayTriangulation2D(VoronoiPointsList points, IEnumerable<VoronoiTriangle2D> triangles)
        {
            Points = points;
            Triangles = triangles.ToArray();

            Edges = new VoronoiEdge2D[3 * Triangles.Length];

            var i = 0;
            foreach (var triangle in Triangles)
            {
                Edges[i] = triangle.Edge12;
                Edges[i + 1] = triangle.Edge23;
                Edges[i + 2] = triangle.Edge31;

                i += 3;
            }
        }

        internal DelaunayTriangulation2D(VoronoiPointsList points, IEnumerable<VoronoiTriangle2D> triangles, IEnumerable<VoronoiEdge2D> edges)
        {
            Points = points;
            Triangles = triangles.ToArray();
            Edges = edges.ToArray();
        }
    }
}