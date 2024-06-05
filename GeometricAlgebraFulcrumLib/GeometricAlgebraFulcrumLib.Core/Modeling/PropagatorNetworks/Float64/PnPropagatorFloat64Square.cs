using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.PropagatorNetworks.Float64;

public sealed class PnPropagatorFloat64Square :
    PnPropagatorFloat64
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Register(PnCellFloat64 inputCell, PnCellFloat64 outputCell)
    {
        var propagator = new PnPropagatorFloat64Square(
            inputCell,
            outputCell
        );

        inputCell.AddClientPropagator(propagator);
    }

    
    public override string OperatorName 
        => "Square";

    public PnCellFloat64 InputCell { get; }

    public override IReadOnlyList<IPropagatorCell> InputCells 
        => new []{InputCell};


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PnPropagatorFloat64Square(PnCellFloat64 inputCell, PnCellFloat64 outputCell)
        : base(outputCell)
    {
        InputCell = inputCell;

        ParentNetwork.DebugMessage($"Created {this}");
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Propagate(IPropagatorClosure closure)
    {
        var value1 = closure.GetValueFloat64(InputCell.Name);

        if (value1.IsEmpty) return;

        var value = PnValueFloat64.Create(value1.Value * value1.Value);

        ParentNetwork.DebugMessage($"Invoking {this} as [{value}] <- {OperatorName}[{value1}]");

        OutputCell.Update(value);
    }
}