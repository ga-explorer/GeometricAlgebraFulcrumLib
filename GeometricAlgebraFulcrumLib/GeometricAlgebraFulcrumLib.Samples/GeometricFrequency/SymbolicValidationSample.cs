using System;
using System.Diagnostics;
using System.IO;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using OxyPlot;
using OxyPlot.Series;
using Wolfram.NETLink;
// ReSharper disable InconsistentNaming

namespace GeometricAlgebraFulcrumLib.Samples.GeometricFrequency
{
    public static class SymbolicValidationSample
    {
        private const string WorkingPath 
            = @"D:\Projects\Books\The Geometric Algebra Cookbook\Geometric Frequency\Data";


        // This is a pre-defined scalar processor for symbolic
        // Wolfram Mathematica scalars using Expr objects
        public static ScalarAlgebraMathematicaProcessor ScalarProcessor { get; }
            = ScalarAlgebraMathematicaProcessor.DefaultProcessor;

        public static uint VSpaceDimension => 3;

        // Create a 6-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        public static GeometricAlgebraEuclideanProcessor<Expr> GeometricProcessor { get; } 
            = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(VSpaceDimension);

        // This is a pre-defined text generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static TextMathematicaComposer TextComposer { get; }
            = TextMathematicaComposer.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static LaTeXMathematicaComposer LaTeXComposer { get; }
            = LaTeXMathematicaComposer.DefaultComposer;


        /// <summary>
        /// Simple harmonic curve in 3D
        /// </summary>
        /// <returns></returns>
        private static GaVector<Expr> DefineCurve1(Expr tExpr)
        {
            var t = tExpr.CreateScalar(GeometricProcessor);

            var a1 = GeometricProcessor.CreateVector(1, 2, 3);
            var a2 = GeometricProcessor.CreateVector(-1, -1, 0);
            var a3 = GeometricProcessor.CreateVector(0, 1, 1);

            var b1 = GeometricProcessor.CreateVector(-2, 1, 1);
            var b2 = GeometricProcessor.CreateVector(-1, 0, 0);
            var b3 = GeometricProcessor.CreateVector(1, 2, 2);

            return 
                a1 * t.Cos() + b1 * t.Sin() +
                a2 * (2 * t).Cos() + b2 * (2 * t).Sin() +
                a3 * (3 * t).Cos() + b3 * (3 * t).Sin();
        }

        /// <summary>
        /// Simple harmonic curve in 4D
        /// </summary>
        /// <returns></returns>
        private static GaVector<Expr> DefineCurve2(Expr tExpr)
        {
            var t = tExpr.CreateScalar(GeometricProcessor);

            var a1 = GeometricProcessor.CreateVector(1, 2, 3, 4);
            var a2 = GeometricProcessor.CreateVector(-1, -1, 0, 1);
            var a3 = GeometricProcessor.CreateVector(0, 1, 1, 2);

            var b1 = GeometricProcessor.CreateVector(-2, 1, 1, 2);
            var b2 = GeometricProcessor.CreateVector(-1, 0, 0, -1);
            var b3 = GeometricProcessor.CreateVector(1, 2, 2, -2);

            return
                a1 * t.Cos() + b1 * t.Sin() +
                a2 * (2 * t).Cos() + b2 * (2 * t).Sin() +
                a3 * (3 * t).Cos() + b3 * (3 * t).Sin();
        }
        
        /// <summary>
        /// Simple Bezier curve in 3D
        /// </summary>
        /// <param name="tExpr"></param>
        /// <returns></returns>
        private static GaVector<Expr> DefineCurve3(Expr tExpr)
        {
            var a0 = GeometricProcessor.CreateVector(1, 2, -1);
            var a1 = GeometricProcessor.CreateVector(-1, -1, 2);
            var a2 = GeometricProcessor.CreateVector(-2, 1, 1);
            var a3 = GeometricProcessor.CreateVector(-2, 1, 2);
            //var a4 = GeometricProcessor.CreateVector(-1, 2, 1);
            //var a5 = GeometricProcessor.CreateVector(1, 2, 2);

            var basis = BernsteinBasisSet<Expr>.Create(
                ScalarProcessor,
                3
            );

            return basis.GetValue(tExpr, a0, a1, a2, a3);
        }

        /// <summary>
        /// Simple Bezier curve in 4D
        /// </summary>
        /// <param name="tExpr"></param>
        /// <returns></returns>
        private static GaVector<Expr> DefineCurve4(Expr tExpr)
        {
            var a0 = GeometricProcessor.CreateVector(1, 2, -1, 1);
            var a1 = GeometricProcessor.CreateVector(-1, -1, 2, 1);
            var a2 = GeometricProcessor.CreateVector(-2, 1, 1, 2);
            var a3 = GeometricProcessor.CreateVector(-2, 1, 2, -1);
            var a4 = GeometricProcessor.CreateVector(-1, 2, 1, -1);
            //var a5 = GeometricProcessor.CreateVector(1, 2, 2, -2);

            var basis = BernsteinBasisSet<Expr>.Create(
                ScalarProcessor,
                4
            );

            return basis.GetValue(tExpr, a0, a1, a2, a3, a4);
        }

        /// <summary>
        /// Simple monomial curve in 3D
        /// </summary>
        /// <returns></returns>
        private static GaVector<Expr> DefineCurve5(Expr tExpr)
        {
            var a0 = GeometricProcessor.CreateVector(0, 0, 0);
            var a1 = GeometricProcessor.CreateVector(1, 0, 0);
            var a2 = GeometricProcessor.CreateVector(0, 1, 0);
            var a3 = GeometricProcessor.CreateVector(0, 0, 1);

            var basis = MonomialBasisSet<Expr>.Create(
                ScalarProcessor,
                3
            );

            return basis.GetValue(tExpr, a0, a1, a2, a3).SimplifyCollectScalars(tExpr);
        }

        /// <summary>
        /// Simple monomial curve in 4D
        /// </summary>
        /// <returns></returns>
        private static GaVector<Expr> DefineCurve6(Expr tExpr)
        {
            var a0 = GeometricProcessor.CreateVector(0, 0, 0, 0);
            var a1 = GeometricProcessor.CreateVector(1, 0, 0, 0);
            var a2 = GeometricProcessor.CreateVector(0, 1, 0, 0);
            var a3 = GeometricProcessor.CreateVector(0, 0, 1, 0);
            var a4 = GeometricProcessor.CreateVector(0, 0, 0, 1);

            var basis = MonomialBasisSet<Expr>.Create(
                ScalarProcessor,
                4
            );

            return basis.GetValue(tExpr, a0, a1, a2, a3, a4).SimplifyCollectScalars(tExpr);
        }
        
        /// <summary>
        /// Simple monomial curve in 6D
        /// </summary>
        /// <returns></returns>
        private static GaVector<Expr> DefineCurve7(Expr tExpr)
        {
            var aArray = new[]
            {
                GeometricProcessor.CreateVector(0, 0, 0, 0, 0, 0),
                GeometricProcessor.CreateVector(1, 0, 0, 0, 0, 0),
                GeometricProcessor.CreateVector(0, 1, 0, 0, 0, 0),
                GeometricProcessor.CreateVector(0, 0, 1, 0, 0, 0),
                GeometricProcessor.CreateVector(0, 0, 0, 1, 0, 0),
                GeometricProcessor.CreateVector(0, 0, 0, 0, 1, 0),
                GeometricProcessor.CreateVector(0, 0, 0, 0, 0, 1)
            };

            var basis = MonomialBasisSet<Expr>.Create(
                ScalarProcessor,
                aArray.Length - 1
            );

            return basis
                .GetValue(tExpr, aArray)
                .SimplifyCollectScalars(tExpr);
        }

        /// <summary>
        /// Simple elliptic curve in 3D
        /// </summary>
        /// <returns></returns>
        private static GaVector<Expr> DefineCurve8(Expr tExpr)
        {
            var t = tExpr.CreateScalar(GeometricProcessor);

            var a = GeometricProcessor.CreateVector(1, 2, 3);
            var b = GeometricProcessor.CreateVector(-2, 1, 1);

            return a * t.Cos() + b * t.Sin();
        }
        
        /// <summary>
        /// Simple elliptic curve in 4D
        /// </summary>
        /// <returns></returns>
        private static GaVector<Expr> DefineCurve9(Expr tExpr)
        {
            var t = tExpr.CreateScalar(GeometricProcessor);

            var a = GeometricProcessor.CreateVector(1, 2, 3, 4);
            var b = GeometricProcessor.CreateVector(-2, 1, 1, 2);

            return a * t.Cos() + b * t.Sin();
        }

        /// <summary>
        /// Harmonic curve in 6D
        /// </summary>
        /// <param name="tExpr"></param>
        /// <returns></returns>
        private static GaVector<Expr> DefineCurve10(Expr tExpr)
        {
            var assumeExpr = 
                $@"And[{tExpr} >= 0, {tExpr} <= 1, Element[{tExpr}, Reals]]".ToExpr();

            MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

            var scalarArray = new Expr[VSpaceDimension];

            for (var k = 0; k < VSpaceDimension; k++)
            {
                var phi = $"2 * {k} * Pi / 3";
                var angle = @$"2 * Pi * 50 * {tExpr} - {phi}";

                if (k < VSpaceDimension - 1)
                {
                    scalarArray[k] = $"Cos[{angle}] + Cos[7 * ({angle})] / 10".ToExpr();
                }
                else
                {
                    scalarArray[k] = $"Cos[{angle}] / 2 + Cos[7 * ({angle})] / 20".ToExpr();
                }
            }

            return GeometricProcessor.CreateVector(scalarArray).SimplifyScalars();
        }


        private static GaVector<Expr> DefineCurve()
        {
            var t = "t".ToSymbolExpr();
            
            var v = DefineCurve10(t);
            
            LaTeXComposer
                .ConsoleWriteLine("Curve:")
                .ConsoleWriteLine(v, @"\boldsymbol{v} \left( t \right)")
                .ConsoleWriteLine();

            PlotCurveComponents(v, Path.Combine(WorkingPath, "v"));

            return v;
        }

        private static Quad<Scalar<Expr>> GetArcLengthParameterTimeDerivatives4D(this Scalar<Expr> sDt1)
        {
            var t = "t".ToSymbolExpr();

            var sDt2 = sDt1.DifferentiateScalar(t).SimplifyCollect(t);
            var sDt3 = sDt2.DifferentiateScalar(t).SimplifyCollect(t);
            var sDt4 = sDt3.DifferentiateScalar(t).SimplifyCollect(t);

            LaTeXComposer
                .ConsoleWriteLine("Time derivatives of arc-length parameter:")
                .ConsoleWriteLine(sDt1, @"s^{\prime} \left( t \right)")
                .ConsoleWriteLine(sDt2, @"s^{\prime\prime} \left( t \right)")
                .ConsoleWriteLine(sDt3, @"s^{\prime\prime\prime} \left( t \right)")
                .ConsoleWriteLine(sDt4, @"s^{\prime\prime\prime\prime} \left( t \right)")
                .ConsoleWriteLine();

            Plot(sDt1, Path.Combine(WorkingPath, "sDt1"));
            Plot(sDt2, Path.Combine(WorkingPath, "sDt2"));
            Plot(sDt3, Path.Combine(WorkingPath, "sDt3"));
            Plot(sDt4, Path.Combine(WorkingPath, "sDt4"));

            return new Quad<Scalar<Expr>>(sDt1, sDt2, sDt3, sDt4);
        }
        
        private static Hexad<Scalar<Expr>> GetArcLengthParameterTimeDerivatives6D(this Scalar<Expr> sDt1)
        {
            var t = "t".ToSymbolExpr();

            var sDt2 = sDt1.DifferentiateScalar(t).SimplifyCollect(t);
            var sDt3 = sDt2.DifferentiateScalar(t).SimplifyCollect(t);
            var sDt4 = sDt3.DifferentiateScalar(t).SimplifyCollect(t);
            var sDt5 = sDt4.DifferentiateScalar(t).SimplifyCollect(t);
            var sDt6 = sDt5.DifferentiateScalar(t).SimplifyCollect(t);

            LaTeXComposer
                .ConsoleWriteLine("Time derivatives of arc-length parameter:")
                .ConsoleWriteLine(sDt1, @"s^{\prime} \left( t \right)")
                .ConsoleWriteLine(sDt2, @"s^{\prime\prime} \left( t \right)")
                .ConsoleWriteLine(sDt3, @"s^{\prime\prime\prime} \left( t \right)")
                .ConsoleWriteLine(sDt4, @"s^{\prime\prime\prime\prime} \left( t \right)")
                .ConsoleWriteLine(sDt5, @"s^{\prime\prime\prime\prime\prime} \left( t \right)")
                .ConsoleWriteLine(sDt6, @"s^{\prime\prime\prime\prime\prime\prime} \left( t \right)")
                .ConsoleWriteLine();

            Plot(sDt1, Path.Combine(WorkingPath, "sDt1"));
            Plot(sDt2, Path.Combine(WorkingPath, "sDt2"));
            Plot(sDt3, Path.Combine(WorkingPath, "sDt3"));
            Plot(sDt4, Path.Combine(WorkingPath, "sDt4"));
            Plot(sDt5, Path.Combine(WorkingPath, "sDt5"));
            Plot(sDt6, Path.Combine(WorkingPath, "sDt6"));

            return new Hexad<Scalar<Expr>>(sDt1, sDt2, sDt3, sDt4, sDt5, sDt6);
        }

        private static Triplet<GaVector<Expr>> GetTimeDerivatives3D(this GaVector<Expr> v)
        {
            var t = "t".ToSymbolExpr();

            var vDt1 = v.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var vDt2 = vDt1.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var vDt3 = vDt2.DifferentiateScalars(t).SimplifyCollectScalars(t);

            LaTeXComposer
                .ConsoleWriteLine("Time derivatives:")
                .ConsoleWriteLine(vDt1, @"\boldsymbol{v}^{\prime} \left( t \right)")
                .ConsoleWriteLine(vDt2, @"\boldsymbol{v}^{\prime\prime} \left( t \right)")
                .ConsoleWriteLine(vDt3, @"\boldsymbol{v}^{\prime\prime\prime} \left( t \right)")
                .ConsoleWriteLine();

            PlotCurveComponents(vDt1, Path.Combine(WorkingPath, "vDt1"));
            PlotCurveComponents(vDt2, Path.Combine(WorkingPath, "vDt2"));
            PlotCurveComponents(vDt3, Path.Combine(WorkingPath, "vDt3"));

            PlotCurveNorm(vDt1, Path.Combine(WorkingPath, "vDt1Norm"));

            return new Triplet<GaVector<Expr>>(vDt1, vDt2, vDt3);
        }

        private static Quad<GaVector<Expr>> GetTimeDerivatives4D(this GaVector<Expr> v)
        {
            var t = "t".ToSymbolExpr();

            var vDt1 = v.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var vDt2 = vDt1.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var vDt3 = vDt2.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var vDt4 = vDt3.DifferentiateScalars(t).SimplifyCollectScalars(t);

            LaTeXComposer
                .ConsoleWriteLine("Time derivatives:")
                .ConsoleWriteLine(vDt1, @"\boldsymbol{v}^{\prime} \left( t \right)")
                .ConsoleWriteLine(vDt2, @"\boldsymbol{v}^{\prime\prime} \left( t \right)")
                .ConsoleWriteLine(vDt3, @"\boldsymbol{v}^{\prime\prime\prime} \left( t \right)")
                .ConsoleWriteLine(vDt4, @"\boldsymbol{v}^{\prime\prime\prime\prime} \left( t \right)")
                .ConsoleWriteLine();

            PlotCurveComponents(vDt1, Path.Combine(WorkingPath, "vDt1"));
            PlotCurveComponents(vDt2, Path.Combine(WorkingPath, "vDt2"));
            PlotCurveComponents(vDt3, Path.Combine(WorkingPath, "vDt3"));
            PlotCurveComponents(vDt4, Path.Combine(WorkingPath, "vDt4"));

            PlotCurveNorm(vDt1, Path.Combine(WorkingPath, "vDt1Norm"));

            return new Quad<GaVector<Expr>>(vDt1, vDt2, vDt3, vDt4);
        }
        
        private static Hexad<GaVector<Expr>> GetTimeDerivatives6D(this GaVector<Expr> v)
        {
            var t = "t".ToSymbolExpr();

            var vDt1 = v.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var vDt2 = vDt1.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var vDt3 = vDt2.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var vDt4 = vDt3.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var vDt5 = vDt4.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var vDt6 = vDt5.DifferentiateScalars(t).SimplifyCollectScalars(t);

            LaTeXComposer
                .ConsoleWriteLine("Time derivatives:")
                .ConsoleWriteLine(vDt1, @"\boldsymbol{v}^{\prime} \left( t \right)")
                .ConsoleWriteLine(vDt2, @"\boldsymbol{v}^{\prime\prime} \left( t \right)")
                .ConsoleWriteLine(vDt3, @"\boldsymbol{v}^{\prime\prime\prime} \left( t \right)")
                .ConsoleWriteLine(vDt4, @"\boldsymbol{v}^{\prime\prime\prime\prime} \left( t \right)")
                .ConsoleWriteLine(vDt5, @"\boldsymbol{v}^{\prime\prime\prime\prime\prime} \left( t \right)")
                .ConsoleWriteLine(vDt6, @"\boldsymbol{v}^{\prime\prime\prime\prime\prime\prime} \left( t \right)")
                .ConsoleWriteLine();

            PlotCurveComponents(vDt1, Path.Combine(WorkingPath, "vDt1"));
            PlotCurveComponents(vDt2, Path.Combine(WorkingPath, "vDt2"));
            PlotCurveComponents(vDt3, Path.Combine(WorkingPath, "vDt3"));
            PlotCurveComponents(vDt4, Path.Combine(WorkingPath, "vDt4"));
            PlotCurveComponents(vDt5, Path.Combine(WorkingPath, "vDt5"));
            PlotCurveComponents(vDt6, Path.Combine(WorkingPath, "vDt6"));

            PlotCurveNorm(vDt1, Path.Combine(WorkingPath, "vDt1Norm"));

            return new Hexad<GaVector<Expr>>(vDt1, vDt2, vDt3, vDt4, vDt5, vDt6);
        }
        
        private static Triplet<GaVector<Expr>> GetArcLengthDerivatives3D(this GaVector<Expr> v)
        {
            var t = "t".ToSymbolExpr();

            var vDt1 = v.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var sDt1 = vDt1.Norm();
            
            var vDs1 = vDt1 / sDt1;
            var vDs2 = vDs1.DifferentiateScalars("t").SimplifyCollectScalars(t) / sDt1;
            var vDs3 = vDs2.DifferentiateScalars("t").SimplifyCollectScalars(t) / sDt1;

            LaTeXComposer
                .ConsoleWriteLine("Arc-length derivatives:")
                .ConsoleWriteLine(vDs1, @"\overset{\cdot}{\boldsymbol{v}} \left( s \right)")
                .ConsoleWriteLine(vDs2, @"\overset{\cdot\cdot}{\boldsymbol{v}} \left( s \right)")
                .ConsoleWriteLine(vDs3, @"\overset{\cdot\cdot\cdot}{\boldsymbol{v}} \left( s \right)")
                .ConsoleWriteLine();

            PlotCurveComponents(vDs1, Path.Combine(WorkingPath, "vDs1"));
            PlotCurveComponents(vDs2, Path.Combine(WorkingPath, "vDs2"));
            PlotCurveComponents(vDs3, Path.Combine(WorkingPath, "vDs3"));

            PlotCurveArcLength(vDt1, Path.Combine(WorkingPath, "s"));

            PlotCurveNorm(vDs1, Path.Combine(WorkingPath, "vDs1Norm"));

            return new Triplet<GaVector<Expr>>(vDs1, vDs2, vDs3);
        }

        private static Quad<GaVector<Expr>> GetArcLengthDerivatives4D(this GaVector<Expr> v)
        {
            var t = "t".ToSymbolExpr();

            var vDt1 = v.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var sDt1 = vDt1.Norm();
            
            var vDs1 = vDt1 / sDt1;
            var vDs2 = vDs1.DifferentiateScalars("t").SimplifyCollectScalars(t) / sDt1;
            var vDs3 = vDs2.DifferentiateScalars("t").SimplifyCollectScalars(t) / sDt1;
            var vDs4 = vDs3.DifferentiateScalars("t").SimplifyCollectScalars(t) / sDt1;

            LaTeXComposer
                .ConsoleWriteLine("Arc-length derivatives:")
                .ConsoleWriteLine(vDs1, @"\overset{\cdot}{\boldsymbol{v}} \left( s \right)")
                .ConsoleWriteLine(vDs2, @"\overset{\cdot\cdot}{\boldsymbol{v}} \left( s \right)")
                .ConsoleWriteLine(vDs3, @"\overset{\cdot\cdot\cdot}{\boldsymbol{v}} \left( s \right)")
                .ConsoleWriteLine(vDs4, @"\overset{\cdot\cdot\cdot\cdot}{\boldsymbol{v}} \left( s \right)")
                .ConsoleWriteLine();

            PlotCurveComponents(vDs1, Path.Combine(WorkingPath, "vDs1"));
            PlotCurveComponents(vDs2, Path.Combine(WorkingPath, "vDs2"));
            PlotCurveComponents(vDs3, Path.Combine(WorkingPath, "vDs3"));
            PlotCurveComponents(vDs4, Path.Combine(WorkingPath, "vDs4"));

            PlotCurveArcLength(vDt1, Path.Combine(WorkingPath, "s"));

            PlotCurveNorm(vDs1, Path.Combine(WorkingPath, "vDs1Norm"));

            return new Quad<GaVector<Expr>>(vDs1, vDs2, vDs3, vDs4);
        }
        
        private static Hexad<GaVector<Expr>> GetArcLengthDerivatives6D(this GaVector<Expr> v)
        {
            var t = "t".ToSymbolExpr();

            var vDt1 = v.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var sDt1 = vDt1.Norm();
            
            var vDs1 = vDt1 / sDt1;
            var vDs2 = vDs1.DifferentiateScalars("t").SimplifyCollectScalars(t) / sDt1;
            var vDs3 = vDs2.DifferentiateScalars("t").SimplifyCollectScalars(t) / sDt1;
            var vDs4 = vDs3.DifferentiateScalars("t").SimplifyCollectScalars(t) / sDt1;
            var vDs5 = vDs4.DifferentiateScalars("t").SimplifyCollectScalars(t) / sDt1;
            var vDs6 = vDs5.DifferentiateScalars("t").SimplifyCollectScalars(t) / sDt1;

            LaTeXComposer
                .ConsoleWriteLine("Arc-length derivatives:")
                .ConsoleWriteLine(vDs1, @"\overset{\cdot}{\boldsymbol{v}} \left( s \right)")
                .ConsoleWriteLine(vDs2, @"\overset{\cdot\cdot}{\boldsymbol{v}} \left( s \right)")
                .ConsoleWriteLine(vDs3, @"\overset{\cdot\cdot\cdot}{\boldsymbol{v}} \left( s \right)")
                .ConsoleWriteLine(vDs4, @"\overset{\cdot\cdot\cdot\cdot}{\boldsymbol{v}} \left( s \right)")
                .ConsoleWriteLine(vDs5, @"\overset{\cdot\cdot\cdot\cdot\cdot}{\boldsymbol{v}} \left( s \right)")
                .ConsoleWriteLine(vDs6, @"\overset{\cdot\cdot\cdot\cdot\cdot\cdot}{\boldsymbol{v}} \left( s \right)")
                .ConsoleWriteLine();

            PlotCurveComponents(vDs1, Path.Combine(WorkingPath, "vDs1"));
            PlotCurveComponents(vDs2, Path.Combine(WorkingPath, "vDs2"));
            PlotCurveComponents(vDs3, Path.Combine(WorkingPath, "vDs3"));
            PlotCurveComponents(vDs4, Path.Combine(WorkingPath, "vDs4"));
            PlotCurveComponents(vDs5, Path.Combine(WorkingPath, "vDs5"));
            PlotCurveComponents(vDs6, Path.Combine(WorkingPath, "vDs6"));

            PlotCurveArcLength(vDt1, Path.Combine(WorkingPath, "s"));

            PlotCurveNorm(vDs1, Path.Combine(WorkingPath, "vDs1Norm"));

            return new Hexad<GaVector<Expr>>(vDs1, vDs2, vDs3, vDs4, vDs5, vDs6);
        }
        
        private static Triplet<GaVector<Expr>> GetTimeGramSchmidtFrame3D(GaVector<Expr> vDt1, GaVector<Expr> vDt2, GaVector<Expr> vDt3)
        {
            var t = "t".ToSymbolExpr();

            var (u1t, u2t, u3t) = 
                GeometricProcessor.ApplyGramSchmidt(vDt1, vDt2, vDt3, false);
            
            u1t = u1t.SimplifyCollectScalars(t);
            u2t = u2t.SimplifyCollectScalars(t);
            u3t = u3t.SimplifyCollectScalars(t);
            
            var e1t = u1t.DivideByNorm().SimplifyCollectScalars(t);
            var e2t = u2t.DivideByNorm().SimplifyCollectScalars(t);
            var e3t = u3t.DivideByNorm().SimplifyCollectScalars(t);
            
            // Make sure frame is orthogonal
            Debug.Assert(
                e1t.Sp(e2t).IsZero() &&
                e1t.Sp(e3t).IsZero() &&
                e2t.Sp(e3t).IsZero()
            );

            //LaTeXComposer
            //    .ConsoleWriteLine("")
            //    .ConsoleWriteLine(u1t, @"\boldsymbol{u}_{1} \left( t \right)")
            //    .ConsoleWriteLine(u2t, @"\boldsymbol{u}_{2} \left( t \right)")
            //    .ConsoleWriteLine(u3t, @"\boldsymbol{u}_{3} \left( t \right)")
            //    .ConsoleWriteLine(u4t, @"\boldsymbol{u}_{4} \left( t \right)")
            //    .ConsoleWriteLine();
            
            LaTeXComposer
                .ConsoleWriteLine("Time Gram-Schmidt frame:")
                .ConsoleWriteLine(e1t, @"\boldsymbol{e}_{1} \left( t \right)")
                .ConsoleWriteLine(e2t, @"\boldsymbol{e}_{2} \left( t \right)")
                .ConsoleWriteLine(e3t, @"\boldsymbol{e}_{3} \left( t \right)")
                .ConsoleWriteLine();

            return new Triplet<GaVector<Expr>>(e1t, e2t, e3t);
        }

        private static Quad<GaVector<Expr>> GetTimeGramSchmidtFrame4D(GaVector<Expr> vDt1, GaVector<Expr> vDt2, GaVector<Expr> vDt3, GaVector<Expr> vDt4)
        {
            var t = "t".ToSymbolExpr();

            var (u1t, u2t, u3t, u4t) = 
                GeometricProcessor.ApplyGramSchmidt(vDt1, vDt2, vDt3, vDt4, false);
            
            u1t = u1t.SimplifyCollectScalars(t);
            u2t = u2t.SimplifyCollectScalars(t);
            u3t = u3t.SimplifyCollectScalars(t);
            u4t = u4t.SimplifyCollectScalars(t);
            
            var e1t = u1t.DivideByNorm().SimplifyCollectScalars(t);
            var e2t = u2t.DivideByNorm().SimplifyCollectScalars(t);
            var e3t = u3t.DivideByNorm().SimplifyCollectScalars(t);
            var e4t = u4t.DivideByNorm().SimplifyCollectScalars(t);
            
            // Make sure frame is orthogonal
            Debug.Assert(
                e1t.Sp(e2t).IsZero() &&
                e1t.Sp(e3t).IsZero() &&
                e1t.Sp(e4t).IsZero() &&
                e2t.Sp(e3t).IsZero() &&
                e2t.Sp(e4t).IsZero() &&
                e3t.Sp(e4t).IsZero()
            );

            //LaTeXComposer
            //    .ConsoleWriteLine("")
            //    .ConsoleWriteLine(u1t, @"\boldsymbol{u}_{1} \left( t \right)")
            //    .ConsoleWriteLine(u2t, @"\boldsymbol{u}_{2} \left( t \right)")
            //    .ConsoleWriteLine(u3t, @"\boldsymbol{u}_{3} \left( t \right)")
            //    .ConsoleWriteLine(u4t, @"\boldsymbol{u}_{4} \left( t \right)")
            //    .ConsoleWriteLine();
            
            LaTeXComposer
                .ConsoleWriteLine("Time Gram-Schmidt frame:")
                .ConsoleWriteLine(e1t, @"\boldsymbol{e}_{1} \left( t \right)")
                .ConsoleWriteLine(e2t, @"\boldsymbol{e}_{2} \left( t \right)")
                .ConsoleWriteLine(e3t, @"\boldsymbol{e}_{3} \left( t \right)")
                .ConsoleWriteLine(e4t, @"\boldsymbol{e}_{4} \left( t \right)")
                .ConsoleWriteLine();

            return new Quad<GaVector<Expr>>(e1t, e2t, e3t, e4t);
        }
        
        private static Hexad<GaVector<Expr>> GetTimeGramSchmidtFrame6D(GaVector<Expr> vDt1, GaVector<Expr> vDt2, GaVector<Expr> vDt3, GaVector<Expr> vDt4, GaVector<Expr> vDt5, GaVector<Expr> vDt6)
        {
            var t = "t".ToSymbolExpr();

            var (u1t, u2t, u3t, u4t, u5t, u6t) = 
                GeometricProcessor.ApplyGramSchmidt(vDt1, vDt2, vDt3, vDt4, vDt5, vDt6, false);
            
            u1t = u1t.SimplifyCollectScalars(t);
            u2t = u2t.SimplifyCollectScalars(t);
            u3t = u3t.SimplifyCollectScalars(t);
            u4t = u4t.SimplifyCollectScalars(t);
            u5t = u5t.SimplifyCollectScalars(t);
            u6t = u6t.SimplifyCollectScalars(t);
            
            var e1t = u1t.DivideByNorm().SimplifyCollectScalars(t);
            var e2t = u2t.DivideByNorm().SimplifyCollectScalars(t);
            var e3t = u3t.DivideByNorm().SimplifyCollectScalars(t);
            var e4t = u4t.DivideByNorm().SimplifyCollectScalars(t);
            var e5t = u5t.DivideByNorm().SimplifyCollectScalars(t);
            var e6t = u6t.DivideByNorm().SimplifyCollectScalars(t);
            
            // Make sure frame is orthogonal
            Debug.Assert(
                e1t.Sp(e2t).IsZero() &&
                e1t.Sp(e3t).IsZero() &&
                e1t.Sp(e4t).IsZero() &&
                e1t.Sp(e5t).IsZero() &&
                e1t.Sp(e6t).IsZero() &&
                e2t.Sp(e3t).IsZero() &&
                e2t.Sp(e4t).IsZero() &&
                e2t.Sp(e5t).IsZero() &&
                e2t.Sp(e6t).IsZero() &&
                e3t.Sp(e4t).IsZero() &&
                e3t.Sp(e5t).IsZero() &&
                e3t.Sp(e6t).IsZero() &&
                e4t.Sp(e5t).IsZero() &&
                e4t.Sp(e6t).IsZero() &&
                e5t.Sp(e6t).IsZero()
            );

            //LaTeXComposer
            //    .ConsoleWriteLine("")
            //    .ConsoleWriteLine(u1t, @"\boldsymbol{u}_{1} \left( t \right)")
            //    .ConsoleWriteLine(u2t, @"\boldsymbol{u}_{2} \left( t \right)")
            //    .ConsoleWriteLine(u3t, @"\boldsymbol{u}_{3} \left( t \right)")
            //    .ConsoleWriteLine(u4t, @"\boldsymbol{u}_{4} \left( t \right)")
            //    .ConsoleWriteLine();
            
            LaTeXComposer
                .ConsoleWriteLine("Time Gram-Schmidt frame:")
                .ConsoleWriteLine(e1t, @"\boldsymbol{e}_{1} \left( t \right)")
                .ConsoleWriteLine(e2t, @"\boldsymbol{e}_{2} \left( t \right)")
                .ConsoleWriteLine(e3t, @"\boldsymbol{e}_{3} \left( t \right)")
                .ConsoleWriteLine(e4t, @"\boldsymbol{e}_{4} \left( t \right)")
                .ConsoleWriteLine(e5t, @"\boldsymbol{e}_{5} \left( t \right)")
                .ConsoleWriteLine(e6t, @"\boldsymbol{e}_{6} \left( t \right)")
                .ConsoleWriteLine();

            return new Hexad<GaVector<Expr>>(e1t, e2t, e3t, e4t, e5t, e6t);
        }
        
        private static Triplet<GaVector<Expr>> GetArcLengthGramSchmidtFrame3D(GaVector<Expr> vDs1, GaVector<Expr> vDs2, GaVector<Expr> vDs3)
        {
            var t = "t".ToSymbolExpr();

            var (u1s, u2s, u3s) = 
                GeometricProcessor.ApplyGramSchmidtByProjections(vDs1, vDs2, vDs3, false);
            
            u1s = u1s.SimplifyCollectScalars(t);
            u2s = u2s.SimplifyCollectScalars(t);
            u3s = u3s.SimplifyCollectScalars(t);
            
            var e1s = u1s.DivideByNorm().SimplifyCollectScalars(t);
            var e2s = u2s.DivideByNorm().SimplifyCollectScalars(t);
            var e3s = u3s.DivideByNorm().SimplifyCollectScalars(t);
            
            // Make sure frame is orthogonal
            Debug.Assert(
                e1s.Sp(e2s).IsZero() &&
                e1s.Sp(e3s).IsZero() &&
                e2s.Sp(e3s).IsZero()
            );

            //LaTeXComposer
            //    .ConsoleWriteLine("")
            //    .ConsoleWriteLine(u1s, @"\boldsymbol{u}_{1} \left( s \right)")
            //    .ConsoleWriteLine(u2s, @"\boldsymbol{u}_{2} \left( s \right)")
            //    .ConsoleWriteLine(u3s, @"\boldsymbol{u}_{3} \left( s \right)")
            //    .ConsoleWriteLine(u4s, @"\boldsymbol{u}_{4} \left( s \right)")
            //    .ConsoleWriteLine();
            
            LaTeXComposer
                .ConsoleWriteLine("Arc-length Gram-Schmidt frame:")
                .ConsoleWriteLine(e1s, @"\boldsymbol{e}_{1} \left( s \right)")
                .ConsoleWriteLine(e2s, @"\boldsymbol{e}_{2} \left( s \right)")
                .ConsoleWriteLine(e3s, @"\boldsymbol{e}_{3} \left( s \right)")
                .ConsoleWriteLine();

            return new Triplet<GaVector<Expr>>(e1s, e2s, e3s);
        }
        
        private static Quad<GaVector<Expr>> GetArcLengthGramSchmidtFrame4D(GaVector<Expr> vDs1, GaVector<Expr> vDs2, GaVector<Expr> vDs3, GaVector<Expr> vDs4)
        {
            var t = "t".ToSymbolExpr();

            var (u1s, u2s, u3s, u4s) = 
                GeometricProcessor.ApplyGramSchmidtByProjections(vDs1, vDs2, vDs3, vDs4, false);
            
            u1s = u1s.SimplifyCollectScalars(t);
            u2s = u2s.SimplifyCollectScalars(t);
            u3s = u3s.SimplifyCollectScalars(t);
            u4s = u4s.SimplifyCollectScalars(t);
            
            var e1s = u1s.DivideByNorm().SimplifyCollectScalars(t);
            var e2s = u2s.DivideByNorm().SimplifyCollectScalars(t);
            var e3s = u3s.DivideByNorm().SimplifyCollectScalars(t);
            var e4s = u4s.DivideByNorm().SimplifyCollectScalars(t);
            
            // Make sure frame is orthogonal
            Debug.Assert(
                e1s.Sp(e2s).IsZero() &&
                e1s.Sp(e3s).IsZero() &&
                e1s.Sp(e4s).IsZero() &&
                e2s.Sp(e3s).IsZero() &&
                e2s.Sp(e4s).IsZero() &&
                e3s.Sp(e4s).IsZero()
            );

            //LaTeXComposer
            //    .ConsoleWriteLine("")
            //    .ConsoleWriteLine(u1s, @"\boldsymbol{u}_{1} \left( s \right)")
            //    .ConsoleWriteLine(u2s, @"\boldsymbol{u}_{2} \left( s \right)")
            //    .ConsoleWriteLine(u3s, @"\boldsymbol{u}_{3} \left( s \right)")
            //    .ConsoleWriteLine(u4s, @"\boldsymbol{u}_{4} \left( s \right)")
            //    .ConsoleWriteLine();
            
            LaTeXComposer
                .ConsoleWriteLine("Arc-length Gram-Schmidt frame:")
                .ConsoleWriteLine(e1s, @"\boldsymbol{e}_{1} \left( s \right)")
                .ConsoleWriteLine(e2s, @"\boldsymbol{e}_{2} \left( s \right)")
                .ConsoleWriteLine(e3s, @"\boldsymbol{e}_{3} \left( s \right)")
                .ConsoleWriteLine(e4s, @"\boldsymbol{e}_{4} \left( s \right)")
                .ConsoleWriteLine();

            return new Quad<GaVector<Expr>>(e1s, e2s, e3s, e4s);
        }
        
        private static Hexad<GaVector<Expr>> GetArcLengthGramSchmidtFrame6D(GaVector<Expr> vDs1, GaVector<Expr> vDs2, GaVector<Expr> vDs3, GaVector<Expr> vDs4, GaVector<Expr> vDs5, GaVector<Expr> vDs6)
        {
            var t = "t".ToSymbolExpr();

            var (u1s, u2s, u3s, u4s, u5s, u6s) = 
                GeometricProcessor.ApplyGramSchmidtByProjections(vDs1, vDs2, vDs3, vDs4, vDs5, vDs6, false);
            
            u1s = u1s.SimplifyCollectScalars(t);
            u2s = u2s.SimplifyCollectScalars(t);
            u3s = u3s.SimplifyCollectScalars(t);
            u4s = u4s.SimplifyCollectScalars(t);
            u5s = u5s.SimplifyCollectScalars(t);
            u6s = u6s.SimplifyCollectScalars(t);
            
            var e1s = u1s.DivideByNorm().SimplifyCollectScalars(t);
            var e2s = u2s.DivideByNorm().SimplifyCollectScalars(t);
            var e3s = u3s.DivideByNorm().SimplifyCollectScalars(t);
            var e4s = u4s.DivideByNorm().SimplifyCollectScalars(t);
            var e5s = u5s.DivideByNorm().SimplifyCollectScalars(t);
            var e6s = u6s.DivideByNorm().SimplifyCollectScalars(t);
            
            // Make sure frame is orthogonal
            Debug.Assert(
                e1s.Sp(e2s).IsZero() &&
                e1s.Sp(e3s).IsZero() &&
                e1s.Sp(e4s).IsZero() &&
                e1s.Sp(e5s).IsZero() &&
                e1s.Sp(e6s).IsZero() &&
                e2s.Sp(e3s).IsZero() &&
                e2s.Sp(e4s).IsZero() &&
                e2s.Sp(e5s).IsZero() &&
                e2s.Sp(e6s).IsZero() &&
                e3s.Sp(e4s).IsZero() &&
                e3s.Sp(e5s).IsZero() &&
                e3s.Sp(e6s).IsZero() &&
                e4s.Sp(e5s).IsZero() &&
                e4s.Sp(e6s).IsZero() &&
                e5s.Sp(e6s).IsZero()
            );

            //LaTeXComposer
            //    .ConsoleWriteLine("")
            //    .ConsoleWriteLine(u1s, @"\boldsymbol{u}_{1} \left( s \right)")
            //    .ConsoleWriteLine(u2s, @"\boldsymbol{u}_{2} \left( s \right)")
            //    .ConsoleWriteLine(u3s, @"\boldsymbol{u}_{3} \left( s \right)")
            //    .ConsoleWriteLine(u4s, @"\boldsymbol{u}_{4} \left( s \right)")
            //    .ConsoleWriteLine();
            
            LaTeXComposer
                .ConsoleWriteLine("Arc-length Gram-Schmidt frame:")
                .ConsoleWriteLine(e1s, @"\boldsymbol{e}_{1} \left( s \right)")
                .ConsoleWriteLine(e2s, @"\boldsymbol{e}_{2} \left( s \right)")
                .ConsoleWriteLine(e3s, @"\boldsymbol{e}_{3} \left( s \right)")
                .ConsoleWriteLine(e4s, @"\boldsymbol{e}_{4} \left( s \right)")
                .ConsoleWriteLine(e5s, @"\boldsymbol{e}_{5} \left( s \right)")
                .ConsoleWriteLine(e6s, @"\boldsymbol{e}_{6} \left( s \right)")
                .ConsoleWriteLine();

            return new Hexad<GaVector<Expr>>(e1s, e2s, e3s, e4s, e5s, e6s);
        }
        
        private static Triplet<GaVector<Expr>> GetFrameTimeDerivative3D(GaVector<Expr> e1t, GaVector<Expr> e2t, GaVector<Expr> e3t)
        {
            var t = "t".ToSymbolExpr();

            var e1tDt = e1t.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var e2tDt = e2t.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var e3tDt = e3t.DifferentiateScalars(t).SimplifyCollectScalars(t);

            LaTeXComposer
                .ConsoleWriteLine("Time derivatives of Gram-Schmidt time frame:")
                .ConsoleWriteLine(e1tDt, @"\boldsymbol{e}^{\prime}_{1}\left(t\right)")
                .ConsoleWriteLine(e2tDt, @"\boldsymbol{e}^{\prime}_{2}\left(t\right)")
                .ConsoleWriteLine(e3tDt, @"\boldsymbol{e}^{\prime}_{3}\left(t\right)")
                .ConsoleWriteLine();

            return new Triplet<GaVector<Expr>>(e1tDt, e2tDt, e3tDt);
        }

        private static Quad<GaVector<Expr>> GetFrameTimeDerivative4D(GaVector<Expr> e1t, GaVector<Expr> e2t, GaVector<Expr> e3t, GaVector<Expr> e4t)
        {
            var t = "t".ToSymbolExpr();

            var e1tDt = e1t.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var e2tDt = e2t.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var e3tDt = e3t.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var e4tDt = e4t.DifferentiateScalars(t).SimplifyCollectScalars(t);

            LaTeXComposer
                .ConsoleWriteLine("Time derivatives of Gram-Schmidt time frame:")
                .ConsoleWriteLine(e1tDt, @"\boldsymbol{e}^{\prime}_{1}\left(t\right)")
                .ConsoleWriteLine(e2tDt, @"\boldsymbol{e}^{\prime}_{2}\left(t\right)")
                .ConsoleWriteLine(e3tDt, @"\boldsymbol{e}^{\prime}_{3}\left(t\right)")
                .ConsoleWriteLine(e4tDt, @"\boldsymbol{e}^{\prime}_{4}\left(t\right)")
                .ConsoleWriteLine();

            return new Quad<GaVector<Expr>>(e1tDt, e2tDt, e3tDt, e4tDt);
        }
        
        private static Hexad<GaVector<Expr>> GetFrameTimeDerivative6D(GaVector<Expr> e1t, GaVector<Expr> e2t, GaVector<Expr> e3t, GaVector<Expr> e4t, GaVector<Expr> e5t, GaVector<Expr> e6t)
        {
            var t = "t".ToSymbolExpr();

            var e1tDt = e1t.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var e2tDt = e2t.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var e3tDt = e3t.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var e4tDt = e4t.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var e5tDt = e5t.DifferentiateScalars(t).SimplifyCollectScalars(t);
            var e6tDt = e6t.DifferentiateScalars(t).SimplifyCollectScalars(t);

            LaTeXComposer
                .ConsoleWriteLine("Time derivatives of Gram-Schmidt time frame:")
                .ConsoleWriteLine(e1tDt, @"\boldsymbol{e}^{\prime}_{1}\left(t\right)")
                .ConsoleWriteLine(e2tDt, @"\boldsymbol{e}^{\prime}_{2}\left(t\right)")
                .ConsoleWriteLine(e3tDt, @"\boldsymbol{e}^{\prime}_{3}\left(t\right)")
                .ConsoleWriteLine(e4tDt, @"\boldsymbol{e}^{\prime}_{4}\left(t\right)")
                .ConsoleWriteLine(e5tDt, @"\boldsymbol{e}^{\prime}_{5}\left(t\right)")
                .ConsoleWriteLine(e6tDt, @"\boldsymbol{e}^{\prime}_{6}\left(t\right)")
                .ConsoleWriteLine();

            return new Hexad<GaVector<Expr>>(e1tDt, e2tDt, e3tDt, e4tDt, e5tDt, e6tDt);
        }
        
        private static Triplet<GaVector<Expr>> GetFrameArcLengthDerivative3D(GaVector<Expr> e1s, GaVector<Expr> e2s, GaVector<Expr> e3s, Expr sDt)
        {
            var t = "t".ToSymbolExpr();

            var e1sDs = e1s.DifferentiateScalars(t).SimplifyCollectScalars(t) / sDt;
            var e2sDs = e2s.DifferentiateScalars(t).SimplifyCollectScalars(t) / sDt;
            var e3sDs = e3s.DifferentiateScalars(t).SimplifyCollectScalars(t) / sDt;

            LaTeXComposer
                .ConsoleWriteLine("Arc-length derivatives of Gram-Schmidt arc-length frame:")
                .ConsoleWriteLine(e1sDs, @"\dot{\boldsymbol{e}}_{1}\left(s\right)")
                .ConsoleWriteLine(e2sDs, @"\dot{\boldsymbol{e}}_{2}\left(s\right)")
                .ConsoleWriteLine(e3sDs, @"\dot{\boldsymbol{e}}_{3}\left(s\right)")
                .ConsoleWriteLine();

            return new Triplet<GaVector<Expr>>(e1sDs, e2sDs, e3sDs);
        }

        private static Quad<GaVector<Expr>> GetFrameArcLengthDerivative4D(GaVector<Expr> e1s, GaVector<Expr> e2s, GaVector<Expr> e3s, GaVector<Expr> e4s, Expr sDt)
        {
            var t = "t".ToSymbolExpr();

            var e1sDs = e1s.DifferentiateScalars(t).SimplifyCollectScalars(t) / sDt;
            var e2sDs = e2s.DifferentiateScalars(t).SimplifyCollectScalars(t) / sDt;
            var e3sDs = e3s.DifferentiateScalars(t).SimplifyCollectScalars(t) / sDt;
            var e4sDs = e4s.DifferentiateScalars(t).SimplifyCollectScalars(t) / sDt;

            LaTeXComposer
                .ConsoleWriteLine("Arc-length derivatives of Gram-Schmidt arc-length frame:")
                .ConsoleWriteLine(e1sDs, @"\dot{\boldsymbol{e}}_{1}\left(s\right)")
                .ConsoleWriteLine(e2sDs, @"\dot{\boldsymbol{e}}_{2}\left(s\right)")
                .ConsoleWriteLine(e3sDs, @"\dot{\boldsymbol{e}}_{3}\left(s\right)")
                .ConsoleWriteLine(e4sDs, @"\dot{\boldsymbol{e}}_{4}\left(s\right)")
                .ConsoleWriteLine();

            return new Quad<GaVector<Expr>>(e1sDs, e2sDs, e3sDs, e4sDs);
        }
        
        private static Hexad<GaVector<Expr>> GetFrameArcLengthDerivative6D(GaVector<Expr> e1s, GaVector<Expr> e2s, GaVector<Expr> e3s, GaVector<Expr> e4s, GaVector<Expr> e5s, GaVector<Expr> e6s, Expr sDt)
        {
            var t = "t".ToSymbolExpr();

            var e1sDs = e1s.DifferentiateScalars(t).SimplifyCollectScalars(t) / sDt;
            var e2sDs = e2s.DifferentiateScalars(t).SimplifyCollectScalars(t) / sDt;
            var e3sDs = e3s.DifferentiateScalars(t).SimplifyCollectScalars(t) / sDt;
            var e4sDs = e4s.DifferentiateScalars(t).SimplifyCollectScalars(t) / sDt;
            var e5sDs = e5s.DifferentiateScalars(t).SimplifyCollectScalars(t) / sDt;
            var e6sDs = e6s.DifferentiateScalars(t).SimplifyCollectScalars(t) / sDt;

            LaTeXComposer
                .ConsoleWriteLine("Arc-length derivatives of Gram-Schmidt arc-length frame:")
                .ConsoleWriteLine(e1sDs, @"\dot{\boldsymbol{e}}_{1}\left(s\right)")
                .ConsoleWriteLine(e2sDs, @"\dot{\boldsymbol{e}}_{2}\left(s\right)")
                .ConsoleWriteLine(e3sDs, @"\dot{\boldsymbol{e}}_{3}\left(s\right)")
                .ConsoleWriteLine(e4sDs, @"\dot{\boldsymbol{e}}_{4}\left(s\right)")
                .ConsoleWriteLine(e5sDs, @"\dot{\boldsymbol{e}}_{5}\left(s\right)")
                .ConsoleWriteLine(e6sDs, @"\dot{\boldsymbol{e}}_{6}\left(s\right)")
                .ConsoleWriteLine();

            return new Hexad<GaVector<Expr>>(e1sDs, e2sDs, e3sDs, e4sDs, e5sDs, e6sDs);
        }


        private static void PlotCurveArcLength(GaVector<Expr> curve, string filePath)
        {
            var t = "t".ToSymbolExpr();
            var x = "x".ToSymbolExpr();

            var sDt = curve.Norm().ScalarValue.ReplaceAll(t, x);

            var pm = new PlotModel
            {
                Title = "s(t)",
                Background = OxyColor.FromRgb(255, 255, 255)
            };

            var s1 = new FunctionSeries(
                v => Mfs.NIntegrate[sDt, Mfs.List[x, Expr.INT_ZERO, v.ToExpr()]].Evaluate().AsDouble(),
                0,
                1,
                512
            );

            pm.Series.Add(s1);

            OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
            //OxyPlot.SkiaSharp.PngExporter.Export(pm, filePath + "png", 1024, 768, 300);
        }
        
        private static void Plot(Expr curve, string filePath)
        {
            var t = "t".ToSymbolExpr();

            var curveNorm = curve;

            var pm = new PlotModel
            {
                //Title = $"|| v(t) ||",
                Background = OxyColor.FromRgb(255, 255, 255)
            };

            var s1 = new FunctionSeries(
                v => curveNorm.ReplaceAll(t, v.ToExpr()).Evaluate().AsDouble(),
                0,
                1,
                512
            );

            pm.Series.Add(s1);
            
            OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
            //OxyPlot.SkiaSharp.PngExporter.Export(pm, filePath + "png", 1024, 768, 300);
        }

        private static void PlotCurveNorm(GaVector<Expr> curve, string filePath)
        {
            var t = "t".ToSymbolExpr();

            var curveNorm = curve.Norm().ScalarValue;

            var pm = new PlotModel
            {
                //Title = $"|| v(t) ||",
                Background = OxyColor.FromRgb(255, 255, 255)
            };

            var s1 = new FunctionSeries(
                v => curveNorm.ReplaceAll(t, v.ToExpr()).Evaluate().AsDouble(),
                0,
                1,
                512
            );

            pm.Series.Add(s1);
            
            OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
            //OxyPlot.SkiaSharp.PngExporter.Export(pm, filePath + "png", 1024, 768, 300);
        }

        private static void PlotCurveComponents(GaVector<Expr> curve, string filePath)
        {
            var t = "t".ToSymbolExpr();

            var pm = new PlotModel
            {
                //Title = $"|| v(t) ||",
                Background = OxyColor.FromRgb(255, 255, 255)
            };

            for (var i = 0; i < GeometricProcessor.VSpaceDimension; i++)
            {
                var index = i;

                var s1 = new FunctionSeries(
                    v => curve[index].ScalarValue.ReplaceAll(t, v.ToExpr()).Evaluate().AsDouble(),
                    0.001,
                    1,
                    512
                );

                pm.Series.Add(s1);
            }
            
            OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
            //OxyPlot.SkiaSharp.PngExporter.Export(pm, filePath + "png", 1024, 768, 300);
        }
        
        
        private static void ValidateEqual(Expr v1, Expr v2, string v1Text, string v2Text)
        {
            var t = "t".ToExpr();

            var u1 = Mfs.Collect[v1, t].Evaluate();
            var u2 = Mfs.Collect[v2, t].Evaluate();
            var uDiff = Mfs.Collect[Mfs.Subtract[u1, u2].FullSimplify(), t].Evaluate();

            if (!uDiff.IsZero())
            {
                Console.WriteLine();
                Console.WriteLine("Invalid Equality:");
                Console.WriteLine(@$"${v1Text} = {LaTeXComposer.GetScalarText(u1)}$");
                Console.WriteLine(@$"${v2Text} = {LaTeXComposer.GetScalarText(u2)}$");
            }

            Console.WriteLine(@$"$\left( {v1Text} \right) - \left( {v2Text} \right) = {LaTeXComposer.GetScalarText(uDiff)}$");
            Console.WriteLine();
        }
        
        private static void ValidateEqual(GaVector<Expr> v1, GaVector<Expr> v2, string v1Text, string v2Text)
        {
            var t = "t".ToExpr();

            var u1 = v1.MapScalars(s => s.Collect(t));
            var u2 = v2.MapScalars(s => s.Collect(t));
            var uDiff = (u1 - u2).MapScalars(s => s.FullSimplify().Collect(t));

            if (!uDiff.IsNearZero())
            {
                Console.WriteLine();
                Console.WriteLine("Invalid Equality:");
                Console.WriteLine(@$"${v1Text} = {LaTeXComposer.GetMultivectorText(u1)}$");
                Console.WriteLine(@$"${v2Text} = {LaTeXComposer.GetMultivectorText(u2)}$");
            }

            Console.WriteLine(@$"$\left( {v1Text} \right) - \left( {v2Text} \right) = {LaTeXComposer.GetMultivectorText(uDiff)}$");
            Console.WriteLine();
        }
        
        private static void ValidateEqual(GaMultivector<Expr> v1, GaMultivector<Expr> v2, string v1Text, string v2Text)
        {
            var t = "t".ToExpr();

            var u1 = v1.MapScalars(s => s.Collect(t));
            var u2 = v2.MapScalars(s => s.Collect(t));
            var uDiff = (u1 - u2).MapScalars(s => s.FullSimplify().Collect(t));

            if (!uDiff.IsNearZero())
            {
                Console.WriteLine();
                Console.WriteLine("Invalid Equality:");
                Console.WriteLine(@$"${v1Text} = {LaTeXComposer.GetMultivectorText(u1)}$");
                Console.WriteLine(@$"${v2Text} = {LaTeXComposer.GetMultivectorText(u2)}$");
            }

            Console.WriteLine(@$"$\left( {v1Text} \right) - \left( {v2Text} \right) = {LaTeXComposer.GetMultivectorText(uDiff)}$");
            Console.WriteLine();
        }

        private static void ValidateOrthogonal(GaVector<Expr> v1, GaVector<Expr> v2, string v1Text, string v2Text)
        {
            var t = "t".ToExpr();

            var u1 = v1.MapScalars(s => s.Collect(t));
            var u2 = v2.MapScalars(s => s.Collect(t));
            var uDot = u1.Sp(u2).FullSimplify().Collect(t);

            if (!uDot.IsNearZero())
            {
                Console.WriteLine();
                Console.WriteLine("Invalid Orthogonality:");
                Console.WriteLine(@$"${v1Text} = {LaTeXComposer.GetMultivectorText(u1)}$");
                Console.WriteLine(@$"${v2Text} = {LaTeXComposer.GetMultivectorText(u2)}$");
            }

            Console.WriteLine(@$"$\left( {v1Text} \right) \cdot \left( {v2Text} \right) = {LaTeXComposer.GetScalarText(uDot)}$");
            Console.WriteLine();
        }
        
        
        public static void Example3D()
        {
            LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

            //var assumeExpr = @"And[t >= 0, t <= 1, Element[{t}, Reals]]".ToExpr();
            
            //MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

            // Symbolic time variable
            var t = "t".ToExpr();

            // Curve
            var v = 
                DefineCurve();

            // Time derivatives of curve
            var (vDt1, vDt2, vDt3) = 
                v.GetTimeDerivatives3D();
            
            // Arc-length derivatives of curve
            var (vDs1, vDs2, vDs3) = 
                v.GetArcLengthDerivatives3D();

            // Make sure vDs1 is a unit vector, and orthogonal to vDs2
            Debug.Assert((vDs1.NormSquared() - 1).IsZero());
            Debug.Assert(vDs1.Sp(vDs2).IsZero());

            var vDt1NormSquared = vDt1.NormSquared().SimplifyCollect(t);
            var vDt1Norm = vDt1NormSquared.Sqrt();

            var vDt2NormSquared = vDt2.NormSquared().SimplifyCollect(t);
            var vDt2Norm = vDt2NormSquared.Sqrt();
            var vDt1Dt2Dot = vDt1.Sp(vDt2).SimplifyCollect(t);
            
            LaTeXComposer
                .ConsoleWriteLine(vDt1Norm, @"\left\Vert \boldsymbol{v}^{\prime}\right\Vert = s^{\prime}")
                .ConsoleWriteLine(vDt2Norm, @"\left\Vert \boldsymbol{v}^{\prime\prime}\right\Vert")
                .ConsoleWriteLine()
                .ConsoleWriteLine(vDt1NormSquared, @"\left\Vert \boldsymbol{v}^{\prime}\right\Vert ^{2}")
                .ConsoleWriteLine(vDt2NormSquared, @"\left\Vert \boldsymbol{v}^{\prime\prime}\right\Vert ^{2}")
                .ConsoleWriteLine(vDt1Dt2Dot, @"\boldsymbol{v}^{\prime}\cdot\boldsymbol{v}^{\prime\prime}")
                .ConsoleWriteLine();

            var vDs1NormSquared = vDs1.NormSquared().SimplifyCollect(t);
            var vDs1Norm = vDs1NormSquared.Sqrt();

            var vDs2NormSquared = vDs2.NormSquared().SimplifyCollect(t);
            var vDs2Norm = vDs2NormSquared.Sqrt();

            var vDs3NormSquared = vDs3.NormSquared().SimplifyCollect(t);
            var vDs3Norm = vDs3NormSquared.Sqrt();
            
            LaTeXComposer
                .ConsoleWriteLine(vDs1Norm, @"\left\Vert \dot{\boldsymbol{v}}\left(s\right) \right\Vert")
                .ConsoleWriteLine(vDs2Norm, @"\left\Vert \ddot{\boldsymbol{v}}\left(s\right) \right\Vert")
                .ConsoleWriteLine()
                .ConsoleWriteLine(vDs1NormSquared, @"\left\Vert \dot{\boldsymbol{v}}\left(s\right) \right\Vert ^{2}")
                .ConsoleWriteLine(vDs2NormSquared, @"\left\Vert \ddot{\boldsymbol{v}}\left(s\right) \right\Vert ^{2}")
                .ConsoleWriteLine();
            
            // Time derivatives of arc-length parameter
            var (sDt1, sDt2, sDt3, sDt4, sDt5, sDt6) = 
                vDt1Norm.GetArcLengthParameterTimeDerivatives6D();
            
            // Validate the general expressions for sDt1, sDt2, sDt3, sDt4
            var sDt1_1 = vDt1.Sp(vDt1).Sqrt();
            var sDt2_1 = vDt1.Sp(vDt2) / sDt1_1;
            var sDt3_1 = (vDt2.Sp(vDt2) + vDt1.Sp(vDt3) - sDt2_1.Square()) / sDt1_1;

            sDt1_1 = sDt1_1.FullSimplify().Collect(t);
            sDt2_1 = sDt2_1.FullSimplify().Collect(t);
            sDt3_1 = sDt3_1.FullSimplify().Collect(t);
            
            Debug.Assert((sDt1_1 - sDt1).FullSimplify().IsZero());
            Debug.Assert((sDt2_1 - sDt2).FullSimplify().IsZero());
            Debug.Assert((sDt3_1 - sDt3).FullSimplify().IsZero());

            // Validate the general expression for norm of vDs2
            var vDs2Norm_1 = 
                (vDt2NormSquared - 2 * (sDt2 / sDt1) * vDt1Dt2Dot + sDt2.Square()).Sqrt() / sDt1.Square();

            Debug.Assert((vDs2Norm_1 - vDs2Norm).FullSimplify().IsZero());

            // Validate the general expression for vDs1, vDs2, vDs3, ...
            var vDs1_1 = vDt1 / sDt1;
            var vDs2_1 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
            var vDs3_1 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Power(2) - sDt1 * sDt3) * vDt1) / sDt1.Power(5);

            Debug.Assert((vDs1_1 - vDs1).FullSimplifyScalars().IsZero());
            Debug.Assert((vDs2_1 - vDs2).FullSimplifyScalars().IsZero());
            Debug.Assert((vDs3_1 - vDs3).FullSimplifyScalars().IsZero());


            // Gram-Schmidt frame and its derivative of time parametrization of curve
            var (e1t, e2t, e3t) = 
                GetTimeGramSchmidtFrame3D(vDt1, vDt2, vDt3);

            var (e1tDt, e2tDt, e3tDt) = 
                GetFrameTimeDerivative3D(e1t, e2t, e3t);

            // Gram-Schmidt frame and its derivative of arc-length parametrization of curve
            var (u1s, u2s, u3s) = 
                GeometricProcessor.ApplyGramSchmidtByProjections(vDs1, vDs2, vDs3, false);
            
            var u1sNorm = u1s.Norm().SimplifyCollect(t);
            var u2sNorm = u2s.Norm().SimplifyCollect(t);
            var u3sNorm = u3s.Norm().SimplifyCollect(t);

            var (e1s, e2s, e3s) = 
                GetArcLengthGramSchmidtFrame3D(vDs1, vDs2, vDs3);
            
            var (e1sDs, e2sDs, e3sDs) = 
                GetFrameArcLengthDerivative3D(e1s, e2s, e3s, sDt1);

            // Darboux bivector
            var omega = 
                (e1sDs.Op(e1s) + e2sDs.Op(e2s) + e3sDs.Op(e3s)) / 2;

            // Angular velocity blade
            var omegaBar = 
                vDs2.Op(vDs1).SimplifyCollectScalars(t);

            // Bivector B = omega - omegaBar
            var bBivector = 
                (omega - omegaBar).SimplifyCollectScalars(t);

            // Validate vDs1 is orthogonal to Bivector B
            var vDs1DotB = 
                vDs1.Lcp(bBivector).SimplifyCollectScalars(t);

            Debug.Assert(vDs1DotB.IsZero());

            //ValidateEqual(
            //    e1sDs,
            //    -e1s.Lcp(omega),
            //    @"\dot{\boldsymbol{e}}_{1}",
            //    @"\boldsymbol{e}_{1}\lfloor\boldsymbol{\Omega}=-\boldsymbol{e}_{1}\rfloor\boldsymbol{\Omega}"
            //);

            Debug.Assert((e1sDs - -e1s.Lcp(omegaBar)).FullSimplifyScalars().CollectScalars(t).IsZero());
            Debug.Assert((e1sDs - -e1s.Lcp(omega)).FullSimplifyScalars().CollectScalars(t).IsZero());
            Debug.Assert((e2sDs - -e2s.Lcp(omega)).FullSimplifyScalars().CollectScalars(t).IsZero());
            Debug.Assert((e3sDs - -e3s.Lcp(omega)).FullSimplifyScalars().CollectScalars(t).IsZero());

            //ValidateEqual(
            //    e1sDs,
            //    -e1s.Lcp(omega1),
            //    @"\dot{\boldsymbol{e}}_{1}",
            //    @"\boldsymbol{e}_{1}\lfloor\bar{\boldsymbol{\Omega}} = -\boldsymbol{e}_{1}\rfloor \bar{\boldsymbol{\Omega}}"
            //);
            
            //ValidateEqual(
            //    e2sDs,
            //    -e2s.Lcp(omega1),
            //    @"\dot{\boldsymbol{e}}_{2}",
            //    @"\boldsymbol{e}_{2}\lfloor\bar{\boldsymbol{\Omega}} = -\boldsymbol{e}_{2}\rfloor \bar{\boldsymbol{\Omega}}"
            //);
            
            //ValidateEqual(
            //    e3sDs,
            //    -e3s.Lcp(omega1),
            //    @"\dot{\boldsymbol{e}}_{3}",
            //    @"\boldsymbol{e}_{3}\lfloor\bar{\boldsymbol{\Omega}} = -\boldsymbol{e}_{3}\rfloor \bar{\boldsymbol{\Omega}}"
            //);

            LaTeXComposer
                .ConsoleWriteLine("Darboux bivector:")
                .ConsoleWriteLine(omega, @"\boldsymbol{\Omega}")
                .ConsoleWriteLine(omegaBar, @"\bar{\boldsymbol{\Omega}}")
                .ConsoleWriteLine(bBivector, @"\boldsymbol{B}")
                .ConsoleWriteLine(vDs1DotB, @"\dot{\boldsymbol{v}}\rfloor\boldsymbol{B}")
                .ConsoleWriteLine();

            // Curvature parameters based on parametrization of curve
            var kappa1 = Mfs.ExpandAll[e1sDs.Sp(e2s).ScalarValue].FullSimplify();
            var kappa2 = Mfs.ExpandAll[e2sDs.Sp(e3s).ScalarValue].FullSimplify();

            LaTeXComposer
                .ConsoleWriteLine("Curvature coefficients:")
                .ConsoleWriteLine(kappa1, @"\kappa_{1}")
                .ConsoleWriteLine(kappa2, @"\kappa_{2}")
                .ConsoleWriteLine();

            var scalarZero = Expr.INT_ZERO.CreateScalar(GeometricProcessor);

            var kappa1_1 = u2sNorm.IsZero() ? scalarZero : u2sNorm / u1sNorm;
            var kappa2_1 = u3sNorm.IsZero() ? scalarZero : u3sNorm / u2sNorm;

            LaTeXComposer
                .ConsoleWriteLine(kappa1_1, @"\kappa_{1}")
                .ConsoleWriteLine(kappa2_1, @"\kappa_{2}")
                .ConsoleWriteLine();


            // Make sure the first curvature is equal to the norm of vDs2
            Debug.Assert((kappa1 - kappa1_1).FullSimplify().IsZero());
            Debug.Assert((kappa2 - kappa2_1).FullSimplify().IsZero());

            var omega2 =
                kappa1 * e2s.Op(e1s) +
                kappa2 * e3s.Op(e2s);

            Debug.Assert((omega - omega2).IsZero());
        }

        public static void Example4D()
        {
            LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

            var assumeExpr = @"And[t >= 0, t <= 1, Element[{t}, Reals]]".ToExpr();
            
            MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

            // Symbolic time variable
            var t = "t".ToExpr();

            // Curve
            var v = 
                DefineCurve();

            // Time derivatives of curve
            var (vDt1, vDt2, vDt3, vDt4) = 
                v.GetTimeDerivatives4D();
            
            // Arc-length derivatives of curve
            var (vDs1, vDs2, vDs3, vDs4) = 
                v.GetArcLengthDerivatives4D();

            // Make sure vDs1 is a unit vector, and orthogonal to vDs2
            Debug.Assert((vDs1.NormSquared() - 1).IsZero());
            Debug.Assert(vDs1.Sp(vDs2).IsZero());

            var vDt1NormSquared = vDt1.NormSquared().SimplifyCollect(t);
            var vDt1Norm = vDt1NormSquared.Sqrt();

            var vDt2NormSquared = vDt2.NormSquared().SimplifyCollect(t);
            var vDt2Norm = vDt2NormSquared.Sqrt();
            var vDt1Dt2Dot = vDt1.Sp(vDt2).SimplifyCollect(t);
            
            LaTeXComposer
                .ConsoleWriteLine(vDt1Norm, @"\left\Vert \boldsymbol{v}^{\prime}\right\Vert = s^{\prime}")
                .ConsoleWriteLine(vDt2Norm, @"\left\Vert \boldsymbol{v}^{\prime\prime}\right\Vert")
                .ConsoleWriteLine()
                .ConsoleWriteLine(vDt1NormSquared, @"\left\Vert \boldsymbol{v}^{\prime}\right\Vert ^{2}")
                .ConsoleWriteLine(vDt2NormSquared, @"\left\Vert \boldsymbol{v}^{\prime\prime}\right\Vert ^{2}")
                .ConsoleWriteLine(vDt1Dt2Dot, @"\boldsymbol{v}^{\prime}\cdot\boldsymbol{v}^{\prime\prime}")
                .ConsoleWriteLine();

            var vDs1NormSquared = vDs1.NormSquared().SimplifyCollect(t);
            var vDs1Norm = vDs1NormSquared.Sqrt();

            var vDs2NormSquared = vDs2.NormSquared().SimplifyCollect(t);
            var vDs2Norm = vDs2NormSquared.Sqrt();

            var vDs3NormSquared = vDs3.NormSquared().SimplifyCollect(t);
            var vDs3Norm = vDs3NormSquared.Sqrt();

            var vDs4NormSquared = vDs4.NormSquared().SimplifyCollect(t);
            var vDs4Norm = vDs4NormSquared.Sqrt();
            
            LaTeXComposer
                .ConsoleWriteLine(vDs1Norm, @"\left\Vert \dot{\boldsymbol{v}}\left(s\right) \right\Vert")
                .ConsoleWriteLine(vDs2Norm, @"\left\Vert \ddot{\boldsymbol{v}}\left(s\right) \right\Vert")
                .ConsoleWriteLine()
                .ConsoleWriteLine(vDs1NormSquared, @"\left\Vert \dot{\boldsymbol{v}}\left(s\right) \right\Vert ^{2}")
                .ConsoleWriteLine(vDs2NormSquared, @"\left\Vert \ddot{\boldsymbol{v}}\left(s\right) \right\Vert ^{2}")
                .ConsoleWriteLine();
            
            // Time derivatives of arc-length parameter
            var (sDt1, sDt2, sDt3, sDt4) = 
                vDt1Norm.GetArcLengthParameterTimeDerivatives4D();
            
            // Validate the general expressions for sDt1, sDt2, sDt3, sDt4
            var sDt1_1 = vDt1.Sp(vDt1).Sqrt();
            var sDt2_1 = vDt1.Sp(vDt2) / sDt1_1;
            var sDt3_1 = (vDt2.Sp(vDt2) + vDt1.Sp(vDt3) - sDt2_1.Square()) / sDt1_1;
            var sDt4_1 = (3 * vDt2.Sp(vDt3) + vDt1.Sp(vDt4) - 3 * sDt2_1 * sDt3_1) / sDt1_1;

            sDt1_1 = sDt1_1.FullSimplify().Collect(t);
            sDt2_1 = sDt2_1.FullSimplify().Collect(t);
            sDt3_1 = sDt3_1.FullSimplify().Collect(t);
            sDt4_1 = sDt4_1.FullSimplify().Collect(t);

            //LaTeXComposer.ConsoleWriteLine(sDt4, "sDt4");
            //LaTeXComposer.ConsoleWriteLine(sDt4_1, "sDt4_1");

            Debug.Assert((sDt1_1 - sDt1).FullSimplify().IsZero());
            Debug.Assert((sDt2_1 - sDt2).FullSimplify().IsZero());
            Debug.Assert((sDt3_1 - sDt3).FullSimplify().IsZero());
            Debug.Assert((sDt4_1 - sDt4).FullSimplify().IsZero());

            // Validate the general expression for norm of vDs2
            var vDs2Norm_1 = 
                (vDt2NormSquared - 2 * (sDt2 / sDt1) * vDt1Dt2Dot + sDt2.Square()).Sqrt() / sDt1.Square();

            Debug.Assert((vDs2Norm_1 - vDs2Norm).FullSimplify().IsZero());

            // Validate the general expression for vDs1, vDs2, vDs3
            var vDs1_1 = vDt1 / sDt1;
            var vDs2_1 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
            var vDs3_1 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Power(2) - sDt1 * sDt3) * vDt1) / sDt1.Power(5);
            var vDs4_1 = (sDt1.Cube() * vDt4 - 6 * sDt1.Square() * sDt2 * vDt3 - (4 * sDt1.Square() * sDt3 - 15 * sDt1 * sDt2.Square()) * vDt2 + (10 * sDt1 * sDt2 * sDt3 - 15 * sDt2.Cube() - sDt1.Square() * sDt4) * vDt1) / sDt1.Power(7);

            Debug.Assert((vDs1_1 - vDs1).FullSimplifyScalars().IsZero());
            Debug.Assert((vDs2_1 - vDs2).FullSimplifyScalars().IsZero());
            Debug.Assert((vDs3_1 - vDs3).FullSimplifyScalars().IsZero());
            Debug.Assert((vDs4_1 - vDs4).FullSimplifyScalars().IsZero());


            // Gram-Schmidt frame and its derivative of time parametrization of curve
            var (e1t, e2t, e3t, e4t) = 
                GetTimeGramSchmidtFrame4D(vDt1, vDt2, vDt3, vDt4);

            var (e1tDt, e2tDt, e3tDt, e4tDt) = 
                GetFrameTimeDerivative4D(e1t, e2t, e3t, e4t);

            // Gram-Schmidt frame and its derivative of arc-length parametrization of curve
            var (u1s, u2s, u3s, u4s) = 
                GeometricProcessor.ApplyGramSchmidtByProjections(vDs1, vDs2, vDs3, vDs4, false);
            
            var u1sNorm = u1s.Norm().SimplifyCollect(t);
            var u2sNorm = u2s.Norm().SimplifyCollect(t);
            var u3sNorm = u3s.Norm().SimplifyCollect(t);
            var u4sNorm = u4s.Norm().SimplifyCollect(t);

            var (e1s, e2s, e3s, e4s) = 
                GetArcLengthGramSchmidtFrame4D(vDs1, vDs2, vDs3, vDs4);
            
            var (e1sDs, e2sDs, e3sDs, e4sDs) = 
                GetFrameArcLengthDerivative4D(e1s, e2s, e3s, e4s, sDt1);

            // Darboux bivector
            var omega = 
                (e1sDs.Op(e1s) + e2sDs.Op(e2s) + e3sDs.Op(e3s) + e4sDs.Op(e4s)) / 2;

            // Angular velocity blade
            var omegaBar = 
                vDs2.Op(vDs1).SimplifyCollectScalars(t);

            // Bivector B = omega - omegaBar
            var bBivector = 
                (omega - omegaBar).SimplifyCollectScalars(t);

            // Validate vDs1 is orthogonal to Bivector B
            var vDs1DotB = 
                vDs1.Lcp(bBivector).SimplifyCollectScalars(t);

            Debug.Assert(vDs1DotB.IsZero());

            //ValidateEqual(
            //    e1sDs,
            //    -e1s.Lcp(omega),
            //    @"\dot{\boldsymbol{e}}_{1}",
            //    @"\boldsymbol{e}_{1}\lfloor\boldsymbol{\Omega}=-\boldsymbol{e}_{1}\rfloor\boldsymbol{\Omega}"
            //);

            Debug.Assert((e1sDs - -e1s.Lcp(omegaBar)).FullSimplifyScalars().CollectScalars(t).IsZero());
            Debug.Assert((e1sDs - -e1s.Lcp(omega)).FullSimplifyScalars().CollectScalars(t).IsZero());
            Debug.Assert((e2sDs - -e2s.Lcp(omega)).FullSimplifyScalars().CollectScalars(t).IsZero());
            Debug.Assert((e3sDs - -e3s.Lcp(omega)).FullSimplifyScalars().CollectScalars(t).IsZero());
            Debug.Assert((e4sDs - -e4s.Lcp(omega)).FullSimplifyScalars().CollectScalars(t).IsZero());

            //ValidateEqual(
            //    e1sDs,
            //    -e1s.Lcp(omega1),
            //    @"\dot{\boldsymbol{e}}_{1}",
            //    @"\boldsymbol{e}_{1}\lfloor\bar{\boldsymbol{\Omega}} = -\boldsymbol{e}_{1}\rfloor \bar{\boldsymbol{\Omega}}"
            //);
            
            //ValidateEqual(
            //    e2sDs,
            //    -e2s.Lcp(omega1),
            //    @"\dot{\boldsymbol{e}}_{2}",
            //    @"\boldsymbol{e}_{2}\lfloor\bar{\boldsymbol{\Omega}} = -\boldsymbol{e}_{2}\rfloor \bar{\boldsymbol{\Omega}}"
            //);
            
            //ValidateEqual(
            //    e3sDs,
            //    -e3s.Lcp(omega1),
            //    @"\dot{\boldsymbol{e}}_{3}",
            //    @"\boldsymbol{e}_{3}\lfloor\bar{\boldsymbol{\Omega}} = -\boldsymbol{e}_{3}\rfloor \bar{\boldsymbol{\Omega}}"
            //);

            LaTeXComposer
                .ConsoleWriteLine("Darboux bivector:")
                .ConsoleWriteLine(omega, @"\boldsymbol{\Omega}")
                .ConsoleWriteLine(omegaBar, @"\bar{\boldsymbol{\Omega}}")
                .ConsoleWriteLine(bBivector, @"\boldsymbol{B}")
                .ConsoleWriteLine(vDs1DotB, @"\dot{\boldsymbol{v}}\rfloor\boldsymbol{B}")
                .ConsoleWriteLine();

            // Curvature parameters based on parametrization of curve
            var kappa1 = Mfs.ExpandAll[e1sDs.Sp(e2s).ScalarValue].FullSimplify();
            var kappa2 = Mfs.ExpandAll[e2sDs.Sp(e3s).ScalarValue].FullSimplify();
            var kappa3 = Mfs.ExpandAll[e3sDs.Sp(e4s).ScalarValue].FullSimplify();

            LaTeXComposer
                .ConsoleWriteLine("Curvature coefficients:")
                .ConsoleWriteLine(kappa1, @"\kappa_{1}")
                .ConsoleWriteLine(kappa2, @"\kappa_{2}")
                .ConsoleWriteLine(kappa3, @"\kappa_{3}")
                .ConsoleWriteLine();

            var scalarZero = Expr.INT_ZERO.CreateScalar(GeometricProcessor);

            var kappa1_1 = u2sNorm.IsZero() ? scalarZero : (u2sNorm / u1sNorm);
            var kappa2_1 = u3sNorm.IsZero() ? scalarZero : (u3sNorm / u2sNorm);
            var kappa3_1 = u4sNorm.IsZero() ? scalarZero : (u4sNorm / u3sNorm);

            LaTeXComposer
                .ConsoleWriteLine(kappa1_1, @"\kappa_{1}")
                .ConsoleWriteLine(kappa2_1, @"\kappa_{2}")
                .ConsoleWriteLine(kappa3_1, @"\kappa_{3}")
                .ConsoleWriteLine();


            // Make sure the first curvature is equal to the norm of vDs2
            Debug.Assert((kappa1 - kappa1_1).FullSimplify().IsZero());
            Debug.Assert((kappa2 - kappa2_1).FullSimplify().IsZero());
            Debug.Assert((kappa3 - kappa3_1).FullSimplify().IsZero());

            var omega2 = 
                kappa1 * e2s.Op(e1s) + kappa2 * e3s.Op(e2s) + kappa3 * e4s.Op(e3s);

            Debug.Assert((omega - omega2).IsZero());
        }

        public static void Example6D()
        {
            LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

            //var assumeExpr = @"And[t >= 0, t <= 1, Element[{t}, Reals]]".ToExpr();
            
            //MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

            // Symbolic time variable
            var t = "t".ToExpr();

            // Curve
            var v = 
                DefineCurve();

            // Time derivatives of curve
            var (vDt1, vDt2, vDt3, vDt4, vDt5, vDt6) = 
                v.GetTimeDerivatives6D();
            
            // Arc-length derivatives of curve
            var (vDs1, vDs2, vDs3, vDs4, vDs5, vDs6) = 
                v.GetArcLengthDerivatives6D();

            // Make sure vDs1 is a unit vector, and orthogonal to vDs2
            Debug.Assert((vDs1.NormSquared() - 1).IsZero());
            Debug.Assert(vDs1.Sp(vDs2).IsZero());

            var vDt1NormSquared = vDt1.NormSquared().SimplifyCollect(t);
            var vDt1Norm = vDt1NormSquared.Sqrt();

            var vDt2NormSquared = vDt2.NormSquared().SimplifyCollect(t);
            var vDt2Norm = vDt2NormSquared.Sqrt();
            var vDt1Dt2Dot = vDt1.Sp(vDt2).SimplifyCollect(t);
            
            LaTeXComposer
                .ConsoleWriteLine(vDt1Norm, @"\left\Vert \boldsymbol{v}^{\prime}\right\Vert = s^{\prime}")
                .ConsoleWriteLine(vDt2Norm, @"\left\Vert \boldsymbol{v}^{\prime\prime}\right\Vert")
                .ConsoleWriteLine()
                .ConsoleWriteLine(vDt1NormSquared, @"\left\Vert \boldsymbol{v}^{\prime}\right\Vert ^{2}")
                .ConsoleWriteLine(vDt2NormSquared, @"\left\Vert \boldsymbol{v}^{\prime\prime}\right\Vert ^{2}")
                .ConsoleWriteLine(vDt1Dt2Dot, @"\boldsymbol{v}^{\prime}\cdot\boldsymbol{v}^{\prime\prime}")
                .ConsoleWriteLine();

            var vDs1NormSquared = vDs1.NormSquared().SimplifyCollect(t);
            var vDs1Norm = vDs1NormSquared.Sqrt();

            var vDs2NormSquared = vDs2.NormSquared().SimplifyCollect(t);
            var vDs2Norm = vDs2NormSquared.Sqrt();

            var vDs3NormSquared = vDs3.NormSquared().SimplifyCollect(t);
            var vDs3Norm = vDs3NormSquared.Sqrt();

            var vDs4NormSquared = vDs4.NormSquared().SimplifyCollect(t);
            var vDs4Norm = vDs4NormSquared.Sqrt();
            
            var vDs5NormSquared = vDs5.NormSquared().SimplifyCollect(t);
            var vDs5Norm = vDs5NormSquared.Sqrt();

            var vDs6NormSquared = vDs6.NormSquared().SimplifyCollect(t);
            var vDs6Norm = vDs6NormSquared.Sqrt();

            LaTeXComposer
                .ConsoleWriteLine(vDs1Norm, @"\left\Vert \dot{\boldsymbol{v}}\left(s\right) \right\Vert")
                .ConsoleWriteLine(vDs2Norm, @"\left\Vert \ddot{\boldsymbol{v}}\left(s\right) \right\Vert")
                .ConsoleWriteLine()
                .ConsoleWriteLine(vDs1NormSquared, @"\left\Vert \dot{\boldsymbol{v}}\left(s\right) \right\Vert ^{2}")
                .ConsoleWriteLine(vDs2NormSquared, @"\left\Vert \ddot{\boldsymbol{v}}\left(s\right) \right\Vert ^{2}")
                .ConsoleWriteLine();
            
            // Time derivatives of arc-length parameter
            var (sDt1, sDt2, sDt3, sDt4, sDt5, sDt6) = 
                vDt1Norm.GetArcLengthParameterTimeDerivatives6D();
            
            // Validate the general expressions for sDt1, sDt2, sDt3, sDt4
            var sDt1_1 = vDt1.Sp(vDt1).Sqrt();
            var sDt2_1 = vDt1.Sp(vDt2) / sDt1_1;
            var sDt3_1 = (vDt2.Sp(vDt2) + vDt1.Sp(vDt3) - sDt2_1.Square()) / sDt1_1;
            var sDt4_1 = (3 * vDt2.Sp(vDt3) + vDt1.Sp(vDt4) - 3 * sDt2_1 * sDt3_1) / sDt1_1;
            var sDt5_1 = (3 * vDt3.Sp(vDt3) + 4 * vDt2.Sp(vDt4) + vDt1.Sp(vDt5) - 3 * sDt3_1.Square() - 4 * sDt2_1 * sDt4_1) / sDt1_1;
            var sDt6_1 = (10 * vDt3.Sp(vDt4) + 5 * vDt2.Sp(vDt5) + vDt1.Sp(vDt6) - 5 * sDt2_1 * sDt5_1 - 10 * sDt3_1 * sDt4_1) / sDt1_1;

            sDt1_1 = sDt1_1.FullSimplify().Collect(t);
            sDt2_1 = sDt2_1.FullSimplify().Collect(t);
            sDt3_1 = sDt3_1.FullSimplify().Collect(t);
            sDt4_1 = sDt4_1.FullSimplify().Collect(t);
            sDt5_1 = sDt5_1.FullSimplify().Collect(t);
            sDt6_1 = sDt6_1.FullSimplify().Collect(t);
            
            Debug.Assert((sDt1_1 - sDt1).FullSimplify().IsZero());
            Debug.Assert((sDt2_1 - sDt2).FullSimplify().IsZero());
            Debug.Assert((sDt3_1 - sDt3).FullSimplify().IsZero());
            Debug.Assert((sDt4_1 - sDt4).FullSimplify().IsZero());
            Debug.Assert((sDt5_1 - sDt5).FullSimplify().IsZero());
            Debug.Assert((sDt6_1 - sDt6).FullSimplify().IsZero());

            // Validate the general expression for norm of vDs2
            var vDs2Norm_1 = 
                (vDt2NormSquared - 2 * (sDt2 / sDt1) * vDt1Dt2Dot + sDt2.Square()).Sqrt() / sDt1.Square();

            Debug.Assert((vDs2Norm_1 - vDs2Norm).FullSimplify().IsZero());

            // Validate the general expression for vDs1, vDs2, vDs3, ...
            var vDs1_1 = vDt1 / sDt1;
            var vDs2_1 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
            var vDs3_1 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Power(2) - sDt1 * sDt3) * vDt1) / sDt1.Power(5);
            var vDs4_1 = (sDt1.Cube() * vDt4 - 6 * sDt1.Square() * sDt2 * vDt3 - (4 * sDt1.Square() * sDt3 - 15 * sDt1 * sDt2.Square()) * vDt2 + (10 * sDt1 * sDt2 * sDt3 - 15 * sDt2.Cube() - sDt1.Square() * sDt4) * vDt1) / sDt1.Power(7);
            var vDs5_1 = ((45 * sDt1.Square() * sDt2.Square() - 10 * sDt1.Cube() * sDt3) * vDt3 + vDt2 * (-105 * sDt1 * sDt2.Cube() + 60 * sDt1.Square() * sDt2 * sDt3 - 5 * sDt1.Cube() * sDt4) - 10 * sDt1.Cube() * sDt2 * vDt4 + vDt1 * (5 * (21 * sDt2.Power(4) - 21 * sDt1 * sDt2.Square() * sDt3 + 2 * sDt1.Square() * sDt3.Square() + 3 * sDt1.Square() * sDt2 * sDt4) - sDt1.Cube() * sDt5) + sDt1.Power(4) * vDt5) / sDt1.Power(9);
            var vDs6_1 = (sDt1.Power(5) * vDt6 - 15 * sDt1.Power(4) * sDt2 * vDt5 + (105 * sDt1.Cube() * sDt2.Square() - 20 * sDt1.Power(4) * sDt3) * vDt4 - (420 * sDt1.Square() * sDt2.Cube() - 210 * sDt1.Cube() * sDt2 * sDt3 + 15 * sDt1.Power(4) * sDt4) * vDt3 + (945 * sDt1 * sDt2.Power(4) + 70 * sDt1.Cube() * sDt3.Square() - 840 * sDt1.Square() * sDt2.Square() * sDt3 + 105 * sDt1.Cube() * sDt2 * sDt4 - 6 * sDt1.Power(4) * sDt5) * vDt2 + (21 * sDt1.Cube() * sDt2 * sDt5 - sDt1.Power(4) * sDt6 - 35 * (27 * sDt2.Power(5) - 36 * sDt1 * sDt2.Cube() * sDt3 + 8 * sDt1.Square() * sDt2 * sDt3.Square() + sDt1.Square() * (6 * sDt2.Square() - sDt1 * sDt3) * sDt4)) * vDt1) / sDt1.Power(11);

            Debug.Assert((vDs1_1 - vDs1).FullSimplifyScalars().IsZero());
            Debug.Assert((vDs2_1 - vDs2).FullSimplifyScalars().IsZero());
            Debug.Assert((vDs3_1 - vDs3).FullSimplifyScalars().IsZero());
            Debug.Assert((vDs4_1 - vDs4).FullSimplifyScalars().IsZero());
            Debug.Assert((vDs5_1 - vDs5).FullSimplifyScalars().IsZero());
            Debug.Assert((vDs6_1 - vDs6).FullSimplifyScalars().IsZero());


            // Gram-Schmidt frame and its derivative of time parametrization of curve
            var (e1t, e2t, e3t, e4t, e5t, e6t) = 
                GetTimeGramSchmidtFrame6D(vDt1, vDt2, vDt3, vDt4, vDt5, vDt6);

            var (e1tDt, e2tDt, e3tDt, e4tDt, e5tDt, e6tDt) = 
                GetFrameTimeDerivative6D(e1t, e2t, e3t, e4t, e5t, e6t);

            // Gram-Schmidt frame and its derivative of arc-length parametrization of curve
            var (u1s, u2s, u3s, u4s, u5s, u6s) = 
                GeometricProcessor.ApplyGramSchmidtByProjections(vDs1, vDs2, vDs3, vDs4, vDs5, vDs6, false);
            
            var u1sNorm = u1s.Norm().SimplifyCollect(t);
            var u2sNorm = u2s.Norm().SimplifyCollect(t);
            var u3sNorm = u3s.Norm().SimplifyCollect(t);
            var u4sNorm = u4s.Norm().SimplifyCollect(t);
            var u5sNorm = u5s.Norm().SimplifyCollect(t);
            var u6sNorm = u6s.Norm().SimplifyCollect(t);

            var (e1s, e2s, e3s, e4s, e5s, e6s) = 
                GetArcLengthGramSchmidtFrame6D(vDs1, vDs2, vDs3, vDs4, vDs5, vDs6);
            
            var (e1sDs, e2sDs, e3sDs, e4sDs, e5sDs, e6sDs) = 
                GetFrameArcLengthDerivative6D(e1s, e2s, e3s, e4s, e5s, e6s, sDt1);

            // Darboux bivector
            var omega = 
                (e1sDs.Op(e1s) + e2sDs.Op(e2s) + e3sDs.Op(e3s) + e4sDs.Op(e4s) + e5sDs.Op(e5s) + e6sDs.Op(e6s)) / 2;

            // Angular velocity blade
            var omegaBar = 
                vDs2.Op(vDs1).SimplifyCollectScalars(t);

            // Bivector B = omega - omegaBar
            var bBivector = 
                (omega - omegaBar).SimplifyCollectScalars(t);

            // Validate vDs1 is orthogonal to Bivector B
            var vDs1DotB = 
                vDs1.Lcp(bBivector).SimplifyCollectScalars(t);

            Debug.Assert(vDs1DotB.IsZero());

            //ValidateEqual(
            //    e1sDs,
            //    -e1s.Lcp(omega),
            //    @"\dot{\boldsymbol{e}}_{1}",
            //    @"\boldsymbol{e}_{1}\lfloor\boldsymbol{\Omega}=-\boldsymbol{e}_{1}\rfloor\boldsymbol{\Omega}"
            //);

            Debug.Assert((e1sDs - -e1s.Lcp(omegaBar)).FullSimplifyScalars().CollectScalars(t).IsZero());
            Debug.Assert((e1sDs - -e1s.Lcp(omega)).FullSimplifyScalars().CollectScalars(t).IsZero());
            Debug.Assert((e2sDs - -e2s.Lcp(omega)).FullSimplifyScalars().CollectScalars(t).IsZero());
            Debug.Assert((e3sDs - -e3s.Lcp(omega)).FullSimplifyScalars().CollectScalars(t).IsZero());
            Debug.Assert((e4sDs - -e4s.Lcp(omega)).FullSimplifyScalars().CollectScalars(t).IsZero());
            Debug.Assert((e5sDs - -e5s.Lcp(omega)).FullSimplifyScalars().CollectScalars(t).IsZero());
            Debug.Assert((e6sDs - -e6s.Lcp(omega)).FullSimplifyScalars().CollectScalars(t).IsZero());

            //ValidateEqual(
            //    e1sDs,
            //    -e1s.Lcp(omega1),
            //    @"\dot{\boldsymbol{e}}_{1}",
            //    @"\boldsymbol{e}_{1}\lfloor\bar{\boldsymbol{\Omega}} = -\boldsymbol{e}_{1}\rfloor \bar{\boldsymbol{\Omega}}"
            //);
            
            //ValidateEqual(
            //    e2sDs,
            //    -e2s.Lcp(omega1),
            //    @"\dot{\boldsymbol{e}}_{2}",
            //    @"\boldsymbol{e}_{2}\lfloor\bar{\boldsymbol{\Omega}} = -\boldsymbol{e}_{2}\rfloor \bar{\boldsymbol{\Omega}}"
            //);
            
            //ValidateEqual(
            //    e3sDs,
            //    -e3s.Lcp(omega1),
            //    @"\dot{\boldsymbol{e}}_{3}",
            //    @"\boldsymbol{e}_{3}\lfloor\bar{\boldsymbol{\Omega}} = -\boldsymbol{e}_{3}\rfloor \bar{\boldsymbol{\Omega}}"
            //);

            LaTeXComposer
                .ConsoleWriteLine("Darboux bivector:")
                .ConsoleWriteLine(omega, @"\boldsymbol{\Omega}")
                .ConsoleWriteLine(omegaBar, @"\bar{\boldsymbol{\Omega}}")
                .ConsoleWriteLine(bBivector, @"\boldsymbol{B}")
                .ConsoleWriteLine(vDs1DotB, @"\dot{\boldsymbol{v}}\rfloor\boldsymbol{B}")
                .ConsoleWriteLine();

            // Curvature parameters based on parametrization of curve
            var kappa1 = Mfs.ExpandAll[e1sDs.Sp(e2s).ScalarValue].FullSimplify();
            var kappa2 = Mfs.ExpandAll[e2sDs.Sp(e3s).ScalarValue].FullSimplify();
            var kappa3 = Mfs.ExpandAll[e3sDs.Sp(e4s).ScalarValue].FullSimplify();
            var kappa4 = Mfs.ExpandAll[e4sDs.Sp(e5s).ScalarValue].FullSimplify();
            var kappa5 = Mfs.ExpandAll[e5sDs.Sp(e6s).ScalarValue].FullSimplify();

            LaTeXComposer
                .ConsoleWriteLine("Curvature coefficients:")
                .ConsoleWriteLine(kappa1, @"\kappa_{1}")
                .ConsoleWriteLine(kappa2, @"\kappa_{2}")
                .ConsoleWriteLine(kappa3, @"\kappa_{3}")
                .ConsoleWriteLine(kappa4, @"\kappa_{4}")
                .ConsoleWriteLine(kappa5, @"\kappa_{5}")
                .ConsoleWriteLine();

            var scalarZero = Expr.INT_ZERO.CreateScalar(GeometricProcessor);

            var kappa1_1 = u2sNorm.IsZero() ? scalarZero : u2sNorm / u1sNorm;
            var kappa2_1 = u3sNorm.IsZero() ? scalarZero : u3sNorm / u2sNorm;
            var kappa3_1 = u4sNorm.IsZero() ? scalarZero : u4sNorm / u3sNorm;
            var kappa4_1 = u5sNorm.IsZero() ? scalarZero : u5sNorm / u4sNorm;
            var kappa5_1 = u6sNorm.IsZero() ? scalarZero : u6sNorm / u5sNorm;

            LaTeXComposer
                .ConsoleWriteLine(kappa1_1, @"\kappa_{1}")
                .ConsoleWriteLine(kappa2_1, @"\kappa_{2}")
                .ConsoleWriteLine(kappa3_1, @"\kappa_{3}")
                .ConsoleWriteLine(kappa4_1, @"\kappa_{4}")
                .ConsoleWriteLine(kappa5_1, @"\kappa_{5}")
                .ConsoleWriteLine();


            // Make sure the first curvature is equal to the norm of vDs2
            Debug.Assert((kappa1 - kappa1_1).FullSimplify().IsZero());
            Debug.Assert((kappa2 - kappa2_1).FullSimplify().IsZero());
            Debug.Assert((kappa3 - kappa3_1).FullSimplify().IsZero());
            Debug.Assert((kappa4 - kappa4_1).FullSimplify().IsZero());
            Debug.Assert((kappa5 - kappa5_1).FullSimplify().IsZero());

            var omega2 =
                kappa1 * e2s.Op(e1s) +
                kappa2 * e3s.Op(e2s) +
                kappa3 * e4s.Op(e3s) +
                kappa4 * e5s.Op(e4s) +
                kappa5 * e6s.Op(e5s);

            Debug.Assert((omega - omega2).IsZero());
        }
    }
}