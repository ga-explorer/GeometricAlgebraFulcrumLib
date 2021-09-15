using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Frames
{
    public sealed class GeoFixedFrame<T> :
        IGeometricAlgebraElement<T>
    {
        public bool IsValid 
            => FreeFrame.IsValid;

        public bool IsInvalid 
            => FreeFrame.IsInvalid;
        
        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => FreeFrame.GeometricProcessor;
        
        public ILinearAlgebraProcessor<T> LinearProcessor 
            => FreeFrame.GeometricProcessor;
        
        public IGeometricAlgebraProcessor<T> GeometricProcessor 
            => FreeFrame.GeometricProcessor;

        public VectorStorage<T> Point { get; }

        public GeoFreeFrame<T> FreeFrame { get; }


        internal GeoFixedFrame([NotNull] VectorStorage<T> point, [NotNull] IGeometricAlgebraProcessor<T> processor, GeoFreeFrameKind frameKind)
        {
            Point = point;
            FreeFrame = new GeoFreeFrame<T>(processor, frameKind);
        }

        internal GeoFixedFrame([NotNull] VectorStorage<T> point, [NotNull] IGeometricAlgebraProcessor<T> processor, GeoFreeFrameKind frameKind, [NotNull] IEnumerable<VectorStorage<T>> vectorStoragesList)
        {
            Point = point;
            FreeFrame = new GeoFreeFrame<T>(processor, frameKind, vectorStoragesList);
        }

        internal GeoFixedFrame([NotNull] VectorStorage<T> point, [NotNull] GeoFreeFrame<T> vectorsFrame)
        {
            Point = point;
            FreeFrame = vectorsFrame;
        }
    }
}