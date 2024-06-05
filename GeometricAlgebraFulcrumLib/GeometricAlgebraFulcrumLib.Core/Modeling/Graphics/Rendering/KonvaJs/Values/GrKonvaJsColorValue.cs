using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsColorValue :
    SparseCodeAttributeValue<Color>
{
    public static implicit operator GrKonvaJsColorValue(string valueText)
    {
        return new GrKonvaJsColorValue(valueText);
    }

    public static implicit operator GrKonvaJsColorValue(Color value)
    {
        return new GrKonvaJsColorValue(value);
    }


    private GrKonvaJsColorValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsColorValue(Color value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetKonvaJsCode(true) 
            : ValueText;
    }
}