using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Generic.PhCurves;

public sealed class PhCurve3DDegree5<T>
{
    public static PhCurve3DDegree5<T> Create(RGaProcessor<T> processor, RGaVector<T> point0, RGaVector<T> tangent0, RGaVector<T> point1, RGaVector<T> tangent1)
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

    public static PhCurve3DDegree5<T> Create(RGaProcessor<T> processor, RGaVector<T> point0, RGaVector<T> tangent0, RGaVector<T> point1, RGaVector<T> tangent1, LinAngle<T> theta1, LinAngle<T> theta2)
    {
        return new PhCurve3DDegree5<T>(
            processor,
            point0, tangent0, point1, tangent1, theta1, theta2
        );
    }


    public RGaVector<T> Point0 { get; }

    public RGaVector<T> Point1 { get; }

    public RGaVector<T> Tangent0 { get; }

    public RGaVector<T> Tangent1 { get; }

    public IScalar<T> TangentLength0 { get; }
        
    public IScalar<T> Theta1 
        => CanonicalCurve.Theta1;

    public IScalar<T> Theta2 
        => CanonicalCurve.Theta2;

    public RGaScaledPureRotor<T> ScaledRotor { get; }

    public PhCurve3DDegree5Canonical<T> CanonicalCurve { get; }


    private PhCurve3DDegree5(RGaProcessor<T> processor, RGaVector<T> point0, RGaVector<T> tangent0, RGaVector<T> point1, RGaVector<T> tangent1, LinAngle<T> theta1, LinAngle<T> theta2)
    {
        Point0 = point0;
        Point1 = point1;
        Tangent0 = tangent0;
        Tangent1 = tangent1;
        TangentLength0 = Tangent0.ENorm();

        ScaledRotor = processor.VectorTerm(0).CreateScaledPureRotor(tangent0);

        var scaledRotorInv = ScaledRotor.GetPureScaledRotorInverse();

        CanonicalCurve = PhCurve3DDegree5Canonical<T>.Create(
            processor,
            scaledRotorInv.OmMap(point1 - point0), 
            scaledRotorInv.OmMap(tangent1),
            theta1,
            theta2
        );
    }

        
    public RGaVector<T> GetHodographPoint(T parameterValue)
    {
        return ScaledRotor.OmMap(
            CanonicalCurve.GetHodographPoint(parameterValue)
        );
    }

    public RGaVector<T> GetCurvePoint(T parameterValue)
    {
        return Point0 + ScaledRotor.OmMap(
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