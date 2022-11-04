using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps;

public sealed class VectorToAxisRotation
{
    private readonly Float64DenseTuple _uVector;
    private readonly int _vAxisIndex;
    private readonly bool _vAxisNegative;
    private readonly Float64DenseTuple _vuProjVector;
    private readonly double _vuDotPlus1Inv;


    public VectorToAxisRotation(Float64DenseTuple u, int vAxisIndex, bool vAxisNegative)
    {
        Debug.Assert(
            u.GetLengthSquared().IsNearOne()
        );

        var vuDot = u.GetComponent(vAxisIndex, vAxisNegative);

        Debug.Assert(
            !(vuDot + 1d).IsNearZero()
        );

        _uVector = u;
        _vAxisIndex = vAxisIndex;
        _vAxisNegative = vAxisNegative;

        _vuDotPlus1Inv = 1d / (1d + vuDot);
        _vuProjVector = -vuDot * u;

        if (vAxisNegative)
            _vuProjVector[_vAxisIndex] -= 1d;
        else
            _vuProjVector[_vAxisIndex] += 1d;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple Rotate(Float64DenseTuple x)
    {
        //var r = x.VectorDot(_vuProjVector) * _vuDotPlus1Inv;
        //var s = x.VectorDot(_uVector);
            
        //return x - (r + s) * _uVector - (r - s) * _vVector;

        var r = x.VectorDot(_vuProjVector) * _vuDotPlus1Inv;
        var s = x.VectorDot(_uVector);
            
        var y = 
            x - (r + s) * _uVector;

        if (_vAxisNegative)
            y[_vAxisIndex] += r - s;
        else
            y[_vAxisIndex] -= r - s;

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