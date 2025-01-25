namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

public interface IFloat64LineSegment3D :
    IFloat64FiniteGeometricShape3D
{
    double Point1X { get; }

    double Point1Y { get; }

    double Point1Z { get; }


    double Point2X { get; }

    double Point2Y { get; }

    double Point2Z { get; }
}