using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Bezier
{
    public static class BezierCurveUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BernsteinBasis_0(this double t)
        {
            return 1;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BernsteinBasis_0_1(this double t)
        {
            return 1 - t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BernsteinBasis_1_1(this double t)
        {
            return t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<double> BernsteinBasis_1(this double t)
        {
            return new Pair<double>(1 - t, t);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BernsteinBasis_0_2(this double t)
        {
            var s = 1 - t;

            return s * s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BernsteinBasis_1_2(this double t)
        {
            return 2 * (1 - t) * t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BernsteinBasis_2_2(this double t)
        {
            return t * t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<double> BernsteinBasis_2(this double t)
        {
            var s = 1 - t;

            return new Triplet<double>(s * s, 2 * s * t, t * 3);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BernsteinBasis_0_3(this double t)
        {
            var s = 1 - t;

            return s * s * s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BernsteinBasis_1_3(this double t)
        {
            var s = 1 - t;

            return 3 * t * s * s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BernsteinBasis_2_3(this double t)
        {
            var s = 1 - t;

            return 3 * t * t * s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BernsteinBasis_3_3(this double t)
        {
            return t * t * t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<double> BernsteinBasis_3(this double t)
        {
            var t2 = t * t;
            var t3 = t * t2;

            var s = 1 - t;
            var s2 = s * s;
            var s3 = s * s2;

            return new Quad<double>(s3, 3 * s2 * t, 3 * s * t2, t3);
        }


        public static Float64Vector3D DeCasteljau(this double t, Float64Vector3D p0)
        {
            return new Float64Vector3D(p0);
        }

        public static Float64Vector3D DeCasteljau(this double t, Float64Vector3D p0, Float64Vector3D p1)
        {
            var s = 1.0d - t;

            return Float64Vector3D.Create(s * p0.X + t * p1.X,
                s * p0.Y + t * p1.Y,
                s * p0.Z + t * p1.Z);
        }

        public static Float64Vector3D DeCasteljau(this double t, Float64Vector3D p0, Float64Vector3D p1, Float64Vector3D p2)
        {
            //Not using the Lerp function to increase performance by avoiding heap allocations and function calls
            var s = 1.0d - t;

            var x0 = s * p0.X + t * p1.X;
            var y0 = s * p0.Y + t * p1.Y;
            var z0 = s * p0.Z + t * p1.Z;

            var x1 = s * p1.X + t * p2.X;
            var y1 = s * p1.Y + t * p2.Y;
            var z1 = s * p1.Z + t * p2.Z;

            return Float64Vector3D.Create(s * x0 + t * x1,
                s * y0 + t * y1,
                s * z0 + t * z1);
        }

        public static Float64Vector3D DeCasteljau(this double t, Float64Vector3D p0, Float64Vector3D p1, Float64Vector3D p2, Float64Vector3D p3)
        {
            //Not using the Lerp function to increase performance by avoiding heap allocations and function calls
            var s = 1.0d - t;

            var x0 = s * p0.X + t * p1.X;
            var y0 = s * p0.Y + t * p1.Y;
            var z0 = s * p0.Z + t * p1.Z;

            var x1 = s * p1.X + t * p2.X;
            var y1 = s * p1.Y + t * p2.Y;
            var z1 = s * p1.Z + t * p2.Z;

            var x2 = s * p2.X + t * p3.X;
            var y2 = s * p2.Y + t * p3.Y;
            var z2 = s * p2.Z + t * p3.Z;

            x0 = s * x0 + t * x1;
            y0 = s * y0 + t * y1;
            z0 = s * z0 + t * z1;

            x1 = s * x1 + t * x2;
            y1 = s * y1 + t * y2;
            z1 = s * z1 + t * z2;

            return Float64Vector3D.Create(s * x0 + t * x1,
                s * y0 + t * y1,
                s * z0 + t * z1);
        }

        public static Float64Vector3D DeCasteljau(this double t, params Float64Vector3D[] pointsList)
        {
            var pointsCount = pointsList.Length;

            if (pointsCount == 1)
                return new Float64Vector3D(pointsList[0]);

            if (pointsCount == 2)
                return t.Lerp(pointsList[0], pointsList[1]);

            //We have at least 3 points
            var s = 1.0d - t;
            pointsCount--;
            var xList = new double[pointsCount];
            var yList = new double[pointsCount];
            var zList = new double[pointsCount];

            //Perform first stage of linear interpolation on given points
            for (var i = 0; i < pointsCount; i++)
            {
                var j = i + 1;

                xList[i] = s * xList[i] + t * xList[j];
                yList[i] = s * yList[i] + t * yList[j];
                zList[i] = s * zList[i] + t * zList[j];
            }

            //Perform remaining stages of linear interpolation
            while (pointsCount > 2)
            {
                pointsCount--;

                for (var i = 0; i < pointsCount; i++)
                {
                    var j = i + 1;

                    xList[i] = s * xList[i] + t * xList[j];
                    yList[i] = s * yList[i] + t * yList[j];
                    zList[i] = s * zList[i] + t * zList[j];
                }
            }

            //Only two points remain; interpolate them at t
            return Float64Vector3D.Create(s * xList[0] + t * xList[1],
                s * yList[0] + t * yList[1],
                s * zList[0] + t * zList[1]);
        }
    }
}
