using System.Collections.Generic;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Geometry
{
    public sealed class GaFactoredVectorsLinearMap<T>
        : IGaVectorsLinearMap<T>
    {
        public IGaScalarProcessor<T> ScalarProcessor 
            => LinearMapsList[0].ScalarProcessor;

        public IReadOnlyList<IGaVectorsLinearMap<T>> LinearMapsList { get; }


        public bool IsValid
            => true;

        public bool IsInvalid
            => false;


        public IGaVectorsLinearMap<T> GetAdjoint()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public IGaKVectorStorage<T> MapTerm(IGaKVectorTermStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorGradedStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorTermsStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }
    }
}