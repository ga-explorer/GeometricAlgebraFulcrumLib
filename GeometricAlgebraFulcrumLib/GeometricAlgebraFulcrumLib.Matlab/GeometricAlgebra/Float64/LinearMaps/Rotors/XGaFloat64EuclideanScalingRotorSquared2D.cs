using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Rotors;

public sealed class XGaFloat64EuclideanScalingRotorSquared2D
    : XGaFloat64ScalingRotorBase
{
    
    internal static XGaFloat64EuclideanScalingRotorSquared2D Create(XGaFloat64Processor processor, double scalar0)
    {
        return new XGaFloat64EuclideanScalingRotorSquared2D(processor, scalar0, 0d);
    }

    
    internal static XGaFloat64EuclideanScalingRotorSquared2D Create(XGaFloat64Processor processor, double scalar0, double scalar12)
    {
        return new XGaFloat64EuclideanScalingRotorSquared2D(processor, scalar0, scalar12);
    }


    //
    //public static implicit operator Multivector(PureScalingRotor rotor)
    //{
    //    return rotor.Multivector;
    //}


    public double Scalar0 { get; }

    public double Scalar12 { get; }


    private XGaFloat64EuclideanScalingRotorSquared2D(XGaFloat64Processor processor, double scalar0, double scalar12)
        : base(processor)
    {
        Scalar0 = scalar0;
        Scalar12 = scalar12;
    }


    
    public override bool IsValid()
    {
        return true;
    }

    
    public XGaFloat64EuclideanScalingRotorSquared2D GetPureScalingRotorSquared2DInverse()
    {
        var scalingFactorSquared = GetScalingFactorSquared();

        return new XGaFloat64EuclideanScalingRotorSquared2D(
            Processor,
            Scalar0 / scalingFactorSquared,
            -Scalar12 / scalingFactorSquared
        );
    }

    
    public override double GetScalingFactor()
    {
        return GetScalingFactorSquared().Sqrt();
    }
        
    
    public double GetScalingFactorSquared()
    {
        return Scalar0 * Scalar0 + Scalar12 * Scalar12;
    }

    
    public XGaFloat64EuclideanScalingRotorSquared2D GetPureRotorSquared2D()
    {
        var scalingFactor = 
            GetScalingFactor();

        return Create(
            Processor,
            Scalar0 / scalingFactor,
            Scalar12 / scalingFactor
        );
    }

    
    public override IXGaFloat64ScalingRotor GetScalingRotorInverse()
    {
        throw new NotImplementedException();
    }

    
    public XGaFloat64Vector OmMap(double u1, double u2)
    {
        var v1 = Scalar0 * u1 + Scalar12 * u2;
        var v2 = Scalar0 * u2 - Scalar12 * u1;

        return Processor.Vector(v1, v2);
    }

    
    public override XGaFloat64Vector OmMap(XGaFloat64Vector mv)
    {
        var u1 = mv[0];
        var u2 = mv[1];

        return OmMap(u1, u2);
    }

    
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector mv)
    {
        return GetScalingFactor() * mv;
    }

    
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        throw new InvalidOperationException();
    }
        
    
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector mv)
    {
        var mv0 = mv.Scalar();
        var mv1 = mv[0];
        var mv2 = mv[1];
        var mv12 = mv[0, 1];
            
        var scalingFactor = GetScalingFactor();

        var v0 = scalingFactor * mv0;
        var v1 = Scalar0 * mv1 + Scalar12 * mv2;
        var v2 = Scalar0 * mv2 - Scalar12 * mv1;
        var v12 = scalingFactor * mv12;

        return Processor.Multivector2D(v0, v1, v2, v12);
    }


    //
    //public EuclideanScalingRotorSquared2D GpSquared()
    //{
    //    var s0 = ScalarProcessor.Subtract(
    //        ScalarProcessor.Times(Scalar0, Scalar0),
    //        ScalarProcessor.Times(Scalar12, Scalar12)
    //    );

    //    var s12 = ScalarProcessor.Times(
    //        ScalarProcessor.ScalarTwo,
    //        ScalarProcessor.Times(Scalar0, Scalar12)
    //    );

    //    return new EuclideanScalingRotorSquared2D(s0, s12);
    //}

    
    public XGaFloat64EuclideanScalingRotorSquared2D Gp(XGaFloat64EuclideanScalingRotorSquared2D rotor2)
    {
        var s0 = Scalar0 * rotor2.Scalar0 - Scalar12 * rotor2.Scalar12;
        var s12 = Scalar0 * rotor2.Scalar12 + Scalar12 * rotor2.Scalar0;

        return new XGaFloat64EuclideanScalingRotorSquared2D(Processor, s0, s12);
    }

    //
    //public VectorStorage Gp(VectorStorage vector)
    //{
    //    var u1 = ScalarProcessor.GetTermScalarByIndex(vector, 0);
    //    var u2 = ScalarProcessor.GetTermScalarByIndex(vector, 1);

    //    var v1 = ScalarProcessor.Add(
    //        ScalarProcessor.Times(Scalar0, u1),
    //        ScalarProcessor.Times(Scalar12, u2)
    //    );

    //    var v2 = ScalarProcessor.Subtract(
    //        ScalarProcessor.Times(Scalar0, u2),
    //        ScalarProcessor.Times(Scalar12, u1)
    //    );

    //    return ScalarProcessor.CreateVectorStorage(v1, v2);
    //}


    
    public override XGaFloat64Multivector GetMultivector()
    {
        return Processor
            .CreateMultivectorComposer()
            .SetScalarTerm(Scalar0)
            .SetBivectorTerm(0, 1, Scalar12)
            .GetSimpleMultivector();
    }

    
    public override XGaFloat64Multivector GetMultivectorReverse()
    {
        return Processor
            .CreateMultivectorComposer()
            .SetScalarTerm(Scalar0)
            .SetBivectorTerm(0, 1, -Scalar12)
            .GetSimpleMultivector();
    }

    
    public override XGaFloat64Multivector GetMultivectorInverse()
    {
        var scalingFactor = 1d / GetScalingFactor();

        return Processor
            .CreateMultivectorComposer()
            .SetScalarTerm(Scalar0 * scalingFactor)
            .SetBivectorTerm(0, 1, -Scalar12 * scalingFactor)
            .GetSimpleMultivector();
    }
}