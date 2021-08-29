using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis
{
    public sealed record GaBasisVector 
        : GaBasisBlade
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GaBasisVector Create(ulong index)
        {
            return new GaBasisVector(index);
        }


        public override ulong Id => Index.BasisVectorIndexToId();

        public override uint Grade => 1;

        public override ulong Index { get; }

        public override bool IsScalar => false;

        public override bool IsVector => true;

        public override bool IsBivector => false;


        private GaBasisVector(ulong index)
        {
            Index = index;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<ulong> GetBasisVectorIndices()
        {
            yield return Index;
        }

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