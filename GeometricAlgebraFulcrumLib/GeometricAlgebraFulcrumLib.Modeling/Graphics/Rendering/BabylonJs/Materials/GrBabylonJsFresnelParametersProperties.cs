using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

public class GrBabylonJsFresnelParametersProperties :
    GrBabylonJsObjectProperties
{
    public GrBabylonJsFloat32Value? Bias
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("bias");
        set => SetAttributeValue("bias", value);
    }

    public GrBabylonJsBooleanValue? IsEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isEnabled");
        set => SetAttributeValue("isEnabled", value);
    }

    public GrBabylonJsColor3Value? LeftColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("leftColor");
        set => SetAttributeValue("leftColor", value);
    }

    public GrBabylonJsColor3Value? RightColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("rightColor");
        set => SetAttributeValue("rightColor", value);
    }

    public GrBabylonJsFloat32Value? Power
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("power");
        set => SetAttributeValue("power", value);
    }
            

    public GrBabylonJsFresnelParametersProperties()
    {
    }

    public GrBabylonJsFresnelParametersProperties(GrBabylonJsFresnelParametersProperties options)
    {
        SetAttributeValues(options);
    }
}