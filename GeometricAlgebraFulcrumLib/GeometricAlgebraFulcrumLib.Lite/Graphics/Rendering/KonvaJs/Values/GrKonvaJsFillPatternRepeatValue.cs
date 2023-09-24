﻿using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

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