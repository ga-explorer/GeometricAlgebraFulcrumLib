namespace CodeComposerLib.SyntaxTree.Expressions;

public class SteFunctionHeadSpecs : ISteCompositeHeadSpecs
{
    public string FunctionName { get; }

    public string HeadText => FunctionName;


    public SteFunctionHeadSpecs(string functionName)
    {
        FunctionName = functionName;
    }


    public override string ToString()
    {
        return FunctionName;
    }
}