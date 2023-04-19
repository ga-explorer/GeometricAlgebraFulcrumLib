namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

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