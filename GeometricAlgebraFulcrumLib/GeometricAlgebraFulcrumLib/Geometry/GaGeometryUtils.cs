using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Geometry.Metric;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public static class GaGeometryUtils
    {

        public static GaEuclideanSubspace<T> CreateEuclideanSubspace<T>(this IGaKVectorStorage<T> storage)
        {
            return GaEuclideanSubspace<T>.Create(storage);
        }

        public static GaMetricSubspace<T> CreateMetricSubspace<T>(this IGaKVectorStorage<T> storage, IGaMultivectorProcessor<T> processor)
        {
            return GaMetricSubspace<T>.Create(processor, storage);
        }


        public static GaEuclideanVector<T> CreateEuclideanVector<T>(this IGaVectorStorage<T> storage)
        {
            return GaEuclideanVector<T>.Create(storage);
        }

        public static GaVectorsLinearMap<T> CreateVectorsLinearMap<T>(this T[,] matrix, IGaScalarProcessor<T> scalarProcessor)
        {
            return GaVectorsLinearMap<T>.Create(scalarProcessor, matrix);
        }

        public static GaVectorsLinearMap<T> CreateVectorsLinearMap<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix)
        {
            return GaVectorsLinearMap<T>.Create(scalarProcessor, matrix);
        }

        public static GaVectorsLinearMap<T> CreateVectorsLinearMap<T>(this GaEuclideanVectorsFrame<T> frame)
        {
            return GaVectorsLinearMap<T>.Create(frame.ScalarProcessor, frame.GetMatrix());
        }
    }
}
