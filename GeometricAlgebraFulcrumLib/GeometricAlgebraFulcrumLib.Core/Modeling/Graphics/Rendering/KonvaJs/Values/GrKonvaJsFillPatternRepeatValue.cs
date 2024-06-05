using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Constants;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

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