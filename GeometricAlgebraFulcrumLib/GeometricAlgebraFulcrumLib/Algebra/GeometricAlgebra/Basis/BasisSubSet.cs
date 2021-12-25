using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis
{
    public sealed record BasisSubSet : 
        IGeometricAlgebraSpace
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BasisSubSet Create(uint vSpaceDimension)
        {
            if (vSpaceDimension < 1)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            return new BasisSubSet(((ulong) vSpaceDimension).GetRange().ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BasisSubSet Create(IReadOnlyList<ulong> basisVectorIndices)
        {
            if (basisVectorIndices.Count < 1)
                throw new ArgumentException(nameof(basisVectorIndices));

            return new BasisSubSet(basisVectorIndices);
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

        public IEnumerable<BasisVector> BasisVectors
            => BasisVectorIndices.Select(index => index.CreateBasisVector());


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BasisSubSet([NotNull] IReadOnlyList<ulong> basisVectorIndices)
        {
            BasisVectorIndices = basisVectorIndices;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BasisScalar GetBasisScalar()
        {
            return BasisScalar.DefaultBasisScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BasisVector GetBasisVector(int index)
        {
            return BasisVector.Create(BasisVectorIndices[index]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BasisVector GetBasisVector(ulong index)
        {
            return BasisVector.Create(BasisVectorIndices[(int) index]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BasisBivector GetBasisBivector(int index)
        {
            var basisVectorIndices = 
                ((ulong) index)
                    .BasisBivectorIndexToId()
                    .BasisBladeIdToBasisVectorIndices()
                    .Select(i => BasisVectorIndices[(int) i])
                    .ToArray();

            return BasisBivector.Create(basisVectorIndices[0], basisVectorIndices[1]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BasisBivector GetBasisBivector(ulong index)
        {
            var basisVectorIndices = 
                index
                    .BasisBivectorIndexToId()
                    .BasisBladeIdToBasisVectorIndices()
                    .Select(i => BasisVectorIndices[(int) i])
                    .ToArray();

            return BasisBivector.Create(basisVectorIndices[0], basisVectorIndices[1]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BasisBlade GetBasisBlade(ulong id)
        {
            if (id == 0)
                return BasisScalar.DefaultBasisScalar;

            if (id > MaxBasisBladeId)
                throw new ArgumentOutOfRangeException(nameof(id));

            return id
                .BasisBladeIdToBasisVectorIndices()
                .Select(basisVectorIndex => BasisVectorIndices[(int) basisVectorIndex])
                .CreateBasisBlade();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BasisBlade GetBasisBlade(uint grade, ulong index)
        {
            if (grade == 0)
                return index == 0
                    ? BasisScalar.DefaultBasisScalar
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