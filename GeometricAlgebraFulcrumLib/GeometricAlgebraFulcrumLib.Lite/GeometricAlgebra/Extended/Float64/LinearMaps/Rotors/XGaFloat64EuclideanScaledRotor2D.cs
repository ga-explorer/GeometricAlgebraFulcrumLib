﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors;

/// <summary>
/// This class 
/// </summary>
public sealed class XGaFloat64EuclideanScaledRotor2D
    : XGaFloat64ScaledRotorBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64EuclideanScaledRotor2D Create(XGaFloat64Processor metric, double scalar0)
    {
        return new XGaFloat64EuclideanScaledRotor2D(metric, scalar0, 0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64EuclideanScaledRotor2D Create(XGaFloat64Processor metric, double scalar0, double scalar12)
    {
        return new XGaFloat64EuclideanScaledRotor2D(metric, scalar0, scalar12);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator XGaFloat64Multivector(XGaFloat64EuclideanScaledRotor2D rotor)
    {
        return rotor.GetMultivector();
    }


    public double Scalar0 { get; }

    public double Scalar12 { get; }


    private XGaFloat64EuclideanScaledRotor2D(XGaFloat64Processor metric, double scalar0, double scalar12)
        : base(metric)
    {
        Scalar0 = scalar0;
        Scalar12 = scalar12;
    }


    public override bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64EuclideanScaledRotor2D GetPureScaledRotor2DInverse()
    {
        var scalingFactor = GetScalingFactor();

        return new XGaFloat64EuclideanScaledRotor2D(
            Processor,
            Scalar0 / scalingFactor,
            -Scalar12 / scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetScalingFactor()
    {
        return Scalar0 * Scalar0 + Scalar12 * Scalar12;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64EuclideanScaledRotor2D GetPureRotor()
    {
        var scalingFactor = 
            GetScalingFactor().Sqrt();

        return Create(
            Processor,
            Scalar0 / scalingFactor,
            Scalar12 / scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaFloat64ScaledRotor GetScaledRotorInverse()
    {
        return GetPureScaledRotor2DInverse();
    }

    public XGaFloat64Vector OmMap(double u1, double u2)
    {
        var s0 = Scalar0 * Scalar0 - Scalar12 * Scalar12;
        var s12 = 2 * Scalar0 * Scalar12;
        var v1 = s0 * u1 + s12 * u2;
        var v2 = s0 * u2 - s12 * u1;

        return Processor.CreateVector(v1, v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMap(XGaFloat64Vector mv)
    {
        return OmMap(mv[0], mv[1]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector mv)
    {
        return GetScalingFactor() * mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector mv)
    {
        throw new InvalidOperationException();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector mv)
    {
        var mv0 = mv.Scalar();
        var mv1 = mv[0];
        var mv2 = mv[1];
        var mv12 = mv[0, 1];

        var s0 = Scalar0 * Scalar0 - Scalar12 * Scalar12;
        var s12 = 2 * Scalar0 * Scalar12;

        var scalingFactor = GetScalingFactor();

        var v0 = scalingFactor * mv0;
        var v1 = s0 * mv1 + s12 * mv2;
        var v2 = s0 * mv2 - s12 * mv1;
        var v12 = scalingFactor * mv12;

        return Processor
            .CreateComposer()
            .SetTerm(0, v0)
            .SetTerm(1, v1)
            .SetTerm(2, v2)
            .SetTerm(3, v12)
            .GetSimpleMultivector();
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public EuclideanScaledRotor2D GpSquared()
    //{
    //    var s0 = ScalarProcessor.Subtract(
    //        ScalarProcessor.Times(Scalar0, Scalar0),
    //        ScalarProcessor.Times(Scalar12, Scalar12)
    //    );

    //    var s12 = ScalarProcessor.Times(
    //        ScalarProcessor.ScalarTwo,
    //        ScalarProcessor.Times(Scalar0, Scalar12)
    //    );

    //    return new EuclideanScaledRotor2D(GeometricProcessor, s0, s12);
    //}
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64EuclideanScaledRotor2D Gp(XGaFloat64EuclideanScaledRotor2D rotor2)
    {
        var s0 = Scalar0 * rotor2.Scalar0 - Scalar12 * rotor2.Scalar12;
        var s12 = Scalar0 * rotor2.Scalar12 + Scalar12 * rotor2.Scalar0;

        return new XGaFloat64EuclideanScaledRotor2D(Processor, s0, s12);
    }
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    //    return GeometricProcessor.CreateVectorStorage(v1, v2);
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivector()
    {
        return Processor
            .CreateComposer()
            .SetTerm(0, Scalar0)
            .SetTerm(3, Scalar12)
            .GetSimpleMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivectorReverse()
    {
        return Processor
            .CreateComposer()
            .SetTerm(0, Scalar0)
            .SetTerm(3, -(Scalar12))
            .GetSimpleMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivectorInverse()
    {
        var scalingFactor = GetScalingFactor();

        return Processor
            .CreateComposer()
            .SetTerm(0, Scalar0 / scalingFactor)
            .SetTerm(3, -Scalar12 / scalingFactor)
            .GetSimpleMultivector();
    }
}