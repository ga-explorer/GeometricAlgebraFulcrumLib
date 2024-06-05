using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;

public static class RGaEuclideanScaledRotorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaEuclideanScaledRotor2D<T> CreateEuclideanScaledRotor2D<T>(this RGaProcessor<T> processor, Scalar<T> scalar0, Scalar<T> scalar12)
    {
        return RGaEuclideanScaledRotor2D<T>.Create(
            processor,
            scalar0,
            scalar12
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaEuclideanScaledRotor2D<T> CreateEuclideanScaledRotor2D<T>(this RGaProcessor<T> processor, T scalar0, T scalar12)
    {
        return RGaEuclideanScaledRotor2D<T>.Create(
            processor,
            processor.ScalarProcessor.ScalarFromValue(scalar0),
            processor.ScalarProcessor.ScalarFromValue(scalar12)
        );
    }
        
    public static RGaEuclideanScaledRotor2D<T> CreateEuclideanScaledRotor2D<T>(this RGaVector<T> sourceVector, RGaVector<T> targetVector)
    {
        Debug.Assert(
            ReferenceEquals(
                sourceVector.ScalarProcessor,
                targetVector.ScalarProcessor
            )
        );

        Debug.Assert(
            sourceVector.Processor.HasSameSignature(targetVector.Processor)
        );

        var metric = sourceVector.Processor;

        var u1 = sourceVector.Scalar(0);
        var u2 = sourceVector.Scalar(1);

        var v1 = targetVector.Scalar(0);
        var v2 = targetVector.Scalar(1);

        var vuDot = v1 * u1 + v2 * u2;
        var uNormSquared = u1 * u1 + u2 * u2;
        var vNormSquared = v1 * v1 + v2 * v2;

        var t1 = (vNormSquared / uNormSquared).Sqrt();
        var t2 = vuDot / uNormSquared;

        var vuWedgeScalar = (v1 * u2 - v2 * u1).Sign();

        var a0 = ((t1 + t2) / 2).Sqrt();
        var a12 = ((t1 - t2) / 2).Sqrt() * vuWedgeScalar;

        return RGaEuclideanScaledRotor2D<T>.Create(metric, a0, a12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaEuclideanScaledRotorSquared2D<T> CreateEuclideanScaledRotorSquared2D<T>(this RGaProcessor<T> processor, T scalar0, T scalar12)
    {
        return RGaEuclideanScaledRotorSquared2D<T>.Create(
            processor,
            scalar0.ScalarFromValue(processor.ScalarProcessor),
            scalar12.ScalarFromValue(processor.ScalarProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaEuclideanScaledRotorSquared2D<T> CreateEuclideanScaledRotorSquared2D<T>(this RGaProcessor<T> processor, Scalar<T> scalar0, Scalar<T> scalar12)
    {
        return RGaEuclideanScaledRotorSquared2D<T>.Create(
            processor,
            scalar0,
            scalar12
        );
    }
        
    public static RGaEuclideanScaledRotorSquared2D<T> CreateEuclideanScaledRotorSquared2D<T>(this RGaVector<T> sourceVector, RGaVector<T> targetVector)
    {
        var processor = sourceVector.Processor;

        var u1 = sourceVector.Scalar(0);
        var u2 = sourceVector.Scalar(1);

        var v1 = targetVector.Scalar(0);
        var v2 = targetVector.Scalar(1);

        var uNormSquared = u1 * u1 + u2 * u2;

        var a0 = (v1 * u1 + v2 * u2) / uNormSquared;
        var a12 = (v1 * u2 - v2 * u1) / uNormSquared;

        return RGaEuclideanScaledRotorSquared2D<T>.Create(processor, a0, a12);
    }
}