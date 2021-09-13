using System.Linq;
using CodeComposerLib.Languages.CSharp;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Composite;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Numbers;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages.CSharp
{
    // ReSharper disable once InconsistentNaming
    public sealed class GaFuLCSharpExpressionConverter : 
        GaFuLLanguageExpressionConverterBase
    {
        public static GaFuLCSharpExpressionConverter DefaultConverter { get; }
            = new GaFuLCSharpExpressionConverter();


        private GaFuLCSharpExpressionConverter()
            : base(CclCSharpUtils.CSharp4Info)
        {
            
        }
        

        public override SteExpression Visit(ISymbolicFunction functionExpr)
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
                        CclCSharpUtils.Operators.Add, argumentsArray
                    );

                case "Minus":
                    return SteExpression.CreateOperator(
                        CclCSharpUtils.Operators.UnaryMinus, argumentsArray
                    );

                case "Subtract":
                    return SteExpression.CreateOperator(
                        CclCSharpUtils.Operators.Subtract, argumentsArray
                    );

                case "Times":
                    if (argumentsArray[0].ToString() == "-1" && argumentsArray.Length == 2)
                        return SteExpression.CreateOperator(
                            CclCSharpUtils.Operators.UnaryMinus, argumentsArray[1]
                        );

                    return SteExpression.CreateOperator(
                        CclCSharpUtils.Operators.Multiply, argumentsArray
                    );

                case "Divide":
                    return SteExpression.CreateOperator(
                        CclCSharpUtils.Operators.Divide, argumentsArray
                    );

                case "Power":
                    return argumentsArray[1].ToString() switch
                    {
                        "-1" => SteExpression.CreateOperator(CclCSharpUtils.Operators.Divide,
                            SteExpression.CreateLiteralNumber(1), argumentsArray[0]),
                        "2" => SteExpression.CreateOperator(CclCSharpUtils.Operators.Multiply, argumentsArray[0],
                            argumentsArray[0]),
                        "3" => SteExpression.CreateOperator(CclCSharpUtils.Operators.Multiply, argumentsArray[0],
                            argumentsArray[0], argumentsArray[0]),
                        _ => SteExpression.CreateFunction("Math.Pow", argumentsArray)
                    };

                case "Abs":
                    return SteExpression.CreateFunction("Math.Abs", argumentsArray);

                case "Exp":
                    return SteExpression.CreateFunction("Math.Exp", argumentsArray);

                case "Sin":
                    return SteExpression.CreateFunction("Math.Sin", argumentsArray);

                case "Cos":
                    return SteExpression.CreateFunction("Math.Cos", argumentsArray);

                case "Tan":
                    return SteExpression.CreateFunction("Math.Tan", argumentsArray);

                case "ArcSin":
                    return SteExpression.CreateFunction("Math.Asin", argumentsArray);

                case "ArcCos":
                    return SteExpression.CreateFunction("Math.Acos", argumentsArray);

                case "ArcTan":
                    return SteExpression.CreateFunction(
                        argumentsArray.Length == 1 ? "Math.Atan" : "Math.Atan2",
                        argumentsArray
                    );

                case "Sinh":
                    return SteExpression.CreateFunction("Math.Sinh", argumentsArray);

                case "Cosh":
                    return SteExpression.CreateFunction("Math.Cosh", argumentsArray);

                case "Tanh":
                    return SteExpression.CreateFunction("Math.Tanh", argumentsArray);

                case "Log":
                    return SteExpression.CreateFunction(
                        "Math.Log", 
                        argumentsArray.Length == 1 ? argumentsArray : argumentsArray.Reverse()
                    );

                case "Log10":
                    return SteExpression.CreateFunction("Math.Log10", argumentsArray);

                case "Sqrt":
                    return SteExpression.CreateFunction("Math.Sqrt", argumentsArray);

                case "Floor":
                    return SteExpression.CreateFunction(
                        argumentsArray.Length == 1 ? "Math.Floor" : "MathHelper.Floor",
                        argumentsArray
                    );

                case "Ceiling":
                    return SteExpression.CreateFunction(
                        argumentsArray.Length == 1 ? "Math.Ceiling" : "MathHelper.Ceiling",
                        argumentsArray
                    );

                case "Round":
                    return SteExpression.CreateFunction(
                        argumentsArray.Length == 1 ? "Math.Round" : "MathHelper.Round",
                        argumentsArray
                    );

                case "Min":
                    return SteExpression.CreateFunction(
                        argumentsArray.Length == 1 ? "Math.Min" : "MathHelper.Min",
                        argumentsArray
                    );

                case "Max":
                    return SteExpression.CreateFunction(
                        argumentsArray.Length == 1 ? "Math.Max" : "MathHelper.Max",
                        argumentsArray
                    );

                case "Sign":
                    return SteExpression.CreateFunction("Math.Sign", argumentsArray);

                case "IntegerPart":
                    return SteExpression.CreateFunction("Math.Truncate", argumentsArray);
            }

            return SteExpression.CreateFunction("MathHelper." + functionName, argumentsArray);
        }

        public override SteExpression Visit(ISymbolicNumber numberExpr)
        {
            return numberExpr.HeadSpecs switch
            {
                SymbolicHeadSpecsNumberSymbolic symbolicHeadSpecs => 
                    symbolicHeadSpecs.HeadText switch
                    {
                        "Pi" => SteExpression.CreateSymbolicNumber("Math.PI"),
                        "E" => SteExpression.CreateSymbolicNumber("Math.E"),
                        _ => numberExpr.ToSimpleTextExpression()
                    },

                SymbolicHeadSpecsNumberRational rationalHeadSpecs => 
                    SteExpression.CreateLiteralNumber(rationalHeadSpecs.NumberFloat64Value),

                _ => 
                    numberExpr.ToSimpleTextExpression()
            };
        }
    }
}
