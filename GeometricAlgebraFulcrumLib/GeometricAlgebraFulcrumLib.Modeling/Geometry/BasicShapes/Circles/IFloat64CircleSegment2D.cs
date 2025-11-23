namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Circles;

public interface IFloat64CircleSegment2D : 
    IFloat64BoundedShape2D
{
    double CenterX { get; }

    double CenterY { get; }

    double OriginX { get; }

    double OriginY { get; }

    double TurnsValue { get; }
}