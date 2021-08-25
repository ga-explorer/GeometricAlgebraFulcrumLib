namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public interface IGaListEvenSingleKey<T> :
        IGaListEvenSparse<T>
    {
        public ulong Key { get; }

        public T Value { get; set; }
    }
}