using System.Collections;
using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Core.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space2D;

/// <summary>
/// An immutable 2-tuple of double precision numbers
/// </summary>
public sealed record LinComplexVector2D<T> :
    ILinComplexVector2D<T>,
    IEnumerable<ComplexNumber<T>>
{
    public static LinComplexVector2D<T> Zero(IScalarProcessor<T> scalarProcessor)
    {
        var zero = scalarProcessor.CreateComplexNumberZero();

        return new LinComplexVector2D<T>(zero, zero);
    }


    public static LinComplexVector2D<T> operator -(LinComplexVector2D<T> v1)
    {
        return new LinComplexVector2D<T>(-v1.X, -v1.Y);
    }

    public static LinComplexVector2D<T> operator +(LinComplexVector2D<T> v1, LinComplexVector2D<T> v2)
    {
        return new LinComplexVector2D<T>(v1.X + v2.X, v1.Y + v2.Y);
    }

    public static LinComplexVector2D<T> operator -(LinComplexVector2D<T> v1, LinComplexVector2D<T> v2)
    {
        return new LinComplexVector2D<T>(v1.X - v2.X, v1.Y - v2.Y);
    }

    public static LinComplexVector2D<T> operator *(LinComplexVector2D<T> v1, double s)
    {
        return new LinComplexVector2D<T>(v1.X * s, v1.Y * s);
    }
    
    public static LinComplexVector2D<T> operator *(LinComplexVector2D<T> v1, T s)
    {
        Debug.Assert(s is not null);

        return new LinComplexVector2D<T>(v1.X * s, v1.Y * s);
    }
    
    public static LinComplexVector2D<T> operator *(LinComplexVector2D<T> v1, Scalar<T> s)
    {
        return new LinComplexVector2D<T>(v1.X * s, v1.Y * s);
    }

    public static LinComplexVector2D<T> operator *(double s, LinComplexVector2D<T> v1)
    {
        return new LinComplexVector2D<T>(v1.X * s, v1.Y * s);
    }
    
    public static LinComplexVector2D<T> operator *(T s, LinComplexVector2D<T> v1)
    {
        Debug.Assert(s is not null);

        return new LinComplexVector2D<T>(v1.X * s, v1.Y * s);
    }
    
    public static LinComplexVector2D<T> operator *(Scalar<T> s, LinComplexVector2D<T> v1)
    {
        return new LinComplexVector2D<T>(v1.X * s, v1.Y * s);
    }
    
    public static LinComplexVector2D<T> operator /(LinComplexVector2D<T> v1, double s)
    {
        return new LinComplexVector2D<T>(v1.X / s, v1.Y / s);
    }
    
    public static LinComplexVector2D<T> operator /(LinComplexVector2D<T> v1, T s)
    {
        Debug.Assert(s is not null);

        return new LinComplexVector2D<T>(v1.X / s, v1.Y / s);
    }
    
    public static LinComplexVector2D<T> operator /(LinComplexVector2D<T> v1, Scalar<T> s)
    {
        return new LinComplexVector2D<T>(v1.X / s, v1.Y / s);
    }

    public static LinComplexVector2D<T> operator /(LinComplexVector2D<T> v1, ComplexNumber<T> s)
    {
        s = 1.0d / s;

        return new LinComplexVector2D<T>(v1.X * s, v1.Y * s);
    }


    public IScalarProcessor<T> ScalarProcessor 
        => X.ScalarProcessor;
    
    public int VSpaceDimensions 
        => 2;

    public ComplexNumber<T> X { get; }

    public ComplexNumber<T> Y { get; }

    public ComplexNumber<T> Item1 
        => X;

    public ComplexNumber<T> Item2 
        => Y;

    /// <summary>
    /// Get the ith component of this tuple
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public ComplexNumber<T> this[int index]
    {
        get
        {
            Debug.Assert(index is 0 or 1);

            return index switch
            {
                0 => X,
                1 => Y,
                _ => ScalarProcessor.CreateComplexNumberZero()
            };
        }
    }

    public bool IsValid()
    {
        return !X.IsValid() && !Y.IsValid();
    }


    public LinComplexVector2D(IScalarProcessor<T> scalarProcessor, T x, T y)
    {
        X = new ComplexNumber<T>(scalarProcessor, x);
        Y = new ComplexNumber<T>(scalarProcessor, y);

        Debug.Assert(IsValid());
    }

    public LinComplexVector2D(ComplexNumber<T> x, ComplexNumber<T> y)
    {
        X = x;
        Y = y;

        Debug.Assert(IsValid());
    }

    public LinComplexVector2D(ILinComplexVector2D<T> tuple)
    {
        X = tuple.X;
        Y = tuple.Y;

        Debug.Assert(IsValid());
    }


    public IEnumerator<ComplexNumber<T>> GetEnumerator()
    {
        yield return X;
        yield return Y;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return X;
        yield return Y;
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append("(")
            .AppendComplexNumber(ScalarProcessor, X.RealValue, X.ImaginaryValue)
            .Append(", ")
            .AppendComplexNumber(ScalarProcessor, Y.RealValue, Y.ImaginaryValue)
            .Append(")")
            .ToString();
    }
}