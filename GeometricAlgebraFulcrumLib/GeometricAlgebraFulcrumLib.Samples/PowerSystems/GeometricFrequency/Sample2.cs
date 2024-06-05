using System;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Subspaces;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using OxyPlot;
using OxyPlot.Series;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Samples.PowerSystems.GeometricFrequency;

public static class Sample2
{
    // This is a pre-defined scalar processor for numeric scalars
    public static ScalarProcessorOfFloat64 ScalarProcessor { get; }
        = ScalarProcessorOfFloat64.Instance;

    // Create a 3-dimensional Euclidean geometric algebra processor based on the
    // selected scalar processor
    public static RGaFloat64Processor GeometricProcessor { get; }
        = RGaFloat64Processor.Euclidean;

    public static int VSpaceDimensions 
        => 3;

    // This is a pre-defined text generator for displaying multivectors
    public static TextComposerFloat64 TextComposer { get; }
        = TextComposerFloat64.DefaultComposer;

    // This is a pre-defined LaTeX generator for displaying multivectors
    public static LaTeXComposerFloat64 LaTeXComposer { get; }
        = LaTeXComposerFloat64.DefaultComposer;


    public static double Va { get; }
        = 230d;

    public static double Vb { get; }
        = 225d;

    public static double Vc { get; }
        = 221.5d;

    public static double FreqHz { get; }
        = 60d;

    public static double Freq { get; }
        = 2 * Math.PI * FreqHz;

    public static double ConstA { get; }
        = -2 * Va * Va + Vb * Vb + Vc * Vc;

    public static double ConstB { get; }
        = Math.Sqrt(3) * (Vb * Vb - Vc * Vc);

    public static double ConstC { get; }
        = Va * Va + Vb * Vb + Vc * Vc;

    public static double ConstD { get; }
        = Math.Sqrt(ConstA * ConstA + ConstB * ConstB);

    public static RGaFloat64Vector K { get; }
        = GeometricProcessor.Vector(Vb * Vc, Va * Vc, Va * Vb);

    public static double KNorm { get; }
        = K.Norm();

    public static RGaFloat64Vector KUnit { get; }
        = K.DivideByNorm();

    public static RGaFloat64Bivector KDual { get; }
        = K.Dual(VSpaceDimensions).GetBivectorPart();


    public static RGaFloat64Vector Curve(double t)
    {
        const double pi = Math.PI;
        var sqrt2 = Math.Sqrt(2);

        return GeometricProcessor.Vector(
            sqrt2 * Va * (Freq * t).Cos(),
            sqrt2 * Vb * (Freq * t - 2 * pi / 3).Cos(),
            sqrt2 * Vc * (Freq * t + 2 * pi / 3).Cos()
        );
    }

    public static RGaFloat64Vector CurveDt1(double t)
    {
        var sqrt2 = Math.Sqrt(2);
        var sqrt3 = Math.Sqrt(3);
        var wtCos = (Freq * t).Cos();
        var wtSin = (Freq * t).Sin();

        return -Freq / sqrt2 * GeometricProcessor.Vector(
            2 * Va * wtSin,
            -Vb * (wtSin + sqrt3 * wtCos),
            -Vc * (wtSin - sqrt3 * wtCos)
        );
    }

    public static RGaFloat64Vector CurveDt2(double t)
    {
        var sqrt2 = Math.Sqrt(2);
        var sqrt3 = Math.Sqrt(3);
        var wtCos = (Freq * t).Cos();
        var wtSin = (Freq * t).Sin();

        return -Freq.Square() / sqrt2 * GeometricProcessor.Vector(
            2 * Va * wtCos,
            Vb * (sqrt3 * wtSin - wtCos),
            -Vc * (sqrt3 * wtSin + wtCos)
        );
    }

    public static RGaFloat64Vector CurveDt3(double t)
    {
        var sqrt2 = Math.Sqrt(2);
        var sqrt3 = Math.Sqrt(3);
        var wtCos = (Freq * t).Cos();
        var wtSin = (Freq * t).Sin();

        return Freq.Cube() / sqrt2 * GeometricProcessor.Vector(
            2 * Va * wtSin,
            -Vb * (wtSin + sqrt3 * wtCos),
            -Vc * (wtSin - sqrt3 * wtCos)
        );
    }

    private static double G(double t)
    {
        return ConstA * (2 * Freq * t).Cos() + ConstB * (2 * Freq * t).Sin() + ConstC;
    }

    private static double GDt(double t)
    {
        return 2 * Freq * (-ConstA * (2 * Freq * t).Sin() + ConstB * (2 * Freq * t).Cos());
    }

    public static double CurveDt1NormSquared(double t)
    {
        return Freq.Square() * G(t) / 2;
    }

    public static double CurveDt1Norm(double t)
    {
        return Math.Sqrt(CurveDt1NormSquared(t));
    }

    public static RGaFloat64Bivector Omega(double t)
    {
        return -2 * Math.Sqrt(3) * Freq / G(t) * KDual;
    }

    public static RGaFloat64Bivector OmegaDt(double t)
    {
        return 2 * Math.Sqrt(3) * Freq * GDt(t) / G(t).Square() * KDual;
    }

    public static Triplet<RGaFloat64Vector> CurveFrame(double t)
    {
        return new Triplet<RGaFloat64Vector>(
            CurveDt1(t),
            CurveDt2(t),
            CurveDt3(t)
        );
    }

    public static Triplet<RGaFloat64Vector> CurveFrameDt(double t)
    {
        var vDt1 = CurveDt1(t);
        var vDt2 = CurveDt2(t);
        var vDt3 = CurveDt3(t);

        // Apply GS process
        var u1 = vDt1;
        var e1 = u1.DivideByNorm();

        var u2 = vDt2 - vDt2.ProjectOn(u1.ToSubspace());
        var e2 = u2.DivideByNorm();

        var u3 = vDt3 - vDt3.ProjectOn(u1.ToSubspace()) - vDt3.ProjectOn(u2.ToSubspace());
        var e3 = u3.DivideByNorm();

        return new Triplet<RGaFloat64Vector>(e1, e2, e3);
    }

    public static Triplet<RGaFloat64Vector> CurveFrameDs(double t)
    {
        var va2 = Va * Va;
        var vb2 = Vb * Vb;
        var vc2 = Vc * Vc;
        var sqrt2 = Math.Sqrt(2);
        var sqrt3 = Math.Sqrt(3);
        var wtCos = (Freq * t).Cos();
        var wtSin = (Freq * t).Sin();

        var g = G(t);
        var gDt = GDt(t);

        var e1d = gDt / (sqrt2 * g.Square()) * GeometricProcessor.Vector(
            2 * Va * wtCos,
            Vb * (sqrt3 * wtSin - wtCos),
            -Vc * (sqrt3 * wtSin + wtCos)
        );

        var e2d = gDt / (sqrt2 * g.Square() * KNorm) * GeometricProcessor.Vector(
            Va * (3 * (vc2 + vb2) * wtSin + (vc2 - vb2) * wtCos),
            -Vb * (sqrt3 * vc2 * wtSin + (2 * va2 + vc2) * wtCos),
            Vc * (sqrt3 * vb2 * wtSin - (2 * va2 + vb2) * wtCos)
        );

        var e3d = GeometricProcessor.VectorZero;

        return new Triplet<RGaFloat64Vector>(e1d, e2d, e3d);
    }

    public static double CurveCurvature1(double t)
    {
        var (_, e2, _) = CurveFrameDt(t);
        var (e1d, _, _) = CurveFrameDs(t);

        return e1d.Sp(e2);
    }

    public static Pair<double> CurveCurvature(double t)
    {
        var (_, e2, e3) = CurveFrameDt(t);
        var (e1d, e2d, _) = CurveFrameDs(t);

        var kappa1 = e1d.Sp(e2);
        var kappa2 = e2d.Sp(e3);

        return new Pair<double>(kappa1, kappa2);
    }

    public static void PlotKappa1(string filePath)
    {
        var tMin = 0;
        var tMax = 1 / FreqHz;
        var tCount = 256;

        var pm = new PlotModel
        {
            Title = $"|| v'(t) ||",
            Background = OxyColor.FromRgb(255, 255, 255)
        };

        pm.Series.Add(new FunctionSeries(
            CurveCurvature1,
            tMin,
            tMax,
            tCount
        ));

        OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + "pdf", 1024, 768);
        //OxyPlot.SkiaSharp.PngExporter.Export(pm, filePath + "png", 1024, 768, 300);
    }

    public static void Example2()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        //var tValues = 0d.GetLinearRange(1 / FreqHz, 256).ToArray();

        //foreach (var t in tValues)
        //{
        //    var v = Curve(t);
        //    var vDt1 = CurveDt1(t);
        //    var vDt2 = CurveDt2(t);
        //    var vDt3 = CurveDt3(t);

        //    var g = G(t);
        //    var gDt = GDt(t);

        //    var vDt1NormSquared = CurveDt1NormSquared(t);

        //    //var s = 
        //    //    v
        //    //    .MapScalars(s => s.ReplaceAll("t", "x"))
        //    //    .ArcLength("x", Expr.INT_ZERO, "t".ToExpr())
        //    //    .Simplify();

        //    var sDt = CurveDt1Norm(t);

        //    var omega = Omega(t);
        //    var omegaDt = OmegaDt(t);

        //    var (e1, e2, e3) = CurveFrameDt(t);
        //    var (e1d, e2d, e3d) = CurveFrameDs(t);

        //    var (kappa1, kappa2) = CurveCurvature(t);

        //    Console.WriteLine($@"$t = {LaTeXComposer.GetScalarText(t)}$");
        //    Console.WriteLine($@"$\boldsymbol{{k}} = {LaTeXComposer.GetMultivectorText(K)}$");
        //    Console.WriteLine($@"$\boldsymbol{{k}}^{{*}} = {LaTeXComposer.GetMultivectorText(KDual)}$");
        //    Console.WriteLine();

        //    Console.WriteLine($@"$\boldsymbol{{v}}\left(t\right) = {LaTeXComposer.GetMultivectorText(v)}$");
        //    Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(vDt1)}$");
        //    Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(vDt2)}$");
        //    Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime\prime\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(vDt3)}$");
        //    Console.WriteLine();

        //    //Console.WriteLine($@"$s\left(t\right) = {LaTeXComposer.GetScalarText(s)}$");
        //    Console.WriteLine($@"$s^{{\prime}}\left(t\right) = {LaTeXComposer.GetScalarText(sDt)}$");
        //    Console.WriteLine();

        //    Console.WriteLine($@"$\boldsymbol{{\Omega}}\left(t\right) = {LaTeXComposer.GetMultivectorText(omega)}$");
        //    Console.WriteLine($@"$\boldsymbol{{\Omega}}^{{\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(omegaDt)}$");
        //    Console.WriteLine();

        //    Console.WriteLine($@"$\boldsymbol{{e}}_{{1}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e1)}$");
        //    Console.WriteLine($@"$\boldsymbol{{e}}_{{2}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e2)}$");
        //    Console.WriteLine($@"$\boldsymbol{{e}}_{{3}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e3)}$");
        //    Console.WriteLine();

        //    Console.WriteLine($@"$\dot{{\boldsymbol{{e}}}}_{{1}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e1d)}$");
        //    Console.WriteLine($@"$\dot{{\boldsymbol{{e}}}}_{{2}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e2d)}$");
        //    //Console.WriteLine($@"$\dot{{\boldsymbol{{e}}}}_{{3}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e3d)}$");
        //    Console.WriteLine();

        //    Console.WriteLine($@"$\kappa_{{1}}\left(t\right) = {LaTeXComposer.GetScalarText(kappa1)}$");
        //    Console.WriteLine($@"$\kappa_{{2}}\left(t\right) = {LaTeXComposer.GetScalarText(kappa2)}$");
        //    Console.WriteLine();
        //}

        PlotKappa1(@"D:\Projects\Study\Geometric Frequency\Kappa1.pdf");

        {
            var tAxis = LinFloat64PolarAngle.CreateFromVector(ConstA / ConstD, ConstB / ConstD).RadiansValue / (2 * Freq);

            var (a1, a2, _) = CurveFrameDt(tAxis);

            var a3 = KUnit;

            var sigma1 = GeometricProcessor.VectorTerm(0);
            var sigma2 = GeometricProcessor.VectorTerm(1);
            var sigma3 = GeometricProcessor.VectorTerm(2);

            var rotor1 =
                sigma3.CreatePureRotor(KUnit, true);

            var r1 =
                rotor1.OmMap(sigma1);

            var angle2 = r1.Lcp(a1).ArcCos();

            var rotor2 = r1.CreatePureRotor(a1);

            var rotor =
                rotor2.Multivector.Gp(rotor1.Multivector).CreatePureRotor();

            Console.WriteLine($@"$t_{{axis}} = {LaTeXComposer.GetScalarText(tAxis)}$");
            Console.WriteLine($@"$\boldsymbol{{e}}_{{1}}\left(t_{{axis}}\right) = {LaTeXComposer.GetMultivectorText(a1)}$");
            Console.WriteLine($@"$\boldsymbol{{e}}_{{2}}\left(t_{{axis}}\right) = {LaTeXComposer.GetMultivectorText(a2)}$");
            Console.WriteLine();

            //Console.WriteLine($@"R1 = {TextComposer.GetMultivectorText(rotor1)}");
            Console.WriteLine($@"$\boldsymbol{{R}}_{{1}} = {LaTeXComposer.GetMultivectorText(rotor1)}$");
            Console.WriteLine($@"$\boldsymbol{{R}}_{{1}}\boldsymbol{{\sigma}}_{{1}}\boldsymbol{{R}}_{{1}}^{{\dagger}} = {LaTeXComposer.GetMultivectorText(r1)}$");
            Console.WriteLine($@"$\cos\left(\varphi_{{2}}\right) = {LaTeXComposer.GetScalarText(angle2.RadiansValue)}$");
            Console.WriteLine();

            Console.WriteLine($@"$\boldsymbol{{R}}_{{2}} = {LaTeXComposer.GetMultivectorText(rotor2)}$");
            Console.WriteLine();

            Console.WriteLine($@"$\boldsymbol{{R}} = {LaTeXComposer.GetMultivectorText(rotor)}$");
            Console.WriteLine();
        }

        //// For validation
        //var vDt1NormSquared1 = VectorSpUtils.Sp(vDt1, vDt1);
        //var vDt1NormSquaredDiff = (vDt1NormSquared1 - vDt1NormSquared).FullSimplify();
        //Debug.Assert(vDt1NormSquaredDiff.IsZero());

        //var gDt1 = g.DifferentiateScalar("t", 1).Simplify();
        //var gDtDiff = (gDt1 - gDt).FullSimplify();
        //Debug.Assert(gDtDiff.IsZero());

        //var sDt1 =  vDt1.Norm().ScalarValue.ReplaceAll("Abs", "Plus");
        //var sDtDiff = (sDt1 - sDt).FullSimplify();
        //Debug.Assert(sDtDiff.IsZero());

        //var omega1 = (VectorOpUtils.Op(vDt1, vDt2) / ScalarUtils.Power(vDt1.Norm(), 2)).MapScalars(s => s.ReplaceAll("Abs", "Plus").Simplify());
        //var omegaDiff = (omega1 - omega).FullSimplifyScalars();
        //Debug.Assert(omegaDiff.IsZero());

        //var omegaDt1 = omega.MapScalars(s => s.DifferentiateScalar("t", 1).Simplify());
        //var omegaDtDiff = (omegaDt1 - omegaDt).FullSimplifyScalars();
        //Debug.Assert(omegaDtDiff.IsZero());

        //var k1 = VectorOpUtils.Op(e1, e2).Dual().MapScalars(s => s.ReplaceAll("Abs", "Plus").Simplify()).AsVector();
        //var kDiff = (k1 - k.DivideByNorm()).FullSimplifyScalars();
        //Debug.Assert(kDiff.IsZero());

        ////var a1Diff = (rotor.OmMap(sigma1) - a1).SimplifyScalars();
        ////Debug.Assert(a1Diff.IsZero());

        ////var a2Diff = (rotor.OmMap(sigma2) - a2).SimplifyScalars();
        ////Debug.Assert(a2Diff.IsZero());

        ////var a3Diff = (rotor.OmMap(sigma3) - a3).SimplifyScalars();
        ////Debug.Assert(a3Diff.IsZero());
    }
}