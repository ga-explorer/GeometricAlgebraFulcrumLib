using GraphicsComposerLib.Rendering.LaTeX.KaTeX.Expressions;

namespace GraphicsComposerLib.Rendering.LaTeX.KaTeX
{
    public static class KaTeXLetters
    {
        public static class UpperGreek
        {
            public static KaTeXLeafExpression Alpha { get; }
                = new KaTeXLeafExpression(@"\Alpha");

        }

        public static class LowerGreek
        {
            public static KaTeXLeafExpression Alpha { get; }
                = new KaTeXLeafExpression(@"\alpha");

        }

        public static class Other
        {

        }
    }
}