using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    /// <summary>
    /// Can store the scalar coefficients of a vector of any dimension.
    /// The scalars are assumed to be of immutable type such as T, complex, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed record BivectorStorage<T> 
        : KVectorStorage<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Create(T scalar)
        {
            return new BivectorStorage<T>(
                new LinVectorSingleScalarGradedStorage<T>(2, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Create(ulong index, T scalar)
        {
            return new BivectorStorage<T>(
                new LinVectorSingleScalarGradedStorage<T>(2, index, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Create(params T[] indexScalarList)
        {
            return indexScalarList.Length switch
            {
                0 => ZeroBivector,
                1 => new BivectorStorage<T>(new LinVectorSingleScalarGradedStorage<T>(2, indexScalarList[0])),
                _ => new BivectorStorage<T>(new LinVectorSingleGradeStorage<T>(2, indexScalarList.CreateLinVectorDenseStorage()))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Create(IReadOnlyList<T> indexScalarList)
        {
            return indexScalarList.Count switch
            {
                0 => ZeroBivector,
                1 => new BivectorStorage<T>(new LinVectorSingleScalarGradedStorage<T>(2, indexScalarList[0])),
                _ => new BivectorStorage<T>(new LinVectorSingleGradeStorage<T>(2, indexScalarList.CreateLinVectorDenseStorage()))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Create(IEnumerable<T> indexScalarList)
        {
            return Create(indexScalarList.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Create(Dictionary<ulong, T> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => ZeroBivector,
                1 => new BivectorStorage<T>(indexScalarDictionary.First()),
                _ => new BivectorStorage<T>(new LinVectorSingleGradeStorage<T>(2, indexScalarDictionary.CreateLinVectorSparseStorage()))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Create(ILinVectorSingleScalarStorage<T> singleScalarVectorStorage)
        {
            return new BivectorStorage<T>(singleScalarVectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Create(ILinVectorStorage<T> indexScalarList)
        {
            return indexScalarList.IsEmpty()
                ? ZeroBivector
                : new BivectorStorage<T>(indexScalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Create(ILinVectorSingleGradeStorage<T> gradeIndexScalarList)
        {
            return new BivectorStorage<T>(gradeIndexScalarList);
        }


        public override uint MinVSpaceDimension 
            => VSpaceDimensions 
                ??= GetLinVectorIndexScalarStorage().GetMinVSpaceDimensionOfBivector();


        internal BivectorStorage() 
            : base(2)
        {
        }
        
        private BivectorStorage(KeyValuePair<ulong, T> indexScalarPair)
            : base(2, indexScalarPair)
        {
        }
        
        private BivectorStorage(ILinVectorSingleScalarStorage<T> singleScalarVectorStorage)
            : base(2, singleScalarVectorStorage)
        {
        }
        
        private BivectorStorage(ILinVectorStorage<T> singleScalarVectorStorage)
            : base(2, singleScalarVectorStorage)
        {
        }

        private BivectorStorage(ILinVectorSingleGradeStorage<T> gradeIndexScalarList)
            : base(gradeIndexScalarList.Grade == 2 ? gradeIndexScalarList : throw new ArgumentException())
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsTerm(int basisVectorIndex1, int basisVectorIndex2)
        {
            var index = 
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1, 
                    basisVectorIndex2
                );

            return GetLinVectorIndexScalarStorage().ContainsIndex(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsTerm(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            var index = 
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1, 
                    basisVectorIndex2
                );

            return GetLinVectorIndexScalarStorage().ContainsIndex(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTermScalar(ulong index1, ulong index2, out T scalar)
        {
            if (index1 >= index2)
                throw new InvalidOperationException();
               
            var index = 
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2);

            if (GetLinVectorIndexScalarStorage().TryGetScalar(index, out scalar))
                return true;

            scalar = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTermByIndex(int index, out KeyValuePair<ulong, T> term)
        {
            if (GetLinVectorIndexScalarStorage().TryGetScalar(index, out var value))
            {
                term = value.CreateBasisBivectorTerm(index);
                return true;
            }

            term = new KeyValuePair<ulong, T>();
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTermByIndex(ulong index, out KeyValuePair<ulong, T> term)
        {
            if (GetLinVectorIndexScalarStorage().TryGetScalar(index, out var value))
            {
                term = value.CreateBasisBivectorTerm(index);
                return true;
            }

            term = new KeyValuePair<ulong, T>();
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTerm(ulong id, out KeyValuePair<ulong, T> term)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            if (Grade == grade && GetLinVectorIndexScalarStorage().TryGetScalar(index, out var value))
            {
                term = value.CreateBasisBivectorTerm(index);
                return true;
            }

            term = new KeyValuePair<ulong, T>();
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTerm(uint grade, ulong index, out KeyValuePair<ulong, T> term)
        {
            if (Grade == grade && GetLinVectorIndexScalarStorage().TryGetScalar(index, out var value))
            {
                term = value.CreateBasisBivectorTerm(index);
                return true;
            }

            term = new KeyValuePair<ulong, T>();
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTerm(ulong index1, ulong index2, out KeyValuePair<ulong, T> term)
        {
            if (index1 >= index2)
                throw new InvalidOperationException();

            var index = 
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2);

            if (GetLinVectorIndexScalarStorage().TryGetScalar(index, out var scalar))
            {
                term = scalar.CreateBasisBivectorTerm(index1, index2);

                return true;
            }

            term = new KeyValuePair<ulong, T>();
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetTermsBasisVectorsIndices()
        {
            return GetLinVectorIndexScalarStorage().GetIndices()
                .Select(BasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetTermsBasisVectorsIndices(Func<ulong, bool> filterFunc)
        {
            return GetLinVectorIndexScalarStorage().GetIndices()
                .Where(filterFunc)
                .Select(BasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetTermsBasisVectorsIndices(Func<uint, ulong, bool> filterFunc)
        {
            return GetLinVectorIndexScalarStorage().GetIndices()
                .Where(index => filterFunc(Grade, index))
                .Select(BasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetTermsBasisVectorsIndices(Func<ulong, T, bool> filterFunc)
        {
            return GetLinVectorIndexScalarStorage()
                .GetIndexScalarRecords()
                .Where(pair => filterFunc(pair.KvIndex, pair.Scalar))
                .Select(pair => pair.KvIndex)
                .Select(BasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetTermsBasisVectorsIndices(Func<uint, ulong, T, bool> filterFunc)
        {
            return GetLinVectorIndexScalarStorage()
                .GetIndexScalarRecords()
                .Where(pair => filterFunc(Grade, pair.KvIndex, pair.Scalar))
                .Select(pair => pair.KvIndex)
                .Select(BasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetTermsBasisVectorsIndices(Func<T, bool> filterFunc)
        {
            return GetLinVectorIndexScalarStorage()
                .GetIndexScalarRecords()
                .Where(pair => filterFunc(pair.Scalar))
                .Select(pair => pair.KvIndex.BasisBivectorIndexToVectorIndices());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivectorCopy()
        {
            var indexScalarList = 
                GetLinVectorIndexScalarStorage().GetCopy();

            return Create(indexScalarList);
        }

        public BivectorStorage<T2> MapBivectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            var indexScalarList = 
                GetLinVectorIndexScalarStorage().MapScalars(scalarMapping);

            return BivectorStorage<T2>.Create(indexScalarList);
        }

        public BivectorStorage<T2> MapBivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var indexScalarList = 
                GetLinVectorIndexScalarStorage().MapScalars(
                    (index, scalar) => 
                        idScalarMapping(index.BasisBivectorIndexToId(), scalar)
                );

            return BivectorStorage<T2>.Create(indexScalarList);
        }

        public BivectorStorage<T2> MapBivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var indexScalarList = 
                GetLinVectorIndexScalarStorage().MapScalars(indexScalarMapping);

            return BivectorStorage<T2>.Create(indexScalarList);
        }

        public BivectorStorage<T2> MapBivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var indexScalarList = 
                GetLinVectorIndexScalarStorage().MapScalars(
                    (index, scalar) => 
                        gradeIndexScalarMapping(2U, index, scalar)
                );

            return BivectorStorage<T2>.Create(indexScalarList);
        }


        public BivectorStorage<T> FilterBivectorByScalar(Func<T, bool> scalarFilter)
        {
            var indexScalarList =
                GetLinVectorIndexScalarStorage().FilterByScalar(scalarFilter);

            return Create(indexScalarList);
        }

        public BivectorStorage<T> FilterBivectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter)
        {
            var indexScalarList =
                GetLinVectorIndexScalarStorage().FilterByIndexScalar(indexScalarFilter);

            return Create(indexScalarList);
        }

        public BivectorStorage<T> FilterBivectorByIndex(Func<ulong, bool> indexFilter)
        {
            var indexScalarList =
                GetLinVectorIndexScalarStorage().FilterByIndex(indexFilter);

            return Create(indexScalarList);
        }


        public override VectorStorage<T> GetVectorPart()
        {
            return ZeroVector;
        }

        public override VectorStorage<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            return ZeroVector;
        }

        public override VectorStorage<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return ZeroVector;
        }

        public override VectorStorage<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return ZeroVector;
        }

        public override BivectorStorage<T> GetBivectorPart()
        {
            return IsEmpty()
                ? ZeroBivector
                : this;
        }

        public override BivectorStorage<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarList =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => scalarSelection(pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override BivectorStorage<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarList =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => indexScalarSelection(pair.KvIndex, pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override BivectorStorage<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarList =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => indexSelection(pair.KvIndex)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override KVectorStorage<T> GetKVectorPart(uint grade)
        {
            return grade == 2 && !IsEmpty()
                ? this 
                : CreateKVectorZero(grade);
        }


        public override KVectorStorage<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            return grade == 2
                ? GetBivectorPart(scalarSelection)
                : CreateKVectorZero(grade);
        }

        public override KVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            return grade == 2
                ? GetBivectorPart(indexScalarSelection)
                : CreateKVectorZero(grade);
        }

        public override KVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            return grade == 2
                ? GetBivectorPart(indexSelection)
                : CreateKVectorZero(grade);
        }

        public override IMultivectorStorage<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var indexScalarList =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => idSelection(pair.KvIndex.BasisBivectorIndexToId())
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var indexScalarList =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => gradeIndexSelection(2, pair.KvIndex)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IMultivectorStorage<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarList =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => scalarSelection(pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IMultivectorStorage<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var indexScalarList =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => idScalarSelection(pair.KvIndex.BasisBivectorIndexToId(), pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var indexScalarList =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => gradeIndexScalarSelection(2, pair.KvIndex, pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                ZeroVector,
                ZeroVector
            );
        }

        public override Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                ZeroVector,
                ZeroVector
            );
        }

        public override Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                ZeroVector,
                ZeroVector
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorSingleGradeStorage<T> GetLinVectorSingleGradeStorage()
        {
            return SingleGradeVectorStorage;
        }


        public override IEnumerable<KeyValuePair<ulong, T>> GetTerms()
        {
            return GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Select(pair => 
                pair.Scalar.CreateBasisBivectorTerm(pair.KvIndex)
            );
        }

        public override IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                var id = index.BasisBivectorIndexToId();

                if (idSelection(id))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public override IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (gradeIndexSelection(2, index))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public override IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (scalarSelection(scalar))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public override IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                var id = index.BasisBivectorIndexToId();

                if (idScalarSelection(id, scalar))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public override IEnumerable<KeyValuePair<ulong, T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (gradeIndexScalarSelection(2, index, scalar))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public BivectorStorage<T> GetBivectorStorage()
        {
            return this;
        }

        public BivectorStorage<T> GetBivectorStorageCopy()
        {
            return Create(
                GetLinVectorIndexScalarStorage().GetCopy()
            );
        }

        public BivectorStorage<T> GetBivectorStorageCopy(Func<T, T> scalarMapping)
        {
            return Create(
                GetLinVectorIndexScalarStorage().MapScalars(scalarMapping)
            );
        }

        public IEnumerable<RGaKvIndexPairScalarRecord<T>> GetBasisVectorsIndexScalarRecords()
        {
            return GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Select(
                pair =>
                {
                    var (index, scalar) = pair;
                    var (index1, index2) = 
                        index.BasisBivectorIndexToVectorIndices();

                    return new RGaKvIndexPairScalarRecord<T>(index1, index2, scalar);
                }
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return this.GetMultivectorText();
        }
    }
}