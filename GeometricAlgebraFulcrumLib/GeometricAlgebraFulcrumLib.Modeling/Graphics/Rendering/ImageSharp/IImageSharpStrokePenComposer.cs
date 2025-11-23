using SixLabors.ImageSharp.Drawing.Processing;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp;

public interface IImageSharpStrokePenComposer
{
    /// <summary>
    /// Gets the brush used to color the stroke outline of a shape.
    /// </summary>
    Brush GetStrokeBrush();

    /// <summary>
    /// Gets the pen options used to construct the final pen object.
    /// </summary>
    /// <returns></returns>
    PenOptions GetStrokePenOptions();

    /// <summary>
    /// Gets the final pen object
    /// </summary>
    Pen GetStrokePen();
}