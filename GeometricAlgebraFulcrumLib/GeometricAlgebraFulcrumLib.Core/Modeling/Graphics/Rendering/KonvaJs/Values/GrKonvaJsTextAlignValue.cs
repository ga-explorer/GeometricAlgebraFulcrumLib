using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Constants;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsTextAlignValue :
    SparseCodeAttributeValue<GrKonvaJsTextAlign>
{
    public static implicit operator GrKonvaJsTextAlignValue(string valueText)
    {
        return new GrKonvaJsTextAlignValue(valueText);
    }

    public static implicit operator GrKonvaJsTextAlignValue(GrKonvaJsTextAlign value)
    {
        return new GrKonvaJsTextAlignValue(value);
    }


    private GrKonvaJsTextAlignValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsTextAlignValue(GrKonvaJsTextAlign value)
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