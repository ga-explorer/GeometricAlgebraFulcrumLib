using System.Globalization;
using System.Numerics;

namespace GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

[Serializable]
public readonly struct Float64ComplexScalar : 
    IEquatable<Complex>,
    ISignedNumber<Float64ComplexScalar>
{
    public Complex Value { get; }


    public bool Equals(Float64ComplexScalar other)
    {
        throw new NotImplementedException();
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        throw new NotImplementedException();
    }

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar Parse(string s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Float64ComplexScalar result)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Float64ComplexScalar result)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar operator +(Float64ComplexScalar left, Float64ComplexScalar right)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar AdditiveIdentity { get; }
    public static Float64ComplexScalar operator --(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar operator /(Float64ComplexScalar left, Float64ComplexScalar right)
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(Float64ComplexScalar left, Float64ComplexScalar right)
    {
        throw new NotImplementedException();
    }

    public static bool operator !=(Float64ComplexScalar left, Float64ComplexScalar right)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar operator ++(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar MultiplicativeIdentity { get; }
    public static Float64ComplexScalar operator *(Float64ComplexScalar left, Float64ComplexScalar right)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar operator -(Float64ComplexScalar left, Float64ComplexScalar right)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar operator -(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar operator +(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar Abs(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsCanonical(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsComplexNumber(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsEvenInteger(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsFinite(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsImaginaryNumber(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsInfinity(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsInteger(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsNaN(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsNegative(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsNegativeInfinity(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsNormal(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsOddInteger(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsPositive(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsPositiveInfinity(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsRealNumber(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsSubnormal(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static bool IsZero(Float64ComplexScalar value)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar MaxMagnitude(Float64ComplexScalar x, Float64ComplexScalar y)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar MaxMagnitudeNumber(Float64ComplexScalar x, Float64ComplexScalar y)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar MinMagnitude(Float64ComplexScalar x, Float64ComplexScalar y)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar MinMagnitudeNumber(Float64ComplexScalar x, Float64ComplexScalar y)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar Parse(string s, NumberStyles style, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertFromChecked<TOther>(TOther value, out Float64ComplexScalar result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertFromSaturating<TOther>(TOther value, out Float64ComplexScalar result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertFromTruncating<TOther>(TOther value, out Float64ComplexScalar result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertToChecked<TOther>(Float64ComplexScalar value, out TOther result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertToSaturating<TOther>(Float64ComplexScalar value, out TOther result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertToTruncating<TOther>(Float64ComplexScalar value, out TOther result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out Float64ComplexScalar result)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(string? s, NumberStyles style, IFormatProvider? provider, out Float64ComplexScalar result)
    {
        throw new NotImplementedException();
    }

    public static Float64ComplexScalar One { get; }
    public static int Radix { get; }
    public static Float64ComplexScalar Zero { get; }
    public static Float64ComplexScalar NegativeOne { get; }

    public bool Equals(Complex other)
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        return obj is Float64ComplexScalar other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}