using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Frames;

public class XGaBasisKVectorFrame<T> :
    IXGaKVectorFrame<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBasisKVectorFrame<T> Create(XGaProcessor<T> processor, int vSpaceDimensions)
    {
        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        var kVectorArray =
            gaSpaceDimensions
                .GetRange()
                .Select(id => processor.KVectorTerm(
                    id.ToUInt64IndexSet(), 
                    processor.ScalarProcessor.OneValue)
                ).ToArray();

        return new XGaBasisKVectorFrame<T>(kVectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBasisKVectorFrame<T> Create(IEnumerable<XGaKVector<T>> kVectorList)
    {
        var kVectorArray =
            kVectorList.ToArray();

        return new XGaBasisKVectorFrame<T>(kVectorArray);
    }

    internal static XGaBasisKVectorFrame<T> CreateFrom(XGaBasisVectorFrame<T> vectorFrame)
    {
        var processor = vectorFrame.Processor;
        var scalarProcessor = vectorFrame.ScalarProcessor;
        var vSpaceDimensions = vectorFrame.VSpaceDimensions;
        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        var kVectorArray = new XGaKVector<T>[gaSpaceDimensions];

        kVectorArray[0] = processor.Scalar(scalarProcessor.OneValue);

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

                kVectorArray[(ulong)id] = kVectorArray[(ulong)basisBladeId].Op(
                    kVectorArray[(ulong)basisVectorId]
                );
            }
        }

        return new XGaBasisKVectorFrame<T>(kVectorArray);
    }


    private readonly IReadOnlyList<XGaKVector<T>> _kVectorArray;


    public XGaProcessor<T> Processor 
        => _kVectorArray[0].Processor;

    public XGaMetric Metric 
        => _kVectorArray[0].Metric;
        
    public IScalarProcessor<T> ScalarProcessor
        => _kVectorArray[0].ScalarProcessor;

    public int VSpaceDimensions 
        => _kVectorArray.Max(kv => kv.VSpaceDimensions);

    public int Count
        => _kVectorArray.Count;

    public XGaKVector<T> this[int index]
    {
        get => _kVectorArray[index];
        //set => _kVectorArray[index] = value.KVectorStorage ?? throw new ArgumentNullException(nameof(value));
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaBasisKVectorFrame(IReadOnlyList<XGaKVector<T>> kVectorArray)
    {
        _kVectorArray = kVectorArray;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBasisKVectorFrame<T> MapAsBasisUsing(Func<XGaKVector<T>, XGaKVector<T>> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(this[index]))
                .ToArray();

        return new XGaBasisKVectorFrame<T>(vectorArray);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _kVectorArray.All(kv => kv.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<XGaKVector<T>> GetEnumerator()
    {
        return _kVectorArray.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}