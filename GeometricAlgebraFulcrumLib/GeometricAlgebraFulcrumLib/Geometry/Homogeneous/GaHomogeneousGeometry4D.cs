﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Homogeneous
{
    public sealed class GaHomogeneousGeometry4D<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public IGeometricAlgebraEuclideanProcessor<T> GeometricProcessor { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaHomogeneousGeometry4D([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
            GeometricProcessor = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(4);

            if (scalarProcessor is MetaContext context)
                context.GeometricProcessor = GeometricProcessor as 
                    IGeometricAlgebraEuclideanProcessor<IMetaExpressionAtomic>;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E3DPoint<T> GetE3DPoint(GaVector<T> vector)
        {
            return new E3DPoint<T>(
                ScalarProcessor, 
                vector[0], 
                vector[1], 
                vector[2]
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> GetDirectionMultivector(float x, float y, float z)
        {
            return GeometricProcessor.CreateVector(
                ScalarProcessor.GetScalarFromNumber(x), 
                ScalarProcessor.GetScalarFromNumber(y), 
                ScalarProcessor.GetScalarFromNumber(z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> GetDirectionMultivector(double x, double y, double z)
        {
            return GeometricProcessor.CreateVector(
                ScalarProcessor.GetScalarFromNumber(x), 
                ScalarProcessor.GetScalarFromNumber(y), 
                ScalarProcessor.GetScalarFromNumber(z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> GetDirectionMultivector(T x, T y, T z)
        {
            return GeometricProcessor.CreateVector(x, y, z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> GetDirectionMultivector(E3DVector<T> vector)
        {
            return GeometricProcessor.CreateVector(
                vector.X, 
                vector.Y, 
                vector.Z
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> GetPointMultivector(float x, float y, float z)
        {
            return GeometricProcessor.CreateVector(
                ScalarProcessor.GetScalarFromNumber(x), 
                ScalarProcessor.GetScalarFromNumber(y), 
                ScalarProcessor.GetScalarFromNumber(z),
                ScalarProcessor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> GetPointMultivector(double x, double y, double z)
        {
            return GeometricProcessor.CreateVector(
                ScalarProcessor.GetScalarFromNumber(x), 
                ScalarProcessor.GetScalarFromNumber(y), 
                ScalarProcessor.GetScalarFromNumber(z),
                ScalarProcessor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> GetPointMultivector(T x, T y, T z)
        {
            return GeometricProcessor.CreateVector(
                x, 
                y, 
                z, 
                ScalarProcessor.ScalarOne
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> GetPointMultivector(E3DPoint<T> point)
        {
            return GeometricProcessor.CreateVector(
                point.X, 
                point.Y, 
                point.Z, 
                ScalarProcessor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> GetLineMultivector(E3DPoint<T> point1, E3DPoint<T> point2)
        {
            var pointMv1 = GetPointMultivector(point1);
            var pointMv2 = GetPointMultivector(point2);

            return pointMv1.Op(pointMv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> GetLineMultivector(E3DPoint<T> origin, E3DVector<T> direction)
        {
            var originMv = GetPointMultivector(origin);
            var directionMv = GetDirectionMultivector(direction);

            return originMv.Op(directionMv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> GetLineMultivector(E3DLine<T> line)
        {
            var pointMv1 = GetPointMultivector(line.Point1);
            var pointMv2 = GetPointMultivector(line.Point2);

            return pointMv1.Op(pointMv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> GetPlaneMultivector(E3DPoint<T> point1, E3DPoint<T> point2, E3DPoint<T> point3)
        {
            var pointMv1 = GetPointMultivector(point1);
            var pointMv2 = GetPointMultivector(point2);
            var pointMv3 = GetPointMultivector(point3);

            return pointMv1.Op(pointMv2).Op(pointMv3);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> GetPlaneMultivector(E3DPlane<T> plane)
        {
            var pointMv1 = GetPointMultivector(plane.Point1);
            var pointMv2 = GetPointMultivector(plane.Point2);
            var pointMv3 = GetPointMultivector(plane.Point3);

            return pointMv1.Op(pointMv2).Op(pointMv3);
        }

        public E3DPoint<T> ReflectPointOnLine(E3DPoint<T> point, E3DLine<T> line)
        {
            var a = GetPointMultivector(point);

            var q1 = GetPointMultivector(line.Point1);
            var q2 = GetPointMultivector(line.Point2);

            var v = q2 - q1;
            var b = q1 + v.Gp(a - q1).Gp(v.Inverse()).GetVectorPart();

            return GetE3DPoint(b);
        }
        
        public E3DPoint<T> ReflectPointOnPlane(E3DPoint<T> point, E3DPlane<T> plane)
        {
            var a = GetPointMultivector(point);

            var q1 = GetPointMultivector(plane.Point1);
            var q2 = GetPointMultivector(plane.Point2);
            var q3 = GetPointMultivector(plane.Point3);

            var v = (q2 - q1).Op(q3 - q2);
            var b = q1 + v.Gp(a - q1).Gp(v.Inverse()).GetVectorPart();

            return GetE3DPoint(b);
        }

        public Scalar<T> GetDistance(E3DPoint<T> point, E3DPlane<T> plane)
        {
            var a = GetPointMultivector(point);

            var q1 = GetPointMultivector(plane.Point1);
            var q2 = GetPointMultivector(plane.Point2);
            var q3 = GetPointMultivector(plane.Point3);

            var i = GeometricProcessor.PseudoScalarInverse;

            return a.Op(q1).Op(q2).Op(q3).Lcp(i).AsScalar();
        }

        public Scalar<T> GetDistance(E3DLine<T> line1, E3DLine<T> line2)
        {
            var p1 = GetPointMultivector(line1.Point1);
            var p2 = GetPointMultivector(line1.Point2);

            var q1 = GetPointMultivector(line2.Point1);
            var q2 = GetPointMultivector(line2.Point2);

            var i = GeometricProcessor.PseudoScalarInverse;

            return p1.Op(p2).Op(q1).Op(q2).Lcp(i).AsScalar();
        }

        public E3DLinePlaneIntersectionRecord<T> GetIntersection(E3DLine<T> line, E3DPlane<T> plane)
        {
            var linePoint1Dist = GetDistance(line.Point1, plane);
            var linePoint2Dist = GetDistance(line.Point2, plane);

            var planeSide12Dist = GetDistance(plane.LineSegment12, line);
            var planeSide23Dist = GetDistance(plane.LineSegment23, line);
            var planeSide31Dist = GetDistance(plane.LineSegment31, line);

            // Make sure we get a single intersection point
            //Debug.Assert(
            //    (line.GetPoint(t) - plane.GetPoint(t1, t2)).GetNormSquared().IsNearZero()
            //);

            return new E3DLinePlaneIntersectionRecord<T>(ScalarProcessor)
            {
                LinePoint1Distance = linePoint1Dist,
                LinePoint2Distance = linePoint2Dist,
                PlaneLine12Distance = planeSide12Dist,
                PlaneLine23Distance = planeSide23Dist,
                PlaneLine31Distance = planeSide31Dist
            };
        }
    }
}