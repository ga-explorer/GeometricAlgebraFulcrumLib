using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib;
using DataStructuresLib.Combinations;
using DataStructuresLib.Extensions;
using GeometricAlgebraLib.Multivectors.Basis;
using GeometricAlgebraLib.Multivectors.Terms;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage.Composers;
using GeometricAlgebraLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraLib.Storage.Trees;
using GaBasisUtils = GeometricAlgebraLib.Multivectors.Basis.GaBasisUtils;

namespace GeometricAlgebraLib.Storage
{
    public sealed class GaMultivectorTermsStorage<TScalar>
        : GaMultivectorStorageBase<TScalar>, IGaMultivectorTermsStorage<TScalar>
    {
        public static GaMultivectorTermsStorage<TScalar> CreateScalar(IGaScalarProcessor<TScalar> scalarProcessor, TScalar scalar)
        {
            var idScalarDictionary = new Dictionary<ulong, TScalar>() {{0, scalar}};

            return new GaMultivectorTermsStorage<TScalar>(
                scalarProcessor, 
                idScalarDictionary,
                0UL
            );
        }

        public static GaMultivectorTermsStorage<TScalar> CreateZero(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            var idScalarDictionary = new Dictionary<ulong, TScalar>();

            return new GaMultivectorTermsStorage<TScalar>(
                scalarProcessor, 
                idScalarDictionary,
                0UL
            );
        }
        
        public static GaMultivectorTermsStorage<TScalar> CreateTerm(IGaScalarProcessor<TScalar> scalarProcessor, ulong id, TScalar scalar)
        {
            var idScalarDictionary = 
                new Dictionary<ulong, TScalar>() {{id, scalar}};

            return new GaMultivectorTermsStorage<TScalar>(
                scalarProcessor,
                idScalarDictionary,
                id
            );
        }

        public static GaMultivectorTermsStorage<TScalar> CreateVector(IGaScalarProcessor<TScalar> scalarProcessor, IReadOnlyDictionary<ulong, TScalar> indexScalarDictionary)
        {
            var idScalarDictionary =
                indexScalarDictionary.ToDictionary(
                    pair => 1UL << (int)pair.Key,
                    pair => pair.Value
                );

            return new GaMultivectorTermsStorage<TScalar>(
                scalarProcessor,
                idScalarDictionary,
                idScalarDictionary.Keys.GetMaxBasisBladeId()
            );
        }

        public static GaMultivectorTermsStorage<TScalar> CreateBivector(IGaScalarProcessor<TScalar> scalarProcessor, IReadOnlyDictionary<ulong, TScalar> indexScalarDictionary)
        {
            var idScalarDictionary =
                indexScalarDictionary.ToDictionary(
                    pair => BinaryCombinationsUtilsUInt64.IndexToCombinadicPattern(pair.Key),
                    pair => pair.Value
                );

            return new GaMultivectorTermsStorage<TScalar>(
                scalarProcessor,
                idScalarDictionary,
                idScalarDictionary.Keys.GetMaxBasisBladeId()
            );
        }

        public static GaMultivectorTermsStorage<TScalar> CreateKVector(IGaScalarProcessor<TScalar> scalarProcessor, int grade, IReadOnlyDictionary<ulong, TScalar> indexScalarDictionary)
        {
            var idScalarDictionary =
                indexScalarDictionary.ToDictionary(
                    pair => GaBasisUtils.BasisBladeId(grade, pair.Key),
                    pair => pair.Value
                );

            return new GaMultivectorTermsStorage<TScalar>(
                scalarProcessor,
                idScalarDictionary,
                idScalarDictionary.Keys.GetMaxBasisBladeId()
            );
        }


        public static GaMultivectorTermsStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, Dictionary<ulong, TScalar> idScalarDictionary)
        {
            return new GaMultivectorTermsStorage<TScalar>(
                scalarProcessor,
                idScalarDictionary,
                idScalarDictionary.Keys.GetMaxBasisBladeId()
            );
        }


        public Dictionary<ulong, TScalar> IdScalarDictionary { get; }


        public override int VSpaceDimension 
            => MaxBasisBladeId.LastOneBitPosition() + 1;

        public override int GradesCount =>
            IdScalarDictionary
                .Keys
                .Select(id => id.BasisBladeGrade())
                .Distinct()
                .Count();

        public override int TermsCount 
            => IdScalarDictionary.Count;

        public override TScalar this[ulong id]
        {
            get => IdScalarDictionary.TryGetValue(id, out var scalar)
                ? scalar 
                : ScalarProcessor.ZeroScalar;

            set
            {
                if (ScalarProcessor.IsZero(value))
                {
                    IdScalarDictionary.Remove(id);

                    return;
                }

                if (IdScalarDictionary.ContainsKey(id))
                    IdScalarDictionary[id] = value;
                else
                    IdScalarDictionary.Add(id, value);
            }
        }

        public override TScalar this[int grade, ulong index]
        {
            get 
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                return IdScalarDictionary.TryGetValue(id, out var scalar)
                    ? scalar
                    : ScalarProcessor.ZeroScalar;
            }
            set
            {
                var id = GaBasisUtils.BasisBladeId(grade, index);

                if (ScalarProcessor.IsZero(value))
                {
                    IdScalarDictionary.Remove(id);

                    return;
                }

                if (IdScalarDictionary.ContainsKey(id))
                    IdScalarDictionary[id] = value;
                else
                    IdScalarDictionary.Add(id, value);
            }
        }

        public override bool IsUniform => true;

        public override bool IsGraded => false;


        private GaMultivectorTermsStorage([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, [NotNull] Dictionary<ulong, TScalar> idScalarDictionary, ulong maxBasisBladeId)
            : base(scalarProcessor, maxBasisBladeId)
        {
            IdScalarDictionary = idScalarDictionary;
        }


        public override bool ContainsKey(ulong key)
        {
            return IdScalarDictionary.ContainsKey(key);
        }

        public override bool TryGetValue(ulong key, out TScalar value)
        {
            return IdScalarDictionary.TryGetValue(key, out value);
        }

        public override bool ContainsTermsOfGrade(int grade)
        {
            return IdScalarDictionary.Keys.Any(id => grade == id.BasisBladeGrade());
        }


        public override bool IsEmpty()
        {
            return IdScalarDictionary.Count == 0;
        }

        public override bool IsZero()
        {
            return IdScalarDictionary.Count == 0 ||
                   IdScalarDictionary.Values.All(scalar => ScalarProcessor.IsZero(scalar));
        }
        
        public override bool IsNearZero()
        {
            return IdScalarDictionary.Count == 0 ||
                   IdScalarDictionary.Values.All(scalar => ScalarProcessor.IsNearZero(scalar));
        }

        public override bool IsScalar()
        {
            return !IdScalarDictionary.Any(
                pair => pair.Key > 0 && !ScalarProcessor.IsZero(pair.Value)
            );
        }

        public override bool IsVector()
        {
            throw new NotImplementedException();
        }

        public override bool IsBivector()
        {
            throw new NotImplementedException();
        }

        public override bool IsKVector()
        {
            throw new NotImplementedException();
        }

        public override bool IsKVector(int grade)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<int> GetGrades()
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

        public override bool ContainsTerm(int grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return IdScalarDictionary.ContainsKey(id);
        }


        public override TScalar GetTermScalar(ulong id)
        {
            return IdScalarDictionary.TryGetValue(id, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        public override TScalar GetTermScalar(int grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return IdScalarDictionary.TryGetValue(id, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }


        public override bool TryGetTermScalar(ulong id, out TScalar value)
        {
            if (IdScalarDictionary.TryGetValue(id, out value))
                return true;

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public override bool TryGetTermScalar(int grade, ulong index, out TScalar value)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            if (IdScalarDictionary.TryGetValue(id, out value))
                return true;

            value = ScalarProcessor.ZeroScalar;
            return false;
        }


        public GaMultivectorTermsStorage<TScalar> GetNegativeScalarsCopy()
        {
            var idScalarDictionary = 
                IdScalarDictionary.CopyToDictionary(ScalarProcessor.Negative);

            return new GaMultivectorTermsStorage<TScalar>(
                ScalarProcessor,
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaMultivectorTermsStorage<TScalar> GetLeftScaledScalarsCopy(TScalar scalingFactor)
        {
            var idScalarDictionary = 
                IdScalarDictionary.CopyToDictionary(
                    scalar => ScalarProcessor.Times(scalingFactor, scalar)
                );

            return new GaMultivectorTermsStorage<TScalar>(
                ScalarProcessor,
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaMultivectorTermsStorage<TScalar> GetRightScaledScalarsCopy(TScalar scalingFactor)
        {
            var idScalarDictionary = IdScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => ScalarProcessor.Times(pair.Value, scalingFactor)
            );

            return new GaMultivectorTermsStorage<TScalar>(
                ScalarProcessor,
                idScalarDictionary,
                MaxBasisBladeId
            );
        }


        public override IEnumerable<IGaKVectorStorage<TScalar>> GetKVectorStorages()
        {
            return IdScalarDictionary.GroupBy(
                pair => pair.Key.BasisBladeGrade()
            ).Select(g => 
                GaKVectorStorage<TScalar>.Create(
                    ScalarProcessor, 
                    g.Key,
                    g.ToDictionary(
                        pair => pair.Key.BasisBladeIndex(), 
                        pair => pair.Value
                    )
                )
            );
        }

        public override IReadOnlyDictionary<int, IGaKVectorStorage<TScalar>> GetKVectorStoragesDictionary()
        {
            return IdScalarDictionary.GroupBy(
                pair => pair.Key.BasisBladeGrade()
            ).ToDictionary(
                g => g.Key,
                g => (IGaKVectorStorage<TScalar>)GaKVectorStorage<TScalar>.Create(
                    ScalarProcessor, 
                    g.Key,
                    g.ToDictionary(
                        pair => pair.Key.BasisBladeIndex(), 
                        pair => pair.Value
                    )
                )
            );
        }

        public override bool TryGetKVectorStorage(int grade, out IGaKVectorStorage<TScalar> storage)
        {
            if (grade == 0)
            {
                if (IdScalarDictionary.TryGetValue(0, out var scalar))
                {
                    storage = GaScalarTermStorage<TScalar>.Create(ScalarProcessor, scalar);
                    return true;
                }

                storage = null;
                return false;
            }

            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => pair.Key.BasisBladeGrade() == grade)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            switch (indexScalarDictionary.Count)
            {
                case 0:
                    storage = null;
                    return false;

                case 1:
                    var (index, scalar) = indexScalarDictionary.First();

                    storage = GaKVectorTermStorage<TScalar>.Create(ScalarProcessor, grade, index, scalar);
                    return true;

                default:
                    storage = grade switch
                    {
                        1 => GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary),
                        2 => GaBivectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary),
                        _ => GaKVectorStorage<TScalar>.Create(ScalarProcessor, grade, indexScalarDictionary)
                    };

                    return true;
            }
        }

        public override bool TryGetKVectorStorageDictionary(int grade, out IReadOnlyDictionary<ulong, TScalar> indexScalarDictionary)
        {
            if (grade == 0)
            {
                if (IdScalarDictionary.TryGetValue(0, out var scalar))
                {
                    indexScalarDictionary = new Dictionary<ulong, TScalar>(){{0UL, scalar}};
                    return true;
                }

                indexScalarDictionary = null;
                return false;
            }

            indexScalarDictionary = IdScalarDictionary
                .Where(pair => pair.Key.BasisBladeGrade() == grade)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            if (indexScalarDictionary.Count > 0)
                return true;

            indexScalarDictionary = null;
            return false;
        }

        public override IReadOnlyDictionary<ulong, TScalar> GetIdScalarDictionary()
        {
            return IdScalarDictionary;
        }

        public override IReadOnlyDictionary<int, Dictionary<ulong, TScalar>> GetGradeIndexScalarDictionary()
        {
            return IdScalarDictionary.GroupBy(
                pair => pair.Key.BasisBladeGrade()
            ).ToDictionary(
                g => g.Key,
                g => g.ToDictionary(
                    pair => pair.Key.BasisBladeIndex(), 
                    pair => pair.Value
                )
            );
        }


        public override GaTerm<TScalar> GetTerm(ulong id)
        {
            return GaTerm<TScalar>.CreateUniform(id, this[id]);
        }

        public override GaTerm<TScalar> GetTerm(int grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return GaTerm<TScalar>.CreateUniform(id, this[id]);
        }

        public override bool TryGetTerm(ulong id, out GaTerm<TScalar> term)
        {
            if (TryGetValue(id, out var value))
            {
                term = GaTerm<TScalar>.CreateUniform(id, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(int grade, ulong index, out GaTerm<TScalar> term)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            if (TryGetValue(id, out var value))
            {
                term = GaTerm<TScalar>.CreateUniform(id, value);
                return true;
            }

            term = null;
            return false;
        }


        public override IEnumerable<ulong> GetIds()
        {
            return IdScalarDictionary.Keys;
        }

        public override IEnumerable<Tuple<int, ulong>> GetGradeIndexTuples()
        {
            return IdScalarDictionary
                .Keys
                .Select(id => id.BasisBladeGradeIndex());
        }

        public override IEnumerable<IGaBasisBlade> GetBasisBlades()
        {
            return IdScalarDictionary.Keys.Select(id => 
                (IGaBasisBlade)new GaBasisUniform(id)
            );
        }

        public override IEnumerable<TScalar> GetScalars()
        {
            return IdScalarDictionary.Values;
        }

        public override IEnumerable<GaTerm<TScalar>> GetTerms()
        {
            return IdScalarDictionary.Select(pair => 
                GaTerm<TScalar>.CreateUniform(pair.Key, pair.Value)
            );
        }

        public override IEnumerable<GaTerm<TScalar>> GetNotZeroTerms()
        {
            return IdScalarDictionary
                .Where(pair => !ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetNotNearZeroTerms()
        {
            return IdScalarDictionary
                .Where(pair => !ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetZeroTerms()
        {
            return IdScalarDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetNearZeroTerms()
        {
            return IdScalarDictionary
                .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<KeyValuePair<ulong, TScalar>> GetIdScalarPairs()
        {
            return IdScalarDictionary;
        }

        public override IEnumerable<Tuple<ulong, TScalar>> GetIdScalarTuples()
        {
            return IdScalarDictionary.Select(pair => 
                new Tuple<ulong, TScalar>(pair.Key, pair.Value)
            );
        }

        public override IEnumerable<Tuple<int, ulong, TScalar>> GetGradeIndexScalarTuples()
        {
            return IdScalarDictionary
                .Select(pair =>
                {
                    var (grade, index) = pair.Key.BasisBladeGradeIndex();
                    return new Tuple<int, ulong, TScalar>(grade, index, pair.Value);
                });
        }

        
        public override IGaGbtMultivectorStorageStack1<TScalar> CreateGbtStack(int treeDepth, int capacity)
        {
            return GaGbtMultivectorStorageUniformStack1<TScalar>.Create(
                capacity, 
                treeDepth, 
                this
            );
        }

        /// <summary>
        /// Construct a binary tree representation of this storage
        /// </summary>
        /// <returns></returns>
        public override GaBinaryTree<TScalar> GetBinaryTree(int treeDepth)
        {
            Debug.Assert(treeDepth > 0);
            //var treeDepth = GetIds().Max().LastOneBitPosition() + 1;

            var dict = GetIdScalarPairs()
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );

            return new GaBinaryTree<TScalar>(treeDepth, dict);
        }


        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<ulong, TScalar, TScalar2> idScalarMapping)
        {
            return new GaMultivectorTermsStorage<TScalar2>(
                scalarProcessor,
                IdScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => idScalarMapping(pair.Key, pair.Value)
                ),
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<int, ulong, TScalar, TScalar2> gradeIndexScalarMapping)
        {
            return new GaMultivectorTermsStorage<TScalar2>(
                scalarProcessor,
                IdScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair =>
                    {
                        pair.Key.BasisBladeGradeIndex(out var grade, out var index);
                        return gradeIndexScalarMapping(grade, index, pair.Value);
                    }
                ),
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<TScalar, TScalar2> scalarMapping)
        {
            return new GaMultivectorTermsStorage<TScalar2>(
                scalarProcessor,
                IdScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => scalarMapping(pair.Value)
                ),
                MaxBasisBladeId
            );
        }


        public override IGaMultivectorStorage<TScalar> CopyToMultivectorStorage()
        {
            var idScalarDictionary =
                GetIdScalarPairs().CopyToDictionary();

            return new GaMultivectorTermsStorage<TScalar>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override GaMultivectorGradedStorage<TScalar> GetMultivectorGradedStorageCopy()
        {
            var composer = new GaMultivectorGradedStorageComposer<TScalar>(ScalarProcessor);

            composer.AddTerms(GetIdScalarPairs());

            return composer.CreateMultivectorGradedStorage();
        }


        public override IGaMultivectorStorage<TScalar> GetCompactStorage()
        {
            switch (IdScalarDictionary.Count)
            {
                case 0:
                    return GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);

                case 1:
                    var (id, scalar) = IdScalarDictionary.First();

                    return id == 0
                        ? GaScalarTermStorage<TScalar>.Create(ScalarProcessor, scalar)
                        : GaKVectorTermStorage<TScalar>.Create(ScalarProcessor, id, scalar);

                default:
                    return this;
            }
        }

        public override IGaMultivectorGradedStorage<TScalar> GetCompactGradedStorage()
        {
            var composer = GaMultivectorStorageComposerBase<TScalar>.CreateGradedComposer(ScalarProcessor);

            composer.SetTerms(IdScalarDictionary);

            return composer.GetCompactGradedStorage();
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy()
        {
            var idScalarDictionary = IdScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return new GaMultivectorTermsStorage<TScalar>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            var idScalarDictionary = IdScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => scalarMapping(pair.Value)
            );

            return new GaMultivectorTermsStorage<TScalar>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative()
        {
            var idScalarDictionary = IdScalarDictionary.CopyToDictionary(
                ScalarProcessor.Negative
            );

            return new GaMultivectorTermsStorage<TScalar>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative(Predicate<int> gradeToNegativePredicate)
        {
            var idScalarDictionary = IdScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => 
                    gradeToNegativePredicate(pair.Key.BasisBladeGrade())
                        ? ScalarProcessor.Negative(pair.Value) 
                        : pair.Value
            );

            return new GaMultivectorTermsStorage<TScalar>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetReverse()
        {
            var idScalarDictionary = IdScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => 
                    pair.Key.BasisBladeIdHasNegativeReverse() 
                        ? ScalarProcessor.Negative(pair.Value) 
                        : pair.Value
            );

            return new GaMultivectorTermsStorage<TScalar>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetGradeInvolution()
        {
            var idScalarDictionary = IdScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => 
                    pair.Key.BasisBladeIdHasNegativeGradeInvolution() 
                        ? ScalarProcessor.Negative(pair.Value) 
                        : pair.Value
            );

            return new GaMultivectorTermsStorage<TScalar>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetCliffordConjugate()
        {
            var idScalarDictionary = IdScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => 
                    pair.Key.BasisBladeIdHasNegativeCliffordConjugate() 
                        ? ScalarProcessor.Negative(pair.Value) 
                        : pair.Value
            );

            return new GaMultivectorTermsStorage<TScalar>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorTermsStorage<TScalar> GetTermsStorage()
        {
            return this;
        }

        public override IGaMultivectorGradedStorage<TScalar> GetGradedStorage()
        {
            return GetMultivectorGradedStorageCopy();
        }

        public override IGaScalarStorage<TScalar> GetScalarPart()
        {
            return IdScalarDictionary.TryGetValue(0, out var scalar)
                ? GaScalarTermStorage<TScalar>.Create(ScalarProcessor, scalar)
                : GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaScalarStorage<TScalar> GetScalarPart(Func<TScalar, TScalar> scalarMapping)
        {
            var scalar = scalarMapping(GetTermScalar(0));

            return GaScalarTermStorage<TScalar>.Create(ScalarProcessor, scalar);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart()
        {
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => pair.Key.BasisBladeGrade() == 1)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return indexScalarDictionary.Count == 0 
                ? GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor) 
                : GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => pair.Key.BasisBladeGrade() == 1)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => scalarMapping(pair.Value)
                );

            return indexScalarDictionary.Count == 0 
                ? GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor) 
                : GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => pair.Key.BasisBladeGrade() == 1)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => indexScalarMapping(pair.Key, pair.Value)
                );

            return indexScalarDictionary.Count == 0 
                ? GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor) 
                : GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, bool> scalarSelection)
        {
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair =>
                {
                    var (id, scalar) = pair;
                    var grade = id.BasisBladeGrade();
                    return grade == 1 && scalarSelection(scalar);
                })
                .CopyToDictionary();

            return indexScalarDictionary.Count == 0 
                ? GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor) 
                : GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair =>
                {
                    var (id, scalar) = pair;
                    id.BasisBladeGradeIndex(out var grade, out var index);
                    return grade == 1 && indexScalarSelection(index, scalar);
                })
                .CopyToDictionary();

            return indexScalarDictionary.Count == 0 
                ? GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor) 
                : GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair =>
                {
                    pair.Key.BasisBladeGradeIndex(out var grade, out var index);
                    return grade == 1 && indexSelection(index);
                })
                .CopyToDictionary();

            return indexScalarDictionary.Count == 0 
                ? GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor) 
                : GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart()
        {
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => pair.Key.BasisBladeGrade() == 2)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return indexScalarDictionary.Count == 0 
                ? GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor) 
                : GaBivectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => pair.Key.BasisBladeGrade() == 2)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => scalarMapping(pair.Value)
                );

            return indexScalarDictionary.Count == 0 
                ? GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor) 
                : GaBivectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => pair.Key.BasisBladeGrade() == 2)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => indexScalarMapping(pair.Key, pair.Value)
                );

            return indexScalarDictionary.Count == 0 
                ? GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor) 
                : GaBivectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, bool> scalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade)
        {
            if (TryGetKVectorStorage(grade, out var storage))
                return storage;

            return grade switch
            {
                0 => GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor),
                1 => GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                2 => GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                _ => GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade)
            };
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, TScalar> scalarMapping)
        {
            if (grade == 0)
                return GaScalarTermStorage<TScalar>.Create(ScalarProcessor, scalarMapping(GetTermScalar(0)));

            if (grade == 1)
                return GetVectorPart(scalarMapping);

            if (grade == 2)
                return GetBivectorPart(scalarMapping);

            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => pair.Key.BasisBladeGrade() == grade)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => scalarMapping(pair.Value)
                );
            
            return indexScalarDictionary.Count == 0
                ? GaKVectorStorage<TScalar>.Create(ScalarProcessor, grade, indexScalarDictionary)
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            if (grade == 0)
                return GaScalarTermStorage<TScalar>.Create(ScalarProcessor, indexScalarMapping(0, GetTermScalar(0)));

            if (grade == 1)
                return GetVectorPart(indexScalarMapping);

            if (grade == 2)
                return GetBivectorPart(indexScalarMapping);

            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => pair.Key.BasisBladeGrade() == grade)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => indexScalarMapping(pair.Key, pair.Value)
                );
            
            return indexScalarDictionary.Count == 0
                ? GaKVectorStorage<TScalar>.Create(ScalarProcessor, grade, indexScalarDictionary)
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, bool> scalarSelection)
        {
            var composer = new GaKVectorStorageComposer<TScalar>(ScalarProcessor, grade);

            composer.SetTerms(
                GetIdScalarPairs().Where(pair =>
                    pair.Key.BasisBladeGrade() == grade && 
                    scalarSelection(pair.Value)
                )
                .Select(pair => new KeyValuePair<ulong, TScalar>(
                    pair.Key.BasisBladeIndex(),
                    pair.Value
                ))
            );

            composer.RemoveZeroTerms();

            return composer.GetKVectorStorage();
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, bool> indexScalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, bool> indexSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(
                GetIdScalarPairs()
                    .Select(pair => new KeyValuePair<ulong, TScalar>(
                        pair.Key, 
                        scalarMapping(pair.Value)
                    ))
            );

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(
                GetIdScalarPairs().Where(pair => idSelection(pair.Key))
            );

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, bool> gradeIndexSelection)
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(
                GetIdScalarPairs().Where(pair =>
                {
                    pair.Key.BasisBladeGradeIndex(out var grade, out var index);
                    return gradeIndexSelection(grade, index);
                })
            );

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, bool> scalarSelection)
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(
                GetIdScalarPairs().Where(pair => scalarSelection(pair.Value))
            );

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, TScalar, bool> idScalarSelection)
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(
                GetIdScalarPairs().Where(pair => idScalarSelection(pair.Key, pair.Value))
            );

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, TScalar, bool> gradeIndexScalarSelection)
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerms(
                GetIdScalarPairs().Where(pair =>
                {
                    pair.Key.BasisBladeGradeIndex(out var grade, out var index);
                    return gradeIndexScalarSelection(grade, index, pair.Value);
                })
            );

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, TScalar>();
            var indexScalarDictionary2 = new Dictionary<ulong, TScalar>();

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

            return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary1),
                GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary2)
            );
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, TScalar>();
            var indexScalarDictionary2 = new Dictionary<ulong, TScalar>();

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

            return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary1),
                GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary2)
            );
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<TScalar, bool> scalarSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, TScalar>();
            var indexScalarDictionary2 = new Dictionary<ulong, TScalar>();

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

            return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary1),
                GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary2)
            );
        }
        
    }
}