using System.Collections.Generic;
using System.Linq;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.Curves.Space2D;
using EuclideanGeometryLib.Curves.Space3D;

namespace EuclideanGeometryLib.Curves
{
    public static class CurvesUtils
    {
        public static IEnumerable<Tuple2D> GetPointsAt(this ICurve2D curve, IEnumerable<double> tList)
            => tList.Select(curve.GetPointAt);

        public static Tuple2D[] GetPointsAt(this ICurve2D curve, params double[] tList)
            => tList.Select(curve.GetPointAt).ToArray();

        //public static Tuple2D[] ToPolyline(this ICurve2D curve, IBoundingBox1D curveParamRange, int pointsCount, double lengthError = 0.95)
        //{
        //    pointsCount = (Math.Max(pointsCount, 2) - 2).UpperPower2Limit();


        //}


        public static IEnumerable<Tuple3D> GetPointsAt(this ICurve3D curve, IEnumerable<double> tList)
            => tList.Select(curve.GetPointAt);

        public static Tuple3D[] GetPointsAt(this ICurve3D curve, params double[] tList)
            => tList.Select(curve.GetPointAt).ToArray();


    }
}
