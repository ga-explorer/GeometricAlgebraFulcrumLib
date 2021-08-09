namespace GeometricAlgebraFulcrumLib.Visuals.Geometry.Space3D
{
    public interface IGavGeometry3D :
        IGavGeometry
    {
        GavGeometryContext3D GeometryContext { get; }
    }
}