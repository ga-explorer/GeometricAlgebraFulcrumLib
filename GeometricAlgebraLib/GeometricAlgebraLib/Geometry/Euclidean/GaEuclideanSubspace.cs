using System;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraLib.Algebra.Signatures;
using GeometricAlgebraLib.Processing.Scalars;
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

        public T BladeScalarProductSquared { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        private GaEuclideanSubspace([NotNull] IGaKVectorStorage<T> storage)
        {
            BladeStorage = storage;
            BladeScalarProductSquared = storage.ENormSquared();
        }


        public IGaMultivectorStorage<T> Project(IGaMultivectorStorage<T> storage)
        {
            return storage.ELcp(BladeStorage).ELcp(BladeStorage).Divide(BladeScalarProductSquared);
        }

        public GaEuclideanVector<T> Project(GaEuclideanVector<T> vector)
        {
            return GaEuclideanVector<T>.Create(
                Project(vector.Storage).GetVectorPart()
            );
        }

        public IGaMultivectorStorage<T> Reflect(IGaMultivectorStorage<T> storage)
        {
            return BladeStorage.EGp(storage.GetGradeInvolution()).EGp(BladeStorage.EBladeInverse());
        }

        public IGaMultivectorStorage<T> Rotate([NotNull] IGaMultivectorStorage<T> storage)
        {
            if (BladeStorage.Grade.IsOdd())
                throw new InvalidOperationException();

            //Debug.Assert(ScalarProcessor.IsOne(BladeScalarProductSquared));

            return BladeStorage.EGp(storage).EGp(BladeStorage.GetReverse());
        }

        public IGaMultivectorStorage<T> VersorProduct(IGaMultivectorStorage<T> storage)
        {
            return BladeStorage.EGp(storage).EGp(BladeStorage.EBladeInverse());
        }
        
        public IGaMultivectorStorage<T> Complement(IGaMultivectorStorage<T> storage)
        {
            return storage.ELcp(BladeStorage.EBladeInverse());
        }
    }
}