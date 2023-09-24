using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space2D.Objects
{
    /// <summary>
    /// A plane tangent is a local 2D frame with one origin point and two direction vectors
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class E2DPlaneTangent<T> :
        E2DPlane<T>
    {
        public E2DPoint<T> Origin 
            => Point1;

        public override IScalarProcessor<T> ScalarProcessor 
            => Point1.ScalarProcessor;

        public override bool IsSegment 
            => false;

        public override bool IsTangent 
            => true;

        public override E2DPoint<T> Point1 { get; }

        public override E2DPoint<T> Point2 
            => Point1 + Direction12;

        public override E2DPoint<T> Point3 
            => Point1 + Direction13;

        public override E2DVector<T> Direction12 { get; }

        public override E2DVector<T> Direction21 
            => -Direction12;

        public override E2DVector<T> Direction13 { get; }

        public override E2DVector<T> Direction31 
            => -Direction13;

        public override E2DVector<T> Direction23 
            => Direction13 - Direction12;

        public override E2DVector<T> Direction32 
            => Direction12 - Direction13;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal E2DPlaneTangent(E2DPoint<T> origin, E2DVector<T> direction12, E2DVector<T> direction13)
        {
            Point1 = origin;
            Direction12 = direction12;
            Direction13 = direction13;
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E2DPlaneSegment<T> ToSegment()
        {
            return new E2DPlaneSegment<T>(
                Point1,
                Point2,
                Point3
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E2DPlaneTangent<T> ToTangent()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E2DPoint<T> GetPoint(T t2, T t3)
        {
            return Point1 + t2 * Direction12 + t3 * Direction13;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E2DPlaneTangent<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new E2DPlaneTangent<T>(
                Point1.MapScalars(scalarMapping),
                Direction12.MapScalars(scalarMapping),
                Direction13.MapScalars(scalarMapping)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E2DPlaneTangent<T2> MapScalars<T2>(Func<T, T2> scalarMapping, IScalarProcessor<T2> scalarProcessor)
        {
            return new E2DPlaneTangent<T2>(
                Point1.MapScalars(scalarMapping, scalarProcessor),
                Direction12.MapScalars(scalarMapping, scalarProcessor),
                Direction13.MapScalars(scalarMapping, scalarProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E2DPlaneTangent<T> MapPoints(Func<E2DPoint<T>, E2DPoint<T>> pointMapping)
        {
            var point1 = pointMapping(Point1);
            var point2 = pointMapping(Point2);
            var point3 = pointMapping(Point3);

            return new E2DPlaneTangent<T>(
                point1,
                point2 - point1,
                point3 - point1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E2DPlaneTangent<T2> MapPoints<T2>(Func<E2DPoint<T>, E2DPoint<T2>> pointMapping)
        {
            var point1 = pointMapping(Point1);
            var point2 = pointMapping(Point2);
            var point3 = pointMapping(Point3);

            return new E2DPlaneTangent<T2>(
                point1,
                point2 - point1,
                point3 - point1
            );
        }
    }
}