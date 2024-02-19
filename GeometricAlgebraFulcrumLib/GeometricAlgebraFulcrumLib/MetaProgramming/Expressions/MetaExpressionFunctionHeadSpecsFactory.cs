using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

public sealed class MetaExpressionFunctionHeadSpecsFactory
{
    public MetaContext Context { get; }

    public MetaExpressionHeadSpecsOperator Plus { get; }

    public MetaExpressionHeadSpecsOperator Subtract { get; }

    public MetaExpressionHeadSpecsOperator Times { get; }

    public MetaExpressionHeadSpecsOperator Divide { get; }

    public MetaExpressionHeadSpecsOperator Negative { get; }

    public MetaExpressionHeadSpecsFunction Inverse { get; }

    public MetaExpressionHeadSpecsFunction Sign { get; }

    public MetaExpressionHeadSpecsFunction UnitStep { get; }

    public MetaExpressionHeadSpecsFunction Abs { get; }

    public MetaExpressionHeadSpecsFunction Sqrt { get; }

    public MetaExpressionHeadSpecsFunction Exp { get; }

    public MetaExpressionHeadSpecsFunction Power { get; }

    public MetaExpressionHeadSpecsFunction Log { get; }

    public MetaExpressionHeadSpecsFunction Log2 { get; }

    public MetaExpressionHeadSpecsFunction Log10 { get; }

    public MetaExpressionHeadSpecsFunction Cos { get; }

    public MetaExpressionHeadSpecsFunction Sin { get; }

    public MetaExpressionHeadSpecsFunction Tan { get; }

    public MetaExpressionHeadSpecsFunction ArcCos { get; }

    public MetaExpressionHeadSpecsFunction ArcSin { get; }

    public MetaExpressionHeadSpecsFunction ArcTan { get; }

    public MetaExpressionHeadSpecsFunction ArcTan2 { get; }

    public MetaExpressionHeadSpecsFunction Cosh { get; }

    public MetaExpressionHeadSpecsFunction Sinh { get; }

    public MetaExpressionHeadSpecsFunction Tanh { get; }


    internal MetaExpressionFunctionHeadSpecsFactory(MetaContext context)
    {
        Context = context;

        Negative = MetaExpressionHeadSpecsOperator.CreateNonAssociative(Context, MetaExpressionFunctionNames.Negative, " -", 1, MetaExpressionOperatorPosition.Prefix);

        Plus = MetaExpressionHeadSpecsOperator.CreateAssociative(Context, MetaExpressionFunctionNames.Plus, " + ", 2, MetaExpressionOperatorPosition.Infix);
        Subtract = MetaExpressionHeadSpecsOperator.CreateNonAssociative(Context, MetaExpressionFunctionNames.Subtract, " - ", 2, MetaExpressionOperatorPosition.Infix);
        Times = MetaExpressionHeadSpecsOperator.CreateAssociative(Context, MetaExpressionFunctionNames.Times, " * ", 3, MetaExpressionOperatorPosition.Infix);
        Divide = MetaExpressionHeadSpecsOperator.CreateNonAssociative(Context, MetaExpressionFunctionNames.Divide, " / ", 3, MetaExpressionOperatorPosition.Infix);

        Inverse = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Inverse);
        Sign = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Sign);
        UnitStep = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.UnitStep);
        Abs = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Abs);
        Sqrt = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Sqrt);
        Exp = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Exp);
        Power = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Power);
        Log = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Log);
        Log2 = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Log2);
        Log10 = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Log10);
        Cos = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Cos);
        Sin = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Sin);
        Tan = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Tan);
        ArcCos = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.ArcCos);
        ArcSin = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.ArcSin);
        ArcTan = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.ArcTan);
        ArcTan2 = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.ArcTan2);
        Cosh = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Cosh);
        Sinh = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Sinh);
        Tanh = MetaExpressionHeadSpecsFunction.CreateNonAssociative(Context, MetaExpressionFunctionNames.Tanh);
    }
}