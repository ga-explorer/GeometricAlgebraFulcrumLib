using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Rotors;

/// <summary>
/// A pure rotor is the exponential of a 2-blade. The geometric product of
/// the rotor with its reverse is one. The squared norm of the 2-blade could either
/// be positive, zero, or negative. Each case has its own formulation for the exponential
/// See Section 7.4 of "Geometric Algebra for Computer Science"
/// </summary>
public sealed class XGaFloat64PureScalingRotor
    : XGaFloat64ScalingRotorBase
{
    
    internal static XGaFloat64PureScalingRotor CreateIdentity(XGaFloat64Processor metric)
    {
        return new XGaFloat64PureScalingRotor(metric, 1d);
    }

    
    internal static XGaFloat64PureScalingRotor Create(XGaFloat64Processor metric, double scalarPart)
    {
        return new XGaFloat64PureScalingRotor(metric, scalarPart);
    }

    
    internal static XGaFloat64PureScalingRotor Create(double scalarPart, XGaFloat64Bivector bivectorPart)
    {
        return new XGaFloat64PureScalingRotor(
            scalarPart + bivectorPart,
            scalarPart - bivectorPart
        );
    }

    
    internal static XGaFloat64PureScalingRotor Create(XGaFloat64Multivector multivector)
    {
        return new XGaFloat64PureScalingRotor(
            multivector,
            multivector.Reverse()
        );
    }
       
    
    internal static XGaFloat64PureScalingRotor Create(XGaFloat64Multivector multivector, XGaFloat64Multivector multivectorReverse)
    {
        return new XGaFloat64PureScalingRotor(
            multivector,
            multivectorReverse
        );
    }


    
    public static implicit operator XGaFloat64Multivector(XGaFloat64PureScalingRotor rotor)
    {
        return rotor.Multivector;
    }


    public XGaFloat64Multivector Multivector { get; }

    public XGaFloat64Multivector MultivectorReverse { get; }


    private XGaFloat64PureScalingRotor(XGaFloat64Processor metric, double scalarPart)
        : base(metric)
    {
        Multivector = Processor.Scalar(scalarPart);
        MultivectorReverse = Multivector;
    }

    private XGaFloat64PureScalingRotor(double scalarPart, XGaFloat64Bivector bivectorPart)
        : base(bivectorPart.Processor)
    {
        Multivector = scalarPart + bivectorPart;
        MultivectorReverse = scalarPart - bivectorPart;
    }

    private XGaFloat64PureScalingRotor(XGaFloat64Multivector multivector, XGaFloat64Multivector multivectorReverse)
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

    
    public XGaFloat64PureScalingRotor GetPureScalingRotorInverse()
    {
        var scalingFactor = GetScalingFactor();
            
        return new XGaFloat64PureScalingRotor(
            MultivectorReverse / scalingFactor,
            Multivector / scalingFactor
        );
    }

    
    public override double GetScalingFactor()
    {
        return Multivector.Sp(MultivectorReverse).Scalar();
    }
        
    
    public XGaFloat64PureRotor GetPureRotor()
    {
        var mv = Metric.IsEuclidean
            ? Multivector / Multivector.Sp(MultivectorReverse).Sqrt()
            : Multivector / Multivector.Sp(MultivectorReverse).SqrtOfAbs();

        return XGaFloat64PureRotor.Create(mv);
    }

    
    public override IXGaFloat64ScalingRotor GetScalingRotorInverse()
    {
        return GetPureScalingRotorInverse();
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

    
    public LinFloat64Vector2D OmMap(ILinFloat64Vector2D vector)
    {
        return Multivector.Gp(vector.ToXGaFloat64Vector(Processor)).Gp(MultivectorReverse).VectorPartToVector2D();
    }

    
    public LinFloat64Vector3D OmMap(ILinFloat64Vector3D vector)
    {
        return Multivector.Gp(vector.ToXGaFloat64Vector(Processor)).Gp(MultivectorReverse).VectorPartToVector3D();
    }


    
    public XGaFloat64PureScalingRotor CreatePureScalingRotor(double scalingFactor)
    {
        var mv = scalingFactor.Sqrt() * Multivector;
        var mvReverse = mv.Reverse();

        return new XGaFloat64PureScalingRotor(
            mv,
            mvReverse
        );
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
        
}