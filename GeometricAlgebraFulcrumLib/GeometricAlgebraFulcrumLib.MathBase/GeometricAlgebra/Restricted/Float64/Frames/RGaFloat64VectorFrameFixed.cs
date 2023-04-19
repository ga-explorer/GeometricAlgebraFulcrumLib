using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Frames
{
    public sealed class RGaFloat64VectorFrameFixed :
        IRGaFloat64VectorFrame
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64VectorFrameFixed Create(RGaFloat64Vector point, IRGaFloat64VectorFrame vectorFrame)
        {
            return new RGaFloat64VectorFrameFixed(point, vectorFrame);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64VectorFrameFixed Create(RGaFloat64Vector point, RGaFloat64VectorFrameSpecs frameSpecs, IEnumerable<RGaFloat64Vector> vectorStoragesList)
        {
            var vectorFrame = RGaFloat64VectorFrame.Create(frameSpecs, vectorStoragesList);

            return new RGaFloat64VectorFrameFixed(point, vectorFrame);
        }

        
        public RGaFloat64Processor Processor 
            => VectorFrame.Processor;

        public RGaMetric Metric 
            => VectorFrame.Metric;
        
        public RGaFloat64Vector Point { get; }

        public IRGaFloat64VectorFrame VectorFrame { get; }
        
        public int VSpaceDimensions 
            => VectorFrame.VSpaceDimensions;

        public int Count
            => VectorFrame.Count;

        public RGaFloat64Vector this[int index]
            => Point + VectorFrame[index];


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RGaFloat64VectorFrameFixed(RGaFloat64Vector point, IRGaFloat64VectorFrame vectorFrame)
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
        public IEnumerator<RGaFloat64Vector> GetEnumerator()
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