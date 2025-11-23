namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;

public static class SteExpressionUtils
{
    public static IEnumerable<SteExpression> CreateCopy(this IEnumerable<SteExpression> exprList)
    {
        return exprList?.Select(
            a => a?.CreateCopy()
        );
    }

    public static bool Equals(this SteExpression symbolicExpr1, SteExpression symbolicExpr2)
    {
        if (ReferenceEquals(symbolicExpr1, null) || ReferenceEquals(symbolicExpr2, null))
            return false;

        if (ReferenceEquals(symbolicExpr1, symbolicExpr2))
            return true;

        if (symbolicExpr1.HeadText != symbolicExpr2.HeadText)
            return false;

        if (symbolicExpr1.ArgumentsCount == 0 && symbolicExpr2.ArgumentsCount == 0)
            return true;

        if (symbolicExpr1.ArgumentsCount != symbolicExpr2.ArgumentsCount)
            return false;

        return symbolicExpr1.Arguments.Zip(symbolicExpr2.Arguments, (t, s) => t.Equals(s)).All(b => b);
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
        return expr.AddArgument(SteExpression.CreateVariable(varName));
    }

    public static SteExpression AddVariables(this SteExpression expr, IEnumerable<string> varNamesList)
    {
        return expr.AddArguments(varNamesList.Select(SteExpression.CreateVariable));
    }

    public static SteExpression AddVariables(this SteExpression expr, params string[] varNamesList)
    {
        return expr.AddArguments(varNamesList.Select(SteExpression.CreateVariable));
    }

    public static SteExpression AddSymbolicNumber(this SteExpression expr, string numberText)
    {
        return expr.AddArgument(SteExpression.CreateSymbolicNumber(numberText));
    }

    public static SteExpression AddLiteralNumber(this SteExpression expr, string numberText)
    {
        return expr.AddArgument(SteExpression.CreateLiteralNumber(numberText));
    }

    public static SteExpression AddLiteralNumbers(this SteExpression expr, IEnumerable<string> numbersTextList)
    {
        return expr.AddArguments(numbersTextList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression AddLiteralNumbers(this SteExpression expr, params string[] numbersTextList)
    {
        return expr.AddArguments(numbersTextList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression AddLiteralNumber(this SteExpression expr, int number)
    {
        return expr.AddArgument(SteExpression.CreateLiteralNumber(number));
    }

    public static SteExpression AddLiteralNumbers(this SteExpression expr, IEnumerable<int> numbersList)
    {
        return expr.AddArguments(numbersList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression AddLiteralNumbers(this SteExpression expr, params int[] numbersList)
    {
        return expr.AddArguments(numbersList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression AddLiteralNumber(this SteExpression expr, double number)
    {
        return expr.AddArgument(SteExpression.CreateLiteralNumber(number));
    }

    public static SteExpression AddLiteralNumbers(this SteExpression expr, IEnumerable<double> numbersList)
    {
        return expr.AddArguments(numbersList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression AddLiteralNumbers(this SteExpression expr, params double[] numbersList)
    {
        return expr.AddArguments(numbersList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression AddLiteralNumber(this SteExpression expr, float number)
    {
        return expr.AddArgument(SteExpression.CreateLiteralNumber(number));
    }

    public static SteExpression AddLiteralNumbers(this SteExpression expr, IEnumerable<float> numbersList)
    {
        return expr.AddArguments(numbersList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression AddLiteralNumbers(this SteExpression expr, params float[] numbersList)
    {
        return expr.AddArguments(numbersList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression AddFunction(this SteExpression expr, string head)
    {
        return expr.AddArgument(SteExpression.CreateFunction(head));
    }

    public static SteExpression AddFunction(this SteExpression expr, string head, params SteExpression[] args)
    {
        return expr.AddArgument(SteExpression.CreateFunction(head, args));
    }

    public static SteExpression AddFunction(this SteExpression expr, string head, IEnumerable<SteExpression> args)
    {
        return expr.AddArgument(SteExpression.CreateFunction(head, args));
    }

    public static SteExpression AddArrayAccess(this SteExpression expr, string head)
    {
        return expr.AddArgument(SteExpression.CreateArrayAccess(head));
    }

    public static SteExpression AddArrayAccess(this SteExpression expr, string head, params SteExpression[] args)
    {
        return expr.AddArgument(SteExpression.CreateArrayAccess(head, args));
    }

    public static SteExpression AddArrayAccess(this SteExpression expr, string head, IEnumerable<SteExpression> args)
    {
        return expr.AddArgument(SteExpression.CreateArrayAccess(head, args));
    }

    public static SteExpression AddOperator(this SteExpression expr, SteOperatorSpecs head)
    {
        return expr.AddArgument(SteExpression.CreateOperator(head));
    }

    public static SteExpression AddOperator(this SteExpression expr, SteOperatorSpecs head, params SteExpression[] args)
    {
        return expr.AddArgument(SteExpression.CreateOperator(head, args));
    }

    public static SteExpression AddOperator(this SteExpression expr, SteOperatorSpecs head, IEnumerable<SteExpression> args)
    {
        return expr.AddArgument(SteExpression.CreateOperator(head, args));
    }


    public static SteExpression InsertVariable(this SteExpression expr, int index, string varName)
    {
        return expr.InsertArgument(index, SteExpression.CreateVariable(varName));
    }

    public static SteExpression InsertVariables(this SteExpression expr, int index, IEnumerable<string> varNamesList)
    {
        return expr.InsertArguments(index, varNamesList.Select(SteExpression.CreateVariable));
    }

    public static SteExpression InsertVariables(this SteExpression expr, int index, params string[] varNamesList)
    {
        return expr.InsertArguments(index, varNamesList.Select(SteExpression.CreateVariable));
    }

    public static SteExpression InsertSymbolicNumber(this SteExpression expr, int index, string numberText)
    {
        return expr.InsertArgument(index, SteExpression.CreateSymbolicNumber(numberText));
    }

    public static SteExpression InsertLiteralNumber(this SteExpression expr, int index, string numberText)
    {
        return expr.InsertArgument(index, SteExpression.CreateLiteralNumber(numberText));
    }

    public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, IEnumerable<string> numbersTextList)
    {
        return expr.InsertArguments(index, numbersTextList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, params string[] numbersTextList)
    {
        return expr.InsertArguments(index, numbersTextList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression InsertLiteralNumber(this SteExpression expr, int index, int number)
    {
        return expr.InsertArgument(index, SteExpression.CreateLiteralNumber(number));
    }

    public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, IEnumerable<int> numbersList)
    {
        return expr.InsertArguments(index, numbersList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, params int[] numbersList)
    {
        return expr.InsertArguments(index, numbersList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression InsertLiteralNumber(this SteExpression expr, int index, double number)
    {
        return expr.InsertArgument(index, SteExpression.CreateLiteralNumber(number));
    }

    public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, IEnumerable<double> numbersList)
    {
        return expr.InsertArguments(index, numbersList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, params double[] numbersList)
    {
        return expr.InsertArguments(index, numbersList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression InsertLiteralNumber(this SteExpression expr, int index, float number)
    {
        return expr.InsertArgument(index, SteExpression.CreateLiteralNumber(number));
    }

    public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, IEnumerable<float> numbersList)
    {
        return expr.InsertArguments(index, numbersList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression InsertLiteralNumbers(this SteExpression expr, int index, params float[] numbersList)
    {
        return expr.InsertArguments(index, numbersList.Select(SteExpression.CreateLiteralNumber));
    }

    public static SteExpression InsertFunction(this SteExpression expr, int index, string head)
    {
        return expr.InsertArgument(index, SteExpression.CreateFunction(head));
    }

    public static SteExpression InsertFunction(this SteExpression expr, int index, string head, params SteExpression[] args)
    {
        return expr.InsertArgument(index, SteExpression.CreateFunction(head, args));
    }

    public static SteExpression InsertFunction(this SteExpression expr, int index, string head, IEnumerable<SteExpression> args)
    {
        return expr.InsertArgument(index, SteExpression.CreateFunction(head, args));
    }

    public static SteExpression InsertOperator(this SteExpression expr, int index, SteOperatorSpecs head)
    {
        return expr.InsertArgument(index, SteExpression.CreateOperator(head));
    }

    public static SteExpression InsertOperator(this SteExpression expr, int index, SteOperatorSpecs head, params SteExpression[] args)
    {
        return expr.InsertArgument(index, SteExpression.CreateOperator(head, args));
    }

    public static SteExpression InsertOperator(this SteExpression expr, int index, SteOperatorSpecs head, IEnumerable<SteExpression> args)
    {
        return expr.InsertArgument(index, SteExpression.CreateOperator(head, args));
    }
}