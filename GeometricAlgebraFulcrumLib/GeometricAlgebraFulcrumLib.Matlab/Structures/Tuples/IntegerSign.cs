using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples
{
    public sealed record IntegerSign : 
    IComparable<IntegerSign>
    {
    public static IntegerSign Positive { get; } 
        = new IntegerSign(1);

    public static IntegerSign Negative { get; } 
        = new IntegerSign(-1);

    public static IntegerSign Zero { get; } 
        = new IntegerSign(0);


    
    public static IntegerSign CreateFromInt32(int signValue)
    {
        return signValue switch
        {
            > 0 => Positive,
            < 0 => Negative,
            _ => Zero
        };
    }

    //
    //public static implicit operator IntegerSign(int signValue)
    //{
    //    return signValue switch
    //    {
    //        > 0 => Positive,
    //        < 0 => Negative,
    //        _ => Zero
    //    };
    //}

    //
    //public static implicit operator int(IntegerSign sign)
    //{
    //    return sign.Value;
    //}


    
    public static bool operator >(IntegerSign operand1, IntegerSign operand2)
    {
        return operand1.Value > operand2.Value;
    }

    
    public static bool operator <(IntegerSign operand1, IntegerSign operand2)
    {
        return operand1.Value < operand2.Value;
    }

    
    public static bool operator >=(IntegerSign operand1, IntegerSign operand2)
    {
        return operand1.Value >= operand2.Value;
    }

    
    public static bool operator <=(IntegerSign operand1, IntegerSign operand2)
    {
        return operand1.Value <= operand2.Value;
    }


    
    public static IntegerSign operator +(IntegerSign sign)
    {
        return sign;
    }
    
    
    public static IntegerSign operator -(IntegerSign sign)
    {
        return sign.Value switch
        {
            > 0 => Negative,
            < 0 => Positive,
            _ => Zero
        };
    }
    
    
    public static short operator *(IntegerSign sign, short number)
    {
        if (sign.IsZero || number == 0) 
            return 0;

        return (short)(sign.IsPositive ? number : -number);
    }
    
    
    public static short operator *(short number, IntegerSign sign)
    {
        if (sign.IsZero || number == 0) 
            return 0;

        return (short)(sign.IsPositive ? number : -number);
    }

    
    public static int operator *(IntegerSign sign, int number)
    {
        if (sign.IsZero || number == 0) 
            return 0;

        return sign.IsPositive ? number : -number;
    }
    
    
    public static int operator *(int number, IntegerSign sign)
    {
        if (sign.IsZero || number == 0) 
            return 0;

        return sign.IsPositive ? number : -number;
    }
    
    
    public static long operator *(IntegerSign sign, long number)
    {
        if (sign.IsZero || number == 0) 
            return 0;

        return sign.IsPositive ? number : -number;
    }
    
    
    public static long operator *(long number, IntegerSign sign)
    {
        if (sign.IsZero || number == 0) 
            return 0;

        return sign.IsPositive ? number : -number;
    }

    
    public static float operator *(IntegerSign sign, float number)
    {
        if (sign.IsZero || number == 0) 
            return 0;

        return sign.IsPositive ? number : -number;
    }
    
    
    public static float operator *(float number, IntegerSign sign)
    {
        if (sign.IsZero || number == 0) 
            return 0;

        return sign.IsPositive ? number : -number;
    }

    
    public static double operator *(IntegerSign sign, double number)
    {
        if (sign.IsZero || number == 0) 
            return 0;

        return sign.IsPositive ? number : -number;
    }
    
    
    public static double operator *(double number, IntegerSign sign)
    {
        if (sign.IsZero || number == 0) 
            return 0;

        return sign.IsPositive ? number : -number;
    }
    
    //
    //public static Complex operator *(IntegerSign sign, Complex number)
    //{
    //    if (sign.IsZero || number.IsZero()) 
    //        return Complex.Zero;

    //    return sign.IsPositive ? number : -number;
    //}
    
    //
    //public static Complex operator *(Complex number, IntegerSign sign)
    //{
    //    if (sign.IsZero || number.IsZero()) 
    //        return Complex.Zero;

    //    return sign.IsPositive ? number : -number;
    //}

    
    public static IntegerSign operator *(IntegerSign sign1, IntegerSign sign2)
    {
        if (sign1.IsZero || sign2.IsZero) 
            return Zero;

        return sign1.Value == sign2.Value
            ? Positive 
            : Negative;
    }


    public int Value { get; }

    public bool IsPositive 
        => Value > 0;
    
    public bool IsNegative 
        => Value < 0;

    public bool IsZero 
        => Value == 0;
    
    public bool IsNotPositive 
        => Value <= 0;

    public bool IsNotNegative 
        => Value >= 0;
    
    public bool IsNotZero 
        => Value != 0;

    
    
    private IntegerSign(int value)
    {
        Debug.Assert(value is -1 or 0 or 1);

        Value = value;
    }

    
    
    public int ToInt32()
    {
        return Value switch
        {
            > 0 => 1,
            < 0 => -1,
            _ => 0
        };
    }
    
    
    public long ToInt64()
    {
        return Value switch
        {
            > 0 => 1L,
            < 0 => -1L,
            _ => 0L
        };
    }
    
    
    public float ToFloat32()
    {
        return Value switch
        {
            > 0 => 1f,
            < 0 => -1f,
            _ => 0f
        };
    }

    
    public double ToFloat64()
    {
        return Value switch
        {
            > 0 => 1d,
            < 0 => -1d,
            _ => 0d
        };
    }

    
    public IntegerSign Power(int powerValue)
    {
        return Value switch
        {
            > 0 => Positive,
            < 0 => Int32BitUtils.IsEven(powerValue) ? Positive : Negative,
            _ => powerValue > 0 ? Zero : throw new InvalidOperationException()
        };
    }

    
    public int CompareTo(IntegerSign other)
    {
        if (Value < other.Value) return -1;
        if (Value > other.Value) return 1;
        return 0;
    }
    
    
    public override string ToString()
    {
        return Value.ToString();
    }
    }
}