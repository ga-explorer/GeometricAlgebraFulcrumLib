using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Frames;

public class XGaBasisVectorFrame<T> :
    IXGaVectorFrame<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBasisVectorFrame<T> Create(XGaProcessor<T> processor, int vSpaceDimensions)
    {
        var vectorArray =
            vSpaceDimensions
                .GetRange()
                .Select(processor.VectorTerm)
                .ToArray();

        return new XGaBasisVectorFrame<T>(vectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBasisVectorFrame<T> Create(IEnumerable<XGaVector<T>> vectorList)
    {
        var vectorArray =
            vectorList.ToArray();

        return new XGaBasisVectorFrame<T>(vectorArray);
    }


    private readonly IReadOnlyList<XGaVector<T>> _vectorList;

        
    public XGaProcessor<T> Processor
        => _vectorList[0].Processor;

    public XGaMetric Metric 
        => _vectorList[0].Metric;

    public IScalarProcessor<T> ScalarProcessor 
        => _vectorList[0].ScalarProcessor;

    public int VSpaceDimensions 
        => _vectorList.Max(v => v.VSpaceDimensions);

    public int Count 
        => _vectorList.Count;

    public XGaVector<T> this[int index]
    {
        get => _vectorList[index];
        //set => _vectorList[index] = value ?? throw new ArgumentNullException(nameof(value));
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaBasisVectorFrame(IReadOnlyList<XGaVector<T>> vectorList)
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
    public XGaBasisVectorFrame<T> GetReciprocalVectorFrame()
    {
        var pseudoScalarInv =
            _vectorList.Op().Inverse();

        var vectorArray = new XGaVector<T>[Count];

        for (var i = 0; i < Count; i++)
        {
            //TODO: This can be made more efficient
            var vectorList = _vectorList.ToList();
            vectorList.RemoveAt(i);

            var b =
                vectorList.Op().Lcp(pseudoScalarInv).GetVectorPart();

            vectorArray[i] = i.IsEven() ? b : b.Negative();
        }

        return new XGaBasisVectorFrame<T>(vectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBasisVectorFrame<T> MapAsBasisUsing(Func<XGaVector<T>, XGaVector<T>> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(this[index]))
                .ToArray();

        return new XGaBasisVectorFrame<T>(vectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBasisVectorFrame<T> MapAsBasisUsing(Func<int, XGaVector<T>, XGaVector<T>> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(index, this[index]))
                .ToArray();

        return new XGaBasisVectorFrame<T>(vectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBasisVectorFrame<T> MapUsing(IXGaOutermorphism<T> om)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => om.OmMap(this[index]))
                .ToArray();

        return new XGaBasisVectorFrame<T>(vectorArray);
    }

    
    /// <summary>
    /// See paper "Calculation of Elements of Spin Groups Using Method
    /// of Averaging in Clifford’s Geometric Algebra"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="targetFrame"></param>
    /// <returns></returns>
    public XGaRotor<T> CreateRotorToFrame(XGaBasisVectorFrame<T> targetFrame)
    {
        var processor = Processor;

        var mvFrame1 = CreateBasisKVectorFrame().MapAsBasisUsing(mv => mv.Inverse());
        var mvFrame2 = targetFrame.CreateBasisKVectorFrame();

        var rotorMultivector =
            mvFrame2
                .Zip(mvFrame1)
                .Select(vectorPair => vectorPair.First.Gp(vectorPair.Second))
                .Aggregate(
                    (XGaMultivector<T>) processor.GradedMultivectorZero,
                    (mv1, mv2) => mv1.Add(mv2)
                );

        rotorMultivector /= rotorMultivector.Sp(rotorMultivector.Reverse()).Sqrt();

        return XGaRotor<T>.Create(rotorMultivector);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBasisKVectorFrame<T> CreateBasisKVectorFrame()
    {
        return XGaBasisKVectorFrame<T>.CreateFrom(this);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBasisMultivectorFrame<T> CreateBasisMultivectorFrame()
    {
        return XGaBasisMultivectorFrame<T>.CreateFrom(this);
    }

        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<XGaVector<T>> GetEnumerator()
    {
        return _vectorList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}