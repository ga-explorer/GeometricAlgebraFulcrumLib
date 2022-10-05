using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space2D.Objects;

public sealed record E2DBivector<T>
{
    public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

    public T Xy { get; }
    
    

    public bool AssumeUnit { get; }


    internal E2DBivector([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, [NotNull] T xy, bool assumeUnit = false)
    {
        ScalarProcessor = scalarProcessor;
        Xy = xy;
        AssumeUnit = assumeUnit;
    }
}