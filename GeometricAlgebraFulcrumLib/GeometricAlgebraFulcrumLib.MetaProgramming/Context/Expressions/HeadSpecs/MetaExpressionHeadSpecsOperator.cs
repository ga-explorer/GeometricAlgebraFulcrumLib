using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Composite;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

public sealed record MetaExpressionHeadSpecsOperator :
    IMetaExpressionHeadSpecsOperator
{
    public static MetaExpressionHeadSpecsOperator Create(string functionName, string opSymbol, int opPrecedence, MetaExpressionOperatorPosition opPosition, bool isLeftAssociative, bool isRightAssociative)
    {
        return new MetaExpressionHeadSpecsOperator(
            functionName,
            opSymbol,
            opPrecedence,
            opPosition,
            isLeftAssociative,
            isRightAssociative
        );
    }

    public static MetaExpressionHeadSpecsOperator CreateLeftAssociative(string functionName, string opSymbol, int opPrecedence, MetaExpressionOperatorPosition opPosition)
    {
        return new MetaExpressionHeadSpecsOperator(
            functionName,
            opSymbol,
            opPrecedence,
            opPosition,
            true,
            false
        );
    }

    public static MetaExpressionHeadSpecsOperator CreateRightAssociative(string functionName, string opSymbol, int opPrecedence, MetaExpressionOperatorPosition opPosition)
    {
        return new MetaExpressionHeadSpecsOperator(
            functionName,
            opSymbol,
            opPrecedence,
            opPosition,
            false,
            true
        );
    }

    public static MetaExpressionHeadSpecsOperator CreateAssociative(string functionName, string opSymbol, int opPrecedence, MetaExpressionOperatorPosition opPosition)
    {
        return new MetaExpressionHeadSpecsOperator(
            functionName,
            opSymbol,
            opPrecedence,
            opPosition,
            true,
            true
        );
    }

    public static MetaExpressionHeadSpecsOperator CreateNonAssociative(string functionName, string opSymbol, int opPrecedence, MetaExpressionOperatorPosition opPosition)
    {
        return new MetaExpressionHeadSpecsOperator(
            functionName,
            opSymbol,
            opPrecedence,
            opPosition,
            false,
            false
        );
    }


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
        => true;

    public bool IsArrayAccess
        => false;

    public string FunctionName { get; }

    public int Precedence { get; }

    public string SymbolText { get; }

    public MetaExpressionOperatorPosition Position { get; }

    public bool IsLeftAssociative { get; }

    public bool IsRightAssociative { get; }

    public bool IsAssociative
        => IsLeftAssociative || IsRightAssociative;

    public bool IsNonAssociative
        => !IsLeftAssociative && !IsRightAssociative;

    public MetaExpressionFunctionAssociationKind AssociationKind
        => IsLeftAssociative
            ? IsRightAssociative
                ? MetaExpressionFunctionAssociationKind.LeftRight
                : MetaExpressionFunctionAssociationKind.Left
            : IsRightAssociative
                ? MetaExpressionFunctionAssociationKind.Right
                : MetaExpressionFunctionAssociationKind.None;


    private MetaExpressionHeadSpecsOperator(string functionName, string opSymbol, int opPrecedence, MetaExpressionOperatorPosition opPosition, bool isLeftAssociative, bool isRightAssociative)
    {
        if (string.IsNullOrEmpty(opSymbol))
            throw new ArgumentNullException(nameof(opSymbol), @"Operator symbol not initialized");

        FunctionName = functionName;
        Precedence = opPrecedence;
        IsLeftAssociative = isLeftAssociative;
        IsRightAssociative = isRightAssociative;
        SymbolText = opSymbol;
        Position = opPosition;
    }


    //public IMetaExpressionFunction CreateFunction()
    //{
    //    return new MetaExpressionFunction(this);
    //}

    //public IMetaExpressionFunction CreateFunction(IMetaExpression argument1)
    //{
    //    return new MetaExpressionFunction(
    //        this, 
    //        new []
    //        {
    //            argument1
    //        } 
    //    );
    //}

    //public IMetaExpressionFunction CreateFunction(IMetaExpression argument1, IMetaExpression argument2)
    //{
    //    return new MetaExpressionFunction(
    //        this, 
    //        new []
    //        {
    //            argument1,
    //            argument2
    //        } 
    //    );
    //}

    //public IMetaExpressionFunction CreateFunction(IMetaExpression argument1, IMetaExpression argument2, IMetaExpression argument3)
    //{
    //    return new MetaExpressionFunction(
    //        this, 
    //        new []
    //        {
    //            argument1,
    //            argument2,
    //            argument3
    //        } 
    //    );
    //}

    //public IMetaExpressionFunction CreateFunction(params IMetaExpression[] argumentsList)
    //{
    //    return new MetaExpressionFunction(
    //        this, 
    //        argumentsList
    //    );
    //}

    //public IMetaExpressionFunction CreateFunction(IEnumerable<IMetaExpression> argumentsList)
    //{
    //    return new MetaExpressionFunction(
    //        this, 
    //        argumentsList
    //    );
    //}


    public override string ToString()
    {
        return SymbolText;
    }
}