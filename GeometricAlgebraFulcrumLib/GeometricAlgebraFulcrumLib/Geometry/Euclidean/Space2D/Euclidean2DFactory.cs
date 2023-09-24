using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space2D.Objects;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space2D
{
    public static class Euclidean2DFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> CreateE2DVector<T>(this IScalarProcessor<T> scalarProcessor, T x, T y, bool assumeUnitVector = false)
        {
            return new E2DVector<T>(scalarProcessor, x, y, assumeUnitVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> CreateE2DVectorZero<T>(this IScalarProcessor<T> scalarProcessor)
        {
            var zero = scalarProcessor.ScalarZero;

            return new E2DVector<T>(scalarProcessor, zero, zero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DPoint<T> CreateE2DPoint<T>(this IScalarProcessor<T> scalarProcessor, T x, T y)
        {
            return new E2DPoint<T>(scalarProcessor, x, y);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DPoint<T> CreateE2DPointZero<T>(this IScalarProcessor<T> scalarProcessor)
        {
            var zero = scalarProcessor.ScalarZero;

            return new E2DPoint<T>(scalarProcessor, zero, zero);
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DLineTangent<T> CreateE2DLineTangent<T>(this IScalarProcessor<T> scalarProcessor, E2DPoint<T> origin, E2DVector<T> direction)
        {
            return new E2DLineTangent<T>(origin, direction);
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DLineSegment<T> CreateE2DLineSegment<T>(this IScalarProcessor<T> scalarProcessor, E2DPoint<T> point1, E2DPoint<T> point2)
        {
            return new E2DLineSegment<T>(point1, point2);
        }
    
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DPlaneSegment<T> CreateE2DPlaneSegment<T>(this IScalarProcessor<T> scalarProcessor, E2DPoint<T> point1, E2DPoint<T> point2, E2DPoint<T> point3)
        {
            return new E2DPlaneSegment<T>(point1, point2, point3);
        }
    }
}