using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;

public sealed class LinFloat64VectorComposer2D :
    ILinFloat64Vector2D
{
    private double _x;
    public double X 
    { 
        get => _x;
        private set => _x = value.IsValid() ? value : throw new InvalidOperationException();
    }

    private double _y;
    public double Y
    {
        get => _y; 
        private set => _y = value.IsValid() ? value : throw new InvalidOperationException();
    }

    public int VSpaceDimensions
        => 2;

    public double Item1
        => X;

    public double Item2
        => Y;

    /// <summary>
    /// Get or set the ith component of this tuple
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public double this[int index]
    {
        get
        {
            return index switch
            {
                0 => X,
                1 => Y,
                _ => 0.0d
            };
        }
        set
        {
            if (index == 0) 
                X = value;
            else if (index == 1) 
                Y = value;
            else 
                throw new IndexOutOfRangeException();
        }
    }


    public LinFloat64VectorComposer2D()
    {
    }

    public LinFloat64VectorComposer2D(double x, double y)
    {
        X = x;
        Y = y;

        Debug.Assert(IsValid());
    }

    public LinFloat64VectorComposer2D(IPair<double> tuple)
    {
        X = tuple.Item1;
        Y = tuple.Item2;

        Debug.Assert(IsValid());
    }
    
    public LinFloat64VectorComposer2D(LinFloat64Vector2D tuple)
    {
        X = tuple.X;
        Y = tuple.Y;

        Debug.Assert(IsValid());
    }


    public LinFloat64VectorComposer2D SetTuple(double x, double y)
    {
        X = x;
        Y = y;

        return this;
    }

    public LinFloat64VectorComposer2D SetTuple(ILinFloat64Vector2D tuple)
    {
        X = tuple.X;
        Y = tuple.Y;

        return this;
    }
    
    public bool IsValid()
    {
        return X.IsValid() &&
               Y.IsValid();
    }

    public LinFloat64VectorComposer2D SetToZero()
    {
        X = 0;
        Y = 0;

        return this;
    }

    public LinFloat64VectorComposer2D SetDirection(ILinFloat64Vector2D direction)
    {
        var oldLength2 =
            direction.X * direction.X +
            direction.Y * direction.Y;

        var newLength2 = X * X + Y * Y;

        var s = Math.Sqrt(newLength2 / oldLength2);

        X = direction.X * s;
        Y = direction.Y * s;

        Debug.Assert(IsValid());

        return this;
    }

    public LinFloat64VectorComposer2D SetLength(double d)
    {
        var s = d / Math.Sqrt(X * X + Y * Y);

        X = X * s;
        Y = Y * s;

        Debug.Assert(IsValid());

        return this;
    }

    public LinFloat64VectorComposer2D SetLengthToUnit()
    {
        var s = 1 / Math.Sqrt(X * X + Y * Y);

        X = X * s;
        Y = Y * s;

        Debug.Assert(IsValid());

        return this;
    }

    public LinFloat64VectorComposer2D SetToNegative()
    {
        X = -X;
        Y = -Y;

        return this;
    }

    public LinFloat64VectorComposer2D SetToSameSide(ILinFloat64Vector2D direction)
    {
        if (!(X * direction.X + Y * direction.Y).IsNegative())
            return this;

        X = -X;
        Y = -Y;

        return this;
    }

    public LinFloat64VectorComposer2D SetToOtherSide(ILinFloat64Vector2D direction)
    {
        if (!(X * direction.X + Y * direction.Y).IsPositive())
            return this;

        X = -X;
        Y = -Y;

        return this;
    }


    public override string ToString()
    {
        return new StringBuilder()
            .Append("(")
            .Append(X.ToString("G"))
            .Append(", ")
            .Append(Y.ToString("G"))
            .Append(")")
            .ToString();
    }
}