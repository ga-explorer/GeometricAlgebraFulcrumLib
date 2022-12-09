using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    /// <summary>
    /// This class represents an outermorphism defined by a square diagonal vector mapping matrix.
    /// Only the scalars on the diagonal of the vector mapping matrix are stored, all other basis
    /// mappings are computed as needed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GaDiagonalLinearMapOutermorphism<T> : 
        IGaAutomorphism<T>
    {
        public IReadOnlyList<T> DiagonalScalars { get; }

        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }
        

        internal GaDiagonalLinearMapOutermorphism([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor, [NotNull] IReadOnlyList<T> basisVectorsSignatures)
        {
            GeometricProcessor = geometricProcessor;
            DiagonalScalars = basisVectorsSignatures;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaOutermorphism<T> GetOmAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> OmMapBasisVector(ulong index)
        {
            return GeometricProcessor.CreateVector(
                GeometricProcessor.CreateVectorStorageTerm(
                    index, 
                    DiagonalScalars[(int) index]
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> OmMapBasisBivector(ulong index)
        {
            var (index1, index2) = 
                index.BasisBivectorIndexToVectorIndices();

            return GeometricProcessor.CreateBivector(
                GeometricProcessor.CreateBivectorTermStorage(
                    index, 
                    GeometricProcessor.Times(
                        DiagonalScalars[(int) index1], 
                        DiagonalScalars[(int) index2]
                    )
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return GeometricProcessor.CreateBivectorZero();

            return GeometricProcessor.CreateBivector(
                GeometricProcessor.CreateBivectorTermStorage(
                    index1, 
                    index2, 
                    GeometricProcessor.Times(
                        DiagonalScalars[(int) index1], 
                        DiagonalScalars[(int) index2]
                    )
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> OmMapBasisBlade(ulong id)
        {
            var scalar = GeometricProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            return GeometricProcessor.CreateKVector(
                GeometricProcessor.CreateKVectorStorageTerm(id, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> OmMapBasisBlade(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            var scalar = GeometricProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            return GeometricProcessor.CreateKVector(
                GeometricProcessor.CreateKVectorStorageTerm(grade, index, scalar)
            );
        }


        public GaVector<T> OmMap(GaVector<T> vector)
        {
            var storage = 
                GeometricProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in vector.VectorStorage.GetIndexScalarRecords())
                storage.SetTerm(
                    index, 
                    GeometricProcessor.Times(scalar, DiagonalScalars[(int)index])
                );

            storage.RemoveZeroTerms();

            return storage.CreateVector();
        }

        public GaBivector<T> OmMap(GaBivector<T> bivector)
        {
            var storage = 
                GeometricProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in bivector.BivectorStorage.GetIndexScalarRecords())
            {
                var id = index.BasisBivectorIndexToId();

                var newScalar = GeometricProcessor.Times(
                    scalar,
                    GeometricProcessor.Times(
                        DiagonalScalars.PickItemsUsingPattern(id)
                    )
                );

                storage.SetTerm(index, newScalar);
            }

            storage.RemoveZeroTerms();

            return storage.CreateBivector();
        }

        public GaKVector<T> OmMap(GaKVector<T> kVector)
        {
            var storage = 
                GeometricProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in kVector.KVectorStorage.GetIndexScalarRecords())
            {
                var id = index.BasisBladeIndexToId(kVector.Grade);

                var newScalar = GeometricProcessor.Times(
                    scalar,
                    GeometricProcessor.Times(
                        DiagonalScalars.PickItemsUsingPattern(id)
                    )
                );

                storage.SetTerm(index, newScalar);
            }

            storage.RemoveZeroTerms();

            return storage.CreateKVector(kVector.Grade);
        }

        public GaMultivector<T> OmMap(GaMultivector<T> multivector)
        {
            var storage = 
                GeometricProcessor.CreateVectorStorageComposer();

            foreach (var (id, scalar) in multivector.MultivectorStorage.GetIdScalarRecords())
            {
                var newScalar = GeometricProcessor.Times(
                    scalar,
                    GeometricProcessor.Times(
                        DiagonalScalars.PickItemsUsingPattern(id)
                    )
                );

                storage.SetTerm(id, newScalar);
            }

            storage.RemoveZeroTerms();

            return storage.CreateMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsInvalid()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaUnilinearMap<T> GetAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisScalar()
        {
            return GeometricProcessor.CreateMultivector(
                GeometricProcessor.ScalarOne.CreateKVectorStorageScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisVector(ulong index)
        {
            return OmMapBasisVector(index).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisBivector(ulong index)
        {
            return OmMapBasisBivector(index).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return OmMapBasisBivector(index1, index2).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisBlade(ulong id)
        {
            return OmMapBasisBlade(id).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisBlade(uint grade, ulong index)
        {
            return OmMapBasisBlade(grade, index).AsMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Map(T mv)
        {
            return GeometricProcessor.CreateMultivector(
                mv.CreateKVectorStorageScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Map(GaVector<T> vector)
        {
            return OmMap(vector).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Map(GaBivector<T> bivector)
        {
            return OmMap(bivector).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Map(GaKVector<T> kVector)
        {
            return OmMap(kVector).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Map(GaMultivector<T> multivector)
        {
            return OmMap(multivector);
        }


        public ILinMatrixStorage<T> GetMultivectorMappingMatrixStorage()
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrix<T> GetMultivectorMappingMatrix()
        {
            return GetMultivectorMappingMatrixStorage().CreateLinMatrix(LinearProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetVectorOmMappingMatrixStorage()
        {
            return DiagonalScalars
                .CreateLinVectorStorage()
                .CreateLinMatrixDiagonalStorage();
        }

        public ILinMatrixStorage<T> GetBivectorOmMappingMatrixStorage()
        {
            throw new System.NotImplementedException();
        }

        public ILinMatrixStorage<T> GetKVectorOmMappingMatrixStorage(uint grade)
        {
            throw new System.NotImplementedException();
        }

        public ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrixStorage()
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrix<T> GetVectorOmMappingMatrix()
        {
            return GetVectorOmMappingMatrixStorage().CreateLinMatrix(LinearProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrix<T> GetBivectorOmMappingMatrix()
        {
            return GetBivectorOmMappingMatrixStorage().CreateLinMatrix(LinearProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrix<T> GetKVectorOmMappingMatrix(uint grade)
        {
            return GetKVectorOmMappingMatrixStorage(grade).CreateLinMatrix(LinearProcessor);
        }
        

        public IEnumerable<IdMultivectorRecord<T>> GetMappedBasisBlades()
        {
            throw new System.NotImplementedException();
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexVectorRecord<T>> GetOmMappedBasisVectors()
        {
            return DiagonalScalars
                .Count
                .GetRange()
                .Select(index => 
                    new IndexVectorRecord<T>(
                        (ulong) index, 
                        OmMapBasisVector((ulong) index)
                    )
                );
        }
    }
}