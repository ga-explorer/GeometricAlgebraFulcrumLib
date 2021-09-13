namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra
{
    public interface ILinArrayStorage2D<out T> :
        ILinArrayStorage<T>
    {
        int GetSparseCount1();

        int GetSparseCount2();
    }
}