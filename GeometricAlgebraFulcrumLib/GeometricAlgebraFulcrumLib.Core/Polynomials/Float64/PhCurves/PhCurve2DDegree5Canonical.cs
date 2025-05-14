using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Maps;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Polynomials.Float64.CurveBasis;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Polynomials.Float64.PhCurves;

/// <summary>
/// This class represents a quintic Pythagorean hodograph curve in 3D Euclidean space
/// </summary>
public sealed class PhCurve2DDegree5Canonical
{
    public static PhCurve2DDegree5Canonical Create(ILinFloat64Vector2D p, ILinFloat64Vector2D d)
    {
        return new PhCurve2DDegree5Canonical(p, d);
    }

    public static PhCurve2DDegree5Canonical Create(double p1, double p2, double d1, double d2)
    {
        return new PhCurve2DDegree5Canonical(
            LinFloat64Vector2D.Create((Float64Scalar)p1, (Float64Scalar)p2),
            LinFloat64Vector2D.Create((Float64Scalar)d1, (Float64Scalar)d2)
        );
    }


    private readonly BernsteinBasisPairProductSet _basisPairProductSet;
    private readonly BernsteinBasisPairProductIntegralSet _basisPairProductIntegralSet;

    public XGaFloat64Processor BasisBladeSet { get; }

    public BernsteinBasisSet BasisSet { get; }

    public double Scalar00 { get; }

    public double Scalar01 { get; }

    public double Scalar02 { get; }

    public double Scalar11 { get; }

    public double Scalar12 { get; }

    public double Scalar22 { get; }

    public LinFloat64Vector2D VectorU { get; }

    public LinFloat64Vector2D Vector00 { get; }

    public LinFloat64Vector2D Vector01 { get; }

    public LinFloat64Vector2D Vector02 { get; }

    public LinFloat64Vector2D Vector11 { get; }

    public LinFloat64Vector2D Vector12 { get; }

    public LinFloat64Vector2D Vector22 { get; }

    public GaScaledPureRotor ScaledRotor0 { get; }

    public GaScaledPureRotor ScaledRotor1 { get; }

    public GaScaledPureRotor ScaledRotor2 { get; }

    public GaScaledPureRotor ScaledRotorV { get; }


    private PhCurve2DDegree5Canonical(ILinFloat64Vector2D p, ILinFloat64Vector2D d)
    {
        BasisBladeSet = XGaFloat64Processor.Euclidean;

        BasisSet = BernsteinBasisSet.Create(2);
        _basisPairProductSet = BernsteinBasisPairProductSet.Create(BasisSet);
        _basisPairProductIntegralSet = BernsteinBasisPairProductIntegralSet.Create(_basisPairProductSet);

        var e1 = LinFloat64Vector2D.E1;
        var e1Multivector = BasisBladeSet.VectorTerm(0);

        ScaledRotor0 = BasisBladeSet.CreateIdentityRotor();

        var f01 = _basisPairProductIntegralSet.GetValueAt1(0, 1);
        var f11 = _basisPairProductIntegralSet.GetValueAt1(1, 1);
        var f12 = _basisPairProductIntegralSet.GetValueAt1(1, 2);

        //var dNorm = d.ENorm();
        //var dUnit = d / dNorm;

        ScaledRotor2 = e1.CreateEuclideanScaledPureRotor(d);

        Vector00 = e1;
        Vector22 = d.ToLinVector2D();
        Vector02 = (e1Multivector.Gp(ScaledRotor2.MultivectorReverse) + ScaledRotor2.Multivector.Gp(e1Multivector)).VectorPartToVector2D();

        VectorU = p - (e1 + d) / 8 + Vector02 / 24;
        //var uNorm = VectorU.ENorm();
        //var uUnit = VectorU / uNorm;

        var v1 = f11.Sqrt();
        var v0 = f01 / v1;
        var v2 = f12 / v1;

        var v = e1.CreateEuclideanScaledPureRotor(VectorU).Multivector;

        ScaledRotorV = BasisBladeSet.CreateScaledPureRotor2D(
            v.Scalar(),
            v[0, 1]
        );

        var a1 = (v - v0 - v2 * ScaledRotor2.Multivector) / v1;

        ScaledRotor1 = BasisBladeSet.CreateScaledPureRotor2D(
            a1.Scalar(),
            a1[0, 1]
        );

        Vector01 = (e1Multivector.Gp(ScaledRotor1.MultivectorReverse) + ScaledRotor1.Multivector.Gp(e1Multivector)).VectorPartToVector2D();
        Vector12 = (ScaledRotor1.Multivector.Gp(e1Multivector).Gp(ScaledRotor2.MultivectorReverse) + ScaledRotor2.Multivector.Gp(e1Multivector).Gp(ScaledRotor1.MultivectorReverse)).VectorPartToVector2D();
        Vector11 = ScaledRotor1.Multivector.Gp(e1Multivector).Gp(ScaledRotor1.MultivectorReverse).VectorPartToVector2D();

        Scalar00 = ScaledRotor0.Multivector.ESp(ScaledRotor0.MultivectorReverse).ScalarValue;
        Scalar11 = ScaledRotor1.Multivector.ESp(ScaledRotor1.MultivectorReverse).ScalarValue;
        Scalar22 = ScaledRotor2.Multivector.ESp(ScaledRotor2.MultivectorReverse).ScalarValue;

        Scalar01 = ScaledRotor0.Multivector.ESp(ScaledRotor1.MultivectorReverse).ScalarValue +
                   ScaledRotor1.Multivector.ESp(ScaledRotor0.MultivectorReverse).ScalarValue;

        Scalar02 = ScaledRotor0.Multivector.ESp(ScaledRotor2.MultivectorReverse).ScalarValue +
                   ScaledRotor2.Multivector.ESp(ScaledRotor0.MultivectorReverse).ScalarValue;

        Scalar12 = ScaledRotor1.Multivector.ESp(ScaledRotor2.MultivectorReverse).ScalarValue +
                   ScaledRotor2.Multivector.ESp(ScaledRotor1.MultivectorReverse).ScalarValue;
    }


    public ILinFloat64Vector2D GetHodographPoint(double parameterValue)
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

    public ILinFloat64Vector2D GetCurvePoint(double parameterValue)
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

    public double GetSigmaValue(double parameterValue)
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

    public double GetLength(double parameterValue)
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

        return
            f00 * Scalar00 +
            f01 * Scalar01 +
            f02 * Scalar02 +
            f11 * Scalar11 +
            f12 * Scalar12 +
            f22 * Scalar22;
    }
}