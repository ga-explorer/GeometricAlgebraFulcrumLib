using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Text
{
    public static class TextComposerFactory
    {
        public static TextComposer<T> CreateTextComposer<T>(this IScalarAlgebraProcessor<T> processor)
        {
            return new TextComposer<T>(processor);
        }

        public static LaTeXComposer<T> CreateLaTeXComposer<T>(this IScalarAlgebraProcessor<T> processor)
        {
            return new LaTeXComposer<T>(processor);
        }
    }
}