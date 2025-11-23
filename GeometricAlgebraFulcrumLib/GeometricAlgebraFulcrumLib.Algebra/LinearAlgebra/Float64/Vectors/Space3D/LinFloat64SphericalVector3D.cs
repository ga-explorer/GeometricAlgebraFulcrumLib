using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public sealed class LinFloat64SphericalVector3D :
    ILinFloat64SphericalVector3D
{
    public int VSpaceDimensions
        => 3;

    public LinFloat64PolarAngle Theta { get; }

    public LinFloat64PolarAngle Phi { get; }

    public Float64Scalar R { get; }

    public double X
        => R * Theta.Sin() * Phi.Cos();

    public double Y
        => R * Theta.Sin() * Phi.Sin();

    public double Z
        => R * Theta.Cos();

    public double Item1
        => X;

    public double Item2
        => Y;

    public double Item3
        => Z;
    
    public double this[int i] 
        => i switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => Float64Scalar.Zero
        };


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64SphericalVector3D(LinFloat64PolarAngle theta, LinFloat64PolarAngle phi)
    {
        Theta = theta.ToPolarAngleInPeriodicRange(Math.PI);
        Phi = phi;
        R = Float64Scalar.One;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64SphericalVector3D(LinFloat64PolarAngle theta, LinFloat64PolarAngle phi, Float64Scalar r)
    {
        Theta = theta.ToPolarAngleInPeriodicRange(Math.PI);
        Phi = phi;
        R = r.ScalarValue > 0 ? r : Float64Scalar.Zero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return R.IsValid() &&
               R.ScalarValue >= 0 &&
               Theta.IsValid() &&
               Phi.IsValid();
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
            .Append("Spherical Position< theta: ")
            .Append(Theta)
            .Append(", phi: ")
            .Append(Phi)
            .Append(", r: ")
            .Append(R.ToString("G"))
            .Append(" >")
            .ToString();
    }
}