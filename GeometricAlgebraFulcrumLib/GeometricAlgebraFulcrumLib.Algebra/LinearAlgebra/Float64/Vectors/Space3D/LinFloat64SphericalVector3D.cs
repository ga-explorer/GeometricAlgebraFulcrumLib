﻿using System.Runtime.CompilerServices;
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

    public Float64Scalar X
        => R * Theta.Sin() * Phi.Cos();

    public Float64Scalar Y
        => R * Theta.Sin() * Phi.Sin();

    public Float64Scalar Z
        => R * Theta.Cos();

    public Float64Scalar Item1
        => X;

    public Float64Scalar Item2
        => Y;

    public Float64Scalar Item3
        => Z;


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
    public bool IsNearUnitVector(double epsilon = 1E-12)
    {
        return R.IsNearOne(epsilon);
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