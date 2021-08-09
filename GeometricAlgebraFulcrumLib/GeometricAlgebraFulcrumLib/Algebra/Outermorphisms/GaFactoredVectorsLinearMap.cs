using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms
{
    public sealed class GaFactoredVectorsLinearMap<T>
        : IGaOutermorphism<T>
    {
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

        public IReadOnlyList<IGaOutermorphism<T>> LinearMapsList { get; }


        public bool IsValid
            => true;

        public bool IsInvalid
            => false;


        public IGaOutermorphism<T> GetAdjoint()
        {
            throw new System.NotImplementedException();
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

        public IGaStorageScalar<T> MapScalar(IGaStorageScalar<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaStorageKVector<T> MapTerm(IGaStorageKVector<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaStorageVector<T> MapVector(IGaStorageVector<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaStorageBivector<T> MapBivector(IGaStorageBivector<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaStorageKVector<T> MapKVector(IGaStorageKVector<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivectorGraded<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivectorSparse<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivector<T> storage)
        {
            throw new System.NotImplementedException();
        }
    }
}