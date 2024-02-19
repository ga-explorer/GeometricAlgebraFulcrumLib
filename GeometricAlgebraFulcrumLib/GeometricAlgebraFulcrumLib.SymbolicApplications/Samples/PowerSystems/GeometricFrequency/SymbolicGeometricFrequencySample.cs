using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Frames;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using TextComposerLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.SymbolicApplications.Samples.PowerSystems.GeometricFrequency;

public static class SymbolicGeometricFrequencySample
{

    // This is a pre-defined scalar processor for symbolic
    // Wolfram Mathematica scalars using Expr objects
    public static ScalarProcessorOfWolframExpr ScalarProcessor { get; }
        = ScalarProcessorOfWolframExpr.DefaultProcessor;

    public static int VSpaceDimensions
        => 3;

    public static int HarmonicsCount
        => 2;

    // Create a 6-dimensional Euclidean geometric algebra processor based on the
    // selected scalar processor
    public static RGaProcessor<Expr> GeometricProcessor { get; }
        = ScalarProcessor.CreateEuclideanRGaProcessor();

    // This is a pre-defined text generator for displaying multivectors
    // with symbolic Wolfram Mathematica scalars using Expr objects
    public static TextComposerExpr TextComposer { get; }
        = TextComposerExpr.DefaultComposer;

    // This is a pre-defined LaTeX generator for displaying multivectors
    // with symbolic Wolfram Mathematica scalars using Expr objects
    public static LaTeXComposerExpr LaTeXComposer { get; }
        = LaTeXComposerExpr.DefaultComposer;


    public static void Example1()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        var assumeExpr =
            $@"And[t >= 0, t <= 1, \[Omega] > 0, Element[t | \[Omega], Reals]]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var t = "t".CreateScalar(ScalarProcessor);
        var w = @"\[Omega]".CreateScalar(ScalarProcessor); //"2 * Pi * 50".CreateScalar(ScalarProcessor);
        var theta = "2 * Pi / 3".CreateScalar(ScalarProcessor);

        var v1 = "19".ToExpr() * GeometricProcessor.CreateVector(
            (w * t).Sin(),
            (w * t - theta).Sin(),
            (w * t + theta).Sin()
        );

        var v2 = "3".ToExpr() * GeometricProcessor.CreateVector(
            (2 * (w * t)).Sin(),
            (2 * (w * t - theta)).Sin(),
            (2 * (w * t + theta)).Sin()
        );

        var v3 = "5".ToExpr() * GeometricProcessor.CreateVector(
            (7 * (w * t)).Sin(),
            (7 * (w * t - theta)).Sin(),
            (7 * (w * t + theta)).Sin()
        );

        var v4 = "9".ToExpr() * GeometricProcessor.CreateVector(
            (5 * (w * t)).Sin(),
            (5 * (w * t - theta)).Sin(),
            (5 * (w * t + theta)).Sin()
        );

        //var v = (v1).TrigReduceScalars();
        //var v = (v1 + v2).TrigReduceScalars();
        var v = (v1 + v2 + v3).TrigReduceScalars();
        //var v = (v1 + v2 + v3 + v4).TrigReduceScalars();

        Console.WriteLine(@$"$\boldsymbol{{v}}={LaTeXComposer.GetMultivectorText(v)}$");
        Console.WriteLine();

        var vDt1 = v.DifferentiateScalars(t, 1).TrigReduceScalars();
        var vDt2 = v.DifferentiateScalars(t, 2).TrigReduceScalars();

        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime}}={LaTeXComposer.GetMultivectorText(vDt1)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime\prime}}={LaTeXComposer.GetMultivectorText(vDt2)}$");
        Console.WriteLine();

        var vDt1NormSquared = vDt1.NormSquared().TrigReduceScalar();

        var sDt1 = vDt1NormSquared.Sqrt();
        var sDt2 = sDt1.DifferentiateScalar(t).TrigReduceScalar();

        Console.WriteLine(@$"$s^{{\prime}}={LaTeXComposer.GetScalarText(sDt1)}$");
        Console.WriteLine(@$"$s^{{\prime\prime}}={LaTeXComposer.GetScalarText(sDt2)}$");
        Console.WriteLine();

        var vDs1 = (vDt1 / sDt1).TrigReduceScalars();
        var vDs2 = (vDs1.DifferentiateScalars(t) / sDt1).TrigReduceScalars();

        Console.WriteLine(@$"$\dot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs1)}$");
        Console.WriteLine(@$"$\ddot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs2)}$");
        Console.WriteLine();

        var (u1, u2) =
            vDs1.ApplyGramSchmidtByProjections(vDs2, false);

        //var vDs1NormSquared = vDs1.NormSquared();
        //var vDs12Dot = vDs1.Sp(vDs2);

        //var u1 = vDs1;
        //var u2 = vDs2 - (vDs12Dot / vDs1NormSquared) * u1;

        u1 = u1.TrigReduceScalars();
        u2 = u2.TrigReduceScalars();

        Console.WriteLine(@$"$\boldsymbol{{u}}_{{1}}={LaTeXComposer.GetMultivectorText(u1)}$");
        Console.WriteLine(@$"$\boldsymbol{{u}}_{{2}}={LaTeXComposer.GetMultivectorText(u2)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{1}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(u1.NormSquared())}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{2}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(u2.NormSquared())}$");
        Console.WriteLine();

        var omega1 =
            (sDt1 / u1.NormSquared() * u1.Op(u2)).TrigReduceScalars();

        var tMin = ScalarProcessor.CreateScalarZero();
        var tMax = "2 * Pi".ToExpr() / w;
        var tRange = tMax - tMin;

        var omega1Mean = omega1.MapScalars(s =>
            s.IntegrateScalar(t, tMin, tMax).TrigReduce()
        ) / tRange;

        var omega1MeanNorm =
            omega1Mean.NormSquared().TrigReduceScalar().Sqrt();

        Console.WriteLine(@$"$\boldsymbol{{\Omega}}_{{1}}={LaTeXComposer.GetMultivectorText(omega1)}$");
        Console.WriteLine(@$"$\overline{{\boldsymbol{{\Omega}}}}_{{1}}={LaTeXComposer.GetMultivectorText(omega1Mean)}$");
        Console.WriteLine(@$"$\left\Vert \overline{{\boldsymbol{{\Omega}}}}_{{1}}\right\Vert ={LaTeXComposer.GetScalarText(omega1MeanNorm)}$");
        Console.WriteLine();
    }

    public static void Example2()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        var assumeExpr =
            $@"And[t >= 0, t <= 1, \[Omega] > 0, Element[t | \[Omega], Reals]]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var t = "t".CreateScalar(ScalarProcessor);
        var w = @"\[Omega]".CreateScalar(ScalarProcessor);
        var twoPi = "2 * Pi".CreateScalar(ScalarProcessor);

        var n = VSpaceDimensions;
        var v1 = "200".ToExpr() * GeometricProcessor.CreateVector(
            (w * t - 0 * twoPi / n).Sin(),
            (w * t - 1 * twoPi / n).Sin(),
            (w * t - 2 * twoPi / n).Sin(),
            (w * t - 3 * twoPi / n).Sin(),
            (w * t - 4 * twoPi / n).Sin()
        );

        var v2 = "21".ToExpr() * GeometricProcessor.CreateVector(
            (2 * (w * t - 0 * twoPi / n)).Sin(),
            (2 * (w * t - 1 * twoPi / n)).Sin(),
            (2 * (w * t - 2 * twoPi / n)).Sin(),
            (2 * (w * t - 3 * twoPi / n)).Sin(),
            (2 * (w * t - 4 * twoPi / n)).Sin()
        );

        var v3 = "-55".ToExpr() * GeometricProcessor.CreateVector(
            (3 * (w * t - 0 * twoPi / n)).Sin(),
            (3 * (w * t - 1 * twoPi / n)).Sin(),
            (3 * (w * t - 2 * twoPi / n)).Sin(),
            (3 * (w * t - 3 * twoPi / n)).Sin(),
            (3 * (w * t - 4 * twoPi / n)).Sin()
        );

        var v4 = "-30".ToExpr() * GeometricProcessor.CreateVector(
            (4 * (w * t - 0 * twoPi / n)).Sin(),
            (4 * (w * t - 1 * twoPi / n)).Sin(),
            (4 * (w * t - 2 * twoPi / n)).Sin(),
            (4 * (w * t - 3 * twoPi / n)).Sin(),
            (4 * (w * t - 4 * twoPi / n)).Sin()
        );

        var v5 = "35".ToExpr() * GeometricProcessor.CreateVector(
            (5 * (w * t - 0 * twoPi / n)).Sin(),
            (5 * (w * t - 1 * twoPi / n)).Sin(),
            (5 * (w * t - 2 * twoPi / n)).Sin(),
            (5 * (w * t - 3 * twoPi / n)).Sin(),
            (5 * (w * t - 4 * twoPi / n)).Sin()
        );

        //var v = (v1).TrigReduceScalars();
        //var v = (v1 + v2).TrigReduceScalars();
        var v = (v1 + v2 + v3).TrigReduceScalars();
        //var v = (v1 + v2 + v3 + v4).TrigReduceScalars();
        //var v = (v1 + v2 + v3 + v4 + v5).TrigReduceScalars();

        Console.WriteLine(@$"$\boldsymbol{{v}}={LaTeXComposer.GetMultivectorText(v)}$");
        Console.WriteLine();

        var vDt1 = v.DifferentiateScalars(t, 1).TrigReduceScalars();
        var vDt2 = v.DifferentiateScalars(t, 2).TrigReduceScalars();

        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime}}={LaTeXComposer.GetMultivectorText(vDt1)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime\prime}}={LaTeXComposer.GetMultivectorText(vDt2)}$");
        Console.WriteLine();

        var vDt1NormSquared = vDt1.NormSquared().TrigReduceScalar();

        var sDt1 = vDt1NormSquared.Sqrt();
        var sDt2 = sDt1.DifferentiateScalar(t).TrigReduceScalar();

        Console.WriteLine(@$"$s^{{\prime}}={LaTeXComposer.GetScalarText(sDt1)}$");
        Console.WriteLine(@$"$s^{{\prime\prime}}={LaTeXComposer.GetScalarText(sDt2)}$");
        Console.WriteLine();

        var vDs1 = (vDt1 / sDt1).TrigReduceScalars();
        var vDs2 = (vDs1.DifferentiateScalars(t) / sDt1).TrigReduceScalars();

        Console.WriteLine(@$"$\dot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs1)}$");
        Console.WriteLine(@$"$\ddot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs2)}$");
        Console.WriteLine();

        var (u1, u2) =
            vDs1.ApplyGramSchmidtByProjections(vDs2, false);

        u1 = u1.TrigReduceScalars();
        u2 = u2.TrigReduceScalars();

        Console.WriteLine(@$"$\boldsymbol{{u}}_{{1}}={LaTeXComposer.GetMultivectorText(u1)}$");
        Console.WriteLine(@$"$\boldsymbol{{u}}_{{2}}={LaTeXComposer.GetMultivectorText(u2)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{1}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(u1.NormSquared())}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{2}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(u2.NormSquared())}$");
        Console.WriteLine();

        var omega1 =
            (sDt1 / u1.NormSquared() * u1.Op(u2)).TrigReduceScalars();

        Console.WriteLine(@$"$\boldsymbol{{\Omega}}_{{1}}={LaTeXComposer.GetMultivectorText(omega1)}$");
        Console.WriteLine();

        var tMin = ScalarProcessor.CreateScalarZero();
        var tMax = "2 * Pi".ToExpr() / w;
        var tRange = tMax - tMin;

        var omega1Mean = omega1.MapScalars(s =>
            s.IntegrateScalar(t, tMin, tMax).TrigReduce()
        ) / tRange;

        Console.WriteLine(@$"$\overline{{\boldsymbol{{\Omega}}}}_{{1}}={LaTeXComposer.GetMultivectorText(omega1Mean)}$");
        Console.WriteLine();

        var omega1MeanNorm =
            omega1Mean.NormSquared().TrigReduceScalar().Sqrt();

        Console.WriteLine(@$"$\left\Vert \overline{{\boldsymbol{{\Omega}}}}_{{1}}\right\Vert ={LaTeXComposer.GetScalarText(omega1MeanNorm)}$");
        Console.WriteLine();
    }

    public static void Example3()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        var assumeExpr =
            $@"And[t >= 0, t <= 1, \[Omega] > 0, Element[t | \[Omega], Reals]]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var t = "t".CreateScalar(ScalarProcessor);
        var w = @"\[Omega]".CreateScalar(ScalarProcessor); //"2 * Pi * 50".CreateScalar(ScalarProcessor);
        var theta = "2 * Pi / 3".CreateScalar(ScalarProcessor);

        var v1 = "Sqrt[2]".ToExpr() * GeometricProcessor.CreateVector(
            210 * (w * t).Sin(),
            200 * (w * t - theta).Sin(),
            195 * (w * t + theta).Sin()
        );

        var v2 = "Sqrt[2]".ToExpr() * GeometricProcessor.CreateVector(
            22 * (2 * (w * t)).Sin(),
            20 * (2 * (w * t - theta)).Sin(),
            18 * (2 * (w * t + theta)).Sin()
        );

        var v3 = "Sqrt[2]".ToExpr() * GeometricProcessor.CreateVector(
            -30 * (7 * (w * t)).Sin(),
            -35 * (7 * (w * t - theta)).Sin(),
            -24 * (7 * (w * t + theta)).Sin()
        );

        var v = (v1 + v2 + v3).TrigReduceScalars();

        Console.WriteLine(@$"$\boldsymbol{{v}}={LaTeXComposer.GetMultivectorText(v)}$");
        Console.WriteLine();

        var vDt1 = v.DifferentiateScalars(t, 1).TrigReduceScalars();
        var vDt2 = v.DifferentiateScalars(t, 2).TrigReduceScalars();
        var vDt3 = v.DifferentiateScalars(t, 3).TrigReduceScalars();

        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime}}={LaTeXComposer.GetMultivectorText(vDt1)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime\prime}}={LaTeXComposer.GetMultivectorText(vDt2)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime\prime\prime}}={LaTeXComposer.GetMultivectorText(vDt3)}$");
        Console.WriteLine();

        var vDt1NormSquared = vDt1.NormSquared().TrigReduceScalar();

        var sDt1 = vDt1NormSquared.Sqrt();
        var sDt2 = sDt1.DifferentiateScalar(t).TrigReduceScalar();
        var sDt3 = sDt2.DifferentiateScalar(t).TrigReduceScalar();

        Console.WriteLine(@$"$s^{{\prime}}={LaTeXComposer.GetScalarText(sDt1)}$");
        Console.WriteLine(@$"$s^{{\prime\prime}}={LaTeXComposer.GetScalarText(sDt2)}$");
        Console.WriteLine(@$"$s^{{\prime\prime\prime}}={LaTeXComposer.GetScalarText(sDt3)}$");
        Console.WriteLine();

        var vDs1 = (vDt1 / sDt1).TrigReduceScalars();
        var vDs2 = (vDs1.DifferentiateScalars(t) / sDt1).TrigReduceScalars();
        var vDs3 = (vDs2.DifferentiateScalars(t) / sDt1).TrigReduceScalars();

        Console.WriteLine(@$"$\dot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs1)}$");
        Console.WriteLine(@$"$\ddot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs2)}$");
        Console.WriteLine(@$"$\ddot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs3)}$");
        Console.WriteLine();

        var (u1, u2, u3) =
            vDs1.ApplyGramSchmidtByProjections(vDs2, vDs3, false);

        u1 = u1.TrigReduceScalars();
        u2 = u2.TrigReduceScalars();
        u3 = u3.TrigReduceScalars();

        Console.WriteLine(@$"$\boldsymbol{{u}}_{{1}}={LaTeXComposer.GetMultivectorText(u1)}$");
        Console.WriteLine(@$"$\boldsymbol{{u}}_{{2}}={LaTeXComposer.GetMultivectorText(u2)}$");
        Console.WriteLine(@$"$\boldsymbol{{u}}_{{3}}={LaTeXComposer.GetMultivectorText(u3)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{1}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(u1.NormSquared())}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{2}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(u2.NormSquared())}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{3}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(u3.NormSquared())}$");
        Console.WriteLine();

        var omega1 =
            (sDt1 / u1.NormSquared() * u1.Op(u2)).TrigReduceScalars();

        var omega2 =
            (sDt1 / u2.NormSquared() * u2.Op(u3)).TrigReduceScalars();

        var tMin = ScalarProcessor.CreateScalarZero();
        var tMax = "2 * Pi".ToExpr() / w;
        var tRange = tMax - tMin;

        var omega1Mean = omega1.MapScalars(s =>
            s.IntegrateScalar(t, tMin, tMax).TrigReduce()
        ) / tRange;

        var omega2Mean = omega2.MapScalars(s =>
            s.IntegrateScalar(t, tMin, tMax).TrigReduce()
        ) / tRange;

        var omega1MeanNorm =
            omega1Mean.NormSquared().TrigReduceScalar().Sqrt();

        var omega2MeanNorm =
            omega2Mean.NormSquared().TrigReduceScalar().Sqrt();

        Console.WriteLine(@$"$\boldsymbol{{\Omega}}_{{1}}={LaTeXComposer.GetMultivectorText(omega1)}$");
        Console.WriteLine(@$"$\boldsymbol{{\Omega}}_{{2}}={LaTeXComposer.GetMultivectorText(omega2)}$");
        Console.WriteLine(@$"$\overline{{\boldsymbol{{\Omega}}}}_{{1}}={LaTeXComposer.GetMultivectorText(omega1Mean)}$");
        Console.WriteLine(@$"$\overline{{\boldsymbol{{\Omega}}}}_{{2}}={LaTeXComposer.GetMultivectorText(omega2Mean)}$");
        Console.WriteLine(@$"$\left\Vert \overline{{\boldsymbol{{\Omega}}}}_{{1}}\right\Vert ={LaTeXComposer.GetScalarText(omega1MeanNorm)}$");
        Console.WriteLine(@$"$\left\Vert \overline{{\boldsymbol{{\Omega}}}}_{{2}}\right\Vert ={LaTeXComposer.GetScalarText(omega2MeanNorm)}$");
        Console.WriteLine();
    }

    public static void Example4()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        var assumeExpr =
            $@"And[t >= 0, t <= 1, \[Omega] > 0, V > 0, Element[t | \[Omega] | V, Reals], Element[a, Vectors[{VSpaceDimensions}, Reals]]]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var t = "t".CreateScalar(ScalarProcessor);
        var w = @"\[Omega]".CreateScalar(ScalarProcessor);
        var wt = w * t;

        var a =
            Enumerable
                .Range(0, VSpaceDimensions)
                .Select(k => $"V * Cos[2 * Pi * {k} / {VSpaceDimensions}]".ToExpr())
                .CreateVector(GeometricProcessor);

        var b =
            Enumerable
                .Range(0, VSpaceDimensions)
                .Select(k => $"V * Sin[2 * Pi * {k} / {VSpaceDimensions}]".ToExpr())
                .CreateVector(GeometricProcessor);

        var aNormSquared = a.NormSquared();
        var bNormSquared = b.NormSquared();
        var abDot = a.Sp(b);
        var abOp = a.Op(b);

        var v = wt.Cos() * a + wt.Sin() * b;

        var vDt1 = v.DifferentiateScalars(t, 1);
        var vDt2 = v.DifferentiateScalars(t, 2);

        var vDt1NormSquared = vDt1.NormSquared();
        var vDt12Dot = vDt1.Sp(vDt2);

        var sDt1 = vDt1NormSquared.Sqrt();
        var sDt2 = sDt1.DifferentiateScalar(t);

        var vDs1 = vDt1 / sDt1;
        var vDs2 = vDs1.DifferentiateScalars(t) / sDt1;

        var vDs1NormSquared = vDs1.NormSquared();
        var vDs12Dot = vDs1.Sp(vDs2);

        var u1 = vDs1;
        var u2 = vDs2 - vDs12Dot / vDs1NormSquared * vDs1;

        var u1NormSquared = u1.NormSquared();
        var u2NormSquared = u2.NormSquared();

        var e1 = u1;
        var e2 = u2 / u2NormSquared.Sqrt();

        var kappa1 = sDt1 * (u2NormSquared / u1NormSquared).Sqrt();
        var omega1 = kappa1 * e1.Op(e2);


        Console.WriteLine(@$"$\boldsymbol{{a}}={LaTeXComposer.GetMultivectorText(a)}$");
        Console.WriteLine(@$"$\boldsymbol{{b}}={LaTeXComposer.GetMultivectorText(b)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{a}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(aNormSquared)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{b}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(bNormSquared)}$");
        Console.WriteLine(@$"$\boldsymbol{{a}}\cdot\boldsymbol{{b}} ={LaTeXComposer.GetScalarText(abDot)}$");
        Console.WriteLine(@$"$\boldsymbol{{a}}\wedge\boldsymbol{{b}} ={LaTeXComposer.GetMultivectorText(abOp)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{v}}={LaTeXComposer.GetMultivectorText(v)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime}}={LaTeXComposer.GetMultivectorText(vDt1)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime\prime}}={LaTeXComposer.GetMultivectorText(vDt2)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\dot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs1)}$");
        Console.WriteLine(@$"$\ddot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs2)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$s^{{\prime}}={LaTeXComposer.GetScalarText(sDt1)}$");
        Console.WriteLine(@$"$s^{{\prime\prime}}={LaTeXComposer.GetScalarText(sDt2)}$");
        Console.WriteLine();

        //Console.WriteLine(@$"$\boldsymbol{{u}}_{{1}}={LaTeXComposer.GetMultivectorText(u1)}$");
        //Console.WriteLine(@$"$\boldsymbol{{u}}_{{2}}={LaTeXComposer.GetMultivectorText(u2)}$");
        //Console.WriteLine();

        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{1}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(u1NormSquared)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{2}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(u2NormSquared)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{e}}_{{1}}={LaTeXComposer.GetMultivectorText(e1)}$");
        Console.WriteLine(@$"$\boldsymbol{{e}}_{{2}}={LaTeXComposer.GetMultivectorText(e2)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\kappa_{{1}}={LaTeXComposer.GetScalarText(kappa1)}$");
        Console.WriteLine(@$"$\boldsymbol{{\Omega}}_{{1}}={LaTeXComposer.GetMultivectorText(omega1)}$");
        Console.WriteLine();
    }

    public static void Example5()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        var assumeExpr1 =
            Enumerable
                .Range(0, VSpaceDimensions)
                .Select(k => $"Subscript[V,{k + 1}] > 0")
                .Concatenate(",");

        var assumeExpr2 =
            Enumerable
                .Range(0, VSpaceDimensions)
                .Select(k => $"Subscript[V,{k + 1}]")
                .Concatenate(" | ");

        var assumeExpr =
            $@"And[t >= 0, t <= 1, \[Omega] > 0, {assumeExpr1}, Element[t | \[Omega] | {assumeExpr2}, Reals], Element[a, Vectors[{VSpaceDimensions}, Reals]]]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var t = "t".CreateScalar(ScalarProcessor);
        var w = @"\[Omega]".CreateScalar(ScalarProcessor);
        var wt = w * t;

        var a =
            Enumerable
                .Range(0, VSpaceDimensions)
                .Select(k => $"Subscript[V,{k + 1}] * Cos[2 * Pi * {k} / {VSpaceDimensions}]".ToExpr())
                .CreateVector(GeometricProcessor);

        var b =
            Enumerable
                .Range(0, VSpaceDimensions)
                .Select(k => $"Subscript[V,{k + 1}] * Sin[2 * Pi * {k} / {VSpaceDimensions}]".ToExpr())
                .CreateVector(GeometricProcessor);

        var aNormSquared = a.NormSquared();
        var bNormSquared = b.NormSquared();
        var abDot = a.Sp(b);
        var abOp = a.Op(b);

        var v = wt.Cos() * a + wt.Sin() * b;

        var vDt1 = v.DifferentiateScalars(t, 1);
        var vDt2 = v.DifferentiateScalars(t, 2);

        var vDt1NormSquared = vDt1.NormSquared();
        var vDt12Dot = vDt1.Sp(vDt2);

        var sDt1 = vDt1NormSquared.Sqrt();
        var sDt2 = sDt1.DifferentiateScalar(t);

        var vDs1 = vDt1 / sDt1;
        var vDs2 = vDs1.DifferentiateScalars(t) / sDt1;

        var vDs1NormSquared = vDs1.NormSquared();
        var vDs12Dot = vDs1.Sp(vDs2);

        var u1 = vDs1;
        var u2 = vDs2 - vDs12Dot / vDs1NormSquared * vDs1;

        var u1Norm = u1.Norm();
        var u2Norm = u2.Norm();

        var e1 = u1 / u1Norm;
        var e2 = u2 / u2Norm;

        var kappa1 = sDt1 * (u2Norm / u1Norm);
        var omega1 = kappa1 * e1.Op(e2);


        Console.WriteLine(@$"$\boldsymbol{{a}}={LaTeXComposer.GetMultivectorText(a)}$");
        Console.WriteLine(@$"$\boldsymbol{{b}}={LaTeXComposer.GetMultivectorText(b)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{a}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(aNormSquared)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{b}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(bNormSquared)}$");
        Console.WriteLine(@$"$\boldsymbol{{a}}\cdot\boldsymbol{{b}} ={LaTeXComposer.GetScalarText(abDot)}$");
        Console.WriteLine(@$"$\boldsymbol{{a}}\wedge\boldsymbol{{b}} ={LaTeXComposer.GetMultivectorText(abOp)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{v}}={LaTeXComposer.GetMultivectorText(v)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime}}={LaTeXComposer.GetMultivectorText(vDt1)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime\prime}}={LaTeXComposer.GetMultivectorText(vDt2)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\dot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs1)}$");
        Console.WriteLine(@$"$\ddot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs2)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$s^{{\prime}}={LaTeXComposer.GetScalarText(sDt1)}$");
        Console.WriteLine(@$"$s^{{\prime\prime}}={LaTeXComposer.GetScalarText(sDt2)}$");
        Console.WriteLine();

        //Console.WriteLine(@$"$\boldsymbol{{u}}_{{1}}={LaTeXComposer.GetMultivectorText(u1)}$");
        //Console.WriteLine(@$"$\boldsymbol{{u}}_{{2}}={LaTeXComposer.GetMultivectorText(u2)}$");
        //Console.WriteLine();

        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{1}}\right\Vert ={LaTeXComposer.GetScalarText(u1Norm)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{2}}\right\Vert ={LaTeXComposer.GetScalarText(u2Norm)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{e}}_{{1}}={LaTeXComposer.GetMultivectorText(e1)}$");
        Console.WriteLine(@$"$\boldsymbol{{e}}_{{2}}={LaTeXComposer.GetMultivectorText(e2)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\kappa_{{1}}={LaTeXComposer.GetScalarText(kappa1)}$");
        Console.WriteLine(@$"$\boldsymbol{{\Omega}}_{{1}}={LaTeXComposer.GetMultivectorText(omega1)}$");
        Console.WriteLine();
    }

    public static void Example6()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        var assumeExprArray = new List<string>();
        for (var i = 1; i <= VSpaceDimensions; i++)
        for (var j = 1; j <= HarmonicsCount; j++)
            assumeExprArray.Add($"Subscript[V,{i}{j}]");

        var assumeExpr1 =
            assumeExprArray.Select(s => $"{s} > 0").Concatenate(", ");

        var assumeExpr2 =
            assumeExprArray.Concatenate(" | ");

        var assumeExpr =
            $@"And[t >= 0, t <= 1, \[Omega] > 0, {assumeExpr1}, Element[t | \[Omega] | {assumeExpr2}, Reals], Element[a, Vectors[{VSpaceDimensions}, Reals]]]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var t = "t".CreateScalar(ScalarProcessor);
        var w = @"\[Omega]".CreateScalar(ScalarProcessor);
        var wtArray =
            Enumerable
                .Range(1, HarmonicsCount)
                .Select(k => k * w * t)
                .ToArray();

        var aVectors = new RGaVector<Expr>[HarmonicsCount];
        var bVectors = new RGaVector<Expr>[HarmonicsCount];
        var v = GeometricProcessor.CreateZeroVector();

        for (var i = 0; i < HarmonicsCount; i++)
        {
            var j = i + 1;

            aVectors[i] =
                Enumerable
                    .Range(0, VSpaceDimensions)
                    .Select(k => $"Subscript[V,{k + 1}{j}] * Cos[2 * Pi * {k} / {VSpaceDimensions}]".ToExpr())
                    .CreateVector(GeometricProcessor);

            bVectors[i] =
                Enumerable
                    .Range(0, VSpaceDimensions)
                    .Select(k => $"Subscript[V,{k + 1}{j}] * Sin[2 * Pi * {k} / {VSpaceDimensions}]".ToExpr())
                    .CreateVector(GeometricProcessor);

            v += wtArray[i].Cos() * aVectors[i] +
                 wtArray[i].Sin() * bVectors[i];
        }

        //var aNormSquared = a1.NormSquared();
        //var bNormSquared = b1.NormSquared();
        //var abDot = a1.Sp(b1);
        //var abOp = a1.Op(b1);

        var vDt1 = v.DifferentiateScalars(t, 1);
        var vDt2 = v.DifferentiateScalars(t, 2);
        var vDt3 = v.DifferentiateScalars(t, 3);

        var vDt1NormSquared = vDt1.NormSquared();
        //var vDt12Dot = vDt1.Sp(vDt2);

        var sDt1 = vDt1NormSquared.Sqrt();
        var sDt2 = sDt1.DifferentiateScalar(t);
        var sDt3 = sDt2.DifferentiateScalar(t);

        var vDs1 = vDt1 / sDt1;
        var vDs2 = vDs1.DifferentiateScalars(t) / sDt1;
        var vDs3 = vDs2.DifferentiateScalars(t) / sDt1;

        var vDs1NormSquared = vDs1.NormSquared();
        var vDs12Dot = vDs1.Sp(vDs2);

        var (u1, u2, u3) =
            vDs1.ApplyGramSchmidtByProjections(vDs2, vDs3, false);

        //var u1 = vDs1;
        //var u2 = vDs2 - (vDs12Dot / vDs1NormSquared) * u1;

        var u1Norm = u1.Norm();
        var u2Norm = u2.Norm();
        var u3Norm = u3.Norm();

        var e1 = u1 / u1Norm;
        var e2 = u2 / u2Norm;
        var e3 = u3 / u3Norm;

        var kappa1 = sDt1 * (u2Norm / u1Norm);
        var kappa2 = sDt1 * (u3Norm / u2Norm);

        var omega1 = kappa1 * e1.Op(e2);
        var omega2 = kappa2 * e2.Op(e3);


        for (var i = 0; i < HarmonicsCount; i++)
        {
            Console.WriteLine(@$"$\boldsymbol{{a}}_{{{i + 1}}}={LaTeXComposer.GetMultivectorText(aVectors[0])}$");
            Console.WriteLine(@$"$\boldsymbol{{b}}_{{{i + 1}}}={LaTeXComposer.GetMultivectorText(bVectors[0])}$");
            Console.WriteLine();
        }

        //Console.WriteLine(@$"$\left\Vert \boldsymbol{{a}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(aNormSquared)}$");
        //Console.WriteLine(@$"$\left\Vert \boldsymbol{{b}}\right\Vert^{{2}} ={LaTeXComposer.GetScalarText(bNormSquared)}$");
        //Console.WriteLine(@$"$\boldsymbol{{a}}\cdot\boldsymbol{{b}} ={LaTeXComposer.GetScalarText(abDot)}$");
        //Console.WriteLine(@$"$\boldsymbol{{a}}\wedge\boldsymbol{{b}} ={LaTeXComposer.GetMultivectorText(abOp)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{v}}={LaTeXComposer.GetMultivectorText(v)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime}}={LaTeXComposer.GetMultivectorText(vDt1)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime\prime}}={LaTeXComposer.GetMultivectorText(vDt2)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime\prime\prime}}={LaTeXComposer.GetMultivectorText(vDt3)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\dot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs1)}$");
        Console.WriteLine(@$"$\ddot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs2)}$");
        Console.WriteLine(@$"$\dddot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs3)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$s^{{\prime}}={LaTeXComposer.GetScalarText(sDt1)}$");
        Console.WriteLine(@$"$s^{{\prime\prime}}={LaTeXComposer.GetScalarText(sDt2)}$");
        Console.WriteLine(@$"$s^{{\prime\prime\prime}}={LaTeXComposer.GetScalarText(sDt3)}$");
        Console.WriteLine();

        //Console.WriteLine(@$"$\boldsymbol{{u}}_{{1}}={LaTeXComposer.GetMultivectorText(u1)}$");
        //Console.WriteLine(@$"$\boldsymbol{{u}}_{{2}}={LaTeXComposer.GetMultivectorText(u2)}$");
        //Console.WriteLine();

        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{1}}\right\Vert ={LaTeXComposer.GetScalarText(u1Norm)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{2}}\right\Vert ={LaTeXComposer.GetScalarText(u2Norm)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{3}}\right\Vert ={LaTeXComposer.GetScalarText(u3Norm)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{e}}_{{1}}={LaTeXComposer.GetMultivectorText(e1)}$");
        Console.WriteLine(@$"$\boldsymbol{{e}}_{{2}}={LaTeXComposer.GetMultivectorText(e2)}$");
        Console.WriteLine(@$"$\boldsymbol{{e}}_{{3}}={LaTeXComposer.GetMultivectorText(e3)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\kappa_{{1}}={LaTeXComposer.GetScalarText(kappa1)}$");
        Console.WriteLine(@$"$\kappa_{{2}}={LaTeXComposer.GetScalarText(kappa2)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{\Omega}}_{{1}}={LaTeXComposer.GetMultivectorText(omega1)}$");
        Console.WriteLine(@$"$\boldsymbol{{\Omega}}_{{2}}={LaTeXComposer.GetMultivectorText(omega2)}$");
        Console.WriteLine();
    }

    public static void Example7()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        var assumeExpr =
            $@"And[t >= 0, t <= 1, \[Omega] > 0, Element[t | \[Omega], Reals], Element[a | b, Vectors[{VSpaceDimensions}, Reals]]]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var t = "t".CreateScalar(ScalarProcessor);
        var w = @"\[Omega]".CreateScalar(ScalarProcessor);
        var wt = w * t;

        var a = "a".CreateScalar(ScalarProcessor);
        var b = "b".CreateScalar(ScalarProcessor);

        var v = wt.Cos() * a + wt.Sin() * b;

        var vDt1 = v.DifferentiateScalar(t, 1);
        var vDt2 = v.DifferentiateScalar(t, 2);

        var vDt1NormSquared = vDt1.NormSquared();
        var vDt12Dot = vDt1.Dot(vDt2);

        var sDt1 = vDt1NormSquared.Sqrt();
        var sDt2 = sDt1.DifferentiateScalar(t);

        var vDs1 = vDt1 / sDt1;
        var vDs2 = vDs1.DifferentiateScalar(t) / sDt1;

        var vDs1NormSquared = vDs1.NormSquared();
        var vDs12Dot = vDs1.Dot(vDs2);

        var u1 = vDs1;
        var u2 = vDs2 - vDs2.ProjectOn(u1);

        var u1Norm = u1.Norm();
        var u2Norm = u2.Norm();

        var e1 = u1 / u1Norm;
        var e2 = u2 / u2Norm;

        var kappa1 = sDt1 * (u2Norm / u1Norm);
        //var omega1 = kappa1 * e1.Op(e2);

        Console.WriteLine(@$"$\boldsymbol{{v}}={LaTeXComposer.GetScalarText(v)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime}}={LaTeXComposer.GetScalarText(vDt1)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime\prime}}={LaTeXComposer.GetScalarText(vDt2)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\dot{{\boldsymbol{{v}}}}={LaTeXComposer.GetScalarText(vDs1)}$");
        Console.WriteLine(@$"$\ddot{{\boldsymbol{{v}}}}={LaTeXComposer.GetScalarText(vDs2)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$s^{{\prime}}={LaTeXComposer.GetScalarText(sDt1)}$");
        Console.WriteLine(@$"$s^{{\prime\prime}}={LaTeXComposer.GetScalarText(sDt2)}$");
        Console.WriteLine();

        //Console.WriteLine(@$"$\boldsymbol{{u}}_{{1}}={LaTeXComposer.GetScalarText(u1)}$");
        //Console.WriteLine(@$"$\boldsymbol{{u}}_{{2}}={LaTeXComposer.GetScalarText(u2)}$");
        //Console.WriteLine();

        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{1}}\right\Vert ={LaTeXComposer.GetScalarText(u1Norm)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{2}}\right\Vert ={LaTeXComposer.GetScalarText(u2Norm)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{e}}_{{1}}={LaTeXComposer.GetScalarText(e1)}$");
        Console.WriteLine(@$"$\boldsymbol{{e}}_{{2}}={LaTeXComposer.GetScalarText(e2)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\kappa_{{1}}={LaTeXComposer.GetScalarText(kappa1)}$");
        //Console.WriteLine(@$"$\boldsymbol{{\Omega}}_{{1}}={LaTeXComposer.GetMultivectorText(omega1)}$");
        Console.WriteLine();
    }

    public static void Example8()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        var assumeExpr =
            $@"And[t >= 0, t <= 1, \[Omega] > 0, Element[t | \[Omega], Reals], Element[a | b | c, Vectors[{VSpaceDimensions}, Reals]]]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var t = "t".CreateScalar(ScalarProcessor);
        var w = @"\[Omega]".CreateScalar(ScalarProcessor);
        var wt = w * t;

        var a = "a".CreateScalar(ScalarProcessor);
        var b = "b".CreateScalar(ScalarProcessor);
        var c = "c".CreateScalar(ScalarProcessor);

        var v =
            wt.Cos() * a + wt.Sin() * a +
            (2 * wt).Cos() * b + (2 * wt).Sin() * b +
            (3 * wt).Cos() * c + (3 * wt).Sin() * c;

        var vDt1 = v.DifferentiateScalar(t, 1);
        var vDt1NormSquared = vDt1.NormSquared();

        Console.WriteLine(@$"$\boldsymbol{{v}}={LaTeXComposer.GetScalarText(v)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime}}={LaTeXComposer.GetScalarText(vDt1)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{v}}^{{\prime}} \right\Vert ={LaTeXComposer.GetScalarText(vDt1NormSquared)}$");
        Console.WriteLine();

        var vDt2 = v.DifferentiateScalar(t, 2);
        var vDt3 = v.DifferentiateScalar(t, 3);

        //var vDt1NormSquared = vDt1.NormSquared();
        var vDt12Dot = vDt1.Dot(vDt2);

        var sDt1 = vDt1NormSquared.Sqrt();
        var sDt2 = sDt1.DifferentiateScalar(t);
        var sDt3 = sDt2.DifferentiateScalar(t);

        var vDs1 = vDt1 / sDt1;
        var vDs2 = vDs1.DifferentiateScalar(t) / sDt1;
        var vDs3 = vDs2.DifferentiateScalar(t) / sDt1;

        var u1 = vDs1;
        var u2 = vDs2 - vDs2.ProjectOn(u1);
        var u3 = vDs3 - vDs3.ProjectOn(u1) - vDs3.ProjectOn(u2);

        var u1Norm = u1.Norm();
        var u2Norm = u2.Norm();
        var u3Norm = u3.Norm();

        var e1 = u1 / u1Norm;
        var e2 = u2 / u2Norm;
        var e3 = u3 / u3Norm;

        var kappa1 = sDt1 * (u2Norm / u1Norm);
        var kappa2 = sDt1 * (u3Norm / u2Norm);
        //var omega1 = kappa1 * e1.Op(e2);

        Console.WriteLine(@$"$\boldsymbol{{v}}={LaTeXComposer.GetScalarText(v)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime}}={LaTeXComposer.GetScalarText(vDt1)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime\prime}}={LaTeXComposer.GetScalarText(vDt2)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime\prime\prime}}={LaTeXComposer.GetScalarText(vDt3)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\dot{{\boldsymbol{{v}}}}={LaTeXComposer.GetScalarText(vDs1)}$");
        Console.WriteLine(@$"$\ddot{{\boldsymbol{{v}}}}={LaTeXComposer.GetScalarText(vDs2)}$");
        Console.WriteLine(@$"$\dddot{{\boldsymbol{{v}}}}={LaTeXComposer.GetScalarText(vDs3)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$s^{{\prime}}={LaTeXComposer.GetScalarText(sDt1)}$");
        Console.WriteLine(@$"$s^{{\prime\prime}}={LaTeXComposer.GetScalarText(sDt2)}$");
        Console.WriteLine(@$"$s^{{\prime\prime\prime}}={LaTeXComposer.GetScalarText(sDt3)}$");
        Console.WriteLine();

        //Console.WriteLine(@$"$\boldsymbol{{u}}_{{1}}={LaTeXComposer.GetScalarText(u1)}$");
        //Console.WriteLine(@$"$\boldsymbol{{u}}_{{2}}={LaTeXComposer.GetScalarText(u2)}$");
        //Console.WriteLine(@$"$\boldsymbol{{u}}_{{3}}={LaTeXComposer.GetScalarText(u3)}$");
        //Console.WriteLine();

        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{1}}\right\Vert = {LaTeXComposer.GetScalarText(u1Norm)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{2}}\right\Vert = {LaTeXComposer.GetScalarText(u2Norm)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{3}}\right\Vert = {LaTeXComposer.GetScalarText(u3Norm)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{e}}_{{1}}={LaTeXComposer.GetScalarText(e1)}$");
        Console.WriteLine(@$"$\boldsymbol{{e}}_{{2}}={LaTeXComposer.GetScalarText(e2)}$");
        Console.WriteLine(@$"$\boldsymbol{{e}}_{{3}}={LaTeXComposer.GetScalarText(e3)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\kappa_{{1}}={LaTeXComposer.GetScalarText(kappa1)}$");
        Console.WriteLine(@$"$\kappa_{{2}}={LaTeXComposer.GetScalarText(kappa2)}$");
        //Console.WriteLine(@$"$\boldsymbol{{\Omega}}_{{1}}={LaTeXComposer.GetMultivectorText(omega1)}$");
        Console.WriteLine();
    }

    public static void Example9()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        var assumeExpr =
            $@"And[t >= 0, t <= 1, \[Omega] > 0, Element[t | \[Omega], Reals], Element[a1 | a2 | a3 | b1 | b2 | b3, Vectors[{VSpaceDimensions}, Reals]]]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var t = "t".CreateScalar(ScalarProcessor);
        var w = @"\[Omega]".CreateScalar(ScalarProcessor);
        var wt = w * t;

        var a1 = "a1".CreateScalar(ScalarProcessor);
        var a2 = "a2".CreateScalar(ScalarProcessor);
        var a3 = "a3".CreateScalar(ScalarProcessor);

        var b1 = "b1".CreateScalar(ScalarProcessor);
        var b2 = "b2".CreateScalar(ScalarProcessor);
        var b3 = "b3".CreateScalar(ScalarProcessor);

        var v =
            wt.Cos() * a1 + wt.Sin() * b1 +
            (2 * wt).Cos() * a2 + (2 * wt).Sin() * b2;
        //(3 * wt).Cos() * a3 + (3 * wt).Sin() * b3;

        var vDt1 = v.DifferentiateScalar(t, 1);
        var vDt2 = v.DifferentiateScalar(t, 2);
        var vDt3 = v.DifferentiateScalar(t, 3);

        var vDt1NormSquared = vDt1.NormSquared();
        var vDt12Dot = vDt1.Dot(vDt2);

        var sDt1 = vDt1NormSquared.Sqrt();
        var sDt2 = sDt1.DifferentiateScalar(t);
        var sDt3 = sDt2.DifferentiateScalar(t);

        var vDs1 = vDt1 / sDt1;
        var vDs2 = vDs1.DifferentiateScalar(t) / sDt1;
        var vDs3 = vDs2.DifferentiateScalar(t) / sDt1;

        var u1 = vDs1;
        var u2 = vDs2 - vDs2.ProjectOn(u1);
        var u3 = vDs3 - vDs3.ProjectOn(u1) - vDs3.ProjectOn(u2);

        var u1Norm = u1.Norm();
        var u2Norm = u2.Norm();
        var u3Norm = u3.Norm();

        var e1 = u1 / u1Norm;
        var e2 = u2 / u2Norm;
        var e3 = u3 / u3Norm;

        var kappa1 = sDt1 * (u2Norm / u1Norm);
        var kappa2 = sDt1 * (u3Norm / u2Norm);
        //var omega1 = kappa1 * e1.Op(e2);

        Console.WriteLine(@$"$\boldsymbol{{v}}={LaTeXComposer.GetScalarText(v)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime}}={LaTeXComposer.GetScalarText(vDt1)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime\prime}}={LaTeXComposer.GetScalarText(vDt2)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime\prime\prime}}={LaTeXComposer.GetScalarText(vDt3)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\dot{{\boldsymbol{{v}}}}={LaTeXComposer.GetScalarText(vDs1)}$");
        Console.WriteLine(@$"$\ddot{{\boldsymbol{{v}}}}={LaTeXComposer.GetScalarText(vDs2)}$");
        Console.WriteLine(@$"$\dddot{{\boldsymbol{{v}}}}={LaTeXComposer.GetScalarText(vDs3)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$s^{{\prime}}={LaTeXComposer.GetScalarText(sDt1)}$");
        Console.WriteLine(@$"$s^{{\prime\prime}}={LaTeXComposer.GetScalarText(sDt2)}$");
        Console.WriteLine(@$"$s^{{\prime\prime\prime}}={LaTeXComposer.GetScalarText(sDt3)}$");
        Console.WriteLine();

        //Console.WriteLine(@$"$\boldsymbol{{u}}_{{1}}={LaTeXComposer.GetScalarText(u1)}$");
        //Console.WriteLine(@$"$\boldsymbol{{u}}_{{2}}={LaTeXComposer.GetScalarText(u2)}$");
        //Console.WriteLine(@$"$\boldsymbol{{u}}_{{3}}={LaTeXComposer.GetScalarText(u3)}$");
        //Console.WriteLine();

        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{1}}\right\Vert = {LaTeXComposer.GetScalarText(u1Norm)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{2}}\right\Vert = {LaTeXComposer.GetScalarText(u2Norm)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{3}}\right\Vert = {LaTeXComposer.GetScalarText(u3Norm)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{e}}_{{1}}={LaTeXComposer.GetScalarText(e1)}$");
        Console.WriteLine(@$"$\boldsymbol{{e}}_{{2}}={LaTeXComposer.GetScalarText(e2)}$");
        Console.WriteLine(@$"$\boldsymbol{{e}}_{{3}}={LaTeXComposer.GetScalarText(e3)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\kappa_{{1}}={LaTeXComposer.GetScalarText(kappa1)}$");
        Console.WriteLine(@$"$\kappa_{{2}}={LaTeXComposer.GetScalarText(kappa2)}$");
        //Console.WriteLine(@$"$\boldsymbol{{\Omega}}_{{1}}={LaTeXComposer.GetMultivectorText(omega1)}$");
        Console.WriteLine();
    }
}