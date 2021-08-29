using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis
{
    public abstract record GaBasisBlade
    {
        public abstract ulong Id { get; }

        public abstract uint Grade { get; }

        public abstract ulong Index { get; }
        
        public abstract bool IsScalar { get; }
        
        public abstract bool IsVector { get; }

        public abstract bool IsBivector { get; }


        public GradeIndexRecord GetGradeIndexRecord()
        {
            return new GradeIndexRecord(Grade, Index);
        }

        public abstract IEnumerable<ulong> GetBasisVectorIndices();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaBasisVector> GetBasisVectors()
        {
            return GetBasisVectorIndices().Select(GaBasisVector.Create);
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
        public GaBasisTerm<T> CreateTerm<T>(T scalar)
        {
            return GaBasisTerm<T>.Create(this, scalar);
        }
    }
}