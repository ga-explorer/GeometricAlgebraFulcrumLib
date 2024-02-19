﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Structures.Data;

public sealed record PointData2D<T> :
    IFloat64Vector2D
{
    public bool IsValid()
    {
        return !double.IsNaN(X) &&
               !double.IsNaN(Y) &&
               PointIndex >= 0;
    }

    public double Item1 
        => X.Value;

    public double Item2 
        => Y.Value;
        
    public int VSpaceDimensions 
        => 2;

    public Float64Scalar X { get; }
        
    public Float64Scalar Y { get; }

    public int PointIndex { get; }
        
    public T DataValue { get; }


    public PointData2D(int pointIndex, double x, double y, [NotNull] T dataValue)
    {
        X = x;
        Y = y;
        PointIndex = pointIndex;
        DataValue = dataValue;

        Debug.Assert(IsValid());
    }

    public PointData2D(int pointIndex, IPair<double> point, [NotNull] T dataValue)
    {
        X = point.Item1;
        Y = point.Item2;
        PointIndex = pointIndex;
        DataValue = dataValue;

        Debug.Assert(IsValid());
    }
        
    public PointData2D(int index, PointData2D<T> pointData)
    {
        X = pointData.Item1;
        Y = pointData.Item2;
        PointIndex = index;
        DataValue = pointData.DataValue;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData2D<T> TranslateBy(double dx, double dy)
    {
        return new PointData2D<T>(
            PointIndex,
            X + dx,
            Y + dy,
            DataValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData2D<T> ScaleBy(double s)
    {
        return new PointData2D<T>(
            PointIndex,
            X * s,
            Y * s,
            DataValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData2D<T> ScaleBy(double sx, double sy)
    {
        return new PointData2D<T>(
            PointIndex,
            X * sx,
            Y * sy,
            DataValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData2D<T> RotateBy(double angle)
    {
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        return new PointData2D<T>(
            PointIndex,
            X * cosAngle - Y * sinAngle,
            X * sinAngle + Y * cosAngle,
            DataValue
        );
    }
}