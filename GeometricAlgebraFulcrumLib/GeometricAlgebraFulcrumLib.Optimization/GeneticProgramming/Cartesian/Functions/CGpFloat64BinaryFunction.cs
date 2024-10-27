using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Functions;

public sealed class CGpFloat64BinaryFunction :
    CGpFloat64Function
{
    public override string Name { get; }

    public override Pair<int> ArityRange 
        => TwoArityRange;

    public Func<double, double, double> MappingFunc { get; }


    internal CGpFloat64BinaryFunction(string name, Func<double, double, double> mappingFunc)
    {
        Name = name;
        MappingFunc = mappingFunc;
    }


    public override double GetOutput(IReadOnlyList<double> inputs, IReadOnlyList<double> weights)
    {
        return MappingFunc(
            weights[0] * inputs[0], 
            weights[1] * inputs[1]
        );
    }
}