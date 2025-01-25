namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Circles;

public interface IFloat64Circle2D : 
    IFloat64FiniteGeometricShape2D
{
    double CenterX { get; }

    double CenterY { get; }

    double Radius { get; }

    double RadiusSquared { get; }
}