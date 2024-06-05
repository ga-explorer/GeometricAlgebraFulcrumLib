using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Geometry.Euclidean.Space3D.Objects;

/// <summary>
/// A line tangent is a local 1D frame with one origin point and one direction vector
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class E3DLineTangent<T> :
    E3DLine<T>
{
    public E3DPoint<T> Origin 
        => Point1;

    public E3DVector<T> Direction 
        => Direction12;

    public override IScalarProcessor<T> ScalarProcessor 
        => Point1.ScalarProcessor;
    
    public override bool IsSegment 
        => false;

    public override bool IsTangent 
        => true;

    public override E3DPoint<T> Point1 { get; }

    public override E3DPoint<T> Point2 
        => Point1 + Direction12;

    public override E3DVector<T> Direction12 { get; }

    public override E3DVector<T> Direction21 
        => -Direction12;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal E3DLineTangent(E3DPoint<T> origin, E3DVector<T> direction)
    {
        Point1 = origin;
        Direction12 = direction;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DLineTangent<T> GetUnitDirectionLine()
    {
        return Direction12.AssumeUnit 
            ? this 
            : new E3DLineTangent<T>(Point1, Direction12.GetUnitVector());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DPoint<T> GetPoint(T t)
    {
        return Point1 + t * Direction12;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DLineSegment<T> ToSegment()
    {
        return new E3DLineSegment<T>(Point1, Point2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DLineTangent<T> ToTangent()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DLineTangent<T> MapScalars(Func<T, T> scalarMapping)
    {
        return new E3DLineTangent<T>(
            Point1.MapScalars(scalarMapping),
            Direction12.MapScalars(scalarMapping)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DLineTangent<T2> MapScalars<T2>(Func<T, T2> scalarMapping, IScalarProcessor<T2> scalarProcessor)
    {
        return new E3DLineTangent<T2>(
            Point1.MapScalars(scalarMapping, scalarProcessor),
            Direction12.MapScalars(scalarMapping, scalarProcessor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DLineTangent<T> MapPoints(Func<E3DPoint<T>, E3DPoint<T>> pointMapping)
    {
        var point1 = pointMapping(Point1);
        var point2 = pointMapping(Point2);

        return new E3DLineTangent<T>(
            point1,
            point2 - point1
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DLineTangent<T2> MapPoints<T2>(Func<E3DPoint<T>, E3DPoint<T2>> pointMapping)
    {
        var point1 = pointMapping(Point1);
        var point2 = pointMapping(Point2);

        return new E3DLineTangent<T2>(
            point1,
            point2 - point1
        );
    }
}