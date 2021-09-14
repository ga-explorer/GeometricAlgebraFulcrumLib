using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Projectors
{
    public sealed class Projector<T> :
        OutermorphismBase<T>,
        IProjector<T>
    {
        public override ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }
        
        public KVectorStorage<T> UnitBladeStorage { get; }


        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }

        public override ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<IdMultivectorStorageRecord<T>> GetMappedBasisBlades()
        {
            throw new System.NotImplementedException();
        }

        public override IOutermorphism<T> GetOmAdjoint()
        {
            throw new System.NotImplementedException();
        }

        public override VectorStorage<T> OmMapBasisVector(ulong index)
        {
            throw new System.NotImplementedException();
        }

        public override BivectorStorage<T> OmMapBasisBivector(ulong index)
        {
            throw new System.NotImplementedException();
        }

        public override BivectorStorage<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            throw new System.NotImplementedException();
        }

        public override KVectorStorage<T> OmMapBasisBlade(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public override KVectorStorage<T> OmMapBasisBlade(uint grade, ulong index)
        {
            throw new System.NotImplementedException();
        }

        public override VectorStorage<T> OmMapVector(VectorStorage<T> vector)
        {
            throw new System.NotImplementedException();
        }

        public override BivectorStorage<T> OmMapBivector(BivectorStorage<T> bivector)
        {
            throw new System.NotImplementedException();
        }

        public override KVectorStorage<T> OmMapKVector(KVectorStorage<T> kVector)
        {
            throw new System.NotImplementedException();
        }

        public override MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> storage)
        {
            return GeometricProcessor.ELcp(
                GeometricProcessor.ELcp(storage, UnitBladeStorage), 
                UnitBladeStorage
            ).ToMultivectorStorage();
        }

        public override MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> multivector)
        {
            throw new System.NotImplementedException();
        }

        public override ILinMatrixStorage<T> GetVectorOmMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        public override ILinMatrixStorage<T> GetBivectorOmMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        public override ILinMatrixStorage<T> GetKVectorOmMappingMatrix(uint grade)
        {
            throw new System.NotImplementedException();
        }

        public override ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<IndexVectorStorageRecord<T>> GetOmMappedBasisVectors()
        {
            throw new System.NotImplementedException();
        }
    }
}