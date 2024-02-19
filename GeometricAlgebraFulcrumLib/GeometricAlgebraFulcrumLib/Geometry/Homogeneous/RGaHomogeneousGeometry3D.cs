using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space2D.Objects;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Geometry.Homogeneous;

public sealed class RGaHomogeneousGeometry3D<T>
{
    public IScalarProcessor<T> ScalarProcessor 
        => GeometricProcessor.ScalarProcessor;

    public RGaEuclideanProcessor<T> GeometricProcessor { get; }

    public int VSpaceDimensions 
        => 3;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaHomogeneousGeometry3D(IScalarProcessor<T> scalarProcessor)
    {
        GeometricProcessor = scalarProcessor.CreateEuclideanRGaProcessor();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DPoint<T> GetE2DPoint(RGaVector<T> vector)
    {
        return new E2DPoint<T>(
            ScalarProcessor, 
            vector.Scalar(0), 
            vector.Scalar(1)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GetDirectionMultivector(float x, float y)
    {
        return GeometricProcessor.CreateVector(
            ScalarProcessor.GetScalarFromNumber(x), 
            ScalarProcessor.GetScalarFromNumber(y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GetDirectionMultivector(double x, double y)
    {
        return GeometricProcessor.CreateVector(
            ScalarProcessor.GetScalarFromNumber(x), 
            ScalarProcessor.GetScalarFromNumber(y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GetDirectionMultivector(T x, T y)
    {
        return GeometricProcessor.CreateVector(x, y);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GetDirectionMultivector(E2DVector<T> vector)
    {
        return GeometricProcessor.CreateVector(
            vector.X, 
            vector.Y
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GetPointMultivector(float x, float y)
    {
        return GeometricProcessor.CreateVector(
            ScalarProcessor.GetScalarFromNumber(x), 
            ScalarProcessor.GetScalarFromNumber(y),
            ScalarProcessor.ScalarOne
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GetPointMultivector(double x, double y)
    {
        return GeometricProcessor.CreateVector(
            ScalarProcessor.GetScalarFromNumber(x), 
            ScalarProcessor.GetScalarFromNumber(y),
            ScalarProcessor.ScalarOne
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GetPointMultivector(T x, T y)
    {
        return GeometricProcessor.CreateVector(
            x, 
            y, 
            ScalarProcessor.ScalarOne
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GetPointMultivector(E2DPoint<T> point)
    {
        return GeometricProcessor.CreateVector(
            point.X, 
            point.Y, 
            ScalarProcessor.ScalarOne
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBivector<T> GetLineMultivector(E2DPoint<T> point1, E2DPoint<T> point2)
    {
        var pointMv1 = GetPointMultivector(point1);
        var pointMv2 = GetPointMultivector(point2);

        return pointMv1.Op(pointMv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBivector<T> GetLineMultivector(E2DPoint<T> origin, E2DVector<T> direction)
    {
        var originMv = GetPointMultivector(origin);
        var directionMv = GetDirectionMultivector(direction);

        return originMv.Op(directionMv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBivector<T> GetLineMultivector(E2DLine<T> line)
    {
        var pointMv1 = GetPointMultivector(line.Point1);
        var pointMv2 = GetPointMultivector(line.Point2);

        return pointMv1.Op(pointMv2);
    }
    
    public E2DPoint<T> ReflectPointOnLine(E2DPoint<T> point, E2DLine<T> line)
    {
        var a = GetPointMultivector(point);

        var q1 = GetPointMultivector(line.Point1);
        var q2 = GetPointMultivector(line.Point2);

        var v = q2 - q1;
        var b = q1 + v.Gp(a - q1).Gp(v.Inverse()).GetVectorPart();

        return GetE2DPoint(b);
    }
       
    public Scalar<T> GetDistance(E2DPoint<T> point, E2DLine<T> line)
    {
        var a = GetPointMultivector(point);

        var q1 = GetPointMultivector(line.Point1);
        var q2 = GetPointMultivector(line.Point2);

        var i = GeometricProcessor.CreatePseudoScalarInverse(VSpaceDimensions);

        return a.Op(q1).Op(q2).Lcp(i).AsScalar();
    }

    public Scalar<T> GetDistance(E2DLine<T> line1, E2DLine<T> line2)
    {
        var p1 = GetPointMultivector(line1.Point1);
        var p2 = GetPointMultivector(line1.Point2);

        var q1 = GetPointMultivector(line2.Point1);
        var q2 = GetPointMultivector(line2.Point2);

        var i = GeometricProcessor.CreatePseudoScalarInverse(VSpaceDimensions);

        return p1.Op(p2).Op(q1).Op(q2).Lcp(i).AsScalar();
    }

    public E2DLineLineIntersectionRecord<T> GetIntersection(E2DLine<T> line1, E2DLine<T> line2)
    {
        var l1P1Dist = GetDistance(line1.Point1, line2);
        var l1P2Dist = GetDistance(line1.Point2, line2);
        var t1 = l1P1Dist / (l1P1Dist - l1P2Dist);

        var l2P1Dist = GetDistance(line2.Point1, line1);
        var l2P2Dist = GetDistance(line2.Point2, line1);
        var t2 = l2P1Dist / (l2P1Dist - l2P2Dist);

        // Make sure we get a single intersection point
        Debug.Assert(
            (line1.GetPoint(t1) - line2.GetPoint(t2)).GetNormSquared().IsNearZero()
        );
        
        // Line-line intersection condition:
        //    l1P1Dist != l1P2Dist && l2P1Dist != l2P2Dist
        // also:
        //    t1 and t2 are finite numbers

        // Ray-line intersection condition:
        //    l1P2Dist < l1P1Dist
        // also:
        //    t1 > 0

        // Segment-line intersection condition:
        //    (l1P1Dist < 0 && l1P2Dist > 0) || (l1P1Dist > 0 && l1P2Dist < 0)
        // also:
        //    0 < t1 < 1
        // also:
        //    t1 > 0 && 1 - t1 > 0

        // Ray-ray intersection condition:
        //    l1P2Dist < l1P1Dist && l2P2Dist < l2P1Dist
        // also:
        //    t1 > 0 && t2 > 0

        // Segment-ray intersection condition:
        //    ((l1P1Dist < 0 && l1P2Dist > 0) || (l1P1Dist > 0 && l1P2Dist < 0)) && l2P2Dist < l2P1Dist
        // also:
        //    0 < t1 < 1 && t2 > 0
        // also:
        //    t1 > 0 && 1 - t1 > 0 && t2 > 0

        // Segment-segment intersection condition:
        //    ((l1P1Dist < 0 && l1P2Dist > 0) || (l1P1Dist > 0 && l1P2Dist < 0)) && ((l2P1Dist < 0 && l2P2Dist > 0) || (l2P1Dist > 0 && l2P2Dist < 0))
        // also:
        //    0 < t1 < 1 && 0 < t2 < 1
        // also:
        //    t1 > 0 && 1 - t1 > 0 && t2 > 0 && 1 - t2 > 0

        return new E2DLineLineIntersectionRecord<T>(ScalarProcessor)
        {
            Line1Point1Distance = l1P1Dist,
            Line1Point2Distance = l1P2Dist,
            Line2Point1Distance = l2P1Dist,
            Line2Point2Distance = l2P2Dist
        };
    }
}