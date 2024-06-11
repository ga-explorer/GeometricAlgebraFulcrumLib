namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;

public interface IRotateMap2D :
    IAffineMap2D
{
    IRotateMap2D InverseRotateMap();
}