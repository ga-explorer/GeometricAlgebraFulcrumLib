namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Points;

public interface IFloat64PointPair3D : 
    IFloat64FiniteGeometricShape3D
{
    double Point1X { get; }

    double Point1Y { get; }

    double Point1Z { get; }


    double Point2X { get; }

    double Point2Y { get; }

    double Point2Z { get; }
}