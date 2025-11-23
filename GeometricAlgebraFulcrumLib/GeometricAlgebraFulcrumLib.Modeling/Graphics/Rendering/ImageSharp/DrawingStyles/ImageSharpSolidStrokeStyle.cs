using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingStyles;

public class ImageSharpSolidStrokeStyle(Color strokeColor, float strokeWidth = 1) :
    IImageSharpStrokePenComposer
{
    /// <summary>
    /// The color of the solid stroke brush used in the pen
    /// </summary>
    public Color StrokeColor { get; } = strokeColor;

    /// <summary>
    /// The stroke width in px units. Defaults to 1px.
    /// </summary>
    public float StrokeWidth { get; } = strokeWidth;

    /// <summary>
    /// The stroke pattern.
    /// </summary>
    public float[] StrokePattern { get; set; } = [];

    /// <summary>
    /// The joint style.
    /// </summary>
    public JointStyle JointStyle { get; set; } = JointStyle.Square;

    /// <summary>
    /// The end cap style.
    /// </summary>
    public EndCapStyle EndCapStyle { get; set; } = EndCapStyle.Butt;


    public Brush GetStrokeBrush()
    {
        return new SolidBrush(StrokeColor);
    }

    public PenOptions GetStrokePenOptions()
    {
        var brush = GetStrokeBrush();

        return new PenOptions(brush, StrokeWidth, StrokePattern)
        {
            JointStyle = JointStyle,
            EndCapStyle = EndCapStyle
        };
    }

    public Pen GetStrokePen()
    {
        var options = GetStrokePenOptions();

        return StrokePattern.Length > 0 
            ? new PatternPen(options) 
            : new SolidPen(options);
    }
}
