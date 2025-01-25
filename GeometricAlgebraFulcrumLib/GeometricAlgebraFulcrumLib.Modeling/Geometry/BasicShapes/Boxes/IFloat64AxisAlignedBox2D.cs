namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Boxes;

public interface IFloat64AxisAlignedBox2D : 
    IFloat64FiniteGeometricShape2D
{
    double Corner1X { get; }

    double Corner1Y { get; }


    double Corner2X { get; }

    double Corner2Y { get; }
}