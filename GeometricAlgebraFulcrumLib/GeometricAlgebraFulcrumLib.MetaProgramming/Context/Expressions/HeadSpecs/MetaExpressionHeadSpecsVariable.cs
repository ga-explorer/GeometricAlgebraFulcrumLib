namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

public sealed record MetaExpressionHeadSpecsVariable :
    IMetaExpressionHeadSpecsVariable
{
    public static MetaExpressionHeadSpecsVariable Create(string variableName)
    {
        return new MetaExpressionHeadSpecsVariable(variableName);
    }


    public string VariableName { get; }

    public string HeadText
        => VariableName;

    public bool IsNumber
        => false;

    public bool IsSymbolicNumber
        => false;

    public bool IsLiteralNumber
        => false;

    public bool IsSymbolicNumberOrVariable
        => true;

    public bool IsVariable
        => true;

    public bool IsAtomic
        => true;

    public bool IsComposite
        => false;

    public bool IsFunction
        => false;

    public bool IsOperator
        => false;

    public bool IsArrayAccess
        => false;


    private MetaExpressionHeadSpecsVariable(string variableName)
    {
        if (string.IsNullOrEmpty(variableName))
            throw new ArgumentNullException(nameof(variableName), @"Variable name not initialized");

        VariableName = variableName;
    }


    public override string ToString()
    {
        return VariableName;
    }
}