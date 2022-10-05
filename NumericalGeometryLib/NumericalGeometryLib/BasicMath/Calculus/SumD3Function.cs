using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.BasicMath.Calculus;

public class SumD3Function :
    IScalarD3Function,
    IReadOnlyList<IScalarD3Function>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SumD3Function Create()
    {
        return new SumD3Function();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SumD3Function Create(IScalarD3Function scalarFunction)
    {
        var sum = new SumD3Function
        {
            scalarFunction
        };

        return sum;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SumD3Function Create(IScalarD3Function scalarFunction1, IScalarD3Function scalarFunction2)
    {
        var sum = new SumD3Function
        {
            {scalarFunction1, scalarFunction2}
        };

        return sum;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SumD3Function Create(IScalarD3Function scalarFunction1, IScalarD3Function scalarFunction2, IScalarD3Function scalarFunction3)
    {
        var sum = new SumD3Function
        {
            {scalarFunction1, scalarFunction2, scalarFunction3}
        };

        return sum;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SumD3Function Create(IEnumerable<IScalarD3Function> scalarFunctionList)
    {
        var sum = new SumD3Function
        {
            scalarFunctionList
        };

        return sum;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SumD3Function Create(params IScalarD3Function[] scalarFunctionList)
    {
        var sum = new SumD3Function
        {
            scalarFunctionList
        };

        return sum;
    }


    private readonly List<IScalarD3Function> _functionList
        = new List<IScalarD3Function>();


    public int Count 
        => _functionList.Count;

    public IScalarD3Function this[int index] 
        => _functionList[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SumD3Function Clear()
    {
        _functionList.Clear();

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(IScalarD3Function scalarFunction)
    {
        return _functionList.Remove(scalarFunction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SumD3Function RemoveAt(int index)
    {
        _functionList.RemoveAt(index);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(IScalarD3Function scalarFunction)
    {
        _functionList.Add(scalarFunction);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(IScalarD3Function scalarFunction1, IScalarD3Function scalarFunction2)
    {
        _functionList.Add(scalarFunction1);
        _functionList.Add(scalarFunction2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(IScalarD3Function scalarFunction1, IScalarD3Function scalarFunction2, IScalarD3Function scalarFunction3)
    {
        _functionList.Add(scalarFunction1);
        _functionList.Add(scalarFunction2);
        _functionList.Add(scalarFunction3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(IEnumerable<IScalarD3Function> scalarFunctionList)
    {
        _functionList.AddRange(scalarFunctionList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(params IScalarD3Function[] scalarFunctionList)
    {
        _functionList.AddRange(scalarFunctionList);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(double t)
    {
        return _functionList.Select(f => f.GetValue(t)).Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetFirstDerivative(double t)
    {
        return _functionList.Select(f => f.GetFirstDerivative(t)).Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSecondDerivative(double t)
    {
        return _functionList.Select(f => f.GetSecondDerivative(t)).Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetThirdDerivative(double t)
    {
        return _functionList.Select(f => f.GetThirdDerivative(t)).Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<IScalarD3Function> GetEnumerator()
    {
        return _functionList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}