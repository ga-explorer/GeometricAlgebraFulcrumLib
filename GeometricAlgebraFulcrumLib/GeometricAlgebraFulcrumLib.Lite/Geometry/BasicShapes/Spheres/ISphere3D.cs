namespace GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Spheres
{
    public interface ISphere3D : IFiniteGeometricShape3D
    {
        double CenterX { get; }

        double CenterY { get; }

        double CenterZ { get; }

        double Radius { get; }

        double RadiusSquared { get; }
    }
}