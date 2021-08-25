using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures;

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


        public GaRecordGradeKey GetGradeIndexRecord()
        {
            return new GaRecordGradeKey(Grade, Index);
        }

        public abstract IEnumerable<ulong> GetBasisVectorIndices();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaBasisVector> GetBasisVectors()
        {
            return GetBasisVectorIndices().Select(GaBasisVector.Create);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordGradeKey GetGradeIndexRecord<T>()
        {
            return new GaRecordGradeKey(Grade, Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordKeyValue<T> GetIdScalarRecord<T>([NotNull] T scalar)
        {
            return new GaRecordKeyValue<T>(Id, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordKeyValue<T> GetIndexScalarRecord<T>([NotNull] T scalar)
        {
            return new GaRecordKeyValue<T>(Index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordGradeKeyValue<T> GetGradeIndexScalarRecord<T>([NotNull] T scalar)
        {
            return new GaRecordGradeKeyValue<T>(Grade, Index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBasisTerm<T> CreateTerm<T>(T scalar)
        {
            return GaBasisTerm<T>.Create(this, scalar);
        }
    }
}