namespace GeometricAlgebraFulcrumLib.Modeling.PropagatorNetworks
{
    public interface IPropagatorCell
    {
        IPropagatorNetwork ParentNetwork { get; }

        string Name { get; }

        IPropagatorValue Value { get; }

        public bool IsEmpty { get; }
        
        void ResetValue();

        IEnumerable<IPropagator> ClientPropagators { get; }
        
        IEnumerable<IPropagatorCell> ClientCells { get; }

        void AddClientPropagator(IPropagator clientPropagator);
    }

    public interface IPropagatorCell<T> :
        IPropagatorCell
    {
        Func<IPropagatorValue, IPropagatorValue, IPropagatorValue> MergeFunction { get; }

        IPropagatorValue Update(IPropagatorValue value);
    }
}
