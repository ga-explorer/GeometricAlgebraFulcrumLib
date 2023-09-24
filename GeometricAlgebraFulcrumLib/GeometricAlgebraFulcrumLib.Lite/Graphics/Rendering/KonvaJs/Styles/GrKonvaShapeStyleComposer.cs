namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Styles;

public class GrKonvaShapeStyleComposer
{
    public GrKonvaShapeStyle CreatePointStyle(double lineWidth, Color lineColor)
    {
        var style = new GrKonvaShapeStyle(
            GrKonvaShapeStrokeKind.Color
        )
        {
            StrokeEnabled = true,
            StrokeWidth = lineWidth,
            ColorStroke =
            {
                Color = lineColor
            }
        };

        return style;
    }

    public GrKonvaShapeStyle CreatePointStyle(double lineWidth, Color lineColor, Color fillColor)
    {
        var style = new GrKonvaShapeStyle(
            GrKonvaShapeStrokeKind.Color, 
            GrKonvaShapeFillKind.Color
        )
        {
            StrokeEnabled = true,
            StrokeWidth = lineWidth,
            ColorStroke =
            {
                Color = lineColor
            },

            FillEnabled = true,
            ColorFill =
            {
                Color = fillColor
            }
        };

        return style;
    }
}