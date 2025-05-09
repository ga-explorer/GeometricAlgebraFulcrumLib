using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Functions;

public sealed class CGpFloat64InverseFunction :
    CGpFloat64Function
{
    public static CGpFloat64InverseFunction Instance { get; }
        = new CGpFloat64InverseFunction();


    public override string Name 
        => "inverse";
    
    public override Pair<int> ArityRange 
        => OneArityRange;


    private CGpFloat64InverseFunction()
    {
    }


    public override double GetOutput(IReadOnlyList<double> inputs, IReadOnlyList<double> weights)
    {
        return 1d / (weights[0] * inputs[0]);
    }
}