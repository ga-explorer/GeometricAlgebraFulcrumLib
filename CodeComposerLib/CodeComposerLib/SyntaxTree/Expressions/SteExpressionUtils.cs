using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeComposerLib.SyntaxTree.Expressions
{
    public static class SteExpressionUtils
    {
        /// <summary>
        /// Create an expression from a variable name
        /// </summary>
        /// <param name="varName"></param>
        /// <returns></returns>
        public static SteExpression CreateVariable(string varName)
        {
            if (String.IsNullOrEmpty(varName))
                throw new ArgumentNullException(nameof(varName), @"Variable name not initialized");

            return new SteExpression(new SteVariableHeadSpecs(varName));
        }

        /// <summary>
        /// Create a symbolic number like "Pi" and "e".
        /// </summary>
        /// <param name="numberText"></param>
        /// <returns></returns>
        public static SteExpression CreateSymbolicNumber(string numberText)
        {
            if (String.IsNullOrEmpty(numberText))
                throw new ArgumentNullException(nameof(numberText), @"Number not initialized");

            return new SteExpression(new SteNumberHeadSpecs(numberText, true));
        }

        /// <summary>
        /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static SteExpression CreateLiteralNumber(int number)
        {
            return new SteExpression(new SteNumberHeadSpecs(number));
        }

        /// <summary>
        /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static SteExpression CreateLiteralNumber(double number)
        {
            return new SteExpression(new SteNumberHeadSpecs(number));
        }

        /// <summary>
        /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static SteExpression CreateLiteralNumber(float number)
        {
            return new SteExpression(new SteNumberHeadSpecs(number));
        }

        /// <summary>
        /// Create a literal number expression like "2", "-1.7" and "3.56e-4"
        /// </summary>
        /// <param name="numberText"></param>
        /// <returns></returns>
        public static SteExpression CreateLiteralNumber(string numberText)
        {
            if (String.IsNullOrEmpty(numberText))
                throw new ArgumentNullException(nameof(numberText), @"Number not initialized");

            return new SteExpression(new SteNumberHeadSpecs(numberText, false));
        }

        /// <summary>
        /// Create a function expression with no arguments
        /// </summary>
        /// <param name="funcName"></param>
        /// <returns></returns>
        public static SteExpression CreateFunction(string funcName)
        {
            if (String.IsNullOrEmpty(funcName))
                throw new ArgumentNullException(nameof(funcName), @"Function name not initialized");

            return new SteExpression(new SteFunctionHeadSpecs(funcName), Enumerable.Empty<SteExpression>());
        }

        /// <summary>
        /// Create a function expression with some arguments
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SteExpression CreateFunction(string funcName, params SteExpression[] args)
        {
            if (String.IsNullOrEmpty(funcName))
                throw new ArgumentNullException(nameof(funcName), @"Function name not initialized");

            var funcHeadSpecs = new SteFunctionHeadSpecs(funcName);

            return args == null
                ? new SteExpression(funcHeadSpecs, Enumerable.Empty<SteExpression>())
                : new SteExpression(funcHeadSpecs, args);
        }

        /// <summary>
        /// Create a function expression with some arguments
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SteExpression CreateFunction(string funcName, IEnumerable<SteExpression> args)
        {
            if (String.IsNullOrEmpty(funcName))
                throw new ArgumentNullException(nameof(funcName), @"Function name not initialized");

            var funcHeadSpecs = new SteFunctionHeadSpecs(funcName);

            return args == null
                ? new SteExpression(funcHeadSpecs, Enumerable.Empty<SteExpression>())
                : new SteExpression(funcHeadSpecs, args);
        }

        /// <summary>
        /// Create a function expression with no arguments
        /// </summary>
        /// <param name="arrayName"></param>
        /// <returns></returns>
        public static SteExpression CreateArrayAccess(string arrayName)
        {
            if (String.IsNullOrEmpty(arrayName))
                throw new ArgumentNullException(nameof(arrayName), @"Array name not initialized");

            return new SteExpression(new SteArrayAccessHeadSpecs(arrayName), Enumerable.Empty<SteExpression>());
        }

        /// <summary>
        /// Create a function expression with some arguments
        /// </summary>
        /// <param name="arrayName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SteExpression CreateArrayAccess(string arrayName, params SteExpression[] args)
        {
            if (String.IsNullOrEmpty(arrayName))
                throw new ArgumentNullException(nameof(arrayName), @"Array name not initialized");

            var funcHeadSpecs = new SteArrayAccessHeadSpecs(arrayName);

            return args == null
                ? new SteExpression(funcHeadSpecs, Enumerable.Empty<SteExpression>())
                : new SteExpression(funcHeadSpecs, args);
        }

        /// <summary>
        /// Create a function expression with some arguments
        /// </summary>
        /// <param name="arrayName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SteExpression CreateArrayAccess(string arrayName, IEnumerable<SteExpression> args)
        {
            if (String.IsNullOrEmpty(arrayName))
                throw new ArgumentNullException(nameof(arrayName), @"Array name not initialized");

            var funcHeadSpecs = new SteArrayAccessHeadSpecs(arrayName);

            return args == null
                ? new SteExpression(funcHeadSpecs, Enumerable.Empty<SteExpression>())
                : new SteExpression(funcHeadSpecs, args);
        }

        /// <summary>
        /// Create an operator expression without any arguments
        /// </summary>
        /// <param name="opHeadSpecs"></param>
        /// <returns></returns>
        public static SteExpression CreateOperator(SteOperatorSpecs opHeadSpecs)
        {
            if (ReferenceEquals(opHeadSpecs, null))
                throw new ArgumentNullException(nameof(opHeadSpecs), @"Expression head not initialized");

            return new SteExpression(opHeadSpecs, Enumerable.Empty<SteExpression>());
        }

        /// <summary>
        /// Create an operator expression from a head and a set of expressions as its arguments
        /// </summary>
        /// <param name="opHeadSpecs"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SteExpression CreateOperator(SteOperatorSpecs opHeadSpecs, params SteExpression[] args)
        {
            if (ReferenceEquals(opHeadSpecs, null))
                throw new ArgumentNullException(nameof(opHeadSpecs), @"Expression head not initialized");

            return args == null
                ? new SteExpression(opHeadSpecs, Enumerable.Empty<SteExpression>())
                : new SteExpression(opHeadSpecs, args);
        }

        /// <summary>
        /// Create an operator expression from a head and a set of expressions as its arguments
        /// </summary>
        /// <param name="opHeadSpecs"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SteExpression CreateOperator(SteOperatorSpecs opHeadSpecs, IEnumerable<SteExpression> args)
        {
            if (ReferenceEquals(opHeadSpecs, null))
                throw new ArgumentNullException(nameof(opHeadSpecs), @"Expression head not initialized");

            return args == null
                ? new SteExpression(opHeadSpecs, Enumerable.Empty<SteExpression>())
                : new SteExpression(opHeadSpecs, args);
        }


        public static SteExpression ResetAsVariable(this SteExpression expr, string varName)
        {
            expr.Reset(new SteVariableHeadSpecs(varName));

            return expr;
        }

        public static SteExpression ResetAsSymbolicNumber(this SteExpression expr, string numberText)
        {
            expr.Reset(new SteNumberHeadSpecs(numberText, true));

            return expr;
        }

        public static SteExpression ResetAsLiteralNumber(this SteExpression expr, string numberText)
        {
            expr.Reset(new SteNumberHeadSpecs(numberText, false));

            return expr;
        }

        public static SteExpression ResetAsLiteralNumber(this SteExpression expr, int number)
        {
            expr.Reset(new SteNumberHeadSpecs(number));

            return expr;
        }

        public static SteExpression ResetAsLiteralNumber(this SteExpression expr, double number)
        {
            expr.Reset(new SteNumberHeadSpecs(number));

            return expr;
        }

        public static SteExpression ResetAsLiteralNumber(this SteExpression expr, float number)
        {
            expr.Reset(new SteNumberHeadSpecs(number));

            return expr;
        }

        public static SteExpression ResetAsFunction(this SteExpression expr, string funcName, bool clearArgs = true)
        {
            var headSpecs = new SteFunctionHeadSpecs(funcName);

            if (clearArgs)
                expr.Reset(headSpecs);
            else
                expr.ResetHead(headSpecs);

            return expr;
        }

        public static SteExpression ResetAsFunction(this SteExpression expr, string funcName, params SteExpression[] args)
        {
            var headSpecs = new SteFunctionHeadSpecs(funcName);

            expr.Reset(headSpecs, args);

            return expr;
        }

        public static SteExpression ResetAsFunction(this SteExpression expr, string funcName, IEnumerable<SteExpression> args)
        {
            var headSpecs = new SteFunctionHeadSpecs(funcName);

            expr.Reset(headSpecs, args);

            return expr;
        }

        public static SteExpression ResetAsArrayAccess(this SteExpression expr, string arrayName, bool clearArgs = true)
        {
            var headSpecs = new SteArrayAccessHeadSpecs(arrayName);

            if (clearArgs)
                expr.Reset(headSpecs);
            else
                expr.ResetHead(headSpecs);

            return expr;
        }

        public static SteExpression ResetAsArrayAccess(this SteExpression expr, string arrayName, params SteExpression[] args)
        {
            var headSpecs = new SteArrayAccessHeadSpecs(arrayName);

            expr.Reset(headSpecs, args);

            return expr;
        }

        public static SteExpression ResetAsArrayAccess(this SteExpression expr, string arrayName, IEnumerable<SteExpression> args)
        {
            var headSpecs = new SteArrayAccessHeadSpecs(arrayName);

            expr.Reset(headSpecs, args);

            return expr;
        }

        public static SteExpression ResetAsOperator(this SteExpression expr, SteOperatorSpecs opHeadSpecs, bool clearArgs = true)
        {
            var headSpecs = opHeadSpecs;

            if (clearArgs)
                expr.Reset(headSpecs);
            else
                expr.ResetHead(headSpecs);

            return expr;
        }

        public static SteExpression ResetAsOperator(this SteExpression expr, SteOperatorSpecs opHeadSpecs, params SteExpression[] args)
        {
            var headSpecs = opHeadSpecs;

            expr.Reset(headSpecs, args);

            return expr;
        }

        public static SteExpression ResetAsOperator(this SteExpression expr, SteOperatorSpecs opHeadSpecs, IEnumerable<SteExpression> args)
        {
            var headSpecs = opHeadSpecs;

            expr.Reset(headSpecs, args);

            return expr;
        }


        public static SteExpression AddVariable(this SteExpression expr, string varName)
        {
            return expr.AddArgument(CreateVariable(varName));
        }

        public static SteExpression AddVariables(this SteExpression expr, IEnumerable<string> varNamesList)
        {
            return expr.AddArguments(varNamesList.Select(CreateVariable));
        }

        public static SteExpression AddVariables(this SteExpression expr, params string[] varNamesList)
        {
            return expr.AddArguments(varNamesList.Select(CreateVariable));
        }

        public static SteExpression AddSymbolicNumber(this SteExpression expr, string numberText)
        {
            return expr.AddArgument(CreateSymbolicNumber(numberText));
        }

        public static SteExpression AddLiteralNumber(this SteExpression expr, string numberText)
        {
            return expr.AddArgument(CreateLiteralNumber(numberText));
        }

        public static SteExpression AddLiteralNumbers(this SteExpression expr, IEnumerable<string> numbersTextList)
        {
            return expr.AddArguments(numbersTextList.Select(CreateLiteralNumber));
        }

        public static SteExpression AddLiteralNumbers(this SteExpression expr, params string[] numbersTextList)
        {
            return expr.AddArguments(numbersTextList.Select(CreateLiteralNumber));
        }

        public static SteExpression AddLiteralNumber(this SteExpression expr, int number)
        {
            return expr.AddArgument(CreateLiteralNumber(number));
        }

        public static SteExpression AddLiteralNumbers(this SteExpression expr, IEnumerable<int> numbersList)
        {
            return expr.AddArguments(numbersList.Select(CreateLiteralNumber));
        }

        public static SteExpression AddLiteralNumbers(this SteExpression expr, params int[] numbersList)
        {
            return expr.AddArguments(numbersList.Select(CreateLiteralNumber));
        }

        public static SteExpression AddLiteralNumber(this SteExpression expr, double number)
        {
            return expr.AddArgument(CreateLiteralNumber(number));
        }

        public static SteExpression AddLiteralNumbers(this SteExpression expr, IEnumerable<double> numbersList)
        {
            return expr.AddArguments(numbersList.Select(CreateLiteralNumber));
        }

        public static SteExpression AddLiteralNumbers(this SteExpression expr, params double[] numbersList)
        {
            return expr.AddArguments(numbersList.Select(CreateLiteralNumber));
        }

        public static SteExpression AddLiteralNumber(this SteExpression expr, float number)
        {
            return expr.AddArgument(CreateLiteralNumber(number));
        }

        public static SteExpression AddLiteralNumbers(this SteExpression expr, IEnumerable<float> numbersList)
        {
            return expr.AddArguments(numbersList.Select(CreateLiteralNumber));
        }

        public static SteExpression AddLiteralNumbers(this SteExpression expr, params float[] numbersList)
        {
            return expr.AddArguments(numbersList.Select(CreateLiteralNumber));
        }

        public static SteExpression AddFunction(this SteExpression expr, string head)
        {
            return expr.AddArgument(CreateFunction(head));
        }

        public static SteExpression AddFunction(this SteExpression expr, string head, params SteExpression[] args)
        {
            return expr.AddArgument(CreateFunction(head, args));
        }

        public static SteExpression AddFunction(this SteExpression expr, string head, IEnumerable<SteExpression> args)
        {
            return expr.AddArgument(CreateFunction(head, args));
        }

        public static SteExpression AddArrayAccess(this SteExpression expr, string head)
        {
            return expr.AddArgument(CreateArrayAccess(head));
        }

        public static SteExpression AddArrayAccess(this SteExpression expr, string head, params SteExpression[] args)
        {
            return expr.AddArgument(CreateArrayAccess(head, args));
        }

        public static SteExpression AddArrayAccess(this SteExpression expr, string head, IEnumerable<SteExpression> args)
        {
            return expr.AddArgument(CreateArrayAccess(head, args));
        }

        public static SteExpression AddOperator(this SteExpression expr, SteOperatorSpecs head)
        {
            return expr.AddArgument(CreateOperator(head));
        }

        public static SteExpression AddOperator(this SteExpression expr, SteOperatorSpecs head, params SteExpression[] args)
        {
            return expr.AddArgument(CreateOperator(head, args));
        }

        public static SteExpression AddOperator(this SteExpression expr, SteOperatorSpecs head, IEnumerable<SteExpression> args)
        {
            return expr.AddArgument(CreateOperator(head, args));
        }


        public static SteExpression InsertVariable(this SteExpression expr, int index, string varName)
        {
            return expr.InsertArgument(index, CreateVariable(varName));
        }

        public static SteExpression InsertVariables(this SteExpression expr, int index, IEnumerable<string> varNamesList)
        {
            return expr.InsertArguments(index, varNamesList.Select(CreateVariable));
        }

        public static SteExpression InsertVariables(this SteExpression expr, int index, params string[] varNamesList)
        {
            return expr.InsertArguments(index, varNamesList.Select(CreateVariable));
        }

        public static SteExpression InsertSymbolicNumber(this SteExpression expr, int index, string numberText)
        {
            return expr.InsertArgument(index, CreateSymbolicNumber(numberText));
        }

        public static SteExpression InsertLiteralNumber(this SteExpression expr, int index, string numberText)
        {
            return expr.InsertArgument(index, CreateLiteralNumber(numberText));
        }

        public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, IEnumerable<string> numbersTextList)
        {
            return expr.InsertArguments(index, numbersTextList.Select(CreateLiteralNumber));
        }

        public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, params string[] numbersTextList)
        {
            return expr.InsertArguments(index, numbersTextList.Select(CreateLiteralNumber));
        }

        public static SteExpression InsertLiteralNumber(this SteExpression expr, int index, int number)
        {
            return expr.InsertArgument(index, CreateLiteralNumber(number));
        }

        public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, IEnumerable<int> numbersList)
        {
            return expr.InsertArguments(index, numbersList.Select(CreateLiteralNumber));
        }

        public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, params int[] numbersList)
        {
            return expr.InsertArguments(index, numbersList.Select(CreateLiteralNumber));
        }

        public static SteExpression InsertLiteralNumber(this SteExpression expr, int index, double number)
        {
            return expr.InsertArgument(index, CreateLiteralNumber(number));
        }

        public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, IEnumerable<double> numbersList)
        {
            return expr.InsertArguments(index, numbersList.Select(CreateLiteralNumber));
        }

        public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, params double[] numbersList)
        {
            return expr.InsertArguments(index, numbersList.Select(CreateLiteralNumber));
        }

        public static SteExpression InsertLiteralNumber(this SteExpression expr, int index, float number)
        {
            return expr.InsertArgument(index, CreateLiteralNumber(number));
        }

        public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, IEnumerable<float> numbersList)
        {
            return expr.InsertArguments(index, numbersList.Select(CreateLiteralNumber));
        }

        public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, params float[] numbersList)
        {
            return expr.InsertArguments(index, numbersList.Select(CreateLiteralNumber));
        }

        public static SteExpression InsertFunction(this SteExpression expr, int index, string head)
        {
            return expr.InsertArgument(index, CreateFunction(head));
        }

        public static SteExpression InsertFunction(this SteExpression expr, int index, string head, params SteExpression[] args)
        {
            return expr.InsertArgument(index, CreateFunction(head, args));
        }

        public static SteExpression InsertFunction(this SteExpression expr, int index, string head, IEnumerable<SteExpression> args)
        {
            return expr.InsertArgument(index, CreateFunction(head, args));
        }

        public static SteExpression InsertOperator(this SteExpression expr, int index, SteOperatorSpecs head)
        {
            return expr.InsertArgument(index, CreateOperator(head));
        }

        public static SteExpression InsertOperator(this SteExpression expr, int index, SteOperatorSpecs head, params SteExpression[] args)
        {
            return expr.InsertArgument(index, CreateOperator(head, args));
        }

        public static SteExpression InsertOperator(this SteExpression expr, int index, SteOperatorSpecs head, IEnumerable<SteExpression> args)
        {
            return expr.InsertArgument(index, CreateOperator(head, args));
        }


    }
}
