using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public sealed class GrBabylonJsExtrudeShapeOptions :
    GrBabylonJsObjectOptions
{
    public GrBabylonJsVector3ArrayValue? Path
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector3ArrayValue>("path");
        set => SetAttributeValue("path", value);
    }

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

    public GrBabylonJsFloat32Value? Rotation
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("rotation");
        set => SetAttributeValue("rotation", value);
    }

    public GrBabylonJsFloat32Value? Scale
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("scale");
        set => SetAttributeValue("scale", value);
    }

    public GrBabylonJsBooleanValue? AdjustFrame
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("adjustFrame");
        set => SetAttributeValue("adjustFrame", value);
    }

    public GrBabylonJsVector3Value? FirstNormal
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("firstNormal");
        set => SetAttributeValue("firstNormal", value);
    }

    public GrBabylonJsBooleanValue? CloseShape
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("closeShape");
        set => SetAttributeValue("closeShape", value);
    }

    public GrBabylonJsBooleanValue? ClosePath
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("closePath");
        set => SetAttributeValue("closePath", value);
    }

    public GrBabylonJsMeshOrientationValue? SideOrientation
    {
        get => GetAttributeValueOrNull<GrBabylonJsMeshOrientationValue>("sideOrientation");
        set => SetAttributeValue("sideOrientation", value);
    }

    public GrBabylonJsMeshCapValue? Cap
    {
        get => GetAttributeValueOrNull<GrBabylonJsMeshCapValue>("cap");
        set => SetAttributeValue("cap", value);
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

    public GrBabylonJsBooleanValue? InvertUv
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("invertUV");
        set => SetAttributeValue("invertUv", value);
    }

    public GrBabylonJsBooleanValue? Updatable
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
        set => SetAttributeValue("updatable", value);
    }


    public GrBabylonJsExtrudeShapeOptions()
    {
    }

    public GrBabylonJsExtrudeShapeOptions(GrBabylonJsExtrudeShapeOptions options)
    {
        SetAttributeValues(options);
    }
}