using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

/// <summary>
/// This represents a beam (a pair of rays with a common origin) in 2D space
/// </summary>
public sealed class Float64Beam2D : 
    IFloat64Beam2D
{
    public static Float64Beam2D Create(ILinFloat64Vector2D origin, ILinFloat64Vector2D direction1, ILinFloat64Vector2D direction2)
    {
        return new Float64Beam2D(
            origin.X, origin.Y,
            direction1.X, direction1.Y,
            direction2.X, direction2.Y
        );
    }


    public double OriginX { get; }

    public double OriginY { get; }

    public double Direction1X { get; }

    public double Direction1Y { get; }

    public double Direction2X { get; }

    public double Direction2Y { get; }

    public bool IsValid()
    {
        return !double.IsNaN(OriginX) &&
               !double.IsNaN(OriginY) &&
               !double.IsNaN(Direction1X) &&
               !double.IsNaN(Direction1Y) &&
               !double.IsNaN(Direction2X) &&
               !double.IsNaN(Direction2Y);
    }


    internal Float64Beam2D(double pX, double pY, double v1X, double v1Y, double v2X, double v2Y)
    {
        OriginX = pX;
        OriginY = pY;

        Direction1X = v1X;
        Direction1Y = v1Y;

        Direction2X = v2X;
        Direction2Y = v2Y;
    }
}