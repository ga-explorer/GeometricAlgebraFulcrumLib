using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Algebra.Signatures;
using GeometricAlgebraLib.Processing.Multivectors;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage;
using GeometricAlgebraLib.Storage.Composers;

namespace GeometricAlgebraLib.Geometry.Euclidean
{
    public sealed class GaEuclideanSimpleRotor<T>
        : IGaEuclideanGeometry<T>, IGaRotor<T>
    {
        public static GaEuclideanSimpleRotor<T> CreateIdentity(IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaEuclideanSimpleRotor<T>(
                GaScalarTermStorage<T>.CreateBasisScalar(scalarProcessor)
            );
        }
        
        public static GaEuclideanSimpleRotor<T> Create(IGaMultivectorStorage<T> storage)
        {
            return new GaEuclideanSimpleRotor<T>(storage);
        }

        public static GaEuclideanSimpleRotor<T> Create(IGaVectorStorage<T> sourceVector, IGaVectorStorage<T> targetVector)
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
                    scalarProcessor.Sqrt(
                        scalarProcessor.Negative(
                            rotationBlade.EGp().GetTermScalar(0)
                        )
                    )
                    //scalarProcessor.SqrtOfAbs(rotationBlade.EGpSquared().GetTermScalar(0))
                );

            var rotorStorage = cosHalfAngle.Subtract(
                rotationBladeScalar.Times(rotationBlade)
            );
            
            //rotor.IsSimpleRotor();

            return new GaEuclideanSimpleRotor<T>(rotorStorage);
        }

        /// <summary>
        /// Create a simple rotor from an angle and a blade
        /// </summary>
        /// <param name="rotationAngle"></param>
        /// <param name="rotationBlade"></param>
        /// <returns></returns>
        public static GaEuclideanSimpleRotor<T> Create(T rotationAngle, IGaKVectorStorage<T> rotationBlade)
        {
            if (rotationBlade.Grade != 2)
                throw new InvalidOperationException();

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

            return new GaEuclideanSimpleRotor<T>(rotorStorage);
        }

        public static GaEuclideanSimpleRotor<T> Create(IGaVectorStorage<T> inputVector1, IGaVectorStorage<T> inputVector2, IGaVectorStorage<T> rotatedVector1, IGaVectorStorage<T> rotatedVector2)
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

            var rotor = GaEuclideanSimpleRotorsSequence<T>.CreateFromOrthonormalFrames(
                inputFrame, 
                rotatedFrame, 
                true
            ).GetFinalRotor();

            return new GaEuclideanSimpleRotor<T>(
                rotor.Storage, 
                rotor.StorageReverse
            );
        }
        
        public static GaEuclideanSimpleRotor<T> Create(int baseSpaceDimensions, IGaVectorStorage<T> inputVector1, IGaVectorStorage<T> inputVector2, IGaVectorStorage<T> rotatedVector1, IGaVectorStorage<T> rotatedVector2)
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

            var rotor = GaEuclideanSimpleRotorsSequence<T>.CreateFromFrames(
                baseSpaceDimensions, 
                inputFrame, 
                rotatedFrame
            ).GetFinalRotor();

            return new GaEuclideanSimpleRotor<T>(
                rotor.Storage,
                rotor.StorageReverse
            );
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
        public static GaEuclideanSimpleRotor<T> CreateGivensRotor(IGaScalarProcessor<T> scalarProcessor, int i, int j, T rotationAngle)
        {
            Debug.Assert(i >= 0 && j > i);

            var halfRotationAngle = scalarProcessor.Divide(rotationAngle, scalarProcessor.IntegerToScalar(2));
            var cosHalfAngle = scalarProcessor.Cos(halfRotationAngle);
            var sinHalfAngle = scalarProcessor.Sin(halfRotationAngle);

            var bladeId = (1UL << i) | (1UL << j);

            var composer = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

            composer.SetTerm(0, cosHalfAngle);
            composer.SetTerm(bladeId, sinHalfAngle);

            return new GaEuclideanSimpleRotor<T>(
                composer.GetCompactStorage()
            );
        }


        public IGaScalarProcessor<T> ScalarProcessor 
            => Storage.ScalarProcessor;

        public IGaMultivectorStorage<T> Storage { get; }

        public IGaMultivectorStorage<T> StorageReverse { get; }

        public bool IsValid
        {
            get
            {
                // Make sure the storage and its reverse are correct
                if (!Storage.GetReverse().Subtract(StorageReverse).IsNearZero())
                    return false;

                // Make sure storage contains only terms of grades 0,2
                if ((Storage.GetStoredGradesBitPattern() | 5UL) != 5UL)
                    return false;

                // Make sure storage gp reverse(storage) == 1
                var gp = Storage.EGp(StorageReverse);

                if (!gp.IsScalar())
                    return false;

                var diff =
                    ScalarProcessor.Subtract(
                        gp.GetTermScalar(0),
                        ScalarProcessor.OneScalar
                    );

                if (!ScalarProcessor.IsNearZero(diff))
                    return false;

                return true;
            }
        }

        public bool IsInvalid 
            => !IsValid;


        private GaEuclideanSimpleRotor([NotNull] IGaMultivectorStorage<T> storage)
        {
            Storage = storage;
            StorageReverse = Storage.GetReverse();
        }

        private GaEuclideanSimpleRotor([NotNull] IGaMultivectorStorage<T> storage, [NotNull] IGaMultivectorStorage<T> storageReverse)
        {
            Storage = storage;
            StorageReverse = storageReverse;
        }


        public GaEuclideanVector<T> Map(GaEuclideanVector<T> vector)
        {
            return GaEuclideanVector<T>.Create(
                MapVector(vector.Storage)
            );
        }

        public GaEuclideanSimpleRotor<T> GetReverseRotor()
        {
            return new GaEuclideanSimpleRotor<T>(
                StorageReverse, 
                Storage
            );
        }
        

        public IGaVectorsLinearMap<T> GetAdjoint()
        {
            return new GaEuclideanSimpleRotor<T>(
                StorageReverse, 
                Storage
            );
        }

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            return MapVector(
                GaVectorTermStorage<T>.Create(
                    ScalarProcessor, 
                    index, 
                    ScalarProcessor.OneScalar
                )
            );
        }

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            return MapVector(
                GaVectorTermStorage<T>.Create(
                    ScalarProcessor, 
                    index, 
                    ScalarProcessor.OneScalar
                )
            );
        }

        public IGaBivectorStorage<T> MapBasisBivector(int index1, int index2)
        {
            return MapBivector(
                GaBivectorTermStorage<T>.Create(
                    ScalarProcessor, 
                    index1, 
                    index2,
                    ScalarProcessor.OneScalar
                )
            );
        }

        public IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return MapBivector(
                GaBivectorTermStorage<T>.Create(
                    ScalarProcessor, 
                    index1, 
                    index2,
                    ScalarProcessor.OneScalar
                )
            );
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            return MapTerm(
                GaKVectorTermStorage<T>.Create(
                    ScalarProcessor, 
                    id, 
                    ScalarProcessor.OneScalar
                )
            );
        }

        public IGaKVectorStorage<T> MapBasisBlade(int grade, ulong index)
        {
            return MapTerm(
                GaKVectorTermStorage<T>.Create(
                    ScalarProcessor, 
                    grade,
                    index, 
                    ScalarProcessor.OneScalar
                )
            );
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