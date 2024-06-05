using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Maps;

public static class GaMapFactory
{
    /// <summary>
    /// Create an identity rotor
    /// </summary>
    /// <param name="basisSet"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaScaledPureRotor CreateIdentityRotor(this RGaFloat64Processor basisSet)
    {
        var multivector = 
            basisSet.Scalar(1);

        return new GaScaledPureRotor(
            multivector,
            multivector,
            1d
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaScaledPureRotor CreateScaledIdentityRotor(this RGaFloat64Processor basisSet, double scalingFactor)
    {
        if (scalingFactor <= 0)
            throw new ArgumentOutOfRangeException(nameof(scalingFactor));

        var multivector = 
            basisSet.Scalar(Math.Sqrt(scalingFactor));

        return new GaScaledPureRotor(
            multivector,
            multivector,
            scalingFactor
        );
    }
        
    /// <summary>
    /// Create a pure rotor from a 2-blade, the signature of the blade
    /// is computed automatically
    /// </summary>
    /// <param name="blade"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaScaledPureRotor CreatePureRotor(this RGaFloat64Multivector blade)
    {
        var sign = blade.SpSquared().Sign();

        if (sign.IsZero)
            return CreatePureRotor(blade, IntegerSign.Zero);

        return sign.IsPositive
            ? CreatePureRotor(blade, IntegerSign.Positive)
            : CreatePureRotor(blade, IntegerSign.Negative);
    }

    /// <summary>
    /// Create a pure rotor from a 2-blade, the signature of the blade
    /// is given by the user
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="bladeSignature"></param>
    /// <returns></returns>
    public static GaScaledPureRotor CreatePureRotor(this RGaFloat64Multivector blade, IntegerSign bladeSignature)
    {
        var basisSet = blade.Processor;

        if (bladeSignature.IsZero)
        {
            var mv = basisSet.Scalar(1);

            return new GaScaledPureRotor(mv, mv, 1d);
        }
            
        if (bladeSignature.IsNegative)
        {
            var alpha = (-blade.SpSquared()).Sqrt();
            var mv = blade.Times(alpha.Sin() / alpha) + alpha.Cos();

            return new GaScaledPureRotor(mv);
        }
        else
        {
            var alpha = blade.SpSquared().Sqrt();
            var mv = blade.Times(alpha.Sinh() / alpha) + alpha.Cosh();

            return new GaScaledPureRotor(mv);
        }
    }

    /// <summary>
    /// Create a pure rotor from its scalar and bivector parts
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaScaledPureRotor CreateScaledPureRotor(this RGaFloat64Multivector multivector)
    {
        return new GaScaledPureRotor(multivector);
    }
        
    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector into the target vector
    /// </summary>
    /// <param name="sourceVector"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVectors"></param>
    /// <returns></returns>
    public static GaScaledPureRotor CreateEuclideanPureRotor(this RGaFloat64Vector sourceVector, RGaFloat64Vector targetVector, bool assumeUnitVectors = false)
    {
        var basisSet = sourceVector.Processor;

        var cosAngle = 
            assumeUnitVectors
                ? targetVector.ESp(sourceVector)
                : targetVector.ESp(sourceVector) / 
                  (targetVector.ENormSquared() * sourceVector.ENormSquared()).Sqrt();

        if (cosAngle == 1d)
            return basisSet.CreateIdentityRotor();

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt().ScalarValue;
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt().ScalarValue;

        var rotationBlade = 
            cosAngle == -1
                ? sourceVector.GetEUnitNormalVector().Op(sourceVector)
                : targetVector.Op(sourceVector);

        var unitRotationBlade = 
            rotationBlade.Divide((-rotationBlade.ESpSquared()).Sqrt());

        var bivectorPart = 
            unitRotationBlade.Times(sinHalfAngle);

        return new GaScaledPureRotor(
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
    public static GaScaledPureRotor CreateEuclideanPureRotor(this ILinFloat64Vector2D sourceVector, ILinFloat64Vector2D targetVector, bool assumeUnitVectors = false)
    {
        var basisSet = RGaFloat64Processor.Euclidean;

        var cosAngle = 
            assumeUnitVectors
                ? targetVector.VectorESp(sourceVector)
                : targetVector.VectorESp(sourceVector) / 
                  (targetVector.VectorENormSquared() * sourceVector.VectorENormSquared()).Sqrt();

        if (cosAngle == 1d)
            return basisSet.CreateIdentityRotor();

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        var rotationBlade = 
            cosAngle.IsNegativeOne()
                ? sourceVector.GetUnitNormal().ToRGaFloat64Vector().Op(sourceVector.ToRGaFloat64Vector())
                : targetVector.ToRGaFloat64Vector().Op(sourceVector.ToRGaFloat64Vector());

        var unitRotationBlade = 
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

        var bivectorPart = sinHalfAngle * unitRotationBlade;

        return new GaScaledPureRotor(
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
    public static GaScaledPureRotor CreateEuclideanPureRotor(this ILinFloat64Vector3D sourceVector, ILinFloat64Vector3D targetVector, bool assumeUnitVectors = false)
    {
        var basisSet = RGaFloat64Processor.Euclidean;

        var cosAngle = 
            assumeUnitVectors
                ? targetVector.VectorESp(sourceVector)
                : targetVector.VectorESp(sourceVector) / 
                  (targetVector.VectorENormSquared() * sourceVector.VectorENormSquared()).Sqrt();

        if (cosAngle == 1d)
            return basisSet.CreateIdentityRotor();

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        var rotationBlade = 
            cosAngle == -1
                ? sourceVector.GetUnitNormal().ToRGaFloat64Vector().Op(sourceVector.ToRGaFloat64Vector())
                : targetVector.ToRGaFloat64Vector().Op(sourceVector.ToRGaFloat64Vector());

        var unitRotationBlade = 
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

        var bivectorPart = sinHalfAngle * unitRotationBlade;

        return new GaScaledPureRotor(
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
    public static GaScaledPureRotor CreateEuclideanScaledPureRotor(this RGaFloat64Vector sourceVector, RGaFloat64Vector targetVector)
    {
        var basisSet = sourceVector.Processor;

        var uNorm = sourceVector.ENorm();
        var vNorm = targetVector.ENorm();
        var scalingFactor = (vNorm / uNorm).Sqrt();
        var cosAngle = targetVector.ESp(sourceVector).Divide(uNorm * vNorm);

        if (cosAngle == 1d)
            return basisSet.CreateScaledIdentityRotor(scalingFactor.ScalarValue);
            
        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        var rotationBlade = 
            cosAngle == -1d
                ? sourceVector.GetEUnitNormalVector().Op(sourceVector)
                : targetVector.Op(sourceVector);

        var unitRotationBlade = 
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

        var scalarPart = 
            scalingFactor * cosHalfAngle;

        var bivectorPart = 
            scalingFactor * sinHalfAngle * unitRotationBlade;

        return new GaScaledPureRotor(
            scalarPart.ScalarValue + bivectorPart
        );
    }
        
    /// <summary>
    /// Create a scaled pure Euclidean rotor that rotates and
    /// scales the given source vector into the target vector
    /// </summary>
    /// <param name="sourceVector"></param>
    /// <param name="targetVector"></param>
    /// <returns></returns>
    public static GaScaledPureRotor CreateEuclideanScaledPureRotor(this ILinFloat64Vector2D sourceVector, ILinFloat64Vector2D targetVector)
    {
        var basisSet = RGaFloat64Processor.Euclidean;

        var uNorm = sourceVector.VectorENorm();
        var vNorm = targetVector.VectorENorm();
        var scalingFactor = (vNorm / uNorm).Sqrt();
        var cosAngle = targetVector.VectorESp(sourceVector) / (uNorm * vNorm);

        if (cosAngle == 1d)
            return basisSet.CreateScaledIdentityRotor(scalingFactor);
            
        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        var rotationBlade = 
            cosAngle == -1d
                ? sourceVector.GetUnitNormal().ToRGaFloat64Vector().Op(sourceVector.ToRGaFloat64Vector())
                : targetVector.ToRGaFloat64Vector().Op(sourceVector.ToRGaFloat64Vector());

        var unitRotationBlade = 
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

        var scalarPart = 
            scalingFactor * cosHalfAngle;

        var bivectorPart = 
            scalingFactor * sinHalfAngle * unitRotationBlade;

        return new GaScaledPureRotor(
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
    public static GaScaledPureRotor CreateEuclideanScaledPureRotor(this ILinFloat64Vector3D sourceVector, ILinFloat64Vector3D targetVector)
    {
        var basisSet = RGaFloat64Processor.Euclidean;

        var uNorm = sourceVector.VectorENorm();
        var vNorm = targetVector.VectorENorm();
        var scalingFactor = (vNorm / uNorm).Sqrt();
        var cosAngle = targetVector.VectorESp(sourceVector) / (uNorm * vNorm);

        if (cosAngle == 1d)
            return basisSet.CreateScaledIdentityRotor(scalingFactor);
            
        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        var rotationBlade = 
            cosAngle == -1d
                ? sourceVector.GetUnitNormal().ToRGaFloat64Vector().Op(sourceVector.ToRGaFloat64Vector())
                : targetVector.ToRGaFloat64Vector().Op(sourceVector.ToRGaFloat64Vector());

        var unitRotationBlade = 
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

        var scalarPart = 
            scalingFactor * cosHalfAngle;

        var bivectorPart = 
            scalingFactor * sinHalfAngle * unitRotationBlade;

        return new GaScaledPureRotor(
            scalarPart + bivectorPart
        );
    }

    /// <summary>
    /// Create a simple rotor from an angle and a 2-blade
    /// </summary>
    /// <param name="rotationAngle"></param>
    /// <param name="rotationBlade"></param>
    /// <returns></returns>
    public static GaScaledPureRotor CreateEuclideanPureRotor(this RGaFloat64Bivector rotationBlade, LinFloat64Angle rotationAngle)
    {
        var (sinHalfAngle, cosHalfAngle) = 
            (0.5d * rotationAngle.RadiansValue).SinCos();

        var bivectorPart =
            sinHalfAngle / (-rotationBlade.ESpSquared()).Sqrt() * rotationBlade;

        return new GaScaledPureRotor(
            cosHalfAngle + bivectorPart,
            cosHalfAngle - bivectorPart
        );
    }

    /// <summary>
    /// Create one rotor from the parametric family of pure rotors taking
    /// sourceVector to targetVector in 3D Euclidean space
    /// </summary>
    /// <param name="basisSet"></param>
    /// <param name="sourceVector"></param>
    /// <param name="targetVector"></param>
    /// <param name="angleTheta"></param>
    /// <returns></returns>
    public static GaScaledPureRotor CreateEuclideanParametricPureRotor3D(this RGaFloat64Processor basisSet, ILinFloat64Vector3D sourceVector, ILinFloat64Vector3D targetVector, LinFloat64Angle angleTheta)
    {
        //var basisSet = BasisBladeSet.CreateEuclidean(3);

        // Compute inverse of 3D pseudo-scalar = -e123
        var pseudoScalarInverse =
            basisSet.CreateBasisBlade(7).ToKVector().EInverse();

        // Compute the smallest angle between source and target vectors
        var cosAngle0 = 
            sourceVector.VectorESp(targetVector);

        // Define a rotor S with angle theta in the plane orthogonal to targetVector - sourceVector
        var rotorSBlade = 
            targetVector.VectorSubtract(sourceVector).ToRGaFloat64Vector().EGp(pseudoScalarInverse).GetBivectorPart();

        var rotorS = 
            rotorSBlade.CreateEuclideanPureRotor(angleTheta);

        // Define parametric 2-blade of rotation
        // The actual plane of rotation is made by rotating the plane containing
        // sourceVector and targetVector by angle theta in the plane orthogonal to
        // targetVector - sourceVector using rotor S
        var rotorBlade =
            rotorS.OmMap(targetVector.ToRGaFloat64Vector().Op(sourceVector.ToRGaFloat64Vector())).GetBivectorPart();
            
        // Define parametric angle of rotation
        var rotorAngle =
            (1 + 2 * (cosAngle0 - 1) / (2 - angleTheta.Sin().Square() * (cosAngle0 + 1))).ArcCos();

        // Math.Acos(1 + 2 * (cosAngle0 - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (cosAngle0 + 1)));
            
        // Return the final rotor taking v1 into v2
        return rotorBlade.CreateEuclideanPureRotor(rotorAngle);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaScaledPureRotor CreateEuclideanScaledParametricPureRotor3D(this RGaFloat64Processor basisSet, ILinFloat64Vector3D sourceVector, ILinFloat64Vector3D targetVector, LinFloat64Angle angleTheta, bool assumeUnitVectors = false)
    {
        if (assumeUnitVectors)
            basisSet.CreateEuclideanParametricPureRotor3D(sourceVector, targetVector, angleTheta);

        var (sourceVectorUnit, sourceVectorLength) = 
            sourceVector.GetUnitVectorENormTuple();

        var (targetVectorUnit, targetVectorLength) = 
            targetVector.GetUnitVectorENormTuple();

        var scalingFactor = targetVectorLength / sourceVectorLength;

        return basisSet
            .CreateEuclideanParametricPureRotor3D(sourceVectorUnit, targetVectorUnit, angleTheta)
            .CreateScaledPureRotor(scalingFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaScaledPureRotor CreateEuclideanScaledParametricPureRotor3D(this RGaFloat64Processor basisSet, ILinFloat64Vector3D sourceVector, ILinFloat64Vector3D targetVector, LinFloat64Angle angleTheta, double scalingFactor)
    {
        return basisSet
            .CreateEuclideanParametricPureRotor3D(sourceVector, targetVector, angleTheta)
            .CreateScaledPureRotor(scalingFactor);
    }


    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <param name="basisSet"></param>
    /// <param name="sourceBasisVectorIndex"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    public static GaScaledPureRotor CreateEuclideanPureRotor(this RGaFloat64Processor basisSet, int sourceBasisVectorIndex, RGaFloat64Vector targetVector, bool assumeUnitVector = false)
    {
        var ek = basisSet.CreateBasisVector(sourceBasisVectorIndex).ToKVector();
        var vk = targetVector.GetTermScalarByIndex(sourceBasisVectorIndex);
        var vk1 = 1 + vk;

        var scalarPart = (vk1 / 2).Sqrt();
        var bivectorPart = (targetVector - vk * ek).Op(ek) / (2 * vk1).Sqrt();

        return new GaScaledPureRotor(
            scalarPart + bivectorPart,
            scalarPart - bivectorPart
        );
    }

    public static GaScaledPureRotor CreateEuclideanPureRotor(this RGaFloat64Vector sourceVector1, RGaFloat64Vector sourceVector2, RGaFloat64Vector targetVector1, RGaFloat64Vector targetVector2, bool assumeUnitVectors = false)
    {
        var rotor1 = 
            sourceVector1.CreateEuclideanPureRotor(
                targetVector1,
                assumeUnitVectors
            );

        var rotor2 = 
            rotor1.OmMap(sourceVector2)
                .GetVectorPart()
                .CreateEuclideanPureRotor(
                    targetVector2,
                    assumeUnitVectors
                );

        var multivector = 
            rotor2.Multivector.EGp(rotor1.Multivector);
            
        return new GaScaledPureRotor(multivector);
    }

    //public static GaScaledPureRotor CreateEuclideanPureRotor(this GaBasisSet basisSet, uint baseSpaceDimensions, IRGaMultivector inputVector1, IRGaMultivector inputVector2, IRGaMultivector rotatedVector1, IRGaMultivector rotatedVector2)
    //{
    //    var inputFrame = basisSet.CreateFreeFrame(
    //        GeoFreeFrameSpecs.CreateLinearlyIndependentSpecs(),
    //        inputVector1, 
    //        inputVector2
    //    );

    //    var rotatedFrame = basisSet.CreateFreeFrame(
    //        GeoFreeFrameSpecs.CreateLinearlyIndependentSpecs(),
    //        rotatedVector1, 
    //        rotatedVector2
    //    );

    //    var rotor = PureRotorsSequence.CreateFromEuclideanFrames(
    //        baseSpaceDimensions, 
    //        inputFrame, 
    //        rotatedFrame
    //    ).GetFinalRotor();

    //    var (scalar, bivector) = basisSet.GetScalarBivectorParts(rotor.Multivector);

    //    return new GaScaledPureRotor(basisSet, scalar, bivector);
    //}

    /// <summary>
    /// Construct a rotor in the e_i-e_j plane with the given angle where i is less than j
    /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
    /// </summary>
    /// <param name="basisSet"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="rotationAngle"></param>
    /// <returns></returns>
    public static GaScaledPureRotor CreateGivensRotor(this RGaFloat64Processor basisSet, int i, int j, LinFloat64Angle rotationAngle)
    {
        Debug.Assert(j != i);

        var halfRotationAngle = 0.5 * rotationAngle.RadiansValue;
        var (sinHalfAngle, cosHalfAngle) = halfRotationAngle.SinCos();

        var bivectorPart = basisSet.BivectorTerm(
            new Pair<int>(i, j),
            i < j ? sinHalfAngle : -sinHalfAngle
        );

        //var bivectorPart = basisSet.CreateBivectorTerm(i, j, sinHalfAngle);

        return new GaScaledPureRotor(
            cosHalfAngle + bivectorPart,
            cosHalfAngle - bivectorPart
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaScaledPureRotor CreateScaledPureRotor(this GaScaledPureRotor rotor, double scalingFactor)
    {
        var mv = scalingFactor.Sqrt() * rotor.Multivector;
        var mvReverse = mv.Reverse();

        return new GaScaledPureRotor(
            mv,
            mvReverse
        );
    }
        
    /// <summary>
    /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
    /// </summary>
    /// <param name="basisSet"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaScaledPureRotor CreateScaledPureRotor2D(this RGaFloat64Processor basisSet, double r0, double r12)
    {
        var multivector = basisSet.CreateComposer();

        multivector[0] = r0;
        multivector[3] = r12;
            
        return new GaScaledPureRotor(
            multivector.GetSimpleMultivector()
        );
    }

    /// <summary>
    /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
    /// </summary>
    /// <param name="basisSet"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <param name="r13"></param>
    /// <param name="r23"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaScaledPureRotor CreateScaledPureRotor3D(this RGaFloat64Processor basisSet, double r0, double r12, double r13, double r23)
    {
        var multivector = basisSet.CreateComposer();
            
        multivector[0] = r0;
        multivector[3] = r12;
        multivector[5] = r13;
        multivector[6] = r23;
            
        return new GaScaledPureRotor(
            multivector.GetSimpleMultivector()
        );
    }


    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <param name="sourceBasisVectorIndex"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    public static GaScaledPureRotor CreatePureRotorFromBasisVector(this RGaFloat64Multivector targetVector, int sourceBasisVectorIndex, bool assumeUnitVector = false)
    {
        var processor = targetVector.Metric;
        var k = sourceBasisVectorIndex;
            
        var v = 
            assumeUnitVector
                ? targetVector
                : targetVector.Divide(targetVector.ENorm());

        var ek = processor.CreateBasisVector(k).ToKVector();
            
        var vk = v.Scalar(k);
        var vk1 = 1 + vk;

        return new GaScaledPureRotor(
            (vk1 / 2).Sqrt() + v.Op(ek) / (2 * vk1).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <param name="sourceBasisVectorIndex"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    public static GaScaledPureRotor CreateScaledPureRotorFromBasisVector(this RGaFloat64Multivector targetVector, int sourceBasisVectorIndex, bool assumeUnitVector = false)
    {
        var processor = targetVector.Metric;
        var k = sourceBasisVectorIndex;
        var vNorm = 
            assumeUnitVector 
                ? 1d 
                : targetVector.ENorm().ScalarValue;

        var v = 
            assumeUnitVector
                ? targetVector
                : targetVector / vNorm;

        var ek = processor.CreateBasisVector(k).ToKVector();
            
        var vk = v.Scalar(k);
        var vk1 = 1 + vk;

        return new GaScaledPureRotor(
            (vNorm * vk1 / 2).Sqrt() + v.Op(ek) * (vNorm / (vk1 * 2)).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <param name="targetBasisVectorIndex"></param>
    /// <param name="assumeUnitVector"></param>
    /// <param name="sourceVector"></param>
    /// <returns></returns>
    public static GaScaledPureRotor CreateScaledPureRotorToBasisVector(this RGaFloat64Vector sourceVector, int targetBasisVectorIndex, bool assumeUnitVector = false)
    {
        var processor = sourceVector.Metric;
        var k = targetBasisVectorIndex;
        var vNorm = 
            assumeUnitVector 
                ? 1d 
                : sourceVector.ENorm().ScalarValue;

        var v = 
            assumeUnitVector
                ? sourceVector
                : sourceVector / vNorm;

        var ek = processor.CreateBasisVector(k).ToKVector();
            
        var vk = v.Scalar(k);
        var vk1 = 1 + vk;

        return new GaScaledPureRotor(
            (vk1 / vNorm / 2).Sqrt() + ek.Op(v) / (vNorm * vk1 * 2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector
    /// into the target basis vector
    /// </summary>
    /// <param name="targetBasisVectorIndex"></param>
    /// <param name="assumeUnitVector"></param>
    /// <param name="sourceVector"></param>
    /// <returns></returns>
    public static GaScaledPureRotor CreatePureRotorToBasisVector(this RGaFloat64Vector sourceVector, int targetBasisVectorIndex, bool assumeUnitVector = false)
    {
        var processor = sourceVector.Metric;
        var k = targetBasisVectorIndex;
            
        var v = 
            assumeUnitVector
                ? sourceVector
                : sourceVector.Divide(sourceVector.ENorm());

        var ek = processor.CreateBasisVector(k).ToKVector();
            
        var vk = v.Scalar(k);
        var vk1 = 1 + vk;

        return new GaScaledPureRotor(
            (vk1 / 2).Sqrt() + ek.Op(v) / (2 * vk1).Sqrt()
        );
    }

}