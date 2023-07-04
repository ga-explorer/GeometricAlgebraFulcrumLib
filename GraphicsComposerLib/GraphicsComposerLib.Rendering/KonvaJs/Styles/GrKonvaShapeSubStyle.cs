namespace GraphicsComposerLib.Rendering.KonvaJs.Styles;

public abstract class GrKonvaShapeSubStyle
{
    protected GrKonvaShapeStyle ParentStyle { get; }


    protected GrKonvaShapeSubStyle(GrKonvaShapeStyle parentStyle)
    {
        ParentStyle = parentStyle;
    }
}