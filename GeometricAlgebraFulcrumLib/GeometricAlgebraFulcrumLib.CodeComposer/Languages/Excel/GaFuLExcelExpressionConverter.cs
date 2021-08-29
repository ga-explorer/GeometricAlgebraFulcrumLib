using System.Linq;
using CodeComposerLib.Languages.Excel;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Composite;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Numbers;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages.Excel
{
    public sealed class GaFuLExcelExpressionConverter : 
        GaFuLLanguageExpressionConverterBase
    {
        public static GaFuLExcelExpressionConverter DefaultConverter { get; }
            = new GaFuLExcelExpressionConverter();


        private GaFuLExcelExpressionConverter()
            : base(CclExcelUtils.Excel2007Info)
        {

        }

        
        /// <summary>
        /// https://www.excelfunctions.net/Excel-Math-Functions.html
        /// </summary>
        /// <returns></returns>
        public override SteExpression Visit(ISymbolicFunction functionExpr)
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

        public override SteExpression Visit(ISymbolicNumber numberExpr)
        {
            return numberExpr.HeadSpecs switch
            {
                SymbolicHeadSpecsNumberSymbolic symbolicHeadSpecs => 
                    symbolicHeadSpecs.HeadText switch
                    {
                        "Pi" => SteExpression.CreateSymbolicNumber("PI"),
                        "E" => SteExpression.CreateSymbolicNumber("EXP(1)"),
                        _ => numberExpr.ToSimpleTextExpression()
                    },

                SymbolicHeadSpecsNumberRational rationalHeadSpecs => 
                    SteExpression.CreateLiteralNumber(rationalHeadSpecs.NumberValue),

                _ => 
                    numberExpr.ToSimpleTextExpression()
            };
        }
    }
}