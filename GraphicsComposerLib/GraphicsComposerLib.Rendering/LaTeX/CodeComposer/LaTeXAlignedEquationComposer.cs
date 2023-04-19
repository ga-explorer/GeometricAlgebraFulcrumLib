using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.LaTeX.CodeComposer
{
    public sealed class LaTeXAlignedEquationComposer :
        LinearTextComposer
    {
        public bool AddEquationNumber { get; set; }


        public LaTeXAlignedEquationComposer AppendLaTeXBegin()
        {
            var alignText = 
                AddEquationNumber ? "align" : "align*";

            AppendLineAtNewLine(@$"\begin{{{alignText}}}");//.IncreaseIndentation();

            return this;
        }

        public LaTeXAlignedEquationComposer AppendLaTeXEnd()
        {
            var alignText = 
                AddEquationNumber ? "align" : "align*";

            AppendAtNewLine(@$"\end{{{alignText}}}");
            //AppendLine().DecreaseIndentation().AppendLine(@$"\end{{{alignText}}}");

            return this;
        }

        public LaTeXAlignedEquationComposer AppendLaTeX(string leftLaTeX, string midLaTeX, string rightLaTeX)
        {
            AppendAtNewLine(leftLaTeX)
                .Append(midLaTeX)
                .Append(rightLaTeX)
                .AppendLine(@"\\");

            return this;
        }

        public LaTeXAlignedEquationComposer AppendLaTeXEqual(string leftLaTeX, string rightLaTeX)
        {
            return AppendLaTeX(leftLaTeX, @" & = ", rightLaTeX);
        }
        
        public LaTeXAlignedEquationComposer AppendLaTeXEqual(string rightLaTeX)
        {
            return AppendLaTeX(string.Empty, @" & = ", rightLaTeX);
        }

        public LaTeXAlignedEquationComposer AppendLaTeXPlus(string rightLaTeX)
        {
            return AppendLaTeX(string.Empty, @" & + ", rightLaTeX);
        }
        
        public LaTeXAlignedEquationComposer AppendLaTeXMinus(string rightLaTeX)
        {
            return AppendLaTeX(string.Empty, @" & - ", rightLaTeX);
        }
    }
}