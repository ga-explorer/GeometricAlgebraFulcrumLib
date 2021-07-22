using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Multivectors;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public static class GaGeometryUtils
    {

        public static GaSubspace<T> CreateSubspace<T>(this IGaProcessor<T> processor, IGasKVector<T> blade)
        {
            return GaSubspace<T>.Create(processor, blade);
        }


        public static GaVector<T> CreateVector<T>(this IGaProcessor<T> processor, IGasVector<T> storage)
        {
            return GaVector<T>.Create(processor, storage);
        }

        
        public static IGaOutermorphism<T> CreateComputedOutermorphism<T>(this GaVectorsFrame<T> frame)
        {
            var matrix = frame.GetMatrix();

            return frame.Processor.CreateComputedOutermorphism(matrix);
        }
    }
}
