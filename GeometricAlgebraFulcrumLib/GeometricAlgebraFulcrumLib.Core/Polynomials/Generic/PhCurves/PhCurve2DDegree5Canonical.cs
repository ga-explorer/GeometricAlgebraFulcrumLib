﻿using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Polynomials.Generic.Basis;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.Polynomials.Generic.PhCurves;

/// <summary>
/// This class represents a quintic Pythagorean hodograph curve in 3D Euclidean space
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class PhCurve2DDegree5Canonical<T>
{
    public static PhCurve2DDegree5Canonical<T> Create(XGaProcessor<T> processor, XGaVector<T> p, XGaVector<T> d)
    {
        return new PhCurve2DDegree5Canonical<T>(processor, p, d);
    }

    public static PhCurve2DDegree5Canonical<T> Create(XGaProcessor<T> processor, T p1, T p2, T d1, T d2)
    {
        return new PhCurve2DDegree5Canonical<T>(
            processor,
            processor.Vector(p1, p2),
            processor.Vector(d1, d2)
        );
    }


    public BernsteinBasisSet<T> BasisSet { get; }

    private readonly BernsteinBasisPairProductSet<T> _basisPairProductSet;
    private readonly BernsteinBasisPairProductIntegralSet<T> _basisPairProductIntegralSet;

    public IScalar<T> Scalar00 { get; }

    public IScalar<T> Scalar01 { get; }

    public IScalar<T> Scalar02 { get; }

    public IScalar<T> Scalar11 { get; }

    public IScalar<T> Scalar12 { get; }

    public IScalar<T> Scalar22 { get; }

    public XGaVector<T> VectorU { get; }

    public XGaVector<T> Vector00 { get; }

    public XGaVector<T> Vector01 { get; }

    public XGaVector<T> Vector02 { get; }

    public XGaVector<T> Vector11 { get; }

    public XGaVector<T> Vector12 { get; }

    public XGaVector<T> Vector22 { get; }

    public XGaScaledPureRotor<T> ScaledRotor0 { get; }

    public XGaScaledPureRotor<T> ScaledRotor1 { get; }

    public XGaScaledPureRotor<T> ScaledRotor2 { get; }

    public XGaScaledPureRotor<T> ScaledRotorV { get; }

    public XGaProcessor<T> GeometricProcessor { get; }


    private PhCurve2DDegree5Canonical(XGaProcessor<T> processor, XGaVector<T> p, XGaVector<T> d)
    {
        GeometricProcessor = processor;

        BasisSet = BernsteinBasisSet<T>.Create(processor.ScalarProcessor, 2);
        _basisPairProductSet = BernsteinBasisPairProductSet<T>.Create(BasisSet);
        _basisPairProductIntegralSet = BernsteinBasisPairProductIntegralSet<T>.Create(_basisPairProductSet);

        var e1 = processor.VectorTerm(0);

        ScaledRotor0 = processor.CreateScaledIdentityRotor();

        var f01 = _basisPairProductIntegralSet.GetValueAt1(0, 1);
        var f11 = _basisPairProductIntegralSet.GetValueAt1(1, 1);
        var f12 = _basisPairProductIntegralSet.GetValueAt1(1, 2);

        var dNorm = d.ENorm();
        var dUnit = d / dNorm;
            
        ScaledRotor2 = e1.CreateScaledPureRotor(d);

        Vector00 = e1;
        Vector22 = d;
        Vector02 = (e1.Gp(ScaledRotor2.MultivectorReverse) + ScaledRotor2.Multivector.Gp(e1)).GetVectorPart();

        VectorU = p - (e1 + d) / 8 + Vector02 / 24;
        var uNorm = VectorU.ENorm();
        var uUnit = VectorU / uNorm;

        var v1 = f11.Sqrt();
        var v0 = f01 / v1;
        var v2 = f12 / v1;
            
        var v = e1.CreateScaledPureRotor(VectorU).Multivector;

        ScaledRotorV = processor.CreateScaledPureRotor2D(
            v.Scalar().ScalarValue, 
            v[0, 1].ScalarValue
        );

        var a1 = (v - v0 - v2 * ScaledRotor2.Multivector) / v1;

        ScaledRotor1 = processor.CreateScaledPureRotor2D(
            a1.Scalar().ScalarValue, 
            a1[0, 1].ScalarValue
        );

        Vector01 = (e1.Gp(ScaledRotor1.MultivectorReverse) + ScaledRotor1.Multivector.Gp(e1)).GetVectorPart();
        Vector12 = (ScaledRotor1.Multivector.Gp(e1).Gp(ScaledRotor2.MultivectorReverse) + ScaledRotor2.Multivector.Gp(e1).Gp(ScaledRotor1.MultivectorReverse)).GetVectorPart();
        Vector11 = ScaledRotor1.Multivector.Gp(e1).Gp(ScaledRotor1.MultivectorReverse).GetVectorPart();

        Scalar00 = ScaledRotor0.Multivector.ESp(ScaledRotor0.MultivectorReverse);
        Scalar11 = ScaledRotor1.Multivector.ESp(ScaledRotor1.MultivectorReverse);
        Scalar22 = ScaledRotor2.Multivector.ESp(ScaledRotor2.MultivectorReverse);

        Scalar01 = ScaledRotor0.Multivector.ESp(ScaledRotor1.MultivectorReverse) +
                   ScaledRotor1.Multivector.ESp(ScaledRotor0.MultivectorReverse);

        Scalar02 = ScaledRotor0.Multivector.ESp(ScaledRotor2.MultivectorReverse) +
                   ScaledRotor2.Multivector.ESp(ScaledRotor0.MultivectorReverse);

        Scalar12 = ScaledRotor1.Multivector.ESp(ScaledRotor2.MultivectorReverse) +
                   ScaledRotor2.Multivector.ESp(ScaledRotor1.MultivectorReverse);
    }


    public XGaVector<T> GetHodographPoint(T parameterValue)
    {
        var f00 = _basisPairProductSet.GetValue(0, 0, parameterValue);
        var f01 = _basisPairProductSet.GetValue(0, 1, parameterValue);
        var f02 = _basisPairProductSet.GetValue(0, 2, parameterValue);
        var f11 = _basisPairProductSet.GetValue(1, 1, parameterValue);
        var f12 = _basisPairProductSet.GetValue(1, 2, parameterValue);
        var f22 = _basisPairProductSet.GetValue(2, 2, parameterValue);

        return 
            f00 * Vector00 +
            f01 * Vector01 +
            f02 * Vector02 +
            f11 * Vector11 +
            f12 * Vector12 +
            f22 * Vector22;
    }

    public XGaVector<T> GetCurvePoint(T parameterValue)
    {
        var f00 = _basisPairProductIntegralSet.GetValue(0, 0, parameterValue);
        var f01 = _basisPairProductIntegralSet.GetValue(0, 1, parameterValue);
        var f02 = _basisPairProductIntegralSet.GetValue(0, 2, parameterValue);
        var f11 = _basisPairProductIntegralSet.GetValue(1, 1, parameterValue);
        var f12 = _basisPairProductIntegralSet.GetValue(1, 2, parameterValue);
        var f22 = _basisPairProductIntegralSet.GetValue(2, 2, parameterValue);

        return 
            f00 * Vector00 +
            f01 * Vector01 +
            f02 * Vector02 +
            f11 * Vector11 +
            f12 * Vector12 +
            f22 * Vector22;
    }

    public Scalar<T> GetSigmaValue(T parameterValue)
    {
        var f00 = _basisPairProductSet.GetValue(0, 0, parameterValue);
        var f01 = _basisPairProductSet.GetValue(0, 1, parameterValue);
        var f02 = _basisPairProductSet.GetValue(0, 2, parameterValue);
        var f11 = _basisPairProductSet.GetValue(1, 1, parameterValue);
        var f12 = _basisPairProductSet.GetValue(1, 2, parameterValue);
        var f22 = _basisPairProductSet.GetValue(2, 2, parameterValue);

        return 
            f00 * Scalar00 +
            f01 * Scalar01 +
            f02 * Scalar02 +
            f11 * Scalar11 +
            f12 * Scalar12 +
            f22 * Scalar22;
    }

    public Scalar<T> GetLength(T parameterValue)
    {
        var f00 = _basisPairProductIntegralSet.GetValue(0, 0, parameterValue);
        var f01 = _basisPairProductIntegralSet.GetValue(0, 1, parameterValue);
        var f02 = _basisPairProductIntegralSet.GetValue(0, 2, parameterValue);
        var f11 = _basisPairProductIntegralSet.GetValue(1, 1, parameterValue);
        var f12 = _basisPairProductIntegralSet.GetValue(1, 2, parameterValue);
        var f22 = _basisPairProductIntegralSet.GetValue(2, 2, parameterValue);

        return 
            f00 * Scalar00 +
            f01 * Scalar01 +
            f02 * Scalar02 +
            f11 * Scalar11 +
            f12 * Scalar12 +
            f22 * Scalar22;
    }

    public Scalar<T> GetLength(T parameterValue1, T parameterValue2)
    {
        return GetLength(parameterValue2) - GetLength(parameterValue1);
    }

    public Scalar<T> GetLength()
    {
        var f00 = _basisPairProductIntegralSet.GetValueAt1(0, 0);
        var f01 = _basisPairProductIntegralSet.GetValueAt1(0, 1);
        var f02 = _basisPairProductIntegralSet.GetValueAt1(0, 2);
        var f11 = _basisPairProductIntegralSet.GetValueAt1(1, 1);
        var f12 = _basisPairProductIntegralSet.GetValueAt1(1, 2);
        var f22 = _basisPairProductIntegralSet.GetValueAt1(2, 2);

        return 
            f00 * Scalar00 +
            f01 * Scalar01 +
            f02 * Scalar02 +
            f11 * Scalar11 +
            f12 * Scalar12 +
            f22 * Scalar22;
    }
}