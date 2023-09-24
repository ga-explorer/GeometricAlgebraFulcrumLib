using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs;

/// <summary>
/// https://konvajs.org/api/Konva.Node.html#cache__anchor
/// </summary>
public class GrKonvaJsCacheConfig :
    GrKonvaJsObjectOptions
{
    public GrKonvaJsFloat32Value? X
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("X");
        init => SetAttributeValue("X", value);
    }

    public GrKonvaJsFloat32Value? Y
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Y");
        init => SetAttributeValue("Y", value);
    }

    public GrKonvaJsFloat32Value? Width
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Width");
        init => SetAttributeValue("Width", value);
    }

    public GrKonvaJsFloat32Value? Height
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Height");
        init => SetAttributeValue("Height", value);
    }
    
    public GrKonvaJsFloat32Value? Offset
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Offset");
        init => SetAttributeValue("Offset", value);
    }
    
    public GrKonvaJsFloat32Value? PixelRatio
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("PixelRatio");
        init => SetAttributeValue("PixelRatio", value);
    }
    
    public GrKonvaJsFloat32Value? HitCanvasPixelRatio
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("HitCanvasPixelRatio");
        init => SetAttributeValue("HitCanvasPixelRatio", value);
    }
    
    public GrKonvaJsBooleanValue? ImageSmoothingEnabled
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("ImageSmoothingEnabled");
        init => SetAttributeValue("ImageSmoothingEnabled", value);
    }

    public GrKonvaJsBooleanValue? DrawBorder
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("DrawBorder");
        init => SetAttributeValue("DrawBorder", value);
    }

    
}