using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

public sealed class GrBabylonJsGradientMaterialProperties :
    GrBabylonJsMaterialProperties
{
    public GrBabylonJsColor3Value? BottomColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("bottomColor");
        set => SetAttributeValue("bottomColor", value);
    }

    public GrBabylonJsColor3Value? TopColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("topColor");
        set => SetAttributeValue("topColor", value);
    }

    public GrBabylonJsFloat32Value? BottomColorAlpha
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("bottomColorAlpha");
        set => SetAttributeValue("bottomColorAlpha", value);
    }

    public GrBabylonJsFloat32Value? TopColorAlpha
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("topColorAlpha");
        set => SetAttributeValue("topColorAlpha", value);
    }

    public GrBabylonJsFloat32Value? Offset
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("offset");
        set => SetAttributeValue("offset", value);
    }

    public GrBabylonJsFloat32Value? Scale
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("scale");
        set => SetAttributeValue("scale", value);
    }

    public GrBabylonJsFloat32Value? Smoothness
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("smoothness");
        set => SetAttributeValue("smoothness", value);
    }

    public GrBabylonJsInt32Value? MaxSimultaneousLights
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("maxSimultaneousLights");
        set => SetAttributeValue("maxSimultaneousLights", value);
    }

    public GrBabylonJsBooleanValue? DisableLighting
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("disableLighting");
        set => SetAttributeValue("disableLighting", value);
    }


    public GrBabylonJsGradientMaterialProperties()
    {
    }

    public GrBabylonJsGradientMaterialProperties(GrBabylonJsGradientMaterialProperties properties)
    {
        SetAttributeValues(properties);
    }
}