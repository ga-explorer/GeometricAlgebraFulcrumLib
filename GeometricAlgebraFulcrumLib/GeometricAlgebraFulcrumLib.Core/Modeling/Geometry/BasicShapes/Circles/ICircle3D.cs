namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Circles;

public interface ICircle3D : IFiniteGeometricShape3D
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


    double Radius { get; }

    double RadiusSquared { get; }
}