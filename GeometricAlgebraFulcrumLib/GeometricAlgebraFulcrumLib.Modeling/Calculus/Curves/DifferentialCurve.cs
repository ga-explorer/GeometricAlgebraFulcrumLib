using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;

public class DifferentialCurve
{
    public IReadOnlyList<DifferentialFunction> ScalarFunctions { get; }


    public int Dimensions 
        => ScalarFunctions.Count;

    public DifferentialFunction this[int index] 
        => ScalarFunctions[index];


    public DifferentialCurve(IReadOnlyList<DifferentialFunction> scalarFunctions)
    {
        ScalarFunctions = scalarFunctions;
    }



}