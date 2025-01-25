using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public sealed class GrBabylonJsTorusOptions :
    GrBabylonJsObjectOptions
{
    public GrBabylonJsFloat32Value? Diameter
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("diameter");
        set => SetAttributeValue("diameter", value);
    }

    public GrBabylonJsFloat32Value? Thickness
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("thickness");
        set => SetAttributeValue("thickness", value);
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


    public GrBabylonJsTorusOptions()
    {
    }

    public GrBabylonJsTorusOptions(GrBabylonJsTorusOptions options)
    {
        SetAttributeValues(options);
    }
}