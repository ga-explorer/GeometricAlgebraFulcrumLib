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
    public sealed record GaVectorStorage<T> 
        : GaKVectorStorageBase<T>, IGaVectorStorage<T>
    {
        public static GaVectorStorage<T> ZeroVector { get; }
            = new GaVectorStorage<T>();

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> Create([NotNull] T scalar)
        {
            return new GaVectorStorage<T>(
                new LaVectorSingleGradeIndexStorage<T>(1, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> Create(ulong index, T scalar)
        {
            return new GaVectorStorage<T>(
                new LaVectorSingleGradeIndexStorage<T>(1, index, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> Create(params T[] indexScalarList)
        {
            return indexScalarList.Length switch
            {
                0 => ZeroVector,
                1 => new GaVectorStorage<T>(new LaVectorSingleGradeIndexStorage<T>(1, indexScalarList[0])),
                _ => new GaVectorStorage<T>(new LaVectorSingleGradeStorage<T>(1, indexScalarList.CreateLaVectorDenseStorage()))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> Create(IReadOnlyList<T> indexScalarList)
        {
            return indexScalarList.Count switch
            {
                0 => ZeroVector,
                1 => new GaVectorStorage<T>(new LaVectorSingleGradeIndexStorage<T>(1, indexScalarList[0])),
                _ => new GaVectorStorage<T>(new LaVectorSingleGradeStorage<T>(1, indexScalarList.CreateLaVectorDenseStorage()))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> Create(IEnumerable<T> indexScalarList)
        {
            return Create(indexScalarList.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> Create(Dictionary<ulong, T> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => ZeroVector,
                1 => new GaVectorStorage<T>(indexScalarDictionary.First()),
                _ => new GaVectorStorage<T>(new LaVectorSingleGradeStorage<T>(1, indexScalarDictionary.CreateLaVectorSparseStorage()))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> Create(ILaVectorSingleIndexEvenStorage<T> singleKeyList)
        {
            return new GaVectorStorage<T>(singleKeyList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> Create(ILaVectorEvenStorage<T> indexScalarList)
        {
            return indexScalarList.IsEmpty()
                ? ZeroVector
                : new GaVectorStorage<T>(indexScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> Create(ILaVectorSingleGradeStorage<T> gradeIndexScalarList)
        {
            return new GaVectorStorage<T>(gradeIndexScalarList);
        }


        public override ILaVectorSingleGradeStorage<T> SingleGradeIndexScalarList { get; }

        private uint? _vSpaceDimension;
        public override uint MinVSpaceDimension 
            => _vSpaceDimension 
                ??= IndexScalarList.GetMinVSpaceDimensionOfVector();


        private GaVectorStorage()
        {
            SingleGradeIndexScalarList = 
                new LaVectorEmptySingleGradeStorage<T>(1);
        }
        
        private GaVectorStorage(KeyValuePair<ulong, T> indexScalarPair)
        {
            var (index, scalar) = indexScalarPair;

            SingleGradeIndexScalarList = 
                new LaVectorSingleGradeIndexStorage<T>(1, index, scalar);
        }
        
        private GaVectorStorage([NotNull] ILaVectorSingleIndexEvenStorage<T> singleKeyList)
        {
            SingleGradeIndexScalarList = new LaVectorSingleGradeIndexStorage<T>(1, singleKeyList);
        }
        
        private GaVectorStorage([NotNull] ILaVectorEvenStorage<T> singleKeyList)
        {
            SingleGradeIndexScalarList = new LaVectorSingleGradeStorage<T>(1, singleKeyList);
        }

        private GaVectorStorage([NotNull] ILaVectorSingleGradeStorage<T> gradeIndexScalarList)
        {
            if (gradeIndexScalarList.Grade != 1)
                throw new ArgumentException();

            SingleGradeIndexScalarList = 
                gradeIndexScalarList;
        }


        public override bool TryGetScalar(out T value)
        {
            value = default;
            return false;
        }

        public override bool TryGetTermByIndex(int index, out GaBasisTerm<T> term)
        {
            var i = (ulong) index;

            if (IndexScalarList.TryGetScalar(i, out var value))
            {
                term = value.CreateBasisVectorTerm(i);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaBasisTerm<T> term)
        {
            if (IndexScalarList.TryGetScalar(index, out var value))
            {
                term = value.CreateBasisVectorTerm(index);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaBasisTerm<T> term)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            if (grade == 1 && IndexScalarList.TryGetScalar(index, out var value))
            {
                term = value.CreateBasisVectorTerm(index);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term)
        {
            if (grade == 1 && IndexScalarList.TryGetScalar(index, out var value))
            {
                term = value.CreateBasisVectorTerm(index);
                return true;
            }

            term = null;
            return false;
        }


        public IGaVectorStorage<T> GetVectorCopy()
        {
            var indexScalarDictionary = 
                IndexScalarList.GetCopy();

            return Create(indexScalarDictionary);
        }

        public IGaVectorStorage<T2> MapVectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarList.MapScalars(scalarMapping);

            return GaVectorStorage<T2>.Create(indexScalarDictionary);
        }

        public IGaVectorStorage<T2> MapVectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarList.MapScalars(
                    (index, scalar) => 
                        idScalarMapping(index.BasisVectorIndexToId(), scalar)
                );

            return GaVectorStorage<T2>.Create(indexScalarDictionary);
        }

        public IGaVectorStorage<T2> MapVectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarList.MapScalars(indexScalarMapping);

            return GaVectorStorage<T2>.Create(indexScalarDictionary);
        }

        public IGaVectorStorage<T2> MapVectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarList.MapScalars(
                    (index, scalar) => 
                        gradeIndexScalarMapping(1U, index, scalar)
                );

            return GaVectorStorage<T2>.Create(indexScalarDictionary);
        }


        public IGaVectorStorage<T> FilterVectorByScalar(Func<T, bool> scalarFilter)
        {
            var indexScalarDictionary =
                IndexScalarList.FilterByScalar(scalarFilter);

            return Create(indexScalarDictionary);
        }

        public IGaVectorStorage<T> FilterVectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter)
        {
            var indexScalarDictionary =
                IndexScalarList.FilterByIndexScalar(indexScalarFilter);

            return Create(indexScalarDictionary);
        }

        public IGaVectorStorage<T> FilterVectorByIndex(Func<ulong, bool> indexFilter)
        {
            var indexScalarDictionary =
                IndexScalarList.FilterByIndex(indexFilter);

            return Create(indexScalarDictionary);
        }


        public override IGaVectorStorage<T> GetVectorPart()
        {
            return IsEmpty()
                ? ZeroVector
                : this;
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => scalarSelection(pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => indexScalarSelection(pair.Index, pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override IGaVectorStorage<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => indexSelection(pair.Index)
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override IGaBivectorStorage<T> GetBivectorPart()
        {
            return GaBivectorStorage<T>.ZeroBivector;
        }

        public override IGaBivectorStorage<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            return GaBivectorStorage<T>.ZeroBivector;
        }

        public override IGaBivectorStorage<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return GaBivectorStorage<T>.ZeroBivector;
        }

        public override IGaBivectorStorage<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            return GaBivectorStorage<T>.ZeroBivector;
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade)
        {
            return grade == 1 && !IsEmpty()
                ? this
                : GaKVectorStorage<T>.ZeroKVector(grade);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            return grade == 1
                ? GetVectorPart(scalarSelection)
                : GaKVectorStorage<T>.ZeroKVector(grade);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            return grade == 1
                ? GetVectorPart(indexScalarSelection)
                : GaKVectorStorage<T>.ZeroKVector(grade);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            return grade == 1
                ? GetVectorPart(indexSelection)
                : GaKVectorStorage<T>.ZeroKVector(grade);
        }
        

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => idSelection(pair.Index.BasisVectorIndexToId())
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => gradeIndexSelection(1, pair.Index)
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => scalarSelection(pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => idScalarSelection(pair.Index.BasisVectorIndexToId(), pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetIndexScalarRecords().Where(
                    pair => gradeIndexScalarSelection(1, pair.Index, pair.Scalar)
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                if (indexSelection(index))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                indexScalarDictionary1.CreateGaVectorStorage(),
                indexScalarDictionary2.CreateGaVectorStorage()
            );
        }

        public override Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                if (indexScalarSelection(index, scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                indexScalarDictionary1.CreateGaVectorStorage(),
                indexScalarDictionary2.CreateGaVectorStorage()
            );
        }

        public override Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                if (scalarSelection(scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<IGaVectorStorage<T>, IGaVectorStorage<T>>(
                indexScalarDictionary1.CreateGaVectorStorage(),
                indexScalarDictionary2.CreateGaVectorStorage()
            );
        }
        
        public override IEnumerable<GaBasisBlade> GetBasisBlades()
        {
            return IndexScalarList.GetIndices().Select(index => 
                (GaBasisBlade) index.CreateBasisVector()
            );
        }
        
        public override IEnumerable<GaBasisTerm<T>> GetTerms()
        {
            return IndexScalarList
                .GetIndexScalarRecords()
                .Select(pair => pair.Scalar.CreateBasisVectorTerm(pair.Index));
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                var id = index.BasisVectorIndexToId();

                if (idSelection(id))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                if (gradeIndexSelection(1, index))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                if (scalarSelection(scalar))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                var id = index.BasisVectorIndexToId();

                if (idScalarSelection(id, scalar))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetIndexScalarRecords())
            {
                if (gradeIndexScalarSelection(1, index, scalar))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        public IGaVectorStorage<T> GetVectorStorage()
        {
            return this;
        }

        public IGaVectorStorage<T> GetVectorStorageCopy()
        {
            return Create(IndexScalarList.GetCopy());
        }

        public IGaVectorStorage<T> GetVectorStorageCopy(Func<T, T> scalarMapping)
        {
            return Create(IndexScalarList.MapScalars(scalarMapping));
        }
    }
}