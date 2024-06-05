using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Triangles.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.LatticeShapes.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.LatticeShapes.Surfaces;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.LatticeShapes;

public static class GrLatticeShapesUtils
{
    public static GrLatticeCurve3D AddXyCircle(this GrLatticeCurveList3D curveList, int samplesCount)
    {
        var curve = curveList.AddCurve(samplesCount, true);

        var thetaRange = 0d.GetLinearRange(2 * Math.PI, samplesCount, true).ToArray();

        for (var indexU = 0; indexU < samplesCount; indexU++)
        {
            var theta = thetaRange[indexU];
            var x = Math.Cos(theta);
            var y = Math.Sin(theta);

            curve.SetLatticeVertex(indexU, x, y, 0);
        }

        return curve;
    }

    public static GrLatticeCurve3D AddLine(this GrLatticeCurveList3D curveList, ILinFloat64Vector3D point1, ILinFloat64Vector3D point2, int samplesCount)
    {
        var curve = curveList.AddCurve(samplesCount, false);

        var tRange = 0d.GetLinearRange(1, samplesCount, false).ToArray();

        for (var indexU = 0; indexU < samplesCount; indexU++)
        {
            var t = tRange[indexU];
            var point = t.Lerp(point1, point2);

            curve.SetLatticeVertex(indexU, point);
        }

        return curve;
    }


    public static GrLatticeSurface3D AddZConeSurface(this GrLatticeSurfaceList3D surfaceList, int baseSamplesCount, int heightSamplesCount)
    {
        var surface = surfaceList.AddSurface(
            baseSamplesCount, 
            heightSamplesCount,
            true,
            false
        );

        //surface.ReverseNormals = true;

        var thetaRange = 0d.GetLinearRange(2 * Math.PI, baseSamplesCount, true).ToArray();
        var zRange = (-0.5d).GetLinearRange(0.5d, heightSamplesCount, false).ToArray();
        var rRange = (0.5).GetLinearRange(0d, heightSamplesCount, false).ToArray();

        for (var indexV = 0; indexV < heightSamplesCount - 1; indexV++)
        {
            var z = zRange[indexV];
            var r = rRange[indexV];

            for (var indexU = 0; indexU < baseSamplesCount; indexU++)
            {
                var theta = thetaRange[indexU];
                var x = r * Math.Cos(theta);
                var y = r * Math.Sin(theta);

                surface.SetLatticeVertex(indexU, indexV, x, y, z);
            }
        }

        surface.SetLatticeVerticesAtIndexV(heightSamplesCount - 1, new Triplet<Float64Scalar>(0, 0, 0.5d));

        return surface;
    }

    public static GrLatticeSurface3D AddClosedZConeSurface(this GrLatticeSurfaceList3D surfaceList, int baseSamplesCount, int heightSamplesCount)
    {
        var surface = surfaceList.AddSurface(
            baseSamplesCount, 
            1 + heightSamplesCount,
            true,
            false
        );

        //surface.ReverseNormals = true;

        var thetaRange = 0d.GetLinearRange(2 * Math.PI, baseSamplesCount, true).ToArray();
        var zRange = (-0.5d).GetLinearRange(0.5d, heightSamplesCount, false).ToArray();
        var rRange = (0.5).GetLinearRange(0d, heightSamplesCount, false).ToArray();

        surface.SetLatticeVerticesAtIndexV(0, new Triplet<Float64Scalar>(0, 0, -0.5d));

        for (var indexV = 1; indexV < heightSamplesCount; indexV++)
        {
            var z = zRange[indexV - 1];
            var r = rRange[indexV - 1];

            for (var indexU = 0; indexU < baseSamplesCount; indexU++)
            {
                var theta = thetaRange[indexU];
                var x = r * Math.Cos(theta);
                var y = r * Math.Sin(theta);

                surface.SetLatticeVertex(indexU, indexV, x, y, z);
            }
        }

        surface.SetLatticeVerticesAtIndexV(heightSamplesCount, new Triplet<Float64Scalar>(0, 0, 0.5d));

        return surface;
    }

    public static GrLatticeSurface3D AddZCylinderSurface(this GrLatticeSurfaceList3D surfaceList, int baseSamplesCount, int heightSamplesCount)
    {
        var surface = surfaceList.AddSurface(
            baseSamplesCount, 
            heightSamplesCount,
            true,
            false
        );

        //surface.ReverseNormals = true;

        var thetaRange = 0d.GetLinearRange(2 * Math.PI, baseSamplesCount, true).ToArray();
        var zRange = (-0.5d).GetLinearRange(0.5d, heightSamplesCount, false).ToArray();

        for (var indexV = 0; indexV < heightSamplesCount; indexV++)
        {
            var z = zRange[indexV];

            for (var indexU = 0; indexU < baseSamplesCount; indexU++)
            {
                var theta = thetaRange[indexU];
                var x = 0.5d * Math.Cos(theta);
                var y = 0.5d * Math.Sin(theta);

                surface.SetLatticeVertex(indexU, indexV, x, y, z);
            }
        }

        return surface;
    }

    public static GrLatticeSurface3D AddClosedZCylinderSurface(this GrLatticeSurfaceList3D surfaceList, int baseSamplesCount, int heightSamplesCount)
    {
        var surface = surfaceList.AddSurface(
            baseSamplesCount, 
            2 + heightSamplesCount,
            true,
            false
        );

        //surface.ReverseNormals = true;

        surface.SetLatticeVerticesAtIndexV(0, new Triplet<Float64Scalar>(0, 0, -0.5d));

        var thetaRange = 0d.GetLinearRange(2 * Math.PI, baseSamplesCount, true).ToArray();
        var zRange = (-0.5d).GetLinearRange(0.5d, heightSamplesCount, false).ToArray();

        for (var indexV = 1; indexV <= heightSamplesCount; indexV++)
        {
            var z = zRange[indexV];

            for (var indexU = 0; indexU < baseSamplesCount; indexU++)
            {
                var theta = thetaRange[indexU];
                var x = 0.5d * Math.Cos(theta);
                var y = 0.5d * Math.Sin(theta);

                surface.SetLatticeVertex(indexU, indexV, x, y, z);
            }
        }

        surface.SetLatticeVerticesAtIndexV(heightSamplesCount, new Triplet<Float64Scalar>(0, 0, 0.5d));

        return surface;
    }

    public static GrLatticeSurface3D AddZSphereSurface(this GrLatticeSurfaceList3D surfaceList, int thetaSamplesCount, int phiSamplesCount)
    {
        var surface = surfaceList.AddSurface(
            thetaSamplesCount, 
            phiSamplesCount, 
            true, 
            false
        );

        //surface.ReverseNormals = true;

        surface.SetLatticeVerticesAtIndexV(0, new Triplet<Float64Scalar>(0, 0, -0.5d));

        var thetaRange = 0d.GetLinearRange(2 * Math.PI, thetaSamplesCount, true).ToArray();
        var phiRange = Math.PI.GetLinearRange(0, phiSamplesCount, false).ToArray();

        //var evenSlice = false;
        //var sliceAngle = Math.PI / thetaSamplesCount;
        for (var indexV = 1; indexV < phiSamplesCount - 1; indexV++)
        {
            var phi = phiRange[indexV];
            var cosPhi = Math.Cos(phi);
            var sinPhi = Math.Sin(phi);

            for (var indexU = 0; indexU < thetaSamplesCount; indexU++)
            {
                var theta = thetaRange[indexU];

                //if (evenSlice)
                //    theta += sliceAngle;

                var cosTheta = Math.Cos(theta);
                var sinTheta = Math.Sin(theta);

                var x = 0.5d * cosTheta * sinPhi;
                var y = 0.5d * sinTheta * sinPhi;
                var z = 0.5d * cosPhi;
                    
                surface.SetLatticeVertex(indexU, indexV, x, y, z);
            }
                
            //evenSlice = !evenSlice;
        }

        surface.SetLatticeVerticesAtIndexV(phiSamplesCount - 1, new Triplet<Float64Scalar>(0, 0, 0.5d));

        return surface;
    }

    public static GrLatticeSurface3D AddTubeSurface(this GrLatticeSurfaceList3D surfaceList, IReadOnlyList<ParametricCurveLocalFrame3D> sampledCurve, double tubeRadius, int baseSamplesCount, bool closedTube)
    {
        var tubeSamples = sampledCurve.Count;

        var surface = surfaceList.AddSurface(
            baseSamplesCount, 
            tubeSamples,
            true,
            closedTube
        );

        //surface.ReverseNormals = true;

        var thetaRange = 0d.GetLinearRange(2 * Math.PI, baseSamplesCount, true).ToArray();

        for (var indexV = 0; indexV < tubeSamples; indexV++)
        {
            var frame = sampledCurve[indexV];

            for (var indexU = 0; indexU < baseSamplesCount; indexU++)
            {
                var theta = thetaRange[indexU];
                    
                var x = tubeRadius * Math.Cos(theta);
                var y = tubeRadius * Math.Sin(theta);

                var point = 
                    frame.Point + 
                    x * frame.Normal1.ToLinVector3D() + 
                    y * frame.Normal2.ToLinVector3D();

                surface.SetLatticeVertex(indexU, indexV, point);
            }
        }

        return surface;
    }
        

    public static LinFloat64Vector3D GetDisplacedPoint(this IGraphicsSurfaceLocalFrame3D vertex, double t)
    {
        return LinFloat64Vector3D.Create(vertex.Point.X + t * vertex.Normal.X,
            vertex.Point.Y + t * vertex.Normal.Y,
            vertex.Point.Z + t * vertex.Normal.Z);
    }
        
    public static LineSegment3D GetDisplacedLineSegment(this GrLatticeSurfaceLocalFrame3D vertex, double t1, double t2)
    {
        return LineSegment3D.Create(
            LinFloat64Vector3D.Create(vertex.Point.X + t1 * vertex.Normal.X,
                vertex.Point.Y + t1 * vertex.Normal.Y,
                vertex.Point.Z + t1 * vertex.Normal.Z),
            LinFloat64Vector3D.Create(vertex.Point.X + t2 * vertex.Normal.X,
                vertex.Point.Y + t2 * vertex.Normal.Y,
                vertex.Point.Z + t2 * vertex.Normal.Z)
        );
    }

    public static IEnumerable<ILineSegment3D> GetNormalLines(this GrLatticeSurfaceList3D geometry, double t2)
    {
        return geometry.Select(v => 
            LineSegment3D.Create(
                v, 
                v.GetPointInDirection(v.Normal, t2)
            )
        );
    }

    public static IEnumerable<ILineSegment3D> GetNormalLines(this GrLatticeSurfaceList3D geometry, double t1, double t2)
    {
        return geometry.Select(v => 
            LineSegment3D.Create(
                v.GetPointInDirection(v.Normal, t1),
                v.GetPointInDirection(v.Normal, t2)
            )
        );
    }

    public static IEnumerable<ITriangle3D> GetDisplacedTriangles(this GrLatticeSurfaceList3D geometry, double t)
    {
        foreach (var triangleIndices in geometry.TriangleVerticesList)
        {
            var (v1, v2, v3) = 
                triangleIndices.ToTuple();

            var p1 = v1.GetPointInDirection(v1.Normal, t);
            var p2 = v2.GetPointInDirection(v2.Normal, t);
            var p3 = v3.GetPointInDirection(v3.Normal, t);

            yield return Triangle3D.Create(p1, p2, p3);
        }
    }

    public static IEnumerable<ILineSegment3D> GetDisplacedTriangleEdges(this GrLatticeSurfaceList3D geometry, double t)
    {
        foreach (var (v1, v2, v3) in geometry.TriangleVerticesList)
        {
            var p1 = v1.GetPointInDirection(v1.Normal, t);
            var p2 = v2.GetPointInDirection(v2.Normal, t);
            var p3 = v3.GetPointInDirection(v3.Normal, t);

            yield return LineSegment3D.Create(p1, p2);
            yield return LineSegment3D.Create(p2, p3);
            yield return LineSegment3D.Create(p3, p1);
        }
    }
}