using DataStructuresLib.AttributeSet;
using GraphicsComposerLib.Rendering.KonvaJs.Constants;

namespace GraphicsComposerLib.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsTextWrapValue :
    SparseCodeAttributeValue<GrKonvaJsTextWrap>
{
    public static implicit operator GrKonvaJsTextWrapValue(string valueText)
    {
        return new GrKonvaJsTextWrapValue(valueText);
    }

    public static implicit operator GrKonvaJsTextWrapValue(GrKonvaJsTextWrap value)
    {
        return new GrKonvaJsTextWrapValue(value);
    }


    private GrKonvaJsTextWrapValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsTextWrapValue(GrKonvaJsTextWrap value)
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