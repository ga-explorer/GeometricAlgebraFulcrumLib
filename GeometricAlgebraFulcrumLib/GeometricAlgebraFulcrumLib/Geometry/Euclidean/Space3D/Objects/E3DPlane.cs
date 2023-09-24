using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects
{
    public abstract class E3DPlane<T>
    {
        public abstract IScalarProcessor<T> ScalarProcessor { get; }

        public abstract bool IsSegment { get; }

        public abstract bool IsTangent { get; }


        public abstract E3DPoint<T> Point1 { get; }

        public abstract E3DPoint<T> Point2 { get; }

        public abstract E3DPoint<T> Point3 { get; }


        public abstract E3DVector<T> Normal { get; }

        public abstract E3DVector<T> Direction12 { get; }

        public abstract E3DVector<T> Direction21 { get; }

        public abstract E3DVector<T> Direction23 { get; }

        public abstract E3DVector<T> Direction32 { get; }

        public abstract E3DVector<T> Direction31 { get; }
    
        public abstract E3DVector<T> Direction13 { get; }


        public E3DLineSegment<T> LineSegment12 
            => new E3DLineSegment<T>(Point1, Point2);

        public E3DLineSegment<T> LineSegment21 
            => new E3DLineSegment<T>(Point2, Point1);

        public E3DLineSegment<T> LineSegment13 
            => new E3DLineSegment<T>(Point1, Point3);

        public E3DLineSegment<T> LineSegment31 
            => new E3DLineSegment<T>(Point3, Point1);

        public E3DLineSegment<T> LineSegment23 
            => new E3DLineSegment<T>(Point2, Point3);

        public E3DLineSegment<T> LineSegment32 
            => new E3DLineSegment<T>(Point3, Point2);

        public E3DLineTangent<T> LineTangent12 
            => new E3DLineTangent<T>(Point1, Direction12);

        public E3DLineTangent<T> LineTangent21 
            => new E3DLineTangent<T>(Point2, Direction21);

        public E3DLineTangent<T> LineTangent13 
            => new E3DLineTangent<T>(Point1, Direction13);

        public E3DLineTangent<T> LineTangent31 
            => new E3DLineTangent<T>(Point3, Direction21);

        public E3DLineTangent<T> LineTangent23 
            => new E3DLineTangent<T>(Point2, Direction23);

        public E3DLineTangent<T> LineTangent32 
            => new E3DLineTangent<T>(Point3, Direction32);


        public abstract E3DPlaneSegment<T> ToSegment();

        public abstract E3DPlaneTangent<T> ToTangent();

        public abstract E3DPoint<T> GetPoint(T t2, T t3);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPoint<T> GetPoint(IPair<float> t)
        {
            return GetPoint(
                t.Item1, 
                t.Item2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPoint<T> GetPoint(IPair<double> t)
        {
            return GetPoint(
                t.Item1, 
                t.Item2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPoint<T> GetPoint(IPair<T> t)
        {
            return GetPoint(
                t.Item1, 
                t.Item2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPoint<T> GetPoint(float t2, float t3)
        {
            return GetPoint(
                ScalarProcessor.GetScalarFromNumber(t2),
                ScalarProcessor.GetScalarFromNumber(t3)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPoint<T> GetPoint(double t2, double t3)
        {
            return GetPoint(
                ScalarProcessor.GetScalarFromNumber(t2),
                ScalarProcessor.GetScalarFromNumber(t3)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPlaneSegment<T> GetSegment(IPair<float> t1, IPair<float> t2, IPair<float> t3)
        {
            return new E3DPlaneSegment<T>(
                GetPoint(t1), 
                GetPoint(t2),
                GetPoint(t3)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPlaneSegment<T> GetSegment(IPair<double> t1, IPair<double> t2, IPair<double> t3)
        {
            return new E3DPlaneSegment<T>(
                GetPoint(t1), 
                GetPoint(t2),
                GetPoint(t3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPlaneSegment<T> GetSegment(IPair<T> t1, IPair<T> t2, IPair<T> t3)
        {
            return new E3DPlaneSegment<T>(
                GetPoint(t1), 
                GetPoint(t2),
                GetPoint(t3)
            );
        }
    }
}