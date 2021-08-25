namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public abstract class GaGridEvenDenseImmutableBase<T> :
        GaGridEvenDenseBase<T>, IGaGridEvenDenseImmutable<T>
    {
        public T this[int index1, int index2] 
            => GetValue((ulong) index1, (ulong) index2);

        public T this[ulong index1, ulong index2] 
            => GetValue(index1, index2);
    }
}