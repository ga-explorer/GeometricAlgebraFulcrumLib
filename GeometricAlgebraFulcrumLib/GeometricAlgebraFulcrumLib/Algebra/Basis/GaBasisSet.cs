using System;
using System.Collections.Generic;
using DataStructuresLib.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.Basis
{
    public readonly struct GaBasisSet : 
        IGaSpace
    {
        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => 1UL << (int) VSpaceDimension;

        public ulong MaxBasisBladeId 
            => (1UL << (int) VSpaceDimension) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();


        internal GaBasisSet(uint vSpaceDimension)
        {
            if (vSpaceDimension < 2 || vSpaceDimension > GaSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            VSpaceDimension = vSpaceDimension;
        }
    }
}