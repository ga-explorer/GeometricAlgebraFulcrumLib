using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using SixLabors.ImageSharp;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Samples.KaTeX;

public static class KaTeXSamples1
{
    public static string WorkingFolder { get; }
        = @"D:\Projects\Active\GeometricAlgebraFulcrumLib.Utilities.Web\Study";

    public static string[] KeTeXCode { get; }
        = new string[]
        {
            "x_2-y_1",
            @"f \left( x \right) = e^{\pi/2kt}"
        };
        

    public static void Example1()
    {
        var katexComposer = new WclKaTeXComposer(WorkingFolder)
        {
            ThrowOnError = false,
            FontSizeEm = 4,
            Output = WclKaTeXComposer.OutputKind.MathMl,
            SaveImages = true
        };
            
        for (var i = 0; i < KeTeXCode.Length; i++)
            katexComposer.AddLaTeXCode($"katex{i}", KeTeXCode[i]);

        katexComposer.RenderKaTeX();

        //var index = 0;
        //foreach (var svgCode in katexComposer.KaTeXSvgDataUrlList)
        //{
        //    File.WriteAllText(
        //        WorkingFolder.GetFilePath(
        //            $"KaTeX-{index:D6}",
        //            "svg"
        //        ),
        //        svgCode
        //    );

        //    index++;
        //}
    }

    public static void Example2()
    {
        var katexComposer = new WclKaTeXComposer(WorkingFolder)
        {
            ThrowOnError = false,
            FontSizeEm = 4,
            Output = WclKaTeXComposer.OutputKind.Html
        };

        for (var i = 0; i < KeTeXCode.Length; i++)
            katexComposer.AddLaTeXCode($"katex{i}", KeTeXCode[i]);

        katexComposer.RenderKaTeX();

        var index = 0;
        foreach (var image in katexComposer.Images)
        {
            image.SaveAsPng(
                WorkingFolder.GetFilePath(
                    $"KaTeX-{index:D6}",
                    "png"
                )
            );

            index++;
        }
    }
}