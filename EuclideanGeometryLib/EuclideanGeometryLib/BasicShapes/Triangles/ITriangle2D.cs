namespace EuclideanGeometryLib.BasicShapes.Triangles
{
    public interface ITriangle2D : IFiniteGeometricShape2D
    {
        double Point1X { get; }

        double Point1Y { get; }

        double Point2X { get; }

        double Point2Y { get; }

        double Point3X { get; }

        double Point3Y { get; }
    }
}