using CSharpMath.Rendering.FrontEnd;
using CSharpMath.SkiaSharp;
using GraphicsComposerLib.Rendering.Colors;

namespace GraphicsComposerLib.Rendering.LaTeX.ImageComposers
{
    public class GrCSharpMathLaTeXImageComposer :
        IGrLaTeXImageComposer
    {
        public static GrCSharpMathLaTeXImageComposer DefaultMathComposer { get; }
            = new GrCSharpMathLaTeXImageComposer()
            {
                TextColor = Color.Black,
                FontSize = 16f,
                Magnification = 1f,
                CanvasWidth = 2000f,
                PaintStyle = PaintStyle.Fill,
                RendererType = GrCSharpMathLaTeXRendererType.MathPainter
            };

        public static GrCSharpMathLaTeXImageComposer DefaultTextComposer { get; }
            = new GrCSharpMathLaTeXImageComposer()
            {
                TextColor = Color.Black,
                FontSize = 16f,
                Magnification = 1f,
                CanvasWidth = 2000f,
                PaintStyle = PaintStyle.Fill,
                RendererType = GrCSharpMathLaTeXRendererType.TextPainter
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

        public GrCSharpMathLaTeXRendererType RendererType { get; init; } 
            = GrCSharpMathLaTeXRendererType.MathPainter;


        public Image RenderToPngImage(string latexCode)
        {
            Stream imageStream;

            if (RendererType == GrCSharpMathLaTeXRendererType.MathPainter)
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
}