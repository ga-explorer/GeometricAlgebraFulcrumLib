using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Functions;

public sealed class CGpFloat64SubtractFunction :
    CGpFloat64Function
{
    public static CGpFloat64SubtractFunction Instance { get; }
        = new CGpFloat64SubtractFunction();


    public override string Name 
        => "subtract";
    
    public override Pair<int> ArityRange 
        => TwoArityRange;


    private CGpFloat64SubtractFunction()
    {
    }


    public override double GetOutput(IReadOnlyList<double> inputs, IReadOnlyList<double> weights)
    {
        return weights[0] * inputs[0] - 
               weights[1] * inputs[1];
    }
}