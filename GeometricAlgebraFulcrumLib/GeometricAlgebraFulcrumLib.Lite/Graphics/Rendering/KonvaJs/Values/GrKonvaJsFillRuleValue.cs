using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsFillRuleValue :
    SparseCodeAttributeValue<GrKonvaJsFillRule>
{
    public static implicit operator GrKonvaJsFillRuleValue(string valueText)
    {
        return new GrKonvaJsFillRuleValue(valueText);
    }

    public static implicit operator GrKonvaJsFillRuleValue(GrKonvaJsFillRule value)
    {
        return new GrKonvaJsFillRuleValue(value);
    }


    private GrKonvaJsFillRuleValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsFillRuleValue(GrKonvaJsFillRule value)
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