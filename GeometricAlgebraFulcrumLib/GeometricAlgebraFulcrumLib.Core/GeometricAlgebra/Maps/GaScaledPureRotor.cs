﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Maps;

public class GaScaledPureRotor :
    IAlgebraicElement
{
    public XGaFloat64Processor Processor 
        => Multivector.Processor;

    public double ScalingFactor { get; }

    public XGaFloat64Multivector Multivector { get; }

    public XGaFloat64Multivector MultivectorReverse { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GaScaledPureRotor(XGaFloat64Multivector multivector)
    {
        Multivector = multivector;
        MultivectorReverse = multivector.Reverse();
        ScalingFactor = Multivector.Sp(MultivectorReverse).ScalarValue;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GaScaledPureRotor(XGaFloat64Multivector multivector, XGaFloat64Multivector multivectorReverse)
    {
        Multivector = multivector;
        MultivectorReverse = multivectorReverse;
        ScalingFactor = multivector.Sp(multivectorReverse).ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GaScaledPureRotor(XGaFloat64Multivector multivector, XGaFloat64Multivector multivectorReverse, double scalingFactor)
    {
        Multivector = multivector;
        MultivectorReverse = multivectorReverse;
        ScalingFactor = scalingFactor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Multivector.IsValid() &&
               MultivectorReverse.IsValid() &&
               Multivector.Gp(MultivectorReverse).IsScalar();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalingFactor()
    {
        return Multivector.Sp(MultivectorReverse).ScalarValue;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GaScaledPureRotor GetPureRotor()
    {
        var normSquared = Multivector.Sp(MultivectorReverse).ScalarValue;

        var mv = Processor.IsEuclidean
            ? Multivector.Divide(normSquared.Sqrt())
            : Multivector.Divide(normSquared.SqrtOfAbs());

        return new GaScaledPureRotor(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GaScaledPureRotor GetPureScaledRotorInverse()
    {
        var scalingFactor = GetScalingFactor();
            
        return new GaScaledPureRotor(
            MultivectorReverse.Divide(scalingFactor),
            Multivector.Divide(scalingFactor)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D OmMap(ILinFloat64Vector2D vector)
    {
        return Multivector.Gp(vector.ToXGaFloat64Vector(Processor)).Gp(MultivectorReverse).VectorPartToVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D OmMap(ILinFloat64Vector3D vector)
    {
        return Multivector.Gp(vector.ToXGaFloat64Vector(Processor)).Gp(MultivectorReverse).VectorPartToVector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
    {
        return Multivector.Gp(multivector).Gp(MultivectorReverse);
    }
}