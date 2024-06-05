using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Frames;

public class RGaBasisVectorFrame<T> :
    IRGaVectorFrame<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaBasisVectorFrame<T> Create(RGaProcessor<T> processor, int vSpaceDimensions)
    {
        var vectorArray =
            vSpaceDimensions
                .GetRange()
                .Select(processor.VectorTerm)
                .ToArray();

        return new RGaBasisVectorFrame<T>(vectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaBasisVectorFrame<T> Create(IEnumerable<RGaVector<T>> vectorList)
    {
        var vectorArray =
            vectorList.ToArray();

        return new RGaBasisVectorFrame<T>(vectorArray);
    }


    private readonly IReadOnlyList<RGaVector<T>> _vectorList;

        
    public RGaProcessor<T> Processor 
        => _vectorList[0].Processor;

    public RGaMetric Metric 
        => _vectorList[0].Metric;

    public IScalarProcessor<T> ScalarProcessor 
        => _vectorList[0].ScalarProcessor;

    public int VSpaceDimensions 
        => _vectorList.Max(v => v.VSpaceDimensions);

    public int Count 
        => _vectorList.Count;

    public RGaVector<T> this[int index] 
        => _vectorList[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaBasisVectorFrame(IReadOnlyList<RGaVector<T>> vectorList)
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
    public RGaBasisVectorFrame<T> GetReciprocalVectorFrame()
    {
        var pseudoScalarInv =
            _vectorList.Op().Inverse();

        var vectorArray = new RGaVector<T>[Count];

        for (var i = 0; i < Count; i++)
        {
            //TODO: This can be made more efficient
            var vectorList = _vectorList.ToList();
            vectorList.RemoveAt(i);

            var b =
                vectorList.Op().Lcp(pseudoScalarInv).GetVectorPart();

            vectorArray[i] = i.IsEven() ? b : b.Negative();
        }

        return new RGaBasisVectorFrame<T>(vectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisVectorFrame<T> MapAsBasisUsing(Func<RGaVector<T>, RGaVector<T>> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(this[index]))
                .ToArray();

        return new RGaBasisVectorFrame<T>(vectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisVectorFrame<T> MapAsBasisUsing(Func<int, RGaVector<T>, RGaVector<T>> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(index, this[index]))
                .ToArray();

        return new RGaBasisVectorFrame<T>(vectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisVectorFrame<T> MapUsing(IRGaOutermorphism<T> om)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => om.OmMap(this[index]))
                .ToArray();

        return new RGaBasisVectorFrame<T>(vectorArray);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<RGaVector<T>> GetEnumerator()
    {
        return _vectorList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}