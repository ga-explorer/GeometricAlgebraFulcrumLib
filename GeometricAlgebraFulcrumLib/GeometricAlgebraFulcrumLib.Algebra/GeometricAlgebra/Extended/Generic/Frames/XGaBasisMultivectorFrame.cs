using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Frames;

public class XGaBasisMultivectorFrame<T> :
    IXGaMultivectorFrame<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBasisMultivectorFrame<T> Create(XGaProcessor<T> processor, int vSpaceDimensions)
    {
        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        var multivectorArray =
            gaSpaceDimensions
                .GetRange()
                .Select(id => processor.KVectorTerm(
                    id.BitPatternToIndexSet(), 
                    processor.ScalarProcessor.OneValue
                )).ToArray();

        return new XGaBasisMultivectorFrame<T>(multivectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBasisMultivectorFrame<T> Create(IEnumerable<XGaMultivector<T>> multivectorList)
    {
        var multivectorArray =
            multivectorList.ToArray();

        return new XGaBasisMultivectorFrame<T>(multivectorArray);
    }

    internal static XGaBasisMultivectorFrame<T> CreateFrom(XGaBasisVectorFrame<T> vectorFrame)
    {
        var processor = vectorFrame.Processor;
        var scalarProcessor = vectorFrame.ScalarProcessor;
        var vSpaceDimensions = vectorFrame.VSpaceDimensions;

        var multivectorArray = new XGaMultivector<T>[1UL << vSpaceDimensions];

        multivectorArray[0] = processor.Scalar(scalarProcessor.OneValue);

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

        return new XGaBasisMultivectorFrame<T>(multivectorArray);
    }


    private readonly IReadOnlyList<XGaMultivector<T>> _multivectorArray;

        
    public XGaProcessor<T> Processor 
        => _multivectorArray[0].Processor;

    public XGaMetric Metric 
        => _multivectorArray[0].Metric;

    public IScalarProcessor<T> ScalarProcessor
        => _multivectorArray[0].ScalarProcessor;

    public int VSpaceDimensions
        => _multivectorArray.Max(mv => mv.VSpaceDimensions);

    public int Count
        => _multivectorArray.Count;

    public XGaMultivector<T> this[int index]
    {
        get => _multivectorArray[index];
        //set => _multivectorArray[index] = value.MultivectorStorage ?? throw new ArgumentNullException(nameof(value));
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaBasisMultivectorFrame(IReadOnlyList<XGaMultivector<T>> multivectorArray)
    {
        _multivectorArray = multivectorArray;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _multivectorArray.All(mv => mv.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBasisMultivectorFrame<T> MapAsBasisUsing(Func<XGaMultivector<T>, XGaMultivector<T>> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(this[index]))
                .ToArray();

        return new XGaBasisMultivectorFrame<T>(vectorArray);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<XGaMultivector<T>> GetEnumerator()
    {
        return _multivectorArray.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}