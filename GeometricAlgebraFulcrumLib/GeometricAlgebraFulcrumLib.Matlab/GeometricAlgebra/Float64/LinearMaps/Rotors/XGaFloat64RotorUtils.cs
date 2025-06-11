using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Rotors;

public static class XGaFloat64RotorUtils
{
    

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector into the target vector
    /// </summary>
    /// <param name="sourceVector"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVectors"></param>
    /// <returns></returns>
    public static XGaFloat64PureScalingRotor CreateEuclideanPureRotor(this ILinFloat64Vector2D sourceVector, ILinFloat64Vector2D targetVector, bool assumeUnitVectors = false)
    {
        var basisSet = XGaFloat64Processor.Euclidean;

        var cosAngle =
            assumeUnitVectors
                ? targetVector.VectorESp(sourceVector)
                : targetVector.VectorESp(sourceVector) /
                  (targetVector.VectorENormSquared() * sourceVector.VectorENormSquared()).Sqrt();

        if (cosAngle == 1d)
            return basisSet.IdentityScalingRotor();

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        var rotationBlade =
            cosAngle.IsMinusOne()
                ? sourceVector.GetUnitNormal().ToXGaFloat64Vector().Op(sourceVector.ToXGaFloat64Vector())
                : targetVector.ToXGaFloat64Vector().Op(sourceVector.ToXGaFloat64Vector());

        var unitRotationBlade =
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

        var bivectorPart = sinHalfAngle * unitRotationBlade;

        return XGaFloat64PureScalingRotor.Create(
            cosHalfAngle + bivectorPart,
            cosHalfAngle - bivectorPart
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector into the target vector
    /// </summary>
    /// <param name="sourceVector"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVectors"></param>
    /// <returns></returns>
    public static XGaFloat64PureScalingRotor CreateEuclideanPureRotor(this ILinFloat64Vector3D sourceVector, ILinFloat64Vector3D targetVector, bool assumeUnitVectors = false)
    {
        var basisSet = XGaFloat64Processor.Euclidean;

        var cosAngle =
            assumeUnitVectors
                ? targetVector.VectorESp(sourceVector)
                : targetVector.VectorESp(sourceVector) /
                  (targetVector.VectorENormSquared() * sourceVector.VectorENormSquared()).Sqrt();

        if (cosAngle == 1d)
            return basisSet.IdentityScalingRotor();

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        var rotationBlade =
            cosAngle == -1
                ? sourceVector.GetUnitNormal().ToXGaFloat64Vector().Op(sourceVector.ToXGaFloat64Vector())
                : targetVector.ToXGaFloat64Vector().Op(sourceVector.ToXGaFloat64Vector());

        var unitRotationBlade =
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

        var bivectorPart = sinHalfAngle * unitRotationBlade;

        return XGaFloat64PureScalingRotor.Create(
            cosHalfAngle + bivectorPart,
            cosHalfAngle - bivectorPart
        );
    }

    /// <summary>
    /// Create a scaled pure Euclidean rotor that rotates and
    /// scales the given source vector into the target vector
    /// </summary>
    /// <param name="sourceVector"></param>
    /// <param name="targetVector"></param>
    /// <returns></returns>
    public static XGaFloat64PureScalingRotor CreateEuclideanPureScalingRotor(this ILinFloat64Vector2D sourceVector, ILinFloat64Vector2D targetVector)
    {
        var basisSet = XGaFloat64Processor.Euclidean;

        var uNorm = sourceVector.VectorENorm();
        var vNorm = targetVector.VectorENorm();
        var scalingFactor = (vNorm / uNorm).Sqrt();
        var cosAngle = targetVector.VectorESp(sourceVector) / (uNorm * vNorm);

        if (cosAngle == 1d)
            return basisSet.IdentityScalingRotor(scalingFactor);

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        var rotationBlade =
            cosAngle == -1d
                ? sourceVector.GetUnitNormal().ToXGaFloat64Vector().Op(sourceVector.ToXGaFloat64Vector())
                : targetVector.ToXGaFloat64Vector().Op(sourceVector.ToXGaFloat64Vector());

        var unitRotationBlade =
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

        var scalarPart =
            scalingFactor * cosHalfAngle;

        var bivectorPart =
            scalingFactor * sinHalfAngle * unitRotationBlade;

        return XGaFloat64PureScalingRotor.Create(
            scalarPart + bivectorPart
        );
    }

    /// <summary>
    /// Create a scaled pure Euclidean rotor that rotates and
    /// scales the given source vector into the target vector
    /// </summary>
    /// <param name="sourceVector"></param>
    /// <param name="targetVector"></param>
    /// <returns></returns>
    public static XGaFloat64PureScalingRotor CreateEuclideanPureScalingRotor(this ILinFloat64Vector3D sourceVector, ILinFloat64Vector3D targetVector)
    {
        var basisSet = XGaFloat64Processor.Euclidean;

        var uNorm = sourceVector.VectorENorm();
        var vNorm = targetVector.VectorENorm();
        var scalingFactor = (vNorm / uNorm).Sqrt();
        var cosAngle = targetVector.VectorESp(sourceVector) / (uNorm * vNorm);

        if (cosAngle == 1d)
            return basisSet.IdentityScalingRotor(scalingFactor);

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        var rotationBlade =
            cosAngle == -1d
                ? sourceVector.GetUnitNormal().ToXGaFloat64Vector().Op(sourceVector.ToXGaFloat64Vector())
                : targetVector.ToXGaFloat64Vector().Op(sourceVector.ToXGaFloat64Vector());

        var unitRotationBlade =
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

        var scalarPart =
            scalingFactor * cosHalfAngle;

        var bivectorPart =
            scalingFactor * sinHalfAngle * unitRotationBlade;

        return XGaFloat64PureScalingRotor.Create(
            scalarPart + bivectorPart
        );
    }
}