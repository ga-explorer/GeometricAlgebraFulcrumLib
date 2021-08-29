using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis
{
    public sealed record GaBasisBivector 
        : GaBasisBlade
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GaBasisBivector Create(ulong index)
        {
            var (basisVectorIndex1, basisVectorIndex2) = 
                index.BasisBivectorIndexToVectorIndices();

            return new GaBasisBivector(
                basisVectorIndex1,
                basisVectorIndex2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GaBasisBivector Create(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            return new GaBasisBivector(
                basisVectorIndex1,
                basisVectorIndex2
            );
        }


        public ulong BasisVectorIndex1 { get; }

        public ulong BasisVectorIndex2 { get; }

        public override ulong Id 
            => GaBasisBivectorUtils.BasisBivectorId(
                BasisVectorIndex1, 
                BasisVectorIndex2
            );

        public override uint Grade => 2;

        public override ulong Index 
            => GaBasisBivectorUtils.BasisBivectorIndex(
                BasisVectorIndex1, 
                BasisVectorIndex2
            );

        public override bool IsScalar => false;

        public override bool IsVector => false;

        public override bool IsBivector => true;


        private GaBasisBivector(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            Debug.Assert(basisVectorIndex1 < basisVectorIndex2);

            BasisVectorIndex1 = basisVectorIndex1;
            BasisVectorIndex2 = basisVectorIndex2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<ulong> GetBasisVectorIndices()
        {
            yield return BasisVectorIndex1;
            yield return BasisVectorIndex2;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append('<')
                .Append(BasisVectorIndex1)
                .Append(',')
                .Append(BasisVectorIndex2)
                .Append('>')
                .ToString();
        }
    }
}