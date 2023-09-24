using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsTextVerticalAlignValue :
    SparseCodeAttributeValue<GrKonvaJsTextVerticalAlign>
{
    public static implicit operator GrKonvaJsTextVerticalAlignValue(string valueText)
    {
        return new GrKonvaJsTextVerticalAlignValue(valueText);
    }

    public static implicit operator GrKonvaJsTextVerticalAlignValue(GrKonvaJsTextVerticalAlign value)
    {
        return new GrKonvaJsTextVerticalAlignValue(value);
    }


    private GrKonvaJsTextVerticalAlignValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsTextVerticalAlignValue(GrKonvaJsTextVerticalAlign value)
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