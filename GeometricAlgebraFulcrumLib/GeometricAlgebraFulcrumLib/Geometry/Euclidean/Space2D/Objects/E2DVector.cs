using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space2D.Objects
{
    public sealed record E2DVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator -(E2DVector<T> v1)
        {
            var processor = v1.ScalarProcessor;

            return new E2DVector<T>(
                processor, 
                processor.Negative(v1.X),
                processor.Negative(v1.Y),
                v1.AssumeUnit
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator +(E2DVector<T> v1, E2DVector<T> v2)
        {
            var processor = v1.ScalarProcessor;

            return new E2DVector<T>(
                processor, 
                processor.Add(v1.X, v2.X),
                processor.Add(v1.Y, v2.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator -(E2DVector<T> v1, E2DVector<T> v2)
        {
            var processor = v1.ScalarProcessor;

            return new E2DVector<T>(
                processor, 
                processor.Subtract(v1.X, v2.X),
                processor.Subtract(v1.Y, v2.Y)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator *(int v1, E2DVector<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E2DVector<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator *(E2DVector<T> v1, int v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E2DVector<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator *(uint v1, E2DVector<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E2DVector<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator *(E2DVector<T> v1, uint v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E2DVector<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator *(float v1, E2DVector<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E2DVector<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator *(E2DVector<T> v1, long v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E2DVector<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator *(long v1, E2DVector<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E2DVector<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator *(ulong v1, E2DVector<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E2DVector<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator *(E2DVector<T> v1, ulong v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E2DVector<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator *(E2DVector<T> v1, float v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E2DVector<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator *(double v1, E2DVector<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E2DVector<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator *(E2DVector<T> v1, double v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E2DVector<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator *(T v1, E2DVector<T> v2)
        {
            var processor = v2.ScalarProcessor;

            return new E2DVector<T>(
                processor, 
                processor.Times(v1, v2.X),
                processor.Times(v1, v2.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator *(E2DVector<T> v1, T v2)
        {
            var processor = v1.ScalarProcessor;

            return new E2DVector<T>(
                processor, 
                processor.Times(v1.X, v2),
                processor.Times(v1.Y, v2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator /(E2DVector<T> v1, int v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E2DVector<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator /(E2DVector<T> v1, uint v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E2DVector<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator /(E2DVector<T> v1, long v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E2DVector<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator /(E2DVector<T> v1, ulong v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E2DVector<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator /(E2DVector<T> v1, float v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E2DVector<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator /(E2DVector<T> v1, double v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E2DVector<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E2DVector<T> operator /(E2DVector<T> v1, T v2)
        {
            var processor = v1.ScalarProcessor;

            return new E2DVector<T>(
                processor, 
                processor.Divide(v1.X, v2),
                processor.Divide(v1.Y, v2)
            );
        }


        public IScalarProcessor<T> ScalarProcessor { get; }

        public T X { get; }

        public T Y { get; }
    
        public Scalar<T> XScalar 
            => ScalarProcessor.CreateScalar(X);

        public Scalar<T> YScalar 
            => ScalarProcessor.CreateScalar(Y);

        public bool AssumeUnit { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal E2DVector(IScalarProcessor<T> scalarProcessor, T x, T y, bool assumeUnit = false)
        {
            ScalarProcessor = scalarProcessor;
            X = x;
            Y = y;
            AssumeUnit = assumeUnit;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetNorm()
        {
            if (AssumeUnit)
                return ScalarProcessor.ScalarOne.CreateScalar(ScalarProcessor);

            return ScalarProcessor.Sqrt(
                ScalarProcessor.Add(
                    ScalarProcessor.Square(X),
                    ScalarProcessor.Square(Y)
                )
            ).CreateScalar(ScalarProcessor);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetNormSquared()
        {
            if (AssumeUnit)
                return ScalarProcessor.ScalarOne.CreateScalar(ScalarProcessor);

            return ScalarProcessor.Add(
                ScalarProcessor.Square(X),
                ScalarProcessor.Square(Y)
            ).CreateScalar(ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E2DVector<T> GetUnitVector()
        {
            if (AssumeUnit)
                return this;

            var norm = ScalarProcessor.Sqrt(
                ScalarProcessor.Add(
                    ScalarProcessor.Square(X),
                    ScalarProcessor.Square(Y)
                )
            );

            return this / norm;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E2DVector<T> GetInverseVector()
        {
            if (AssumeUnit)
                return this;

            var normSquared = ScalarProcessor.Add(
                ScalarProcessor.Square(X),
                ScalarProcessor.Square(Y)
            );

            return this / normSquared;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E2DPoint<T> ToE2DPoint()
        {
            return new E2DPoint<T>(ScalarProcessor, X, Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> Dot(E2DVector<T> vector)
        {
            return ScalarProcessor.Add(
                ScalarProcessor.Times(X, vector.X),
                ScalarProcessor.Times(Y, vector.Y)
            ).CreateScalar(ScalarProcessor);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetCosAngle(E2DVector<T> vector)
        {
            var norm1 = GetNorm().ScalarValue;
            var norm2 = vector.GetNorm().ScalarValue;

            return ScalarProcessor.Divide(
                ScalarProcessor.Add(
                    ScalarProcessor.Times(X, vector.X),
                    ScalarProcessor.Times(Y, vector.Y)
                ),
                ScalarProcessor.Times(norm1, norm2)
            ).CreateScalar(ScalarProcessor);
        }

        public E2DBivector<T> Op(E2DVector<T> vector)
        {
            var xy = ScalarProcessor.Subtract(
                ScalarProcessor.Times(X, vector.Y), 
                ScalarProcessor.Times(Y, vector.X)
            );

            return new E2DBivector<T>(ScalarProcessor, xy);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E2DVector<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new E2DVector<T>(
                ScalarProcessor,
                scalarMapping(X),
                scalarMapping(Y)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E2DVector<T2> MapScalars<T2>(Func<T, T2> scalarMapping, IScalarProcessor<T2> scalarProcessor)
        {
            return new E2DVector<T2>(
                scalarProcessor,
                scalarMapping(X),
                scalarMapping(Y)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E2DLineTangent<T> CreateE2DLineTangent(E2DPoint<T> origin)
        {
            return new E2DLineTangent<T>(origin, this);
        }


    }
}