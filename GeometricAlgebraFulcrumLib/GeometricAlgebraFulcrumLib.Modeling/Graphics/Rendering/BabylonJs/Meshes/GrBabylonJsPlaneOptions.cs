using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreatePlane-2
/// </summary>
public sealed class GrBabylonJsPlaneOptions :
    GrBabylonJsObjectOptions
{
    //sourcePlane?: Plane; 
    public GrBabylonJsFloat32Value? Size
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("size");
        set => SetAttributeValue("size", value);
    }

    public GrBabylonJsFloat32Value? Height
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("height");
        set => SetAttributeValue("height", value);
    }

    public GrBabylonJsFloat32Value? Width
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("width");
        set => SetAttributeValue("width", value);
    }

    public GrBabylonJsCodeValue? SourcePlane
    {
        get => GetAttributeValueOrNull<GrBabylonJsCodeValue>("sourcePlane");
        set => SetAttributeValue("sourcePlane", value);
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


    public GrBabylonJsPlaneOptions()
    {
    }

    public GrBabylonJsPlaneOptions(GrBabylonJsPlaneOptions options)
    {
        SetAttributeValues(options);
    }
}