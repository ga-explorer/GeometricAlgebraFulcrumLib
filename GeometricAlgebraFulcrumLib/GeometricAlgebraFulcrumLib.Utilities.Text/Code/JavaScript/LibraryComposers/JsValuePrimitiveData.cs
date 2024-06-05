using System.Diagnostics.CodeAnalysis;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript.LibraryComposers;

public class JsValuePrimitiveData :
    JsValueData
{
    public JsPrimitiveType PrimitiveExpression { get; }


    internal JsValuePrimitiveData([NotNull] JsPrimitiveType primitiveExpression, JsClassNameData valueType)
        : base(valueType)
    {
        PrimitiveExpression = primitiveExpression;
    }


    public override string GetJsCode()
    {
        return PrimitiveExpression.GetJsCode();
    }

    public override string GetCsCode()
    {
        if (PrimitiveExpression is null)
            return "new JsObject()";

        var codeText = PrimitiveExpression.GetCsCode();

        return PrimitiveExpression switch
        {
            JsNumber => $"({codeText}).AsJsNumber()",
            JsBoolean => $"({codeText}).AsJsBoolean()",
            JsString => $"({codeText}).AsJsString()",
            _ => codeText
        };
    }
}