using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Layers;

public sealed class GrBabylonJsLayerProperties :
    GrBabylonJsObjectProperties
{
    public GrBabylonJsAlphaBlendingModeValue? AlphaBlendingMode
    {
        get => GetAttributeValueOrNull<GrBabylonJsAlphaBlendingModeValue>("alphaBlendingMode");
        set => SetAttributeValue("alphaBlendingMode", value);
    }

    public GrBabylonJsBooleanValue? AlphaTest
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("alphaTest");
        set => SetAttributeValue("alphaTest", value);
    }

    public GrBabylonJsBooleanValue? RenderOnlyInRenderTargetTextures
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("renderOnlyInRenderTargetTextures");
        set => SetAttributeValue("renderOnlyInRenderTargetTextures", value);
    }

    public GrBabylonJsColor4Value? Color
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("color");
        set => SetAttributeValue("color", value);
    }

    public GrBabylonJsBooleanValue? IsBackground
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isBackground");
        set => SetAttributeValue("isBackground", value);
    }

    public GrBabylonJsBooleanValue? IsEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isEnabled");
        set => SetAttributeValue("isEnabled", value);
    }

    public GrBabylonJsInt32Value? LayerMask
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("layerMask");
        set => SetAttributeValue("layerMask", value);
    }

    public GrBabylonJsStringValue? Name
    {
        get => GetAttributeValueOrNull<GrBabylonJsStringValue>("name");
        set => SetAttributeValue("name", value);
    }

    public GrBabylonJsVector2Value? Offset
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector2Value>("offset");
        set => SetAttributeValue("offset", value);
    }

    public GrBabylonJsVector2Value? Scale
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector2Value>("scale");
        set => SetAttributeValue("scale", value);
    }

    public GrBabylonJsTextureArrayValue? RenderTargetTextures
    {
        get => GetAttributeValueOrNull<GrBabylonJsTextureArrayValue>("renderTargetTextures");
        set => SetAttributeValue("renderTargetTextures", value);
    }

    public GrBabylonJsTextureValue? Texture
    {
        get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("texture");
        set => SetAttributeValue("texture", value);
    }


    public GrBabylonJsLayerProperties()
    {
    }

    public GrBabylonJsLayerProperties(GrBabylonJsLayerProperties properties)
    {
        SetAttributeValues(properties);
    }
}