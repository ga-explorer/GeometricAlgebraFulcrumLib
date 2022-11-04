using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using TextComposerLib.Text;

namespace NumericalGeometryLib.BasicMath.Tuples.Mutable;

public sealed class Float64DenseTuple : 
    IFloat64Tuple
{
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator Tuple2D(DenseTuple tuple)
    //{
    //    return new Tuple2D(
    //        tuple[0],
    //        tuple[1]
    //    );
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator Tuple3D(DenseTuple tuple)
    //{
    //    return new Tuple3D(
    //        tuple[0],
    //        tuple[1],
    //        tuple[2]
    //    );
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator Tuple4D(DenseTuple tuple)
    //{
    //    return new Tuple4D(
    //        tuple[0],
    //        tuple[1],
    //        tuple[2],
    //        tuple[3]
    //    );
    //}
        
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator DenseTuple(Tuple2D tuple)
    //{
    //    return new DenseTuple(
    //        tuple.Item1, 
    //        tuple.Item2
    //    );
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator DenseTuple(Tuple3D tuple)
    //{
    //    return new DenseTuple(
    //        tuple.Item1, 
    //        tuple.Item2, 
    //        tuple.Item3
    //    );
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator DenseTuple(Tuple4D tuple)
    //{
    //    return new DenseTuple(
    //        tuple.Item1, 
    //        tuple.Item2, 
    //        tuple.Item3, 
    //        tuple.Item4
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator -(Float64DenseTuple tuple1)
    {
        var tuple = new Float64DenseTuple(tuple1.Count);

        for (var i = 0; i < tuple._itemArray.Length; i++)
            tuple._itemArray[i] = -tuple._itemArray[i];

        return tuple;
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator +(Float64DenseTuple tuple1, IPair<double> tuple2)
    {
        var tuple = new Float64DenseTuple(tuple1);

        tuple[0] += tuple2.Item1;
        tuple[1] += tuple2.Item2;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator +(IPair<double> tuple1, Float64DenseTuple tuple2)
    {
        var tuple = new Float64DenseTuple(tuple2);

        tuple[0] += tuple1.Item1;
        tuple[1] += tuple1.Item2;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator +(Float64DenseTuple tuple1, ITriplet<double> tuple2)
    {
        var tuple = new Float64DenseTuple(tuple1);

        tuple[0] += tuple2.Item1;
        tuple[1] += tuple2.Item2;
        tuple[2] += tuple2.Item3;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator +(ITriplet<double> tuple1, Float64DenseTuple tuple2)
    {
        var tuple = new Float64DenseTuple(tuple2);

        tuple[0] += tuple1.Item1;
        tuple[1] += tuple1.Item2;
        tuple[2] += tuple1.Item3;

        return tuple;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator +(Float64DenseTuple tuple1, IQuad<double> tuple2)
    {
        var tuple = new Float64DenseTuple(tuple1);

        tuple[0] += tuple2.Item1;
        tuple[1] += tuple2.Item2;
        tuple[2] += tuple2.Item3;
        tuple[3] += tuple2.Item4;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator +(IQuad<double> tuple1, Float64DenseTuple tuple2)
    {
        var tuple = new Float64DenseTuple(tuple2);

        tuple[0] += tuple1.Item1;
        tuple[1] += tuple1.Item2;
        tuple[2] += tuple1.Item3;
        tuple[3] += tuple1.Item4;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator +(Float64DenseTuple tuple1, IQuint<double> tuple2)
    {
        var tuple = new Float64DenseTuple(tuple1);

        tuple[0] += tuple2.Item1;
        tuple[1] += tuple2.Item2;
        tuple[2] += tuple2.Item3;
        tuple[3] += tuple2.Item4;
        tuple[4] += tuple2.Item5;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator +(IQuint<double> tuple1, Float64DenseTuple tuple2)
    {
        var tuple = new Float64DenseTuple(tuple2);

        tuple[0] += tuple1.Item1;
        tuple[1] += tuple1.Item2;
        tuple[2] += tuple1.Item3;
        tuple[3] += tuple1.Item4;
        tuple[4] += tuple1.Item5;

        return tuple;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator +(Float64DenseTuple tuple1, IHexad<double> tuple2)
    {
        var tuple = new Float64DenseTuple(tuple1);

        tuple[0] += tuple2.Item1;
        tuple[1] += tuple2.Item2;
        tuple[2] += tuple2.Item3;
        tuple[3] += tuple2.Item4;
        tuple[4] += tuple2.Item5;
        tuple[5] += tuple2.Item6;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator +(IHexad<double> tuple1, Float64DenseTuple tuple2)
    {
        var tuple = new Float64DenseTuple(tuple2);

        tuple[0] += tuple1.Item1;
        tuple[1] += tuple1.Item2;
        tuple[2] += tuple1.Item3;
        tuple[3] += tuple1.Item4;
        tuple[4] += tuple1.Item5;
        tuple[5] += tuple1.Item6;

        return tuple;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator +(Float64DenseTuple tuple1, Float64DenseTuple tuple2)
    {
        var tuple = new Float64DenseTuple(tuple1);

        return tuple.InPlaceAdd(tuple2);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator -(Float64DenseTuple tuple1, IPair<double> tuple2)
    {
        var tuple = new Float64DenseTuple(tuple1);

        tuple[0] -= tuple2.Item1;
        tuple[1] -= tuple2.Item2;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator -(IPair<double> tuple1, Float64DenseTuple tuple2)
    {
        var tuple = -tuple2;

        tuple[0] += tuple1.Item1;
        tuple[1] += tuple1.Item2;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator -(Float64DenseTuple tuple1, ITriplet<double> tuple2)
    {
        var tuple = new Float64DenseTuple(tuple1);

        tuple[0] -= tuple2.Item1;
        tuple[1] -= tuple2.Item2;
        tuple[2] -= tuple2.Item3;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator -(ITriplet<double> tuple1, Float64DenseTuple tuple2)
    {
        var tuple = -tuple2;

        tuple[0] += tuple1.Item1;
        tuple[1] += tuple1.Item2;
        tuple[2] += tuple1.Item3;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator -(Float64DenseTuple tuple1, IQuad<double> tuple2)
    {
        var tuple = new Float64DenseTuple(tuple1);

        tuple[0] -= tuple2.Item1;
        tuple[1] -= tuple2.Item2;
        tuple[2] -= tuple2.Item3;
        tuple[3] -= tuple2.Item4;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator -(IQuad<double> tuple1, Float64DenseTuple tuple2)
    {
        var tuple = -tuple2;

        tuple[0] += tuple1.Item1;
        tuple[1] += tuple1.Item2;
        tuple[2] += tuple1.Item3;
        tuple[3] += tuple1.Item4;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator -(Float64DenseTuple tuple1, IQuint<double> tuple2)
    {
        var tuple = new Float64DenseTuple(tuple1);

        tuple[0] -= tuple2.Item1;
        tuple[1] -= tuple2.Item2;
        tuple[2] -= tuple2.Item3;
        tuple[3] -= tuple2.Item4;
        tuple[4] -= tuple2.Item5;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator -(IQuint<double> tuple1, Float64DenseTuple tuple2)
    {
        var tuple = -tuple2;

        tuple[0] += tuple1.Item1;
        tuple[1] += tuple1.Item2;
        tuple[2] += tuple1.Item3;
        tuple[3] += tuple1.Item4;
        tuple[4] += tuple1.Item5;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator -(Float64DenseTuple tuple1, IHexad<double> tuple2)
    {
        var tuple = new Float64DenseTuple(tuple1);

        tuple[0] -= tuple2.Item1;
        tuple[1] -= tuple2.Item2;
        tuple[2] -= tuple2.Item3;
        tuple[3] -= tuple2.Item4;
        tuple[4] -= tuple2.Item5;
        tuple[5] -= tuple2.Item6;

        return tuple;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator -(IHexad<double> tuple1, Float64DenseTuple tuple2)
    {
        var tuple = -tuple2;

        tuple[0] += tuple1.Item1;
        tuple[1] += tuple1.Item2;
        tuple[2] += tuple1.Item3;
        tuple[3] += tuple1.Item4;
        tuple[4] += tuple1.Item5;
        tuple[5] += tuple1.Item6;

        return tuple;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator -(Float64DenseTuple tuple1, Float64DenseTuple tuple2)
    {
        var tuple = new Float64DenseTuple(tuple1);

        return tuple.InPlaceSubtract(tuple2);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator *(Float64DenseTuple tuple1, double scalar)
    {
        var tuple = new Float64DenseTuple(tuple1);

        return tuple.InPlaceScale(scalar);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator *(double scalar, Float64DenseTuple tuple1)
    {
        var tuple = new Float64DenseTuple(tuple1);

        return tuple.InPlaceScale(scalar);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64DenseTuple operator /(Float64DenseTuple tuple1, double scalar)
    {
        var tuple = new Float64DenseTuple(tuple1);

        return tuple.InPlaceScale(1d / scalar);
    }


    private readonly double[] _itemArray;


    public int Count 
        => _itemArray.Length;

    public double this[int index]
    {
        get => index >= _itemArray.Length ? 0d : _itemArray[index];
        set
        {
            Debug.Assert(value.IsNotNaN());

            if (index >= _itemArray.Length && value == 0) return;

            _itemArray[index] = value;
        }
    }

    public double X
    {
        get => this[0];
        set => this[0] = value;
    }
        
    public double Y
    {
        get => this[1];
        set => this[1] = value;
    }
        
    public double Z
    {
        get => this[2];
        set => this[2] = value;
    }
        
    public double W
    {
        get => this[3];
        set => this[3] = value;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple(int count)
    {
        if (count < 1)
            throw new ArgumentException(nameof(count));

        _itemArray = new double[count];
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple(double item0)
        : this(1)
    {
        Debug.Assert(item0.IsNotNaN());

        _itemArray[0] = item0;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple(double item0, double item1)
        : this(2)
    {
        Debug.Assert(
            item0.IsNotNaN() &&
            item1.IsNotNaN()
        );

        _itemArray[0] = item0;
        _itemArray[1] = item1;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple(double item0, double item1, double item2)
        : this(3)
    {
        Debug.Assert(
            item0.IsNotNaN() &&
            item1.IsNotNaN() &&
            item2.IsNotNaN()
        );

        _itemArray[0] = item0;
        _itemArray[1] = item1;
        _itemArray[2] = item2;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple(double item0, double item1, double item2, double item3)
        : this(4)
    {
        Debug.Assert(
            item0.IsNotNaN() &&
            item1.IsNotNaN() &&
            item2.IsNotNaN() &&
            item3.IsNotNaN()
        );

        _itemArray[0] = item0;
        _itemArray[1] = item1;
        _itemArray[2] = item2;
        _itemArray[3] = item3;
    }
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public DenseTuple(IEnumerable<KeyValuePair<int, double>> indexItemList)
    //{
    //    foreach (var (index, item) in indexItemList)
    //    {
    //        Debug.Assert(item.IsNotNaN());

    //        _itemArray.Add(index, item);
    //    }
    //}
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple(Float64DenseTuple tuple)
        : this(tuple.Count)
    {
        tuple._itemArray.CopyTo(_itemArray, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Float64DenseTuple(double[] itemArray)
        : this(itemArray.Length)
    {
        Debug.Assert(
            itemArray.All(s => s.IsNotNaN())
        );

        _itemArray = itemArray;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple(IReadOnlyList<double> itemList)
        : this(itemList.Count)
    {
        for (var index = 0; index < itemList.Count; index++)
        {
            Debug.Assert(itemList[index].IsNotNaN());

            _itemArray[index] = itemList[index];
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple(IPair<double> tuple)
        : this(2)
    {
        Debug.Assert(
            tuple.Item1.IsNotNaN() &&
            tuple.Item2.IsNotNaN()
        );

        _itemArray[0] = tuple.Item1;
        _itemArray[1] = tuple.Item2;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple(ITriplet<double> tuple)
        : this(3)
    {
        Debug.Assert(
            tuple.Item1.IsNotNaN() &&
            tuple.Item2.IsNotNaN() &&
            tuple.Item3.IsNotNaN()
        );

        _itemArray[0] = tuple.Item1;
        _itemArray[1] = tuple.Item2;
        _itemArray[2] = tuple.Item3;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple(IQuad<double> tuple)
        : this(4)
    {
        Debug.Assert(
            tuple.Item1.IsNotNaN() &&
            tuple.Item2.IsNotNaN() &&
            tuple.Item3.IsNotNaN() &&
            tuple.Item4.IsNotNaN()
        );

        _itemArray[0] = tuple.Item1;
        _itemArray[1] = tuple.Item2;
        _itemArray[2] = tuple.Item3;
        _itemArray[3] = tuple.Item4;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple(IQuint<double> tuple)
        : this(5)
    {
        Debug.Assert(
            tuple.Item1.IsNotNaN() &&
            tuple.Item2.IsNotNaN() &&
            tuple.Item3.IsNotNaN() &&
            tuple.Item4.IsNotNaN() &&
            tuple.Item5.IsNotNaN()
        );

        _itemArray[0] = tuple.Item1;
        _itemArray[1] = tuple.Item2;
        _itemArray[2] = tuple.Item3;
        _itemArray[3] = tuple.Item4;
        _itemArray[4] = tuple.Item5;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple(IHexad<double> tuple)
        : this(6)
    {
        Debug.Assert(
            tuple.Item1.IsNotNaN() &&
            tuple.Item2.IsNotNaN() &&
            tuple.Item3.IsNotNaN() &&
            tuple.Item4.IsNotNaN() &&
            tuple.Item5.IsNotNaN() &&
            tuple.Item6.IsNotNaN()
        );

        _itemArray[0] = tuple.Item1;
        _itemArray[1] = tuple.Item2;
        _itemArray[2] = tuple.Item3;
        _itemArray[3] = tuple.Item4;
        _itemArray[4] = tuple.Item5;
        _itemArray[5] = tuple.Item6;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple(Color color)
        : this(4)
    {
        var tuple = color.ToPixel<Rgba32>();
            
        _itemArray[0] = tuple.R / 255d;
        _itemArray[1] = tuple.G / 255d;
        _itemArray[2] = tuple.B / 255d;
        _itemArray[3] = tuple.A / 255d;
    }

    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _itemArray.All(s => s.IsNotNaN());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return _itemArray.All(s => s == 0d);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double epsilon = 1e-12)
    {
        return GetLength().IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple Clear()
    {
        for (var i = 0; i < _itemArray.Length; i++)
            _itemArray[0] = 0d;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple2D ToTuple2D()
    {
        return new Tuple2D(
            _itemArray[0], 
            _itemArray[1]
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple3D ToTuple3D()
    {
        return new Tuple3D(
            _itemArray[0], 
            _itemArray[1], 
            _itemArray[2]
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple4D ToTuple4D()
    {
        return new Tuple4D(
            _itemArray[0], 
            _itemArray[1], 
            _itemArray[2], 
            _itemArray[4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color ToColorRgba()
    {
        Debug.Assert(
            _itemArray[0] >= 0d && _itemArray[0] <= 1d &&
            _itemArray[1] >= 0d && _itemArray[1] <= 1d &&
            _itemArray[2] >= 0d && _itemArray[2] <= 1d &&
            _itemArray[3] >= 0d && _itemArray[3] <= 1d
        );

        return Color.FromRgba(
            (byte) (_itemArray[0] * 255),
            (byte) (_itemArray[1] * 255),
            (byte) (_itemArray[2] * 255),
            (byte) (_itemArray[3] * 255)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color ToColorRgb()
    {
        Debug.Assert(
            _itemArray[0] >= 0d && _itemArray[0] <= 1d &&
            _itemArray[1] >= 0d && _itemArray[1] <= 1d &&
            _itemArray[2] >= 0d && _itemArray[2] <= 1d
        );

        return Color.FromRgb(
            (byte) (_itemArray[0] * 255),
            (byte) (_itemArray[1] * 255),
            (byte) (_itemArray[2] * 255)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple InPlaceMap(Func<double, double> scalarMapping)
    {
        for (var i = 0; i < _itemArray.Length; i++)
            this[i] = scalarMapping(_itemArray[i]);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple InPlaceMap(Func<int, double, double> scalarMapping)
    {
        for (var i = 0; i < _itemArray.Length; i++)
            this[i] = scalarMapping(i, _itemArray[i]);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple InPlaceAdd(Float64DenseTuple tuple)
    {
        for (var index = 0; index < tuple._itemArray.Length; index++)
            this[index] += tuple._itemArray[index];

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple InPlaceAdd(Float64SparseTuple tuple)
    {
        foreach (var (index, value) in tuple.IndexItemPairs)
            this[index] += value;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple InPlaceSubtract(Float64DenseTuple tuple)
    {
        for (var index = 0; index < tuple._itemArray.Length; index++)
            this[index] -= tuple._itemArray[index];

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple InPlaceSubtract(Float64SparseTuple tuple)
    {
        foreach (var (index, value) in tuple.IndexItemPairs)
            this[index] -= value;

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple InPlaceNegative()
    {
        for (var index = 0; index < _itemArray.Length; index++)
            _itemArray[index] *= -1d;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple InPlaceScale(double scalar)
    {
        Debug.Assert(scalar.IsNotNaN());

        for (var index = 0; index < _itemArray.Length; index++)
            _itemArray[index] *= scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple InPlaceNormalize()
    {
        var length = GetLength();

        return length == 0 ? this : InPlaceScale(1d / length);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetComponent(int axisIndex, bool axisNegative = false)
    {
        var component = this[axisIndex];

        return axisNegative ? -component : component;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetLengthSquared()
    {
        return _itemArray.Aggregate(
            0d, 
            (length, item) => length + item * item
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetLength()
    {
        return GetLengthSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetDistanceSquaredToPoint(Float64DenseTuple point)
    {
        return (this - point).GetLengthSquared();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetDistanceToPoint(Float64DenseTuple point)
    {
        return (this - point).GetLength();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64DenseTuple ToUnitVector()
    {
        return IsZero() 
            ? new Float64DenseTuple(_itemArray.Length)
            : this / GetLength();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double VectorDot(IFloat64Tuple tuple)
    {
        if (tuple is Float64DenseTuple denseTuple)
            return VectorDot(denseTuple);

        return VectorDot((Float64SparseTuple) tuple);
    }

    public double VectorDot(Float64SparseTuple tuple)
    {
        var dotValue = 0d;

        foreach (var (index, value1) in tuple.IndexItemPairs)
        {
            var value2 = tuple[index];

            dotValue += value1 * value2;
        }

        return dotValue;
    }

    public double VectorDot(Float64DenseTuple tuple)
    {
        var maxIdx = Math.Min(_itemArray.Length, tuple._itemArray.Length);
        var dotValue = 0d;

        for (var i = 0; i < maxIdx; i++)
            dotValue += _itemArray[i] * tuple._itemArray[i];

        return dotValue;
    }


    public IEnumerator<double> GetEnumerator()
    {
        return ((IEnumerable<double>) _itemArray).GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return _itemArray
            .Select(s => s.ToString("G"))
            .Concatenate(", ", "(", ")");
    }
}