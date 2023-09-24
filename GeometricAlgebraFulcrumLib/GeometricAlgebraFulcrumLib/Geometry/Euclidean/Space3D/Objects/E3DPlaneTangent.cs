using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects
{
    /// <summary>
    /// A plane tangent is a local 2D frame with one origin point and two direction vectors
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class E3DPlaneTangent<T> :
        E3DPlane<T>
    {
        public E3DPoint<T> Origin 
            => Point1;

        public override IScalarProcessor<T> ScalarProcessor 
            => Point1.ScalarProcessor;

        public override bool IsSegment 
            => false;

        public override bool IsTangent 
            => true;

        public override E3DPoint<T> Point1 { get; }

        public override E3DPoint<T> Point2 
            => Point1 + Direction12;

        public override E3DPoint<T> Point3 
            => Point1 + Direction13;
    
        public override E3DVector<T> Normal 
            => Direction12.Cross(Direction23);

        public override E3DVector<T> Direction12 { get; }

        public override E3DVector<T> Direction21 
            => -Direction12;

        public override E3DVector<T> Direction13 { get; }

        public override E3DVector<T> Direction31 
            => -Direction13;

        public override E3DVector<T> Direction23 
            => Direction13 - Direction12;

        public override E3DVector<T> Direction32 
            => Direction12 - Direction13;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal E3DPlaneTangent(E3DPoint<T> origin, E3DVector<T> direction12, E3DVector<T> direction13)
        {
            Point1 = origin;
            Direction12 = direction12;
            Direction13 = direction13;
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E3DPlaneSegment<T> ToSegment()
        {
            return new E3DPlaneSegment<T>(
                Point1,
                Point2,
                Point3
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E3DPlaneTangent<T> ToTangent()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E3DPoint<T> GetPoint(T t2, T t3)
        {
            return Point1 + t2 * Direction12 + t3 * Direction13;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPlaneTangent<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new E3DPlaneTangent<T>(
                Point1.MapScalars(scalarMapping),
                Direction12.MapScalars(scalarMapping),
                Direction13.MapScalars(scalarMapping)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPlaneTangent<T2> MapScalars<T2>(Func<T, T2> scalarMapping, IScalarProcessor<T2> scalarProcessor)
        {
            return new E3DPlaneTangent<T2>(
                Point1.MapScalars(scalarMapping, scalarProcessor),
                Direction12.MapScalars(scalarMapping, scalarProcessor),
                Direction13.MapScalars(scalarMapping, scalarProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPlaneTangent<T> MapPoints(Func<E3DPoint<T>, E3DPoint<T>> pointMapping)
        {
            var point1 = pointMapping(Point1);
            var point2 = pointMapping(Point2);
            var point3 = pointMapping(Point3);

            return new E3DPlaneTangent<T>(
                point1,
                point2 - point1,
                point3 - point1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPlaneTangent<T2> MapPoints<T2>(Func<E3DPoint<T>, E3DPoint<T2>> pointMapping)
        {
            var point1 = pointMapping(Point1);
            var point2 = pointMapping(Point2);
            var point3 = pointMapping(Point3);

            return new E3DPlaneTangent<T2>(
                point1,
                point2 - point1,
                point3 - point1
            );
        }
    }
}