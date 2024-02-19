using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space2D.Objects;

public abstract class E2DLine<T>
{
    public abstract IScalarProcessor<T> ScalarProcessor { get; }

    public abstract bool IsSegment { get; }

    public abstract bool IsTangent { get; }

    public abstract E2DPoint<T> Point1 { get; }

    public abstract E2DPoint<T> Point2 { get; }

    public abstract E2DVector<T> Direction12 { get; }

    public abstract E2DVector<T> Direction21 { get; }


    public abstract E2DLineSegment<T> ToSegment();

    public abstract E2DLineTangent<T> ToTangent();

    public abstract E2DPoint<T> GetPoint(T t);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DPoint<T> GetPoint(float t)
    {
        return GetPoint(
            ScalarProcessor.GetScalarFromNumber(t)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DPoint<T> GetPoint(double t)
    {
        return GetPoint(
            ScalarProcessor.GetScalarFromNumber(t)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DLineSegment<T> GetSegment(float t1, float t2)
    {
        return GetSegment(
            ScalarProcessor.GetScalarFromNumber(t1), 
            ScalarProcessor.GetScalarFromNumber(t2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DLineSegment<T> GetSegment(double t1, double t2)
    {
        return GetSegment(
            ScalarProcessor.GetScalarFromNumber(t1), 
            ScalarProcessor.GetScalarFromNumber(t2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DLineSegment<T> GetSegment(T t1, T t2)
    {
        return new E2DLineSegment<T>(
            GetPoint(t1), 
            GetPoint(t2)
        );
    }
}