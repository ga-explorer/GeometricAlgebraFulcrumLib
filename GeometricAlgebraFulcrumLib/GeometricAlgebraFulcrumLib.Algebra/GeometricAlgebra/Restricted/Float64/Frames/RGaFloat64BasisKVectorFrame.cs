using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Frames;

public class RGaFloat64BasisKVectorFrame :
    IRGaFloat64KVectorFrame
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64BasisKVectorFrame Create(RGaFloat64Processor metric, int vSpaceDimensions)
    {
        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        var kVectorArray =
            gaSpaceDimensions
                .GetRange()
                .Select(id => metric.KVectorTerm(id)
                ).ToArray();

        return new RGaFloat64BasisKVectorFrame(kVectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64BasisKVectorFrame Create(IEnumerable<RGaFloat64KVector> kVectorList)
    {
        var kVectorArray =
            kVectorList.ToArray();

        return new RGaFloat64BasisKVectorFrame(kVectorArray);
    }

    internal static RGaFloat64BasisKVectorFrame CreateFrom(RGaFloat64BasisVectorFrame vectorFrame)
    {
        var metric = vectorFrame.Processor;
        var vSpaceDimensions = vectorFrame.VSpaceDimensions;
        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        var kVectorArray = new RGaFloat64KVector[gaSpaceDimensions];

        kVectorArray[0] = metric.ScalarOne;

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

        return new RGaFloat64BasisKVectorFrame(kVectorArray);
    }


    private readonly IReadOnlyList<RGaFloat64KVector> _kVectorArray;

        
    public RGaFloat64Processor Processor 
        => _kVectorArray[0].Processor;

    public RGaMetric Metric 
        => _kVectorArray[0].Metric;
        
    public int VSpaceDimensions 
        => _kVectorArray.Max(kv => kv.VSpaceDimensions);

    public int Count
        => _kVectorArray.Count;

    public RGaFloat64KVector this[int index]
    {
        get => _kVectorArray[index];
        //set => _kVectorArray[index] = value.KVectorStorage ?? throw new ArgumentNullException(nameof(value));
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaFloat64BasisKVectorFrame(IReadOnlyList<RGaFloat64KVector> kVectorArray)
    {
        _kVectorArray = kVectorArray;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64BasisKVectorFrame MapAsBasisUsing(Func<RGaFloat64KVector, RGaFloat64KVector> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(this[index]))
                .ToArray();

        return new RGaFloat64BasisKVectorFrame(vectorArray);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _kVectorArray.All(kv => kv.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<RGaFloat64KVector> GetEnumerator()
    {
        return _kVectorArray.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}