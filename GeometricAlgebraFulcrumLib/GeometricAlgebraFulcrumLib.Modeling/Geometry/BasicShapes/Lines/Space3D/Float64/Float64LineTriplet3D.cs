using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

public sealed class Float64LineTriplet3D
{
    public double Origin1X { get; }

    public double Origin1Y { get; }

    public double Origin1Z { get; }


    public double Origin2X { get; }

    public double Origin2Y { get; }

    public double Origin2Z { get; }


    public double Origin3X { get; }

    public double Origin3Y { get; }

    public double Origin3Z { get; }


    public double Direction1X { get; }

    public double Direction1Y { get; }

    public double Direction1Z { get; }


    public double Direction2X { get; }

    public double Direction2Y { get; }

    public double Direction2Z { get; }


    public double Direction3X { get; }

    public double Direction3Y { get; }

    public double Direction3Z { get; }


    public LinFloat64Vector3D Origin1
    {
        get { return LinFloat64Vector3D.Create(Origin1X, Origin1Y, Origin1Z); }
    }

    public LinFloat64Vector3D Origin2
    {
        get { return LinFloat64Vector3D.Create(Origin2X, Origin2Y, Origin2Z); }
    }

    public LinFloat64Vector3D Origin3
    {
        get { return LinFloat64Vector3D.Create(Origin3X, Origin3Y, Origin3Z); }
    }


    public LinFloat64Vector3D Direction1
    {
        get { return LinFloat64Vector3D.Create(Direction1X, Direction1Y, Direction1Z); }
    }

    public LinFloat64Vector3D Direction2
    {
        get { return LinFloat64Vector3D.Create(Direction2X, Direction2Y, Direction2Z); }
    }

    public LinFloat64Vector3D Direction3
    {
        get { return LinFloat64Vector3D.Create(Direction3X, Direction3Y, Direction3Z); }
    }


    public Float64Line3D Line1
    {
        get
        {
            return new Float64Line3D(
                Origin1X, Origin1Y, Origin1Z,
                Direction1X, Direction1Y, Direction1Z
            );
        }
    }

    public Float64Line3D Line2
    {
        get
        {
            return new Float64Line3D(
                Origin2X, Origin2Y, Origin2Z,
                Direction2X, Direction2Y, Direction2Z
            );
        }
    }

    public Float64Line3D Line3
    {
        get
        {
            return new Float64Line3D(
                Origin3X, Origin3Y, Origin3Z,
                Direction3X, Direction3Y, Direction3Z
            );
        }
    }


    internal Float64LineTriplet3D()
    {

    }
}