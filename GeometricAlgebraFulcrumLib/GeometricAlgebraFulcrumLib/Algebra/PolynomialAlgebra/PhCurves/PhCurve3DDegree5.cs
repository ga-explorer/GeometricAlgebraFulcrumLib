using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.PhCurves
{
    public sealed class PhCurve3DDegree5<T>
    {
        public static PhCurve3DDegree5<T> Create(IGeometricAlgebraEuclideanProcessor<T> processor, GaVector<T> point0, GaVector<T> tangent0, GaVector<T> point1, GaVector<T> tangent1)
        {
            return new PhCurve3DDegree5<T>(
                processor,
                point0, tangent0, point1, tangent1, processor.ScalarZero, processor.ScalarZero
            );
        }

        public static PhCurve3DDegree5<T> Create(IGeometricAlgebraEuclideanProcessor<T> processor, GaVector<T> point0, GaVector<T> tangent0, GaVector<T> point1, GaVector<T> tangent1, T theta1, T theta2)
        {
            return new PhCurve3DDegree5<T>(
                processor,
                point0, tangent0, point1, tangent1, theta1, theta2
            );
        }


        public GaVector<T> Point0 { get; }

        public GaVector<T> Point1 { get; }

        public GaVector<T> Tangent0 { get; }

        public GaVector<T> Tangent1 { get; }

        public Scalar<T> TangentLength0 { get; }
        
        public Scalar<T> Theta1 
            => CanonicalCurve.Theta1;

        public Scalar<T> Theta2 
            => CanonicalCurve.Theta2;

        public ScaledPureRotor<T> ScaledRotor { get; }

        public PhCurve3DDegree5Canonical<T> CanonicalCurve { get; }


        private PhCurve3DDegree5([NotNull] IGeometricAlgebraEuclideanProcessor<T> processor, [NotNull] GaVector<T> point0, [NotNull] GaVector<T> tangent0, [NotNull] GaVector<T> point1, [NotNull] GaVector<T> tangent1, [NotNull] T theta1, [NotNull] T theta2)
        {
            Point0 = point0;
            Point1 = point1;
            Tangent0 = tangent0;
            Tangent1 = tangent1;
            TangentLength0 = Tangent0.ENorm();

            ScaledRotor = processor.CreateScaledPureRotor(
                processor.CreateVectorBasis(0),
                tangent0
            );

            var scaledRotorInv = ScaledRotor.GetPureScaledRotorInverse();

            CanonicalCurve = PhCurve3DDegree5Canonical<T>.Create(
                processor,
                scaledRotorInv.OmMap(point1 - point0), 
                scaledRotorInv.OmMap(tangent1),
                theta1,
                theta2
            );
        }

        
        public GaVector<T> GetHodographPoint(T parameterValue)
        {
            return ScaledRotor.OmMap(
                CanonicalCurve.GetHodographPoint(parameterValue)
            );
        }

        public GaVector<T> GetCurvePoint(T parameterValue)
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
}