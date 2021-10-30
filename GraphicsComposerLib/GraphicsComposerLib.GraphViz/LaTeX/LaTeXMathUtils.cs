using System.Text;

namespace GraphicsComposerLib.GraphViz.LaTeX
{
    public static class LaTeXMathUtils
    {
        public static string LaTeXMathRoundParentheses(this string latexMathText)
        {
            return $@"\left( {latexMathText} \right)";
        }

        public static string LaTeXMathSquareBrackets(this string latexMathText)
        {
            return $@"\left[ {latexMathText} \right]";
        }

        public static string LaTeXMathCurlyBraces(this string latexMathText)
        {
            return $@"\left\{{ {latexMathText} \right\}}";
        }

        public static string LaTeXMathGroupBrackets(this string latexMathText)
        {
            return $@"\left\lgroup {latexMathText} \right\rgroup";
        }

        public static string LaTeXMathAngleBraces(this string latexMathText)
        {
            return $@"\left\langle {latexMathText} \right\rangle";
        }

        public static string LaTeXMathPipes(this string latexMathText)
        {
            return $@"\left| {latexMathText} \right|";
        }

        public static string LaTeXMathDoublePipes(this string latexMathText)
        {
            return $@"\left\| {latexMathText} \right\|";
        }

        public static string LaTeXMathCeilingBrackets(this string latexMathText)
        {
            return $@"\left\lceil {latexMathText} \right\rceil";
        }

        public static string LaTeXMathFloorBrackets(this string latexMathText)
        {
            return $@"\left\lfloor {latexMathText} \right\rfloor";
        }

        public static string LaTeXMathBrackets(this string latexMathText, string leftBracket, string rightBracket)
        {
            return $@"\left{leftBracket} {latexMathText} \right{rightBracket}";
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
