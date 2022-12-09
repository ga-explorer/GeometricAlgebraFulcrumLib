using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.Multivectors;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace NumericalGeometryLib.GeometricAlgebra.Maps
{
    public static class GaMapFactory
    {
        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <param name="basisSet"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScaledPureRotor CreateIdentityRotor(this BasisBladeSet basisSet)
        {
            var multivector = basisSet.CreateBasisBlade(0);

            return new GaScaledPureRotor(
                multivector,
                multivector,
                1d
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScaledPureRotor CreateScaledIdentityRotor(this BasisBladeSet basisSet, double scalingFactor)
        {
            if (scalingFactor <= 0)
                throw new ArgumentOutOfRangeException(nameof(scalingFactor));

            var multivector = basisSet.CreateTerm(0, Math.Sqrt(scalingFactor));

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
        public static GaScaledPureRotor CreatePureRotor(this GaMultivector blade)
        {
            return CreatePureRotor(blade, blade.SpSquared().Sign());
        }

        /// <summary>
        /// Create a pure rotor from a 2-blade, the signature of the blade
        /// is given by the user
        /// </summary>
        /// <param name="blade"></param>
        /// <param name="bladeSignature"></param>
        /// <returns></returns>
        public static GaScaledPureRotor CreatePureRotor(this GaMultivector blade, int bladeSignature)
        {
            var basisSet = blade.BasisSet;

            if (bladeSignature == 0)
            {
                var mv = basisSet.CreateBasisScalar();

                return new GaScaledPureRotor(mv, mv, 1d);
            }
            else if (bladeSignature < 0)
            {
                var alpha = (-blade.SpSquared()).Sqrt();
                var mv = alpha.Cos() + (alpha.Sin() / alpha) * blade;

                return new GaScaledPureRotor(mv);
            }
            else
            {
                var alpha = blade.SpSquared().Sqrt();
                var mv = alpha.Cosh() + (alpha.Sinh() / alpha) * blade;

                return new GaScaledPureRotor(mv);
            }
        }

        /// <summary>
        /// Create a pure rotor from its scalar and bivector parts
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScaledPureRotor CreateScaledPureRotor(this GaMultivector multivector)
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
        public static GaScaledPureRotor CreateEuclideanPureRotor(this GaMultivector sourceVector, GaMultivector targetVector, bool assumeUnitVectors = false)
        {
            var basisSet = sourceVector.BasisSet;

            var cosAngle = 
                assumeUnitVectors
                    ? targetVector.ESp(sourceVector)
                    : targetVector.ESp(sourceVector) / 
                      (targetVector.ENormSquared() * sourceVector.ENormSquared()).Sqrt();

            if (cosAngle == 1d)
                return basisSet.CreateIdentityRotor();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var rotationBlade = 
                cosAngle == -1
                    ? sourceVector.VectorEUnitNormal().Op(sourceVector)
                    : targetVector.Op(sourceVector);

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
        public static GaScaledPureRotor CreateEuclideanPureRotor(this IFloat64Tuple2D sourceVector, IFloat64Tuple2D targetVector, bool assumeUnitVectors = false)
        {
            var basisSet = BasisBladeSet.Euclidean2D;

            var cosAngle = 
                assumeUnitVectors
                    ? targetVector.VectorDot(sourceVector)
                    : targetVector.VectorDot(sourceVector) / 
                      (targetVector.GetVectorNormSquared() * sourceVector.GetVectorNormSquared()).Sqrt();

            if (cosAngle == 1d)
                return basisSet.CreateIdentityRotor();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var rotationBlade = 
                cosAngle == -1
                    ? sourceVector.GetUnitNormal().Op(sourceVector)
                    : targetVector.Op(sourceVector);

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
        public static GaScaledPureRotor CreateEuclideanPureRotor(this IFloat64Tuple3D sourceVector, IFloat64Tuple3D targetVector, bool assumeUnitVectors = false)
        {
            var basisSet = BasisBladeSet.Euclidean3D;

            var cosAngle = 
                assumeUnitVectors
                    ? targetVector.VectorDot(sourceVector)
                    : targetVector.VectorDot(sourceVector) / 
                      (targetVector.GetVectorNormSquared() * sourceVector.GetVectorNormSquared()).Sqrt();

            if (cosAngle == 1d)
                return basisSet.CreateIdentityRotor();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var rotationBlade = 
                cosAngle == -1
                    ? sourceVector.GetUnitNormal().Op(sourceVector)
                    : targetVector.Op(sourceVector);

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
        public static GaScaledPureRotor CreateEuclideanScaledPureRotor(this GaMultivector sourceVector, GaMultivector targetVector)
        {
            var basisSet = sourceVector.BasisSet;

            var uNorm = sourceVector.ENorm();
            var vNorm = targetVector.ENorm();
            var scalingFactor = (vNorm / uNorm).Sqrt();
            var cosAngle = targetVector.ESp(sourceVector) / (uNorm * vNorm);

            if (cosAngle == 1d)
                return basisSet.CreateScaledIdentityRotor(scalingFactor);
            
            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var rotationBlade = 
                cosAngle == -1d
                    ? sourceVector.VectorEUnitNormal().Op(sourceVector)
                    : targetVector.Op(sourceVector);

            var unitRotationBlade = 
                rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

            var scalarPart = 
                scalingFactor * cosHalfAngle;

            var bivectorPart = 
                (scalingFactor * sinHalfAngle * unitRotationBlade);

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
        public static GaScaledPureRotor CreateEuclideanScaledPureRotor(this IFloat64Tuple2D sourceVector, IFloat64Tuple2D targetVector)
        {
            var basisSet = BasisBladeSet.Euclidean2D;

            var uNorm = sourceVector.GetVectorNorm();
            var vNorm = targetVector.GetVectorNorm();
            var scalingFactor = (vNorm / uNorm).Sqrt();
            var cosAngle = targetVector.VectorDot(sourceVector) / (uNorm * vNorm);

            if (cosAngle == 1d)
                return basisSet.CreateScaledIdentityRotor(scalingFactor);
            
            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var rotationBlade = 
                cosAngle == -1d
                    ? sourceVector.GetUnitNormal().Op(sourceVector)
                    : targetVector.Op(sourceVector);

            var unitRotationBlade = 
                rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

            var scalarPart = 
                scalingFactor * cosHalfAngle;

            var bivectorPart = 
                (scalingFactor * sinHalfAngle * unitRotationBlade);

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
        public static GaScaledPureRotor CreateEuclideanScaledPureRotor(this IFloat64Tuple3D sourceVector, IFloat64Tuple3D targetVector)
        {
            var basisSet = BasisBladeSet.Euclidean3D;

            var uNorm = sourceVector.GetVectorNorm();
            var vNorm = targetVector.GetVectorNorm();
            var scalingFactor = (vNorm / uNorm).Sqrt();
            var cosAngle = targetVector.VectorDot(sourceVector) / (uNorm * vNorm);

            if (cosAngle == 1d)
                return basisSet.CreateScaledIdentityRotor(scalingFactor);
            
            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var rotationBlade = 
                cosAngle == -1d
                    ? basisSet.Op(sourceVector.GetUnitNormal(), sourceVector)
                    : basisSet.Op(targetVector, sourceVector);

            var unitRotationBlade = 
                rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

            var scalarPart = 
                scalingFactor * cosHalfAngle;

            var bivectorPart = 
                (scalingFactor * sinHalfAngle * unitRotationBlade);

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
        public static GaScaledPureRotor CreateEuclideanPureRotor(this GaMultivector rotationBlade, PlanarAngle rotationAngle)
        {
            var (sinHalfAngle, cosHalfAngle) = 
                (0.5d * rotationAngle.Radians).SinCos();

            var bivectorPart =
                (sinHalfAngle / (-rotationBlade.ESpSquared()).Sqrt()) * rotationBlade;

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
        public static GaScaledPureRotor CreateEuclideanParametricPureRotor3D(this BasisBladeSet basisSet, IFloat64Tuple3D sourceVector, IFloat64Tuple3D targetVector, PlanarAngle angleTheta)
        {
            //var basisSet = BasisBladeSet.CreateEuclidean(3);

            // Compute inverse of 3D pseudo-scalar = -e123
            var pseudoScalarInverse =
                basisSet.CreateBasisBlade(7).EBladeInverse();

            // Compute the smallest angle between source and target vectors
            var cosAngle0 = 
                sourceVector.VectorDot(targetVector);

            // Define a rotor S with angle theta in the plane orthogonal to targetVector - sourceVector
            var rotorSBlade = 
                targetVector.Subtract(sourceVector).EGp(pseudoScalarInverse);

            var rotorS = 
                rotorSBlade.CreateEuclideanPureRotor(angleTheta);

            // Define parametric 2-blade of rotation
            // The actual plane of rotation is made by rotating the plane containing
            // sourceVector and targetVector by angle theta in the plane orthogonal to
            // targetVector - sourceVector using rotor S
            var rotorBlade =
                rotorS.OmMap(basisSet.Op(targetVector, sourceVector));
            
            // Define parametric angle of rotation
            var rotorAngle =
                (1 + 2 * (cosAngle0 - 1) / (2 - angleTheta.Sin().Square() * (cosAngle0 + 1))).ArcCos();

            // Math.Acos(1 + 2 * (cosAngle0 - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (cosAngle0 + 1)));
            
            // Return the final rotor taking v1 into v2
            return rotorBlade.CreateEuclideanPureRotor(rotorAngle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScaledPureRotor CreateEuclideanScaledParametricPureRotor3D(this BasisBladeSet basisSet, IFloat64Tuple3D sourceVector, IFloat64Tuple3D targetVector, PlanarAngle angleTheta, bool assumeUnitVectors = false)
        {
            if (assumeUnitVectors)
                basisSet.CreateEuclideanParametricPureRotor3D(sourceVector, targetVector, angleTheta);

            var (sourceVectorUnit, sourceVectorLength) = 
                sourceVector.GetUnitVectorNormTuple();

            var (targetVectorUnit, targetVectorLength) = 
                targetVector.GetUnitVectorNormTuple();

            var scalingFactor = targetVectorLength / sourceVectorLength;

            return basisSet
                .CreateEuclideanParametricPureRotor3D(sourceVectorUnit, targetVectorUnit, angleTheta)
                .CreateScaledPureRotor(scalingFactor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScaledPureRotor CreateEuclideanScaledParametricPureRotor3D(this BasisBladeSet basisSet, IFloat64Tuple3D sourceVector, IFloat64Tuple3D targetVector, PlanarAngle angleTheta, double scalingFactor)
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
        public static GaScaledPureRotor CreateEuclideanPureRotor(this BasisBladeSet basisSet, ulong sourceBasisVectorIndex, GaMultivector targetVector, bool assumeUnitVector = false)
        {
            var ek = basisSet.CreateBasisVector(sourceBasisVectorIndex);
            var vk = targetVector[sourceBasisVectorIndex];
            var vk1 = 1 + vk;

            var scalarPart = (vk1 / 2).Sqrt();
            var bivectorPart = (targetVector - vk * ek).Op(ek) / (2 * vk1).Sqrt();

            return new GaScaledPureRotor(
                scalarPart + bivectorPart,
                scalarPart - bivectorPart
            );
        }

        public static GaScaledPureRotor CreateEuclideanPureRotor(this GaMultivector sourceVector1, GaMultivector sourceVector2, GaMultivector targetVector1, GaMultivector targetVector2, bool assumeUnitVectors = false)
        {
            var rotor1 = 
                sourceVector1.CreateEuclideanPureRotor(
                    targetVector1,
                    assumeUnitVectors
                );

            var rotor2 = 
                rotor1.OmMap(sourceVector2).CreateEuclideanPureRotor(
                    targetVector2,
                    assumeUnitVectors
                );

            var multivector = 
                rotor2.Multivector.EGp(rotor1.Multivector);
            
            return new GaScaledPureRotor(multivector);
        }

        //public static GaScaledPureRotor CreateEuclideanPureRotor(this GaBasisSet basisSet, uint baseSpaceDimensions, GaMultivector inputVector1, GaMultivector inputVector2, GaMultivector rotatedVector1, GaMultivector rotatedVector2)
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
        public static GaScaledPureRotor CreateGivensRotor(this BasisBladeSet basisSet, ulong i, ulong j, PlanarAngle rotationAngle)
        {
            Debug.Assert(j != i);

            var halfRotationAngle = 0.5 * rotationAngle.Radians;
            var (sinHalfAngle, cosHalfAngle) = halfRotationAngle.SinCos();

            var bivectorPart = basisSet.CreateBivectorTerm(i, j, sinHalfAngle);

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
        public static GaScaledPureRotor CreateScaledPureRotor2D(this BasisBladeSet basisSet, double r0, double r12)
        {
            var multivector = new GaMultivector(basisSet)
            {
                [0] = r0,
                [3] = r12
            };

            return new GaScaledPureRotor(multivector);
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
        public static GaScaledPureRotor CreateScaledPureRotor3D(this BasisBladeSet basisSet, double r0, double r12, double r13, double r23)
        {
            var multivector = new GaMultivector(basisSet)
            {
                [0] = r0,
                [3] = r12,
                [5] = r13,
                [6] = r23
            };

            return new GaScaledPureRotor(multivector);
        }


        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source basis vector
        /// into the target vector
        /// </summary>
        /// <param name="sourceBasisVectorIndex"></param>
        /// <param name="targetVector"></param>
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public static GaScaledPureRotor CreatePureRotorFromBasisVector(this GaMultivector targetVector, ulong sourceBasisVectorIndex, bool assumeUnitVector = false)
        {
            var processor = targetVector.BasisSet;
            var k = sourceBasisVectorIndex;
            
            var v = 
                assumeUnitVector
                    ? targetVector
                    : targetVector.DivideByENorm();

            var ek = processor.CreateBasisVector(k);
            
            var vk = v[k];
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
        public static GaScaledPureRotor CreateScaledPureRotorFromBasisVector(this GaMultivector targetVector, ulong sourceBasisVectorIndex, bool assumeUnitVector = false)
        {
            var processor = targetVector.BasisSet;
            var k = sourceBasisVectorIndex;
            var vNorm = 
                assumeUnitVector 
                    ? 1d 
                    : targetVector.ENorm();

            var v = 
                assumeUnitVector
                    ? targetVector
                    : targetVector / vNorm;

            var ek = processor.CreateBasisVector(k);
            
            var vk = v[k];
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
        public static GaScaledPureRotor CreateScaledPureRotorToBasisVector(this GaMultivector sourceVector, ulong targetBasisVectorIndex, bool assumeUnitVector = false)
        {
            var processor = sourceVector.BasisSet;
            var k = targetBasisVectorIndex;
            var vNorm = 
                assumeUnitVector 
                    ? 1d 
                    : sourceVector.ENorm();

            var v = 
                assumeUnitVector
                    ? sourceVector
                    : sourceVector / vNorm;

            var ek = processor.CreateBasisVector(k);
            
            var vk = v[k];
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
        public static GaScaledPureRotor CreatePureRotorToBasisVector(this GaMultivector sourceVector, ulong targetBasisVectorIndex, bool assumeUnitVector = false)
        {
            var processor = sourceVector.BasisSet;
            var k = targetBasisVectorIndex;
            
            var v = 
                assumeUnitVector
                    ? sourceVector
                    : sourceVector.DivideByENorm();

            var ek = processor.CreateBasisVector(k);
            
            var vk = v[k];
            var vk1 = 1 + vk;

            return new GaScaledPureRotor(
                (vk1 / 2).Sqrt() + ek.Op(v) / (2 * vk1).Sqrt()
            );
        }

    }
}
