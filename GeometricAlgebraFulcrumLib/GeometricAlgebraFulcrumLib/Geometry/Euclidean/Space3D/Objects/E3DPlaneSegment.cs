using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects
{
    /// <summary>
    /// A plane segment is a triangle defined by 3 points
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class E3DPlaneSegment<T> :
        E3DPlane<T>
    {
        public override IScalarProcessor<T> ScalarProcessor 
            => Point1.ScalarProcessor;

        public override bool IsSegment 
            => true;

        public override bool IsTangent 
            => false;

        public override E3DPoint<T> Point1 { get; }

        public override E3DPoint<T> Point2 { get; }

        public override E3DPoint<T> Point3 { get; }

        public override E3DVector<T> Normal 
            => Direction12.Cross(Direction23);

        public override E3DVector<T> Direction12 
            => Point2 - Point1;

        public override E3DVector<T> Direction21 
            => Point1 - Point2;
    
        public override E3DVector<T> Direction23 
            => Point3 - Point2;
    
        public override E3DVector<T> Direction32 
            => Point2 - Point3;
    
        public override E3DVector<T> Direction31 
            => Point1 - Point3;
    
        public override E3DVector<T> Direction13 
            => Point3 - Point1;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal E3DPlaneSegment(E3DPoint<T> point1, E3DPoint<T> point2, E3DPoint<T> point3)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E3DPlaneSegment<T> ToSegment()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E3DPlaneTangent<T> ToTangent()
        {
            return new E3DPlaneTangent<T>(
                Point1, 
                Direction12, 
                Direction13
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E3DPoint<T> GetPoint(T t2, T t3)
        {
            var t1 = ScalarProcessor.Subtract(
                ScalarProcessor.ScalarOne, 
                ScalarProcessor.Add(t2, t3)
            );

            return t1 * Point1 + t2 * Point2 + t3 * Point3;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPlaneSegment<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new E3DPlaneSegment<T>(
                Point1.MapScalars(scalarMapping),
                Point2.MapScalars(scalarMapping),
                Point3.MapScalars(scalarMapping)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPlaneSegment<T2> MapScalars<T2>(Func<T, T2> scalarMapping, IScalarProcessor<T2> scalarProcessor)
        {
            return new E3DPlaneSegment<T2>(
                Point1.MapScalars(scalarMapping, scalarProcessor),
                Point2.MapScalars(scalarMapping, scalarProcessor),
                Point3.MapScalars(scalarMapping, scalarProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPlaneSegment<T> MapPoints(Func<E3DPoint<T>, E3DPoint<T>> pointMapping)
        {
            return new E3DPlaneSegment<T>(
                pointMapping(Point1),
                pointMapping(Point2),
                pointMapping(Point3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPlaneSegment<T2> MapPoints<T2>(Func<E3DPoint<T>, E3DPoint<T2>> pointMapping)
        {
            return new E3DPlaneSegment<T2>(
                pointMapping(Point1),
                pointMapping(Point2),
                pointMapping(Point3)
            );
        }
    }
}