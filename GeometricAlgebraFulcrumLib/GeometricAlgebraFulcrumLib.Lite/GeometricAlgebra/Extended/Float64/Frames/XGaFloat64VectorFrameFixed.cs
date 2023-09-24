using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Frames
{
    public sealed class XGaFloat64VectorFrameFixed :
        IXGaFloat64VectorFrame
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static XGaFloat64VectorFrameFixed Create(XGaFloat64Vector point, IXGaFloat64VectorFrame vectorFrame)
        {
            return new XGaFloat64VectorFrameFixed(point, vectorFrame);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static XGaFloat64VectorFrameFixed Create(XGaFloat64Vector point, XGaFloat64VectorFrameSpecs frameSpecs, IEnumerable<XGaFloat64Vector> vectorStoragesList)
        {
            var vectorFrame = XGaFloat64VectorFrame.Create(frameSpecs, vectorStoragesList);

            return new XGaFloat64VectorFrameFixed(point, vectorFrame);
        }

        
        public XGaMetric Metric 
            => VectorFrame.Processor;

        public XGaFloat64Processor Processor 
            => VectorFrame.Processor;
        
        public XGaFloat64Vector Point { get; }

        public IXGaFloat64VectorFrame VectorFrame { get; }
        
        public int VSpaceDimensions 
            => VectorFrame.VSpaceDimensions;

        public int Count
            => VectorFrame.Count;

        public XGaFloat64Vector this[int index]
            => Point + VectorFrame[index];


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private XGaFloat64VectorFrameFixed(XGaFloat64Vector point, IXGaFloat64VectorFrame vectorFrame)
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
        public IEnumerator<XGaFloat64Vector> GetEnumerator()
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
            var composer = new StringBuilder();

            composer
                .AppendLine($"Fixed Frame at {Point} {{");

            foreach (var vector in VectorFrame)
                composer.AppendLine(vector.ToString());

            composer
                .Append("}");

            return composer.ToString();
        }
    }
}