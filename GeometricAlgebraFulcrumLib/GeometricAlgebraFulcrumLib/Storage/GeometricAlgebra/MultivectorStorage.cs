using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    public sealed record MultivectorStorage<T> : 
        IMultivectorStorage<T>
    {
        public static MultivectorStorage<T> ZeroMultivector { get; }
            = new MultivectorStorage<T>(LinVectorEmptyStorage<T>.EmptyStorage);

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Create(T scalar)
        {
            var idScalarList = 
                scalar.CreateLinVectorSingleScalarDenseStorage();

            return new MultivectorStorage<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Create(ulong id, T scalar)
        {
            var idScalarList = 
                scalar.CreateLinVectorSingleScalarStorage(id);

            return new MultivectorStorage<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Create(uint grade, ulong index, T scalar)
        {
            var id = index.BasisBladeIndexToId(grade);

            var idScalarList = 
                scalar.CreateLinVectorSingleScalarStorage(id);

            return new MultivectorStorage<T>(idScalarList);
        }

        public static MultivectorStorage<T> Create(uint grade, params T[] indexScalarList)
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < (ulong) indexScalarList.Length; index++)
                idScalarDictionary.Add(
                    index.BasisBladeIndexToId(grade),
                    indexScalarList[index]
                );

            var idScalarList = 
                idScalarDictionary.CreateLinVectorStorage();

            return new MultivectorStorage<T>(idScalarList);
        }

        public static MultivectorStorage<T> Create(uint grade, IReadOnlyList<T> indexScalarList)
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < (ulong) indexScalarList.Count; index++)
                idScalarDictionary.Add(
                    index.BasisBladeIndexToId(grade),
                    indexScalarList[(int) index]
                );

            var idScalarList = 
                idScalarDictionary.CreateLinVectorStorage();

            return new MultivectorStorage<T>(idScalarList);
        }

        public static MultivectorStorage<T> Create(uint grade, IEnumerable<T> indexScalarList)
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            var index = 0UL;
            foreach (var scalar in indexScalarList)
            {
                idScalarDictionary.Add(
                    index.BasisBladeIndexToId(grade),
                    scalar
                );

                index++;
            }

            var idScalarList = 
                idScalarDictionary.CreateLinVectorStorage();

            return new MultivectorStorage<T>(idScalarList);
        }

        public static MultivectorStorage<T> Create(uint grade, IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in indexScalarDictionary)
            {
                idScalarDictionary.Add(
                    index.BasisBladeIndexToId(grade),
                    scalar
                );
            }

            var idScalarList = 
                idScalarDictionary.CreateLinVectorStorage();

            return new MultivectorStorage<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Create(params T[] scalarList)
        {
            var idScalarList = 
                scalarList.CreateLinVectorDenseStorage();

            return new MultivectorStorage<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Create(IReadOnlyList<T> scalarList)
        {
            var idScalarList = 
                scalarList.CreateLinVectorDenseStorage();

            return new MultivectorStorage<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Create(IEnumerable<T> scalarList)
        {
            var idScalarList = 
                scalarList.CreateLinVectorDenseStorage();

            return new MultivectorStorage<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Create(Dictionary<ulong, T> idScalarDictionary)
        {
            var idScalarList = 
                idScalarDictionary.CreateLinVectorStorage();

            return new MultivectorStorage<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Create(ILinVectorStorage<T> idScalarDictionary)
        {
            return new MultivectorStorage<T>(idScalarDictionary);
        }


        public ILinVectorStorage<T> GetLinVectorIdScalarStorage()
        {
            return _idScalarVectorStorage;
        }

        private uint? _vSpaceDimension;
        private readonly ILinVectorStorage<T> _idScalarVectorStorage;

        public uint MinVSpaceDimension 
            => _vSpaceDimension 
                ??= GetLinVectorIdScalarStorage().GetMinVSpaceDimensionOfMultivector();

        public int GradesCount =>
            GetLinVectorIdScalarStorage().GetIndices()
                .Select(id => id.BasisBladeIdToGrade())
                .Distinct()
                .Count();

        public int TermsCount 
            => GetLinVectorIdScalarStorage().GetSparseCount();

        public bool IsEven 
            => true;

        public bool IsGraded 
            => false;


        private MultivectorStorage([NotNull] ILinVectorStorage<T> idScalarDictionary)
        {
            _idScalarVectorStorage = idScalarDictionary;
        }


        /// <summary>
        /// Create a bit pattern where each active grades is a 1
        /// </summary>
        /// <returns></returns>
        public ulong GetStoredGradesBitPattern()
        {
            return GetGrades().Aggregate(
                0UL, 
                (current, grade) => current | 1UL << (int) grade
            );
        }

        public MultivectorStorage<T> GetMultivectorCopy()
        {
            var idScalarList = 
                GetLinVectorIdScalarStorage().GetCopy();

            return new MultivectorStorage<T>(idScalarList);
        }

        public MultivectorStorage<T2> MapMultivectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            var idScalarList = 
                GetLinVectorIdScalarStorage().MapScalars(scalarMapping);

            return new MultivectorStorage<T2>(idScalarList);
        }
        
        public MultivectorStorage<T2> MapMultivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var idScalarList = 
                GetLinVectorIdScalarStorage().MapScalars(idScalarMapping);

            return new MultivectorStorage<T2>(idScalarList);
        }
        
        public MultivectorStorage<T2> MapMultivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var idScalarList = 
                GetLinVectorIdScalarStorage().MapScalars((id, scalar) => 
                    indexScalarMapping(id.BasisBladeIdToIndex(), scalar)
                );

            return new MultivectorStorage<T2>(idScalarList);
        }

        public MultivectorStorage<T2> MapMultivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var idScalarList = 
                GetLinVectorIdScalarStorage().MapScalars(
                    (id, scalar) =>
                    {
                        id.BasisBladeIdToGradeIndex(out var grade, out var index);
                        return gradeIndexScalarMapping(grade, index, scalar);
                    }
                );

            return new MultivectorStorage<T2>(idScalarList);
        }


        public MultivectorStorage<T> FilterMultivectorByScalar(Func<T, bool> scalarFilter)
        {
            var idScalarDictionary =
                GetLinVectorIdScalarStorage().FilterByScalar(scalarFilter);

            return new MultivectorStorage<T>(idScalarDictionary);
        }

        public MultivectorStorage<T> FilterMultivectorByIdScalar(Func<ulong, T, bool> idScalarFilter)
        {
            var idScalarDictionary =
                GetLinVectorIdScalarStorage().FilterByIndexScalar(idScalarFilter);

            return new MultivectorStorage<T>(idScalarDictionary);
        }

        public MultivectorStorage<T> FilterMultivectorById(Func<ulong, bool> idFilter)
        {
            var idScalarDictionary =
                GetLinVectorIdScalarStorage().FilterByIndex(idFilter);

            return new MultivectorStorage<T>(idScalarDictionary);
        }


        public bool ContainsVectorPart()
        {
            return GetLinVectorIdScalarStorage().GetIndices().Any(key => key.IsBasisVectorId());
        }

        public bool TryGetScalar(out T value)
        {
            return GetLinVectorIdScalarStorage().TryGetScalar(0, out value);
        }

        public bool ContainsBivectorPart()
        {
            return GetLinVectorIdScalarStorage().GetIndices().Any(key => key.IsBasisBivectorId());
        }

        public bool ContainsKVectorPart(uint grade)
        {
            return GetLinVectorIdScalarStorage()
                .GetIndices()
                .Any(id => id.IsBasisBladeOfGrade(grade));
        }


        public bool IsEmpty()
        {
            return GetLinVectorIdScalarStorage().GetSparseCount() == 0;
        }
        

        public bool IsScalar()
        {
            return !GetLinVectorIdScalarStorage().GetIndices().Any(key => key > 0);
        }

        public bool IsVector()
        {
            return GetLinVectorIdScalarStorage().GetIndices().All(key => key.IsBasicPattern());
        }

        public bool IsBivector()
        {
            return GetLinVectorIdScalarStorage()
                .GetIndices()
                .All(id => id.IsBasisBivectorId());
        }

        public bool IsKVector()
        {
            return GetLinVectorIdScalarStorage().GetIndices()
                .Select(id => id.BasisBladeIdToGrade())
                .Distinct()
                .Count() < 2;
        }

        public bool IsKVector(uint grade)
        {
            return GetLinVectorIdScalarStorage()
                .GetIndices()
                .All(id => id.IsBasisBladeOfGrade(grade));
        }

        public ulong GetMinId()
        {
            return GetLinVectorIdScalarStorage().GetMinIndex();
        }

        public ulong GetMaxId()
        {
            return GetLinVectorIdScalarStorage().GetMaxIndex();
        }

        public ulong GetMinId(uint grade)
        {
            return GetLinVectorIdScalarStorage()
                .GetIndexScalarRecords()
                .Select(idScalar => idScalar.Index)
                .Where(id => id.IsBasisBladeOfGrade(grade))
                .Min();
        }

        public ulong GetMaxId(uint grade)
        {
            return GetLinVectorIdScalarStorage()
                .GetIndexScalarRecords()
                .Select(idScalar => idScalar.Index)
                .Where(id => id.IsBasisBladeOfGrade(grade))
                .Max();
        }

        public uint GetMinGrade()
        {
            return GetGrades().TryGetMinValue(out var grade)
                ? grade
                : 0;
        }

        public uint GetMaxGrade()
        {
            return GetGrades().TryGetMaxValue(out var grade)
                ? grade
                : 0;
        }

        public ulong GetMinIndex(uint grade)
        {
            return GetLinVectorIdScalarStorage()
                .GetIndexScalarRecords()
                .Select(idScalar => idScalar.Index)
                .Where(id => id.IsBasisBladeOfGrade(grade))
                .Min()
                .BasisBivectorIdToIndex();
        }

        public ulong GetMaxIndex(uint grade)
        {
            return GetLinVectorIdScalarStorage()
                .GetIndexScalarRecords()
                .Select(idScalar => idScalar.Index)
                .Where(id => id.IsBasisBladeOfGrade(grade))
                .Max()
                .BasisBivectorIdToIndex();
        }

        public IEnumerable<uint> GetGrades()
        {
            return GetLinVectorIdScalarStorage().GetIndices()
                .Select(id => id.BasisBladeIdToGrade())
                .Distinct();
        }


        public bool ContainsTerm(ulong id)
        {
            return GetLinVectorIdScalarStorage().ContainsIndex(id);
        }

        public bool ContainsTerm(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            return GetLinVectorIdScalarStorage().ContainsIndex(id);
        }

        public bool ContainsScalarPart()
        {
            return GetLinVectorIdScalarStorage().GetIndices().Any(key => key == 0);
        }

        public bool TryGetTermScalar(ulong id, out T value)
        {
            if (GetLinVectorIdScalarStorage().TryGetScalar(id, out value))
                return true;

            value = default;
            return false;
        }

        public bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            var id = index.BasisBladeIndexToId(grade);

            if (GetLinVectorIdScalarStorage().TryGetScalar(id, out value))
                return true;

            value = default;
            return false;
        }


        public bool TryGetKVectorPart(uint grade, out KVectorStorage<T> kVector)
        {
            var indexScalarDictionary = GetLinVectorIdScalarStorage()
                .GetIndexScalarRecords()
                .Where(pair => 
                    pair.Index.IsBasisBladeOfGrade(grade)
                )
                .ToDictionary(
                    pair => pair.Index.BasisBladeIdToIndex(),
                    pair => pair.Scalar
                );

            if (indexScalarDictionary.Count == 0)
            {
                kVector = null;
                return false;
            }

            kVector = KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary);
            return true;
        }


        public bool TryGetScalarPartList(out ILinVectorStorage<T> indexScalarList)
        {
            if (GetLinVectorIdScalarStorage().TryGetScalar(0, out var scalar))
            {
                indexScalarList = scalar.CreateLinVectorSingleScalarDenseStorage();
                return true;
            }

            indexScalarList = null;
            return false;
        }

        public bool TryGetVectorPartList(out ILinVectorStorage<T> indexScalarDictionary)
        {
            var dict =
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            if (dict.Count > 0)
            {
                indexScalarDictionary = dict.CreateLinVectorStorage();
                return true;
            }

            indexScalarDictionary = LinVectorEmptyStorage<T>.EmptyStorage;
            return false;
        }

        public bool TryGetBivectorPartList(out ILinVectorStorage<T> indexScalarDictionary)
        {
            var dict =
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            if (dict.Count > 0)
            {
                indexScalarDictionary = dict.CreateLinVectorStorage();
                return true;
            }

            indexScalarDictionary = LinVectorEmptyStorage<T>.EmptyStorage;
            return false;
        }

        public bool TryGetKVectorPartList(uint grade, out ILinVectorStorage<T> indexScalarDictionary)
        {
            if (grade == 0)
            {
                if (GetLinVectorIdScalarStorage().TryGetScalar(0, out var scalar))
                {
                    indexScalarDictionary = scalar.CreateLinVectorSingleScalarDenseStorage();
                    return true;
                }

                indexScalarDictionary = LinVectorEmptyStorage<T>.EmptyStorage;
                return false;
            }

            var dict =
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBladeOfGrade(grade))
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            if (dict.Count > 0)
            {
                indexScalarDictionary = dict.CreateLinVectorStorage();
                return true;
            }

            indexScalarDictionary = LinVectorEmptyStorage<T>.EmptyStorage;
            return false;
        }
        

        public ILinVectorGradedStorage<T> GetLinVectorGradedStorage()
        {
            return GetLinVectorIdScalarStorage().GetIndexScalarRecords().GroupBy(
                pair => pair.Index.BasisBladeIdToGrade()
            ).ToDictionary(
                g => g.Key,
                g => g.ToDictionary(
                    pair => pair.Index.BasisBladeIdToIndex(), 
                    pair => pair.Scalar
                )
            ).CreateLinVectorGradedStorage();
        }

        public ILinVectorStorage<T> GetLinVectorIndexScalarStorage(uint grade)
        {
            return TryGetKVectorPartList(grade, out var indexScalarList)
                ? indexScalarList 
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }


        public bool TryGetTerm(ulong id, out BasisTerm<T> term)
        {
            if (TryGetTermScalar(id, out var value))
            {
                term = value.CreateBasisTerm(id);
                return true;
            }

            term = null;
            return false;
        }

        public bool TryGetTerm(uint grade, ulong index, out BasisTerm<T> term)
        {
            var id = index.BasisBladeIndexToId(grade);

            if (TryGetTermScalar(id, out var value))
            {
                term = value.CreateBasisTerm(id);
                return true;
            }

            term = null;
            return false;
        }

        public ILinVectorStorage<T> GetScalarPartList()
        {
            return GetLinVectorIdScalarStorage().TryGetScalar(0, out var scalar)
                ? scalar.CreateLinVectorSingleScalarDenseStorage()
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorStorage<T> GetVectorPartList()
        {
            var indexScalarDictionary =
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(), 
                        pair => pair.Scalar
                    );

            return indexScalarDictionary.Count > 0
                ? indexScalarDictionary.CreateLinVectorStorage()
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorStorage<T> GetBivectorPartList()
        {
            var indexScalarDictionary =
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(), 
                        pair => pair.Scalar
                    );

            return indexScalarDictionary.Count > 0
                ? indexScalarDictionary.CreateLinVectorStorage()
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorStorage<T> GetKVectorPartList(uint grade)
        {
            var indexScalarDictionary =
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBladeOfGrade(grade))
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(), 
                        pair => pair.Scalar
                    );

            return indexScalarDictionary.Count > 0
                ? indexScalarDictionary.CreateLinVectorStorage()
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public bool TryGetVectorPart(out VectorStorage<T> vector)
        {
            var indexScalarDictionary =
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisVectorIdToIndex(), 
                        pair => pair.Scalar
                    );

            if (indexScalarDictionary.Count > 0)
            {
                vector = VectorStorage<T>.CreateVector(indexScalarDictionary);
                return true;
            }

            vector = null;
            return false;
        }

        public bool TryGetBivectorPart(out BivectorStorage<T> bivector)
        {
            var indexScalarDictionary =
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisBivectorIdToIndex(), 
                        pair => pair.Scalar
                    );

            if (indexScalarDictionary.Count > 0)
            {
                bivector = BivectorStorage<T>.CreateBivector(indexScalarDictionary);
                return true;
            }

            bivector = null;
            return false;
        }


        public IEnumerable<ulong> GetIds()
        {
            return GetLinVectorIdScalarStorage().GetIndices();
        }

        public IEnumerable<GradeIndexRecord> GetGradeIndexRecords()
        {
            return GetLinVectorIdScalarStorage().GetIndices()
                .Select(id => id.BasisBladeIdToGradeIndex());
        }

        public IEnumerable<BasisBlade> GetBasisBlades()
        {
            return GetLinVectorIdScalarStorage().GetIndices().Select(
                id => id.CreateBasisBlade()
            );
        }

        public IEnumerable<T> GetScalars()
        {
            return GetLinVectorIdScalarStorage().GetScalars();
        }

        public IEnumerable<BasisTerm<T>> GetTerms()
        {
            return GetLinVectorIdScalarStorage().GetIndexScalarRecords().Select(pair => 
                pair.Scalar.CreateBasisTerm(pair.Index)
            );
        }

        public IEnumerable<BasisTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (id, scalar) in GetLinVectorIdScalarStorage().GetIndexScalarRecords())
            {
                if (idSelection(id))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public IEnumerable<BasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (id, scalar) in GetLinVectorIdScalarStorage().GetIndexScalarRecords())
            {
                var (grade, index) = id.BasisBladeIdToGradeIndex();

                if (gradeIndexSelection(grade, index))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public IEnumerable<BasisTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (id, scalar) in GetLinVectorIdScalarStorage().GetIndexScalarRecords())
            {
                if (scalarSelection(scalar))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public IEnumerable<BasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (id, scalar) in GetLinVectorIdScalarStorage().GetIndexScalarRecords())
            {
                if (idScalarSelection(id, scalar))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public IEnumerable<BasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (id, scalar) in GetLinVectorIdScalarStorage().GetIndexScalarRecords())
            {
                var (grade, index) = id.BasisBladeIdToGradeIndex();

                if (gradeIndexScalarSelection(grade, index, scalar))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords()
        {
            return GetLinVectorIdScalarStorage().GetIndexScalarRecords();
        }

        public IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords(uint grade)
        {
            return GetLinVectorIdScalarStorage()
                .GetIndexScalarRecords()
                .Where(idScalar => idScalar.Index.IsBasisBladeOfGrade(grade))
                .Select(idScalar => 
                    new IndexScalarRecord<T>(
                        idScalar.Index.BasisBladeIdToIndex(), 
                        idScalar.Scalar
                    )
            );
        }

        public IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return GetLinVectorIdScalarStorage()
                .GetIndexScalarRecords()
                .Select(pair =>
                {
                    var (grade, index) = pair.Index.BasisBladeIdToGradeIndex();
                    return new GradeIndexScalarRecord<T>(grade, index, pair.Scalar);
                });
        }

        
        public IGeoGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return GeoGbtMultivectorStorageUniformStack1<T>.Create(
                capacity, 
                treeDepth, 
                scalarProcessor,
                this
            );
        }

        /// <summary>
        /// Construct a binary tree representation of this storage
        /// </summary>
        /// <returns></returns>
        public LinVectorTreeStorage<T> GetBinaryTree(int treeDepth)
        {
            Debug.Assert(treeDepth > 0);
            //var treeDepth = GetIds().Max().LastOneBitPosition() + 1;

            var dict = GetIdScalarRecords()
                .ToDictionary(
                    pair => pair.Index,
                    pair => pair.Scalar
                );

            return new LinVectorTreeStorage<T>(treeDepth, dict);
        }


        public MultivectorStorage<T> ToMultivectorStorage()
        {
            return this;
        }

        public MultivectorGradedStorage<T> ToGradedMultivectorStorage()
        {
            return MultivectorGradedStorage<T>.Create(
                GetLinVectorIdScalarStorage().ToVectorGradedStorage()
            );
        }

        public VectorStorage<T> GetVectorPart()
        {
            var indexScalarDictionary =             
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return VectorStorage<T>.CreateVector(indexScalarDictionary);
        }

        public VectorStorage<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =             
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisVectorId() && scalarSelection(pair.Scalar))
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return VectorStorage<T>.CreateVector(indexScalarDictionary);
        }

        public VectorStorage<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary =             
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisVectorId() && indexScalarSelection(pair.Index.BasisBladeIdToIndex(), pair.Scalar))
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return VectorStorage<T>.CreateVector(indexScalarDictionary);
        }

        public VectorStorage<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary =             
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisVectorId() && indexSelection(pair.Index.BasisBladeIdToIndex()))
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return VectorStorage<T>.CreateVector(indexScalarDictionary);
        }

        public BivectorStorage<T> GetBivectorPart()
        {
            var indexScalarDictionary =             
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return BivectorStorage<T>.CreateBivector(indexScalarDictionary);
        }


        public BivectorStorage<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =             
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBivectorId() && scalarSelection(pair.Scalar))
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return BivectorStorage<T>.CreateBivector(indexScalarDictionary);
        }

        public BivectorStorage<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary =             
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBivectorId() && indexScalarSelection(pair.Index.BasisBladeIdToIndex(), pair.Scalar))
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return BivectorStorage<T>.CreateBivector(indexScalarDictionary);
        }

        public BivectorStorage<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary =             
                GetLinVectorIdScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBivectorId() && indexSelection(pair.Index.BasisBladeIdToIndex()))
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return BivectorStorage<T>.CreateBivector(indexScalarDictionary);
        }

        public KVectorStorage<T> GetKVectorPart(uint grade)
        {
            var indexScalarDictionary = GetLinVectorIdScalarStorage()
                .GetIndexScalarRecords()
                .Where(pair => 
                    pair.Index.IsBasisBladeOfGrade(grade)
                )
                .ToDictionary(
                    pair => pair.Index.BasisBladeIdToIndex(),
                    pair => pair.Scalar
                );

            return KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary);
        }

        public KVectorStorage<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary = GetLinVectorIdScalarStorage()
                .GetIndexScalarRecords()
                .Where(pair => 
                    pair.Index.IsBasisBladeOfGrade(grade) && 
                    scalarSelection(pair.Scalar)
                )
                .ToDictionary(
                    pair => pair.Index.BasisBladeIdToIndex(),
                    pair => pair.Scalar
                );

            return KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary);
        }

        public KVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary = GetLinVectorIdScalarStorage()
                .GetIndexScalarRecords()
                .Where(pair => 
                    pair.Index.IsBasisBladeOfGrade(grade) && 
                    indexScalarSelection(pair.Index.BasisBladeIdToIndex(), pair.Scalar)
                )
                .ToDictionary(
                    pair => pair.Index.BasisBladeIdToIndex(),
                    pair => pair.Scalar
                );

            return KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary);
        }

        public KVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary = GetLinVectorIdScalarStorage()
                .GetIndexScalarRecords()
                .Where(pair => 
                    pair.Index.IsBasisBladeOfGrade(grade) && 
                    indexSelection(pair.Index.BasisBladeIdToIndex())
                )
                .ToDictionary(
                    pair => pair.Index.BasisBladeIdToIndex(),
                    pair => pair.Scalar
                );

            return KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary);
        }
        
        public IMultivectorStorage<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var idScalarList =
                GetLinVectorIdScalarStorage().FilterByIndex(idSelection);

            return new MultivectorStorage<T>(idScalarList);
        }

        public IMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var idScalarList =
                GetLinVectorIdScalarStorage().FilterByIndex(
                    id =>
                    {
                        id.BasisBladeIdToGradeIndex(out var grade, out var index);
                        return gradeIndexSelection(grade, index);
                    }
                );

            return new MultivectorStorage<T>(idScalarList);

        }

        public IMultivectorStorage<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var idScalarList =
                GetLinVectorIdScalarStorage().FilterByScalar(scalarSelection);

            return new MultivectorStorage<T>(idScalarList);
        }

        public IMultivectorStorage<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var idScalarList =
                GetLinVectorIdScalarStorage().FilterByIndexScalar(idScalarSelection);

            return new MultivectorStorage<T>(idScalarList);
        }

        public IMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var idScalarList =
                GetLinVectorIdScalarStorage().FilterByIndexScalar(
                    (id, scalar) =>
                    {
                        id.BasisBladeIdToGradeIndex(out var grade, out var index);
                        return gradeIndexScalarSelection(grade, index, scalar);
                    }
                );

            return new MultivectorStorage<T>(idScalarList);
        }

        public Tuple<IMultivectorStorage<T>, IMultivectorStorage<T>> SplitEvenOddParts()
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (id, scalar) in GetIdScalarRecords())
            {
                if (id.BasisBladeIdToGrade().IsEven())
                    indexScalarDictionary1.Add(id, scalar);
                else
                    indexScalarDictionary2.Add(id, scalar);
            }

            return new Tuple<IMultivectorStorage<T>, IMultivectorStorage<T>>(
                indexScalarDictionary1.CreateMultivectorSparseStorage(),
                indexScalarDictionary2.CreateMultivectorSparseStorage()
            );
        }

        public Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (id, scalar) in GetLinVectorIdScalarStorage().GetIndexScalarRecords())
            {
                id.BasisBladeIdToGradeIndex(out var grade, out var index);

                if (grade != 1)
                    continue;

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
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (id, scalar) in GetLinVectorIdScalarStorage().GetIndexScalarRecords())
            {
                id.BasisBladeIdToGradeIndex(out var grade, out var index);

                if (grade != 1)
                    continue;

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
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (id, scalar) in GetLinVectorIdScalarStorage().GetIndexScalarRecords())
            {
                id.BasisBladeIdToGradeIndex(out var grade, out var index);

                if (grade != 1)
                    continue;

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