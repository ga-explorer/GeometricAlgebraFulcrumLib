namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public interface IGaStorageComposerKVector<T> 
        : IGaStorageComposer<T>
    {
        IGaStorageKVector<T> GetKVector();

        IGaStorageKVector<T> GetKVector(bool copyFlag);
    }
}