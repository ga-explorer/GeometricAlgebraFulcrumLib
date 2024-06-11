using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.PropagatorNetworks.Float64;

public sealed class PnPropagatorFloat64Plus :
    PnPropagatorFloat64
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Register(PnCellFloat64 inputCell1, PnCellFloat64 inputCell2, PnCellFloat64 outputCell)
    {
        var propagator = new PnPropagatorFloat64Plus(
            inputCell1,
            inputCell2,
            outputCell
        );

        inputCell1.AddClientPropagator(propagator);
        inputCell2.AddClientPropagator(propagator);
    }

    
    public override string OperatorName 
        => "Plus";

    public PnCellFloat64 InputCell1 { get; }

    public PnCellFloat64 InputCell2 { get; }

    public override IReadOnlyList<IPropagatorCell> InputCells 
        => new []{InputCell1, InputCell2};


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PnPropagatorFloat64Plus(PnCellFloat64 inputCell1, PnCellFloat64 inputCell2, PnCellFloat64 outputCell)
        : base(outputCell)
    {
        InputCell1 = inputCell1;
        InputCell2 = inputCell2;

        ParentNetwork.DebugMessage($"Created {this}");
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Propagate(IPropagatorClosure closure)
    {
        var value1 = closure.GetValueFloat64(InputCell1.Name);
        var value2 = closure.GetValueFloat64(InputCell2.Name);

        if (value1.IsEmpty || value2.IsEmpty) return;

        var value = PnValueFloat64.Create(value1.Value + value2.Value);

        ParentNetwork.DebugMessage($"Invoking {this} as [{value}] <- {OperatorName}[{value1}, {value2}]");

        OutputCell.Update(value);
    }
}