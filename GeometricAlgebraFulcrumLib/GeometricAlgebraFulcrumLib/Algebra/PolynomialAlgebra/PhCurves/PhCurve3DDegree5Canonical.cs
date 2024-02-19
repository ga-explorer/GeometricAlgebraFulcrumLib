using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.PhCurves;

/// <summary>
/// This class represents a quintic Pythagorean hodograph curve in 3D Euclidean space
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class PhCurve3DDegree5Canonical<T>
{
    public static PhCurve3DDegree5Canonical<T> Create(RGaProcessor<T> processor, RGaVector<T> p, RGaVector<T> d)
    {
        return new PhCurve3DDegree5Canonical<T>(
            processor, 
            p,
            d,
            processor.CreateZeroScalar(),
            processor.CreateZeroScalar()
        );
    }
        
    public static PhCurve3DDegree5Canonical<T> Create(RGaProcessor<T> processor, RGaVector<T> p, RGaVector<T> d, Scalar<T> theta1, Scalar<T> theta2)
    {
        return new PhCurve3DDegree5Canonical<T>(
            processor, 
            p,
            d,
            theta1,
            theta2
        );
    }
        
    public static PhCurve3DDegree5Canonical<T> Create(RGaProcessor<T> processor, RGaVector<T> p, RGaVector<T> d, T theta1, T theta2)
    {
        return new PhCurve3DDegree5Canonical<T>(
            processor, 
            p,
            d,
            theta1.CreateScalar(processor.ScalarProcessor),
            theta2.CreateScalar(processor.ScalarProcessor)
        );
    }

    public static PhCurve3DDegree5Canonical<T> Create(RGaProcessor<T> processor, T p1, T p2, T p3, T d1, T d2, T d3)
    {
        return new PhCurve3DDegree5Canonical<T>(
            processor, 
            processor.CreateVector(p1, p2, p3),
            processor.CreateVector(d1, d2, d3),
            processor.CreateZeroScalar(),
            processor.CreateZeroScalar()
        );
    }


    public BernsteinBasisSet<T> BasisSet { get; }

    private readonly BernsteinBasisPairProductSet<T> _basisPairProductSet;
    private readonly BernsteinBasisPairProductIntegralSet<T> _basisPairProductIntegralSet;

    public Scalar<T> Scalar00 { get; }
        
    public Scalar<T> Scalar01 { get; }
        
    public Scalar<T> Scalar02 { get; }
        
    public Scalar<T> Scalar11 { get; }
        
    public Scalar<T> Scalar12 { get; }
        
    public Scalar<T> Scalar22 { get; }

    public RGaVector<T> Vector00 { get; }
        
    public RGaVector<T> Vector01 { get; }
        
    public RGaVector<T> Vector02 { get; }
        
    public RGaVector<T> Vector11 { get; }
        
    public RGaVector<T> Vector12 { get; }
        
    public RGaVector<T> Vector22 { get; }

    public RGaVector<T> VectorU { get; }

    public RGaScaledPureRotor<T> ScaledRotor0 { get; }

    public RGaScaledPureRotor<T> ScaledRotor1 { get; }
        
    public RGaScaledPureRotor<T> ScaledRotor2 { get; }
        
    public RGaScaledPureRotor<T> ScaledRotorV { get; }

    public Scalar<T> Theta1 { get; }

    public Scalar<T> Theta2 { get; }

    public RGaProcessor<T> GeometricProcessor { get; }


    private PhCurve3DDegree5Canonical(RGaProcessor<T> processor, RGaVector<T> p, RGaVector<T> d, Scalar<T> theta1, Scalar<T> theta2)
    {
        GeometricProcessor = processor;
        Theta1 = theta1;
        Theta2 = theta2;

        BasisSet = BernsteinBasisSet<T>.Create(processor.ScalarProcessor, 2);
        _basisPairProductSet = BernsteinBasisPairProductSet<T>.Create(BasisSet);
        _basisPairProductIntegralSet = BernsteinBasisPairProductIntegralSet<T>.Create(_basisPairProductSet);

        var e1 = processor.CreateTermVector(0);

        ScaledRotor0 = processor.CreateScaledIdentityRotor();

        var f01 = _basisPairProductIntegralSet.GetValueAt1(0, 1);
        var f11 = _basisPairProductIntegralSet.GetValueAt1(1, 1);
        var f12 = _basisPairProductIntegralSet.GetValueAt1(1, 2);

        var dNorm = d.ENorm();
        var dUnit = d / dNorm;
            
        ScaledRotor2 = e1.CreateScaledParametricPureRotor3D(
            dUnit,
            theta2,
            dNorm
        );

        Vector00 = e1;
        Vector22 = d;
        Vector02 = (e1.Gp(ScaledRotor2.MultivectorReverse) + ScaledRotor2.Multivector.Gp(e1)).GetVectorPart();

        VectorU = p - (e1 + d) / 8 + Vector02 / 24;
        var uNorm = VectorU.ENorm();
        var uUnit = VectorU / uNorm;

        var v1 = f11.Sqrt();
        var v0 = f01 / v1;
        var v2 = f12 / v1;
            
        var v = e1.CreateScaledParametricPureRotor3D(
            uUnit,
            theta1,
            uNorm
        ).Multivector;

        ScaledRotorV = 
            processor.CreateScaledPureRotor3D(
                v.Scalar(), 
                v[0, 1], 
                v[0, 2], 
                v[1, 2]
            );

        var a1 = (v - v0 - v2 * ScaledRotor2.Multivector) / v1;

        ScaledRotor1 = 
            processor.CreateScaledPureRotor3D(
                a1.Scalar(), 
                a1[0, 1], 
                a1[0, 2], 
                a1[1, 2]
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


    public RGaVector<T> GetHodographPoint(T parameterValue)
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

    public RGaVector<T> GetCurvePoint(T parameterValue)
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