using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.BasicMath.Calculus;

public class SumD2Function :
    IScalarD2Function,
    IReadOnlyList<IScalarD2Function>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SumD2Function Create()
    {
        return new SumD2Function();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SumD2Function Create(IScalarD2Function scalarFunction)
    {
        var sum = new SumD2Function
        {
            scalarFunction
        };

        return sum;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SumD2Function Create(IScalarD2Function scalarFunction1, IScalarD2Function scalarFunction2)
    {
        var sum = new SumD2Function
        {
            {scalarFunction1, scalarFunction2}
        };

        return sum;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SumD2Function Create(IScalarD2Function scalarFunction1, IScalarD2Function scalarFunction2, IScalarD2Function scalarFunction3)
    {
        var sum = new SumD2Function
        {
            {scalarFunction1, scalarFunction2, scalarFunction3}
        };

        return sum;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SumD2Function Create(IEnumerable<IScalarD2Function> scalarFunctionList)
    {
        var sum = new SumD2Function
        {
            scalarFunctionList
        };

        return sum;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SumD2Function Create(params IScalarD2Function[] scalarFunctionList)
    {
        var sum = new SumD2Function
        {
            scalarFunctionList
        };

        return sum;
    }


    private readonly List<IScalarD2Function> _functionList
        = new List<IScalarD2Function>();


    public int Count 
        => _functionList.Count;

    public IScalarD2Function this[int index] 
        => _functionList[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SumD2Function Clear()
    {
        _functionList.Clear();

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(IScalarD2Function scalarFunction)
    {
        return _functionList.Remove(scalarFunction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SumD2Function RemoveAt(int index)
    {
        _functionList.RemoveAt(index);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(IScalarD2Function scalarFunction)
    {
        _functionList.Add(scalarFunction);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(IScalarD2Function scalarFunction1, IScalarD2Function scalarFunction2)
    {
        _functionList.Add(scalarFunction1);
        _functionList.Add(scalarFunction2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(IScalarD2Function scalarFunction1, IScalarD2Function scalarFunction2, IScalarD2Function scalarFunction3)
    {
        _functionList.Add(scalarFunction1);
        _functionList.Add(scalarFunction2);
        _functionList.Add(scalarFunction3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(IEnumerable<IScalarD2Function> scalarFunctionList)
    {
        _functionList.AddRange(scalarFunctionList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(params IScalarD2Function[] scalarFunctionList)
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
    public IEnumerator<IScalarD2Function> GetEnumerator()
    {
        return _functionList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}