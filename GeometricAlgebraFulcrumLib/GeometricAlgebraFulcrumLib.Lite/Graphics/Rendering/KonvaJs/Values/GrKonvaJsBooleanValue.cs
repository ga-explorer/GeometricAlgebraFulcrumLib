using DataStructuresLib.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsBooleanValue :
    SparseCodeAttributeValue<bool>
{
    public static implicit operator GrKonvaJsBooleanValue(string valueText)
    {
        return new GrKonvaJsBooleanValue(valueText);
    }

    public static implicit operator GrKonvaJsBooleanValue(bool value)
    {
        return new GrKonvaJsBooleanValue(value);
    }


    private GrKonvaJsBooleanValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsBooleanValue(bool value)
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