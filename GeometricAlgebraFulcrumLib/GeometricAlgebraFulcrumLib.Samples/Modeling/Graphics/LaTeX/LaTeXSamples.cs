using CSharpMath.SkiaSharp;
using GeometricAlgebraFulcrumLib.Samples.Modeling.Graphics.SkiaSharp;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Graphics.LaTeX;

public static class LaTeXSamples
{
    public static void CSharpMathExample1()
    {
        var painter = new MathPainter
        {
            AntiAlias = true,
            LaTeX = @"\left\langle A\right\rangle _{k}&=&\sum_{j=0}^{\binom{n}{k}-1}a_{\Phi\left(k,j\right)}\bm{E}_{\Phi\left(k,j\right)}"
        }; // or TextPainter
        using var png = painter.DrawAsStream();

        // or painter.DrawAsStream(format: SkiaSharp.SKEncodedImageFormat.Jpeg) for JPEG
        // or painter.DrawAsStream(format: SkiaSharp.SKEncodedImageFormat.Gif) for GIF
        // or painter.DrawAsStream(format: SkiaSharp.SKEncodedImageFormat.Bmp) for BMP
        // or... you get it.

        png?.SaveStream(@"D:\CSharpMathExample1.png");

    }

    //public static void WpfMathExample1()
    //{
    //    const string latex = @"\frac{2+2}{2}";
    //    const string fileName = @"T:\Temp\formula.png";

    //    var parser = WpfTeXFormulaParser.Instance;
    //    var formula = parser.Parse(latex);
    //    var environment = WpfTeXEnvironment.Create(TexStyle.Display, 20.0, "Arial");
    //    var bitmapSource = formula.RenderToBitmap(environment);
    //    Console.WriteLine($"Image width: {bitmapSource.Width}");
    //    Console.WriteLine($"Image height: {bitmapSource.Height}");

    //    var encoder = new PngBitmapEncoder();
    //    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

    //    using var target = new FileStream(fileName, FileMode.Create);

    //    encoder.Save(target);
    //    Console.WriteLine($"File saved to {fileName}");

    //}

    //public static void WpfMathExample2()
    //{
    //    const string latex = @"\frac{2+2}{2}";
    //    const string fileName = @"T:\Temp\formula.png";

    //    var parser = WpfTeXFormulaParser.Instance;
    //    var formula = parser.Parse(latex);
    //    var environment = WpfTeXEnvironment.Create(TexStyle.Display, 20.0, "Arial");
    //    var bitmapSource = formula.RenderToBitmap(environment);
    //    Console.WriteLine($"Image width: {bitmapSource.Width}");
    //    Console.WriteLine($"Image height: {bitmapSource.Height}");

    //    var encoder = new PngBitmapEncoder();
    //    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

    //    using var target = new FileStream(fileName, FileMode.Create);

    //    encoder.Save(target);
    //    Console.WriteLine($"File saved to {fileName}");
    //}
}