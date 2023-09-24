using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space4D.Curves.CatmullRom;

public static class CatmullRom4DUtils
{
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
    public static Float64Vector4D GetCatmullRomValue(this double t, Quad<double> tQuad, Quad<IFloat64Vector4D> pQuad)
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

    public static Float64Vector4D GetCatmullRomDerivativeValue(this double t, Quad<double> tQuad, Quad<IFloat64Vector4D> pQuad)
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

    public static Float64Vector4D GetCatmullRomDerivative2Value(this double t, Quad<double> tQuad, Quad<IFloat64Vector4D> pQuad)
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

        var d2P210 = 2d * (dp21 - dp10) / t210;
        var d2P321 = 2d * (dp32 - dp21) / t321;

        var d2P3210 = (2d * (dp321 - dp210) + d2P321 * tt1 - d2P210 * tt2) / t3210;

        return d2P3210;
    }

}