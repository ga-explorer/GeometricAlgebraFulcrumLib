namespace GeometricAlgebraFulcrumLib.Visuals.Geometry.Space3D
{
    public interface IGeovGeometry3D :
        IGeovGeometry
    {
        GeovGeometryContext3D GeometryContext { get; }
    }
}