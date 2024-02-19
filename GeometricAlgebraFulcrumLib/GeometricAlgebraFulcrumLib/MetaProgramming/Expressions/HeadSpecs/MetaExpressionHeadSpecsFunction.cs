using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;

public sealed record MetaExpressionHeadSpecsFunction : 
    IMetaExpressionHeadSpecsFunction
{
    public static MetaExpressionHeadSpecsFunction Create(MetaContext context, string functionName, bool isLeftAssociative, bool isRightAssociative)
    {
        return new MetaExpressionHeadSpecsFunction(
            context, 
            functionName, 
            isLeftAssociative, 
            isRightAssociative
        );
    }

    public static MetaExpressionHeadSpecsFunction CreateLeftAssociative(MetaContext context, string functionName)
    {
        return new MetaExpressionHeadSpecsFunction(
            context, 
            functionName, 
            true, 
            false
        );
    }

    public static MetaExpressionHeadSpecsFunction CreateRightAssociative(MetaContext context, string functionName)
    {
        return new MetaExpressionHeadSpecsFunction(
            context, 
            functionName, 
            false, 
            true
        );
    }

    public static MetaExpressionHeadSpecsFunction CreateAssociative(MetaContext context, string functionName)
    {
        return new MetaExpressionHeadSpecsFunction(
            context, 
            functionName, 
            true, 
            true
        );
    }

    public static MetaExpressionHeadSpecsFunction CreateNonAssociative(MetaContext context, string functionName)
    {
        return new MetaExpressionHeadSpecsFunction(
            context, 
            functionName, 
            false, 
            false
        );
    }


    public MetaContext Context { get; }

    public string FunctionName { get; }

    public int Precedence 
        => 0;

    public string HeadText 
        => FunctionName;

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
        => true;

    public bool IsOperator 
        => false;

    public bool IsArrayAccess 
        => false;

    public bool IsLeftAssociative { get; }

    public bool IsRightAssociative { get; }

    public bool IsAssociative 
        => IsLeftAssociative || IsRightAssociative;

    public bool IsNonAssociative 
        => !IsLeftAssociative && !IsRightAssociative;

    public MetaExpressionFunctionAssociationKind AssociationKind 
        => IsLeftAssociative
            ? (IsRightAssociative 
                ? MetaExpressionFunctionAssociationKind.LeftRight 
                : MetaExpressionFunctionAssociationKind.Left)
            : (IsRightAssociative 
                ? MetaExpressionFunctionAssociationKind.Right 
                : MetaExpressionFunctionAssociationKind.None);


    private MetaExpressionHeadSpecsFunction(MetaContext context, string functionName, bool isLeftAssociative, bool isRightAssociative)
    {
        if (string.IsNullOrEmpty(functionName))
            throw new ArgumentNullException(nameof(functionName), @"Function name not initialized");

        Context = context;            
        FunctionName = functionName;
        IsLeftAssociative = isLeftAssociative;
        IsRightAssociative = isRightAssociative;
    }


    public IMetaExpressionFunction CreateFunction()
    {
        return new MetaExpressionFunction(this);
    }

    public IMetaExpressionFunction CreateFunction(IMetaExpression argument1)
    {
        return new MetaExpressionFunction(
            this, 
            new []
            {
                argument1
            } 
        );
    }

    public IMetaExpressionFunction CreateFunction(IMetaExpression argument1, IMetaExpression argument2)
    {
        return new MetaExpressionFunction(
            this, 
            new []
            {
                argument1,
                argument2
            } 
        );
    }

    public IMetaExpressionFunction CreateFunction(IMetaExpression argument1, IMetaExpression argument2, IMetaExpression argument3)
    {
        return new MetaExpressionFunction(
            this, 
            new []
            {
                argument1,
                argument2,
                argument3
            } 
        );
    }

    public IMetaExpressionFunction CreateFunction(params IMetaExpression[] argumentsList)
    {
        return new MetaExpressionFunction(
            this, 
            argumentsList
        );
    }

    public IMetaExpressionFunction CreateFunction(IEnumerable<IMetaExpression> argumentsList)
    {
        return new MetaExpressionFunction(
            this, 
            argumentsList
        );
    }


    public override string ToString()
    {
        return FunctionName;
    }
}