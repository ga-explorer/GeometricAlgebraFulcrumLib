using DataStructuresLib.AttributeSet;
using GraphicsComposerLib.Rendering.KonvaJs.Constants;

namespace GraphicsComposerLib.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsTransformsEnabledValue :
    SparseCodeAttributeValue<GrKonvaJsTransformsEnabled>
{
    public static implicit operator GrKonvaJsTransformsEnabledValue(string valueText)
    {
        return new GrKonvaJsTransformsEnabledValue(valueText);
    }

    public static implicit operator GrKonvaJsTransformsEnabledValue(GrKonvaJsTransformsEnabled value)
    {
        return new GrKonvaJsTransformsEnabledValue(value);
    }


    private GrKonvaJsTransformsEnabledValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsTransformsEnabledValue(GrKonvaJsTransformsEnabled value)
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