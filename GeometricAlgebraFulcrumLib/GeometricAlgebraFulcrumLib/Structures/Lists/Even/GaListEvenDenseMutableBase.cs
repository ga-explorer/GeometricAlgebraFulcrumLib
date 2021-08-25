namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public abstract class GaListEvenDenseMutableBase<T> :
        GaListEvenDenseBase<T>, IGaListEvenDenseMutable<T>
    {
        public abstract T this[int index] { get; set; }

        public abstract T this[ulong index] { get; set; }
    }
}