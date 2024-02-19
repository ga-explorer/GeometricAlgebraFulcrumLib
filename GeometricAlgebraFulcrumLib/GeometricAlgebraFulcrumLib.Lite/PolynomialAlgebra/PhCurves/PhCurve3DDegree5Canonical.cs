using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Maps;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.PolynomialAlgebra.CurveBasis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.PolynomialAlgebra.PhCurves;

/// <summary>
/// This class represents a quintic Pythagorean hodograph curve in 3D Euclidean space
/// </summary>
public sealed class PhCurve3DDegree5Canonical
{
    public static PhCurve3DDegree5Canonical Create(IFloat64Vector3D p, IFloat64Vector3D d, Float64PlanarAngle theta1, Float64PlanarAngle theta2)
    {
        return new PhCurve3DDegree5Canonical(p, d, theta1, theta2);
    }

    public static PhCurve3DDegree5Canonical Create(double p1, double p2, double p3, double d1, double d2, double d3)
    {
        return new PhCurve3DDegree5Canonical(
            Float64Vector3D.Create(p1, p2, p3),
            Float64Vector3D.Create(d1, d2, d3),
            0d,
            0d
        );
    }

        
    private readonly BernsteinBasisPairProductSet _basisPairProductSet;
    private readonly BernsteinBasisPairProductIntegralSet _basisPairProductIntegralSet;


    public RGaFloat64Processor BasisBladeSet { get; }

    public BernsteinBasisSet BasisPolynomialSet { get; }

    public double Scalar00 { get; }
        
    public double Scalar01 { get; }
        
    public double Scalar02 { get; }
        
    public double Scalar11 { get; }
        
    public double Scalar12 { get; }
        
    public double Scalar22 { get; }

    public Float64Vector3D Vector00 { get; }
        
    public Float64Vector3D Vector01 { get; }
        
    public Float64Vector3D Vector02 { get; }
        
    public Float64Vector3D Vector11 { get; }
        
    public Float64Vector3D Vector12 { get; }
        
    public Float64Vector3D Vector22 { get; }

    public GaScaledPureRotor ScaledRotor0 { get; }

    public GaScaledPureRotor ScaledRotor1 { get; }
        
    public GaScaledPureRotor ScaledRotor2 { get; }
        
    public Float64PlanarAngle Theta1 { get; }

    public Float64PlanarAngle Theta2 { get; }
        

    private PhCurve3DDegree5Canonical(IFloat64Vector3D p, IFloat64Vector3D d, Float64PlanarAngle theta1, Float64PlanarAngle theta2)
    {
        BasisBladeSet = RGaFloat64Processor.Euclidean;

        Theta1 = theta1;
        Theta2 = theta2;

        BasisPolynomialSet = BernsteinBasisSet.Create(2);
        _basisPairProductSet = BernsteinBasisPairProductSet.Create(BasisPolynomialSet);
        _basisPairProductIntegralSet = BernsteinBasisPairProductIntegralSet.Create(_basisPairProductSet);

        var e1 = Float64Vector3D.E1;
        var e1Multivector = BasisBladeSet.CreateTermVector(0);

        ScaledRotor0 = BasisBladeSet.CreateIdentityRotor();

        var f01 = _basisPairProductIntegralSet.GetValueAt1(0, 1);
        var f11 = _basisPairProductIntegralSet.GetValueAt1(1, 1);
        var f12 = _basisPairProductIntegralSet.GetValueAt1(1, 2);

        //var (dUnit, dNorm) = d.GetUnitVectorLengthTuple();
            
        ScaledRotor2 = BasisBladeSet.CreateEuclideanScaledParametricPureRotor3D(
            e1,
            d,
            theta2
        );

        Vector00 = e1;
        Vector22 = d.ToVector3D();
        Vector02 = (e1Multivector.Gp(ScaledRotor2.MultivectorReverse) + ScaledRotor2.Multivector.Gp(e1Multivector)).GetVectorPartAsTuple3D();

        var u = p - (e1 + d) / 8 + Vector02 / 24;
        //var (uUnit, uNorm) = u.GetUnitVectorLengthTuple();

        var v1 = f11.Sqrt();
        var v0 = f01 / v1;
        var v2 = f12 / v1;
            
        var v = BasisBladeSet.CreateEuclideanScaledParametricPureRotor3D(
            e1,
            u,
            theta1
        ).Multivector;

        var a1 = (v - v0 - v2 * ScaledRotor2.Multivector) / v1;

        ScaledRotor1 = BasisBladeSet.CreateScaledPureRotor3D(
            a1.Scalar(),
            a1[0, 1],
            a1[0, 2],
            a1[1, 2]
        );

        Vector01 = (e1Multivector.Gp(ScaledRotor1.MultivectorReverse) + ScaledRotor1.Multivector.Gp(e1Multivector)).GetVectorPartAsTuple3D();
        Vector12 = (ScaledRotor1.Multivector.Gp(e1Multivector).Gp(ScaledRotor2.MultivectorReverse) + ScaledRotor2.Multivector.Gp(e1Multivector).Gp(ScaledRotor1.MultivectorReverse)).GetVectorPartAsTuple3D();
        Vector11 = ScaledRotor1.Multivector.Gp(e1Multivector).Gp(ScaledRotor1.MultivectorReverse).GetVectorPartAsTuple3D();

        Scalar00 = ScaledRotor0.Multivector.ESp(ScaledRotor0.MultivectorReverse).ScalarValue();
        Scalar11 = ScaledRotor1.Multivector.ESp(ScaledRotor1.MultivectorReverse).ScalarValue();
        Scalar22 = ScaledRotor2.Multivector.ESp(ScaledRotor2.MultivectorReverse).ScalarValue();

        Scalar01 = ScaledRotor0.Multivector.ESp(ScaledRotor1.MultivectorReverse).ScalarValue() +
                   ScaledRotor1.Multivector.ESp(ScaledRotor0.MultivectorReverse).ScalarValue();

        Scalar02 = ScaledRotor0.Multivector.ESp(ScaledRotor2.MultivectorReverse).ScalarValue() +
                   ScaledRotor2.Multivector.ESp(ScaledRotor0.MultivectorReverse).ScalarValue();

        Scalar12 = ScaledRotor1.Multivector.ESp(ScaledRotor2.MultivectorReverse).ScalarValue() +
                   ScaledRotor2.Multivector.ESp(ScaledRotor1.MultivectorReverse).ScalarValue();
    }


    public Float64Vector3D GetHodographPoint(double parameterValue)
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

    public Float64Vector3D GetCurvePoint(double parameterValue)
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