using System.Text;
using AngouriMath;
using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Composite;

public sealed class MetaExpressionFunction :
    MetaExpressionCompositeBase,
    IMetaExpressionFunction
{
    public static MetaExpressionFunction Create(MetaContext context, IMetaExpressionHeadSpecsFunction headSpecs)
    {
        return new MetaExpressionFunction(
            context,
            headSpecs
        );
    }

    public static MetaExpressionFunction Create(MetaContext context, IMetaExpressionHeadSpecsFunction headSpecs, params IMetaExpression[] argumentsList)
    {
        return new MetaExpressionFunction(
            context,
            headSpecs,
            argumentsList
        );
    }

    public static MetaExpressionFunction Create(MetaContext context, IMetaExpressionHeadSpecsFunction headSpecs, IEnumerable<IMetaExpression> argumentsList)
    {
        return new MetaExpressionFunction(
            context,
            headSpecs,
            argumentsList
        );
    }

    public static MetaExpressionFunction Create(MetaContext context, string functionName, bool isLeftAssociative, bool isRightAssociative)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.Create(functionName, isLeftAssociative, isRightAssociative)
        );
    }

    public static MetaExpressionFunction CreateLeftAssociative(MetaContext context, string functionName, bool isLeftAssociative, bool isRightAssociative, params IMetaExpression[] argumentsList)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.Create(functionName, isLeftAssociative, isRightAssociative),
            argumentsList
        );
    }

    public static MetaExpressionFunction CreateLeftAssociative(MetaContext context, string functionName, bool isLeftAssociative, bool isRightAssociative, IEnumerable<IMetaExpression> argumentsList)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.Create(functionName, isLeftAssociative, isRightAssociative),
            argumentsList
        );
    }

    public static MetaExpressionFunction CreateLeftAssociative(MetaContext context, string functionName)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.CreateLeftAssociative(functionName)
        );
    }

    public static MetaExpressionFunction CreateLeftAssociative(MetaContext context, string functionName, params IMetaExpression[] argumentsList)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.CreateLeftAssociative(functionName),
            argumentsList
        );
    }

    public static MetaExpressionFunction CreateLeftAssociative(MetaContext context, string functionName, IEnumerable<IMetaExpression> argumentsList)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.CreateLeftAssociative(functionName),
            argumentsList
        );
    }

    public static MetaExpressionFunction CreateRightAssociative(MetaContext context, string functionName)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.CreateRightAssociative(functionName)
        );
    }

    public static MetaExpressionFunction CreateRightAssociative(MetaContext context, string functionName, params IMetaExpression[] argumentsList)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.CreateRightAssociative(functionName),
            argumentsList
        );
    }

    public static MetaExpressionFunction CreateRightAssociative(MetaContext context, string functionName, IEnumerable<IMetaExpression> argumentsList)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.CreateRightAssociative(functionName),
            argumentsList
        );
    }

    public static MetaExpressionFunction CreateAssociative(MetaContext context, string functionName)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.CreateAssociative(functionName)
        );
    }

    public static MetaExpressionFunction CreateAssociative(MetaContext context, string functionName, params IMetaExpression[] argumentsList)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.CreateAssociative(functionName),
            argumentsList
        );
    }

    public static MetaExpressionFunction CreateAssociative(MetaContext context, string functionName, IEnumerable<IMetaExpression> argumentsList)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.CreateAssociative(functionName),
            argumentsList
        );
    }

    public static MetaExpressionFunction CreateNonAssociative(MetaContext context, string functionName)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.CreateNonAssociative(functionName)
        );
    }

    public static MetaExpressionFunction CreateNonAssociative(MetaContext context, string functionName, params IMetaExpression[] argumentsList)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.CreateNonAssociative(functionName),
            argumentsList
        );
    }

    public static MetaExpressionFunction CreateNonAssociative(MetaContext context, string functionName, IEnumerable<IMetaExpression> argumentsList)
    {
        return new MetaExpressionFunction(
            context,
            MetaExpressionHeadSpecsFunction.CreateNonAssociative(functionName),
            argumentsList
        );
    }


    public override IMetaExpressionHeadSpecs HeadSpecs
        => FunctionHeadSpecs;

    public override IMetaExpressionHeadSpecsComposite CompositeHeadSpecs
        => FunctionHeadSpecs;

    public IMetaExpressionHeadSpecsFunction FunctionHeadSpecs { get; }

    public IMetaExpressionHeadSpecsOperator OperatorHeadSpecs
        => FunctionHeadSpecs as IMetaExpressionHeadSpecsOperator;

    public bool IsLeftAssociative
        => FunctionHeadSpecs.IsLeftAssociative;

    public bool IsRightAssociative
        => FunctionHeadSpecs.IsRightAssociative;

    public bool IsAssociative
        => FunctionHeadSpecs.IsAssociative;

    public bool IsNonAssociative
        => FunctionHeadSpecs.IsNonAssociative;

    public MetaExpressionFunctionAssociationKind AssociationKind
        => FunctionHeadSpecs.AssociationKind;

    public string FunctionName
        => FunctionHeadSpecs.FunctionName;

    public override string HeadText
        => FunctionHeadSpecs.FunctionName;

    public override bool IsFunction
        => true;

    public override bool IsArrayAccess
        => false;

    public override bool IsOperator
        => FunctionHeadSpecs.IsOperator;


    internal MetaExpressionFunction(MetaContext context, IMetaExpressionHeadSpecsFunction headSpecs)
        : base(context)
    {
        FunctionHeadSpecs = headSpecs;
    }

    internal MetaExpressionFunction(MetaContext context, IMetaExpressionHeadSpecsFunction headSpecs, IEnumerable<IMetaExpression> argumentsList)
        : base(context, argumentsList)
    {
        FunctionHeadSpecs = headSpecs;
    }


    public override MetaExpressionCompositeBase CreateCopy()
    {
        return ArgumentsList.Count == 0
            ? new MetaExpressionFunction(Context, FunctionHeadSpecs)
            : new MetaExpressionFunction(Context, FunctionHeadSpecs, ArgumentsList);
    }

    public override MetaExpressionCompositeBase GetEmptyExpressionCopy(MetaContext context)
    {
        return new MetaExpressionFunction(context, FunctionHeadSpecs);
    }

    public override MetaExpressionCompositeBase GetEmptyExpressionCopy()
    {
        return new MetaExpressionFunction(Context, FunctionHeadSpecs);
    }

    public override Entity ToAngouriMathEntity()
    {
        var argumentsList =
            ArgumentsList
                .Select(a => a.ToAngouriMathEntity())
                .ToArray();

        return FunctionName switch
        {
            MetaExpressionFunctionNames.Plus => argumentsList.Aggregate((a, b) => a + b),
            MetaExpressionFunctionNames.Subtract => argumentsList[0] - argumentsList[1],
            MetaExpressionFunctionNames.Times => argumentsList.Aggregate((a, b) => a * b),
            MetaExpressionFunctionNames.Divide => argumentsList[0] / argumentsList[1],
            MetaExpressionFunctionNames.Negative => -argumentsList[0],
            MetaExpressionFunctionNames.Inverse => Entity.Number.Integer.One / argumentsList[0],
            MetaExpressionFunctionNames.Abs => MathS.Abs(argumentsList[0]),
            MetaExpressionFunctionNames.Sqrt => MathS.Sqrt(argumentsList[0]),
            MetaExpressionFunctionNames.Exp => MathS.Pow(MathS.e, argumentsList[0]),
            MetaExpressionFunctionNames.Log => MathS.Ln(argumentsList[0]),
            MetaExpressionFunctionNames.Log2 => MathS.Log(2, argumentsList[0]),
            MetaExpressionFunctionNames.Log10 => MathS.Log(argumentsList[0]),
            MetaExpressionFunctionNames.Cos => MathS.Cos(argumentsList[0]),
            MetaExpressionFunctionNames.Sin => MathS.Sin(argumentsList[0]),
            MetaExpressionFunctionNames.Tan => MathS.Tan(argumentsList[0]),
            MetaExpressionFunctionNames.ArcCos => MathS.Arccos(argumentsList[0]),
            MetaExpressionFunctionNames.ArcSin => MathS.Arcsin(argumentsList[0]),
            MetaExpressionFunctionNames.ArcTan => MathS.Arctan(argumentsList[0]),
            MetaExpressionFunctionNames.Cosh => MathS.Hyperbolic.Cosh(argumentsList[0]),
            MetaExpressionFunctionNames.Sinh => MathS.Hyperbolic.Sinh(argumentsList[0]),
            MetaExpressionFunctionNames.Tanh => MathS.Hyperbolic.Tanh(argumentsList[0]),

            _ => throw new InvalidOperationException()
        };
    }

    public override SteExpression ToSimpleTextExpression()
    {
        if (ArgumentsList.Count == 0)
            return SteExpression.CreateFunction(FunctionHeadSpecs.FunctionName);

        var argumentsList =
            ArgumentsList.Select(expr => expr.ToSimpleTextExpression());

        if (!IsOperator)
            return SteExpression.CreateFunction(
                FunctionHeadSpecs.FunctionName,
                argumentsList
            );

        var headSpecs = OperatorHeadSpecs;

        var opAssociation = headSpecs.AssociationKind switch
        {
            MetaExpressionFunctionAssociationKind.Left => SteOperatorAssociation.Left,
            MetaExpressionFunctionAssociationKind.Right => SteOperatorAssociation.Right,
            MetaExpressionFunctionAssociationKind.LeftRight => SteOperatorAssociation.LeftRight,
            _ => SteOperatorAssociation.None
        };

        var opPosition = headSpecs.Position switch
        {
            MetaExpressionOperatorPosition.Prefix => SteOperatorPosition.Prefix,
            MetaExpressionOperatorPosition.Infix => SteOperatorPosition.Infix,
            _ => SteOperatorPosition.Suffix
        };

        return SteExpression.CreateOperator(
            new SteOperatorSpecs(headSpecs.SymbolText, headSpecs.Precedence, opPosition, opAssociation),
            argumentsList
        );
    }


    public override string ToString()
    {
        var composer = new StringBuilder();

        composer.Append(FunctionHeadSpecs.FunctionName)
            .Append("[");

        if (ArgumentsList.Count > 0)
        {
            composer.Append(ArgumentsList[0]);

            for (var i = 1; i < ArgumentsList.Count; i++)
                composer
                    .Append(", ")
                    .Append(ArgumentsList[i]);
        }

        composer.Append("]");

        return composer.ToString();
    }
}