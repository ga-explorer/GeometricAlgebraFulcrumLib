using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public interface IXGaIdRecord
{
    /// <summary>
    /// The Basis Blade ID
    /// </summary>
    IndexSet Id { get; }
}