using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.PropagatorNetworks.Float64;

public abstract class PnPropagatorFloat64 :
    IPropagator
{
    public abstract string OperatorName { get; }

    public PnCellFloat64 OutputCell { get; }

    public IPropagatorNetwork ParentNetwork 
        => OutputCell.ParentNetwork;

    public abstract IReadOnlyList<IPropagatorCell> InputCells { get; }

    public IReadOnlyList<IPropagatorCell> OutputCells 
        => new []{OutputCell};


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected PnPropagatorFloat64(PnCellFloat64 outputCell)
    {
        OutputCell = outputCell;
    }


    public abstract void Propagate(IPropagatorClosure closure);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        var inputsText = 
            InputCells
                .Select(c => c.Name)
                .Concatenate(", ");

        var outputsText = 
            OutputCells
                .Select(c => c.Name)
                .Concatenate(", ");

        return $"Propagator [{outputsText}] <- {OperatorName}[{inputsText}]";

        //var composer = new LinearTextComposer();

        //composer
        //    .AppendLine("Propagator {")
        //    .IncreaseIndentation()
        //    .AppendLine($"   name: '{OperatorName}'")
        //    .AppendLine($" inputs: [{inputsText}]")
        //    .AppendLine($"outputs: [{outputsText}]")
        //    .DecreaseIndentation()
        //    .Append("}");

        //return composer.ToString();
    }
}