using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis
{
    public sealed record BasisBivector 
        : BasisBlade
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BasisBivector Create(ulong index)
        {
            var (basisVectorIndex1, basisVectorIndex2) = 
                index.BasisBivectorIndexToVectorIndices();

            return new BasisBivector(
                basisVectorIndex1,
                basisVectorIndex2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BasisBivector Create(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            return new BasisBivector(
                basisVectorIndex1,
                basisVectorIndex2
            );
        }


        public ulong BasisVectorIndex1 { get; }

        public ulong BasisVectorIndex2 { get; }

        public override ulong Id 
            => BasisBivectorUtils.BasisVectorIndicesToBivectorId(
                BasisVectorIndex1, 
                BasisVectorIndex2
            );

        public override uint Grade => 2;

        public override ulong Index 
            => BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                BasisVectorIndex1, 
                BasisVectorIndex2
            );

        public override bool IsScalar => false;

        public override bool IsVector => false;

        public override bool IsBivector => true;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BasisBivector(ulong basisVectorIndex1, ulong basisVectorIndex2)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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