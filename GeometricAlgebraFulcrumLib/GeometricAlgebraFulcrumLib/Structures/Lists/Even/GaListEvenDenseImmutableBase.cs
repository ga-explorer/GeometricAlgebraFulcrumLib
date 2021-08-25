namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public abstract class GaListEvenDenseImmutableBase<T> :
        GaListEvenDenseBase<T>, IGaListEvenDenseImmutable<T>
    {
        public T this[int index] 
            => GetValue((ulong) index);

        public T this[ulong index] 
            => GetValue(index);
    }
}