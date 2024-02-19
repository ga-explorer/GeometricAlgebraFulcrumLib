using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

public sealed class Float64SphericalVector3D : 
    IFloat64SphericalVector3D
{
    public int VSpaceDimensions 
        => 3;

    public Float64PlanarAngle Theta { get; }

    public Float64PlanarAngle Phi { get; }

    public Float64Scalar R { get; }

    public Float64Scalar X 
        => R * Theta.Sin() * Phi.Cos();

    public Float64Scalar Y 
        => R * Theta.Sin() * Phi.Sin();

    public Float64Scalar Z 
        => R * Theta.Cos();

    public double Item1 
        => X.Value;

    public double Item2 
        => Y.Value;

    public double Item3 
        => Z.Value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SphericalVector3D(Float64PlanarAngle theta, Float64PlanarAngle phi)
    {
        Theta = theta.GetAngleInPeriodicRange(Math.PI);
        Phi = phi.GetAngleInPositiveRange();
        R = Float64Scalar.One;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SphericalVector3D(Float64PlanarAngle theta, Float64PlanarAngle phi, Float64Scalar r)
    {
        Theta = theta.GetAngleInPeriodicRange(Math.PI);
        Phi = phi.GetAngleInPositiveRange();
        R = r.Value > 0 ? r : Float64Scalar.Zero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return R.IsValid() &&
               R.Value >= 0 &&
               Theta.IsValid() &&
               Phi.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsUnitVector()
    {
        return R.IsOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearUnitVector(double epsilon = 1E-12)
    {
        return R.IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return new StringBuilder()
            .Append("Spherical Position< theta: ")
            .Append(Theta.ToString())
            .Append(", phi: ")
            .Append(Phi.ToString())
            .Append(", r: ")
            .Append(R.ToString("G"))
            .Append(" >")
            .ToString();
    }
}