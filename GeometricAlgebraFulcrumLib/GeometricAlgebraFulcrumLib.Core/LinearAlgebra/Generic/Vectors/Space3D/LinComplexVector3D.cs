using System.Collections;
using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Core.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space3D;

/// <summary>
/// An immutable 2-tuple of double precision numbers
/// </summary>
public sealed record LinComplexVector3D<T> :
    ILinComplexVector3D<T>,
    IEnumerable<ComplexNumber<T>>
{
    public static LinComplexVector3D<T> Zero(IScalarProcessor<T> scalarProcessor)
    {
        var zero = scalarProcessor.CreateComplexNumberZero();

        return new LinComplexVector3D<T>(zero, zero, zero);
    }
        


    public static LinComplexVector3D<T> operator -(LinComplexVector3D<T> v1)
    {
        return new LinComplexVector3D<T>(
            -v1.X,
            -v1.Y,
            -v1.Z
        );
    }

    public static LinComplexVector3D<T> operator +(LinComplexVector3D<T> v1, LinComplexVector3D<T> v2)
    {
        return new LinComplexVector3D<T>(
            v1.X + v2.X,
            v1.Y + v2.Y,
            v1.Z + v2.Z
        );
    }

    public static LinComplexVector3D<T> operator -(LinComplexVector3D<T> v1, LinComplexVector3D<T> v2)
    {
        return new LinComplexVector3D<T>(
            v1.X - v2.X,
            v1.Y - v2.Y,
            v1.Z - v2.Z
        );
    }

    public static LinComplexVector3D<T> operator *(LinComplexVector3D<T> v1, double s)
    {
        return new LinComplexVector3D<T>(
            v1.X * s,
            v1.Y * s,
            v1.Z * s
        );
    }

    public static LinComplexVector3D<T> operator *(double s, LinComplexVector3D<T> v1)
    {
        return new LinComplexVector3D<T>(
            v1.X * s,
            v1.Y * s,
            v1.Z * s
        );
    }

    public static LinComplexVector3D<T> operator /(LinComplexVector3D<T> v1, ComplexNumber<T> s)
    {
        s = s.Inverse();

        return new LinComplexVector3D<T>(
            v1.X * s,
            v1.Y * s,
            v1.Z * s
        );
    }


    public IScalarProcessor<T> ScalarProcessor 
        => X.ScalarProcessor;
    
    public int VSpaceDimensions 
        => 3;

    public ComplexNumber<T> X { get; }

    public ComplexNumber<T> Y { get; }

    public ComplexNumber<T> Z { get; }

    public ComplexNumber<T> Item1 => X;

    public ComplexNumber<T> Item2 => Y;

    public ComplexNumber<T> Item3 => Z;

    /// <summary>
    /// Get the ith component of this tuple
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public ComplexNumber<T> this[int index]
    {
        get
        {
            Debug.Assert(index is 0 or 1 or 2);

            return index switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                _ => ScalarProcessor.CreateComplexNumberZero()
            };
        }

    }

    public bool IsValid()
    {
        return X.IsValid() && Y.IsValid() && Z.IsValid();
    }


    public LinComplexVector3D(Scalar<T> x, Scalar<T> y, Scalar<T> z)
    {
        X = x.ScalarProcessor.CreateComplexNumberReal(x);
        Y = x.ScalarProcessor.CreateComplexNumberReal(y);
        Z = x.ScalarProcessor.CreateComplexNumberReal(z);

        Debug.Assert(IsValid());
    }

    public LinComplexVector3D(ComplexNumber<T> x, ComplexNumber<T> y, ComplexNumber<T> z)
    {
        X = x;
        Y = y;
        Z = z;

        Debug.Assert(IsValid());
    }

    public LinComplexVector3D(ILinComplexVector3D<T> tuple)
    {
        X = tuple.X;
        Y = tuple.Y;
        Z = tuple.Z;

        Debug.Assert(IsValid());
    }


    public IEnumerator<ComplexNumber<T>> GetEnumerator()
    {
        yield return X;
        yield return Y;
        yield return Z;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return X;
        yield return Y;
        yield return Z;
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append("(")
            .AppendComplexNumber(ScalarProcessor, X.RealValue, X.ImaginaryValue)
            .Append(", ")
            .AppendComplexNumber(ScalarProcessor, Y.RealValue, Y.ImaginaryValue)
            .Append(", ")
            .AppendComplexNumber(ScalarProcessor, Z.RealValue, Z.ImaginaryValue)
            .Append(")")
            .ToString();
    }

}