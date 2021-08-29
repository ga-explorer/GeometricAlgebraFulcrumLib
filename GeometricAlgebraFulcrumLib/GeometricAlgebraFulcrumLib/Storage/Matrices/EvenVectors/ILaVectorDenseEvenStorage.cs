namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public interface ILaVectorDenseEvenStorage<T> :
        ILaVectorEvenStorage<T>
    {
        int Count { get; }
    }
}