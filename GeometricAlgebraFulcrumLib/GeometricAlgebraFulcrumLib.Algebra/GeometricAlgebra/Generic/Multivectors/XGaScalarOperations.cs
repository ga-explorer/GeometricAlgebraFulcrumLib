using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;
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
    public XGaScalar<T> MapScalar(Func<T, T> scalarMapping)
    {
        return IsZero
            ? this
            : new XGaScalar<T>(
                Processor, 
                scalarMapping(ScalarValue)
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar MapScalar(XGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        return IsZero
            ? processor.ScalarZero
            : processor.Scalar(
                scalarMapping(ScalarValue)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T1> MapScalar<T1>(XGaProcessor<T1> processor, Func<T, T1> scalarMapping)
    {
        return IsZero
            ? processor.ScalarZero
            : processor.Scalar(
                scalarMapping(ScalarValue)
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar MapScalar(XGaFloat64Processor processor, Func<IndexSet, T, double> scalarMapping)
    {
        return IsZero
            ? processor.ScalarZero
            : processor.Scalar(
                scalarMapping(Processor.GetBasisScalarId(), ScalarValue)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> MapScalar(Func<IndexSet, T, T> scalarMapping)
    {
        return IsZero
            ? this
            : new XGaScalar<T>(
                Processor, 
                scalarMapping(
                    Processor.GetBasisScalarId(), 
                    ScalarValue
                )
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T1> MapScalar<T1>(XGaProcessor<T1> processor, Func<IndexSet, T, T1> scalarMapping)
    {
        return IsZero
            ? processor.ScalarZero
            : processor.Scalar(
                scalarMapping(Processor.GetBasisScalarId(), ScalarValue)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Negative()
    {
        return IsZero
            ? this
            : new XGaScalar<T>(
                Processor, 
                ScalarProcessor.Negative(ScalarValue)
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Times(T scalarValue)
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
    public XGaScalar<T> Times(IScalar<T> scalar)
    {
        return Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Divide(T scalarValue)
    {
        if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

        if (ScalarProcessor.IsZero(scalarValue))
            return Processor.ScalarZero;

        return new XGaScalar<T>(
            Processor, 
            ScalarProcessor.Divide(ScalarValue, scalarValue)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Divide(IScalar<T> scalar)
    {
        return Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> CliffordConjugate()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Conjugate()
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> ESpSquared()
    {
        return IsZero
            ? ScalarProcessor.Zero
            : ScalarProcessor.Square(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> SpSquared()
    {
        return IsZero
            ? ScalarProcessor.Zero
            : ScalarProcessor.Square(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> EInverse()
    {
        return new XGaScalar<T>(
            Processor, 
            ScalarProcessor.Inverse(ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Inverse()
    {
        return new XGaScalar<T>(
            Processor, 
            ScalarProcessor.Inverse(ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> PseudoInverse()
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
            .CreateComposer()
            .SetScalarTerm(ScalarValue)
            .AddMultivector(mv2)
            .GetSimpleMultivector();
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
            .CreateComposer()
            .SetScalarTerm(ScalarValue)
            .SubtractMultivector(mv2)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Op(XGaScalar<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Op(XGaVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> Op(XGaBivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> Op(XGaHigherKVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Op(XGaKVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Op(XGaGradedMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> Op(XGaUniformMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Op(XGaMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> EGp(XGaScalar<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> EGp(XGaVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> EGp(XGaBivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> EGp(XGaHigherKVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> EGp(XGaKVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> EGp(XGaGradedMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> EGp(XGaUniformMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> EGp(XGaMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Gp(XGaScalar<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Gp(XGaVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> Gp(XGaBivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> Gp(XGaHigherKVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> Gp(XGaKVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Gp(XGaGradedMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> Gp(XGaUniformMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Gp(XGaMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ELcp(XGaScalar<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> ELcp(XGaVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> ELcp(XGaBivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> ELcp(XGaHigherKVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> ELcp(XGaKVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> ELcp(XGaGradedMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> ELcp(XGaUniformMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> ELcp(XGaMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Lcp(XGaScalar<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Lcp(XGaVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> Lcp(XGaBivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> Lcp(XGaHigherKVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Lcp(XGaKVector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Lcp(XGaGradedMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> Lcp(XGaUniformMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Lcp(XGaMultivector<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ERcp(XGaScalar<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ERcp(XGaVector<T> mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ERcp(XGaBivector<T> mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ERcp(XGaHigherKVector<T> mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> ERcp(XGaKVector<T> mv2)
    {
        return mv2 is XGaScalar<T> mv
            ? mv.Times(ScalarValue)
            : Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ERcp(XGaGradedMultivector<T> mv2)
    {
        return mv2.TryGetKVector(0, out var mv)
            ? Times(((XGaScalar<T>) mv).ScalarValue)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> ERcp(XGaMultivector<T> mv2)
    {
        return Times(mv2.Scalar().ScalarValue);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Rcp(XGaScalar<T> mv2)
    {
        return mv2.Times(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Rcp(XGaVector<T> mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Rcp(XGaBivector<T> mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Rcp(XGaHigherKVector<T> mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Rcp(XGaKVector<T> mv2)
    {
        return mv2 is XGaScalar<T> mv
            ? mv.Times(ScalarValue)
            : Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Rcp(XGaGradedMultivector<T> mv2)
    {
        return mv2.TryGetKVector(0, out var mv)
            ? Times(((XGaScalar<T>) mv).ScalarValue)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Rcp(XGaMultivector<T> mv2)
    {
        return Times(mv2.Scalar().ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaVector<T> mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaBivector<T> mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaHigherKVector<T> mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaKVector<T> mv2)
    {
        return mv2 is XGaScalar<T> mv
            ? Times(mv.ScalarValue)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaGradedMultivector<T> mv2)
    {
        return mv2.TryGetKVector(0, out var mv)
            ? Times(((XGaScalar<T>) mv).ScalarValue)
            : Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaUniformMultivector<T> mv2)
    {
        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv2)
            .GetXGaScalar(Processor);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaVector<T> mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaBivector<T> mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaHigherKVector<T> mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaKVector<T> mv2)
    {
        return mv2 is XGaScalar<T> mv
            ? Times(mv.ScalarValue)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaGradedMultivector<T> mv2)
    {
        return mv2.TryGetKVector(0, out var mv)
            ? Times(((XGaScalar<T>) mv).ScalarValue)
            : Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaUniformMultivector<T> mv2)
    {
        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv2)
            .GetXGaScalar(Processor);
    }

}