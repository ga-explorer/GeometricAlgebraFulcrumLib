using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Data;

public sealed record PointData3D<T> :
    ILinFloat64Vector3D
{
    public bool IsValid() =>
        !double.IsNaN(X) &&
        !double.IsNaN(Y) &&
        !double.IsNaN(Z) &&
        PointIndex >= 0;
        
    public int VSpaceDimensions 
        => 3;

    public Float64Scalar Item1 => X;

    public Float64Scalar Item2 => Y;

    public Float64Scalar Item3 => Z;
        
    public Float64Scalar X { get; }
        
    public Float64Scalar Y { get; }

    public Float64Scalar Z { get; }

    public int PointIndex { get; }

    public T DataValue { get; }


    public PointData3D(int pointIndex, double x, double y, double z, [NotNull] T dataValue)
    {
        X = x;
        Y = y;
        Z = z;
        PointIndex = pointIndex;
        DataValue = dataValue;

        Debug.Assert(IsValid());
    }

    public PointData3D(int pointIndex, ITriplet<Float64Scalar> point, [NotNull] T dataValue)
    {
        X = point.Item1;
        Y = point.Item2;
        Z = point.Item3;
        PointIndex = pointIndex;
        DataValue = dataValue;

        Debug.Assert(IsValid());
    }
        
    public PointData3D(int pointIndex, PointData3D<T> pointData)
    {
        X = pointData.Item1;
        Y = pointData.Item2;
        Z = pointData.Item3;
        PointIndex = pointIndex;
        DataValue = pointData.DataValue;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData3D<T> TranslateBy(double dx, double dy, double dz)
    {
        return new PointData3D<T>(
            PointIndex,
            X + dx,
            Y + dy,
            Z + dz,
            DataValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData3D<T> ScaleBy(double s)
    {
        return new PointData3D<T>(
            PointIndex,
            X * s,
            Y * s,
            Z * s,
            DataValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData3D<T> ScaleBy(double sx, double sy, double sz)
    {
        return new PointData3D<T>(
            PointIndex,
            X * sx,
            Y * sy,
            Z * sz,
            DataValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData3D<T> RotateXBy(double angle)
    {
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        return new PointData3D<T>(
            PointIndex,
            1,
            Y * cosAngle - Z * sinAngle,
            Y * sinAngle + Z * cosAngle,
            DataValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData3D<T> RotateYBy(double angle)
    {
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        return new PointData3D<T>(
            PointIndex,
            X * cosAngle + Z * sinAngle,
            1,
            -X * sinAngle + Z * cosAngle,
            DataValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PointData3D<T> RotateZBy(double angle)
    {
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        return new PointData3D<T>(
            PointIndex,
            X * cosAngle - Y * sinAngle,
            X * sinAngle + Y * cosAngle,
            1,
            DataValue
        );
    }
}