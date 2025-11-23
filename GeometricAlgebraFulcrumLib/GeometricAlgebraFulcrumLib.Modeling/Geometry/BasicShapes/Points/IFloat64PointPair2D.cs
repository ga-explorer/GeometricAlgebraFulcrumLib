namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Points;

public interface IFloat64PointPair2D : 
    IFloat64BoundedShape2D
{
    double Point1X { get; }

    double Point1Y { get; }

    double Point2X { get; }

    double Point2Y { get; }
}