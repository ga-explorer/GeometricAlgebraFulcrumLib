using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Rotors;

/// <summary>
/// A pure rotor is the exponential of a 2-blade. The geometric product of
/// the rotor with its reverse is one. The squared norm of the 2-blade could either
/// be positive, zero, or negative. Each case has its own formulation for the exponential
/// See Section 7.4 of "Geometric Algebra for Computer Science"
/// </summary>
public sealed class XGaFloat64PureRotor
    : XGaFloat64RotorBase
{
    
    public static XGaFloat64PureRotor Create(double scalarPart, XGaFloat64Bivector bivectorPart)
    {
        return new XGaFloat64PureRotor(
            scalarPart + bivectorPart,
            scalarPart - bivectorPart
        );
    }

    
    public static XGaFloat64PureRotor Create(XGaFloat64Multivector multivector)
    {
        return new XGaFloat64PureRotor(
            multivector,
            multivector.Reverse()
        );
    }


    
    public static implicit operator XGaFloat64Multivector(XGaFloat64PureRotor rotor)
    {
        return rotor.Multivector;
    }


    public XGaFloat64Multivector Multivector { get; }

    public XGaFloat64Multivector MultivectorReverse { get; }


    
    private XGaFloat64PureRotor(double scalarPart, XGaFloat64Bivector bivectorPart)
        : base(bivectorPart.Processor)
    {
        Multivector = scalarPart + bivectorPart;
        MultivectorReverse = scalarPart - bivectorPart;
    }
        
    
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

    
    public XGaFloat64PureRotor GetPureRotorInverse()
    {
        return new XGaFloat64PureRotor(
            MultivectorReverse, 
            Multivector
        );
    }

    
    public override IXGaFloat64Rotor GetRotorInverse()
    {
        return GetPureRotorInverse();
    }
        

    
    public override XGaFloat64Vector OmMap(XGaFloat64Vector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetVectorPart();
    }

    
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetBivectorPart();
    }

    
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetHigherKVectorPart(mv.Grade);
    }
        
    
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse);
    }


    
    public override XGaFloat64Multivector GetMultivector()
    {
        return Multivector;
    }

    
    public override XGaFloat64Multivector GetMultivectorReverse()
    {
        return MultivectorReverse;
    }

    
    public override XGaFloat64Multivector GetMultivectorInverse()
    {
        return MultivectorReverse;
    }
        
    
    public override double GetScalingFactor()
    {
        return 1d;
    }

    
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


    
    public XGaFloat64PureScalingRotor CreatePureScalingRotor(double scalingFactor)
    {
        var s = scalingFactor.Sqrt();
        var scalarPart = s * Multivector.Scalar();
        var bivectorPart = s * Multivector.GetBivectorPart();

        return XGaFloat64PureScalingRotor.Create(
            scalarPart,
            bivectorPart
        );
    }
}