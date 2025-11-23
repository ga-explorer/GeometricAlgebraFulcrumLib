using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public sealed class LinFloat64PolarVector2D :
    ILinFloat64PolarVector2D
{
    public int VSpaceDimensions
        => 2;

    public LinFloat64PolarAngle Theta { get; }

    public double R { get; }

    public double X
        => R * Theta.CosValue;

    public double Y
        => R * Theta.SinValue;

    public double Item1
        => X;

    public double Item2
        => Y;

    public double this[int i] 
        => i switch
        {
            0 => X,
            1 => Y,
            _ => Float64Scalar.Zero
        };
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarVector2D(double r, LinFloat64PolarAngle theta)
    {
        if (!r.IsValid())
            throw new ArgumentException(nameof(r));

        if (r > 0)
        {
            R = r;
            Theta = theta;
        }
        else if (r < 0)
        {
            R = -r;
            Theta = theta.OppositeAngle();
        }
        else
        {
            R = 0;
            Theta = LinFloat64PolarAngle.Angle0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarVector2D(LinFloat64PolarAngle theta)
    {
        R = 1;
        Theta = theta;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return R.IsValid() &&
               R >= 0 &&
               Theta.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsUnitVector()
    {
        return R.IsOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearUnitVector(double tolerance = Float64Utils.DefaultTolerance)
    {
        return R.IsNearOne(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return new StringBuilder()
            .Append("Polar Position< r: ")
            .Append(R.ToString("G"))
            .Append(", theta: ")
            .Append(Theta)
            .Append(" >")
            .ToString();
    }
}