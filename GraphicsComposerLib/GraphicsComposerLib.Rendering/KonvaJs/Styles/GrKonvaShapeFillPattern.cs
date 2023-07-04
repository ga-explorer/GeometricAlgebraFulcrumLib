using GraphicsComposerLib.Rendering.KonvaJs.Values;

namespace GraphicsComposerLib.Rendering.KonvaJs.Styles;

public sealed class GrKonvaShapeFillPattern : 
    GrKonvaShapeFill
{
    public GrKonvaJsImageValue? Image
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsImageValue>("FillPatternImage");
        set => ParentStyle.SetAttributeValue("FillPatternImage", value);
    }
    
    public GrKonvaJsVector2Value? Offset
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsVector2Value>("FillPatternOffset");
        set => ParentStyle.SetAttributeValue("FillPatternOffset", value);
    }
    
    public GrKonvaJsFloat32Value? OffsetX
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillPatternOffsetX");
        set => ParentStyle.SetAttributeValue("FillPatternOffsetX", value);
    }

    public GrKonvaJsFloat32Value? OffsetY
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillPatternOffsetY");
        set => ParentStyle.SetAttributeValue("FillPatternOffsetY", value);
    }

    public GrKonvaJsFillPatternRepeatValue? Repeat
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFillPatternRepeatValue>("FillPatternRepeat");
        set => ParentStyle.SetAttributeValue("FillPatternRepeat", value);
    }

    public GrKonvaJsFloat32Value? Rotation
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillPatternRotation");
        set => ParentStyle.SetAttributeValue("FillPatternRotation", value);
    }
        
    public GrKonvaJsVector2Value? Scale
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsVector2Value>("FillPatternScale");
        set => ParentStyle.SetAttributeValue("FillPatternScale", value);
    }
    
    public GrKonvaJsFloat32Value? ScaleX
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillPatternScaleX");
        set => ParentStyle.SetAttributeValue("FillPatternScaleX", value);
    }

    public GrKonvaJsFloat32Value? ScaleY
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillPatternScaleY");
        set => ParentStyle.SetAttributeValue("FillPatternScaleY", value);
    }
    
    public GrKonvaJsFloat32Value? X
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillPatternX");
        set => ParentStyle.SetAttributeValue("FillPatternX", value);
    }

    public GrKonvaJsFloat32Value? Y
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("FillPatternY");
        set => ParentStyle.SetAttributeValue("FillPatternY", value);
    }
        
    public override GrKonvaShapeFillKind Kind 
        => GrKonvaShapeFillKind.Pattern;


    internal GrKonvaShapeFillPattern(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}