using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    /// <summary>
    /// Can store the scalar coefficients of a vector of any dimension.
    /// The scalars are assumed to be of immutable type such as T, complex, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed record GaBivectorStorage<T> 
        : GaKVectorStorageBase<T>, IGaBivectorStorage<T>
    {
        public static GaBivectorStorage<T> ZeroBivector { get; }
            = new GaBivectorStorage<T>();

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> Create([NotNull] T scalar)
        {
            return new GaBivectorStorage<T>(
                new LaVectorSingleGradeIndexStorage<T>(2, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> Create(ulong index, T scalar)
        {
            return new GaBivectorStorage<T>(
                new LaVectorSingleGradeIndexStorage<T>(2, index, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> Create(params T[] indexScalarList)
        {
            return indexScalarList.Length switch
            {
                0 => ZeroBivector,
                1 => new GaBivectorStorage<T>(new LaVectorSingleGradeIndexStorage<T>(2, indexScalarList[0])),
                _ => new GaBivectorStorage<T>(new LaVectorSingleGradeStorage<T>(2, indexScalarList.CreateLaVectorDenseStorage()))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> Create(IReadOnlyList<T> indexScalarList)
        {
            return indexScalarList.Count switch
            {
                0 => ZeroBivector,
                1 => new GaBivectorStorage<T>(new LaVectorSingleGradeIndexStorage<T>(2, indexScalarList[0])),
                _ => new GaBivectorStorage<T>(new LaVectorSingleGradeStorage<T>(2, indexScalarList.CreateLaVectorDenseStorage()))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> Create(IEnumerable<T> indexScalarList)
        {
            return Create(indexScalarList.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> Create(Dictionary<ulong, T> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => ZeroBivector,
                1 => new GaBivectorStorage<T>(indexScalarDictionary.First()),
                _ => new GaBivectorStorage<T>(new LaVectorSingleGradeStorage<T>(2, indexScalarDictionary.CreateLaVectorSparseStorage()))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> Create(ILaVectorSingleIndexEvenStorage<T> singleKeyList)
        {
            return new GaBivectorStorage<T>(singleKeyList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> Create(ILaVectorEvenStorage<T> indexScalarList)
        {
            return indexScalarList.IsEmpty()
                ? ZeroBivector
                : new GaBivectorStorage<T>(indexScalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivectorStorage<T> Create(ILaVectorSingleGradeStorage<T> gradeIndexScalarList)
        {
            return new GaBivectorStorage<T>(gradeIndexScalarList);
        }


        public override ILaVectorSingleGradeStorage<T> SingleGradeIndexScalarList { get; }

        private uint? _vSpaceDimension;
        public override uint MinVSpaceDimension 
            => _vSpaceDimension 
                ??= IndexScalarList.GetMinVSpaceDimensionOfBivector();


        private GaBivectorStorage()
        {
            SingleGradeIndexScalarList = 
                new LaVectorEmptySingleGradeStorage<T>(2);
        }
        
        private GaBivectorStorage(KeyValuePair<ulong, T> indexScalarPair)
        {
            var (index, scalar) = indexScalarPair;

            SingleGradeIndexScalarList = 
                new LaVectorSingleGradeIndexStorage<T>(2, index, scalar);
        }
        
        private GaBivectorStorage([NotNull] ILaVectorSingleIndexEvenStorage<T> singleKeyList)
        {
            SingleGradeIndexScalarList = new LaVectorSingleGradeIndexStorage<T>(2, singleKeyList);
        }
        
        private GaBivectorStorage([NotNull] ILaVectorEvenStorage<T> singleKeyList)
        {
            SingleGradeIndexScalarList = new LaVectorSingleGradeStorage<T>(2, singleKeyList);
        }

        private GaBivectorStorage([NotNull] ILaVectorSingleGradeStorage<T> gradeIndexScalarList)
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

            return IndexScalarList.ContainsIndex(index);
        }

        public bool ContainsTerm(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            var index = 
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1, 
                    basisVectorIndex2
                );

            return IndexScalarList.ContainsIndex(index);
        }


        public bool TryGetTermScalar(ulong index1, ulong index2, out T scalar)
        {
            if (index1 >= index2)
                throw new InvalidOperationException();
               
            var index = 
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2);

            if (IndexScalarList.TryGetScalar(index, out scalar))
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
            if (IndexScalarList.TryGetScalar(index, out var value))
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

            if (Grade == grade && IndexScalarList.TryGetScalar(index, out var value))
            {
                term = value.CreateBasisBivectorTerm(index);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term)
        {
            if (Grade == grade && IndexScalarList.TryGetScalar(index, out var value))
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

            if (IndexScalarList.TryGetScalar(index, out var scalar))
            {
                term = scalar.CreateBasisBivectorTerm(index1, index2);

                return true;
            }

            term = null;
            return false;
        }


        public IEnumerable<IndexPairRecord> GetTermsBasisVectorsIndices()
        {
            return IndexScalarList.GetIndices()
                .Select(GaBasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        public IEnumerable<IndexPairRecord> GetTermsBasisVectorsIndices(Func<ulong, bool> filterFunc)
        {
            return IndexScalarList.GetIndices()
                .Where(filterFunc)
                .Select(GaBasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        public IEnumerable<IndexPairRecord> GetTermsBasisVectorsIndices(Func<uint, ulong, bool> filterFunc)
        {
            return IndexScalarList.GetIndices()
                .Where(index => filterFunc(Grade, index))
                .Select(GaBasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        public IEnumerable<IndexPairRecord> GetTermsBasisVectorsIndices(Func<ulong, T, bool> filterFunc)
        {
            return IndexScalarList
                .GetIndexScalarRecords()
                .Where(pair => filterFunc(pair.Index, pair.Scalar))
                .Select(pair => pair.Index)
                .Select(GaBasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        public IEnumerable<IndexPairRecord> GetTermsBasisVectorsIndices(Func<uint, ulong, T, bool> filterFunc)
        {
            return IndexScalarList
                .GetIndexScalarRecords()
                .Where(pair => filterFunc(Grade, pair.Index, pair.Scalar))
                .Select(pair => pair.Index)
                .Select(GaBasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }

        public IEnumerable<IndexPairRecord> GetTermsBasisVectorsIndices(Func<T, bool> filterFunc)
        {
            return IndexScalarList
                .GetIndexScalarRecords()
                .Where(pair => filterFunc(pair.Scalar))
                .Select(pair => pair.Index)
                .Select(GaBasisBivectorUtils.BasisBivectorIndexToVectorIndices);
        }


        public IGaBivectorStorage<T> GetBivectorCopy()
        {
            var indexScalarList = 
                IndexScalarList.GetCopy();

            return Create(indexScalarList);
        }

        public IGaBivectorStorage<T2> MapBivectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            var indexScalarList = 
                IndexScalarList.MapScalars(scalarMapping);

            return GaBivectorStorage<T2>.Create(indexScalarList);
        }

        public IGaBivectorStorage<T2> MapBivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var indexScalarList = 
                IndexScalarList.MapScalars(
                    (index, scalar) => 
                        idScalarMapping(index.BasisBivectorIndexToId(), scalar)
                );

            return GaBivectorStorage<T2>.Create(indexScalarList);
        }

        public IGaBivectorStorage<T2> MapBivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var indexScalarList = 
                IndexScalarList.MapScalars(indexScalarMapping);

            return GaBivectorStorage<T2>.Create(indexScalarList);
        }

        public IGaBivectorStorage<T2> MapBivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var indexScalarList = 
                IndexScalarList.MapScalars(
                    (index, scalar) => 
                        gradeIndexScalarMapping(2U, index, scalar)
                );

            return GaBivectorStorage<T2>.Create(indexScalarList);
        }


        public IGaBivectorStorage<T> FilterBivectorByScalar(Func<T, bool> scalarFilter)
        {
            var indexScalarList =
                IndexScalarList.FilterByScalar(scalarFilter);

            return Create(indexScalarList);
        }

        public IGaBivectorStorage<T> FilterBivectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter)
        {
            var indexScalarList =
                IndexScalarList.FilterByIndexScalar(indexScalarFilter);

            return Create(indexScalarList);
        }

        public IGaBivectorStorage<T> FilterBivectorByIndex(Func<ulong, bool> indexFilter)
        {
            var indexScalarList =
                IndexScalarList.FilterByIndex(indexFilter);

            return Create(indexScalarList);
        }


        public override IGaVectorStorage<T> GetVectorPart()
        {
            return GaVectorStorage<T>.ZeroVector;
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            return GaVectorStorage<T>.ZeroVector;
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return GaVectorStorage<T>.ZeroVector;
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return GaVectorStorage<T>.ZeroVector;
        }

        public override IGaBivectorStorage<T> GetBivectorPart()
        {
            return IsEmpty()
                ? ZeroBivector
                : this;
        }

        public override IGaBivectorStorage<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarList =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => scalarSelection(pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IGaBivectorStorage<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarList =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => indexScalarSelection(pair.Index, pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IGaBivectorStorage<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarList =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => indexSelection(pair.Index)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade)
        {
            return grade == 2 && !IsEmpty()
                ? this 
                : GaKVectorStorage<T>.ZeroKVector(grade);
        }


        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            return grade == 2
                ? GetBivectorPart(scalarSelection)
                : GaKVectorStorage<T>.ZeroKVector(grade);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            return grade == 2
                ? GetBivectorPart(indexScalarSelection)
                : GaKVectorStorage<T>.ZeroKVector(grade);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            return grade == 2
                ? GetBivectorPart(indexSelection)
                : GaKVectorStorage<T>.ZeroKVector(grade);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var indexScalarList =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => idSelection(pair.Index.BasisBivectorIndexToId())
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var indexScalarList =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => gradeIndexSelection(2, pair.Index)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarList =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => scalarSelection(pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var indexScalarList =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => idScalarSelection(pair.Index.BasisBivectorIndexToId(), pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var indexScalarList =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => gradeIndexScalarSelection(2, pair.Index, pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarList);
        }

        public override Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                GaVectorStorage<T>.ZeroVector,
                GaVectorStorage<T>.ZeroVector
            );
        }

        public override Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                GaVectorStorage<T>.ZeroVector,
                GaVectorStorage<T>.ZeroVector
            );
        }

        public override Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                GaVectorStorage<T>.ZeroVector,
                GaVectorStorage<T>.ZeroVector
            );
        }

        public override IEnumerable<GaBasisBlade> GetBasisBlades()
        {
            return IndexScalarList.GetIndexScalarRecords().Select(pair => 
                (GaBasisBlade)pair.Index.CreateBasisBivector()
            );
        }
        

        public override IEnumerable<GaBasisTerm<T>> GetTerms()
        {
            return IndexScalarList.GetIndexScalarRecords().Select(pair => 
                pair.Scalar.CreateBasisBivectorTerm(pair.Index)
            );
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                var id = index.BasisBivectorIndexToId();

                if (idSelection(id))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                if (gradeIndexSelection(2, index))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                if (scalarSelection(scalar))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                var id = index.BasisBivectorIndexToId();

                if (idScalarSelection(id, scalar))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                if (gradeIndexScalarSelection(2, index, scalar))
                    yield return scalar.CreateBasisBivectorTerm(index);
            }
        }

        public IGaBivectorStorage<T> GetBivectorStorage()
        {
            return this;
        }

        public IGaBivectorStorage<T> GetBivectorStorageCopy()
        {
            return Create(
                IndexScalarList.GetCopy()
            );
        }

        public IGaBivectorStorage<T> GetBivectorStorageCopy(Func<T, T> scalarMapping)
        {
            return Create(
                IndexScalarList.MapScalars(scalarMapping)
            );
        }

        public IEnumerable<IndexPairScalarRecord<T>> GetBasisVectorsIndexScalarRecords()
        {
            return IndexScalarList.GetIndexScalarRecords().Select(
                pair =>
                {
                    var (index, scalar) = pair;
                    var (index1, index2) = 
                        index.BasisBivectorIndexToVectorIndices();

                    return new IndexPairScalarRecord<T>(index1, index2, scalar);
                }
            );
        }
    }
}