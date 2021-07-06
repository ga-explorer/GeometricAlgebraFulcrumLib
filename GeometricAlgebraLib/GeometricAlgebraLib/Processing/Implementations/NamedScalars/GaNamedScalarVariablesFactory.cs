using GeometricAlgebraLib.Geometry.Euclidean;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Processing.Implementations.NamedScalars
{
    public sealed class GaNamedScalarVariablesFactory<TScalar> :
        GaNamedScalarsFactory<TScalar>
    {
        internal GaNamedScalarVariablesFactory(GaNamedScalarsCollection<TScalar> namedScalarsCollection)
            : base(namedScalarsCollection)
        {
        }


        public GaEuclideanSimpleRotor<IGaNamedScalar<TScalar>> CreateEuclideanSimpleRotor(IGaVectorStorage<IGaNamedScalar<TScalar>> sourceVector, IGaVectorStorage<IGaNamedScalar<TScalar>> targetVector)
        {
            return GaEuclideanSimpleRotor<IGaNamedScalar<TScalar>>.Create(
                sourceVector,
                targetVector
            );
        }
    }
}