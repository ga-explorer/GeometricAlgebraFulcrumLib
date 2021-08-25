using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Structures;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
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
            = new GaStorageBivector<T>();

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create([NotNull] T scalar)
        {
            return new GaStorageBivector<T>(
                new GaListGradedSingleGradeKey<T>(2, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create(ulong index, T scalar)
        {
            return new GaStorageBivector<T>(
                new GaListGradedSingleGradeKey<T>(2, index, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create(params T[] indexScalarList)
        {
            return indexScalarList.Length switch
            {
                0 => ZeroBivector,
                1 => new GaStorageBivector<T>(new GaListGradedSingleGradeKey<T>(2, indexScalarList[0])),
                _ => new GaStorageBivector<T>(new GaListGradedSingleGrade<T>(2, indexScalarList.CreateEvenListDense()))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create(IReadOnlyList<T> indexScalarList)
        {
            return indexScalarList.Count switch
            {
                0 => ZeroBivector,
                1 => new GaStorageBivector<T>(new GaListGradedSingleGradeKey<T>(2, indexScalarList[0])),
                _ => new GaStorageBivector<T>(new GaListGradedSingleGrade<T>(2, indexScalarList.CreateEvenListDense()))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create(IEnumerable<T> indexScalarList)
        {
            return Create(indexScalarList.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create(Dictionary<ulong, T> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => ZeroBivector,
                1 => new GaStorageBivector<T>(indexScalarDictionary.First()),
                _ => new GaStorageBivector<T>(new GaListGradedSingleGrade<T>(2, indexScalarDictionary.CreateEvenListSparse()))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create(IGaListEvenSingleKey<T> singleKeyList)
        {
            return new GaStorageBivector<T>(singleKeyList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create(IGaListEven<T> indexScalarList)
        {
            return indexScalarList.IsEmpty()
                ? ZeroBivector
                : new GaStorageBivector<T>(indexScalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Create(IGaListGradedSingleGrade<T> gradeIndexScalarList)
        {
            return new GaStorageBivector<T>(gradeIndexScalarList);
        }


        public override IGaListGradedSingleGrade<T> SingleGradeIndexScalarList { get; }

        private uint? _vSpaceDimension;
        public override uint MinVSpaceDimension 
            => _vSpaceDimension 
                ??= IndexScalarList.GetMinVSpaceDimensionOfBivector();


        private GaStorageBivector()
        {
            SingleGradeIndexScalarList = 
                new GaListGradedSingleGradeEmpty<T>(2);
        }
        
        private GaStorageBivector(KeyValuePair<ulong, T> indexScalarPair)
        {
            var (index, scalar) = indexScalarPair;

            SingleGradeIndexScalarList = 
                new GaListGradedSingleGradeKey<T>(2, index, scalar);
        }
        
        private GaStorageBivector([NotNull] IGaListEvenSingleKey<T> singleKeyList)
        {
            SingleGradeIndexScalarList = new GaListGradedSingleGradeKey<T>(2, singleKeyList);
        }
        
        private GaStorageBivector([NotNull] IGaListEven<T> singleKeyList)
        {
            SingleGradeIndexScalarList = new GaListGradedSingleGrade<T>(2, singleKeyList);
        }

        private GaStorageBivector([NotNull] IGaListGradedSingleGrade<T> gradeIndexScalarList)
        {
            if (gradeIndexScalarList.Grade != 2)
                throw new ArgumentException();

            SingleGradeIndexScalarList = 
                gradeIndexScalarList;
        }


        public bool ContainsTerm(int basisVectorIndex1, int basisVectorIndex2)
        {
            var index = 
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1, 
                    basisVectorIndex2
                );

            return IndexScalarList.ContainsKey(index);
        }

        public bool ContainsTerm(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            var index = 
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1, 
                    basisVectorIndex2
                );

            return IndexScalarList.ContainsKey(index);
        }


        public bool TryGetTermScalar(ulong index1, ulong index2, out T scalar)
        {
            if (index1 >= index2)
                throw new InvalidOperationException();
               
            var index = 
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2);

            if (IndexScalarList.TryGetValue(index, out scalar))
                return true;

            scalar = default;
            return false;
        }


        public override bool TryGetScalar(out T value)
        {
            value = default;
            return false;
        }

        public override bool TryGetTermByIndex(int index, out GaBasisTerm<T> term)
        {
            if (IndexScalarList.TryGetValue(index, out var value))
            {
                term = value.CreateBasisBivectorTerm(index);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaBasisTerm<T> term)
        {
            if (IndexScalarList.TryGetValue(index, out var value))
            {
                term = value.CreateBasisBivectorTerm(index);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaBasisTerm<T> term)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            if (Grade == grade && IndexScalarList.TryGetValue(index, out var value))
            {
                term = value.CreateBasisBivectorTerm(index);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term)
        {
            if (Grade == grade && IndexScalarList.TryGetValue(index, out var value))
            {
                term = value.CreateBasisBivectorTerm(index);
                return true;
            }

            term = null;
            return false;
        }

        public bool TryGetTerm(ulong index1, ulong index2, out GaBasisTerm<T> term)
        {
            if (index1 >= index2)
                throw new InvalidOperationException();

            var index = 
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2);

            if (IndexScalarList.TryGetValue(index, out var scalar))
            {
                term = scalar.CreateBasisBivectorTerm(index1, index2);

                return true;
            }

            term = null;
            return false;
        }


        public IEnumerable<GaRecordKeyPair> GetTermsBasisVectorsIndices()
        {
            return IndexScalarList.GetKeys()
                .Select(GaBasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        public IEnumerable<GaRecordKeyPair> GetTermsBasisVectorsIndices(Func<ulong, bool> filterFunc)
        {
            return IndexScalarList.GetKeys()
                .Where(filterFunc)
                .Select(GaBasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        public IEnumerable<GaRecordKeyPair> GetTermsBasisVectorsIndices(Func<uint, ulong, bool> filterFunc)
        {
            return IndexScalarList.GetKeys()
                .Where(index => filterFunc(Grade, index))
                .Select(GaBasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        public IEnumerable<GaRecordKeyPair> GetTermsBasisVectorsIndices(Func<ulong, T, bool> filterFunc)
        {
            return IndexScalarList
                .GetKeyValueRecords()
                .Where(pair => filterFunc(pair.Key, pair.Value))
                .Select(pair => pair.Key)
                .Select(GaBasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        public IEnumerable<GaRecordKeyPair> GetTermsBasisVectorsIndices(Func<uint, ulong, T, bool> filterFunc)
        {
            return IndexScalarList
                .GetKeyValueRecords()
                .Where(pair => filterFunc(Grade, pair.Key, pair.Value))
                .Select(pair => pair.Key)
                .Select(GaBasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        public IEnumerable<GaRecordKeyPair> GetTermsBasisVectorsIndices(Func<T, bool> filterFunc)
        {
            return IndexScalarList
                .GetKeyValueRecords()
                .Where(pair => filterFunc(pair.Value))
                .Select(pair => pair.Key)
                .Select(GaBasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }


        public IGaStorageBivector<T> GetBivectorCopy()
        {
            var indexScalarList = 
                IndexScalarList.GetCopy();

            return GaStorageBivector<T>.Create(indexScalarList);
        }

        public IGaStorageBivector<T2> MapBivectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            var indexScalarList = 
                IndexScalarList.MapValues(scalarMapping);

            return GaStorageBivector<T2>.Create(indexScalarList);
        }

        public IGaStorageBivector<T2> MapBivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var indexScalarList = 
                IndexScalarList.MapValues(
                    (index, scalar) => 
                        idScalarMapping(index.BasisBivectorIndexToId(), scalar)
                );

            return GaStorageBivector<T2>.Create(indexScalarList);
        }

        public IGaStorageBivector<T2> MapBivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var indexScalarList = 
                IndexScalarList.MapValues(indexScalarMapping);

            return GaStorageBivector<T2>.Create(indexScalarList);
        }

        public IGaStorageBivector<T2> MapBivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var indexScalarList = 
                IndexScalarList.MapValues(
                    (index, scalar) => 
                        gradeIndexScalarMapping(2U, index, scalar)
                );

            return GaStorageBivector<T2>.Create(indexScalarList);
        }


        public IGaStorageBivector<T> FilterBivectorByScalar(Func<T, bool> scalarFilter)
        {
            var indexScalarList =
                IndexScalarList.FilterByValue(scalarFilter);

            return GaStorageBivector<T>.Create(indexScalarList);
        }

        public IGaStorageBivector<T> FilterBivectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter)
        {
            var indexScalarList =
                IndexScalarList.FilterByKeyValue(indexScalarFilter);

            return GaStorageBivector<T>.Create(indexScalarList);
        }

        public IGaStorageBivector<T> FilterBivectorByIndex(Func<ulong, bool> indexFilter)
        {
            var indexScalarList =
                IndexScalarList.FilterByKey(indexFilter);

            return GaStorageBivector<T>.Create(indexScalarList);
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
                ? ZeroBivector
                : this;
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarList =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => scalarSelection(pair.Value)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarList =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => indexScalarSelection(pair.Key, pair.Value)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarList =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => indexSelection(pair.Key)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade)
        {
            return grade == 2 && !IsEmpty()
                ? this 
                : GaStorageKVector<T>.ZeroKVector(grade);
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
            var indexScalarList =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => idSelection(pair.Key.BasisBivectorIndexToId())
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var indexScalarList =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => gradeIndexSelection(2, pair.Key)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarList =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => scalarSelection(pair.Value)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var indexScalarList =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => idScalarSelection(pair.Key.BasisBivectorIndexToId(), pair.Value)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var indexScalarList =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => gradeIndexScalarSelection(2, pair.Key, pair.Value)
                ).CreateDictionary();

            return Create(indexScalarList);
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

        public override IEnumerable<GaBasisBlade> GetBasisBlades()
        {
            return IndexScalarList.GetKeyValueRecords().Select(pair => 
                (GaBasisBlade)pair.Key.CreateBasisBivector()
            );
        }
        

        public override IEnumerable<GaBasisTerm<T>> GetTerms()
        {
            return IndexScalarList.GetKeyValueRecords().Select(pair => 
                pair.Value.CreateBasisBivectorTerm(pair.Key)
            );
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                var id = index.BasisBivectorIndexToId();

                if (idSelection(id))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                if (gradeIndexSelection(2, index))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                if (scalarSelection(scalar))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                var id = index.BasisBivectorIndexToId();

                if (idScalarSelection(id, scalar))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                if (gradeIndexScalarSelection(2, index, scalar))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public IGaStorageBivector<T> GetBivectorStorage()
        {
            return this;
        }

        public IGaStorageBivector<T> GetBivectorStorageCopy()
        {
            return GaStorageBivector<T>.Create(
                IndexScalarList.GetCopy()
            );
        }

        public IGaStorageBivector<T> GetBivectorStorageCopy(Func<T, T> scalarMapping)
        {
            return GaStorageBivector<T>.Create(
                IndexScalarList.MapValues(scalarMapping)
            );
        }

        public IEnumerable<GaRecordKeyPairValue<T>> GetBasisVectorsIndexScalarRecords()
        {
            return IndexScalarList.GetKeyValueRecords().Select(
                pair =>
                {
                    var (index, scalar) = pair;
                    var (index1, index2) = 
                        index.BasisBivectorIndexToVectorIndices();

                    return new GaRecordKeyPairValue<T>(index1, index2, scalar);
                }
            );
        }
    }
}