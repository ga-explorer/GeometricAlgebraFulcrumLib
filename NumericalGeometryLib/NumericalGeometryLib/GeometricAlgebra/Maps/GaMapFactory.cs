using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
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
        public static GaScaledPureRotor CreateIdentityRotor(this GaBasisSet basisSet)
        {
            var multivector = basisSet.CreateBasisBlade(0);

            return new GaScaledPureRotor(
                multivector,
                multivector,
                1d
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScaledPureRotor CreateScaledIdentityRotor(this GaBasisSet basisSet, double scalingFactor)
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
        /// <param name="sourceVector"></param>
        /// <param name="targetVector"></param>
        /// <param name="angleTheta"></param>
        /// <returns></returns>
        public static GaScaledPureRotor CreateEuclideanParametricPureRotor3D(this GaMultivector sourceVector, GaMultivector targetVector, PlanarAngle angleTheta)
        {
            var basisSet = sourceVector.BasisSet;

            // Compute inverse of 3D pseudo-scalar = -e123
            var pseudoScalarInverse =
                basisSet.CreateBasisBlade(7).EBladeInverse();

            // Compute the smallest angle between source and target vectors
            var cosAngle0 = 
                sourceVector.ESp(targetVector);

            // Define a rotor S with angle theta in the plane orthogonal to targetVector - sourceVector
            var rotorSBlade = 
                (targetVector - sourceVector).EGp(pseudoScalarInverse);

            var rotorS = 
                rotorSBlade.CreateEuclideanPureRotor(angleTheta);

            // Define parametric 2-blade of rotation
            // The actual plane of rotation is made by rotating the plane containing
            // sourceVector and targetVector by angle theta in the plane orthogonal to
            // targetVector - sourceVector using rotor S
            var rotorBlade =
                rotorS.Rotate(targetVector.Op(sourceVector));
            
            // Define parametric angle of rotation
            var rotorAngle =
                (1 + 2 * (cosAngle0 - 1) / (2 - angleTheta.Sin().Squared() * (cosAngle0 + 1))).ArcCos();

            // Math.Acos(1 + 2 * (cosAngle0 - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (cosAngle0 + 1)));
            
            // Return the final rotor taking v1 into v2
            return rotorBlade.CreateEuclideanPureRotor(rotorAngle);
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
        public static GaScaledPureRotor CreateEuclideanPureRotor(this GaBasisSet basisSet, ulong sourceBasisVectorIndex, GaMultivector targetVector, bool assumeUnitVector = false)
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
                rotor1.Rotate(sourceVector2).CreateEuclideanPureRotor(
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
        public static GaScaledPureRotor CreateGivensRotor(this GaBasisSet basisSet, ulong i, ulong j, PlanarAngle rotationAngle)
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
    }
}
