using System.Collections.Generic;
using System.Linq;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves
{
    public static class GraphicsCurveUtils
    {
        public static IEnumerable<GrParametricCurveLocalFrame3D> GetTangentData(this IGraphicsParametricCurve3D curve, IEnumerable<double> parameterValueList)
        {
            return parameterValueList.Select(
                parameterValue =>
                    GrParametricCurveLocalFrame3D.CreateFrame(
                        parameterValue,
                        curve.GetPoint(parameterValue),
                        curve.GetTangent(parameterValue)
                    )
            );
        }
        
        public static double ComputeCurveLength(this IEnumerable<GrParametricCurveLocalFrame3D> framesList)
        {
            var arcLength = 0d;
            
            GrParametricCurveLocalFrame3D frame1 = null;
            var firstFrame = true;
            foreach (var frame2 in framesList)
            {
                if (firstFrame)
                {
                    frame1 = frame2;
                    firstFrame = false;
                    continue;
                }

                arcLength += frame2.Point.GetDistanceToPoint(frame1.Point);

                frame1 = frame2;
            }

            return arcLength;
        }

        public static IEnumerable<Tuple2D> GetPointsAt(this IGraphicsParametricCurve2D curve, IEnumerable<double> tList)
        {
            return tList.Select(curve.GetPoint);
        }

        public static Tuple2D[] GetPointsAt(this IGraphicsParametricCurve2D curve, params double[] tList)
        {
            return tList.Select(curve.GetPoint).ToArray();
        }

        //public static Tuple2D[] ToPolyline(this ICurve2D curve, IBoundingBox1D curveParamRange, int pointsCount, double lengthError = 0.95)
        //{
        //    pointsCount = (Math.Max(pointsCount, 2) - 2).UpperPower2Limit();


        //}


        public static IEnumerable<Tuple3D> GetPoints(this IGraphicsParametricCurve3D curve, IEnumerable<double> tList)
        {
            return tList.Select(curve.GetPoint);
        }

        public static Tuple3D[] GetPoints(this IGraphicsParametricCurve3D curve, params double[] tList)
        {
            return tList.Select(curve.GetPoint).ToArray();
        }
    }
}
