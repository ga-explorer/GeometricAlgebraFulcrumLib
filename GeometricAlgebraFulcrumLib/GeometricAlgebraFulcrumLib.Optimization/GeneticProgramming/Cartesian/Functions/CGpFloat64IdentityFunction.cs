using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Functions;

public sealed class CGpFloat64IdentityFunction :
    CGpFloat64Function
{
    public static CGpFloat64IdentityFunction Instance { get; } 
        = new CGpFloat64IdentityFunction();


    public override string Name 
        => "identity";

    public override Pair<int> ArityRange 
        => OneArityRange;


    private CGpFloat64IdentityFunction()
    {
    }

    
    public override double GetOutput(IReadOnlyList<double> inputs, IReadOnlyList<double> weights)
    {
        return weights[0] * inputs[0];
    }
}