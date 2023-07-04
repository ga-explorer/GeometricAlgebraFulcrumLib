using DataStructuresLib.AttributeSet;
using GraphicsComposerLib.Rendering.KonvaJs.Constants;

namespace GraphicsComposerLib.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsTextBaselineValue :
    SparseCodeAttributeValue<GrKonvaJsTextBaseline>
{
    public static implicit operator GrKonvaJsTextBaselineValue(string valueText)
    {
        return new GrKonvaJsTextBaselineValue(valueText);
    }

    public static implicit operator GrKonvaJsTextBaselineValue(GrKonvaJsTextBaseline value)
    {
        return new GrKonvaJsTextBaselineValue(value);
    }


    private GrKonvaJsTextBaselineValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsTextBaselineValue(GrKonvaJsTextBaseline value)
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