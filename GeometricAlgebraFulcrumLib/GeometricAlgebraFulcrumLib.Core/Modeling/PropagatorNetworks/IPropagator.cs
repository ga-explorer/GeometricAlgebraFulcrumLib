namespace GeometricAlgebraFulcrumLib.Core.Modeling.PropagatorNetworks
{
    public interface IPropagator
    {
        IPropagatorNetwork ParentNetwork { get; }

        IReadOnlyList<IPropagatorCell> InputCells { get; }

        IReadOnlyList<IPropagatorCell> OutputCells { get; }

        void Propagate(IPropagatorClosure closure);
    }
}
