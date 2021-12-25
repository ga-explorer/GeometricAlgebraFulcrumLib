using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis
{
    public abstract record BasisBlade
    {
        public abstract ulong Id { get; }

        public abstract uint Grade { get; }

        public abstract ulong Index { get; }
        
        public abstract bool IsScalar { get; }
        
        public abstract bool IsVector { get; }

        public abstract bool IsBivector { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GradeIndexRecord GetGradeIndexRecord()
        {
            return new GradeIndexRecord(Grade, Index);
        }

        public abstract IEnumerable<ulong> GetBasisVectorIndices();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<BasisVector> GetBasisVectors()
        {
            return GetBasisVectorIndices().Select(BasisVector.Create);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GradeIndexRecord GetGradeIndexRecord<T>()
        {
            return new GradeIndexRecord(Grade, Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexScalarRecord<T> GetIdScalarRecord<T>([NotNull] T scalar)
        {
            return new IndexScalarRecord<T>(Id, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexScalarRecord<T> GetIndexScalarRecord<T>([NotNull] T scalar)
        {
            return new IndexScalarRecord<T>(Index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GradeIndexScalarRecord<T> GetGradeIndexScalarRecord<T>([NotNull] T scalar)
        {
            return new GradeIndexScalarRecord<T>(Grade, Index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BasisTerm<T> CreateTerm<T>(T scalar)
        {
            return BasisTerm<T>.Create(this, scalar);
        }
    }
}