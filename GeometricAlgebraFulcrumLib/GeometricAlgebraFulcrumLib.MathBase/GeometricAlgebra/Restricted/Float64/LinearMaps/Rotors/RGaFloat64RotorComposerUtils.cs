using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Frames;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors
{
    public static class RGaFloat64RotorComposerUtils
    {
        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <param name="metric"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64PureRotor CreateIdentityRotor(this RGaFloat64Processor metric)
        {
            return RGaFloat64PureRotor.Create(
                1d,
                metric.CreateZeroBivector()
            );
        }

        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <param name="metric"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ScaledPureRotor CreateScaledIdentityRotor(this RGaFloat64Processor metric)
        {
            return RGaFloat64ScaledPureRotor.Create(
                1d,
                metric.CreateZeroBivector()
            );
        }

        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ScaledPureRotor CreateScaledIdentityRotor(this RGaFloat64Processor metric, double scalingFactor)
        {
            return RGaFloat64ScaledPureRotor.Create(
                scalingFactor,
                metric.CreateZeroBivector()
            );
        }

        /// <summary>
        /// Create a pure rotor from a 2-blade, the signature of the blade
        /// is computed automatically using the given processor which must
        /// be of numerical type
        /// </summary>
        /// <param name="blade"></param>
        /// <returns></returns>
        public static RGaFloat64PureRotor CreatePureRotor(this RGaFloat64Bivector blade)
        {
            var bladeSignature = blade.SpSquared();

            if (bladeSignature.IsNearZero())
                return RGaFloat64PureRotor.Create(
                    1d,
                    blade
                );

            if (bladeSignature.Scalar < 0)
            {
                var alpha = (-bladeSignature).Sqrt();
                var scalar = alpha.Cos().ScalarValue;
                var bivector = alpha.Sin() / alpha * blade;

                return RGaFloat64PureRotor.Create(
                    scalar,
                    bivector
                );
            }
            else
            {
                var alpha = bladeSignature.Sqrt();
                var scalar = alpha.Cosh().ScalarValue;
                var bivector = alpha.Sinh() / alpha * blade;

                return RGaFloat64PureRotor.Create(
                    scalar,
                    bivector
                );
            }
        }

        /// <summary>
        /// Create a pure rotor from a 2-blade, the signature of the blade
        /// is given by the user
        /// </summary>
        /// <param name="blade"></param>
        /// <param name="bladeSignatureSign"></param>
        /// <returns></returns>
        public static RGaFloat64PureRotor CreatePureRotor(this RGaFloat64Bivector blade, IntegerSign bladeSignatureSign)
        {
            if (bladeSignatureSign.IsZero)
                return RGaFloat64PureRotor.Create(
                    1d,
                    blade
                );

            var bladeSignature = blade.SpSquared();

            if (bladeSignatureSign.IsNegative)
            {
                var alpha = (-bladeSignature).Sqrt();
                var scalar = alpha.Cos().ScalarValue;
                var bivector = alpha.Sin() / alpha * blade;

                return RGaFloat64PureRotor.Create(
                    scalar,
                    bivector
                );
            }
            else
            {
                var alpha = bladeSignature.Sqrt();
                var scalar = alpha.Cosh().ScalarValue;
                var bivector = alpha.Sinh() / alpha * blade;

                return RGaFloat64PureRotor.Create(
                    scalar,
                    bivector
                );
            }
        }

        /// <summary>
        /// Create a pure rotor from its scalar and bivector parts
        /// </summary>
        /// <param name="rotorMv"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64PureRotor CreatePureRotor(this RGaFloat64Multivector rotorMv)
        {
            return RGaFloat64PureRotor.Create(
                rotorMv.GetScalarPart().ScalarValue,
                rotorMv.GetBivectorPart()
            );
        }
        
        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source vector into the target vector
        /// </summary>
        /// <param name="sourceVector"></param>
        /// <param name="targetVector"></param>
        /// <param name="assumeUnitVectors"></param>
        /// <returns></returns>
        public static RGaFloat64PureRotor CreatePureRotor(this RGaFloat64Vector sourceVector, RGaFloat64Vector targetVector, bool assumeUnitVectors = false)
        {
            var metric = sourceVector.Processor;

            var cosAngle =
                assumeUnitVectors
                    ? targetVector.ESp(sourceVector)
                    : targetVector.ESp(sourceVector) / (targetVector.ENormSquared() * sourceVector.ENormSquared()).Sqrt();

            if (cosAngle.IsOne)
                return metric.CreateIdentityRotor();

            //TODO: Handle the case for cosAngle == -1
            if (cosAngle.IsMinusOne)
                throw new InvalidOperationException();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var rotationBlade = targetVector.Op(sourceVector);
            var unitRotationBlade =
                rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

            var scalarPart = cosHalfAngle.ScalarValue;
            var bivectorPart = sinHalfAngle * unitRotationBlade;

            return RGaFloat64PureRotor.Create(
                scalarPart,
                bivectorPart
            );
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source vector
        /// into the target vector
        /// </summary>
        /// <param name="sourceVector"></param>
        /// <param name="targetVector"></param>
        /// <returns></returns>
        public static RGaFloat64ScaledPureRotor CreateScaledPureRotor(this RGaFloat64Vector sourceVector, RGaFloat64Vector targetVector)
        {
            var metric = sourceVector.Processor;

            var uNorm = sourceVector.ENorm();
            var vNorm = targetVector.ENorm();
            var scalingFactor = (vNorm / uNorm).Sqrt().ScalarValue;
            var cosAngle = targetVector.ESp(sourceVector) / (uNorm * vNorm);

            if (cosAngle.IsOne)
                return RGaFloat64ScaledPureRotor.Create(metric, scalingFactor);

            //TODO: Handle the case for cosAngle == -1
            if (cosAngle.IsMinusOne)
                throw new InvalidOperationException();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var rotationBlade = targetVector.Op(sourceVector);
            var unitRotationBlade =
                rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

            var scalarPart =
                scalingFactor * cosHalfAngle;

            var bivectorPart =
                scalingFactor * sinHalfAngle * unitRotationBlade;

            return RGaFloat64ScaledPureRotor.Create(
                scalarPart.ScalarValue,
                bivectorPart
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
        public static RGaFloat64PureRotor CreateParametricPureRotor3D(this RGaFloat64Vector sourceVector, RGaFloat64Vector targetVector, double angleTheta)
        {
            var metric = sourceVector.Processor;

            // Compute inverse of 3D pseudo-scalar = -e123
            var pseudoScalarInverse =
                metric.CreatePseudoScalarInverse(3);

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

            var sinAngleThetaSquare = angleTheta.Sin().Square();

            // Define parametric angle of rotation
            var rotorAngle =
                (1 + 2 * (cosAngle0 - 1) / (2 - sinAngleThetaSquare * (cosAngle0 + 1))).ArcCos().ScalarValue;

            // Math.Acos(1 + 2 * (cosAngle0 - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (cosAngle0 + 1)));

            // Return the final rotor taking v1 into v2
            return rotorBlade.CreatePureRotor(rotorAngle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ScaledPureRotor CreateScaledParametricPureRotor3D(this RGaFloat64Vector sourceVector, RGaFloat64Vector targetVector, double angleTheta, double scalingFactor)
        {
            return sourceVector
                .CreateParametricPureRotor3D(targetVector, angleTheta)
                .CreateScaledPureRotor(scalingFactor);
        }


        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source basis vector
        /// into the target vector
        /// </summary>
        /// <param name="sourceAxis"></param>
        /// <param name="targetVector"></param>
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public static RGaFloat64ScaledPureRotor CreateScaledPureRotorFromAxis(this RGaFloat64Vector targetVector, LinSignedBasisVector sourceAxis, bool assumeUnitVector = false)
        {
            var metric = targetVector.Processor;
            
            var k = sourceAxis.Index;
            var vNorm = assumeUnitVector
                ? 1d
                : targetVector.ENorm().ScalarValue;

            var ek = metric.CreateVector(k);

            var vk1 = vNorm + (sourceAxis.IsPositive ? targetVector[k] : -targetVector[k]);
            var vOpAxis = sourceAxis.IsPositive ? targetVector.Op(ek) : ek.Op(targetVector);

            return RGaFloat64ScaledPureRotor.Create(
                (vk1 / 2).Sqrt(),
                vOpAxis / (vk1 * 2).Sqrt()
            );
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source basis vector
        /// into the target vector
        /// </summary>
        /// <param name="targetAxis"></param>
        /// <param name="assumeUnitVector"></param>
        /// <param name="sourceVector"></param>
        /// <returns></returns>
        public static RGaFloat64ScaledPureRotor CreateScaledPureRotorToAxis(this RGaFloat64Vector sourceVector, LinSignedBasisVector targetAxis, bool assumeUnitVector = false)
        {
            var metric = sourceVector.Processor;

            var k = targetAxis.Index;
            var vNorm =
                assumeUnitVector
                    ? 1d
                    : sourceVector.ENorm().ScalarValue;

            var vNorm2 =
                assumeUnitVector
                    ? 2d
                    : 2d * sourceVector.ENormSquared().ScalarValue;

            var ek = metric.CreateVector(k);

            var vk1 = vNorm + (targetAxis.IsPositive ? sourceVector[k] : -sourceVector[k]);
            var vOpAxis = targetAxis.IsPositive ? ek.Op(sourceVector) : sourceVector.Op(ek);

            return RGaFloat64ScaledPureRotor.Create(
                (vk1 / vNorm2).Sqrt(),
                vOpAxis / (vk1 * vNorm2).Sqrt()
            );
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source basis vector
        /// into the target vector
        /// </summary>
        /// <param name="sourceAxis"></param>
        /// <param name="targetVector"></param>
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public static RGaFloat64PureRotor CreatePureRotorFromAxis(this RGaFloat64Vector targetVector, LinSignedBasisVector sourceAxis, bool assumeUnitVector = false)
        {
            var metric = targetVector.Processor;

            var k = sourceAxis.Index;

            var v =
                assumeUnitVector
                    ? targetVector
                    : targetVector.DivideByENorm();

            var ek = metric.CreateVector(k);

            var vk1 = 1 + (sourceAxis.IsPositive ? v[k] : -v[k]);
            var vOpAxis = sourceAxis.IsPositive ? v.Op(ek) : ek.Op(v);

            return RGaFloat64PureRotor.Create(
                (vk1 / 2).Sqrt(),
                vOpAxis / (vk1 * 2).Sqrt()
            );
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source vector
        /// into the target basis vector
        /// </summary>
        /// <param name="targetAxis"></param>
        /// <param name="assumeUnitVector"></param>
        /// <param name="sourceVector"></param>
        /// <returns></returns>
        public static RGaFloat64PureRotor CreatePureRotorToAxis(this RGaFloat64Vector sourceVector, LinSignedBasisVector targetAxis, bool assumeUnitVector = false)
        {
            var metric = sourceVector.Processor;

            var k = targetAxis.Index;

            var v =
                assumeUnitVector
                    ? sourceVector
                    : sourceVector.DivideByENorm();

            var ek = metric.CreateVector(k);

            var vk1 = 1 + (targetAxis.IsPositive ? v[k] : -v[k]);
            var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

            return RGaFloat64PureRotor.Create(
                (vk1 / 2).Sqrt(),
                vOpAxis / (vk1 * 2).Sqrt()
            );
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source basis vector
        /// into the target vector
        /// </summary>
        /// <param name="sourceAxis"></param>
        /// <param name="targetVector"></param>
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public static RGaFloat64PureRotor CreatePureRotor(this LinSignedBasisVector sourceAxis, RGaFloat64Vector targetVector, bool assumeUnitVector = false)
        {
            var metric = targetVector.Processor;

            var k = sourceAxis.Index;

            var v =
                assumeUnitVector
                    ? targetVector
                    : targetVector.DivideByENorm();

            var ek = metric.CreateVector(k);

            var vk1 = 1 + (sourceAxis.IsPositive ? v[k] : -v[k]);
            var vOpAxis = sourceAxis.IsPositive ? v.Op(ek) : ek.Op(v);

            return RGaFloat64PureRotor.Create(
                (vk1 / 2).Sqrt(),
                vOpAxis / (vk1 * 2).Sqrt()
            );
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source vector
        /// into the target basis vector
        /// </summary>
        /// <param name="targetAxis"></param>
        /// <param name="assumeUnitVector"></param>
        /// <param name="sourceVector"></param>
        /// <returns></returns>
        public static RGaFloat64PureRotor CreatePureRotor(this RGaFloat64Vector sourceVector, LinSignedBasisVector targetAxis, bool assumeUnitVector = false)
        {
            var metric = sourceVector.Processor;

            var k = targetAxis.Index;

            var v =
                assumeUnitVector
                    ? sourceVector
                    : sourceVector.DivideByENorm();

            var ek = metric.CreateVector(k);

            var vk1 = 1 + (targetAxis.IsPositive ? v[k] : -v[k]);
            var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

            return RGaFloat64PureRotor.Create(
                (vk1 / 2).Sqrt(),
                vOpAxis / (vk1 * 2).Sqrt()
            );
        }

        /// <summary>
        /// Create a simple rotor from an angle and a 2-blade
        /// </summary>
        /// <param name="rotationAngle"></param>
        /// <param name="rotationBlade"></param>
        /// <returns></returns>
        public static RGaFloat64PureRotor CreatePureRotor(this RGaFloat64Bivector rotationBlade, double rotationAngle)
        {
            var halfRotationAngle = rotationAngle / 2d;
            var cosHalfAngle = halfRotationAngle.Cos();
            var sinHalfAngle = halfRotationAngle.Sin();

            var rotationBladeScalar =
                sinHalfAngle / (-rotationBlade.ESp(rotationBlade)).Sqrt();

            return RGaFloat64PureRotor.Create(
                cosHalfAngle,
                rotationBladeScalar * rotationBlade
            );
        }

        public static RGaFloat64PureRotorsSequence CreatePureRotorSequence(this RGaFloat64Vector sourceVector1, RGaFloat64Vector sourceVector2, RGaFloat64Vector targetVector1, RGaFloat64Vector targetVector2, bool assumeUnitVectors = false)
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

            return RGaFloat64PureRotorsSequence.Create(rotor1, rotor2);
        }

        public static RGaFloat64PureRotor CreatePureRotor(this RGaFloat64Vector inputVector1, RGaFloat64Vector inputVector2, RGaFloat64Vector rotatedVector1, RGaFloat64Vector rotatedVector2, int baseSpaceDimensions)
        {
            var inputFrame = RGaFloat64VectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(
                    inputVector1,
                    inputVector2
                );

            var rotatedFrame = RGaFloat64VectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(
                    rotatedVector1,
                    rotatedVector2
                );

            var rotor = RGaFloat64PureRotorsSequence.CreateFromEuclideanFrames(
                baseSpaceDimensions,
                inputFrame,
                rotatedFrame
            ).GetFinalRotor();

            var (scalar, bivector) = rotor.Multivector.GetScalarBivectorParts();

            return RGaFloat64PureRotor.Create(scalar.ScalarValue, bivector);
        }

        /// <summary>
        /// Construct a rotor in the e_i-e_j plane with the given angle where i is less than j
        /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64PureRotor CreateGivensRotor(int i, int j, double rotationAngle)
        {
            Debug.Assert(i >= 0 && j != i);

            var metric = RGaFloat64Processor.Euclidean;

            var halfRotationAngle = rotationAngle / 2d;
            var cosHalfAngle = halfRotationAngle.Cos();
            var sinHalfAngle = halfRotationAngle.Sin();

            return RGaFloat64PureRotor.Create(
                cosHalfAngle,
                metric.CreateBivector(i, j, sinHalfAngle)
            );
        }

        /// <summary>
        /// Construct a scaled rotor in the e_i-e_j plane with the given angle where i is less than j
        /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="rotationAngle"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ScaledPureRotor CreateScaledGivensRotor(this int i, int j, double rotationAngle, double scalingFactor)
        {
            Debug.Assert(i >= 0 && j != i);

            var metric = RGaFloat64Processor.Euclidean;

            var halfRotationAngle = rotationAngle / 2d;
            var cosHalfAngle = halfRotationAngle.Cos();
            var sinHalfAngle = halfRotationAngle.Sin();
            var s = scalingFactor.Sqrt();
            var scalarPart = s * cosHalfAngle;
            var bivectorPart = s * metric.CreateBivector(i, j, sinHalfAngle);

            return RGaFloat64ScaledPureRotor.Create(scalarPart, bivectorPart);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ScaledPureRotor CreateScaledPureRotor(this RGaFloat64PureRotor rotor, double scalingFactor)
        {
            var s = scalingFactor.Sqrt();
            var scalarPart = s * rotor.Multivector[0];
            var bivectorPart = s * rotor.Multivector.GetBivectorPart();

            return RGaFloat64ScaledPureRotor.Create(
                scalarPart,
                bivectorPart
            );
        }

        /// <summary>
        /// Create a scaled pure rotor in 2D Euclidean space directly using its scalar components
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ScaledPureRotor CreateScaledPureRotor2D(this RGaFloat64Processor metric, float r0, float r12)
        {
            return RGaFloat64ScaledPureRotor.Create(
                metric
                    .CreateComposer()
                    .SetTerm(0, r0)
                    .SetTerm(3, r12)
                    .GetMultivector()
            );
        }

        /// <summary>
        /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ScaledPureRotor CreateScaledPureRotor2D(this RGaFloat64Processor metric, double r0, double r12)
        {
            return RGaFloat64ScaledPureRotor.Create(
                metric
                    .CreateComposer()
                    .SetTerm(0, r0)
                    .SetTerm(3, r12)
                    .GetMultivector()
            );
        }
        
        /// <summary>
        /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <param name="r13"></param>
        /// <param name="r23"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ScaledPureRotor CreateScaledPureRotor3D(this RGaFloat64Processor metric, float r0, float r12, float r13, float r23)
        {
            return RGaFloat64ScaledPureRotor.Create(
                metric
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
        /// <param name="metric"></param>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <param name="r13"></param>
        /// <param name="r23"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ScaledPureRotor CreateScaledPureRotor3D(this RGaFloat64Processor metric, double r0, double r12, double r13, double r23)
        {
            return RGaFloat64ScaledPureRotor.Create(
                metric
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
        /// <param name="sourceFrame"></param>
        /// <param name="targetFrame"></param>
        /// <returns></returns>
        public static RGaFloat64Rotor CreateRotorToFrame(this RGaFloat64BasisVectorFrame sourceFrame, RGaFloat64BasisVectorFrame targetFrame)
        {
            var metric = sourceFrame.Processor;

            var mvFrame1 = sourceFrame.CreateBasisKVectorFrame().MapAsBasisUsing(mv => mv.Inverse());
            var mvFrame2 = targetFrame.CreateBasisKVectorFrame();

            var rotorMultivector =
                mvFrame2
                    .Zip(mvFrame1)
                    .Select(vectorPair => vectorPair.First.Gp(vectorPair.Second))
                    .Aggregate(
                        (RGaFloat64Multivector) metric.CreateZeroMultivector(),
                        (mv1, mv2) => mv1.Add(mv2)
                    );

            rotorMultivector /= rotorMultivector.Sp(rotorMultivector.Reverse()).Sqrt().ScalarValue;

            return RGaFloat64Rotor.Create(rotorMultivector);
        }
    }
}