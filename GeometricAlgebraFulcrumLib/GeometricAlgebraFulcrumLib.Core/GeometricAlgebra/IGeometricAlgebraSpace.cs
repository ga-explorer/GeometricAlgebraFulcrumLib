namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra;

public interface IGeometricAlgebraSpace
{
    uint VSpaceDimensions { get; }

    ulong GaSpaceDimensions { get; }

    ulong MaxBasisBladeId { get; }

    uint GradesCount { get; }

    IEnumerable<uint> Grades { get; }
}