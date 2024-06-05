using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Functions;

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