using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
    public sealed record GaStorageMultivectorGraded<T>
        : GaStorageMultivectorBase<T>, IGaStorageMultivectorGraded<T>
    {
        public static GaStorageMultivectorGraded<T> ZeroMultivector { get; }
            = new GaStorageMultivectorGraded<T>(
                GaGradedDictionaryEmpty<T>.DefaultDictionary,
                0UL
            );


        public static GaStorageMultivectorGraded<T> Create(IGaGradedDictionary<T> gradeIndexScalarDictionary)
        {
            return new GaStorageMultivectorGraded<T>(
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        internal static GaStorageMultivectorGraded<T> Create(IGaGradedDictionary<T> gradeIndexScalarDictionary, ulong maxBasisBladeId)
        {
            return new GaStorageMultivectorGraded<T>(
                gradeIndexScalarDictionary,
                maxBasisBladeId
            );
        }


        public IGaGradedDictionary<T> GradeIndexScalarDictionary { get; }

        public override uint VSpaceDimension 
            => (uint) MaxBasisBladeId.LastOneBitPosition() + 1;

        public override int GradesCount 
            => GradeIndexScalarDictionary.Count;

        public override int TermsCount 
            => GradeIndexScalarDictionary.Sum(p => p.Value.Count);
        
        public override bool IsUniform => false;

        public override bool IsGraded => true;


        private GaStorageMultivectorGraded([NotNull] IGaGradedDictionary<T> gradeIndexScalarDictionary, ulong maxBasisBladeId)
            : base(maxBasisBladeId)
        {
            GradeIndexScalarDictionary = gradeIndexScalarDictionary;
        }

        
        public GaStorageMultivectorGraded<T> GetStorageCopy()
        {
            var gradeIndexScalarDictionary = 
                GradeIndexScalarDictionary.GetCopy();

            return new GaStorageMultivectorGraded<T>(
                gradeIndexScalarDictionary,
                MaxBasisBladeId
            );
        }
        
        public GaStorageMultivectorGraded<T2> MapScalars<T2>(Func<T, T2> scalarMapping)
        {
            var gradeIndexScalarDictionary =
                GradeIndexScalarDictionary.MapValues(scalarMapping);

            return new GaStorageMultivectorGraded<T2>(
                gradeIndexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageMultivectorGraded<T2> MapScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var gradeIndexScalarDictionary =
                GradeIndexScalarDictionary.MapValues(
                    (grade, index, scalar) => 
                        idScalarMapping(GaBasisUtils.BasisBladeId(grade, index), scalar)
                );

            return new GaStorageMultivectorGraded<T2>(
                gradeIndexScalarDictionary,
                MaxBasisBladeId
            );
        }
        
        public GaStorageMultivectorGraded<T2> MapScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var gradeIndexScalarDictionary =
                GradeIndexScalarDictionary.MapValues(
                    (_, index, scalar) => 
                        indexScalarMapping(index, scalar)
                );

            return new GaStorageMultivectorGraded<T2>(
                gradeIndexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageMultivectorGraded<T2> MapScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var gradeIndexScalarDictionary =
                GradeIndexScalarDictionary.MapValues(gradeIndexScalarMapping);

            return new GaStorageMultivectorGraded<T2>(
                gradeIndexScalarDictionary,
                MaxBasisBladeId
            );
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

            value = default;
            return false;
        }

        public override bool ContainsVectorPart()
        {
            return GradeIndexScalarDictionary.ContainsKey(1);
        }

        public override bool ContainsBivectorPart()
        {
            return GradeIndexScalarDictionary.ContainsKey(2);
        }

        public override bool ContainsKVectorPart(uint grade)
        {
            return GradeIndexScalarDictionary.ContainsKey(grade);
        }


        public override bool IsEmpty()
        {
            return GradeIndexScalarDictionary.IsEmpty();
        }

        public override bool IsScalar()
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                if (grade == 0)
                    continue;

                if (!indexScalarDictionary.IsEmpty())
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

                if (!indexScalarDictionary.IsEmpty())
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

                if (!indexScalarDictionary.IsEmpty())
                    return false;
            }

            return true;
        }

        public override bool IsKVector()
        {
            return GradeIndexScalarDictionary.Count(pair => !pair.Value.IsEmpty()) < 2;
        }

        public override bool IsKVector(uint grade)
        {
            foreach (var (g, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                if (g == grade)
                    continue;

                if (!indexScalarDictionary.IsEmpty())
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

        public override bool ContainsScalarPart()
        {
            return GradeIndexScalarDictionary.ContainsKey(0);
        }


        public override bool TryGetTermScalar(ulong id, out T value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (GradeIndexScalarDictionary.TryGetValue(grade, out var storage))
                return storage.TryGetValue(index, out value);

            value = default;
            return false;
        }

        public override bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            if (GradeIndexScalarDictionary.TryGetValue(grade, out var storage))
                return storage.TryGetValue(index, out value);

            value = default;
            return false;
        }


        public override bool TryGetKVectorPart(uint grade, out IGaStorageKVector<T> kVector)
        {
            if (GradeIndexScalarDictionary.TryGetValue(grade, out var evenDictionary))
            {
                kVector = GaStorageKVector<T>.Create(grade, evenDictionary);
                return true;
            }

            kVector = null;
            return false;
        }

        public override IEnumerable<IGaStorageKVector<T>> GetKVectorStorages()
        {
            return GradeIndexScalarDictionary
                .Select(
                    pair => GaStorageKVector<T>.Create(pair.Key, pair.Value)
                );
        }

        public override IReadOnlyDictionary<uint, IGaStorageKVector<T>> GetKVectorStoragesDictionary()
        {
            return GradeIndexScalarDictionary
                .ToDictionary(
                    pair => pair.Key,
                    pair => GaStorageKVector<T>.Create(pair.Key, pair.Value)
                );
        }

        public override bool TryGetVectorPartDictionary(out IGaEvenDictionary<T> indexScalarDictionary)
        {
            if (GradeIndexScalarDictionary.TryGetValue(1, out indexScalarDictionary) && !indexScalarDictionary.IsEmpty())
                return true;

            indexScalarDictionary = GaEvenDictionaryEmpty<T>.DefaultDictionary;
            return false;
        }

        public override bool TryGetBivectorPartDictionary(out IGaEvenDictionary<T> indexScalarDictionary)
        {
            if (GradeIndexScalarDictionary.TryGetValue(2, out indexScalarDictionary) && !indexScalarDictionary.IsEmpty())
                return true;

            indexScalarDictionary = GaEvenDictionaryEmpty<T>.DefaultDictionary;
            return false;

        }

        public override bool TryGetKVectorPartDictionary(uint grade, out IGaEvenDictionary<T> indexScalarDictionary)
        {
            if (GradeIndexScalarDictionary.TryGetValue(grade, out indexScalarDictionary) && !indexScalarDictionary.IsEmpty())
                return true;

            indexScalarDictionary = GaEvenDictionaryEmpty<T>.DefaultDictionary;
            return false;

        }

        public override IGaEvenDictionary<T> GetIdScalarDictionary()
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
                ).CreateEvenDictionary();
        }

        public override IGaGradedDictionary<T> GetGradeIndexScalarDictionary()
        {
            return GradeIndexScalarDictionary;
        }


        public bool TryGetValue(uint grade, ulong index, out T value)
        {
            if (GradeIndexScalarDictionary.TryGetValue(grade, out var kVectorStorage))
                return kVectorStorage.TryGetValue(index, out value);

            value = default;
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

        public override IGaEvenDictionary<T> GetScalarPartDictionary()
        {
            return 
                GradeIndexScalarDictionary.TryGetValue(0, out var evenDictionary) && 
                evenDictionary.TryGetValue(0, out var scalar)
                    ? scalar.CreateEvenDictionarySingleZeroKey()
                    : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public override IGaEvenDictionary<T> GetVectorPartDictionary()
        {
            return 
                GradeIndexScalarDictionary.TryGetValue(1, out var evenDictionary) && 
                !evenDictionary.IsEmpty()
                    ? evenDictionary
                    : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public override IGaEvenDictionary<T> GetBivectorPartDictionary()
        {
            return 
                GradeIndexScalarDictionary.TryGetValue(2, out var evenDictionary) && 
                !evenDictionary.IsEmpty()
                    ? evenDictionary
                    : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public override IGaEvenDictionary<T> GetKVectorPartDictionary(uint grade)
        {
            return 
                GradeIndexScalarDictionary.TryGetValue(grade, out var evenDictionary) && 
                !evenDictionary.IsEmpty()
                    ? evenDictionary
                    : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public override bool TryGetScalarPart(out IGaStorageScalar<T> scalar)
        {
            if (GradeIndexScalarDictionary.TryGetValue(0, out var evenDictionary) && evenDictionary.TryGetValue(0, out var s))
            {
                scalar = GaStorageScalar<T>.Create(s);
                return true;
            }

            scalar = null;
            return false;
        }

        public override bool TryGetVectorPart(out IGaStorageVector<T> vector)
        {
            if (GradeIndexScalarDictionary.TryGetValue(1, out var evenDictionary))
            {
                vector = GaStorageVector<T>.Create(evenDictionary);
                return true;
            }

            vector = null;
            return false;
        }

        public override bool TryGetBivectorPart(out IGaStorageBivector<T> bivector)
        {
            if (GradeIndexScalarDictionary.TryGetValue(2, out var evenDictionary))
            {
                bivector = GaStorageBivector<T>.Create(evenDictionary);
                return true;
            }

            bivector = null;
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

        public override IEnumerable<GaTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    var id = GaBasisUtils.BasisBladeId(grade, index);

                    if (idSelection(id))
                        yield return GaTerm<T>.CreateGraded(grade, index, scalar);
                }
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    if (gradeIndexSelection(grade, index))
                        yield return GaTerm<T>.CreateGraded(grade, index, scalar);
                }
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    if (scalarSelection(scalar))
                        yield return GaTerm<T>.CreateGraded(grade, index, scalar);
                }
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    var id = GaBasisUtils.BasisBladeId(grade, index);

                    if (idScalarSelection(id, scalar))
                        yield return GaTerm<T>.CreateGraded(grade, index, scalar);
                }
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    if (gradeIndexScalarSelection(grade, index, scalar))
                        yield return GaTerm<T>.CreateGraded(grade, index, scalar);
                }
            }
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


        public override IGaStorageMultivector<T> GetCompactStorage()
        {
            return GetCompactGradedStorage();
        }

        public override IGaStorageMultivectorGraded<T> GetCompactGradedStorage()
        {
            return this;
        }
        

        public override IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity, IGaScalarProcessor<T> scalarProcessor)
        {
            //return GaGbtMultivectorStorageGradedStack1<T>.Create(
            //    capacity, 
            //    treeDepth,
            //    this
            //);
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
            if (treeDepth < VSpaceDimension)
                throw new InvalidOperationException();

            var dict = GetIdScalarPairs()
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );

            return new GaEvenDictionaryTree<T>(treeDepth, dict);
        }



        public override IGaStorageMultivector<T> CopyToMultivectorStorage()
        {
            var gradeIndexScalarDictionary =
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => indexScalarDictionary.CopyToDictionary()
                ).CreateGradedDictionary();

            return new GaStorageMultivectorGraded<T>(
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaStorageMultivectorGraded<T> GetGradedMultivectorCopy()
        {
            var gradeIndexScalarDictionary =
                GradeIndexScalarDictionary.CopyToDictionary(
                    indexScalarDictionary => indexScalarDictionary.CopyToDictionary()
                ).CreateGradedDictionary();

            return new GaStorageMultivectorGraded<T>(
                gradeIndexScalarDictionary,
                gradeIndexScalarDictionary.GetMaxBasisBladeId()
            );
        }
        

        
        public override IGaStorageMultivectorSparse<T> ToSparseMultivector()
        {
            var idScalarDictionary = 
                GetIdScalarPairs()
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value
                    ).CreateEvenDictionary();

            return GaStorageMultivectorSparse<T>.Create(
                idScalarDictionary, 
                MaxBasisBladeId
            );
        }

        public override IGaStorageMultivectorGraded<T> ToGradedMultivector()
        {
            return this;
        }

        public override IGaStorageVector<T> GetVectorPart()
        {
            if (!GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary))
                return GaStorageVector<T>.ZeroVector;
            
            return indexScalarDictionary.Count == 0
                ? GaStorageVector<T>.ZeroVector
                : GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary))
                return GaStorageVector<T>.ZeroVector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByValue(scalarSelection);
            
            return indexScalarDictionary.Count == 0
                ? GaStorageVector<T>.ZeroVector
                : GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary))
                return GaStorageVector<T>.ZeroVector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByKeyValue(indexScalarSelection);
            
            return indexScalarDictionary.Count == 0
                ? GaStorageVector<T>.ZeroVector
                : GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(1, out var indexScalarDictionary))
                return GaStorageVector<T>.ZeroVector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByKey(indexSelection);
            
            return indexScalarDictionary.Count == 0
                ? GaStorageVector<T>.ZeroVector
                : GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart()
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return GaStorageBivector<T>.ZeroBivector;
            
            return indexScalarDictionary.Count == 0
                ? GaStorageBivector<T>.ZeroBivector
                : GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return GaStorageBivector<T>.ZeroBivector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByValue(scalarSelection);
            
            return indexScalarDictionary.Count == 0
                ? GaStorageBivector<T>.ZeroBivector
                : GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return GaStorageBivector<T>.ZeroBivector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByKeyValue(indexScalarSelection);
            
            return indexScalarDictionary.Count == 0
                ? GaStorageBivector<T>.ZeroBivector
                : GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(2, out var indexScalarDictionary))
                return GaStorageBivector<T>.ZeroBivector;

            indexScalarDictionary = 
                indexScalarDictionary.FilterByKey(indexSelection);
            
            return indexScalarDictionary.Count == 0
                ? GaStorageBivector<T>.ZeroBivector
                : GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
                return GaStorageKVector<T>.ZeroKVector(grade);
            
            return indexScalarDictionary.Count == 0
                ? GaStorageKVector<T>.ZeroKVector(grade)
                : GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
                return GaStorageKVector<T>.ZeroKVector(grade);

            indexScalarDictionary = 
                indexScalarDictionary.FilterByValue(scalarSelection);
            
            return indexScalarDictionary.Count == 0
                ? GaStorageKVector<T>.ZeroKVector(grade)
                : GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
                return GaStorageKVector<T>.ZeroKVector(grade);

            indexScalarDictionary = 
                indexScalarDictionary.FilterByKeyValue(indexScalarSelection);
            
            return indexScalarDictionary.Count == 0
                ? GaStorageKVector<T>.ZeroKVector(grade)
                : GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            if (!GradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
                return GaStorageKVector<T>.ZeroKVector(grade);

            indexScalarDictionary = 
                indexScalarDictionary.FilterByKey(indexSelection);
            
            return indexScalarDictionary.Count == 0
                ? GaStorageKVector<T>.ZeroKVector(grade)
                : GaStorageKVector<T>.Create(grade, indexScalarDictionary);
        }
        
        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                var indexScalarDictionary2 = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    var id = GaBasisUtils.BasisBladeId(grade, index);

                    if (idSelection(id))
                        indexScalarDictionary2.Add(index, scalar);
                }

                if (indexScalarDictionary2.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary2);
            }

            var gradedDictionary =
                gradeIndexScalarDictionary.CreateGradedDictionary();

            return new GaStorageMultivectorGraded<T>(
                gradedDictionary,
                gradedDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                var indexScalarDictionary2 = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    if (gradeIndexSelection(grade, index))
                        indexScalarDictionary2.Add(index, scalar);
                }

                if (indexScalarDictionary2.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary2);
            }

            var gradedDictionary =
                gradeIndexScalarDictionary.CreateGradedDictionary();

            return new GaStorageMultivectorGraded<T>(
                gradedDictionary,
                gradedDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                var indexScalarDictionary2 = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    if (scalarSelection(scalar))
                        indexScalarDictionary2.Add(index, scalar);
                }

                if (indexScalarDictionary2.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary2);
            }

            var gradedDictionary =
                gradeIndexScalarDictionary.CreateGradedDictionary();

            return new GaStorageMultivectorGraded<T>(
                gradedDictionary,
                gradedDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

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
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary2);
            }

            var gradedDictionary =
                gradeIndexScalarDictionary.CreateGradedDictionary();

            return new GaStorageMultivectorGraded<T>(
                gradedDictionary,
                gradedDictionary.GetMaxBasisBladeId()
            );
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, indexScalarDictionary) in GradeIndexScalarDictionary)
            {
                var indexScalarDictionary2 = new Dictionary<ulong, T>();

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    if (gradeIndexScalarSelection(grade, index, scalar))
                        indexScalarDictionary2.Add(index, scalar);
                }

                if (indexScalarDictionary2.Count > 0)
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary2);
            }

            var gradedDictionary =
                gradeIndexScalarDictionary.CreateGradedDictionary();

            return new GaStorageMultivectorGraded<T>(
                gradedDictionary,
                gradedDictionary.GetMaxBasisBladeId()
            );
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            if (!TryGetVectorPartDictionary(out var indexScalarDictionary))
                return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                    GaStorageVector<T>.ZeroVector,
                    GaStorageVector<T>.ZeroVector
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

            return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                indexScalarDictionary1.CreateStorageVector(),
                indexScalarDictionary2.CreateStorageVector()
            );
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!TryGetVectorPartDictionary(out var indexScalarDictionary))
                return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                    GaStorageVector<T>.ZeroVector,
                    GaStorageVector<T>.ZeroVector
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

            return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                indexScalarDictionary1.CreateStorageVector(),
                indexScalarDictionary2.CreateStorageVector()
            );
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            if (!TryGetVectorPartDictionary(out var indexScalarDictionary))
                return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                    GaStorageVector<T>.ZeroVector,
                    GaStorageVector<T>.ZeroVector
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

            return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                indexScalarDictionary1.CreateStorageVector(),
                indexScalarDictionary2.CreateStorageVector()
            );
        }
    }
}