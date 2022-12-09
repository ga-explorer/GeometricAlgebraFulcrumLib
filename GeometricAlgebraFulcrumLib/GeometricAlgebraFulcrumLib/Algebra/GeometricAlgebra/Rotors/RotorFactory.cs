using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
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
            return PureRotor<T>.Create(
                processor.ScalarOne,
                processor.CreateBivector(KVectorStorage<T>.ZeroBivector)
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
            return ScaledPureRotor<T>.Create(
                processor.ScalarOne,
                processor.CreateBivector(KVectorStorage<T>.ZeroBivector)
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
            return ScaledPureRotor<T>.Create(
                scalingFactor,
                processor.CreateBivector(KVectorStorage<T>.ZeroBivector)
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
        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraProcessor<T> processor, GaBivector<T> blade)
        {
            if (!processor.IsNumeric)
                throw new InvalidOperationException();

            var bladeSignature = blade.Sp();

            if (bladeSignature.IsNearZero())
                return PureRotor<T>.Create(
                    processor.ScalarOne,
                    blade
                );

            if (bladeSignature.IsNegative())
            {
                var alpha = (-blade.Sp()).Sqrt();
                var scalar = alpha.Cos().ScalarValue;
                var bivector = alpha.Sin() / alpha * blade;

                return PureRotor<T>.Create(
                    scalar,
                    bivector
                );
            }
            else
            {
                var alpha = blade.Sp().Sqrt();
                var scalar = alpha.Cosh().ScalarValue;
                var bivector = alpha.Sinh() / alpha * blade;

                return PureRotor<T>.Create(
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
        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraProcessor<T> processor, GaBivector<T> blade, BladeSignatureKind bladeSignatureKind)
        {
            if (bladeSignatureKind == BladeSignatureKind.Zero)
                return PureRotor<T>.Create(
                    processor.ScalarOne,
                    blade
                );

            if (bladeSignatureKind == BladeSignatureKind.Negative)
            {
                var alpha = (-blade.Sp()).Sqrt();
                var scalar = alpha.Cos().ScalarValue;
                var bivector = alpha.Sin() / alpha * blade;

                return PureRotor<T>.Create(
                    scalar,
                    bivector
                );
            }
            else
            {
                var alpha = blade.Sp().Sqrt();
                var scalar = alpha.Cosh().ScalarValue;
                var bivector = alpha.Sinh() / alpha * blade;

                return PureRotor<T>.Create(
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
        /// <param name="rotorMv"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotor<T> CreatePureRotor<T>(this GaMultivector<T> rotorMv)
        {
            return PureRotor<T>.Create(
                rotorMv.GetScalarPart(),
                rotorMv.GetBivectorPart()
            );
        }

        /// <summary>
        /// Create a pure rotor from its scalar and bivector parts
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <param name="rotorMv"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraProcessor<T> processor, GaMultivector<T> rotorMv)
        {
            return PureRotor<T>.Create(
                rotorMv.GetScalarPart(),
                rotorMv.GetBivectorPart()
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
        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, GaVector<T> sourceVector, GaVector<T> targetVector, bool assumeUnitVectors = false)
        {
            var cosAngle =
                assumeUnitVectors
                    ? targetVector.ESp(sourceVector)
                    : targetVector.ESp(sourceVector) / (targetVector.ENormSquared() * sourceVector.ENormSquared()).Sqrt();

            if (cosAngle.IsOne())
                return processor.CreateIdentityRotor();

            //TODO: Handle the case for cosAngle == -1
            if (cosAngle.IsMinusOne())
                throw new InvalidOperationException();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var rotationBlade = targetVector.Op(sourceVector);
            var unitRotationBlade =
                rotationBlade / (-rotationBlade.ESp()).Sqrt();

            var scalarPart = cosHalfAngle.ScalarValue;
            var bivectorPart = sinHalfAngle * unitRotationBlade;

            return PureRotor<T>.Create(
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
        public static ScaledPureRotor<T> CreateScaledPureRotor<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, GaVector<T> sourceVector, GaVector<T> targetVector)
        {
            var uNorm = sourceVector.ENorm();
            var vNorm = targetVector.ENorm();
            var scalingFactor = (vNorm / uNorm).Sqrt().ScalarValue;
            var cosAngle = targetVector.ESp(sourceVector) / (uNorm * vNorm);

            if (cosAngle.IsOne())
                return ScaledPureRotor<T>.Create(processor, scalingFactor);

            //TODO: Handle the case for cosAngle == -1
            if (cosAngle.IsMinusOne())
                throw new InvalidOperationException();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var rotationBlade = targetVector.Op(sourceVector);
            var unitRotationBlade =
                rotationBlade / (-rotationBlade.ESp()).Sqrt();

            var scalarPart =
                scalingFactor * cosHalfAngle;

            var bivectorPart =
                scalingFactor * sinHalfAngle * unitRotationBlade;

            return ScaledPureRotor<T>.Create(
                scalarPart.ScalarValue,
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
        public static PureRotor<T> CreateParametricPureRotor3D<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, GaVector<T> sourceVector, GaVector<T> targetVector, T angleTheta)
        {
            // Compute inverse of 3D pseudo-scalar = -e123
            var pseudoScalarInverse =
                processor.VSpaceDimension == 3
                    ? processor.PseudoScalarInverse
                    : processor.CreateKVectorEuclideanPseudoScalarInverse(3);

            // Compute the smallest angle between source and target vectors
            var cosAngle0 =
                sourceVector.ESp(targetVector);

            // Define a rotor S with angle theta in the plane orthogonal to targetVector - sourceVector
            var rotorSBlade =
                (targetVector - sourceVector).AsMultivector().EGp(
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
                rotorS.OmMap(targetVector.Op(sourceVector));

            var sinAngleThetaSquare = processor.Square(processor.Sin(angleTheta));

            // Define parametric angle of rotation
            var rotorAngle =
                (1 + 2 * (cosAngle0 - 1) / (2 - sinAngleThetaSquare * (cosAngle0 + 1))).ArcCos().ScalarValue;

            // Math.Acos(1 + 2 * (cosAngle0 - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (cosAngle0 + 1)));

            // Return the final rotor taking v1 into v2
            return processor.CreatePureRotor(rotorAngle, rotorBlade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScaledPureRotor<T> CreateScaledParametricPureRotor3D<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, GaVector<T> sourceVector, GaVector<T> targetVector, T angleTheta, T scalingFactor)
        {
            return processor
                .CreateParametricPureRotor3D(sourceVector, targetVector, angleTheta)
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
        public static ScaledPureRotor<T> CreateScaledPureRotorFromAxis<T>(this GaVector<T> targetVector, Axis sourceAxis, bool assumeUnitVector = false)
        {
            var processor = targetVector.GeometricProcessor;
            var k = sourceAxis.BasisVectorIndex;
            var vNorm = assumeUnitVector
                ? processor.ScalarOne
                : targetVector.ENorm().ScalarValue;

            var ek = processor.CreateVectorBasis(k);

            var vk1 = vNorm + (sourceAxis.IsPositive ? targetVector[k] : -targetVector[k]);
            var vOpAxis = sourceAxis.IsPositive ? targetVector.Op(ek) : ek.Op(targetVector);

            return ScaledPureRotor<T>.Create(
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
        public static ScaledPureRotor<T> CreateScaledPureRotorToAxis<T>(this GaVector<T> sourceVector, Axis targetAxis, bool assumeUnitVector = false)
        {
            var processor = sourceVector.GeometricProcessor;
            var k = targetAxis.BasisVectorIndex;
            var vNorm =
                assumeUnitVector
                    ? processor.ScalarOne
                    : sourceVector.ENorm().ScalarValue;

            var vNorm2 =
                assumeUnitVector
                    ? processor.ScalarTwo
                    : processor.Times(2, sourceVector.ENormSquared());

            var ek = processor.CreateVectorBasis(k);

            var vk1 = vNorm + (targetAxis.IsPositive ? sourceVector[k] : -sourceVector[k]);
            var vOpAxis = targetAxis.IsPositive ? ek.Op(sourceVector) : sourceVector.Op(ek);

            return ScaledPureRotor<T>.Create(
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
        public static PureRotor<T> CreatePureRotorFromAxis<T>(this GaVector<T> targetVector, Axis sourceAxis, bool assumeUnitVector = false)
        {
            var processor = targetVector.GeometricProcessor;
            var k = sourceAxis.BasisVectorIndex;

            var v =
                assumeUnitVector
                    ? targetVector
                    : targetVector.DivideByENorm();

            var ek = processor.CreateVectorBasis(k);

            var vk1 = 1 + (sourceAxis.IsPositive ? v[k] : -v[k]);
            var vOpAxis = sourceAxis.IsPositive ? v.Op(ek) : ek.Op(v);

            return PureRotor<T>.Create(
                (vk1 / 2).Sqrt(),
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
        public static PureRotor<T> CreatePureRotorToAxis<T>(this GaVector<T> sourceVector, Axis targetAxis, bool assumeUnitVector = false)
        {
            var processor = sourceVector.GeometricProcessor;
            var k = targetAxis.BasisVectorIndex;

            var v =
                assumeUnitVector
                    ? sourceVector
                    : sourceVector.DivideByENorm();

            var ek = processor.CreateVectorBasis(k);

            var vk1 = 1 + (targetAxis.IsPositive ? v[k] : -v[k]);
            var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

            return PureRotor<T>.Create(
                (vk1 / 2).Sqrt(),
                vOpAxis / (vk1 * 2).Sqrt()
            );
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source basis vector
        /// into the target vector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <param name="sourceAxis"></param>
        /// <param name="targetVector"></param>
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, Axis sourceAxis, GaVector<T> targetVector, bool assumeUnitVector = false)
        {
            var k = sourceAxis.BasisVectorIndex;

            var v =
                assumeUnitVector
                    ? targetVector
                    : targetVector.DivideByENorm();

            var ek = processor.CreateVectorBasis(k);

            var vk1 = 1 + (sourceAxis.IsPositive ? v[k] : -v[k]);
            var vOpAxis = sourceAxis.IsPositive ? v.Op(ek) : ek.Op(v);

            return PureRotor<T>.Create(
                (vk1 / 2).Sqrt(),
                vOpAxis / (vk1 * 2).Sqrt()
            );
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source vector
        /// into the target basis vector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <param name="targetAxis"></param>
        /// <param name="assumeUnitVector"></param>
        /// <param name="sourceVector"></param>
        /// <returns></returns>
        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, GaVector<T> sourceVector, Axis targetAxis, bool assumeUnitVector = false)
        {
            var k = targetAxis.BasisVectorIndex;

            var v =
                assumeUnitVector
                    ? sourceVector
                    : sourceVector.DivideByENorm();

            var ek = processor.CreateVectorBasis(k);

            var vk1 = 1 + (targetAxis.IsPositive ? v[k] : -v[k]);
            var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

            return PureRotor<T>.Create(
                (vk1 / 2).Sqrt(),
                vOpAxis / (vk1 * 2).Sqrt()
            );
        }

        /// <summary>
        /// Create a simple rotor from an angle and a 2-blade
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="rotationAngle"></param>
        /// <param name="rotationBlade"></param>
        /// <returns></returns>
        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, T rotationAngle, GaBivector<T> rotationBlade)
        {
            var halfRotationAngle = processor.Divide(rotationAngle, processor.GetScalarFromNumber(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);

            var rotationBladeScalar =
                sinHalfAngle / (-rotationBlade.ESp(rotationBlade)).Sqrt();

            return PureRotor<T>.Create(
                cosHalfAngle,
                rotationBladeScalar * rotationBlade
            );
        }

        public static PureRotorsSequence<T> CreatePureRotorSequence<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, GaVector<T> sourceVector1, GaVector<T> sourceVector2, GaVector<T> targetVector1, GaVector<T> targetVector2, bool assumeUnitVectors = false)
        {
            var rotor1 =
                processor.CreatePureRotor(
                    sourceVector1,
                    targetVector1,
                    assumeUnitVectors
                );

            var rotor2 =
                processor.CreatePureRotor(
                    rotor1.OmMap(sourceVector2),
                    targetVector2,
                    assumeUnitVectors
                );

            //var rotor = 
            //    rotor2.Multivector.EGp(rotor1.Multivector);

            //var (scalar, bivector) = rotor.GetScalarBivectorParts();

            return PureRotorsSequence<T>.Create(processor, rotor1, rotor2);
        }

        public static PureRotor<T> CreatePureRotor<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, uint baseSpaceDimensions, GaVector<T> inputVector1, GaVector<T> inputVector2, GaVector<T> rotatedVector1, GaVector<T> rotatedVector2)
        {
            var inputFrame = processor.CreateVectorFrame(
                VectorFrameSpecs.CreateLinearlyIndependentSpecs(),
                inputVector1,
                inputVector2
            );

            var rotatedFrame = processor.CreateVectorFrame(
                VectorFrameSpecs.CreateLinearlyIndependentSpecs(),
                rotatedVector1,
                rotatedVector2
            );

            var rotor = PureRotorsSequence<T>.CreateFromEuclideanFrames(
                baseSpaceDimensions,
                inputFrame,
                rotatedFrame
            ).GetFinalRotor();

            var (scalar, bivector) = rotor.Multivector.GetScalarBivectorParts();

            return PureRotor<T>.Create(scalar.ScalarValue, bivector);
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

            return PureRotor<T>.Create(
                cosHalfAngle,
                processor.CreateBivectorTerm(i, j, sinHalfAngle)
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
        public static ScaledPureRotor<T> CreateScaledGivensRotor<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, int i, int j, T rotationAngle, T scalingFactor)
        {
            Debug.Assert(i >= 0 && j != i);

            var halfRotationAngle = processor.Divide(rotationAngle, processor.GetScalarFromNumber(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);
            var s = processor.Sqrt(scalingFactor);
            var scalarPart = processor.Times(s, cosHalfAngle);
            var bivectorPart = s * processor.CreateBivectorTerm(i, j, sinHalfAngle);

            return ScaledPureRotor<T>.Create(scalarPart, bivectorPart);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScaledPureRotor<T> CreateScaledPureRotor<T>(this PureRotor<T> rotor, T scalingFactor)
        {
            var processor = rotor.GeometricProcessor;

            var s = processor.Sqrt(scalingFactor);
            var scalarPart = s * rotor.Multivector[0];
            var bivectorPart = s * rotor.Multivector.GetBivectorPart();

            return ScaledPureRotor<T>.Create(
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
        public static ScaledPureRotor<T> CreateScaledPureRotor2D<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, float r0, float r12)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = processor.GetScalarFromNumber(r0),
                [3] = processor.GetScalarFromNumber(r12)
            };

            return ScaledPureRotor<T>.Create(
                processor,
                processor.CreateMultivectorSparse(idScalarDictionary)
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
        public static ScaledPureRotor<T> CreateScaledPureRotor2D<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, double r0, double r12)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = processor.GetScalarFromNumber(r0),
                [3] = processor.GetScalarFromNumber(r12)
            };

            return ScaledPureRotor<T>.Create(
                processor,
                processor.CreateMultivectorSparse(idScalarDictionary)
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
        public static ScaledPureRotor<T> CreateScaledPureRotor2D<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, string r0, string r12)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = processor.GetScalarFromText(r0),
                [3] = processor.GetScalarFromText(r12)
            };

            return ScaledPureRotor<T>.Create(
                processor,
                processor.CreateMultivectorSparse(idScalarDictionary)
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
        public static ScaledPureRotor<T> CreateScaledPureRotor2D<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, object r0, object r12)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = processor.GetScalarFromObject(r0),
                [3] = processor.GetScalarFromObject(r12)
            };

            return ScaledPureRotor<T>.Create(
                processor,
                processor.CreateMultivectorSparse(idScalarDictionary)
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
        public static ScaledPureRotor<T> CreateScaledPureRotor2D<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, T r0, T r12)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = r0,
                [3] = r12
            };

            return ScaledPureRotor<T>.Create(
                processor,
                processor.CreateMultivectorSparse(idScalarDictionary)
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
        public static ScaledPureRotor<T> CreateScaledPureRotor3D<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, float r0, float r12, float r13, float r23)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = processor.GetScalarFromNumber(r0),
                [3] = processor.GetScalarFromNumber(r12),
                [5] = processor.GetScalarFromNumber(r13),
                [6] = processor.GetScalarFromNumber(r23)
            };

            return ScaledPureRotor<T>.Create(
                processor,
                processor.CreateMultivectorSparse(idScalarDictionary)
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
        public static ScaledPureRotor<T> CreateScaledPureRotor3D<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, double r0, double r12, double r13, double r23)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = processor.GetScalarFromNumber(r0),
                [3] = processor.GetScalarFromNumber(r12),
                [5] = processor.GetScalarFromNumber(r13),
                [6] = processor.GetScalarFromNumber(r23)
            };

            return ScaledPureRotor<T>.Create(
                processor,
                processor.CreateMultivectorSparse(idScalarDictionary)
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
        public static ScaledPureRotor<T> CreateScaledPureRotor3D<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, string r0, string r12, string r13, string r23)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = processor.GetScalarFromText(r0),
                [3] = processor.GetScalarFromText(r12),
                [5] = processor.GetScalarFromText(r13),
                [6] = processor.GetScalarFromText(r23)
            };

            return ScaledPureRotor<T>.Create(
                processor,
                processor.CreateMultivectorSparse(idScalarDictionary)
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
        public static ScaledPureRotor<T> CreateScaledPureRotor3D<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, object r0, object r12, object r13, object r23)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = processor.GetScalarFromObject(r0),
                [3] = processor.GetScalarFromObject(r12),
                [5] = processor.GetScalarFromObject(r13),
                [6] = processor.GetScalarFromObject(r23)
            };

            return ScaledPureRotor<T>.Create(
                processor,
                processor.CreateMultivectorSparse(idScalarDictionary)
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
        public static ScaledPureRotor<T> CreateScaledPureRotor3D<T>(this IGeometricAlgebraEuclideanProcessor<T> processor, T r0, T r12, T r13, T r23)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = r0,
                [3] = r12,
                [5] = r13,
                [6] = r23
            };

            return ScaledPureRotor<T>.Create(
                processor,
                processor.CreateMultivectorSparse(idScalarDictionary)
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
        public static Rotor<T> CreateRotorToFrame<T>(this BasisVectorFrame<T> sourceFrame, BasisVectorFrame<T> targetFrame)
        {
            var processor = sourceFrame.GeometricProcessor;

            var mvFrame1 = sourceFrame.CreateBasisKVectorFrame().MapAsBasisUsing(mv => mv.Inverse());
            var mvFrame2 = targetFrame.CreateBasisKVectorFrame();

            var rotorMultivector =
                mvFrame2
                    .Zip(mvFrame1)
                    .Select(vectorPair => vectorPair.First.Gp(vectorPair.Second))
                    .Aggregate(
                        processor.CreateMultivectorSparseZero(),
                        (mv1, mv2) => mv1 + mv2
                    );

            rotorMultivector /= rotorMultivector.Sp(rotorMultivector.Reverse()).Sqrt();

            return Rotor<T>.Create(rotorMultivector);
        }
    }
}