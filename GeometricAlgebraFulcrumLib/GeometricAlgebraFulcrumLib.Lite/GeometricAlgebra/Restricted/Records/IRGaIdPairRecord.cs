namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public interface IRGaIdPairRecord
{
    /// <summary>
    /// The First Basis Blade ID
    /// </summary>
    ulong Id1 { get; }

    /// <summary>
    /// The Second Basis Blade ID
    /// </summary>
    ulong Id2 { get; }
}