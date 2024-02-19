using DataStructuresLib.IndexSets;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Records;

public interface IXGaIdRecord
{
    /// <summary>
    /// The Basis Blade ID
    /// </summary>
    IIndexSet Id { get; }
}