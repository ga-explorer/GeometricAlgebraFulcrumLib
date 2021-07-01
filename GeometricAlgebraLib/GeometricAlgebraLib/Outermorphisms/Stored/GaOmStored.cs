using System.Collections.Generic;
using GeometricAlgebraLib.Multivectors.Basis;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Outermorphisms.Stored
{
    public sealed class GaOmStored<T> : IGaOutermorphism<T>
    {
        private readonly Dictionary<ulong, GaKVectorStorageBase<T>> _mappedBasisBladesDictionary
            = new();


        public int DomainVSpaceDimension { get; }

        public ulong DomainGaSpaceDimension 
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public IGaScalarProcessor<T> ScalarProcessor { get; }


        private GaOmStored(IGaScalarProcessor<T> scalarProcessor, int domainVSpaceDimension)
        {
            ScalarProcessor = scalarProcessor;
            DomainVSpaceDimension = domainVSpaceDimension;
        }


        public IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors()
        {
            throw new System.NotImplementedException();
        }

        public T GetDeterminant()
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

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public IGaKVectorStorage<T> MapBasisBlade(int grade, ulong index)
        {
            throw new System.NotImplementedException();
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> vector)
        {
            throw new System.NotImplementedException();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> kVector)
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> mv)
        {
            throw new System.NotImplementedException();
        }
        
    }
}