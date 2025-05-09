using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public interface IXGaIdPairRecord
{
    /// <summary>
    /// The First Basis Blade ID
    /// </summary>
    IndexSet Id1 { get; }

    /// <summary>
    /// The Second Basis Blade ID
    /// </summary>
    IndexSet Id2 { get; }
}