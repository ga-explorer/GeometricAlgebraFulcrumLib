using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space2D;

public sealed class LinFloat64PolarVector2D :
    ILinFloat64PolarVector2D
{
    public int VSpaceDimensions
        => 2;

    public LinFloat64PolarAngle Theta { get; }

    public Float64Scalar R { get; }

    public Float64Scalar X
        => R * Theta.Cos();

    public Float64Scalar Y
        => R * Theta.Sin();

    public Float64Scalar Item1
        => X;

    public Float64Scalar Item2
        => Y;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarVector2D(Float64Scalar r, LinFloat64PolarAngle theta)
    {
        if (r.ScalarValue > 0)
        {
            R = r;
            Theta = theta;
        }
        else if (r.ScalarValue < 0)
        {
            R = -r;
            Theta = theta.OppositeAngle();
        }
        else
        {
            R = Float64Scalar.Zero;
            Theta = LinFloat64PolarAngle.Angle0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarVector2D(LinFloat64PolarAngle theta)
    {
        R = Float64Scalar.One;
        Theta = theta;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return R.IsValid() &&
               R.ScalarValue >= 0 &&
               Theta.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsUnitVector()
    {
        return R.IsOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearUnitVector(double zeroEpsilon = 1E-12)
    {
        return R.IsNearOne(zeroEpsilon);
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