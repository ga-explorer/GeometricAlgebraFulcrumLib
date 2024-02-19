﻿using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsAlphaModeValue :
    SparseCodeAttributeValue<GrBabylonJsAlphaMode>
{
    public static implicit operator GrBabylonJsAlphaModeValue(string valueText)
    {
        return new GrBabylonJsAlphaModeValue(valueText);
    }

    public static implicit operator GrBabylonJsAlphaModeValue(GrBabylonJsAlphaMode value)
    {
        return new GrBabylonJsAlphaModeValue(value);
    }


    private GrBabylonJsAlphaModeValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsAlphaModeValue(GrBabylonJsAlphaMode value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode() 
            : ValueText;
    }
}