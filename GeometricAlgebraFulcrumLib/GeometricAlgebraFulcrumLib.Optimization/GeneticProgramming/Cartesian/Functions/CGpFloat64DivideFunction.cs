using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Functions;

public sealed class CGpFloat64DivideFunction :
    CGpFloat64Function
{
    public static CGpFloat64DivideFunction Instance { get; }
        = new CGpFloat64DivideFunction();


    public override string Name 
        => "divide";
    
    public override Pair<int> ArityRange 
        => TwoArityRange;


    private CGpFloat64DivideFunction()
    {
    }


    public override double GetOutput(IReadOnlyList<double> inputs, IReadOnlyList<double> weights)
    {
        return (weights[0] * inputs[0]) / (weights[1] * inputs[1]);
    }
}