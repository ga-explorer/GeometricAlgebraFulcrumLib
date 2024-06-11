namespace GeometricAlgebraFulcrumLib.Modeling.PropagatorNetworks;

public interface IPropagatorNetwork :
    IReadOnlyDictionary<string, IPropagatorCell>
{
    IEnumerable<IPropagatorCell> Cells { get; }

    IEnumerable<IPropagator> Propagators { get; }

    bool ModifyEnabled { get; }

    void BeginModify();

    void EndModify();

    void ResetCellValues();

    bool DebugMode { get; set; }

    void DebugMessage(string text);
}