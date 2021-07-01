using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib;
using DataStructuresLib.Extensions;
using GeometricAlgebraLib.Multivectors.Basis;
using GeometricAlgebraLib.Multivectors.Terms;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraLib.Storage.Trees;
using GaBasisUtils = GeometricAlgebraLib.Multivectors.Basis.GaBasisUtils;

namespace GeometricAlgebraLib.Storage
{
    public sealed class GaMultivectorGradedStorage<TScalar>
        : GaMultivectorStorageBase<TScalar>, IGaMultivectorGradedStorage<TScalar>
    {
        
        public static GaMultivectorGradedStorage<TScalar> CreateScalar(IGaScalarProcessor<TScalar> scalarProcessor, TScalar scalar)
        {
            var gradeIndexScalarDictionary = new Dictionary<int, Dictionary<ulong, TScalar>>()
            {
                {0, new Dictionary<ulong, TScalar>() {{0, scalar}}}
            };

            return new GaMultivectorGradedStorage<TScalar>(
                scalarProcessor, 
                gradeIndexScalarDictionary,
                0UL
            );
        }

        public static GaMultivectorGradedStorage<TScalar> CreateZero(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            var gradeIndexScalarDictionary = new Dictionary<int, Dictionary<ulong, TScalar>>();

            return new GaMultivectorGradedStorage<TScalar>(
                scalarProcessor, 
                gradeIndexScalarDictionary,
                0UL
            );
        }

        public static GaMultivectorGradedStorage<TScalar> CreateTerm(IGaScalarProcessor<TScalar> scalarProcessor, IGaBasisBlade basisBlade, TScalar scalar)
        {
            var gradeIndexScalarDictionary =
                new Dictionary<int, Dictionary<ulong, TScalar>>()
                {
                    {basisBlade.Grade, new Dictionary<ulong, TScalar>() {{basisBlade.Index, scalar}}}
                };

            return new GaMultivectorGradedStorage<TScalar>(
                scalarProcessor,
                gradeIndexScalarDictionary,
                basisBlade.Id
            );
        }

        public static GaMultivectorGradedStorage<TScalar> CreateVector(IGaScalarProcessor<TScalar> scalarProcessor, Dictionary<ulong, TScalar> indexScalarDictionary)
        {
            var gradeIndexScalarDictionary = 
                new Dictionary<int, Dictionary<ulong, TScalar>>()
                {
                    {1, indexScalarDictionary}
                };

            return new GaMultivectorGradedStorage<TScalar>(
                scalarProcessor,
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public static GaMultivectorGradedStorage<TScalar> CreateBivector(IGaScalarProcessor<TScalar> scalarProcessor, Dictionary<ulong, TScalar> indexScalarDictionary)
        {
            var gradeIndexScalarDictionary = 
                new Dictionary<int, Dictionary<ulong, TScalar>>()
                {
                    {2, indexScalarDictionary}
                };

            return new GaMultivectorGradedStorage<TScalar>(
                scalarProcessor,
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public static GaMultivectorGradedStorage<TScalar> CreateKVector(IGaScalarProcessor<TScalar> scalarProcessor, int grade, Dictionary<ulong, TScalar> indexScalarDictionary)
        {
            var gradeIndexScalarDictionary = 
                new Dictionary<int, Dictionary<ulong, TScalar>>()
                {
                    {grade, indexScalarDictionary}
                };

            return new GaMultivectorGradedStorage<TScalar>(
                scalarProcessor,
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public static GaMultivectorGradedStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, Dictionary<int, Dictionary<ulong, TScalar>> gradeIndexScalarDictionary)
        {
            return new(
                scalarProcessor,
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }


        public Dictionary<int, Dictionary<ulong, TScalar>> GradeIndexScalarDictionary { get; }


        public override int VSpaceDimension 
            => MaxBasisBladeId.LastOneBitPosition() + 1;

        public override int GradesCount 
            => GradeIndexScalarDictionary.Count;

        public override int TermsCount 
            => GradeIndexScalarDictionary.Sum(p => p.Value.Count);

        public override TScalar this[ulong id]
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

        public override TScalar this[int grade, ulong index]
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


        private GaMultivectorGradedStorage([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, [NotNull] Dictionary<int, Dictionary<ulong, TScalar>> gradeIndexScalarDictionary, ulong maxBasisBladeId)
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

        public override bool TryGetValue(ulong id, out TScalar value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (GradeIndexScalarDictionary.TryGetValue(grade, out var kVectorStorage))
                return kVectorStorage.TryGetValue(index, out value);

            value = ScalarProcessor.ZeroScalar;

            return false;
        }

        public override bool ContainsTermsOfGrade(int grade)
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
            return GradeIndexScalarDictionary.Keys;
        }


        public override bool ContainsTerm(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return GradeIndexScalarDictionary.TryGetValue(grade, out var storage) &&
                   storage.ContainsKey(index);
        }

        public override bool ContainsTerm(int grade, ulong index)
        {
            return GradeIndexScalarDictionary.TryGetValue(grade, out var storage) &&
                   storage.ContainsKey(index);
        }
        

        public override TScalar GetTermScalar(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var storage)) 
                return ScalarProcessor.ZeroScalar;

            return storage.TryGetValue(index, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        public override TScalar GetTermScalar(int grade, ulong index)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var storage)) 
                return ScalarProcessor.ZeroScalar;

            return storage.TryGetValue(index, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        
        public override bool TryGetTermScalar(ulong id, out TScalar value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (GradeIndexScalarDictionary.TryGetValue(grade, out var storage))
                return storage.TryGetValue(index, out value);

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public override bool TryGetTermScalar(int grade, ulong index, out TScalar value)
        {
            if (GradeIndexScalarDictionary.TryGetValue(grade, out var storage))
                return storage.TryGetValue(index, out value);

            value = ScalarProcessor.ZeroScalar;
            return false;
        }


        public override IEnumerable<IGaKVectorStorage<TScalar>> GetKVectorStorages()
        {
            return GradeIndexScalarDictionary
                .Select(d => GaKVectorStorage<TScalar>.Create(
                    ScalarProcessor, 
                    d.Key, 
                    d.Value
                )
            );
        }

        public override IReadOnlyDictionary<int, IGaKVectorStorage<TScalar>> GetKVectorStoragesDictionary()
        {
            return GradeIndexScalarDictionary
                .ToDictionary(
                    d => d.Key,
                    d => (IGaKVectorStorage<TScalar>)GaKVectorStorage<TScalar>.Create(
                        ScalarProcessor, 
                        d.Key, 
                        d.Value
                    )
                );
        }

        public override bool TryGetKVectorStorage(int grade, out IGaKVectorStorage<TScalar> storage)
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
                    ? GaScalarTermStorage<TScalar>.Create(ScalarProcessor, scalar)
                    : GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
                return true;
            }

            if (indexScalarDictionary.Count == 1)
            {
                var (index, scalar) = indexScalarDictionary.First();
                storage = GaKVectorTermStorage<TScalar>.Create(ScalarProcessor, grade, index, scalar);
                return true;
            }

            if (grade == 1)
            {
                storage = GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary);
                return true;
            }

            if (grade == 2)
            {
                storage = GaBivectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary);
                return true;
            }

            storage = GaKVectorStorage<TScalar>.Create(ScalarProcessor, grade, indexScalarDictionary);
            return true;
        }

        public override bool TryGetKVectorStorageDictionary(int grade, out IReadOnlyDictionary<ulong, TScalar> indexScalarDictionary)
        {
            if (GradeIndexScalarDictionary.TryGetValue(grade, out var dictionary))
            {
                indexScalarDictionary = dictionary;
                return true;
            }

            indexScalarDictionary = null;
            return false;
        }

        public override IReadOnlyDictionary<ulong, TScalar> GetIdScalarDictionary()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage.Value.Select(pair => 
                        new KeyValuePair<ulong, TScalar>(
                            GaBasisUtils.BasisBladeId(storage.Key, pair.Key), 
                            pair.Value
                        )
                    )
                ).ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );
        }

        public override IReadOnlyDictionary<int, Dictionary<ulong, TScalar>> GetGradeIndexScalarDictionary()
        {
            return GradeIndexScalarDictionary;
        }


        public GaMultivectorGradedStorage<TScalar> GetNegativeScalarsCopy()
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => 
                        indexScalarDictionary.CopyToDictionary(ScalarProcessor.Negative)
                );

            return new GaMultivectorGradedStorage<TScalar>(
                ScalarProcessor,
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public GaMultivectorGradedStorage<TScalar> GetLeftScaledScalarsCopy(TScalar value)
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => 
                        indexScalarDictionary.CopyToDictionary(s => 
                            ScalarProcessor.Times(value, s)
                        )
                );

            return new GaMultivectorGradedStorage<TScalar>(
                ScalarProcessor,
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public GaMultivectorGradedStorage<TScalar> GetRightScaledScalarsCopy(TScalar value)
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => 
                    indexScalarDictionary.CopyToDictionary(s => 
                        ScalarProcessor.Times(s, value)
                    )
                );

            return new GaMultivectorGradedStorage<TScalar>(
                ScalarProcessor,
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }


        public override GaTerm<TScalar> GetTerm(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return GaTerm<TScalar>.CreateGraded(
                grade,
                index,
                this[grade, index]
            );
        }

        public override GaTerm<TScalar> GetTerm(int grade, ulong index)
        {
            return GaTerm<TScalar>.CreateGraded(
                grade,
                index,
                this[grade, index]
            );
        }


        public bool TryGetValue(int grade, ulong index, out TScalar value)
        {
            if (GradeIndexScalarDictionary.TryGetValue(grade, out var kVectorStorage))
                return kVectorStorage.TryGetValue(index, out value);

            value = ScalarProcessor.ZeroScalar;

            return false;
        }

        
        public override bool TryGetTerm(ulong id, out GaTerm<TScalar> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (TryGetValue(grade, index, out var value))
            {
                term = GaTerm<TScalar>.CreateGraded(grade, index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(int grade, ulong index, out GaTerm<TScalar> term)
        {
            if (TryGetValue(grade, index, out var value))
            {
                term = GaTerm<TScalar>.CreateGraded(grade, index, value);
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

        public override IEnumerable<Tuple<int, ulong>> GetGradeIndexTuples()
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            foreach (var index in indexScalarDictionary.Keys)
                yield return new Tuple<int, ulong>(grade, index);
        }

        public override IEnumerable<IGaBasisBlade> GetBasisBlades()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage.Value.Select(pair => 
                        (IGaBasisBlade)new GaBasisGraded(storage.Key, pair.Key)
                    )
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetTerms()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage.Value.Select(pair => 
                        GaTerm<TScalar>.CreateGraded(storage.Key, pair.Key, pair.Value)
                    )
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetNotZeroTerms()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage
                        .Value
                        .Where(pair => !ScalarProcessor.IsZero(pair.Value))
                        .Select(pair => 
                            GaTerm<TScalar>.CreateGraded(storage.Key, pair.Key, pair.Value)
                        )
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetNotNearZeroTerms()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage
                        .Value
                        .Where(pair => !ScalarProcessor.IsNearZero(pair.Value))
                        .Select(pair => 
                            GaTerm<TScalar>.CreateGraded(storage.Key, pair.Key, pair.Value)
                        )
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetZeroTerms()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage
                        .Value
                        .Where(pair => ScalarProcessor.IsZero(pair.Value))
                        .Select(pair => 
                            GaTerm<TScalar>.CreateGraded(storage.Key, pair.Key, pair.Value)
                        )
                );
        }

        public override IEnumerable<GaTerm<TScalar>> GetNearZeroTerms()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage
                        .Value
                        .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                        .Select(pair => 
                            GaTerm<TScalar>.CreateGraded(storage.Key, pair.Key, pair.Value)
                        )
                );
        }

        public override IEnumerable<KeyValuePair<ulong, TScalar>> GetIdScalarPairs()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage.Value.Select(pair => 
                        new KeyValuePair<ulong, TScalar>(
                            GaBasisUtils.BasisBladeId(storage.Key, pair.Key), 
                            pair.Value
                        )
                    )
                );
        }

        public override IEnumerable<Tuple<ulong, TScalar>> GetIdScalarTuples()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage.Value.Select(pair => 
                        new Tuple<ulong, TScalar>(
                            GaBasisUtils.BasisBladeId(storage.Key, pair.Key), 
                            pair.Value
                        )
                    )
                );
        }

        public override IEnumerable<Tuple<int, ulong, TScalar>> GetGradeIndexScalarTuples()
        {
            return GradeIndexScalarDictionary
                .SelectMany(storage => 
                    storage.Value.Select(pair => 
                        new Tuple<int, ulong, TScalar>(storage.Key, pair.Key, pair.Value)
                    )
                );
        }

        public override IEnumerable<TScalar> GetScalars()
        {
            return GradeIndexScalarDictionary
                .Values
                .SelectMany(storage => storage.Values);
        }


        public override IGaMultivectorStorage<TScalar> GetCompactStorage()
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorGradedStorage<TScalar> GetCompactGradedStorage()
        {
            throw new NotImplementedException();
        }
        

        public override IGaGbtMultivectorStorageStack1<TScalar> CreateGbtStack(int treeDepth, int capacity)
        {
            //return GaGbtMultivectorStorageGradedStack1<T>.Create(
            //    capacity, 
            //    treeDepth,
            //    this
            //);
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
            if (treeDepth < VSpaceDimension)
                throw new InvalidOperationException();

            var dict = GetIdScalarPairs()
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );

            return new GaBinaryTree<TScalar>(treeDepth, dict);
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<TScalar, TScalar2> scalarMapping)
        {
            var gradeIndexScalarDictionary =
                GradeIndexScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.ToDictionary(
                        indexScalarPair => indexScalarPair.Key,
                        indexScalarPair => scalarMapping(indexScalarPair.Value)
                    )
                );

            return new GaMultivectorGradedStorage<TScalar2>(
                scalarProcessor,
                gradeIndexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<ulong, TScalar, TScalar2> idScalarMapping)
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

            return new GaMultivectorGradedStorage<TScalar2>(
                scalarProcessor,
                gradeIndexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<int, ulong, TScalar, TScalar2> gradeIndexScalarMapping)
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

            return new GaMultivectorGradedStorage<TScalar2>(
                scalarProcessor,
                gradeIndexScalarDictionary,
                MaxBasisBladeId
            );
        }


        public override IGaMultivectorStorage<TScalar> CopyToMultivectorStorage()
        {
            var gradeIndexScalarDictionary =
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => indexScalarDictionary.CopyToDictionary()
                );

            return new GaMultivectorGradedStorage<TScalar>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override GaMultivectorGradedStorage<TScalar> GetMultivectorGradedStorageCopy()
        {
            var gradeIndexScalarDictionary =
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => indexScalarDictionary.CopyToDictionary()
                );

            return new GaMultivectorGradedStorage<TScalar>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }
        

        public override IGaMultivectorStorage<TScalar> GetStorageCopy()
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => indexScalarDictionary.CopyToDictionary()
                );

            return new GaMultivectorGradedStorage<TScalar>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => 
                        indexScalarDictionary.CopyToDictionary(scalarMapping)
                );

            return new GaMultivectorGradedStorage<TScalar>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative()
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => 
                        indexScalarDictionary.CopyToDictionary(ScalarProcessor.Negative)
                );

            return new GaMultivectorGradedStorage<TScalar>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative(Predicate<int> gradeToNegativePredicate)
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => 
                        gradeToNegativePredicate(pair.Key) 
                            ? pair.Value.CopyToDictionary(ScalarProcessor.Negative) 
                            : pair.Value.CopyToDictionary()
                );

            return new GaMultivectorGradedStorage<TScalar>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaMultivectorStorage<TScalar> GetReverse()
        {
            var gradeIndexScalarDictionary = GradeIndexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => 
                    pair.Key.GradeHasNegativeReverse() 
                        ? pair.Value.CopyToDictionary(ScalarProcessor.Negative) 
                        : pair.Value.CopyToDictionary()
            );

            return new GaMultivectorGradedStorage<TScalar>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaMultivectorStorage<TScalar> GetGradeInvolution()
        {
            var gradeIndexScalarDictionary = GradeIndexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => 
                    pair.Key.GradeHasNegativeGradeInvolution() 
                        ? pair.Value.CopyToDictionary(ScalarProcessor.Negative) 
                        : pair.Value.CopyToDictionary()
            );

            return new GaMultivectorGradedStorage<TScalar>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaMultivectorStorage<TScalar> GetCliffordConjugate()
        {
            var gradeIndexScalarDictionary = GradeIndexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => 
                    pair.Key.GradeHasNegativeCliffordConjugate() 
                        ? pair.Value.CopyToDictionary(ScalarProcessor.Negative) 
                        : pair.Value.CopyToDictionary()
            );

            return new GaMultivectorGradedStorage<TScalar>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaMultivectorTermsStorage<TScalar> GetTermsStorage()
        {
            var idScalarDictionary = GetIdScalarPairs().ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            if (idScalarDictionary.Count == 0)
                return GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);

            if (idScalarDictionary.Count == 1)
            {
                var (id, scalar) = idScalarDictionary.First();

                id.BasisBladeGradeIndex(out var grade, out var index);

                return GaKVectorTermStorageBase<TScalar>.CreateKVector(
                    ScalarProcessor,
                    grade,
                    index,
                    scalar
                );
            }

            return GaMultivectorTermsStorage<TScalar>.Create(
                ScalarProcessor,
                idScalarDictionary
            );
        }

        public override IGaMultivectorGradedStorage<TScalar> GetGradedStorage()
        {
            return this;
        }

        public override IGaScalarStorage<TScalar> GetScalarPart()
        {
            if (!GradeIndexScalarDictionary.TryGetValue(0, out var indexScalarDictionary))
                return GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return indexScalarDictionary.TryGetValue(0, out var scalar) 
                ? GaScalarTermStorage<TScalar>.Create(ScalarProcessor, scalar) 
                : GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaScalarStorage<TScalar> GetScalarPart(Func<TScalar, TScalar> scalarMapping)
        {
            var scalar = GetTermScalar(0);
            
            return GaScalarTermStorage<TScalar>.Create(ScalarProcessor, scalar);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart()
        {
            return GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary) 
                ? GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary)
                : GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary))
                return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary.CopyToDictionary(scalarMapping)
            );
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary))
                return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => indexScalarMapping(pair.Key, pair.Value)
                )
            );
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, bool> scalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary))
                return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary))
                return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary
                    .Where(pair => indexScalarSelection(pair.Key, pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary))
                return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary
                    .Where(pair => indexSelection(pair.Key))
                    .CopyToDictionary()
            );
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart()
        {
            return GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary) 
                ? GaBivectorStorage<TScalar>.Create(ScalarProcessor, indexScalarDictionary)
                : GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => scalarMapping(pair.Value)
                )
            );
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => indexScalarMapping(pair.Key, pair.Value)
                )
            );
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, bool> scalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary
                    .Where(pair => scalarSelection(pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary
                    .Where(pair => indexScalarSelection(pair.Key, pair.Value))
                    .CopyToDictionary()
            );
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary
                    .Where(pair => indexSelection(pair.Key))
                    .CopyToDictionary()
            );
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade)
        {
            return grade switch
            {
                0 => GetScalarPart(),
                1 => GetVectorPart(),
                2 => GetBivectorPart(),
                _ => GradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary)
                    ? GaKVectorStorage<TScalar>.Create(ScalarProcessor, grade, indexScalarDictionary)
                    : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade)
            };
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, TScalar> scalarMapping)
        {
            return grade switch
            {
                0 => GetScalarPart(scalarMapping),
                1 => GetVectorPart(scalarMapping),
                2 => GetBivectorPart(scalarMapping),
                _ => GradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary)
                    ? GaBivectorStorage<TScalar>.Create(
                        ScalarProcessor, 
                        indexScalarDictionary.CopyToDictionary(scalarMapping)
                    )
                    : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade)
            };
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            throw new NotImplementedException();
        }

        public override Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<TScalar, bool> scalarSelection)
        {
            throw new NotImplementedException();
        }
    }
}