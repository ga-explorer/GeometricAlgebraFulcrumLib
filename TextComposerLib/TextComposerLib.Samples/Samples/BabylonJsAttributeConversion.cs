using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;
using TextComposerLib.Text.Parametric;

namespace TextComposerLib.Samples.Samples
{
    public static class BabylonJsAttributeConversion
    {
        public static string InputCode { get; } = @"
public GrBabylonJsFloat32Value? AnimationLength { get; set; }

public GrBabylonJsColor3Value? BaseColor { get; set; }

public GrBabylonJsFloat32Value? BaseScale { get; set; }

public GrBabylonJsFloat32Value? DragScale { get; set; }

public GrBabylonJsColor3Value? HoverColor { get; set; }

public GrBabylonJsFloat32Value? HoverScale { get; set; }

public GrBabylonJsFloat32Value? Hover { get; set; }

public GrBabylonJsFloat32Value? Drag { get; set; }
";

        public static void Convert()
        {
            var composer = new ParametricTextComposer(
                "#", 
                "#",
                @"
public #type#? #name#
{
    get => GetAttributeValueOrNull<#type#>(""#js-name#"");
    set => SetAttributeValue(""#js-name#"", value);
}
".Trim()
            );

            var codeLines =
                InputCode
                    .SplitLines()
                    .Where(line => !string.IsNullOrEmpty(line.Trim()));

            foreach (var inputLine in codeLines)
            {
                var index1 = inputLine.IndexOf(' ') + 1;
                var index2 = inputLine.IndexOf('?', index1) - 1;
                var count = index2 - index1 + 1;

                var attributeType = 
                    inputLine.Substring(index1, count);

                index1 = inputLine.IndexOf(' ', index2 + 2) + 1;
                index2 = inputLine.IndexOf(' ', index1) - 1;
                count = index2 - index1 + 1;

                var attributeName = 
                    inputLine.Substring(index1, count);

                var code = composer.GenerateText(
                    new Dictionary<string, string>()
                    {
                        {"type", attributeType},
                        {"name", attributeName},
                        {"js-name", attributeName.Camelize()}
                    }
                );

                Console.WriteLine(code);
                Console.WriteLine();
            }
        }
    }
}
