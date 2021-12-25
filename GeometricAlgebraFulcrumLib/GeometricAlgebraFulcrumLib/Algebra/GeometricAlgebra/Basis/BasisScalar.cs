using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis
{
    public sealed record BasisScalar
        : BasisBlade
    {
        public static BasisScalar DefaultBasisScalar { get; }
            = new BasisScalar();


        public override ulong Id => 0UL;

        public override uint Grade => 0U;

        public override ulong Index => 0UL;

        public override bool IsScalar => true;

        public override bool IsVector => false;

        public override bool IsBivector => false;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BasisScalar()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<ulong> GetBasisVectorIndices()
        {
            return Enumerable.Empty<ulong>();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return new StringBuilder()
                .Append('<')
                .Append(0)
                .Append('>')
                .ToString();
        }
    }
}