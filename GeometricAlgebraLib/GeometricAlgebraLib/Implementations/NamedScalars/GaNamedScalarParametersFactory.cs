using System.Linq;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Implementations.NamedScalars
{
    public sealed class GaNamedScalarParametersFactory<TScalar> :
        GaNamedScalarsFactory<TScalar>
    {
        internal GaNamedScalarParametersFactory(GaNamedScalarsCollection<TScalar> namedScalarsCollection)
            : base(namedScalarsCollection)
        {
        }


        public GaScalarTermStorage<IGaNamedScalar<TScalar>> CreateScalarTerm(string scalarName)
        {
            var namedScalar = 
                NamedScalarsCollection.GetParameterByName(scalarName);

            return GaScalarTermStorage<IGaNamedScalar<TScalar>>.Create(
                NamedScalarProcessor,
                namedScalar
            );
        }

        public GaVectorStorage<IGaNamedScalar<TScalar>> CreateVector(params string[] scalarNames)
        {
            return GaVectorStorage<IGaNamedScalar<TScalar>>.Create(
                NamedScalarProcessor,
                scalarNames
                    .Select(NamedScalarsCollection.GetParameterByName)
                    .Cast<IGaNamedScalar<TScalar>>()
                    .ToArray()
            );
        }

        public GaVectorTermStorage<IGaNamedScalar<TScalar>> CreateVectorTerm(ulong index, string scalarName)
        {
            var namedScalar = 
                NamedScalarsCollection.GetParameterByName(scalarName);

            return GaVectorTermStorage<IGaNamedScalar<TScalar>>.Create(
                NamedScalarProcessor,
                index,
                namedScalar
            );
        }

        //TODO: Add more functions for constructing multivectors
    }
}