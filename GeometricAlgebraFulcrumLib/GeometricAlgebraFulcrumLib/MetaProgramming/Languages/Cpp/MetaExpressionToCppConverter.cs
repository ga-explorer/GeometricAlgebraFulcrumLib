using System.Linq;
using CodeComposerLib.Languages.Cpp;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Languages.Cpp
{
    public sealed class MetaExpressionToCppConverter : 
        MetaExpressionToLanguageConverterBase
    {
        public static MetaExpressionToCppConverter DefaultConverter { get; }
            = new MetaExpressionToCppConverter();


        private MetaExpressionToCppConverter()
            : base(CclCppUtils.Cpp11Info)
        {
            
        }

        
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
                        CclCppUtils.Operators.Add, argumentsArray
                    );

                case "Minus":
                    return SteExpression.CreateOperator(
                        CclCppUtils.Operators.UnaryMinus, argumentsArray
                    );

                case "Subtract":
                    return SteExpression.CreateOperator(
                        CclCppUtils.Operators.Subtract, argumentsArray
                    );

                case "Times":
                    if (argumentsArray[0].ToString() == "-1" && argumentsArray.Length == 2)
                        return SteExpression.CreateOperator(
                            CclCppUtils.Operators.UnaryMinus, argumentsArray[1]
                        );

                    return SteExpression.CreateOperator(
                        CclCppUtils.Operators.Multiply, argumentsArray
                    );

                case "Divide":
                    return SteExpression.CreateOperator(
                        CclCppUtils.Operators.Divide, argumentsArray
                    );

                case "Power":
                    return argumentsArray[1].ToString() switch
                    {
                        "-1" => 
                            SteExpression.CreateOperator(
                                CclCppUtils.Operators.Divide, 
                                SteExpression.CreateLiteralNumber(1), 
                                argumentsArray[0]
                            ),

                        "2" => 
                            SteExpression.CreateOperator(
                                CclCppUtils.Operators.Multiply, 
                                argumentsArray[0], 
                                argumentsArray[0]
                            ),

                        "3" => 
                            SteExpression.CreateOperator(
                                CclCppUtils.Operators.Multiply, 
                                argumentsArray[0],
                                argumentsArray[0], 
                                argumentsArray[0]
                            ),

                        _ => 
                            SteExpression.CreateFunction("pow", argumentsArray)
                    };

                case "Abs":
                    return SteExpression.CreateFunction("fabs", argumentsArray);

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
                    return SteExpression.CreateFunction(
                        argumentsArray.Length == 1 ? "atan" : "atan2",
                        argumentsArray
                    );

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
                    return SteExpression.CreateFunction("floor", argumentsArray);

                case "Ceiling":
                    return SteExpression.CreateFunction("ceil", argumentsArray);

                case "Round":
                    return SteExpression.CreateFunction("round", argumentsArray);

                case "Min":
                    return SteExpression.CreateFunction("fmin", argumentsArray);

                case "Max":
                    return SteExpression.CreateFunction("fmax", argumentsArray);

                //case "Sign":
                //    return SteExpression.CreateFunction("Math.Sign", arguments);

                case "IntegerPart":
                    return SteExpression.CreateFunction("trunc", argumentsArray);
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
                        "Pi" => SteExpression.CreateSymbolicNumber("M_PI"),
                        "E" => SteExpression.CreateSymbolicNumber("M_E"),
                        _ => numberExpr.ToSimpleTextExpression()
                    },

                MetaExpressionHeadSpecsNumberRational rationalHeadSpecs => 
                    SteExpression.CreateLiteralNumber(rationalHeadSpecs.NumberFloat64Value),

                _ => 
                    numberExpr.ToSimpleTextExpression()
            };
        }
    }
}