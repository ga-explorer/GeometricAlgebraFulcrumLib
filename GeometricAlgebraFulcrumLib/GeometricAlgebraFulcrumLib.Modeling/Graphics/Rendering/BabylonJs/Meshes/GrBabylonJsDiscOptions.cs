using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public sealed class GrBabylonJsDiscOptions :
    GrBabylonJsObjectOptions
{
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

    public GrBabylonJsInt32Value? Tessellation
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("tessellation");
        set => SetAttributeValue("tessellation", value);
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


    public GrBabylonJsDiscOptions()
    {
    }

    public GrBabylonJsDiscOptions(GrBabylonJsDiscOptions options)
    {
        SetAttributeValues(options);
    }
}