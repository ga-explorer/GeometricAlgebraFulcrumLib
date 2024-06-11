using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Euclidean.Space2D.Objects;

/// <summary>
/// A line tangent is a local 1D frame with one origin point and one direction vector
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class E2DLineTangent<T> :
    E2DLine<T>
{
    public E2DPoint<T> Origin
        => Point1;

    public E2DVector<T> Direction
        => Direction12;

    public override IScalarProcessor<T> ScalarProcessor
        => Point1.ScalarProcessor;

    public override bool IsSegment
        => false;

    public override bool IsTangent
        => true;

    public override E2DPoint<T> Point1 { get; }

    public override E2DPoint<T> Point2
        => Point1 + Direction12;

    public override E2DVector<T> Direction12 { get; }

    public override E2DVector<T> Direction21
        => -Direction12;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal E2DLineTangent(E2DPoint<T> origin, E2DVector<T> direction)
    {
        Point1 = origin;
        Direction12 = direction;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DLineTangent<T> GetUnitDirectionLine()
    {
        return Direction12.AssumeUnit
            ? this
            : new E2DLineTangent<T>(Point1, Direction12.GetUnitVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E2DPoint<T> GetPoint(T t)
    {
        return Point1 + t * Direction12;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E2DLineSegment<T> ToSegment()
    {
        return new E2DLineSegment<T>(Point1, Point2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E2DLineTangent<T> ToTangent()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DLineTangent<T> MapScalars(Func<T, T> scalarMapping)
    {
        return new E2DLineTangent<T>(
            Point1.MapScalars(scalarMapping),
            Direction12.MapScalars(scalarMapping)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DLineTangent<T2> MapScalars<T2>(Func<T, T2> scalarMapping, IScalarProcessor<T2> scalarProcessor)
    {
        return new E2DLineTangent<T2>(
            Point1.MapScalars(scalarMapping, scalarProcessor),
            Direction12.MapScalars(scalarMapping, scalarProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DLineTangent<T> MapPoints(Func<E2DPoint<T>, E2DPoint<T>> pointMapping)
    {
        var point1 = pointMapping(Point1);
        var point2 = pointMapping(Point2);

        return new E2DLineTangent<T>(
            point1,
            point2 - point1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DLineTangent<T2> MapPoints<T2>(Func<E2DPoint<T>, E2DPoint<T2>> pointMapping)
    {
        var point1 = pointMapping(Point1);
        var point2 = pointMapping(Point2);

        return new E2DLineTangent<T2>(
            point1,
            point2 - point1
        );
    }
}