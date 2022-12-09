using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis
{
    public sealed record BasisVector 
        : BasisBlade
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BasisVector Create(ulong index)
        {
            return new BasisVector(index);
        }


        public override ulong Id 
            => Index.BasisVectorIndexToId();

        public override uint Grade => 1;

        public override ulong Index { get; }

        public override bool IsScalar 
            => false;

        public override bool IsVector 
            => true;

        public override bool IsBivector 
            => false;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BasisVector(ulong index)
        {
            Index = index;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<ulong> GetBasisVectorIndices()
        {
            yield return Index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return new StringBuilder()
                .Append('<')
                .Append(Index)
                .Append('>')
                .ToString();
        }
    }
}