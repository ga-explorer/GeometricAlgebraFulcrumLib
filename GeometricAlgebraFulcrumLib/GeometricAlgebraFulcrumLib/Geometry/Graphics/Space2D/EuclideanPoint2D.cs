﻿using System;
using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Geometry.Graphics.Space2D;

public sealed record EuclideanPoint2D : 
    IFloat64Vector2D
{
    public static EuclideanVector2D operator -(EuclideanPoint2D p1)
    {
        return new EuclideanVector2D(-p1.X, -p1.Y);
    }

    public static EuclideanPoint2D operator +(EuclideanPoint2D p1, EuclideanVector2D v2)
    {
        return new EuclideanPoint2D(
            p1.X + v2.X,
            p1.Y + v2.Y
        );
    }

    public static EuclideanPoint2D operator +(EuclideanPoint2D p1, EuclideanPoint2D p2)
    {
        return new EuclideanPoint2D(
            p1.X + p2.X,
            p1.Y + p2.Y
        );
    }

    public static EuclideanPoint2D operator -(EuclideanPoint2D p1, EuclideanVector2D v2)
    {
        return new EuclideanPoint2D(
            p1.X - v2.X,
            p1.Y - v2.Y
        );
    }

    public static EuclideanVector2D operator -(EuclideanPoint2D p1, EuclideanPoint2D p2)
    {
        return new EuclideanVector2D(
            p1.X - p2.X,
            p1.Y - p2.Y
        );
    }

    public static EuclideanPoint2D operator *(double s1, EuclideanPoint2D p2)
    {
        return new EuclideanPoint2D(
            s1 * p2.X,
            s1 * p2.Y
        );
    }

    public static EuclideanPoint2D operator *(EuclideanPoint2D p1, double s2)
    {
        return new EuclideanPoint2D(
            p1.X * s2,
            p1.Y * s2
        );
    }

    public static EuclideanPoint2D operator /(EuclideanPoint2D p1, double s2)
    {
        s2 = 1d / s2;

        return new EuclideanPoint2D(
            p1.X * s2,
            p1.Y * s2
        );
    }

    //public static bool operator ==(GeoEuclideanPoint2D p1, GeoEuclideanPoint2D p2)
    //{
    //    return p1.Equals(p2);
    //}

    //public static bool operator !=(GeoEuclideanPoint2D p1, GeoEuclideanPoint2D p2)
    //{
    //    return !p1.Equals(p2);
    //}


        
    public IScalarProcessor<double> ScalarProcessor 
        => ScalarProcessorOfFloat64.DefaultProcessor;
        
    public RGaFloat64Processor GeometricProcessor 
        => GraphicsUtils.GeometricProcessor;

    public Float64Scalar X { get; }

    public Float64Scalar Y { get; }
        
    public int VSpaceDimensions 
        => 2;

    public bool IsValid()
        => !double.IsNaN(X) && 
           !double.IsNaN(Y);

    public double Item1 
        => X.Value;

    public double Item2 
        => Y.Value;


    public EuclideanPoint2D(double x, double y)
    {
        X = x;
        Y = y;

        Debug.Assert(IsValid());
    }


    public double GetDistance(EuclideanPoint2D p2)
    {
        var x = X - p2.X;
        var y = Y - p2.Y;

        return Math.Sqrt(x * x + y * y);
    }

    public double GetDistanceSquared(EuclideanPoint2D p2)
    {
        var x = X - p2.X;
        var y = Y - p2.Y;

        return x * x + y * y;
    }

    public EuclideanVector2D AsVector()
    {
        return new EuclideanVector2D(X, Y);
    }
        
    public override string ToString()
    {
        return new StringBuilder()
            .Append("Point(")
            .Append(X.ToString("G"))
            .Append(", ")
            .Append(Y.ToString("G"))
            .Append(")")
            .ToString();
    }
}