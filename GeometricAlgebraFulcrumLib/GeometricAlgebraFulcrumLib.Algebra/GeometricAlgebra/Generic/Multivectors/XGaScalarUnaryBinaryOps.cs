using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public sealed partial class XGaScalar<T>
{
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator T(XGaScalar<T> mv)
    //{
    //    return mv.ScalarValue;
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator Scalar<T>(XGaScalar<T> mv)
    //{
    //    return mv.Scalar();
    //}
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(XGaScalar<T> mv)
    {
        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(XGaScalar<T> s1)
    {
        return new XGaScalar<T>(
            s1.Processor,
            s1.ScalarProcessor.Negative(s1.ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(XGaScalar<T> s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
            s1.ScalarProcessor.Add(s1.ScalarValue, s2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(XGaScalar<T> s1, Scalar<T> s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
            s1.ScalarProcessor.Add(s1.ScalarValue, s2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(Scalar<T> s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
            s2.ScalarProcessor.Add(s1.ScalarValue, s2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(XGaScalar<T> s1, int s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
            s1.ScalarProcessor.Add(
                s1.ScalarValue, 
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(int s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Add(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(XGaScalar<T> s1, uint s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Add(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(uint s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Add(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(XGaScalar<T> s1, long s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Add(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(long s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Add(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(XGaScalar<T> s1, ulong s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Add(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(ulong s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Add(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(XGaScalar<T> s1, float s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Add(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(float s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Add(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(XGaScalar<T> s1, double s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Add(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(double s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Add(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(XGaScalar<T> s1, T s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Add(s1.ScalarValue, s2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator +(T s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Add(s1, s2.ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(XGaScalar<T> s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Subtract(s1.ScalarValue, s2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(XGaScalar<T> s1, Scalar<T> s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Subtract(s1.ScalarValue, s2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(Scalar<T> s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Subtract(s1.ScalarValue, s2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(XGaScalar<T> s1, int s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Subtract(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(int s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Subtract(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(XGaScalar<T> s1, uint s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Subtract(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(uint s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Subtract(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(XGaScalar<T> s1, long s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Subtract(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(long s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Subtract(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(XGaScalar<T> s1, ulong s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Subtract(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(ulong s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Subtract(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(XGaScalar<T> s1, float s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Subtract(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(float s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Subtract(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(XGaScalar<T> s1, double s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Subtract(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(double s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Subtract(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(XGaScalar<T> s1, T s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Subtract(s1.ScalarValue, s2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator -(T s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Subtract(s1, s2.ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(XGaScalar<T> s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Times(s1.ScalarValue, s2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(XGaScalar<T> s1, Scalar<T> s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Times(s1.ScalarValue, s2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(Scalar<T> s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Times(s1.ScalarValue, s2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(XGaScalar<T> s1, int s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Times(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(int s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Times(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(XGaScalar<T> s1, uint s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Times(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(uint s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Times(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(XGaScalar<T> s1, long s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Times(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(long s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Times(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(XGaScalar<T> s1, ulong s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Times(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(ulong s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Times(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(XGaScalar<T> s1, float s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Times(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(float s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Times(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(XGaScalar<T> s1, double s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Times(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(double s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Times(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(XGaScalar<T> s1, T s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Times(s1.ScalarValue, s2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator *(T s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Times(s1, s2.ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(XGaScalar<T> s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Divide(s1.ScalarValue, s2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(XGaScalar<T> s1, Scalar<T> s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Divide(s1.ScalarValue, s2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(Scalar<T> s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Divide(s1.ScalarValue, s2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(XGaScalar<T> s1, int s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Divide(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(int s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Divide(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(XGaScalar<T> s1, uint s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Divide(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(uint s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Divide(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(XGaScalar<T> s1, long s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Divide(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(long s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Divide(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(XGaScalar<T> s1, ulong s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Divide(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(ulong s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Divide(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(XGaScalar<T> s1, float s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Divide(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(float s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Divide(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(XGaScalar<T> s1, double s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Divide(
                s1.ScalarValue,
                s1.ScalarProcessor.ValueFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(double s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Divide(
                s2.ScalarProcessor.ValueFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(XGaScalar<T> s1, T s2)
    {
        return new XGaScalar<T>(
            s1.Processor,
                
            s1.ScalarProcessor.Divide(s1.ScalarValue, s2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> operator /(T s1, XGaScalar<T> s2)
    {
        return new XGaScalar<T>(
            s2.Processor,
                
            s2.ScalarProcessor.Divide(s1, s2.ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(XGaScalar<T> s1, int s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2
        ).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(XGaScalar<T> s1, int s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2
        ).IsNotZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(int s1, XGaScalar<T> s2)
    {
        var processor = s2.Processor;

        return processor.ScalarProcessor.Subtract(
            s1,
            s2.ScalarValue
        ).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(int s1, XGaScalar<T> s2)
    {
        var processor = s2.Processor;

        return processor.ScalarProcessor.Subtract(
            s1,
            s2.ScalarValue
        ).IsNotZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(XGaScalar<T> s1, double s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2
        ).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(XGaScalar<T> s1, double s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2
        ).IsNotZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(double s1, XGaScalar<T> s2)
    {
        var processor = s2.Processor;

        return processor.ScalarProcessor.Subtract(
            s1,
            s2.ScalarValue
        ).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(double s1, XGaScalar<T> s2)
    {
        var processor = s2.Processor;

        return processor.ScalarProcessor.Subtract(
            s1,
            s2.ScalarValue
        ).IsNotZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(XGaScalar<T> s1, XGaScalar<T> s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2.ScalarValue
        ).IsNegative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(XGaScalar<T> s1, XGaScalar<T> s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2.ScalarValue
        ).IsPositive();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(XGaScalar<T> s1, int s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2
        ).IsNegative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(XGaScalar<T> s1, int s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2
        ).IsPositive();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(int s1, XGaScalar<T> s2)
    {
        var processor = s2.Processor;

        return processor.ScalarProcessor.Subtract(
            s1,
            s2.ScalarValue
        ).IsNegative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(int s1, XGaScalar<T> s2)
    {
        var processor = s2.Processor;

        return processor.ScalarProcessor.Subtract(
            s1,
            s2.ScalarValue
        ).IsPositive();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(XGaScalar<T> s1, double s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2
        ).IsNegative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(XGaScalar<T> s1, double s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2
        ).IsPositive();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(double s1, XGaScalar<T> s2)
    {
        var processor = s2.Processor;

        return processor.ScalarProcessor.Subtract(
            s1,
            s2.ScalarValue
        ).IsNegative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(double s1, XGaScalar<T> s2)
    {
        var processor = s2.Processor;

        return processor.ScalarProcessor.Subtract(
            s1,
            s2.ScalarValue
        ).IsPositive();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(XGaScalar<T> s1, XGaScalar<T> s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2.ScalarValue
        ).IsZeroOrNegative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(XGaScalar<T> s1, XGaScalar<T> s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2.ScalarValue
        ).IsZeroOrPositive();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(XGaScalar<T> s1, int s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2
        ).IsZeroOrNegative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(XGaScalar<T> s1, int s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2
        ).IsZeroOrPositive();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(int s1, XGaScalar<T> s2)
    {
        var processor = s2.Processor;

        return processor.ScalarProcessor.Subtract(
            s1,
            s2.ScalarValue
        ).IsZeroOrNegative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(int s1, XGaScalar<T> s2)
    {
        var processor = s2.Processor;

        return processor.ScalarProcessor.Subtract(
            s1,
            s2.ScalarValue
        ).IsZeroOrPositive();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(XGaScalar<T> s1, double s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2
        ).IsZeroOrNegative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(XGaScalar<T> s1, double s2)
    {
        var processor = s1.Processor;

        return processor.ScalarProcessor.Subtract(
            s1.ScalarValue,
            s2
        ).IsZeroOrPositive();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(double s1, XGaScalar<T> s2)
    {
        var processor = s2.Processor;

        return processor.ScalarProcessor.Subtract(
            s1,
            s2.ScalarValue
        ).IsZeroOrNegative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(double s1, XGaScalar<T> s2)
    {
        var processor = s2.Processor;

        return processor.ScalarProcessor.Subtract(
            s1,
            s2.ScalarValue
        ).IsZeroOrPositive();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Negative()
    {
        return IsZero
            ? this
            : new XGaScalar<T>(
                Processor, 
                ScalarProcessor.Negative(ScalarValue)
            );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Times(int scalar)
    {
        return Times(ScalarProcessor.ScalarFromNumber(scalar).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Times(double scalar)
    {
        return Times(ScalarProcessor.ScalarFromNumber(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Times(T scalarValue)
    {
        if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

        return ScalarProcessor.IsZero(scalarValue)
            ? Processor.ScalarZero
            : Processor.ScalarFromProduct(
                ScalarValue,
                scalarValue
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Times(Scalar<T> scalar)
    {
        return Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Times(IScalar<T> scalar)
    {
        return Times(scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Divide(int scalar)
    {
        return Divide(ScalarProcessor.ScalarFromNumber(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Divide(double scalar)
    {
        return Divide(ScalarProcessor.ScalarFromNumber(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Divide(T scalarValue)
    {
        if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

        if (ScalarProcessor.IsZero(scalarValue))
            throw new DivideByZeroException();

        return new XGaScalar<T>(
            Processor, 
            ScalarProcessor.Divide(ScalarValue, scalarValue)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Divide(IScalar<T> scalar)
    {
        return Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> CliffordConjugate()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Conjugate()
    {
        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> ENormSquared()
    {
        return IsZero
            ? ScalarProcessor.Zero
            : ScalarProcessor.Square(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> NormSquared()
    {
        return IsZero
            ? ScalarProcessor.Zero
            : ScalarProcessor.Square(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> ENorm()
    {
        return IsZero
            ? ScalarProcessor.Zero
            : ScalarProcessor.Abs(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> Norm()
    {
        return IsZero
            ? ScalarProcessor.Zero
            : ScalarProcessor.Abs(ScalarValue);
    }
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override Scalar<T> ESpSquared()
    //{
    //    return IsZero
    //        ? ScalarProcessor.Zero
    //        : ScalarProcessor.Square(ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override Scalar<T> SpSquared()
    //{
    //    return IsZero
    //        ? ScalarProcessor.Zero
    //        : ScalarProcessor.Square(ScalarValue);
    //}
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> EInverse()
    {
        return new XGaScalar<T>(
            Processor, 
            ScalarProcessor.Inverse(ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Inverse()
    {
        return new XGaScalar<T>(
            Processor, 
            ScalarProcessor.Inverse(ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> PseudoInverse()
    {
        return new XGaScalar<T>(
            Processor, 
            ScalarProcessor.Inverse(ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Add(XGaScalar<T> mv2)
    {
        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return new XGaScalar<T>(
            Processor, 
            ScalarProcessor.Add(ScalarValue, mv2.ScalarValue)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Add(XGaMultivector<T> mv2)
    {
        if (mv2 is XGaScalar<T> mv)
            return Add(mv);

        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateMultivectorComposer()
            .SetScalarTerm(ScalarValue)
            .AddMultivector(mv2)
            .GetMultivector();
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Subtract(XGaScalar<T> mv2)
    {
        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return new XGaScalar<T>(
            Processor, 
            ScalarProcessor.Subtract(ScalarValue, mv2.ScalarValue)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Subtract(XGaMultivector<T> mv2)
    {
        if (mv2 is XGaScalar<T> mv)
            return Subtract(mv);

        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateMultivectorComposer()
            .SetScalarTerm(ScalarValue)
            .SubtractMultivector(mv2)
            .GetMultivector();
    }

}