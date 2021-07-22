using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Multivectors;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean
{
    public sealed class GaEuclideanSimpleRotor<T>
        : IGaEuclideanGeometry<T>, IGaRotor<T>
    {
        public static GaEuclideanSimpleRotor<T> CreateIdentity(IGaProcessor<T> processor)
        {
            return new GaEuclideanSimpleRotor<T>(
                processor,
                processor.CreateBasisScalar()
            );
        }
        
        public static GaEuclideanSimpleRotor<T> Create(IGaProcessor<T> processor, IGasMultivector<T> storage)
        {
            return new GaEuclideanSimpleRotor<T>(processor, storage);
        }

        public static GaEuclideanSimpleRotor<T> Create(IGaProcessor<T> processor, IGasVector<T> sourceVector, IGasVector<T> targetVector)
        {
            var norm1 = sourceVector.ENorm();
            var norm2 = targetVector.ENorm();
            var cosAngle = processor.Divide(
                sourceVector.ESp(targetVector), 
                processor.Times(norm1, norm2)
            );

            if (processor.IsZero(processor.Subtract(cosAngle, processor.OneScalar)))
                return CreateIdentity(processor);
            
            //TODO: Handle the case for cosAngle == -1

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
                sourceVector.Op(targetVector);

            var rotationBladeScalar =
                processor.Divide(
                    sinHalfAngle,
                    processor.Sqrt(
                        processor.Negative(
                            rotationBlade.EGp().GetTermScalar(0)
                        )
                    )
                    //processor.SqrtOfAbs(rotationBlade.EGpSquared().GetTermScalar(0))
                );

            var rotorStorage = cosHalfAngle.Subtract(
                rotationBladeScalar.Times(rotationBlade)
            );
            
            //rotor.IsSimpleRotor();

            return new GaEuclideanSimpleRotor<T>(processor, rotorStorage);
        }

        /// <summary>
        /// Create a simple rotor from an angle and a blade
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="rotationAngle"></param>
        /// <param name="rotationBlade"></param>
        /// <returns></returns>
        public static GaEuclideanSimpleRotor<T> Create(IGaProcessor<T> processor, T rotationAngle, IGasKVector<T> rotationBlade)
        {
            if (rotationBlade.Grade != 2)
                throw new InvalidOperationException();

            var halfRotationAngle = processor.Divide(rotationAngle, processor.IntegerToScalar(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);

            var rotationBladeScalar =
                processor.Divide(
                    sinHalfAngle,
                    processor.SqrtOfAbs(rotationBlade.EGp().GetTermScalar(0))
                );

            var rotorStorage = cosHalfAngle.Add(
                rotationBladeScalar.Times(rotationBlade)
            );

            //rotor.IsSimpleRotor();

            return new GaEuclideanSimpleRotor<T>(processor, rotorStorage);
        }

        public static GaEuclideanSimpleRotor<T> Create(IGaProcessor<T> processor, IGasVector<T> inputVector1, IGasVector<T> inputVector2, IGasVector<T> rotatedVector1, IGasVector<T> rotatedVector2)
        {
            var inputFrame = 
                GaVectorsFrame<T>.Create(
                    processor, 
                    inputVector1, inputVector2
                );

            var rotatedFrame = GaVectorsFrame<T>.Create(
                processor, 
                rotatedVector1, rotatedVector2
            );

            var rotor = GaEuclideanSimpleRotorsSequence<T>.CreateFromOrthonormalFrames(
                inputFrame, 
                rotatedFrame, 
                true
            ).GetFinalRotor();

            return new GaEuclideanSimpleRotor<T>(
                processor, 
                rotor.Rotor, 
                rotor.RotorReverse
            );
        }
        
        public static GaEuclideanSimpleRotor<T> Create(IGaProcessor<T> processor, uint baseSpaceDimensions, IGasVector<T> inputVector1, IGasVector<T> inputVector2, IGasVector<T> rotatedVector1, IGasVector<T> rotatedVector2)
        {
            var inputFrame = GaVectorsFrame<T>.Create(
                processor,
                inputVector1, inputVector2
            );

            var rotatedFrame = GaVectorsFrame<T>.Create(
                processor,
                rotatedVector1, rotatedVector2
            );

            var rotor = GaEuclideanSimpleRotorsSequence<T>.CreateFromFrames(
                baseSpaceDimensions, 
                inputFrame, 
                rotatedFrame
            ).GetFinalRotor();

            return new GaEuclideanSimpleRotor<T>(
                processor,
                rotor.Rotor,
                rotor.RotorReverse
            );
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
        public static GaEuclideanSimpleRotor<T> CreateGivensRotor(IGaProcessor<T> processor, int i, int j, T rotationAngle)
        {
            Debug.Assert(i >= 0 && j > i);

            var halfRotationAngle = processor.Divide(rotationAngle, processor.IntegerToScalar(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);

            var bladeId = (1UL << i) | (1UL << j);

            var composer = new GaMultivectorTermsStorageComposer<T>(processor);

            composer.SetTerm(0, cosHalfAngle);
            composer.SetTerm(bladeId, sinHalfAngle);

            return new GaEuclideanSimpleRotor<T>(
                processor,
                composer.GetCompactMultivector()
            );
        }


        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public ulong MaxBasisBladeId { get; }

        public uint GradesCount { get; }
        
        public IEnumerable<uint> Grades { get; }

        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IGasKVector<T> MappedPseudoScalar { get; }

        public IGaProcessor<T> Processor { get; }

        public IGasMultivector<T> Rotor { get; }

        public IGasMultivector<T> RotorReverse { get; }

        public bool IsValid
        {
            get
            {
                // Make sure the storage and its reverse are correct
                if (!Rotor.GetReverse().Subtract(RotorReverse).IsNearZero())
                    return false;

                // Make sure storage contains only terms of grades 0,2
                if ((Rotor.GetStoredGradesBitPattern() | 5UL) != 5UL)
                    return false;

                // Make sure storage gp reverse(storage) == 1
                var gp = Rotor.EGp(RotorReverse);

                if (!gp.IsScalar())
                    return false;

                var diff =
                    Processor.Subtract(
                        gp.GetTermScalar(0),
                        Processor.OneScalar
                    );

                if (!Processor.IsNearZero(diff))
                    return false;

                return true;
            }
        }

        public bool IsInvalid 
            => !IsValid;


        private GaEuclideanSimpleRotor([NotNull] IGaProcessor<T> processor, [NotNull] IGasMultivector<T> storage)
        {
            Processor = processor;
            Rotor = storage;
            RotorReverse = Rotor.GetReverse();
        }

        private GaEuclideanSimpleRotor([NotNull] IGaProcessor<T> processor, [NotNull] IGasMultivector<T> rotor, [NotNull] IGasMultivector<T> rotorReverse)
        {
            Processor = processor;
            Rotor = rotor;
            RotorReverse = rotorReverse;
        }


        public GaVector<T> Map(GaVector<T> vector)
        {
            return GaVector<T>.Create(
                Processor,
                MapVector(vector.Storage)
            );
        }

        public GaEuclideanSimpleRotor<T> GetReverseRotor()
        {
            return new GaEuclideanSimpleRotor<T>(
                Processor, 
                RotorReverse, 
                Rotor
            );
        }
        

        public IGaOutermorphism<T> GetAdjoint()
        {
            return new GaEuclideanSimpleRotor<T>(
                Processor,
                RotorReverse, 
                Rotor
            );
        }

        public IGasVector<T> MapBasisVector(int index)
        {
            return MapVector(
                Processor.CreateVector(index, 
                    Processor.OneScalar
                )
            );
        }

        public IReadOnlyList<IGasVector<T>> GetMappedBasisVectors()
        {
            throw new NotImplementedException();
        }

        public IGasVector<T> MapBasisVector(ulong index)
        {
            return MapVector(
                Processor.CreateVector(index, 
                    Processor.OneScalar
                )
            );
        }

        public IGasBivector<T> MapBasisBivector(int index1, int index2)
        {
            return MapBivector(
                Processor.CreateBivector(index1, 
                    index2,
                    Processor.OneScalar
                )
            );
        }

        public IGasBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return MapBivector(
                Processor.CreateBivector(index1, 
                    index2,
                    Processor.OneScalar
                )
            );
        }

        public IGasKVector<T> MapBasisBlade(ulong id)
        {
            return MapTerm(
                Processor.CreateKVector(id, 
                    Processor.OneScalar
                )
            );
        }

        public IGasKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            return MapTerm(
                Processor.CreateKVector(grade,
                    index, 
                    Processor.OneScalar
                )
            );
        }

        public IGasScalar<T> MapScalar(IGasScalar<T> storage)
        {
            return Processor.CreateScalar(
                storage.Scalar
            );
        }

        public IGasKVector<T> MapTerm(IGasKVectorTerm<T> storage)
        {
            return Rotor.EGp(storage).EGp(RotorReverse).GetKVectorPart(storage.Grade);
        }

        public IGasVector<T> MapVector(IGasVector<T> storage)
        {
            return Rotor.EGp(storage).EGp(RotorReverse).GetVectorPart();
        }

        public IGasBivector<T> MapBivector(IGasBivector<T> storage)
        {
            return Rotor.EGp(storage).EGp(RotorReverse).GetBivectorPart();
        }

        public IGasKVector<T> MapKVector(IGasKVector<T> storage)
        {
            return Rotor.EGp(storage).EGp(RotorReverse).GetKVectorPart(storage.Grade);
        }

        public IGasMultivector<T> MapMultivector(IGasGradedMultivector<T> storage)
        {
            return Rotor.EGp(storage).EGp(RotorReverse);
        }

        public IGasMultivector<T> MapMultivector(IGasTermsMultivector<T> storage)
        {
            return Rotor.EGp(storage).EGp(RotorReverse);
        }

        public IGasMultivector<T> MapMultivector(IGasMultivector<T> storage)
        {
            return Rotor.EGp(storage).EGp(RotorReverse);
        }
    }
}