using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace DataStructuresLib.BitManipulation;

public readonly struct TriStateValue :
    IEquatable<TriStateValue>,
    IComparable<TriStateValue>
{
    public static TriStateValue True { get; }
        = new TriStateValue(1);

    public static TriStateValue False { get; }
        = new TriStateValue(-1);

    public static TriStateValue Undecided { get; }
        = new TriStateValue(0);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TriStateValue CreateFromInt32(int valueValue)
    {
        return valueValue switch
        {
            > 0 => True,
            < 0 => False,
            _ => Undecided
        };
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator TriStateValue(int valueValue)
    //{
    //    return valueValue switch
    //    {
    //        > 0 => Positive,
    //        < 0 => Negative,
    //        _ => Zero
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator int(TriStateValue value)
    //{
    //    return value.Value;
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(TriStateValue operand1, TriStateValue operand2)
    {
        return operand1.Value == operand2.Value;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(TriStateValue operand1, TriStateValue operand2)
    {
        return operand1.Value != operand2.Value;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(TriStateValue operand1, TriStateValue operand2)
    {
        return operand1.Value > operand2.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(TriStateValue operand1, TriStateValue operand2)
    {
        return operand1.Value < operand2.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(TriStateValue operand1, TriStateValue operand2)
    {
        return operand1.Value >= operand2.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(TriStateValue operand1, TriStateValue operand2)
    {
        return operand1.Value <= operand2.Value;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TriStateValue operator +(TriStateValue value)
    {
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TriStateValue operator -(TriStateValue value)
    {
        return value.Value switch
        {
            > 0 => False,
            < 0 => True,
            _ => Undecided
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TriStateValue operator +(TriStateValue value1, TriStateValue value2)
    {
        if (value1.IsTrue)
            return value2.IsFalse ? Undecided : True;

        if (value1.IsFalse)
            return value2.IsTrue ? Undecided : False;

        return value2.IsUndecided ? Undecided : value2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TriStateValue operator -(TriStateValue value1, TriStateValue value2)
    {
        if (value1.IsTrue)
            return value2.IsTrue ? Undecided : True;

        if (value1.IsFalse)
            return value2.IsFalse ? Undecided : False;

        return value2.IsUndecided ? Undecided : -value2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short operator *(TriStateValue value, short number)
    {
        if (value.IsUndecided || number == 0)
            return 0;

        return (short)(value.IsTrue ? number : -number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short operator *(short number, TriStateValue value)
    {
        if (value.IsUndecided || number == 0)
            return 0;

        return (short)(value.IsTrue ? number : -number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int operator *(TriStateValue value, int number)
    {
        if (value.IsUndecided || number == 0)
            return 0;

        return value.IsTrue ? number : -number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int operator *(int number, TriStateValue value)
    {
        if (value.IsUndecided || number == 0)
            return 0;

        return value.IsTrue ? number : -number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long operator *(TriStateValue value, long number)
    {
        if (value.IsUndecided || number == 0)
            return 0;

        return value.IsTrue ? number : -number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long operator *(long number, TriStateValue value)
    {
        if (value.IsUndecided || number == 0)
            return 0;

        return value.IsTrue ? number : -number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float operator *(TriStateValue value, float number)
    {
        if (value.IsUndecided || number == 0)
            return 0;

        return value.IsTrue ? number : -number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float operator *(float number, TriStateValue value)
    {
        if (value.IsUndecided || number == 0)
            return 0;

        return value.IsTrue ? number : -number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double operator *(TriStateValue value, double number)
    {
        if (value.IsUndecided || number == 0)
            return 0;

        return value.IsTrue ? number : -number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double operator *(double number, TriStateValue value)
    {
        if (value.IsUndecided || number == 0)
            return 0;

        return value.IsTrue ? number : -number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex operator *(TriStateValue value, Complex number)
    {
        if (value.IsUndecided || number is { Real: 0, Imaginary: 0 })
            return Complex.Zero;

        return value.IsTrue ? number : -number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex operator *(Complex number, TriStateValue value)
    {
        if (value.IsUndecided || number is { Real: 0, Imaginary: 0 })
            return Complex.Zero;

        return value.IsTrue ? number : -number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TriStateValue operator *(TriStateValue value1, TriStateValue value2)
    {
        if (value1.IsUndecided || value2.IsUndecided)
            return Undecided;

        return value1.Value == value2.Value
            ? True
            : False;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TriStateValue operator !(TriStateValue value)
    {
        return value.Value switch
        {
            > 0 => False,
            < 0 => True,
            _ => Undecided
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TriStateValue operator ~(TriStateValue value)
    {
        return value.Value switch
        {
            > 0 => False,
            < 0 => True,
            _ => Undecided
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TriStateValue operator &(TriStateValue value1, TriStateValue value2)
    {
        if (value1.IsFalse || value2.IsFalse)
            return False;

        if (value1.IsTrue && value2.IsTrue)
            return True;

        return Undecided;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TriStateValue operator |(TriStateValue value1, TriStateValue value2)
    {
        if (value1.IsTrue || value2.IsTrue)
            return True;

        if (value1.IsFalse && value2.IsFalse)
            return False;

        return Undecided;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TriStateValue operator ^(TriStateValue value1, TriStateValue value2)
    {
        if (value1.IsUndecided || value2.IsUndecided)
            return Undecided;

        return value1.Value == value2.Value
            ? False
            : True;
    }


    public int Value { get; }

    public bool IsTrue
        => Value > 0;

    public bool IsFalse
        => Value < 0;

    public bool IsTrueOrFalse
        => Value != 0;

    public bool IsUndecided
        => Value == 0;

    public bool IsTrueOrUndecided
        => Value >= 0;

    public bool IsFalseOrUndecided
        => Value <= 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TriStateValue(int value)
    {
        Debug.Assert(value is -1 or 0 or 1);

        Value = value;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int ToInt32()
    {
        return Value switch
        {
            > 0 => 1,
            < 0 => -1,
            _ => 0
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long ToInt64()
    {
        return Value switch
        {
            > 0 => 1L,
            < 0 => -1L,
            _ => 0L
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float ToFloat32()
    {
        return Value switch
        {
            > 0 => 1f,
            < 0 => -1f,
            _ => 0f
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64()
    {
        return Value switch
        {
            > 0 => 1d,
            < 0 => -1d,
            _ => 0d
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriStateValue Power(int powerValue)
    {
        return Value switch
        {
            > 0 => True,
            < 0 => (powerValue & 1) == 0 ? True : False,
            _ => powerValue > 0
                ? Undecided
                : throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(TriStateValue other)
    {
        if (Value < other.Value) return -1;
        if (Value > other.Value) return 1;

        return 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return Value.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(TriStateValue other)
    {
        return Value == other.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj)
    {
        return obj is TriStateValue other && Equals(other);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return Value;
    }
}