using GeometricAlgebraLib.Processing.Scalars;

namespace GeometricAlgebraLib.Processing.Implementations.NamedScalars
{
    public interface IGaScalarProcessorNamedScalar<TScalar>
        : IGaSymbolicScalarProcessor<IGaNamedScalar<TScalar>>
    {
        GaNamedScalarsCollection<TScalar> NamedScalarsCollection { get; }

        IGaSymbolicScalarProcessor<TScalar> SymbolicScalarProcessor { get; }
    }
}