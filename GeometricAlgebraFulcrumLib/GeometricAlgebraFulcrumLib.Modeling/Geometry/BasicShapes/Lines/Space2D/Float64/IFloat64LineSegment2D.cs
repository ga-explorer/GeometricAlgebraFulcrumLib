namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

public interface IFloat64LineSegment2D :
    IFloat64FiniteGeometricShape2D
{
    double Point1X { get; }

    double Point1Y { get; }


    double Point2X { get; }

    double Point2Y { get; }
}