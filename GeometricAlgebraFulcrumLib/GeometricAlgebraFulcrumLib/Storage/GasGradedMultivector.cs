using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Trees;


namespace GeometricAlgebraFulcrumLib.Storage
{
    public sealed class GasGradedMultivector<T>
        : GasMultivectorBase<T>, IGasGradedMultivector<T>
    {
        private Dictionary<uint, Dictionary<ulong, T>> GradeIndexScalarDictionary { get; }


        public override uint VSpaceDimension 
            => (uint) MaxBasisBladeId.LastOneBitPosition() + 1;

        public override int GradesCount 
            => GradeIndexScalarDictionary.Count;

        public override int TermsCount 
            => GradeIndexScalarDictionary.Sum(p => p.Value.Count);

        public override T this[ulong id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                if (
                    GradeIndexScalarDictionary.TryGetValue(grade, out var storage) &&
                    storage.TryGetValue(index, out var scalar)
                )
                    return scalar;

                return ScalarProcessor.ZeroScalar;
            }
            set
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                var storage = GradeIndexScalarDictionary[grade];

                storage[index] = value;
            }
        }

        public override T this[uint grade, ulong index]
        {
            get
            { 
                if (
                    GradeIndexScalarDictionary.TryGetValue(grade, out var storage) &&
                    storage.TryGetValue(index, out var scalar)
                )
                    return scalar;

                return ScalarProcessor.ZeroScalar;
            }
            set
            {
                var storage = GradeIndexScalarDictionary[grade];

                storage[index] = value;
            }
        }

        public override bool IsUniform => false;

        public override bool IsGraded => true;


        internal GasGradedMultivector([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] Dictionary<uint, Dictionary<ulong, T>> gradeIndexScalarDictionary, ulong maxBasisBladeId)
            : base(scalarProcessor, maxBasisBladeId)
        {
            GradeIndexScalarDictionary = gradeIndexScalarDictionary;
        }


        public override bool ContainsKey(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return GradeIndexScalarDictionary.TryGetValue(grade, out var kVectorStorage) && 
                   kVectorStorage.ContainsKey(index);
        }

        public override bool TryGetValue(ulong id, out T value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (GradeIndexScalarDictionary.TryGetValue(grade, out var kVectorStorage))
                return kVectorStorage.TryGetValue(index, out value);

            value = ScalarProcessor.ZeroScalar;

            return false;
        }

        public override bool ContainsTermsOfGrade(uint grade)
        {
            return GradeIndexScalarDictionary.ContainsKey(grade);
        }


        public override bool IsEmpty()
        {
            return GradeIndexScalarDictionary.Count == 0;
        }

        public override bool IsZero()
        {
            return GradeIndexScalarDictionary.Count == 0 ||
                   GradeIndexScalarDictionary
                       .Values
                       .SelectMany(d => d.Values)
                       .All(storage => ScalarProcessor.IsZero(storage));
        }
        
        public override bool IsNearZero()
        {
            return GradeIndexScalarDictionary.Count == 0 ||
                   GradeIndexScalarDictionary
                       .Values
                       .SelectMany(d => d.Values)
                       .All(storage => ScalarProcessor.IsNearZero(storage));
        }

        public override bool IsScalar()
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                if (grade == 0)
                    continue;

                if (indexScalarDictionary.Values.Any(scalar => !ScalarProcessor.IsZero(scalar)))
                    return false;
            }

            return true;
        }

        public override bool IsVector()
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                if (grade == 1)
                    continue;

                if (indexScalarDictionary.Values.Any(scalar => !ScalarProcessor.IsZero(scalar)))
                    return false;
            }

            return true;
        }

        public override bool IsBivector()
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                if (grade == 2)
                    continue;

                if (indexScalarDictionary.Values.Any(scalar => !ScalarProcessor.IsZero(scalar)))
                    return false;
            }

            return true;
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
            foreach (var (g, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                if (g == grade)
                    continue;

                if (indexScalarDictionary.Values.Any(scalar => !ScalarProcessor.IsZero(scalar)))
                    return false;
            }

            return true;
        }

        public override IEnumerable<uint> GetGrades()
        {
            return GradeIndexScalarDictionary.Keys;
        }


        public override bool ContainsTerm(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return GradeIndexScalarDictionary.TryGetValue(grade, out var storage) &&
                   storage.ContainsKey(index);
        }

        public override bool ContainsTerm(uint grade, ulong index)
        {
            return GradeIndexScalarDictionary.TryGetValue(grade, out var storage) &&
                   storage.ContainsKey(index);
        }
        

        public override T GetTermScalar(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var storage)) 
                return ScalarProcessor.ZeroScalar;

            return storage.TryGetValue(index, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        public override T GetTermScalar(uint grade, ulong index)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var storage)) 
                return ScalarProcessor.ZeroScalar;

            return storage.TryGetValue(index, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        
        public override bool TryGetTermScalar(ulong id, out T value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (GradeIndexScalarDictionary.TryGetValue(grade, out var storage))
                return storage.TryGetValue(index, out value);

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public override bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            if (GradeIndexScalarDictionary.TryGetValue(grade, out var storage))
                return storage.TryGetValue(index, out value);

            value = ScalarProcessor.ZeroScalar;
            return false;
        }


        public override IEnumerable<IGasKVector<T>> GetKVectorStorages()
        {
            return GradeIndexScalarDictionary
                .Select(d => ScalarProcessor.CreateKVector(d.Key, 
                    d.Value
                )
            );
        }

        public override IReadOnlyDictionary<uint, IGasKVector<T>> GetKVectorStoragesDictionary()
        {
            return GradeIndexScalarDictionary
                .ToDictionary(
                    d => d.Key,
                    d => ScalarProcessor.CreateKVector(d.Key, 
                        d.Value
                    )
                );
        }

        public override bool TryGetKVectorStorage(uint grade, out IGasKVector<T> storage)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
            {
                storage = null;
                return false;
            }

            if (indexScalarDictionary.Count == 0)
            {
                storage = null;
                return false;
            }

            if (grade == 0)
            {
                storage = indexScalarDictionary.TryGetValue(0, out var scalar)
                    ? ScalarProcessor.CreateScalar(scalar)
                    : ScalarProcessor.CreateZeroScalar();
                return true;
            }

            if (indexScalarDictionary.Count == 1)
            {
                var (index, scalar) = indexScalarDictionary.First();
                storage = ScalarProcessor.CreateKVector(grade, index, scalar);
                return true;
            }

            if (grade == 1)
            {
                storage = ScalarProcessor.CreateVector(indexScalarDictionary);
                return true;
            }

            if (grade == 2)
            {
                storage = ScalarProcessor.CreateBivector(indexScalarDictionary);
                return true;
            }

            storage = ScalarProcessor.CreateKVector(grade, indexScalarDictionary);
            return true;
        }

        public override bool TryGetKVectorStorageDictionary(uint grade, out IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            if (GradeIndexScalarDictionary.TryGetValue(grade, out var dictionary))
            {
                indexScalarDictionary = dictionary;
                return true;
            }

            indexScalarDictionary = null;
            return false;
        }

        public override IReadOnlyDictionary<ulong, T> GetIdScalarDictionary()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage.Value.Select(pair => 
                        new KeyValuePair<ulong, T>(
                            GaBasisUtils.BasisBladeId(storage.Key, pair.Key), 
                            pair.Value
                        )
                    )
                ).ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );
        }

        public override IReadOnlyDictionary<uint, Dictionary<ulong, T>> GetGradeIndexScalarDictionary()
        {
            return GradeIndexScalarDictionary;
        }


        public GasGradedMultivector<T> GetNegativeScalarsCopy()
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => 
                        indexScalarDictionary.CopyToDictionary(ScalarProcessor.Negative)
                );

            return new GasGradedMultivector<T>(
                ScalarProcessor,
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public GasGradedMultivector<T> GetLeftScaledScalarsCopy(T value)
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => 
                        indexScalarDictionary.CopyToDictionary(s => 
                            ScalarProcessor.Times(value, s)
                        )
                );

            return new GasGradedMultivector<T>(
                ScalarProcessor,
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public GasGradedMultivector<T> GetRightScaledScalarsCopy(T value)
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => 
                    indexScalarDictionary.CopyToDictionary(s => 
                        ScalarProcessor.Times(s, value)
                    )
                );

            return new GasGradedMultivector<T>(
                ScalarProcessor,
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }


        public override GaTerm<T> GetTerm(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return GaTerm<T>.CreateGraded(
                grade,
                index,
                this[grade, index]
            );
        }

        public override GaTerm<T> GetTerm(uint grade, ulong index)
        {
            return GaTerm<T>.CreateGraded(
                grade,
                index,
                this[grade, index]
            );
        }


        public bool TryGetValue(uint grade, ulong index, out T value)
        {
            if (GradeIndexScalarDictionary.TryGetValue(grade, out var kVectorStorage))
                return kVectorStorage.TryGetValue(index, out value);

            value = ScalarProcessor.ZeroScalar;

            return false;
        }

        
        public override bool TryGetTerm(ulong id, out GaTerm<T> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (TryGetValue(grade, index, out var value))
            {
                term = GaTerm<T>.CreateGraded(grade, index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term)
        {
            if (TryGetValue(grade, index, out var value))
            {
                term = GaTerm<T>.CreateGraded(grade, index, value);
                return true;
            }

            term = null;
            return false;
        }


        public override IEnumerable<ulong> GetIds()
        {
            return GradeIndexScalarDictionary
                .SelectMany(pair => 
                    pair.Value.Keys.Select(
                        index => GaBasisUtils.BasisBladeId(pair.Key, index)
                    )
                );
        }

        public override IEnumerable<Tuple<uint, ulong>> GetGradeIndexTuples()
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            foreach (var index in indexScalarDictionary.Keys)
                yield return new Tuple<uint, ulong>(grade, index);
        }

        public override IEnumerable<IGaBasisBlade> GetBasisBlades()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage.Value.Select(pair => 
                        (IGaBasisBlade) storage.Key.CreateGradedBasisBlade(pair.Key)
                    )
                );
        }

        public override IEnumerable<GaTerm<T>> GetTerms()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage.Value.Select(pair => 
                        GaTerm<T>.CreateGraded(storage.Key, pair.Key, pair.Value)
                    )
                );
        }

        public override IEnumerable<GaTerm<T>> GetNotZeroTerms()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage
                        .Value
                        .Where(pair => !ScalarProcessor.IsZero(pair.Value))
                        .Select(pair => 
                            GaTerm<T>.CreateGraded(storage.Key, pair.Key, pair.Value)
                        )
                );
        }

        public override IEnumerable<GaTerm<T>> GetNotNearZeroTerms()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage
                        .Value
                        .Where(pair => !ScalarProcessor.IsNearZero(pair.Value))
                        .Select(pair => 
                            GaTerm<T>.CreateGraded(storage.Key, pair.Key, pair.Value)
                        )
                );
        }

        public override IEnumerable<GaTerm<T>> GetZeroTerms()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage
                        .Value
                        .Where(pair => ScalarProcessor.IsZero(pair.Value))
                        .Select(pair => 
                            GaTerm<T>.CreateGraded(storage.Key, pair.Key, pair.Value)
                        )
                );
        }

        public override IEnumerable<GaTerm<T>> GetNearZeroTerms()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage
                        .Value
                        .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                        .Select(pair => 
                            GaTerm<T>.CreateGraded(storage.Key, pair.Key, pair.Value)
                        )
                );
        }

        public override IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage.Value.Select(pair => 
                        new KeyValuePair<ulong, T>(
                            GaBasisUtils.BasisBladeId(storage.Key, pair.Key), 
                            pair.Value
                        )
                    )
                );
        }

        public override IEnumerable<Tuple<ulong, T>> GetIdScalarTuples()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage.Value.Select(pair => 
                        new Tuple<ulong, T>(
                            GaBasisUtils.BasisBladeId(storage.Key, pair.Key), 
                            pair.Value
                        )
                    )
                );
        }

        public override IEnumerable<Tuple<uint, ulong, T>> GetGradeIndexScalarTuples()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage.Value.Select(pair => 
                        new Tuple<uint, ulong, T>(storage.Key, pair.Key, pair.Value)
                    )
                );
        }

        public override IEnumerable<T> GetScalars()
        {
            return GradeIndexScalarDictionary
                .Values
                .SelectMany(storage => storage.Values);
        }


        public override IGasMultivector<T> GetCompactStorage()
        {
            return GetCompactGradedStorage();
        }

        public override IGasGradedMultivector<T> GetCompactGradedStorage()
        {
            var gradesCount = GradeIndexScalarDictionary.Count;

            if (gradesCount == 0)
                return ScalarProcessor.CreateZeroScalar();

            if (gradesCount > 1) 
                return this;

            var (grade, indexScalarDictionary) = 
                GradeIndexScalarDictionary.First();
                
            return indexScalarDictionary.Count == 0
                ? ScalarProcessor.CreateZeroKVector(grade)
                : ScalarProcessor.CreateKVector(grade, indexScalarDictionary);
        }
        

        public override IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity)
        {
            //return GaGbtMultivectorStorageGradedStack1<T>.Create(
            //    capacity, 
            //    treeDepth,
            //    this
            //);
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
            if (treeDepth < VSpaceDimension)
                throw new InvalidOperationException();

            var dict = GetIdScalarPairs()
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );

            return new GaBinaryTree<T>(treeDepth, dict);
        }

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<T, T2> scalarMapping)
        {
            var gradeIndexScalarDictionary =
                GradeIndexScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.ToDictionary(
                        indexScalarPair => indexScalarPair.Key,
                        indexScalarPair => scalarMapping(indexScalarPair.Value)
                    )
                );

            return new GasGradedMultivector<T2>(
                scalarProcessor,
                gradeIndexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<ulong, T, T2> idScalarMapping)
        {
            var gradeIndexScalarDictionary =
                GradeIndexScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.ToDictionary(
                        indexScalarPair => indexScalarPair.Key,
                        indexScalarPair => idScalarMapping(
                            GaBasisUtils.BasisBladeId(pair.Key, indexScalarPair.Key), 
                            indexScalarPair.Value
                        )
                    )
                );

            return new GasGradedMultivector<T2>(
                scalarProcessor,
                gradeIndexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(
            IGaScalarProcessor<T2> scalarProcessor, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var gradeIndexScalarDictionary =
                GradeIndexScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.ToDictionary(
                        indexScalarPair => indexScalarPair.Key,
                        indexScalarPair => gradeIndexScalarMapping(
                            pair.Key, 
                            indexScalarPair.Key, 
                            indexScalarPair.Value
                        )
                    )
                );

            return new GasGradedMultivector<T2>(
                scalarProcessor,
                gradeIndexScalarDictionary,
                MaxBasisBladeId
            );
        }


        public override IGasMultivector<T> CopyToMultivectorStorage()
        {
            var gradeIndexScalarDictionary =
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => indexScalarDictionary.CopyToDictionary()
                );

            return new GasGradedMultivector<T>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGasGradedMultivector<T> GetGradedMultivectorCopy()
        {
            var gradeIndexScalarDictionary =
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => indexScalarDictionary.CopyToDictionary()
                );

            return new GasGradedMultivector<T>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }
        

        public override IGasMultivector<T> GetCopy()
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => indexScalarDictionary.CopyToDictionary()
                );

            return new GasGradedMultivector<T>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGasMultivector<T> GetCopy(Func<T, T> scalarMapping)
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => 
                        indexScalarDictionary.CopyToDictionary(scalarMapping)
                );

            return new GasGradedMultivector<T>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGasMultivector<T> GetNegative()
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => 
                        indexScalarDictionary.CopyToDictionary(ScalarProcessor.Negative)
                );

            return new GasGradedMultivector<T>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGasMultivector<T> GetNegative(Predicate<uint> gradeToNegativePredicate)
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => 
                        gradeToNegativePredicate(pair.Key) 
                            ? pair.Value.CopyToDictionary(ScalarProcessor.Negative) 
                            : pair.Value.CopyToDictionary()
                );

            return new GasGradedMultivector<T>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGasMultivector<T> GetReverse()
        {
            var gradeIndexScalarDictionary = GradeIndexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => 
                    pair.Key.GradeHasNegativeReverse() 
                        ? pair.Value.CopyToDictionary(ScalarProcessor.Negative) 
                        : pair.Value.CopyToDictionary()
            );

            return new GasGradedMultivector<T>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGasMultivector<T> GetGradeInvolution()
        {
            var gradeIndexScalarDictionary = GradeIndexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => 
                    pair.Key.GradeHasNegativeGradeInvolution() 
                        ? pair.Value.CopyToDictionary(ScalarProcessor.Negative) 
                        : pair.Value.CopyToDictionary()
            );

            return new GasGradedMultivector<T>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGasMultivector<T> GetCliffordConjugate()
        {
            var gradeIndexScalarDictionary = GradeIndexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => 
                    pair.Key.GradeHasNegativeCliffordConjugate() 
                        ? pair.Value.CopyToDictionary(ScalarProcessor.Negative) 
                        : pair.Value.CopyToDictionary()
            );

            return new GasGradedMultivector<T>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGasTermsMultivector<T> ToTermsMultivector()
        {
            var idScalarDictionary = GetIdScalarPairs().ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            if (idScalarDictionary.Count == 0)
                return ScalarProcessor.CreateZeroScalar();

            if (idScalarDictionary.Count == 1)
            {
                var (id, scalar) = idScalarDictionary.First();

                id.BasisBladeGradeIndex(out var grade, out var index);

                return ScalarProcessor.CreateKVector(
                    grade,
                    index,
                    scalar
                );
            }

            return ScalarProcessor.CreateTermsMultivector(idScalarDictionary
            );
        }

        public override IGasGradedMultivector<T> ToGradedMultivector()
        {
            return this;
        }

        public override IGasScalar<T> GetScalarPart()
        {
            if (!GradeIndexScalarDictionary.TryGetValue(0, out var indexScalarDictionary))
                return ScalarProcessor.CreateZeroScalar();

            return indexScalarDictionary.TryGetValue(0, out var scalar) 
                ? ScalarProcessor.CreateScalar(scalar) 
                : ScalarProcessor.CreateZeroScalar();
        }

        public override IGasScalar<T> GetScalarPart(Func<T, T> scalarMapping)
        {
            var scalar = GetTermScalar(0);
            
            return ScalarProcessor.CreateScalar(scalar);
        }

        public override IGasVector<T> GetVectorPart()
        {
            return 
                GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary) &&
                indexScalarDictionary.Count > 0
                    ? ScalarProcessor.CreateVector(indexScalarDictionary)
                    : ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<T, T> scalarMapping)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary) ||
                indexScalarDictionary.Count == 0) 
                return ScalarProcessor.CreateZeroVector();
            
            return ScalarProcessor.CreateVector(
                indexScalarDictionary.CopyToDictionary(scalarMapping)
            );

        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary) ||
                indexScalarDictionary.Count == 0)
                return ScalarProcessor.CreateZeroVector();

            return ScalarProcessor.CreateVector(indexScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => indexScalarMapping(pair.Key, pair.Value)
                )
            );
        }

        public override IGasVector<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary))
                return ScalarProcessor.CreateZeroVector();

            indexScalarDictionary = 
                indexScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .CopyToDictionary();
            
            return indexScalarDictionary.Count == 0
                ? ScalarProcessor.CreateZeroVector()
                : ScalarProcessor.CreateVector(indexScalarDictionary);
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary))
                return ScalarProcessor.CreateZeroVector();

            indexScalarDictionary = 
                indexScalarDictionary
                    .Where(pair => indexScalarSelection(pair.Key, pair.Value))
                    .CopyToDictionary();
            
            return indexScalarDictionary.Count == 0
                ? ScalarProcessor.CreateZeroVector()
                : ScalarProcessor.CreateVector(indexScalarDictionary);
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary))
                return ScalarProcessor.CreateZeroVector();

            indexScalarDictionary = 
                indexScalarDictionary
                    .Where(pair => indexSelection(pair.Key))
                    .CopyToDictionary();
            
            return indexScalarDictionary.Count == 0
                ? ScalarProcessor.CreateZeroVector()
                : ScalarProcessor.CreateVector(indexScalarDictionary);
        }

        public override IGasBivector<T> GetBivectorPart()
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return ScalarProcessor.CreateZeroBivector();

            return indexScalarDictionary.Count == 0
                ? ScalarProcessor.CreateZeroBivector()
                : ScalarProcessor.CreateBivector(indexScalarDictionary);
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, T> scalarMapping)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return ScalarProcessor.CreateZeroBivector();

            indexScalarDictionary = 
                indexScalarDictionary
                    .CopyToDictionary(scalarMapping);
            
            return ScalarProcessor.CreateBivector(indexScalarDictionary);
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return ScalarProcessor.CreateZeroBivector();

            indexScalarDictionary = 
                indexScalarDictionary
                    .ToDictionary(
                        pair => pair.Key, 
                        pair => indexScalarMapping(pair.Key, pair.Value)
                    );
            
            return ScalarProcessor.CreateBivector(indexScalarDictionary);
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return ScalarProcessor.CreateZeroBivector();

            indexScalarDictionary = 
                indexScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .CopyToDictionary();
            
            return indexScalarDictionary.Count == 0
                ? ScalarProcessor.CreateZeroBivector()
                : ScalarProcessor.CreateBivector(indexScalarDictionary);
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return ScalarProcessor.CreateZeroBivector();

            indexScalarDictionary = 
                indexScalarDictionary
                    .Where(pair => indexScalarSelection(pair.Key, pair.Value))
                    .CopyToDictionary();
            
            return indexScalarDictionary.Count == 0
                ? ScalarProcessor.CreateZeroBivector()
                : ScalarProcessor.CreateBivector(indexScalarDictionary);
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return ScalarProcessor.CreateZeroBivector();

            indexScalarDictionary = 
                indexScalarDictionary
                    .Where(pair => indexSelection(pair.Key))
                    .CopyToDictionary();
            
            return indexScalarDictionary.Count == 0
                ? ScalarProcessor.CreateZeroBivector()
                : ScalarProcessor.CreateBivector(indexScalarDictionary);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary) ||
                indexScalarDictionary.Count == 0)
                ScalarProcessor.CreateZeroKVector(grade);

            return ScalarProcessor.CreateKVector(grade, indexScalarDictionary);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, T> scalarMapping)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary) ||
                indexScalarDictionary.Count == 0)
                ScalarProcessor.CreateZeroKVector(grade);

            indexScalarDictionary = 
                indexScalarDictionary!.CopyToDictionary(scalarMapping);

            return ScalarProcessor.CreateKVector(grade, indexScalarDictionary);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, T> indexScalarMapping)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary) ||
                indexScalarDictionary.Count == 0)
                ScalarProcessor.CreateZeroKVector(grade);

            indexScalarDictionary = 
                indexScalarDictionary!.ToDictionary(
                    pair => pair.Key,
                    pair => indexScalarMapping(pair.Key, pair.Value)
                );

            return ScalarProcessor.CreateKVector(grade, indexScalarDictionary);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary) ||
                indexScalarDictionary.Count == 0)
                ScalarProcessor.CreateZeroKVector(grade);

            indexScalarDictionary = 
                indexScalarDictionary!
                    .Where(pair => scalarSelection(pair.Value))
                    .CopyToDictionary();

            return ScalarProcessor.CreateKVector(grade, indexScalarDictionary);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary) ||
                indexScalarDictionary.Count == 0)
                ScalarProcessor.CreateZeroKVector(grade);

            indexScalarDictionary = 
                indexScalarDictionary!
                    .Where(pair => indexScalarSelection(pair.Key, pair.Value))
                    .CopyToDictionary();

            return ScalarProcessor.CreateKVector(grade, indexScalarDictionary);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary) ||
                indexScalarDictionary.Count == 0)
                ScalarProcessor.CreateZeroKVector(grade);

            indexScalarDictionary = 
                indexScalarDictionary!
                    .Where(pair => indexSelection(pair.Key))
                    .CopyToDictionary();

            return ScalarProcessor.CreateKVector(grade, indexScalarDictionary);
        }
        
        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var idScalarDictionary = 
                GetIdScalarPairs()
                    .Where(pair => idSelection(pair.Key))
                    .CopyToDictionary();

            return ScalarProcessor.CreateTermsMultivector(idScalarDictionary);
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var gradeIndexScalarDictionary2 = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                var indexScalarDictionary2 = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    if (gradeIndexSelection(grade, index))
                        indexScalarDictionary2.Add(index, scalar);
                }

                if (indexScalarDictionary2.Count > 0)
                    gradeIndexScalarDictionary2.Add(grade, indexScalarDictionary2);
            }

            return ScalarProcessor.CreateGradedMultivector(gradeIndexScalarDictionary2);
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var gradeIndexScalarDictionary2 = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                var indexScalarDictionary2 = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    if (scalarSelection(scalar))
                        indexScalarDictionary2.Add(index, scalar);
                }

                if (indexScalarDictionary2.Count > 0)
                    gradeIndexScalarDictionary2.Add(grade, indexScalarDictionary2);
            }

            return ScalarProcessor.CreateGradedMultivector(gradeIndexScalarDictionary2);
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var gradeIndexScalarDictionary2 = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                var indexScalarDictionary2 = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    var id = GaBasisUtils.BasisBladeId(grade, index);

                    if (idScalarSelection(id, scalar))
                        indexScalarDictionary2.Add(index, scalar);
                }

                if (indexScalarDictionary2.Count > 0)
                    gradeIndexScalarDictionary2.Add(grade, indexScalarDictionary2);
            }

            return ScalarProcessor.CreateGradedMultivector(gradeIndexScalarDictionary2);
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var gradeIndexScalarDictionary2 = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                var indexScalarDictionary2 = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    if (gradeIndexScalarSelection(grade, index, scalar))
                        indexScalarDictionary2.Add(index, scalar);
                }

                if (indexScalarDictionary2.Count > 0)
                    gradeIndexScalarDictionary2.Add(grade, indexScalarDictionary2);
            }

            return ScalarProcessor.CreateGradedMultivector(gradeIndexScalarDictionary2);
        }

        public override Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            if (!TryGetKVectorStorageDictionary(1, out var indexScalarDictionary))
                return new Tuple<IGasVector<T>, IGasVector<T>>(
                    ScalarProcessor.CreateZeroVector(),
                    ScalarProcessor.CreateZeroVector()
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in indexScalarDictionary)
            {
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
            if (!TryGetKVectorStorageDictionary(1, out var indexScalarDictionary))
                return new Tuple<IGasVector<T>, IGasVector<T>>(
                    ScalarProcessor.CreateZeroVector(),
                    ScalarProcessor.CreateZeroVector()
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in indexScalarDictionary)
            {
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
            if (!TryGetKVectorStorageDictionary(1, out var indexScalarDictionary))
                return new Tuple<IGasVector<T>, IGasVector<T>>(
                    ScalarProcessor.CreateZeroVector(),
                    ScalarProcessor.CreateZeroVector()
                );

            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in indexScalarDictionary)
            {
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