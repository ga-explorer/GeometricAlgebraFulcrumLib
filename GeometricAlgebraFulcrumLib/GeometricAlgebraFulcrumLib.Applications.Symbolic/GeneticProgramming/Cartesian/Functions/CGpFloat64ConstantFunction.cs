using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Functions;

public sealed class CGpFloat64ConstantFunction :
    CGpFloat64Function
{
    public override string Name 
        => "constant " + Value.ToString("G");
    
    public override Pair<int> ArityRange 
        => ZeroArityRange;

    public double Value { get; }


    internal CGpFloat64ConstantFunction(double value)
    {
        Debug.Assert(
            !double.IsNaN(value) &&
            double.IsFinite(value)
        );

        Value = value;
    }


    public override double GetOutput(IReadOnlyList<double> inputs, IReadOnlyList<double> weights)
    {
        return Value;
    }
}