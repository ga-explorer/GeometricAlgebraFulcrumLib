﻿using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Meshes;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsMeshValue :
    SparseCodeAttributeValue<GrBabylonJsMesh>
{
    public static implicit operator GrBabylonJsMeshValue(string valueText)
    {
        return new GrBabylonJsMeshValue(valueText);
    }

    public static implicit operator GrBabylonJsMeshValue(GrBabylonJsMesh value)
    {
        return new GrBabylonJsMeshValue(value);
    }


    private GrBabylonJsMeshValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsMeshValue(GrBabylonJsMesh value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.ToString() 
            : ValueText;
    }
}