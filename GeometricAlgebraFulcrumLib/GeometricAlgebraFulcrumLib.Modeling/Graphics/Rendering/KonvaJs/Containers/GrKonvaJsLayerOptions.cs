using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Containers;

public class GrKonvaJsLayerOptions :
    GrKonvaJsObjectOptions
{
    public GrKonvaJsBooleanValue? ClearBeforeDraw
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("ClearBeforeDraw");
        init => SetAttributeValue("ClearBeforeDraw", value);
    }

    public GrKonvaJsFloat32Value? X
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("X");
        set => SetAttributeValue("X", value);
    }

    public GrKonvaJsFloat32Value? Y
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Y");
        set => SetAttributeValue("Y", value);
    }

    public GrKonvaJsFloat32Value? Width
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Width");
        set => SetAttributeValue("Width", value);
    }

    public GrKonvaJsFloat32Value? Height
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Height");
        set => SetAttributeValue("Height", value);
    }

    public GrKonvaJsBooleanValue? Visible
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("Visible");
        set => SetAttributeValue("Visible", value);
    }

    public GrKonvaJsBooleanValue? Listening
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("Listening");
        set => SetAttributeValue("Listening", value);
    }

    public GrKonvaJsStringValue? Id
    {
        get => GetAttributeValueOrNull<GrKonvaJsStringValue>("Id");
        set => SetAttributeValue("Id", value);
    }

    public GrKonvaJsStringValue? Name
    {
        get => GetAttributeValueOrNull<GrKonvaJsStringValue>("Name");
        set => SetAttributeValue("Name", value);
    }

    public GrKonvaJsFloat32Value? Opacity
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Opacity");
        set => SetAttributeValue("Opacity", value);
    }

    public GrKonvaJsVector2Value? Scale
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("Scale");
        set => SetAttributeValue("Scale", value);
    }

    public GrKonvaJsFloat32Value? ScaleX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ScaleX");
        set => SetAttributeValue("ScaleX", value);
    }

    public GrKonvaJsFloat32Value? ScaleY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ScaleY");
        set => SetAttributeValue("ScaleY", value);
    }

    public GrKonvaJsFloat32Value? Rotation
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Rotation");
        set => SetAttributeValue("Rotation", value);
    }

    public GrKonvaJsVector2Value? Offset
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("Offset");
        set => SetAttributeValue("Offset", value);
    }

    public GrKonvaJsFloat32Value? OffsetX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("OffsetX");
        set => SetAttributeValue("OffsetX", value);
    }

    public GrKonvaJsFloat32Value? OffsetY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("OffsetY");
        set => SetAttributeValue("OffsetY", value);
    }

    public GrKonvaJsBooleanValue? Draggable
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("Draggable");
        set => SetAttributeValue("Draggable", value);
    }

    public GrKonvaJsFloat32Value? DragDistance
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("DragDistance");
        set => SetAttributeValue("DragDistance", value);
    }

    public GrKonvaJsCodeValue? DragBoundFunc
    {
        get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("DragBoundFunc");
        set => SetAttributeValue("DragBoundFunc", value);
    }

    public GrKonvaJsFloat32Value? ClipX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ClipX");
        init => SetAttributeValue("ClipX", value);
    }

    public GrKonvaJsFloat32Value? ClipY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ClipY");
        init => SetAttributeValue("ClipY", value);
    }

    public GrKonvaJsFloat32Value? ClipWidth
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ClipWidth");
        init => SetAttributeValue("ClipWidth", value);
    }

    public GrKonvaJsFloat32Value? ClipHeight
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ClipHeight");
        init => SetAttributeValue("ClipHeight", value);
    }
        
    public GrKonvaJsCodeValue? ClipFunc
    {
        get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("ClipFunc");
        init => SetAttributeValue("ClipFunc", value);
    }


    public GrKonvaJsLayerOptions()
    {
    }
        
    public GrKonvaJsLayerOptions(GrKonvaJsLayerOptions options)
    {
        SetAttributeValues(options);
    }
}