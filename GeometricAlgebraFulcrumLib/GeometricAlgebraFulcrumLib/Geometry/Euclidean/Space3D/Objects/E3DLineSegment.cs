using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects
{
    public sealed class E3DLineSegment<T> :
        E3DLine<T>
    {
        public override IScalarProcessor<T> ScalarProcessor 
            => Point1.ScalarProcessor;

        public override bool IsSegment 
            => true;

        public override bool IsTangent 
            => false;

        public override E3DPoint<T> Point1 { get; }

        public override E3DPoint<T> Point2 { get; }

        public override E3DVector<T> Direction12 
            => Point2 - Point1;
    
        public override E3DVector<T> Direction21 
            => Point1 - Point2;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal E3DLineSegment(E3DPoint<T> point1, E3DPoint<T> point2)
        {
            Point1 = point1;
            Point2 = point2;
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E3DPoint<T> GetPoint(T t)
        {
            var s = ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, t);

            return s * Point1 + t * Point2;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E3DLineSegment<T> ToSegment()
        {
            return this;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E3DLineTangent<T> ToTangent()
        {
            return new E3DLineTangent<T>(Point1, Point2 - Point1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DLineSegment<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new E3DLineSegment<T>(
                Point1.MapScalars(scalarMapping),
                Point2.MapScalars(scalarMapping)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DLineSegment<T2> MapScalars<T2>(Func<T, T2> scalarMapping, IScalarProcessor<T2> scalarProcessor)
        {
            return new E3DLineSegment<T2>(
                Point1.MapScalars(scalarMapping, scalarProcessor),
                Point2.MapScalars(scalarMapping, scalarProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DLineSegment<T> MapPoints(Func<E3DPoint<T>, E3DPoint<T>> pointMapping)
        {
            return new E3DLineSegment<T>(
                pointMapping(Point1),
                pointMapping(Point2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DLineSegment<T2> MapPoints<T2>(Func<E3DPoint<T>, E3DPoint<T2>> pointMapping)
        {
            return new E3DLineSegment<T2>(
                pointMapping(Point1),
                pointMapping(Point2)
            );
        }
    }
}