using System;
using System.Collections.Generic;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Calculus;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
// ReSharper disable InconsistentNaming

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.CatmullRom;

public static class GrCatmullRomUtils
{
    /// <summary>
    /// This method will calculate the Catmull-Rom interpolation curve, returning
    /// it as a list of Tuple3D coordinate objects.  This method in particular
    /// adds the first and last control points which are not visible, but required
    /// for calculating the spline.
    /// https://stackoverflow.com/questions/9489736/catmull-rom-curve-with-no-cusps-and-no-self-intersections/23980479#23980479
    /// </summary>
    /// <param name="inputPointList">The list of original points to calculate an interpolation from.</param>
    /// <param name="pointsPerSegment">The number of equally spaced points to return along each curve. The actual distance between each point will depend on the spacing between the control points.</param>
    /// <param name="curveType">Chordal (stiff), Uniform (floppy), or Centripetal (medium)</param>
    /// <param name="isClosed">Handle the input points to get a closed curve</param>
    /// <returns>The list of interpolated points.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static List<Float64Tuple3D> CatmullRomInterpolate(this IEnumerable<Float64Tuple3D> inputPointList, int pointsPerSegment, CatmullRomSplineType curveType, bool isClosed)
    {
        var vertices = new List<Float64Tuple3D>(inputPointList);

        if (pointsPerSegment < 2)
            throw new ArgumentException("The pointsPerSegment parameter must be greater than 2, since 2 points is just the linear segment.");

        // Cannot interpolate curves given only two points.  Two points
        // is best represented as a simple line segment.
        if (vertices.Count < 3)
            return vertices;

        Float64Tuple3D startPoint, endPoint;

        if (isClosed)
        {
            // Test whether the shape is actually open or closed by checking to see if
            // the first point intersects with the last point.
            if (!vertices[0].GetDistanceSquaredToPoint(vertices[^1]).IsNearZero())
                vertices.Add(vertices[0]);
            
            // Use the second and second from last points as control points.
            startPoint = vertices[^2];
            endPoint = vertices[1];
        }
        else
        {
            // The shape is open, so use control points that simply extend
            // the first and last segments
            startPoint = 2 * vertices[0] - vertices[1];
            endPoint = 2 * vertices[^1] - vertices[^2];
        }

        // Insert the start and end control points.
        vertices.Insert(0, startPoint);
        vertices.Add(endPoint);

        var result = new List<Float64Tuple3D>();

        // When looping, each cycle requires 4 points, starting with i
        // and ending with i+3. So we don't loop through all the points.
        for (var i = 0; i < vertices.Count - 3; i++)
        {
            // Calculate the Catmull-Rom curve for one segment.
            var points = CatmullRomInterpolate(
                vertices, 
                i, 
                pointsPerSegment, 
                curveType
            );

            // Since the middle points are added twice, once for each bordering
            // segment, we only add the 0 index result point for the first
            // segment. Otherwise we will have duplicate points.
            if (result.Count > 0) 
                points.RemoveAt(0);

            // Add the coordinates for the segment to the result list.
            result.AddRange(points);
        }

        return result;
    }

    /// <summary>
    /// Given a list of control points, this will create a list of pointsPerSegment
    /// points spaced uniformly along the resulting Catmull-Rom curve.
    /// </summary>
    /// <param name="points">The list of control points, leading and ending with a coordinate that is only used for controlling the spline and is not visualized.</param>
    /// <param name="index">The index of control point p0, where p0, p1, p2, and p3 are used in order to create a curve between p1 and p2.</param>
    /// <param name="pointsPerSegment">The total number of uniformly spaced interpolated points to calculate for each segment. The larger this number, the smoother the resulting curve.</param>
    /// <param name="curveType">Clarifies whether the curve should use uniform, chordal or centripetal curve types. Uniform can produce loops, chordal can produce large distortions from the original lines, and centripetal is an optimal balance without spaces.</param>
    /// <returns>The list of coordinates that define the CatmullRom curve between the points defined by index+1 and index+2.</returns>
    private static List<Float64Tuple3D> CatmullRomInterpolate(this IReadOnlyList<Float64Tuple3D> points, int index, int pointsPerSegment, CatmullRomSplineType curveType)
    {
        var result = new List<Float64Tuple3D>();
        var tArray = new double[4];
        var xArray = new double[4];
        var yArray = new double[4];
        var zArray = new double[4];

        for (var i = 0; i < 4; i++)
        {
            //tArray[i] = i;
            xArray[i] = points[index + i].X;
            yArray[i] = points[index + i].Y;
            zArray[i] = points[index + i].Z;
        }

        var tStart = 1d;
        var tEnd = 2d;

        if (curveType != CatmullRomSplineType.Uniform)
        {
            var total = 0d;

            for (var i = 1; i < 4; i++)
            {
                var dx = xArray[i] - xArray[i - 1];
                var dy = yArray[i] - yArray[i - 1];
                var dz = zArray[i] - zArray[i - 1];
                var ds = dx * dx + dy * dy + dz * dz;

                var power = 
                    curveType == CatmullRomSplineType.Centripetal 
                        ? 0.25d : 0.5d;

                total += Math.Pow(ds, power);
                
                tArray[i] = total;
            }

            tStart = tArray[1];
            tEnd = tArray[2];
        }
        
        var segmentCount = pointsPerSegment - 1;

        result.Add(
            points[index + 1]
        );

        var tQuad = tArray.GetItemQuad(0);
        var xQuad = xArray.GetItemQuad(0);
        var yQuad = yArray.GetItemQuad(0);
        var zQuad = zArray.GetItemQuad(0);

        for (var i = 1d; i < segmentCount; i++)
        {
            var ti = tStart + i * (tEnd - tStart) / segmentCount;
            var xi = ti.GetCatmullRomValue(tQuad, xQuad);
            var yi = ti.GetCatmullRomValue(tQuad, yQuad);
            var zi = ti.GetCatmullRomValue(tQuad, zQuad);

            result.Add(
                new Float64Tuple3D(xi, yi, zi)
            );
        }

        result.Add(
            points[index + 2]
        );

        return result;
    }
}