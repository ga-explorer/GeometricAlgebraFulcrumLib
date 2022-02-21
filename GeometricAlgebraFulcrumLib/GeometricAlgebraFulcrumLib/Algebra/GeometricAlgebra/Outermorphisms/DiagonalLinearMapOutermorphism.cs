using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
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
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }
        

        internal DiagonalLinearMapOutermorphism([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor, [NotNull] IReadOnlyList<T> basisVectorsSignatures)
        {
            GeometricProcessor = geometricProcessor;
            DiagonalScalars = basisVectorsSignatures;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IOutermorphism<T> GetOmAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> OmMapBasisVector(ulong index)
        {
            return GeometricProcessor.CreateVector(
                GeometricProcessor.CreateVectorStorageTerm(
                    index, 
                    DiagonalScalars[(int) index]
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> OmMapBasisBivector(ulong index)
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
        public Bivector<T> OmMapBasisBivector(ulong index1, ulong index2)
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
        public KVector<T> OmMapBasisBlade(ulong id)
        {
            var scalar = GeometricProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            return GeometricProcessor.CreateKVector(
                GeometricProcessor.CreateKVectorStorageTerm(id, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> OmMapBasisBlade(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            var scalar = GeometricProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            return GeometricProcessor.CreateKVector(
                GeometricProcessor.CreateKVectorStorageTerm(grade, index, scalar)
            );
        }


        public Vector<T> OmMap(Vector<T> vector)
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

        public Bivector<T> OmMap(Bivector<T> bivector)
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

        public KVector<T> OmMap(KVector<T> kVector)
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

        public Multivector<T> OmMap(Multivector<T> multivector)
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
        public IUnilinearMap<T> GetAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> MapBasisScalar()
        {
            return GeometricProcessor.CreateMultivector(
                GeometricProcessor.ScalarOne.CreateKVectorStorageScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> MapBasisVector(ulong index)
        {
            return OmMapBasisVector(index).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> MapBasisBivector(ulong index)
        {
            return OmMapBasisBivector(index).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return OmMapBasisBivector(index1, index2).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> MapBasisBlade(ulong id)
        {
            return OmMapBasisBlade(id).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> MapBasisBlade(uint grade, ulong index)
        {
            return OmMapBasisBlade(grade, index).AsMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> Map(T mv)
        {
            return GeometricProcessor.CreateMultivector(
                mv.CreateKVectorStorageScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> Map(Vector<T> vector)
        {
            return OmMap(vector).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> Map(Bivector<T> bivector)
        {
            return OmMap(bivector).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> Map(KVector<T> kVector)
        {
            return OmMap(kVector).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> Map(Multivector<T> multivector)
        {
            return OmMap(multivector);
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