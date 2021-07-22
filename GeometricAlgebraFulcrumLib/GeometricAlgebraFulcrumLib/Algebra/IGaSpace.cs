using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra
{
    public interface IGaSpace
    {
        uint VSpaceDimension { get; }

        ulong GaSpaceDimension { get; }

        ulong MaxBasisBladeId { get; }

        uint GradesCount { get; }

        IEnumerable<uint> Grades { get; }
    }
}