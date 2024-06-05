using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Functions;

public class CGpFloat64NegativeFunction :
    CGpFloat64Function
{
    public static CGpFloat64NegativeFunction Instance { get; }
        = new CGpFloat64NegativeFunction();


    public override string Name 
        => "negative";
    
    public override Pair<int> ArityRange 
        => OneArityRange;


    private CGpFloat64NegativeFunction()
    {
    }


    public override double GetOutput(IReadOnlyList<double> inputs, IReadOnlyList<double> weights)
    {
        return -weights[0] * inputs[0];
    }
}