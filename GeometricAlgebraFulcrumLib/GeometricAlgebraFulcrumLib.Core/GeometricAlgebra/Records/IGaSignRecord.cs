using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

public interface IGaSignRecord
{
    /// <summary>
    /// The Sign
    /// </summary>
    IntegerSign Sign { get; }
}