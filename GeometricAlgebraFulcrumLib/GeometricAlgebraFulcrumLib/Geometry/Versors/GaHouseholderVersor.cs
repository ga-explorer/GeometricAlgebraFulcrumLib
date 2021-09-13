using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Geometry.Versors
{
    public sealed class GeoHouseholderVersor<T> 
        : IGeoVersor<T>
    {
        private readonly KVectorStorage<T> _mappedPseudoScalar;

        public static GeoHouseholderVersor<T> Create(IGeometricAlgebraProcessor<T> processor, VectorStorage<T> unitVectorStorage)
        {
            return new GeoHouseholderVersor<T>(processor, unitVectorStorage);
        }


        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public VectorStorage<T> UnitVectorStorage { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        private GeoHouseholderVersor([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] VectorStorage<T> unitVectorStorage)
        {
            GeometricProcessor = processor;
            UnitVectorStorage = unitVectorStorage;
        }


        public IOutermorphism<T> GetAdjoint()
        {
            return this;
        }

        public VectorStorage<T> MapBasisVector(int index)
        {
            return OmMapVector(
                GeometricProcessor.CreateVectorBasisStorage(index)
            );
        }

        public ILinMatrixGradedStorage<T> GetOmMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        public ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IndexVectorStorageRecord<T>> GetOmMappedBasisVectors()
        {
            throw new System.NotImplementedException();
        }

        public IOutermorphism<T> GetOmAdjoint()
        {
            throw new System.NotImplementedException();
        }

        public VectorStorage<T> OmMapBasisVector(ulong index)
        {
            return OmMapVector(
                GeometricProcessor.CreateVectorBasisStorage(index)
            );
        }

        public BivectorStorage<T> OmMapBasisBivector(ulong index)
        {
            throw new System.NotImplementedException();
        }

        public BivectorStorage<T> MapBasisBivector(int index1, int index2)
        {
            return OmMapBivector(
                GeometricProcessor.CreateBivectorBasisStorage(index1, index2)
            );
        }

        public BivectorStorage<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            return OmMapBivector(
                GeometricProcessor.CreateBivectorBasisStorage(index1, index2)
            );
        }

        public KVectorStorage<T> OmMapBasisBlade(ulong id)
        {
            return OmMapKVector(
                GeometricProcessor.CreateKVectorBasisStorage(id)
            );
        }

        public KVectorStorage<T> OmMapBasisBlade(uint grade, ulong index)
        {
            return OmMapKVector(
                GeometricProcessor.CreateKVectorBasisStorage(grade, index)
            );
        }

        public VectorStorage<T> OmMapVector(VectorStorage<T> storage)
        {
            return GeometricProcessor.EGp(
                UnitVectorStorage, 
                GeometricProcessor.Negative(storage),
                UnitVectorStorage
            ).GetVectorPart();
        }

        public BivectorStorage<T> OmMapBivector(BivectorStorage<T> storage)
        {
            return GeometricProcessor.EGp(
                UnitVectorStorage, 
                storage,
                UnitVectorStorage
            ).GetBivectorPart();
        }

        public KVectorStorage<T> OmMapKVector(KVectorStorage<T> storage)
        {
            return GeometricProcessor.EGp(
                UnitVectorStorage, 
                GeometricProcessor.GradeInvolution(storage),
                UnitVectorStorage
            ).GetKVectorPart(storage.Grade);
        }

        public MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> multivector)
        {
            throw new System.NotImplementedException();
        }

        public MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> multivector)
        {
            throw new System.NotImplementedException();
        }

        public ILinMatrixStorage<T> GetVectorOmMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        public ILinMatrixStorage<T> GetBivectorOmMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        public ILinMatrixStorage<T> GetKVectorOmMappingMatrix(uint grade)
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapMultivector(IMultivectorGradedStorage<T> storage)
        {
            return GeometricProcessor.EGp(
                UnitVectorStorage, 
                GeometricProcessor.GradeInvolution(storage),
                UnitVectorStorage
            );
        }

        public IMultivectorStorage<T> MapKVector(KVectorStorage<T> mv)
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapMultivector(MultivectorGradedStorage<T> mv)
        {
            throw new System.NotImplementedException();
        }

        public ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IdMultivectorStorageRecord<T>> GetMappedBasisBlades()
        {
            throw new System.NotImplementedException();
        }

        IUnilinearMap<T> IUnilinearMap<T>.GetAdjoint()
        {
            return GetAdjoint();
        }

        public IMultivectorStorage<T> MapBasisScalar()
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisVector(ulong index)
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisBivector(ulong index)
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisBlade(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapScalar(T mv)
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapVector(VectorStorage<T> mv)
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapBivector(BivectorStorage<T> mv)
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapMultivector(MultivectorStorage<T> storage)
        {
            return GeometricProcessor.EGp(
                UnitVectorStorage, 
                GeometricProcessor.GradeInvolution(storage),
                UnitVectorStorage
            );
        }

        public ILinVectorStorage<T> LinMapBasisVector(ulong index)
        {
            throw new System.NotImplementedException();
        }

        public ILinVectorStorage<T> LinMapVector(ILinVectorStorage<T> vectorStorage)
        {
            throw new System.NotImplementedException();
        }

        public ILinMatrixStorage<T> LinMapMatrix(ILinMatrixStorage<T> matrixStorage)
        {
            throw new System.NotImplementedException();
        }

        public ILinMatrixStorage<T> GetLinMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IndexLinVectorStorageRecord<T>> GetLinMappedBasisVectors()
        {
            throw new System.NotImplementedException();
        }
    }
}