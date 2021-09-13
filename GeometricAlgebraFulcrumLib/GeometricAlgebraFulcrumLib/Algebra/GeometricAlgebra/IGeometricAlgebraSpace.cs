using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra
{
    public interface IGeometricAlgebraSpace
    {
        uint VSpaceDimension { get; }

        ulong GaSpaceDimension { get; }

        ulong MaxBasisBladeId { get; }

        uint GradesCount { get; }

        IEnumerable<uint> Grades { get; }
    }
}