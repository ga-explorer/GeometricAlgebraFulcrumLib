using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Functions;

public sealed class CGpFloat64MeanFunction :
    CGpFloat64Function
{
    public static CGpFloat64MeanFunction Instance { get; }
        = new CGpFloat64MeanFunction();


    public override string Name 
        => "mean";

    public override Pair<int> ArityRange 
        => TwoToMaxArityRange;


    private CGpFloat64MeanFunction()
    {
    }


    public override double GetOutput(IReadOnlyList<double> inputs, IReadOnlyList<double> weights)
    {
        return inputs.Count.GetRange(i => weights[i] * inputs[i]).Average();
    }
}