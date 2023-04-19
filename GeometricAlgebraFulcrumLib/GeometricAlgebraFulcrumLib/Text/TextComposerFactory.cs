using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.Text
{
    public static class TextComposerFactory
    {
        public static TextComposer<T> CreateTextComposer<T>(this IScalarProcessor<T> processor)
        {
            return new TextComposer<T>(processor);
        }

        public static LaTeXComposer<T> CreateLaTeXComposer<T>(this IScalarProcessor<T> processor)
        {
            return new LaTeXComposer<T>(processor);
        }
    }
}