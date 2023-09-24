using System.Collections;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Frames
{
    public class RGaFloat64BasisVectorFrame :
        IRGaFloat64VectorFrame
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64BasisVectorFrame Create(RGaFloat64Processor metric, int vSpaceDimensions)
        {
            var vectorArray =
                vSpaceDimensions
                    .GetRange()
                    .Select(index => 
                        metric.CreateTermVector(index)
                    ).ToArray();

            return new RGaFloat64BasisVectorFrame(vectorArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64BasisVectorFrame Create(IEnumerable<RGaFloat64Vector> vectorList)
        {
            var vectorArray =
                vectorList.ToArray();

            return new RGaFloat64BasisVectorFrame(vectorArray);
        }


        private readonly IReadOnlyList<RGaFloat64Vector> _vectorList;

        
        public RGaFloat64Processor Processor 
            => _vectorList[0].Processor;

        public RGaMetric Metric 
            => _vectorList[0].Metric;
        
        public int VSpaceDimensions 
            => _vectorList.Max(v => v.VSpaceDimensions);

        public int Count 
            => _vectorList.Count;

        public RGaFloat64Vector this[int index]
        {
            get => _vectorList[index];
            //set => _vectorList[index] = value ?? throw new ArgumentNullException(nameof(value));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RGaFloat64BasisVectorFrame(IReadOnlyList<RGaFloat64Vector> vectorList)
        {
            _vectorList = vectorList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return _vectorList.All(v => v.IsValid());
        }

        /// <summary>
        /// See "Geometric Algebra for Computer Science" section 3.8
        /// </summary>
        /// <returns></returns>
        public RGaFloat64BasisVectorFrame GetReciprocalVectorFrame()
        {
            var pseudoScalarInv =
                _vectorList.Op().Inverse();

            var vectorArray = new RGaFloat64Vector[Count];

            for (var i = 0; i < Count; i++)
            {
                //TODO: This can be made more efficient
                var vectorList = _vectorList.ToList();
                vectorList.RemoveAt(i);

                var b =
                    vectorList.Op().Lcp(pseudoScalarInv).GetVectorPart();

                vectorArray[i] = i.IsEven() ? b : b.Negative();
            }

            return new RGaFloat64BasisVectorFrame(vectorArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64BasisVectorFrame MapAsBasisUsing(Func<RGaFloat64Vector, RGaFloat64Vector> vectorMapping)
        {
            var vectorArray =
                Count
                    .GetRange()
                    .Select(index => vectorMapping(this[index]))
                    .ToArray();

            return new RGaFloat64BasisVectorFrame(vectorArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64BasisVectorFrame MapAsBasisUsing(Func<int, RGaFloat64Vector, RGaFloat64Vector> vectorMapping)
        {
            var vectorArray =
                Count
                    .GetRange()
                    .Select(index => vectorMapping(index, this[index]))
                    .ToArray();

            return new RGaFloat64BasisVectorFrame(vectorArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64BasisVectorFrame MapUsing(IRGaFloat64Outermorphism om)
        {
            var vectorArray =
                Count
                    .GetRange()
                    .Select(index => om.OmMap(this[index]))
                    .ToArray();

            return new RGaFloat64BasisVectorFrame(vectorArray);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<RGaFloat64Vector> GetEnumerator()
        {
            return _vectorList.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}