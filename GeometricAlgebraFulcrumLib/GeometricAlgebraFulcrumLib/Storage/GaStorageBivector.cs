using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Combinations;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Storage
{
    /// <summary>
    /// Can store the scalar coefficients of a vector of any dimension.
    /// The scalars are assumed to be of immutable type such as T, complex, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed record GaStorageBivector<T> 
        : GaStorageKVectorBase<T>, IGaStorageBivector<T>
    {
        public static GaStorageBivector<T> ZeroBivector { get; }
            = new GaStorageBivector<T>(
                GaEvenDictionaryEmpty<T>.DefaultDictionary, 
                0UL
            );


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create(ulong index, T scalar)
        {
            var evenDictionary = 
                scalar.CreateEvenDictionarySingleKey(index);

            return new GaStorageBivector<T>(
                evenDictionary, 
                GaBasisUtils.BasisBladeId(2, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create(params T[] indexScalarList)
        {
            var evenDictionary = 
                indexScalarList.CreateEvenDictionaryList();

            return new GaStorageBivector<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId(2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create(IReadOnlyList<T> indexScalarList)
        {
            var evenDictionary = 
                indexScalarList.CreateEvenDictionaryList();

            return new GaStorageBivector<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId(2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create(IEnumerable<T> indexScalarList)
        {
            var evenDictionary = 
                indexScalarList.CreateEvenDictionaryList();

            return new GaStorageBivector<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId(2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create(Dictionary<ulong, T> indexScalarDictionary)
        {
            var evenDictionary = 
                indexScalarDictionary.CreateEvenDictionary();

            return new GaStorageBivector<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId(2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create(IGaEvenDictionary<T> indexScalarDictionary)
        {
            return new GaStorageBivector<T>(
                indexScalarDictionary, 
                indexScalarDictionary.GetMaxBasisBladeId(2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GaStorageBivector<T> Create(IGaEvenDictionary<T> indexScalarDictionary, ulong maxBasisBladeId)
        {
            return new GaStorageBivector<T>(
                indexScalarDictionary, 
                maxBasisBladeId
            );
        }


        public override uint Grade 
            => 2;


        private GaStorageBivector([NotNull] IGaEvenDictionary<T> indexScalarDictionary, ulong maxBasisBladeId)
            : base(indexScalarDictionary, maxBasisBladeId)
        {
        }
        

        public bool ContainsTerm(int basisVectorIndex1, int basisVectorIndex2)
        {
            var index = basisVectorIndex1 > basisVectorIndex2
                ? BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1)
                : BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

            return IndexScalarDictionary.ContainsKey(index);
        }

        public bool ContainsTerm(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            var index = basisVectorIndex1 > basisVectorIndex2
                ? BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1)
                : BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

            return IndexScalarDictionary.ContainsKey(index);
        }


        public bool TryGetTermScalar(ulong index1, ulong index2, out T scalar)
        {
            if (index1 >= index2)
                throw new InvalidOperationException();
               
            var index = 
                BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2);

            if (IndexScalarDictionary.TryGetValue(index, out scalar))
                return true;

            scalar = default;
            return false;
        }


        public override bool TryGetTermByIndex(int index, out GaTerm<T> term)
        {
            var i = (ulong) index;

            if (IndexScalarDictionary.TryGetValue(i, out var value))
            {
                term = GaTerm<T>.CreateBivector(i, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaTerm<T> term)
        {
            if (IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateBivector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaTerm<T> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (Grade == grade && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateBivector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term)
        {
            if (Grade == grade && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateBivector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public bool TryGetTerm(ulong index1, ulong index2, out GaTerm<T> term)
        {
            if (index1 >= index2)
                throw new InvalidOperationException();

            var index = 
                BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2);

            if (IndexScalarDictionary.TryGetValue(index, out var scalar))
            {
                term = GaTerm<T>.CreateBivector(
                    index1,
                    index2,
                    scalar
                );

                return true;
            }

            term = null;
            return false;
        }


        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices()
        {
            return IndexScalarDictionary
                .Keys
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices(Func<ulong, bool> filterFunc)
        {
            return IndexScalarDictionary
                .Keys
                .Where(filterFunc)
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices(Func<uint, ulong, bool> filterFunc)
        {
            return IndexScalarDictionary
                .Keys
                .Where(index => filterFunc(Grade, index))
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices(Func<ulong, T, bool> filterFunc)
        {
            return IndexScalarDictionary
                .Where(pair => filterFunc(pair.Key, pair.Value))
                .Select(pair => pair.Key)
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices(Func<uint, ulong, T, bool> filterFunc)
        {
            return IndexScalarDictionary
                .Where(pair => filterFunc(Grade, pair.Key, pair.Value))
                .Select(pair => pair.Key)
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }

        public IEnumerable<Tuple<ulong, ulong>> GetTermsBasisVectorsIndices(Func<T, bool> filterFunc)
        {
            return IndexScalarDictionary
                .Where(pair => filterFunc(pair.Value))
                .Select(pair => pair.Key)
                .Select(BinaryCombinationsUtilsUInt64.IndexToCombinadic);
        }


        public override IGaStorageKVector<T> GetComputedCopy(Func<T, T> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(mappingFunc);

            return new GaStorageBivector<T>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaStorageKVector<T> GetComputedCopy(Func<ulong, T, T> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(mappingFunc);

            return new GaStorageBivector<T>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaStorageKVector<T> GetComputedCopy(Func<uint, ulong, T, T> mappingFunc)
        {
            var indexScalarDictionary = IndexScalarDictionary.ToDictionary(
                pair => pair.Key, 
                pair => mappingFunc(Grade, pair.Key, pair.Value)
            ).CreateEvenDictionary();

            return GaStorageBivector<T>.Create(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }


        public GaStorageBivector<T> GetStorageCopy()
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.GetCopy();

            return new GaStorageBivector<T>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageBivector<T2> MapScalars<T2>(Func<T, T2> scalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(scalarMapping);

            return new GaStorageBivector<T2>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageBivector<T2> MapScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(
                    (index, scalar) => 
                        idScalarMapping(GaBasisUtils.BasisBladeId(2, index), scalar)
                );

            return new GaStorageBivector<T2>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageBivector<T2> MapScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(indexScalarMapping);

            return new GaStorageBivector<T2>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageBivector<T2> MapScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(
                    (index, scalar) => 
                        gradeIndexScalarMapping(2U, index, scalar)
                );

            return new GaStorageBivector<T2>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }


        public override IGaStorageVector<T> GetVectorPart()
        {
            return GaStorageVector<T>.ZeroVector;
        }

        public override IGaStorageVector<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            return GaStorageVector<T>.ZeroVector;
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return GaStorageVector<T>.ZeroVector;
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return GaStorageVector<T>.ZeroVector;
        }

        public override IGaStorageBivector<T> GetBivectorPart()
        {
            return IsEmpty()
                ? GaStorageBivector<T>.ZeroBivector
                : this;
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.Where(
                    pair => scalarSelection(pair.Value)
                ).CopyToDictionary();

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.Where(
                    pair => indexScalarSelection(pair.Key, pair.Value)
                ).CopyToDictionary();

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.Where(
                    pair => indexSelection(pair.Key)
                ).CopyToDictionary();

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade)
        {
            return grade == 2 && !IsEmpty()
                ? this 
                : GaStorageKVector<T>.ZeroKVector(grade);
        }


        public GaStorageBivector<T> GetPart(Func<T, bool> scalarFilter)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.FilterByValue(scalarFilter);

            return new GaStorageBivector<T>(
                indexScalarDictionary,
                indexScalarDictionary.GetMaxBasisBladeId(2)
            );
        }

        public GaStorageBivector<T> GetPart(Func<ulong, T, bool> indexScalarFilter)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.FilterByKeyValue(indexScalarFilter);

            return new GaStorageBivector<T>(
                indexScalarDictionary,
                indexScalarDictionary.GetMaxBasisBladeId(2)
            );
        }

        public GaStorageBivector<T> GetPart(Func<ulong, bool> indexFilter)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.FilterByKey(indexFilter);

            return new GaStorageBivector<T>(
                indexScalarDictionary,
                indexScalarDictionary.GetMaxBasisBladeId(2)
            );
        }


        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            return grade == 2
                ? GetBivectorPart(scalarSelection)
                : GaStorageKVector<T>.ZeroKVector(grade);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            return grade == 2
                ? GetBivectorPart(indexScalarSelection)
                : GaStorageKVector<T>.ZeroKVector(grade);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            return grade == 2
                ? GetBivectorPart(indexSelection)
                : GaStorageKVector<T>.ZeroKVector(grade);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.Where(
                    pair => idSelection(GaBasisUtils.BasisBladeId(2, pair.Key))
                ).CopyToDictionary();

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.Where(
                    pair => gradeIndexSelection(2, pair.Key)
                ).CopyToDictionary();

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.Where(
                    pair => scalarSelection(pair.Value)
                ).CopyToDictionary();

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.Where(
                    pair => idScalarSelection(GaBasisUtils.BasisBladeId(2, pair.Key), pair.Value)
                ).CopyToDictionary();

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.Where(
                    pair => gradeIndexScalarSelection(2, pair.Key, pair.Value)
                ).CopyToDictionary();

            return GaStorageBivector<T>.Create(indexScalarDictionary);
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                GaStorageVector<T>.ZeroVector,
                GaStorageVector<T>.ZeroVector
            );
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                GaStorageVector<T>.ZeroVector,
                GaStorageVector<T>.ZeroVector
            );
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            return new Tuple<IGaStorageVector<T>, IGaStorageVector<T>>(
                GaStorageVector<T>.ZeroVector,
                GaStorageVector<T>.ZeroVector
            );
        }

        public override IEnumerable<IGaBasisBlade> GetBasisBlades()
        {
            return IndexScalarDictionary.Select(pair => 
                (IGaBasisBlade)pair.Key.CreateBasisBivector()
            );
        }
        

        public override IEnumerable<GaTerm<T>> GetTerms()
        {
            return IndexScalarDictionary.Select(pair => 
                GaTerm<T>.CreateBivector(pair.Key, pair.Value)
            );
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                var id = GaBasisUtils.BasisBladeId(2, index);

                if (idSelection(id))
                    yield return GaTerm<T>.CreateBivector(index, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (gradeIndexSelection(2, index))
                    yield return GaTerm<T>.CreateBivector(index, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (scalarSelection(scalar))
                    yield return GaTerm<T>.CreateBivector(index, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                var id = GaBasisUtils.BasisBladeId(2, index);

                if (idScalarSelection(id, scalar))
                    yield return GaTerm<T>.CreateBivector(index, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (gradeIndexScalarSelection(2, index, scalar))
                    yield return GaTerm<T>.CreateBivector(index, scalar);
            }
        }

        public IGaStorageBivector<T> GetBivectorStorage()
        {
            return this;
        }

        public IGaStorageBivector<T> GetBivectorStorageCopy()
        {
            return new GaStorageBivector<T>(
                IndexScalarDictionary.GetCopy(),
                MaxBasisBladeId
            );
        }

        public IGaStorageBivector<T> GetBivectorStorageCopy(Func<T, T> scalarMapping)
        {
            return new GaStorageBivector<T>(
                IndexScalarDictionary.MapValues(scalarMapping),
                MaxBasisBladeId
            );
        }

        public IEnumerable<Tuple<ulong, ulong, T>> GetBasisVectorsIndexScalarTuples()
        {
            return IndexScalarDictionary.Select(
                pair =>
                {
                    var (index, scalar) = pair;
                    var (index1, index2) = 
                        BinaryCombinationsUtilsUInt64.IndexToCombinadic(index);

                    return new Tuple<ulong, ulong, T>(index1, index2, scalar);
                }
            );
        }
    }
}