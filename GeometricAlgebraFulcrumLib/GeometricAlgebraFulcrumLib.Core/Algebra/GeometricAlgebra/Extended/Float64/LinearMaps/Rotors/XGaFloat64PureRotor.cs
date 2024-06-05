using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors;

/// <summary>
/// A pure rotor is the exponential of a 2-blade. The geometric product of
/// the rotor with its reverse is one. The squared norm of the 2-blade could either
/// be positive, zero, or negative. Each case has its own formulation for the exponential
/// See Section 7.4 of "Geometric Algebra for Computer Science"
/// </summary>
public sealed class XGaFloat64PureRotor
    : XGaFloat64RotorBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureRotor Create(double scalarPart, XGaFloat64Bivector bivectorPart)
    {
        return new XGaFloat64PureRotor(
            scalarPart + bivectorPart,
            scalarPart - bivectorPart
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureRotor Create(XGaFloat64Multivector multivector)
    {
        return new XGaFloat64PureRotor(
            multivector,
            multivector.Reverse()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator XGaFloat64Multivector(XGaFloat64PureRotor rotor)
    {
        return rotor.Multivector;
    }


    public XGaFloat64Multivector Multivector { get; }

    public XGaFloat64Multivector MultivectorReverse { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaFloat64PureRotor(double scalarPart, XGaFloat64Bivector bivectorPart)
        : base(bivectorPart.Processor)
    {
        Multivector = scalarPart + bivectorPart;
        MultivectorReverse = scalarPart - bivectorPart;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaFloat64PureRotor(XGaFloat64Multivector multivector, XGaFloat64Multivector multivectorReverse)
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
        if (Multivector.IsEven(2))
            return false;

        // Make sure storage gp reverse(storage) == 1
        var gp = 
            Multivector.Gp(MultivectorReverse);

        if (!gp.IsScalar())
            return false;

        var diff = gp.Scalar() - 1;

        return diff.IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64PureRotor GetPureRotorInverse()
    {
        return new XGaFloat64PureRotor(
            MultivectorReverse, 
            Multivector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaFloat64Rotor GetRotorInverse()
    {
        return GetPureRotorInverse();
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetScalingFactor()
    {
        return 1d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<LinFloat64PolarAngle, XGaFloat64Bivector> GetEuclideanAngleBivector()
    {
        var halfAngle = 
            Multivector.GetScalarPart().CosToPolarAngle();

        var angle = 
            halfAngle.DoublePolarAngle();

        var bivector = 
            Multivector.GetBivectorPart() / halfAngle.Sin();

        return new Tuple<LinFloat64PolarAngle, XGaFloat64Bivector>(
            angle,
            bivector
        );
    }
}