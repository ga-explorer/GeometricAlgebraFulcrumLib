namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public interface IRGaKvIndexPairRecord
{
    /// <summary>
    /// The first k-vector Index
    /// </summary>
    ulong KvIndex1 { get; }

    /// <summary>
    /// The second k-vector Index
    /// </summary>
    ulong KvIndex2 { get; }
}