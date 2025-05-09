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

public class RGaBasisMultivectorFrame<T> :
    IRGaMultivectorFrame<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaBasisMultivectorFrame<T> Create(RGaProcessor<T> processor, int vSpaceDimensions)
    {
        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        var multivectorArray =
            gaSpaceDimensions
                .GetRange()
                .Select(id => processor.KVectorTerm(
                    id, 
                    processor.ScalarProcessor.OneValue
                )).ToArray();

        return new RGaBasisMultivectorFrame<T>(multivectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaBasisMultivectorFrame<T> Create(IEnumerable<RGaMultivector<T>> multivectorList)
    {
        var multivectorArray =
            multivectorList.ToArray();

        return new RGaBasisMultivectorFrame<T>(multivectorArray);
    }

    internal static RGaBasisMultivectorFrame<T> CreateFrom(RGaBasisVectorFrame<T> vectorFrame)
    {
        var processor = vectorFrame.Processor;
        var vSpaceDimensions = vectorFrame.VSpaceDimensions;

        var multivectorArray = new RGaMultivector<T>[1UL << vSpaceDimensions];

        multivectorArray[0] = processor.Scalar(processor.ScalarProcessor.OneValue);

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

        return new RGaBasisMultivectorFrame<T>(multivectorArray);
    }


    private readonly IReadOnlyList<RGaMultivector<T>> _multivectorArray;

        
    public RGaProcessor<T> Processor 
        => _multivectorArray[0].Processor;

    public RGaMetric Metric 
        => _multivectorArray[0].Metric;

    public IScalarProcessor<T> ScalarProcessor
        => _multivectorArray[0].ScalarProcessor;

    public int VSpaceDimensions
        => _multivectorArray.Max(mv => mv.VSpaceDimensions);

    public int Count
        => _multivectorArray.Count;

    public RGaMultivector<T> this[int index]
    {
        get => _multivectorArray[index];
        //set => _multivectorArray[index] = value.MultivectorStorage ?? throw new ArgumentNullException(nameof(value));
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaBasisMultivectorFrame(IReadOnlyList<RGaMultivector<T>> multivectorArray)
    {
        _multivectorArray = multivectorArray;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _multivectorArray.All(mv => mv.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisMultivectorFrame<T> MapAsBasisUsing(Func<RGaMultivector<T>, RGaMultivector<T>> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(this[index]))
                .ToArray();

        return new RGaBasisMultivectorFrame<T>(vectorArray);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<RGaMultivector<T>> GetEnumerator()
    {
        return _multivectorArray.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}