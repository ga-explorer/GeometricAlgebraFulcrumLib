using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;

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

    public MetaExpressionHeadSpecsFunction VectorToRadians { get; }

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

        Negative = MetaExpressionHeadSpecsOperator.CreateNonAssociative(MetaExpressionFunctionNames.Negative, " -", 1, MetaExpressionOperatorPosition.Prefix);

        Plus = MetaExpressionHeadSpecsOperator.CreateAssociative(MetaExpressionFunctionNames.Plus, " + ", 2, MetaExpressionOperatorPosition.Infix);
        Subtract = MetaExpressionHeadSpecsOperator.CreateNonAssociative(MetaExpressionFunctionNames.Subtract, " - ", 2, MetaExpressionOperatorPosition.Infix);
        Times = MetaExpressionHeadSpecsOperator.CreateAssociative(MetaExpressionFunctionNames.Times, " * ", 3, MetaExpressionOperatorPosition.Infix);
        Divide = MetaExpressionHeadSpecsOperator.CreateNonAssociative(MetaExpressionFunctionNames.Divide, " / ", 3, MetaExpressionOperatorPosition.Infix);

        VectorToRadians = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.VectorToRadians);

        Inverse = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Inverse);
        Sign = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Sign);
        UnitStep = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.UnitStep);
        Abs = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Abs);
        Sqrt = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Sqrt);
        Exp = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Exp);
        Power = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Power);
        Log = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Log);
        Log2 = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Log2);
        Log10 = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Log10);
        Cos = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Cos);
        Sin = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Sin);
        Tan = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Tan);
        ArcCos = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.ArcCos);
        ArcSin = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.ArcSin);
        ArcTan = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.ArcTan);
        ArcTan2 = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.ArcTan2);
        Cosh = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Cosh);
        Sinh = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Sinh);
        Tanh = MetaExpressionHeadSpecsFunction.CreateNonAssociative(MetaExpressionFunctionNames.Tanh);
    }
}