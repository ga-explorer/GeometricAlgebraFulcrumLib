using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

public sealed class GrBabylonJsShadowOnlyMaterialProperties :
    GrBabylonJsMaterialProperties
{
    public GrBabylonJsColor3Value? ShadowColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("shadowColor");
        set => SetAttributeValue("shadowColor", value);
    }
            
    public GrBabylonJsCodeValue? ActiveLight
    {
        get => GetAttributeValueOrNull<GrBabylonJsCodeValue>("activeLight");
        set => SetAttributeValue("activeLight", value);
    }


    public GrBabylonJsShadowOnlyMaterialProperties()
    {
    }

    public GrBabylonJsShadowOnlyMaterialProperties(GrBabylonJsShadowOnlyMaterialProperties properties)
    {
        SetAttributeValues(properties);
    }
}