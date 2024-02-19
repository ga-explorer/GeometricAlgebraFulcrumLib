namespace TextComposerLib.Code.JavaScript;

public static class JsExpressionFactory
{
    //#region Boolean
    //public static JsBooleanVariable VarSetBoolean(this JavaScriptCodeComposer composer, string variableName, string codeText)
    //{
    //    var value = (JsBoolean) codeText;

    //    return VarSet(composer, variableName, value);
    //}

    //public static JsBooleanVariable VarLetBoolean(this JavaScriptCodeComposer composer, string variableName, string codeText)
    //{
    //    var value = (JsBoolean) codeText;

    //    return VarLet(composer, variableName, value);
    //}

    //public static JsBooleanVariable VarConstBoolean(this JavaScriptCodeComposer composer, string variableName, string codeText)
    //{
    //    var value = (JsBoolean) codeText;

    //    return VarConst(composer, variableName, value);
    //}


    //public static JsBooleanVariable VarSetBoolean(this JavaScriptCodeComposer composer, string variableName, bool literalValue)
    //{
    //    var value = (JsBoolean) literalValue;

    //    return VarSet(composer, variableName, value);
    //}

    //public static JsBooleanVariable VarLetBoolean(this JavaScriptCodeComposer composer, string variableName, bool literalValue)
    //{
    //    var value = (JsBoolean) literalValue;

    //    return VarLet(composer, variableName, value);
    //}

    //public static JsBooleanVariable VarConstBoolean(this JavaScriptCodeComposer composer, string variableName, bool literalValue)
    //{
    //    var value = (JsBoolean) literalValue;

    //    return VarConst(composer, variableName, value);
    //}


    //public static JsBooleanVariable VarSet(this JavaScriptCodeComposer composer, string variableName, JsBooleanExpression value)
    //{
    //    composer.AssignToVariable(variableName, value.GetJsCode());

    //    return new JsBooleanVariable(variableName);
    //}

    //public static JsBooleanVariable VarLet(this JavaScriptCodeComposer composer, string variableName, JsBooleanExpression value)
    //{
    //    composer.AssignToVariableLet(variableName, value.GetJsCode());

    //    return new JsBooleanVariable(variableName);
    //}

    //public static JsBooleanVariable VarConst(this JavaScriptCodeComposer composer, string variableName, JsBooleanExpression value)
    //{
    //    composer.AssignToVariableConst(variableName, value.GetJsCode());

    //    return new JsBooleanVariable(variableName, value);
    //}
    //#endregion

    //#region Number
    //public static JsNumberVariable VarSetNumber(this JavaScriptCodeComposer composer, string variableName, string codeText)
    //{
    //    var value = (JsNumberValue) codeText;

    //    return VarSet(composer, variableName, value);
    //}

    //public static JsNumberVariable VarLetNumber(this JavaScriptCodeComposer composer, string variableName, string codeText)
    //{
    //    var value = (JsNumberValue) codeText;

    //    return VarLet(composer, variableName, value);
    //}

    //public static JsNumberVariable VarConstNumber(this JavaScriptCodeComposer composer, string variableName, string codeText)
    //{
    //    var value = (JsNumberValue) codeText;

    //    return VarConst(composer, variableName, value);
    //}


    //public static JsNumberVariable VarSetNumber(this JavaScriptCodeComposer composer, string variableName, double literalValue)
    //{
    //    var value = (JsNumberValue) literalValue;

    //    return VarSet(composer, variableName, value);
    //}

    //public static JsNumberVariable VarLetNumber(this JavaScriptCodeComposer composer, string variableName, double literalValue)
    //{
    //    var value = (JsNumberValue) literalValue;

    //    return VarLet(composer, variableName, value);
    //}

    //public static JsNumberVariable VarConstNumber(this JavaScriptCodeComposer composer, string variableName, double literalValue)
    //{
    //    var value = (JsNumberValue) literalValue;

    //    return VarConst(composer, variableName, value);
    //}


    //public static JsNumberVariable VarSet(this JavaScriptCodeComposer composer, string variableName, JsNumberExpression value)
    //{
    //    composer.AssignToVariable(variableName, value.GetJsCode());

    //    return new JsNumberVariable(variableName);
    //}

    //public static JsNumberVariable VarLet(this JavaScriptCodeComposer composer, string variableName, JsNumberExpression value)
    //{
    //    composer.AssignToVariableLet(variableName, value.GetJsCode());

    //    return new JsNumberVariable(variableName);
    //}
        
    //public static JsNumberVariable VarConst(this JavaScriptCodeComposer composer, string variableName, JsNumberExpression value)
    //{
    //    composer.AssignToVariableConst(variableName, value.GetJsCode());

    //    return new JsNumberVariable(variableName, value);
    //}

        
    //public static JsNumberVariable DefineNumberMember(this IJsObjectVariable variable, [NotNull] string memberName)
    //{
    //    return new JsNumberVariable($"{variable.VariableName}.{memberName}");
    //}
    //#endregion

    //#region String
    //public static JsStringVariable VarSetString(this JavaScriptCodeComposer composer, string variableName, string codeText)
    //{
    //    var value = (JsString) codeText;

    //    return VarSet(composer, variableName, value);
    //}

    //public static JsStringVariable VarLetString(this JavaScriptCodeComposer composer, string variableName, string codeText)
    //{
    //    var value = (JsString) codeText;

    //    return VarLet(composer, variableName, value);
    //}

    //public static JsStringVariable VarConstString(this JavaScriptCodeComposer composer, string variableName, string codeText)
    //{
    //    var value = (JsString) codeText;

    //    return VarConst(composer, variableName, value);
    //}


    //public static JsStringVariable VarSetStringLiteral(this JavaScriptCodeComposer composer, string variableName, string literalValue)
    //{
    //    var value = (JsString) literalValue;

    //    return VarSet(composer, variableName, value);
    //}

    //public static JsStringVariable VarLetStringLiteral(this JavaScriptCodeComposer composer, string variableName, string literalValue)
    //{
    //    var value = (JsString) literalValue;

    //    return VarLet(composer, variableName, value);
    //}

    //public static JsStringVariable VarConstStringLiteral(this JavaScriptCodeComposer composer, string variableName, string literalValue)
    //{
    //    var value = (JsString) literalValue;

    //    return VarConst(composer, variableName, value);
    //}


    //public static JsStringVariable VarSet(this JavaScriptCodeComposer composer, string variableName, JsStringExpression value)
    //{
    //    composer.AssignToVariable(variableName, value.GetJsCode());

    //    return new JsStringVariable(variableName);
    //}

    //public static JsStringVariable VarLet(this JavaScriptCodeComposer composer, string variableName, JsStringExpression value)
    //{
    //    composer.AssignToVariableLet(variableName, value.GetJsCode());

    //    return new JsStringVariable(variableName);
    //}
        
    //public static JsStringVariable VarConst(this JavaScriptCodeComposer composer, string variableName, JsStringExpression value)
    //{
    //    composer.AssignToVariableConst(variableName, value.GetJsCode());

    //    return new JsStringVariable(variableName, value);
    //}
    //#endregion
}