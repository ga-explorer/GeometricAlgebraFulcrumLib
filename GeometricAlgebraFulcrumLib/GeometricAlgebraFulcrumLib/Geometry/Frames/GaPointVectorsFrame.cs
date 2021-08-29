using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Geometry.Frames
{
    public sealed class GaPointVectorsFrame<T> :
        IGaGeometry<T>
    {
        public bool IsValid 
            => VectorsFrame.IsValid;

        public bool IsInvalid 
            => VectorsFrame.IsInvalid;
        
        public uint VSpaceDimension 
            => VectorsFrame.Processor.VSpaceDimension;

        public ulong GaSpaceDimension 
            => VectorsFrame.Processor.GaSpaceDimension;
        
        public IGaProcessor<T> Processor 
            => VectorsFrame.Processor;

        public IGaVectorStorage<T> Point { get; }

        public GaVectorsFrame<T> VectorsFrame { get; }


        internal GaPointVectorsFrame([NotNull] IGaVectorStorage<T> point, [NotNull] IGaProcessor<T> processor, GaVectorsFrameKind frameKind)
        {
            Point = point;
            VectorsFrame = new GaVectorsFrame<T>(processor, frameKind);
        }

        internal GaPointVectorsFrame([NotNull] IGaVectorStorage<T> point, [NotNull] IGaProcessor<T> processor, GaVectorsFrameKind frameKind, [NotNull] IEnumerable<IGaVectorStorage<T>> vectorStoragesList)
        {
            Point = point;
            VectorsFrame = new GaVectorsFrame<T>(processor, frameKind, vectorStoragesList);
        }

        internal GaPointVectorsFrame([NotNull] IGaVectorStorage<T> point, [NotNull] GaVectorsFrame<T> vectorsFrame)
        {
            Point = point;
            VectorsFrame = vectorsFrame;
        }


    }
}