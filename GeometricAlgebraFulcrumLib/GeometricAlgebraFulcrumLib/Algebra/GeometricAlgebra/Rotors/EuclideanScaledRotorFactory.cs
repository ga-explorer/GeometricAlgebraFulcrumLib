using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
{
    public static class EuclideanScaledRotorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EuclideanScaledRotor2D<T> CreateEuclideanScaledRotor2D<T>(this IGeometricAlgebraProcessor<T> processor, Scalar<T> scalar0, Scalar<T> scalar12)
        {
            return EuclideanScaledRotor2D<T>.Create(
                processor,
                scalar0,
                scalar12
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EuclideanScaledRotor2D<T> CreateEuclideanScaledRotor2D<T>(this IGeometricAlgebraProcessor<T> processor, T scalar0, T scalar12)
        {
            return EuclideanScaledRotor2D<T>.Create(
                processor,
                processor.CreateScalar(scalar0),
                processor.CreateScalar(scalar12)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EuclideanScaledRotor2D<T> CreateEuclideanScaledRotor2D<T>(this IGeometricAlgebraProcessor<T> processor, GaVector<T> sourceVector, GaVector<T> targetVector)
        {
            Debug.Assert(
                ReferenceEquals(
                    processor,
                    targetVector.GeometricProcessor
                )
            );

            return sourceVector.CreateEuclideanScaledRotor2D(targetVector);
        }

        public static EuclideanScaledRotor2D<T> CreateEuclideanScaledRotor2D<T>(this GaVector<T> sourceVector, GaVector<T> targetVector)
        {
            Debug.Assert(
                ReferenceEquals(
                    sourceVector.GeometricProcessor,
                    targetVector.GeometricProcessor
                )
            );

            var processor = sourceVector.GeometricProcessor;

            var u1 = sourceVector[0];
            var u2 = sourceVector[1];

            var v1 = targetVector[0];
            var v2 = targetVector[1];

            var vuDot = v1 * u1 + v2 * u2;
            var uNormSquared = u1 * u1 + u2 * u2;
            var vNormSquared = v1 * v1 + v2 * v2;

            var t1 = (vNormSquared / uNormSquared).Sqrt();
            var t2 = vuDot / uNormSquared;

            var vuWedgeScalar = (v1 * u2 - v2 * u1).Sign();

            var a0 = ((t1 + t2) / 2).Sqrt();
            var a12 = ((t1 - t2) / 2).Sqrt() * vuWedgeScalar;

            return EuclideanScaledRotor2D<T>.Create(processor, a0, a12);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EuclideanScaledRotorSquared2D<T> CreateEuclideanScaledRotorSquared2D<T>(this IGeometricAlgebraProcessor<T> processor, T scalar0, T scalar12)
        {
            return EuclideanScaledRotorSquared2D<T>.Create(
                processor,
                scalar0.CreateScalar(processor),
                scalar12.CreateScalar(processor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EuclideanScaledRotorSquared2D<T> CreateEuclideanScaledRotorSquared2D<T>(this IGeometricAlgebraProcessor<T> processor, Scalar<T> scalar0, Scalar<T> scalar12)
        {
            return EuclideanScaledRotorSquared2D<T>.Create(
                processor,
                scalar0,
                scalar12
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EuclideanScaledRotorSquared2D<T> CreateEuclideanScaledRotorSquared2D<T>(this IGeometricAlgebraProcessor<T> processor, GaVector<T> sourceVector, GaVector<T> targetVector)
        {
            Debug.Assert(
                ReferenceEquals(
                    processor,
                    targetVector.GeometricProcessor
                )
            );

            return sourceVector.CreateEuclideanScaledRotorSquared2D(targetVector);
        }

        public static EuclideanScaledRotorSquared2D<T> CreateEuclideanScaledRotorSquared2D<T>(this GaVector<T> sourceVector, GaVector<T> targetVector)
        {
            var processor = sourceVector.GeometricProcessor;

            var u1 = sourceVector[0];
            var u2 = sourceVector[1];

            var v1 = targetVector[0];
            var v2 = targetVector[1];

            var uNormSquared = u1 * u1 + u2 * u2;

            var a0 = (v1 * u1 + v2 * u2) / uNormSquared;
            var a12 = (v1 * u2 - v2 * u1) / uNormSquared;

            return EuclideanScaledRotorSquared2D<T>.Create(processor, a0, a12);
        }
    }
}