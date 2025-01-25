namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Circles;

public interface IFloat64CircleSegment3D : 
    IFloat64FiniteGeometricShape3D
{
    double Direction1X { get; }

    double Direction1Y { get; }

    double Direction1Z { get; }


    double Direction2X { get; }

    double Direction2Y { get; }

    double Direction2Z { get; }


    double CenterX { get; }

    double CenterY { get; }

    double CenterZ { get; }


    double OriginX { get; }

    double OriginY { get; }

    double OriginZ { get; }


    double TurnsValue { get; }
}