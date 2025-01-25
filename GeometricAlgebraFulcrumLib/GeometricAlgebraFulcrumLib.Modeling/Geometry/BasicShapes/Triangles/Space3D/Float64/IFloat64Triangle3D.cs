namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space3D.Float64;

public interface IFloat64Triangle3D :
    IFloat64FiniteGeometricShape3D,
    IIntersectable
{
    double Point1X { get; }

    double Point1Y { get; }

    double Point1Z { get; }


    double Point2X { get; }

    double Point2Y { get; }

    double Point2Z { get; }


    double Point3X { get; }

    double Point3Y { get; }

    double Point3Z { get; }
}