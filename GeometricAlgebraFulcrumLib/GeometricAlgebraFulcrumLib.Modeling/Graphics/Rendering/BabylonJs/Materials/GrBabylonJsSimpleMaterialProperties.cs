using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

public sealed class GrBabylonJsSimpleMaterialProperties :
    GrBabylonJsMaterialProperties
{
    public GrBabylonJsColor3Value? DiffuseColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("diffuseColor");
        set => SetAttributeValue("diffuseColor", value);
    }
            
    public GrBabylonJsTextureValue? DiffuseTexture
    {
        get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("diffuseTexture");
        set => SetAttributeValue("diffuseTexture", value);
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


    public GrBabylonJsSimpleMaterialProperties()
    {
    }

    public GrBabylonJsSimpleMaterialProperties(GrBabylonJsSimpleMaterialProperties properties)
    {
        SetAttributeValues(properties);
    }
}