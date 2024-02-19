using CSharpMath.Rendering.FrontEnd;
using CSharpMath.SkiaSharp;
using WebComposerLib.Colors;

namespace WebComposerLib.LaTeX.ImageComposers;

public class WclCSharpMathLaTeXImageComposer :
    IWclLaTeXImageComposer
{
    public static WclCSharpMathLaTeXImageComposer DefaultMathComposer { get; }
        = new WclCSharpMathLaTeXImageComposer()
        {
            TextColor = Color.Black,
            FontSize = 16f,
            Magnification = 1f,
            CanvasWidth = 2000f,
            PaintStyle = PaintStyle.Fill,
            RendererType = WclCSharpMathLaTeXRendererType.MathPainter
        };

    public static WclCSharpMathLaTeXImageComposer DefaultTextComposer { get; }
        = new WclCSharpMathLaTeXImageComposer()
        {
            TextColor = Color.Black,
            FontSize = 16f,
            Magnification = 1f,
            CanvasWidth = 2000f,
            PaintStyle = PaintStyle.Fill,
            RendererType = WclCSharpMathLaTeXRendererType.TextPainter
        };


    public Color TextColor { get; init; } 
        = Color.Black;

    public float FontSize { get; init; } 
        = 16f;

    public float Magnification { get; init; } 
        = 1f;

    public float CanvasWidth { get; init; }
        = 2000f;

    public PaintStyle PaintStyle { get; init; } 
        = PaintStyle.Fill;

    public WclCSharpMathLaTeXRendererType RendererType { get; init; } 
        = WclCSharpMathLaTeXRendererType.MathPainter;


    public Image RenderToPngImage(string latexCode)
    {
        Stream imageStream;

        if (RendererType == WclCSharpMathLaTeXRendererType.MathPainter)
        {
            var mathPainter = new MathPainter
            {
                LaTeX = latexCode,
                AntiAlias = true,
                Magnification = Magnification,
                FontSize = FontSize,
                PaintStyle = PaintStyle,
                TextColor = TextColor.ToSkiaSharpColor()
            };

            imageStream = mathPainter.DrawAsStream(textPainterCanvasWidth: CanvasWidth);
        }
        else
        {
            var textPainter = new TextPainter
            {
                LaTeX = latexCode,
                Magnification = Magnification,
                FontSize = FontSize,
                PaintStyle = PaintStyle,
                TextColor = TextColor.ToSkiaSharpColor()
            };

            imageStream = textPainter.DrawAsStream(textPainterCanvasWidth: CanvasWidth);
        }

        if (imageStream is null)
            throw new NullReferenceException(nameof(imageStream));

        return Image.Load(imageStream);
    }

    public void RenderToPngFile(string filePath, string latexCode)
    {
        RenderToPngImage(latexCode).SaveAsPng(filePath);
    }

    public void RenderToPngFiles(Func<int, string> filePathFunc, params string[] latexCodeList)
    {
        for (var i = 0; i < latexCodeList.Length; i++)
            RenderToPngFile(filePathFunc(i), latexCodeList[i]);
    }

    public void RenderToPngFiles(Func<int, string> filePathFunc, IEnumerable<string> latexCodeList)
    {
        var i = 0;
        foreach (var mathTextLine in latexCodeList)
        {
            RenderToPngFile(filePathFunc(i), mathTextLine);

            i++;
        }
    }
}