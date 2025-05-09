﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsCameraModeValue :
    SparseCodeAttributeValue<GrBabylonJsCameraMode>
{
    public static implicit operator GrBabylonJsCameraModeValue(string valueText)
    {
        return new GrBabylonJsCameraModeValue(valueText);
    }

    public static implicit operator GrBabylonJsCameraModeValue(GrBabylonJsCameraMode value)
    {
        return new GrBabylonJsCameraModeValue(value);
    }


    private GrBabylonJsCameraModeValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsCameraModeValue(GrBabylonJsCameraMode value)
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