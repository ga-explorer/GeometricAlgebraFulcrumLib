using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Records;

public interface IXGaIdRecord
{
    /// <summary>
    /// The Basis Blade ID
    /// </summary>
    IndexSet Id { get; }
}