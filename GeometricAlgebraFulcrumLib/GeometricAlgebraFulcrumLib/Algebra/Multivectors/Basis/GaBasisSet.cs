using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis
{
    public sealed record GaBasisSet : 
        IGaSpace
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GaBasisSet Create(uint vSpaceDimension)
        {
            if (vSpaceDimension < 1)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            return new GaBasisSet(((ulong) vSpaceDimension).GetRange().ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GaBasisSet Create(IReadOnlyList<ulong> basisVectorIndices)
        {
            if (basisVectorIndices.Count < 1)
                throw new ArgumentException(nameof(basisVectorIndices));

            return new GaBasisSet(basisVectorIndices);
        }


        public uint VSpaceDimension 
            => (uint) BasisVectorIndices.Count;

        public ulong GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public ulong MaxBasisBladeId 
            => GaSpaceDimension - 1;

        public uint GradesCount
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();

        public IReadOnlyList<ulong> BasisVectorIndices { get; }

        public IEnumerable<GaBasisVector> BasisVectors
            => BasisVectorIndices.Select(index => index.CreateBasisVector());


        private GaBasisSet([NotNull] IReadOnlyList<ulong> basisVectorIndices)
        {
            BasisVectorIndices = basisVectorIndices;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBasisScalar GetBasisScalar()
        {
            return GaBasisScalar.BasisScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBasisVector GetBasisVector(int index)
        {
            return GaBasisVector.Create(BasisVectorIndices[index]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBasisVector GetBasisVector(ulong index)
        {
            return GaBasisVector.Create(BasisVectorIndices[(int) index]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBasisBivector GetBasisBivector(int index)
        {
            var basisVectorIndices = 
                ((ulong) index)
                    .BasisBivectorIndexToId()
                    .BasisBladeIdToBasisVectorIndices()
                    .Select(i => BasisVectorIndices[(int) i])
                    .ToArray();

            return GaBasisBivector.Create(basisVectorIndices[0], basisVectorIndices[1]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBasisBivector GetBasisBivector(ulong index)
        {
            var basisVectorIndices = 
                index
                    .BasisBivectorIndexToId()
                    .BasisBladeIdToBasisVectorIndices()
                    .Select(i => BasisVectorIndices[(int) i])
                    .ToArray();

            return GaBasisBivector.Create(basisVectorIndices[0], basisVectorIndices[1]);
        }

        public GaBasisBlade GetBasisBlade(ulong id)
        {
            if (id == 0)
                return GaBasisScalar.BasisScalar;

            if (id > MaxBasisBladeId)
                throw new ArgumentOutOfRangeException(nameof(id));

            return id
                .BasisBladeIdToBasisVectorIndices()
                .Select(basisVectorIndex => BasisVectorIndices[(int) basisVectorIndex])
                .CreateBasisBlade();
        }

        public GaBasisBlade GetBasisBlade(uint grade, ulong index)
        {
            if (grade == 0)
                return index == 0
                    ? GaBasisScalar.BasisScalar
                    : throw new ArgumentOutOfRangeException(nameof(index));

            if (grade == 1)
                return BasisVectorIndices[(int) index].CreateBasisVector();

            if (grade > VSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return index
                .BasisBladeIndexToId(grade)
                .BasisBladeIdToBasisVectorIndices()
                .Select(basisVectorIndex => BasisVectorIndices[(int) basisVectorIndex])
                .CreateBasisBlade();
        }
    }
}