namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public interface IGaScalarStorageComposer<TScalar>
        : IGaKVectorStorageComposer<TScalar>
    {
        IGaScalarStorage<TScalar> GetScalarStorage();
    }
}