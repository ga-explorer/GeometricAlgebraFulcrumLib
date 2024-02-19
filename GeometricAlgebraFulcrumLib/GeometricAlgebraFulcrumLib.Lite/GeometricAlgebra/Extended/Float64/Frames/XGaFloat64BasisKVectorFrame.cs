using System.Collections;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Frames;

public class XGaFloat64BasisKVectorFrame :
    IXGaFloat64KVectorFrame
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64BasisKVectorFrame Create(XGaFloat64Processor metric, int vSpaceDimensions)
    {
        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        var kVectorArray =
            gaSpaceDimensions
                .GetRange()
                .Select(id => metric.CreateTermKVector(id.BitPatternToUInt64IndexSet())
                ).ToArray();

        return new XGaFloat64BasisKVectorFrame(kVectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64BasisKVectorFrame Create(IEnumerable<XGaFloat64KVector> kVectorList)
    {
        var kVectorArray =
            kVectorList.ToArray();

        return new XGaFloat64BasisKVectorFrame(kVectorArray);
    }

    internal static XGaFloat64BasisKVectorFrame CreateFrom(XGaFloat64BasisVectorFrame vectorFrame)
    {
        var metric = vectorFrame.Processor;
        var vSpaceDimensions = vectorFrame.VSpaceDimensions;
        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        var kVectorArray = new XGaFloat64KVector[gaSpaceDimensions];

        kVectorArray[0] = metric.CreateOneScalar();

        for (var index = 0; index < vectorFrame.Count; index++)
            kVectorArray[1ul << index] = vectorFrame[index];

        for (var grade = 2; grade <= vSpaceDimensions; grade++)
        {
            var kvSpaceDimensions =
                vSpaceDimensions.GetBinomialCoefficient(grade);

            for (var index = 0UL; index < kvSpaceDimensions; index++)
            {
                var id = BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, index);

                var (basisVectorId, basisBladeId) =
                    id.SplitByLargestBasisVectorId();

                kVectorArray[id] = kVectorArray[basisBladeId].Op(
                    kVectorArray[basisVectorId]
                );
            }
        }

        return new XGaFloat64BasisKVectorFrame(kVectorArray);
    }


    private readonly IReadOnlyList<XGaFloat64KVector> _kVectorArray;


    public XGaFloat64Processor Processor 
        => _kVectorArray[0].Processor;
        
    public XGaMetric Metric 
        => _kVectorArray[0].Metric;

    public int VSpaceDimensions 
        => _kVectorArray.Max(kv => kv.VSpaceDimensions);

    public int Count
        => _kVectorArray.Count;

    public XGaFloat64KVector this[int index] 
        => _kVectorArray[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaFloat64BasisKVectorFrame(IReadOnlyList<XGaFloat64KVector> kVectorArray)
    {
        _kVectorArray = kVectorArray;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64BasisKVectorFrame MapAsBasisUsing(Func<XGaFloat64KVector, XGaFloat64KVector> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(this[index]))
                .ToArray();

        return new XGaFloat64BasisKVectorFrame(vectorArray);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _kVectorArray.All(kv => kv.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<XGaFloat64KVector> GetEnumerator()
    {
        return _kVectorArray.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}