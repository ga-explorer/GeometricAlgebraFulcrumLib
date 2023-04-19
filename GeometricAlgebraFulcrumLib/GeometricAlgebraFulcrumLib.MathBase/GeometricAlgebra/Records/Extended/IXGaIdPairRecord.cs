using DataStructuresLib.IndexSets;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Extended;

public interface IXGaIdPairRecord
{
    /// <summary>
    /// The First Basis Blade ID
    /// </summary>
    IIndexSet Id1 { get; }

    /// <summary>
    /// The Second Basis Blade ID
    /// </summary>
    IIndexSet Id2 { get; }
}