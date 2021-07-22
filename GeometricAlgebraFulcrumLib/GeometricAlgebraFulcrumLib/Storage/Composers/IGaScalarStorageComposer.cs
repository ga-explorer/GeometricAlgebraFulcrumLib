namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public interface IGaScalarStorageComposer<T>
        : IGaKVectorStorageComposer<T>
    {
        IGasScalar<T> GetScalarStorage();
    }
}