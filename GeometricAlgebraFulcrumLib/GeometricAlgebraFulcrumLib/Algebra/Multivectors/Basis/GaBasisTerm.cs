using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis
{
    public sealed record GaBasisTerm<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisTerm<T> Create(GaBasisBlade basisBlade, T scalar)
        {
            return new GaBasisTerm<T>(
                basisBlade,
                scalar
            );
        }


        public GaBasisBlade BasisBlade { get; }

        public T Scalar { get; }

        public ulong Id => BasisBlade.Id;

        public uint Grade => BasisBlade.Grade;

        public ulong Index => BasisBlade.Index;

        public bool IsScalar => BasisBlade.IsScalar;

        public bool IsVector => BasisBlade.IsVector;

        public bool IsBivector => BasisBlade.IsBivector;


        private GaBasisTerm([NotNull] GaBasisBlade basisBlade, [NotNull] T scalar)
        {
            BasisBlade = basisBlade;
            Scalar = scalar;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetBasisVectorIndices()
        {
            return BasisBlade.GetBasisVectorIndices();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaBasisVector> GetBasisVectors()
        {
            return BasisBlade.GetBasisVectors();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GradeIndexRecord GetGradeIndexRecord()
        {
            return new GradeIndexRecord(BasisBlade.Grade, BasisBlade.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexScalarRecord<T> GetIdScalarRecord()
        {
            return new IndexScalarRecord<T>(BasisBlade.Id, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexScalarRecord<T> GetIndexScalarRecord()
        {
            return new IndexScalarRecord<T>(BasisBlade.Index, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GradeIndexScalarRecord<T> GetGradeIndexScalarRecord()
        {
            return new GradeIndexScalarRecord<T>(BasisBlade.Grade, BasisBlade.Index, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBasisTerm<T2> CreateTerm<T2>(T2 scalar)
        {
            return GaBasisTerm<T2>.Create(BasisBlade, scalar);
        }


        public override string ToString()
        {
            return $"'{Scalar}'{BasisBlade}";
        }
    }
}