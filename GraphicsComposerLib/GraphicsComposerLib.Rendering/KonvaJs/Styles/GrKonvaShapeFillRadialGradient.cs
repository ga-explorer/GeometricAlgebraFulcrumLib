using GraphicsComposerLib.Rendering.KonvaJs.Values;

namespace GraphicsComposerLib.Rendering.KonvaJs.Styles;

public sealed class GrKonvaShapeFillRadialGradient : 
    GrKonvaShapeFill
{
    public GrKonvaJsColorLinearGradientListValue? ColorStops { get; init; }
    
    public GrKonvaJsVector2Value? StartPoint { get; init; }
        
    public GrKonvaJsFloat32Value? StartPointX { get; init; }

    public GrKonvaJsFloat32Value? StartPointY { get; init; }

    public GrKonvaJsVector2Value? EndPoint { get; init; }
        
    public GrKonvaJsFloat32Value? EndPointX { get; init; }

    public GrKonvaJsFloat32Value? EndPointY { get; init; }
        
    public GrKonvaJsFloat32Value? StartRadius { get; init; }
        
    public GrKonvaJsFloat32Value? EndRadius { get; init; }

    public override GrKonvaShapeFillKind Kind 
        => GrKonvaShapeFillKind.RadialGradient;


    internal GrKonvaShapeFillRadialGradient(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}