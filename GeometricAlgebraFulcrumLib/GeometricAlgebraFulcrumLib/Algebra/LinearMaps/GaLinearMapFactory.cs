using System;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearMaps
{
    public static class GaLinearMapFactory
    {
        public static GaUnilinearMapStored<T> CreateStoredUnilinearMap<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GaUnilinearMapStored<T>(scalarProcessor, vSpaceDimension);
        }

        public static GaUnilinearMapStored<T> CreateStoredUnilinearMap<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension, Func<ulong, IGaStorageMultivector<T>> mappingFunc)
        {
            var linearMap = new GaUnilinearMapStored<T>(scalarProcessor, vSpaceDimension);

            var gaSpaceDimension = 1UL << (int) vSpaceDimension;

            for (var id = 0UL; id < gaSpaceDimension; id++)
                linearMap[id] = mappingFunc(id);

            return linearMap;
        }
    }
}