﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsLineCapValue :
    SparseCodeAttributeValue<GrKonvaJsLineCap>
{
    public static implicit operator GrKonvaJsLineCapValue(string valueText)
    {
        return new GrKonvaJsLineCapValue(valueText);
    }

    public static implicit operator GrKonvaJsLineCapValue(GrKonvaJsLineCap value)
    {
        return new GrKonvaJsLineCapValue(value);
    }


    private GrKonvaJsLineCapValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsLineCapValue(GrKonvaJsLineCap value)
        : base(value)
    {
    }


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetKonvaJsCode() 
            : ValueText;
    }
}