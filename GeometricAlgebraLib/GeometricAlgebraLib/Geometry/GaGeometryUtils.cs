using GeometricAlgebraLib.Geometry.Euclidean;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Geometry
{
    public static class GaGeometryUtils
    {
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
