using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib;
using GeometricAlgebraLib.Processors.Multivectors;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Geometry.Euclidean
{
    public sealed class GaEuclideanSubspace<T> 
        : IGaSubspace<T>, IGaEuclideanGeometry<T>
    {
        public static GaEuclideanSubspace<T> Create(IGaKVectorStorage<T> storage)
        {
            return new(storage);
        }

        public static GaEuclideanSubspace<T> CreateFromPseudoScalar(IGaScalarProcessor<T> scalarProcessor, int vSpaceDimension)
        {
            return new(
                GaKVectorTermStorage<T>.CreatePseudoScalar(scalarProcessor, vSpaceDimension)
            );
        }


        public IGaScalarProcessor<T> ScalarProcessor
            => BladeStorage.ScalarProcessor;

        public IGaKVectorStorage<T> BladeStorage { get; }

        public T BladeNormSquared { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        private GaEuclideanSubspace([NotNull] IGaKVectorStorage<T> storage)
        {
            BladeStorage = storage;
            BladeNormSquared = storage.ENormSquared();
        }


        public IGaMultivectorStorage<T> Project(IGaMultivectorStorage<T> storage)
        {
            return storage.ELcp(BladeStorage).ELcp(BladeStorage).Divide(BladeNormSquared);
        }

        public GaEuclideanVector<T> Project(GaEuclideanVector<T> vector)
        {
            return GaEuclideanVector<T>.Create(
                Project(vector.Storage).GetVectorPart()
            );
        }

        public IGaMultivectorStorage<T> Reflect(IGaMultivectorStorage<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGaMultivectorStorage<T> Rotate([NotNull] IGaMultivectorStorage<T> storage)
        {
            if (BladeStorage.Grade.IsOdd())
                throw new InvalidOperationException();

            Debug.Assert(ScalarProcessor.IsOne(BladeNormSquared));

            return BladeStorage.EGp(storage).EGp(BladeStorage.GetReverse());
        }

        public IGaMultivectorStorage<T> VersorProduct(IGaMultivectorStorage<T> storage)
        {
            throw new NotImplementedException();
        }
        
        public IGaKVectorStorage<T> Complement(IGaKVectorStorage<T> storage)
        {
            return storage
                .ELcp(BladeStorage.EBladeInverse())
                .GetKVectorPart(BladeStorage.Grade - storage.Grade);
        }
    }
}