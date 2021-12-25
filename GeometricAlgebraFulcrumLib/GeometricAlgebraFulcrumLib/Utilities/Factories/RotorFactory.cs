using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Geometry.Frames;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class RotorFactory
    {
        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotor<T> CreateIdentityRotor<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            return new PureRotor<T>(
                processor,
                processor.ScalarOne,
                KVectorStorage<T>.ZeroBivector
            );
        }
        
        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScaledPureRotor<T> CreateScaledIdentityRotor<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            return new ScaledPureRotor<T>(
                processor,
                processor.ScalarOne,
                KVectorStorage<T>.ZeroBivector
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
        public static ScaledPureRotor<T> CreateScaledIdentityRotor<T>(this IGeometricAlgebraProcessor<T> processor, T scalingFactor)
        {
            return new ScaledPureRotor<T>(
                processor,
                scalingFactor,
                KVectorStorage<T>.ZeroBivector
            );
        }

        /// <summary>
        /// Create a pure rotor from a 2-blade, the signature of the blade
        /// is computed automatically using the given processor which must
        /// be of numerical type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <param name="blade"></param>
        /// <returns></returns>
        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraProcessor<T> processor, BivectorStorage<T> blade)
        {
            if (!processor.IsNumeric)
                throw new InvalidOperationException();

            var bladeSignature = processor.Sp(blade);

            if (processor.IsNearZero(bladeSignature))
                return new PureRotor<T>(
                    processor,
                    processor.ScalarOne, 
                    blade
                );

            if (processor.IsNegative(bladeSignature))
            {
                var alpha = processor.Sqrt(processor.Negative(processor.Sp(blade)));
                var scalar = processor.Cos(alpha);
                var bivector = processor.Times(processor.Divide(processor.Sin(alpha), alpha), blade);

                return new PureRotor<T>(
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

                return new PureRotor<T>(
                    processor,
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
        /// <param name="processor"></param>
        /// <param name="blade"></param>
        /// <param name="bladeSignatureKind"></param>
        /// <returns></returns>
        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraProcessor<T> processor, BivectorStorage<T> blade, BladeSignatureKind bladeSignatureKind)
        {
            if (bladeSignatureKind == BladeSignatureKind.Zero) 
                return new PureRotor<T>(
                    processor,
                    processor.ScalarOne, 
                    blade
                );

            if (bladeSignatureKind == BladeSignatureKind.Negative)
            {
                var alpha = processor.Sqrt(processor.Negative(processor.Sp(blade)));
                var scalar = processor.Cos(alpha);
                var bivector = processor.Times(processor.Divide(processor.Sin(alpha), alpha), blade);

                return new PureRotor<T>(
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

                return new PureRotor<T>(
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
        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraProcessor<T> processor, T scalarPart, BivectorStorage<T> bivectorPart)
        {
            return new PureRotor<T>(
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
        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, VectorStorage<T> sourceVector, VectorStorage<T> targetVector, bool assumeUnitVectors = false)
        {
            var u = sourceVector.CreateVector(processor);
            var v = targetVector.CreateVector(processor);

            var cosAngle = 
                assumeUnitVectors
                    ? v.ESp(u)
                    : v.ESp(u) / (v.ENormSquared() * u.ENormSquared()).Sqrt();

            if (cosAngle.IsOne)
                return processor.CreateIdentityRotor();
            
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

            return new PureRotor<T>(
                processor, 
                scalarPart,
                bivectorPart
            );
        }
        
        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source vector
        /// into the target vector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <param name="sourceVector"></param>
        /// <param name="targetVector"></param>
        /// <returns></returns>
        public static ScaledPureRotor<T> CreateScaledPureRotor<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, VectorStorage<T> sourceVector, VectorStorage<T> targetVector)
        {
            var u = sourceVector.CreateVector(processor);
            var v = targetVector.CreateVector(processor);

            var uNorm = u.ENorm();
            var vNorm = v.ENorm();
            var scalingFactor = (vNorm / uNorm).Sqrt();
            var cosAngle = v.ESp(u) / (uNorm * vNorm);

            if (cosAngle.IsOne)
                return ScaledPureRotor<T>.Create(processor, scalingFactor);
            
            //TODO: Handle the case for cosAngle == -1
            if (cosAngle.IsMinusOne)
                throw new InvalidOperationException();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var rotationBlade = v.Op(u);
            var unitRotationBlade = 
                rotationBlade / (-rotationBlade.ESp()).Sqrt();

            var scalarPart = 
                scalingFactor * cosHalfAngle.ScalarValue;

            var bivectorPart = 
                (scalingFactor * sinHalfAngle * unitRotationBlade).BivectorStorage;

            return new ScaledPureRotor<T>(
                processor, 
                scalarPart,
                bivectorPart
            );
        }
        
        /// <summary>
        /// Create one rotor from the parametric family of pure rotors taking
        /// sourceVector to targetVector in 3D Euclidean space
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <param name="sourceVector"></param>
        /// <param name="targetVector"></param>
        /// <param name="angleTheta"></param>
        /// <returns></returns>
        public static PureRotor<T> CreateParametricPureRotor3D<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, VectorStorage<T> sourceVector, VectorStorage<T> targetVector, T angleTheta)
        {
            // Compute inverse of 3D pseudo-scalar = -e123
            var pseudoScalarInverse =
                processor.VSpaceDimension == 3
                    ? processor.PseudoScalarInverse
                    : processor.CreateEuclideanPseudoScalarInverseStorage(3);

            // Compute the smallest angle between source and target vectors
            var cosAngle0 = 
                processor.ESp(sourceVector, targetVector);

            // Define a rotor S with angle theta in the plane orthogonal to targetVector - sourceVector
            var rotorSBlade =
                processor.EGp(
                    processor.Subtract(targetVector, sourceVector),
                    pseudoScalarInverse
                ).GetBivectorPart();

            var rotorS = processor.CreatePureRotor(
                angleTheta, 
                rotorSBlade
            );

            // Define parametric 2-blade of rotation
            // The actual plane of rotation is made by rotating the plane containing
            // sourceVector and targetVector by angle theta in the plane orthogonal to
            // targetVector - sourceVector using rotor S
            var rotorBlade =
                rotorS.OmMapBivector(
                    processor.Op(targetVector, sourceVector)
                );
                
            // Define parametric angle of rotation
            var sinTheta = processor.Sin(angleTheta);
            var rotorAngle =
                processor.Add(
                    1,
                    processor.Divide(
                        processor.Times(2, processor.Subtract(cosAngle0, 1)),
                        processor.Subtract(
                            2, 
                            processor.Times(
                                processor.Square(sinTheta),
                                processor.Add(cosAngle0, 1)
                            )
                        )
                    )
                );
            // Math.Acos(1 + 2 * (cosAngle0 - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (cosAngle0 + 1)));
            
            // Return the final rotor taking v1 into v2
            return processor.CreatePureRotor(rotorAngle, rotorBlade);
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source basis vector
        /// into the target vector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <param name="sourceBasisVectorIndex"></param>
        /// <param name="targetVector"></param>
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, ulong sourceBasisVectorIndex, VectorStorage<T> targetVector, bool assumeUnitVector = false)
        {
            var k = sourceBasisVectorIndex;
            var v = processor.CreateVector(targetVector);
            var ek = processor.CreateVectorBasis(k);
            var vk = v[k];
            var vk1 = 1 + vk;

            var scalarPart = (vk1 / 2).Sqrt();
            var bivectorPart = (v - vk * ek).Op(ek) / (2 * vk1).Sqrt();

            return new PureRotor<T>(
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
        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, T rotationAngle, BivectorStorage<T> rotationBlade)
        {
            var halfRotationAngle = processor.Divide(rotationAngle, processor.GetScalarFromNumber(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);

            var rotationBladeScalar =
                processor.Divide(
                    sinHalfAngle,
                    processor.SqrtOfNegative(processor.GetTermScalar(processor.EGp(rotationBlade), 0))
                );

            return new PureRotor<T>(
                processor, 
                cosHalfAngle, 
                processor.Times(rotationBladeScalar, rotationBlade)
            );
        }

        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, VectorStorage<T> sourceVector1, VectorStorage<T> sourceVector2, VectorStorage<T> targetVector1, VectorStorage<T> targetVector2, bool assumeUnitVectors = false)
        {
            var rotor1 = 
                processor.CreatePureRotor(
                    sourceVector1, 
                    targetVector1,
                    assumeUnitVectors
                );

            var rotor2 = 
                processor.CreatePureRotor(
                    rotor1.OmMapVector(sourceVector2), 
                    targetVector2,
                    assumeUnitVectors
                );

            var rotor = 
                processor.EGp(rotor2.Multivector, rotor1.Multivector);

            var (scalar, bivector) = processor.GetScalarBivectorParts(rotor);

            return new PureRotor<T>(processor, scalar, bivector);
        }

        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, uint baseSpaceDimensions, VectorStorage<T> inputVector1, VectorStorage<T> inputVector2, VectorStorage<T> rotatedVector1, VectorStorage<T> rotatedVector2)
        {
            var inputFrame = processor.CreateFreeFrame(
                GeoFreeFrameSpecs.CreateLinearlyIndependentSpecs(),
                inputVector1, 
                inputVector2
            );

            var rotatedFrame = processor.CreateFreeFrame(
                GeoFreeFrameSpecs.CreateLinearlyIndependentSpecs(),
                rotatedVector1, 
                rotatedVector2
            );

            var rotor = PureRotorsSequence<T>.CreateFromEuclideanFrames(
                baseSpaceDimensions, 
                inputFrame, 
                rotatedFrame
            ).GetFinalRotor();

            var (scalar, bivector) = processor.GetScalarBivectorParts(rotor.Multivector);

            return new PureRotor<T>(processor, scalar, bivector);
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
        public static PureRotor<T> CreateGivensRotor<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, int i, int j, T rotationAngle)
        {
            Debug.Assert(i >= 0 && j != i);

            var halfRotationAngle = processor.Divide(rotationAngle, processor.GetScalarFromNumber(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);

            return new PureRotor<T>(
                processor,
                cosHalfAngle,
                processor.CreateBivectorTermStorage(i, j, sinHalfAngle)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScaledPureRotor<T> CreateScaledPureRotor<T>(this PureRotor<T> rotor, T scalingFactor)
        {
            var processor = rotor.GeometricProcessor;
            
            var scalarPart = processor.Times(
                scalingFactor, 
                processor.GetScalar(rotor.Multivector)
            );

            var bivectorPart = processor.Times(
                scalingFactor, 
                rotor.Multivector.GetBivectorPart()
            );

            return new ScaledPureRotor<T>(
                rotor.GeometricProcessor,
                scalarPart,
                bivectorPart
            );
        }
    }
}