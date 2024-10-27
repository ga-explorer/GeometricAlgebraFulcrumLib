using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Functions;

public class CGpFloat64NaryFunction :
    CGpFloat64Function
{
    public override string Name { get; }

    public override Pair<int> ArityRange 
        => ZeroToMaxArityRange;

    public Func<IEnumerable<double>, double> MappingFunc { get; }


    internal CGpFloat64NaryFunction(string name, Func<IEnumerable<double>, double> mappingFunc)
    {
        Name = name;
        MappingFunc = mappingFunc;
    }


    public override double GetOutput(IReadOnlyList<double> inputs, IReadOnlyList<double> weights)
    {
        var inputList = 
            inputs.Count.GetRange(i => weights[i] * inputs[i]);

        return MappingFunc(inputList);
    }
}