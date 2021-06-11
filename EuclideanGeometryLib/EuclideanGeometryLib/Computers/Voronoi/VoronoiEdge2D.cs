using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.Borders.Space2D.Immutable;
using EuclideanGeometryLib.Borders.Space2D.Mutable;

namespace EuclideanGeometryLib.Computers.Voronoi
{
    public sealed class VoronoiEdge2D : ILineSegment2D
    {
        public int PointIndex1 { get; }

        public int PointIndex2 { get; }

        public VoronoiPointsList PointsList { get; }

        public double Point1X => PointsList[PointIndex1].X;

        public double Point1Y => PointsList[PointIndex1].Y;

        public double Point2X => PointsList[PointIndex2].X;

        public double Point2Y => PointsList[PointIndex2].Y;

        public bool IsValid
            => !double.IsNaN(Point1X) &&
               !double.IsNaN(Point1Y) &&
               !double.IsNaN(Point2X) &&
               !double.IsNaN(Point2Y);

        public bool IsInvalid
            => double.IsNaN(Point1X) || 
               double.IsNaN(Point1Y) ||
               double.IsNaN(Point2X) || 
               double.IsNaN(Point2Y);

        public bool IntersectionTestsEnabled { get; set; } = true;

        public bool IsBad { get; internal set; }


        internal VoronoiEdge2D(VoronoiPointsList pointsList, int pointIndex1, int pointIndex2)
        {
            PointsList = pointsList;

            PointIndex1 = pointIndex1;
            PointIndex2 = pointIndex2;
        }


        public bool IsSameEdge(VoronoiEdge2D edge)
        {
            return
                (edge.PointIndex1 == PointIndex1 && edge.PointIndex2 == PointIndex2) ||
                (edge.PointIndex1 == PointIndex2 && edge.PointIndex2 == PointIndex1);
        }

        public BoundingBox2D GetBoundingBox()
        {
            throw new System.NotImplementedException();
        }

        public MutableBoundingBox2D GetMutableBoundingBox()
        {
            throw new System.NotImplementedException();
        }
    }
}