using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors;

/// <summary>
/// A pure rotor is the exponential of a 2-blade. The geometric product of
/// the rotor with its reverse is one. The squared norm of the 2-blade could either
/// be positive, zero, or negative. Each case has its own formulation for the exponential
/// See Section 7.4 of "Geometric Algebra for Computer Science"
/// </summary>
public sealed class XGaFloat64ScaledPureRotor
    : XGaFloat64ScaledRotorBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64ScaledPureRotor CreateIdentity(XGaFloat64Processor metric)
    {
        return new XGaFloat64ScaledPureRotor(metric, 1d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64ScaledPureRotor Create(XGaFloat64Processor metric, double scalarPart)
    {
        return new XGaFloat64ScaledPureRotor(metric, scalarPart);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64ScaledPureRotor Create(double scalarPart, XGaFloat64Bivector bivectorPart)
    {
        return new XGaFloat64ScaledPureRotor(
            scalarPart + bivectorPart,
            scalarPart - bivectorPart
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64ScaledPureRotor Create(XGaFloat64Multivector multivector)
    {
        return new XGaFloat64ScaledPureRotor(
            multivector,
            multivector.Reverse()
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator XGaFloat64Multivector(XGaFloat64ScaledPureRotor rotor)
    {
        return rotor.Multivector;
    }


    public XGaFloat64Multivector Multivector { get; }

    public XGaFloat64Multivector MultivectorReverse { get; }


    private XGaFloat64ScaledPureRotor(XGaFloat64Processor metric, double scalarPart)
        : base(metric)
    {
        Multivector = Processor.CreateScalar(scalarPart);
        MultivectorReverse = Multivector;
    }

    private XGaFloat64ScaledPureRotor(double scalarPart, XGaFloat64Bivector bivectorPart)
        : base(bivectorPart.Processor)
    {
        Multivector = scalarPart + bivectorPart;
        MultivectorReverse = scalarPart - bivectorPart;
    }

    private XGaFloat64ScaledPureRotor(XGaFloat64Multivector multivector, XGaFloat64Multivector multivectorReverse)
        : base(multivector.Processor)
    {
        Multivector = multivector;
        MultivectorReverse = multivectorReverse;
    }


    public override bool IsValid()
    {
        // Make sure the storage and its reverse are correct
        if (!(Multivector.Reverse() - MultivectorReverse).IsNearZero())
            return false;

        // Make sure storage contains only terms of grades 0,2
        if (Multivector.GetMaxGrade() <= 2 && !Multivector.ContainsKVectorPart(1))
            return false;

        // Make sure storage gp reverse(storage) == scalar
        var gp = 
            Multivector.Gp(MultivectorReverse);

        return gp.IsScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64ScaledPureRotor GetPureScaledRotorInverse()
    {
        var scalingFactor = GetScalingFactor();
            
        return new XGaFloat64ScaledPureRotor(
            MultivectorReverse / scalingFactor,
            Multivector / scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetScalingFactor()
    {
        return Multivector.Sp(MultivectorReverse).Scalar();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64PureRotor GetPureRotor()
    {
        var mv = Metric.IsEuclidean
            ? Multivector / Multivector.Sp(MultivectorReverse).Sqrt()
            : Multivector / Multivector.Sp(MultivectorReverse).SqrtOfAbs();

        return XGaFloat64PureRotor.Create(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaFloat64ScaledRotor GetScaledRotorInverse()
    {
        return GetPureScaledRotorInverse();
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMap(XGaFloat64Vector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetHigherKVectorPart(mv.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivector()
    {
        return Multivector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivectorReverse()
    {
        return MultivectorReverse;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivectorInverse()
    {
        return MultivectorReverse;
    }
        
}