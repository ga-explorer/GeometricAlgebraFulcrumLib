using GeometricAlgebraFulcrumLib.Utilities.Code.Languages.Matlab;
using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code.Matlab;

public sealed class MetaExpressionToMatlabFloat64Converter :
    MetaExpressionToLanguageConverterBase
{
    public static MetaExpressionToMatlabFloat64Converter DefaultConverter { get; }
        = new MetaExpressionToMatlabFloat64Converter();


    private MetaExpressionToMatlabFloat64Converter()
        : base(CclMatlabUtils.Matlab4Info)
    {

    }


    public override SteExpression Visit(IMetaExpressionFunction functionExpr)
    {
        var functionName =
            functionExpr.HeadSpecs.HeadText;

        var argumentsArray =
            functionExpr.Arguments.Select(Convert).ToArray();

        if (argumentsArray.Length == 0)
            return SteExpression.CreateFunction(functionName);

        switch (functionName)
        {
            case "Plus":
                return SteExpression.CreateOperator(
                    CclMatlabUtils.Operators.Add, argumentsArray
                );

            case "Minus":
                return SteExpression.CreateOperator(
                    CclMatlabUtils.Operators.UnaryMinus, argumentsArray
                );

            case "Subtract":
                return SteExpression.CreateOperator(
                    CclMatlabUtils.Operators.Subtract, argumentsArray
                );

            case "Times":
                if (argumentsArray[0].ToString() == "-1" && argumentsArray.Length == 2)
                    return SteExpression.CreateOperator(
                        CclMatlabUtils.Operators.UnaryMinus, argumentsArray[1]
                    );

                return SteExpression.CreateOperator(
                    CclMatlabUtils.Operators.Multiply, argumentsArray
                );

            case "Divide":
                return SteExpression.CreateOperator(
                    CclMatlabUtils.Operators.Divide, argumentsArray
                );

            case "Power":
                return argumentsArray[1].ToString() switch
                {
                    "0.5" => SteExpression.CreateFunction(
                        "sqrt",
                        argumentsArray[0]
                    ),
                    "-0.5" => SteExpression.CreateOperator(
                        CclMatlabUtils.Operators.Divide,
                        SteExpression.CreateLiteralNumber(1),
                        SteExpression.CreateFunction(
                            "sqrt",
                            argumentsArray[0]
                        )
                    ),
                    "-1" => SteExpression.CreateOperator(
                        CclMatlabUtils.Operators.Divide,
                        SteExpression.CreateLiteralNumber(1),
                        argumentsArray[0]
                    ),
                    "2" => SteExpression.CreateOperator(
                        CclMatlabUtils.Operators.Multiply,
                        argumentsArray[0],
                        argumentsArray[0]
                    ),
                    "3" => SteExpression.CreateOperator(
                        CclMatlabUtils.Operators.Multiply,
                        argumentsArray[0],
                        argumentsArray[0],
                        argumentsArray[0]
                    ),
                    _ => SteExpression.CreateFunction("power", argumentsArray)
                };

            case "Abs":
                return SteExpression.CreateFunction("abs", argumentsArray);

            case "Exp":
                return SteExpression.CreateFunction("exp", argumentsArray);

            case "Sin":
                return SteExpression.CreateFunction("sin", argumentsArray);

            case "Cos":
                return SteExpression.CreateFunction("cos", argumentsArray);

            case "Tan":
                return SteExpression.CreateFunction("tan", argumentsArray);

            case "ArcSin":
                return SteExpression.CreateFunction("asin", argumentsArray);

            case "ArcCos":
                return SteExpression.CreateFunction("acos", argumentsArray);

            case "ArcTan":
                return argumentsArray.Length switch
                {
                    1 => SteExpression.CreateFunction("atan", argumentsArray[0]),
                    2 => SteExpression.CreateFunction("atan2", argumentsArray[0], argumentsArray[1]),
                    _ => throw new InvalidOperationException()
                };

            case "Sinh":
                return SteExpression.CreateFunction("sinh", argumentsArray);

            case "Cosh":
                return SteExpression.CreateFunction("cosh", argumentsArray);

            case "Tanh":
                return SteExpression.CreateFunction("tanh", argumentsArray);

            case "Log":
                return SteExpression.CreateFunction(
                    "log",
                    argumentsArray.Length == 1 ? argumentsArray : argumentsArray.Reverse()
                );

            case "Log10":
                return SteExpression.CreateFunction("log10", argumentsArray);

            case "Sqrt":
                return SteExpression.CreateFunction("sqrt", argumentsArray);

            case "Floor":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? "floor" : "MathHelper.Floor",
                    argumentsArray
                );

            case "Ceiling":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? "ceiling" : "MathHelper.Ceiling",
                    argumentsArray
                );

            case "Round":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? "round" : "MathHelper.Round",
                    argumentsArray
                );

            case "Min":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? "min" : "MathHelper.Min",
                    argumentsArray
                );

            case "Max":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? "max" : "MathHelper.Max",
                    argumentsArray
                );

            case "Sign":
                return SteExpression.CreateFunction("sign", argumentsArray);

            case "IntegerPart":
                return SteExpression.CreateFunction("truncate", argumentsArray);
        }

        return SteExpression.CreateFunction("MathHelper." + functionName, argumentsArray);
    }

    public override SteExpression Visit(IMetaExpressionNumber numberExpr)
    {
        return numberExpr.HeadSpecs switch
        {
            MetaExpressionHeadSpecsNumberSymbolic symbolicHeadSpecs =>
                symbolicHeadSpecs.HeadText switch
                {
                    "Pi" => SteExpression.CreateSymbolicNumber("pi"),
                    "E" => SteExpression.CreateSymbolicNumber("exp(1)"),
                    _ => numberExpr.ToSimpleTextExpression()
                },

            MetaExpressionHeadSpecsNumberRational rationalHeadSpecs =>
                SteExpression.CreateLiteralNumber(rationalHeadSpecs.NumberFloat64Value),

            _ =>
                numberExpr.ToSimpleTextExpression()
        };
    }
}