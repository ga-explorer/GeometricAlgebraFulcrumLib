using System;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;

public sealed record MetaExpressionHeadSpecsArrayAccess : 
    IMetaExpressionHeadSpecsComposite
{
    public static MetaExpressionHeadSpecsArrayAccess Create(MetaContext context, string arrayName)
    {
        return new MetaExpressionHeadSpecsArrayAccess(context, arrayName);
    }


    public string ArrayName { get; }

    public MetaContext Context { get; }

    public string HeadText 
        => ArrayName;

    public bool IsNumber 
        => false;

    public bool IsSymbolicNumber 
        => false;

    public bool IsLiteralNumber 
        => false;

    public bool IsSymbolicNumberOrVariable 
        => false;

    public bool IsVariable 
        => false;

    public bool IsAtomic 
        => false;

    public bool IsComposite 
        => true;

    public bool IsFunction 
        => false;

    public bool IsOperator 
        => false;

    public bool IsArrayAccess
        => true;


    private MetaExpressionHeadSpecsArrayAccess(MetaContext context, string arrayName)
    {
        if (string.IsNullOrEmpty(arrayName))
            throw new ArgumentNullException(nameof(arrayName), @"Array name not initialized");

        Context = context; 
        ArrayName = arrayName;
    }


    public override string ToString()
    {
        return ArrayName;
    }
}