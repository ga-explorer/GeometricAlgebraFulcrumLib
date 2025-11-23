using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;

public static class XGaFloat64RotorUtils
{
    

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector into the target vector
    /// </summary>
    /// <param name="sourceVector"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVectors"></param>
    /// <returns></returns>
    public static XGaFloat64PureScalingRotor CreateEuclideanPureRotor(this LinFloat64Vector2D sourceVector, LinFloat64Vector2D targetVector, bool assumeUnitVectors = false)
    {
        var basisSet = XGaFloat64Processor.Euclidean;

        var cosAngle =
            assumeUnitVectors
                ? targetVector.VectorDot(sourceVector)
                : targetVector.VectorDot(sourceVector) /
                  (targetVector.L2NormSquared() * sourceVector.L2NormSquared()).Sqrt();

        if (cosAngle == 1d)
            return basisSet.IdentityScalingRotor();

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        var rotationBlade =
            cosAngle.IsNegativeOne()
                ? sourceVector.GetUnitNormal().ToXGaVector().Op(sourceVector.ToXGaVector())
                : targetVector.ToXGaVector().Op(sourceVector.ToXGaVector());

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
    public static XGaFloat64PureScalingRotor CreateEuclideanPureRotor(this LinFloat64Vector3D sourceVector, LinFloat64Vector3D targetVector, bool assumeUnitVectors = false)
    {
        var basisSet = XGaFloat64Processor.Euclidean;

        var cosAngle =
            assumeUnitVectors
                ? targetVector.VectorDot(sourceVector)
                : targetVector.VectorDot(sourceVector) /
                  (targetVector.L2NormSquared() * sourceVector.L2NormSquared()).Sqrt();

        if (cosAngle == 1d)
            return basisSet.IdentityScalingRotor();

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        var rotationBlade =
            cosAngle == -1
                ? sourceVector.GetUnitNormal().ToXGaVector().Op(sourceVector.ToXGaVector())
                : targetVector.ToXGaVector().Op(sourceVector.ToXGaVector());

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
    public static XGaFloat64PureScalingRotor CreateEuclideanPureScalingRotor(this LinFloat64Vector2D sourceVector, LinFloat64Vector2D targetVector)
    {
        var basisSet = XGaFloat64Processor.Euclidean;

        var uNorm = sourceVector.L2Norm();
        var vNorm = targetVector.L2Norm();
        var scalingFactor = (vNorm / uNorm).Sqrt();
        var cosAngle = targetVector.VectorDot(sourceVector) / (uNorm * vNorm);

        if (cosAngle == 1d)
            return basisSet.IdentityScalingRotor(scalingFactor);

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        var rotationBlade =
            cosAngle == -1d
                ? sourceVector.GetUnitNormal().ToXGaVector().Op(sourceVector.ToXGaVector())
                : targetVector.ToXGaVector().Op(sourceVector.ToXGaVector());

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
    public static XGaFloat64PureScalingRotor CreateEuclideanPureScalingRotor(this LinFloat64Vector3D sourceVector, LinFloat64Vector3D targetVector)
    {
        var basisSet = XGaFloat64Processor.Euclidean;

        var uNorm = sourceVector.L2Norm();
        var vNorm = targetVector.L2Norm();
        var scalingFactor = (vNorm / uNorm).Sqrt();
        var cosAngle = targetVector.VectorDot(sourceVector) / (uNorm * vNorm);

        if (cosAngle == 1d)
            return basisSet.IdentityScalingRotor(scalingFactor);

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        var rotationBlade =
            cosAngle == -1d
                ? sourceVector.GetUnitNormal().ToXGaVector().Op(sourceVector.ToXGaVector())
                : targetVector.ToXGaVector().Op(sourceVector.ToXGaVector());

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