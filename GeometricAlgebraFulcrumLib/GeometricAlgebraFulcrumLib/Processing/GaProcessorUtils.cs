using System.Linq;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing
{
    public static class GaProcessorUtils
    {
        public static T GetAngle<T>(this IGasVector<T> v1, IGasVector<T> v2)
        {
            var scalarProcessor = v1.ScalarProcessor;

            return scalarProcessor.ArcCos(
                scalarProcessor.Divide(
                    v1.ESp(v2),
                    scalarProcessor.Sqrt(
                        scalarProcessor.Times(v1.ESp(), v2.ESp())
                    )
                )
            );
        }


        public static bool IsEuclideanRotor<T>(this IGasMultivector<T> storage)
        {
            if (storage.GetGrades().Any(grade => grade % 2 != 0))
                return false;

            return storage.EGpReverse()
                .Subtract(storage.ScalarProcessor.OneScalar)
                .IsZero();
        }

        public static bool IsSimpleEuclideanRotor<T>(this IGasMultivector<T> storage)
        {
            if (storage.GetGrades().Any(grade => grade != 0 && grade != 2))
                return false;

            return storage.EGpReverse()
                .Subtract(storage.ScalarProcessor.OneScalar)
                .IsZero();
        }
    }
}