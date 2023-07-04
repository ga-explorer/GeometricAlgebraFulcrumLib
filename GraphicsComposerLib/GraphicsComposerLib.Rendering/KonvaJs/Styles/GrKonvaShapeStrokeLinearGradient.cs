using GraphicsComposerLib.Rendering.KonvaJs.Values;

namespace GraphicsComposerLib.Rendering.KonvaJs.Styles;

public class GrKonvaShapeStrokeLinearGradient :
    GrKonvaShapeStroke
{
    public GrKonvaJsColorLinearGradientListValue? ColorStops
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsColorLinearGradientListValue>("StrokeLinearGradientColorStops");
        set => ParentStyle.SetAttributeValue("StrokeLinearGradientColorStops", value);
    }
        
    public GrKonvaJsVector2Value? StartPoint
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsVector2Value>("StrokeLinearGradientStartPoint");
        set => ParentStyle.SetAttributeValue("StrokeLinearGradientStartPoint", value);
    }
        
    public GrKonvaJsFloat32Value? StartPointX
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("StrokeLinearGradientStartPointX");
        set => ParentStyle.SetAttributeValue("StrokeLinearGradientStartPointX", value);
    }

    public GrKonvaJsFloat32Value? StartPointY
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("StrokeLinearGradientStartPointY");
        set => ParentStyle.SetAttributeValue("StrokeLinearGradientStartPointY", value);
    }
    
    public GrKonvaJsVector2Value? EndPoint
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsVector2Value>("StrokeLinearGradientEndPoint");
        set => ParentStyle.SetAttributeValue("StrokeLinearGradientEndPoint", value);
    }
        
    public GrKonvaJsFloat32Value? EndPointX
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("StrokeLinearGradientEndPointX");
        set => ParentStyle.SetAttributeValue("StrokeLinearGradientEndPointX", value);
    }

    public GrKonvaJsFloat32Value? EndPointY
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("StrokeLinearGradientEndPointY");
        set => ParentStyle.SetAttributeValue("StrokeLinearGradientEndPointY", value);
    }


    public GrKonvaShapeStrokeLinearGradient(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}