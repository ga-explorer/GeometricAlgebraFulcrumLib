using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps;

public sealed class AxisToAxisRotation
{
    private readonly int _uAxisIndex;
    private readonly bool _uAxisNegative;
    private readonly int _vAxisIndex;
    private readonly bool _vAxisNegative;


    public AxisToAxisRotation(int uAxisIndex, bool uAxisNegative, int vAxisIndex, bool vAxisNegative)
    {
        Debug.Assert(
            uAxisIndex != vAxisIndex
        );

        _uAxisIndex = uAxisIndex;
        _uAxisNegative = uAxisNegative;
        _vAxisIndex = vAxisIndex;
        _vAxisNegative = vAxisNegative;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple Rotate(Float64DenseTuple x)
    {
        var r = _vAxisNegative ? -x[_vAxisIndex] : x[_vAxisIndex];
        var s = _uAxisNegative ? -x[_uAxisIndex] : x[_uAxisIndex];

        var y = new Float64DenseTuple(x);

        if (_uAxisNegative)
            y[_uAxisIndex] += r + s;
        else
            y[_uAxisIndex] -= r + s;

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