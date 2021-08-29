using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public sealed record GaMultivectorSparseStorage<T>
        : GaMultivectorStorageBase<T>, IGaMultivectorSparseStorage<T>
    {
        public static GaMultivectorSparseStorage<T> ZeroMultivector { get; }
            = new GaMultivectorSparseStorage<T>(LaVectorEmptyStorage<T>.ZeroStorage);

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> Create(T scalar)
        {
            var idScalarList = 
                scalar.CreateLaVectorZeroIndexStorage();

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> Create(ulong id, T scalar)
        {
            var idScalarList = 
                scalar.CreateLaVectorSingleIndexEvenStorage(id);

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> Create(uint grade, ulong index, T scalar)
        {
            var id = index.BasisBladeIndexToId(grade);

            var idScalarList = 
                scalar.CreateLaVectorSingleIndexEvenStorage(id);

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        public static GaMultivectorSparseStorage<T> Create(uint grade, params T[] indexScalarList)
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < (ulong) indexScalarList.Length; index++)
                idScalarDictionary.Add(
                    index.BasisBladeIndexToId(grade),
                    indexScalarList[index]
                );

            var idScalarList = 
                idScalarDictionary.CreateLaVectorStorage();

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        public static GaMultivectorSparseStorage<T> Create(uint grade, IReadOnlyList<T> indexScalarList)
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < (ulong) indexScalarList.Count; index++)
                idScalarDictionary.Add(
                    index.BasisBladeIndexToId(grade),
                    indexScalarList[(int) index]
                );

            var idScalarList = 
                idScalarDictionary.CreateLaVectorStorage();

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        public static GaMultivectorSparseStorage<T> Create(uint grade, IEnumerable<T> indexScalarList)
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
                idScalarDictionary.CreateLaVectorStorage();

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        public static GaMultivectorSparseStorage<T> Create(uint grade, IReadOnlyDictionary<ulong, T> indexScalarDictionary)
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
                idScalarDictionary.CreateLaVectorStorage();

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> Create(params T[] scalarList)
        {
            var idScalarList = 
                scalarList.CreateLaVectorDenseStorage();

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> Create(IReadOnlyList<T> scalarList)
        {
            var idScalarList = 
                scalarList.CreateLaVectorDenseStorage();

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> Create(IEnumerable<T> scalarList)
        {
            var idScalarList = 
                scalarList.CreateLaVectorDenseStorage();

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> Create(Dictionary<ulong, T> idScalarDictionary)
        {
            var idScalarList = 
                idScalarDictionary.CreateLaVectorStorage();

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> Create(ILaVectorEvenStorage<T> idScalarDictionary)
        {
            return new GaMultivectorSparseStorage<T>(idScalarDictionary);
        }


        public ILaVectorEvenStorage<T> IdScalarList { get; }
        
        private uint? _vSpaceDimension;
        public override uint MinVSpaceDimension 
            => _vSpaceDimension 
                ??= IdScalarList.GetMinVSpaceDimensionOfMultivector();

        public override int GradesCount =>
            IdScalarList.GetIndices()
                .Select(id => id.BasisBladeIdToGrade())
                .Distinct()
                .Count();

        public override int TermsCount 
            => IdScalarList.GetSparseCount();

        public override bool IsUniform 
            => true;

        public override bool IsGraded 
            => false;


        private GaMultivectorSparseStorage([NotNull] ILaVectorEvenStorage<T> idScalarDictionary)
        {
            IdScalarList = idScalarDictionary;
        }


        public GaMultivectorSparseStorage<T> GetSparseMultivectorCopy()
        {
            var idScalarList = 
                IdScalarList.GetCopy();

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        public GaMultivectorSparseStorage<T2> MapSparseMultivectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            var idScalarList = 
                IdScalarList.MapScalars(scalarMapping);

            return new GaMultivectorSparseStorage<T2>(idScalarList);
        }
        
        public GaMultivectorSparseStorage<T2> MapSparseMultivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var idScalarList = 
                IdScalarList.MapScalars(idScalarMapping);

            return new GaMultivectorSparseStorage<T2>(idScalarList);
        }
        
        public GaMultivectorSparseStorage<T2> MapSparseMultivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var idScalarList = 
                IdScalarList.MapScalars((id, scalar) => 
                    indexScalarMapping(id.BasisBladeIdToIndex(), scalar)
                );

            return new GaMultivectorSparseStorage<T2>(idScalarList);
        }

        public GaMultivectorSparseStorage<T2> MapSparseMultivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var idScalarList = 
                IdScalarList.MapScalars(
                    (id, scalar) =>
                    {
                        id.BasisBladeIdToGradeIndex(out var grade, out var index);
                        return gradeIndexScalarMapping(grade, index, scalar);
                    }
                );

            return new GaMultivectorSparseStorage<T2>(idScalarList);
        }


        public GaMultivectorSparseStorage<T> FilterSparseMultivectorByScalar(Func<T, bool> scalarFilter)
        {
            var idScalarDictionary =
                IdScalarList.FilterByScalar(scalarFilter);

            return new GaMultivectorSparseStorage<T>(idScalarDictionary);
        }

        public GaMultivectorSparseStorage<T> FilterSparseMultivectorByIdScalar(Func<ulong, T, bool> idScalarFilter)
        {
            var idScalarDictionary =
                IdScalarList.FilterByIndexScalar(idScalarFilter);

            return new GaMultivectorSparseStorage<T>(idScalarDictionary);
        }

        public GaMultivectorSparseStorage<T> FilterSparseMultivectorById(Func<ulong, bool> idFilter)
        {
            var idScalarDictionary =
                IdScalarList.FilterByIndex(idFilter);

            return new GaMultivectorSparseStorage<T>(idScalarDictionary);
        }


        public override bool ContainsVectorPart()
        {
            return IdScalarList.GetIndices().Any(key => key.IsBasisVectorId());
        }

        public override bool TryGetScalar(out T value)
        {
            return IdScalarList.TryGetScalar(0, out value);
        }

        public override bool ContainsBivectorPart()
        {
            return IdScalarList.GetIndices().Any(key => key.IsBasisBivectorId());
        }

        public override bool ContainsKVectorPart(uint grade)
        {
            return IdScalarList.GetIndices().Any(id => grade == id.BasisBladeIdToGrade());
        }


        public override bool IsEmpty()
        {
            return IdScalarList.GetSparseCount() == 0;
        }
        

        public override bool IsScalar()
        {
            return !IdScalarList.GetIndices().Any(key => key > 0);
        }

        public override bool IsVector()
        {
            return IdScalarList.GetIndices().All(key => key.IsBasicPattern());
        }

        public override bool IsBivector()
        {
            return IdScalarList.GetIndices().All(key => key.BasisBladeIdToGrade() == 2);
        }

        public override bool IsKVector()
        {
            return IdScalarList.GetIndices()
                .Select(key => key.BasisBladeIdToGrade())
                .Distinct()
                .Count() < 2;
        }

        public override bool IsKVector(uint grade)
        {
            return IdScalarList.GetIndices().All(key => key.BasisBladeIdToGrade() == grade);
        }

        public override ulong GetMinId()
        {
            return IdScalarList.GetMinIndex();
        }

        public override ulong GetMaxId()
        {
            return IdScalarList.GetMaxIndex();
        }

        public override ulong GetMinId(uint grade)
        {
            return IdScalarList
                .GetIndexScalarRecords()
                .Select(idScalar => idScalar.Index)
                .Where(id => id.BasisBladeIdToGrade() == grade)
                .Min();
        }

        public override ulong GetMaxId(uint grade)
        {
            return IdScalarList
                .GetIndexScalarRecords()
                .Select(idScalar => idScalar.Index)
                .Where(id => id.BasisBladeIdToGrade() == grade)
                .Max();
        }

        public override uint GetMinGrade()
        {
            return GetGrades().TryGetMinValue(out var grade)
                ? grade
                : 0;
        }

        public override uint GetMaxGrade()
        {
            return GetGrades().TryGetMaxValue(out var grade)
                ? grade
                : 0;
        }

        public override ulong GetMinIndex(uint grade)
        {
            return IdScalarList
                .GetIndexScalarRecords()
                .Select(idScalar => idScalar.Index)
                .Where(id => id.BasisBladeIdToGrade() == grade)
                .Min()
                .BasisBivectorIdToIndex();
        }

        public override ulong GetMaxIndex(uint grade)
        {
            return IdScalarList
                .GetIndexScalarRecords()
                .Select(idScalar => idScalar.Index)
                .Where(id => id.BasisBladeIdToGrade() == grade)
                .Max()
                .BasisBivectorIdToIndex();
        }

        public override IEnumerable<uint> GetGrades()
        {
            return IdScalarList.GetIndices()
                .Select(id => id.BasisBladeIdToGrade())
                .Distinct();
        }


        public override bool ContainsTerm(ulong id)
        {
            return IdScalarList.ContainsIndex(id);
        }

        public override bool ContainsTerm(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            return IdScalarList.ContainsIndex(id);
        }

        public override bool ContainsScalarPart()
        {
            return IdScalarList.GetIndices().Any(key => key == 0);
        }

        public override bool TryGetTermScalar(ulong id, out T value)
        {
            if (IdScalarList.TryGetScalar(id, out value))
                return true;

            value = default;
            return false;
        }

        public override bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            var id = index.BasisBladeIndexToId(grade);

            if (IdScalarList.TryGetScalar(id, out value))
                return true;

            value = default;
            return false;
        }


        public override bool TryGetKVectorPart(uint grade, out IGaKVectorStorage<T> kVector)
        {
            var indexScalarDictionary = IdScalarList
                .GetIndexScalarRecords()
                .Where(pair => 
                    pair.Index.BasisBladeIdToGrade() == grade
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

            kVector = GaKVectorStorage<T>.Create(grade, indexScalarDictionary);
            return true;
        }


        public override bool TryGetScalarPartList(out ILaVectorEvenStorage<T> indexScalarList)
        {
            if (IdScalarList.TryGetScalar(0, out var scalar))
            {
                indexScalarList = scalar.CreateLaVectorZeroIndexStorage();
                return true;
            }

            indexScalarList = null;
            return false;
        }

        public override bool TryGetVectorPartList(out ILaVectorEvenStorage<T> indexScalarDictionary)
        {
            var dict =
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            if (dict.Count > 0)
            {
                indexScalarDictionary = dict.CreateLaVectorStorage();
                return true;
            }

            indexScalarDictionary = LaVectorEmptyStorage<T>.ZeroStorage;
            return false;
        }

        public override bool TryGetBivectorPartList(out ILaVectorEvenStorage<T> indexScalarDictionary)
        {
            var dict =
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            if (dict.Count > 0)
            {
                indexScalarDictionary = dict.CreateLaVectorStorage();
                return true;
            }

            indexScalarDictionary = LaVectorEmptyStorage<T>.ZeroStorage;
            return false;
        }

        public override bool TryGetKVectorPartList(uint grade, out ILaVectorEvenStorage<T> indexScalarDictionary)
        {
            if (grade == 0)
            {
                if (IdScalarList.TryGetScalar(0, out var scalar))
                {
                    indexScalarDictionary = scalar.CreateLaVectorZeroIndexStorage();
                    return true;
                }

                indexScalarDictionary = LaVectorEmptyStorage<T>.ZeroStorage;
                return false;
            }

            var dict =
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.BasisBladeIdToGrade() == grade)
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            if (dict.Count > 0)
            {
                indexScalarDictionary = dict.CreateLaVectorStorage();
                return true;
            }

            indexScalarDictionary = LaVectorEmptyStorage<T>.ZeroStorage;
            return false;
        }

        public override ILaVectorEvenStorage<T> GetIdScalarList()
        {
            return IdScalarList;
        }

        public override ILaVectorGradedStorage<T> GetGradeIndexScalarList()
        {
            return IdScalarList.GetIndexScalarRecords().GroupBy(
                pair => pair.Index.BasisBladeIdToGrade()
            ).ToDictionary(
                g => g.Key,
                g => g.ToDictionary(
                    pair => pair.Index.BasisBladeIdToIndex(), 
                    pair => pair.Scalar
                )
            ).CreateLaVectorGradedStorage();
        }

        public override ILaVectorEvenStorage<T> GetIndexScalarList(uint grade)
        {
            return TryGetKVectorPartList(grade, out var indexScalarList)
                ? indexScalarList 
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }


        public override bool TryGetTerm(ulong id, out GaBasisTerm<T> term)
        {
            if (TryGetTermScalar(id, out var value))
            {
                term = value.CreateBasisTerm(id);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term)
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

        public override ILaVectorEvenStorage<T> GetScalarPartList()
        {
            return IdScalarList.TryGetScalar(0, out var scalar)
                ? scalar.CreateLaVectorZeroIndexStorage()
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public override ILaVectorEvenStorage<T> GetVectorPartList()
        {
            var indexScalarDictionary =
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(), 
                        pair => pair.Scalar
                    );

            return indexScalarDictionary.Count > 0
                ? indexScalarDictionary.CreateLaVectorStorage()
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public override ILaVectorEvenStorage<T> GetBivectorPartList()
        {
            var indexScalarDictionary =
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(), 
                        pair => pair.Scalar
                    );

            return indexScalarDictionary.Count > 0
                ? indexScalarDictionary.CreateLaVectorStorage()
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public override ILaVectorEvenStorage<T> GetKVectorPartList(uint grade)
        {
            var indexScalarDictionary =
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.BasisBladeIdToGrade() == grade)
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(), 
                        pair => pair.Scalar
                    );

            return indexScalarDictionary.Count > 0
                ? indexScalarDictionary.CreateLaVectorStorage()
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public override bool TryGetScalarPart(out IGaScalarStorage<T> scalar)
        {
            if (IdScalarList.TryGetScalar(0, out var s))
            {
                scalar = GaScalarStorage<T>.Create(s);
                return true;
            }

            scalar = null;
            return false;
        }

        public override bool TryGetVectorPart(out IGaVectorStorage<T> vector)
        {
            var indexScalarDictionary =
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisVectorIdToIndex(), 
                        pair => pair.Scalar
                    );

            if (indexScalarDictionary.Count > 0)
            {
                vector = GaVectorStorage<T>.Create(indexScalarDictionary);
                return true;
            }

            vector = null;
            return false;
        }

        public override bool TryGetBivectorPart(out IGaBivectorStorage<T> bivector)
        {
            var indexScalarDictionary =
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisBivectorIdToIndex(), 
                        pair => pair.Scalar
                    );

            if (indexScalarDictionary.Count > 0)
            {
                bivector = GaBivectorStorage<T>.Create(indexScalarDictionary);
                return true;
            }

            bivector = null;
            return false;
        }


        public override IEnumerable<ulong> GetIds()
        {
            return IdScalarList.GetIndices();
        }

        public override IEnumerable<GradeIndexRecord> GetGradeIndexRecords()
        {
            return IdScalarList.GetIndices()
                .Select(id => id.BasisBladeIdToGradeIndex());
        }

        public override IEnumerable<GaBasisBlade> GetBasisBlades()
        {
            return IdScalarList.GetIndices().Select(
                id => id.CreateBasisBlade()
            );
        }

        public override IEnumerable<T> GetScalars()
        {
            return IdScalarList.GetScalars();
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms()
        {
            return IdScalarList.GetIndexScalarRecords().Select(pair => 
                pair.Scalar.CreateBasisTerm(pair.Index)
            );
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (id, scalar) in IdScalarList.GetIndexScalarRecords())
            {
                if (idSelection(id))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (id, scalar) in IdScalarList.GetIndexScalarRecords())
            {
                var (grade, index) = id.BasisBladeIdToGradeIndex();

                if (gradeIndexSelection(grade, index))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (id, scalar) in IdScalarList.GetIndexScalarRecords())
            {
                if (scalarSelection(scalar))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (id, scalar) in IdScalarList.GetIndexScalarRecords())
            {
                if (idScalarSelection(id, scalar))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (id, scalar) in IdScalarList.GetIndexScalarRecords())
            {
                var (grade, index) = id.BasisBladeIdToGradeIndex();

                if (gradeIndexScalarSelection(grade, index, scalar))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public override IEnumerable<IndexScalarRecord<T>> GetIdScalarRecords()
        {
            return IdScalarList.GetIndexScalarRecords();
        }

        public override IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords(uint grade)
        {
            return IdScalarList
                .GetIndexScalarRecords()
                .Where(idScalar => idScalar.Index.BasisBladeIdToGrade() == grade)
                .Select(idScalar => 
                    new IndexScalarRecord<T>(
                        idScalar.Index.BasisBladeIdToIndex(), 
                        idScalar.Scalar
                    )
            );
        }

        public override IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return IdScalarList
                .GetIndexScalarRecords()
                .Select(pair =>
                {
                    var (grade, index) = pair.Index.BasisBladeIdToGradeIndex();
                    return new GradeIndexScalarRecord<T>(grade, index, pair.Scalar);
                });
        }

        
        public override IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IScalarProcessor<T> scalarProcessor)
        {
            return GaGbtMultivectorStorageUniformStack1<T>.Create(
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
        public override LaVectorTreeStorage<T> GetBinaryTree(int treeDepth)
        {
            Debug.Assert(treeDepth > 0);
            //var treeDepth = GetIds().Max().LastOneBitPosition() + 1;

            var dict = GetIdScalarRecords()
                .ToDictionary(
                    pair => pair.Index,
                    pair => pair.Scalar
                );

            return new LaVectorTreeStorage<T>(treeDepth, dict);
        }


        public override IGaMultivectorSparseStorage<T> ToSparseMultivector()
        {
            return this;
        }

        public override IGaMultivectorGradedStorage<T> ToGradedMultivector()
        {
            return GaMultivectorGradedStorage<T>.Create(
                IdScalarList.ToGradedList()
            );
        }

        public override IGaVectorStorage<T> GetVectorPart()
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return GaVectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisVectorId() && scalarSelection(pair.Scalar))
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return GaVectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisVectorId() && indexScalarSelection(pair.Index.BasisBladeIdToIndex(), pair.Scalar))
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return GaVectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisVectorId() && indexSelection(pair.Index.BasisBladeIdToIndex()))
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return GaVectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaBivectorStorage<T> GetBivectorPart()
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return GaBivectorStorage<T>.Create(indexScalarDictionary);
        }


        public override IGaBivectorStorage<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBivectorId() && scalarSelection(pair.Scalar))
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return GaBivectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaBivectorStorage<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBivectorId() && indexScalarSelection(pair.Index.BasisBladeIdToIndex(), pair.Scalar))
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return GaBivectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaBivectorStorage<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetIndexScalarRecords()
                    .Where(pair => pair.Index.IsBasisBivectorId() && indexSelection(pair.Index.BasisBladeIdToIndex()))
                    .ToDictionary(
                        pair => pair.Index.BasisBladeIdToIndex(),
                        pair => pair.Scalar
                    );

            return GaBivectorStorage<T>.Create(indexScalarDictionary);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade)
        {
            var indexScalarDictionary = IdScalarList
                .GetIndexScalarRecords()
                .Where(pair => 
                    pair.Index.BasisBladeIdToGrade() == grade
                )
                .ToDictionary(
                    pair => pair.Index.BasisBladeIdToIndex(),
                    pair => pair.Scalar
                );

            return GaKVectorStorage<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary = IdScalarList
                .GetIndexScalarRecords()
                .Where(pair => 
                    pair.Index.BasisBladeIdToGrade() == grade && 
                    scalarSelection(pair.Scalar)
                )
                .ToDictionary(
                    pair => pair.Index.BasisBladeIdToIndex(),
                    pair => pair.Scalar
                );

            return GaKVectorStorage<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary = IdScalarList
                .GetIndexScalarRecords()
                .Where(pair => 
                    pair.Index.BasisBladeIdToGrade() == grade && 
                    indexScalarSelection(pair.Index.BasisBladeIdToIndex(), pair.Scalar)
                )
                .ToDictionary(
                    pair => pair.Index.BasisBladeIdToIndex(),
                    pair => pair.Scalar
                );

            return GaKVectorStorage<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary = IdScalarList
                .GetIndexScalarRecords()
                .Where(pair => 
                    pair.Index.BasisBladeIdToGrade() == grade && 
                    indexSelection(pair.Index.BasisBladeIdToIndex())
                )
                .ToDictionary(
                    pair => pair.Index.BasisBladeIdToIndex(),
                    pair => pair.Scalar
                );

            return GaKVectorStorage<T>.Create(grade, indexScalarDictionary);
        }
        
        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var idScalarList =
                IdScalarList.FilterByIndex(idSelection);

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var idScalarList =
                IdScalarList.FilterByIndex(
                    id =>
                    {
                        id.BasisBladeIdToGradeIndex(out var grade, out var index);
                        return gradeIndexSelection(grade, index);
                    }
                );

            return new GaMultivectorSparseStorage<T>(idScalarList);

        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var idScalarList =
                IdScalarList.FilterByScalar(scalarSelection);

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var idScalarList =
                IdScalarList.FilterByIndexScalar(idScalarSelection);

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var idScalarList =
                IdScalarList.FilterByIndexScalar(
                    (id, scalar) =>
                    {
                        id.BasisBladeIdToGradeIndex(out var grade, out var index);
                        return gradeIndexScalarSelection(grade, index, scalar);
                    }
                );

            return new GaMultivectorSparseStorage<T>(idScalarList);
        }

        public override Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (id, scalar) in IdScalarList.GetIndexScalarRecords())
            {
                id.BasisBladeIdToGradeIndex(out var grade, out var index);

                if (grade != 1)
                    continue;

                if (indexSelection(index))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                indexScalarDictionary1.CreateGaVectorStorage(),
                indexScalarDictionary2.CreateGaVectorStorage()
            );
        }

        public override Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (id, scalar) in IdScalarList.GetIndexScalarRecords())
            {
                id.BasisBladeIdToGradeIndex(out var grade, out var index);

                if (grade != 1)
                    continue;

                if (indexScalarSelection(index, scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                indexScalarDictionary1.CreateGaVectorStorage(),
                indexScalarDictionary2.CreateGaVectorStorage()
            );
        }

        public override Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (id, scalar) in IdScalarList.GetIndexScalarRecords())
            {
                id.BasisBladeIdToGradeIndex(out var grade, out var index);

                if (grade != 1)
                    continue;

                if (scalarSelection(scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                indexScalarDictionary1.CreateGaVectorStorage(),
                indexScalarDictionary2.CreateGaVectorStorage()
            );
        }
    }
}