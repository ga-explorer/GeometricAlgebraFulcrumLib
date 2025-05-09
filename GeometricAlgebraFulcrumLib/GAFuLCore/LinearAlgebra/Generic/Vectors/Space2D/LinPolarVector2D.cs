using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;

public sealed class LinPolarVector2D<T> :
    ILinPolarVector2D<T>
{
    public IScalarProcessor<T> ScalarProcessor 
        => R.ScalarProcessor;

    public int VSpaceDimensions
        => 2;

    public LinPolarAngle<T> Theta { get; }

    public Scalar<T> R { get; }

    public Scalar<T> X
        => R * Theta.Cos();

    public Scalar<T> Y
        => R * Theta.Sin();

    public Scalar<T> Item1
        => X;

    public Scalar<T> Item2
        => Y;

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarVector2D(IScalar<T> r, T theta)
    {
        R = r.ToScalar();
        Theta = r.ScalarProcessor.CreatePolarAngleFromRadians(theta);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarVector2D(IScalar<T> r, LinAngle<T> theta)
    {
        R = r.ToScalar();
        Theta = theta.ToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarVector2D(IScalar<T> theta)
    {
        R = theta.ScalarProcessor.One;
        Theta = theta.ScalarProcessor.CreatePolarAngleFromRadians(theta.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarVector2D(LinAngle<T> theta)
    {
        R = theta.ScalarProcessor.One;
        Theta = theta.ToPolarAngle();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return R.IsValid() &&
               Theta.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsUnitVector()
    {
        return R.IsOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearUnitVector()
    {
        return R.IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return new StringBuilder()
            .Append("Polar Position< r: ")
            .Append(R)
            .Append(", theta: ")
            .Append(Theta)
            .Append(" >")
            .ToString();
    }
}