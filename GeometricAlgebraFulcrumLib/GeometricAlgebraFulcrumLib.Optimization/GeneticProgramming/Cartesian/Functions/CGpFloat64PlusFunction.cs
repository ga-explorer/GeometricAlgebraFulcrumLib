using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Functions;

public sealed class CGpFloat64PlusFunction :
    CGpFloat64Function
{
    public static CGpFloat64PlusFunction Instance { get; }
        = new CGpFloat64PlusFunction();


    public override string Name 
        => "plus";

    public override Pair<int> ArityRange 
        => TwoToMaxArityRange;


    private CGpFloat64PlusFunction()
    {
    }


    public override double GetOutput(IReadOnlyList<double> inputs, IReadOnlyList<double> weights)
    {
        return inputs.Count.GetRange(i => weights[i] * inputs[i]).Sum();
    }
}