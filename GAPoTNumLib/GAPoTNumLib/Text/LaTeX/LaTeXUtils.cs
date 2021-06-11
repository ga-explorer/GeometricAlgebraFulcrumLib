using System;
using System.Linq;
using System.Text;
using GAPoTNumLib.GAPoT;

namespace GAPoTNumLib.Text.LaTeX
{
    public static class LaTeXUtils
    {
        public static int LaTeXDecimalPlaces { get; set; }
            = 5;


        public static string GetLaTeXNumber(this double value)
        {
            var valueText = value.ToString("G");

            if (valueText.Contains('E'))
            {
                var ePosition = valueText.IndexOf('E');

                var magnitude = double.Parse(valueText.Substring(0, ePosition));
                var magnitudeText = Math.Round(magnitude, LaTeXDecimalPlaces).ToString("G");
                var exponentText = valueText.Substring(ePosition + 1);

                return $@"{magnitudeText} \times 10^{{{exponentText}}}";
            }

            return Math.Round(value, LaTeXDecimalPlaces).ToString("G");
        }

        public static string GetLaTeXAngleInDegrees(this double angleInRadians)
        {
            var angleInDegrees = angleInRadians.RadiansToDegrees().GetLaTeXNumber();

            return angleInDegrees + @"^{\circ}";
        }

        public static string LaTeXSuperscript(this string item, string subscript)
        {
            //return $@"\boldsymbol{{\sigma^{{{basisSubscript}}}}}";
            return $@"{item}^{{{subscript}}}";
        }

        public static string LaTeXSubscript(this string item, string subscript)
        {
            //return $@"\boldsymbol{{\sigma_{{{basisSubscript}}}}}";
            return $@"{item}_{{{subscript}}}";
        }

        public static string GetLaTeXBasisName(this string basisSubscript)
        {
            return @"\bm{\sigma}".LaTeXSubscript(basisSubscript);
        }

        public static string GetLaTeXArray(this double[,] array)
        {
            var rowsCount = array.GetLength(0);
            var colsCount = array.GetLength(1);

            var textComposer = new StringBuilder();

            textComposer.AppendLine(@"\[");

            var ccc = Enumerable.Repeat("c", array.GetLength(1)).Concatenate();
            textComposer.AppendLine(@"\left(\begin{array}{" + ccc + "}");

            for (var i = 0; i < rowsCount; i++)
            {
                for (var j = 0; j < colsCount; j++)
                {
                    textComposer.Append(array[i, j].GetLaTeXNumber());

                    if (j < colsCount - 1)
                        textComposer.Append(@" & ");
                }

                if (i < rowsCount - 1)
                    textComposer.Append(@"\\");
            }

            textComposer.AppendLine(@"\end{array}\right)");

            textComposer.AppendLine(@"\]");

            return textComposer.ToString();
        }

        public static string GetLaTeXDisplayEquation(this string latexMathText)
        {
            var textComposer = new StringBuilder();

            textComposer.AppendLine(@"\[");
            textComposer.AppendLine(latexMathText.Trim());
            textComposer.AppendLine(@"\]");

            return textComposer.ToString();
        }

        public static string GetLaTeXInlineEquation(this string latexMathText)
        {
            var textComposer = new StringBuilder();

            textComposer.Append(@"$");
            textComposer.Append(latexMathText.Trim());
            textComposer.Append(@"$");

            return textComposer.ToString();
        }
    }
}
