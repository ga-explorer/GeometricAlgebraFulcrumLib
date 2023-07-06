using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Text
{
    public sealed class TextComposerFloat64
        : TextComposer<double>
    {
        public static TextComposerFloat64 DefaultComposer { get; }
            = new TextComposerFloat64();

        
        private TextComposerFloat64() 
            : base(ScalarProcessorOfFloat64.DefaultProcessor)
        {
        }
    }
}