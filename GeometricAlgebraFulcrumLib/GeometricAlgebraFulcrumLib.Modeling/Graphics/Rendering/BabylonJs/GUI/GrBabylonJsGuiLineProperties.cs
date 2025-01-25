using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

public sealed class GrBabylonJsGuiLineProperties :
    GrBabylonJsGuiControlProperties
{
    public GrBabylonJsControlValue? ConnectedControl
    {
        get => GetAttributeValueOrNull<GrBabylonJsControlValue>("connectedControl");
        set => SetAttributeValue("connectedControl", value);
    }

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

    public GrBabylonJsFloat32Value? X1
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("x1");
        set => SetAttributeValue("x1", value);
    }

    public GrBabylonJsFloat32Value? Y1
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("y1");
        set => SetAttributeValue("y1", value);
    }

    public GrBabylonJsFloat32Value? X2
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("x2");
        set => SetAttributeValue("x2", value);
    }

    public GrBabylonJsFloat32Value? Y2
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("y2");
        set => SetAttributeValue("y2", value);
    }


    public GrBabylonJsGuiLineProperties()
    {
    }

    public GrBabylonJsGuiLineProperties(GrBabylonJsGuiLineProperties properties)
    {
        SetAttributeValues(properties);
    }
}