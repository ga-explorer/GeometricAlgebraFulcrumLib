using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;

// ReSharper disable ArrangeObjectCreationWhenTypeEvident
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Float64Scalar : 
    IEquatable<double>,
    IComparable<double>,
    IConvertible,
    IBinaryFloatingPointIeee754<Float64Scalar>,
    IMinMaxValue<Float64Scalar>,
    IGeometricElement
{
    public static Float64Scalar Zero { get; } 
        = new Float64Scalar();

    public static Float64Scalar One { get; } 
        = new Float64Scalar(1d);
    
    public static Float64Scalar NegativeOne { get; } 
        = new Float64Scalar(-1d);
    
    public static Float64Scalar Sqrt2 { get; } 
        = new Float64Scalar(Math.Sqrt(2));

    public static Float64Scalar Sqrt3 { get; } 
        = new Float64Scalar(Math.Sqrt(3));
    
    public static Float64Scalar InvSqrt2 { get; } 
        = new Float64Scalar(1d / Math.Sqrt(2));

    public static Float64Scalar InvSqrt3 { get; } 
        = new Float64Scalar(1d / Math.Sqrt(3));

    public static Float64Scalar E { get; } 
        = new Float64Scalar(double.E);
    
    public static Float64Scalar Pi { get; } 
        = new Float64Scalar(double.Pi);
    
    public static Float64Scalar Tau { get; } 
        = new Float64Scalar(double.Tau);
    
    public static Float64Scalar Epsilon { get; }
        = new Float64Scalar(double.Epsilon);

    static Float64Scalar IFloatingPointIeee754<Float64Scalar>.NaN
        => throw new NotImplementedException();

    public static Float64Scalar NegativeInfinity { get; }
        = new Float64Scalar(double.NegativeInfinity);
    
    public static Float64Scalar NegativeZero { get; }
        = new Float64Scalar(double.NegativeZero);
    
    public static Float64Scalar PositiveInfinity { get; }
        = new Float64Scalar(double.PositiveInfinity);
    
    public static Float64Scalar MaxValue { get; }
        = new Float64Scalar(double.MaxValue);
    
    public static Float64Scalar MinValue { get; }
        = new Float64Scalar(double.MinValue);
    
    public static int Radix 
        => 2;

    public static Float64Scalar AdditiveIdentity 
        => Zero;

    public static Float64Scalar MultiplicativeIdentity 
        => One;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float64Scalar(byte value)
    {
        return new Float64Scalar(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float64Scalar(int value)
    {
        return new Float64Scalar(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float64Scalar(long value)
    {
        return new Float64Scalar(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float64Scalar(uint value)
    {
        return new Float64Scalar(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float64Scalar(ulong value)
    {
        return new Float64Scalar(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float64Scalar(float value)
    {
        return new Float64Scalar(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float64Scalar(double value)
    {
        return new Float64Scalar(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator double(Float64Scalar scalar)
    {
        return scalar.Value;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator byte(Float64Scalar scalar)
    {
        return (byte) scalar.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator int(Float64Scalar scalar)
    {
        return (int) scalar.Value;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator long(Float64Scalar scalar)
    {
        return (long) scalar.Value;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator uint(Float64Scalar scalar)
    {
        return (uint) scalar.Value;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator ulong(Float64Scalar scalar)
    {
        return (ulong) scalar.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator float(Float64Scalar scalar)
    {
        return (float) scalar.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Complex(Float64Scalar scalar)
    {
        return new Complex(scalar.Value, 0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator +(Float64Scalar scalar)
    {
        return scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator -(Float64Scalar scalar)
    {
        return new Float64Scalar(-scalar.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator +(int scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 + scalar2.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator +(Float64Scalar scalar1, int scalar2)
    {
        return new Float64Scalar(scalar1.Value + scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator +(long scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 + scalar2.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator +(Float64Scalar scalar1, long scalar2)
    {
        return new Float64Scalar(scalar1.Value + scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator +(float scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 + scalar2.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator +(Float64Scalar scalar1, float scalar2)
    {
        return new Float64Scalar(scalar1.Value + scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator +(double scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 + scalar2.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator +(Float64Scalar scalar1, double scalar2)
    {
        return new Float64Scalar(scalar1.Value + scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator +(Float64Scalar scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1.Value + scalar2.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator -(int scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 - scalar2.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator -(Float64Scalar scalar1, int scalar2)
    {
        return new Float64Scalar(scalar1.Value - scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator -(long scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 - scalar2.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator -(Float64Scalar scalar1, long scalar2)
    {
        return new Float64Scalar(scalar1.Value - scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator -(float scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 - scalar2.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator -(Float64Scalar scalar1, float scalar2)
    {
        return new Float64Scalar(scalar1.Value - scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator -(double scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 - scalar2.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator -(Float64Scalar scalar1, double scalar2)
    {
        return new Float64Scalar(scalar1.Value - scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator -(Float64Scalar scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1.Value - scalar2.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator *(int scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 * scalar2.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator *(Float64Scalar scalar1, int scalar2)
    {
        return new Float64Scalar(scalar1.Value * scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator *(long scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 * scalar2.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator *(Float64Scalar scalar1, long scalar2)
    {
        return new Float64Scalar(scalar1.Value * scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator *(float scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 * scalar2.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator *(Float64Scalar scalar1, float scalar2)
    {
        return new Float64Scalar(scalar1.Value * scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator *(double scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 * scalar2.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator *(Float64Scalar scalar1, double scalar2)
    {
        return new Float64Scalar(scalar1.Value * scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator *(Float64Scalar scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1.Value * scalar2.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator /(int scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 / scalar2.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator /(Float64Scalar scalar1, int scalar2)
    {
        return new Float64Scalar(scalar1.Value / scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator /(long scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 / scalar2.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator /(Float64Scalar scalar1, long scalar2)
    {
        return new Float64Scalar(scalar1.Value / scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator /(float scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 / scalar2.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator /(Float64Scalar scalar1, float scalar2)
    {
        return new Float64Scalar(scalar1.Value / scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator /(double scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1 / scalar2.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator /(Float64Scalar scalar1, double scalar2)
    {
        return new Float64Scalar(scalar1.Value / scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator /(Float64Scalar scalar1, Float64Scalar scalar2)
    {
        return new Float64Scalar(scalar1.Value / scalar2.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator %(Float64Scalar left, Float64Scalar right)
    {
        return new Float64Scalar(left.Value % right.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator &(Float64Scalar left, Float64Scalar right)
    {
        var bits = BitConverter.DoubleToUInt64Bits(left.Value) & BitConverter.DoubleToUInt64Bits(right.Value);
        return new Float64Scalar(BitConverter.UInt64BitsToDouble(bits));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator |(Float64Scalar left, Float64Scalar right)
    {
        var bits = BitConverter.DoubleToUInt64Bits(left.Value) | BitConverter.DoubleToUInt64Bits(right.Value);
        return new Float64Scalar(BitConverter.UInt64BitsToDouble(bits));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator ^(Float64Scalar left, Float64Scalar right)
    {
        var bits = BitConverter.DoubleToUInt64Bits(left.Value) ^ BitConverter.DoubleToUInt64Bits(right.Value);
        return new Float64Scalar(BitConverter.UInt64BitsToDouble(bits));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator ~(Float64Scalar value)
    {
        var bits = ~BitConverter.DoubleToUInt64Bits(value.Value);
        return new Float64Scalar(BitConverter.UInt64BitsToDouble(bits));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Float64Scalar left, int right)
    {
        return left.Value == right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Float64Scalar left, int right)
    {
        return left.Value != right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Float64Scalar left, int right)
    {
        return left.Value > right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Float64Scalar left, int right)
    {
        return left.Value >= right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Float64Scalar left, int right)
    {
        return left.Value < right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Float64Scalar left, int right)
    {
        return left.Value <= right;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(int left, Float64Scalar right)
    {
        return left == right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(int left, Float64Scalar right)
    {
        return left != right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(int left, Float64Scalar right)
    {
        return left > right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(int left, Float64Scalar right)
    {
        return left >= right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(int left, Float64Scalar right)
    {
        return left < right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(int left, Float64Scalar right)
    {
        return left <= right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Float64Scalar left, double right)
    {
        return left.Value == right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Float64Scalar left, double right)
    {
        return left.Value != right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Float64Scalar left, double right)
    {
        return left.Value > right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Float64Scalar left, double right)
    {
        return left.Value >= right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Float64Scalar left, double right)
    {
        return left.Value < right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Float64Scalar left, double right)
    {
        return left.Value <= right;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(double left, Float64Scalar right)
    {
        return left == right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(double left, Float64Scalar right)
    {
        return left != right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(double left, Float64Scalar right)
    {
        return left > right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(double left, Float64Scalar right)
    {
        return left >= right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(double left, Float64Scalar right)
    {
        return left < right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(double left, Float64Scalar right)
    {
        return left <= right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Float64Scalar left, Float64Scalar right)
    {
        return left.Value == right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Float64Scalar left, Float64Scalar right)
    {
        return left.Value != right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Float64Scalar left, Float64Scalar right)
    {
        return left.Value > right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Float64Scalar left, Float64Scalar right)
    {
        return left.Value >= right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Float64Scalar left, Float64Scalar right)
    {
        return left.Value < right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Float64Scalar left, Float64Scalar right)
    {
        return left.Value <= right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator --(Float64Scalar value)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar operator ++(Float64Scalar value)
    {
        throw new NotImplementedException();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCanonical(Float64Scalar value)
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsComplexNumber(Float64Scalar value)
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEvenInteger(Float64Scalar value)
    {
        return double.IsEvenInteger(value.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(Float64Scalar value)
    {
        return double.IsFinite(value.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsImaginaryNumber(Float64Scalar value)
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInfinity(Float64Scalar value)
    {
        return double.IsInfinity(value.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInteger(Float64Scalar value)
    {
        return double.IsInteger(value.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNaN(Float64Scalar value)
    {
        return double.IsNaN(value.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegative(Float64Scalar value)
    {
        return double.IsNegative(value.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegativeInfinity(Float64Scalar value)
    {
        return double.IsNegativeInfinity(value.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNormal(Float64Scalar value)
    {
        return double.IsNormal(value.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOddInteger(Float64Scalar value)
    {
        return double.IsOddInteger(value.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPositive(Float64Scalar value)
    {
        return double.IsPositive(value.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPositiveInfinity(Float64Scalar value)
    {
        return double.IsPositiveInfinity(value.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsRealNumber(Float64Scalar value)
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSubnormal(Float64Scalar value)
    {
        return double.IsSubnormal(value.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(Float64Scalar value)
    {
        return value.Value == 0d;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPow2(Float64Scalar value)
    {
        return double.IsPow2(value.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
    {
        return new Float64Scalar(double.Parse(s, style, provider));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Parse(string s, NumberStyles style, IFormatProvider? provider)
    {
        return new Float64Scalar(double.Parse(s, style, provider));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool INumberBase<Float64Scalar>.TryConvertFromChecked<TOther>(TOther value, out Float64Scalar result)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool INumberBase<Float64Scalar>.TryConvertFromSaturating<TOther>(TOther value, out Float64Scalar result)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool INumberBase<Float64Scalar>.TryConvertFromTruncating<TOther>(TOther value, out Float64Scalar result)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool INumberBase<Float64Scalar>.TryConvertToChecked<TOther>(Float64Scalar value, out TOther result)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool INumberBase<Float64Scalar>.TryConvertToSaturating<TOther>(Float64Scalar value, out TOther result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<Float64Scalar>.TryConvertToTruncating<TOther>(Float64Scalar value, out TOther result)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out Float64Scalar result)
    {
        if (double.TryParse(s, style, provider, out var resultValue))
        {
            result = new Float64Scalar(resultValue);
            return true;
        }

        result = Zero;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParse(string? s, NumberStyles style, IFormatProvider? provider, out Float64Scalar result)
    {
        if (double.TryParse(s, style, provider, out var resultValue))
        {
            result = new Float64Scalar(resultValue);
            return true;
        }

        result = Zero;
        return false;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Parse(string s, IFormatProvider? provider)
    {
        return new Float64Scalar(double.Parse(s, provider));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParse(string? s, IFormatProvider? provider, out Float64Scalar result)
    {
        if (double.TryParse(s, provider, out var resultValue))
        {
            result = new Float64Scalar(resultValue);
            return true;
        }

        result = Zero;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        return new Float64Scalar(double.Parse(s, provider));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Float64Scalar result)
    {
        if (double.TryParse(s, provider, out var resultValue))
        {
            result = new Float64Scalar(resultValue);
            return true;
        }

        result = Zero;
        return false;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar MaxMagnitude(Float64Scalar x, Float64Scalar y)
    {
        return new Float64Scalar(double.MaxMagnitude(x.Value, y.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar MaxMagnitudeNumber(Float64Scalar x, Float64Scalar y)
    {
        return new Float64Scalar(double.MaxMagnitudeNumber(x.Value, y.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar MinMagnitude(Float64Scalar x, Float64Scalar y)
    {
        return new Float64Scalar(double.MinMagnitude(x.Value, y.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar MinMagnitudeNumber(Float64Scalar x, Float64Scalar y)
    {
        return new Float64Scalar(double.MinMagnitudeNumber(x.Value, y.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Pow(Float64Scalar x, Float64Scalar y)
    {
        return new Float64Scalar(double.Pow(x.Value, y.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Abs(Float64Scalar value)
    {
        return new Float64Scalar(double.Abs(value.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log(Float64Scalar x)
    {
        return new Float64Scalar(double.Log(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log(Float64Scalar x, Float64Scalar newBase)
    {
        return new Float64Scalar(double.Log(x.Value, newBase.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log10(Float64Scalar x)
    {
        return new Float64Scalar(double.Log10(x.Value));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log2(Float64Scalar value)
    {
        return new Float64Scalar(double.Log2(value.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Exp(Float64Scalar x)
    {
        return new Float64Scalar(double.Exp(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Cosh(Float64Scalar x)
    {
        return new Float64Scalar(double.Cosh(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sinh(Float64Scalar x)
    {
        return new Float64Scalar(double.Sinh(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Tanh(Float64Scalar x)
    {
        return new Float64Scalar(double.Tanh(x.Value));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Exp10(Float64Scalar x)
    {
        return new Float64Scalar(double.Exp10(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Exp2(Float64Scalar x)
    {
        return new Float64Scalar(double.Exp2(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Round(Float64Scalar x, int digits, MidpointRounding mode)
    {
        return new Float64Scalar(double.Round(x.Value, digits, mode));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Acosh(Float64Scalar x)
    {
        return new Float64Scalar(double.Acosh(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Asinh(Float64Scalar x)
    {
        return new Float64Scalar(double.Asinh(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Atanh(Float64Scalar x)
    {
        return new Float64Scalar(double.Atanh(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Cbrt(Float64Scalar x)
    {
        return new Float64Scalar(double.Cbrt(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Hypot(Float64Scalar x, Float64Scalar y)
    {
        return new Float64Scalar(double.Hypot(x.Value, y.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar RootN(Float64Scalar x, int n)
    {
        return new Float64Scalar(double.RootN(x.Value, n));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sqrt(Float64Scalar x)
    {
        return new Float64Scalar(double.Sqrt(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Acos(Float64Scalar x)
    {
        return new Float64Scalar(double.Acos(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar AcosPi(Float64Scalar x)
    {
        return new Float64Scalar(double.AcosPi(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Asin(Float64Scalar x)
    {
        return new Float64Scalar(double.Asin(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar AsinPi(Float64Scalar x)
    {
        return new Float64Scalar(double.AsinPi(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Atan(Float64Scalar x)
    {
        return new Float64Scalar(double.Atan(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar AtanPi(Float64Scalar x)
    {
        return new Float64Scalar(double.AtanPi(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Cos(Float64Scalar x)
    {
        return new Float64Scalar(double.Cos(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar CosPi(Float64Scalar x)
    {
        return new Float64Scalar(double.CosPi(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sin(Float64Scalar x)
    {
        return new Float64Scalar(double.Sin(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (Float64Scalar Sin, Float64Scalar Cos) SinCos(Float64Scalar x)
    {
        var (sinValue, cosValue) = double.SinCos(x.Value);

        return (new Float64Scalar(sinValue), new Float64Scalar(cosValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (Float64Scalar SinPi, Float64Scalar CosPi) SinCosPi(Float64Scalar x)
    {
        var (sinValue, cosValue) = double.SinCosPi(x.Value);

        return (new Float64Scalar(sinValue), new Float64Scalar(cosValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar SinPi(Float64Scalar x)
    {
        return new Float64Scalar(double.SinPi(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Tan(Float64Scalar x)
    {
        return new Float64Scalar(double.Tan(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar TanPi(Float64Scalar x)
    {
        return new Float64Scalar(double.TanPi(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Atan2(Float64Scalar y, Float64Scalar x)
    {
        return new Float64Scalar(double.Atan2(y.Value, x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Atan2Pi(Float64Scalar y, Float64Scalar x)
    {
        return new Float64Scalar(double.Atan2(y.Value, x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar BitDecrement(Float64Scalar x)
    {
        return new Float64Scalar(double.BitDecrement(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar BitIncrement(Float64Scalar x)
    {
        return new Float64Scalar(double.BitIncrement(x.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar FusedMultiplyAdd(Float64Scalar left, Float64Scalar right, Float64Scalar addend)
    {
        return new Float64Scalar(double.FusedMultiplyAdd(left.Value, right.Value, addend.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Ieee754Remainder(Float64Scalar left, Float64Scalar right)
    {
        return new Float64Scalar(double.Ieee754Remainder(left.Value, right.Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ILogB(Float64Scalar x)
    {
        return double.ILogB(x.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar ScaleB(Float64Scalar x, int n)
    {
        return new Float64Scalar(double.ScaleB(x.Value, n));
    }
    

    public double Value { get; }

    public bool ScalarIsZero 
        => Value == 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar(double value)
    {
        if (!value.IsValid())
            throw new ArgumentException(nameof(value));

        Value = value;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return Value == 0d;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsOne()
    {
        return Value == 1d;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNegativeOne()
    {
        return Value == -1d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double epsilon = 1e-12)
    {
        return !(double.IsInfinity(Value) || 
                 Value < -epsilon || 
                 Value > epsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearOne(double epsilon = 1e-12)
    {
        var value = Value - 1d;

        return !(double.IsInfinity(value) || 
                 value < -epsilon || 
                 value > epsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearNegativeOne(double epsilon = 1e-12)
    {
        var value = Value + 1d;

        return !(double.IsInfinity(value) || 
                 value < -epsilon || 
                 value > epsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearEqual(double value2, double epsilon = 1e-12)
    {
        var value = Value - value2;

        return !(double.IsInfinity(value) || 
                 value < -epsilon || 
                 value > epsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsPositive()
    {
        return Value > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNegative()
    {
        return Value < 0;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsFinite()
    {
        return double.IsFinite(Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsInfinite()
    {
        return double.IsInfinity(Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsInRange(double value1, double value2)
    {
        Debug.Assert(
            !double.IsNaN(value1) &&
            !double.IsNaN(value2) &&
            value1 < value2
        );

        return !(IsInfinite() || Value < value1 || Value > value2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearInRange(double value1, double value2, double epsilon = 1e-12d)
    {
        Debug.Assert(
            !double.IsNaN(value1) &&
            !double.IsNaN(value2) &&
            value1 < value2
        );

        return !(IsInfinite() || Value < value1 - epsilon || Value > value2 + epsilon);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign Sign()
    {
        return Value switch
        {
            < 0 => IntegerSign.Negative,
            > 0 => IntegerSign.Positive,
            _ => IntegerSign.Zero
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign Sign(double epsilon)
    {
        if (Value < -epsilon)
            return IntegerSign.Negative;

        if (Value > epsilon)
            return IntegerSign.Positive;

        return IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Negative()
    {
        return new Float64Scalar(-Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Add(double value)
    {
        return new Float64Scalar(Value + value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Add(params double[] valueList)
    {
        var sumValue = valueList.Aggregate(
            Value,
            (a, b) => a + b
        );

        return new Float64Scalar(sumValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Add(IEnumerable<double> valueList)
    {
        var sumValue = valueList.Aggregate(
            Value,
            (a, b) => a + b
        );

        return new Float64Scalar(sumValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Subtract(double value)
    {
        return new Float64Scalar(Value - value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Times(double value)
    {
        return new Float64Scalar(Value * value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Times(params double[] valueList)
    {
        var sumValue = valueList.Aggregate(
            Value,
            (a, b) => a * b
        );

        return new Float64Scalar(sumValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Times(IEnumerable<double> valueList)
    {
        var sumValue = valueList.Aggregate(
            Value,
            (a, b) => a * b
        );

        return new Float64Scalar(sumValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Divide(double value)
    {
        return new Float64Scalar(Value / value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Inverse()
    {
        return new Float64Scalar(1d / Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Ceiling()
    {
        return new Float64Scalar(Math.Ceiling(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Floor()
    {
        return new Float64Scalar(Math.Floor(Value));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar MaxMagnitude(double y)
    {
        return new Float64Scalar(double.MaxMagnitude(Value, y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar MaxMagnitudeNumber(double y)
    {
        return new Float64Scalar(double.MaxMagnitudeNumber(Value, y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar MinMagnitude(double y)
    {
        return new Float64Scalar(double.MinMagnitude(Value, y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar MinMagnitudeNumber(double y)
    {
        return new Float64Scalar(double.MinMagnitudeNumber(Value, y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Pow(double y)
    {
        return new Float64Scalar(Math.Pow(Value, y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Abs()
    {
        return new Float64Scalar(Math.Abs(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Log()
    {
        return new Float64Scalar(Math.Log(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar LogB(double newBase)
    {
        return new Float64Scalar(Math.Log(Value, newBase));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Log10()
    {
        return new Float64Scalar(Math.Log10(Value));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Log2()
    {
        return new Float64Scalar(Math.Log2(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Exp()
    {
        return new Float64Scalar(Math.Exp(Value));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Exp10()
    {
        return new Float64Scalar(double.Exp10(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Exp2()
    {
        return new Float64Scalar(double.Exp2(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Cosh()
    {
        return new Float64Scalar(Math.Cosh(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Sinh()
    {
        return new Float64Scalar(Math.Sinh(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Tanh()
    {
        return new Float64Scalar(Math.Tanh(Value));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Sech()
    {
        return new Float64Scalar(1d / Math.Cosh(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Csch()
    {
        return new Float64Scalar(1d / Math.Sinh(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Coth()
    {
        return new Float64Scalar(1d / Math.Tan(Value));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ArcCosh()
    {
        return new Float64Scalar(Math.Acosh(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ArcSinh()
    {
        return new Float64Scalar(Math.Asinh(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ArcTanh()
    {
        return new Float64Scalar(Math.Atanh(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ArcSech()
    {
        return new Float64Scalar(Math.Acosh(1d / Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ArcCsch()
    {
        return new Float64Scalar(Math.Asinh(1d / Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ArcCoth()
    {
        return new Float64Scalar(Math.Atanh(1d / Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Round(int digits, MidpointRounding mode)
    {
        return new Float64Scalar(Math.Round(Value, digits, mode));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Cbrt()
    {
        return new Float64Scalar(Math.Cbrt(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Hypot(double y)
    {
        return new Float64Scalar(double.Hypot(Value, y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar RootN(int n)
    {
        return new Float64Scalar(double.RootN(Value, n));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Square()
    {
        return new Float64Scalar(Value * Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Power(double n)
    {
        return new Float64Scalar(
            Math.Pow(Value, n)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Sqrt()
    {
        return new Float64Scalar(Math.Sqrt(Value));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Cos()
    {
        return new Float64Scalar(Math.Cos(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Sin()
    {
        return new Float64Scalar(Math.Sin(Value));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<Float64Scalar> SinCos()
    {
        var (sinValue, cosValue) = 
            Math.SinCos(Value);

        return new Pair<Float64Scalar>(
            new Float64Scalar(sinValue), 
            new Float64Scalar(cosValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Tan()
    {
        return new Float64Scalar(Math.Tan(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Sec()
    {
        return new Float64Scalar(1d / Math.Cos(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Csc()
    {
        return new Float64Scalar(1d / Math.Sin(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Cot()
    {
        return new Float64Scalar(1d / Math.Tan(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar CosPi()
    {
        return new Float64Scalar(double.CosPi(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar SinPi()
    {
        return new Float64Scalar(double.SinPi(Value));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar TanPi()
    {
        return new Float64Scalar(double.TanPi(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<Float64Scalar> SinCosPi()
    {
        var (sinValue, cosValue) = 
            double.SinCosPi(Value);

        return new Pair<Float64Scalar>(
            new Float64Scalar(sinValue), 
            new Float64Scalar(cosValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle ArcCos()
    {
        return Math.Acos(Value.Clamp(-1, 1)).RadiansToAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle ArcSin()
    {
        return Math.Asin(Value.Clamp(-1, 1)).RadiansToAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle ArcTan()
    {
        return Math.Atan(Value).RadiansToAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle ArcSec()
    {
        return Math.Acos((1d / Value).Clamp(-1, 1)).RadiansToAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle ArcCsc()
    {
        return Math.Asin((1d / Value).Clamp(-1, 1)).RadiansToAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle ArcCot()
    {
        return Math.Atan(1d / Value).RadiansToAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle ArcTan2(double y)
    {
        return Math.Atan2(y, Value).RadiansToAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ArcCosPi()
    {
        return new Float64Scalar(double.AcosPi(Value));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ArcSinPi()
    {
        return new Float64Scalar(double.AsinPi(Value));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ArcTanPi()
    {
        return new Float64Scalar(double.AtanPi(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ArcTan2Pi(double y)
    {
        return new Float64Scalar(Math.Atan2(y, Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar BitDecrement()
    {
        return new Float64Scalar(Math.BitDecrement(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar BitIncrement()
    {
        return new Float64Scalar(Math.BitIncrement(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar FusedMultiplyAdd(double right, double addend)
    {
        return new Float64Scalar(Math.FusedMultiplyAdd(Value, right, addend));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Ieee754Remainder(double right)
    {
        return new Float64Scalar(double.Ieee754Remainder(Value, right));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // ReSharper disable once InconsistentNaming
    public int ILogB()
    {
        return double.ILogB(Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ScaleB(int n)
    {
        return new Float64Scalar(double.ScaleB(Value, n));
    }
    


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Clamp(double lowerLimit, double upperLimit)
    {
        if (Value < lowerLimit) return lowerLimit;
        if (Value > upperLimit) return upperLimit;

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ClampToUnit()
    {
        return Value switch
        {
            < 0.0d => Zero,
            > 1.0d => One,
            _ => this
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ClampTo(double maxValue)
    {
        if (Value < 0.0d) return Zero;
        if (Value > maxValue) return maxValue;
        
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ClampTo(double minValue, double maxValue)
    {
        if (Value < minValue) return minValue;
        if (Value > maxValue) return maxValue;
        
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ClampToSymmetric(double maxValue)
    {
        if (Value < -maxValue) return -maxValue;
        if (Value > maxValue) return maxValue;
        
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Lerp(double v1, double v2)
    {
        return new Float64Scalar(
            (1.0d - Value) * v1 + Value * v2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector Lerp(Float64Vector v1, Float64Vector v2)
    {
        return (1.0d - Value) * v1 + Value * v2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D Lerp(IFloat64Vector2D v1, IFloat64Vector2D v2)
    {
        var t = Value;
        var s = 1.0d - t;

        return Float64Vector2D.Create(s * v1.X + t * v2.X,
            s * v1.Y + t * v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D Lerp(IFloat64Vector3D v1, IFloat64Vector3D v2)
    {
        var t = Value;
        var s = 1.0d - t;

        return Float64Vector3D.Create(s * v1.X + t * v2.X,
            s * v1.Y + t * v2.Y,
            s * v1.Z + t * v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Quaternion Lerp(IFloat64Vector4D v1, IFloat64Vector4D v2)
    {
        var t = Value;
        var s = 1.0d - t;

        return Float64Quaternion.Create(s * v1.X + t * v2.X,
            s * v1.Y + t * v2.Y,
            s * v1.Z + t * v2.Z,
            s * v1.W + t * v2.W);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetExponentByteCount()
    {
        return sizeof(short);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    int IFloatingPoint<Float64Scalar>.GetExponentShortestBitLength()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    int IFloatingPoint<Float64Scalar>.GetSignificandBitLength()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    int IFloatingPoint<Float64Scalar>.GetSignificandByteCount()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool IFloatingPoint<Float64Scalar>.TryWriteExponentBigEndian(Span<byte> destination, out int bytesWritten)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool IFloatingPoint<Float64Scalar>.TryWriteExponentLittleEndian(Span<byte> destination, out int bytesWritten)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool IFloatingPoint<Float64Scalar>.TryWriteSignificandBigEndian(Span<byte> destination, out int bytesWritten)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool IFloatingPoint<Float64Scalar>.TryWriteSignificandLittleEndian(Span<byte> destination, out int bytesWritten)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Float64Scalar other)
    {
        if (Value < other.Value) return -1;
        if (Value > other.Value) return 1;

        return 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(double otherValue)
    {
        Debug.Assert(otherValue.IsValid());

        if (Value < otherValue) return -1;
        if (Value > otherValue) return 1;

        return 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Float64Scalar other)
    {
        return Value == other.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(double otherValue)
    {
        Debug.Assert(otherValue.IsValid());

        return Value == otherValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            Float64Scalar scalar1 => Equals(scalar1),
            double scalar2 => Equals(scalar2),
            _ => false
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return Value.ToString(format, formatProvider);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        return Value.TryFormat(destination, out charsWritten, format, provider);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(object? obj)
    {
        return Value.CompareTo(obj);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TypeCode GetTypeCode()
    {
        return Value.GetTypeCode();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ToBoolean(IFormatProvider? provider)
    {
        return Convert.ToBoolean(Value, provider);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ToByte(IFormatProvider? provider)
    {
        return Convert.ToByte(Value, provider);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public char ToChar(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DateTime ToDateTime(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public decimal ToDecimal(IFormatProvider? provider)
    {
        return Convert.ToDecimal(Value, provider);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToDouble(IFormatProvider? provider)
    {
        return Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public short ToInt16(IFormatProvider? provider)
    {
        return Convert.ToInt16(Value, provider);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int ToInt32(IFormatProvider? provider)
    {
        return Convert.ToInt32(Value, provider);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long ToInt64(IFormatProvider? provider)
    {
        return Convert.ToInt64(Value, provider);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sbyte ToSByte(IFormatProvider? provider)
    {
        return Convert.ToSByte(Value, provider);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float ToSingle(IFormatProvider? provider)
    {
        return Convert.ToSingle(Value, provider);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(string formatString)
    {
        return Value.ToString(formatString);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(IFormatProvider? provider)
    {
        return Value.ToString(provider);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ushort ToUInt16(IFormatProvider? provider)
    {
        return Convert.ToUInt16(Value, provider);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint ToUInt32(IFormatProvider? provider)
    {
        return Convert.ToUInt32(Value, provider);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong ToUInt64(IFormatProvider? provider)
    {
        return Convert.ToUInt64(Value, provider);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle DegreesToAngle()
    {
        return Float64PlanarAngle.CreateFromDegrees(Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle RadiansToAngle()
    {
        return Float64PlanarAngle.CreateFromRadians(Value);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Scalar> GetLinearRange(double finish, int count, bool isPeriodicRange = false)
    {
        var start = Value;
        var length = finish - start;
        var n = isPeriodicRange
            ? length / count
            : length / (count - 1);

        return Enumerable
            .Range(0, count)
            .Select(i => new Float64Scalar(start + i * n));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Scalar> GetLinearPeriodicRange(double finish, int count)
    {
        var start = Value;
        var length = finish - start;
        var n = length / count;

        return Enumerable
            .Range(0, count)
            .Select(i => new Float64Scalar(start + i * n));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar CosWave(double value1, double value2, int cycleCount = 1)
    {
        var t = (cycleCount * Value).ClampPeriodic(1);

        return new Float64Scalar(
            value1 + 0.5d * (1d - Math.Cos(2d * Math.PI * t)) * (value2 - value1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar CosWave(IPair<double> valuePair, int cycleCount = 1)
    {
        var value1 = valuePair.Item1;
        var value2 = valuePair.Item2;
        
        var t = (cycleCount * Value).ClampPeriodic(1);

        return new Float64Scalar(
            value1 + 0.5d * (1d - Math.Cos(2d * Math.PI * t)) * (value2 - value1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar TriangleWave(double value1, double value2, int cycleCount = 1)
    {
        var t = (cycleCount * Value).ClampPeriodic(1);

        return new Float64Scalar(
            t <= 0.5d
                ? value1 + 2 * t * (value2 - value1)
                : value1 + 2 * (1d - t) * (value2 - value1)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar TriangleWave(IPair<double> valuePair, int cycleCount = 1)
    {
        var value1 = valuePair.Item1;
        var value2 = valuePair.Item2;
        
        var t = (cycleCount * Value).ClampPeriodic(1);

        return new Float64Scalar(
            t <= 0.5d
                ? value1 + 2 * t * (value2 - value1)
                : value1 + 2 * (1d - t) * (value2 - value1)
        );
    }

    /// <summary>
    /// Go from start to finish then to start again following a cos shape
    /// </summary>
    /// <param name="finish"></param>
    /// <param name="sampleCount"></param>
    /// <param name="cycleCount"></param>
    /// <param name="isPeriodicRange"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Scalar> GetCosRange(double finish, int sampleCount, int cycleCount = 1, bool isPeriodicRange = false)
    {
        var start = Value;
        var length = finish - start;
        var n = isPeriodicRange
            ? 2d * Math.PI * cycleCount / sampleCount
            : 2d * Math.PI * cycleCount / (sampleCount - 1);

        return Enumerable
            .Range(0, sampleCount)
            .Select(i => new Float64Scalar(
                start + 0.5d * (1d - Math.Cos(i * n)) * length
            ));
    }

}