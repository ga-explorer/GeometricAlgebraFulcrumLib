using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;
using GeometricAlgebraFulcrumLib.Structures.Graded;


namespace GeometricAlgebraFulcrumLib.Storage
{
    public sealed record GaStorageMultivectorSparse<T>
        : GaStorageMultivectorBase<T>, IGaStorageMultivectorSparse<T>
    {
        public static GaStorageMultivectorSparse<T> ZeroMultivector { get; }
            = new GaStorageMultivectorSparse<T>(
                GaEvenDictionaryEmpty<T>.DefaultDictionary,
                0UL
            );


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(ulong id, T scalar)
        {
            var evenDictionary = 
                scalar.CreateEvenDictionarySingleKey(id);

            return new GaStorageMultivectorSparse<T>(evenDictionary, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(uint grade, ulong index, T scalar)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            var evenDictionary = 
                scalar.CreateEvenDictionarySingleKey(id);

            return new GaStorageMultivectorSparse<T>(evenDictionary, id);
        }

        public static GaStorageMultivectorSparse<T> Create(uint grade, params T[] indexScalarList)
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            for (var index = 0; index < indexScalarList.Length; index++)
                idScalarDictionary.Add(
                    GaBasisUtils.BasisBladeId(grade, (ulong) index),
                    indexScalarList[index]
                );

            var evenDictionary = 
                idScalarDictionary.CreateEvenDictionary();

            return new GaStorageMultivectorSparse<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId()
            );
        }

        public static GaStorageMultivectorSparse<T> Create(uint grade, IReadOnlyList<T> indexScalarList)
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            for (var index = 0; index < indexScalarList.Count; index++)
                idScalarDictionary.Add(
                    GaBasisUtils.BasisBladeId(grade, (ulong) index),
                    indexScalarList[index]
                );

            var evenDictionary = 
                idScalarDictionary.CreateEvenDictionary();

            return new GaStorageMultivectorSparse<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId()
            );
        }

        public static GaStorageMultivectorSparse<T> Create(uint grade, IEnumerable<T> indexScalarList)
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            var index = 0UL;
            foreach (var scalar in indexScalarList)
            {
                idScalarDictionary.Add(
                    GaBasisUtils.BasisBladeId(grade, index),
                    scalar
                );

                index++;
            }

            var evenDictionary = 
                idScalarDictionary.CreateEvenDictionary();

            return new GaStorageMultivectorSparse<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId()
            );
        }

        public static GaStorageMultivectorSparse<T> Create(uint grade, IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            var idScalarDictionary = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in indexScalarDictionary)
            {
                idScalarDictionary.Add(
                    GaBasisUtils.BasisBladeId(grade, index),
                    scalar
                );
            }

            var evenDictionary = 
                idScalarDictionary.CreateEvenDictionary();

            return new GaStorageMultivectorSparse<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(T scalar)
        {
            var evenDictionary = 
                scalar.CreateEvenDictionarySingleZeroKey();

            return new GaStorageMultivectorSparse<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(params T[] idScalarList)
        {
            var evenDictionary = 
                idScalarList.CreateEvenDictionaryList();

            return new GaStorageMultivectorSparse<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(IReadOnlyList<T> idScalarList)
        {
            var evenDictionary = 
                idScalarList.CreateEvenDictionaryList();

            return new GaStorageMultivectorSparse<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(IEnumerable<T> idScalarList)
        {
            var evenDictionary = 
                idScalarList.CreateEvenDictionaryList();

            return new GaStorageMultivectorSparse<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(Dictionary<ulong, T> idScalarDictionary)
        {
            var evenDictionary = 
                idScalarDictionary.CreateEvenDictionary();

            return new GaStorageMultivectorSparse<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Create(IGaEvenDictionary<T> indexScalarDictionary)
        {
            return new GaStorageMultivectorSparse<T>(
                indexScalarDictionary, 
                indexScalarDictionary.GetMaxBasisBladeId(1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GaStorageMultivectorSparse<T> Create(IGaEvenDictionary<T> indexScalarDictionary, ulong maxBasisBladeId)
        {
            return new GaStorageMultivectorSparse<T>(
                indexScalarDictionary, 
                maxBasisBladeId
            );
        }


        public IGaEvenDictionary<T> IdScalarDictionary { get; }


        public override uint VSpaceDimension 
            => (uint) (MaxBasisBladeId.LastOneBitPosition() + 1);

        public override int GradesCount =>
            IdScalarDictionary
                .Keys
                .Select(id => id.BasisBladeGrade())
                .Distinct()
                .Count();

        public override int TermsCount 
            => IdScalarDictionary.Count;


        public override bool IsUniform => true;

        public override bool IsGraded => false;


        private GaStorageMultivectorSparse([NotNull] IGaEvenDictionary<T> idScalarDictionary, ulong maxBasisBladeId)
            : base(maxBasisBladeId)
        {
            IdScalarDictionary = idScalarDictionary;
        }


        public GaStorageMultivectorSparse<T> GetStorageCopy()
        {
            var idScalarDictionary = 
                IdScalarDictionary.GetCopy();

            return new GaStorageMultivectorSparse<T>(
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageMultivectorSparse<T2> MapScalars<T2>(Func<T, T2> scalarMapping)
        {
            var idScalarDictionary = 
                IdScalarDictionary.MapValues(scalarMapping);

            return new GaStorageMultivectorSparse<T2>(
                idScalarDictionary,
                MaxBasisBladeId
            );
        }
        
        public GaStorageMultivectorSparse<T2> MapScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var idScalarDictionary = 
                IdScalarDictionary.MapValues(idScalarMapping);

            return new GaStorageMultivectorSparse<T2>(
                idScalarDictionary,
                MaxBasisBladeId
            );
        }
        
        public GaStorageMultivectorSparse<T2> MapScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var idScalarDictionary = 
                IdScalarDictionary.MapValues((id, scalar) => 
                    indexScalarMapping(id.BasisBladeIndex(), scalar)
                );

            return new GaStorageMultivectorSparse<T2>(
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageMultivectorSparse<T2> MapScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var idScalarDictionary = 
                IdScalarDictionary.MapValues(
                    (id, scalar) =>
                    {
                        id.BasisBladeGradeIndex(out var grade, out var index);
                        return gradeIndexScalarMapping(grade, index, scalar);
                    }
                );

            return new GaStorageMultivectorSparse<T2>(
                idScalarDictionary,
                MaxBasisBladeId
            );
        }


        public GaStorageMultivectorSparse<T> GetPart(Func<T, bool> scalarFilter)
        {
            var indexScalarDictionary =
                IdScalarDictionary.FilterByValue(scalarFilter);

            return new GaStorageMultivectorSparse<T>(
                indexScalarDictionary,
                indexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public GaStorageMultivectorSparse<T> GetPart(Func<ulong, T, bool> idScalarFilter)
        {
            var indexScalarDictionary =
                IdScalarDictionary.FilterByKeyValue(idScalarFilter);

            return new GaStorageMultivectorSparse<T>(
                indexScalarDictionary,
                indexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public GaStorageMultivectorSparse<T> GetPart(Func<ulong, bool> idFilter)
        {
            var indexScalarDictionary =
                IdScalarDictionary.FilterByKey(idFilter);

            return new GaStorageMultivectorSparse<T>(
                indexScalarDictionary,
                indexScalarDictionary.GetMaxBasisBladeId()
            );
        }


        public override bool ContainsKey(ulong key)
        {
            return IdScalarDictionary.ContainsKey(key);
        }

        public override bool TryGetValue(ulong key, out T value)
        {
            return IdScalarDictionary.TryGetValue(key, out value);
        }

        public override bool ContainsVectorPart()
        {
            return IdScalarDictionary.Keys.Any(key => key.IsBasisVectorId());
        }

        public override bool ContainsBivectorPart()
        {
            return IdScalarDictionary.Keys.Any(key => key.IsBasisBivectorId());
        }

        public override bool ContainsKVectorPart(uint grade)
        {
            return IdScalarDictionary.Keys.Any(id => grade == id.BasisBladeGrade());
        }


        public override bool IsEmpty()
        {
            return IdScalarDictionary.Count == 0;
        }
        

        public override bool IsScalar()
        {
            return !IdScalarDictionary.Keys.Any(key => key > 0);
        }

        public override bool IsVector()
        {
            return IdScalarDictionary.Keys.All(key => key.IsBasicPattern());
        }

        public override bool IsBivector()
        {
            return IdScalarDictionary.Keys.All(key => key.BasisBladeGrade() == 2);
        }

        public override bool IsKVector()
        {
            return IdScalarDictionary
                .Keys
                .Select(key => key.BasisBladeGrade())
                .Distinct()
                .Count() < 2;
        }

        public override bool IsKVector(uint grade)
        {
            return IdScalarDictionary.Keys.All(key => key.BasisBladeGrade() == grade);
        }

        public override IEnumerable<uint> GetGrades()
        {
            return IdScalarDictionary
                .Keys
                .Select(id => id.BasisBladeGrade())
                .Distinct();
        }


        public override bool ContainsTerm(ulong id)
        {
            return IdScalarDictionary.ContainsKey(id);
        }

        public override bool ContainsTerm(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return IdScalarDictionary.ContainsKey(id);
        }

        public override bool ContainsScalarPart()
        {
            return IdScalarDictionary.Keys.Any(key => key == 0);
        }

        public override bool TryGetTermScalar(ulong id, out T value)
        {
            if (IdScalarDictionary.TryGetValue(id, out value))
                return true;

            value = default;
            return false;
        }

        public override bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            if (IdScalarDictionary.TryGetValue(id, out value))
                return true;

            value = default;
            return false;
        }


        public override bool TryGetKVectorPart(uint grade, out IGaStorageKVector<T> kVector)
        {
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => 
                    pair.Key.BasisBladeGrade() == grade
                )
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
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

        public override IEnumerable<IGaStorageKVector<T>> GetKVectorStorages()
        {
            return IdScalarDictionary.GroupBy(
                pair => pair.Key.BasisBladeGrade()
            ).Select(g => 
                GaStorageKVector<T>.Create(
                    g.Key,
                    g.ToDictionary(
                        pair => pair.Key.BasisBladeIndex(), 
                        pair => pair.Value
                    )
                )
            );
        }

        public override IReadOnlyDictionary<uint, IGaStorageKVector<T>> GetKVectorStoragesDictionary()
        {
            return IdScalarDictionary.GroupBy(
                pair => pair.Key.BasisBladeGrade()
            ).ToDictionary(
                g => g.Key,
                g => GaStorageKVector<T>.Create(
                    g.Key,
                    g.ToDictionary(
                        pair => pair.Key.BasisBladeIndex(), 
                        pair => pair.Value
                    )
                )
            );
        }

        public override bool TryGetVectorPartDictionary(out IGaEvenDictionary<T> indexScalarDictionary)
        {
            var dict =
                IdScalarDictionary
                    .Where(pair => pair.Key.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            if (dict.Count > 0)
            {
                indexScalarDictionary = dict.CreateEvenDictionary();
                return true;
            }

            indexScalarDictionary = GaEvenDictionaryEmpty<T>.DefaultDictionary;
            return false;
        }

        public override bool TryGetBivectorPartDictionary(out IGaEvenDictionary<T> indexScalarDictionary)
        {
            var dict =
                IdScalarDictionary
                    .Where(pair => pair.Key.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            if (dict.Count > 0)
            {
                indexScalarDictionary = dict.CreateEvenDictionary();
                return true;
            }

            indexScalarDictionary = GaEvenDictionaryEmpty<T>.DefaultDictionary;
            return false;
        }

        public override bool TryGetKVectorPartDictionary(uint grade, out IGaEvenDictionary<T> indexScalarDictionary)
        {
            if (grade == 0)
            {
                if (IdScalarDictionary.TryGetValue(0, out var scalar))
                {
                    indexScalarDictionary = scalar.CreateEvenDictionarySingleZeroKey();
                    return true;
                }

                indexScalarDictionary = GaEvenDictionaryEmpty<T>.DefaultDictionary;
                return false;
            }

            var dict =
                IdScalarDictionary
                    .Where(pair => pair.Key.BasisBladeGrade() == grade)
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            if (dict.Count > 0)
            {
                indexScalarDictionary = dict.CreateEvenDictionary();
                return true;
            }

            indexScalarDictionary = GaEvenDictionaryEmpty<T>.DefaultDictionary;
            return false;
        }

        public override IGaEvenDictionary<T> GetIdScalarDictionary()
        {
            return IdScalarDictionary;
        }

        public override IGaGradedDictionary<T> GetGradeIndexScalarDictionary()
        {
            return IdScalarDictionary.GroupBy(
                pair => pair.Key.BasisBladeGrade()
            ).ToDictionary(
                g => g.Key,
                g => g.ToDictionary(
                    pair => pair.Key.BasisBladeIndex(), 
                    pair => pair.Value
                )
            ).CreateGradedDictionary();
        }


        public override bool TryGetTerm(ulong id, out GaTerm<T> term)
        {
            if (TryGetValue(id, out var value))
            {
                term = GaTerm<T>.CreateUniform(id, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            if (TryGetValue(id, out var value))
            {
                term = GaTerm<T>.CreateUniform(id, value);
                return true;
            }

            term = null;
            return false;
        }

        public override IGaEvenDictionary<T> GetScalarPartDictionary()
        {
            return IdScalarDictionary.TryGetValue(0, out var scalar)
                ? scalar.CreateEvenDictionarySingleZeroKey()
                : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public override IGaEvenDictionary<T> GetVectorPartDictionary()
        {
            var indexScalarDictionary =
                IdScalarDictionary
                    .Where(pair => pair.Key.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(), 
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count > 0
                ? indexScalarDictionary.CreateEvenDictionary()
                : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public override IGaEvenDictionary<T> GetBivectorPartDictionary()
        {
            var indexScalarDictionary =
                IdScalarDictionary
                    .Where(pair => pair.Key.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(), 
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count > 0
                ? indexScalarDictionary.CreateEvenDictionary()
                : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public override IGaEvenDictionary<T> GetKVectorPartDictionary(uint grade)
        {
            var indexScalarDictionary =
                IdScalarDictionary
                    .Where(pair => pair.Key.BasisBladeGrade() == grade)
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(), 
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count > 0
                ? indexScalarDictionary.CreateEvenDictionary()
                : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public override bool TryGetScalarPart(out IGaStorageScalar<T> scalar)
        {
            if (IdScalarDictionary.TryGetValue(0, out var s))
            {
                scalar = s.CreateStorageScalar();
                return true;
            }

            scalar = null;
            return false;
        }

        public override bool TryGetVectorPart(out IGaStorageVector<T> vector)
        {
            var indexScalarDictionary =
                IdScalarDictionary
                    .Where(pair => pair.Key.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(), 
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
                IdScalarDictionary
                    .Where(pair => pair.Key.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(), 
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
            return IdScalarDictionary.Keys;
        }

        public override IEnumerable<Tuple<uint, ulong>> GetGradeIndexTuples()
        {
            return IdScalarDictionary
                .Keys
                .Select(id => id.BasisBladeGradeIndex());
        }

        public override IEnumerable<IGaBasisBlade> GetBasisBlades()
        {
            return IdScalarDictionary.Keys.Select(id => 
                (IGaBasisBlade) id.CreateUniformBasisBlade()
            );
        }

        public override IEnumerable<T> GetScalars()
        {
            return IdScalarDictionary.Values;
        }

        public override IEnumerable<GaTerm<T>> GetTerms()
        {
            return IdScalarDictionary.Select(pair => 
                GaTerm<T>.CreateUniform(pair.Key, pair.Value)
            );
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (id, scalar) in IdScalarDictionary)
            {
                if (idSelection(id))
                    yield return GaTerm<T>.CreateUniform(id, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (id, scalar) in IdScalarDictionary)
            {
                var (grade, index) = id.BasisBladeGradeIndex();

                if (gradeIndexSelection(grade, index))
                    yield return GaTerm<T>.CreateUniform(id, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (id, scalar) in IdScalarDictionary)
            {
                if (scalarSelection(scalar))
                    yield return GaTerm<T>.CreateUniform(id, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (id, scalar) in IdScalarDictionary)
            {
                if (idScalarSelection(id, scalar))
                    yield return GaTerm<T>.CreateUniform(id, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (id, scalar) in IdScalarDictionary)
            {
                var (grade, index) = id.BasisBladeGradeIndex();

                if (gradeIndexScalarSelection(grade, index, scalar))
                    yield return GaTerm<T>.CreateUniform(id, scalar);
            }
        }

        public override IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs()
        {
            return IdScalarDictionary;
        }

        public override IEnumerable<Tuple<ulong, T>> GetIdScalarTuples()
        {
            return IdScalarDictionary.Select(pair => 
                new Tuple<ulong, T>(pair.Key, pair.Value)
            );
        }

        public override IEnumerable<Tuple<uint, ulong, T>> GetGradeIndexScalarTuples()
        {
            return IdScalarDictionary
                .Select(pair =>
                {
                    var (grade, index) = pair.Key.BasisBladeGradeIndex();
                    return new Tuple<uint, ulong, T>(grade, index, pair.Value);
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
        public override GaEvenDictionaryTree<T> GetBinaryTree(int treeDepth)
        {
            Debug.Assert(treeDepth > 0);
            //var treeDepth = GetIds().Max().LastOneBitPosition() + 1;

            var dict = GetIdScalarPairs()
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );

            return new GaEvenDictionaryTree<T>(treeDepth, dict);
        }




        public override IGaStorageMultivector<T> CopyToMultivectorStorage()
        {
            var idScalarDictionary =
                IdScalarDictionary
                    .CopyToDictionary()
                    .CreateEvenDictionary();

            return new GaStorageMultivectorSparse<T>(
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaStorageMultivectorGraded<T> GetGradedMultivectorCopy()
        {
            return GaStorageMultivectorGraded<T>.Create(
                GetGradeIndexScalarDictionary(), 
                MaxBasisBladeId
            );
        }


        public override IGaStorageMultivector<T> GetCompactStorage()
        {
            return this;
        }

        public override IGaStorageMultivectorGraded<T> GetCompactGradedStorage()
        {
            return GaStorageMultivectorGraded<T>.Create(
                GetGradeIndexScalarDictionary(), 
                MaxBasisBladeId
            );
        }
        
        
        public override IGaStorageMultivectorSparse<T> ToSparseMultivector()
        {
            return this;
        }

        public override IGaStorageMultivectorGraded<T> ToGradedMultivector()
        {
            return GetGradedMultivectorCopy();
        }

        public override IGaStorageVector<T> GetVectorPart()
        {
            var indexScalarDictionary =             
                IdScalarDictionary
                    .Where(pair => pair.Key.IsBasisVectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =             
                IdScalarDictionary
                    .Where(pair => pair.Key.IsBasisVectorId() && scalarSelection(pair.Value))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary =             
                IdScalarDictionary
                    .Where(pair => pair.Key.IsBasisVectorId() && indexScalarSelection(pair.Key.BasisBladeIndex(), pair.Value))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary =             
                IdScalarDictionary
                    .Where(pair => pair.Key.IsBasisVectorId() && indexSelection(pair.Key.BasisBladeIndex()))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart()
        {
            var indexScalarDictionary =             
                IdScalarDictionary
                    .Where(pair => pair.Key.IsBasisBivectorId())
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }


        public override IGaStorageBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =             
                IdScalarDictionary
                    .Where(pair => pair.Key.IsBasisBivectorId() && scalarSelection(pair.Value))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary =             
                IdScalarDictionary
                    .Where(pair => pair.Key.IsBasisBivectorId() && indexScalarSelection(pair.Key.BasisBladeIndex(), pair.Value))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary =             
                IdScalarDictionary
                    .Where(pair => pair.Key.IsBasisBivectorId() && indexSelection(pair.Key.BasisBladeIndex()))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade)
        {
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => 
                    pair.Key.BasisBladeGrade() == grade
                )
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => 
                    pair.Key.BasisBladeGrade() == grade && 
                    scalarSelection(pair.Value)
                )
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => 
                    pair.Key.BasisBladeGrade() == grade && 
                    indexScalarSelection(pair.Key.BasisBladeIndex(), pair.Value)
                )
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => 
                    pair.Key.BasisBladeGrade() == grade && 
                    indexSelection(pair.Key.BasisBladeIndex())
                )
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }
        
        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var idScalarDictionary =
                IdScalarDictionary
                    .Where(pair => idSelection(pair.Key))
                    .CopyToDictionary()
                    .CreateEvenDictionary();

            return new GaStorageMultivectorSparse<T>(
                idScalarDictionary, 
                idScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var idScalarDictionary =
                IdScalarDictionary
                    .Where(pair =>
                    {
                        pair.Key.BasisBladeGradeIndex(out var grade, out var index);
                        return gradeIndexSelection(grade, index);
                    })
                    .CopyToDictionary()
                    .CreateEvenDictionary();

            return new GaStorageMultivectorSparse<T>(
                idScalarDictionary, 
                idScalarDictionary.GetMaxBasisBladeId()
            );

        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var idScalarDictionary =
                IdScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .CopyToDictionary()
                    .CreateEvenDictionary();

            return new GaStorageMultivectorSparse<T>(
                idScalarDictionary, 
                idScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var idScalarDictionary =
                IdScalarDictionary
                    .Where(pair => idScalarSelection(pair.Key, pair.Value))
                    .CopyToDictionary()
                    .CreateEvenDictionary();

            return new GaStorageMultivectorSparse<T>(
                idScalarDictionary, 
                idScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var idScalarDictionary =
                IdScalarDictionary
                    .Where(pair =>
                    {
                        pair.Key.BasisBladeGradeIndex(out var grade, out var index);
                        return gradeIndexScalarSelection(grade, index, pair.Value);
                    })
                    .CopyToDictionary()
                    .CreateEvenDictionary();

            return new GaStorageMultivectorSparse<T>(
                idScalarDictionary, 
                idScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (id, scalar) in IdScalarDictionary)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

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

            foreach (var (id, scalar) in IdScalarDictionary)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

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

            foreach (var (id, scalar) in IdScalarDictionary)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

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