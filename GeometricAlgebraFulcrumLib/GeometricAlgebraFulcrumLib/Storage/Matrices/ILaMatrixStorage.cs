namespace GeometricAlgebraFulcrumLib.Storage.Matrices
{
    public interface ILaMatrixStorage<out T> :
        ILaStorage<T>
    {
        int GetSparseCount1();

        int GetSparseCount2();
    }
}