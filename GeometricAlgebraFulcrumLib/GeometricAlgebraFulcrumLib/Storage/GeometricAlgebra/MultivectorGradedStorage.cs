using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;
using GeometricAlgebraFulcrumLib.Text;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    public sealed record MultivectorGradedStorage<T>
        : IMultivectorGradedStorage<T>
    {
        public static MultivectorGradedStorage<T> ZeroMultivector { get; }
            = new MultivectorGradedStorage<T>(LinVectorEmptyGradedStorage<T>.EmptyStorage);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorGradedStorage<T> Create(ILinVectorGradedStorage<T> gradeIndexScalarList)
        {
            return new MultivectorGradedStorage<T>(
                gradeIndexScalarList
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> GetLinVectorGradedStorage()
        {
            return _gradeIndexScalarVectorStorage;
        }

        private uint? _vSpaceDimension;
        private readonly ILinVectorGradedStorage<T> _gradeIndexScalarVectorStorage;

        public uint MinVSpaceDimension 
            => _vSpaceDimension 
                ??= GetLinVectorGradedStorage().GetMinVSpaceDimensionOfMultivector();

        public int GradesCount 
            => GetLinVectorGradedStorage().GradesCount;

        public int TermsCount 
            => GetLinVectorGradedStorage().GetGradeStorageRecords().Sum(p => p.Storage.GetSparseCount());
        
        public bool IsEven 
            => false;

        public bool IsGraded 
            => true;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private MultivectorGradedStorage(ILinVectorGradedStorage<T> gradeIndexScalarList)
        {
            _gradeIndexScalarVectorStorage = gradeIndexScalarList;
        }


        /// <summary>
        /// Create a bit pattern where each active grades is a 1
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetStoredGradesBitPattern()
        {
            return GetGrades().Aggregate(
                0UL, 
                (current, grade) => current | 1UL << (int) grade
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetStoredBasisVectorsBitPattern()
        {
            return GetIds().Aggregate(0UL, (id1, id2) => id1 | id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorGradedStorage<T> GetGradedMultivectorCopy()
        {
            var gradeIndexScalarList = 
                GetLinVectorGradedStorage().GetCopy();

            return new MultivectorGradedStorage<T>(
                gradeIndexScalarList
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorGradedStorage<T2> MapGradedMultivectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            var gradeIndexScalarList =
                GetLinVectorGradedStorage().MapScalars(scalarMapping);

            return new MultivectorGradedStorage<T2>(
                gradeIndexScalarList
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorGradedStorage<T2> MapGradedMultivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var gradeIndexScalarList =
                GetLinVectorGradedStorage().MapScalars(
                    (grade, index, scalar) => 
                        idScalarMapping(index.BasisBladeIndexToId(grade), scalar)
                );

            return new MultivectorGradedStorage<T2>(
                gradeIndexScalarList
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorGradedStorage<T2> MapGradedMultivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var gradeIndexScalarList =
                GetLinVectorGradedStorage().MapScalars(
                    (_, index, scalar) => 
                        indexScalarMapping(index, scalar)
                );

            return new MultivectorGradedStorage<T2>(
                gradeIndexScalarList
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorGradedStorage<T2> MapGradedMultivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var gradeIndexScalarList =
                GetLinVectorGradedStorage().MapScalars(gradeIndexScalarMapping);

            return new MultivectorGradedStorage<T2>(
                gradeIndexScalarList
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsVectorPart()
        {
            return GetLinVectorGradedStorage().ContainsGrade(1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(out T value)
        {
            return GetLinVectorGradedStorage().TryGetScalar(0, 0, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsBivectorPart()
        {
            return GetLinVectorGradedStorage().ContainsGrade(2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKVectorPart(uint grade)
        {
            return GetLinVectorGradedStorage().ContainsGrade(grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return GetLinVectorGradedStorage().IsEmpty();
        }

        public bool IsScalar()
        {
            foreach (var (grade, indexScalarDictionary) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                if (grade == 0)
                    continue;

                if (!indexScalarDictionary.IsEmpty())
                    return false;
            }

            return true;
        }

        public bool IsVector()
        {
            foreach (var (grade, indexScalarDictionary) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                if (grade == 1)
                    continue;

                if (!indexScalarDictionary.IsEmpty())
                    return false;
            }

            return true;
        }

        public bool IsBivector()
        {
            foreach (var (grade, indexScalarDictionary) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                if (grade == 2)
                    continue;

                if (!indexScalarDictionary.IsEmpty())
                    return false;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsKVector()
        {
            return GetLinVectorGradedStorage().GetVectorStorages().Count(pair => !pair.IsEmpty()) < 2;
        }

        public bool IsKVector(uint grade)
        {
            foreach (var (g, indexScalarDictionary) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                if (g == grade)
                    continue;

                if (!indexScalarDictionary.IsEmpty())
                    return false;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinId()
        {
            return GetLinVectorGradedStorage()
                .GetIndices(BasisBladeUtils.BasisBladeGradeIndexToId)
                .Min();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxId()
        {
            return GetLinVectorGradedStorage()
                .GetIndices(BasisBladeUtils.BasisBladeGradeIndexToId)
                .Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinId(uint grade)
        {
            return GetLinVectorGradedStorage().GetMinIndex(grade).BasisBladeIndexToId(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxId(uint grade)
        {
            return GetLinVectorGradedStorage().GetMaxIndex(grade).BasisBladeIndexToId(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMinGrade()
        {
            return GetLinVectorGradedStorage().GetMinGrade();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMaxGrade()
        {
            return GetLinVectorGradedStorage().GetMaxGrade();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex(uint grade)
        {
            return GetLinVectorGradedStorage().GetMinIndex(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex(uint grade)
        {
            return GetLinVectorGradedStorage().GetMaxIndex(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return GetLinVectorGradedStorage().GetGrades();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsTerm(ulong id)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return GetLinVectorGradedStorage().ContainsIndex(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsTerm(uint grade, ulong index)
        {
            return GetLinVectorGradedStorage().ContainsIndex(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsScalarPart()
        {
            return GetLinVectorGradedStorage().ContainsGrade(0);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTermScalar(ulong id, out T value)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            if (GetLinVectorGradedStorage().TryGetVectorStorage(grade, out var storage))
                return storage.TryGetScalar(index, out value);

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            if (GetLinVectorGradedStorage().TryGetVectorStorage(grade, out var storage))
                return storage.TryGetScalar(index, out value);

            value = default;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetKVectorPart(uint grade, out KVectorStorage<T> kVector)
        {
            if (GetLinVectorGradedStorage().TryGetVectorStorage(grade, out var evenDictionary))
            {
                kVector = KVectorStorage<T>.CreateKVector(grade, evenDictionary);
                return true;
            }

            kVector = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalarPartList(out ILinVectorStorage<T> indexScalarList)
        {
            return GetLinVectorGradedStorage().TryGetVectorStorage(0, out indexScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetVectorPartList(out ILinVectorStorage<T> indexScalarDictionary)
        {
            if (GetLinVectorGradedStorage().TryGetVectorStorage(1U, out indexScalarDictionary) && !indexScalarDictionary.IsEmpty())
                return true;

            indexScalarDictionary = LinVectorEmptyStorage<T>.EmptyStorage;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetBivectorPartList(out ILinVectorStorage<T> indexScalarDictionary)
        {
            if (GetLinVectorGradedStorage().TryGetVectorStorage(2U, out indexScalarDictionary) && !indexScalarDictionary.IsEmpty())
                return true;

            indexScalarDictionary = LinVectorEmptyStorage<T>.EmptyStorage;
            return false;

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetKVectorPartList(uint grade, out ILinVectorStorage<T> indexScalarDictionary)
        {
            if (GetLinVectorGradedStorage().TryGetVectorStorage(grade, out indexScalarDictionary) && !indexScalarDictionary.IsEmpty())
                return true;

            indexScalarDictionary = LinVectorEmptyStorage<T>.EmptyStorage;
            return false;

        }

        public ILinVectorStorage<T> GetLinVectorIdScalarStorage()
        {
            return GetLinVectorGradedStorage()
                .GetGradeStorageRecords()
                .SelectMany(storage => 
                    storage.Storage.GetIndexScalarRecords().Select(pair => 
                        new KeyValuePair<ulong, T>(
                            pair.KvIndex.BasisBladeIndexToId(storage.Grade), 
                            pair.Scalar
                        )
                    )
                ).ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                ).CreateLinVectorStorage();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetLinVectorIndexScalarStorage(uint grade)
        {
            return GetLinVectorGradedStorage().TryGetVectorStorage(grade, out var indexScalarList)
                ? indexScalarList
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTerm(ulong id, out KeyValuePair<ulong, T> term)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            if (TryGetTermScalar(grade, index, out var value))
            {
                term = value.CreateBasisTerm(grade, index);
                return true;
            }

            term = new KeyValuePair<ulong, T>();
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTerm(uint grade, ulong index, out KeyValuePair<ulong, T> term)
        {
            if (TryGetTermScalar(grade, index, out var value))
            {
                term = value.CreateBasisTerm(grade, index);
                return true;
            }

            term = new KeyValuePair<ulong, T>();
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetScalarPartList()
        {
            return 
                GetLinVectorGradedStorage().TryGetVectorStorage(0, out var evenDictionary) && 
                evenDictionary.TryGetScalar(0UL, out var scalar)
                    ? scalar.CreateLinVectorSingleScalarDenseStorage()
                    : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetVectorPartList()
        {
            return 
                GetLinVectorGradedStorage().TryGetVectorStorage(1U, out var evenDictionary) && 
                !evenDictionary.IsEmpty()
                    ? evenDictionary
                    : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetBivectorPartList()
        {
            return 
                GetLinVectorGradedStorage().TryGetVectorStorage(2U, out var evenDictionary) && 
                !evenDictionary.IsEmpty()
                    ? evenDictionary
                    : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetKVectorPartList(uint grade)
        {
            return 
                GetLinVectorGradedStorage().TryGetVectorStorage(grade, out var evenDictionary) && 
                !evenDictionary.IsEmpty()
                    ? evenDictionary
                    : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetVectorPart(out VectorStorage<T> vector)
        {
            if (GetLinVectorGradedStorage().TryGetVectorStorage(1U, out var evenDictionary))
            {
                vector = VectorStorage<T>.CreateVectorStorage(evenDictionary);
                return true;
            }

            vector = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetBivectorPart(out BivectorStorage<T> bivector)
        {
            if (GetLinVectorGradedStorage().TryGetVectorStorage(2U, out var evenDictionary))
            {
                bivector = BivectorStorage<T>.Create(evenDictionary);
                return true;
            }

            bivector = null;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIds()
        {
            return GetLinVectorGradedStorage()
                .GetGradeStorageRecords()
                .SelectMany(pair => 
                    pair.Storage.GetIndices().Select(
                        index => index.BasisBladeIndexToId(pair.Grade)
                    )
                );
        }

        public IEnumerable<RGaGradeKvIndexRecord> GetGradeIndexRecords()
        {
            foreach (var (grade, indexScalarDictionary) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            foreach (var index in indexScalarDictionary.GetIndices())
                yield return new RGaGradeKvIndexRecord(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, T>> GetTerms()
        {
            return GetLinVectorGradedStorage()
                .GetGradeStorageRecords()
                .SelectMany(storage => 
                    storage.Storage.GetIndexScalarRecords().Select(pair => 
                        pair.Scalar.CreateBasisTerm(storage.Grade, pair.KvIndex)
                    )
                );
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
                {
                    var id = index.BasisBladeIndexToId(grade);

                    if (idSelection(id))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
                {
                    if (gradeIndexSelection(grade, index))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
                {
                    if (scalarSelection(scalar))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
                {
                    var id = index.BasisBladeIndexToId(grade);

                    if (idScalarSelection(id, scalar))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
                {
                    if (gradeIndexScalarSelection(grade, index, scalar))
                        yield return scalar.CreateBasisTerm(grade, index);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexScalarRecord<T>> GetIdScalarRecords()
        {
            return GetLinVectorGradedStorage()
                .GetGradeStorageRecords()
                .SelectMany(storage => 
                    storage.Storage.GetIndexScalarRecords().Select(pair => 
                        new RGaKvIndexScalarRecord<T>(
                            pair.KvIndex.BasisBladeIndexToId(storage.Grade), 
                            pair.Scalar
                        )
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexScalarRecord<T>> GetIndexScalarRecords(uint grade)
        {
            return GetLinVectorGradedStorage().GetVectorStorage(grade).GetIndexScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaGradeKvIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return GetLinVectorGradedStorage()
                .GetGradeStorageRecords()
                .SelectMany(storage => 
                    storage.Storage.GetIndexScalarRecords().Select(pair => 
                        new RGaGradeKvIndexScalarRecord<T>(storage.Grade, pair.KvIndex, pair.Scalar)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return GetLinVectorGradedStorage().GetVectorStorages()
                .SelectMany(storage => storage.GetScalars());
        }


        /// <summary>
        /// Construct a binary tree representation of this storage
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorTreeStorage<T> GetBinaryTree(int treeDepth)
        {
            if (treeDepth < MinVSpaceDimension)
                throw new InvalidOperationException();

            var dict = GetIdScalarRecords()
                .ToDictionary(
                    pair => pair.KvIndex,
                    pair => pair.Scalar
                );

            return new LinVectorTreeStorage<T>(treeDepth, dict);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorStorage<T> ToMultivectorStorage()
        {
            return MultivectorStorage<T>.Create(
                GetLinVectorGradedStorage().ToLinVectorStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorGradedStorage<T> ToMultivectorGradedStorage()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorPart()
        {
            if (!GetLinVectorGradedStorage().TryGetVectorStorage(1, out var indexScalarDictionary))
                return VectorStorage<T>.ZeroVector;
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? VectorStorage<T>.ZeroVector
                : VectorStorage<T>.CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            if (!GetLinVectorGradedStorage().TryGetVectorStorage(1, out var indexScalarDictionary))
                return VectorStorage<T>.ZeroVector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByScalar(scalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? VectorStorage<T>.ZeroVector
                : VectorStorage<T>.CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GetLinVectorGradedStorage().TryGetVectorStorage(1, out var indexScalarDictionary))
                return VectorStorage<T>.ZeroVector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByIndexScalar(indexScalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? VectorStorage<T>.ZeroVector
                : VectorStorage<T>.CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            if (!GetLinVectorGradedStorage().TryGetVectorStorage(1, out var indexScalarDictionary))
                return VectorStorage<T>.ZeroVector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByIndex(indexSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? VectorStorage<T>.ZeroVector
                : VectorStorage<T>.CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivectorPart()
        {
            if (!GetLinVectorGradedStorage().TryGetVectorStorage(2, out var indexScalarDictionary))
                return BivectorStorage<T>.ZeroBivector;
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? BivectorStorage<T>.ZeroBivector
                : BivectorStorage<T>.Create(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            if (!GetLinVectorGradedStorage().TryGetVectorStorage(2, out var indexScalarDictionary))
                return BivectorStorage<T>.ZeroBivector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByScalar(scalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? BivectorStorage<T>.ZeroBivector
                : BivectorStorage<T>.Create(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GetLinVectorGradedStorage().TryGetVectorStorage(2, out var indexScalarDictionary))
                return BivectorStorage<T>.ZeroBivector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByIndexScalar(indexScalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? BivectorStorage<T>.ZeroBivector
                : BivectorStorage<T>.Create(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            if (!GetLinVectorGradedStorage().TryGetVectorStorage(2, out var indexScalarDictionary))
                return BivectorStorage<T>.ZeroBivector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByIndex(indexSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? BivectorStorage<T>.ZeroBivector
                : BivectorStorage<T>.Create(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorPart(uint grade)
        {
            if (!GetLinVectorGradedStorage().TryGetVectorStorage(grade, out var indexScalarDictionary))
                return KVectorStorage<T>.CreateKVectorZero(grade);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? KVectorStorage<T>.CreateKVectorZero(grade)
                : KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            if (!GetLinVectorGradedStorage().TryGetVectorStorage(grade, out var indexScalarDictionary))
                return KVectorStorage<T>.CreateKVectorZero(grade);

            indexScalarDictionary = 
                indexScalarDictionary.FilterByScalar(scalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? KVectorStorage<T>.CreateKVectorZero(grade)
                : KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GetLinVectorGradedStorage().TryGetVectorStorage(grade, out var indexScalarDictionary))
                return KVectorStorage<T>.CreateKVectorZero(grade);

            indexScalarDictionary = 
                indexScalarDictionary.FilterByIndexScalar(indexScalarSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? KVectorStorage<T>.CreateKVectorZero(grade)
                : KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            if (!GetLinVectorGradedStorage().TryGetVectorStorage(grade, out var indexScalarDictionary))
                return KVectorStorage<T>.CreateKVectorZero(grade);

            indexScalarDictionary = 
                indexScalarDictionary.FilterByIndex(indexSelection);
            
            return indexScalarDictionary.GetSparseCount() == 0
                ? KVectorStorage<T>.CreateKVectorZero(grade)
                : KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary);
        }
        
        public IMultivectorStorage<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetIndexScalarRecords())
                {
                    var id = index.BasisBladeIndexToId(grade);

                    if (idSelection(id))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateLinVectorGradedStorage();

            return new MultivectorGradedStorage<T>(
                gradeIndexScalarList
            );
        }

        public IMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetIndexScalarRecords())
                {
                    if (gradeIndexSelection(grade, index))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateLinVectorGradedStorage();

            return new MultivectorGradedStorage<T>(
                gradeIndexScalarList
            );
        }

        public IMultivectorStorage<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetIndexScalarRecords())
                {
                    if (scalarSelection(scalar))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateLinVectorGradedStorage();

            return new MultivectorGradedStorage<T>(
                gradeIndexScalarList
            );
        }

        public IMultivectorStorage<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetIndexScalarRecords())
                {
                    var id = index.BasisBladeIndexToId(grade);

                    if (idScalarSelection(id, scalar))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateLinVectorGradedStorage();

            return new MultivectorGradedStorage<T>(
                gradeIndexScalarList
            );
        }

        public IMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarList) in GetLinVectorGradedStorage().GetGradeStorageRecords())
            {
                var indexScalarDictionary = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarList.GetIndexScalarRecords())
                {
                    if (gradeIndexScalarSelection(grade, index, scalar))
                        indexScalarDictionary.Add(index, scalar);
                }

                if (indexScalarDictionary.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
            }

            var gradeIndexScalarList =
                gradeIndexScalarDictionary.CreateLinVectorGradedStorage();

            return new MultivectorGradedStorage<T>(gradeIndexScalarList);
        }

        public Tuple<IMultivectorStorage<T>, IMultivectorStorage<T>> SplitEvenOddParts()
        {
            var gradeIndexScalarDictionary1 = new Dictionary<uint, ILinVectorStorage<T>>();
            var gradeIndexScalarDictionary2 = new Dictionary<uint, ILinVectorStorage<T>>();

            foreach (var (grade, indexScalarList) in _gradeIndexScalarVectorStorage.GetGradeStorageRecords())
            {
                if (grade.IsEven())
                    gradeIndexScalarDictionary1.Add(grade, indexScalarList);
                else
                    gradeIndexScalarDictionary2.Add(grade, indexScalarList);
            }

            var mv1 =
                gradeIndexScalarDictionary1.Count == 0
                    ? ZeroMultivector
                    : gradeIndexScalarDictionary1
                        .CreateLinVectorGradedStorage()
                        .CreateMultivectorStorageGraded();

            var mv2 =
                gradeIndexScalarDictionary2.Count == 0
                    ? ZeroMultivector
                    : gradeIndexScalarDictionary2
                        .CreateLinVectorGradedStorage()
                        .CreateMultivectorStorageGraded();

            return new Tuple<IMultivectorStorage<T>, IMultivectorStorage<T>>(mv1, mv2);
        }

        public Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            if (!TryGetVectorPartList(out var indexScalarDictionary))
                return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                    VectorStorage<T>.ZeroVector,
                    VectorStorage<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
            {
                if (indexSelection(index))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                indexScalarDictionary1.CreateVectorStorage(),
                indexScalarDictionary2.CreateVectorStorage()
            );
        }

        public Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!TryGetVectorPartList(out var indexScalarDictionary))
                return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                    VectorStorage<T>.ZeroVector,
                    VectorStorage<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
            {
                if (indexScalarSelection(index, scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                indexScalarDictionary1.CreateVectorStorage(),
                indexScalarDictionary2.CreateVectorStorage()
            );
        }

        public Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            if (!TryGetVectorPartList(out var indexScalarDictionary))
                return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                    VectorStorage<T>.ZeroVector,
                    VectorStorage<T>.ZeroVector
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in indexScalarDictionary.GetIndexScalarRecords())
            {
                if (scalarSelection(scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                indexScalarDictionary1.CreateVectorStorage(),
                indexScalarDictionary2.CreateVectorStorage()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return this.GetMultivectorText();
        }
    }
}