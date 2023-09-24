﻿using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsLineJoinValue :
    SparseCodeAttributeValue<GrKonvaJsLineJoin>
{
    public static implicit operator GrKonvaJsLineJoinValue(string valueText)
    {
        return new GrKonvaJsLineJoinValue(valueText);
    }

    public static implicit operator GrKonvaJsLineJoinValue(GrKonvaJsLineJoin value)
    {
        return new GrKonvaJsLineJoinValue(value);
    }


    private GrKonvaJsLineJoinValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsLineJoinValue(GrKonvaJsLineJoin value)
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