namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public interface IGaListEvenDense<T> :
        IGaListEven<T>
    {
        int Count { get; }
    }
}