using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Svg.DrawingBoard;

public sealed class SvgDrawingBoardStyle
{
    private readonly List<int> _penDashArray
        = new List<int>();


    public SvgDrawingBoard ParentDrawingBoard { get; }

    /// <summary>
    /// The current width in pixels of the pen used to draw the up coming
    /// shapes
    /// </summary>
    public int PenPixelsWidth { get; set; }
        = 1;

    public double PenWidth
        => PenPixelsWidth * ParentDrawingBoard.LengthByPixelsRatio;

    /// <summary>
    /// The current color of the pen used to draw the up coming shapes
    /// </summary>
    public Color PenColor { get; set; }
        = Color.Black;

    public double PenOpacity { get; set; }
        = 1.0d;

    /// <summary>
    /// The current dash pattern of the pen used to draw the up coming shapes
    /// </summary>
    public string PenDashPattern
        => _penDashArray
            .Select(n => ParentDrawingBoard.LengthByPixelsRatio * n)
            .Concatenate(" ");

    /// <summary>
    /// The current fill color of the pen used to draw the up coming shapes
    /// </summary>
    public Color FillColor { get; set; }
        = Color.White;

    public double FillOpacity { get; set; }
        = 1.0d;


    /// <summary>
    /// The current font size in pixels used to draw the upcoming text
    /// </summary>
    public int FontPixelsSize { get; set; }
        = 16;

    /// <summary>
    /// The current font size in pixels used to draw the upcoming text
    /// </summary>
    public double FontSize
        => FontPixelsSize * ParentDrawingBoard.LengthByPixelsRatio;


    internal SvgDrawingBoardStyle(SvgDrawingBoard parentDrawingBoard)
    {
        ParentDrawingBoard = parentDrawingBoard;
    }


    public SvgDrawingBoardStyle Reset()
    {
        PenPixelsWidth = 1;
        PenColor = Color.Black;
        PenOpacity = 1;
        _penDashArray.Clear();

        FillColor = Color.White;
        FillOpacity = 1;

        return this;
    }


    public SvgDrawingBoardStyle SetPen(int penPixelsWidth, System.Drawing.Color penColor, params int[] penDashPattern)
    {
        return SetPen(penPixelsWidth, penColor.ToImageSharpColor(), penDashPattern);
    }

    public SvgDrawingBoardStyle SetPen(int penPixelsWidth, Color penColor, params int[] penDashPattern)
    {
        PenPixelsWidth = penPixelsWidth;

        PenColor = penColor;

        _penDashArray.Clear();
        if (penDashPattern.Length > 0)
            _penDashArray.AddRange(penDashPattern);

        return this;
    }

    public SvgDrawingBoardStyle SetFill(Color fillColor, double fillOpacity = 1)
    {
        FillColor = fillColor;
        FillOpacity = fillOpacity;

        return this;
    }

    /// <summary>
    /// Set the current pen's drawing pattern to solid
    /// </summary>
    /// <returns></returns>
    public SvgDrawingBoardStyle ClearPenDashPattern()
    {
        _penDashArray.Clear();

        return this;
    }

    /// <summary>
    /// Set the current pen's drawing pattern
    /// </summary>
    /// <param name="penDashPattern"></param>
    /// <returns></returns>
    public SvgDrawingBoardStyle SetPenDashPattern(params int[] penDashPattern)
    {
        _penDashArray.Clear();
        _penDashArray.AddRange(penDashPattern);

        return this;
    }

}