using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Geometry.Frames;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaFramesUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVectors(this GaVectorsFrameKind frameKind)
        {
            return frameKind is 
                GaVectorsFrameKind.UnitVectors or 
                GaVectorsFrameKind.OrthogonalUnitVectors;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrthogonal(this GaVectorsFrameKind frameKind)
        {
            return frameKind is 
                GaVectorsFrameKind.Orthogonal or 
                GaVectorsFrameKind.OrthogonalUnitVectors;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaPointVectorsFrame<T> CreatePointVectorsFrame<T>(this GaVectorsFrame<T> frame, IGaVectorStorage<T> point)
        {
            return new GaPointVectorsFrame<T>(point, frame);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorsFrame<T> CreateVectorsFrame<T>(this IGaProcessor<T> processor, GaVectorsFrameKind frameKind)
        {
            return new GaVectorsFrame<T>(processor, frameKind);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorsFrame<T> CreateVectorsFrame<T>(this IGaProcessor<T> processor, GaVectorsFrameKind frameKind, IGaVectorStorage<T> vector1, IGaVectorStorage<T> vector2)
        {
            return new GaVectorsFrame<T>(
                processor, 
                frameKind, 
                new []{ vector1, vector2 }
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorsFrame<T> CreateVectorsFrame<T>(this IGaProcessor<T> processor, GaVectorsFrameKind frameKind, params IGaVectorStorage<T>[] vectorsList)
        {
            return new GaVectorsFrame<T>(processor, frameKind, vectorsList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorsFrame<T> CreateVectorsFrame<T>(this IGaProcessor<T> processor, GaVectorsFrameKind frameKind, IEnumerable<IGaVectorStorage<T>> vectorsList)
        {
            return new GaVectorsFrame<T>(processor, frameKind, vectorsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorsFrame<T> CreateBasisVectorsFrame<T>(this IGaProcessor<T> processor, uint vSpaceDimension)
        {
            var vectorsList = 
                vSpaceDimension
                    .GetRange()
                    .Select(index => processor.CreateGaVectorStorage(index));

            return new GaVectorsFrame<T>(
                processor, 
                GaVectorsFrameKind.OrthogonalUnitVectors, 
                vectorsList
            );
        }

        

    }
}