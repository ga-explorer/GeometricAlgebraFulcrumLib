using GeometricAlgebraFulcrumLib.Utilities.Code.Languages.Excel;
using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code.Excel;

public sealed class MetaExpressionToExcelConverter :
    MetaExpressionToLanguageConverterBase
{
    public static MetaExpressionToExcelConverter DefaultConverter { get; }
        = new MetaExpressionToExcelConverter();


    private MetaExpressionToExcelConverter()
        : base(CclExcelUtils.Excel2007Info)
    {

    }


    /// <summary>
    /// https://www.excelfunctions.net/Excel-Math-Functions.html
    /// </summary>
    /// <returns></returns>
    public override SteExpression Visit(IMetaExpressionFunction functionExpr)
    {
        var functionName = functionExpr.HeadSpecs.HeadText;

        var argumentsArray =
            functionExpr.Arguments.Select(Convert).ToArray();

        if (argumentsArray.Length == 0)
            return SteExpression.CreateFunction(functionName);

        switch (functionName)
        {
            case "Plus":
                return SteExpression.CreateOperator(
                    CclExcelUtils.Operators.Add, argumentsArray
                );

            case "Minus":
                return SteExpression.CreateOperator(
                    CclExcelUtils.Operators.UnaryMinus, argumentsArray
                );

            case "Subtract":
                return SteExpression.CreateOperator(
                    CclExcelUtils.Operators.Subtract, argumentsArray
                );

            case "Times":
                if (argumentsArray[0].ToString() == "-1" && argumentsArray.Length == 2)
                    return SteExpression.CreateOperator(
                        CclExcelUtils.Operators.UnaryMinus, argumentsArray[1]
                    );

                return SteExpression.CreateOperator(
                    CclExcelUtils.Operators.Multiply, argumentsArray
                );

            case "Divide":
                return SteExpression.CreateOperator(
                    CclExcelUtils.Operators.Divide, argumentsArray
                );

            case "Power":
                return argumentsArray[1].ToString() switch
                {
                    "-1" =>
                        SteExpression.CreateOperator(
                            CclExcelUtils.Operators.Divide,
                            SteExpression.CreateLiteralNumber(1),
                            argumentsArray[0]
                        ),

                    "2" =>
                        SteExpression.CreateOperator(
                            CclExcelUtils.Operators.Multiply,
                            argumentsArray[0],
                            argumentsArray[0]
                        ),

                    "3" =>
                        SteExpression.CreateOperator(
                            CclExcelUtils.Operators.Multiply,
                            argumentsArray[0],
                            argumentsArray[0],
                            argumentsArray[0]
                        ),

                    _ =>
                        SteExpression.CreateFunction("POWER", argumentsArray)
                };

            case "Abs":
                return SteExpression.CreateFunction("ABS", argumentsArray);

            case "Exp":
                return SteExpression.CreateFunction("EXP", argumentsArray);

            case "Sin":
                return SteExpression.CreateFunction("SIN", argumentsArray);

            case "Cos":
                return SteExpression.CreateFunction("COS", argumentsArray);

            case "Tan":
                return SteExpression.CreateFunction("TAN", argumentsArray);

            case "ArcSin":
                return SteExpression.CreateFunction("ASIN", argumentsArray);

            case "ArcCos":
                return SteExpression.CreateFunction("ACOS", argumentsArray);

            case "ArcTan":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? "ATAN" : "ATAN2",
                    argumentsArray
                );

            case "Sinh":
                return SteExpression.CreateFunction("SINH", argumentsArray);

            case "Cosh":
                return SteExpression.CreateFunction("COSH", argumentsArray);

            case "Tanh":
                return SteExpression.CreateFunction("TANH", argumentsArray);

            case "Log":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? "LN" : "LOG",
                    argumentsArray.Length == 1 ? argumentsArray : argumentsArray.Reverse()
                );

            case "Log10":
                return SteExpression.CreateFunction("LOG10", argumentsArray);

            case "Sqrt":
                return SteExpression.CreateFunction("SQRT", argumentsArray);

            case "Floor":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? "FLOOR" : "FLOOR.MATH",
                    argumentsArray
                );

            case "Ceiling":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? "CEILING" : "CEILING.MATH",
                    argumentsArray
                );

            case "Round":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? "ROUND" : "MROUND",
                    argumentsArray
                );

            case "Min":
                return SteExpression.CreateFunction("MIN", argumentsArray);

            case "Max":
                return SteExpression.CreateFunction("MAX", argumentsArray);

            case "Sign":
                return SteExpression.CreateFunction("SIGN", argumentsArray);

            case "IntegerPart":
                return SteExpression.CreateFunction("TRUNC", argumentsArray);
        }

        return SteExpression.CreateFunction(functionName, argumentsArray);
    }

    public override SteExpression Visit(IMetaExpressionNumber numberExpr)
    {
        return numberExpr.HeadSpecs switch
        {
            MetaExpressionHeadSpecsNumberSymbolic symbolicHeadSpecs =>
                symbolicHeadSpecs.HeadText switch
                {
                    "Pi" => SteExpression.CreateSymbolicNumber("PI"),
                    "E" => SteExpression.CreateSymbolicNumber("EXP(1)"),
                    _ => numberExpr.ToSimpleTextExpression()
                },

            MetaExpressionHeadSpecsNumberRational rationalHeadSpecs =>
                SteExpression.CreateLiteralNumber(rationalHeadSpecs.NumberFloat64Value),

            _ =>
                numberExpr.ToSimpleTextExpression()
        };
    }
}