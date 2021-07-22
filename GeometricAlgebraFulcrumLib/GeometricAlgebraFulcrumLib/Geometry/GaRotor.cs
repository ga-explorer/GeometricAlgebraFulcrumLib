using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Geometry.Multivectors;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public sealed class GaRotor<T> 
        : IGaEuclideanGeometry<T>, IGaRotor<T>
    {
        public static GaRotor<T> CreateIdentity(IGaProcessor<T> processor)
        {
            return new GaRotor<T>(
                processor, 
                processor.CreateBasisScalar()
            );
        }
        
        public static GaRotor<T> Create(IGaProcessor<T> processor, IGasMultivector<T> mv)
        {
            return new GaRotor<T>(processor, mv);
        }


        public static GaRotor<T> CreateSimpleRotor(IGaProcessor<T> processor, IGasVector<T> sourceVector, IGasVector<T> targetVector)
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
                    processor.SqrtOfAbs(rotationBlade.EGp().GetTermScalar(0))
                );

            var rotorStorage = cosHalfAngle.Subtract(
                rotationBladeScalar.Times(rotationBlade)
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
        public static GaRotor<T> CreateSimpleRotor(IGaProcessor<T> processor, T rotationAngle, IGasKVector<T> rotationBlade)
        {
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

            return new GaRotor<T>(processor, rotorStorage);
        }

        public static GaRotor<T> CreateSimpleRotor(IGaProcessor<T> processor, IGasVector<T> inputVector1, IGasVector<T> inputVector2, IGasVector<T> rotatedVector1, IGasVector<T> rotatedVector2)
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

            return GaEuclideanSimpleRotorsSequence<T>.CreateFromOrthonormalFrames(
                inputFrame, 
                rotatedFrame, 
                true
            ).GetFinalRotor();
        }
        
        public static GaRotor<T> CreateSimpleRotor(IGaProcessor<T> processor, uint baseSpaceDimensions, IGasVector<T> inputVector1, IGasVector<T> inputVector2, IGasVector<T> rotatedVector1, IGasVector<T> rotatedVector2)
        {
            var inputFrame = GaVectorsFrame<T>.Create(
                processor,
                inputVector1, inputVector2
            );

            var rotatedFrame = GaVectorsFrame<T>.Create(
                processor,
                rotatedVector1, rotatedVector2
            );

            return GaEuclideanSimpleRotorsSequence<T>.CreateFromFrames(
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

            var composer = new GaMultivectorTermsStorageComposer<T>(processor);

            composer.SetTerm(0, cosHalfAngle);
            composer.SetTerm(bladeId, sinHalfAngle);

            return new GaRotor<T>(
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
            => true;

        public bool IsInvalid
            => false;


        private GaRotor([NotNull] IGaProcessor<T> processor, [NotNull] IGasMultivector<T> mv)
        {
            Processor = processor;
            Rotor = mv;
            RotorReverse = Rotor.GetReverse();
        }

        private GaRotor([NotNull] IGasMultivector<T> mv, [NotNull] IGasMultivector<T> mvReverse)
        {
            Rotor = mv;
            RotorReverse = mvReverse;
        }


        public GaVector<T> Rotate(GaVector<T> vector)
        {
            return GaVector<T>.Create(
                Processor,
                MapVector(vector.Storage).GetVectorPart()
            );
        }

        public GaRotor<T> GetReverseRotor()
        {
            return new GaRotor<T>(
                RotorReverse, 
                Rotor
            );
        }
        

        public IGaOutermorphism<T> GetAdjoint()
        {
            return new GaRotor<T>(
                RotorReverse, 
                Rotor
            );
        }

        public IGasVector<T> MapBasisVector(int index)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<IGasVector<T>> GetMappedBasisVectors()
        {
            throw new System.NotImplementedException();
        }

        public IGasVector<T> MapBasisVector(ulong index)
        {
            throw new System.NotImplementedException();
        }

        public IGasBivector<T> MapBasisBivector(int index1, int index2)
        {
            throw new System.NotImplementedException();
        }

        public IGasBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            throw new System.NotImplementedException();
        }

        public IGasKVector<T> MapBasisBlade(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public IGasKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            throw new System.NotImplementedException();
        }

        public IGasScalar<T> MapScalar(IGasScalar<T> mv)
        {
            return Processor.CreateScalar(mv.Scalar);
        }

        public IGasKVector<T> MapTerm(IGasKVectorTerm<T> mv)
        {
            return Processor.Gp(Rotor, mv, RotorReverse).GetKVectorPart(mv.Grade);
        }

        public IGasVector<T> MapVector(IGasVector<T> mv)
        {
            return Processor.Gp(Rotor, mv, RotorReverse).GetVectorPart();
        }

        public IGasBivector<T> MapBivector(IGasBivector<T> mv)
        {
            return Processor.Gp(Rotor, mv, RotorReverse).GetBivectorPart();
        }

        public IGasKVector<T> MapKVector(IGasKVector<T> mv)
        {
            return Processor.Gp(Rotor, mv, RotorReverse).GetKVectorPart(mv.Grade);
        }

        public IGasMultivector<T> MapMultivector(IGasGradedMultivector<T> mv)
        {
            return Processor.Gp(Rotor, mv, RotorReverse);
        }

        public IGasMultivector<T> MapMultivector(IGasTermsMultivector<T> mv)
        {
            return Processor.Gp(Rotor, mv, RotorReverse);
        }

        public IGasMultivector<T> MapMultivector(IGasMultivector<T> mv)
        {
            return Processor.Gp(Rotor, mv, RotorReverse);
        }
    }
}