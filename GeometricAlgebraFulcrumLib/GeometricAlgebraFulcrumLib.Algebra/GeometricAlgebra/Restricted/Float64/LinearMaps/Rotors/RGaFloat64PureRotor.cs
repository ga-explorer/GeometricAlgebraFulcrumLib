using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;

/// <summary>
/// A pure rotor is the exponential of a 2-blade. The geometric product of
/// the rotor with its reverse is one. The squared norm of the 2-blade could either
/// be positive, zero, or negative. Each case has its own formulation for the exponential
/// See Section 7.4 of "Geometric Algebra for Computer Science"
/// </summary>
public sealed class RGaFloat64PureRotor
    : RGaFloat64RotorBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64PureRotor Create(double scalarPart, RGaFloat64Bivector bivectorPart)
    {
        return new RGaFloat64PureRotor(
            scalarPart + bivectorPart,
            scalarPart - bivectorPart
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64PureRotor Create(RGaFloat64Multivector multivector)
    {
        return new RGaFloat64PureRotor(
            multivector,
            multivector.Reverse()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator RGaFloat64Multivector(RGaFloat64PureRotor rotor)
    {
        return rotor.Multivector;
    }


    public RGaFloat64Multivector Multivector { get; }

    public RGaFloat64Multivector MultivectorReverse { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaFloat64PureRotor(double scalarPart, RGaFloat64Bivector bivectorPart)
        : base(bivectorPart.Processor)
    {
        Multivector = scalarPart + bivectorPart;
        MultivectorReverse = scalarPart - bivectorPart;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaFloat64PureRotor(RGaFloat64Multivector multivector, RGaFloat64Multivector multivectorReverse)
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
    public RGaFloat64PureRotor GetPureRotorInverse()
    {
        return new RGaFloat64PureRotor(
            MultivectorReverse, 
            Multivector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaFloat64Rotor GetRotorInverse()
    {
        return GetPureRotorInverse();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector OmMap(RGaFloat64Vector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Bivector OmMap(RGaFloat64Bivector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetHigherKVectorPart(mv.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector OmMap(RGaFloat64Multivector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetMultivector()
    {
        return Multivector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetMultivectorReverse()
    {
        return MultivectorReverse;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetMultivectorInverse()
    {
        return MultivectorReverse;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetScalingFactor()
    {
        return 1d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<LinFloat64PolarAngle, RGaFloat64Bivector> GetEuclideanAngleBivector()
    {
        var halfAngle = 
            Multivector.GetScalarPart().ArcCos();

        var angle = 
            halfAngle.DoublePolarAngle();

        var bivector = 
            Multivector.GetBivectorPart() / halfAngle.SinValue;

        return new Tuple<LinFloat64PolarAngle, RGaFloat64Bivector>(
            angle,
            bivector
        );
    }
}