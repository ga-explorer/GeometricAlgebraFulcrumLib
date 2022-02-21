using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Geometry.Frames;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
{
    public class ScaledRotor<T> 
        : ScaledRotorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScaledRotor<T> CreateIdentity(IGeometricAlgebraProcessor<T> processor)
        {
            return new ScaledRotor<T>(
                processor.CreateKVectorStorageBasisScalar().CreateMultivector(processor)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScaledRotor<T> Create(Multivector<T> mv)
        {
            return new ScaledRotor<T>(mv);
        }

        public static ScaledRotor<T> CreateEuclideanScaledPureRotor(Vector<T> sourceVector, Vector<T> targetVector)
        {
            var norm1 = sourceVector.ENorm();
            var norm2 = targetVector.ENorm();
            var cosAngle = sourceVector.ESp(targetVector) / (norm1 * norm2);

            if (cosAngle.IsOne())
                return CreateIdentity(sourceVector.GeometricProcessor);
            
            //TODO: Handle the case for cosAngle == -1
            if (cosAngle.IsMinusOne())
                throw new InvalidOperationException();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();
            var rotationBlade = targetVector.Op(sourceVector);
            var rotationBladeScalar = sinHalfAngle / (-rotationBlade.ESp(rotationBlade)).Sqrt();
            var rotorStorage = cosHalfAngle + rotationBladeScalar * rotationBlade;
            
            //rotor.IsSimpleScaledRotor();

            return new ScaledRotor<T>(rotorStorage);
        }

        /// <summary>
        /// Create a simple rotor from an angle and a blade
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="rotationAngle"></param>
        /// <param name="rotationBlade"></param>
        /// <returns></returns>
        public static ScaledRotor<T> CreateEuclideanScaledPureRotor(IGeometricAlgebraProcessor<T> processor, T rotationAngle, KVector<T> rotationBlade)
        {
            var halfRotationAngle = processor.Divide(rotationAngle, processor.GetScalarFromNumber(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);
            var rotationBladeScalar = sinHalfAngle / (-rotationBlade.ESp(rotationBlade)).Sqrt();
            var rotorStorage = cosHalfAngle + rotationBladeScalar * rotationBlade;

            //rotor.IsSimpleScaledRotor();

            return new ScaledRotor<T>(rotorStorage);
        }

        public static ScaledRotor<T> CreateEuclideanScaledPureRotor(Vector<T> inputVector1, Vector<T> inputVector2, Vector<T> rotatedVector1, Vector<T> rotatedVector2)
        {
            var processor = inputVector1.GeometricProcessor;

            var inputFrame = 
                processor.CreateVectorFrame(
                    VectorFrameSpecs.CreateLinearlyIndependentSpecs(), 
                    inputVector1, 
                    inputVector2
                );

            var rotatedFrame = 
                processor.CreateVectorFrame(
                    VectorFrameSpecs.CreateLinearlyIndependentSpecs(), 
                    rotatedVector1, 
                    rotatedVector2
                );

            return ScaledPureRotorsSequence<T>.CreateFromOrthonormalEuclideanFrames(
                inputFrame, 
                rotatedFrame, 
                true
            ).GetFinalScaledRotor();
        }
        
        public static ScaledRotor<T> CreateEuclideanScaledPureRotor(uint baseSpaceDimensions, Vector<T> inputVector1, Vector<T> inputVector2, Vector<T> rotatedVector1, Vector<T> rotatedVector2)
        {
            var processor = inputVector1.GeometricProcessor;

            var inputFrame = 
                processor.CreateVectorFrame(
                    VectorFrameSpecs.CreateLinearlyIndependentSpecs(),
                    inputVector1, 
                    inputVector2
                );

            var rotatedFrame = 
                processor.CreateVectorFrame(
                    VectorFrameSpecs.CreateLinearlyIndependentSpecs(),
                    rotatedVector1, 
                    rotatedVector2
                );

            return ScaledPureRotorsSequence<T>.CreateFromEuclideanFrames(
                baseSpaceDimensions, 
                inputFrame, 
                rotatedFrame
            ).GetFinalScaledRotor();
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
        public static ScaledRotor<T> CreateScaledGivensRotor(IGeometricAlgebraProcessor<T> processor, int i, int j, T rotationAngle)
        {
            Debug.Assert(i >= 0 && j > i);

            var halfRotationAngle = processor.Divide(rotationAngle, processor.GetScalarFromNumber(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);

            var bladeId = BasisBivectorUtils.BasisVectorIndicesToBivectorId(i, j);

            var composer = processor.CreateVectorStorageComposer();

            composer.SetTerm(0, cosHalfAngle);
            composer.SetTerm(bladeId, sinHalfAngle);

            return new ScaledRotor<T>(
                composer.CreateMultivector()
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Multivector<T>(ScaledRotor<T> rotor)
        {
            return rotor.Multivector;
        }


        public Multivector<T> Multivector { get; }

        public Multivector<T> MultivectorReverse { get; }


        private ScaledRotor([NotNull] Multivector<T> mv)
            : base(mv.GeometricProcessor)
        {
            Multivector = mv;
            MultivectorReverse = mv.Reverse();
        }

        private ScaledRotor([NotNull] Multivector<T> mv, [NotNull] Multivector<T> mvReverse)
            : base(mv.GeometricProcessor)
        {
            Multivector = mv;
            MultivectorReverse = mvReverse;
        }

        
        public override bool IsValid()
        {
            // Make sure the storage and its reverse are correct
            if (!GeometricProcessor.IsNearZero(GeometricProcessor.Subtract(GeometricProcessor.Reverse(Multivector.MultivectorStorage), MultivectorReverse.MultivectorStorage)))
                return false;

            // Make sure storage contains only terms of even grade
            if (!Multivector.GetGrades().All(g => g.IsEven()))
                return false;

            // Make sure storage gp reverse(storage) == 1
            var gp = 
                Multivector.Gp(MultivectorReverse);

            return gp.IsScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalingFactor()
        {
            return Multivector.Sp(MultivectorReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IScaledRotor<T> GetScaledRotorInverse()
        {
            return new ScaledRotor<T>(
                MultivectorReverse, 
                Multivector
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector<T> OmMap(Vector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Bivector<T> OmMap(Bivector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetBivectorPart();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVector<T> OmMap(KVector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetKVectorPart(mv.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> OmMap(Multivector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> GetMultivector()
        {
            return Multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> GetMultivectorReverse()
        {
            return MultivectorReverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> GetMultivectorInverse()
        {
            return MultivectorReverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorage()
        {
            return Multivector.MultivectorStorage;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorageReverse()
        {
            return MultivectorReverse.MultivectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorageInverse()
        {
            return MultivectorReverse.MultivectorStorage;
        }
    }
}