namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public interface IGaGridEvenDense<T> :
        IGaGridEven<T>
    {
        int Count1 { get; }

        int Count2 { get; }

        int Count { get; }
    }
}