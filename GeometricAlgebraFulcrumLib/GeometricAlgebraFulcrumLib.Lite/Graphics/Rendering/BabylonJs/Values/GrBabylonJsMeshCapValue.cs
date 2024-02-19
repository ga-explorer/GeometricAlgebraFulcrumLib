﻿using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsMeshCapValue :
    SparseCodeAttributeValue<GrBabylonJsMeshCap>
{
    public static implicit operator GrBabylonJsMeshCapValue(string valueText)
    {
        return new GrBabylonJsMeshCapValue(valueText);
    }

    public static implicit operator GrBabylonJsMeshCapValue(GrBabylonJsMeshCap value)
    {
        return new GrBabylonJsMeshCapValue(value);
    }


    private GrBabylonJsMeshCapValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsMeshCapValue(GrBabylonJsMeshCap value)
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