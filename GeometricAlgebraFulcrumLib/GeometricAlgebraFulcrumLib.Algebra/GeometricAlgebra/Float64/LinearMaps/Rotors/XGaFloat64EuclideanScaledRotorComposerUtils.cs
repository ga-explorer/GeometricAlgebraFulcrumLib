using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;

public static class XGaFloat64EuclideanScaledRotorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64EuclideanScaledRotor2D CreateEuclideanScaledRotor2D(this XGaFloat64Processor metric, double scalar0, double scalar12)
    {
        return XGaFloat64EuclideanScaledRotor2D.Create(
            metric,
            scalar0,
            scalar12
        );
    }
        
    public static XGaFloat64EuclideanScaledRotor2D CreateEuclideanScaledRotor2D(this XGaFloat64Vector sourceVector, XGaFloat64Vector targetVector)
    {
        Debug.Assert(
            sourceVector.Processor.HasSameSignature(targetVector.Processor)
        );

        var metric = sourceVector.Processor;

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

        return XGaFloat64EuclideanScaledRotor2D.Create(metric, a0, a12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64EuclideanScaledRotorSquared2D CreateEuclideanScaledRotorSquared2D(XGaFloat64Processor processor, double scalar0, double scalar12)
    {
        return XGaFloat64EuclideanScaledRotorSquared2D.Create(processor, scalar0, scalar12);
    }

    public static XGaFloat64EuclideanScaledRotorSquared2D CreateEuclideanScaledRotorSquared2D(this XGaFloat64Vector sourceVector, XGaFloat64Vector targetVector)
    {
        var u1 = sourceVector[0];
        var u2 = sourceVector[1];

        var v1 = targetVector[0];
        var v2 = targetVector[1];

        var uNormSquared = u1 * u1 + u2 * u2;

        var a0 = (v1 * u1 + v2 * u2) / uNormSquared;
        var a12 = (v1 * u2 - v2 * u1) / uNormSquared;

        return XGaFloat64EuclideanScaledRotorSquared2D.Create(sourceVector.Processor, a0, a12);
    }
}