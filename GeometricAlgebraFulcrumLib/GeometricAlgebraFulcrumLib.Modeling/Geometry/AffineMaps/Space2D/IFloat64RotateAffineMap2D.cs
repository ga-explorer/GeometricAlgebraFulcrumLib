namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;

public interface IFloat64RotateAffineMap2D :
    IFloat64AffineMap2D
{
    IFloat64RotateAffineMap2D InverseRotateMap();
}