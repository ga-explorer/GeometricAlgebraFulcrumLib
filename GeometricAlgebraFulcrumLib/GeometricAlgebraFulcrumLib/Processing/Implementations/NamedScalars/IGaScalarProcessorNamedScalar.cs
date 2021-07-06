using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Processing.Implementations.NamedScalars
{
    public interface IGaScalarProcessorNamedScalar<TScalar>
        : IGaSymbolicScalarProcessor<IGaNamedScalar<TScalar>>
    {
        GaNamedScalarsCollection<TScalar> NamedScalarsCollection { get; }

        IGaSymbolicScalarProcessor<TScalar> SymbolicScalarProcessor { get; }
    }
}