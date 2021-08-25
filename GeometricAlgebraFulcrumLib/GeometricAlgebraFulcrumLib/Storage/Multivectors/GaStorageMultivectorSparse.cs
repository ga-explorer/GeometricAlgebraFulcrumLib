using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Structures;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public sealed record GaStorageMultivectorSparse<T>
        : GaStorageMultivectorBase<T>, IGaStorageMultivectorSparse<T>
    {
        public static GaStorageMultivectorSparse<T> ZeroMultivector { get; }
            = new GaStorageMultivectorSparse<T>(GaListEvenEmpty<T>.EmptyList);

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(T scalar)
        {
            var idScalarList = 
                scalar.CreateEvenListSingleKeyZero();

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(ulong id, T scalar)
        {
            var idScalarList = 
                scalar.CreateEvenListSingleKey(id);

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(uint grade, ulong index, T scalar)
        {
            var id = index.BasisBladeIndexToId(grade);

            var idScalarList = 
                scalar.CreateEvenListSingleKey(id);

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        public static GaStorageMultivectorSparse<T> Create(uint grade, params T[] indexScalarList)
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < (ulong) indexScalarList.Length; index++)
                idScalarDictionary.Add(
                    index.BasisBladeIndexToId(grade),
                    indexScalarList[index]
                );

            var idScalarList = 
                idScalarDictionary.CreateEvenList();

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        public static GaStorageMultivectorSparse<T> Create(uint grade, IReadOnlyList<T> indexScalarList)
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < (ulong) indexScalarList.Count; index++)
                idScalarDictionary.Add(
                    index.BasisBladeIndexToId(grade),
                    indexScalarList[(int) index]
                );

            var idScalarList = 
                idScalarDictionary.CreateEvenList();

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        public static GaStorageMultivectorSparse<T> Create(uint grade, IEnumerable<T> indexScalarList)
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
                idScalarDictionary.CreateEvenList();

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        public static GaStorageMultivectorSparse<T> Create(uint grade, IReadOnlyDictionary<ulong, T> indexScalarDictionary)
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
                idScalarDictionary.CreateEvenList();

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(params T[] scalarList)
        {
            var idScalarList = 
                scalarList.CreateEvenListDense();

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(IReadOnlyList<T> scalarList)
        {
            var idScalarList = 
                scalarList.CreateEvenListDense();

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(IEnumerable<T> scalarList)
        {
            var idScalarList = 
                scalarList.CreateEvenListDense();

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(Dictionary<ulong, T> idScalarDictionary)
        {
            var idScalarList = 
                idScalarDictionary.CreateEvenList();

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(IGaListEven<T> idScalarDictionary)
        {
            return new GaStorageMultivectorSparse<T>(idScalarDictionary);
        }


        public IGaListEven<T> IdScalarList { get; }
        
        private uint? _vSpaceDimension;
        public override uint MinVSpaceDimension 
            => _vSpaceDimension 
                ??= IdScalarList.GetMinVSpaceDimensionOfMultivector();

        public override int GradesCount =>
            IdScalarList.GetKeys()
                .Select(id => id.BasisBladeIdToGrade())
                .Distinct()
                .Count();

        public override int TermsCount 
            => IdScalarList.GetSparseCount();

        public override bool IsUniform 
            => true;

        public override bool IsGraded 
            => false;


        private GaStorageMultivectorSparse([NotNull] IGaListEven<T> idScalarDictionary)
        {
            IdScalarList = idScalarDictionary;
        }


        public GaStorageMultivectorSparse<T> GetSparseMultivectorCopy()
        {
            var idScalarList = 
                IdScalarList.GetCopy();

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        public GaStorageMultivectorSparse<T2> MapSparseMultivectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            var idScalarList = 
                IdScalarList.MapValues(scalarMapping);

            return new GaStorageMultivectorSparse<T2>(idScalarList);
        }
        
        public GaStorageMultivectorSparse<T2> MapSparseMultivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var idScalarList = 
                IdScalarList.MapValues(idScalarMapping);

            return new GaStorageMultivectorSparse<T2>(idScalarList);
        }
        
        public GaStorageMultivectorSparse<T2> MapSparseMultivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var idScalarList = 
                IdScalarList.MapValues((id, scalar) => 
                    indexScalarMapping(id.BasisBladeIdToIndex(), scalar)
                );

            return new GaStorageMultivectorSparse<T2>(idScalarList);
        }

        public GaStorageMultivectorSparse<T2> MapSparseMultivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var idScalarList = 
                IdScalarList.MapValues(
                    (id, scalar) =>
                    {
                        id.BasisBladeIdToGradeIndex(out var grade, out var index);
                        return gradeIndexScalarMapping(grade, index, scalar);
                    }
                );

            return new GaStorageMultivectorSparse<T2>(idScalarList);
        }


        public GaStorageMultivectorSparse<T> FilterSparseMultivectorByScalar(Func<T, bool> scalarFilter)
        {
            var idScalarDictionary =
                IdScalarList.FilterByValue(scalarFilter);

            return new GaStorageMultivectorSparse<T>(idScalarDictionary);
        }

        public GaStorageMultivectorSparse<T> FilterSparseMultivectorByIdScalar(Func<ulong, T, bool> idScalarFilter)
        {
            var idScalarDictionary =
                IdScalarList.FilterByKeyValue(idScalarFilter);

            return new GaStorageMultivectorSparse<T>(idScalarDictionary);
        }

        public GaStorageMultivectorSparse<T> FilterSparseMultivectorById(Func<ulong, bool> idFilter)
        {
            var idScalarDictionary =
                IdScalarList.FilterByKey(idFilter);

            return new GaStorageMultivectorSparse<T>(idScalarDictionary);
        }


        public override bool ContainsVectorPart()
        {
            return IdScalarList.GetKeys().Any(key => key.IsBasisVectorId());
        }

        public override bool TryGetScalar(out T value)
        {
            return IdScalarList.TryGetValue(0, out value);
        }

        public override bool ContainsBivectorPart()
        {
            return IdScalarList.GetKeys().Any(key => key.IsBasisBivectorId());
        }

        public override bool ContainsKVectorPart(uint grade)
        {
            return IdScalarList.GetKeys().Any(id => grade == id.BasisBladeIdToGrade());
        }


        public override bool IsEmpty()
        {
            return IdScalarList.GetSparseCount() == 0;
        }
        

        public override bool IsScalar()
        {
            return !IdScalarList.GetKeys().Any(key => key > 0);
        }

        public override bool IsVector()
        {
            return IdScalarList.GetKeys().All(key => key.IsBasicPattern());
        }

        public override bool IsBivector()
        {
            return IdScalarList.GetKeys().All(key => key.BasisBladeIdToGrade() == 2);
        }

        public override bool IsKVector()
        {
            return IdScalarList.GetKeys()
                .Select(key => key.BasisBladeIdToGrade())
                .Distinct()
                .Count() < 2;
        }

        public override bool IsKVector(uint grade)
        {
            return IdScalarList.GetKeys().All(key => key.BasisBladeIdToGrade() == grade);
        }

        public override ulong GetMinId()
        {
            return IdScalarList.GetMinKey();
        }

        public override ulong GetMaxId()
        {
            return IdScalarList.GetMaxKey();
        }

        public override ulong GetMinId(uint grade)
        {
            return IdScalarList
                .GetKeyValueRecords()
                .Select(idScalar => idScalar.Key)
                .Where(id => id.BasisBladeIdToGrade() == grade)
                .Min();
        }

        public override ulong GetMaxId(uint grade)
        {
            return IdScalarList
                .GetKeyValueRecords()
                .Select(idScalar => idScalar.Key)
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
                .GetKeyValueRecords()
                .Select(idScalar => idScalar.Key)
                .Where(id => id.BasisBladeIdToGrade() == grade)
                .Min()
                .BasisBivectorIdToIndex();
        }

        public override ulong GetMaxIndex(uint grade)
        {
            return IdScalarList
                .GetKeyValueRecords()
                .Select(idScalar => idScalar.Key)
                .Where(id => id.BasisBladeIdToGrade() == grade)
                .Max()
                .BasisBivectorIdToIndex();
        }

        public override IEnumerable<uint> GetGrades()
        {
            return IdScalarList.GetKeys()
                .Select(id => id.BasisBladeIdToGrade())
                .Distinct();
        }


        public override bool ContainsTerm(ulong id)
        {
            return IdScalarList.ContainsKey(id);
        }

        public override bool ContainsTerm(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            return IdScalarList.ContainsKey(id);
        }

        public override bool ContainsScalarPart()
        {
            return IdScalarList.GetKeys().Any(key => key == 0);
        }

        public override bool TryGetTermScalar(ulong id, out T value)
        {
            if (IdScalarList.TryGetValue(id, out value))
                return true;

            value = default;
            return false;
        }

        public override bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            var id = index.BasisBladeIndexToId(grade);

            if (IdScalarList.TryGetValue(id, out value))
                return true;

            value = default;
            return false;
        }


        public override bool TryGetKVectorPart(uint grade, out IGaStorageKVector<T> kVector)
        {
            var indexScalarDictionary = IdScalarList
                .GetKeyValueRecords()
                .Where(pair => 
                    pair.Key.BasisBladeIdToGrade() == grade
                )
                .ToDictionary(
                    pair => pair.Key.BasisBladeIdToIndex(),
                    pair => pair.Value
                );

            if (indexScalarDictionary.Count == 0)
            {
                kVector = null;
                return false;
            }

            kVector = GaStorageKVector<T>.Create(grade, indexScalarDictionary);
            return true;
        }


        public override bool TryGetScalarPartList(out IGaListEven<T> indexScalarList)
        {
            if (IdScalarList.TryGetValue(0, out var scalar))
            {
                indexScalarList = scalar.CreateEvenListSingleKeyZero();
                return true;
            }

            indexScalarList = null;
            return false;
        }

        public override bool TryGetVectorPartList(out IGaListEven<T> indexScalarDictionary)
        {
            var dict =
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIdToIndex(),
                        pair => pair.Value
                    );

            if (dict.Count > 0)
            {
                indexScalarDictionary = dict.CreateEvenList();
                return true;
            }

            indexScalarDictionary = GaListEvenEmpty<T>.EmptyList;
            return false;
        }

        public override bool TryGetBivectorPartList(out IGaListEven<T> indexScalarDictionary)
        {
            var dict =
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIdToIndex(),
                        pair => pair.Value
                    );

            if (dict.Count > 0)
            {
                indexScalarDictionary = dict.CreateEvenList();
                return true;
            }

            indexScalarDictionary = GaListEvenEmpty<T>.EmptyList;
            return false;
        }

        public override bool TryGetKVectorPartList(uint grade, out IGaListEven<T> indexScalarDictionary)
        {
            if (grade == 0)
            {
                if (IdScalarList.TryGetValue(0, out var scalar))
                {
                    indexScalarDictionary = scalar.CreateEvenListSingleKeyZero();
                    return true;
                }

                indexScalarDictionary = GaListEvenEmpty<T>.EmptyList;
                return false;
            }

            var dict =
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.BasisBladeIdToGrade() == grade)
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIdToIndex(),
                        pair => pair.Value
                    );

            if (dict.Count > 0)
            {
                indexScalarDictionary = dict.CreateEvenList();
                return true;
            }

            indexScalarDictionary = GaListEvenEmpty<T>.EmptyList;
            return false;
        }

        public override IGaListEven<T> GetIdScalarList()
        {
            return IdScalarList;
        }

        public override IGaListGraded<T> GetGradeIndexScalarList()
        {
            return IdScalarList.GetKeyValueRecords().GroupBy(
                pair => pair.Key.BasisBladeIdToGrade()
            ).ToDictionary(
                g => g.Key,
                g => g.ToDictionary(
                    pair => pair.Key.BasisBladeIdToIndex(), 
                    pair => pair.Value
                )
            ).CreateGradedList();
        }

        public override IGaListEven<T> GetIndexScalarList(uint grade)
        {
            return TryGetKVectorPartList(grade, out var indexScalarList)
                ? indexScalarList 
                : GaListEvenEmpty<T>.EmptyList;
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

        public override IGaListEven<T> GetScalarPartList()
        {
            return IdScalarList.TryGetValue(0, out var scalar)
                ? scalar.CreateEvenListSingleKeyZero()
                : GaListEvenEmpty<T>.EmptyList;
        }

        public override IGaListEven<T> GetVectorPartList()
        {
            var indexScalarDictionary =
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIdToIndex(), 
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count > 0
                ? indexScalarDictionary.CreateEvenList()
                : GaListEvenEmpty<T>.EmptyList;
        }

        public override IGaListEven<T> GetBivectorPartList()
        {
            var indexScalarDictionary =
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIdToIndex(), 
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count > 0
                ? indexScalarDictionary.CreateEvenList()
                : GaListEvenEmpty<T>.EmptyList;
        }

        public override IGaListEven<T> GetKVectorPartList(uint grade)
        {
            var indexScalarDictionary =
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.BasisBladeIdToGrade() == grade)
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIdToIndex(), 
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count > 0
                ? indexScalarDictionary.CreateEvenList()
                : GaListEvenEmpty<T>.EmptyList;
        }

        public override bool TryGetScalarPart(out IGaStorageScalar<T> scalar)
        {
            if (IdScalarList.TryGetValue(0, out var s))
            {
                scalar = GaStorageScalar<T>.Create(s);
                return true;
            }

            scalar = null;
            return false;
        }

        public override bool TryGetVectorPart(out IGaStorageVector<T> vector)
        {
            var indexScalarDictionary =
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisVectorIdToIndex(), 
                        pair => pair.Value
                    );

            if (indexScalarDictionary.Count > 0)
            {
                vector = GaStorageVector<T>.Create(indexScalarDictionary);
                return true;
            }

            vector = null;
            return false;
        }

        public override bool TryGetBivectorPart(out IGaStorageBivector<T> bivector)
        {
            var indexScalarDictionary =
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBivectorIdToIndex(), 
                        pair => pair.Value
                    );

            if (indexScalarDictionary.Count > 0)
            {
                bivector = GaStorageBivector<T>.Create(indexScalarDictionary);
                return true;
            }

            bivector = null;
            return false;
        }


        public override IEnumerable<ulong> GetIds()
        {
            return IdScalarList.GetKeys();
        }

        public override IEnumerable<GaRecordGradeKey> GetGradeIndexRecords()
        {
            return IdScalarList.GetKeys()
                .Select(id => id.BasisBladeIdToGradeIndex());
        }

        public override IEnumerable<GaBasisBlade> GetBasisBlades()
        {
            return IdScalarList.GetKeys().Select(
                id => id.CreateBasisBlade()
            );
        }

        public override IEnumerable<T> GetScalars()
        {
            return IdScalarList.GetValues();
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms()
        {
            return IdScalarList.GetKeyValueRecords().Select(pair => 
                pair.Value.CreateBasisTerm(pair.Key)
            );
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (id, scalar) in IdScalarList.GetKeyValueRecords())
            {
                if (idSelection(id))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (id, scalar) in IdScalarList.GetKeyValueRecords())
            {
                var (grade, index) = id.BasisBladeIdToGradeIndex();

                if (gradeIndexSelection(grade, index))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (id, scalar) in IdScalarList.GetKeyValueRecords())
            {
                if (scalarSelection(scalar))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (id, scalar) in IdScalarList.GetKeyValueRecords())
            {
                if (idScalarSelection(id, scalar))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (id, scalar) in IdScalarList.GetKeyValueRecords())
            {
                var (grade, index) = id.BasisBladeIdToGradeIndex();

                if (gradeIndexScalarSelection(grade, index, scalar))
                    yield return scalar.CreateBasisTerm(id);
            }
        }

        public override IEnumerable<GaRecordKeyValue<T>> GetIdScalarRecords()
        {
            return IdScalarList.GetKeyValueRecords();
        }

        public override IEnumerable<GaRecordKeyValue<T>> GetIndexScalarRecords(uint grade)
        {
            return IdScalarList
                .GetKeyValueRecords()
                .Where(idScalar => idScalar.Key.BasisBladeIdToGrade() == grade)
                .Select(idScalar => 
                    new GaRecordKeyValue<T>(
                        idScalar.Key.BasisBladeIdToIndex(), 
                        idScalar.Value
                    )
            );
        }

        public override IEnumerable<GaRecordGradeKeyValue<T>> GetGradeIndexScalarRecords()
        {
            return IdScalarList
                .GetKeyValueRecords()
                .Select(pair =>
                {
                    var (grade, index) = pair.Key.BasisBladeIdToGradeIndex();
                    return new GaRecordGradeKeyValue<T>(grade, index, pair.Value);
                });
        }

        
        public override IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IGaScalarProcessor<T> scalarProcessor)
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
        public override GaListEvenTree<T> GetBinaryTree(int treeDepth)
        {
            Debug.Assert(treeDepth > 0);
            //var treeDepth = GetIds().Max().LastOneBitPosition() + 1;

            var dict = GetIdScalarRecords()
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );

            return new GaListEvenTree<T>(treeDepth, dict);
        }


        public override IGaStorageMultivectorSparse<T> ToSparseMultivector()
        {
            return this;
        }

        public override IGaStorageMultivectorGraded<T> ToGradedMultivector()
        {
            return GaStorageMultivectorGraded<T>.Create(
                IdScalarList.ToGradedList()
            );
        }

        public override IGaStorageVector<T> GetVectorPart()
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIdToIndex(),
                        pair => pair.Value
                    );

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.IsBasisVectorId() && scalarSelection(pair.Value))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIdToIndex(),
                        pair => pair.Value
                    );

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.IsBasisVectorId() && indexScalarSelection(pair.Key.BasisBladeIdToIndex(), pair.Value))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIdToIndex(),
                        pair => pair.Value
                    );

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.IsBasisVectorId() && indexSelection(pair.Key.BasisBladeIdToIndex()))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIdToIndex(),
                        pair => pair.Value
                    );

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart()
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIdToIndex(),
                        pair => pair.Value
                    );

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }


        public override IGaStorageBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.IsBasisBivectorId() && scalarSelection(pair.Value))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIdToIndex(),
                        pair => pair.Value
                    );

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.IsBasisBivectorId() && indexScalarSelection(pair.Key.BasisBladeIdToIndex(), pair.Value))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIdToIndex(),
                        pair => pair.Value
                    );

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary =             
                IdScalarList
                    .GetKeyValueRecords()
                    .Where(pair => pair.Key.IsBasisBivectorId() && indexSelection(pair.Key.BasisBladeIdToIndex()))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIdToIndex(),
                        pair => pair.Value
                    );

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade)
        {
            var indexScalarDictionary = IdScalarList
                .GetKeyValueRecords()
                .Where(pair => 
                    pair.Key.BasisBladeIdToGrade() == grade
                )
                .ToDictionary(
                    pair => pair.Key.BasisBladeIdToIndex(),
                    pair => pair.Value
                );

            return GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary = IdScalarList
                .GetKeyValueRecords()
                .Where(pair => 
                    pair.Key.BasisBladeIdToGrade() == grade && 
                    scalarSelection(pair.Value)
                )
                .ToDictionary(
                    pair => pair.Key.BasisBladeIdToIndex(),
                    pair => pair.Value
                );

            return GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary = IdScalarList
                .GetKeyValueRecords()
                .Where(pair => 
                    pair.Key.BasisBladeIdToGrade() == grade && 
                    indexScalarSelection(pair.Key.BasisBladeIdToIndex(), pair.Value)
                )
                .ToDictionary(
                    pair => pair.Key.BasisBladeIdToIndex(),
                    pair => pair.Value
                );

            return GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary = IdScalarList
                .GetKeyValueRecords()
                .Where(pair => 
                    pair.Key.BasisBladeIdToGrade() == grade && 
                    indexSelection(pair.Key.BasisBladeIdToIndex())
                )
                .ToDictionary(
                    pair => pair.Key.BasisBladeIdToIndex(),
                    pair => pair.Value
                );

            return GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }
        
        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var idScalarList =
                IdScalarList.FilterByKey(idSelection);

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var idScalarList =
                IdScalarList.FilterByKey(
                    id =>
                    {
                        id.BasisBladeIdToGradeIndex(out var grade, out var index);
                        return gradeIndexSelection(grade, index);
                    }
                );

            return new GaStorageMultivectorSparse<T>(idScalarList);

        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var idScalarList =
                IdScalarList.FilterByValue(scalarSelection);

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var idScalarList =
                IdScalarList.FilterByKeyValue(idScalarSelection);

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var idScalarList =
                IdScalarList.FilterByKeyValue(
                    (id, scalar) =>
                    {
                        id.BasisBladeIdToGradeIndex(out var grade, out var index);
                        return gradeIndexScalarSelection(grade, index, scalar);
                    }
                );

            return new GaStorageMultivectorSparse<T>(idScalarList);
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (id, scalar) in IdScalarList.GetKeyValueRecords())
            {
                id.BasisBladeIdToGradeIndex(out var grade, out var index);

                if (grade != 1)
                    continue;

                if (indexSelection(index))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                indexScalarDictionary1.CreateStorageVector(),
                indexScalarDictionary2.CreateStorageVector()
            );
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (id, scalar) in IdScalarList.GetKeyValueRecords())
            {
                id.BasisBladeIdToGradeIndex(out var grade, out var index);

                if (grade != 1)
                    continue;

                if (indexScalarSelection(index, scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                indexScalarDictionary1.CreateStorageVector(),
                indexScalarDictionary2.CreateStorageVector()
            );
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (id, scalar) in IdScalarList.GetKeyValueRecords())
            {
                id.BasisBladeIdToGradeIndex(out var grade, out var index);

                if (grade != 1)
                    continue;

                if (scalarSelection(scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                indexScalarDictionary1.CreateStorageVector(),
                indexScalarDictionary2.CreateStorageVector()
            );
        }
    }
}