namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.SpaceND;

public readonly struct RayND
{
    public readonly float[] Origin;
    public readonly float[] Direction;
    public int Dimensions => Origin.Length;

    public RayND(float[] origin, float[] direction)
    {
        Origin = origin;
        Direction = direction;
    }
}