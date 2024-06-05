﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Spaces.Conformal;

public class XGaConformalOpnsRound<T>
    : XGaConformalBlade<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalOpnsRound<T> operator *(XGaConformalOpnsRound<T> mv, T s)
    {
        return new XGaConformalOpnsRound<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalOpnsRound<T> operator *(T s, XGaConformalOpnsRound<T> mv)
    {
        return new XGaConformalOpnsRound<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalOpnsRound<T> operator /(XGaConformalOpnsRound<T> mv, T s)
    {
        return new XGaConformalOpnsRound<T>(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override XGaKVector<T> Blade { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalOpnsRound(XGaConformalSpace<T> space, XGaKVector<T> blade)
        : base(space)
    {
        Blade = blade;
    }

        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Square()
    {
        return Blade.SpSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalIpnsRound<T> ToIpnsRound()
    {
        return new XGaConformalIpnsRound<T>(
            Space,
            Blade.Dual(Space.VSpaceDimensions)
        );
    }
}