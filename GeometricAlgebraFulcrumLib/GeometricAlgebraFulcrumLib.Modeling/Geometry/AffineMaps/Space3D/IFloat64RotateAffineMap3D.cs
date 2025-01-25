namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

public interface IFloat64RotateAffineMap3D :
    IFloat64AffineMap3D
{
    IFloat64RotateAffineMap3D InverseRotateMap();
}