using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
    public sealed class GasTermsMultivector<T>
        : GasMultivectorBase<T>, IGasTermsMultivector<T>
    {
        private Dictionary<ulong, T> IdScalarDictionary { get; }


        public override uint VSpaceDimension 
            => (uint) MaxBasisBladeId.LastOneBitPosition() + 1;

        public override int GradesCount =>
            IdScalarDictionary
                .Keys
                .Select(id => id.BasisBladeGrade())
                .Distinct()
                .Count();

        public override int TermsCount 
            => IdScalarDictionary.Count;

        public override T this[ulong id]
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

        public override T this[uint grade, ulong index]
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


        internal GasTermsMultivector([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] Dictionary<ulong, T> idScalarDictionary, ulong maxBasisBladeId)
            : base(scalarProcessor, maxBasisBladeId)
        {
            IdScalarDictionary = idScalarDictionary;
        }


        public override bool ContainsKey(ulong key)
        {
            return IdScalarDictionary.ContainsKey(key);
        }

        public override bool TryGetValue(ulong key, out T value)
        {
            return IdScalarDictionary.TryGetValue(key, out value);
        }

        public override bool ContainsTermsOfGrade(uint grade)
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
                pair => 
                    pair.Key > 0 && 
                    !ScalarProcessor.IsZero(pair.Value)
            );
        }

        public override bool IsVector()
        {
            return !IdScalarDictionary.Any(
                pair => 
                    !pair.Key.IsBasicPattern() && 
                    !ScalarProcessor.IsZero(pair.Value)
            );
        }

        public override bool IsBivector()
        {
            return !IdScalarDictionary.Any(
                pair => 
                    pair.Key.BasisBladeGrade() != 2 && 
                    !ScalarProcessor.IsZero(pair.Value)
            );
        }

        public override bool IsKVector()
        {
            return GetNotZeroTerms()
                .Select(term => term.BasisBlade.Grade)
                .Distinct()
                .Count() < 2;
        }

        public override bool IsKVector(uint grade)
        {
            return !IdScalarDictionary.Any(
                pair => 
                    pair.Key.BasisBladeGrade() != grade && 
                    !ScalarProcessor.IsZero(pair.Value)
            );
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


        public override T GetTermScalar(ulong id)
        {
            return IdScalarDictionary.TryGetValue(id, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        public override T GetTermScalar(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            return IdScalarDictionary.TryGetValue(id, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }


        public override bool TryGetTermScalar(ulong id, out T value)
        {
            if (IdScalarDictionary.TryGetValue(id, out value))
                return true;

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public override bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            if (IdScalarDictionary.TryGetValue(id, out value))
                return true;

            value = ScalarProcessor.ZeroScalar;
            return false;
        }


        public GasTermsMultivector<T> GetNegativeScalarsCopy()
        {
            var idScalarDictionary = 
                IdScalarDictionary.CopyToDictionary(ScalarProcessor.Negative);

            return new GasTermsMultivector<T>(
                ScalarProcessor,
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GasTermsMultivector<T> GetLeftScaledScalarsCopy(T scalingFactor)
        {
            var idScalarDictionary = 
                IdScalarDictionary.CopyToDictionary(
                    scalar => ScalarProcessor.Times(scalingFactor, scalar)
                );

            return new GasTermsMultivector<T>(
                ScalarProcessor,
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GasTermsMultivector<T> GetRightScaledScalarsCopy(T scalingFactor)
        {
            var idScalarDictionary = IdScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => ScalarProcessor.Times(pair.Value, scalingFactor)
            );

            return new GasTermsMultivector<T>(
                ScalarProcessor,
                idScalarDictionary,
                MaxBasisBladeId
            );
        }


        public override IEnumerable<IGasKVector<T>> GetKVectorStorages()
        {
            return IdScalarDictionary.GroupBy(
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
            return IdScalarDictionary.GroupBy(
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
                if (IdScalarDictionary.TryGetValue(0, out var scalar))
                {
                    storage = ScalarProcessor.CreateScalar(scalar);
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
                if (IdScalarDictionary.TryGetValue(0, out var scalar))
                {
                    indexScalarDictionary = new Dictionary<ulong, T>(){{0UL, scalar}};
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

        public override IReadOnlyDictionary<ulong, T> GetIdScalarDictionary()
        {
            return IdScalarDictionary;
        }

        public override IReadOnlyDictionary<uint, Dictionary<ulong, T>> GetGradeIndexScalarDictionary()
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

        public override IEnumerable<GaTerm<T>> GetNotZeroTerms()
        {
            return IdScalarDictionary
                .Where(pair => !ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<T>> GetNotNearZeroTerms()
        {
            return IdScalarDictionary
                .Where(pair => !ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<T>> GetZeroTerms()
        {
            return IdScalarDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateUniform(pair.Key, pair.Value)
                );
        }

        public override IEnumerable<GaTerm<T>> GetNearZeroTerms()
        {
            return IdScalarDictionary
                .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                .Select(pair => 
                    GaTerm<T>.CreateUniform(pair.Key, pair.Value)
                );
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

        
        public override IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity)
        {
            return GaGbtMultivectorStorageUniformStack1<T>.Create(
                capacity, 
                treeDepth, 
                this
            );
        }

        /// <summary>
        /// Construct a binary tree representation of this storage
        /// </summary>
        /// <returns></returns>
        public override GaBinaryTree<T> GetBinaryTree(int treeDepth)
        {
            Debug.Assert(treeDepth > 0);
            //var treeDepth = GetIds().Max().LastOneBitPosition() + 1;

            var dict = GetIdScalarPairs()
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );

            return new GaBinaryTree<T>(treeDepth, dict);
        }


        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<ulong, T, T2> idScalarMapping)
        {
            return new GasTermsMultivector<T2>(
                scalarProcessor,
                IdScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => idScalarMapping(pair.Key, pair.Value)
                ),
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(
            IGaScalarProcessor<T2> scalarProcessor, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return new GasTermsMultivector<T2>(
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

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<T, T2> scalarMapping)
        {
            return new GasTermsMultivector<T2>(
                scalarProcessor,
                IdScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => scalarMapping(pair.Value)
                ),
                MaxBasisBladeId
            );
        }


        public override IGasMultivector<T> CopyToMultivectorStorage()
        {
            var idScalarDictionary =
                GetIdScalarPairs().CopyToDictionary();

            return new GasTermsMultivector<T>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasGradedMultivector<T> GetGradedMultivectorCopy()
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(ScalarProcessor);

            composer.AddTerms(GetIdScalarPairs());

            return composer.CreateMultivectorGradedStorage();
        }


        public override IGasMultivector<T> GetCompactStorage()
        {
            switch (IdScalarDictionary.Count)
            {
                case 0:
                    return ScalarProcessor.CreateZeroScalar();

                case 1:
                    var (id, scalar) = IdScalarDictionary.First();

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

            composer.SetTerms(IdScalarDictionary);

            return composer.GetCompactGradedMultivector();
        }

        public override IGasMultivector<T> GetCopy()
        {
            var idScalarDictionary = IdScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return new GasTermsMultivector<T>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T> GetCopy(Func<T, T> scalarMapping)
        {
            var idScalarDictionary = IdScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => scalarMapping(pair.Value)
            );

            return new GasTermsMultivector<T>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T> GetNegative()
        {
            var idScalarDictionary = IdScalarDictionary.CopyToDictionary(
                ScalarProcessor.Negative
            );

            return new GasTermsMultivector<T>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T> GetNegative(Predicate<uint> gradeToNegativePredicate)
        {
            var idScalarDictionary = IdScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => 
                    gradeToNegativePredicate(pair.Key.BasisBladeGrade())
                        ? ScalarProcessor.Negative(pair.Value) 
                        : pair.Value
            );

            return new GasTermsMultivector<T>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T> GetReverse()
        {
            var idScalarDictionary = IdScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => 
                    pair.Key.BasisBladeIdHasNegativeReverse() 
                        ? ScalarProcessor.Negative(pair.Value) 
                        : pair.Value
            );

            return new GasTermsMultivector<T>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T> GetGradeInvolution()
        {
            var idScalarDictionary = IdScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => 
                    pair.Key.BasisBladeIdHasNegativeGradeInvolution() 
                        ? ScalarProcessor.Negative(pair.Value) 
                        : pair.Value
            );

            return new GasTermsMultivector<T>(
                ScalarProcessor, 
                idScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T> GetCliffordConjugate()
        {
            var idScalarDictionary = IdScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => 
                    pair.Key.BasisBladeIdHasNegativeCliffordConjugate() 
                        ? ScalarProcessor.Negative(pair.Value) 
                        : pair.Value
            );

            return new GasTermsMultivector<T>(
                ScalarProcessor, 
                idScalarDictionary,
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
            return IdScalarDictionary.TryGetValue(0, out var scalar)
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
            var indexScalarDictionary = IdScalarDictionary
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
            var indexScalarDictionary = IdScalarDictionary
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
            var indexScalarDictionary = IdScalarDictionary
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
            var indexScalarDictionary = IdScalarDictionary
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
            var indexScalarDictionary = IdScalarDictionary
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
            var indexScalarDictionary = IdScalarDictionary
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
            var indexScalarDictionary = IdScalarDictionary
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
            var indexScalarDictionary = IdScalarDictionary
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
            var indexScalarDictionary = IdScalarDictionary
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
            var indexScalarDictionary =             
                IdScalarDictionary
                    .Where(pair => pair.Key.BasisBladeGrade() == 2 && scalarSelection(pair.Value))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroBivector() 
                : ScalarProcessor.CreateBivector(indexScalarDictionary);
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary =             
                IdScalarDictionary
                    .Where(pair => pair.Key.BasisBladeGrade() == 2 && indexScalarSelection(pair.Key.BasisBladeIndex(), pair.Value))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroBivector() 
                : ScalarProcessor.CreateBivector(indexScalarDictionary);
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary =             
                IdScalarDictionary
                    .Where(pair => pair.Key.BasisBladeGrade() == 2 && indexSelection(pair.Key.BasisBladeIndex()))
                    .ToDictionary(
                        pair => pair.Key.BasisBladeIndex(),
                        pair => pair.Value
                    );

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroBivector() 
                : ScalarProcessor.CreateBivector(indexScalarDictionary);
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

            var indexScalarDictionary = IdScalarDictionary
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

            var indexScalarDictionary = IdScalarDictionary
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
            var indexScalarDictionary = IdScalarDictionary
                .Where(pair => 
                    pair.Key.BasisBladeGrade() == grade && 
                    scalarSelection(pair.Value)
                )
                .ToDictionary(
                    pair => pair.Key.BasisBladeIndex(),
                    pair => pair.Value
                );

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroKVector(grade) 
                : ScalarProcessor.CreateKVector(grade, indexScalarDictionary);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
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

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroKVector(grade) 
                : ScalarProcessor.CreateKVector(grade, indexScalarDictionary);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
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

            return indexScalarDictionary.Count == 0 
                ? ScalarProcessor.CreateZeroKVector(grade) 
                : ScalarProcessor.CreateKVector(grade, indexScalarDictionary);
        }
        
        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(ScalarProcessor);

            composer.SetTerms(
                GetIdScalarPairs().Where(pair => idSelection(pair.Key))
            );

            composer.RemoveZeroTerms();

            return composer.GetCompactMultivector();
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(ScalarProcessor);

            composer.SetTerms(
                GetIdScalarPairs().Where(pair =>
                {
                    pair.Key.BasisBladeGradeIndex(out var grade, out var index);
                    return gradeIndexSelection(grade, index);
                })
            );

            composer.RemoveZeroTerms();

            return composer.GetCompactMultivector();
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(ScalarProcessor);

            composer.SetTerms(
                GetIdScalarPairs().Where(pair => scalarSelection(pair.Value))
            );

            composer.RemoveZeroTerms();

            return composer.GetCompactMultivector();
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(ScalarProcessor);

            composer.SetTerms(
                GetIdScalarPairs().Where(pair => idScalarSelection(pair.Key, pair.Value))
            );

            composer.RemoveZeroTerms();

            return composer.GetCompactMultivector();
        }

        public override IGasMultivector<T> GetMultivectorPart(
            Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(ScalarProcessor);

            composer.SetTerms(
                GetIdScalarPairs().Where(pair =>
                {
                    pair.Key.BasisBladeGradeIndex(out var grade, out var index);
                    return gradeIndexScalarSelection(grade, index, pair.Value);
                })
            );

            composer.RemoveZeroTerms();

            return composer.GetCompactMultivector();
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
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

            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateVector(indexScalarDictionary1),
                ScalarProcessor.CreateVector(indexScalarDictionary2)
            );
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
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

            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateVector(indexScalarDictionary1),
                ScalarProcessor.CreateVector(indexScalarDictionary2)
            );
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<T, bool> scalarSelection)
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

            return new Tuple<IGasVector<T>, IGasVector<T>>(
                ScalarProcessor.CreateVector(indexScalarDictionary1),
                ScalarProcessor.CreateVector(indexScalarDictionary2)
            );
        }
    }
}