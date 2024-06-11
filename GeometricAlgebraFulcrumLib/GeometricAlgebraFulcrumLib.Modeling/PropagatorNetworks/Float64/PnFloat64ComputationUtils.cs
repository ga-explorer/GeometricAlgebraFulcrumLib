using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.PropagatorNetworks.Float64;

public static class PnFloat64ComputationUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PnCellFloat64 DefineFloat64Cell(this PropagatorNetwork network, string name)
    {
        var cell = PnCellFloat64.Create(network, name);

        network.AddCell(cell);

        return cell;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PnCellFloat64 DefineFloat64Cell(this PropagatorNetwork network, string name, Func<IPropagatorValue, IPropagatorValue, IPropagatorValue> mergeFunction)
    {
        var cell = PnCellFloat64.Create(network, name, mergeFunction);

        network.AddCell(cell);

        return cell;
    }


    public static PropagatorNetwork AssignFloat64Square(this PropagatorNetwork network, string lhsCellName, string rhsCellName)
    {
        var lhsCell = (PnCellFloat64)network[lhsCellName];
        var rhsCell = (PnCellFloat64)network[rhsCellName];

        PnPropagatorFloat64Square.Register(
            rhsCell,
            lhsCell
        );

        PnPropagatorFloat64SquareRoot.Register(
            lhsCell,
            rhsCell
        );

        return network;
    }

    public static PropagatorNetwork AssignFloat64Sum(this PropagatorNetwork network, string lhsCellName, string rhsCell1Name, string rhsCell2Name)
    {
        var lhsCell = (PnCellFloat64)network[lhsCellName];
        var rhsCell1 = (PnCellFloat64)network[rhsCell1Name];
        var rhsCell2 = (PnCellFloat64)network[rhsCell2Name];

        PnPropagatorFloat64Plus.Register(
            rhsCell1,
            rhsCell2,
            lhsCell
        );

        PnPropagatorFloat64Minus.Register(
            lhsCell,
            rhsCell1,
            rhsCell2
        );

        PnPropagatorFloat64Minus.Register(
            lhsCell,
            rhsCell2,
            rhsCell1
        );

        return network;
    }

    public static PropagatorNetwork AssignFloat64Product(this PropagatorNetwork network, string lhsCellName, string rhsCell1Name, string rhsCell2Name)
    {
        var lhsCell = (PnCellFloat64)network[lhsCellName];
        var rhsCell1 = (PnCellFloat64)network[rhsCell1Name];
        var rhsCell2 = (PnCellFloat64)network[rhsCell2Name];

        PnPropagatorFloat64Times.Register(
            rhsCell1,
            rhsCell2,
            lhsCell
        );

        PnPropagatorFloat64Divide.Register(
            lhsCell,
            rhsCell1,
            rhsCell2
        );

        PnPropagatorFloat64Divide.Register(
            lhsCell,
            rhsCell2,
            rhsCell1
        );

        return network;
    }

    public static PropagatorNetwork AssignFloat64PythagoreanSum(this PropagatorNetwork network, string lhsCellName, string rhsCell1Name, string rhsCell2Name)
    {
        var rhsCell1Square = network.DefineFloat64Cell(rhsCell1Name + "Square");
        var rhsCell2Square = network.DefineFloat64Cell(rhsCell2Name + "Square");
        
        var lhsCellSquare = network.DefineFloat64Cell(lhsCellName + "Square");

        network.AssignFloat64Square(rhsCell1Square.Name, rhsCell1Name);
        network.AssignFloat64Square(rhsCell2Square.Name, rhsCell2Name);

        network.AssignFloat64Square(lhsCellSquare.Name, lhsCellName);

        network.AssignFloat64Sum(lhsCellSquare.Name, rhsCell1Square.Name, rhsCell2Square.Name);

        return network;
    }

}