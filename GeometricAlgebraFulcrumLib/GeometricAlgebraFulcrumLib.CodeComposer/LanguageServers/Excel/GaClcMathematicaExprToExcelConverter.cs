using System;
using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Languages.Excel;
using CodeComposerLib.SyntaxTree.Expressions;

namespace GeometricAlgebraFulcrumLib.CodeComposer.LanguageServers.Excel
{
    public sealed class GaClcMathematicaExprToExcelConverter : 
        GaClcMathematicaExprConverter
    {
        public GaClcMathematicaExprToExcelConverter()
            : base(ExcelUtils.Excel2007Info)
        {

        }

        /// <summary>
        /// https://www.excelfunctions.net/Excel-Math-Functions.html
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private static SteExpression ConvertFunction(string functionName, params SteExpression[] arguments)
        {
            switch (functionName)
            {
                //case "Rational":
                //    return SteExpression.CreateOperator(
                //        ExcelUtils.Operators.Divide, 
                //        SteExpression.CreateLiteralNumber(arguments[0].ToString()),
                //        SteExpression.CreateLiteralNumber(arguments[1].ToString())
                //        );

                case "Plus":
                    return SteExpression.CreateOperator(
                        ExcelUtils.Operators.Add, arguments
                    );

                case "Minus":
                    return SteExpression.CreateOperator(
                        ExcelUtils.Operators.UnaryMinus, arguments
                    );

                case "Subtract":
                    return SteExpression.CreateOperator(
                        ExcelUtils.Operators.Subtract, arguments
                    );

                case "Times":
                    if (arguments[0].ToString() == "-1" && arguments.Length == 2)
                        return SteExpression.CreateOperator(
                            ExcelUtils.Operators.UnaryMinus, arguments[1]
                        );

                    return SteExpression.CreateOperator(
                        ExcelUtils.Operators.Multiply, arguments
                    );

                case "Divide":
                    return SteExpression.CreateOperator(
                        ExcelUtils.Operators.Divide, arguments
                    );

                case "Power":
                    if (arguments[1].ToString() == "-1")
                        return SteExpression.CreateOperator(
                            ExcelUtils.Operators.Divide,
                            SteExpression.CreateLiteralNumber(1),
                            arguments[0]
                        );

                    if (arguments[1].ToString() == "2")
                        return SteExpression.CreateOperator(
                            ExcelUtils.Operators.Multiply,
                            arguments[0],
                            arguments[0]
                        );

                    if (arguments[1].ToString() == "3")
                        return SteExpression.CreateOperator(
                            ExcelUtils.Operators.Multiply,
                            arguments[0],
                            arguments[0],
                            arguments[0]
                        );

                    return SteExpression.CreateFunction("POWER", arguments);

                case "Abs":
                    return SteExpression.CreateFunction("ABS", arguments);

                case "Exp":
                    return SteExpression.CreateFunction("EXP", arguments);

                case "Sin":
                    return SteExpression.CreateFunction("SIN", arguments);

                case "Cos":
                    return SteExpression.CreateFunction("COS", arguments);

                case "Tan":
                    return SteExpression.CreateFunction("TAN", arguments);

                case "ArcSin":
                    return SteExpression.CreateFunction("ASIN", arguments);

                case "ArcCos":
                    return SteExpression.CreateFunction("ACOS", arguments);

                case "ArcTan":
                    return SteExpression.CreateFunction(
                        arguments.Length == 1 ? "ATAN" : "ATAN2",
                        arguments
                    );

                case "Sinh":
                    return SteExpression.CreateFunction("SINH", arguments);

                case "Cosh":
                    return SteExpression.CreateFunction("COSH", arguments);

                case "Tanh":
                    return SteExpression.CreateFunction("TANH", arguments);

                case "Log":
                    return SteExpression.CreateFunction(
                        arguments.Length == 1 ? "LN" : "LOG",
                        arguments.Length == 1 ? arguments : arguments.Reverse()
                    );

                case "Log10":
                    return SteExpression.CreateFunction("LOG10", arguments);

                case "Sqrt":
                    return SteExpression.CreateFunction("SQRT", arguments);

                case "Floor":
                    return SteExpression.CreateFunction(
                        arguments.Length == 1 ? "FLOOR" : "FLOOR.MATH",
                        arguments
                    );

                case "Ceiling":
                    return SteExpression.CreateFunction(
                        arguments.Length == 1 ? "CEILING" : "CEILING.MATH",
                        arguments
                    );

                case "Round":
                    return SteExpression.CreateFunction(
                        arguments.Length == 1 ? "ROUND" : "MROUND",
                        arguments
                    );

                case "Min":
                    return SteExpression.CreateFunction("MIN", arguments);

                case "Max":
                    return SteExpression.CreateFunction("MAX", arguments);

                case "Sign":
                    return SteExpression.CreateFunction("SIGN", arguments);

                case "IntegerPart":
                    return SteExpression.CreateFunction("TRUNC", arguments);
            }

            return SteExpression.CreateFunction(functionName, arguments);
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
                        return SteExpression.CreateSymbolicNumber("PI");

                    case "E":
                        return SteExpression.CreateSymbolicNumber("EXP(1)");
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
                        return SteExpression.CreateSymbolicNumber("PI");

                    case "E":
                        return SteExpression.CreateSymbolicNumber("EXP(1)");
                }

                return expr.CreateCopy();
            }

            //A function; the arguments are converted before creating the main function expression
            return expr.IsFunction
                ? ConvertFunction(
                    expr.HeadText, 
                    expr.Arguments.Select(a => Convert(a, targetVarsDictionary)).ToArray())
                : expr.CreateCopy();
        }
    }
}