using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
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
    public sealed record VectorStorage<T> 
        : KVectorStorage<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage([NotNull] T scalar)
        {
            return new VectorStorage<T>(
                new LinVectorSingleScalarGradedStorage<T>(1, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage(ulong index, T scalar)
        {
            return new VectorStorage<T>(
                new LinVectorSingleScalarGradedStorage<T>(1, index, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage(params T[] indexScalarList)
        {
            return indexScalarList.Length switch
            {
                0 => ZeroVector,
                1 => new VectorStorage<T>(new LinVectorSingleScalarGradedStorage<T>(1, indexScalarList[0])),
                _ => new VectorStorage<T>(new LinVectorSingleGradeStorage<T>(1, indexScalarList.CreateLinVectorDenseStorage()))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage(IReadOnlyList<T> indexScalarList)
        {
            return indexScalarList.Count switch
            {
                0 => ZeroVector,
                1 => new VectorStorage<T>(new LinVectorSingleScalarGradedStorage<T>(1, indexScalarList[0])),
                _ => new VectorStorage<T>(new LinVectorSingleGradeStorage<T>(1, indexScalarList.CreateLinVectorDenseStorage()))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage(IEnumerable<T> indexScalarList)
        {
            return CreateVectorStorage(indexScalarList.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage(Dictionary<ulong, T> indexScalarDictionary)
        {
            return indexScalarDictionary.Count switch
            {
                0 => ZeroVector,
                1 => new VectorStorage<T>(indexScalarDictionary.First()),
                _ => new VectorStorage<T>(new LinVectorSingleGradeStorage<T>(1, indexScalarDictionary.CreateLinVectorSparseStorage()))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage(ILinVectorSingleScalarStorage<T> singleScalarVectorStorage)
        {
            return new VectorStorage<T>(singleScalarVectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage(ILinVectorStorage<T> indexScalarList)
        {
            return indexScalarList.IsEmpty()
                ? ZeroVector
                : new VectorStorage<T>(indexScalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage(ILinVectorSingleGradeStorage<T> gradeIndexScalarList)
        {
            return new VectorStorage<T>(gradeIndexScalarList);
        }


        public override uint MinVSpaceDimension 
            => VSpaceDimension 
                ??= GetLinVectorIndexScalarStorage().GetMinVSpaceDimensionOfVector();


        internal VectorStorage() 
            : base(1)
        {
        }
        
        private VectorStorage(KeyValuePair<ulong, T> indexScalarPair)
            : base(1, indexScalarPair)
        {
        }
        
        private VectorStorage(ILinVectorSingleScalarStorage<T> singleScalarVectorStorage)
            : base(1, singleScalarVectorStorage)
        {
        }
        
        private VectorStorage(ILinVectorStorage<T> singleScalarVectorStorage)
            : base(1, singleScalarVectorStorage)
        {
        }

        private VectorStorage(ILinVectorSingleGradeStorage<T> gradeIndexScalarList)
            : base(gradeIndexScalarList.Grade == 1 ? gradeIndexScalarList : throw new ArgumentException())
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorSingleGradeStorage<T> GetLinVectorSingleGradeStorage()
        {
            return SingleGradeVectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTermByIndex(int index, out BasisTerm<T> term)
        {
            var i = (ulong) index;

            if (GetLinVectorIndexScalarStorage().TryGetScalar(i, out var value))
            {
                term = value.CreateBasisVectorTerm(i);
                return true;
            }

            term = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTermByIndex(ulong index, out BasisTerm<T> term)
        {
            if (GetLinVectorIndexScalarStorage().TryGetScalar(index, out var value))
            {
                term = value.CreateBasisVectorTerm(index);
                return true;
            }

            term = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTerm(ulong id, out BasisTerm<T> term)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            if (grade == 1 && GetLinVectorIndexScalarStorage().TryGetScalar(index, out var value))
            {
                term = value.CreateBasisVectorTerm(index);
                return true;
            }

            term = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTerm(uint grade, ulong index, out BasisTerm<T> term)
        {
            if (grade == 1 && GetLinVectorIndexScalarStorage().TryGetScalar(index, out var value))
            {
                term = value.CreateBasisVectorTerm(index);
                return true;
            }

            term = null;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorCopy()
        {
            var indexScalarDictionary = 
                GetLinVectorIndexScalarStorage().GetCopy();

            return CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T2> MapVectorScalars<T2>(Func<T, T2> scalarMapping)
        {
            var indexScalarDictionary = 
                GetLinVectorIndexScalarStorage().MapScalars(scalarMapping);

            return VectorStorage<T2>.CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T2> MapVectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var indexScalarDictionary = 
                GetLinVectorIndexScalarStorage().MapScalars(
                    (index, scalar) => 
                        idScalarMapping(index.BasisVectorIndexToId(), scalar)
                );

            return VectorStorage<T2>.CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T2> MapVectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var indexScalarDictionary = 
                GetLinVectorIndexScalarStorage().MapScalars(indexScalarMapping);

            return VectorStorage<T2>.CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T2> MapVectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var indexScalarDictionary = 
                GetLinVectorIndexScalarStorage().MapScalars(
                    (index, scalar) => 
                        gradeIndexScalarMapping(1U, index, scalar)
                );

            return VectorStorage<T2>.CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> FilterVectorByScalar(Func<T, bool> scalarFilter)
        {
            var indexScalarDictionary =
                GetLinVectorIndexScalarStorage().FilterByScalar(scalarFilter);

            return CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> FilterVectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter)
        {
            var indexScalarDictionary =
                GetLinVectorIndexScalarStorage().FilterByIndexScalar(indexScalarFilter);

            return CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> FilterVectorByIndex(Func<ulong, bool> indexFilter)
        {
            var indexScalarDictionary =
                GetLinVectorIndexScalarStorage().FilterByIndex(indexFilter);

            return CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorage<T> GetVectorPart()
        {
            return IsEmpty()
                ? ZeroVector
                : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorage<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => scalarSelection(pair.Scalar)
                ).CreateDictionary();

            return CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorage<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => indexScalarSelection(pair.Index, pair.Scalar)
                ).CreateDictionary();

            return CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorage<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => indexSelection(pair.Index)
                ).CreateDictionary();

            return CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> GetBivectorPart()
        {
            return ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            return ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            return ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> GetKVectorPart(uint grade)
        {
            return grade == 1 && !IsEmpty()
                ? this
                : CreateKVectorZero(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            return grade == 1
                ? GetVectorPart(scalarSelection)
                : CreateKVectorZero(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            return grade == 1
                ? GetVectorPart(indexScalarSelection)
                : CreateKVectorZero(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            return grade == 1
                ? GetVectorPart(indexSelection)
                : CreateKVectorZero(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            var indexScalarDictionary =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => idSelection(pair.Index.BasisVectorIndexToId())
                ).CreateDictionary();

            return CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var indexScalarDictionary =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => gradeIndexSelection(1, pair.Index)
                ).CreateDictionary();

            return CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => scalarSelection(pair.Scalar)
                ).CreateDictionary();

            return CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var indexScalarDictionary =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => idScalarSelection(pair.Index.BasisVectorIndexToId(), pair.Scalar)
                ).CreateDictionary();

            return CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var indexScalarDictionary =
                GetLinVectorIndexScalarStorage().GetIndexScalarRecords().Where(
                    pair => gradeIndexScalarSelection(1, pair.Index, pair.Scalar)
                ).CreateDictionary();

            return CreateVectorStorage(indexScalarDictionary);
        }

        public override Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (indexSelection(index))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                indexScalarDictionary1.CreateVectorStorage(),
                indexScalarDictionary2.CreateVectorStorage()
            );
        }

        public override Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (indexScalarSelection(index, scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                indexScalarDictionary1.CreateVectorStorage(),
                indexScalarDictionary2.CreateVectorStorage()
            );
        }

        public override Tuple<VectorStorage<T>, VectorStorage<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (scalarSelection(scalar))
                    indexScalarDictionary1.Add(index, scalar);
                else
                    indexScalarDictionary2.Add(index, scalar);
            }

            return new Tuple<VectorStorage<T>, VectorStorage<T>>(
                indexScalarDictionary1.CreateVectorStorage(),
                indexScalarDictionary2.CreateVectorStorage()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<BasisBlade> GetBasisBlades()
        {
            return GetLinVectorIndexScalarStorage().GetIndices().Select(index => 
                (BasisBlade) index.CreateBasisVector()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<BasisTerm<T>> GetTerms()
        {
            return GetLinVectorIndexScalarStorage()
                .GetIndexScalarRecords()
                .Select(pair => pair.Scalar.CreateBasisVectorTerm(pair.Index));
        }

        public override IEnumerable<BasisTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                var id = index.BasisVectorIndexToId();

                if (idSelection(id))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        public override IEnumerable<BasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (gradeIndexSelection(1, index))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        public override IEnumerable<BasisTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (scalarSelection(scalar))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        public override IEnumerable<BasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                var id = index.BasisVectorIndexToId();

                if (idScalarSelection(id, scalar))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        public override IEnumerable<BasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (index, scalar) in GetLinVectorIndexScalarStorage().GetIndexScalarRecords())
            {
                if (gradeIndexScalarSelection(1, index, scalar))
                    yield return scalar.CreateBasisVectorTerm(index);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorStorage()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorStorageCopy()
        {
            return CreateVectorStorage(GetLinVectorIndexScalarStorage().GetCopy());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorStorageCopy(Func<T, T> scalarMapping)
        {
            return CreateVectorStorage(GetLinVectorIndexScalarStorage().MapScalars(scalarMapping));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return this.GetMultivectorText();
        }
    }
}