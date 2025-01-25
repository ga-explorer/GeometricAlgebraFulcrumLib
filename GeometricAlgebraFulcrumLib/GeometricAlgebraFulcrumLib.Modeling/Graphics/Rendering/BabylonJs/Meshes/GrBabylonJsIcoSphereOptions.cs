using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public sealed class GrBabylonJsIcoSphereOptions :
    GrBabylonJsObjectOptions
{
    public GrBabylonJsBooleanValue? Flat
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("flat");
        set => SetAttributeValue("flat", value);
    }

    public GrBabylonJsFloat32Value? Radius
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("radius");
        set => SetAttributeValue("radius", value);
    }

    public GrBabylonJsFloat32Value? RadiusX
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("radiusX");
        set => SetAttributeValue("radiusX", value);
    }

    public GrBabylonJsFloat32Value? RadiusY
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("radiusY");
        set => SetAttributeValue("radiusY", value);
    }

    public GrBabylonJsFloat32Value? RadiusZ
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("radiusZ");
        set => SetAttributeValue("radiusZ", value);
    }

    public GrBabylonJsInt32Value? Subdivisions
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("subdivisions");
        set => SetAttributeValue("subdivisions", value);
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


    public GrBabylonJsIcoSphereOptions()
    {
    }

    public GrBabylonJsIcoSphereOptions(GrBabylonJsIcoSphereOptions options)
    {
        SetAttributeValues(options);
    }
}