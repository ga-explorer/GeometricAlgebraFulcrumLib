using System;
using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Languages.Cpp;
using CodeComposerLib.SyntaxTree.Expressions;

namespace GeometricAlgebraLib.CodeComposer.LanguageServers.Cpp
{
    public sealed class GaClcMathematicaExprToCppConverter : 
        GaClcMathematicaExprConverter
    {
        public GaClcMathematicaExprToCppConverter()
            : base(CppUtils.Cpp11Info)
        {
            
        }


        private static SteExpression ConvertFunction(string functionName, params SteExpression[] arguments)
        {

            switch (functionName)
            {
                //case "Rational":
                //    return SteExpression.CreateOperator(
                //        CppUtils.Operators.Divide, 
                //        SteExpression.CreateLiteralNumber(arguments[0].ToString()),
                //        SteExpression.CreateLiteralNumber(arguments[1].ToString())
                //        );

                case "Plus":
                    return SteExpression.CreateOperator(
                        CppUtils.Operators.Add, arguments
                    );

                case "Minus":
                    return SteExpression.CreateOperator(
                        CppUtils.Operators.UnaryMinus, arguments
                    );

                case "Subtract":
                    return SteExpression.CreateOperator(
                        CppUtils.Operators.Subtract, arguments
                    );

                case "Times":
                    if (arguments[0].ToString() == "-1" && arguments.Length == 2)
                        return SteExpression.CreateOperator(
                            CppUtils.Operators.UnaryMinus, arguments[1]
                        );

                    return SteExpression.CreateOperator(
                        CppUtils.Operators.Multiply, arguments
                    );

                case "Divide":
                    return SteExpression.CreateOperator(
                        CppUtils.Operators.Divide, arguments
                    );

                case "Power":
                    if (arguments[1].ToString() == "-1")
                        return SteExpression.CreateOperator(
                            CppUtils.Operators.Divide,
                            SteExpression.CreateLiteralNumber(1),
                            arguments[0]
                        );

                    if (arguments[1].ToString() == "2")
                        return SteExpression.CreateOperator(
                            CppUtils.Operators.Multiply,
                            arguments[0],
                            arguments[0]
                        );

                    if (arguments[1].ToString() == "3")
                        return SteExpression.CreateOperator(
                            CppUtils.Operators.Multiply,
                            arguments[0],
                            arguments[0],
                            arguments[0]
                        );

                    return SteExpression.CreateFunction("pow", arguments);

                case "Abs":
                    return SteExpression.CreateFunction("fabs", arguments);

                case "Exp":
                    return SteExpression.CreateFunction("exp", arguments);

                case "Sin":
                    return SteExpression.CreateFunction("sin", arguments);

                case "Cos":
                    return SteExpression.CreateFunction("cos", arguments);

                case "Tan":
                    return SteExpression.CreateFunction("tan", arguments);

                case "ArcSin":
                    return SteExpression.CreateFunction("asin", arguments);

                case "ArcCos":
                    return SteExpression.CreateFunction("acos", arguments);

                case "ArcTan":
                    return SteExpression.CreateFunction(
                        arguments.Length == 1 ? "atan" : "atan2",
                        arguments
                    );

                case "Sinh":
                    return SteExpression.CreateFunction("sinh", arguments);

                case "Cosh":
                    return SteExpression.CreateFunction("cosh", arguments);

                case "Tanh":
                    return SteExpression.CreateFunction("tanh", arguments);

                case "Log":
                    return SteExpression.CreateFunction(
                        "log", 
                        arguments.Length == 1 ? arguments : arguments.Reverse()
                    );

                case "Log10":
                    return SteExpression.CreateFunction("log10", arguments);

                case "Sqrt":
                    return SteExpression.CreateFunction("sqrt", arguments);

                case "Floor":
                    return SteExpression.CreateFunction("floor", arguments);

                case "Ceiling":
                    return SteExpression.CreateFunction("ceil", arguments);

                case "Round":
                    return SteExpression.CreateFunction("round", arguments);

                case "Min":
                    return SteExpression.CreateFunction("fmin", arguments);

                case "Max":
                    return SteExpression.CreateFunction("fmax", arguments);

                //case "Sign":
                //    return SteExpression.CreateFunction("Math.Sign", arguments);

                case "IntegerPart":
                    return SteExpression.CreateFunction("trunc", arguments);
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
                        return SteExpression.CreateSymbolicNumber("M_PI");

                    case "E":
                        return SteExpression.CreateSymbolicNumber("M_E");
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
                        return SteExpression.CreateSymbolicNumber("M_PI");

                    case "E":
                        return SteExpression.CreateSymbolicNumber("M_E");
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