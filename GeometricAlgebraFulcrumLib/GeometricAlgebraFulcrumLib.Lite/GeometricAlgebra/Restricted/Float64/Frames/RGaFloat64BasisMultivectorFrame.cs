using System.Collections;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Frames
{
    public class RGaFloat64BasisMultivectorFrame :
        IRGaFloat64MultivectorFrame
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64BasisMultivectorFrame Create(RGaFloat64Processor metric, int vSpaceDimensions)
        {
            var gaSpaceDimensions = 1UL << vSpaceDimensions;

            var multivectorArray =
                gaSpaceDimensions
                    .GetRange()
                    .Select(id => metric.CreateTermKVector(id))
                    .ToArray();

            return new RGaFloat64BasisMultivectorFrame(multivectorArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64BasisMultivectorFrame Create(IEnumerable<RGaFloat64Multivector> multivectorList)
        {
            var multivectorArray =
                multivectorList.ToArray();

            return new RGaFloat64BasisMultivectorFrame(multivectorArray);
        }

        internal static RGaFloat64BasisMultivectorFrame CreateFrom(RGaFloat64BasisVectorFrame vectorFrame)
        {
            var metric = vectorFrame.Processor;
            var vSpaceDimensions = vectorFrame.VSpaceDimensions;

            var multivectorArray = new RGaFloat64Multivector[1UL << vSpaceDimensions];

            multivectorArray[0] = metric.CreateOneScalar();

            for (var index = 0; index < vectorFrame.Count; index++)
                multivectorArray[1ul << index] = vectorFrame[index];

            for (var grade = 2; grade <= vSpaceDimensions; grade++)
            {
                var kvSpaceDimensions =
                    vSpaceDimensions.GetBinomialCoefficient(grade);

                for (var index = 0UL; index < kvSpaceDimensions; index++)
                {
                    var id = BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, index);

                    var (basisVectorId, basisBladeId) =
                        id.SplitByLargestBasisVectorId();

                    multivectorArray[id] = 
                        multivectorArray[basisBladeId].Op(
                            multivectorArray[basisVectorId]
                        );
                }
            }

            return new RGaFloat64BasisMultivectorFrame(multivectorArray);
        }


        private readonly IReadOnlyList<RGaFloat64Multivector> _multivectorArray;

        
        public RGaFloat64Processor Processor 
            => _multivectorArray[0].Processor;

        public RGaMetric Metric 
            => _multivectorArray[0].Metric;
        
        public int VSpaceDimensions
            => _multivectorArray.Max(mv => mv.VSpaceDimensions);

        public int Count
            => _multivectorArray.Count;

        public RGaFloat64Multivector this[int index]
        {
            get => _multivectorArray[index];
            //set => _multivectorArray[index] = value.MultivectorStorage ?? throw new ArgumentNullException(nameof(value));
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RGaFloat64BasisMultivectorFrame(IReadOnlyList<RGaFloat64Multivector> multivectorArray)
        {
            _multivectorArray = multivectorArray;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return _multivectorArray.All(mv => mv.IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64BasisMultivectorFrame MapAsBasisUsing(Func<RGaFloat64Multivector, RGaFloat64Multivector> vectorMapping)
        {
            var vectorArray =
                Count
                    .GetRange()
                    .Select(index => vectorMapping(this[index]))
                    .ToArray();

            return new RGaFloat64BasisMultivectorFrame(vectorArray);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<RGaFloat64Multivector> GetEnumerator()
        {
            return _multivectorArray.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}