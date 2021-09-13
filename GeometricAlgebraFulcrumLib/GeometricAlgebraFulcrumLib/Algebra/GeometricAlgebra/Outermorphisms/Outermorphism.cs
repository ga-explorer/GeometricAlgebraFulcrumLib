using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    public class Outermorphism<T> :
        IOutermorphism<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => LinearProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor { get; }

        public IOutermorphismStorage<T> OmStorage { get; }


        internal Outermorphism([NotNull] ILinearAlgebraProcessor<T> linearProcessor, [NotNull] IOutermorphismStorage<T> omStorage)
        {
            LinearProcessor = linearProcessor;
            OmStorage = omStorage;
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
            return OmStorage.GetMappedBasisVector(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBivector(ulong index)
        {
            return OmStorage.GetMappedBasisBivector(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return OmStorage.GetMappedBasisBivector(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBlade(ulong id)
        {
            return OmStorage.GetMappedBasisBlade(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            return OmStorage.GetMappedBasisBlade(grade, index);
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IOutermorphism<T> GetOmAdjoint()
        {
            return new Outermorphism<T>(
                LinearProcessor,
                OmStorage.GetTranspose()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> OmMapBasisVector(ulong index)
        {
            return OmStorage.GetMappedBasisVector(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> OmMapBasisBivector(ulong index)
        {
            return OmStorage.GetMappedBasisBivector(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            return OmStorage.GetMappedBasisBivector(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> OmMapBasisBlade(ulong id)
        {
            return OmStorage.GetMappedBasisBlade(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> OmMapBasisBlade(uint grade, ulong index)
        {
            return OmStorage.GetMappedBasisBlade(grade, index);
        }


        public VectorStorage<T> OmMapVector(VectorStorage<T> vector)
        {
            var composer = LinearProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in vector.GetIndexScalarRecords())
                composer.AddScaledTerms(
                    scalar,
                    OmMapBasisVector(index).GetIndexScalarRecords()
                );

            return composer.CreateVectorStorage();
        }

        public BivectorStorage<T> OmMapBivector(BivectorStorage<T> bivector)
        {
            var composer = LinearProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in bivector.GetIndexScalarRecords())
                composer.AddScaledTerms(
                    scalar,
                    OmMapBasisBivector(index).GetIndexScalarRecords()
                );

            return composer.CreateBivectorStorage();
        }

        public KVectorStorage<T> OmMapKVector(KVectorStorage<T> kVector)
        {
            var composer = LinearProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in kVector.GetIndexScalarRecords())
                composer.AddScaledTerms(
                    scalar,
                    OmMapBasisVector(index).GetIndexScalarRecords()
                );

            return composer.CreateKVectorStorage(kVector.Grade);
        }

        public MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> multivector)
        {
            var composer = LinearProcessor.CreateVectorStorageComposer();

            foreach (var (id, scalar) in multivector.GetIdScalarRecords())
                composer.AddScaledTerms(
                    scalar,
                    OmMapBasisBlade(id).GetIdScalarRecords()
                );

            return composer.CreateMultivectorSparseStorage();
        }

        public MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> multivector)
        {
            var composer = LinearProcessor.CreateVectorGradedStorageComposer();

            foreach (var (id, scalar) in multivector.GetIdScalarRecords())
                composer.AddScaledTerms(
                    scalar,
                    OmMapBasisBlade(id).GetIdScalarRecords()
                );

            return composer.CreateMultivectorGradedStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            return OmStorage.GetMultivectorMappingMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetVectorOmMappingMatrix()
        {
            return OmStorage.GetVectorMappingMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetBivectorOmMappingMatrix()
        {
            return OmStorage.GetBivectorMappingMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetKVectorOmMappingMatrix(uint grade)
        {
            return OmStorage.GetKVectorMappingMatrix(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrix()
        {
            return OmStorage.GetMultivectorGradedMappingMatrix();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IdMultivectorStorageRecord<T>> GetMappedBasisBlades()
        {
            return OmStorage
                .GetMappedBasisBladesById()
                .Select(r => 
                    new IdMultivectorStorageRecord<T>(r.Id, r.Storage)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexVectorStorageRecord<T>> GetOmMappedBasisVectors()
        {
            return OmStorage
                .GetVectorMappingMatrix()
                .GetColumns()
                .Select(r => 
                    new IndexVectorStorageRecord<T>(
                        r.Index, 
                        r.Storage.CreateVectorStorage()
                    )
            );
        }
    }
}