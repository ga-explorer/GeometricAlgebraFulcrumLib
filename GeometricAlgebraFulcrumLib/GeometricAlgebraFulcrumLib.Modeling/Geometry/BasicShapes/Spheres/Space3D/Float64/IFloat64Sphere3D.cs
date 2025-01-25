namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Spheres.Space3D.Float64;

public interface IFloat64Sphere3D :
    IFloat64FiniteGeometricShape3D
{
    double CenterX { get; }

    double CenterY { get; }

    double CenterZ { get; }

    double Radius { get; }

    double RadiusSquared { get; }
}