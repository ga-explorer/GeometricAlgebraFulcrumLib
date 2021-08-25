namespace GeometricAlgebraFulcrumLib.Structures.Grids
{
    public interface IGaGrid<out T> :
        IGaCollection<T>
    {
        int GetSparseCount1();

        int GetSparseCount2();
    }
}