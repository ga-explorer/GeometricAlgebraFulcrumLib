using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Functions;

public sealed class CGpFloat64TimesFunction :
    CGpFloat64Function
{
    public static CGpFloat64TimesFunction Instance { get; } 
        = new CGpFloat64TimesFunction();


    public override string Name 
        => "times";

    public override Pair<int> ArityRange 
        => TwoToMaxArityRange;


    private CGpFloat64TimesFunction()
    {
    }


    public override double GetOutput(IReadOnlyList<double> inputs, IReadOnlyList<double> weights)
    {
        return inputs.Count.GetRange(i => weights[i] * inputs[i]).Aggregate(1d, (a, b) => a * b);
    }
}