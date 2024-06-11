using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Frames;

public class RGaBasisKVectorFrame<T> :
    IRGaKVectorFrame<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaBasisKVectorFrame<T> Create(RGaProcessor<T> processor, int vSpaceDimensions)
    {
        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        var kVectorArray =
            gaSpaceDimensions
                .GetRange()
                .Select(id => processor.KVectorTerm(
                    id, 
                    processor.ScalarProcessor.OneValue)
                ).ToArray();

        return new RGaBasisKVectorFrame<T>(kVectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaBasisKVectorFrame<T> Create(IEnumerable<RGaKVector<T>> kVectorList)
    {
        var kVectorArray =
            kVectorList.ToArray();

        return new RGaBasisKVectorFrame<T>(kVectorArray);
    }

    internal static RGaBasisKVectorFrame<T> CreateFrom(RGaBasisVectorFrame<T> vectorFrame)
    {
        var metric = vectorFrame.Processor;
        var scalarProcessor = vectorFrame.ScalarProcessor;
        var vSpaceDimensions = vectorFrame.VSpaceDimensions;
        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        var kVectorArray = new RGaKVector<T>[gaSpaceDimensions];

        kVectorArray[0] = metric.Scalar(scalarProcessor.OneValue);

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

        return new RGaBasisKVectorFrame<T>(kVectorArray);
    }


    private readonly IReadOnlyList<RGaKVector<T>> _kVectorArray;

        
    public RGaProcessor<T> Processor 
        => _kVectorArray[0].Processor;

    public RGaMetric Metric 
        => _kVectorArray[0].Metric;
        
    public IScalarProcessor<T> ScalarProcessor
        => _kVectorArray[0].ScalarProcessor;

    public int VSpaceDimensions 
        => _kVectorArray.Max(kv => kv.VSpaceDimensions);

    public int Count
        => _kVectorArray.Count;

    public RGaKVector<T> this[int index]
    {
        get => _kVectorArray[index];
        //set => _kVectorArray[index] = value.KVectorStorage ?? throw new ArgumentNullException(nameof(value));
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaBasisKVectorFrame(IReadOnlyList<RGaKVector<T>> kVectorArray)
    {
        _kVectorArray = kVectorArray;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisKVectorFrame<T> MapAsBasisUsing(Func<RGaKVector<T>, RGaKVector<T>> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(this[index]))
                .ToArray();

        return new RGaBasisKVectorFrame<T>(vectorArray);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _kVectorArray.All(kv => kv.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<RGaKVector<T>> GetEnumerator()
    {
        return _kVectorArray.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}