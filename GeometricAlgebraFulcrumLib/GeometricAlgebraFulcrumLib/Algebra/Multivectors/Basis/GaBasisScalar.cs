using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis
{
    public sealed record GaBasisScalar
        : GaBasisBlade
    {
        public static GaBasisScalar BasisScalar { get; }
            = new GaBasisScalar();


        public override ulong Id => 0UL;

        public override uint Grade => 0U;

        public override ulong Index => 0UL;

        public override bool IsScalar => true;

        public override bool IsVector => false;

        public override bool IsBivector => false;


        private GaBasisScalar()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<ulong> GetBasisVectorIndices()
        {
            return Enumerable.Empty<ulong>();
        }


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