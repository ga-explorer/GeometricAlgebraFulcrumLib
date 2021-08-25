using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public sealed record GaStorageScalar<T>
        : GaStorageKVectorBase<T>, IGaStorageScalar<T>
    {
        public static GaStorageScalar<T> ZeroScalar { get; }
            = new GaStorageScalar<T>();

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> Create(T scalar)
        {
            return new GaStorageScalar<T>(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> Create(IGaListEvenSingleKey<T> singleKeyList)
        {
            return new GaStorageScalar<T>(singleKeyList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> Create(IGaListGradedSingleGrade<T> singleGradeIndexScalarList)
        {
            return new GaStorageScalar<T>(singleGradeIndexScalarList);
        }


        public override IGaListGradedSingleGrade<T> SingleGradeIndexScalarList { get; }

        public override uint MinVSpaceDimension 
            => 0U;
        
        
        private GaStorageScalar() 
        {
            SingleGradeIndexScalarList = new GaListGradedSingleGradeEmpty<T>(0);
        }

        private GaStorageScalar([NotNull] T scalar) 
        {
            SingleGradeIndexScalarList = new GaListGradedSingleGradeKey<T>(0, 0, scalar);
        }

        private GaStorageScalar([NotNull] IGaListEvenSingleKey<T> singleKeyList)
        {
            if (singleKeyList.Key != 0)
                throw new ArgumentException();

            SingleGradeIndexScalarList = new GaListGradedSingleGradeKey<T>(0, singleKeyList);
        }

        private GaStorageScalar([NotNull] IGaListGradedSingleGrade<T> singleGradeIndexScalarList)
        {
            if (singleGradeIndexScalarList.Grade != 0)
                throw new ArgumentException();

            var count = singleGradeIndexScalarList.GetSparseCount();

            if (count > 1)
                throw new ArgumentException();

            if (count == 1 && !singleGradeIndexScalarList.ContainsKey(0, 0))
                throw new ArgumentException();

            SingleGradeIndexScalarList = singleGradeIndexScalarList;
        }


        public override bool TryGetScalar(out T value)
        {
            if (SingleGradeIndexScalarList is GaListGradedSingleGradeKey<T> gradedList)
            {
                value = gradedList.Value;
                return true;
            }
            
            value = default;
            return false;
        }

        public override bool TryGetTermByIndex(int index, out GaBasisTerm<T> term)
        {
            if (index == 0 && IndexScalarList.TryGetValue((ulong) index, out var value))
            {
                term = value.CreateBasisScalarTerm();
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaBasisTerm<T> term)
        {
            if (index == 0 && IndexScalarList.TryGetValue(index, out var value))
            {
                term = value.CreateBasisScalarTerm();
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaBasisTerm<T> term)
        {
            if (id == 0 && IndexScalarList.TryGetValue(0, out var value))
            {
                term = value.CreateBasisScalarTerm();
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term)
        {
            if (grade == 0 && index == 0 && IndexScalarList.TryGetValue(0, out var value))
            {
                term = value.CreateBasisScalarTerm();
                return true;
            }

            term = null;
            return false;
        }

        public override IEnumerable<GaBasisBlade> GetBasisBlades()
        {
            if (!IndexScalarList.IsEmpty())
                yield return GaBasisBladeFactory.BasisScalar;
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms()
        {
            if (!IndexScalarList.IsEmpty())
                yield return IndexScalarList.GetMinKeyValue().CreateBasisScalarTerm();
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, bool> idSelection)
        {
            if (!IndexScalarList.IsEmpty() && idSelection(0))
                yield return IndexScalarList.GetMinKeyValue().CreateBasisScalarTerm();
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, bool> gradeIndexSelection)
        {
            if (!IndexScalarList.IsEmpty() && gradeIndexSelection(0, 0))
                yield return IndexScalarList.GetMinKeyValue().CreateBasisScalarTerm();
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<T, bool> scalarSelection)
        {
            if (!IndexScalarList.IsEmpty())
            {
                var scalar = IndexScalarList.GetMinKeyValue();

                if (scalarSelection(scalar))
                    yield return scalar.CreateBasisScalarTerm();
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<ulong, T, bool> idScalarSelection)
        {
            if (!IndexScalarList.IsEmpty())
            {
                var scalar = IndexScalarList.GetMinKeyValue();

                if (idScalarSelection(0, scalar))
                    yield return scalar.CreateBasisScalarTerm();
            }
        }

        public override IEnumerable<GaBasisTerm<T>> GetTerms(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            if (!IndexScalarList.IsEmpty())
            {
                var scalar = IndexScalarList.GetMinKeyValue();

                if (gradeIndexScalarSelection(0, 0, scalar))
                    yield return scalar.CreateBasisScalarTerm();
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

            return TryGetScalar(out var scalar) && scalarSelection(scalar)
                ? this
                : ZeroScalar;
        }

        public override IGaStorageKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            if (grade != 0)
                return GaStorageKVector<T>.ZeroKVector(grade);

            return TryGetScalar(out var scalar) && indexScalarSelection(0, scalar)
                ? this
                : ZeroScalar;
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
            return TryGetScalar(out var scalar) && scalarSelection(scalar)
                ? this
                : ZeroScalar;
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            return TryGetScalar(out var scalar) && idScalarSelection(0, scalar)
                ? this
                : ZeroScalar;
        }

        public override IGaStorageMultivector<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            return TryGetScalar(out var scalar) && gradeIndexScalarSelection(0, 0, scalar)
                ? this
                : ZeroScalar;
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


        public IGaStorageScalar<T> GetScalarCopy()
        {
            return this;
        }

        public IGaStorageScalar<T2> MapScalar<T2>(Func<T, T2> scalarMapping)
        {
            return SingleGradeIndexScalarList.TryGetValue(0, 0, out var scalar)
                ? new GaStorageScalar<T2>(scalarMapping(scalar))
                : GaStorageScalar<T2>.ZeroScalar;
        }

        public IGaStorageScalar<T2> MapScalarById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            return SingleGradeIndexScalarList.TryGetValue(0, 0, out var scalar)
                ? new GaStorageScalar<T2>(idScalarMapping(0, scalar))
                : GaStorageScalar<T2>.ZeroScalar;
        }

        public IGaStorageScalar<T2> MapScalarByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            return SingleGradeIndexScalarList.TryGetValue(0, 0, out var scalar)
                ? new GaStorageScalar<T2>(indexScalarMapping(0, scalar))
                : GaStorageScalar<T2>.ZeroScalar;
        }

        public IGaStorageScalar<T2> MapScalarByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return SingleGradeIndexScalarList.TryGetValue(0, 0, out var scalar)
                ? new GaStorageScalar<T2>(gradeIndexScalarMapping(0, 0, scalar))
                : GaStorageScalar<T2>.ZeroScalar;
        }
        

        public IGaStorageScalar<T> FilterScalarByScalar(Func<T, bool> scalarFilter)
        {
            return !TryGetScalar(out var scalar) || scalarFilter(scalar) 
                ? this 
                : GaStorageScalar<T>.ZeroScalar;
        }

        public IGaStorageScalar<T> FilterScalarByIndexScalar(Func<ulong, T, bool> indexScalarFilter)
        {
            return !TryGetScalar(out var scalar) || indexScalarFilter(0, scalar) 
                ? this 
                : GaStorageScalar<T>.ZeroScalar;
        }

        public IGaStorageScalar<T> FilterScalarByIndex(Func<ulong, bool> indexFilter)
        {
            return !ContainsScalarPart() || indexFilter(0) 
                ? this 
                : GaStorageScalar<T>.ZeroScalar;
        }
    }
}