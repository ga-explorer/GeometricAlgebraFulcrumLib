using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

public sealed class GrBabylonJsGuiMultiLineProperties :
    GrBabylonJsGuiControlProperties
{
    public GrBabylonJsInt32ArrayValue? Dash
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32ArrayValue>("dash");
        set => SetAttributeValue("dash", value);
    }

    public GrBabylonJsFloat32Value? LineWidth
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("lineWidth");
        set => SetAttributeValue("lineWidth", value);
    }


    public GrBabylonJsGuiMultiLineProperties()
    {
    }

    public GrBabylonJsGuiMultiLineProperties(GrBabylonJsGuiMultiLineProperties properties)
    {
        SetAttributeValues(properties);
    }
}