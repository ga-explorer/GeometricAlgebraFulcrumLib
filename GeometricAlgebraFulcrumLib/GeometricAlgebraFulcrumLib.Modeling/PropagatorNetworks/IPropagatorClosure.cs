namespace GeometricAlgebraFulcrumLib.Modeling.PropagatorNetworks
{
    public interface IPropagatorClosure
        : IReadOnlyDictionary<string, IPropagatorValue>
    {
        new IPropagatorValue this[string key] { get; set; }

        IPropagatorNetwork ParentNetwork { get; }

        IEnumerable<IPropagatorCell> Cells { get; }
    }
}
