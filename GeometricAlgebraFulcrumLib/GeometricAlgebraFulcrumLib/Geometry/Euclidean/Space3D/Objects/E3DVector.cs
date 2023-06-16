using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects
{
    public sealed record E3DVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator -(E3DVector<T> v1)
        {
            var processor = v1.ScalarProcessor;

            return new E3DVector<T>(
                processor, 
                processor.Negative(v1.X),
                processor.Negative(v1.Y),
                processor.Negative(v1.Z),
                v1.AssumeUnit
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator +(E3DVector<T> v1, E3DVector<T> v2)
        {
            var processor = v1.ScalarProcessor;

            return new E3DVector<T>(
                processor, 
                processor.Add(v1.X, v2.X),
                processor.Add(v1.Y, v2.Y),
                processor.Add(v1.Z, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator -(E3DVector<T> v1, E3DVector<T> v2)
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
        public static E3DVector<T> operator *(int v1, E3DVector<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E3DVector<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y),
                processor.Times(s1, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator *(E3DVector<T> v1, int v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DVector<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2),
                processor.Times(v1.Z, s2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator *(uint v1, E3DVector<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E3DVector<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y),
                processor.Times(s1, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator *(E3DVector<T> v1, uint v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DVector<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2),
                processor.Times(v1.Z, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator *(float v1, E3DVector<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E3DVector<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y),
                processor.Times(s1, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator *(E3DVector<T> v1, long v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DVector<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2),
                processor.Times(v1.Z, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator *(long v1, E3DVector<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E3DVector<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y),
                processor.Times(s1, v2.Z)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator *(ulong v1, E3DVector<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E3DVector<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y),
                processor.Times(s1, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator *(E3DVector<T> v1, ulong v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DVector<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2),
                processor.Times(v1.Z, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator *(E3DVector<T> v1, float v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DVector<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2),
                processor.Times(v1.Z, s2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator *(double v1, E3DVector<T> v2)
        {
            var processor = v2.ScalarProcessor;
            var s1 = processor.GetScalarFromNumber(v1);

            return new E3DVector<T>(
                processor, 
                processor.Times(s1, v2.X),
                processor.Times(s1, v2.Y),
                processor.Times(s1, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator *(E3DVector<T> v1, double v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DVector<T>(
                processor, 
                processor.Times(v1.X, s2),
                processor.Times(v1.Y, s2),
                processor.Times(v1.Z, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator *(T v1, E3DVector<T> v2)
        {
            var processor = v2.ScalarProcessor;

            return new E3DVector<T>(
                processor, 
                processor.Times(v1, v2.X),
                processor.Times(v1, v2.Y),
                processor.Times(v1, v2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator *(E3DVector<T> v1, T v2)
        {
            var processor = v1.ScalarProcessor;

            return new E3DVector<T>(
                processor, 
                processor.Times(v1.X, v2),
                processor.Times(v1.Y, v2),
                processor.Times(v1.Z, v2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator /(E3DVector<T> v1, int v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DVector<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2),
                processor.Divide(v1.Z, s2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator /(E3DVector<T> v1, uint v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DVector<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2),
                processor.Divide(v1.Z, s2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator /(E3DVector<T> v1, long v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DVector<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2),
                processor.Divide(v1.Z, s2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator /(E3DVector<T> v1, ulong v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DVector<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2),
                processor.Divide(v1.Z, s2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator /(E3DVector<T> v1, float v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DVector<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2),
                processor.Divide(v1.Z, s2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator /(E3DVector<T> v1, double v2)
        {
            var processor = v1.ScalarProcessor;
            var s2 = processor.GetScalarFromNumber(v2);

            return new E3DVector<T>(
                processor, 
                processor.Divide(v1.X, s2),
                processor.Divide(v1.Y, s2),
                processor.Divide(v1.Z, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E3DVector<T> operator /(E3DVector<T> v1, T v2)
        {
            var processor = v1.ScalarProcessor;

            return new E3DVector<T>(
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

        public bool AssumeUnit { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal E3DVector(IScalarProcessor<T> scalarProcessor, T x, T y, T z, bool assumeUnit = false)
        {
            ScalarProcessor = scalarProcessor;
            X = x;
            Y = y;
            Z = z;
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
                    ScalarProcessor.Square(Y),
                    ScalarProcessor.Square(Z)
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
                ScalarProcessor.Square(Y),
                ScalarProcessor.Square(Z)
            ).CreateScalar(ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DVector<T> GetUnitVector()
        {
            if (AssumeUnit)
                return this;

            var norm = ScalarProcessor.Sqrt(
                ScalarProcessor.Add(
                    ScalarProcessor.Square(X),
                    ScalarProcessor.Square(Y),
                    ScalarProcessor.Square(Z)
                )
            );

            return this / norm;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DVector<T> GetInverseVector()
        {
            if (AssumeUnit)
                return this;

            var normSquared = ScalarProcessor.Add(
                ScalarProcessor.Square(X),
                ScalarProcessor.Square(Y),
                ScalarProcessor.Square(Z)
            );

            return this / normSquared;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPoint<T> ToE3DPoint()
        {
            return new E3DPoint<T>(ScalarProcessor, X, Y, Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> Dot(E3DVector<T> vector)
        {
            return ScalarProcessor.Add(
                ScalarProcessor.Times(X, vector.X),
                ScalarProcessor.Times(Y, vector.Y),
                ScalarProcessor.Times(Z, vector.Z)
            ).CreateScalar(ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DVector<T> Cross(E3DVector<T> vector)
        {
            var x = ScalarProcessor.Subtract(
                ScalarProcessor.Times(Y, vector.Z), 
                ScalarProcessor.Times(vector.Y, Z)
            );

            var y = ScalarProcessor.Subtract(
                ScalarProcessor.Times(vector.X, Z), 
                ScalarProcessor.Times(X, vector.Z)
            );

            var z = ScalarProcessor.Subtract(
                ScalarProcessor.Times(X, vector.Y), 
                ScalarProcessor.Times(vector.X, Y)
            );

            return new E3DVector<T>(ScalarProcessor, x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetCosAngle(E3DVector<T> vector)
        {
            var norm1 = GetNorm().ScalarValue;
            var norm2 = vector.GetNorm().ScalarValue;

            return ScalarProcessor.Divide(
                ScalarProcessor.Add(
                    ScalarProcessor.Times(X, vector.X),
                    ScalarProcessor.Times(Y, vector.Y),
                    ScalarProcessor.Times(Z, vector.Z)
                ),
                ScalarProcessor.Times(norm1, norm2)
            ).CreateScalar(ScalarProcessor);
        }

        public E3DBivector<T> Op(E3DVector<T> vector)
        {
            //TODO: Review this
            var yz = ScalarProcessor.Subtract(
                ScalarProcessor.Times(Y, vector.Z), 
                ScalarProcessor.Times(vector.Y, Z)
            );

            var xz = ScalarProcessor.Subtract(
                ScalarProcessor.Times(X, vector.Z),
                ScalarProcessor.Times(vector.X, Z) 
            );

            var xy = ScalarProcessor.Subtract(
                ScalarProcessor.Times(X, vector.Y), 
                ScalarProcessor.Times(vector.X, Y)
            );

            return new E3DBivector<T>(ScalarProcessor, xy, xz, yz);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DVector<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new E3DVector<T>(
                ScalarProcessor,
                scalarMapping(X),
                scalarMapping(Y),
                scalarMapping(Z)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DVector<T2> MapScalars<T2>(Func<T, T2> scalarMapping, IScalarProcessor<T2> scalarProcessor)
        {
            return new E3DVector<T2>(
                scalarProcessor,
                scalarMapping(X),
                scalarMapping(Y),
                scalarMapping(Z)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DLineTangent<T> CreateE3DLineTangent(E3DPoint<T> origin)
        {
            return new E3DLineTangent<T>(origin, this);
        }


    }
}