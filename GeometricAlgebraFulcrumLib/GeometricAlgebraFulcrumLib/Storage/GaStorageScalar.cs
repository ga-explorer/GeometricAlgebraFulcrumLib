using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public sealed record GaStorageScalar<T>
        : GaStorageKVectorBase<T>, IGaStorageScalar<T>
    {
        public static GaStorageScalar<T> ZeroScalar { get; }
            = new GaStorageScalar<T>(
                GaEvenDictionaryEmpty<T>.DefaultDictionary
            );

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> Create(T scalar)
        {
            return new GaStorageScalar<T>(
                scalar.CreateEvenDictionarySingleZeroKey()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> Create(IEnumerable<T> scalarList)
        {
            var scalarArray = scalarList.Take(1).ToArray();

            return scalarArray.Length == 1
                ? new GaStorageScalar<T>(scalarArray[0].CreateEvenDictionarySingleZeroKey())
                : ZeroScalar;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> Create(IReadOnlyList<T> scalarList)
        {
            return scalarList.Count == 1
                ? new GaStorageScalar<T>(scalarList[0].CreateEvenDictionarySingleZeroKey())
                : ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> Create(IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            return indexScalarDictionary.TryGetValue(0, out var scalar)
                ? new GaStorageScalar<T>(scalar.CreateEvenDictionarySingleZeroKey())
                : ZeroScalar;
        }


        public override uint Grade 
            => 0U;


        private GaStorageScalar(IGaEvenDictionary<T> indexScalarDictionary) 
            : base(indexScalarDictionary, 0UL)
        {
        }

        
        public override bool TryGetTermByIndex(int index, out GaTerm<T> term)
        {
            if (index == 0 && IndexScalarDictionary.TryGetValue((ulong) index, out var value))
            {
                term = GaTerm<T>.CreateScalar(value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaTerm<T> term)
        {
            if (index == 0 && IndexScalarDictionary.TryGetValue(index, out var value))
            {
                term = GaTerm<T>.CreateScalar(value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaTerm<T> term)
        {
            if (id == 0 && IndexScalarDictionary.TryGetValue(0, out var value))
            {
                term = GaTerm<T>.CreateScalar(value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term)
        {
            if (grade == 0 && index == 0 && IndexScalarDictionary.TryGetValue(0, out var value))
            {
                term = GaTerm<T>.CreateScalar(value);
                return true;
            }

            term = null;
            return false;
        }

        public override IEnumerable<IGaBasisBlade> GetBasisBlades()
        {
            if (!IndexScalarDictionary.IsEmpty())
                yield return GaBasisFactory.BasisScalar;
        }

        public override IEnumerable<GaTerm<T>> GetTerms()
        {
            if (!IndexScalarDictionary.IsEmpty())
                yield return GaTerm<T>.CreateScalar(IndexScalarDictionary.GetFirstValue());
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            if (!IndexScalarDictionary.IsEmpty() && idSelection(0))
                yield return GaTerm<T>.CreateScalar(IndexScalarDictionary.GetFirstValue());
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            if (!IndexScalarDictionary.IsEmpty() && gradeIndexSelection(0, 0))
                yield return GaTerm<T>.CreateScalar(IndexScalarDictionary.GetFirstValue());
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            if (!IndexScalarDictionary.IsEmpty())
            {
                var scalar = IndexScalarDictionary.GetFirstValue();

                if (scalarSelection(scalar))
                    yield return GaTerm<T>.CreateScalar(scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            if (!IndexScalarDictionary.IsEmpty())
            {
                var scalar = IndexScalarDictionary.GetFirstValue();

                if (idScalarSelection(0, scalar))
                    yield return GaTerm<T>.CreateScalar(scalar);
            }
        }

        public override IEnumerable<GaTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            if (!IndexScalarDictionary.IsEmpty())
            {
                var scalar = IndexScalarDictionary.GetFirstValue();

                if (gradeIndexScalarSelection(0, 0, scalar))
                    yield return GaTerm<T>.CreateScalar(scalar);
            }
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
            return grade == 0 && !IsEmpty()
                ? this
                : GaStorageKVector<T>.ZeroKVector(grade);
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            if (grade != 0)
                return GaStorageKVector<T>.ZeroKVector(grade);

            return IsEmpty() || !scalarSelection(FirstScalar)
                ? ZeroScalar
                : this;
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            if (grade != 0)
                return GaStorageKVector<T>.ZeroKVector(grade);

            return IsEmpty() || !indexScalarSelection(0, FirstScalar)
                ? ZeroScalar
                : this;
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            if (grade != 0)
                return GaStorageKVector<T>.ZeroKVector(grade);

            return IsEmpty() || !indexSelection(0)
                ? ZeroScalar
                : this;
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            return IsEmpty() || !idSelection(0)
                ? ZeroScalar
                : this;
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            return IsEmpty() || !gradeIndexSelection(0, 0)
                ? ZeroScalar
                : this;
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            return IsEmpty() || !scalarSelection(FirstScalar)
                ? ZeroScalar
                : this;

        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            return IsEmpty() || !idScalarSelection(0, FirstScalar)
                ? ZeroScalar
                : this;
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            return IsEmpty() || !gradeIndexScalarSelection(0, 0, FirstScalar)
                ? ZeroScalar
                : this;
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

        public override IGaStorageKVector<T> GetComputedCopy(Func<T, T> mappingFunc)
        {
            return IsEmpty()
                ? ZeroScalar
                : GaStorageScalar<T>.Create(mappingFunc(FirstScalar));
        }

        public override IGaStorageKVector<T> GetComputedCopy(Func<ulong, T, T> mappingFunc)
        {
            return IsEmpty()
                ? ZeroScalar
                : GaStorageScalar<T>.Create(mappingFunc(0, FirstScalar));
        }

        public override IGaStorageKVector<T> GetComputedCopy(Func<uint, ulong, T, T> mappingFunc)
        {
            return IsEmpty()
                ? ZeroScalar
                : GaStorageScalar<T>.Create(mappingFunc(0, 0, FirstScalar));
        }


        public GaStorageScalar<T> GetStorageCopy()
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.GetCopy();

            return new GaStorageScalar<T>(
                indexScalarDictionary
            );
        }

        public GaStorageScalar<T2> MapScalars<T2>(Func<T, T2> scalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(scalarMapping);

            return new GaStorageScalar<T2>(
                indexScalarDictionary
            );
        }

        public GaStorageScalar<T2> MapScalarsById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(
                    (index, scalar) => 
                        idScalarMapping(1UL << (int) index, scalar)
                );

            return new GaStorageScalar<T2>(
                indexScalarDictionary
            );
        }

        public GaStorageScalar<T2> MapScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(indexScalarMapping);

            return new GaStorageScalar<T2>(
                indexScalarDictionary
            );
        }

        public GaStorageScalar<T2> MapScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            var indexScalarDictionary = 
                IndexScalarDictionary.MapValues(
                    (index, scalar) => 
                        gradeIndexScalarMapping(0U, index, scalar)
                );

            return new GaStorageScalar<T2>(
                indexScalarDictionary
            );
        }

    }
}