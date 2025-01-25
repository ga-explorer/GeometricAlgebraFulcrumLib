using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

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


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetKonvaJsCode() 
            : ValueText;
    }
}