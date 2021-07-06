using System;
using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Languages.CSharp;
using CodeComposerLib.SyntaxTree.Expressions;

namespace GeometricAlgebraFulcrumLib.CodeComposer.LanguageServers.CSharp
{
    public sealed class GaClcMathematicaExprToCSharpConverter : 
        GaClcMathematicaExprConverter
    {
        public GaClcMathematicaExprToCSharpConverter()
            : base(CSharpUtils.CSharp4Info)
        {
            
        }


        private static SteExpression ConvertFunction(string functionName, params SteExpression[] arguments)
        {

            switch (functionName)
            {
                //case "Rational":
                //    return SteExpression.CreateOperator(
                //        CSharpUtils.Operators.Divide, 
                //        SteExpression.CreateLiteralNumber(arguments[0].ToString()),
                //        SteExpression.CreateLiteralNumber(arguments[1].ToString())
                //        );

                case "Plus":
                    return SteExpression.CreateOperator(
                        CSharpUtils.Operators.Add, arguments
                        );

                case "Minus":
                    return SteExpression.CreateOperator(
                        CSharpUtils.Operators.UnaryMinus, arguments
                        );

                case "Subtract":
                    return SteExpression.CreateOperator(
                        CSharpUtils.Operators.Subtract, arguments
                        );

                case "Times":
                    if (arguments[0].ToString() == "-1" && arguments.Length == 2)
                        return SteExpression.CreateOperator(
                            CSharpUtils.Operators.UnaryMinus, arguments[1]
                        );

                    return SteExpression.CreateOperator(
                        CSharpUtils.Operators.Multiply, arguments
                        );

                case "Divide":
                    return SteExpression.CreateOperator(
                        CSharpUtils.Operators.Divide, arguments
                        );

                case "Power":
                    if (arguments[1].ToString() == "-1")
                        return SteExpression.CreateOperator(
                            CSharpUtils.Operators.Divide,
                            SteExpression.CreateLiteralNumber(1),
                            arguments[0]
                        );

                    if (arguments[1].ToString() == "2")
                        return SteExpression.CreateOperator(
                            CSharpUtils.Operators.Multiply,
                            arguments[0],
                            arguments[0]
                        );

                    if (arguments[1].ToString() == "3")
                        return SteExpression.CreateOperator(
                            CSharpUtils.Operators.Multiply,
                            arguments[0],
                            arguments[0],
                            arguments[0]
                        );

                    return SteExpression.CreateFunction("Math.Pow", arguments);

                case "Abs":
                    return SteExpression.CreateFunction("Math.Abs", arguments);

                case "Exp":
                    return SteExpression.CreateFunction("Math.Exp", arguments);

                case "Sin":
                    return SteExpression.CreateFunction("Math.Sin", arguments);

                case "Cos":
                    return SteExpression.CreateFunction("Math.Cos", arguments);

                case "Tan":
                    return SteExpression.CreateFunction("Math.Tan", arguments);

                case "ArcSin":
                    return SteExpression.CreateFunction("Math.Asin", arguments);

                case "ArcCos":
                    return SteExpression.CreateFunction("Math.Acos", arguments);

                case "ArcTan":
                    return SteExpression.CreateFunction(
                        arguments.Length == 1 ? "Math.Atan" : "Math.Atan2",
                        arguments
                        );

                case "Sinh":
                    return SteExpression.CreateFunction("Math.Sinh", arguments);

                case "Cosh":
                    return SteExpression.CreateFunction("Math.Cosh", arguments);

                case "Tanh":
                    return SteExpression.CreateFunction("Math.Tanh", arguments);

                case "Log":
                    return SteExpression.CreateFunction(
                        "Math.Log", 
                        arguments.Length == 1 ? arguments : arguments.Reverse()
                        );

                case "Log10":
                    return SteExpression.CreateFunction("Math.Log10", arguments);

                case "Sqrt":
                    return SteExpression.CreateFunction("Math.Sqrt", arguments);

                case "Floor":
                    return SteExpression.CreateFunction(
                        arguments.Length == 1 ? "Math.Floor" : "MathHelper.Floor",
                        arguments
                        );

                case "Ceiling":
                    return SteExpression.CreateFunction(
                        arguments.Length == 1 ? "Math.Ceiling" : "MathHelper.Ceiling",
                        arguments
                        );

                case "Round":
                    return SteExpression.CreateFunction(
                        arguments.Length == 1 ? "Math.Round" : "MathHelper.Round",
                        arguments
                        );

                case "Min":
                    return SteExpression.CreateFunction(
                        arguments.Length == 1 ? "Math.Min" : "MathHelper.Min",
                        arguments
                        );

                case "Max":
                    return SteExpression.CreateFunction(
                        arguments.Length == 1 ? "Math.Max" : "MathHelper.Max",
                        arguments
                        );

                case "Sign":
                    return SteExpression.CreateFunction("Math.Sign", arguments);

                case "IntegerPart":
                    return SteExpression.CreateFunction("Math.Truncate", arguments);
            }

            return SteExpression.CreateFunction("MathHelper." + functionName, arguments);
        }

        private static SteExpression ConvertNumber(SteExpression numberExpr)
        {
            var rationalHeadIndex = numberExpr.HeadText.IndexOf(@"Rational[", StringComparison.Ordinal);

            //This is an ordinary number, return as-is
            if (rationalHeadIndex < 0)
            {
                return numberExpr.CreateCopy();
            }

            //This is a rational atomic number; for example Rational[1, 2]. 
            //Extract components and convert to floating point
            var numberTextFull = numberExpr.HeadText.Substring(@"Rational[".Length);
            var commaIndex = numberTextFull.IndexOf(',');
            var bracketIndex = numberTextFull.IndexOf(']');

            var num1Text = numberTextFull.Substring(0, commaIndex);
            var num2Text = numberTextFull.Substring(commaIndex + 1, bracketIndex - commaIndex - 1);

            return SteExpression.CreateLiteralNumber(
                double.Parse(num1Text) / double.Parse(num2Text)
                );
        }

        public override SteExpression Convert(SteExpression expr)
        {
            //A number
            if (expr.IsNumberLiteral) 
                return ConvertNumber(expr);
            //This has a problem with the Rational[] numbers
            //expr.CreateCopy(); 


            //A variable
            if (expr.IsVariable)
            {
                //Try convert a low-level Mathematica variable name into a target variable name

                if (ActiveContext != null && ActiveContext.TryGetAtomic(expr.HeadText, out var targetVar))
                    return SteExpression.CreateVariable(targetVar.ExternalName);

                return expr.CreateCopy();
            }

            //A symbolic constant
            if (expr.IsNumberSymbol)
            {
                switch (expr.HeadText)
                {
                    case "Pi":
                        return SteExpression.CreateSymbolicNumber("Math.PI");

                    case "E":
                        return SteExpression.CreateSymbolicNumber("Math.E");
                }

                return expr.CreateCopy();
            }

            //A function; the arguments are converted before creating the main function expression
            return expr.IsFunction 
                ? ConvertFunction(
                    expr.HeadText, 
                    expr.Arguments.Select(Convert).ToArray()
                ) 
                : expr.CreateCopy();
        }

        public override SteExpression Convert(SteExpression expr, IDictionary<string, string> targetVarsDictionary)
        {
            //A number
            if (expr.IsNumberLiteral)
                return ConvertNumber(expr);
            //This has a problem with the Rational[] numbers
            //expr.CreateCopy(); 

            //A variable
            if (expr.IsVariable)
            {
                //Try convert a low-level Mathematica variable name into a target variable name

                if (targetVarsDictionary != null && targetVarsDictionary.TryGetValue(expr.HeadText, out var targetVarName))
                    return SteExpression.CreateVariable(targetVarName);

                return expr.CreateCopy();
            }

            //A symbolic constant
            if (expr.IsNumberSymbol)
            {
                switch (expr.HeadText)
                {
                    case "Pi":
                        return SteExpression.CreateSymbolicNumber("Math.PI");

                    case "E":
                        return SteExpression.CreateSymbolicNumber("Math.E");
                }

                return expr.CreateCopy();
            }

            //A function; the arguments are converted before creating the main function expression
            return expr.IsFunction
                ? ConvertFunction(
                    expr.HeadText,
                    expr.Arguments.Select(a => Convert(a, targetVarsDictionary)).ToArray()
                )
                : expr.CreateCopy();
        }
    }
}
