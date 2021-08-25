namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public interface IGaListEvenDenseImmutable<T> :
        IGaListEvenDense<T>
    {
        public T this[int index] { get; }

        public T this[ulong index] { get; }
    }
}