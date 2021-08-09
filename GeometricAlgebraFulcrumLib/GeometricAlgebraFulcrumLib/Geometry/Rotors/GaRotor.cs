using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Frames;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Utils;

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
        
        public static GaRotor<T> Create(IGaProcessor<T> processor, IGaStorageMultivector<T> mv)
        {
            return new GaRotor<T>(processor, mv);
        }


        public static GaRotor<T> CreateSimpleRotor(IGaProcessor<T> processor, IGaStorageVector<T> sourceVector, IGaStorageVector<T> targetVector)
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
                        processor.Add(processor.OneScalar, cosAngle),
                        processor.IntegerToScalar(2)
                    )
                );

            var sinHalfAngle = 
                processor.Sqrt(
                    processor.Divide(
                        processor.Subtract(processor.OneScalar, cosAngle),
                        processor.IntegerToScalar(2)
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
        public static GaRotor<T> CreateSimpleRotor(IGaProcessor<T> processor, T rotationAngle, IGaStorageKVector<T> rotationBlade)
        {
            var halfRotationAngle = processor.Divide(rotationAngle, processor.IntegerToScalar(2));
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

        public static GaRotor<T> CreateSimpleRotor(IGaProcessor<T> processor, IGaStorageVector<T> inputVector1, IGaStorageVector<T> inputVector2, IGaStorageVector<T> rotatedVector1, IGaStorageVector<T> rotatedVector2)
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
        
        public static GaRotor<T> CreateSimpleRotor(IGaProcessor<T> processor, uint baseSpaceDimensions, IGaStorageVector<T> inputVector1, IGaStorageVector<T> inputVector2, IGaStorageVector<T> rotatedVector1, IGaStorageVector<T> rotatedVector2)
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

            var halfRotationAngle = processor.Divide(rotationAngle, processor.IntegerToScalar(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);

            var bladeId = (1UL << i) | (1UL << j);

            var composer = new GaStorageComposerMultivectorSparse<T>(processor);

            composer.SetTerm(0, cosHalfAngle);
            composer.SetTerm(bladeId, sinHalfAngle);

            return new GaRotor<T>(
                processor,
                composer.GetMultivector()
            );
        }


        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public ulong MaxBasisBladeId 
            => Processor.MaxBasisBladeId;

        public uint GradesCount 
            => Processor.GradesCount;

        public IEnumerable<uint> Grades 
            => Processor.Grades;

        public IGaScalarProcessor<T> ScalarProcessor 
            => Processor;

        public IGaStorageKVector<T> MappedPseudoScalar { get; }

        public IGaProcessor<T> Processor { get; }

        public IGaStorageMultivector<T> Multivector { get; }

        public IGaStorageMultivector<T> MultivectorReverse { get; }

        public bool IsValid 
            => true;

        public bool IsInvalid
            => false;


        private GaRotor([NotNull] IGaProcessor<T> processor, [NotNull] IGaStorageMultivector<T> mv)
        {
            Processor = processor;
            Multivector = mv;
            MultivectorReverse = processor.Reverse(Multivector);
            MappedPseudoScalar = processor.PseudoScalar;
        }

        private GaRotor([NotNull] IGaStorageMultivector<T> mv, [NotNull] IGaStorageMultivector<T> mvReverse)
        {
            Multivector = mv;
            MultivectorReverse = mvReverse;
        }


        public GaVector<T> Rotate(GaVector<T> vector)
        {
            return Processor.CreateGenericVector(
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

        public IGaStorageVector<T> MapBasisVector(int index)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<IGaStorageVector<T>> GetMappedBasisVectors()
        {
            throw new System.NotImplementedException();
        }

        public IGaStorageVector<T> MapBasisVector(ulong index)
        {
            throw new System.NotImplementedException();
        }

        public IGaStorageBivector<T> MapBasisBivector(int index1, int index2)
        {
            throw new System.NotImplementedException();
        }

        public IGaStorageBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            throw new System.NotImplementedException();
        }

        public IGaStorageKVector<T> MapBasisBlade(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public IGaStorageKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            throw new System.NotImplementedException();
        }

        public IGaStorageScalar<T> MapScalar(IGaStorageScalar<T> mv)
        {
            return mv.IsEmpty()
                ? GaStorageScalar<T>.ZeroScalar 
                : Processor.CreateStorageScalar(mv.FirstScalar);
        }

        public IGaStorageKVector<T> MapTerm(IGaStorageKVector<T> mv)
        {
            return Processor.Gp(Multivector, mv, MultivectorReverse).GetKVectorPart(mv.Grade);
        }

        public IGaStorageVector<T> MapVector(IGaStorageVector<T> mv)
        {
            return Processor.Gp(Multivector, mv, MultivectorReverse).GetVectorPart();
        }

        public IGaStorageBivector<T> MapBivector(IGaStorageBivector<T> mv)
        {
            return Processor.Gp(Multivector, mv, MultivectorReverse).GetBivectorPart();
        }

        public IGaStorageKVector<T> MapKVector(IGaStorageKVector<T> mv)
        {
            return Processor.Gp(Multivector, mv, MultivectorReverse).GetKVectorPart(mv.Grade);
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivectorGraded<T> mv)
        {
            return Processor.Gp(Multivector, mv, MultivectorReverse);
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivectorSparse<T> mv)
        {
            return Processor.Gp(Multivector, mv, MultivectorReverse);
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivector<T> mv)
        {
            return Processor.Gp(Multivector, mv, MultivectorReverse);
        }
    }
}