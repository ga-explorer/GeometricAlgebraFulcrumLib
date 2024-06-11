using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Frames;

public class XGaFloat64BasisMultivectorFrame :
    IXGaFloat64MultivectorFrame
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64BasisMultivectorFrame Create(XGaFloat64Processor metric, int vSpaceDimensions)
    {
        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        var multivectorArray =
            gaSpaceDimensions
                .GetRange()
                .Select(id => metric.KVectorTerm(
                    id.BitPatternToUInt64IndexSet()
                )).ToArray();

        return new XGaFloat64BasisMultivectorFrame(multivectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64BasisMultivectorFrame Create(IEnumerable<XGaFloat64Multivector> multivectorList)
    {
        var multivectorArray =
            multivectorList.ToArray();

        return new XGaFloat64BasisMultivectorFrame(multivectorArray);
    }

    internal static XGaFloat64BasisMultivectorFrame CreateFrom(XGaFloat64BasisVectorFrame vectorFrame)
    {
        var metric = vectorFrame.Processor;
        var vSpaceDimensions = vectorFrame.VSpaceDimensions;

        var multivectorArray = new XGaFloat64Multivector[1UL << vSpaceDimensions];

        multivectorArray[0] = metric.ScalarOne;

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

        return new XGaFloat64BasisMultivectorFrame(multivectorArray);
    }


    private readonly IReadOnlyList<XGaFloat64Multivector> _multivectorArray;

        
    public XGaFloat64Processor Processor 
        => _multivectorArray[0].Processor;

    public XGaMetric Metric 
        => _multivectorArray[0].Metric;

    public int VSpaceDimensions
        => _multivectorArray.Max(mv => mv.VSpaceDimensions);

    public int Count
        => _multivectorArray.Count;

    public XGaFloat64Multivector this[int index] 
        => _multivectorArray[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaFloat64BasisMultivectorFrame(IReadOnlyList<XGaFloat64Multivector> multivectorArray)
    {
        _multivectorArray = multivectorArray;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _multivectorArray.All(mv => mv.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64BasisMultivectorFrame MapAsBasisUsing(Func<XGaFloat64Multivector, XGaFloat64Multivector> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(this[index]))
                .ToArray();

        return new XGaFloat64BasisMultivectorFrame(vectorArray);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<XGaFloat64Multivector> GetEnumerator()
    {
        return _multivectorArray.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}