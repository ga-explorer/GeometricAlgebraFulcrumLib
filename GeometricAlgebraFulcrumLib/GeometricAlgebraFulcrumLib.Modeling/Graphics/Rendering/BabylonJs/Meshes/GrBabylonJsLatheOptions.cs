﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public sealed class GrBabylonJsLatheOptions :
    GrBabylonJsObjectOptions
{
    public GrBabylonJsVector3ArrayValue? Shape
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector3ArrayValue>("shape");
        set => SetAttributeValue("shape", value);
    }

    public GrBabylonJsMeshValue? Instance
    {
        get => GetAttributeValueOrNull<GrBabylonJsMeshValue>("instance");
        set => SetAttributeValue("instance", value);
    }

    public GrBabylonJsInt32Value? Clip
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("clip");
        set => SetAttributeValue("clip", value);
    }

    public GrBabylonJsFloat32Value? Arc
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("arc");
        set => SetAttributeValue("arc", value);
    }

    public GrBabylonJsFloat32Value? Radius
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("radius");
        set => SetAttributeValue("radius", value);
    }

    public GrBabylonJsBooleanValue? Closed
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("closed");
        set => SetAttributeValue("closed", value);
    }

    public GrBabylonJsInt32Value? Tessellation
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("tessellation");
        set => SetAttributeValue("tessellation", value);
    }

    public GrBabylonJsMeshCapValue? Cap
    {
        get => GetAttributeValueOrNull<GrBabylonJsMeshCapValue>("cap");
        set => SetAttributeValue("cap", value);
    }

    public GrBabylonJsMeshOrientationValue? SideOrientation
    {
        get => GetAttributeValueOrNull<GrBabylonJsMeshOrientationValue>("sideOrientation");
        set => SetAttributeValue("sideOrientation", value);
    }

    public GrBabylonJsVector4Value? FrontUVs
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector4Value>("frontUVs");
        set => SetAttributeValue("frontUVs", value);
    }

    public GrBabylonJsVector4Value? BackUVs
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector4Value>("backUVs");
        set => SetAttributeValue("backUVs", value);
    }

    public GrBabylonJsBooleanValue? InvertUV
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("invertUV");
        set => SetAttributeValue("invertUV", value);
    }

    public GrBabylonJsBooleanValue? Updatable
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
        set => SetAttributeValue("updatable", value);
    }


    public GrBabylonJsLatheOptions()
    {
    }

    public GrBabylonJsLatheOptions(GrBabylonJsLatheOptions options)
    {
        SetAttributeValues(options);
    }
}