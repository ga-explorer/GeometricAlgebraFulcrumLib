using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Implementations.NamedScalars
{
    public interface IGaScalarProcessorNamedScalar<TScalar>
        : IGaSymbolicScalarProcessor<IGaNamedScalar<TScalar>>
    {
        GaNamedScalarsCollection<TScalar> NamedScalarsCollection { get; }

        IGaSymbolicScalarProcessor<TScalar> SymbolicScalarProcessor { get; }
    }
}