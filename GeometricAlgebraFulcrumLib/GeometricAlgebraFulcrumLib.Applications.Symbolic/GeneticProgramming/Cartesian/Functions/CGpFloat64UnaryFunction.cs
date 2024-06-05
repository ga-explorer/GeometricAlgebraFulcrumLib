using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Functions;

public class CGpFloat64UnaryFunction :
    CGpFloat64Function
{
    public override string Name { get; }
    
    public override Pair<int> ArityRange 
        => OneArityRange;

    public Func<double, double> MappingFunc { get; }


    internal CGpFloat64UnaryFunction(string name, Func<double, double> mappingFunc)
    {
        Name = name;
        MappingFunc = mappingFunc;
    }


    public override double GetOutput(IReadOnlyList<double> inputs, IReadOnlyList<double> weights)
    {
        return MappingFunc(weights[0] * inputs[0]);
    }
}