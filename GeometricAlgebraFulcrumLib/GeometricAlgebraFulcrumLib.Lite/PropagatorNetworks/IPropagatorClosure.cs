using GeometricAlgebraFulcrumLib.Lite.PropagatorNetworks.Float64;

namespace GeometricAlgebraFulcrumLib.Lite.PropagatorNetworks
{
    public interface IPropagatorClosure
        : IReadOnlyDictionary<string, IPropagatorValue>
    {
        new IPropagatorValue this[string key] { get; set; }

        IPropagatorNetwork ParentNetwork { get; }

        IEnumerable<IPropagatorCell> Cells { get; }
    }
}
