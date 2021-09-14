using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    public abstract class OutermorphismBase<T> :
        IOutermorphism<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => LinearProcessor;

        public abstract ILinearAlgebraProcessor<T> LinearProcessor { get; }


        public abstract bool IsValid();

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsInvalid()
        {
            return !IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IUnilinearMap<T> GetAdjoint()
        {
            return GetOmAdjoint();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisScalar()
        {
            return LinearProcessor.CreateKVectorBasisScalarStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisVector(ulong index)
        {
            return OmMapBasisVector(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBivector(ulong index)
        {
            return OmMapBasisBivector(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return OmMapBasisBivector(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBlade(ulong id)
        {
            return OmMapBasisBlade(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            return OmMapBasisBlade(grade, index);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapScalar(T scalar)
        {
            return scalar.CreateKVectorScalarStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapVector(VectorStorage<T> vector)
        {
            return OmMapVector(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBivector(BivectorStorage<T> bivector)
        {
            return OmMapBivector(bivector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapKVector(KVectorStorage<T> kVector)
        {
            return OmMapKVector(kVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapMultivector(MultivectorStorage<T> multivector)
        {
            return OmMapMultivector(multivector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapMultivector(MultivectorGradedStorage<T> multivector)
        {
            return OmMapMultivector(multivector);
        }

        public abstract ILinMatrixStorage<T> GetMultivectorMappingMatrix();

        public abstract IEnumerable<IdMultivectorStorageRecord<T>> GetMappedBasisBlades();
        
        public abstract IOutermorphism<T> GetOmAdjoint();
        
        public abstract VectorStorage<T> OmMapBasisVector(ulong index);
        
        public abstract BivectorStorage<T> OmMapBasisBivector(ulong index);
        
        public abstract BivectorStorage<T> OmMapBasisBivector(ulong index1, ulong index2);
        
        public abstract KVectorStorage<T> OmMapBasisBlade(ulong id);
        
        public abstract KVectorStorage<T> OmMapBasisBlade(uint grade, ulong index);
        
        public abstract VectorStorage<T> OmMapVector(VectorStorage<T> vector);
        
        public abstract BivectorStorage<T> OmMapBivector(BivectorStorage<T> bivector);
        
        public abstract KVectorStorage<T> OmMapKVector(KVectorStorage<T> kVector);
        
        public abstract MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> multivector);
        
        public abstract MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> multivector);
        
        public abstract ILinMatrixStorage<T> GetVectorOmMappingMatrix();
        
        public abstract ILinMatrixStorage<T> GetBivectorOmMappingMatrix();
        
        public abstract ILinMatrixStorage<T> GetKVectorOmMappingMatrix(uint grade);
        
        public abstract ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrix();
        
        public abstract IEnumerable<IndexVectorStorageRecord<T>> GetOmMappedBasisVectors();
    }
}