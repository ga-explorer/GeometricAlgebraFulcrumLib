using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Geometry.Frames
{
    public sealed class GeoFixedFrame<T> : 
        IReadOnlyList<VectorStorage<T>>,
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

        public int Count 
            => FreeFrame.Count;

        public VectorStorage<T> this[int index] 
            => ScalarProcessor.Add(Point, FreeFrame[index]);


        internal GeoFixedFrame([NotNull] VectorStorage<T> point, [NotNull] IGeometricAlgebraProcessor<T> processor, GeoFreeFrameSpecs frameSpecs)
        {
            Point = point;
            FreeFrame = new GeoFreeFrame<T>(processor, frameSpecs);
        }

        internal GeoFixedFrame([NotNull] VectorStorage<T> point, [NotNull] IGeometricAlgebraProcessor<T> processor, GeoFreeFrameSpecs frameSpecs, [NotNull] IEnumerable<VectorStorage<T>> vectorStoragesList)
        {
            Point = point;
            FreeFrame = new GeoFreeFrame<T>(processor, frameSpecs, vectorStoragesList);
        }

        internal GeoFixedFrame([NotNull] VectorStorage<T> point, [NotNull] GeoFreeFrame<T> vectorsFrame)
        {
            Point = point;
            FreeFrame = vectorsFrame;
        }

        public IEnumerator<VectorStorage<T>> GetEnumerator()
        {
            return FreeFrame
                .Select(v => ScalarProcessor.Add(Point, v))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
        public override string ToString()
        {
            var composer = new LinearTextComposer();

            composer
                .AppendLine($"Fixed Frame at {Point} {{")
                .IncreaseIndentation();

            foreach (var vector in FreeFrame)
                composer.AppendAtNewLine(vector.ToString());

            composer
                .DecreaseIndentation()
                .AppendAtNewLine("}");

            return composer.ToString();
        }
    }
}