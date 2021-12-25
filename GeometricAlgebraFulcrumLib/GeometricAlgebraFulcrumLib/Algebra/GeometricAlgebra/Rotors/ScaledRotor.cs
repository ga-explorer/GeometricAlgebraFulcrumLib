using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
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
        public static ScaledRotor<T> CreateIdentity(IGeometricAlgebraProcessor<T> processor)
        {
            return new ScaledRotor<T>(
                processor, 
                processor.CreateKVectorBasisScalarStorage()
            );
        }
        
        public static ScaledRotor<T> Create(IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv)
        {
            return new ScaledRotor<T>(processor, mv);
        }

        public static ScaledRotor<T> CreateSimpleScaledRotor(IGeometricAlgebraProcessor<T> processor, VectorStorage<T> sourceVector, VectorStorage<T> targetVector)
        {
            var norm1 = processor.ENorm(sourceVector);
            var norm2 = processor.ENorm(targetVector);
            var cosAngle = processor.Divide(
                processor.ESp(sourceVector, targetVector), 
                processor.Times(norm1, norm2)
            );

            if (processor.IsOne(cosAngle))
                return CreateIdentity(processor);
            
            //TODO: Handle the case for cosAngle == -1
            if (processor.IsMinusOne(cosAngle))
                throw new InvalidOperationException();

            var cosHalfAngle = 
                processor.Sqrt(
                    processor.Divide(
                        processor.Add(processor.ScalarOne, cosAngle),
                        processor.GetScalarFromNumber(2)
                    )
                );

            var sinHalfAngle = 
                processor.Sqrt(
                    processor.Divide(
                        processor.Subtract(processor.ScalarOne, cosAngle),
                        processor.GetScalarFromNumber(2)
                    )
                );
            
            var rotationBlade = 
                processor.Op(sourceVector, targetVector);

            var rotationBladeScalar =
                processor.Divide(
                    sinHalfAngle,
                    processor.Sqrt(
                        processor.GetTermScalar(processor.Negative(processor.EGp(rotationBlade)), 0)
                    )
                );

            var rotorStorage = processor.Subtract(
                cosHalfAngle,
                processor.Times(rotationBladeScalar, rotationBlade)
            );
            
            //rotor.IsSimpleScaledRotor();

            return new ScaledRotor<T>(processor, rotorStorage);
        }

        /// <summary>
        /// Create a simple rotor from an angle and a blade
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="rotationAngle"></param>
        /// <param name="rotationBlade"></param>
        /// <returns></returns>
        public static ScaledRotor<T> CreateSimpleScaledRotor(IGeometricAlgebraProcessor<T> processor, T rotationAngle, KVectorStorage<T> rotationBlade)
        {
            var halfRotationAngle = processor.Divide(rotationAngle, processor.GetScalarFromNumber(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);

            var rotationBladeScalar =
                processor.Divide(
                    sinHalfAngle,
                    processor.Sqrt(
                        processor.GetTermScalar(processor.Negative(processor.EGp(rotationBlade)), 0)
                    )
                );

            var rotorStorage = processor.Add(
                cosHalfAngle,
                processor.Times(rotationBladeScalar, rotationBlade)
            );

            //rotor.IsSimpleScaledRotor();

            return new ScaledRotor<T>(processor, rotorStorage);
        }

        public static ScaledRotor<T> CreateSimpleScaledRotor(IGeometricAlgebraProcessor<T> processor, VectorStorage<T> inputVector1, VectorStorage<T> inputVector2, VectorStorage<T> rotatedVector1, VectorStorage<T> rotatedVector2)
        {
            var inputFrame = 
                processor.CreateFreeFrame(
                    GeoFreeFrameSpecs.CreateLinearlyIndependentSpecs(), 
                    inputVector1, 
                    inputVector2
                );

            var rotatedFrame = 
                processor.CreateFreeFrame(
                    GeoFreeFrameSpecs.CreateLinearlyIndependentSpecs(), 
                    rotatedVector1, 
                    rotatedVector2
                );

            return ScaledPureRotorsSequence<T>.CreateFromOrthonormalEuclideanFrames(
                inputFrame, 
                rotatedFrame, 
                true
            ).GetFinalScaledRotor();
        }
        
        public static ScaledRotor<T> CreateSimpleScaledRotor(IGeometricAlgebraProcessor<T> processor, uint baseSpaceDimensions, VectorStorage<T> inputVector1, VectorStorage<T> inputVector2, VectorStorage<T> rotatedVector1, VectorStorage<T> rotatedVector2)
        {
            var inputFrame = 
                processor.CreateFreeFrame(
                    GeoFreeFrameSpecs.CreateLinearlyIndependentSpecs(),
                    inputVector1, 
                    inputVector2
                );

            var rotatedFrame = 
                processor.CreateFreeFrame(
                    GeoFreeFrameSpecs.CreateLinearlyIndependentSpecs(),
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
        public static ScaledRotor<T> CreateGivensScaledRotor(IGeometricAlgebraProcessor<T> processor, int i, int j, T rotationAngle)
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
                processor,
                composer.CreateMultivectorSparseStorage()
            );
        }


        public IMultivectorStorage<T> Multivector { get; }

        public IMultivectorStorage<T> MultivectorReverse { get; }


        internal ScaledRotor(IGeometricAlgebraProcessor<T> processor, [NotNull] IMultivectorStorage<T> mv)
            : base(processor)
        {
            Multivector = mv;
            MultivectorReverse = processor.Reverse(Multivector);
        }

        private ScaledRotor(IGeometricAlgebraProcessor<T> processor, [NotNull] IMultivectorStorage<T> mv, [NotNull] IMultivectorStorage<T> mvReverse)
            : base(processor)
        {
            Multivector = mv;
            MultivectorReverse = mvReverse;
        }

        
        public override bool IsValid()
        {
            // Make sure the storage and its reverse are correct
            if (!GeometricProcessor.IsNearZero(GeometricProcessor.Subtract(GeometricProcessor.Reverse(Multivector), MultivectorReverse)))
                return false;

            // Make sure storage contains only terms of even grade
            if (!Multivector.GetGrades().All(g => g.IsEven()))
                return false;

            // Make sure storage gp reverse(storage) == 1
            var gp = 
                GeometricProcessor.Gp(Multivector, MultivectorReverse);

            return gp.IsScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalingFactor()
        {
            return ScalarProcessor.GetScalar(
                GeometricProcessor.Gp(
                    Multivector,
                    MultivectorReverse
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IScaledRotor<T> GetScaledRotorInverse()
        {
            return new ScaledRotor<T>(
                GeometricProcessor,
                MultivectorReverse, 
                Multivector
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorage<T> OmMapVector(VectorStorage<T> mv)
        {
            return GeometricProcessor
                .Gp(Multivector, mv, MultivectorReverse)
                .GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> OmMapBivector(BivectorStorage<T> mv)
        {
            return GeometricProcessor
                .Gp(Multivector, mv, MultivectorReverse)
                .GetBivectorPart();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> OmMapKVector(KVectorStorage<T> mv)
        {
            return GeometricProcessor
                .Gp(Multivector, mv, MultivectorReverse)
                .GetKVectorPart(mv.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> mv)
        {
            return GeometricProcessor
                .Gp(Multivector, mv, MultivectorReverse)
                .ToMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> mv)
        {
            return GeometricProcessor
                .Gp(Multivector, mv, MultivectorReverse)
                .ToMultivectorStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorage()
        {
            return Multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorInverseStorage()
        {
            return MultivectorReverse;
        }
    }
}