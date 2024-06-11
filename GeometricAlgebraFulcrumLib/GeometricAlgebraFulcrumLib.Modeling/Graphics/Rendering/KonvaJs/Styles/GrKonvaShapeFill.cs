namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Styles;

public abstract class GrKonvaShapeFill :
    GrKonvaShapeSubStyle
{
    public abstract GrKonvaShapeFillKind Kind { get; }


    protected GrKonvaShapeFill(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}