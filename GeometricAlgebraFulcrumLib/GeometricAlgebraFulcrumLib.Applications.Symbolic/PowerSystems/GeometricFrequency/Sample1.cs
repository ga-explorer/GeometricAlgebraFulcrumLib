using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.PowerSystems.GeometricFrequency;

public static class Sample1
{
    // This is a pre-defined scalar processor for symbolic
    // Wolfram Mathematica scalars using Expr objects
    public static ScalarProcessorOfWolframExpr ScalarProcessor { get; }
        = ScalarProcessorOfWolframExpr.Instance;

    // Create a 6-dimensional Euclidean geometric algebra processor based on the
    // selected scalar processor
    public static XGaProcessor<Expr> GeometricProcessor { get; }
        = ScalarProcessor.CreateEuclideanXGaProcessor();

    public static int VSpaceDimensions
        => 3;

    // This is a pre-defined text generator for displaying multivectors
    // with symbolic Wolfram Mathematica scalars using Expr objects
    public static TextComposerExpr TextComposer { get; }
        = TextComposerExpr.DefaultComposer;

    // This is a pre-defined LaTeX generator for displaying multivectors
    // with symbolic Wolfram Mathematica scalars using Expr objects
    public static LaTeXComposerOfWolframExpr LaTeXComposer { get; }
        = LaTeXComposerOfWolframExpr.DefaultComposer;


    public static void Example1()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\mu}";

        var assumeExpr = "And[V > 0, w > 0]".ToExpr();

        var va = "Sqrt[2] V Cos[w t]".ToExpr();
        var vb = "Sqrt[2] V Cos[w t - 2 Pi / 3]".ToExpr();
        var vc = "Sqrt[2] V Cos[w t + 2 Pi / 3]".ToExpr();

        var v = GeometricProcessor.Vector(va, vb, vc);
        var v1 = v.DifferentiateScalars("t", 1).SimplifyScalars(assumeExpr);
        var v2 = v.DifferentiateScalars("t", 2).SimplifyScalars(assumeExpr);
        var v3 = v.DifferentiateScalars("t", 3).SimplifyScalars(assumeExpr);

        var v1Norm = v1.Norm();
        var v1NormCube = v1Norm * v1Norm * v1Norm;
        var omega = (v1.Op(v2) / v1NormCube).SimplifyScalars(assumeExpr);

        var u1 = v1;
        var e1 = u1.DivideByNorm().SimplifyScalars(assumeExpr);
        var e1d = e1.DifferentiateScalars("t").SimplifyScalars(assumeExpr);

        var u2 = v2 - v2.ProjectOn(u1.ToSubspace());
        var e2 = u2.DivideByNorm().SimplifyScalars(assumeExpr);
        var e2d = e2.DifferentiateScalars("t").SimplifyScalars(assumeExpr);

        var u3 = v3 - v3.ProjectOn(u1.ToSubspace()) - v3.ProjectOn(u2.ToSubspace());
        var e3 = u3.DivideByNorm().SimplifyScalars(assumeExpr);
        var e3d = e3.DifferentiateScalars("t").SimplifyScalars(assumeExpr);

        var k1 = e1.Sp(e2d).SimplifyScalar(assumeExpr);
        var k2 = e2.Sp(e3d).SimplifyScalar(assumeExpr);

        Console.WriteLine($@"$\boldsymbol{{v}}\left(t\right) = {LaTeXComposer.GetMultivectorText(v)}$");
        Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(v1)}$");
        Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(v2)}$");
        Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime\prime\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(v3)}$");
        Console.WriteLine($@"$\boldsymbol{{\varOmega}}\left(t\right) = {LaTeXComposer.GetMultivectorText(omega)}$");
        Console.WriteLine($@"$\boldsymbol{{e}}_{{1}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e1)}$");
        Console.WriteLine($@"$\boldsymbol{{e}}_{{2}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e2)}$");
        Console.WriteLine($@"$\boldsymbol{{e}}_{{3}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e3)}$");
        Console.WriteLine($@"$\boldsymbol{{e}}^{{\prime}}_{{1}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e1d)}$");
        Console.WriteLine($@"$\boldsymbol{{e}}^{{\prime}}_{{2}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e2d)}$");
        Console.WriteLine($@"$\boldsymbol{{e}}^{{\prime}}_{{3}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e3d)}$");
        Console.WriteLine($@"$\kappa_{{1}}\left(t\right) = {LaTeXComposer.GetScalarText(k1)}$");
        Console.WriteLine($@"$\kappa_{{2}}\left(t\right) = {LaTeXComposer.GetScalarText(k2)}$");
        Console.WriteLine();
    }

    public static void Example2()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        var assumeExpr = @"And[a > 0, b > 0, c > 0, \[Omega] > 0, t >= 0, Element[{a, b, c, w, t}, Reals]]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var sigma1 = GeometricProcessor.VectorTerm(0);
        var sigma2 = GeometricProcessor.VectorTerm(1);
        var sigma3 = GeometricProcessor.VectorTerm(2);

        var t = "t".ScalarFromText(ScalarProcessor);

        var k = GeometricProcessor.Vector("b * c", "a * c", "a * b");
        var kUnit = k.DivideByNorm();
        var kDual = k.Dual(VSpaceDimensions).AsBivector();

        var v1 = Mfs.TrigExpand[@"Sqrt[2] a Cos[\[Omega] t]".ToExpr()];
        var v2 = Mfs.TrigExpand[@"Sqrt[2] b Cos[\[Omega] t - 2 Pi / 3]".ToExpr()];
        var v3 = Mfs.TrigExpand[@"Sqrt[2] c Cos[\[Omega] t + 2 Pi / 3]".ToExpr()];

        var v = GeometricProcessor.Vector(v1, v2, v3);

        var vDt1 = v.MapScalars(s =>
            s.DifferentiateScalar("t", 1).Simplify()
        );

        var vDt2 = v.MapScalars(s =>
            s.DifferentiateScalar("t", 2).Simplify()
        );

        var vDt3 = v.MapScalars(s =>
            s.DifferentiateScalar("t", 3).Simplify()
        );

        var g = ScalarProcessor.ScalarFromText(
            @"-(Cos[2*t*\[Omega]]*(2*a^2 - b^2 - c^2)) + Sqrt[3]*Sin[2*t*\[Omega]]*(b^2 - c^2) + 2*(a^2 + b^2 + c^2)"
        );

        var gDt = ScalarProcessor.ScalarFromText(
            @"2 \[Omega] (2 a^2 - b^2 - c^2) Sin[2 \[Omega] t] + 2 Sqrt[3] \[Omega] (b^2 - c^2) Cos[2 \[Omega] t]"
        );

        var vDt1NormSquared = @"\[Omega] * \[Omega] / 2".ToExpr() * g;

        //var s = 
        //    v
        //    .MapScalars(s => s.ReplaceAll("t", "x"))
        //    .ArcLength("x", Expr.INT_ZERO, "t".ToExpr())
        //    .Simplify();

        var sDt = @"\[Omega] / Sqrt[2]".ToExpr() * g.Sqrt();

        var omega = @"-2 * Sqrt[3] \[Omega]".ToExpr() / g * kDual;

        var omegaDt = @"2 * Sqrt[3] \[Omega]".ToExpr() * gDt / g.Square() * kDual;

        // Apply GS process
        var u1 = vDt1;
        var e1 = u1.DivideByNorm().SimplifyScalars();
        var e10 = e1.MapScalars(s => s.ReplaceAll(t.ScalarValue, Expr.INT_ZERO).FullSimplify());
        var e1d = (e1.DifferentiateScalars("t") / sDt).SimplifyScalars();

        var u2 = vDt2 - vDt2.ProjectOn(u1.ToSubspace());
        var e2 = u2.DivideByNorm().MapScalars(s => s.ReplaceAll("Abs", "Plus")).SimplifyScalars();
        var e20 = e2.MapScalars(s => s.ReplaceAll(t.ScalarValue, Expr.INT_ZERO).FullSimplify());
        var e2d = (e2.DifferentiateScalars("t") / sDt).SimplifyScalars();

        var u3 = vDt3 - vDt3.ProjectOn(u1.ToSubspace()) - vDt3.ProjectOn(u2.ToSubspace());
        var e3 = u3.DivideByNorm().SimplifyScalars();
        var e3d = (e3.DifferentiateScalars("t") / sDt).SimplifyScalars();

        var kappa1 = e1d.Sp(e2).FullSimplifyScalar();
        var kappa2 = e2d.Sp(e3).SimplifyScalar();

        var twoOmega = @"2 \[Omega]".ScalarFromText(ScalarProcessor).RadiansToPolarAngle();

        var a = "A".ScalarFromText(ScalarProcessor);//@"-2 a^2 + b^2 + c^2".CreateScalar(GeometricProcessor);
        var b = "B".ScalarFromText(ScalarProcessor);//@"Sqrt[3] (b^2 - c^2)".CreateScalar(GeometricProcessor);

        var tAxis = a.ArcTan2(b).ToScalar() / twoOmega;

        var sin2OmegaT = (twoOmega * tAxis).Sin();
        var cos2OmegaT = (twoOmega * tAxis).Cos();
        var sinOmegaT1 = (t * @"\[Omega]".ToExpr()).Sin().FullSimplifyScalar();
        var cosOmegaT1 = (t * @"\[Omega]".ToExpr()).Cos().FullSimplifyScalar();
        var sinOmegaT2 = ((1 - cos2OmegaT) / 2).Sqrt().FullSimplifyScalar();
        var cosOmegaT2 = ((1 + cos2OmegaT) / 2).Sqrt().FullSimplifyScalar();

        Console.WriteLine($@"$\sin(\omega t) = {LaTeXComposer.GetScalarText(sinOmegaT2)}$");
        Console.WriteLine($@"$\cos(\omega t) = {LaTeXComposer.GetScalarText(cosOmegaT2)}$");
        Console.WriteLine($@"$\sin(2 \omega t) = {LaTeXComposer.GetScalarText(sin2OmegaT)}$");
        Console.WriteLine($@"$\cos(2 \omega t) = {LaTeXComposer.GetScalarText(cos2OmegaT)}$");
        Console.WriteLine();

        var a1 = e1.MapScalars(
            s => s
                .ReplaceAll(sinOmegaT1.ScalarValue, sinOmegaT2.ScalarValue)
                .ReplaceAll(cosOmegaT1.ScalarValue, cosOmegaT2.ScalarValue)
                .ReplaceAll(t.ScalarValue, tAxis.ScalarValue)
                .Simplify()
        );

        var a2 = e2.MapScalars(
            s => s
                .ReplaceAll(sinOmegaT1.ScalarValue, sinOmegaT2.ScalarValue)
                .ReplaceAll(cosOmegaT1.ScalarValue, cosOmegaT2.ScalarValue)
                .ReplaceAll(t.ScalarValue, tAxis.ScalarValue)
                .Simplify()
        );

        var a3 = kUnit;

        var rotor1 =
            sigma3.CreatePureRotor(
                kUnit,
                true
            ).SimplifyScalars();

        var r1 =
            rotor1.OmMap(sigma1).SimplifyScalars();

        //var angle2 = Mfs.ArcCos[Mfs.TrigReduce[
        //    r1.Lcp(a1).ScalarValue
        //]].FullSimplify();

        //var phi2 = @"Subscript[\[Phi], 2]".CreateScalar(GeometricProcessor);
        var rotor2 = r1.CreatePureRotor(a1).SimplifyScalars();

        //var rotor2 = GeometricProcessor
        //    .CreatePureRotor(r1, rotor1.OmMap(e1))
        //    .SimplifyScalars();

        var rotor =
            rotor2.Multivector.Gp(rotor1.Multivector).SimplifyScalars().CreatePureRotor();

        var omegaRotated1 = rotor1.OmMap(omega).SimplifyScalars();
        var omegaRotated2 = rotor2.OmMap(omegaRotated1).SimplifyScalars();

        Console.WriteLine($@"$\boldsymbol{{k}} = {LaTeXComposer.GetMultivectorText(k)}$");
        Console.WriteLine($@"$\boldsymbol{{k}}^{{*}} = {LaTeXComposer.GetMultivectorText(kDual)}$");
        Console.WriteLine();

        Console.WriteLine($@"$\boldsymbol{{v}}\left(t\right) = {LaTeXComposer.GetMultivectorText(v)}$");
        Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(vDt1)}$");
        Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(vDt2)}$");
        Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime\prime\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(vDt3)}$");
        Console.WriteLine();

        //Console.WriteLine($@"$s\left(t\right) = {LaTeXComposer.GetScalarText(s)}$");
        Console.WriteLine($@"$s^{{\prime}}\left(t\right) = {LaTeXComposer.GetScalarText(sDt)}$");
        Console.WriteLine();

        Console.WriteLine($@"$\boldsymbol{{\Omega}}\left(t\right) = {LaTeXComposer.GetMultivectorText(omega)}$");
        Console.WriteLine($@"$\boldsymbol{{\Omega}}^{{\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(omegaDt)}$");
        Console.WriteLine();

        Console.WriteLine($@"$\boldsymbol{{e}}_{{1}}\left(t\right) = {TextComposer.GetMultivectorText(e1)}$");
        Console.WriteLine($@"$\boldsymbol{{e}}_{{1}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e1)}$");
        Console.WriteLine($@"$\boldsymbol{{e}}_{{1}}\left(t\right) = {TextComposer.GetMultivectorText(e2)}$");
        Console.WriteLine($@"$\boldsymbol{{e}}_{{2}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e2)}$");
        Console.WriteLine($@"$\boldsymbol{{e}}_{{3}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e3)}$");
        Console.WriteLine();

        Console.WriteLine($@"$\dot{{\boldsymbol{{e}}}}_{{1}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e1d)}$");
        Console.WriteLine($@"$\dot{{\boldsymbol{{e}}}}_{{2}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e2d)}$");
        Console.WriteLine($@"$\dot{{\boldsymbol{{e}}}}_{{3}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e3d)}$");
        Console.WriteLine();

        Console.WriteLine($@"$\kappa_{{1}}\left(t\right) = {LaTeXComposer.GetScalarText(kappa1)}$");
        Console.WriteLine($@"$\kappa_{{2}}\left(t\right) = {LaTeXComposer.GetScalarText(kappa2)}$");
        Console.WriteLine();

        Console.WriteLine($@"$t_{{axis}} = {LaTeXComposer.GetScalarText(tAxis)}$");
        Console.WriteLine($@"$\boldsymbol{{e}}_{{1}}\left(t_{{axis}}\right) = {LaTeXComposer.GetMultivectorText(a1)}$");
        Console.WriteLine($@"$\boldsymbol{{e}}_{{2}}\left(t_{{axis}}\right) = {LaTeXComposer.GetMultivectorText(a2)}$");
        Console.WriteLine();

        //Console.WriteLine($@"R1 = {TextComposer.GetMultivectorText(rotor1)}");
        Console.WriteLine($@"$\boldsymbol{{R}}_{{1}} = {LaTeXComposer.GetMultivectorText(rotor1)}$");
        Console.WriteLine($@"$\boldsymbol{{R}}_{{1}}\boldsymbol{{\Omega}}\boldsymbol{{R}}_{{1}}^{{\dagger}} = {LaTeXComposer.GetMultivectorText(omegaRotated1)}$");
        Console.WriteLine($@"$\boldsymbol{{R}}_{{1}}\boldsymbol{{e}}_{{1}}\left(0\right)\boldsymbol{{R}}_{{1}}^{{\dagger}} = {LaTeXComposer.GetMultivectorText(r1)}$");
        //Console.WriteLine($@"$\cos\left(\varphi_{{2}}\right) = {LaTeXComposer.GetScalarText(angle2)}$");
        Console.WriteLine();

        Console.WriteLine($@"$\boldsymbol{{R}}_{{2}} = {LaTeXComposer.GetMultivectorText(rotor2)}$");
        Console.WriteLine($@"$\boldsymbol{{R}}\boldsymbol{{\Omega}}\boldsymbol{{R}}^{{\dagger}} = {LaTeXComposer.GetMultivectorText(omegaRotated2)}$");
        Console.WriteLine();

        Console.WriteLine($@"$\boldsymbol{{R}} = {LaTeXComposer.GetMultivectorText(rotor)}$");
        Console.WriteLine($@"$\boldsymbol{{R}}\boldsymbol{{\Omega}}\boldsymbol{{R}}^{{\dagger}} = {LaTeXComposer.GetMultivectorText(omegaRotated2)}$");
        Console.WriteLine();

        //// For validation
        //var vDt1NormSquared1 = vDt1.Sp(vDt1);
        //var vDt1NormSquaredDiff = (vDt1NormSquared1 - vDt1NormSquared).FullSimplify();
        //Debug.Assert(vDt1NormSquaredDiff.IsZero());

        //var gDt1 = g.DifferentiateScalar("t", 1).Simplify();
        //var gDtDiff = (gDt1 - gDt).FullSimplify();
        //Debug.Assert(gDtDiff.IsZero());

        //var sDt1 =  vDt1.Norm().ScalarValue.ReplaceAll("Abs", "Plus");
        //var sDtDiff = (sDt1 - sDt).FullSimplify();
        //Debug.Assert(sDtDiff.IsZero());

        //var omega1 = (vDt1.Op(vDt2) / vDt1.Norm().Power(2)).MapScalars(s => s.ReplaceAll("Abs", "Plus").Simplify());
        //var omegaDiff = (omega1 - omega).FullSimplifyScalars();
        //Debug.Assert(omegaDiff.IsZero());

        //var omegaDt1 = omega.MapScalars(s => s.DifferentiateScalar("t", 1).Simplify());
        //var omegaDtDiff = (omegaDt1 - omegaDt).FullSimplifyScalars();
        //Debug.Assert(omegaDtDiff.IsZero());

        //var k1 = e1.Op(e2).Dual().MapScalars(s => s.ReplaceAll("Abs", "Plus").Simplify()).AsVector();
        //var kDiff = (k1 - k.DivideByNorm()).FullSimplifyScalars();
        //Debug.Assert(kDiff.IsZero());

        //var a1Diff = (rotor.OmMap(sigma1) - a1).SimplifyScalars();
        //Debug.Assert(a1Diff.IsZero());

        //var a2Diff = (rotor.OmMap(sigma2) - a2).SimplifyScalars();
        //Debug.Assert(a2Diff.IsZero());

        //var a3Diff = (rotor.OmMap(sigma3) - a3).SimplifyScalars();
        //Debug.Assert(a3Diff.IsZero());
    }

    public static void Example3()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        var assumeExpr = @"And[a > 0, b > 0, c > 0, \[Omega] > 0, t >= 0, Element[{a, b, c, \[Omega], t}, Reals]]".ToExpr();
        //var assumeExpr = @"And[V > 0, \[Omega] > 0, t >= 0, Element[{V, \[Omega], t}, Reals]]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var sigma1 = GeometricProcessor.VectorTerm(0);
        var sigma2 = GeometricProcessor.VectorTerm(1);
        var sigma3 = GeometricProcessor.VectorTerm(2);

        var t = "t".ScalarFromText(ScalarProcessor);

        var k = GeometricProcessor.Vector("b * c", "a * c", "a * b");
        //var k = GeometricProcessor.Vector("V * V", "V * V", "V * V");
        var kNormSquared = k.NormSquared();
        var kUnit = k.DivideByNorm();
        var kDual = k.Dual(VSpaceDimensions).AsBivector();

        Console.WriteLine($@"$\boldsymbol{{k}} = {LaTeXComposer.GetMultivectorText(k)}$");
        Console.WriteLine($@"$\left\Vert \boldsymbol{{k}}\right\Vert ^{{2}} = {LaTeXComposer.GetScalarText(kNormSquared)}$");
        Console.WriteLine($@"$\boldsymbol{{k}}^{{*}} = {LaTeXComposer.GetMultivectorText(kDual)}$");
        Console.WriteLine();

        //var r1 =
        //    GeometricProcessor.CreatePureRotor(
        //        kUnit,
        //        sigma3,
        //        true
        //    ).SimplifyScalars();

        //var e1t0 = r1.OmMap(
        //    GeometricProcessor.Vector("0", "b", "-c")
        //).DivideByNorm().FullSimplifyScalars();

        //Console.WriteLine($@"$\boldsymbol{{e}}_{{1}}\left(0\right) = {LaTeXComposer.GetMultivectorText(e1t0)}$");
        //Console.WriteLine();

        //var r2 = GeometricProcessor.CreatePureRotor(
        //    e1t0,
        //    sigma1,
        //    true
        //).SimplifyScalars();

        //var rotor1 = GeometricProcessor.CreatePureRotor(
        //    r2.Multivector.Gp(r1.Multivector).FullSimplifyScalars()
        //);

        var rotor1 =
            kUnit.CreatePureRotor(
                sigma3,
                true
            ).SimplifyScalars();

        Console.WriteLine($@"$\boldsymbol{{R}}_{{1}} = {LaTeXComposer.GetMultivectorText(rotor1)}$");
        Console.WriteLine();

        var v1 = Mfs.TrigExpand[@"Sqrt[2] a Cos[\[Omega] t]".ToExpr()];
        var v2 = Mfs.TrigExpand[@"Sqrt[2] b Cos[\[Omega] t - 2 Pi / 3]".ToExpr()];
        var v3 = Mfs.TrigExpand[@"Sqrt[2] c Cos[\[Omega] t + 2 Pi / 3]".ToExpr()];

        var v = rotor1.OmMap(
                GeometricProcessor
                    .Vector(v1, v2, v3))
            .MapScalars(s => Mfs.TrigReduce[s].FullSimplify()
            );

        //var v = 
        //    GeometricProcessor
        //        .Vector(v1, v2, v3)
        //        .MapScalars(s => Mfs.TrigReduce[s].FullSimplify());

        Console.WriteLine($@"$\boldsymbol{{v}}\left(t\right) = {LaTeXComposer.GetMultivectorText(v)}$");
        Console.WriteLine();

        //var r1 = 
        //    rotor1.OmMap(sigma1).SimplifyScalars();

        var vDt1 = v.MapScalars(s =>
            s.DifferentiateScalar("t", 1).Simplify()
        );

        var vDt2 = v.MapScalars(s =>
            s.DifferentiateScalar("t", 2).Simplify()
        );

        var vDt3 = v.MapScalars(s =>
            s.DifferentiateScalar("t", 3).Simplify()
        );

        Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(vDt1)}$");
        Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(vDt2)}$");
        Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime\prime\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(vDt3)}$");
        Console.WriteLine();

        //var g = GeometricProcessor.CreateScalar(
        //    @"-(Cos[2*t*\[Omega]]*(2*a^2 - b^2 - c^2)) + Sqrt[3]*Sin[2*t*\[Omega]]*(b^2 - c^2) + 2*(a^2 + b^2 + c^2)"
        //);

        //var gDt = GeometricProcessor.CreateScalar(
        //    @"2 \[Omega] (2 a^2 - b^2 - c^2) Sin[2 \[Omega] t] + 2 Sqrt[3] \[Omega] (b^2 - c^2) Cos[2 \[Omega] t]"
        //);

        var vDt1NormSquared = vDt1.NormSquared();

        Console.WriteLine($@"$\left\Vert \boldsymbol{{v}}^{{\prime}}\left(t\right)\right\Vert ^{{2}} = {LaTeXComposer.GetScalarText(vDt1NormSquared)}$");
        Console.WriteLine();

        //var s = 
        //    v
        //    .MapScalars(s => s.ReplaceAll("t", "x"))
        //    .ArcLength("x", Expr.INT_ZERO, "t".ToExpr())
        //    .Simplify();

        var sDt =
            vDt1.Norm().SimplifyScalar();//.ScalarValue.ReplaceAll("Abs", "Plus");

        //Console.WriteLine($@"$s\left(t\right) = {LaTeXComposer.GetScalarText(s)}$");
        Console.WriteLine($@"$s^{{\prime}}\left(t\right) = {LaTeXComposer.GetScalarText(sDt)}$");
        Console.WriteLine();

        var omega =
            (vDt1.Op(vDt2) / vDt1.NormSquared()).SimplifyScalars();//.MapScalars(s => s.ReplaceAll("Abs", "Plus").Simplify());

        var omegaDt =
            omega.MapScalars(s => s.DifferentiateScalar("t", 1).Simplify());

        Console.WriteLine($@"$\boldsymbol{{\Omega}}\left(t\right) = {LaTeXComposer.GetMultivectorText(omega)}$");
        Console.WriteLine($@"$\boldsymbol{{\Omega}}^{{\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(omegaDt)}$");
        Console.WriteLine();

        // Apply GS process
        var u1 = vDt1;
        var e1 = u1.DivideByNorm().SimplifyScalars();
        var e10 = e1.MapScalars(s => s.ReplaceAll(t.ScalarValue, Expr.INT_ZERO).FullSimplify());
        var e1d = (e1.DifferentiateScalars("t") / sDt).SimplifyScalars();

        Console.WriteLine($@"$\boldsymbol{{e}}_{{1}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e1)}$");
        Console.WriteLine($@"$\dot{{\boldsymbol{{e}}}}_{{1}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e1d)}$");
        Console.WriteLine();

        var u2 = vDt2 - vDt2.ProjectOn(u1.ToSubspace());
        var e2 = u2.DivideByNorm().MapScalars(s => s.ReplaceAll("Abs", "Plus")).SimplifyScalars();
        var e20 = e2.MapScalars(s => s.ReplaceAll(t.ScalarValue, Expr.INT_ZERO).FullSimplify());
        var e2d = (e2.DifferentiateScalars("t") / sDt).SimplifyScalars();

        Console.WriteLine($@"$\boldsymbol{{e}}_{{2}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e2)}$");
        Console.WriteLine($@"$\dot{{\boldsymbol{{e}}}}_{{2}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e2d)}$");
        Console.WriteLine();

        var u3 = vDt3 - vDt3.ProjectOn(u1.ToSubspace()) - vDt3.ProjectOn(u2.ToSubspace());
        var e3 = u3.DivideByNorm().SimplifyScalars();
        var e3d = (e3.DifferentiateScalars("t") / sDt).SimplifyScalars();

        Console.WriteLine($@"$\boldsymbol{{e}}_{{3}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e3)}$");
        Console.WriteLine($@"$\dot{{\boldsymbol{{e}}}}_{{3}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e3d)}$");
        Console.WriteLine();

        var kappa1 = e1d.Sp(e2).SimplifyScalar();
        var kappa2 = e2d.Sp(e3).SimplifyScalar();

        Console.WriteLine($@"$\kappa_{{1}}\left(t\right) = {LaTeXComposer.GetScalarText(kappa1)}$");
        Console.WriteLine($@"$\kappa_{{2}}\left(t\right) = {LaTeXComposer.GetScalarText(kappa2)}$");
        Console.WriteLine();

        //var rotor2 =
        //    GeometricProcessor.CreatePureRotor(
        //        e1,
        //        sigma1,
        //        true
        //    ).SimplifyScalars();

        //Console.WriteLine($@"$\boldsymbol{{R}}_{{2}} = {LaTeXComposer.GetMultivectorText(rotor2)}$");
        //Console.WriteLine();

        //var vRotated = rotor2.OmMap(v).SimplifyScalars();

        //Console.WriteLine($@"$\boldsymbol{{v}}\left(t\right) = {LaTeXComposer.GetMultivectorText(vRotated)}$");
        //Console.WriteLine();

        //var omegaRotated = rotor2.OmMap(omega).SimplifyScalars();

        //Console.WriteLine($@"$\boldsymbol{{\Omega}}\left(t\right) = {LaTeXComposer.GetMultivectorText(omegaRotated)}$");
        //Console.WriteLine();
    }

    public static void Example4()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        //var va1 = "V".ToExpr();
        //var vb1 = "V".ToExpr();
        //var vc1 = "V".ToExpr();

        //var va2 = "V".ToExpr();
        //var vb2 = "V".ToExpr();
        //var vc2 = "V".ToExpr();

        //var va7 = "V".ToExpr();
        //var vb7 = "V".ToExpr();
        //var vc7 = "V".ToExpr();

        //var va1 = "Subscript[V, 1]".ToExpr();
        //var vb1 = "Subscript[V, 1]".ToExpr();
        //var vc1 = "Subscript[V, 1]".ToExpr();

        //var va2 = "Subscript[V, 2]".ToExpr();
        //var vb2 = "Subscript[V, 2]".ToExpr();
        //var vc2 = "Subscript[V, 2]".ToExpr();

        //var va7 = "Subscript[V, 7]".ToExpr();
        //var vb7 = "Subscript[V, 7]".ToExpr();
        //var vc7 = "Subscript[V, 7]".ToExpr();

        var va1 = "Subscript[V, a, 1]".ToExpr();
        var vb1 = "Subscript[V, b, 1]".ToExpr();
        var vc1 = "Subscript[V, c, 1]".ToExpr();

        var va2 = "Subscript[V, a, 2]".ToExpr();
        var vb2 = "Subscript[V, b, 2]".ToExpr();
        var vc2 = "Subscript[V, c, 2]".ToExpr();

        var va7 = "Subscript[V, a, 7]".ToExpr();
        var vb7 = "Subscript[V, b, 7]".ToExpr();
        var vc7 = "Subscript[V, c, 7]".ToExpr();

        var assumeExpr = @$"And[{va1} > 0, {vb1} > 0, {vc1} > 0, {va2} > 0, {vb2} > 0, {vc2} > 0, {va7} > 0, {vb7} > 0, {vc7} > 0, \[Omega] > 0, t >= 0, Element[{{{va1}, {vb1}, {vc1}, {va2}, {vb2}, {vc2}, {va7}, {vb7}, {vc7}, \[Omega], t}}, Reals]]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var sigma1 = GeometricProcessor.VectorTerm(0);
        var sigma2 = GeometricProcessor.VectorTerm(1);
        var sigma3 = GeometricProcessor.VectorTerm(2);

        var t = "t".ScalarFromText(ScalarProcessor);

        //var k = GeometricProcessor.Vector("b * c", "a * c", "a * b");
        //var k = GeometricProcessor.Vector("V * V", "V * V", "V * V");
        //var kNormSquared = k.NormSquared();
        //var kUnit = k.DivideByNorm();
        //var kDual = k.Dual().AsBivector();

        //Console.WriteLine($@"$\boldsymbol{{k}} = {LaTeXComposer.GetMultivectorText(k)}$");
        //Console.WriteLine($@"$\left\Vert \boldsymbol{{k}}\right\Vert ^{{2}} = {LaTeXComposer.GetScalarText(kNormSquared)}$");
        //Console.WriteLine($@"$\boldsymbol{{k}}^{{*}} = {LaTeXComposer.GetMultivectorText(kDual)}$");
        //Console.WriteLine();

        //var r1 =
        //    GeometricProcessor.CreatePureRotor(
        //        kUnit,
        //        sigma3,
        //        true
        //    ).SimplifyScalars();

        //var e1t0 = r1.OmMap(
        //    GeometricProcessor.Vector("0", "b", "-c")
        //).DivideByNorm().FullSimplifyScalars();

        //Console.WriteLine($@"$\boldsymbol{{e}}_{{1}}\left(0\right) = {LaTeXComposer.GetMultivectorText(e1t0)}$");
        //Console.WriteLine();

        //var r2 = GeometricProcessor.CreatePureRotor(
        //    e1t0,
        //    sigma1,
        //    true
        //).SimplifyScalars();

        //var rotor1 = GeometricProcessor.CreatePureRotor(
        //    r2.Multivector.Gp(r1.Multivector).FullSimplifyScalars()
        //);

        //var rotor1 =
        //    GeometricProcessor.CreatePureRotor(
        //        kUnit,
        //        sigma3,
        //        true
        //    ).SimplifyScalars();

        //Console.WriteLine($@"$\boldsymbol{{R}}_{{1}} = {LaTeXComposer.GetMultivectorText(rotor1)}$");
        //Console.WriteLine();

        var v1 = Mfs.TrigExpand[@$"Sqrt[2] ({va1} Cos[\[Omega] t] + {va2} Cos[2 \[Omega] t] + {va7} Cos[7 \[Omega] t])".ToExpr()];
        var v2 = Mfs.TrigExpand[@$"Sqrt[2] ({vb1} Cos[\[Omega] t - 2 Pi / 3] + {vb2} Cos[2 (\[Omega] t - 2 Pi / 3)] + {vb7} Cos[7 (\[Omega] t - 2 Pi / 3)])".ToExpr()];
        var v3 = Mfs.TrigExpand[@$"Sqrt[2] ({vc1} Cos[\[Omega] t + 2 Pi / 3] + {vc2} Cos[2 (\[Omega] t + 2 Pi / 3)] + {vc7} Cos[7 (\[Omega] t + 2 Pi / 3)])".ToExpr()];

        //var v = rotor1.OmMap(
        //    GeometricProcessor.Vector(v1, v2, v3)).MapScalars(s => Mfs.TrigReduce[s].FullSimplify()
        //);
        var v =
            GeometricProcessor
                .Vector(v1, v2, v3)
                .MapScalars(s => Mfs.TrigReduce[s].FullSimplify());

        Console.WriteLine($@"$\boldsymbol{{v}}\left(t\right) = {LaTeXComposer.GetMultivectorText(v)}$");
        Console.WriteLine();

        var vNormSquared = Mfs.TrigReduce[v.NormSquared().ScalarValue].Evaluate();

        Console.WriteLine($@"$\left\Vert \boldsymbol{{v}}\left(t\right)\right\Vert ^{{2}} = {LaTeXComposer.GetScalarText(vNormSquared)}$");
        Console.WriteLine();

        //var r1 = 
        //    rotor1.OmMap(sigma1).SimplifyScalars();

        var vDt1 = v.MapScalars(s =>
            s.DifferentiateScalar("t", 1).Simplify()
        );

        var vDt2 = v.MapScalars(s =>
            s.DifferentiateScalar("t", 2).Simplify()
        );

        var vDt3 = v.MapScalars(s =>
            s.DifferentiateScalar("t", 3).Simplify()
        );

        Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(vDt1)}$");
        Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(vDt2)}$");
        Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime\prime\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(vDt3)}$");
        Console.WriteLine();

        //var g = GeometricProcessor.CreateScalar(
        //    @"-(Cos[2*t*\[Omega]]*(2*a^2 - b^2 - c^2)) + Sqrt[3]*Sin[2*t*\[Omega]]*(b^2 - c^2) + 2*(a^2 + b^2 + c^2)"
        //);

        //var gDt = GeometricProcessor.CreateScalar(
        //    @"2 \[Omega] (2 a^2 - b^2 - c^2) Sin[2 \[Omega] t] + 2 Sqrt[3] \[Omega] (b^2 - c^2) Cos[2 \[Omega] t]"
        //);

        var vDt1NormSquared = Mfs.TrigReduce[Mfs.TrigExpand[vDt1.NormSquared().ScalarValue]].FullSimplify();

        Console.WriteLine($@"$\left\Vert \boldsymbol{{v}}^{{\prime}}\left(t\right)\right\Vert ^{{2}} = {LaTeXComposer.GetScalarText(vDt1NormSquared)}$");
        Console.WriteLine();

        //var s = 
        //    v
        //    .MapScalars(s => s.ReplaceAll("t", "x"))
        //    .ArcLength("x", Expr.INT_ZERO, "t".ToExpr())
        //    .Simplify();

        var sDt =
            vDt1.Norm().SimplifyScalar();//.ScalarValue.ReplaceAll("Abs", "Plus");

        //Console.WriteLine($@"$s\left(t\right) = {LaTeXComposer.GetScalarText(s)}$");
        Console.WriteLine($@"$s^{{\prime}}\left(t\right) = {LaTeXComposer.GetScalarText(sDt)}$");
        Console.WriteLine();

        var omega =
            (vDt1.Op(vDt2) / vDt1.NormSquared()).SimplifyScalars();//.MapScalars(s => s.ReplaceAll("Abs", "Plus").Simplify());

        //var omegaDt = 
        //    omega.MapScalars(s => s.DifferentiateScalar("t", 1).Simplify());

        var omegaNorm = omega.Norm().SimplifyScalar();
        var omegaBlade = omega / omegaNorm;

        Console.WriteLine($@"$\boldsymbol{{\Omega}}\left(t\right) = {LaTeXComposer.GetMultivectorText(omega)}$");
        Console.WriteLine($@"$\left\Vert \boldsymbol{{\Omega}}\left(t\right)\right\Vert ^{{2}} = {LaTeXComposer.GetScalarText(omegaNorm)}$");
        Console.WriteLine($@"$\widehat{{\boldsymbol{{\Omega}}}}\left(t\right) = {LaTeXComposer.GetMultivectorText(omegaBlade)}$");
        //Console.WriteLine($@"$\boldsymbol{{\Omega}}^{{\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(omegaDt)}$");
        Console.WriteLine();

        //// Apply GS process
        //var u1 = vDt1;
        //var e1 = u1.DivideByNorm().SimplifyScalars();
        //var e10 = e1.MapScalars(s => s.ReplaceAll(t, Expr.INT_ZERO).FullSimplify());
        //var e1d = (e1.DifferentiateScalars("t") / sDt).SimplifyScalars();

        //Console.WriteLine($@"$\boldsymbol{{e}}_{{1}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e1)}$");
        //Console.WriteLine($@"$\dot{{\boldsymbol{{e}}}}_{{1}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e1d)}$");
        //Console.WriteLine();

        //var u2 = vDt2 - vDt2.ProjectOn(u1.ToSubspace());
        //var e2 = u2.DivideByNorm().MapScalars(s => s.ReplaceAll("Abs", "Plus")).SimplifyScalars();
        //var e20 = e2.MapScalars(s => s.ReplaceAll(t, Expr.INT_ZERO).FullSimplify());
        //var e2d = (e2.DifferentiateScalars("t") / sDt).SimplifyScalars();

        //Console.WriteLine($@"$\boldsymbol{{e}}_{{2}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e2)}$");
        //Console.WriteLine($@"$\dot{{\boldsymbol{{e}}}}_{{2}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e2d)}$");
        //Console.WriteLine();

        //var u3 = vDt3 - vDt3.ProjectOn(u1.ToSubspace()) - vDt3.ProjectOn(u2.ToSubspace());
        //var e3 = u3.DivideByNorm().SimplifyScalars();
        //var e3d = (e3.DifferentiateScalars("t") / sDt).SimplifyScalars();

        //Console.WriteLine($@"$\boldsymbol{{e}}_{{3}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e3)}$");
        //Console.WriteLine($@"$\dot{{\boldsymbol{{e}}}}_{{3}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e3d)}$");
        //Console.WriteLine();

        //var kappa1 = e1d.Sp(e2).Simplify();
        //var kappa2 = e2d.Sp(e3).Simplify();

        //Console.WriteLine($@"$\kappa_{{1}}\left(t\right) = {LaTeXComposer.GetScalarText(kappa1)}$");
        //Console.WriteLine($@"$\kappa_{{2}}\left(t\right) = {LaTeXComposer.GetScalarText(kappa2)}$");
        //Console.WriteLine();

        //var rotor2 =
        //    GeometricProcessor.CreatePureRotor(
        //        e1,
        //        sigma1,
        //        true
        //    ).SimplifyScalars();

        //Console.WriteLine($@"$\boldsymbol{{R}}_{{2}} = {LaTeXComposer.GetMultivectorText(rotor2)}$");
        //Console.WriteLine();

        //var vRotated = rotor2.OmMap(v).SimplifyScalars();

        //Console.WriteLine($@"$\boldsymbol{{v}}\left(t\right) = {LaTeXComposer.GetMultivectorText(vRotated)}$");
        //Console.WriteLine();

        //var omegaRotated = rotor2.OmMap(omega).SimplifyScalars();

        //Console.WriteLine($@"$\boldsymbol{{\Omega}}\left(t\right) = {LaTeXComposer.GetMultivectorText(omegaRotated)}$");
        //Console.WriteLine();
    }
}