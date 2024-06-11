using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

public sealed class LaTeXComposerFloat64
    : LaTeXComposer<double>
{
    public static LaTeXComposerFloat64 DefaultComposer { get; }
        = new LaTeXComposerFloat64();


    private LaTeXComposerFloat64()
        : base(ScalarProcessorOfFloat64.Instance)
    {
    }
}