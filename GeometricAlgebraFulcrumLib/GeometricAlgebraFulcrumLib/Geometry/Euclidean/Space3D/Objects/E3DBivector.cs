using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;

public sealed record E3DBivector<T>
{
    public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

    public T Xy { get; }

    public T Xz { get; }

    public T Yz { get; }

    public bool AssumeUnit { get; }


    internal E3DBivector([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, [NotNull] T xy, [NotNull] T xz, [NotNull] T yz, bool assumeUnit = false)
    {
        ScalarProcessor = scalarProcessor;
        Xy = xy;
        Xz = xz;
        Yz = yz;
        AssumeUnit = assumeUnit;
    }
}