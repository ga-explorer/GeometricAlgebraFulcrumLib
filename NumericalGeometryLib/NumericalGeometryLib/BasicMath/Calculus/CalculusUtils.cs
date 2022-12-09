using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;
using NumericalGeometryLib.Polynomials.CurveBasis;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using SixLabors.ImageSharp;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace NumericalGeometryLib.BasicMath.Calculus
{
    public static class CalculusUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetCatmullRomParameterValue(this double p, Quad<double> tQuad, Quad<double> pQuad)
        {
            return MathNet.Numerics.RootFinding.RobustNewtonRaphson.FindRoot(
                t => GetCatmullRomValue(t, tQuad, pQuad) - p,
                t => GetCatmullRomDerivativeValue(t, tQuad, pQuad),
                tQuad.Item2,
                tQuad.Item3
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<bool, double> TryGetCatmullRomParameterValue(this double p, Quad<double> tQuad, Quad<double> pQuad)
        {
            var rootFound = MathNet.Numerics.RootFinding.RobustNewtonRaphson.TryFindRoot(
                t => GetCatmullRomValue(t, tQuad, pQuad) - p,
                t => GetCatmullRomDerivativeValue(t, tQuad, pQuad),
                tQuad.Item2,
                tQuad.Item3,
                1e-8,
                200,
                40,
                out var rootValue
            );

            return new Tuple<bool, double>(rootFound, rootValue);
        }


        /// <summary>
        /// Unlike the other implementation here, which uses the default "uniform"
        /// treatment of t, this computation is used to calculate the same values but
        /// introduces the ability to "parametrize" the t values used in the
        /// calculation. This is based on Figure 3 from
        /// http://www.cemyuksel.com/research/catmullrom_param/catmullrom.pdf
        /// </summary>
        /// <param name="pQuad">An array of double values of length 4, where interpolation occurs from p1 to p2.</param>
        /// <param name="tQuad">An array of time measures of length 4, corresponding to each p value.</param>
        /// <param name="t">the actual interpolation ratio from 0 to 1 representing the position between p1 and p2 to interpolate the value.</param>
        /// <returns>The interpolated value</returns>
        public static double GetCatmullRomValue(this double t, Quad<double> tQuad, Quad<double> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = pQuad;

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            var p210 = (p21 * tt0 - p10 * tt2) / t210;
            var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            return p3210.NaNToZero();
        }
        
        /// <summary>
        /// Unlike the other implementation here, which uses the default "uniform"
        /// treatment of t, this computation is used to calculate the same values but
        /// introduces the ability to "parametrize" the t values used in the
        /// calculation. This is based on Figure 3 from
        /// http://www.cemyuksel.com/research/catmullrom_param/catmullrom.pdf
        /// </summary>
        /// <param name="pQuad">An array of double values of length 4, where interpolation occurs from p1 to p2.</param>
        /// <param name="tQuad">An array of time measures of length 4, corresponding to each p value.</param>
        /// <param name="t">the actual interpolation ratio from 0 to 1 representing the position between p1 and p2 to interpolate the value.</param>
        /// <returns>The interpolated value</returns>
        public static Float64Tuple2D GetCatmullRomValue(this double t, Quad<double> tQuad, Quad<IFloat64Tuple2D> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = 
                pQuad.MapItems(p => p.ToTuple2D());

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            var p210 = (p21 * tt0 - p10 * tt2) / t210;
            var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            return p3210;
        }
        
        /// <summary>
        /// Unlike the other implementation here, which uses the default "uniform"
        /// treatment of t, this computation is used to calculate the same values but
        /// introduces the ability to "parametrize" the t values used in the
        /// calculation. This is based on Figure 3 from
        /// http://www.cemyuksel.com/research/catmullrom_param/catmullrom.pdf
        /// </summary>
        /// <param name="pQuad">An array of double values of length 4, where interpolation occurs from p1 to p2.</param>
        /// <param name="tQuad">An array of time measures of length 4, corresponding to each p value.</param>
        /// <param name="t">the actual interpolation ratio from 0 to 1 representing the position between p1 and p2 to interpolate the value.</param>
        /// <returns>The interpolated value</returns>
        public static Float64Tuple3D GetCatmullRomValue(this double t, Quad<double> tQuad, Quad<IFloat64Tuple3D> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = 
                pQuad.MapItems(p => p.ToTuple3D());

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            var p210 = (p21 * tt0 - p10 * tt2) / t210;
            var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            return p3210;
        }
        
        /// <summary>
        /// Unlike the other implementation here, which uses the default "uniform"
        /// treatment of t, this computation is used to calculate the same values but
        /// introduces the ability to "parametrize" the t values used in the
        /// calculation. This is based on Figure 3 from
        /// http://www.cemyuksel.com/research/catmullrom_param/catmullrom.pdf
        /// </summary>
        /// <param name="pQuad">An array of double values of length 4, where interpolation occurs from p1 to p2.</param>
        /// <param name="tQuad">An array of time measures of length 4, corresponding to each p value.</param>
        /// <param name="t">the actual interpolation ratio from 0 to 1 representing the position between p1 and p2 to interpolate the value.</param>
        /// <returns>The interpolated value</returns>
        public static Float64Tuple4D GetCatmullRomValue(this double t, Quad<double> tQuad, Quad<IFloat64Tuple4D> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = 
                pQuad.MapItems(p => p.ToTuple4D());

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            var p210 = (p21 * tt0 - p10 * tt2) / t210;
            var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            return p3210;
        }
        
        /// <summary>
        /// Unlike the other implementation here, which uses the default "uniform"
        /// treatment of t, this computation is used to calculate the same values but
        /// introduces the ability to "parametrize" the t values used in the
        /// calculation. This is based on Figure 3 from
        /// http://www.cemyuksel.com/research/catmullrom_param/catmullrom.pdf
        /// </summary>
        /// <param name="pQuad">An array of double values of length 4, where interpolation occurs from p1 to p2.</param>
        /// <param name="tQuad">An array of time measures of length 4, corresponding to each p value.</param>
        /// <param name="t">the actual interpolation ratio from 0 to 1 representing the position between p1 and p2 to interpolate the value.</param>
        /// <returns>The interpolated value</returns>
        public static Float64Tuple GetCatmullRomValue(this double t, Quad<double> tQuad, Quad<Float64Tuple> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = pQuad;

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            var p210 = (p21 * tt0 - p10 * tt2) / t210;
            var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            return p3210;
        }

        ///// <summary>
        ///// Unlike the other implementation here, which uses the default "uniform"
        ///// treatment of t, this computation is used to calculate the same values but
        ///// introduces the ability to "parametrize" the t values used in the
        ///// calculation. This is based on Figure 3 from
        ///// http://www.cemyuksel.com/research/catmullrom_param/catmullrom.pdf
        ///// </summary>
        ///// <param name="pQuad">An array of double values of length 4, where interpolation occurs from p1 to p2.</param>
        ///// <param name="tQuad">An array of time measures of length 4, corresponding to each p value.</param>
        ///// <param name="t">the actual interpolation ratio from 0 to 1 representing the position between p1 and p2 to interpolate the value.</param>
        ///// <returns>The interpolated value</returns>
        //public static Float64SparseTuple GetCatmullRomValue(this double t, Quad<double> tQuad, Quad<Float64SparseTuple> pQuad)
        //{
        //    var (t0, t1, t2, t3) = tQuad;
        //    var (p0, p1, p2, p3) = pQuad;

        //    var tt0 = t - t0;
        //    var tt1 = t - t1;
        //    var tt2 = t - t2;
        //    var tt3 = t - t3;

        //    var t10 = t1 - t0;
        //    var t21 = t2 - t1;
        //    var t32 = t3 - t2;

        //    var p10 = (p1 * tt0 - p0 * tt1) / t10;
        //    var p21 = (p2 * tt1 - p1 * tt2) / t21;
        //    var p32 = (p3 * tt2 - p2 * tt3) / t32;

        //    var t210 = t2 - t0;
        //    var t321 = t3 - t1;

        //    var p210 = (p21 * tt0 - p10 * tt2) / t210;
        //    var p321 = (p32 * tt1 - p21 * tt3) / t321;

        //    var t3210 = t2 - t1;

        //    var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

        //    return p3210;
        //}


        public static double GetCatmullRomDerivativeValue(this double t, Quad<double> tQuad, Quad<double> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = pQuad;

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            var p210 = (p21 * tt0 - p10 * tt2) / t210;
            var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            //var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            var dp10 = (p1 - p0) / t10;
            var dp21 = (p2 - p1) / t21;
            var dp32 = (p3 - p2) / t32;

            var dp210 = (p21 - p10 + dp21 * tt0 - dp10 * tt2) / t210;
            var dp321 = (p32 - p21 + dp32 * tt1 - dp21 * tt3) / t321;

            var dp3210 = (p321 - p210 + dp321 * tt1 - dp210 * tt2) / t3210;

            return dp3210.NaNToZero();
        }
        
        public static Float64Tuple2D GetCatmullRomDerivativeValue(this double t, Quad<double> tQuad, Quad<IFloat64Tuple2D> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = 
                pQuad.MapItems(p => p.ToTuple2D());

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            var p210 = (p21 * tt0 - p10 * tt2) / t210;
            var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            //var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            var dp10 = (p1 - p0) / t10;
            var dp21 = (p2 - p1) / t21;
            var dp32 = (p3 - p2) / t32;

            var dp210 = (p21 - p10 + dp21 * tt0 - dp10 * tt2) / t210;
            var dp321 = (p32 - p21 + dp32 * tt1 - dp21 * tt3) / t321;

            var dp3210 = (p321 - p210 + dp321 * tt1 - dp210 * tt2) / t3210;

            return dp3210;
        }
        
        public static Float64Tuple3D GetCatmullRomDerivativeValue(this double t, Quad<double> tQuad, Quad<IFloat64Tuple3D> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = 
                pQuad.MapItems(p => p.ToTuple3D());

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            var p210 = (p21 * tt0 - p10 * tt2) / t210;
            var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            //var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            var dp10 = (p1 - p0) / t10;
            var dp21 = (p2 - p1) / t21;
            var dp32 = (p3 - p2) / t32;

            var dp210 = (p21 - p10 + dp21 * tt0 - dp10 * tt2) / t210;
            var dp321 = (p32 - p21 + dp32 * tt1 - dp21 * tt3) / t321;

            var dp3210 = (p321 - p210 + dp321 * tt1 - dp210 * tt2) / t3210;

            return dp3210;
        }
        
        public static Float64Tuple4D GetCatmullRomDerivativeValue(this double t, Quad<double> tQuad, Quad<IFloat64Tuple4D> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = 
                pQuad.MapItems(p => p.ToTuple4D());

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            var p210 = (p21 * tt0 - p10 * tt2) / t210;
            var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            //var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            var dp10 = (p1 - p0) / t10;
            var dp21 = (p2 - p1) / t21;
            var dp32 = (p3 - p2) / t32;

            var dp210 = (p21 - p10 + dp21 * tt0 - dp10 * tt2) / t210;
            var dp321 = (p32 - p21 + dp32 * tt1 - dp21 * tt3) / t321;

            var dp3210 = (p321 - p210 + dp321 * tt1 - dp210 * tt2) / t3210;

            return dp3210;
        }
        
        public static Float64Tuple GetCatmullRomDerivativeValue(this double t, Quad<double> tQuad, Quad<Float64Tuple> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = pQuad;

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            var p210 = (p21 * tt0 - p10 * tt2) / t210;
            var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            //var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            var dp10 = (p1 - p0) / t10;
            var dp21 = (p2 - p1) / t21;
            var dp32 = (p3 - p2) / t32;

            var dp210 = (p21 - p10 + dp21 * tt0 - dp10 * tt2) / t210;
            var dp321 = (p32 - p21 + dp32 * tt1 - dp21 * tt3) / t321;

            var dp3210 = (p321 - p210 + dp321 * tt1 - dp210 * tt2) / t3210;

            return dp3210;
        }
        
        //public static Float64SparseTuple GetCatmullRomDerivativeValue(this double t, Quad<double> tQuad, Quad<Float64SparseTuple> pQuad)
        //{
        //    var (t0, t1, t2, t3) = tQuad;
        //    var (p0, p1, p2, p3) = pQuad;

        //    var tt0 = t - t0;
        //    var tt1 = t - t1;
        //    var tt2 = t - t2;
        //    var tt3 = t - t3;

        //    var t10 = t1 - t0;
        //    var t21 = t2 - t1;
        //    var t32 = t3 - t2;

        //    var p10 = (p1 * tt0 - p0 * tt1) / t10;
        //    var p21 = (p2 * tt1 - p1 * tt2) / t21;
        //    var p32 = (p3 * tt2 - p2 * tt3) / t32;

        //    var t210 = t2 - t0;
        //    var t321 = t3 - t1;

        //    var p210 = (p21 * tt0 - p10 * tt2) / t210;
        //    var p321 = (p32 * tt1 - p21 * tt3) / t321;

        //    var t3210 = t2 - t1;

        //    //var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

        //    var dp10 = (p1 - p0) / t10;
        //    var dp21 = (p2 - p1) / t21;
        //    var dp32 = (p3 - p2) / t32;

        //    var dp210 = (p21 - p10 + dp21 * tt0 - dp10 * tt2) / t210;
        //    var dp321 = (p32 - p21 + dp32 * tt1 - dp21 * tt3) / t321;

        //    var dp3210 = (p321 - p210 + dp321 * tt1 - dp210 * tt2) / t3210;

        //    return dp3210;
        //}


        public static double GetCatmullRomDerivative2Value(this double t, Quad<double> tQuad, Quad<double> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = pQuad;

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            //var p210 = (p21 * tt0 - p10 * tt2) / t210;
            //var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            //var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            var dp10 = (p1 - p0) / t10;
            var dp21 = (p2 - p1) / t21;
            var dp32 = (p3 - p2) / t32;

            var dp210 = (p21 - p10 + dp21 * tt0 - dp10 * tt2) / t210;
            var dp321 = (p32 - p21 + dp32 * tt1 - dp21 * tt3) / t321;

            //var dp3210 = (p321 - p210 + dp321 * tt1 - dp210 * tt2) / t3210;

            var d2p210 = 2d * (dp21 - dp10) / t210;
            var d2p321 = 2d * (dp32 - dp21) / t321;

            var d2p3210 = (2d * (dp321 - dp210) + d2p321 * tt1 - d2p210 * tt2) / t3210;

            return d2p3210.NaNToZero();
        }
        
        public static Float64Tuple2D GetCatmullRomDerivative2Value(this double t, Quad<double> tQuad, Quad<IFloat64Tuple2D> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = 
                pQuad.MapItems(p => p.ToTuple2D());

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            //var p210 = (p21 * tt0 - p10 * tt2) / t210;
            //var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            //var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            var dp10 = (p1 - p0) / t10;
            var dp21 = (p2 - p1) / t21;
            var dp32 = (p3 - p2) / t32;

            var dp210 = (p21 - p10 + dp21 * tt0 - dp10 * tt2) / t210;
            var dp321 = (p32 - p21 + dp32 * tt1 - dp21 * tt3) / t321;

            //var dp3210 = (p321 - p210 + dp321 * tt1 - dp210 * tt2) / t3210;

            var d2p210 = 2d * (dp21 - dp10) / t210;
            var d2p321 = 2d * (dp32 - dp21) / t321;

            var d2p3210 = (2d * (dp321 - dp210) + d2p321 * tt1 - d2p210 * tt2) / t3210;

            return d2p3210;
        }
        
        public static Float64Tuple3D GetCatmullRomDerivative2Value(this double t, Quad<double> tQuad, Quad<IFloat64Tuple3D> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = 
                pQuad.MapItems(p => p.ToTuple3D());

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            //var p210 = (p21 * tt0 - p10 * tt2) / t210;
            //var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            //var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            var dp10 = (p1 - p0) / t10;
            var dp21 = (p2 - p1) / t21;
            var dp32 = (p3 - p2) / t32;

            var dp210 = (p21 - p10 + dp21 * tt0 - dp10 * tt2) / t210;
            var dp321 = (p32 - p21 + dp32 * tt1 - dp21 * tt3) / t321;

            //var dp3210 = (p321 - p210 + dp321 * tt1 - dp210 * tt2) / t3210;

            var d2p210 = 2d * (dp21 - dp10) / t210;
            var d2p321 = 2d * (dp32 - dp21) / t321;

            var d2p3210 = (2d * (dp321 - dp210) + d2p321 * tt1 - d2p210 * tt2) / t3210;

            return d2p3210;
        }
        
        public static Float64Tuple4D GetCatmullRomDerivative2Value(this double t, Quad<double> tQuad, Quad<IFloat64Tuple4D> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = 
                pQuad.MapItems(p => p.ToTuple4D());

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            //var p210 = (p21 * tt0 - p10 * tt2) / t210;
            //var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            //var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            var dp10 = (p1 - p0) / t10;
            var dp21 = (p2 - p1) / t21;
            var dp32 = (p3 - p2) / t32;

            var dp210 = (p21 - p10 + dp21 * tt0 - dp10 * tt2) / t210;
            var dp321 = (p32 - p21 + dp32 * tt1 - dp21 * tt3) / t321;

            //var dp3210 = (p321 - p210 + dp321 * tt1 - dp210 * tt2) / t3210;

            var d2p210 = 2d * (dp21 - dp10) / t210;
            var d2p321 = 2d * (dp32 - dp21) / t321;

            var d2p3210 = (2d * (dp321 - dp210) + d2p321 * tt1 - d2p210 * tt2) / t3210;

            return d2p3210;
        }
        
        public static Float64Tuple GetCatmullRomDerivative2Value(this double t, Quad<double> tQuad, Quad<Float64Tuple> pQuad)
        {
            var (t0, t1, t2, t3) = tQuad;
            var (p0, p1, p2, p3) = pQuad;

            var tt0 = t - t0;
            var tt1 = t - t1;
            var tt2 = t - t2;
            var tt3 = t - t3;

            var t10 = t1 - t0;
            var t21 = t2 - t1;
            var t32 = t3 - t2;

            var p10 = (p1 * tt0 - p0 * tt1) / t10;
            var p21 = (p2 * tt1 - p1 * tt2) / t21;
            var p32 = (p3 * tt2 - p2 * tt3) / t32;

            var t210 = t2 - t0;
            var t321 = t3 - t1;

            //var p210 = (p21 * tt0 - p10 * tt2) / t210;
            //var p321 = (p32 * tt1 - p21 * tt3) / t321;

            var t3210 = t2 - t1;

            //var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

            var dp10 = (p1 - p0) / t10;
            var dp21 = (p2 - p1) / t21;
            var dp32 = (p3 - p2) / t32;

            var dp210 = (p21 - p10 + dp21 * tt0 - dp10 * tt2) / t210;
            var dp321 = (p32 - p21 + dp32 * tt1 - dp21 * tt3) / t321;

            //var dp3210 = (p321 - p210 + dp321 * tt1 - dp210 * tt2) / t3210;

            var d2p210 = 2d * (dp21 - dp10) / t210;
            var d2p321 = 2d * (dp32 - dp21) / t321;

            var d2p3210 = (2d * (dp321 - dp210) + d2p321 * tt1 - d2p210 * tt2) / t3210;

            return d2p3210;
        }
        
        //public static Float64SparseTuple GetCatmullRomDerivative2Value(this double t, Quad<double> tQuad, Quad<Float64SparseTuple> pQuad)
        //{
        //    var (t0, t1, t2, t3) = tQuad;
        //    var (p0, p1, p2, p3) = pQuad;

        //    var tt0 = t - t0;
        //    var tt1 = t - t1;
        //    var tt2 = t - t2;
        //    var tt3 = t - t3;

        //    var t10 = t1 - t0;
        //    var t21 = t2 - t1;
        //    var t32 = t3 - t2;

        //    var p10 = (p1 * tt0 - p0 * tt1) / t10;
        //    var p21 = (p2 * tt1 - p1 * tt2) / t21;
        //    var p32 = (p3 * tt2 - p2 * tt3) / t32;

        //    var t210 = t2 - t0;
        //    var t321 = t3 - t1;

        //    //var p210 = (p21 * tt0 - p10 * tt2) / t210;
        //    //var p321 = (p32 * tt1 - p21 * tt3) / t321;

        //    var t3210 = t2 - t1;

        //    //var p3210 = (p321 * tt1 - p210 * tt2) / t3210;

        //    var dp10 = (p1 - p0) / t10;
        //    var dp21 = (p2 - p1) / t21;
        //    var dp32 = (p3 - p2) / t32;

        //    var dp210 = (p21 - p10 + dp21 * tt0 - dp10 * tt2) / t210;
        //    var dp321 = (p32 - p21 + dp32 * tt1 - dp21 * tt3) / t321;

        //    //var dp3210 = (p321 - p210 + dp321 * tt1 - dp210 * tt2) / t3210;

        //    var d2p210 = 2d * (dp21 - dp10) / t210;
        //    var d2p321 = 2d * (dp32 - dp21) / t321;

        //    var d2p3210 = (2d * (dp321 - dp210) + d2p321 * tt1 - d2p210 * tt2) / t3210;

        //    return d2p3210;
        //}


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static SmoothedCatmullRomSplineFunction CreateCatmullRomSplineFunction(this IEnumerable<double> pointList, CatmullRomSplineType splineType, double tMin, double tMax)
        //{
        //    return SmoothedCatmullRomSplineFunction.Create(tMin, tMax, pointList, splineType);
        //}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AkimaSplineFunction CreateAkimaSplineFunction(this IReadOnlyList<double> pointList, double tMin, double tMax)
        {
            var tValues = 
                tMin.GetLinearRange(tMax, pointList.Count, false);

            return AkimaSplineFunction.Create(tValues, pointList, true);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AkimaSplineFunction CreateAkimaSplineFunction(this IEnumerable<double> pointList, IEnumerable<double> tValues, bool xSorted)
        {
            return AkimaSplineFunction.Create(tValues, pointList, xSorted);
        }

        public static double[] GetBezierSmoothingValues(this IReadOnlyList<double> xValues, int bezierDegree)
        {
            var basisSet = BernsteinBasisSet.Create(bezierDegree);

            var iList = 
                (1 + bezierDegree).GetRange().ToArray();

            var tList = 
                0d.GetLinearRange(1d, 11, false).ToArray();
                
            var xList = new List<double>{ xValues[0] };

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

        public static IEnumerable<Float64Tuple2D> GetBezierSmoothingPoints(this IEnumerable<double> yInputList, IEnumerable<double> xInputList, int bezierDegree, bool makeUniform)
        {
            var basisSet = BernsteinBasisSet.Create(bezierDegree);

            var xArray = xInputList.ToArray();
            var yArray = yInputList.ToArray();

            var valueCount = xArray.Length;

            // Modify some input points to ensure continuous first derivative
            for (var j = bezierDegree; j < valueCount - 2 * bezierDegree; j += bezierDegree)
            {
                var p0 = new Float64Tuple2D(xArray[j - 1], yArray[j - 1]);
                var p1 = new Float64Tuple2D(xArray[j], yArray[j]);
                var p2 = new Float64Tuple2D(xArray[j + 1], yArray[j + 1]);

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
                
            var xOutputList = new List<double>{ xArray[0] };
            var yOutputList = new List<double>{ yArray[0] };

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
                return xOutputList.Count.GetRange().Select(idx =>
                    new Float64Tuple2D(xOutputList[idx], yOutputList[idx])
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
                new Float64Tuple2D(xUniformArray[idx], yUniformArray[idx])
            );
        }

        public static Pair<double[]> GetBezierSmoothingPairs(this IEnumerable<double> yInputList, IEnumerable<double> xInputList, int bezierDegree, bool makeUniform)
        {
            var basisSet = BernsteinBasisSet.Create(bezierDegree);

            var xArray = xInputList.ToArray();
            var yArray = yInputList.ToArray();

            var valueCount = xArray.Length;

            // Modify some input points to ensure continuous first derivative
            for (var j = bezierDegree; j < valueCount - 2 * bezierDegree; j += bezierDegree)
            {
                var p0 = new Float64Tuple2D(xArray[j - 1], yArray[j - 1]);
                var p1 = new Float64Tuple2D(xArray[j], yArray[j]);
                var p2 = new Float64Tuple2D(xArray[j + 1], yArray[j + 1]);

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
                
            var xOutputList = new List<double>{ xArray[0] };
            var yOutputList = new List<double>{ yArray[0] };

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
        public static AkimaSplineFunction CreateSmoothedAkimaSplineFunction(this IEnumerable<double> yValues, IEnumerable<double> xValues, int bezierDegree)
        {
            var (xArray, yArray) = 
                yValues.GetBezierSmoothingPairs(xValues, bezierDegree, true);

            return yArray.CreateAkimaSplineFunction(xArray, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BarycentricPolynomialEquidistantFunction CreateBarycentricPolynomialEquidistantFunction(this IEnumerable<double> pointList, IEnumerable<double> tValues)
        {
            return BarycentricPolynomialEquidistantFunction.Create(tValues, pointList);
        }


        public static Image Plot(this Func<double, double> scalarFunction, double xMin, double xMax)
        {
            var model = new PlotModel
            {
                Background = OxyColors.White
            };

            var dx = (xMax - xMin) / 1024;
            
            model.Axes.Add(
                new LinearAxis
                {
                    Minimum = xMin - (xMax - xMin) / 20,
                    Maximum = xMax + (xMax - xMin) / 20,
                    Position = AxisPosition.Bottom
                }
            );

            model.Series.Add(
                new FunctionSeries(scalarFunction, xMin, xMax, dx)
                {
                    Color = OxyColors.Blue,
                    LineStyle = LineStyle.Solid,
                    StrokeThickness = 2
                }
            );
            
            var renderer = new PngExporter
            {
                //Dpi = 120,
                Width = 1200,
                Height = 600
            };

            using var stream = new MemoryStream();
            renderer.Export(model, stream);

            stream.Position = 0;

            return Image.Load(stream);
        }
        
        public static Image Plot(this Func<double, double> scalarFunction, double xMin, double xMax, double yMin, double yMax)
        {
            var model = new PlotModel
            {
                Background = OxyColors.White
            };

            var dx = (xMax - xMin) / 1024;
            
            model.Axes.Add(
                new LinearAxis
                {
                    Minimum = xMin - (xMax - xMin) / 20,
                    Maximum = xMax + (xMax - xMin) / 20,
                    Position = AxisPosition.Bottom
                }
            );
            
            model.Axes.Add(
                new LinearAxis
                {
                    Minimum = yMin - (yMax - yMin) / 20,
                    Maximum = yMax + (yMax - yMin) / 20,
                    Position = AxisPosition.Left
                }
            );

            model.Series.Add(
                new FunctionSeries(scalarFunction, xMin, xMax, dx)
                {
                    Color = OxyColors.Blue,
                    LineStyle = LineStyle.Solid,
                    StrokeThickness = 2
                }
            );
            
            var renderer = new PngExporter
            {
                //Dpi = 120,
                Width = 1200,
                Height = 600
            };

            using var stream = new MemoryStream();
            renderer.Export(model, stream);

            stream.Position = 0;

            return Image.Load(stream);
        }

        public static Image Plot(this Func<double, double> scalarFunction, IReadOnlyList<double> sampledFunction, IReadOnlyList<double> sampledXValues)
        {
            var model = new PlotModel
            {

            };

            var xMin = sampledXValues[0];
            var xMax = sampledXValues[^1];
            var dx = (xMax - xMin) / 1024;
            model.Series.Add(
                new FunctionSeries(scalarFunction, xMin, xMax, dx)
                {

                }
            );

            var scatterPoints =
                Enumerable
                    .Range(0, sampledXValues.Count)
                    .Select(i => new ScatterPoint(sampledXValues[i], sampledFunction[i]))
                    .ToList();

            var scatterSeries = new ScatterSeries
            {
                MarkerSize = 7,
                MarkerStroke = OxyColors.Black,
                MarkerFill = OxyColors.Blue,
                MarkerStrokeThickness = 1,
                MarkerType = MarkerType.Circle
            };

            scatterSeries.Points.AddRange(scatterPoints);

            model.Series.Add(scatterSeries);

            var renderer = new PngExporter
            {
                Dpi = 300,
                Width = 1200,
                Height = 600
            };

            using var stream = new MemoryStream();
            renderer.Export(model, stream);

            stream.Position = 0;

            return Image.Load(stream);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image PlotValue(this IScalarD0Function scalarFunction, double xMin, double xMax)
        {
            return ((Func<double, double>) scalarFunction.GetValue).Plot(xMin, xMax);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image PlotFirstDerivative(this IScalarD1Function scalarFunction, double xMin, double xMax)
        {
            return ((Func<double, double>) scalarFunction.GetFirstDerivativeValue).Plot(xMin, xMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image PlotSecondDerivative(this IScalarD2Function scalarFunction, double xMin, double xMax)
        {
            return ((Func<double, double>) scalarFunction.GetSecondDerivativeValue).Plot(xMin, xMax);
        }
    }
}

