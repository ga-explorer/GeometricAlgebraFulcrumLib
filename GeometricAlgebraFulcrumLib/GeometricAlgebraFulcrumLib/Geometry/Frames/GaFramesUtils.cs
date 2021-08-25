using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Geometry.Frames
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
        public static GaPointVectorsFrame<T> CreatePointVectorsFrame<T>(this GaVectorsFrame<T> frame, IGaStorageVector<T> point)
        {
            return new GaPointVectorsFrame<T>(point, frame);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorsFrame<T> CreateVectorsFrame<T>(this IGaProcessor<T> processor, GaVectorsFrameKind frameKind)
        {
            return new GaVectorsFrame<T>(processor, frameKind);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorsFrame<T> CreateVectorsFrame<T>(this IGaProcessor<T> processor, GaVectorsFrameKind frameKind, IGaStorageVector<T> vector1, IGaStorageVector<T> vector2)
        {
            return new GaVectorsFrame<T>(
                processor, 
                frameKind, 
                new []{ vector1, vector2 }
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorsFrame<T> CreateVectorsFrame<T>(this IGaProcessor<T> processor, GaVectorsFrameKind frameKind, params IGaStorageVector<T>[] vectorsList)
        {
            return new GaVectorsFrame<T>(processor, frameKind, vectorsList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorsFrame<T> CreateVectorsFrame<T>(this IGaProcessor<T> processor, GaVectorsFrameKind frameKind, IEnumerable<IGaStorageVector<T>> vectorsList)
        {
            return new GaVectorsFrame<T>(processor, frameKind, vectorsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorsFrame<T> CreateBasisVectorsFrame<T>(this IGaProcessor<T> processor, uint vSpaceDimension)
        {
            var vectorsList = 
                vSpaceDimension
                    .GetRange()
                    .Select(index => processor.CreateStorageBasisVector(index));

            return new GaVectorsFrame<T>(
                processor, 
                GaVectorsFrameKind.OrthogonalUnitVectors, 
                vectorsList
            );
        }

        

    }
}