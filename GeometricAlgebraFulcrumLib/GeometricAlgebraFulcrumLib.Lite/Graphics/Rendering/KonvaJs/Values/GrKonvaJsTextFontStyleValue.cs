using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsTextFontStyleValue :
    SparseCodeAttributeValue<GrKonvaJsTextFontStyle>
{
    public static implicit operator GrKonvaJsTextFontStyleValue(string valueText)
    {
        return new GrKonvaJsTextFontStyleValue(valueText);
    }

    public static implicit operator GrKonvaJsTextFontStyleValue(GrKonvaJsTextFontStyle value)
    {
        return new GrKonvaJsTextFontStyleValue(value);
    }


    private GrKonvaJsTextFontStyleValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsTextFontStyleValue(GrKonvaJsTextFontStyle value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetKonvaJsCode() 
            : ValueText;
    }
}