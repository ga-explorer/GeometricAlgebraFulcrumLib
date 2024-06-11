using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsFillPatternRepeatValue :
    SparseCodeAttributeValue<GrKonvaJsFillPatternRepeat>
{
    public static implicit operator GrKonvaJsFillPatternRepeatValue(string valueText)
    {
        return new GrKonvaJsFillPatternRepeatValue(valueText);
    }

    public static implicit operator GrKonvaJsFillPatternRepeatValue(GrKonvaJsFillPatternRepeat value)
    {
        return new GrKonvaJsFillPatternRepeatValue(value);
    }


    private GrKonvaJsFillPatternRepeatValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsFillPatternRepeatValue(GrKonvaJsFillPatternRepeat value)
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