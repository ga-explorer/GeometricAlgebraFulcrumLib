using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Frames;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Rotors
{
    public sealed class GaRotor<T> 
        : IGaGeometry<T>, IGaRotor<T>
    {
        public static GaRotor<T> CreateIdentity(IGaProcessor<T> processor)
        {
            return new GaRotor<T>(
                processor, 
                processor.CreateStorageBasisScalar()
            );
        }
        
        public static GaRotor<T> Create(IGaProcessor<T> processor, IGaMultivectorStorage<T> mv)
        {
            return new GaRotor<T>(processor, mv);
        }


        public static GaRotor<T> CreateSimpleRotor(IGaProcessor<T> processor, IGaVectorStorage<T> sourceVector, IGaVectorStorage<T> targetVector)
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
                        processor.GetScalarFromInteger(2)
                    )
                );

            var sinHalfAngle = 
                processor.Sqrt(
                    processor.Divide(
                        processor.Subtract(processor.ScalarOne, cosAngle),
                        processor.GetScalarFromInteger(2)
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
            
            //rotor.IsSimpleRotor();

            return new GaRotor<T>(processor, rotorStorage);
        }

        /// <summary>
        /// Create a simple rotor from an angle and a blade
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="rotationAngle"></param>
        /// <param name="rotationBlade"></param>
        /// <returns></returns>
        public static GaRotor<T> CreateSimpleRotor(IGaProcessor<T> processor, T rotationAngle, IGaKVectorStorage<T> rotationBlade)
        {
            var halfRotationAngle = processor.Divide(rotationAngle, processor.GetScalarFromInteger(2));
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

            //rotor.IsSimpleRotor();

            return new GaRotor<T>(processor, rotorStorage);
        }

        public static GaRotor<T> CreateSimpleRotor(IGaProcessor<T> processor, IGaVectorStorage<T> inputVector1, IGaVectorStorage<T> inputVector2, IGaVectorStorage<T> rotatedVector1, IGaVectorStorage<T> rotatedVector2)
        {
            var inputFrame = 
                processor.CreateVectorsFrame(
                    GaVectorsFrameKind.LinearlyIndependent, 
                    inputVector1, 
                    inputVector2
                );

            var rotatedFrame = 
                processor.CreateVectorsFrame(
                    GaVectorsFrameKind.LinearlyIndependent, 
                    rotatedVector1, 
                    rotatedVector2
                );

            return GaPureRotorsSequence<T>.CreateFromOrthonormalFrames(
                inputFrame, 
                rotatedFrame, 
                true
            ).GetFinalRotor();
        }
        
        public static GaRotor<T> CreateSimpleRotor(IGaProcessor<T> processor, uint baseSpaceDimensions, IGaVectorStorage<T> inputVector1, IGaVectorStorage<T> inputVector2, IGaVectorStorage<T> rotatedVector1, IGaVectorStorage<T> rotatedVector2)
        {
            var inputFrame = 
                processor.CreateVectorsFrame(
                    GaVectorsFrameKind.LinearlyIndependent,
                    inputVector1, 
                    inputVector2
                );

            var rotatedFrame = 
                processor.CreateVectorsFrame(
                    GaVectorsFrameKind.LinearlyIndependent,
                    rotatedVector1, 
                    rotatedVector2
                );

            return GaPureRotorsSequence<T>.CreateFromFrames(
                baseSpaceDimensions, 
                inputFrame, 
                rotatedFrame
            ).GetFinalRotor();
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
        public static GaRotor<T> CreateGivensRotor(IGaProcessor<T> processor, int i, int j, T rotationAngle)
        {
            Debug.Assert(i >= 0 && j > i);

            var halfRotationAngle = processor.Divide(rotationAngle, processor.GetScalarFromInteger(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);

            var bladeId = GaBasisBivectorUtils.BasisBivectorId(i, j);

            var composer = processor.CreateStorageSparseMultivectorComposer();

            composer.SetTerm(0, cosHalfAngle);
            composer.SetTerm(bladeId, sinHalfAngle);

            return new GaRotor<T>(
                processor,
                composer.CreateGaMultivectorSparseStorage()
            );
        }


        public IGaSpace Space => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public ILaProcessor<T> ScalarsGridProcessor 
            => Processor;

        public IGaKVectorStorage<T> MappedPseudoScalar { get; }

        public IGaProcessor<T> Processor { get; }

        public IGaMultivectorStorage<T> Multivector { get; }

        public IGaMultivectorStorage<T> MultivectorReverse { get; }

        public bool IsValid 
            => true;

        public bool IsInvalid
            => false;


        private GaRotor([NotNull] IGaProcessor<T> processor, [NotNull] IGaMultivectorStorage<T> mv)
        {
            Processor = processor;
            Multivector = mv;
            MultivectorReverse = processor.Reverse(Multivector);
            MappedPseudoScalar = processor.PseudoScalar;
        }

        private GaRotor([NotNull] IGaMultivectorStorage<T> mv, [NotNull] IGaMultivectorStorage<T> mvReverse)
        {
            Multivector = mv;
            MultivectorReverse = mvReverse;
        }


        public GaVector<T> Rotate(GaVector<T> vector)
        {
            return Processor.CreateGaVector(
                MapVector(vector.VectorStorage).GetVectorPart()
            );
        }

        public GaRotor<T> GetReverseRotor()
        {
            return new GaRotor<T>(
                MultivectorReverse, 
                Multivector
            );
        }
        

        public IGaOutermorphism<T> GetAdjoint()
        {
            return new GaRotor<T>(
                MultivectorReverse, 
                Multivector
            );
        }

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors()
        {
            throw new NotImplementedException();
        }

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            throw new NotImplementedException();
        }

        public IGaBivectorStorage<T> MapBasisBivector(int index1, int index2)
        {
            throw new NotImplementedException();
        }

        public IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            throw new NotImplementedException();
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            throw new NotImplementedException();
        }

        public IGaKVectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            throw new NotImplementedException();
        }

        public IGaScalarStorage<T> MapScalar(IGaScalarStorage<T> mv)
        {
            return mv.TryGetScalarPart(out var scalar)
                ? scalar
                : GaScalarStorage<T>.ZeroScalar;
        }

        public IGaKVectorStorage<T> MapTerm(IGaKVectorStorage<T> mv)
        {
            return Processor.Gp(Multivector, mv, MultivectorReverse).GetKVectorPart(mv.Grade);
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> mv)
        {
            return Processor.Gp(Multivector, mv, MultivectorReverse).GetVectorPart();
        }

        public IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> mv)
        {
            return Processor.Gp(Multivector, mv, MultivectorReverse).GetBivectorPart();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> mv)
        {
            return Processor.Gp(Multivector, mv, MultivectorReverse).GetKVectorPart(mv.Grade);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorGradedStorage<T> mv)
        {
            return Processor.Gp(Multivector, mv, MultivectorReverse);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorSparseStorage<T> mv)
        {
            return Processor.Gp(Multivector, mv, MultivectorReverse);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> mv)
        {
            return Processor.Gp(Multivector, mv, MultivectorReverse);
        }
    }
}