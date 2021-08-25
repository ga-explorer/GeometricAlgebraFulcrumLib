namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public interface IGaListEvenDenseMutable<T> :
        IGaListEvenDense<T>
    {
        public T this[int index] { get; set; }

        public T this[ulong index] { get; set; }
    }
}