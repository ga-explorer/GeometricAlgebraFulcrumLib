using System;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;

public sealed record MetaExpressionHeadSpecsVariable : 
    IMetaExpressionHeadSpecsVariable
{
    public static MetaExpressionHeadSpecsVariable Create(MetaContext context, string variableName)
    {
        return new MetaExpressionHeadSpecsVariable(
            context,
            variableName
        );
    }


    public string VariableName { get; }

    public MetaContext Context { get; }

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


    private MetaExpressionHeadSpecsVariable(MetaContext context, string variableName)
    {
        if (string.IsNullOrEmpty(variableName))
            throw new ArgumentNullException(nameof(variableName), @"Variable name not initialized");

        Context = context;
        VariableName = variableName;
    }


    public override string ToString()
    {
        return VariableName;
    }
}