namespace NumericalGeometryLib.BasicMath.Maps.Space2D
{
    public interface IRotateMap2D :
        IAffineMap2D
    {
        IRotateMap2D InverseRotateMap();
    }
}