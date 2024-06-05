using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Geometry.Euclidean.Space2D.Objects;

public sealed record E2DPoint<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator -(E2DPoint<T> v1)
    {
        var processor = v1.ScalarProcessor;

        return new E2DPoint<T>(
            processor.Negative(v1.X),
            processor.Negative(v1.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator +(E2DPoint<T> v1, E2DVector<T> v2)
    {
        var processor = v1.ScalarProcessor;

        return new E2DPoint<T>(
            processor.Add(v1.X, v2.X),
            processor.Add(v1.Y, v2.Y)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator +(E2DVector<T> v1, E2DPoint<T> v2)
    {
        var processor = v1.ScalarProcessor;

        return new E2DPoint<T>(
            processor.Add(v1.X, v2.X),
            processor.Add(v1.Y, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator +(E2DPoint<T> v1, E2DPoint<T> v2)
    {
        var processor = v1.ScalarProcessor;

        return new E2DPoint<T>(
            processor.Add(v1.X, v2.X),
            processor.Add(v1.Y, v2.Y)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator -(E2DPoint<T> v1, E2DVector<T> v2)
    {
        var processor = v1.ScalarProcessor;

        return new E2DPoint<T>(
            processor.Subtract(v1.X, v2.X),
            processor.Subtract(v1.Y, v2.Y)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator -(E2DVector<T> v1, E2DPoint<T> v2)
    {
        var processor = v1.ScalarProcessor;

        return new E2DPoint<T>(
            processor.Subtract(v1.X, v2.X),
            processor.Subtract(v1.Y, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator -(E2DPoint<T> v1, E2DPoint<T> v2)
    {
        var processor = v1.ScalarProcessor;

        return new E2DVector<T>(
            
            processor.Subtract(v1.X, v2.X),
            processor.Subtract(v1.Y, v2.Y)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator *(int v1, E2DPoint<T> v2)
    {
        var processor = v2.ScalarProcessor;
        
        return new E2DPoint<T>(
            processor.Times(v1, v2.X),
            processor.Times(v1, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator *(E2DPoint<T> v1, int v2)
    {
        var processor = v1.ScalarProcessor;

        return new E2DPoint<T>(
            processor.Times(v1.X, v2),
            processor.Times(v1.Y, v2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator *(uint v1, E2DPoint<T> v2)
    {
        var processor = v2.ScalarProcessor;

        return new E2DPoint<T>(
            processor.Times(v1, v2.X),
            processor.Times(v1, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator *(E2DPoint<T> v1, uint v2)
    {
        var processor = v1.ScalarProcessor;

        return new E2DPoint<T>(
            processor.Times(v1.X, v2),
            processor.Times(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator *(float v1, E2DPoint<T> v2)
    {
        var processor = v2.ScalarProcessor;
        
        return new E2DPoint<T>(
            
            processor.Times(v1, v2.X),
            processor.Times(v1, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator *(E2DPoint<T> v1, long v2)
    {
        var processor = v1.ScalarProcessor;
        
        return new E2DPoint<T>(
            
            processor.Times(v1.X, v2),
            processor.Times(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator *(long v1, E2DPoint<T> v2)
    {
        var processor = v2.ScalarProcessor;
        

        return new E2DPoint<T>(
            
            processor.Times(v1, v2.X),
            processor.Times(v1, v2.Y)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator *(ulong v1, E2DPoint<T> v2)
    {
        var processor = v2.ScalarProcessor;
        

        return new E2DPoint<T>(
            
            processor.Times(v1, v2.X),
            processor.Times(v1, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator *(E2DPoint<T> v1, ulong v2)
    {
        var processor = v1.ScalarProcessor;
        

        return new E2DPoint<T>(
            
            processor.Times(v1.X, v2),
            processor.Times(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator *(E2DPoint<T> v1, float v2)
    {
        var processor = v1.ScalarProcessor;
        

        return new E2DPoint<T>(
            
            processor.Times(v1.X, v2),
            processor.Times(v1.Y, v2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator *(double v1, E2DPoint<T> v2)
    {
        var processor = v2.ScalarProcessor;
        

        return new E2DPoint<T>(
            
            processor.Times(v1, v2.X),
            processor.Times(v1, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator *(E2DPoint<T> v1, double v2)
    {
        var processor = v1.ScalarProcessor;
        

        return new E2DPoint<T>(
            
            processor.Times(v1.X, v2),
            processor.Times(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator *(T v1, E2DPoint<T> v2)
    {
        var processor = v2.ScalarProcessor;

        return new E2DPoint<T>(
            
            processor.Times(v1, v2.X),
            processor.Times(v1, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator *(E2DPoint<T> v1, T v2)
    {
        var processor = v1.ScalarProcessor;

        return new E2DPoint<T>(
            
            processor.Times(v1.X, v2),
            processor.Times(v1.Y, v2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator /(E2DPoint<T> v1, int v2)
    {
        var processor = v1.ScalarProcessor;
        

        return new E2DPoint<T>(
            
            processor.Divide(v1.X, v2),
            processor.Divide(v1.Y, v2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator /(E2DPoint<T> v1, uint v2)
    {
        var processor = v1.ScalarProcessor;
        

        return new E2DPoint<T>(
            
            processor.Divide(v1.X, v2),
            processor.Divide(v1.Y, v2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator /(E2DPoint<T> v1, long v2)
    {
        var processor = v1.ScalarProcessor;
        

        return new E2DPoint<T>(
            
            processor.Divide(v1.X, v2),
            processor.Divide(v1.Y, v2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator /(E2DPoint<T> v1, ulong v2)
    {
        var processor = v1.ScalarProcessor;
        

        return new E2DPoint<T>(
            
            processor.Divide(v1.X, v2),
            processor.Divide(v1.Y, v2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator /(E2DPoint<T> v1, float v2)
    {
        var processor = v1.ScalarProcessor;
        

        return new E2DPoint<T>(
            
            processor.Divide(v1.X, v2),
            processor.Divide(v1.Y, v2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator /(E2DPoint<T> v1, double v2)
    {
        var processor = v1.ScalarProcessor;
        

        return new E2DPoint<T>(
            
            processor.Divide(v1.X, v2),
            processor.Divide(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DPoint<T> operator /(E2DPoint<T> v1, T v2)
    {
        var processor = v1.ScalarProcessor;

        return new E2DPoint<T>(
            
            processor.Divide(v1.X, v2),
            processor.Divide(v1.Y, v2)
        );
    }

    public IScalarProcessor<T> ScalarProcessor { get; }

    public T X { get; }

    public T Y { get; }
        
    public Scalar<T> XScalar 
        => ScalarProcessor.ScalarFromValue(X);

    public Scalar<T> YScalar 
        => ScalarProcessor.ScalarFromValue(Y);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal E2DPoint(IScalarProcessor<T> scalarProcessor, T x, T y)
    {
        ScalarProcessor = scalarProcessor;
        X = x;
        Y = y;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal E2DPoint(IScalar<T> x, IScalar<T> y)
    {
        ScalarProcessor = x.ScalarProcessor;
        X = x.ScalarValue;
        Y = y.ScalarValue;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DVector<T> ToE2DVector(bool assumeUnit = false)
    {
        return new E2DVector<T>(ScalarProcessor, X, Y, assumeUnit);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetDistance(E2DPoint<T> point)
    {
        var x = ScalarProcessor.Subtract(X, point.X);
        var y = ScalarProcessor.Subtract(Y, point.Y);

        return (x.Square() + y.Square()).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetDistanceSquared(E2DPoint<T> point)
    {
        var x = ScalarProcessor.Subtract(X, point.X);
        var y = ScalarProcessor.Subtract(Y, point.Y);

        return x.Square() + y.Square();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DLineTangent<T> CreateLineTangent(E2DVector<T> direction)
    {
        return new E2DLineTangent<T>(this, direction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DLineSegment<T> CreateLineSegmentToPoint(E2DPoint<T> point2)
    {
        return new E2DLineSegment<T>(this, point2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DLineSegment<T> CreateLineSegmentFromPoint(E2DPoint<T> point1)
    {
        return new E2DLineSegment<T>(point1, this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DPlaneTangent<T> CreatePlaneTangent(E2DVector<T> direction12, E2DVector<T> direction13)
    {
        return new E2DPlaneTangent<T>(this, direction12, direction13);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DPlaneSegment<T> CreatePlaneSegment(E2DPoint<T> point2, E2DPoint<T> point3)
    {
        return new E2DPlaneSegment<T>(this, point2, point3);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DPoint<T> MapScalars(Func<T, T> scalarMapping)
    {
        return new E2DPoint<T>(
            ScalarProcessor,
            scalarMapping(X),
            scalarMapping(Y)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DPoint<T2> MapScalars<T2>(Func<T, T2> scalarMapping, IScalarProcessor<T2> scalarProcessor)
    {
        return new E2DPoint<T2>(
            scalarProcessor,
            scalarMapping(X),
            scalarMapping(Y)
        );
    }
}