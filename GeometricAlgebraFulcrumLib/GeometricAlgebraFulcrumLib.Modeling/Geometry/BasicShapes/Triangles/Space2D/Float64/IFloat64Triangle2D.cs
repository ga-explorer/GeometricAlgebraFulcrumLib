namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space2D.Float64;

public interface IFloat64Triangle2D :
    IFloat64FiniteGeometricShape2D
{
    double Point1X { get; }

    double Point1Y { get; }

    double Point2X { get; }

    double Point2Y { get; }

    double Point3X { get; }

    double Point3Y { get; }
}