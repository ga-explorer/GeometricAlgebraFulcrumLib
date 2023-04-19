using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using TextComposerLib.Text;
using GAPoTNumLib.Text.Linear;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using TextComposerLib.Text.Parametric;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Applications.GaPoT;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Samples.PowerSystems.GAPoT
{
    public static class SimpleKirchhoffRotationSamples
    {
        public static void CodeGenerationExample1()
        {
            var composer = new LinearTextComposer();

            for (var n = 3; n <= 48; n++)
            {
                var text1 = n.GetRange()
                    .Select(i => $"uVector[{i}]")
                    .Concatenate(" +" + Environment.NewLine, "", ";");

                var text2 = (n - 1).GetRange()
                    .Select(i => $"vVector[{i}] = uVector[{i}] + k;")
                    .Concatenate(Environment.NewLine);

                var text3 = $"vVector[{n - 1}] = uVector[{n - 1}] + k - m;";

                composer
                    .AppendLine($"public double[] SkrRotate{n}D(double[] uVector)")
                    .AppendLine("{")
                    .IncreaseIndentation()
                    .AppendLine($"const int n = {n};")
                    .AppendLine()
                    .AppendLine("var nSqrt = Math.Sqrt(n);")
                    .AppendLine("var vVector = new double[n];")
                    .AppendLine()
                    .AppendLine("var a = ")
                    .IncreaseIndentation()
                    .AppendLine(text1)
                    .DecreaseIndentation()
                    .AppendLine("a /= 1d + nSqrt;")
                    .AppendLine()
                    .AppendLine("var un = uVector[n - 1];")
                    .AppendLine("var k = (un - a) / nSqrt;")
                    .AppendLine("var m = un + a;")
                    .AppendLine()
                    .AppendLine(text2)
                    .AppendLine(text3)
                    .AppendLine()
                    .AppendLine("return vVector;")
                    .DecreaseIndentation()
                    .AppendLineAtNewLine("}")
                    .AppendLine();
            }

            Console.WriteLine(composer.ToString());
        }


        //private static bool GeneratePreComputationsCode(GaFuLMetaContextCodeComposer contextCodeComposer)
        //{
        //    //Generate comments
        //    GaFuLMetaContextCodeComposer.DefaultGenerateCommentsBeforeComputations(contextCodeComposer);

        //    //Temp variables declaration
        //    //Add array declaration code
        //    contextCodeComposer.SyntaxList.Add(
        //        contextCodeComposer.GeoLanguage.SyntaxFactory.DeclareLocalArray(
        //            "double",
        //            "tempArray",
        //            contextCodeComposer.Context.GetTargetTempVarsCount().ToString()
        //        )
        //    );

        //    contextCodeComposer.SyntaxList.AddEmptyLine();

        //    return true;
        //}

        public static void CodeGenerationExample2()
        {
            // The number of dimensions
            for (var n = 3; n <= 48; n++)
            {
                // Stage 1: Define the meta-programming context
                // The meta-programming context is a special kind of symbolic linear processor for code generation
                var context =
                    new MetaContext()
                    {
                        MergeExpressions = false,
                        ContextOptions =
                        {
                            ContextName = "Clarke Transformation"
                        },

                    };

                // Use this if you want Wolfram Mathematica symbolic processor
                // instead of the default AngouriMath symbolic processor
                context.AttachMathematicaExpressionEvaluator();

                // Define a Euclidean multivectors processor for the context
                var processor =
                    context.CreateEuclideanXGaProcessor();

                // Stage 2: Define the input parameters of the context
                // The input parameters are named variables created as scalar parts of multivectors
                // and used for later processing to compute some outputs

                // Define the first vector for constructing the rotor with a given
                // set of scalar components u1, u2, ...
                var clarkeMap =
                    processor.CreateClarkeRotationMap(n);

                var x =
                    context.ParameterVariablesFactory.CreateDenseVector(
                        n,
                        index => $"x{index + 1}"
                    );

                // Stage 3: Define computations and specify which variables are required outputs
                //Define a Euclidean rotor which takes input unit vector u to input unit vector v
                var y =
                    clarkeMap.OmMap(x);

                // Define the final outputs for the computations for proper code generation
                y.SetIsOutput(true);

                // Stage 4: Optimize symbolic computations in the meta-programming context
                context.OptimizeContext();

                // Stage 5: Assign code generated variable names for all variables
                // Define code generated variable names for input variables
                x.SetExternalNamesByTermIndex(
                    idx => $"uVector[{idx}]"
                );

                // Define code generated variable names for output variables
                y.SetExternalNamesByTermIndex(
                    idx => $"vVector[{idx}]"
                );

                // Define code generated variable names for intermediate variables
                context.SetIntermediateExternalNamesByNameIndex(
                    index => $"temp{index}"
                );

                // Stage 6: Define a C# code composer with AngouriMath symbolic expressions converter
                var contextCodeComposer = context.CreateContextCodeComposer(
                    GaFuLLanguageServerBase.CSharpFloat64()
                );

                contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = false;
                //contextCodeComposer.ComposerOptions.ActionBeforeGenerateComputations = GeneratePreComputationsCode;

                // Stage 7: Generate the final C# code
                var code =
                    new ParametricTextComposer("#", "#", @"
namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems
{
    public static partial class ClarkeMapUtils
    {
        public static double[] ClarkeRotate#n#D(this double[] uVector)
        {
            const int n = #n#;

            var vVector = new double[n];

            #code#

            return vVector;
        }
    }
}
".Trim()
                    ).GenerateUsing(n.ToString(), contextCodeComposer.Generate());

                File.WriteAllText(@$"D:\ClarkeMap{n}DUtils.cs", code);

                Console.WriteLine($"Generated code for {n}-dimensions");
            }
        }

        public static void Example1()
        {
            var textComposer
                = TextComposerExpr.DefaultComposer;

            var laTeXComposer
                = LaTeXComposerExpr.DefaultComposer;

            laTeXComposer.BasisName = @"\boldsymbol{\mu}";

            for (var n = 3; n <= 8; n++)
            {
                var scalarProcessor =
                    ScalarProcessorExpr.DefaultProcessor;

                var geometricProcessor =
                    scalarProcessor.CreateEuclideanRGaProcessor();

                var u =
                    geometricProcessor.CreateVector(n - 1);

                var k =
                    geometricProcessor.CreateSymmetricVector(n);

                var v =
                    k.DivideByENorm();

                var dot1 =
                    v.Sp(u);

                var angle =
                    Mfs.Minus[dot1.ArcCos().ScalarValue].EvaluateToDouble().RadiansToAngle();

                var rotor =
                    u.CreatePureRotor(v);

                var matrix =
                    rotor.GetVectorMapPart(n).ToArray(n);

                var x = geometricProcessor.CreateVector(
                    n,
                    i => $"Subscript[x, {i + 1}]".ToExpr()
                );

                var a = x.Sp(u);
                var b = $"-1 / (1 + Sqrt[{n}])".ToExpr() * x.Sp(k - u);

                var y1 =
                    rotor.OmMap(x);

                var y2 =
                    x + (b + a) * v + (b - a) * u;

                var yDiff =
                    (y1 - y2).FullSimplifyScalars();

                laTeXComposer
                    .ConsoleWriteLine($"{n}-Dimensions:")
                    .ConsoleWriteLine()
                    .ConsoleWriteLine($@"$\boldsymbol{{v}} = {laTeXComposer.GetMultivectorText(v)}$")
                    .ConsoleWriteLine($@"$\boldsymbol{{u}} = {laTeXComposer.GetMultivectorText(u)}$")
                    .ConsoleWriteLine($@"$\varphi_{{{n}}}=-\cos^{{-1}}\frac{{1}}{{\sqrt{{{n}}}}} = {laTeXComposer.GetAngleText(angle)}$")
                    .ConsoleWriteLine($@"$\boldsymbol{{R}} = {laTeXComposer.GetMultivectorText(rotor)}$")
                    .ConsoleWriteLine($@"$\boldsymbol{{M}} = {laTeXComposer.GetArrayText(matrix)}$")
                    .ConsoleWriteLine()
                    .ConsoleWriteLine($@"$\boldsymbol{{x}} = {laTeXComposer.GetMultivectorText(x)}$")
                    .ConsoleWriteLine($@"$\boldsymbol{{y}}_{{1}} = \boldsymbol{{R}}\boldsymbol{{x}}\boldsymbol{{R}}^{{\dagger}} = {laTeXComposer.GetMultivectorText(y1)}$")
                    .ConsoleWriteLine($@"$\boldsymbol{{y}}_{{2}} = {laTeXComposer.GetMultivectorText(y2)}$")
                    .ConsoleWriteLine($@"$\boldsymbol{{y}}_{{1}} - \boldsymbol{{y}}_{{2}} = {laTeXComposer.GetMultivectorText(yDiff)}$")
                    .ConsoleWriteLine();
            }
        }

        public static void Example2()
        {
            var textComposer
                = TextComposerExpr.DefaultComposer;

            var laTeXComposer
                = LaTeXComposerExpr.DefaultComposer;

            laTeXComposer.BasisName = @"\boldsymbol{\mu}";

            for (var n = 3; n <= 8; n++)
            {
                var scalarProcessor =
                    ScalarProcessorExpr.DefaultProcessor;

                var geometricProcessor =
                    scalarProcessor.CreateEuclideanRGaProcessor();

                var u =
                    geometricProcessor.CreateVector(n - 1);

                //var v = 
                //    geometricProcessor.CreateVector(
                //        n,
                //        i => Expr.INT_ONE
                //    ).DivideByENorm();

                var v =
                    geometricProcessor.CreateVector(
                        n,
                        i => $"Subscript[v, {i + 1}]".ToExpr()
                    ).DivideByENorm();

                var rotor =
                    u.CreatePureRotor(v);

                var matrix =
                    rotor.GetVectorMapPart(n).ToArray(n);

                var x = geometricProcessor.CreateVector(
                    n,
                    i => $"Subscript[x, {i + 1}]".ToExpr()
                );

                var a = x.Sp(u);
                var b = x.Sp(v - v.Sp(u) * u) / (1 + v.Sp(u));

                var y1 =
                    rotor.OmMap(x);

                var y2 =
                    x - (b - a) * v - (b + a) * u;

                var yDiff =
                    (y1 - y2).FullSimplifyScalars();

                laTeXComposer
                    .ConsoleWriteLine($"{n}-Dimensions:")
                    .ConsoleWriteLine()
                    .ConsoleWriteLine($@"$\boldsymbol{{v}} = {laTeXComposer.GetMultivectorText(v)}$")
                    .ConsoleWriteLine($@"$\boldsymbol{{u}} = {laTeXComposer.GetMultivectorText(u)}$")
                    //.ConsoleWriteLine($@"$\varphi_{{{n}}}=-\cos^{{-1}}\frac{{1}}{{\sqrt{{{n}}}}} = {laTeXComposer.GetAngleText(angle)}$")
                    .ConsoleWriteLine($@"$\boldsymbol{{R}} = {laTeXComposer.GetMultivectorText(rotor)}$")
                    .ConsoleWriteLine($@"$\boldsymbol{{M}} = {laTeXComposer.GetArrayText(matrix)}$")
                    .ConsoleWriteLine()
                    .ConsoleWriteLine($@"$\boldsymbol{{x}} = {laTeXComposer.GetMultivectorText(x)}$")
                    .ConsoleWriteLine($@"$\boldsymbol{{y}}_{{1}} = \boldsymbol{{R}}\boldsymbol{{x}}\boldsymbol{{R}}^{{\dagger}} = {laTeXComposer.GetMultivectorText(y1)}$")
                    .ConsoleWriteLine($@"$\boldsymbol{{y}}_{{2}} = {laTeXComposer.GetMultivectorText(y2)}$")
                    .ConsoleWriteLine($@"$\boldsymbol{{y}}_{{1}} - \boldsymbol{{y}}_{{2}} = {laTeXComposer.GetMultivectorText(yDiff)}$")
                    .ConsoleWriteLine();
            }
        }

        public static void Example3()
        {
            const int n = 5;

            var textComposer =
                TextComposerExpr.DefaultComposer;

            var laTeXComposer =
                LaTeXComposerExpr.DefaultComposer;

            laTeXComposer.BasisName = @"\boldsymbol{\mu}";

            var scalarProcessor =
                ScalarProcessorExpr.DefaultProcessor;

            var geometricProcessor =
                scalarProcessor.CreateEuclideanRGaProcessor();

            var assumeExpr1 =
                n.GetRange(1)
                    .Select(i => $"Subscript[x, {i}] | Subscript[k, {i}]")
                    .Concatenate(" | ");

            var assumeExpr2 =
                n.GetRange(1)
                    .Select(i => $"Subscript[k, {i}]^2")
                    .Concatenate(" + ");

            var assumeExpr =
                $@"And[Element[{assumeExpr1}, Reals], {assumeExpr2} == 1]".ToExpr();

            MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

            var k = geometricProcessor.CreateVector(
                n,
                i => $"Subscript[k, {i + 1}]".ToExpr()
            ).DivideByENorm();

            for (var i = 0; i < n; i++)
            {
                var u = geometricProcessor.CreateVector(i);
                var v = k;

                var rotor =
                    u.CreatePureRotor(v);

                var x = geometricProcessor.CreateVector(
                    n,
                    i => $"Subscript[x, {i + 1}]".ToExpr()
                );

                var y1 =
                    rotor.OmMap(x);

                var psInv = x.Op(u).Op(v).Inverse();
                var a = u.Op(v).Lcp(psInv).Sp(y1).Scalar.FullSimplifyScalar();
                var b = v.Op(x).Lcp(psInv).Sp(y1).Scalar.FullSimplifyScalar();
                var c = x.Op(u).Lcp(psInv).Sp(y1).Scalar.FullSimplifyScalar();

                //var r = ((b - c) / 2).FullSimplify();
                //var s = (-(b + c) / 2).FullSimplify();

                var y2 =
                    a * x + b * u + c * v;

                var yDiff =
                    (y1 - y2).FullSimplifyScalars();

                Debug.Assert(yDiff.IsZero);

                laTeXComposer
                    .ConsoleWriteLine($"{n}-Dimensions:")
                    .ConsoleWriteLine()
                    .ConsoleWriteLine($@"$\boldsymbol{{x}} = {laTeXComposer.GetMultivectorText(x)}$")
                    .ConsoleWriteLine($@"$\boldsymbol{{u}} = {laTeXComposer.GetMultivectorText(u)}$")
                    .ConsoleWriteLine($@"$\boldsymbol{{v}} = {laTeXComposer.GetMultivectorText(v)}$")
                    .ConsoleWriteLine()
                    //.ConsoleWriteLine($@"$\boldsymbol{{R}} = {laTeXComposer.GetMultivectorText(rotor)}$")
                    //.ConsoleWriteLine()
                    .ConsoleWriteLine($@"$a = {laTeXComposer.GetScalarText(a)}$")
                    .ConsoleWriteLine($@"$b = {laTeXComposer.GetScalarText(b)}$")
                    .ConsoleWriteLine($@"$c = {laTeXComposer.GetScalarText(c)}$")
                    .ConsoleWriteLine()
                    //.ConsoleWriteLine($@"$(b - c) / 2 = {laTeXComposer.GetScalarText(r)}$")
                    //.ConsoleWriteLine($@"$-(b + c) / 2 = {laTeXComposer.GetScalarText(s)}$")
                    //.ConsoleWriteLine()
                    //.ConsoleWriteLine($@"$\boldsymbol{{y}}_{{1}} = {laTeXComposer.GetMultivectorText(y1)}$")
                    //.ConsoleWriteLine($@"$\boldsymbol{{y}}_{{2}} = {laTeXComposer.GetMultivectorText(y2)}$")
                    //.ConsoleWriteLine($@"$\boldsymbol{{y}}_{{1}} - \boldsymbol{{y}}_{{2}} = {laTeXComposer.GetMultivectorText(yDiff)}$")
                    .ConsoleWriteLine();
            }
        }

        public static void Example4()
        {
            const int n = 3;

            var textComposer
                = TextComposerExpr.DefaultComposer;

            var laTeXComposer
                = LaTeXComposerExpr.DefaultComposer;

            laTeXComposer.BasisName = @"\boldsymbol{\mu}";

            var scalarProcessor =
                ScalarProcessorExpr.DefaultProcessor;

            var geometricProcessor =
                scalarProcessor.CreateEuclideanRGaProcessor();

            var assumeExpr1 =
                n.GetRange(1)
                    .Select(i => $"Subscript[x, {i}] | Subscript[u, {i}] | Subscript[v, {i}]")
                    .Concatenate(" | ");

            var assumeExpr2 =
                n.GetRange(1)
                    .Select(i => $"Subscript[u, {i}]^2")
                    .Concatenate(" + ");

            var assumeExpr3 =
                n.GetRange(1)
                    .Select(i => $"Subscript[v, {i}]^2")
                    .Concatenate(" + ");

            var assumeExpr =
                $@"And[Element[{assumeExpr1}, Reals], {assumeExpr2} == 1, {assumeExpr3} == 1]".ToExpr();

            MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

            var u =
                geometricProcessor.CreateVector(
                    n,
                    i => $"Subscript[u, {i + 1}]".ToExpr()
                ).DivideByENorm();

            var v =
                geometricProcessor.CreateVector(
                    n,
                    i => $"Subscript[v, {i + 1}]".ToExpr()
                ).DivideByENorm();

            var x =
                geometricProcessor.CreateVector(
                    n,
                    i => $"Subscript[x, {i + 1}]".ToExpr()
                );

            var rotor =
                u.CreatePureRotor(v);

            laTeXComposer
                .ConsoleWriteLine($"{n}-Dimensions:")
                .ConsoleWriteLine()
                .ConsoleWriteLine($@"$\boldsymbol{{u}} = {laTeXComposer.GetMultivectorText(u)}$")
                .ConsoleWriteLine($@"$\boldsymbol{{v}} = {laTeXComposer.GetMultivectorText(v)}$")
                .ConsoleWriteLine($@"$\boldsymbol{{x}} = {laTeXComposer.GetMultivectorText(x)}$")
                .ConsoleWriteLine()
                .ConsoleWriteLine($@"$\boldsymbol{{R}} = {laTeXComposer.GetMultivectorText(rotor)}$")
                .ConsoleWriteLine();

            var a = geometricProcessor.CreateZeroScalar();
            var b = geometricProcessor.CreateZeroScalar();
            var c = geometricProcessor.CreateZeroScalar();

            for (var i = 0; i < n; i++)
            {
                var xi =
                    geometricProcessor.CreateScalar($"Subscript[x, {i + 1}]".ToExpr());

                var ei =
                    geometricProcessor.CreateVector(i);

                var yi =
                    rotor.OmMap(ei);

                var psInv = x.Op(u).Op(v).Inverse();

                a += u.Op(v).Lcp(psInv).Sp(yi) * xi;
                b += v.Op(x).Lcp(psInv).Sp(yi) * xi;
                c += x.Op(u).Lcp(psInv).Sp(yi) * xi;

                //var y2 = a * x + b * u + c * v;
                //var yDiff = (y1 - y2).FullSimplifyScalars();

                laTeXComposer
                    .ConsoleWriteLine($@"$\boldsymbol{{R}} \boldsymbol{{\mu}}_{{{i + 1}}} \boldsymbol{{R}}^{{\dagger}} = {laTeXComposer.GetMultivectorText(yi)}$")
                    .ConsoleWriteLine();
            }

            a = a.FullSimplifyScalar();
            b = b.FullSimplifyScalar();
            c = c.FullSimplifyScalar();

            laTeXComposer
                .ConsoleWriteLine($@"$a = {laTeXComposer.GetScalarText(a)}$")
                .ConsoleWriteLine()
                .ConsoleWriteLine($@"$2 \left( b - c \right) = {laTeXComposer.GetScalarText(2 * (c - b))}$")
                .ConsoleWriteLine()
                .ConsoleWriteLine($@"$2 \left( b + c \right) = {laTeXComposer.GetScalarText(2 * (c + b))}$")
                .ConsoleWriteLine();
        }
    }
}
