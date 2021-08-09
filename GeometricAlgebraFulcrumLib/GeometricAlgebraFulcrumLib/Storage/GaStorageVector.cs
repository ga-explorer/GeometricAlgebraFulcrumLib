using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;


namespace GeometricAlgebraFulcrumLib.Storage
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
            = new GaStorageVector<T>(
                GaEvenDictionaryEmpty<T>.DefaultDictionary, 
                0UL
            );


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create(ulong index, T scalar)
        {
            var evenDictionary = 
                scalar.CreateEvenDictionarySingleKey(index);

            return new GaStorageVector<T>(
                evenDictionary, 
                1UL << (int) index
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create(params T[] indexScalarList)
        {
            var evenDictionary = 
                indexScalarList.CreateEvenDictionaryList();

            return new GaStorageVector<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId(1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create(IReadOnlyList<T> indexScalarList)
        {
            var evenDictionary = 
                indexScalarList.CreateEvenDictionaryList();

            return new GaStorageVector<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId(1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create(IEnumerable<T> indexScalarList)
        {
            var evenDictionary = 
                indexScalarList.CreateEvenDictionaryList();

            return new GaStorageVector<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId(1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create(Dictionary<ulong, T> indexScalarDictionary)
        {
            var evenDictionary = 
                indexScalarDictionary.CreateEvenDictionary();

            return new GaStorageVector<T>(
                evenDictionary, 
                evenDictionary.GetMaxBasisBladeId(1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Create(IGaEvenDictionary<T> indexScalarDictionary)
        {
            return new GaStorageVector<T>(
                indexScalarDictionary, 
                indexScalarDictionary.GetMaxBasisBladeId(1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GaStorageVector<T> Create(IGaEvenDictionary<T> indexScalarDictionary, ulong maxBasisBladeId)
        {
            return new GaStorageVector<T>(
                indexScalarDictionary, 
                maxBasisBladeId
            );
        }


        public override uint Grade 
            => 1;


        private GaStorageVector([NotNull] IGaEvenDictionary<T> indexScalarDictionary, ulong maxBasisBladeId)
            : base(indexScalarDictionary, maxBasisBladeId)
        {
        }

        
        public override bool TryGetTermByIndex(int index, out GaTerm<T> term)
        {
            var i = (ulong) index;

            if (IndexScalarDictionary.TryGetValue(i, out var value))
            {
                term = GaTerm<T>.CreateVector(i, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaTerm<T> term)
        {
            if (IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateVector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaTerm<T> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == 1 && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateVector(index, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term)
        {
            if (grade == 1 && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateVector(index, value);
                return true;
            }

            term = null;
            return false;
        }


        public override IGaStorageKVector<T> GetComputedCopy(Func<T, T> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary
                    .CopyToDictionary(mappingFunc)
                    .CreateEvenDictionary();

            return new GaStorageVector<T>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaStorageKVector<T> GetComputedCopy(Func<ulong, T, T> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => mappingFunc(pair.Key, pair.Value)
                ).CreateEvenDictionary();

            return new GaStorageVector<T>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public override IGaStorageKVector<T> GetComputedCopy(Func<uint, ulong, T, T> mappingFunc)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => mappingFunc(Grade, pair.Key, pair.Value)
                ).CreateEvenDictionary();

            return new GaStorageVector<T>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }


        public GaStorageVector<T> GetStorageCopy()
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.GetCopy();

            return new GaStorageVector<T>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageVector<T2> MapScalars<T2>(Func<T, T2> scalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(scalarMapping);

            return new GaStorageVector<T2>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageVector<T2> MapScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(
                    (index, scalar) => 
                        idScalarMapping(1UL << (int) index, scalar)
                );

            return new GaStorageVector<T2>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageVector<T2> MapScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(indexScalarMapping);

            return new GaStorageVector<T2>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }

        public GaStorageVector<T2> MapScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(
                    (index, scalar) => 
                        gradeIndexScalarMapping(1U, index, scalar)
                );

            return new GaStorageVector<T2>(
                indexScalarDictionary,
                MaxBasisBladeId
            );
        }


        public GaStorageVector<T> GetPart(Func<T, bool> scalarFilter)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.FilterByValue(scalarFilter);

            return new GaStorageVector<T>(
                indexScalarDictionary,
                indexScalarDictionary.GetMaxBasisBladeId(1)
            );
        }

        public GaStorageVector<T> GetPart(Func<ulong, T, bool> indexScalarFilter)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.FilterByKeyValue(indexScalarFilter);

            return new GaStorageVector<T>(
                indexScalarDictionary,
                indexScalarDictionary.GetMaxBasisBladeId(1)
            );
        }

        public GaStorageVector<T> GetPart(Func<ulong, bool> indexFilter)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.FilterByKey(indexFilter);

            return new GaStorageVector<T>(
                indexScalarDictionary,
                indexScalarDictionary.GetMaxBasisBladeId(1)
            );
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
                IndexScalarDictionary.Where(
                    pair => scalarSelection(pair.Value)
                ).CopyToDictionary();

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.Where(
                    pair => indexScalarSelection(pair.Key, pair.Value)
                ).CopyToDictionary();

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.Where(
                    pair => indexSelection(pair.Key)
                ).CopyToDictionary();

            return GaStorageVector<T>.Create(indexScalarDictionary);
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
                IndexScalarDictionary.Where(
                    pair => idSelection(1UL << (int) pair.Key)
                ).CopyToDictionary();

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.Where(
                    pair => gradeIndexSelection(1, pair.Key)
                ).CopyToDictionary();

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.Where(
                    pair => scalarSelection(pair.Value)
                ).CopyToDictionary();

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.Where(
                    pair => idScalarSelection(1UL << (int) pair.Key, pair.Value)
                ).CopyToDictionary();

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            var indexScalarDictionary =
                IndexScalarDictionary.Where(
                    pair => gradeIndexScalarSelection(1, pair.Key, pair.Value)
                ).CopyToDictionary();

            return GaStorageVector<T>.Create(indexScalarDictionary);
        }

        public override Tuple<IGaStorageVector<T>, IGaStorageVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            var indexScalarDictionary1 = new Dictionary<ulong, T>();
            var indexScalarDictionary2 = new Dictionary<ulong, T>();

            foreach (var (index, scalar) in IndexScalarDictionary)
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

            foreach (var (index, scalar) in IndexScalarDictionary)
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

            foreach (var (index, scalar) in IndexScalarDictionary)
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
        
        public override IEnumerable<IGaBasisBlade> GetBasisBlades()
        {
            return IndexScalarDictionary.Keys.Select(index => 
                (IGaBasisBlade) index.CreateBasisVector()
            );
        }
        
        public override IEnumerable<GaTerm<T>> GetTerms()
        {
            return IndexScalarDictionary
                .Select(pair => GaTerm<T>.CreateVector(pair.Key, pair.Value));
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                var id = 1UL << (int) index;

                if (idSelection(id))
                    yield return GaTerm<T>.CreateVector(index, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (gradeIndexSelection(1, index))
                    yield return GaTerm<T>.CreateVector(index, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (scalarSelection(scalar))
                    yield return GaTerm<T>.CreateVector(index, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                var id = 1UL << (int) index;

                if (idScalarSelection(id, scalar))
                    yield return GaTerm<T>.CreateVector(index, scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
            {
                if (gradeIndexScalarSelection(1, index, scalar))
                    yield return GaTerm<T>.CreateVector(index, scalar);
            }
        }

        public IGaStorageVector<T> GetVectorStorage()
        {
            return this;
        }

        public IGaStorageVector<T> GetVectorStorageCopy()
        {
            return GaStorageVector<T>.Create(
                IndexScalarDictionary.GetCopy(),
                MaxBasisBladeId
            );
        }

        public IGaStorageVector<T> GetVectorStorageCopy(Func<T, T> scalarMapping)
        {
            return GaStorageVector<T>.Create(
                IndexScalarDictionary.MapValues(scalarMapping),
                MaxBasisBladeId
            );
        }
    }
}