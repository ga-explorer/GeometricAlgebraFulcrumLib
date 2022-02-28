using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.GeometricFrequency
{
    public static class Sample1
    {
        // This is a pre-defined scalar processor for symbolic
        // Wolfram Mathematica scalars using Expr objects
        public static ScalarAlgebraMathematicaProcessor ScalarProcessor { get; }
            = ScalarAlgebraMathematicaProcessor.DefaultProcessor;
            
        // Create a 6-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        public static GeometricAlgebraEuclideanProcessor<Expr> GeometricProcessor { get; } 
            = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(3);

        // This is a pre-defined text generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static TextMathematicaComposer TextComposer { get; }
            = TextMathematicaComposer.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static LaTeXMathematicaComposer LaTeXComposer { get; }
            = LaTeXMathematicaComposer.DefaultComposer;


        public static void Example1()
        {
            LaTeXComposer.BasisName = @"\boldsymbol{\mu}";

            var assumeExpr = "And[V > 0, w > 0]".ToExpr();

            var va = "Sqrt[2] V Cos[w t]".ToExpr();
            var vb = "Sqrt[2] V Cos[w t - 2 Pi / 3]".ToExpr();
            var vc = "Sqrt[2] V Cos[w t + 2 Pi / 3]".ToExpr();

            var v = GeometricProcessor.CreateVector(va, vb, vc);
            var v1 = v.DifferentiateScalars("t", 1).SimplifyScalars(assumeExpr);
            var v2 = v.DifferentiateScalars("t", 2).SimplifyScalars(assumeExpr);
            var v3 = v.DifferentiateScalars("t", 3).SimplifyScalars(assumeExpr);

            var v1Norm = v1.Norm();
            var v1NormCube = v1Norm * v1Norm * v1Norm;
            var omega = (v1.Op(v2) / v1NormCube).AsMultivector().SimplifyScalars(assumeExpr);

            var u1 = v1;
            var e1 = u1.DivideByNorm().SimplifyScalars(assumeExpr);
            var e1d = e1.DifferentiateScalars("t").SimplifyScalars(assumeExpr);

            var u2 = v2 - v2.ProjectOn(u1.GetSubspace());
            var e2 = u2.DivideByNorm().SimplifyScalars(assumeExpr);
            var e2d = e2.DifferentiateScalars("t").SimplifyScalars(assumeExpr);

            var u3 = v3 - v3.ProjectOn(u1.GetSubspace()) - v3.ProjectOn(u2.GetSubspace());
            var e3 = u3.DivideByNorm().SimplifyScalars(assumeExpr);
            var e3d = e3.DifferentiateScalars("t").SimplifyScalars(assumeExpr);

            var k1 = e1.Sp(e2d).Simplify(assumeExpr);
            var k2 = e2.Sp(e3d).Simplify(assumeExpr);

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

            var assumeExpr = @"And[Subscript[V,a] > 0, Subscript[V,b] > 0, Subscript[V,c] > 0, \[Omega] > 0, t >= 0, Element[{Subscript[V,a], Subscript[V,b], Subscript[V,c], w, t}, Reals]]".ToExpr();

            MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

            var t = "t".ToExpr();

            var k = GeometricProcessor.CreateVector(
                "Subscript[V,b] * Subscript[V,c]", 
                "Subscript[V,a] * Subscript[V,c]",
                "Subscript[V,a] * Subscript[V,b]"
            );

            var kUnit = k.DivideByNorm();

            var kDual = k.Dual().AsBivector();

            var va = Mfs.TrigExpand[@"Sqrt[2] Subscript[V,a] Cos[\[Omega] t]".ToExpr()];
            var vb = Mfs.TrigExpand[@"Sqrt[2] Subscript[V,b] Cos[\[Omega] t - 2 Pi / 3]".ToExpr()];
            var vc = Mfs.TrigExpand[@"Sqrt[2] Subscript[V,c] Cos[\[Omega] t + 2 Pi / 3]".ToExpr()];
            
            var v = GeometricProcessor.CreateVector(va, vb, vc);

            var vDt1 = v.MapScalars(s => 
                s.DifferentiateScalar("t", 1).Simplify()
            );

            var vDt2 = v.MapScalars(s => 
                s.DifferentiateScalar("t", 2).Simplify()
            );

            var vDt3 = v.MapScalars(s => 
                s.DifferentiateScalar("t", 3).Simplify()
            );

            var g = GeometricProcessor.CreateScalar(
                @"-(Cos[2*t*\[Omega]]*(2*Subscript[V, a]^2 - Subscript[V, b]^2 - Subscript[V, c]^2)) + Sqrt[3]*Sin[2*t*\[Omega]]*(Subscript[V, b]^2 - Subscript[V, c]^2) + 2*(Subscript[V, a]^2 + Subscript[V, b]^2 + Subscript[V, c]^2)"
            );

            var gDt = GeometricProcessor.CreateScalar(
                @"2 \[Omega] (2 Subscript[V, a]^2 - Subscript[V, b]^2 - Subscript[V, c]^2) Sin[2 \[Omega] t] + 2 Sqrt[3] \[Omega] (Subscript[V, b]^2 - Subscript[V, c]^2) Cos[2 \[Omega] t]"
            );

            var vDt1NormSquared = @"\[Omega] * \[Omega] / 2".ToExpr() * g;

            //var s = 
            //    v
            //    .MapScalars(s => s.ReplaceAll("t", "x"))
            //    .ArcLength("x", Expr.INT_ZERO, "t".ToExpr())
            //    .Simplify();

            var sDt = @"\[Omega] / Sqrt[2]".ToExpr() * g.Sqrt();

            var omega = (@"-2 * Sqrt[3] \[Omega]".ToExpr() / g) * kDual;

            var omegaDt = (@"2 * Sqrt[3] \[Omega]".ToExpr() * gDt / g.Square()) * kDual;

            // Apply GS process
            var u1 = vDt1;
            var e1 = u1.DivideByNorm().SimplifyScalars();
            var e1d = (e1.DifferentiateScalars("t") / sDt).SimplifyScalars();

            var u2 = vDt2 - vDt2.ProjectOn(u1.GetSubspace());
            var e2 = u2.DivideByNorm().MapScalars(s => s.ReplaceAll("Abs", "Plus")).SimplifyScalars();
            var e2d = (e2.DifferentiateScalars("t") / sDt).SimplifyScalars();

            var u3 = vDt3 - vDt3.ProjectOn(u1.GetSubspace()) - vDt3.ProjectOn(u2.GetSubspace());
            var e3 = u3.DivideByNorm().SimplifyScalars();
            var e3d = (e3.DifferentiateScalars("t") / sDt).SimplifyScalars();

            var kappa1 = e1d.Sp(e2).FullSimplify();
            var kappa2 = e2d.Sp(e3).Simplify();

            var twoOmega = @"2 \[Omega]".ToExpr();

            var a = @"2 Subscript[V,a]^2 - Subscript[V,b]^2 - Subscript[V,c]^2".ToExpr();
            var b = @"Sqrt[3] (Subscript[V,b]^2 - Subscript[V,c]^2)".ToExpr();
            var tAxis = Mfs.Divide[Mfs.ArcTan[Mfs.Minus[a], b], twoOmega].Evaluate();

            var sin2OmegaT = Mfs.Sin[Mfs.Times[twoOmega, tAxis]].FullSimplify();
            var cos2OmegaT = Mfs.Cos[Mfs.Times[twoOmega, tAxis]].FullSimplify();
            var sinOmegaT1 = @"Sin[Times[t,\[Omega]]]".ToExpr();
            var cosOmegaT1 = @"Cos[Times[t,\[Omega]]]".ToExpr();
            var sinOmegaT2 = Mfs.Sqrt[Mfs.Divide[Mfs.Subtract[1, cos2OmegaT], 2]].FullSimplify();
            var cosOmegaT2 = Mfs.Sqrt[Mfs.Divide[Mfs.Plus[1, cos2OmegaT], 2]].FullSimplify();

            Console.WriteLine($@"$\sin(\omega t) = {LaTeXComposer.GetScalarText(sinOmegaT2)}$");
            Console.WriteLine($@"$\cos(\omega t) = {LaTeXComposer.GetScalarText(cosOmegaT2)}$");
            Console.WriteLine($@"$\cos(\omega t) = {LaTeXComposer.GetScalarText(cosOmegaT1.ReplaceAll(cosOmegaT1, cosOmegaT2))}$");
            Console.WriteLine($@"$\sin(2 \omega t) = {LaTeXComposer.GetScalarText(sin2OmegaT)}$");
            Console.WriteLine($@"$\cos(2 \omega t) = {LaTeXComposer.GetScalarText(cos2OmegaT)}$");
            Console.WriteLine();

            var e1tAxis = e1.MapScalars(
                s => s
                    .ReplaceAll(sinOmegaT1, sinOmegaT2)
                    .ReplaceAll(cosOmegaT1, cosOmegaT2)
                    .ReplaceAll(t, tAxis)
                    .Simplify()
            );
            
            var e2tAxis = e2.MapScalars(
                s => s
                    .ReplaceAll(sinOmegaT1, sinOmegaT2)
                    .ReplaceAll(cosOmegaT1, cosOmegaT2)
                    .ReplaceAll(t, tAxis)
                    .Simplify()
            );

            //var rotorMv1 = 
            //    GeometricProcessor.CreatePureRotor(
            //        GeometricProcessor.CreateVectorBasis(2),
            //        kUnit,
            //        true
            //    ).Multivector.SimplifyScalars();

            //var rotor1 = GeometricProcessor.CreatePureRotor(
            //    rotorMv1.GetScalarPart(), 
            //    rotorMv1.GetBivectorPart()
            //);

            //var sigma1 = GeometricProcessor.CreateVectorBasis(0);
            //var angle1 = rotor1.GetRotorInverse().OmMap(e1).SimplifyScalars();

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
            Console.WriteLine($@"$\boldsymbol{{e}}_{{1}}\left(t_{{axis}}\right) = {LaTeXComposer.GetMultivectorText(e1tAxis)}$");
            Console.WriteLine($@"$\boldsymbol{{e}}_{{2}}\left(t_{{axis}}\right) = {LaTeXComposer.GetMultivectorText(e2tAxis)}$");
            Console.WriteLine();

            //Console.WriteLine($@"$R\left(t\right) = {TextComposer.GetMultivectorText(rotor1)}$");
            //Console.WriteLine($@"$R\left(t\right) = {LaTeXComposer.GetMultivectorText(rotor1)}$");
            //Console.WriteLine($@"$R\left(t\right) \boldsymbol{{\sigma}}_{{1}} R^{{\dagger}}\left(t\right) = {LaTeXComposer.GetMultivectorText(angle1)}$");
            //Console.WriteLine();

            // For validation
            var vDt1NormSquared1 = vDt1.Sp(vDt1);
            var vDt1NormSquaredDiff = (vDt1NormSquared1 - vDt1NormSquared).FullSimplify();
            Debug.Assert(vDt1NormSquaredDiff.IsZero());

            var gDt1 = g.DifferentiateScalar("t", 1).Simplify();
            var gDtDiff = (gDt1 - gDt).FullSimplify();
            Debug.Assert(gDtDiff.IsZero());

            var sDt1 =  vDt1.Norm();
            var sDtDiff = (sDt1 - sDt).FullSimplify();
            Debug.Assert(sDtDiff.IsZero());

            var omega1 = (vDt1.Op(vDt2) / vDt1.Norm().Power(2)).SimplifyScalars();
            var omegaDiff = (omega1 - omega).FullSimplifyScalars();
            Debug.Assert(omegaDiff.IsZero());

            var omegaDt1 = omega.MapScalars(s => s.DifferentiateScalar("t", 1).Simplify());
            var omegaDtDiff = (omegaDt1 - omegaDt).FullSimplifyScalars();
            Debug.Assert(omegaDtDiff.IsZero());

            var k1 = e1.Op(e2).Dual().MapScalars(s => s.ReplaceAll("Abs", "Plus").Simplify()).AsVector();
            var kDiff = (k1 - k.DivideByNorm()).FullSimplifyScalars();
            Debug.Assert(kDiff.IsZero());

            return;
            

            //var re2 = rotor1 * .OmMap(GeometricProcessor.CreateVectorBasis(1)).SimplifyScalars();

            //Console.WriteLine($@"$R\left(t\right) = {LaTeXComposer.GetMultivectorText(re2)}$");

            //var rotor2 = 
            //    GeometricProcessor.CreatePureRotor(
            //        re2, 
            //        e2,
            //        true
            //    );

            //Console.WriteLine($@"$R\left(t\right) = {LaTeXComposer.GetMultivectorText(rotor2)}$");

            //var rotor = 
            //    rotor2.Multivector.EGp(rotor1.Multivector);

            ////var rotor = GeometricProcessor.CreatePureRotor(
            ////    e1,
            ////    e2,
            ////    GeometricProcessor.CreateVectorBasis(0),
            ////    GeometricProcessor.CreateVectorBasis(1),
            ////    true
            ////);

            //Console.WriteLine($@"$R\left(t\right) = {LaTeXComposer.GetMultivectorText(rotor)}$");
            //Console.WriteLine();
        }
    }
}
