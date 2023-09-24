using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects
{
    public sealed record E3DPoint<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator -(E3DPoint<T> v1)
        {
            var processor = v1.ScalarProcessor;

            return new E3DPoint<T>(
                processor, 
                processor.Negative(v1.X),
                processor.Negative(v1.Y),
                processor.Negative(v1.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator +(E3DPoint<T> v1, E3DVector<T> v2)
        {
            var processor = v1.ScalarProcessor;

            return new E3DPoint<T>(
                processor, 
                processor.Add(v1.X, v2.X),
                processor.Add(v1.Y, v2.Y),
                processor.Add(v1.Z, v2.Z)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator +(E3DVector<T> v1, E3DPoint<T> v2)
        {
            var processor = v1.ScalarProcessor;

            return new E3DPoint<T>(
                processor, 
                processor.Add(v1.X, v2.X),
                processor.Add(v1.Y, v2.Y),
                processor.Add(v1.Z, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator +(E3DPoint<T> v1, E3DPoint<T> v2)
        {
            var processor = v1.ScalarProcessor;

            return new E3DPoint<T>(
                processor, 
                processor.Add(v1.X, v2.X),
                processor.Add(v1.Y, v2.Y),
                processor.Add(v1.Z, v2.Z)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator -(E3DPoint<T> v1, E3DVector<T> v2)
        {
            var processor = v1.ScalarProcessor;

            return new E3DPoint<T>(
                processor, 
                processor.Subtract(v1.X, v2.X),
                processor.Subtract(v1.Y, v2.Y),
                processor.Subtract(v1.Z, v2.Z)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator -(E3DVector<T> v1, E3DPoint<T> v2)
        {
            var processor = v1.ScalarProcessor;

            return new E3DPoint<T>(
                processor, 
                processor.Subtract(v1.X, v2.X),
                processor.Subtract(v1.Y, v2.Y),
                processor.Subtract(v1.Z, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator -(E3DPoint<T> v1, E3DPoint<T> v2)
        {
            var processor = v1.ScalarProcessor;

            return new E3DVector<T>(
                processor, 
                processor.Subtract(v1.X, v2.X),
                processor.Subtract(v1.Y, v2.Y),
                processor.Subtract(v1.Z, v2.Z)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator *(int v1, E3DPoint<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E3DPoint<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y),
                processor.Times(s1, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator *(E3DPoint<T> v1, int v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DPoint<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2),
                processor.Times(v1.Z, s2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator *(uint v1, E3DPoint<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E3DPoint<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y),
                processor.Times(s1, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator *(E3DPoint<T> v1, uint v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DPoint<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2),
                processor.Times(v1.Z, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator *(float v1, E3DPoint<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E3DPoint<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y),
                processor.Times(s1, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator *(E3DPoint<T> v1, long v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DPoint<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2),
                processor.Times(v1.Z, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator *(long v1, E3DPoint<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E3DPoint<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y),
                processor.Times(s1, v2.Z)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator *(ulong v1, E3DPoint<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E3DPoint<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y),
                processor.Times(s1, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator *(E3DPoint<T> v1, ulong v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DPoint<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2),
                processor.Times(v1.Z, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator *(E3DPoint<T> v1, float v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DPoint<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2),
                processor.Times(v1.Z, s2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator *(double v1, E3DPoint<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E3DPoint<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y),
                processor.Times(s1, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator *(E3DPoint<T> v1, double v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DPoint<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2),
                processor.Times(v1.Z, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator *(T v1, E3DPoint<T> v2)
        {
            var processor = v2.ScalarProcessor;

            return new E3DPoint<T>(
                processor, 
                processor.Times(v1, v2.X),
                processor.Times(v1, v2.Y),
                processor.Times(v1, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator *(E3DPoint<T> v1, T v2)
        {
            var processor = v1.ScalarProcessor;

            return new E3DPoint<T>(
                processor, 
                processor.Times(v1.X, v2),
                processor.Times(v1.Y, v2),
                processor.Times(v1.Z, v2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator /(E3DPoint<T> v1, int v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DPoint<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2),
                processor.Divide(v1.Z, s2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator /(E3DPoint<T> v1, uint v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DPoint<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2),
                processor.Divide(v1.Z, s2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator /(E3DPoint<T> v1, long v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DPoint<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2),
                processor.Divide(v1.Z, s2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator /(E3DPoint<T> v1, ulong v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DPoint<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2),
                processor.Divide(v1.Z, s2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator /(E3DPoint<T> v1, float v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DPoint<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2),
                processor.Divide(v1.Z, s2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator /(E3DPoint<T> v1, double v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DPoint<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2),
                processor.Divide(v1.Z, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DPoint<T> operator /(E3DPoint<T> v1, T v2)
        {
            var processor = v1.ScalarProcessor;

            return new E3DPoint<T>(
                processor, 
                processor.Divide(v1.X, v2),
                processor.Divide(v1.Y, v2),
                processor.Divide(v1.Z, v2)
            );
        }

        public IScalarProcessor<T> ScalarProcessor { get; }

        public T X { get; }

        public T Y { get; }

        public T Z { get; }

        public Scalar<T> XScalar 
            => ScalarProcessor.CreateScalar(X);

        public Scalar<T> YScalar 
            => ScalarProcessor.CreateScalar(Y);

        public Scalar<T> ZScalar 
            => ScalarProcessor.CreateScalar(Z);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal E3DPoint(IScalarProcessor<T> scalarProcessor, T x, T y, T z)
        {
            ScalarProcessor = scalarProcessor;
            X = x;
            Y = y;
            Z = z;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DVector<T> ToE3DVector(bool assumeUnit = false)
        {
            return new E3DVector<T>(ScalarProcessor, X, Y, Z, assumeUnit);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetDistance(E3DPoint<T> point)
        {
            var x = ScalarProcessor.Subtract(X, point.X);
            var y = ScalarProcessor.Subtract(Y, point.Y);
            var z = ScalarProcessor.Subtract(Z, point.Z);

            return ScalarProcessor.Sqrt(
                ScalarProcessor.Add(
                    ScalarProcessor.Square(x),
                    ScalarProcessor.Square(y),
                    ScalarProcessor.Square(z)
                )
            ).CreateScalar(ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetDistanceSquared(E3DPoint<T> point)
        {
            var x = ScalarProcessor.Subtract(X, point.X);
            var y = ScalarProcessor.Subtract(Y, point.Y);
            var z = ScalarProcessor.Subtract(Z, point.Z);

            return ScalarProcessor.Add(
                ScalarProcessor.Square(x),
                ScalarProcessor.Square(y),
                ScalarProcessor.Square(z)
            ).CreateScalar(ScalarProcessor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DLineTangent<T> CreateLineTangent(E3DVector<T> direction)
        {
            return new E3DLineTangent<T>(this, direction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DLineSegment<T> CreateLineSegmentToPoint(E3DPoint<T> point2)
        {
            return new E3DLineSegment<T>(this, point2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DLineSegment<T> CreateLineSegmentFromPoint(E3DPoint<T> point1)
        {
            return new E3DLineSegment<T>(point1, this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPlaneTangent<T> CreatePlaneTangent(E3DVector<T> direction12, E3DVector<T> direction13)
        {
            return new E3DPlaneTangent<T>(this, direction12, direction13);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPlaneSegment<T> CreatePlaneSegment(E3DPoint<T> point2, E3DPoint<T> point3)
        {
            return new E3DPlaneSegment<T>(this, point2, point3);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPoint<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new E3DPoint<T>(
                ScalarProcessor,
                scalarMapping(X),
                scalarMapping(Y),
                scalarMapping(Z)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPoint<T2> MapScalars<T2>(Func<T, T2> scalarMapping, IScalarProcessor<T2> scalarProcessor)
        {
            return new E3DPoint<T2>(
                scalarProcessor,
                scalarMapping(X),
                scalarMapping(Y),
                scalarMapping(Z)
            );
        }
    }
}
