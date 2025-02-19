﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public sealed class GrBabylonJsBoxOptions :
    GrBabylonJsObjectOptions
{
    public GrBabylonJsFloat32Value? BottomBaseAt
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("bottomBaseAt");
        set => SetAttributeValue("bottomBaseAt", value);
    }

    public GrBabylonJsFloat32Value? TopBaseAt
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("topBaseAt");
        set => SetAttributeValue("topBaseAt", value);
    }

    public GrBabylonJsFloat32Value? Width
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("width");
        set => SetAttributeValue("width", value);
    }

    public GrBabylonJsFloat32Value? Height
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("height");
        set => SetAttributeValue("height", value);
    }

    public GrBabylonJsFloat32Value? Depth
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("depth");
        set => SetAttributeValue("depth", value);
    }

    public GrBabylonJsFloat32Value? Size
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("size");
        set => SetAttributeValue("size", value);
    }

    public GrBabylonJsBooleanValue? Wrap
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("wrap");
        set => SetAttributeValue("wrap", value);
    }

    public GrBabylonJsColor4ArrayValue? FaceColors
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4ArrayValue>("faceColors");
        set => SetAttributeValue("faceColors", value);
    }

    public GrBabylonJsVector4ArrayValue? FaceUv
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector4ArrayValue>("faceUV");
        set => SetAttributeValue("faceUV", value);
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

    public GrBabylonJsBooleanValue? Updatable
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
        set => SetAttributeValue("updatable", value);
    }


    public GrBabylonJsBoxOptions()
    {
    }
            
    public GrBabylonJsBoxOptions(GrBabylonJsBoxOptions options)
    {
        SetAttributeValues(options);
    }
}