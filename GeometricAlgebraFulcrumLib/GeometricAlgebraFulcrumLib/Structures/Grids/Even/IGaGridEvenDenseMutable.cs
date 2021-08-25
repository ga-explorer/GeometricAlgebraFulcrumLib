namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public interface IGaGridEvenDenseMutable<T> :
        IGaGridEvenDense<T>
    {
        public T this[int index1, int index2] { get; set; }

        public T this[ulong index1, ulong index2] { get; set; }
    }
}