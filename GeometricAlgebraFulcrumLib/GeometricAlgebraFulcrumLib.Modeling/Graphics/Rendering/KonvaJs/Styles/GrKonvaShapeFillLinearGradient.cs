using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Styles;

public sealed class GrKonvaShapeFillLinearGradient : 
    GrKonvaShapeFill
{
    
    public GrKonvaJsColorLinearGradientListValue? ColorStops
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsColorLinearGradientListValue>("FillRadialGradientColorStops");
        set => ParentStyle.SetAttributeValue("FillRadialGradientColorStops", value);
    }
    
    public GrKonvaJsVector2Value? StartPoint
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsVector2Value>("FillRadialGradientStartPoint");
        set => ParentStyle.SetAttributeValue("FillRadialGradientStartPoint", value);
    }
    
    public GrKonvaJsFloat32Value? StartPointX
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillRadialGradientStartPointX");
        set => ParentStyle.SetAttributeValue("FillRadialGradientStartPointX", value);
    }

    public GrKonvaJsFloat32Value? StartPointY
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillRadialGradientStartPointY");
        set => ParentStyle.SetAttributeValue("FillRadialGradientStartPointY", value);
    }

    public GrKonvaJsVector2Value? EndPoint
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsVector2Value>("FillRadialGradientEndPoint");
        set => ParentStyle.SetAttributeValue("FillRadialGradientEndPoint", value);
    }
    
    public GrKonvaJsFloat32Value? EndPointX
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillRadialGradientEndPointX");
        set => ParentStyle.SetAttributeValue("FillRadialGradientEndPointX", value);
    }

    public GrKonvaJsFloat32Value? EndPointY
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillRadialGradientEndPointY");
        set => ParentStyle.SetAttributeValue("FillRadialGradientEndPointY", value);
    }
    
    public GrKonvaJsFloat32Value? StartRadius
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillRadialGradientStartRadius");
        set => ParentStyle.SetAttributeValue("FillRadialGradientStartRadius", value);
    }
    
    public GrKonvaJsFloat32Value? EndRadius
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillRadialGradientEndRadius");
        set => ParentStyle.SetAttributeValue("FillRadialGradientEndRadius", value);
    }

    public override GrKonvaShapeFillKind Kind 
        => GrKonvaShapeFillKind.LinearGradient;


    internal GrKonvaShapeFillLinearGradient(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}