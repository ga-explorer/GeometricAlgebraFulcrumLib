namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Points;

public interface IPointPair2D : IFiniteGeometricShape2D
{
    double Point1X { get; }

    double Point1Y { get; }

    double Point2X { get; }

    double Point2Y { get; }
}