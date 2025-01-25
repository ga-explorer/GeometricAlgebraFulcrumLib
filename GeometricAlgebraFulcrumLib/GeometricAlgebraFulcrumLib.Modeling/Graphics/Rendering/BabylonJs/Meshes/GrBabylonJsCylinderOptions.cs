using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public sealed class GrBabylonJsCylinderOptions :
    GrBabylonJsObjectOptions
{
    public GrBabylonJsFloat32Value? Arc
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("arc");
        set => SetAttributeValue("arc", value);
    }

    public GrBabylonJsFloat32Value? Diameter
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("diameter");
        set => SetAttributeValue("diameter", value);
    }

    public GrBabylonJsFloat32Value? DiameterBottom
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("diameterBottom");
        set => SetAttributeValue("diameterBottom", value);
    }

    public GrBabylonJsFloat32Value? DiameterTop
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("diameterTop");
        set => SetAttributeValue("diameterTop", value);
    }

    public GrBabylonJsFloat32Value? Height
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("height");
        set => SetAttributeValue("height", value);
    }

    public GrBabylonJsInt32Value? Subdivisions
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("subdivisions");
        set => SetAttributeValue("subdivisions", value);
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

    public GrBabylonJsColor4ArrayValue? FaceColors
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4ArrayValue>("faceColors");
        set => SetAttributeValue("faceColors", value);
    }

    public GrBabylonJsVector4ArrayValue? FaceUVs
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector4ArrayValue>("faceUVs");
        set => SetAttributeValue("faceUVs", value);
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

    public GrBabylonJsBooleanValue? Enclose
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("enclose");
        set => SetAttributeValue("enclose", value);
    }

    public GrBabylonJsBooleanValue? HasRings
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("hasRings");
        set => SetAttributeValue("hasRings", value);
    }

    public GrBabylonJsBooleanValue? Updatable
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
        set => SetAttributeValue("updatable", value);
    }

    public GrBabylonJsCylinderOptions()
    {
    }

    public GrBabylonJsCylinderOptions(GrBabylonJsCylinderOptions options)
    {
        SetAttributeValues(options);
    }
}