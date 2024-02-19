namespace GeometricAlgebraFulcrumLib.Lite.PropagatorNetworks
{
    public interface IPropagatorValue
    {
        bool IsEmpty { get; }
    }

    public interface IPropagatorValue<T> :
        IPropagatorValue
    {
        T Value { get; }

        bool IsEquivalentTo(IPropagatorValue<T> otherValue);
    }
}
