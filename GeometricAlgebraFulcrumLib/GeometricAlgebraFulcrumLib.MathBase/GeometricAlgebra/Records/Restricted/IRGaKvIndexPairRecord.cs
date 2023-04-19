namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted
{
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
}