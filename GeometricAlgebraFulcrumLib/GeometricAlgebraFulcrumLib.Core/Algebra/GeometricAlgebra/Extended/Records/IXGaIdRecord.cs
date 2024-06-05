using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Records;

public interface IXGaIdRecord
{
    /// <summary>
    /// The Basis Blade ID
    /// </summary>
    IIndexSet Id { get; }
}