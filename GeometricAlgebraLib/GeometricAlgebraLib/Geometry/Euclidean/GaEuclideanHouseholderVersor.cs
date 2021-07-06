using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Algebra.Signatures;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Geometry.Euclidean
{
    public sealed class GaEuclideanHouseholderVersor<T> 
        : IGaVersor<T>, IGaEuclideanGeometry<T>
    {
        public static GaEuclideanHouseholderVersor<T> Create(IGaVectorStorage<T> unitVectorStorage)
        {
            return new GaEuclideanHouseholderVersor<T>(unitVectorStorage);
        }


        public IGaScalarProcessor<T> ScalarProcessor 
            => UnitVectorStorage.ScalarProcessor;

        public IGaVectorStorage<T> UnitVectorStorage { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        private GaEuclideanHouseholderVersor([NotNull] IGaVectorStorage<T> unitVectorStorage)
        {
            UnitVectorStorage = unitVectorStorage;
        }
        

        public IGaVectorsLinearMap<T> GetAdjoint()
        {
            return this;
        }

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            return MapVector(
                GaVectorTermStorage<T>.CreateBasisVector(ScalarProcessor, index)
            );
        }

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            return MapVector(
                GaVectorTermStorage<T>.CreateBasisVector(ScalarProcessor, index)
            );
        }

        public IGaBivectorStorage<T> MapBasisBivector(int index1, int index2)
        {
            return MapBivector(
                GaBivectorTermStorage<T>.CreateBasisBivector(ScalarProcessor, index1, index2)
            );
        }

        public IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return MapBivector(
                GaBivectorTermStorage<T>.CreateBasisBivector(ScalarProcessor, index1, index2)
            );
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            return MapTerm(
                GaKVectorTermStorage<T>.CreateBasisBlade(ScalarProcessor, id)
            );
        }

        public IGaKVectorStorage<T> MapBasisBlade(int grade, ulong index)
        {
            return MapTerm(
                GaKVectorTermStorage<T>.CreateBasisBlade(ScalarProcessor, grade, index)
            );
        }

        public IGaScalarStorage<T> MapScalar(IGaScalarStorage<T> storage)
        {
            return storage;
        }

        public IGaKVectorStorage<T> MapTerm(IGaKVectorTermStorage<T> storage)
        {
            return UnitVectorStorage
                .EGp(storage.GetGradeInvolution())
                .EGp(UnitVectorStorage)
                .GetKVectorPart(storage.Grade);
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> storage)
        {
            return UnitVectorStorage
                .EGp(storage.GetNegative())
                .EGp(UnitVectorStorage)
                .GetVectorPart();
        }

        public IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> storage)
        {
            return UnitVectorStorage
                .EGp(storage)
                .EGp(UnitVectorStorage)
                .GetBivectorPart();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> storage)
        {
            return UnitVectorStorage
                .EGp(storage.GetGradeInvolution())
                .EGp(UnitVectorStorage)
                .GetKVectorPart(storage.Grade);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorGradedStorage<T> storage)
        {
            return UnitVectorStorage
                .EGp(storage.GetGradeInvolution())
                .EGp(UnitVectorStorage);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorTermsStorage<T> storage)
        {
            return UnitVectorStorage
                .EGp(storage.GetGradeInvolution())
                .EGp(UnitVectorStorage);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> storage)
        {
            return UnitVectorStorage
                .EGp(storage.GetGradeInvolution())
                .EGp(UnitVectorStorage);
        }
    }
}