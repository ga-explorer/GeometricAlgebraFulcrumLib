﻿using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.CurveBasis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.PhCurves;

/// <summary>
/// This class represents a quintic Pythagorean hodograph curve in 3D Euclidean space
/// </summary>
public sealed class PhCurve3DDegree5Canonical
{
    public static PhCurve3DDegree5Canonical Create(ILinFloat64Vector3D p, ILinFloat64Vector3D d, LinFloat64Angle theta1, LinFloat64Angle theta2)
    {
        return new PhCurve3DDegree5Canonical(p, d, theta1, theta2);
    }

    public static PhCurve3DDegree5Canonical Create(double p1, double p2, double p3, double d1, double d2, double d3)
    {
        return new PhCurve3DDegree5Canonical(
            LinFloat64Vector3D.Create(p1, p2, p3),
            LinFloat64Vector3D.Create(d1, d2, d3),
            LinFloat64PolarAngle.Angle0,
            LinFloat64PolarAngle.Angle0
        );
    }


    private readonly BernsteinBasisPairProductSet _basisPairProductSet;
    private readonly BernsteinBasisPairProductIntegralSet _basisPairProductIntegralSet;


    public XGaFloat64Processor BasisBladeSet { get; }

    public BernsteinBasisSet BasisPolynomialSet { get; }

    public double Scalar00 { get; }

    public double Scalar01 { get; }

    public double Scalar02 { get; }

    public double Scalar11 { get; }

    public double Scalar12 { get; }

    public double Scalar22 { get; }

    public LinFloat64Vector3D Vector00 { get; }

    public LinFloat64Vector3D Vector01 { get; }

    public LinFloat64Vector3D Vector02 { get; }

    public LinFloat64Vector3D Vector11 { get; }

    public LinFloat64Vector3D Vector12 { get; }

    public LinFloat64Vector3D Vector22 { get; }

    public XGaFloat64PureScalingRotor ScalingRotor0 { get; }

    public XGaFloat64PureScalingRotor ScalingRotor1 { get; }

    public XGaFloat64PureScalingRotor ScalingRotor2 { get; }

    public LinFloat64Angle Theta1 { get; }

    public LinFloat64Angle Theta2 { get; }


    private PhCurve3DDegree5Canonical(ILinFloat64Vector3D p, ILinFloat64Vector3D d, LinFloat64Angle theta1, LinFloat64Angle theta2)
    {
        BasisBladeSet = XGaFloat64Processor.Euclidean;

        Theta1 = theta1;
        Theta2 = theta2;

        BasisPolynomialSet = BernsteinBasisSet.Create(2);
        _basisPairProductSet = BernsteinBasisPairProductSet.Create(BasisPolynomialSet);
        _basisPairProductIntegralSet = BernsteinBasisPairProductIntegralSet.Create(_basisPairProductSet);

        var e1 = LinFloat64Vector3D.E1;
        var e1Multivector = BasisBladeSet.VectorTerm(0);

        ScalingRotor0 = BasisBladeSet.IdentityScalingRotor();

        var f01 = _basisPairProductIntegralSet.GetValueAt1(0, 1);
        var f11 = _basisPairProductIntegralSet.GetValueAt1(1, 1);
        var f12 = _basisPairProductIntegralSet.GetValueAt1(1, 2);

        //var (dUnit, dNorm) = d.GetUnitVectorLengthTuple();

        ScalingRotor2 = BasisBladeSet.EuclideanParametricPureScalingRotor3D(
            e1,
            d,
            theta2
        );

        Vector00 = e1;
        Vector22 = d.ToLinVector3D();
        Vector02 = (e1Multivector.Gp(ScalingRotor2.MultivectorReverse) + ScalingRotor2.Multivector.Gp(e1Multivector)).VectorPartToVector3D();

        var u = p - (e1 + d) / 8 + Vector02 / 24;
        //var (uUnit, uNorm) = u.GetUnitVectorLengthTuple();

        var v1 = f11.Sqrt();
        var v0 = f01 / v1;
        var v2 = f12 / v1;

        var v = BasisBladeSet.EuclideanParametricPureScalingRotor3D(
            e1,
            u,
            theta1
        ).Multivector;

        var a1 = (v - v0 - v2 * ScalingRotor2.Multivector) / v1;

        ScalingRotor1 = BasisBladeSet.PureScalingRotor3D(
            a1.Scalar(),
            a1[0, 1],
            a1[0, 2],
            a1[1, 2]
        );

        Vector01 = (e1Multivector.Gp(ScalingRotor1.MultivectorReverse) + ScalingRotor1.Multivector.Gp(e1Multivector)).VectorPartToVector3D();
        Vector12 = (ScalingRotor1.Multivector.Gp(e1Multivector).Gp(ScalingRotor2.MultivectorReverse) + ScalingRotor2.Multivector.Gp(e1Multivector).Gp(ScalingRotor1.MultivectorReverse)).VectorPartToVector3D();
        Vector11 = ScalingRotor1.Multivector.Gp(e1Multivector).Gp(ScalingRotor1.MultivectorReverse).VectorPartToVector3D();

        Scalar00 = ScalingRotor0.Multivector.ESp(ScalingRotor0.MultivectorReverse).ScalarValue;
        Scalar11 = ScalingRotor1.Multivector.ESp(ScalingRotor1.MultivectorReverse).ScalarValue;
        Scalar22 = ScalingRotor2.Multivector.ESp(ScalingRotor2.MultivectorReverse).ScalarValue;

        Scalar01 = ScalingRotor0.Multivector.ESp(ScalingRotor1.MultivectorReverse).ScalarValue +
                   ScalingRotor1.Multivector.ESp(ScalingRotor0.MultivectorReverse).ScalarValue;

        Scalar02 = ScalingRotor0.Multivector.ESp(ScalingRotor2.MultivectorReverse).ScalarValue +
                   ScalingRotor2.Multivector.ESp(ScalingRotor0.MultivectorReverse).ScalarValue;

        Scalar12 = ScalingRotor1.Multivector.ESp(ScalingRotor2.MultivectorReverse).ScalarValue +
                   ScalingRotor2.Multivector.ESp(ScalingRotor1.MultivectorReverse).ScalarValue;
    }


    public LinFloat64Vector3D GetHodographPoint(double parameterValue)
    {
        var f00 = _basisPairProductSet.GetValue(0, 0, parameterValue);
        var f01 = _basisPairProductSet.GetValue(0, 1, parameterValue);
        var f02 = _basisPairProductSet.GetValue(0, 2, parameterValue);
        var f11 = _basisPairProductSet.GetValue(1, 1, parameterValue);
        var f12 = _basisPairProductSet.GetValue(1, 2, parameterValue);
        var f22 = _basisPairProductSet.GetValue(2, 2, parameterValue);

        return f00 * Vector00 + f01 * Vector01 + f02 * Vector02 +
               f11 * Vector11 + f12 * Vector12 + f22 * Vector22;
    }

    public LinFloat64Vector3D GetCurvePoint(double parameterValue)
    {
        var f00 = _basisPairProductIntegralSet.GetValue(0, 0, parameterValue);
        var f01 = _basisPairProductIntegralSet.GetValue(0, 1, parameterValue);
        var f02 = _basisPairProductIntegralSet.GetValue(0, 2, parameterValue);
        var f11 = _basisPairProductIntegralSet.GetValue(1, 1, parameterValue);
        var f12 = _basisPairProductIntegralSet.GetValue(1, 2, parameterValue);
        var f22 = _basisPairProductIntegralSet.GetValue(2, 2, parameterValue);

        return f00 * Vector00 + f01 * Vector01 + f02 * Vector02 +
               f11 * Vector11 + f12 * Vector12 + f22 * Vector22;
    }

    public double GetSigmaValue(double parameterValue)
    {
        var f00 = _basisPairProductSet.GetValue(0, 0, parameterValue);
        var f01 = _basisPairProductSet.GetValue(0, 1, parameterValue);
        var f02 = _basisPairProductSet.GetValue(0, 2, parameterValue);
        var f11 = _basisPairProductSet.GetValue(1, 1, parameterValue);
        var f12 = _basisPairProductSet.GetValue(1, 2, parameterValue);
        var f22 = _basisPairProductSet.GetValue(2, 2, parameterValue);

        return f00 * Scalar00 + f01 * Scalar01 + f02 * Scalar02 +
               f11 * Scalar11 + f12 * Scalar12 + f22 * Scalar22;
    }

    public double GetLength(double parameterValue)
    {
        var f00 = _basisPairProductIntegralSet.GetValue(0, 0, parameterValue);
        var f01 = _basisPairProductIntegralSet.GetValue(0, 1, parameterValue);
        var f02 = _basisPairProductIntegralSet.GetValue(0, 2, parameterValue);
        var f11 = _basisPairProductIntegralSet.GetValue(1, 1, parameterValue);
        var f12 = _basisPairProductIntegralSet.GetValue(1, 2, parameterValue);
        var f22 = _basisPairProductIntegralSet.GetValue(2, 2, parameterValue);

        return f00 * Scalar00 + f01 * Scalar01 + f02 * Scalar02 +
               f11 * Scalar11 + f12 * Scalar12 + f22 * Scalar22;
    }

    public double GetLength(double parameterValue1, double parameterValue2)
    {
        return GetLength(parameterValue2) - GetLength(parameterValue1);
    }

    public double GetLength()
    {
        var f00 = _basisPairProductIntegralSet.GetValueAt1(0, 0);
        var f01 = _basisPairProductIntegralSet.GetValueAt1(0, 1);
        var f02 = _basisPairProductIntegralSet.GetValueAt1(0, 2);
        var f11 = _basisPairProductIntegralSet.GetValueAt1(1, 1);
        var f12 = _basisPairProductIntegralSet.GetValueAt1(1, 2);
        var f22 = _basisPairProductIntegralSet.GetValueAt1(2, 2);

        return f00 * Scalar00 + f01 * Scalar01 + f02 * Scalar02 +
               f11 * Scalar11 + f12 * Scalar12 + f22 * Scalar22;
    }
}