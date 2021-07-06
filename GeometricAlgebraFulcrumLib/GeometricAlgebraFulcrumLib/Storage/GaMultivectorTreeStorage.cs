using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Trees;
using GaBasisUtils = GeometricAlgebraFulcrumLib.Algebra.Basis.GaBasisUtils;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public sealed class GaMultivectorTreeStorage<TScalar>
        : GaMultivectorStorageBase<TScalar>, IGaMultivectorTermsStorage<TScalar>
    {
        public static GaMultivectorTreeStorage<TScalar> CreateScalar(IGaScalarProcessor<TScalar> scalarProcessor, TScalar scalar)
        {
            var idScalarDictionary = new Dictionary<ulong, TScalar>() {{0, scalar}};

            return new GaMultivectorTreeStorage<TScalar>(
                scalarProcessor, 
                idScalarDictionary,
                0UL
            );
        }
        
        public static GaMultivectorTreeStorage<TScalar> CreateTerm(IGaScalarProcessor<TScalar> scalarProcessor, ulong id, TScalar scalar)
        {
            var idScalarDictionary = 
                new Dictionary<ulong, TScalar>() {{id, scalar}};

            return new GaMultivectorTreeStorage<TScalar>(
                scalarProcessor,
                idScalarDictionary,
                id
            );
        }
        
        public static GaMultivectorTreeStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, Dictionary<ulong, TScalar> idScalarDictionary)
        {
            return new GaMultivectorTreeStorage<TScalar>(
                scalarProcessor,
                idScalarDictionary,
                idScalarDictionary.Keys.GetMaxBasisBladeId()
            );
        }

        
        public GaBinaryTree<TScalar> IdScalarTree { get; }


        public override bool IsUniform 
            => true;
        
        public override bool IsGraded 
            => false;

        public override int VSpaceDimension 
            => MaxBasisBladeId.LastOneBitPosition() + 1;

        public override int GradesCount
            => IdScalarTree
                .GetLeafNodeIDs()
                .Select(id => id.BasisBladeGrade())
                .Distinct()
                .Count();

        public override int TermsCount 
            => IdScalarTree.Count;

        public override TScalar this[ulong id]
        {
            get => IdScalarTree[id];
            set => IdScalarTree[id] = value;
        }

        public override TScalar this[int grade, ulong index]
        {
            get => IdScalarTree[GaBasisUtils.BasisBladeId(grade, index)];
            set => IdScalarTree[GaBasisUtils.BasisBladeId(grade, index)] = value;
        }


        private GaMultivectorTreeStorage(IGaScalarProcessor<TScalar> scalarProcessor, IReadOnlyList<ulong> leafNodeIDsList, ulong maxBasisBladeId)
            : base(scalarProcessor, maxBasisBladeId)
        {
            var treeDepth = leafNodeIDsList.Max().LastOneBitPosition() + 1;

            IdScalarTree = new GaBinaryTree<TScalar>(treeDepth, leafNodeIDsList);
        }

        private GaMultivectorTreeStorage(IGaScalarProcessor<TScalar> scalarProcessor, IReadOnlyDictionary<ulong, TScalar> leafNodes, ulong maxBasisBladeId)
            : base(scalarProcessor, maxBasisBladeId)
        {
            var treeDepth = leafNodes.Keys.Max().LastOneBitPosition() + 1;

            IdScalarTree = new GaBinaryTree<TScalar>(treeDepth, leafNodes);
        }

        private GaMultivectorTreeStorage(IGaScalarProcessor<TScalar> scalarProcessor, IReadOnlyList<ulong> leafNodeIDsList, IReadOnlyCollection<TScalar> leafNodeValuesList, ulong maxBasisBladeId)
            : base(scalarProcessor, maxBasisBladeId)
        {
            var treeDepth = leafNodeIDsList.Max().LastOneBitPosition() + 1;

            IdScalarTree = new GaBinaryTree<TScalar>(treeDepth, leafNodeIDsList, leafNodeValuesList);
        }

        private GaMultivectorTreeStorage(IGaScalarProcessor<TScalar> scalarProcessor, GaBinaryTree<TScalar> idScalarTree, ulong maxBasisBladeId)
            : base(scalarProcessor, maxBasisBladeId)
        {
            IdScalarTree = idScalarTree;
        }


        public override bool ContainsKey(ulong key)
        {
            return IdScalarTree.ContainsKey(key);
        }

        public override bool TryGetValue(ulong key, out TScalar value)
        {
            return IdScalarTree.TryGetValue(key, out value);
        }

        public override bool ContainsTermsOfGrade(int grade)
        {
            return IdScalarTree
                .GetLeafNodeIDs()
                .Any(id => id.BasisBladeGrade() == grade);
        }

        public override bool IsEmpty()
        {
            return IdScalarTree.Count == 0;
        }

        public override bool IsZero()
        {
            return IdScalarTree.Count == 0 || IdScalarTree.Values.All(ScalarProcessor.IsZero);
        }

        public override bool IsNearZero()
        {
            return IdScalarTree.Count == 0 || IdScalarTree.Values.All(ScalarProcessor.IsNearZero);
        }

        public override bool IsScalar()
        {
            return !IdScalarTree.Any(
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
            return IdScalarTree
                .GetLeafNodeIDs()
                .Select(id => id.BasisBladeGrade())
                .Distinct();
        }


        public override bool ContainsTerm(ulong id)
        {
            return IdScalarTree.ContainsKey(id);
        }

        public override bool ContainsTerm(int grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return IdScalarTree.ContainsKey(id);
        }


        public override TScalar GetTermScalar(ulong id)
        {
            return IdScalarTree.TryGetValue(id, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        public override TScalar GetTermScalar(int grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return IdScalarTree.TryGetValue(id, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        public override bool TryGetTermScalar(ulong id, out TScalar value)
        {
            if (IdScalarTree.TryGetValue(id, out value))
                return true;

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public override bool TryGetTermScalar(int grade, ulong index, out TScalar value)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            if (IdScalarTree.TryGetValue(id, out value))
                return true;

            value = ScalarProcessor.ZeroScalar;
            return false;
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


        public override IEnumerable<IGaKVectorStorage<TScalar>> GetKVectorStorages()
        {
            return IdScalarTree.GroupBy(
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
            return IdScalarTree.GroupBy(
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
                if (IdScalarTree.TryGetValue(0, out var scalar))
                {
                    storage = GaScalarTermStorage<TScalar>.Create(ScalarProcessor, scalar);
                    return true;
                }

                storage = null;
                return false;
            }

            var indexScalarDictionary = IdScalarTree
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
                if (IdScalarTree.TryGetValue(0, out var scalar))
                {
                    indexScalarDictionary = new Dictionary<ulong, TScalar>(){{0UL, scalar}};
                    return true;
                }

                indexScalarDictionary = null;
                return false;
            }

            indexScalarDictionary = IdScalarTree
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
            return IdScalarTree;
        }

        public override IReadOnlyDictionary<int, Dictionary<ulong, TScalar>> GetGradeIndexScalarDictionary()
        {
            return IdScalarTree.GroupBy(
                pair => pair.Key.BasisBladeGrade()
            ).ToDictionary(
                g => g.Key,
                g => g.ToDictionary(
                    pair => pair.Key.BasisBladeIndex(), 
                    pair => pair.Value
                )
            );
        }


        public override IEnumerable<ulong> GetIds()
        {
            return IdScalarTree.Keys;
        }

        public override IEnumerable<Tuple<int, ulong>> GetGradeIndexTuples()
        {
            return IdScalarTree.Keys.Select(id => id.BasisBladeGradeIndex());
        }

        public override IEnumerable<IGaBasisBlade> GetBasisBlades()
        {
            return IdScalarTree.GetLeafNodeIDs().Select(id => 
                (IGaBasisBlade)new GaBasisUniform(id)
            );
        }

        public override IEnumerable<TScalar> GetScalars()
        {
            return IdScalarTree.Values;
        }

        public override IEnumerable<Tuple<ulong, TScalar>> GetIdScalarTuples()
        {
            return IdScalarTree.Select(pair => 
                new Tuple<ulong, TScalar>(pair.Key, pair.Value)
            );
        }

        public override IEnumerable<Tuple<int, ulong, TScalar>> GetGradeIndexScalarTuples()
        {
            return IdScalarTree.Select(pair =>
            {
                pair.Key.BasisBladeGradeIndex(out var grade, out var index);

                return new Tuple<int, ulong, TScalar>(grade, index, pair.Value);
            });
        }

        public override IEnumerable<GaTerm<TScalar>> GetTerms()
        {
            return IdScalarTree.Select(pair => GaTerm<TScalar>.CreateUniform(pair.Key, pair.Value));
        }

        public override IEnumerable<GaTerm<TScalar>> GetNotZeroTerms()
        {
            return IdScalarTree
                .Where(pair => !ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetNotNearZeroTerms()
        {
            return IdScalarTree
                .Where(pair => !ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetZeroTerms()
        {
            return IdScalarTree
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetNearZeroTerms()
        {
            return IdScalarTree
                .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<TScalar>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<KeyValuePair<ulong, TScalar>> GetIdScalarPairs()
        {
            return IdScalarTree;
        }


        public override IGaMultivectorStorage<TScalar> CopyToMultivectorStorage()
        {
            return new GaMultivectorTreeStorage<TScalar>(
                ScalarProcessor, 
                IdScalarTree.GetCopy(),
                MaxBasisBladeId
            );
        }

        public override GaMultivectorGradedStorage<TScalar> GetMultivectorGradedStorageCopy()
        {
            var composer = new GaMultivectorGradedStorageComposer<TScalar>(ScalarProcessor);

            composer.AddTerms(GetIdScalarPairs());

            return composer.CreateMultivectorGradedStorage();
        }

        
        public override GaBinaryTree<TScalar> GetBinaryTree(int treeDepth)
        {
            return treeDepth == IdScalarTree.TreeDepth
                ? IdScalarTree
                : new GaBinaryTree<TScalar>(treeDepth, IdScalarTree);
        }

        public override IGaGbtMultivectorStorageStack1<TScalar> CreateGbtStack(int treeDepth, int capacity)
        {
            return GaGbtMultivectorStorageUniformStack1<TScalar>.Create(
                capacity, 
                treeDepth, 
                this
            );
        }
        
        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<ulong, TScalar, TScalar2> idScalarMapping)
        {
            return new GaMultivectorTreeStorage<TScalar2>(
                scalarProcessor,
                IdScalarTree.ToDictionary(
                    pair => pair.Key,
                    pair => idScalarMapping(pair.Key, pair.Value)
                ),
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<int, ulong, TScalar, TScalar2> gradeIndexScalarMapping)
        {
            return new GaMultivectorTreeStorage<TScalar2>(
                scalarProcessor,
                IdScalarTree.ToDictionary(
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
            return new GaMultivectorTreeStorage<TScalar2>(
                scalarProcessor,
                IdScalarTree.ToDictionary(
                    pair => pair.Key,
                    pair => scalarMapping(pair.Value)
                ),
                MaxBasisBladeId
            );
        }


        public override IGaMultivectorStorage<TScalar> GetCompactStorage()
        {
            switch (IdScalarTree.Count)
            {
                case 0:
                    return GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);

                case 1:
                    var (id, scalar) = IdScalarTree.First();

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

            composer.SetTerms(IdScalarTree);

            return composer.GetCompactGradedStorage();
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy()
        {
            return new GaMultivectorTreeStorage<TScalar>(
                ScalarProcessor,
                IdScalarTree.CopyToDictionary(),
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaMultivectorTreeStorage<TScalar>(
                ScalarProcessor,
                IdScalarTree.CopyToDictionary(scalarMapping),
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative()
        {
            return new GaMultivectorTreeStorage<TScalar>(
                ScalarProcessor,
                IdScalarTree.CopyToDictionary(ScalarProcessor.Negative),
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative(Predicate<int> gradeToNegativePredicate)
        {
            return new GaMultivectorTreeStorage<TScalar>(
                ScalarProcessor,
                IdScalarTree.ToDictionary(
                    pair => pair.Key,
                    pair => 
                        gradeToNegativePredicate(pair.Key.BasisBladeGrade())
                            ? ScalarProcessor.Negative(pair.Value) 
                            : pair.Value
                ),
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetReverse()
        {
            var leafNodeValuesArray = IdScalarTree.GetLeafNodeValuesArrayCopyByIdValue(
                (id, scalar) => 
                    id.BasisBladeIdHasNegativeReverse() 
                        ? ScalarProcessor.Negative(scalar) 
                        : scalar
            );

            var scalarsTree = IdScalarTree.GetCopy(leafNodeValuesArray);

            return new GaMultivectorTreeStorage<TScalar>(
                ScalarProcessor, 
                scalarsTree,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetGradeInvolution()
        {
            var leafNodeValuesArray = IdScalarTree.GetLeafNodeValuesArrayCopyByIdValue(
                (id, scalar) => 
                    id.BasisBladeIdHasNegativeGradeInvolution() 
                        ? ScalarProcessor.Negative(scalar) 
                        : scalar
            );

            var scalarsTree = IdScalarTree.GetCopy(leafNodeValuesArray);

            return new GaMultivectorTreeStorage<TScalar>(
                ScalarProcessor, 
                scalarsTree,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar> GetCliffordConjugate()
        {
            var leafNodeValuesArray = IdScalarTree.GetLeafNodeValuesArrayCopyByIdValue(
                (id, scalar) => 
                    id.BasisBladeIdHasNegativeCliffordConjugate() 
                        ? ScalarProcessor.Negative(scalar) 
                        : scalar
            );

            var scalarsTree = IdScalarTree.GetCopy(leafNodeValuesArray);

            return new GaMultivectorTreeStorage<TScalar>(
                ScalarProcessor, 
                scalarsTree,
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
            return IdScalarTree.TryGetValue(0, out var scalar)
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
            var indexScalarDictionary = IdScalarTree
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
            var indexScalarDictionary = IdScalarTree
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
            var indexScalarDictionary = IdScalarTree
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
            var indexScalarDictionary = IdScalarTree
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
            var indexScalarDictionary = IdScalarTree
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
            var indexScalarDictionary = IdScalarTree
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
            var indexScalarDictionary = IdScalarTree
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
            var indexScalarDictionary = IdScalarTree
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
            var indexScalarDictionary = IdScalarTree
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

            var indexScalarDictionary = IdScalarTree
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

            var indexScalarDictionary = IdScalarTree
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, bool> gradeIndexSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, bool> scalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, TScalar, bool> idScalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, TScalar, bool> gradeIndexScalarSelection)
        {
            throw new NotImplementedException();
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, TScalar>();
            var indexScalarDictionary2 = new Dictionary<ulong, TScalar>();

            foreach (var (id, scalar) in IdScalarTree)
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

            foreach (var (id, scalar) in IdScalarTree)
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

            foreach (var (id, scalar) in IdScalarTree)
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