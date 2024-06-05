//using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

//namespace GeometricAlgebraFulcrumLib.SymbolicApplications.GeneticProgramming.Cartesian.Functions;

//public class CGpFloat64AffineCombinationFunction :
//    CGpFloat64Function
//{
//    private readonly CGpFloat64FunctionParameter[] _parameterArray;


//    public override string Name 
//        => "affine";

//    public override Pair<int> ArityRange { get; }


//    internal CGpFloat64AffineCombinationFunction(int arity, double midValue, double valueRange)
//    {
//        ArityRange = arity;
//        _parameterArray = new CGpFloat64FunctionParameter[arity + 1];

//        for (var i = 0; i < _parameterArray.Length; i++)
//            _parameterArray[i] = new CGpFloat64FunctionParameter(midValue, valueRange);
//    }

    
//    public override double GetOutput(IReadOnlyList<double> inputs)
//    {
//        var output = 0d;

//        for (var i = 0; i < inputs.Count; i++)
//            output += _parameterArray[i].Value * inputs[i];

//        return output + _parameterArray[inputs.Count].Value;
//    }

//    public override IEnumerator<CGpFloat64FunctionParameter> GetEnumerator()
//    {
//        return ((IEnumerable<CGpFloat64FunctionParameter>)_parameterArray).GetEnumerator();
//    }
//}