using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Tuples;

public sealed class MutableComplexTuple3D : IComplexVector3D, IEnumerable<Complex>
{
    public double RealX { get; set; }

    public double RealY { get; set; }

    public double RealZ { get; set; }


    public double ImagX { get; set; }

    public double ImagY { get; set; }

    public double ImagZ { get; set; }


    public Complex X => new Complex(RealX, ImagX);

    public Complex Y => new Complex(RealY, ImagY);

    public Complex Z => new Complex(RealZ, ImagZ);

    public Complex Item1 => X;

    public Complex Item2 => Y;

    public Complex Item3 => Z;


    public bool IsValid()
    {
        return !double.IsNaN(RealX) &&
               !double.IsNaN(ImagX) &&
               !double.IsNaN(RealY) &&
               !double.IsNaN(ImagY) &&
               !double.IsNaN(RealZ) &&
               !double.IsNaN(ImagZ);
    }


    /// <summary>
    /// Get or set the ith component of this tuple
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Complex this[int index]
    {
        get
        {
            Debug.Assert(index is 0 or 1 or 2);

            if (index == 0) return X;
            if (index == 1) return Y;
            if (index == 2) return Z;

            return 0.0d;
        }
        set
        {
            Debug.Assert(!value.IsNaN());
            Debug.Assert(index is 0 or 1 or 2);

            if (index == 0)
            {
                RealX = value.Real;
                ImagX = value.Imaginary;
            }
            else if (index == 1)
            {
                RealY = value.Real;
                ImagY = value.Imaginary;
            }
            else if (index == 2)
            {
                RealZ = value.Real;
                ImagZ = value.Imaginary;
            }
        }
    }


    public MutableComplexTuple3D()
    {
    }

    public MutableComplexTuple3D(double x, double y, double z)
    {
        RealX = x;
        RealY = y;
        RealZ = z;

        Debug.Assert(IsValid());
    }

    public MutableComplexTuple3D(Complex x, Complex y, Complex z)
    {
        RealX = x.Real;
        RealY = y.Real;
        RealZ = z.Real;

        ImagX = x.Imaginary;
        ImagY = y.Imaginary;
        ImagZ = z.Imaginary;

        Debug.Assert(IsValid());
    }

    public MutableComplexTuple3D(IFloat64Vector3D tuple)
    {
        RealX = tuple.X;
        RealY = tuple.Y;
        RealZ = tuple.Z;

        Debug.Assert(IsValid());
    }

    public MutableComplexTuple3D(IComplexVector3D tuple)
    {
        RealX = tuple.X.Real;
        RealY = tuple.Y.Real;
        RealZ = tuple.Z.Real;

        ImagX = tuple.X.Imaginary;
        ImagY = tuple.Y.Imaginary;
        ImagZ = tuple.Z.Imaginary;

        Debug.Assert(IsValid());
    }


    public MutableComplexTuple3D SetTuple(double x, double y, double z)
    {
        RealX = x;
        RealY = y;
        RealZ = z;

        Debug.Assert(IsValid());

        return this;
    }

    public MutableComplexTuple3D SetRealTuple(double x, double y, double z)
    {
        RealX = x;
        RealY = y;
        RealZ = z;

        Debug.Assert(IsValid());

        return this;
    }

    public MutableComplexTuple3D SetImaginaryTuple(double x, double y, double z)
    {
        ImagX = x;
        ImagY = y;
        ImagZ = z;

        Debug.Assert(IsValid());

        return this;
    }

    public MutableComplexTuple3D SetTuple(Complex x, Complex y, Complex z)
    {
        RealX = x.Real;
        RealY = y.Real;
        RealZ = z.Real;

        ImagX = x.Imaginary;
        ImagY = y.Imaginary;
        ImagZ = z.Imaginary;

        Debug.Assert(IsValid());

        return this;
    }

    public MutableComplexTuple3D SetTuple(IFloat64Vector3D tuple)
    {
        RealX = tuple.X;
        RealY = tuple.Y;
        RealZ = tuple.Z;

        ImagX = 0;
        ImagY = 0;
        ImagZ = 0;

        Debug.Assert(IsValid());

        return this;
    }

    public MutableComplexTuple3D SetRealTuple(IFloat64Vector3D tuple)
    {
        RealX = tuple.X;
        RealY = tuple.Y;
        RealZ = tuple.Z;

        Debug.Assert(IsValid());

        return this;
    }

    public MutableComplexTuple3D SetImaginaryTuple(IFloat64Vector3D tuple)
    {
        ImagX = tuple.X;
        ImagY = tuple.Y;
        ImagZ = tuple.Z;

        Debug.Assert(IsValid());

        return this;
    }

    public MutableComplexTuple3D SetTuple(IComplexVector3D tuple)
    {
        RealX = tuple.X.Real;
        RealY = tuple.Y.Real;
        RealZ = tuple.Z.Real;

        ImagX = tuple.X.Imaginary;
        ImagY = tuple.Y.Imaginary;
        ImagZ = tuple.Z.Imaginary;

        Debug.Assert(IsValid());

        return this;
    }


    public IEnumerator<Complex> GetEnumerator()
    {
        yield return new Complex(RealX, ImagX);
        yield return new Complex(RealY, ImagY);
        yield return new Complex(RealZ, ImagZ);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return new Complex(RealX, ImagX);
        yield return new Complex(RealY, ImagY);
        yield return new Complex(RealZ, ImagZ);
    }


    public override string ToString()
    {
        return new StringBuilder()
            .Append("(")
            .AppendComplexNumber(RealX, ImagX, "G")
            .Append(", ")
            .AppendComplexNumber(RealY, ImagY, "G")
            .Append(", ")
            .AppendComplexNumber(RealZ, ImagZ, "G")
            .Append(")")
            .ToString();
    }
}