using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Generic.PhCurves;

public sealed class PhCurve3DDegree5<T>
{
    public static PhCurve3DDegree5<T> Create(XGaProcessor<T> processor, XGaVector<T> point0, XGaVector<T> tangent0, XGaVector<T> point1, XGaVector<T> tangent1)
    {
        var angle0 = processor.ScalarProcessor.Zero.DegreesToPolarAngle();

        return new PhCurve3DDegree5<T>(
            processor,
            point0, 
            tangent0, 
            point1, 
            tangent1, 
            angle0, 
            angle0
        );
    }

    public static PhCurve3DDegree5<T> Create(XGaProcessor<T> processor, XGaVector<T> point0, XGaVector<T> tangent0, XGaVector<T> point1, XGaVector<T> tangent1, LinAngle<T> theta1, LinAngle<T> theta2)
    {
        return new PhCurve3DDegree5<T>(
            processor,
            point0, tangent0, point1, tangent1, theta1, theta2
        );
    }


    public XGaVector<T> Point0 { get; }

    public XGaVector<T> Point1 { get; }

    public XGaVector<T> Tangent0 { get; }

    public XGaVector<T> Tangent1 { get; }

    public IScalar<T> TangentLength0 { get; }
        
    public IScalar<T> Theta1 
        => CanonicalCurve.Theta1;

    public IScalar<T> Theta2 
        => CanonicalCurve.Theta2;

    public XGaPureScalingRotor<T> ScalingRotor { get; }

    public PhCurve3DDegree5Canonical<T> CanonicalCurve { get; }


    private PhCurve3DDegree5(XGaProcessor<T> processor, XGaVector<T> point0, XGaVector<T> tangent0, XGaVector<T> point1, XGaVector<T> tangent1, LinAngle<T> theta1, LinAngle<T> theta2)
    {
        Point0 = point0;
        Point1 = point1;
        Tangent0 = tangent0;
        Tangent1 = tangent1;
        TangentLength0 = Tangent0.ENorm();

        ScalingRotor = processor.VectorTerm(0).GetEuclideanPureScalingRotor(tangent0);

        var scaledRotorInv = ScalingRotor.GetPureScalingRotorInverse();

        CanonicalCurve = PhCurve3DDegree5Canonical<T>.Create(
            processor,
            scaledRotorInv.OmMap(point1 - point0), 
            scaledRotorInv.OmMap(tangent1),
            theta1,
            theta2
        );
    }

        
    public XGaVector<T> GetHodographPoint(T parameterValue)
    {
        return ScalingRotor.OmMap(
            CanonicalCurve.GetHodographPoint(parameterValue)
        );
    }

    public XGaVector<T> GetCurvePoint(T parameterValue)
    {
        return Point0 + ScalingRotor.OmMap(
            CanonicalCurve.GetCurvePoint(parameterValue)
        );
    }

    public Scalar<T> GetSigmaValue(T parameterValue)
    {
        return CanonicalCurve.GetSigmaValue(parameterValue) * TangentLength0;
    }

    public Scalar<T> GetLength(T parameterValue)
    {
        return CanonicalCurve.GetLength(parameterValue) * TangentLength0;
    }

    public Scalar<T> GetLength(T parameterValue1, T parameterValue2)
    {
        return CanonicalCurve.GetLength(parameterValue1, parameterValue2) * TangentLength0;
    }

    public Scalar<T> GetLength()
    {
        return CanonicalCurve.GetLength() * TangentLength0;
    }
}