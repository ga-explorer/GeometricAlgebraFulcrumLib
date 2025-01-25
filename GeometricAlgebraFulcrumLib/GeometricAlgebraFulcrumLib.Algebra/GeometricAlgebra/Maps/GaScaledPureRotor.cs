using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Maps;

public class GaScaledPureRotor :
    IAlgebraicElement
{
    public RGaFloat64Processor Processor 
        => Multivector.Processor;

    public double ScalingFactor { get; }

    public RGaFloat64Multivector Multivector { get; }

    public RGaFloat64Multivector MultivectorReverse { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GaScaledPureRotor(RGaFloat64Multivector multivector)
    {
        Multivector = multivector;
        MultivectorReverse = multivector.Reverse();
        ScalingFactor = Multivector.Sp(MultivectorReverse).ScalarValue;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GaScaledPureRotor(RGaFloat64Multivector multivector, RGaFloat64Multivector multivectorReverse)
    {
        Multivector = multivector;
        MultivectorReverse = multivectorReverse;
        ScalingFactor = multivector.Sp(multivectorReverse).ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GaScaledPureRotor(RGaFloat64Multivector multivector, RGaFloat64Multivector multivectorReverse, double scalingFactor)
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
        return Multivector.Gp(vector.ToRGaFloat64Vector(Processor)).Gp(MultivectorReverse).GetVectorPartAsVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D OmMap(ILinFloat64Vector3D vector)
    {
        return Multivector.Gp(vector.ToRGaFloat64Vector(Processor)).Gp(MultivectorReverse).GetVectorPartAsVector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector OmMap(RGaFloat64Multivector multivector)
    {
        return Multivector.Gp(multivector).Gp(MultivectorReverse);
    }
}