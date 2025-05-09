using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;

public static class RGaFloat64EuclideanScaledRotorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64EuclideanScaledRotor2D CreateEuclideanScaledRotor2D(this RGaFloat64Processor metric, double scalar0, double scalar12)
    {
        return RGaFloat64EuclideanScaledRotor2D.Create(
            metric,
            scalar0,
            scalar12
        );
    }
        
    public static RGaFloat64EuclideanScaledRotor2D CreateEuclideanScaledRotor2D(this RGaFloat64Vector sourceVector, RGaFloat64Vector targetVector)
    {
        Debug.Assert(
            sourceVector.Metric.HasSameSignature(targetVector.Metric)
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

        return RGaFloat64EuclideanScaledRotor2D.Create(metric, a0, a12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaEuclideanScaledRotorSquared2D CreateEuclideanScaledRotorSquared2D(double scalar0, double scalar12)
    {
        return RGaEuclideanScaledRotorSquared2D.Create(scalar0, scalar12);
    }

    public static RGaEuclideanScaledRotorSquared2D CreateEuclideanScaledRotorSquared2D(this RGaFloat64Vector sourceVector, RGaFloat64Vector targetVector)
    {
        var u1 = sourceVector.Scalar(0);
        var u2 = sourceVector.Scalar(1);

        var v1 = targetVector.Scalar(0);
        var v2 = targetVector.Scalar(1);

        var uNormSquared = u1 * u1 + u2 * u2;

        var a0 = (v1 * u1 + v2 * u2) / uNormSquared;
        var a12 = (v1 * u2 - v2 * u1) / uNormSquared;

        return RGaEuclideanScaledRotorSquared2D.Create(a0, a12);
    }
}