using System;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearMaps
{
    public static class GaLinearMapFactory
    {
        public static GaUnilinearMapStored<T> CreateStoredUnilinearMap<T>(this IGaScalarsGridProcessor<T> arrayProcessor, uint vSpaceDimension)
        {
            return new GaUnilinearMapStored<T>(arrayProcessor, vSpaceDimension);
        }

        public static GaUnilinearMapStored<T> CreateStoredUnilinearMap<T>(this IGaScalarsGridProcessor<T> arrayProcessor, uint vSpaceDimension, Func<ulong, IGaStorageMultivector<T>> mappingFunc)
        {
            var linearMap = new GaUnilinearMapStored<T>(arrayProcessor, vSpaceDimension);

            var gaSpaceDimension = vSpaceDimension.ToGaSpaceDimension();

            for (var id = 0UL; id < gaSpaceDimension; id++)
                linearMap[id] = mappingFunc(id);

            return linearMap;
        }
    }
}