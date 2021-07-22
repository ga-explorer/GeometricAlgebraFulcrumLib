using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Trees;


namespace GeometricAlgebraFulcrumLib.Storage
{
    public sealed class GasTreeMultivector<T>
        : GasMultivectorBase<T>, IGasTermsMultivector<T>
    {
        private GaBinaryTree<T> IdScalarTree { get; }


        public override bool IsUniform 
            => true;
        
        public override bool IsGraded 
            => false;

        public override uint VSpaceDimension 
            => (uint) MaxBasisBladeId.LastOneBitPosition() + 1;

        public override int GradesCount
            => IdScalarTree
                .GetLeafNodeIDs()
                .Select(id => id.BasisBladeGrade())
                .Distinct()
                .Count();

        public override int TermsCount 
            => IdScalarTree.Count;

        public override T this[ulong id]
        {
            get => IdScalarTree[id];
            set => IdScalarTree[id] = value;
        }

        public override T this[uint grade, ulong index]
        {
            get => IdScalarTree[GaBasisUtils.BasisBladeId(grade, index)];
            set => IdScalarTree[GaBasisUtils.BasisBladeId(grade, index)] = value;
        }


        internal GasTreeMultivector(IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<ulong> leafNodeIDsList, ulong maxBasisBladeId)
            : base(scalarProcessor, maxBasisBladeId)
        {
            var treeDepth = leafNodeIDsList.Max().LastOneBitPosition() + 1;

            IdScalarTree = new GaBinaryTree<T>(treeDepth, leafNodeIDsList);
        }

        internal GasTreeMultivector(IGaScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, T> leafNodes, ulong maxBasisBladeId)
            : base(scalarProcessor, maxBasisBladeId)
        {
            var treeDepth = leafNodes.Keys.Max().LastOneBitPosition() + 1;

            IdScalarTree = new GaBinaryTree<T>(treeDepth, leafNodes);
        }

        internal GasTreeMultivector(IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<ulong> leafNodeIDsList, IReadOnlyCollection<T> leafNodeValuesList, ulong maxBasisBladeId)
            : base(scalarProcessor, maxBasisBladeId)
        {
            var treeDepth = leafNodeIDsList.Max().LastOneBitPosition() + 1;

            IdScalarTree = new GaBinaryTree<T>(treeDepth, leafNodeIDsList, leafNodeValuesList);
        }

        internal GasTreeMultivector(IGaScalarProcessor<T> scalarProcessor, GaBinaryTree<T> idScalarTree, ulong maxBasisBladeId)
            : base(scalarProcessor, maxBasisBladeId)
        {
            IdScalarTree = idScalarTree;
        }


        public override bool ContainsKey(ulong key)
        {
            return IdScalarTree.ContainsKey(key);
        }

        public override bool TryGetValue(ulong key, out T value)
        {
            return IdScalarTree.TryGetValue(key, out value);
        }

        public override bool ContainsTermsOfGrade(uint grade)
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

        public override bool IsKVector(uint grade)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<uint> GetGrades()
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

        public override bool ContainsTerm(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return IdScalarTree.ContainsKey(id);
        }


        public override T GetTermScalar(ulong id)
        {
            return IdScalarTree.TryGetValue(id, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        public override T GetTermScalar(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return IdScalarTree.TryGetValue(id, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        public override bool TryGetTermScalar(ulong id, out T value)
        {
            if (IdScalarTree.TryGetValue(id, out value))
                return true;

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public override bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            if (IdScalarTree.TryGetValue(id, out value))
                return true;

            value = ScalarProcessor.ZeroScalar;
            return false;
        }


        public override GaTerm<T> GetTerm(ulong id)
        {
            return GaTerm<T>.CreateUniform(id, this[id]);
        }

        public override GaTerm<T> GetTerm(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return GaTerm<T>.CreateUniform(id, this[id]);
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


        public override IEnumerable<IGasKVector<T>> GetKVectorStorages()
        {
            return IdScalarTree.GroupBy(
                pair => pair.Key.BasisBladeGrade()
            ).Select(g => 
                ScalarProcessor.CreateKVector(g.Key,
                    g.ToDictionary(
                        pair => pair.Key.BasisBladeIndex(), 
                        pair => pair.Value
                    )
                )
            );
        }

        public override IReadOnlyDictionary<uint, IGasKVector<T>> GetKVectorStoragesDictionary()
        {
            return IdScalarTree.GroupBy(
                pair => pair.Key.BasisBladeGrade()
            ).ToDictionary(
                g => g.Key,
                g => ScalarProcessor.CreateKVector(g.Key,
                    g.ToDictionary(
                        pair => pair.Key.BasisBladeIndex(), 
                        pair => pair.Value
                    )
                )
            );
        }

        public override bool TryGetKVectorStorage(uint grade, out IGasKVector<T> storage)
        {
            if (grade == 0)
            {
                if (IdScalarTree.TryGetValue(0, out var scalar))
                {
                    storage = ScalarProcessor.CreateScalar(scalar);
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

                    storage = ScalarProcessor.CreateKVector(grade, index, scalar);
                    return true;

                default:
                    storage = grade switch
                    {
                        1 => ScalarProcessor.CreateVector(indexScalarDictionary),
                        2 => ScalarProcessor.CreateBivector(indexScalarDictionary),
                        _ => ScalarProcessor.CreateKVector(grade, indexScalarDictionary)
                    };

                    return true;
            }
        }

        public override bool TryGetKVectorStorageDictionary(uint grade, out IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            if (grade == 0)
            {
                if (IdScalarTree.TryGetValue(0, out var scalar))
                {
                    indexScalarDictionary = new Dictionary<ulong, T>(){{0UL, scalar}};
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

        public override IReadOnlyDictionary<ulong, T> GetIdScalarDictionary()
        {
            return IdScalarTree;
        }

        public override IReadOnlyDictionary<uint, Dictionary<ulong, T>> GetGradeIndexScalarDictionary()
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

        public override IEnumerable<Tuple<uint, ulong>> GetGradeIndexTuples()
        {
            return IdScalarTree.Keys.Select(id => id.BasisBladeGradeIndex());
        }

        public override IEnumerable<IGaBasisBlade> GetBasisBlades()
        {
            return IdScalarTree.GetLeafNodeIDs().Select(id => 
                (IGaBasisBlade) id.CreateUniformBasisBlade()
            );
        }

        public override IEnumerable<T> GetScalars()
        {
            return IdScalarTree.Values;
        }

        public override IEnumerable<Tuple<ulong, T>> GetIdScalarTuples()
        {
            return IdScalarTree.Select(pair => 
                new Tuple<ulong, T>(pair.Key, pair.Value)
            );
        }

        public override IEnumerable<Tuple<uint, ulong, T>> GetGradeIndexScalarTuples()
        {
            return IdScalarTree.Select(pair =>
            {
                pair.Key.BasisBladeGradeIndex(out var grade, out var index);

                return new Tuple<uint, ulong, T>(grade, index, pair.Value);
            });
        }

        public override IEnumerable<GaTerm<T>> GetTerms()
        {
            return IdScalarTree.Select(pair => GaTerm<T>.CreateUniform(pair.Key, pair.Value));
        }

        public override IEnumerable<GaTerm<T>> GetNotZeroTerms()
        {
            return IdScalarTree
                .Where(pair => !ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<T>> GetNotNearZeroTerms()
        {
            return IdScalarTree
                .Where(pair => !ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<T>> GetZeroTerms()
        {
            return IdScalarTree
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<T>> GetNearZeroTerms()
        {
            return IdScalarTree
                .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs()
        {
            return IdScalarTree;
        }


        public override IGasMultivector<T> CopyToMultivectorStorage()
        {
            return new GasTreeMultivector<T>(
                ScalarProcessor, 
                IdScalarTree.GetCopy(),
                MaxBasisBladeId
            );
        }

        public override IGasGradedMultivector<T> GetGradedMultivectorCopy()
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(ScalarProcessor);

            composer.AddTerms(GetIdScalarPairs());

            return composer.CreateMultivectorGradedStorage();
        }

        
        public override GaBinaryTree<T> GetBinaryTree(int treeDepth)
        {
            return treeDepth == IdScalarTree.TreeDepth
                ? IdScalarTree
                : new GaBinaryTree<T>(treeDepth, IdScalarTree);
        }

        public override IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity)
        {
            return GaGbtMultivectorStorageUniformStack1<T>.Create(
                capacity, 
                treeDepth, 
                this
            );
        }
        
        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<ulong, T, T2> idScalarMapping)
        {
            return new GasTreeMultivector<T2>(
                scalarProcessor,
                IdScalarTree.ToDictionary(
                    pair => pair.Key,
                    pair => idScalarMapping(pair.Key, pair.Value)
                ),
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(
            IGaScalarProcessor<T2> scalarProcessor, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return new GasTreeMultivector<T2>(
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

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<T, T2> scalarMapping)
        {
            return new GasTreeMultivector<T2>(
                scalarProcessor,
                IdScalarTree.ToDictionary(
                    pair => pair.Key,
                    pair => scalarMapping(pair.Value)
                ),
                MaxBasisBladeId
            );
        }


        public override IGasMultivector<T> GetCompactStorage()
        {
            switch (IdScalarTree.Count)
            {
                case 0:
                    return ScalarProcessor.CreateZeroScalar();

                case 1:
                    var (id, scalar) = IdScalarTree.First();

                    return id == 0
                        ? ScalarProcessor.CreateScalar(scalar)
                        : ScalarProcessor.CreateKVector(id, scalar);

                default:
                    return this;
            }
        }

        public override IGasGradedMultivector<T> GetCompactGradedStorage()
        {
            var composer = GaMultivectorStorageComposerBase<T>.CreateGradedComposer(ScalarProcessor);

            composer.SetTerms(IdScalarTree);

            return composer.GetCompactGradedMultivector();
        }

        public override IGasMultivector<T> GetCopy()
        {
            return new GasTreeMultivector<T>(
                ScalarProcessor,
                IdScalarTree.CopyToDictionary(),
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T> GetCopy(Func<T, T> scalarMapping)
        {
            return new GasTreeMultivector<T>(
                ScalarProcessor,
                IdScalarTree.CopyToDictionary(scalarMapping),
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T> GetNegative()
        {
            return new GasTreeMultivector<T>(
                ScalarProcessor,
                IdScalarTree.CopyToDictionary(ScalarProcessor.Negative),
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T> GetNegative(Predicate<uint> gradeToNegativePredicate)
        {
            return new GasTreeMultivector<T>(
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

        public override IGasMultivector<T> GetReverse()
        {
            var leafNodeValuesArray = IdScalarTree.GetLeafNodeValuesArrayCopyByIdValue(
                (id, scalar) => 
                    id.BasisBladeIdHasNegativeReverse() 
                        ? ScalarProcessor.Negative(scalar) 
                        : scalar
            );

            var scalarsTree = IdScalarTree.GetCopy(leafNodeValuesArray);

            return new GasTreeMultivector<T>(
                ScalarProcessor, 
                scalarsTree,
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T> GetGradeInvolution()
        {
            var leafNodeValuesArray = IdScalarTree.GetLeafNodeValuesArrayCopyByIdValue(
                (id, scalar) => 
                    id.BasisBladeIdHasNegativeGradeInvolution() 
                        ? ScalarProcessor.Negative(scalar) 
                        : scalar
            );

            var scalarsTree = IdScalarTree.GetCopy(leafNodeValuesArray);

            return new GasTreeMultivector<T>(
                ScalarProcessor, 
                scalarsTree,
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T> GetCliffordConjugate()
        {
            var leafNodeValuesArray = IdScalarTree.GetLeafNodeValuesArrayCopyByIdValue(
                (id, scalar) => 
                    id.BasisBladeIdHasNegativeCliffordConjugate() 
                        ? ScalarProcessor.Negative(scalar) 
                        : scalar
            );

            var scalarsTree = IdScalarTree.GetCopy(leafNodeValuesArray);

            return new GasTreeMultivector<T>(
                ScalarProcessor, 
                scalarsTree,
                MaxBasisBladeId
            );
        }

        public override IGasTermsMultivector<T> ToTermsMultivector()
        {
            return this;
        }

        public override IGasGradedMultivector<T> ToGradedMultivector()
        {
            return GetGradedMultivectorCopy();
        }

        
        public override IGasScalar<T> GetScalarPart()
        {
            return IdScalarTree.TryGetValue(0, out var scalar)
                ? ScalarProcessor.CreateScalar(scalar)
                : ScalarProcessor.CreateZeroScalar();
        }

        public override IGasScalar<T> GetScalarPart(Func<T, T> scalarMapping)
        {
            var scalar = scalarMapping(GetTermScalar(0));

            return ScalarProcessor.CreateScalar(scalar);
        }

        public override IGasVector<T> GetVectorPart()
        {
            var indexScalarDictionary = IdScalarTree
                .Where(pair => pair.Key.BasisBladeGrade() == 1)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroVector() 
                : ScalarProcessor.CreateVector(indexScalarDictionary);
        }

        public override IGasVector<T> GetVectorPart(Func<T, T> scalarMapping)
        {
            var indexScalarDictionary = IdScalarTree
                .Where(pair => pair.Key.BasisBladeGrade() == 1)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => scalarMapping(pair.Value)
                );

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroVector() 
                : ScalarProcessor.CreateVector(indexScalarDictionary);
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            var indexScalarDictionary = IdScalarTree
                .Where(pair => pair.Key.BasisBladeGrade() == 1)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => indexScalarMapping(pair.Key, pair.Value)
                );

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroVector() 
                : ScalarProcessor.CreateVector(indexScalarDictionary);
        }

        public override IGasVector<T> GetVectorPart(Func<T, bool> scalarSelection)
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
                ? ScalarProcessor.CreateZeroVector() 
                : ScalarProcessor.CreateVector(indexScalarDictionary);
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
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
                ? ScalarProcessor.CreateZeroVector() 
                : ScalarProcessor.CreateVector(indexScalarDictionary);
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary = IdScalarTree
                .Where(pair =>
                {
                    pair.Key.BasisBladeGradeIndex(out var grade, out var index);
                    return grade == 1 && indexSelection(index);
                })
                .CopyToDictionary();

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroVector() 
                : ScalarProcessor.CreateVector(indexScalarDictionary);
        }

        public override IGasBivector<T> GetBivectorPart()
        {
            var indexScalarDictionary = IdScalarTree
                .Where(pair => pair.Key.BasisBladeGrade() == 2)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroBivector() 
                : ScalarProcessor.CreateBivector(indexScalarDictionary);
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, T> scalarMapping)
        {
            var indexScalarDictionary = IdScalarTree
                .Where(pair => pair.Key.BasisBladeGrade() == 2)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => scalarMapping(pair.Value)
                );

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroBivector() 
                : ScalarProcessor.CreateBivector(indexScalarDictionary);
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            var indexScalarDictionary = IdScalarTree
                .Where(pair => pair.Key.BasisBladeGrade() == 2)
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => indexScalarMapping(pair.Key, pair.Value)
                );

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroBivector() 
                : ScalarProcessor.CreateBivector(indexScalarDictionary);
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            throw new NotImplementedException();
        }

        public override IGasKVector<T> GetKVectorPart(uint grade)
        {
            if (TryGetKVectorStorage(grade, out var storage))
                return storage;

            return grade switch
            {
                0 => ScalarProcessor.CreateZeroScalar(),
                1 => ScalarProcessor.CreateZeroVector(),
                2 => ScalarProcessor.CreateZeroBivector(),
                _ => ScalarProcessor.CreateZeroKVector(grade)
            };
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, T> scalarMapping)
        {
            if (grade == 0)
                return ScalarProcessor.CreateScalar(scalarMapping(GetTermScalar(0)));

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
                ? ScalarProcessor.CreateKVector(grade, indexScalarDictionary)
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, T> indexScalarMapping)
        {
            if (grade == 0)
                return ScalarProcessor.CreateScalar(indexScalarMapping(0, GetTermScalar(0)));

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
                ? ScalarProcessor.CreateKVector(grade, indexScalarDictionary)
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            throw new NotImplementedException();
        }
        

        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            throw new NotImplementedException();
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            throw new NotImplementedException();
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            throw new NotImplementedException();
        }

        public override IGasMultivector<T> GetMultivectorPart(
            Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            throw new NotImplementedException();
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

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

            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateVector(indexScalarDictionary1),
                ScalarProcessor.CreateVector(indexScalarDictionary2)
            );
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

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

            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateVector(indexScalarDictionary1),
                ScalarProcessor.CreateVector(indexScalarDictionary2)
            );
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

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

            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateVector(indexScalarDictionary1),
                ScalarProcessor.CreateVector(indexScalarDictionary2)
            );
        }
    }
}