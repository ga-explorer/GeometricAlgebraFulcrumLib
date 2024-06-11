namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

public interface IRotateMap3D :
    IAffineMap3D
{
    IRotateMap3D InverseRotateMap();
}