﻿using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsFillPriorityValue :
    SparseCodeAttributeValue<GrKonvaJsFillPriority>
{
    public static implicit operator GrKonvaJsFillPriorityValue(string valueText)
    {
        return new GrKonvaJsFillPriorityValue(valueText);
    }

    public static implicit operator GrKonvaJsFillPriorityValue(GrKonvaJsFillPriority value)
    {
        return new GrKonvaJsFillPriorityValue(value);
    }


    private GrKonvaJsFillPriorityValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsFillPriorityValue(GrKonvaJsFillPriority value)
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