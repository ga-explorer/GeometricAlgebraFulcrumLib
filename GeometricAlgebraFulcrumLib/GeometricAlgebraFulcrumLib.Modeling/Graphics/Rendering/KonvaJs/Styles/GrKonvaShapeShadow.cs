using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Styles;

public class GrKonvaShapeShadow :
    GrKonvaShapeSubStyle
{
    
    public GrKonvaJsFloat32Value? Blur
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ShadowBlur");
        set => ParentStyle.SetAttributeValue("ShadowBlur", value);
    }
        
    public GrKonvaJsColorValue? Color
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsColorValue>("ShadowColor");
        set => ParentStyle.SetAttributeValue("ShadowColor", value);
    }
            
    public GrKonvaJsBooleanValue? ShadowForStrokeEnabled
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsBooleanValue>("ShadowForStrokeEnabled");
        set => ParentStyle.SetAttributeValue("ShadowForStrokeEnabled", value);
    }
        
    public GrKonvaJsVector2Value? Offset
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsVector2Value>("ShadowOffset");
        set => ParentStyle.SetAttributeValue("ShadowOffset", value);
    }
        
    public GrKonvaJsFloat32Value? OffsetX
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ShadowOffsetX");
        set => ParentStyle.SetAttributeValue("ShadowOffsetX", value);
    }

    public GrKonvaJsFloat32Value? OffsetY
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ShadowOffsetY");
        set => ParentStyle.SetAttributeValue("ShadowOffsetY", value);
    }
        
    public GrKonvaJsFloat32Value? Opacity
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ShadowOpacity");
        set => ParentStyle.SetAttributeValue("ShadowOpacity", value);
    }

    
    internal GrKonvaShapeShadow(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}