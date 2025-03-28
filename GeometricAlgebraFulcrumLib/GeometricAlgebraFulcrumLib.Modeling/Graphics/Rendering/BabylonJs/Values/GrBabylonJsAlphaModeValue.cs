﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

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


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode() 
            : ValueText;
    }
}