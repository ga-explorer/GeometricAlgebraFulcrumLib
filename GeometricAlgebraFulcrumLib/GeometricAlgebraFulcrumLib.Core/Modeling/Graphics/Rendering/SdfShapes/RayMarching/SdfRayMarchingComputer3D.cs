using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.SdfGeometry;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.SdfShapes.RayMarching;

public sealed class SdfRayMarchingComputer3D
{
    private static Tuple<bool, double> NoIntersection { get; }
        = new Tuple<bool, double>(false, 0);


    public int MinStepsCounter { get; private set; } = -1;

    public int MaxStepsCounter { get; private set; } = -1;


    public int MaxSteps { get; set; } = 32;

    public double MinDistance { get; set; } = 0.0d;

    public double MaxDistance { get; set; } = 100.0d;


    public void ResetCounters()
    {
        MinStepsCounter = -1;
        MaxStepsCounter = -1;
    }

    public Tuple<bool, double> ComputeIntersection1(ISdfGeometry3D surface, LinFloat64Vector3D rayOrigin, LinFloat64Vector3D rayDirection)
    {
        var depth = MinDistance;
        for (var i = 0; i < MaxSteps; i++) 
        {
            var rayPoint = rayOrigin + depth * rayDirection;
            var dist = surface.GetScalarDistance(rayPoint);
            var distAbs = Math.Abs(dist);

            if (distAbs < surface.SdfDistanceDelta)
                return new Tuple<bool, double>(true, depth);
                
            depth += dist;

            if (depth >= MaxDistance)
                return NoIntersection;
        }

        return NoIntersection;
    }

    public Tuple<bool, double> ComputeIntersection(ISdfGeometry3D surface, Line3D ray)
    {
        var t0 = MinDistance;
        var i = 0;
        while (i < MaxSteps) 
        {
            //Find new value for tDelta
            var distanceDelta = surface.ComputeSdfRayStep(ray, t0);

            //If distance between current point and previous point is 
            //small, return intersection point is found
            if (Math.Abs(distanceDelta) < surface.SdfDistanceDelta)
            {
                if (i < MinStepsCounter || MinStepsCounter < 0) MinStepsCounter = i;
                if (i > MaxStepsCounter || MaxStepsCounter < 0) MaxStepsCounter = i;

                return new Tuple<bool, double>(true, t0); 
            }

            t0 += distanceDelta;
            i++;
        }

        return NoIntersection;
    }

    /// <summary>
    /// http://iquilezles.org/www/articles/normalsSDF/normalsSDF.htm
    /// </summary>
    /// <param name="surface"></param>
    /// <param name="surfacePoint"></param>
    /// <returns></returns>
    public LinFloat64Vector3D ComputeNormal4(ISdfGeometry3D surface, LinFloat64Vector3D surfacePoint)
    {
        //var pointsArray = new Tuple3D[]
        //{ 
        //    new Tuple3D(
        //        surfacePoint.X + DistanceEpsilon,
        //        surfacePoint.Y - DistanceEpsilon,
        //        surfacePoint.Z - DistanceEpsilon
        //    ),
        //    new Tuple3D(
        //        surfacePoint.X - DistanceEpsilon,
        //        surfacePoint.Y - DistanceEpsilon,
        //        surfacePoint.Z + DistanceEpsilon
        //    ),
        //    new Tuple3D(
        //        surfacePoint.X - DistanceEpsilon,
        //        surfacePoint.Y + DistanceEpsilon,
        //        surfacePoint.Z - DistanceEpsilon
        //    ),
        //    new Tuple3D(
        //        surfacePoint.X + DistanceEpsilon,
        //        surfacePoint.Y + DistanceEpsilon,
        //        surfacePoint.Z + DistanceEpsilon
        //    )
        //};

        //var d = new double[4];

        //Parallel.For(0, 3, i => d[i] = surface.GetDistance(pointsArray[i]));

        //return new Tuple3D(
        //    d[3] + d[0] - d[1] - d[2],
        //    d[3] - d[0] - d[1] + d[2],
        //    d[3] - d[0] + d[1] - d[2]
        //).Normalize();


        var d1 = surface.GetScalarDistance(LinFloat64Vector3D.Create(surfacePoint.X + surface.SdfDistanceDelta,
            surfacePoint.Y - surface.SdfDistanceDelta,
            surfacePoint.Z - surface.SdfDistanceDelta));

        var d2 = surface.GetScalarDistance(LinFloat64Vector3D.Create(surfacePoint.X - surface.SdfDistanceDelta,
            surfacePoint.Y - surface.SdfDistanceDelta,
            surfacePoint.Z + surface.SdfDistanceDelta));

        var d3 = surface.GetScalarDistance(LinFloat64Vector3D.Create(surfacePoint.X - surface.SdfDistanceDelta,
            surfacePoint.Y + surface.SdfDistanceDelta,
            surfacePoint.Z - surface.SdfDistanceDelta));

        var d4 = surface.GetScalarDistance(LinFloat64Vector3D.Create(surfacePoint.X + surface.SdfDistanceDelta,
            surfacePoint.Y + surface.SdfDistanceDelta,
            surfacePoint.Z + surface.SdfDistanceDelta));


        return LinFloat64Vector3D.Create(d4 + d1 - d2 - d3,
            d4 - d1 - d2 + d3,
            d4 - d1 + d2 - d3).ToUnitLinVector3D();
    }

    /// <summary>
    /// http://iquilezles.org/www/articles/normalsSDF/normalsSDF.htm
    /// </summary>
    /// <param name="surface"></param>
    /// <param name="surfacePoint"></param>
    /// <returns></returns>
    public LinFloat64Vector3D GetNormalToSurface6(ISdfGeometry3D surface, LinFloat64Vector3D surfacePoint)
    {
        var dx1 = surface.GetScalarDistance(LinFloat64Vector3D.Create(surfacePoint.X - surface.SdfDistanceDelta,
            surfacePoint.Y,
            surfacePoint.Z));

        var dx2 = surface.GetScalarDistance(LinFloat64Vector3D.Create(surfacePoint.X + surface.SdfDistanceDelta,
            surfacePoint.Y,
            surfacePoint.Z));

        var dy1 = surface.GetScalarDistance(LinFloat64Vector3D.Create(surfacePoint.X,
            surfacePoint.Y - surface.SdfDistanceDelta,
            surfacePoint.Z));

        var dy2 = surface.GetScalarDistance(LinFloat64Vector3D.Create(surfacePoint.X,
            surfacePoint.Y + surface.SdfDistanceDelta,
            surfacePoint.Z));

        var dz1 = surface.GetScalarDistance(LinFloat64Vector3D.Create(surfacePoint.X,
            surfacePoint.Y,
            surfacePoint.Z - surface.SdfDistanceDelta));

        var dz2 = surface.GetScalarDistance(LinFloat64Vector3D.Create(surfacePoint.X,
            surfacePoint.Y,
            surfacePoint.Z + surface.SdfDistanceDelta));


        return LinFloat64Vector3D.Create(dx2 - dx1,
            dy2 - dy1,
            dz2 - dz1).ToUnitLinVector3D();
    }
}