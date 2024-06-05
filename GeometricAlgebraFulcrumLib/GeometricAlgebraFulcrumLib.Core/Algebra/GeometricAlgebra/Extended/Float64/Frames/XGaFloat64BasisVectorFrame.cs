using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Frames;

public class XGaFloat64BasisVectorFrame :
    IXGaFloat64VectorFrame
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64BasisVectorFrame Create(XGaFloat64Processor metric, int vSpaceDimensions)
    {
        var vectorArray =
            vSpaceDimensions
                .GetRange()
                .Select(index => 
                    metric.VectorTerm(index)
                ).ToArray();

        return new XGaFloat64BasisVectorFrame(vectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64BasisVectorFrame Create(IEnumerable<XGaFloat64Vector> vectorList)
    {
        var vectorArray =
            vectorList.ToArray();

        return new XGaFloat64BasisVectorFrame(vectorArray);
    }


    private readonly IReadOnlyList<XGaFloat64Vector> _vectorList;

        
    public XGaFloat64Processor Processor 
        => _vectorList[0].Processor;

    public XGaMetric Metric 
        => _vectorList[0].Metric;

    public int VSpaceDimensions 
        => _vectorList.Max(v => v.VSpaceDimensions);

    public int Count 
        => _vectorList.Count;

    public XGaFloat64Vector this[int index] 
        => _vectorList[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaFloat64BasisVectorFrame(IReadOnlyList<XGaFloat64Vector> vectorList)
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
    public XGaFloat64BasisVectorFrame GetReciprocalVectorFrame()
    {
        var pseudoScalarInv =
            _vectorList.Op(Processor).Inverse();

        var vectorArray = new XGaFloat64Vector[Count];

        for (var i = 0; i < Count; i++)
        {
            //TODO: This can be made more efficient
            var vectorList = _vectorList.ToList();
            vectorList.RemoveAt(i);

            var b =
                vectorList.Op(Processor).Lcp(pseudoScalarInv).GetVectorPart();

            vectorArray[i] = i.IsEven() ? b : b.Negative();
        }

        return new XGaFloat64BasisVectorFrame(vectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64BasisVectorFrame MapAsBasisUsing(Func<XGaFloat64Vector, XGaFloat64Vector> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(this[index]))
                .ToArray();

        return new XGaFloat64BasisVectorFrame(vectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64BasisVectorFrame MapAsBasisUsing(Func<int, XGaFloat64Vector, XGaFloat64Vector> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(index, this[index]))
                .ToArray();

        return new XGaFloat64BasisVectorFrame(vectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64BasisVectorFrame MapUsing(IXGaFloat64Outermorphism om)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => om.OmMap(this[index]))
                .ToArray();

        return new XGaFloat64BasisVectorFrame(vectorArray);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<XGaFloat64Vector> GetEnumerator()
    {
        return _vectorList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}