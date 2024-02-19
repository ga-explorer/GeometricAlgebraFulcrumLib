﻿using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsCameraFovModeValue :
    SparseCodeAttributeValue<GrBabylonJsCameraFovMode>
{
    public static implicit operator GrBabylonJsCameraFovModeValue(string valueText)
    {
        return new GrBabylonJsCameraFovModeValue(valueText);
    }

    public static implicit operator GrBabylonJsCameraFovModeValue(GrBabylonJsCameraFovMode value)
    {
        return new GrBabylonJsCameraFovModeValue(value);
    }


    private GrBabylonJsCameraFovModeValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsCameraFovModeValue(GrBabylonJsCameraFovMode value)
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