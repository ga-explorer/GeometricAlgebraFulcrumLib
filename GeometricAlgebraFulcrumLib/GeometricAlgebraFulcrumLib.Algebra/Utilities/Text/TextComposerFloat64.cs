using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

public sealed class TextComposerFloat64
    : TextComposer<double>
{
    public static TextComposerFloat64 DefaultComposer { get; }
        = new TextComposerFloat64();


    private TextComposerFloat64()
        : base(ScalarProcessorOfFloat64.Instance)
    {
    }
}