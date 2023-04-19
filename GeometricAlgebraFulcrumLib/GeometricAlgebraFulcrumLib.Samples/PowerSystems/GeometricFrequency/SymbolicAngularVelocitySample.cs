using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Frames;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.PowerSystems.GeometricFrequency
{
    public static class SymbolicAngularVelocitySample
    {
        // This is a pre-defined scalar processor for symbolic
        // Wolfram Mathematica scalars using Expr objects
        public static ScalarProcessorExpr ScalarProcessor { get; }
            = ScalarProcessorExpr.DefaultProcessor;

        public static int VSpaceDimensions
            => 6;

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
                $@"And[t >= 0, t <= 1, \[Omega] > 0, V > 0, Element[t | \[Omega] | V, Reals]]".ToExpr();

            MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

            var t = "t".CreateScalar(ScalarProcessor);
            var w = @"\[Omega]".CreateScalar(ScalarProcessor); //"2 * Pi * 50".CreateScalar(ScalarProcessor);
            var wt = w * t;
            var theta = $"2 * Pi / {VSpaceDimensions}".CreateScalar(ScalarProcessor);
            var vMax = "V".CreateScalar(ScalarProcessor);

            ScalarProcessor.SimplificationFunc = expr => expr;

            var v1Scalars =
                Enumerable
                    .Range(0, VSpaceDimensions)
                    .Select(k => vMax * (wt - k * theta).Cos())
                    .ToArray();
            v1Scalars[VSpaceDimensions - 1] /= 2;

            var v1 =
                v1Scalars
                    .Select(s => s.ScalarValue)
                    .CreateVector(GeometricProcessor);

            var v2Scalars =
                Enumerable
                    .Range(0, VSpaceDimensions)
                    .Select(k => vMax * (7 * (wt - k * theta)).Cos() / 10)
                    .ToArray();
            v2Scalars[VSpaceDimensions - 1] /= 2;

            var v2 =
                v2Scalars.Select(s => s.ScalarValue).CreateVector(GeometricProcessor);

            var v = v1 + v2;

            Console.WriteLine(@$"$\boldsymbol{{v}}={LaTeXComposer.GetMultivectorText(v)}$");
            Console.WriteLine();

            ScalarProcessor.SimplificationFunc = expr => expr.TrigReduce();

            var vDt1 = v.DifferentiateScalars(t, 1);
            var vDt2 = v.DifferentiateScalars(t, 2);

            Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime}}={LaTeXComposer.GetMultivectorText(vDt1)}$");
            Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime\prime}}={LaTeXComposer.GetMultivectorText(vDt2)}$");
            Console.WriteLine();

            var vDt1NormSquared = vDt1.NormSquared();

            var sDt1 = vDt1NormSquared.Sqrt().Scalar;
            var sDt2 = sDt1.DifferentiateScalar(t);

            Console.WriteLine(@$"$s^{{\prime}}={LaTeXComposer.GetScalarText(sDt1)}$");
            Console.WriteLine(@$"$s^{{\prime\prime}}={LaTeXComposer.GetScalarText(sDt2)}$");
            Console.WriteLine();

            var vDs1 = vDt1 / sDt1;
            var vDs2 = vDs1.DifferentiateScalars(t) / sDt1;

            Console.WriteLine(@$"$\dot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs1)}$");
            Console.WriteLine(@$"$\ddot{{\boldsymbol{{v}}}}={LaTeXComposer.GetMultivectorText(vDs2)}$");
            Console.WriteLine();

            var (u1, u2) =
                vDs1.ApplyGramSchmidtByProjections(vDs2, false);

            //var vDs1NormSquared = vDs1.NormSquared();
            //var vDs12Dot = vDs1.Sp(vDs2);

            //var u1 = vDs1;
            //var u2 = vDs2 - (vDs12Dot / vDs1NormSquared) * u1;

            //u1 = u1.TrigReduceScalars();
            //u2 = u2.TrigReduceScalars();

            Console.WriteLine(@$"$\boldsymbol{{u}}_{{1}}={LaTeXComposer.GetMultivectorText(u1)}$");
            Console.WriteLine(@$"$\boldsymbol{{u}}_{{2}}={LaTeXComposer.GetMultivectorText(u2)}$");
            Console.WriteLine();

            var u1NormSquared = u1.NormSquared();
            var u2NormSquared = u2.NormSquared();

            Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{1}}\right\Vert ={LaTeXComposer.GetScalarText(u1NormSquared)}$");
            Console.WriteLine(@$"$\left\Vert \boldsymbol{{u}}_{{2}}\right\Vert ={LaTeXComposer.GetScalarText(u2NormSquared)}$");
            Console.WriteLine();

            //var e1 = u1;
            //var e2 = u2 / u2Norm;

            var kappa1 = u2NormSquared.Sqrt();

            Console.WriteLine(@$"$\kappa_{{1}}={LaTeXComposer.GetScalarText(kappa1)}$");
            Console.WriteLine();

            var omega1 = sDt1 * kappa1 * u1.Op(u2);

            Console.WriteLine(@$"$\boldsymbol{{\Omega}}_{{1}}={LaTeXComposer.GetMultivectorText(omega1)}$");
            Console.WriteLine();

            //var tMin = ScalarProcessor.CreateScalarZero();
            //var tMax = "2 * Pi".ToExpr() / w;
            //var tRange = tMax - tMin;

            //var omega1Mean = omega1.MapScalars(s =>
            //    s.IntegrateScalar(t, tMin, tMax)
            //) / tRange;

            //var omega1MeanNorm = omega1Mean.Norm();

            //Console.WriteLine(@$"$\overline{{\boldsymbol{{\Omega}}}}_{{1}}={LaTeXComposer.GetMultivectorText(omega1Mean)}$");
            //Console.WriteLine(@$"$\left\Vert \overline{{\boldsymbol{{\Omega}}}}_{{1}}\right\Vert ={LaTeXComposer.GetScalarText(omega1MeanNorm)}$");
            //Console.WriteLine();
        }
    }
}