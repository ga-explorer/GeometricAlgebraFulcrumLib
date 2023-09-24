using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Interpolators;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.PolynomialAlgebra.CurveBasis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential
{
    public static class DifferentialUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Signal GetSmoothedSignal(this Float64Signal signal, int smoothingFactorsCount)
        {
            if (smoothingFactorsCount < 1) return signal;

            var smoothingFactors = 
                smoothingFactorsCount.GetRange(3).ToImmutableArray();

            var smoothingFactorsLcm = 
                smoothingFactors.Lcm();

            var smoothingSampleCountList = 
                smoothingFactors.Select(f => (smoothingFactorsLcm / f - 1) / 2);
            
            return signal.GetRunningAverageSignal(smoothingSampleCountList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Signal GetSmoothedSignal(this Float64Signal signal, IReadOnlyList<int> smoothingFactors)
        {
            if (smoothingFactors.Count == 0) return signal;

            var smoothingFactorsLcm = 
                smoothingFactors.Lcm();

            var smoothingSampleCountList = 
                smoothingFactors.Select(f => (smoothingFactorsLcm / f - 1) / 2);

            return signal.GetRunningAverageSignal(smoothingSampleCountList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Signal GetSmoothedSignal(this Float64Signal signal, DfSignalInterpolatorOptions options)
        {
            if (options.SmoothingFactors.Count == 0) return signal;

            var smoothingFactorsLcm = 
                options.SmoothingFactors.Lcm();

            var smoothingSampleCountList = 
                options.SmoothingFactors.Select(f => (smoothingFactorsLcm / f - 1) / 2);

            return signal.GetRunningAverageSignal(smoothingSampleCountList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAkimaSplineInterpolator CreateAkimaSplineFunction(this IReadOnlyList<double> pointList, double tMin, double tMax)
        {
            var tValues =
                tMin.GetLinearRange(tMax, pointList.Count, false);

            return DfAkimaSplineInterpolator.Create(tValues, pointList, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAkimaSplineInterpolator CreateAkimaSplineFunction(this IEnumerable<double> pointList, IEnumerable<double> tValues, bool xSorted)
        {
            return DfAkimaSplineInterpolator.Create(tValues, pointList, xSorted);
        }

        public static double[] GetBezierSmoothingValues(this IReadOnlyList<double> xValues, int bezierDegree)
        {
            var basisSet = BernsteinBasisSet.Create(bezierDegree);

            var iList =
                (1 + bezierDegree).GetRange().ToArray();

            var tList =
                0d.GetLinearRange(1d, 11, false).ToArray();

            var xList = new List<double> { xValues[0] };

            for (var j = 0; j < xValues.Count - bezierDegree; j += bezierDegree)
            {
                var i = j;

                xList.AddRange(
                    tList
                        .Skip(1)
                        .Select(t => basisSet.GetValues(t))
                        .Select(bList => iList.Sum(k => bList[k] * xValues[i + k]))
                );
            }

            return xList.ToArray();
        }

        public static IEnumerable<Float64Vector2D> GetBezierSmoothingPoints(this IEnumerable<double> yInputList, IEnumerable<double> xInputList, int bezierDegree, bool assumeUniformX)
        {
            var controlPointCount = bezierDegree + 1;

            var basisSet = BernsteinBasisSet.Create(bezierDegree);

            var xArray = xInputList.ToArray();
            var yArray = yInputList.ToArray();

            var valueCount = xArray.Length;

            // Modify some input points to ensure continuous first derivative
            for (var j = bezierDegree; j < valueCount - bezierDegree; j += bezierDegree)
            {
                var p0 = Float64Vector2D.Create((Float64Scalar)xArray[j - 1], (Float64Scalar)yArray[j - 1]);
                var p1 = Float64Vector2D.Create((Float64Scalar)xArray[j], (Float64Scalar)yArray[j]);
                var p2 = Float64Vector2D.Create((Float64Scalar)xArray[j + 1], (Float64Scalar)yArray[j + 1]);

                var t = (p1.X - p0.X) / (p2.X - p0.X);
                var s = 1d - t;

                yArray[j] = s * p0.Y + t * p2.Y;

                //var v = (p2 - p0).ToUnitVector();
                //var q0 = (p0 - p1).ProjectOnUnitVector(v);
                //var q2 = (p2 - p1).ProjectOnUnitVector(v);

                //q0 = p1 + q0 * (p0.X - p1.X) / q0.X;
                //q2 = p1 + q2 * (p2.X - p1.X) / q2.X;

                //Debug.Assert(
                //    (q0.X - p0.X).IsNearZero()
                //);

                //Debug.Assert(
                //    (q2.X - p2.X).IsNearZero()
                //);

                //Debug.Assert(
                //    (q2 - q0).ToUnitVector().IsNearParallelTo(v, 1e-7)
                //);

                //xArray[j - 1] = q0.X;
                //yArray[j - 1] = q0.Y;

                //xArray[j + 1] = q2.X;
                //yArray[j + 1] = q2.Y;
            }

            var kArray =
                controlPointCount.GetRange().ToArray();

            var tArray =
                0d.GetLinearRange(1d, controlPointCount, false).Skip(1).ToArray();

            var xOutputList = new List<double> { xArray[0] };
            var yOutputList = new List<double> { yArray[0] };

            for (var j = 0; j < valueCount - bezierDegree; j += bezierDegree)
            {
                foreach (var t in tArray)
                {
                    var bList =
                        basisSet.GetValues(t);

                    var x =
                        kArray.Sum(k => bList[k] * xArray[j + k]);

                    var y =
                        kArray.Sum(k => bList[k] * yArray[j + k]);

                    xOutputList.Add(x);
                    yOutputList.Add(y);
                }
            }

            Debug.Assert(
                xOutputList.Count == xArray.Length
            );

            if (assumeUniformX)
                return xOutputList.Count.GetRange().Select(idx =>
                    Float64Vector2D.Create((Float64Scalar)xOutputList[idx], (Float64Scalar)yOutputList[idx])
                );

            // Apply linear interpolation re-sampling to make x values uniform
            var xUniformArray =
                xOutputList[0].GetLinearRange(
                    xOutputList[^1],
                    xOutputList.Count,
                    false
                ).ToArray();

            var yUniformArray = new double[yOutputList.Count];

            var xIndex1 = 0;
            var i = 0;
            foreach (var x in xUniformArray)
            {
                var xIndex2 = xIndex1 + 1;
                var x1 = xOutputList[xIndex1];
                var x2 = xOutputList[xIndex2];

                while (x > x2)
                {
                    xIndex1++;
                    xIndex2++;

                    x1 = xOutputList[xIndex1];
                    x2 = xOutputList[xIndex2];
                }

                if (x == x1)
                {
                    yUniformArray[i++] = yOutputList[xIndex1];
                    continue;
                }

                if (x == x2)
                {
                    yUniformArray[i++] = yOutputList[xIndex2];
                    continue;
                }

                Debug.Assert(x > x1 && x < x2);

                var y1 = yOutputList[xIndex1];
                var y2 = yOutputList[xIndex2];

                yUniformArray[i++] = ((x - x1) / (x2 - x1)).Lerp(y1, y2);
            }

            return xOutputList.Count.GetRange().Select(idx =>
                Float64Vector2D.Create((Float64Scalar)xUniformArray[idx], (Float64Scalar)yUniformArray[idx])
            );
        }

        public static Pair<double[]> GetBezierSmoothingPairs(this IEnumerable<double> yInputList, IEnumerable<double> xInputList, int bezierDegree, bool makeUniform)
        {
            var basisSet = BernsteinBasisSet.Create(bezierDegree);

            var xArray = xInputList.ToArray();
            var yArray = yInputList.ToArray();

            var valueCount = xArray.Length;

            // Modify some input points to ensure continuous first derivative
            for (var j = bezierDegree; j < valueCount - bezierDegree; j += bezierDegree)
            {
                var p0 = Float64Vector2D.Create((Float64Scalar)xArray[j - 1], (Float64Scalar)yArray[j - 1]);
                var p1 = Float64Vector2D.Create((Float64Scalar)xArray[j], (Float64Scalar)yArray[j]);
                var p2 = Float64Vector2D.Create((Float64Scalar)xArray[j + 1], (Float64Scalar)yArray[j + 1]);

                var v = (p2 - p0).ToUnitVector();
                var q0 = (p0 - p1).ProjectOnUnitVector(v) + p1;
                var q2 = (p2 - p1).ProjectOnUnitVector(v) + p1;

                Debug.Assert(
                    (q2 - q0).ToUnitVector().IsNearParallelTo(v)
                );

                xArray[j - 1] = q0.X;
                yArray[j - 1] = q0.Y;

                xArray[j + 1] = q2.X;
                yArray[j + 1] = q2.Y;
            }

            var kArray =
                (1 + bezierDegree).GetRange().ToArray();

            var tArray =
                0d.GetLinearRange(1d, bezierDegree, false).Skip(1).ToArray();

            var xOutputList = new List<double> { xArray[0] };
            var yOutputList = new List<double> { yArray[0] };

            for (var j = 0; j < valueCount - bezierDegree; j += bezierDegree)
            {
                foreach (var t in tArray)
                {
                    var bList =
                        basisSet.GetValues(t);

                    var x =
                        kArray.Sum(k => bList[k] * xArray[j + k]);

                    var y =
                        kArray.Sum(k => bList[k] * yArray[j + k]);

                    xOutputList.Add(x);
                    yOutputList.Add(y);
                }
            }

            if (!makeUniform)
                return new Pair<double[]>(
                    xOutputList.ToArray(),
                    yOutputList.ToArray()
                );

            // Apply linear interpolation re-sampling to make x values uniform
            var xUniformArray =
                xOutputList[0].GetLinearRange(
                    xOutputList[^1],
                    xOutputList.Count,
                    false
                ).ToArray();

            var yUniformArray = new double[yOutputList.Count];

            var xIndex1 = 0;
            var i = 0;
            foreach (var x in xUniformArray)
            {
                var xIndex2 = xIndex1 + 1;
                var x1 = xOutputList[xIndex1];
                var x2 = xOutputList[xIndex2];

                while (x > x2)
                {
                    xIndex1++;
                    xIndex2++;

                    x1 = xOutputList[xIndex1];
                    x2 = xOutputList[xIndex2];
                }

                if (x == x1)
                {
                    yUniformArray[i++] = yOutputList[xIndex1];
                    continue;
                }

                if (x == x2)
                {
                    yUniformArray[i++] = yOutputList[xIndex2];
                    continue;
                }

                Debug.Assert(x > x1 && x < x2);

                var y1 = yOutputList[xIndex1];
                var y2 = yOutputList[xIndex2];

                yUniformArray[i++] = ((x - x1) / (x2 - x1)).Lerp(y1, y2);
            }

            return new Pair<double[]>(
                xUniformArray,
                yUniformArray
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAkimaSplineInterpolator CreateSmoothedAkimaSplineFunction(this IEnumerable<double> yValues, IEnumerable<double> xValues, int bezierDegree)
        {
            var (xArray, yArray) =
                yValues.GetBezierSmoothingPairs(xValues, bezierDegree, true);

            return yArray.CreateAkimaSplineFunction(xArray, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfBarycentricInterpolator CreateBarycentricPolynomialEquidistantFunction(this IEnumerable<double> pointList, IEnumerable<double> tValues)
        {
            return DfBarycentricInterpolator.Create(tValues, pointList);
        }


    }
}