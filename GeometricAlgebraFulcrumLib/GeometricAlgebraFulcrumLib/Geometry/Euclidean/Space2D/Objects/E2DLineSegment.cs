using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space2D.Objects
{
    public sealed class E2DLineSegment<T> :
        E2DLine<T>
    {
        public override IScalarProcessor<T> ScalarProcessor 
            => Point1.ScalarProcessor;

        public override bool IsSegment 
            => true;

        public override bool IsTangent 
            => false;

        public override E2DPoint<T> Point1 { get; }

        public override E2DPoint<T> Point2 { get; }

        public override E2DVector<T> Direction12 
            => Point2 - Point1;
    
        public override E2DVector<T> Direction21 
            => Point1 - Point2;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal E2DLineSegment(E2DPoint<T> point1, E2DPoint<T> point2)
        {
            Point1 = point1;
            Point2 = point2;
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E2DPoint<T> GetPoint(T t)
        {
            var s = ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, t);

            return s * Point1 + t * Point2;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E2DLineSegment<T> ToSegment()
        {
            return this;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E2DLineTangent<T> ToTangent()
        {
            return new E2DLineTangent<T>(Point1, Point2 - Point1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E2DLineSegment<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new E2DLineSegment<T>(
                Point1.MapScalars(scalarMapping),
                Point2.MapScalars(scalarMapping)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E2DLineSegment<T2> MapScalars<T2>(Func<T, T2> scalarMapping, IScalarProcessor<T2> scalarProcessor)
        {
            return new E2DLineSegment<T2>(
                Point1.MapScalars(scalarMapping, scalarProcessor),
                Point2.MapScalars(scalarMapping, scalarProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E2DLineSegment<T> MapPoints(Func<E2DPoint<T>, E2DPoint<T>> pointMapping)
        {
            return new E2DLineSegment<T>(
                pointMapping(Point1),
                pointMapping(Point2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E2DLineSegment<T2> MapPoints<T2>(Func<E2DPoint<T>, E2DPoint<T2>> pointMapping)
        {
            return new E2DLineSegment<T2>(
                pointMapping(Point1),
                pointMapping(Point2)
            );
        }
    }
}