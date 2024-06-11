using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

public readonly struct Scalar<T> :
    IScalar<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Scalar<T> Create(IScalar<T> scalar)
    {
        return scalar is Scalar<T> scalar2 
            ? scalar2 
            : new Scalar<T>(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Scalar<T> Create(IScalarProcessor<T> processor, T scalarValue)
    {
        return new Scalar<T>(processor, scalarValue);
    }

    // TODO: Gradually remove this from depending code
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator T(Scalar<T> d)
    //{
    //    return d.ScalarValue;
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1)
    {
        return s1.Positive();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1)
    {
        return s1.Negative();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, int s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(int s1, Scalar<T> s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, uint s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(uint s1, Scalar<T> s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, long s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(long s1, Scalar<T> s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, ulong s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(ulong s1, Scalar<T> s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, float s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(float s1, Scalar<T> s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, double s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(double s1, Scalar<T> s2)
    {
        return s1.Add(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, string s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(string s1, Scalar<T> s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, T s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(T s1, Scalar<T> s2)
    {
        return s1.Add(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(IScalar<T> s1, Scalar<T> s2)
    {
        return s1.Add(s2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, IScalar<T> s2)
    {
        return s1.Add(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, Scalar<T> s2)
    {
        return s1.Add(s2.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, int s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(int s1, Scalar<T> s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, uint s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(uint s1, Scalar<T> s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, long s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(long s1, Scalar<T> s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, ulong s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(ulong s1, Scalar<T> s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, float s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(float s1, Scalar<T> s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, double s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(double s1, Scalar<T> s2)
    {
        return s1.Subtract(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, string s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(string s1, Scalar<T> s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, T s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(T s1, Scalar<T> s2)
    {
        return s1.Subtract(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(IScalar<T> s1, Scalar<T> s2)
    {
        return s1.Subtract(s2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, IScalar<T> s2)
    {
        return s1.Subtract(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, Scalar<T> s2)
    {
        return s1.Subtract(s2.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, int s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(int s1, Scalar<T> s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, uint s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(uint s1, Scalar<T> s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, long s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(long s1, Scalar<T> s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, ulong s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(ulong s1, Scalar<T> s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, float s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(float s1, Scalar<T> s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, double s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(double s1, Scalar<T> s2)
    {
        return s1.Times(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, string s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(string s1, Scalar<T> s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, T s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(T s1, Scalar<T> s2)
    {
        return s1.Times(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(IScalar<T> s1, Scalar<T> s2)
    {
        return s1.Times(s2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, IScalar<T> s2)
    {
        return s1.Times(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, Scalar<T> s2)
    {
        return s1.Times(s2.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, int s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(int s1, Scalar<T> s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, uint s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(uint s1, Scalar<T> s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, long s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(long s1, Scalar<T> s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, ulong s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(ulong s1, Scalar<T> s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, float s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(float s1, Scalar<T> s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, double s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(double s1, Scalar<T> s2)
    {
        return s1.Divide(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, string s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(string s1, Scalar<T> s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, T s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(T s1, Scalar<T> s2)
    {
        return s1.Divide(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(IScalar<T> s1, Scalar<T> s2)
    {
        return s1.Divide(s2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, IScalar<T> s2)
    {
        return s1.Divide(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, Scalar<T> s2)
    {
        return s1.Divide(s2.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Scalar<T> s1, int s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(int s1, Scalar<T> s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Scalar<T> s1, uint s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(uint s1, Scalar<T> s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Scalar<T> s1, long s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(long s1, Scalar<T> s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Scalar<T> s1, ulong s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(ulong s1, Scalar<T> s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Scalar<T> s1, float s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(float s1, Scalar<T> s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Scalar<T> s1, double s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(double s1, Scalar<T> s2)
    {
        return s1.IsEqualTo(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Scalar<T> s1, string s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(string s1, Scalar<T> s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Scalar<T> s1, T s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(T s1, Scalar<T> s2)
    {
        return s1.IsEqualTo(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(IScalar<T> s1, Scalar<T> s2)
    {
        return s1.IsEqualTo(s2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Scalar<T> s1, IScalar<T> s2)
    {
        return s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Scalar<T> s1, Scalar<T> s2)
    {
        return s1.IsEqualTo(s2.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Scalar<T> s1, int s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(int s1, Scalar<T> s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Scalar<T> s1, uint s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(uint s1, Scalar<T> s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Scalar<T> s1, long s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(long s1, Scalar<T> s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Scalar<T> s1, ulong s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(ulong s1, Scalar<T> s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Scalar<T> s1, float s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(float s1, Scalar<T> s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Scalar<T> s1, double s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(double s1, Scalar<T> s2)
    {
        return !s1.IsEqualTo(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Scalar<T> s1, string s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(string s1, Scalar<T> s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Scalar<T> s1, T s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(T s1, Scalar<T> s2)
    {
        return !s1.IsEqualTo(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(IScalar<T> s1, Scalar<T> s2)
    {
        return !s1.IsEqualTo(s2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Scalar<T> s1, IScalar<T> s2)
    {
        return !s1.IsEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Scalar<T> s1, Scalar<T> s2)
    {
        return !s1.IsEqualTo(s2.ScalarValue);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Scalar<T> s1, int s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(int s1, Scalar<T> s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Scalar<T> s1, uint s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(uint s1, Scalar<T> s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Scalar<T> s1, long s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(long s1, Scalar<T> s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Scalar<T> s1, ulong s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(ulong s1, Scalar<T> s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Scalar<T> s1, float s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(float s1, Scalar<T> s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Scalar<T> s1, double s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(double s1, Scalar<T> s2)
    {
        return s1.IsLessThan(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Scalar<T> s1, string s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(string s1, Scalar<T> s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Scalar<T> s1, T s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(T s1, Scalar<T> s2)
    {
        return s1.IsLessThan(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(IScalar<T> s1, Scalar<T> s2)
    {
        return s1.IsLessThan(s2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Scalar<T> s1, IScalar<T> s2)
    {
        return s1.IsLessThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Scalar<T> s1, Scalar<T> s2)
    {
        return s1.IsLessThan(s2.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Scalar<T> s1, int s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(int s1, Scalar<T> s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Scalar<T> s1, uint s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(uint s1, Scalar<T> s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Scalar<T> s1, long s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(long s1, Scalar<T> s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Scalar<T> s1, ulong s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(ulong s1, Scalar<T> s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Scalar<T> s1, float s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(float s1, Scalar<T> s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Scalar<T> s1, double s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(double s1, Scalar<T> s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Scalar<T> s1, string s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(string s1, Scalar<T> s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Scalar<T> s1, T s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(T s1, Scalar<T> s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(IScalar<T> s1, Scalar<T> s2)
    {
        return s1.IsLessThanOrEqualTo(s2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Scalar<T> s1, IScalar<T> s2)
    {
        return s1.IsLessThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Scalar<T> s1, Scalar<T> s2)
    {
        return s1.IsLessThanOrEqualTo(s2.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Scalar<T> s1, int s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(int s1, Scalar<T> s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Scalar<T> s1, uint s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(uint s1, Scalar<T> s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Scalar<T> s1, long s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(long s1, Scalar<T> s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Scalar<T> s1, ulong s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(ulong s1, Scalar<T> s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Scalar<T> s1, float s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(float s1, Scalar<T> s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Scalar<T> s1, double s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(double s1, Scalar<T> s2)
    {
        return s1.IsMoreThan(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Scalar<T> s1, string s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(string s1, Scalar<T> s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Scalar<T> s1, T s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(T s1, Scalar<T> s2)
    {
        return s1.IsMoreThan(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(IScalar<T> s1, Scalar<T> s2)
    {
        return s1.IsMoreThan(s2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Scalar<T> s1, IScalar<T> s2)
    {
        return s1.IsMoreThan(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Scalar<T> s1, Scalar<T> s2)
    {
        return s1.IsMoreThan(s2.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Scalar<T> s1, int s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(int s1, Scalar<T> s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Scalar<T> s1, uint s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(uint s1, Scalar<T> s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Scalar<T> s1, long s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(long s1, Scalar<T> s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Scalar<T> s1, ulong s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(ulong s1, Scalar<T> s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Scalar<T> s1, float s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(float s1, Scalar<T> s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Scalar<T> s1, double s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(double s1, Scalar<T> s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Scalar<T> s1, string s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(string s1, Scalar<T> s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Scalar<T> s1, T s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(T s1, Scalar<T> s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(IScalar<T> s1, Scalar<T> s2)
    {
        return s1.IsMoreThanOrEqualTo(s2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Scalar<T> s1, IScalar<T> s2)
    {
        return s1.IsMoreThanOrEqualTo(s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Scalar<T> s1, Scalar<T> s2)
    {
        return s1.IsMoreThanOrEqualTo(s2.ScalarValue);
    }


    public IScalarProcessor<T> ScalarProcessor { get; }

    public T ScalarValue { get; }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Scalar(IScalar<T> scalar)
    {
        ScalarProcessor = scalar.ScalarProcessor;
        ScalarValue = scalar.ScalarValue;

        Debug.Assert(ScalarProcessor.IsValid(ScalarValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Scalar(IScalarProcessor<T> scalarProcessor, T scalarValue)
    {
        Debug.Assert(scalarValue is not null);

        ScalarProcessor = scalarProcessor;
        ScalarValue = scalarValue;

        Debug.Assert(ScalarProcessor.IsValid(ScalarValue));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return ScalarProcessor.ToText(ScalarValue);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> MapScalar(Func<T, T> scalarMapping)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        scalarMapping(ScalarValue)
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Negative()
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Negative(ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Negative(T scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Negative(ScalarProcessor.Times(ScalarValue, scalar))
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Negative(IScalar<T> scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Negative(
    //            ScalarProcessor.Times(ScalarValue, scalar.ScalarValue)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Add(int scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Add(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Add(uint scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Add(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Add(long scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Add(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Add(ulong scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Add(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Add(float scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Add(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Add(double scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Add(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Add(T scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Add(ScalarValue, scalar)
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Add(IScalar<T> scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Add(ScalarValue, scalar.ScalarValue)
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Subtract(int scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Subtract(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Subtract(uint scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Subtract(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Subtract(long scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Subtract(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Subtract(ulong scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Subtract(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Subtract(float scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Subtract(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Subtract(double scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Subtract(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Subtract(T scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Subtract(ScalarValue, scalar)
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Subtract(IScalar<T> scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Subtract(ScalarValue, scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Times(int scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Times(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Times(uint scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Times(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Times(long scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Times(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Times(ulong scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Times(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Times(float scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Times(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Times(double scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Times(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Times(T scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Times(ScalarValue, scalar)
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Times(IScalar<T> scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Times(ScalarValue, scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Divide(int scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Divide(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Divide(uint scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Divide(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Divide(long scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Divide(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Divide(ulong scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Divide(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Divide(float scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Divide(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Divide(double scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Divide(
    //            ScalarValue, 
    //            ScalarProcessor.ScalarFromNumber(scalar)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Divide(T scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Divide(ScalarValue, scalar)
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Divide(IScalar<T> scalar)
    //{
    //    return new Scalar<T>(
    //        ScalarProcessor,
    //        ScalarProcessor.Divide(ScalarValue, scalar.ScalarValue)
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Min(IScalar<T> scalar)
    //{
    //    return ScalarProcessor.IsNegative(
    //        ScalarProcessor.Subtract(
    //            ScalarValue,
    //            scalar.ScalarValue
    //        )
    //    ) ? this : scalar.ToScalar();
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> Max(IScalar<T> scalar)
    //{
    //    return ScalarProcessor.IsNegative(
    //        ScalarProcessor.Subtract(
    //            ScalarValue,
    //            scalar.ScalarValue
    //        )
    //    ) ? scalar.ToScalar() : this;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ToScalar()
    {
        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Scalar<T>? other)
    {
        if (other is null) return false;
        //if (ReferenceEquals(this, other)) return true;
        
        return this.IsEqualTo(other);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(IScalar<T>? other)
    {
        if (other is null) return false;
        //if (ReferenceEquals(this, other)) return true;
        
        return this.IsEqualTo(other);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? other)
    {
        if (other is IScalar<T> scalar2) 
            return this.IsEqualTo(scalar2);

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return ScalarValue?.GetHashCode() ?? 0;
    }

}