using WebComposerLib.LaTeX.KaTeX.Expressions;

namespace WebComposerLib.LaTeX.KaTeX;

public static class WclKaTeXLetters
{
    public static class UpperGreek
    {
        public static WclKaTeXLeafExpression Alpha { get; }
            = new WclKaTeXLeafExpression(@"\Alpha");

    }

    public static class LowerGreek
    {
        public static WclKaTeXLeafExpression Alpha { get; }
            = new WclKaTeXLeafExpression(@"\alpha");

    }

    public static class Other
    {

    }
}