﻿using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors
{
    public abstract partial class XGaMultivector<T>
    {
        /// <summary>
        /// Create a pure rotor from its scalar and bivector parts
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureRotor<T> ScalarBivectorPartsToEuclideanPureRotor()
        {
            Debug.Assert(Processor.IsEuclidean);

            return XGaPureRotor<T>.Create(
                GetScalarPart().ScalarValue,
                GetBivectorPart()
            );
        }
    }

    public sealed partial class XGaVector<T>
    {

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source vector
        /// into the target vector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetVector"></param>
        /// <returns></returns>
        public XGaPureScalingRotor<T> GetEuclideanPureScalingRotor(XGaVector<T> targetVector)
        {
            var uNorm = ENorm();
            var vNorm = targetVector.ENorm();
            var scalingFactor = (vNorm / uNorm).Sqrt().ScalarValue;
            var cosAngle = targetVector.ESp(this) / (uNorm * vNorm);

            if (cosAngle.IsOne)
                return XGaPureScalingRotor<T>.Create(Processor, scalingFactor);

            var rotationBlade =
                cosAngle.IsMinusOne
                    ? throw new InvalidOperationException()//sourceVector.GetNormalVector().Op(sourceVector)
                    : targetVector.Op(this);

            var unitRotationBlade =
                rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            Debug.Assert(scalingFactor != null, nameof(scalingFactor) + " != null");

            var scalarPart =
                scalingFactor * cosHalfAngle;

            var bivectorPart =
                scalingFactor * sinHalfAngle * unitRotationBlade;

            return XGaPureScalingRotor<T>.Create(
                scalarPart.ScalarValue,
                bivectorPart
            );
        }

        /// <summary>
        /// Create one rotor from the parametric family of pure rotors taking
        /// sourceVector to targetVector in 3D Euclidean space
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetVector"></param>
        /// <param name="angleTheta"></param>
        /// <returns></returns>
        public XGaPureRotor<T> CreateParametricPureRotor3D(XGaVector<T> targetVector, LinPolarAngle<T> angleTheta)
        {
            // Compute inverse of 3D pseudo-scalar = -e123
            var pseudoScalarInverse =
                Processor.PseudoScalarInverse(3);

            // Compute the smallest angle between source and target vectors
            var cosAngle0 =
                ESp(targetVector);

            // Define a rotor S with angle theta in the plane orthogonal to targetVector - sourceVector
            var rotorSBlade =
                (targetVector - this).EGp(
                    pseudoScalarInverse
                ).GetBivectorPart();

            var rotorS = rotorSBlade.GetEuclideanPureRotor(angleTheta);

            // Define parametric 2-blade of rotation
            // The actual plane of rotation is made by rotating the plane containing
            // sourceVector and targetVector by angle theta in the plane orthogonal to
            // targetVector - sourceVector using rotor S
            var rotorBlade =
                rotorS.OmMap(targetVector.Op(this));

            var sinAngleThetaSquare = angleTheta.Sin().Square();

            // Define parametric angle of rotation
            var rotorAngle =
                (1 + 2 * (cosAngle0 - 1) / (2 - sinAngleThetaSquare * (cosAngle0 + 1))).ArcCos();

            // Math.Acos(1 + 2 * (cosAngle0 - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (cosAngle0 + 1)));

            // Return the final rotor taking v1 into v2
            return rotorBlade.GetEuclideanPureRotor(rotorAngle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureScalingRotor<T> CreateParametricPureScalingRotor3D(XGaVector<T> targetVector, LinPolarAngle<T> angleTheta, T scalingFactor)
        {
            return CreateParametricPureRotor3D(targetVector, angleTheta)
                .CreatePureScalingRotor(scalingFactor);
        }


        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source basis vector
        /// into the target vector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceAxis"></param>
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public XGaPureScalingRotor<T> CreatePureScalingRotorFromAxis(LinBasisVector sourceAxis, bool assumeUnitVector = false)
        {
            var k = sourceAxis.Index;
            var vNorm = assumeUnitVector
                ? Processor.ScalarProcessor.OneValue
                : ENorm().ScalarValue;

            var ek = Processor.VectorTerm(k);

            var vk1 = vNorm! + (sourceAxis.IsPositive ? Scalar(k) : -Scalar(k));
            var vOpAxis = sourceAxis.IsPositive ? Op(ek) : ek.Op(this);

            return XGaPureScalingRotor<T>.Create(
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
        /// <returns></returns>
        public XGaPureScalingRotor<T> CreatePureScalingRotorToAxis(LinBasisVector targetAxis, bool assumeUnitVector = false)
        {
            var scalarProcessor = ScalarProcessor;

            var k = targetAxis.Index;
            var vNorm =
                assumeUnitVector
                    ? scalarProcessor.OneValue
                    : ENorm().ScalarValue;

            var vNorm2 =
                assumeUnitVector
                    ? scalarProcessor.Two
                    : scalarProcessor.Times(2, ENormSquared().ScalarValue);

            var ek = Processor.VectorTerm(k);

            Debug.Assert(vNorm != null, nameof(vNorm) + " != null");

            var vk1 = vNorm + (targetAxis.IsPositive ? Scalar(k) : -Scalar(k));
            var vOpAxis = targetAxis.IsPositive ? ek.Op(this) : Op(ek);

            return XGaPureScalingRotor<T>.Create(
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
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public XGaPureRotor<T> CreatePureRotorFromAxis(LinBasisVector sourceAxis, bool assumeUnitVector = false)
        {
            var k = sourceAxis.Index;

            var v =
                assumeUnitVector
                    ? this
                    : DivideByENorm();

            var ek = Processor.VectorTerm(k);

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
        /// <returns></returns>
        public XGaPureRotor<T> CreatePureRotorToAxis(LinBasisVector targetAxis, bool assumeUnitVector = false)
        {
            var k = targetAxis.Index;

            var v =
                assumeUnitVector
                    ? this
                    : DivideByENorm();

            var ek = Processor.VectorTerm(k);

            var vk1 = 1 + (targetAxis.IsPositive ? v.Scalar(k) : -v.Scalar(k));
            var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

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
        /// <returns></returns>
        public XGaPureRotor<T> CreatePureRotor(LinBasisVector targetAxis, bool assumeUnitVector = false)
        {
            var k = targetAxis.Index;

            var v =
                assumeUnitVector
                    ? this
                    : DivideByENorm();

            var ek = Processor.VectorTerm(k);

            var vk1 = 1 + (targetAxis.IsPositive ? v.Scalar(k) : -v.Scalar(k));
            var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

            return XGaPureRotor<T>.Create(
                (vk1 / 2).Sqrt().ScalarValue,
                vOpAxis / (vk1 * 2).Sqrt()
            );
        }

        public XGaPureRotorSequence<T> CreatePureRotorSequence(XGaVector<T> sourceVector2, XGaVector<T> targetVector1, XGaVector<T> targetVector2, bool assumeUnitVectors = false)
        {
            var rotor1 =
                GetEuclideanPureRotorTo(
                    targetVector1,
                    assumeUnitVectors
                );

            var rotor2 =
                rotor1.OmMap(sourceVector2).GetEuclideanPureRotorTo(
                    targetVector2,
                    assumeUnitVectors
                );

            //var rotor = 
            //    rotor2.Multivector.EGp(rotor1.Multivector);

            //var (scalar, bivector) = rotor.GetScalarBivectorParts();

            return XGaPureRotorSequence<T>.Create(rotor1, rotor2);
        }

        public XGaPureRotor<T> CreatePureRotor(XGaVector<T> inputVector2, XGaVector<T> rotatedVector1, XGaVector<T> rotatedVector2, int baseSpaceDimensions)
        {
            var inputFrame = XGaVectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(
                    this,
                    inputVector2
                );

            var rotatedFrame = XGaVectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(
                    rotatedVector1,
                    rotatedVector2
                );

            var rotor = XGaPureRotorSequence<T>.CreateFromEuclideanFrames(
                baseSpaceDimensions,
                inputFrame,
                rotatedFrame
            ).GetFinalRotor();

            var (scalar, bivector) = rotor.Multivector.GetScalarBivectorParts();

            return XGaPureRotor<T>.Create(scalar.ScalarValue, bivector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaDiagonalOutermorphism<T> CreateDiagonalAutomorphism()
        {
            return new XGaDiagonalOutermorphism<T>(this);
        }

        public XGaEuclideanScalingRotor2D<T> CreateEuclideanScalingRotor2D(XGaVector<T> targetVector)
        {
            Debug.Assert(
                ReferenceEquals(
                    ScalarProcessor,
                    targetVector.ScalarProcessor
                )
            );

            Debug.Assert(
                Metric.HasSameSignature(targetVector.Metric)
            );

            var u1 = Scalar(0);
            var u2 = Scalar(1);

            var v1 = targetVector.Scalar(0);
            var v2 = targetVector.Scalar(1);

            var vuDot = v1 * u1 + v2 * u2;
            var uNormSquared = u1 * u1 + u2 * u2;
            var vNormSquared = v1 * v1 + v2 * v2;

            var t1 = (vNormSquared / uNormSquared).Sqrt();
            var t2 = vuDot / uNormSquared;

            var vuWedgeScalar = (v1 * u2 - v2 * u1).Sign();

            var a0 = ((t1 + t2) / 2).Sqrt();
            var a12 = ((t1 - t2) / 2).Sqrt() * vuWedgeScalar;

            return XGaEuclideanScalingRotor2D<T>.Create(Processor, a0, a12);
        }

        public XGaEuclideanScalingRotorSquared2D<T> CreateEuclideanScalingRotorSquared2D(XGaVector<T> targetVector)
        {
            var u1 = Scalar(0);
            var u2 = Scalar(1);

            var v1 = targetVector.Scalar(0);
            var v2 = targetVector.Scalar(1);

            var uNormSquared = u1 * u1 + u2 * u2;

            var a0 = (v1 * u1 + v2 * u2) / uNormSquared;
            var a12 = (v1 * u2 - v2 * u1) / uNormSquared;

            return XGaEuclideanScalingRotorSquared2D<T>.Create(Processor, a0, a12);
        }


        public Pair<XGaVector<T>> ApplyGramSchmidt(XGaVector<T> v2, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2 }.ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Pair<XGaVector<T>>(zeroVector, zeroVector),
                1 => new Pair<XGaVector<T>>(vectorsList[0], zeroVector),
                _ => new Pair<XGaVector<T>>(vectorsList[0], vectorsList[1])
            };
        }

        public Triplet<XGaVector<T>> ApplyGramSchmidt(XGaVector<T> v2, XGaVector<T> v3, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3 }.ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Triplet<XGaVector<T>>(zeroVector, zeroVector, zeroVector),
                1 => new Triplet<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector),
                2 => new Triplet<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector),
                _ => new Triplet<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2])
            };
        }

        public Quad<XGaVector<T>> ApplyGramSchmidt(XGaVector<T> v2, XGaVector<T> v3, XGaVector<T> v4, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3, v4 }.ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Quad<XGaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quad<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector),
                2 => new Quad<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector),
                3 => new Quad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector),
                _ => new Quad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3])
            };
        }

        public Quint<XGaVector<T>> ApplyGramSchmidt(XGaVector<T> v2, XGaVector<T> v3, XGaVector<T> v4, XGaVector<T> v5, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3, v4, v5 }.ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Quint<XGaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quint<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector),
                3 => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector),
                4 => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector),
                _ => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4])
            };
        }

        public Hexad<XGaVector<T>> ApplyGramSchmidt(XGaVector<T> v2, XGaVector<T> v3, XGaVector<T> v4, XGaVector<T> v5, XGaVector<T> v6, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3, v4, v5, v6 }.ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Hexad<XGaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Hexad<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector, zeroVector),
                3 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector, zeroVector),
                4 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector, zeroVector),
                5 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], zeroVector),
                _ => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], vectorsList[5])
            };
        }

        public Pair<XGaVector<T>> ApplyGramSchmidtByProjections(XGaVector<T> v2, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2 }.ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Pair<XGaVector<T>>(zeroVector, zeroVector),
                1 => new Pair<XGaVector<T>>(vectorsList[0], zeroVector),
                _ => new Pair<XGaVector<T>>(vectorsList[0], vectorsList[1])
            };
        }

        public Triplet<XGaVector<T>> ApplyGramSchmidtByProjections(XGaVector<T> v2, XGaVector<T> v3, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3 }.ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Triplet<XGaVector<T>>(zeroVector, zeroVector, zeroVector),
                1 => new Triplet<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector),
                2 => new Triplet<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector),
                _ => new Triplet<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2])
            };
        }

        public Quad<XGaVector<T>> ApplyGramSchmidtByProjections(XGaVector<T> v2, XGaVector<T> v3, XGaVector<T> v4, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3, v4 }.ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Quad<XGaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quad<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector),
                2 => new Quad<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector),
                3 => new Quad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector),
                _ => new Quad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3])
            };
        }

        public Quint<XGaVector<T>> ApplyGramSchmidtByProjections(XGaVector<T> v2, XGaVector<T> v3, XGaVector<T> v4, XGaVector<T> v5, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3, v4, v5 }.ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Quint<XGaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quint<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector),
                3 => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector),
                4 => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector),
                _ => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4])
            };
        }

        public Hexad<XGaVector<T>> ApplyGramSchmidtByProjections(XGaVector<T> v2, XGaVector<T> v3, XGaVector<T> v4, XGaVector<T> v5, XGaVector<T> v6, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3, v4, v5, v6 }.ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Hexad<XGaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Hexad<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector, zeroVector),
                3 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector, zeroVector),
                4 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector, zeroVector),
                5 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], zeroVector),
                _ => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], vectorsList[5])
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVectorFrameFixed<T> GetBasisVectorFrameFixed(int vSpaceDimensions)
        {
            return Processor
                .CreateFreeFrameOfBasis(vSpaceDimensions)
                .CreateFixedFrame(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVectorFrameFixed<T> GetFixedFrameOfScaledBasis(int vSpaceDimensions, T scalingFactor)
        {
            return Processor
                .CreateFreeFrameOfScaledBasis(vSpaceDimensions, scalingFactor)
                .CreateFixedFrame(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVectorFrameFixed<T> GetFixedFrameOfSimplex(int vSpaceDimensions, T scalingFactor)
        {
            return Processor
                .CreateFreeFrameOfSimplex(vSpaceDimensions, scalingFactor)
                .CreateFixedFrame(this);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinPolarAngle<T> GetEuclideanAngle(XGaVector<T> vector2, bool assumeUnitVectors = false)
        {
            var angle = ESp(vector2).Scalar();

            if (!assumeUnitVectors)
                angle /= ENorm() * vector2.ENorm();

            return angle.ArcCos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> GetUnitBisector(XGaVector<T> vector2, bool assumeEqualNormVectors = false)
        {
            var v = assumeEqualNormVectors
                ? this + vector2
                : DivideByENorm() + vector2.DivideByENorm();

            return v.DivideByENorm();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> OmMapUsing(IXGaOutermorphism<T> om)
        {
            return om.OmMap(this);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureRotor<T> GetEuclideanPureRotorFromBasis(int index)
        {
            return Processor
                .VectorTerm(index)
                .GetEuclideanPureRotorTo(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureRotor<T> GetEuclideanPureRotorFrom(XGaVector<T> vector1)
        {
            return vector1.GetEuclideanPureRotorTo(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureRotor<T> GetEuclideanPureRotorFrom(XGaVector<T> vector1, bool assumeUnitVectors)
        {
            return vector1.GetEuclideanPureRotorTo(
                this,
                assumeUnitVectors
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureRotor<T> GetEuclideanPureRotorToBasis(int index)
        {
            return GetEuclideanPureRotorTo(
                Processor.VectorTerm(index)
            );
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source vector into the target vector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetVector"></param>
        /// <param name="assumeUnitVectors"></param>
        /// <returns></returns>
        public XGaPureRotor<T> GetEuclideanPureRotorTo(XGaVector<T> targetVector, bool assumeUnitVectors = false)
        {
            var cosAngle =
                assumeUnitVectors
                    ? targetVector.ESp(this)
                    : targetVector.ESp(this) / (targetVector.ENormSquared() * ENormSquared()).Sqrt();

            if (cosAngle.IsOne)
                return Processor.CreateIdentityRotor();

            var rotationBlade =
                cosAngle.IsMinusOne
                    ? throw new InvalidOperationException()//sourceVector.GetNormalVector().Op(sourceVector)
                    : targetVector.Op(this);

            var unitRotationBlade =
                rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var scalarPart = cosHalfAngle.ScalarValue;
            var bivectorPart = sinHalfAngle * unitRotationBlade;

            return XGaPureRotor<T>.Create(
                scalarPart,
                bivectorPart
            );
        }

        /// <summary>
        /// Find a Euclidean rotor from vector1 to its projection on subspace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subspace"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureRotor<T> GetEuclideanPureRotorTo(XGaSubspace<T> subspace)
        {
            return GetEuclideanPureRotorTo(
                subspace.Project(this)
            );
        }

    }

    public sealed partial class XGaBivector<T>
    {
        /// <summary>
        /// Create a simple rotor from an angle and a 2-blade
        /// </summary>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        public XGaPureRotor<T> GetEuclideanPureRotor(LinPolarAngle<T> rotationAngle)
        {
            var (cosHalfAngle, sinHalfAngle) =
                rotationAngle.HalfPolarAngle();

            var rotationBladeScalar =
                sinHalfAngle / (-ESpSquared()).Sqrt();

            return XGaPureRotor<T>.Create(
                cosHalfAngle.ScalarValue,
                rotationBladeScalar * this
            );
        }

        /// <summary>
        /// Create a pure rotor from a 2-blade, the signature of the blade
        /// is computed automatically using the given processor which must
        /// be of numerical type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public XGaPureRotor<T> ToPureRotor()
        {
            var processor = ScalarProcessor;

            if (!processor.IsNumeric)
                throw new InvalidOperationException();

            var bladeSignature = SpSquared();

            if (bladeSignature.IsNearZero())
                return XGaPureRotor<T>.Create(
                    processor.OneValue,
                    this
                );

            if (bladeSignature.IsNegative())
            {
                var alpha = (-bladeSignature).Sqrt();
                var scalar = alpha.Cos().ScalarValue;
                var bivector = alpha.Sin() / alpha * this;

                return XGaPureRotor<T>.Create(
                    scalar,
                    bivector
                );
            }
            else
            {
                var alpha = bladeSignature.Sqrt();
                var scalar = alpha.Cosh().ScalarValue;
                var bivector = alpha.Sinh() / alpha * this;

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
        /// <param name="bladeSignatureSign"></param>
        /// <returns></returns>
        public XGaPureRotor<T> ToPureRotor(IntegerSign bladeSignatureSign)
        {
            var processor = ScalarProcessor;

            if (bladeSignatureSign.IsZero)
                return XGaPureRotor<T>.Create(
                    processor.OneValue,
                    this
                );

            var bladeSignature = SpSquared();

            if (bladeSignatureSign.IsNegative)
            {
                var alpha = (-bladeSignature).Sqrt();
                var scalar = alpha.Cos().ScalarValue;
                var bivector = alpha.Sin() / alpha * this;

                return XGaPureRotor<T>.Create(
                    scalar,
                    bivector
                );
            }
            else
            {
                var alpha = bladeSignature.Sqrt();
                var scalar = alpha.Cosh().ScalarValue;
                var bivector = alpha.Sinh() / alpha * this;

                return XGaPureRotor<T>.Create(
                    scalar,
                    bivector
                );
            }
        }

    }

}
