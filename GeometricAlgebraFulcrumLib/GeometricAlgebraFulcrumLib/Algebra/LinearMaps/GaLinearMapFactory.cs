using System;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearMaps
{
    public static class GaLinearMapFactory
    {
        public static GaUnilinearMapStored<T> CreateStoredUnilinearMap<T>(this ILaProcessor<T> arrayProcessor, uint vSpaceDimension)
        {
            return new GaUnilinearMapStored<T>(arrayProcessor, vSpaceDimension);
        }

        public static GaUnilinearMapStored<T> CreateStoredUnilinearMap<T>(this ILaProcessor<T> arrayProcessor, uint vSpaceDimension, Func<ulong, IGaMultivectorStorage<T>> mappingFunc)
        {
            var linearMap = new GaUnilinearMapStored<T>(arrayProcessor, vSpaceDimension);

            var gaSpaceDimension = vSpaceDimension.ToGaSpaceDimension();

            for (var id = 0UL; id < gaSpaceDimension; id++)
                linearMap[id] = mappingFunc(id);

            return linearMap;
        }
    }
}