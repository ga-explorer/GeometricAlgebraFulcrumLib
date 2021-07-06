using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean
{
    public sealed class GaEuclideanRotor<T> 
        : IGaEuclideanGeometry<T>, IGaRotor<T>
    {
        public static GaEuclideanRotor<T> CreateIdentity(IGaScalarProcessor<T> scalarProcessor)
        {
            return new(
                GaScalarTermStorage<T>.CreateBasisScalar(scalarProcessor)
            );
        }
        
        public static GaEuclideanRotor<T> Create(IGaMultivectorStorage<T> storage)
        {
            return new(storage);
        }


        public static GaEuclideanRotor<T> CreateSimpleRotor(IGaVectorStorage<T> sourceVector, IGaVectorStorage<T> targetVector)
        {
            var scalarProcessor = sourceVector.ScalarProcessor;

            var norm1 = sourceVector.ENorm();
            var norm2 = targetVector.ENorm();
            var cosAngle = scalarProcessor.Divide(
                sourceVector.ESp(targetVector), 
                scalarProcessor.Times(norm1, norm2)
            );

            if (scalarProcessor.IsZero(scalarProcessor.Subtract(cosAngle, scalarProcessor.OneScalar)))
                return CreateIdentity(scalarProcessor);
            
            //TODO: Handle the case for cosAngle == -1

            var cosHalfAngle = 
                scalarProcessor.Sqrt(
                    scalarProcessor.Divide(
                        scalarProcessor.Add(scalarProcessor.OneScalar, cosAngle),
                        scalarProcessor.IntegerToScalar(2)
                    )
                );

            var sinHalfAngle = 
                scalarProcessor.Sqrt(
                    scalarProcessor.Divide(
                        scalarProcessor.Subtract(scalarProcessor.OneScalar, cosAngle),
                        scalarProcessor.IntegerToScalar(2)
                    )
                );
            
            var rotationBlade = 
                sourceVector.Op(targetVector);

            var rotationBladeScalar =
                scalarProcessor.Divide(
                    sinHalfAngle,
                    scalarProcessor.SqrtOfAbs(rotationBlade.EGp().GetTermScalar(0))
                );

            var rotorStorage = cosHalfAngle.Subtract(
                rotationBladeScalar.Times(rotationBlade)
            );
            
            //rotor.IsSimpleRotor();

            return new GaEuclideanRotor<T>(rotorStorage);
        }

        /// <summary>
        /// Create a simple rotor from an angle and a blade
        /// </summary>
        /// <param name="rotationAngle"></param>
        /// <param name="rotationBlade"></param>
        /// <returns></returns>
        public static GaEuclideanRotor<T> CreateSimpleRotor(T rotationAngle, IGaKVectorStorage<T> rotationBlade)
        {
            var scalarProcessor = rotationBlade.ScalarProcessor;

            var halfRotationAngle = scalarProcessor.Divide(rotationAngle, scalarProcessor.IntegerToScalar(2));
            var cosHalfAngle = scalarProcessor.Cos(halfRotationAngle);
            var sinHalfAngle = scalarProcessor.Sin(halfRotationAngle);

            var rotationBladeScalar =
                scalarProcessor.Divide(
                    sinHalfAngle,
                    scalarProcessor.SqrtOfAbs(rotationBlade.EGp().GetTermScalar(0))
                );

            var rotorStorage = cosHalfAngle.Add(
                rotationBladeScalar.Times(rotationBlade)
            );

            //rotor.IsSimpleRotor();

            return new GaEuclideanRotor<T>(rotorStorage);
        }

        public static GaEuclideanRotor<T> CreateSimpleRotor(IGaVectorStorage<T> inputVector1, IGaVectorStorage<T> inputVector2, IGaVectorStorage<T> rotatedVector1, IGaVectorStorage<T> rotatedVector2)
        {
            var scalarProcessor = inputVector1.ScalarProcessor;

            var inputFrame = 
                GaEuclideanVectorsFrame<T>.Create(
                    scalarProcessor, 
                    inputVector1, inputVector2
                );

            var rotatedFrame = GaEuclideanVectorsFrame<T>.Create(
                scalarProcessor, 
                rotatedVector1, rotatedVector2
            );

            return GaEuclideanSimpleRotorsSequence<T>.CreateFromOrthonormalFrames(
                inputFrame, 
                rotatedFrame, 
                true
            ).GetFinalRotor();
        }
        
        public static GaEuclideanRotor<T> CreateSimpleRotor(int baseSpaceDimensions, IGaVectorStorage<T> inputVector1, IGaVectorStorage<T> inputVector2, IGaVectorStorage<T> rotatedVector1, IGaVectorStorage<T> rotatedVector2)
        {
            var scalarProcessor = inputVector1.ScalarProcessor;

            var inputFrame = GaEuclideanVectorsFrame<T>.Create(
                scalarProcessor,
                inputVector1, inputVector2
            );

            var rotatedFrame = GaEuclideanVectorsFrame<T>.Create(
                scalarProcessor,
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
        /// <param name="scalarProcessor"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        public static GaEuclideanRotor<T> CreateGivensRotor(IGaScalarProcessor<T> scalarProcessor, int i, int j, T rotationAngle)
        {
            Debug.Assert(i >= 0 && j > i);

            var halfRotationAngle = scalarProcessor.Divide(rotationAngle, scalarProcessor.IntegerToScalar(2));
            var cosHalfAngle = scalarProcessor.Cos(halfRotationAngle);
            var sinHalfAngle = scalarProcessor.Sin(halfRotationAngle);

            var bladeId = (1UL << i) | (1UL << j);

            var composer = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            composer.SetTerm(0, cosHalfAngle);
            composer.SetTerm(bladeId, sinHalfAngle);

            return new GaEuclideanRotor<T>(
                composer.GetCompactStorage()
            );
        }


        public IGaScalarProcessor<T> ScalarProcessor 
            => Storage.ScalarProcessor;

        public IGaMultivectorStorage<T> Storage { get; }

        public IGaMultivectorStorage<T> StorageReverse { get; }

        public bool IsValid 
            => true;

        public bool IsInvalid
            => false;


        private GaEuclideanRotor([NotNull] IGaMultivectorStorage<T> storage)
        {
            Storage = storage;
            StorageReverse = Storage.GetReverse();
        }

        private GaEuclideanRotor([NotNull] IGaMultivectorStorage<T> storage, [NotNull] IGaMultivectorStorage<T> storageReverse)
        {
            Storage = storage;
            StorageReverse = storageReverse;
        }


        public GaEuclideanVector<T> Rotate(GaEuclideanVector<T> vector)
        {
            return GaEuclideanVector<T>.Create(
                MapVector(vector.Storage).GetVectorPart()
            );
        }

        public GaEuclideanRotor<T> GetReverseRotor()
        {
            return new GaEuclideanRotor<T>(
                StorageReverse, 
                Storage
            );
        }
        

        public IGaVectorsLinearMap<T> GetAdjoint()
        {
            return new GaEuclideanRotor<T>(
                StorageReverse, 
                Storage
            );
        }

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            throw new System.NotImplementedException();
        }

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            throw new System.NotImplementedException();
        }

        public IGaBivectorStorage<T> MapBasisBivector(int index1, int index2)
        {
            throw new System.NotImplementedException();
        }

        public IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            throw new System.NotImplementedException();
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public IGaKVectorStorage<T> MapBasisBlade(int grade, ulong index)
        {
            throw new System.NotImplementedException();
        }

        public IGaScalarStorage<T> MapScalar(IGaScalarStorage<T> storage)
        {
            return GaScalarTermStorage<T>.Create(
                ScalarProcessor, 
                storage.Scalar
            );
        }

        public IGaKVectorStorage<T> MapTerm(IGaKVectorTermStorage<T> storage)
        {
            return Storage.EGp(storage).EGp(StorageReverse).GetKVectorPart(storage.Grade);
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> storage)
        {
            return Storage.EGp(storage).EGp(StorageReverse).GetVectorPart();
        }

        public IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> storage)
        {
            return Storage.EGp(storage).EGp(StorageReverse).GetBivectorPart();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> storage)
        {
            return Storage.EGp(storage).EGp(StorageReverse).GetKVectorPart(storage.Grade);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorGradedStorage<T> storage)
        {
            return Storage.EGp(storage).EGp(StorageReverse);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorTermsStorage<T> storage)
        {
            return Storage.EGp(storage).EGp(StorageReverse);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> storage)
        {
            return Storage.EGp(storage).EGp(StorageReverse);
        }
    }
}