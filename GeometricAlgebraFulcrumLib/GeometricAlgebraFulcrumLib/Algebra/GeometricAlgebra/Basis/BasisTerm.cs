using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis
{
    public sealed record BasisTerm<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisTerm<T> Create(BasisBlade basisBlade, T scalar)
        {
            return new BasisTerm<T>(
                basisBlade,
                scalar
            );
        }


        public BasisBlade BasisBlade { get; }

        public T Scalar { get; }

        public ulong Id => BasisBlade.Id;

        public uint Grade => BasisBlade.Grade;

        public ulong Index => BasisBlade.Index;

        public bool IsScalar => BasisBlade.IsScalar;

        public bool IsVector => BasisBlade.IsVector;

        public bool IsBivector => BasisBlade.IsBivector;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BasisTerm([NotNull] BasisBlade basisBlade, [NotNull] T scalar)
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
        public IEnumerable<BasisVector> GetBasisVectors()
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
        public BasisTerm<T2> CreateTerm<T2>(T2 scalar)
        {
            return BasisTerm<T2>.Create(BasisBlade, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"'{Scalar}'{BasisBlade}";
        }
    }
}