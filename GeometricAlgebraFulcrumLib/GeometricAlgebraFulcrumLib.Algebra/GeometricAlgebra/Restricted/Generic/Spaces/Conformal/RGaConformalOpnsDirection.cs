﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

public class RGaConformalOpnsDirection<T> :
    RGaConformalBlade<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalOpnsDirection<T> operator *(RGaConformalOpnsDirection<T> mv, T s)
    {
        return new RGaConformalOpnsDirection<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalOpnsDirection<T> operator *(T s, RGaConformalOpnsDirection<T> mv)
    {
        return new RGaConformalOpnsDirection<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalOpnsDirection<T> operator /(RGaConformalOpnsDirection<T> mv, T s)
    {
        return new RGaConformalOpnsDirection<T>(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override RGaKVector<T> Blade { get; }
        

    internal RGaConformalOpnsDirection(RGaConformalSpace<T> space, RGaKVector<T> blade)
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
    public RGaConformalOpnsDirection<T> ToIpnsDirection()
    {
        return new RGaConformalOpnsDirection<T>(
            Space,
            Blade.Dual(Space.VSpaceDimensions)
        );
    }
}