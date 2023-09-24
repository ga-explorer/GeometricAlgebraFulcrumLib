﻿using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsTextFontVariantValue :
    SparseCodeAttributeValue<GrKonvaJsTextFontVariant>
{
    public static implicit operator GrKonvaJsTextFontVariantValue(string valueText)
    {
        return new GrKonvaJsTextFontVariantValue(valueText);
    }

    public static implicit operator GrKonvaJsTextFontVariantValue(GrKonvaJsTextFontVariant value)
    {
        return new GrKonvaJsTextFontVariantValue(value);
    }


    private GrKonvaJsTextFontVariantValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsTextFontVariantValue(GrKonvaJsTextFontVariant value)
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