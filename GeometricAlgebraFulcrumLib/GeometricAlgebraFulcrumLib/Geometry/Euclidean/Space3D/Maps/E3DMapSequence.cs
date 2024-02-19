using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Maps;

public sealed class E3DMapSequence<T> :
    E3DMap<T>
{
    private readonly List<E3DMap<T>> _mapList
        = new List<E3DMap<T>>();


    public override IScalarProcessor<T> ScalarProcessor { get; }

    public int Count 
        => _mapList.Count;

    public IEnumerable<E3DMap<T>> Maps 
        => _mapList;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal E3DMapSequence(IScalarProcessor<T> scalarProcessor)
    {
        ScalarProcessor = scalarProcessor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DMapSequence<T> Clear()
    {
        _mapList.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DMapSequence<T> Append(E3DMap<T> map)
    {
        _mapList.Add(map);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DMapSequence<T> Prepend(E3DMap<T> map)
    {
        _mapList.Insert(0, map);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DMapSequence<T> Insert(int index, E3DMap<T> map)
    {
        _mapList.Insert(index, map);

        return this;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DMapSequence<T> Remove(int index)
    {
        _mapList.RemoveAt(index);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DVector<T> Map(E3DVector<T> vector)
    {
        if (_mapList.Count == 0)
            return vector;

        return _mapList.Aggregate(
            vector, 
            (current, map) => map.Map(current)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DPoint<T> Map(E3DPoint<T> point)
    {
        if (_mapList.Count == 0)
            return point;

        return _mapList.Aggregate(
            point, 
            (current, map) => map.Map(current)
        );
    }

    public override E3DMap<T> GetInverse()
    {
        var map = new E3DMapSequence<T>(ScalarProcessor);

        foreach (var subMap in Maps.Reverse())
            map.Append(subMap.GetInverse());

        return map;
    }
}