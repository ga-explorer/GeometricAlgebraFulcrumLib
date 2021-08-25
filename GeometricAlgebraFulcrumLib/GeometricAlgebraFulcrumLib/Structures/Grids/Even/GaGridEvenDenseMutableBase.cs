namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public abstract class GaGridEvenDenseMutableBase<T> :
        GaGridEvenDenseBase<T>, IGaGridEvenDenseMutable<T>
    {
        public abstract T this[int index1, int index2] { get; set; }

        public abstract T this[ulong index1, ulong index2] { get; set; }
    }
}