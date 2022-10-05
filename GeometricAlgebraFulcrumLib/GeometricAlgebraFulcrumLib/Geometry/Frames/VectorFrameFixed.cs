using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Geometry.Frames
{
    public sealed class VectorFrameFixed<T> : 
        IVectorFrame<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static VectorFrameFixed<T> Create(GaVector<T> point, IVectorFrame<T> vectorFrame)
        {
            return new VectorFrameFixed<T>(point, vectorFrame);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static VectorFrameFixed<T> Create(GaVector<T> point, VectorFrameSpecs frameSpecs)
        {
            var vectorFrame = VectorFrame<T>.Create(point.GeometricProcessor, frameSpecs);

            return new VectorFrameFixed<T>(point, vectorFrame);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static VectorFrameFixed<T> Create(GaVector<T> point, VectorFrameSpecs frameSpecs, IEnumerable<GaVector<T>> vectorStoragesList)
        {
            var vectorFrame = VectorFrame<T>.Create(point.GeometricProcessor, frameSpecs, vectorStoragesList);

            return new VectorFrameFixed<T>(point, vectorFrame);
        }
        

        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => VectorFrame.GeometricProcessor;
        
        public ILinearAlgebraProcessor<T> LinearProcessor 
            => VectorFrame.GeometricProcessor;
        
        public IGeometricAlgebraProcessor<T> GeometricProcessor 
            => VectorFrame.GeometricProcessor;

        public GaVector<T> Point { get; }

        public IVectorFrame<T> VectorFrame { get; }

        public int Count 
            => VectorFrame.Count;

        public GaVector<T> this[int index] 
            => Point + VectorFrame[index];


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private VectorFrameFixed([NotNull] GaVector<T> point, [NotNull] IVectorFrame<T> vectorFrame)
        {
            Point = point;
            VectorFrame = vectorFrame;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return VectorFrame.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<GaVector<T>> GetEnumerator()
        {
            return VectorFrame
                .Select(v => Point + v)
                .GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

            foreach (var vector in VectorFrame)
                composer.AppendAtNewLine(vector.ToString());

            composer
                .DecreaseIndentation()
                .AppendAtNewLine("}");

            return composer.ToString();
        }
    }
}