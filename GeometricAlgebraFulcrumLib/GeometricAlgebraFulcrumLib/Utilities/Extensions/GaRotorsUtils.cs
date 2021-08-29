using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Frames;
using GeometricAlgebraFulcrumLib.Geometry.Rotors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaRotorsUtils
    {
        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaPureRotor<T> CreateIdentityRotor<T>(this IGaProcessor<T> processor)
        {
            var rotorMultivector = 
                processor.CreateStorageBasisScalar();

            return new GaPureRotor<T>(
                processor,
                rotorMultivector,
                rotorMultivector
            );
        }

        /// <summary>
        /// Create a pure rotor from a 2-blade, the signature of the blade is computed automatically
        /// using the given processor which must be of numerical type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <param name="blade"></param>
        /// <returns></returns>
        public static GaPureRotor<T> CreateRotor<T>(this IGaProcessor<T> processor, IGaBivectorStorage<T> blade)
        {
            if (!processor.IsNumeric)
                throw new InvalidOperationException();

            var bladeSignature = processor.Sp(blade);

            if (processor.IsNearZero(bladeSignature))
                return new GaPureRotor<T>(
                    processor,
                    processor.ScalarOne, 
                    blade
                );

            if (processor.IsNegative(bladeSignature))
            {
                var alpha = processor.Sqrt(processor.Negative(processor.Sp(blade)));
                var scalar = processor.Cos(alpha);
                var bivector = processor.Times(processor.Divide(processor.Sin(alpha), alpha), blade);

                return new GaPureRotor<T>(
                    processor,
                    scalar, 
                    bivector
                );
            }
            else
            {
                var alpha = processor.Sqrt(processor.Sp(blade));
                var scalar = processor.Cosh(alpha);
                var bivector = processor.Times(processor.Divide(processor.Sinh(alpha), alpha), blade);

                return new GaPureRotor<T>(
                    processor,
                    scalar, 
                    bivector
                );
            }
        }

        /// <summary>
        /// Create a pure rotor from a 2-blade, the signature of the blade is given by the user
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <param name="blade"></param>
        /// <param name="bladeSignatureKind"></param>
        /// <returns></returns>
        public static GaPureRotor<T> CreateRotor<T>(this IGaProcessor<T> processor, IGaBivectorStorage<T> blade, GaBladeSignatureKind bladeSignatureKind)
        {
            if (bladeSignatureKind == GaBladeSignatureKind.Zero) 
                return new GaPureRotor<T>(
                    processor,
                    processor.ScalarOne, 
                    blade
                );

            if (bladeSignatureKind == GaBladeSignatureKind.Negative)
            {
                var alpha = processor.Sqrt(processor.Negative(processor.Sp(blade)));
                var scalar = processor.Cos(alpha);
                var bivector = processor.Times(processor.Divide(processor.Sin(alpha), alpha), blade);

                return new GaPureRotor<T>(
                    processor,
                    scalar, 
                    bivector
                );
            }
            else
            {
                var alpha = processor.Sqrt(processor.Sp(blade));
                var scalar = processor.Cosh(alpha);
                var bivector = processor.Times(processor.Divide(processor.Sinh(alpha), alpha), blade);

                return new GaPureRotor<T>(
                    processor,
                    scalar, 
                    bivector
                );
            }
        }

        /// <summary>
        /// Create a pure rotor from its scalar and bivector parts
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <param name="scalarPart"></param>
        /// <param name="bivectorPart"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaPureRotor<T> CreatePureRotor<T>(this IGaProcessor<T> processor, T scalarPart, IGaBivectorStorage<T> bivectorPart)
        {
            return new GaPureRotor<T>(
                processor, 
                scalarPart, 
                bivectorPart
            );
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source vector into the target vector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <param name="sourceVector"></param>
        /// <param name="targetVector"></param>
        /// <param name="assumeUnitVectors"></param>
        /// <returns></returns>
        public static GaPureRotor<T> CreateEuclideanRotor<T>(this IGaProcessor<T> processor, IGaVectorStorage<T> sourceVector, IGaVectorStorage<T> targetVector, bool assumeUnitVectors = false)
        {
            var u = sourceVector.CreateGaVector(processor);
            var v = targetVector.CreateGaVector(processor);

            var cosAngle = 
                assumeUnitVectors
                    ? v.ESp(u)
                    : v.ESp(u) / (v.ENormSquared() * u.ENormSquared()).Sqrt();

            if (cosAngle.IsOne)
                return CreateIdentityRotor(processor);
            
            //TODO: Handle the case for cosAngle == -1
            if (cosAngle.IsMinusOne)
                throw new InvalidOperationException();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var rotationBlade = v.Op(u);
            var unitRotationBlade = 
                rotationBlade / (-rotationBlade.ESp()).Sqrt();

            var scalarPart = cosHalfAngle.ScalarValue;
            var bivectorPart = (sinHalfAngle * unitRotationBlade).BivectorStorage;

            return new GaPureRotor<T>(
                processor, 
                scalarPart,
                bivectorPart
            );
        }

        public static GaPureRotor<T> CreateEuclideanRotor<T>(this IGaProcessor<T> processor, ulong sourceBasisVectorIndex, IGaVectorStorage<T> targetVector, bool assumeUnitVector = false)
        {
            var k = sourceBasisVectorIndex;
            var v = processor.CreateGaVector(targetVector);
            var ek = processor.CreateGaVector(k);
            var vk = v[k];
            var vk1 = 1 + vk;

            var scalarPart = (vk1 / 2).Sqrt();
            var bivectorPart = (v - vk * ek).Op(ek) / (2 * vk1).Sqrt();

            return new GaPureRotor<T>(
                processor,
                scalarPart,
                bivectorPart.BivectorStorage
            );
        }

        /// <summary>
        /// Create a simple rotor from an angle and a 2-blade
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="rotationAngle"></param>
        /// <param name="rotationBlade"></param>
        /// <returns></returns>
        public static GaPureRotor<T> CreateEuclideanRotor<T>(this IGaProcessor<T> processor, T rotationAngle, IGaBivectorStorage<T> rotationBlade)
        {
            var halfRotationAngle = processor.Divide(rotationAngle, processor.GetScalarFromInteger(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);

            var rotationBladeScalar =
                processor.Divide(
                    sinHalfAngle,
                    processor.SqrtOfNegative(processor.GetTermScalar(processor.EGp(rotationBlade), 0))
                );

            return new GaPureRotor<T>(
                processor, 
                cosHalfAngle, 
                processor.Times(rotationBladeScalar, rotationBlade)
            );
        }

        public static GaPureRotor<T> CreateEuclideanRotor<T>(this IGaProcessor<T> processor, IGaVectorStorage<T> sourceVector1, IGaVectorStorage<T> sourceVector2, IGaVectorStorage<T> targetVector1, IGaVectorStorage<T> targetVector2, bool assumeUnitVectors = false)
        {
            var rotor1 = 
                processor.CreateEuclideanRotor(
                    sourceVector1, 
                    targetVector1,
                    assumeUnitVectors
                );

            var rotor2 = 
                processor.CreateEuclideanRotor(
                    rotor1.MapVector(sourceVector2), 
                    targetVector2,
                    assumeUnitVectors
                );

            var rotor = 
                processor.EGp(rotor2.Multivector, rotor1.Multivector);

            return new GaPureRotor<T>(
                processor, 
                rotor,
                processor.Reverse(rotor)
            );
        }
        
        public static GaPureRotor<T> CreateEuclideanSimpleRotor<T>(this IGaProcessor<T> processor, uint baseSpaceDimensions, IGaVectorStorage<T> inputVector1, IGaVectorStorage<T> inputVector2, IGaVectorStorage<T> rotatedVector1, IGaVectorStorage<T> rotatedVector2)
        {
            var inputFrame = processor.CreateVectorsFrame(
                GaVectorsFrameKind.LinearlyIndependent,
                inputVector1, 
                inputVector2
            );

            var rotatedFrame = processor.CreateVectorsFrame(
                GaVectorsFrameKind.LinearlyIndependent,
                rotatedVector1, 
                rotatedVector2
            );

            var rotor = GaPureRotorsSequence<T>.CreateFromFrames(
                baseSpaceDimensions, 
                inputFrame, 
                rotatedFrame
            ).GetFinalRotor();

            return new GaPureRotor<T>(
                processor,
                rotor.Multivector,
                rotor.MultivectorReverse
            );
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
        public static GaPureRotor<T> CreateEuclideanGivensRotor<T>(this IGaProcessor<T> processor, int i, int j, T rotationAngle)
        {
            Debug.Assert(i >= 0 && j != i);

            var halfRotationAngle = processor.Divide(rotationAngle, processor.GetScalarFromInteger(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);

            return new GaPureRotor<T>(
                processor,
                cosHalfAngle,
                processor.CreateBivectorStorage(i, j, sinHalfAngle)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorsFrame<T> MapVectorsFrame<T>(this IGaRotor<T> rotor, GaVectorsFrame<T> frame)
        {
            return new GaVectorsFrame<T>(
                frame.Processor,
                frame.FrameKind,
                frame.Select(rotor.MapVector)
            );
        }


    }
}