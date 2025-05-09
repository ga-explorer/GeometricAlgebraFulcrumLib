using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;

public static class XGaEuclideanScaledRotorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaEuclideanScaledRotor2D<T> CreateEuclideanScaledRotor2D<T>(this XGaProcessor<T> processor, Scalar<T> scalar0, Scalar<T> scalar12)
    {
        return XGaEuclideanScaledRotor2D<T>.Create(
            processor,
            scalar0,
            scalar12
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaEuclideanScaledRotor2D<T> CreateEuclideanScaledRotor2D<T>(this XGaProcessor<T> processor, T scalar0, T scalar12)
    {
        return XGaEuclideanScaledRotor2D<T>.Create(
            processor,
            processor.ScalarProcessor.ScalarFromValue(scalar0),
            processor.ScalarProcessor.ScalarFromValue(scalar12)
        );
    }
        
    public static XGaEuclideanScaledRotor2D<T> CreateEuclideanScaledRotor2D<T>(this XGaVector<T> sourceVector, XGaVector<T> targetVector)
    {
        Debug.Assert(
            ReferenceEquals(
                sourceVector.ScalarProcessor,
                targetVector.ScalarProcessor
            )
        );

        Debug.Assert(
            sourceVector.Metric.HasSameSignature(targetVector.Metric)
        );

        var processor = sourceVector.Processor;

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

        return XGaEuclideanScaledRotor2D<T>.Create(processor, a0, a12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaEuclideanScaledRotorSquared2D<T> CreateEuclideanScaledRotorSquared2D<T>(this XGaProcessor<T> processor, T scalar0, T scalar12)
    {
        return XGaEuclideanScaledRotorSquared2D<T>.Create(
            processor,
            scalar0.ScalarFromValue(processor.ScalarProcessor),
            scalar12.ScalarFromValue(processor.ScalarProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaEuclideanScaledRotorSquared2D<T> CreateEuclideanScaledRotorSquared2D<T>(this XGaProcessor<T> processor, Scalar<T> scalar0, Scalar<T> scalar12)
    {
        return XGaEuclideanScaledRotorSquared2D<T>.Create(
            processor,
            scalar0,
            scalar12
        );
    }
        
    public static XGaEuclideanScaledRotorSquared2D<T> CreateEuclideanScaledRotorSquared2D<T>(this XGaVector<T> sourceVector, XGaVector<T> targetVector)
    {
        var processor = sourceVector.Processor;

        var u1 = sourceVector.Scalar(0);
        var u2 = sourceVector.Scalar(1);

        var v1 = targetVector.Scalar(0);
        var v2 = targetVector.Scalar(1);

        var uNormSquared = u1 * u1 + u2 * u2;

        var a0 = (v1 * u1 + v2 * u2) / uNormSquared;
        var a12 = (v1 * u2 - v2 * u1) / uNormSquared;

        return XGaEuclideanScaledRotorSquared2D<T>.Create(processor, a0, a12);
    }
}