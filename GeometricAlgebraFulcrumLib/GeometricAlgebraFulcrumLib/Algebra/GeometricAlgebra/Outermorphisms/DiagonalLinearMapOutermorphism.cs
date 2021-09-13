using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    /// <summary>
    /// This class represents an outermorphism defined by a square diagonal vector mapping matrix.
    /// Only the scalars on the diagonal of the vector mapping matrix are stored, all other basis
    /// mappings are computed as needed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class DiagonalLinearMapOutermorphism<T> : 
        IAutomorphism<T>
    {
        public IReadOnlyList<T> DiagonalScalars { get; }

        public IScalarAlgebraProcessor<T> ScalarProcessor
            => LinearProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor { get; }
        

        internal DiagonalLinearMapOutermorphism([NotNull] ILinearAlgebraProcessor<T> linearProcessor, [NotNull] IReadOnlyList<T> basisVectorsSignatures)
        {
            LinearProcessor = linearProcessor;
            DiagonalScalars = basisVectorsSignatures;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IOutermorphism<T> GetOmAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> OmMapBasisVector(ulong index)
        {
            return LinearProcessor.CreateVectorTermStorage(
                index, 
                DiagonalScalars[(int) index]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> OmMapBasisBivector(ulong index)
        {
            var (index1, index2) = 
                index.BasisBivectorIndexToVectorIndices();

            return LinearProcessor.CreateBivectorTermStorage(
                index, 
                LinearProcessor.Times(
                    DiagonalScalars[(int) index1], 
                    DiagonalScalars[(int) index2]
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return BivectorStorage<T>.ZeroBivector;

            return LinearProcessor.CreateBivectorTermStorage(
                index1, 
                index2, 
                LinearProcessor.Times(
                    DiagonalScalars[(int) index1], 
                    DiagonalScalars[(int) index2]
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> OmMapBasisBlade(ulong id)
        {
            var scalar = LinearProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            return LinearProcessor.CreateKVectorTermStorage(id, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> OmMapBasisBlade(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            var scalar = LinearProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            return LinearProcessor.CreateKVectorTermStorage(grade, index, scalar);
        }


        public VectorStorage<T> OmMapVector(VectorStorage<T> vector)
        {
            var storage = 
                LinearProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in vector.GetIndexScalarRecords())
                storage.SetTerm(
                    index, 
                    LinearProcessor.Times(scalar, DiagonalScalars[(int)index])
                );

            storage.RemoveZeroTerms();

            return storage.CreateVectorStorage();
        }

        public BivectorStorage<T> OmMapBivector(BivectorStorage<T> bivector)
        {
            var storage = 
                LinearProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in bivector.GetIndexScalarRecords())
            {
                var id = index.BasisBivectorIndexToId();

                var newScalar = LinearProcessor.Times(
                    scalar,
                    LinearProcessor.Times(
                        DiagonalScalars.PickItemsUsingPattern(id)
                    )
                );

                storage.SetTerm(index, newScalar);
            }

            storage.RemoveZeroTerms();

            return storage.CreateBivectorStorage();
        }

        public KVectorStorage<T> OmMapKVector(KVectorStorage<T> kVector)
        {
            var storage = 
                LinearProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in kVector.GetIndexScalarRecords())
            {
                var id = index.BasisBladeIndexToId(kVector.Grade);

                var newScalar = LinearProcessor.Times(
                    scalar,
                    LinearProcessor.Times(
                        DiagonalScalars.PickItemsUsingPattern(id)
                    )
                );

                storage.SetTerm(index, newScalar);
            }

            storage.RemoveZeroTerms();

            return storage.CreateKVectorStorage(kVector.Grade);
        }

        public MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> multivector)
        {
            var storage = 
                LinearProcessor.CreateVectorStorageComposer();

            foreach (var (id, scalar) in multivector.GetIdScalarRecords())
            {
                var newScalar = LinearProcessor.Times(
                    scalar,
                    LinearProcessor.Times(
                        DiagonalScalars.PickItemsUsingPattern(id)
                    )
                );

                storage.SetTerm(id, newScalar);
            }

            storage.RemoveZeroTerms();

            return storage.CreateMultivectorSparseStorage();
        }

        public MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> multivector)
        {
            var storage = 
                LinearProcessor.CreateVectorGradedStorageComposer();

            foreach (var (id, scalar) in multivector.GetIdScalarRecords())
            {
                var newScalar = LinearProcessor.Times(
                    scalar,
                    LinearProcessor.Times(
                        DiagonalScalars.PickItemsUsingPattern(id)
                    )
                );

                storage.SetTerm(id, newScalar);
            }

            storage.RemoveZeroTerms();

            return storage.CreateMultivectorGradedStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IUnilinearMap<T> GetAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisScalar()
        {
            return LinearProcessor.ScalarOne.CreateKVectorScalarStorage();
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
        public IMultivectorStorage<T> MapScalar(T mv)
        {
            return mv.CreateKVectorScalarStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapVector(VectorStorage<T> mv)
        {
            return OmMapVector(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBivector(BivectorStorage<T> mv)
        {
            return OmMapBivector(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapKVector(KVectorStorage<T> mv)
        {
            return OmMapKVector(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapMultivector(MultivectorStorage<T> mv)
        {
            return OmMapMultivector(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapMultivector(MultivectorGradedStorage<T> mv)
        {
            return OmMapMultivector(mv);
        }


        public ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetVectorOmMappingMatrix()
        {
            return DiagonalScalars
                .CreateLinVectorStorage()
                .CreateLinMatrixDiagonalStorage();
        }

        public ILinMatrixStorage<T> GetBivectorOmMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        public ILinMatrixStorage<T> GetKVectorOmMappingMatrix(uint grade)
        {
            throw new System.NotImplementedException();
        }

        public ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrix()
        {
            throw new System.NotImplementedException();
        }


        public IEnumerable<IdMultivectorStorageRecord<T>> GetMappedBasisBlades()
        {
            throw new System.NotImplementedException();
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexVectorStorageRecord<T>> GetOmMappedBasisVectors()
        {
            return DiagonalScalars
                .Count
                .GetRange()
                .Select(index => 
                    new IndexVectorStorageRecord<T>(
                        (ulong) index, 
                        OmMapBasisVector((ulong) index)
                    )
                );
        }
    }
}