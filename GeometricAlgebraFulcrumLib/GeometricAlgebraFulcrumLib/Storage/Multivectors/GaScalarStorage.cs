using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public sealed record GaScalarStorage<T>
        : GaKVectorStorageBase<T>, IGaScalarStorage<T>
    {
        public static GaScalarStorage<T> ZeroScalar { get; }
            = new GaScalarStorage<T>();

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarStorage<T> Create(T scalar)
        {
            return new GaScalarStorage<T>(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarStorage<T> Create(ILaVectorSingleIndexEvenStorage<T> singleKeyList)
        {
            return new GaScalarStorage<T>(singleKeyList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarStorage<T> Create(ILaVectorSingleGradeStorage<T> singleGradeIndexScalarList)
        {
            return new GaScalarStorage<T>(singleGradeIndexScalarList);
        }


        public override ILaVectorSingleGradeStorage<T> SingleGradeIndexScalarList { get; }

        public override uint MinVSpaceDimension 
            => 0U;
        
        
        private GaScalarStorage() 
        {
            SingleGradeIndexScalarList = new LaVectorEmptySingleGradeStorage<T>(0);
        }

        private GaScalarStorage([NotNull] T scalar) 
        {
            SingleGradeIndexScalarList = new LaVectorSingleGradeIndexStorage<T>(0, 0, scalar);
        }

        private GaScalarStorage([NotNull] ILaVectorSingleIndexEvenStorage<T> singleKeyList)
        {
            if (singleKeyList.Index != 0)
                throw new ArgumentException();

            SingleGradeIndexScalarList = new LaVectorSingleGradeIndexStorage<T>(0, singleKeyList);
        }

        private GaScalarStorage([NotNull] ILaVectorSingleGradeStorage<T> singleGradeIndexScalarList)
        {
            if (singleGradeIndexScalarList.Grade != 0)
                throw new ArgumentException();

            var count = singleGradeIndexScalarList.GetSparseCount();

            if (count > 1)
                throw new ArgumentException();

            if (count == 1 && !singleGradeIndexScalarList.ContainsIndex(0, 0))
                throw new ArgumentException();

            SingleGradeIndexScalarList = singleGradeIndexScalarList;
        }


        public override bool TryGetScalar(out T value)
        {
            if (SingleGradeIndexScalarList is LaVectorSingleGradeIndexStorage<T> gradedList)
            {
                value = gradedList.Value;
                return true;
            }
            
            value = default;
            return false;
        }

        public override bool TryGetTermByIndex(int index, out GaBasisTerm<T> term)
        {
            if (index == 0 && IndexScalarList.TryGetScalar((ulong) index, out var value))
            {
                term = value.CreateBasisScalarTerm();
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTermByIndex(ulong index, out GaBasisTerm<T> term)
        {
            if (index == 0 && IndexScalarList.TryGetScalar(index, out var value))
            {
                term = value.CreateBasisScalarTerm();
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaBasisTerm<T> term)
        {
            if (id == 0 && IndexScalarList.TryGetScalar(0, out var value))
            {
                term = value.CreateBasisScalarTerm();
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(uint grade, ulong index, out GaBasisTerm<T> term)
        {
            if (grade == 0 && index == 0 && IndexScalarList.TryGetScalar(0, out var value))
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
            return grade == 0 && !IsEmpty()
                ? this
                : GaKVectorStorage<T>.ZeroKVector(grade);
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            if (grade != 0)
                return GaKVectorStorage<T>.ZeroKVector(grade);

            return TryGetScalar(out var scalar) && scalarSelection(scalar)
                ? this
                : ZeroScalar;
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            if (grade != 0)
                return GaKVectorStorage<T>.ZeroKVector(grade);

            return TryGetScalar(out var scalar) && indexScalarSelection(0, scalar)
                ? this
                : ZeroScalar;
        }

        public override IGaKVectorStorage<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            if (grade != 0)
                return GaKVectorStorage<T>.ZeroKVector(grade);

            return IsEmpty() || !indexSelection(0)
                ? ZeroScalar
                : this;
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            return IsEmpty() || !idSelection(0)
                ? ZeroScalar
                : this;
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            return IsEmpty() || !gradeIndexSelection(0, 0)
                ? ZeroScalar
                : this;
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            return TryGetScalar(out var scalar) && scalarSelection(scalar)
                ? this
                : ZeroScalar;
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            return TryGetScalar(out var scalar) && idScalarSelection(0, scalar)
                ? this
                : ZeroScalar;
        }

        public override IGaMultivectorStorage<T> GetMultivectorPart(Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            return TryGetScalar(out var scalar) && gradeIndexScalarSelection(0, 0, scalar)
                ? this
                : ZeroScalar;
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


        public IGaScalarStorage<T> GetScalarCopy()
        {
            return this;
        }

        public IGaScalarStorage<T2> MapScalar<T2>(Func<T, T2> scalarMapping)
        {
            return SingleGradeIndexScalarList.TryGetScalar(0, 0, out var scalar)
                ? new GaScalarStorage<T2>(scalarMapping(scalar))
                : GaScalarStorage<T2>.ZeroScalar;
        }

        public IGaScalarStorage<T2> MapScalarById<T2>(Func<ulong, T, T2> idScalarMapping)
        {
            return SingleGradeIndexScalarList.TryGetScalar(0, 0, out var scalar)
                ? new GaScalarStorage<T2>(idScalarMapping(0, scalar))
                : GaScalarStorage<T2>.ZeroScalar;
        }

        public IGaScalarStorage<T2> MapScalarByIndex<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            return SingleGradeIndexScalarList.TryGetScalar(0, 0, out var scalar)
                ? new GaScalarStorage<T2>(indexScalarMapping(0, scalar))
                : GaScalarStorage<T2>.ZeroScalar;
        }

        public IGaScalarStorage<T2> MapScalarByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return SingleGradeIndexScalarList.TryGetScalar(0, 0, out var scalar)
                ? new GaScalarStorage<T2>(gradeIndexScalarMapping(0, 0, scalar))
                : GaScalarStorage<T2>.ZeroScalar;
        }
        

        public IGaScalarStorage<T> FilterScalarByScalar(Func<T, bool> scalarFilter)
        {
            return !TryGetScalar(out var scalar) || scalarFilter(scalar) 
                ? this 
                : ZeroScalar;
        }

        public IGaScalarStorage<T> FilterScalarByIndexScalar(Func<ulong, T, bool> indexScalarFilter)
        {
            return !TryGetScalar(out var scalar) || indexScalarFilter(0, scalar) 
                ? this 
                : ZeroScalar;
        }

        public IGaScalarStorage<T> FilterScalarByIndex(Func<ulong, bool> indexFilter)
        {
            return !ContainsScalarPart() || indexFilter(0) 
                ? this 
                : ZeroScalar;
        }
    }
}