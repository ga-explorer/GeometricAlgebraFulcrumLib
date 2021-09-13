using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Geometry.Frames;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GeoFrameUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVectors(this GeoFreeFrameKind frameKind)
        {
            return frameKind is 
                GeoFreeFrameKind.UnitVectors or 
                GeoFreeFrameKind.OrthogonalUnitVectors;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrthogonal(this GeoFreeFrameKind frameKind)
        {
            return frameKind is 
                GeoFreeFrameKind.Orthogonal or 
                GeoFreeFrameKind.OrthogonalUnitVectors;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFixedFrame<T> CreatePointFreeFrame<T>(this GeoFreeFrame<T> frame, VectorStorage<T> point)
        {
            return new GeoFixedFrame<T>(point, frame);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> CreateFreeFrame<T>(this IGeometricAlgebraProcessor<T> processor, GeoFreeFrameKind frameKind)
        {
            return new GeoFreeFrame<T>(processor, frameKind);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> CreateFreeFrame<T>(this IGeometricAlgebraProcessor<T> processor, GeoFreeFrameKind frameKind, VectorStorage<T> vector1, VectorStorage<T> vector2)
        {
            return new GeoFreeFrame<T>(
                processor, 
                frameKind, 
                new []{ vector1, vector2 }
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> CreateFreeFrame<T>(this IGeometricAlgebraProcessor<T> processor, GeoFreeFrameKind frameKind, params VectorStorage<T>[] vectorsList)
        {
            return new GeoFreeFrame<T>(processor, frameKind, vectorsList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> CreateFreeFrame<T>(this IGeometricAlgebraProcessor<T> processor, GeoFreeFrameKind frameKind, IEnumerable<VectorStorage<T>> vectorsList)
        {
            return new GeoFreeFrame<T>(processor, frameKind, vectorsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> CreateBasisFreeFrame<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            var vectorsList = 
                vSpaceDimension
                    .GetRange()
                    .Select(index => processor.CreateVectorBasisStorage(index));

            return new GeoFreeFrame<T>(
                processor, 
                GeoFreeFrameKind.OrthogonalUnitVectors, 
                vectorsList
            );
        }

        

    }
}