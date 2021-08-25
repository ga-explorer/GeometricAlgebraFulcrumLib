using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Storage.Factories;
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
    public sealed record GaStorageVector<T> 
        : GaStorageKVectorBase<T>, IGaStorageVector<T>
    {
        public static GaStorageVector<T> ZeroVector { get; }
            = new GaStorageVector<T>();

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create([NotNull] T scalar)
        {
            return new GaStorageVector<T>(
                new GaListGradedSingleGradeKey<T>(1, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create(ulong index, T scalar)
        {
            return new GaStorageVector<T>(
                new GaListGradedSingleGradeKey<T>(1, index, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create(params T[] indexScalarList)
        {
            return indexScalarList.Length switch
            {
                0 => ZeroVector,
                1 => new GaStorageVector<T>(new GaListGradedSingleGradeKey<T>(1, indexScalarList[0])),
                _ => new GaStorageVector<T>(new GaListGradedSingleGrade<T>(1, indexScalarList.CreateEvenListDense()))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create(IReadOnlyList<T> indexScalarList)
        {
            return indexScalarList.Count switch
            {
                0 => ZeroVector,
                1 => new GaStorageVector<T>(new GaListGradedSingleGradeKey<T>(1, indexScalarList[0])),
                _ => new GaStorageVector<T>(new GaListGradedSingleGrade<T>(1, indexScalarList.CreateEvenListDense()))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create(IEnumerable<T> indexScalarList)
        {
            return Create(indexScalarList.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create(Dictionary<ulong, T> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => ZeroVector,
                1 => new GaStorageVector<T>(indexScalarDictionary.First()),
                _ => new GaStorageVector<T>(new GaListGradedSingleGrade<T>(1, indexScalarDictionary.CreateEvenListSparse()))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create(IGaListEvenSingleKey<T> singleKeyList)
        {
            return new GaStorageVector<T>(singleKeyList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create(IGaListEven<T> indexScalarList)
        {
            return indexScalarList.IsEmpty()
                ? ZeroVector
                : new GaStorageVector<T>(indexScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create(IGaListGradedSingleGrade<T> gradeIndexScalarList)
        {
            return new GaStorageVector<T>(gradeIndexScalarList);
        }


        public override IGaListGradedSingleGrade<T> SingleGradeIndexScalarList { get; }

        private uint? _vSpaceDimension;
        public override uint MinVSpaceDimension 
            => _vSpaceDimension 
                ??= IndexScalarList.GetMinVSpaceDimensionOfVector();


        private GaStorageVector()
        {
            SingleGradeIndexScalarList = 
                new GaListGradedSingleGradeEmpty<T>(1);
        }
        
        private GaStorageVector(KeyValuePair<ulong, T> indexScalarPair)
        {
            var (index, scalar) = indexScalarPair;

            SingleGradeIndexScalarList = 
                new GaListGradedSingleGradeKey<T>(1, index, scalar);
        }
        
        private GaStorageVector([NotNull] IGaListEvenSingleKey<T> singleKeyList)
        {
            SingleGradeIndexScalarList = new GaListGradedSingleGradeKey<T>(1, singleKeyList);
        }
        
        private GaStorageVector([NotNull] IGaListEven<T> singleKeyList)
        {
            SingleGradeIndexScalarList = new GaListGradedSingleGrade<T>(1, singleKeyList);
        }

        private GaStorageVector([NotNull] IGaListGradedSingleGrade<T> gradeIndexScalarList)
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

            if (IndexScalarList.TryGetValue(i, out var value))
            {
                term = value.CreateBasisVectorTerm(i);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaBasisTerm<T> term)
        {
            if (IndexScalarList.TryGetValue(index, out var value))
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

            if (grade == 1 && IndexScalarList.TryGetValue(index, out var value))
            {
                term = value.CreateBasisVectorTerm(index);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term)
        {
            if (grade == 1 && IndexScalarList.TryGetValue(index, out var value))
            {
                term = value.CreateBasisVectorTerm(index);
                return true;
            }

            term = null;
            return false;
        }


        public IGaStorageVector<T> GetVectorCopy()
        {
            var indexScalarDictionary = 
                IndexScalarList.GetCopy();

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public IGaStorageVector<T2> MapVectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarList.MapValues(scalarMapping);

            return GaStorageVector<T2>.Create(indexScalarDictionary);
        }

        public IGaStorageVector<T2> MapVectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarList.MapValues(
                    (index, scalar) => 
                        idScalarMapping(index.BasisVectorIndexToId(), scalar)
                );

            return GaStorageVector<T2>.Create(indexScalarDictionary);
        }

        public IGaStorageVector<T2> MapVectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarList.MapValues(indexScalarMapping);

            return GaStorageVector<T2>.Create(indexScalarDictionary);
        }

        public IGaStorageVector<T2> MapVectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarList.MapValues(
                    (index, scalar) => 
                        gradeIndexScalarMapping(1U, index, scalar)
                );

            return GaStorageVector<T2>.Create(indexScalarDictionary);
        }


        public IGaStorageVector<T> FilterVectorByScalar(Func<T, bool> scalarFilter)
        {
            var indexScalarDictionary =
                IndexScalarList.FilterByValue(scalarFilter);

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public IGaStorageVector<T> FilterVectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter)
        {
            var indexScalarDictionary =
                IndexScalarList.FilterByKeyValue(indexScalarFilter);

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public IGaStorageVector<T> FilterVectorByIndex(Func<ulong, bool> indexFilter)
        {
            var indexScalarDictionary =
                IndexScalarList.FilterByKey(indexFilter);

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }


        public override IGaStorageVector<T> GetVectorPart()
        {
            return IsEmpty()
                ? ZeroVector
                : this;
        }

        public override IGaStorageVector<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => scalarSelection(pair.Value)
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => indexScalarSelection(pair.Key, pair.Value)
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => indexSelection(pair.Key)
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override IGaStorageBivector<T> GetBivectorPart()
        {
            return GaStorageBivector<T>.ZeroBivector;
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            return GaStorageBivector<T>.ZeroBivector;
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return GaStorageBivector<T>.ZeroBivector;
        }

        public override IGaStorageBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            return GaStorageBivector<T>.ZeroBivector;
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade)
        {
            return grade == 1 && !IsEmpty()
                ? this
                : GaStorageKVector<T>.ZeroKVector(grade);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            return grade == 1
                ? GetVectorPart(scalarSelection)
                : GaStorageKVector<T>.ZeroKVector(grade);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            return grade == 1
                ? GetVectorPart(indexScalarSelection)
                : GaStorageKVector<T>.ZeroKVector(grade);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            return grade == 1
                ? GetVectorPart(indexSelection)
                : GaStorageKVector<T>.ZeroKVector(grade);
        }
        

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => idSelection(pair.Key.BasisVectorIndexToId())
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => gradeIndexSelection(1, pair.Key)
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => scalarSelection(pair.Value)
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => idScalarSelection(pair.Key.BasisVectorIndexToId(), pair.Value)
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarList.GetKeyValueRecords().Where(
                    pair => gradeIndexScalarSelection(1, pair.Key, pair.Value)
                ).CreateDictionary();

            return Create(indexScalarDictionary);
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
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
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
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
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
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
        
        public override IEnumerable<GaBasisBlade> GetBasisBlades()
        {
            return IndexScalarList.GetKeys().Select(index => 
                (GaBasisBlade) index.CreateBasisVector()
            );
        }
        
        public override IEnumerable<GaBasisTerm<T>> GetTerms()
        {
            return IndexScalarList
                .GetKeyValueRecords()
                .Select(pair => pair.Value.CreateBasisVectorTerm(pair.Key));
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                var id = index.BasisVectorIndexToId();

                if (idSelection(id))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                if (gradeIndexSelection(1, index))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                if (scalarSelection(scalar))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                var id = index.BasisVectorIndexToId();

                if (idScalarSelection(id, scalar))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarList.GetKeyValueRecords())
            {
                if (gradeIndexScalarSelection(1, index, scalar))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        public IGaStorageVector<T> GetVectorStorage()
        {
            return this;
        }

        public IGaStorageVector<T> GetVectorStorageCopy()
        {
            return Create(IndexScalarList.GetCopy());
        }

        public IGaStorageVector<T> GetVectorStorageCopy(Func<T, T> scalarMapping)
        {
            return Create(IndexScalarList.MapValues(scalarMapping));
        }
    }
}