namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;

public class SteVariableHeadSpecs : ISteAtomicHeadSpecs
{
    public string VariableName { get; }

    public string HeadText => VariableName;


    public SteVariableHeadSpecs(string variableName)
    {
        VariableName = variableName;
    }


    public override string ToString()
    {
        return VariableName;
    }
}