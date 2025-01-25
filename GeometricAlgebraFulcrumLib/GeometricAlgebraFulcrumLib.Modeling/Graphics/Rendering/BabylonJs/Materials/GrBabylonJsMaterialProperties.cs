using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

public abstract class GrBabylonJsMaterialProperties :
    GrBabylonJsObjectProperties
{
    public GrBabylonJsMaterialTransparencyModeValue? TransparencyMode
    {
        get => GetAttributeValueOrNull<GrBabylonJsMaterialTransparencyModeValue>("transparencyMode");
        set => SetAttributeValue("transparencyMode", value);
    }

    public GrBabylonJsFloat32Value? Alpha
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("alpha");
        set => SetAttributeValue("alpha", value);
    }

    public GrBabylonJsAlphaModeValue? AlphaMode
    {
        get => GetAttributeValueOrNull<GrBabylonJsAlphaModeValue>("alphaMode");
        set => SetAttributeValue("alphaMode", value);
    }

    public GrBabylonJsBooleanValue? WireFrame
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("wireFrame");
        set => SetAttributeValue("wireFrame", value);
    }

    public GrBabylonJsMeshOrientationValue? SideOrientation
    {
        get => GetAttributeValueOrNull<GrBabylonJsMeshOrientationValue>("sideOrientation");
        set => SetAttributeValue("sideOrientation", value);
    }

    public GrBabylonJsBooleanValue? BackFaceCulling
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("backFaceCulling");
        set => SetAttributeValue("backFaceCulling", value);
    }

    public GrBabylonJsBooleanValue? CullBackFaces
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("cullBackFaces");
        set => SetAttributeValue("cullBackFaces", value);
    }

    public GrBabylonJsBooleanValue? FogEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("fogEnabled");
        set => SetAttributeValue("fogEnabled", value);
    }

    public GrBabylonJsFloat32Value? PointSize
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("pointSize");
        set => SetAttributeValue("pointSize", value);
    }

    public GrBabylonJsBooleanValue? PointsCloud
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("pointsCloud");
        set => SetAttributeValue("pointsCloud", value);
    }



    protected GrBabylonJsMaterialProperties()
    {
    }

    protected GrBabylonJsMaterialProperties(GrBabylonJsMaterialProperties properties)
    {
        SetAttributeValues(properties);
    }
}