using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Frames;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors
{
    public static class XGaRotorComposerUtils
    {
        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaPureRotor<T> CreateIdentityRotor<T>(this XGaProcessor<T> processor)
        {
            return XGaPureRotor<T>.Create(
                processor.ScalarProcessor.ScalarOne,
                processor.CreateZeroBivector()
            );
        }

        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScaledPureRotor<T> CreateScaledIdentityRotor<T>(this XGaProcessor<T> processor)
        {
            return XGaScaledPureRotor<T>.Create(
                processor.ScalarProcessor.ScalarOne,
                processor.CreateZeroBivector()
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
        public static XGaScaledPureRotor<T> CreateScaledIdentityRotor<T>(this XGaProcessor<T> processor, T scalingFactor)
        {
            return XGaScaledPureRotor<T>.Create(
                scalingFactor,
                processor.CreateZeroBivector()
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
        public static XGaPureRotor<T> CreatePureRotor<T>(this XGaBivector<T> blade)
        {
            var processor = blade.ScalarProcessor;

            if (!processor.IsNumeric)
                throw new InvalidOperationException();

            var bladeSignature = blade.SpSquared();

            if (bladeSignature.IsNearZero())
                return XGaPureRotor<T>.Create(
                    processor.ScalarOne,
                    blade
                );

            if (bladeSignature.Scalar().IsNegative())
            {
                var alpha = (-bladeSignature).Sqrt();
                var scalar = alpha.Cos().ScalarValue();
                var bivector = alpha.Sin() / alpha * blade;

                return XGaPureRotor<T>.Create(
                    scalar,
                    bivector
                );
            }
            else
            {
                var alpha = bladeSignature.Sqrt();
                var scalar = alpha.Cosh().ScalarValue();
                var bivector = alpha.Sinh() / alpha * blade;

                return XGaPureRotor<T>.Create(
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
        public static XGaPureRotor<T> CreatePureRotor<T>(this XGaBivector<T> blade, IntegerSign bladeSignatureSign)
        {
            var processor = blade.ScalarProcessor;

            if (bladeSignatureSign.IsZero)
                return XGaPureRotor<T>.Create(
                    processor.ScalarOne,
                    blade
                );

            var bladeSignature = blade.SpSquared();

            if (bladeSignatureSign.IsNegative)
            {
                var alpha = (-bladeSignature).Sqrt();
                var scalar = alpha.Cos().ScalarValue();
                var bivector = alpha.Sin() / alpha * blade;

                return XGaPureRotor<T>.Create(
                    scalar,
                    bivector
                );
            }
            else
            {
                var alpha = bladeSignature.Sqrt();
                var scalar = alpha.Cosh().ScalarValue();
                var bivector = alpha.Sinh() / alpha * blade;

                return XGaPureRotor<T>.Create(
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
        public static XGaPureRotor<T> CreatePureRotor<T>(this XGaMultivector<T> rotorMv)
        {
            return XGaPureRotor<T>.Create(
                rotorMv.GetScalarPart().ScalarValue(),
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
        public static XGaPureRotor<T> CreatePureRotor<T>(this XGaVector<T> sourceVector, XGaVector<T> targetVector, bool assumeUnitVectors = false)
        {
            var processor = sourceVector.Processor;

            var cosAngle =
                assumeUnitVectors
                    ? targetVector.ESp(sourceVector)
                    : targetVector.ESp(sourceVector) / (targetVector.ENormSquared() * sourceVector.ENormSquared()).Sqrt();

            if (cosAngle.IsOne)
                return processor.CreateIdentityRotor();
            
            var rotationBlade = 
                cosAngle.IsMinusOne
                    ? throw new InvalidOperationException()//sourceVector.GetNormalVector().Op(sourceVector)
                    : targetVector.Op(sourceVector);
                
            var unitRotationBlade =
                rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();
            
            var scalarPart = cosHalfAngle.ScalarValue();
            var bivectorPart = sinHalfAngle * unitRotationBlade;

            return XGaPureRotor<T>.Create(
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
        public static XGaScaledPureRotor<T> CreateScaledPureRotor<T>(this XGaVector<T> sourceVector, XGaVector<T> targetVector)
        {
            var processor = sourceVector.Processor;

            var uNorm = sourceVector.ENorm();
            var vNorm = targetVector.ENorm();
            var scalingFactor = (vNorm / uNorm).Sqrt().ScalarValue();
            var cosAngle = targetVector.ESp(sourceVector) / (uNorm * vNorm);

            if (cosAngle.IsOne)
                return XGaScaledPureRotor<T>.Create(processor, scalingFactor);
            
            var rotationBlade = 
                cosAngle.IsMinusOne
                    ? throw new InvalidOperationException()//sourceVector.GetNormalVector().Op(sourceVector)
                    : targetVector.Op(sourceVector);
                
            var unitRotationBlade =
                rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();
            
            var scalarPart =
                scalingFactor * cosHalfAngle;

            var bivectorPart =
                scalingFactor * sinHalfAngle * unitRotationBlade;

            return XGaScaledPureRotor<T>.Create(
                scalarPart.ScalarValue(),
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
        public static XGaPureRotor<T> CreateParametricPureRotor3D<T>(this XGaVector<T> sourceVector, XGaVector<T> targetVector, T angleTheta)
        {
            var processor = sourceVector.Processor;
            var scalarProcessor = sourceVector.ScalarProcessor;

            // Compute inverse of 3D pseudo-scalar = -e123
            var pseudoScalarInverse =
                processor.CreatePseudoScalarInverse(3);

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

            var sinAngleThetaSquare = scalarProcessor.Square(scalarProcessor.Sin(angleTheta));

            // Define parametric angle of rotation
            var rotorAngle =
                (1 + 2 * (cosAngle0 - 1) / (2 - sinAngleThetaSquare * (cosAngle0 + 1))).ArcCos().ScalarValue();

            // Math.Acos(1 + 2 * (cosAngle0 - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (cosAngle0 + 1)));

            // Return the final rotor taking v1 into v2
            return rotorBlade.CreatePureRotor(rotorAngle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScaledPureRotor<T> CreateScaledParametricPureRotor3D<T>(this XGaVector<T> sourceVector, XGaVector<T> targetVector, T angleTheta, T scalingFactor)
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
        public static XGaScaledPureRotor<T> CreateScaledPureRotorFromAxis<T>(this XGaVector<T> targetVector, LinSignedBasisVector sourceAxis, bool assumeUnitVector = false)
        {
            var metric = targetVector.Metric;
            var processor = targetVector.Processor;

            var k = sourceAxis.Index;
            var vNorm = assumeUnitVector
                ? processor.ScalarProcessor.ScalarOne
                : targetVector.ENorm().ScalarValue();

            var ek = processor.CreateTermVector(k);

            var vk1 = vNorm + (sourceAxis.IsPositive ? targetVector.Scalar(k) : -targetVector.Scalar(k));
            var vOpAxis = sourceAxis.IsPositive ? targetVector.Op(ek) : ek.Op(targetVector);

            return XGaScaledPureRotor<T>.Create(
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
        public static XGaScaledPureRotor<T> CreateScaledPureRotorToAxis<T>(this XGaVector<T> sourceVector, LinSignedBasisVector targetAxis, bool assumeUnitVector = false)
        {
            var processor = sourceVector.Processor;
            var scalarProcessor = sourceVector.ScalarProcessor;

            var k = targetAxis.Index;
            var vNorm =
                assumeUnitVector
                    ? scalarProcessor.ScalarOne
                    : sourceVector.ENorm().ScalarValue();

            var vNorm2 =
                assumeUnitVector
                    ? scalarProcessor.ScalarTwo
                    : scalarProcessor.Times(2, sourceVector.ENormSquared().ScalarValue());

            var ek = processor.CreateTermVector(k);

            var vk1 = vNorm + (targetAxis.IsPositive ? sourceVector.Scalar(k) : -sourceVector.Scalar(k));
            var vOpAxis = targetAxis.IsPositive ? ek.Op(sourceVector) : sourceVector.Op(ek);

            return XGaScaledPureRotor<T>.Create(
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
        public static XGaPureRotor<T> CreatePureRotorFromAxis<T>(this XGaVector<T> targetVector, LinSignedBasisVector sourceAxis, bool assumeUnitVector = false)
        {
            var processor = targetVector.Processor;

            var k = sourceAxis.Index;

            var v =
                assumeUnitVector
                    ? targetVector
                    : targetVector.DivideByENorm();

            var ek = processor.CreateTermVector(k);

            var vk1 = 1 + (sourceAxis.IsPositive ? v.Scalar(k) : -v.Scalar(k));
            var vOpAxis = sourceAxis.IsPositive ? v.Op(ek) : ek.Op(v);

            return XGaPureRotor<T>.Create(
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
        public static XGaPureRotor<T> CreatePureRotorToAxis<T>(this XGaVector<T> sourceVector, LinSignedBasisVector targetAxis, bool assumeUnitVector = false)
        {
            var metric = sourceVector.Metric;
            var processor = sourceVector.Processor;

            var k = targetAxis.Index;

            var v =
                assumeUnitVector
                    ? sourceVector
                    : sourceVector.DivideByENorm();

            var ek = processor.CreateTermVector(k);

            var vk1 = 1 + (targetAxis.IsPositive ? v.Scalar(k) : -v.Scalar(k));
            var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

            return XGaPureRotor<T>.Create(
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
        public static XGaPureRotor<T> CreatePureRotor<T>(this LinSignedBasisVector sourceAxis, XGaVector<T> targetVector, bool assumeUnitVector = false)
        {
            var metric = targetVector.Metric;
            var processor = targetVector.Processor;

            var k = sourceAxis.Index;

            var v =
                assumeUnitVector
                    ? targetVector
                    : targetVector.DivideByENorm();

            var ek = processor.CreateTermVector(k);

            var vk1 = 1 + (sourceAxis.IsPositive ? v.Scalar(k) : -v.Scalar(k));
            var vOpAxis = sourceAxis.IsPositive ? v.Op(ek) : ek.Op(v);

            return XGaPureRotor<T>.Create(
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
        public static XGaPureRotor<T> CreatePureRotor<T>(this XGaVector<T> sourceVector, LinSignedBasisVector targetAxis, bool assumeUnitVector = false)
        {
            var metric = sourceVector.Metric;
            var processor = sourceVector.Processor;

            var k = targetAxis.Index;

            var v =
                assumeUnitVector
                    ? sourceVector
                    : sourceVector.DivideByENorm();

            var ek = processor.CreateTermVector(k);

            var vk1 = 1 + (targetAxis.IsPositive ? v.Scalar(k) : -v.Scalar(k));
            var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

            return XGaPureRotor<T>.Create(
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
        public static XGaPureRotor<T> CreatePureRotor<T>(this XGaBivector<T> rotationBlade, T rotationAngle)
        {
            var processor = rotationBlade.ScalarProcessor;

            var halfRotationAngle = processor.Divide(rotationAngle, processor.GetScalarFromNumber(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);

            var rotationBladeScalar =
                sinHalfAngle / (-rotationBlade.ESp(rotationBlade)).Sqrt();

            return XGaPureRotor<T>.Create(
                cosHalfAngle,
                rotationBladeScalar * rotationBlade
            );
        }

        public static XGaPureRotorsSequence<T> CreatePureRotorSequence<T>(this XGaVector<T> sourceVector1, XGaVector<T> sourceVector2, XGaVector<T> targetVector1, XGaVector<T> targetVector2, bool assumeUnitVectors = false)
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

            return XGaPureRotorsSequence<T>.Create(rotor1, rotor2);
        }

        public static XGaPureRotor<T> CreatePureRotor<T>(this XGaVector<T> inputVector1, XGaVector<T> inputVector2, XGaVector<T> rotatedVector1, XGaVector<T> rotatedVector2, int baseSpaceDimensions)
        {
            var inputFrame = XGaVectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(
                    inputVector1,
                    inputVector2
                );

            var rotatedFrame = XGaVectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(
                    rotatedVector1,
                    rotatedVector2
                );

            var rotor = XGaPureRotorsSequence<T>.CreateFromEuclideanFrames(
                baseSpaceDimensions,
                inputFrame,
                rotatedFrame
            ).GetFinalRotor();

            var (scalar, bivector) = rotor.Multivector.GetScalarBivectorParts();

            return XGaPureRotor<T>.Create(scalar.ScalarValue(), bivector);
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
        public static XGaPureRotor<T> CreateGivensRotor<T>(this XGaProcessor<T> processor, int i, int j, T rotationAngle)
        {
            Debug.Assert(i >= 0 && j != i);
            
            var scalarProcessor = processor.ScalarProcessor;

            var halfRotationAngle = scalarProcessor.Divide(rotationAngle, scalarProcessor.GetScalarFromNumber(2));
            var cosHalfAngle = scalarProcessor.Cos(halfRotationAngle);
            var sinHalfAngle = scalarProcessor.Sin(halfRotationAngle);

            return XGaPureRotor<T>.Create(
                cosHalfAngle,
                processor.CreateTermBivector(i, j, sinHalfAngle)
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
        public static XGaScaledPureRotor<T> CreateScaledGivensRotor<T>(this XGaProcessor<T> processor, int i, int j, T rotationAngle, T scalingFactor)
        {
            Debug.Assert(i >= 0 && j != i);

            var scalarProcessor = processor.ScalarProcessor;

            var halfRotationAngle = scalarProcessor.Divide(rotationAngle, scalarProcessor.GetScalarFromNumber(2));
            var cosHalfAngle = scalarProcessor.Cos(halfRotationAngle);
            var sinHalfAngle = scalarProcessor.Sin(halfRotationAngle);
            var s = scalarProcessor.Sqrt(scalingFactor);
            var scalarPart = scalarProcessor.Times(s, cosHalfAngle);
            var bivectorPart = s * processor.CreateTermBivector(i, j, sinHalfAngle);

            return XGaScaledPureRotor<T>.Create(scalarPart, bivectorPart);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScaledPureRotor<T> CreateScaledPureRotor<T>(this XGaPureRotor<T> rotor, T scalingFactor)
        {
            var processor = rotor.ScalarProcessor;

            var s = processor.Sqrt(scalingFactor);
            var scalarPart = s * rotor.Multivector.Scalar();
            var bivectorPart = s * rotor.Multivector.GetBivectorPart();

            return XGaScaledPureRotor<T>.Create(
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
        public static XGaScaledPureRotor<T> CreateScaledPureRotor2D<T>(this XGaProcessor<T> processor, float r0, float r12)
        {
            return XGaScaledPureRotor<T>.Create(
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
        public static XGaScaledPureRotor<T> CreateScaledPureRotor2D<T>(this XGaProcessor<T> processor, double r0, double r12)
        {
            return XGaScaledPureRotor<T>.Create(
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
        public static XGaScaledPureRotor<T> CreateScaledPureRotor2D<T>(this XGaProcessor<T> processor, string r0, string r12)
        {
            return XGaScaledPureRotor<T>.Create(
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
        public static XGaScaledPureRotor<T> CreateScaledPureRotor2D<T>(this XGaProcessor<T> processor, T r0, T r12)
        {
            return XGaScaledPureRotor<T>.Create(
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
        public static XGaScaledPureRotor<T> CreateScaledPureRotor3D<T>(this XGaProcessor<T> processor, float r0, float r12, float r13, float r23)
        {
            return XGaScaledPureRotor<T>.Create(
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
        public static XGaScaledPureRotor<T> CreateScaledPureRotor3D<T>(this XGaProcessor<T> processor, double r0, double r12, double r13, double r23)
        {
            return XGaScaledPureRotor<T>.Create(
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
        public static XGaScaledPureRotor<T> CreateScaledPureRotor3D<T>(this XGaProcessor<T> processor, string r0, string r12, string r13, string r23)
        {
            return XGaScaledPureRotor<T>.Create(
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
        public static XGaScaledPureRotor<T> CreateScaledPureRotor3D<T>(this XGaProcessor<T> processor, T r0, T r12, T r13, T r23)
        {
            return XGaScaledPureRotor<T>.Create(
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
        public static XGaRotor<T> CreateRotorToFrame<T>(this XGaBasisVectorFrame<T> sourceFrame, XGaBasisVectorFrame<T> targetFrame)
        {
            var processor = sourceFrame.Processor;

            var mvFrame1 = sourceFrame.CreateBasisKVectorFrame().MapAsBasisUsing(mv => mv.Inverse());
            var mvFrame2 = targetFrame.CreateBasisKVectorFrame();

            var rotorMultivector =
                mvFrame2
                    .Zip(mvFrame1)
                    .Select(vectorPair => vectorPair.First.Gp(vectorPair.Second))
                    .Aggregate(
                        (XGaMultivector<T>) processor.CreateZeroMultivector(),
                        (mv1, mv2) => mv1.Add(mv2)
                    );

            rotorMultivector /= rotorMultivector.Sp(rotorMultivector.Reverse()).Sqrt().ScalarValue();

            return XGaRotor<T>.Create(rotorMultivector);
        }

        
        public static XGaPureRotor<T> CreateSimpleKirchhoffRotor<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
        {
            var v1 = 
                processor.CreateSymmetricUnitVector(vSpaceDimensions);

            var v2 = processor.CreateTermVector(
                vSpaceDimensions - 1
            );

            return v2.CreatePureRotor(v1);
        }
    }
}