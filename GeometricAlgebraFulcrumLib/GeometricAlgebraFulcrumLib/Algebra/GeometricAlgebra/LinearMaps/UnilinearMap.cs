using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps
{
    public class UnilinearMap<T> :
        IUnilinearMap<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => LinearProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor { get; }

        public ILinMatrixStorage<T> MatrixStorage { get; }


        internal UnilinearMap([NotNull] ILinearAlgebraProcessor<T> linearProcessor, [NotNull] ILinMatrixStorage<T> matrixStorage)
        {
            LinearProcessor = linearProcessor;
            MatrixStorage = matrixStorage;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IUnilinearMap<T> GetAdjoint()
        {
            return new UnilinearMap<T>(
                LinearProcessor, 
                MatrixStorage.GetTranspose()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisScalar()
        {
            return MatrixStorage
                .GetColumn(0)
                .CreateMultivectorSparseStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisVector(ulong index)
        {
            return MatrixStorage
                .GetColumn(index.BasisVectorIndexToId())
                .CreateMultivectorSparseStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBivector(ulong index)
        {
            return MatrixStorage
                .GetColumn(index.BasisBivectorIndexToId())
                .CreateMultivectorSparseStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return KVectorStorage<T>.ZeroScalar;

            var columnVector = MatrixStorage.GetColumn(
                BasisBivectorUtils.BasisVectorIndicesToBivectorId(index1, index2)
            );

            return index1 < index2 
                ? columnVector.CreateMultivectorSparseStorage() 
                : LinearProcessor.Negative(columnVector).CreateMultivectorSparseStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBlade(ulong id)
        {
            return MatrixStorage
                .GetColumn(id)
                .CreateMultivectorSparseStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = 
                BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

            return MatrixStorage
                .GetColumn(id)
                .CreateMultivectorSparseStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapScalar(T mv)
        {
            return LinearProcessor.Times(
                mv,
                MatrixStorage.GetColumn(0)
            ).CreateMultivectorSparseStorage();
        }

        public IMultivectorStorage<T> MapVector(VectorStorage<T> vector)
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapBivector(BivectorStorage<T> bivector)
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapKVector(KVectorStorage<T> kVector)
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapMultivector(MultivectorStorage<T> multivector)
        {
            throw new System.NotImplementedException();
        }

        public IMultivectorStorage<T> MapMultivector(MultivectorGradedStorage<T> multivector)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            return MatrixStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IdMultivectorStorageRecord<T>> GetMappedBasisBlades()
        {
            return MatrixStorage
                .GetColumns()
                .Select(r => 
                    new IdMultivectorStorageRecord<T>(
                        r.Index, 
                        r.Storage.CreateMultivectorSparseStorage()
                    )
                );
        }
    }
}