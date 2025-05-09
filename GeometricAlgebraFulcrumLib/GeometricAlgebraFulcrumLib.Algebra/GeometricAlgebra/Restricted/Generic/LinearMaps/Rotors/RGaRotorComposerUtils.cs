using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;

public static class RGaRotorComposerUtils
{
    /// <summary>
    /// Create an identity rotor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="processor"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureRotor<T> CreateIdentityRotor<T>(this RGaProcessor<T> processor)
    {
        return RGaPureRotor<T>.Create(
            processor.ScalarProcessor.OneValue,
            processor.BivectorZero
        );
    }

    /// <summary>
    /// Create an identity rotor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="processor"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledPureRotor<T> CreateScaledIdentityRotor<T>(this RGaProcessor<T> processor)
    {
        return RGaScaledPureRotor<T>.Create(
            processor.ScalarProcessor.OneValue,
            processor.BivectorZero
        );
    }

    /// <summary>
    /// Create an identity rotor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="processor"></param>
    /// <param name="scalingFactor"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledPureRotor<T> CreateScaledIdentityRotor<T>(this RGaProcessor<T> processor, T scalingFactor)
    {
        return RGaScaledPureRotor<T>.Create(
            scalingFactor,
            processor.BivectorZero
        );
    }

    /// <summary>
    /// Create a pure rotor from a 2-blade, the signature of the blade
    /// is computed automatically using the given processor which must
    /// be of numerical type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="blade"></param>
    /// <returns></returns>
    public static RGaPureRotor<T> CreatePureRotor<T>(this RGaBivector<T> blade)
    {
        var processor = blade.ScalarProcessor;

        if (!processor.IsNumeric)
            throw new InvalidOperationException();

        var bladeSignature = blade.SpSquared();

        if (bladeSignature.IsNearZero())
            return RGaPureRotor<T>.Create(
                processor.OneValue,
                blade
            );

        if (bladeSignature.IsNegative())
        {
            var alpha = (-bladeSignature).Sqrt();
            var scalar = alpha.Cos().ScalarValue;
            var bivector = alpha.Sin() / alpha * blade;

            return RGaPureRotor<T>.Create(
                scalar,
                bivector
            );
        }
        else
        {
            var alpha = bladeSignature.Sqrt();
            var scalar = alpha.Cosh().ScalarValue;
            var bivector = alpha.Sinh() / alpha * blade;

            return RGaPureRotor<T>.Create(
                scalar,
                bivector
            );
        }
    }

    /// <summary>
    /// Create a pure rotor from a 2-blade, the signature of the blade
    /// is given by the user
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="blade"></param>
    /// <param name="bladeSignatureSign"></param>
    /// <returns></returns>
    public static RGaPureRotor<T> CreatePureRotor<T>(this RGaBivector<T> blade, IntegerSign bladeSignatureSign)
    {
        var processor = blade.ScalarProcessor;

        if (bladeSignatureSign.IsZero)
            return RGaPureRotor<T>.Create(
                processor.OneValue,
                blade
            );

        var bladeSignature = blade.SpSquared();

        if (bladeSignatureSign.IsNegative)
        {
            var alpha = (-bladeSignature).Sqrt();
            var scalar = alpha.Cos().ScalarValue;
            var bivector = alpha.Sin() / alpha * blade;

            return RGaPureRotor<T>.Create(
                scalar,
                bivector
            );
        }
        else
        {
            var alpha = bladeSignature.Sqrt();
            var scalar = alpha.Cosh().ScalarValue;
            var bivector = alpha.Sinh() / alpha * blade;

            return RGaPureRotor<T>.Create(
                scalar,
                bivector
            );
        }
    }

    /// <summary>
    /// Create a pure rotor from its scalar and bivector parts
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rotorMv"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureRotor<T> CreatePureRotor<T>(this RGaMultivector<T> rotorMv)
    {
        return RGaPureRotor<T>.Create(
            rotorMv.GetScalarPart().ScalarValue,
            rotorMv.GetBivectorPart()
        );
    }
        
    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector into the target vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourceVector"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVectors"></param>
    /// <returns></returns>
    public static RGaPureRotor<T> CreatePureRotor<T>(this RGaVector<T> sourceVector, RGaVector<T> targetVector, bool assumeUnitVectors = false)
    {
        var processor = sourceVector.Processor;
        var cosAngle =
            assumeUnitVectors
                ? targetVector.ESp(sourceVector)
                : targetVector.ESp(sourceVector) / (targetVector.ENormSquared() * sourceVector.ENormSquared()).Sqrt();

        if (cosAngle.IsOne)
            return processor.CreateIdentityRotor();

        if (cosAngle.IsMinusOne)
            throw new InvalidOperationException();
            //sourceVector.GetNormalVector().Op(sourceVector)

        var (cosHalfAngle, sinHalfAngle) = 
            LinPolarAngle<T>.CreateHalfAngleFromCos(cosAngle);

        var rotationBlade = targetVector.Op(sourceVector);
        
        var unitRotationBlade =
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

        var scalarPart = cosHalfAngle;
        var bivectorPart = sinHalfAngle * unitRotationBlade;

        return RGaPureRotor<T>.Create(
            scalarPart,
            bivectorPart
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector
    /// into the target vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourceVector"></param>
    /// <param name="targetVector"></param>
    /// <returns></returns>
    public static RGaScaledPureRotor<T> CreateScaledPureRotor<T>(this RGaVector<T> sourceVector, RGaVector<T> targetVector)
    {
        var processor = sourceVector.Processor;

        var uNorm = sourceVector.ENorm();
        var vNorm = targetVector.ENorm();
        var scalingFactor = (vNorm / uNorm).Sqrt().ScalarValue;
        var cosAngle = targetVector.ESp(sourceVector) / (uNorm * vNorm);

        if (cosAngle.IsOne)
            return RGaScaledPureRotor<T>.Create(processor, scalingFactor);
            
        var rotationBlade = 
            cosAngle.IsMinusOne
                ? throw new InvalidOperationException()//sourceVector.GetNormalVector().Op(sourceVector)
                : targetVector.Op(sourceVector);
                
        var unitRotationBlade =
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        Debug.Assert(scalingFactor != null, nameof(scalingFactor) + " != null");
        
        var scalarPart =
            scalingFactor * cosHalfAngle;

        var bivectorPart =
            scalingFactor * sinHalfAngle * unitRotationBlade;

        return RGaScaledPureRotor<T>.Create(
            scalarPart.ScalarValue,
            bivectorPart
        );
    }

    /// <summary>
    /// Create one rotor from the parametric family of pure rotors taking
    /// sourceVector to targetVector in 3D Euclidean space
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourceVector"></param>
    /// <param name="targetVector"></param>
    /// <param name="angleTheta"></param>
    /// <returns></returns>
    public static RGaPureRotor<T> CreateParametricPureRotor3D<T>(this RGaVector<T> sourceVector, RGaVector<T> targetVector, LinAngle<T> angleTheta)
    {
        var processor = sourceVector.Processor;
        
        // Compute inverse of 3D pseudo-scalar = -e123
        var pseudoScalarInverse =
            processor.PseudoScalarInverse(3);

        // Compute the smallest angle between source and target vectors
        var cosAngle0 =
            sourceVector.ESp(targetVector);

        // Define a rotor S with angle theta in the plane orthogonal to targetVector - sourceVector
        var rotorSBlade =
            (targetVector - sourceVector).EGp(
                pseudoScalarInverse
            ).GetBivectorPart();

        var rotorS = rotorSBlade.CreatePureRotor(angleTheta);

        // Define parametric 2-blade of rotation
        // The actual plane of rotation is made by rotating the plane containing
        // sourceVector and targetVector by angle theta in the plane orthogonal to
        // targetVector - sourceVector using rotor S
        var rotorBlade =
            rotorS.OmMap(targetVector.Op(sourceVector));

        var sinAngleThetaSquare = angleTheta.SinSquared();

        // Define parametric angle of rotation
        var rotorAngle =
            (1 + 2 * (cosAngle0 - 1) / (2 - sinAngleThetaSquare * (cosAngle0 + 1))).ArcCos();

        // Math.Acos(1 + 2 * (cosAngle0 - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (cosAngle0 + 1)));

        // Return the final rotor taking v1 into v2
        return rotorBlade.CreatePureRotor(rotorAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledPureRotor<T> CreateScaledParametricPureRotor3D<T>(this RGaVector<T> sourceVector, RGaVector<T> targetVector, LinAngle<T> angleTheta, T scalingFactor)
    {
        return sourceVector
            .CreateParametricPureRotor3D(targetVector, angleTheta)
            .CreateScaledPureRotor(scalingFactor);
    }


    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourceAxis"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    public static RGaScaledPureRotor<T> CreateScaledPureRotorFromAxis<T>(this RGaVector<T> targetVector, LinSignedBasisVector sourceAxis, bool assumeUnitVector = false)
    {
        var processor = targetVector.Processor;
        var scalarProcessor = targetVector.ScalarProcessor;

        var k = sourceAxis.Index;
        var vNorm = assumeUnitVector
            ? scalarProcessor.OneValue
            : targetVector.ENorm().ScalarValue;

        var ek = processor.VectorTerm(k);

        Debug.Assert(vNorm != null, nameof(vNorm) + " != null");
        
        var vk1 = vNorm + (sourceAxis.IsPositive ? targetVector.Scalar(k) : -targetVector.Scalar(k));
        var vOpAxis = sourceAxis.IsPositive ? targetVector.Op(ek) : ek.Op(targetVector);

        return RGaScaledPureRotor<T>.Create(
            (vk1 / 2).Sqrt().ScalarValue,
            vOpAxis / (vk1 * 2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="targetAxis"></param>
    /// <param name="assumeUnitVector"></param>
    /// <param name="sourceVector"></param>
    /// <returns></returns>
    public static RGaScaledPureRotor<T> CreateScaledPureRotorToAxis<T>(this RGaVector<T> sourceVector, LinSignedBasisVector targetAxis, bool assumeUnitVector = false)
    {
        var processor = sourceVector.Processor;
        var scalarProcessor = sourceVector.ScalarProcessor;

        var k = targetAxis.Index;
        var vNorm =
            assumeUnitVector
                ? scalarProcessor.OneValue
                : sourceVector.ENorm().ScalarValue;

        var vNorm2 =
            assumeUnitVector
                ? scalarProcessor.Two
                : sourceVector.ENormSquared().Times(2);

        var ek = processor.VectorTerm(k);

        Debug.Assert(vNorm != null, nameof(vNorm) + " != null");
        
        var vk1 = vNorm + (targetAxis.IsPositive ? sourceVector.Scalar(k) : -sourceVector.Scalar(k));
        var vOpAxis = targetAxis.IsPositive ? ek.Op(sourceVector) : sourceVector.Op(ek);

        return RGaScaledPureRotor<T>.Create(
            (vk1 / vNorm2).Sqrt().ScalarValue,
            vOpAxis / (vk1 * vNorm2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourceAxis"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    public static RGaPureRotor<T> CreatePureRotorFromAxis<T>(this RGaVector<T> targetVector, LinSignedBasisVector sourceAxis, bool assumeUnitVector = false)
    {
        var processor = targetVector.Processor;

        var k = sourceAxis.Index;

        var v =
            assumeUnitVector
                ? targetVector
                : targetVector.DivideByENorm();

        var ek = processor.VectorTerm(k);

        var vk1 = 1 + (sourceAxis.IsPositive ? v.Scalar(k) : -v.Scalar(k));
        var vOpAxis = sourceAxis.IsPositive ? v.Op(ek) : ek.Op(v);

        return RGaPureRotor<T>.Create(
            (vk1 / 2).Sqrt().ScalarValue,
            vOpAxis / (vk1 * 2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector
    /// into the target basis vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="targetAxis"></param>
    /// <param name="assumeUnitVector"></param>
    /// <param name="sourceVector"></param>
    /// <returns></returns>
    public static RGaPureRotor<T> CreatePureRotorToAxis<T>(this RGaVector<T> sourceVector, LinSignedBasisVector targetAxis, bool assumeUnitVector = false)
    {
        var processor = sourceVector.Processor;
            

        var k = targetAxis.Index;

        var v =
            assumeUnitVector
                ? sourceVector
                : sourceVector.DivideByENorm();

        var ek = processor.VectorTerm(k);

        var vk1 = 1 + (targetAxis.IsPositive ? v.Scalar(k) : -v.Scalar(k));
        var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

        return RGaPureRotor<T>.Create(
            (vk1 / 2).Sqrt().ScalarValue,
            vOpAxis / (vk1 * 2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourceAxis"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    public static RGaPureRotor<T> CreatePureRotor<T>(this LinSignedBasisVector sourceAxis, RGaVector<T> targetVector, bool assumeUnitVector = false)
    {
        var processor = targetVector.Processor;

        var k = sourceAxis.Index;

        var v =
            assumeUnitVector
                ? targetVector
                : targetVector.DivideByENorm();

        var ek = processor.VectorTerm(k);

        var vk1 = 1 + (sourceAxis.IsPositive ? v.Scalar(k) : -v.Scalar(k));
        var vOpAxis = sourceAxis.IsPositive ? v.Op(ek) : ek.Op(v);

        return RGaPureRotor<T>.Create(
            (vk1 / 2).Sqrt().ScalarValue,
            vOpAxis / (vk1 * 2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector
    /// into the target basis vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="targetAxis"></param>
    /// <param name="assumeUnitVector"></param>
    /// <param name="sourceVector"></param>
    /// <returns></returns>
    public static RGaPureRotor<T> CreatePureRotor<T>(this RGaVector<T> sourceVector, LinSignedBasisVector targetAxis, bool assumeUnitVector = false)
    {
        var processor = sourceVector.Processor;
            

        var k = targetAxis.Index;

        var v =
            assumeUnitVector
                ? sourceVector
                : sourceVector.DivideByENorm();

        var ek = processor.VectorTerm(k);

        var vk1 = 1 + (targetAxis.IsPositive ? v.Scalar(k) : -v.Scalar(k));
        var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

        return RGaPureRotor<T>.Create(
            (vk1 / 2).Sqrt().ScalarValue,
            vOpAxis / (vk1 * 2).Sqrt()
        );
    }

    /// <summary>
    /// Create a simple rotor from an angle and a 2-blade
    /// </summary>
    /// <param name="rotationAngle"></param>
    /// <param name="rotationBlade"></param>
    /// <returns></returns>
    public static RGaPureRotor<T> CreatePureRotor<T>(this RGaBivector<T> rotationBlade, LinPolarAngle<T> rotationAngle)
    {
        var (cosHalfAngle, sinHalfAngle) = 
            rotationAngle.HalfPolarAngle();

        var rotationBladeScalar =
            sinHalfAngle / (-rotationBlade.ESp(rotationBlade)).Sqrt();

        return RGaPureRotor<T>.Create(
            cosHalfAngle,
            rotationBladeScalar * rotationBlade
        );
    }
    
    /// <summary>
    /// Create a simple rotor from an angle and a 2-blade
    /// </summary>
    /// <param name="rotationAngle"></param>
    /// <param name="rotationBlade"></param>
    /// <returns></returns>
    public static RGaPureRotor<T> CreatePureRotor<T>(this RGaBivector<T> rotationBlade, LinAngle<T> rotationAngle)
    {
        var (cosHalfAngle, sinHalfAngle) = rotationAngle.HalfPolarAngle();

        var rotationBladeScalar =
            sinHalfAngle / (-rotationBlade.ESp(rotationBlade)).Sqrt();

        return RGaPureRotor<T>.Create(
            cosHalfAngle,
            rotationBladeScalar * rotationBlade
        );
    }

    public static RGaPureRotorsSequence<T> CreatePureRotorSequence<T>(this RGaVector<T> sourceVector1, RGaVector<T> sourceVector2, RGaVector<T> targetVector1, RGaVector<T> targetVector2, bool assumeUnitVectors = false)
    {
        var rotor1 =
            sourceVector1.CreatePureRotor(
                targetVector1,
                assumeUnitVectors
            );

        var rotor2 =
            rotor1.OmMap(sourceVector2).CreatePureRotor(
                targetVector2,
                assumeUnitVectors
            );

        //var rotor = 
        //    rotor2.Multivector.EGp(rotor1.Multivector);

        //var (scalar, bivector) = rotor.GetScalarBivectorParts();

        return RGaPureRotorsSequence<T>.Create(rotor1, rotor2);
    }

    public static RGaPureRotor<T> CreatePureRotor<T>(this RGaVector<T> inputVector1, RGaVector<T> inputVector2, RGaVector<T> rotatedVector1, RGaVector<T> rotatedVector2, int baseSpaceDimensions)
    {
        var inputFrame = RGaVectorFrameSpecs
            .CreateLinearlyIndependentSpecs()
            .CreateVectorFrame(
                inputVector1,
                inputVector2
            );

        var rotatedFrame = RGaVectorFrameSpecs
            .CreateLinearlyIndependentSpecs()
            .CreateVectorFrame(
                rotatedVector1,
                rotatedVector2
            );

        var rotor = RGaPureRotorsSequence<T>.CreateFromEuclideanFrames(
            baseSpaceDimensions,
            inputFrame,
            rotatedFrame
        ).GetFinalRotor();

        var (scalar, bivector) = rotor.Multivector.GetScalarBivectorParts();

        return RGaPureRotor<T>.Create(scalar.ScalarValue, bivector);
    }

    /// <summary>
    /// Construct a rotor in the e_i-e_j plane with the given angle where i is less than j
    /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
    /// </summary>
    /// <param name="processor"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="rotationAngle"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureRotor<T> CreateGivensRotor<T>(this RGaProcessor<T> processor, int i, int j, LinPolarAngle<T> rotationAngle)
    {
        Debug.Assert(i >= 0 && j != i);

        var (cosHalfAngle, sinHalfAngle) = 
            rotationAngle.HalfPolarAngle();

        return RGaPureRotor<T>.Create(
            cosHalfAngle,
            processor.BivectorTerm(i, j, sinHalfAngle)
        );
    }

    /// <summary>
    /// Construct a scaled rotor in the e_i-e_j plane with the given angle where i is less than j
    /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
    /// </summary>
    /// <param name="processor"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="rotationAngle"></param>
    /// <param name="scalingFactor"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledPureRotor<T> CreateScaledGivensRotor<T>(this RGaProcessor<T> processor, int i, int j, LinPolarAngle<T> rotationAngle, T scalingFactor)
    {
        Debug.Assert(i >= 0 && j != i);

        var scalarProcessor = processor.ScalarProcessor;

        var (cosHalfAngle, sinHalfAngle) = 
            rotationAngle.HalfPolarAngle();

        var s = scalarProcessor.Sqrt(scalingFactor);
        var scalarPart = scalarProcessor.Times(s, cosHalfAngle);
        var bivectorPart = s * processor.BivectorTerm(i, j, sinHalfAngle);

        return RGaScaledPureRotor<T>.Create(scalarPart, bivectorPart);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledPureRotor<T> CreateScaledPureRotor<T>(this RGaPureRotor<T> rotor, T scalingFactor)
    {
        var processor = rotor.ScalarProcessor;

        var s = processor.Sqrt(scalingFactor);
        var scalarPart = s * rotor.Multivector.Scalar();
        var bivectorPart = s * rotor.Multivector.GetBivectorPart();

        return RGaScaledPureRotor<T>.Create(
            scalarPart.ScalarValue,
            bivectorPart
        );
    }

    /// <summary>
    /// Create a scaled pure rotor in 2D Euclidean space directly using its scalar components
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="processor"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledPureRotor<T> CreateScaledPureRotor2D<T>(this RGaProcessor<T> processor, float r0, float r12)
    {
        return RGaScaledPureRotor<T>.Create(
            processor
                .CreateComposer()
                .SetTerm(0, r0)
                .SetTerm(3, r12)
                .GetMultivector()
        );
    }

    /// <summary>
    /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="processor"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledPureRotor<T> CreateScaledPureRotor2D<T>(this RGaProcessor<T> processor, double r0, double r12)
    {
        return RGaScaledPureRotor<T>.Create(
            processor
                .CreateComposer()
                .SetTerm(0, r0)
                .SetTerm(3, r12)
                .GetMultivector()
        );
    }

    /// <summary>
    /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="processor"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledPureRotor<T> CreateScaledPureRotor2D<T>(this RGaProcessor<T> processor, string r0, string r12)
    {
        return RGaScaledPureRotor<T>.Create(
            processor
                .CreateComposer()
                .SetTerm(0, r0)
                .SetTerm(3, r12)
                .GetMultivector()
        );
    }
        
    /// <summary>
    /// Create a scaled pure rotor in 2D Euclidean space directly using its scalar components
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="processor"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledPureRotor<T> CreateScaledPureRotor2D<T>(this RGaProcessor<T> processor, T r0, T r12)
    {
        return RGaScaledPureRotor<T>.Create(
            processor
                .CreateComposer()
                .SetTerm(0, r0)
                .SetTerm(3, r12)
                .GetMultivector()
        );
    }

    /// <summary>
    /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="processor"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <param name="r13"></param>
    /// <param name="r23"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledPureRotor<T> CreateScaledPureRotor3D<T>(this RGaProcessor<T> processor, float r0, float r12, float r13, float r23)
    {
        return RGaScaledPureRotor<T>.Create(
            processor
                .CreateComposer()
                .SetTerm(0, r0)
                .SetTerm(3, r12)
                .SetTerm(5, r13)
                .SetTerm(6, r23)
                .GetMultivector()
        );
    }

    /// <summary>
    /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="processor"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <param name="r13"></param>
    /// <param name="r23"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledPureRotor<T> CreateScaledPureRotor3D<T>(this RGaProcessor<T> processor, double r0, double r12, double r13, double r23)
    {
        return RGaScaledPureRotor<T>.Create(
            processor
                .CreateComposer()
                .SetTerm(0, r0)
                .SetTerm(3, r12)
                .SetTerm(5, r13)
                .SetTerm(6, r23)
                .GetMultivector()
        );
    }

    /// <summary>
    /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="processor"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <param name="r13"></param>
    /// <param name="r23"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledPureRotor<T> CreateScaledPureRotor3D<T>(this RGaProcessor<T> processor, string r0, string r12, string r13, string r23)
    {
        return RGaScaledPureRotor<T>.Create(
            processor
                .CreateComposer()
                .SetTerm(0, r0)
                .SetTerm(3, r12)
                .SetTerm(5, r13)
                .SetTerm(6, r23)
                .GetMultivector()
        );
    }
        
    /// <summary>
    /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="processor"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <param name="r13"></param>
    /// <param name="r23"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledPureRotor<T> CreateScaledPureRotor3D<T>(this RGaProcessor<T> processor, T r0, T r12, T r13, T r23)
    {
        return RGaScaledPureRotor<T>.Create(
            processor
                .CreateComposer()
                .SetTerm(0, r0)
                .SetTerm(3, r12)
                .SetTerm(5, r13)
                .SetTerm(6, r23)
                .GetMultivector()
        );
    }
    
    /// <summary>
    /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="processor"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <param name="r13"></param>
    /// <param name="r23"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledPureRotor<T> CreateScaledPureRotor3D<T>(this RGaProcessor<T> processor, IScalar<T> r0, IScalar<T> r12, IScalar<T> r13, IScalar<T> r23)
    {
        return RGaScaledPureRotor<T>.Create(
            processor
                .CreateComposer()
                .SetTerm(0, r0)
                .SetTerm(3, r12)
                .SetTerm(5, r13)
                .SetTerm(6, r23)
                .GetMultivector()
        );
    }


    /// <summary>
    /// See paper "Calculation of Elements of Spin Groups Using Method
    /// of Averaging in Clifford’s Geometric Algebra"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourceFrame"></param>
    /// <param name="targetFrame"></param>
    /// <returns></returns>
    public static RGaRotor<T> CreateRotorToFrame<T>(this RGaBasisVectorFrame<T> sourceFrame, RGaBasisVectorFrame<T> targetFrame)
    {
        var processor = sourceFrame.Processor;

        var mvFrame1 = sourceFrame.CreateBasisKVectorFrame().MapAsBasisUsing(mv => mv.Inverse());
        var mvFrame2 = targetFrame.CreateBasisKVectorFrame();

        var rotorMultivector =
            mvFrame2
                .Zip(mvFrame1)
                .Select(vectorPair => vectorPair.First.Gp(vectorPair.Second))
                .Aggregate(
                    (RGaMultivector<T>) processor.MultivectorZero,
                    (mv1, mv2) => mv1.Add(mv2)
                );

        rotorMultivector /= rotorMultivector.Sp(rotorMultivector.Reverse()).Sqrt();

        return RGaRotor<T>.Create(rotorMultivector);
    }
}