﻿using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsBooleanValue :
    SparseCodeAttributeValue<bool>
{
    public static implicit operator GrBabylonJsBooleanValue(string valueText)
    {
        return new GrBabylonJsBooleanValue(valueText);
    }

    public static implicit operator GrBabylonJsBooleanValue(bool value)
    {
        return new GrBabylonJsBooleanValue(value);
    }


    private GrBabylonJsBooleanValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsBooleanValue(bool value)
        : base(value)
    {
    }


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode() 
            : ValueText;
    }
}