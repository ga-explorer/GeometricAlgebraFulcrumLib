﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public sealed class GrBabylonJsDashedLinesOptions :
    GrBabylonJsObjectOptions
{
    public GrBabylonJsVector3ArrayValue? Points
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector3ArrayValue>("points");
        set => SetAttributeValue("points", value);
    }

    public GrBabylonJsMaterialValue? Material
    {
        get => GetAttributeValueOrNull<GrBabylonJsMaterialValue>("material");
        set => SetAttributeValue("material", value);
    }

    public GrBabylonJsInt32Value? DashSize
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("dashSize");
        set => SetAttributeValue("dashSize", value);
    }

    public GrBabylonJsInt32Value? GapSize
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("gapSize");
        set => SetAttributeValue("gapSize", value);
    }

    public GrBabylonJsInt32Value? DashNumber
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("dashNb");
        set => SetAttributeValue("dashNb", value);
    }

    public GrBabylonJsBooleanValue? UseVertexAlpha
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useVertexAlpha");
        set => SetAttributeValue("useVertexAlpha", value);
    }

    public GrBabylonJsLinesMeshValue? Instance
    {
        get => GetAttributeValueOrNull<GrBabylonJsLinesMeshValue>("instance");
        set => SetAttributeValue("instance", value);
    }

    public GrBabylonJsBooleanValue? Updatable
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
        set => SetAttributeValue("updatable", value);
    }


    public GrBabylonJsDashedLinesOptions()
    {
    }

    public GrBabylonJsDashedLinesOptions(GrBabylonJsDashedLinesOptions options)
    {
        SetAttributeValues(options);
    }
}