using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps;

public sealed class AxisToVectorRotation
{
    private readonly int _uAxisIndex;
    private readonly bool _uAxisNegative;
    private readonly Float64DenseTuple _vVector;
    private readonly Float64DenseTuple _vuProjVector;
    private readonly double _vuDotPlus1Inv;


    public AxisToVectorRotation(int uAxisIndex, bool uAxisNegative, Float64DenseTuple v)
    {
        Debug.Assert(
            v.GetLengthSquared().IsNearOne()
        );

        var vuDot = 
            v.GetComponent(uAxisIndex, uAxisNegative);

        Debug.Assert(
            !(vuDot + 1d).IsNearZero()
        );

        _uAxisIndex = uAxisIndex;
        _uAxisNegative = uAxisNegative;
        _vVector = v;

        _vuDotPlus1Inv = 1d / (1d + vuDot);
        _vuProjVector = new Float64DenseTuple(v);

        if (uAxisNegative)
            _vuProjVector[_uAxisIndex] += vuDot;
        else
            _vuProjVector[_uAxisIndex] -= vuDot;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple Rotate(Float64DenseTuple x)
    {
        //var r = x.VectorDot(_vuProjVector) * _vuDotPlus1Inv;
        //var s = x.VectorDot(_uVector);
            
        //return x - (r + s) * _uVector - (r - s) * _vVector;

        var r = x.VectorDot(_vuProjVector) * _vuDotPlus1Inv;
        var s = x.GetComponent(_uAxisIndex, _uAxisNegative);
        
        var y = 
            x - (r - s) * _vVector;

        if (_uAxisNegative)
            y[_uAxisIndex] += r + s;
        else
            y[_uAxisIndex] -= r + s;

        return y;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<Float64DenseTuple> Rotate(params Float64DenseTuple[] xList)
    {
        return xList.Select(Rotate).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64DenseTuple> Rotate(IEnumerable<Float64DenseTuple> xList)
    {
        return xList.Select(Rotate);
    }
}