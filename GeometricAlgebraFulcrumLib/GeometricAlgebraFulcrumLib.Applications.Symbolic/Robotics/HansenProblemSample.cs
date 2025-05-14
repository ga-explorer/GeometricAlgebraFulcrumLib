using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Cost;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Mutation;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Operations;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Versors;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.Robotics;

/// <summary>
/// https://en.wikipedia.org/wiki/Hansen's_problem
/// </summary>
public static class HansenProblemSample
{
    //public static void GenerateTrigCode1()
    //{
    // Stage 1: Define the meta-programming context
    // The meta-programming context is a special kind
    // of symbolic processor for code generation
    //    var context =
    //        new MetaContext()
    //        {
    //            MergeExpressions = true,
    //            ContextOptions =
    //            {
    //                ContextName = "Trig",
    //                AllowGenerateComments = true,
    //                PropagateConstants = true,
    //                ReUseIntermediateVariables = false
    //            }
    //        };

    //    // Use this if you want Wolfram Mathematica symbolic processor
    //    // instead of the default AngouriMath symbolic processor
    //    context.AttachMathematicaExpressionEvaluator();

    //    // Define a Euclidean multivectors processor for the context
    //    var processor = context.CreateEuclideanXGaProcessor();


    // Stage 2: Define the input parameters of the context
    // The input parameters are named variables created as
    // scalar parts of multivectors and used for later
    // processing to compute some outputs

    //    var pi =
    //        context.ScalarProcessor.CreateScalar(context.ScalarPi);

    //    // Define constant EGA basis blades
    //    var e1 =
    //        processor.VectorTerm(0);

    //    var e2 =
    //        processor.VectorTerm(1);

    //    // Define input 2D position vectors of points A,B
    //    var pvA =
    //        context["Ax"] * e1 + context["Ay"] * e2;
    //    //context[-1] * e1 + context[5] * e2;

    //    var pvB =
    //        context["Bx"] * e1 + context["By"] * e2;
    //    //context[6] * e1 + context[4] * e2;

    //    // Define input angles
    //    var alpha1 =
    //        context["alpha1"];
    //    //context[44.534 * Math.PI / 180];

    //    var alpha2 =
    //        context["alpha2"];
    //    //context[32.471 * Math.PI / 180];

    //    var beta1 =
    //        context["beta1"];
    //    //context[93.066 * Math.PI / 180];

    //    var beta2 =
    //        context["beta2"];
    //    //context[69.341 * Math.PI / 180];

    //    var alpha1M = beta1 - alpha1;
    //    var beta1M = alpha2;
    //    var alpha2M = alpha1;
    //    var beta2M = beta2 - alpha2;


    // Stage 3: Perform algebraic computations on input parameters
    // A symbolic expression tree for elementary operations on scalar
    // components is created automatically in this stage

    //    var lambda =
    //        ((beta1M + beta2M).Sin() * (alpha1M + alpha2M + beta1M).Sin()).ArcTan2(
    //            beta1M.Sin() * (beta1M + beta2M + alpha2M).Sin()
    //        );

    //    var s1 =
    //        2 * ((pi / 4 - lambda).Tan() / (alpha1M / 2).Tan()).ArcTan();

    //    var s2 = pi - alpha1M;

    //    var gamma = (s2 + s1) / 2;
    //    var delta = (s2 - s1) / 2;

    //    var phi = 
    //        (context["By"] - context["Ay"]).ArcTan2(context["Bx"] - context["Ax"]);

    //    var theta = 
    //        gamma - pi + alpha1M + alpha2M + beta1M;

    //    var phi1 = phi + gamma;
    //    var phi2 = phi + theta;

    //    var ap1 = 
    //        delta.Sin() * (pvB - pvA).Norm() / alpha1M.Sin();

    //    var ap2 = 
    //        (pi - theta - beta2M).Sin() * (pvB - pvA).Norm() / beta2M.Sin();

    //    var v1 = phi1.Sin() * e1 + phi1.Cos() * e2;
    //    var v2 = phi2.Sin() * e1 + phi2.Cos() * e2;

    //    // Compute final points p1, p2
    //    var pvP1 = pvA + ap1 * v1;
    //    var pvP2 = pvA + ap2 * v2;

    //    // Define the final outputs for the computations for proper code generation
    //    pvP1.SetIsOutput(true);
    //    pvP2.SetIsOutput(true);


    //    // Stage 4: Optimize symbolic computations in the meta-programming context
    //    context.OptimizeContext();


    //    // Stage 5: Assign code generated variable names for all variables
    //    // Define code generated variable names for input variables
    //    context["Ax"].SetExternalName("A.X.Value");
    //    context["Ay"].SetExternalName("A.Y.Value");

    //    context["Bx"].SetExternalName("B.X.Value");
    //    context["By"].SetExternalName("B.Y.Value");

    //    alpha1.SetExternalName("Alpha1.Radians.Value");
    //    alpha2.SetExternalName("Alpha2.Radians.Value");

    //    beta1.SetExternalName("Beta1.Radians.Value");
    //    beta2.SetExternalName("Beta2.Radians.Value");

    //    pvP1[0].SetExternalName("p1X");
    //    pvP1[1].SetExternalName("p1Y");

    //    pvP2[0].SetExternalName("p2X");
    //    pvP2[1].SetExternalName("p2Y");

    //    // Define code generated variable names for intermediate variables
    //    context.SetIntermediateExternalNamesByNameIndex(index => $"temp{index}");


    //    // Stage 6: Define a C# code composer with AngouriMath symbolic expressions converter
    //    var contextCodeComposer = context.CreateContextCodeComposer(
    //        GaFuLLanguageServerBase.CSharpFloat64()
    //    );

    //    contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = false;

    //    // Stage 7: Generate the final C# code
    //    var code = contextCodeComposer.Generate();

    //    Console.WriteLine("Generated Code:");
    //    Console.WriteLine(code);
    //    Console.WriteLine();


    //    var dotCode = contextCodeComposer.GenerateGraphVizCode();

    //    Console.WriteLine("GraphViz Code:");
    //    Console.WriteLine(dotCode);
    //    Console.WriteLine();
    //}

    //public static void GenerateVGaCode1()
    //{
    // Stage 1: Define the meta-programming context
    // The meta-programming context is a special kind
    // of symbolic processor for code generation
    //    var context = 
    //        new MetaContext()
    //        {
    //            MergeExpressions = true,
    //            ContextOptions =
    //            {
    //                ContextName = "VGA", 
    //                AllowGenerateComments = true,
    //                PropagateConstants = true,
    //                ReUseIntermediateVariables = false
    //            }
    //        };

    //    // Use this if you want Wolfram Mathematica symbolic processor
    //    // instead of the default AngouriMath symbolic processor
    //    context.AttachMathematicaExpressionEvaluator();

    //    // Define a Euclidean multivectors processor for the context
    //    var processor = context.CreateEuclideanXGaProcessor();


    // Stage 2: Define the input parameters of the context
    // The input parameters are named variables created as
    // scalar parts of multivectors and used for later
    // processing to compute some outputs

    //    // Define constant EGA basis blades
    //    var pi =
    //        context.CreateScalar(context.ScalarPi).CreatePolarAngleFromRadians();

    //    var e1 = 
    //        processor.VectorTerm(0);

    //    var e2 = 
    //        processor.VectorTerm(1);

    //    var e12 = 
    //        e1.Op(e2);

    //    // Define input 2D position vectors of points A,B
    //    var pvA =
    //        context["Ax"] * e1 + context["Ay"] * e2;
    //        //context[-1] * e1 + context[5] * e2;

    //    var pvB =
    //        context["Bx"] * e1 + context["By"] * e2;
    //        //context[6] * e1 + context[4] * e2;


    //    // Define input angles
    //    var alpha1 =
    //        context.CreatePolarAngle(
    //            context["alpha1Cos"], 
    //            context["alpha1Sin"]
    //        );
    //    //context[44.534 * Math.PI / 180];

    //    var alpha2 =
    //        context.CreatePolarAngle(
    //            context["alpha2Cos"], 
    //            context["alpha2Sin"]
    //        );
    //    //context[32.471 * Math.PI / 180];

    //    var beta1 =
    //        context.CreatePolarAngle(
    //            context["beta1Cos"], 
    //            context["beta1Sin"]
    //        );
    //    //context[93.066 * Math.PI / 180];

    //    var beta2 =
    //        context.CreatePolarAngle(
    //            context["beta2Cos"], 
    //            context["beta2Sin"]
    //        );
    //    //context[69.341 * Math.PI / 180];


    // Stage 3: Perform algebraic computations on input parameters
    // A symbolic expression tree for elementary operations on scalar
    // components is created automatically in this stage

    //    // Define m1, m2, mbp2
    //    var s1 = 
    //        alpha2.Sin() / (alpha2 + beta1).Sin();

    //    var s2 = 
    //        beta1.Sin() / (beta1 + alpha2).Sin();

    //    var s3 = 
    //        alpha1.Sin() / (alpha1 + beta2).Sin();

    //    var s4 = 
    //        beta2.Sin() / (beta2 + alpha1).Sin();

    //    // Compute lambda, theta
    //    var lambda = 
    //        1 / (s2.Square() + s3.Square() - 2 * s2 * s3 * (beta2 - alpha2).Cos()).Sqrt();

    //    var theta =
    //        (s2 - s3 * (beta2 - alpha2).Cos()).ArcTan2(s3 * (beta2 - alpha2).Sin());

    //    // Compute final points p1, p2
    //    var d = pvB - pvA;

    //    var ra1 = -(theta + pi - beta1 - alpha2);
    //    var r1 = ra1.Cos() + ra1.Sin() * e12;

    //    var ra2 = -theta;
    //    var r2 = ra2.Cos() + ra2.Sin() * e12;

    //    var pvP1 = 
    //        pvA + (lambda * s1 * d.Gp(r1)).GetVectorPart();

    //    var pvP2 = 
    //        pvA + (lambda * s2 * d.Gp(r2)).GetVectorPart();

    //    // Define the final outputs for the computations for proper code generation
    //    pvP1.SetIsOutput(true);
    //    pvP2.SetIsOutput(true);


    //    // Stage 4: Optimize symbolic computations in the meta-programming context
    //    context.OptimizeContext();


    //    // Stage 5: Assign code generated variable names for all variables
    //    // Define code generated variable names for input variables
    //    context["Ax"].SetExternalName("A.X.Value");
    //    context["Ay"].SetExternalName("A.Y.Value");

    //    context["Bx"].SetExternalName("B.X.Value");
    //    context["By"].SetExternalName("B.Y.Value");

    //    context["alpha1Cos"].SetExternalName("alpha1Cos");
    //    context["alpha1Sin"].SetExternalName("alpha1Sin");

    //    context["alpha2Cos"].SetExternalName("alpha2Cos");
    //    context["alpha2Sin"].SetExternalName("alpha2Sin");

    //    context["beta1Cos"].SetExternalName("beta1Cos");
    //    context["beta1Sin"].SetExternalName("beta1Sin");

    //    context["beta2Cos"].SetExternalName("beta2Cos");
    //    context["beta2Sin"].SetExternalName("beta2Sin");

    //    pvP1[0].SetExternalName("p1X");
    //    pvP1[1].SetExternalName("p1Y");

    //    pvP2[0].SetExternalName("p2X");
    //    pvP2[1].SetExternalName("p2Y");

    //    // Define code generated variable names for intermediate variables
    //    context.SetIntermediateExternalNamesByNameIndex(index => $"temp{index}");


    //    // Stage 6: Define a C# code composer with AngouriMath symbolic expressions converter
    //    var contextCodeComposer = context.CreateContextCodeComposer(
    //        GaFuLLanguageServerBase.CSharpFloat64()
    //    );

    //    contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = false;

    //    // Stage 7: Generate the final C# code
    //    var code = contextCodeComposer.Generate();

    //    Console.WriteLine("Generated Code:");
    //    Console.WriteLine(code);
    //    Console.WriteLine();


    //    var dotCode = contextCodeComposer.GenerateGraphVizCode();

    //    Console.WriteLine("GraphViz Code:");
    //    Console.WriteLine(dotCode);
    //    Console.WriteLine();
    //}

    public static void GenerateTrigCode()
    {
        // Stage 1: Define the meta-programming context
        // The meta-programming context is a special kind
        // of symbolic processor for code generation
        var context =
            new MetaContext()
            {
                MergeExpressions = true,
                ContextOptions =
                {
                    ContextName = "Hansen-Trig",
                    AllowGenerateComments = true,
                    PropagateConstants = true,
                    //ReUseIntermediateVariables = false
                }
            };

        // Use this if you want Wolfram Mathematica symbolic processor
        // instead of the default AngouriMath symbolic processor
        context.AttachMathematicaExpressionEvaluator();

        // Define a Euclidean multivectors processor for the context
        var processor = context.CreateEuclideanXGaProcessor();


        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as
        // scalar parts of multivectors and used for later
        // processing to compute some outputs

        var pi =
            context.ScalarFromValue(context.PiValue).CreatePolarAngleFromRadians();

        // Define constant EGA basis blades
        var e1 =
            processor.VectorTerm(0);

        var e2 =
            processor.VectorTerm(1);

        // Define input 2D position vectors of points A,B
        var xa = context["Ax"]; //context[-1];
        var ya = context["Ay"]; //context[5];
        var xb = context["Bx"]; //context[6];
        var yb = context["By"]; //context[4];
        var xab = xb - xa;
        var yab = yb - ya;

        //var pvA = xa * e1 + ya * e2;
        //var pvB = xb * e1 + yb * e2;

        // Define input angles
        var alpha1 =
            //context.CreatePolarAngleFromDegrees(44.53);
            context.CreatePolarAngle(
                context["alpha1Cos"],
                context["alpha1Sin"]
            );

        var alpha2 =
            //context.CreatePolarAngleFromDegrees(32.47);
            context.CreatePolarAngle(
                context["alpha2Cos"],
                context["alpha2Sin"]
            );


        var beta1 =
            //context.CreatePolarAngleFromDegrees(93.07);
            context.CreatePolarAngle(
                context["beta1Cos"],
                context["beta1Sin"]
            );

        var beta2 =
            //context.CreatePolarAngleFromDegrees(69.34);
            context.CreatePolarAngle(
                context["beta2Cos"],
                context["beta2Sin"]
            );


        var alpha1M = beta1 - alpha1;
        var beta1M = alpha2;
        var alpha2M = alpha1;
        var beta2M = beta2 - alpha2;


        // Stage 3: Perform algebraic computations on input parameters
        // A symbolic expression tree for elementary operations on scalar
        // components is created automatically in this stage
        var tanLambda = context.BreakMerge(
            beta1M.Sin() * (beta1M + beta2M + alpha2M).Sin() /
            ((beta1M + beta2M).Sin() * (alpha1M + alpha2M + beta1M).Sin())
        );

        //var s0 = (context.CreateScalar(context.ScalarPi) / 4 - tanLambda.ArcTan()).Tan();
        var s0 = context.BreakMerge(
            (1 - tanLambda) / (1 + tanLambda)
        );

        var s1 = context.BreakMerge(
            (s0 / alpha1M.HalfPolarAngle().Tan()).ArcTan().DoublePolarAngle()
        );

        var s2 = (pi - alpha1M).BreakMerge();

        var gamma = (s2 + s1).HalfPolarAngle().BreakMerge();
        var delta = (s2 - s1).HalfPolarAngle().BreakMerge();

        var zeroEpsilon =
            (gamma - (alpha1M + alpha2M + beta1M + beta2M)).BreakMerge();

        var theta =
            (-gamma - (pi - (alpha1M + alpha2M + beta1M))).BreakMerge();

        //var phi = 
        //    (context["By"] - context["Ay"]).ArcTan2(context["Bx"] - context["Ax"]);

        //var phi1 = phi + gamma;
        //var phi2 = phi + theta;

        //var v1 = phi1.Sin() * e1 + phi1.Cos() * e2;
        //var v2 = phi2.Sin() * e1 + phi2.Cos() * e2;
        //var ap1 = 
        //    delta.Sin() * (pvB - pvA).Norm() / alpha1m.Sin();

        //var ap2 = 
        //    (pi - theta - beta2m).Sin() * (pvB - pvA).Norm() / beta2m.Sin();

        var xp1 = context.BreakMerge(
            xa +
            delta.Sin() / alpha1M.Sin() *
            (xab * gamma.Cos() - yab * gamma.Sin())
        );

        var yp1 = context.BreakMerge(
            ya +
            delta.Sin() / alpha1M.Sin() *
            (yab * gamma.Cos() + xab * gamma.Sin())
        );

        var xp2 = context.BreakMerge(
            xa +
            zeroEpsilon.Sin() / beta2M.Sin() *
            (xab * theta.Cos() + yab * theta.Sin())
        );

        var yp2 = context.BreakMerge(
            ya +
            zeroEpsilon.Sin() / beta2M.Sin() *
            (yab * theta.Cos() - xab * theta.Sin())
        );


        // Compute final points p1, p2
        //var pvP1 = pvA + ap1 * v1;
        //var pvP2 = pvA + ap2 * v2;
        var pvP1 = (xp1 * e1 + yp1 * e2).BreakMerge();
        var pvP2 = (xp2 * e1 + yp2 * e2).BreakMerge();


        // Stage 5: Assign code generated variable names for all variables
        // Define code generated variable names for input variables
        context["Ax"].SetExternalName("data.Ax");
        context["Ay"].SetExternalName("data.Ay");

        context["Bx"].SetExternalName("data.Bx");
        context["By"].SetExternalName("data.By");

        context["alpha1Cos"].SetExternalName("alpha1Cos");
        context["alpha1Sin"].SetExternalName("alpha1Sin");

        context["alpha2Cos"].SetExternalName("alpha2Cos");
        context["alpha2Sin"].SetExternalName("alpha2Sin");

        context["beta1Cos"].SetExternalName("beta1Cos");
        context["beta1Sin"].SetExternalName("beta1Sin");

        context["beta2Cos"].SetExternalName("beta2Cos");
        context["beta2Sin"].SetExternalName("beta2Sin");

        pvP1[0].SetAsOutput("p1X");
        pvP1[1].SetAsOutput("p1Y");

        pvP2[0].SetAsOutput("p2X");
        pvP2[1].SetAsOutput("p2Y");

        // Stage 4: Optimize symbolic computations in the meta-programming context
        context.OptimizeContext();

        //TestMutations(context);

        context = MetaContextGeneticOptimizer.Process(
            new McGOptParameters()
            {
                GenerationCount = 300,
                CodeFilePath = @"D:\Projects\Study\Surveying\Hansen Problem\CGACode"
            },
            context
        );

        // Define code generated variable names for intermediate variables
        context.SetComputedExternalNamesByOrder(index => $"temp{index}");


        // Stage 6: Define a C# code composer with AngouriMath symbolic expressions converter
        var contextCodeComposer = context.CreateContextCodeComposer(
            GaFuLLanguageServerBase.CSharpFloat64()
        );

        contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = false;

        // Stage 7: Generate the final C# code
        var code = contextCodeComposer.Generate();

        Console.WriteLine("Generated Code:");
        Console.WriteLine(code);
        Console.WriteLine();


        //var dotCode = contextCodeComposer.GenerateGraphVizCode();

        //Console.WriteLine("GraphViz Code:");
        //Console.WriteLine(dotCode);
        //Console.WriteLine();
    }

    public static void GenerateComplexCode()
    {
        // Stage 1: Define the meta-programming context
        // The meta-programming context is a special kind
        // of symbolic processor for code generation
        var context =
            new MetaContext()
            {
                MergeExpressions = true,
                ContextOptions =
                {
                    ContextName = "Hansen-Complex",
                    AllowGenerateComments = true,
                    PropagateConstants = true,
                    //ReUseIntermediateVariables = false
                }
            };

        // Use this if you want Wolfram Mathematica symbolic processor
        // instead of the default AngouriMath symbolic processor
        context.AttachMathematicaExpressionEvaluator();


        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as
        // scalar parts of multivectors and used for later
        // processing to compute some outputs

        // Define constant EGA basis blades
        //var e1 = 
        //    context.CreateComplexNumber(1, 0);

        //var e2 = 
        //    context.CreateComplexNumber(0, 1);

        // Define input 2D position vectors of points A,B
        var a =
            context.CreateComplexNumber(context["Ax"], context["Ay"]);
        //context[-1] * e1 + context[5] * e2;

        var b =
            context.CreateComplexNumber(context["Bx"], context["By"]);
        //context[6] * e1 + context[4] * e2;

        // Define input angles
        var alpha1 =
            context.CreatePolarAngle(
                context["alpha1Cos"],
                context["alpha1Sin"]
            );
        //context[44.534 * Math.PI / 180];

        var alpha2 =
            context.CreatePolarAngle(
                context["alpha2Cos"],
                context["alpha2Sin"]
            );
        //context[32.471 * Math.PI / 180];

        var beta1 =
            context.CreatePolarAngle(
                context["beta1Cos"],
                context["beta1Sin"]
            );
        //context[93.066 * Math.PI / 180];

        var beta2 =
            context.CreatePolarAngle(
                context["beta2Cos"],
                context["beta2Sin"]
            );
        //context[69.341 * Math.PI / 180];


        // Stage 3: Perform algebraic computations on input parameters
        // A symbolic expression tree for elementary operations on scalar
        // components is created automatically in this stage
        var alpha0M = beta1;
        var alpha1M = alpha1;

        var beta0M = -alpha2;
        var beta1M = -beta2;


        var k0 =
            alpha0M.DoublePolarAngle().ToComplexConjugateNumber();
        //context.CreateComplexNumberUnitPolar(-2 * alpha0m);

        var k1 =
            alpha1M.DoublePolarAngle().ToComplexConjugateNumber();
        //context.CreateComplexNumberUnitPolar(-2 * alpha1m);

        var l0 =
            beta0M.DoublePolarAngle().ToComplexConjugateNumber();
        //context.CreateComplexNumberUnitPolar(-2 * beta0m);

        var l1 =
            beta1M.DoublePolarAngle().ToComplexConjugateNumber();
        //context.CreateComplexNumberUnitPolar(-2 * beta1m);

        var s0 = (1 - l0) / (k0 - l0);
        var s1 = (1 - l1) / (k1 - l1);

        //var la0 = 
        //    context.CreateComplexNumberUnitPolar(-alpha0m) * s0;

        //var la1 = 
        //    context.CreateComplexNumberUnitPolar(-alpha1m) * s1;

        //var lb0 = 
        //    context.CreateComplexNumberUnitPolar(-beta0m) * (1 - s0);

        //var lb1 = 
        //    context.CreateComplexNumberUnitPolar(-beta1m) * (1 - s1);

        // Compute final points p1, p2
        var (p1, p2) =
            context.SolveLinear2D(
                1 - s0, s0, a,
                1 - s1, s1, b
            );

        //var d0 = (1 - s0) * p1 + s0 * p2 - a;
        //var d1 = (1 - s1) * p1 + s1 * p2 - b;

        //var f0 = (a - p1) / (p2 - p1) - s0;
        //var f1 = (b - p1) / (p2 - p1) - s1;

        //var ga0 = k0 * s0 - s0.Conjugate();
        //var ga1 = k1 * s1 - s1.Conjugate();

        //var gb0 = l0 * (1 - s0) - (1 - s0.Conjugate());
        //var gb1 = l1 * (1 - s1) - (1 - s1.Conjugate());

        //var j1 = (p2 - p1) * la0 * context.CreateComplexNumberUnitPolar(alpha0m) + p1 - a;
        //var j2 = (p2 - p1) * la1 * context.CreateComplexNumberUnitPolar(alpha1m) + p1 - b;


        // Stage 5: Assign code generated variable names for all variables
        // Define code generated variable names for input variables
        context["Ax"].SetExternalName("data.Ax");
        context["Ay"].SetExternalName("data.Ay");

        context["Bx"].SetExternalName("data.Bx");
        context["By"].SetExternalName("data.By");

        context["alpha1Cos"].SetExternalName("alpha1Cos");
        context["alpha1Sin"].SetExternalName("alpha1Sin");

        context["alpha2Cos"].SetExternalName("alpha2Cos");
        context["alpha2Sin"].SetExternalName("alpha2Sin");

        context["beta1Cos"].SetExternalName("beta1Cos");
        context["beta1Sin"].SetExternalName("beta1Sin");

        context["beta2Cos"].SetExternalName("beta2Cos");
        context["beta2Sin"].SetExternalName("beta2Sin");

        p1.SetAsOutput("p1X", "p1Y");
        p2.SetAsOutput("p2X", "p2Y");

        // Stage 4: Optimize symbolic computations in the meta-programming context
        context.OptimizeContext();

        context = MetaContextGeneticOptimizer.Process(
            new McGOptParameters()
            {
                CodeFilePath = @"D:\Projects\Study\Surveying\Hansen Problem\CGACode"
            },
            context
        );

        // Define code generated variable names for intermediate variables
        context.SetComputedExternalNamesByOrder(index => $"temp{index}");


        // Stage 6: Define a C# code composer with AngouriMath symbolic expressions converter
        var contextCodeComposer = context.CreateContextCodeComposer(
            GaFuLLanguageServerBase.CSharpFloat64()
        );

        contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = false;

        // Stage 7: Generate the final C# code
        var code = contextCodeComposer.Generate();

        Console.WriteLine("Generated Code:");
        Console.WriteLine(code);
        Console.WriteLine();


        //var dotCode = contextCodeComposer.GenerateGraphVizCode();

        //Console.WriteLine("GraphViz Code:");
        //Console.WriteLine(dotCode);
        //Console.WriteLine();
    }

    public static void GenerateVGaCode()
    {
        // Stage 1: Define the meta-programming context
        // The meta-programming context is a special kind
        // of symbolic processor for code generation
        var context =
            new MetaContext()
            {
                MergeExpressions = false,
                ContextOptions =
                {
                    ContextName = "Hansen-VGA",
                    AllowGenerateComments = true,
                    PropagateConstants = true,
                    //ReUseIntermediateVariables = false
                }
            };

        // Use this if you want Wolfram Mathematica symbolic processor
        // instead of the default AngouriMath symbolic processor
        context.AttachMathematicaExpressionEvaluator();

        // Define a Euclidean multivectors processor for the context
        var processor = context.CreateEuclideanXGaProcessor();


        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as
        // scalar parts of multivectors and used for later
        // processing to compute some outputs

        // Define constant EGA basis blades
        var pi =
            context.CreatePolarAngleFromRadians(context.PiValue);

        var e1 =
            processor.VectorTerm(0);

        var e2 =
            processor.VectorTerm(1);

        var e12 =
            e1.Op(e2);

        // Define input 2D position vectors of points A,B
        var pvA =
            context["Ax"] * e1 + context["Ay"] * e2;
        //context[-1] * e1 + context[5] * e2;

        var pvB =
            context["Bx"] * e1 + context["By"] * e2;
        //context[6] * e1 + context[4] * e2;

        // Define input angles
        var alpha1 =
            context.CreatePolarAngle(
                context["alpha1Cos"],
                context["alpha1Sin"]
            ).BreakMerge();
        //context[44.534 * Math.PI / 180];

        var alpha2 =
            context.CreatePolarAngle(
                context["alpha2Cos"],
                context["alpha2Sin"]
            ).BreakMerge();
        //context[32.471 * Math.PI / 180];

        var beta1 =
            context.CreatePolarAngle(
                context["beta1Cos"],
                context["beta1Sin"]
            ).BreakMerge();
        //context[93.066 * Math.PI / 180];

        var beta2 =
            context.CreatePolarAngle(
                context["beta2Cos"],
                context["beta2Sin"]
            ).BreakMerge();
        //context[69.341 * Math.PI / 180];



        // Stage 3: Perform algebraic computations on input parameters
        // A symbolic expression tree for elementary operations on scalar
        // components is created automatically in this stage

        var s1 =
            (alpha2.Sin() / (alpha2 + beta1).Sin()).BreakMerge();

        var s2 =
            (beta1.Sin() / (beta1 + alpha2).Sin()).BreakMerge();

        var s3 =
            (alpha1.Sin() / (alpha1 + beta2).Sin()).BreakMerge();

        //var s4 = 
        //    beta2.Sin() / (beta2 + alpha1).Sin();

        // Compute lambda, theta
        var lambda =
            (1 / (s2.Square() + s3.Square() - 2 * s2 * s3 * (beta2 - alpha2).Cos()).Sqrt()).BreakMerge();

        var theta =
            context.CreatePolarAngle(
                lambda * (s2 - s3 * (alpha2 - beta2).Cos()),
                lambda * s3 * (alpha2 - beta2).Sin()
            ).BreakMerge();

        var omega =
            (alpha2 + beta1 - pi).BreakMerge();

        var gamma =
            (theta + omega).BreakMerge();

        // Compute final points p1, p2
        var d = pvB - pvA;

        var r1 =
            gamma.Cos() + gamma.Sin() * e12;

        var r2 =
            theta.Cos() + theta.Sin() * e12;

        var pvP1 =
            pvA + (lambda * s1 * d.Gp(r1)).GetVectorPart().BreakMerge();

        var pvP2 =
            pvA + (lambda * s2 * d.Gp(r2)).GetVectorPart().BreakMerge();


        // Stage 5: Assign code generated variable names for all variables
        // Define code generated variable names for input variables
        context["Ax"].SetExternalName("data.Ax");
        context["Ay"].SetExternalName("data.Ay");

        context["Bx"].SetExternalName("data.Bx");
        context["By"].SetExternalName("data.By");

        context["alpha1Cos"].SetExternalName("alpha1Cos");
        context["alpha1Sin"].SetExternalName("alpha1Sin");

        context["alpha2Cos"].SetExternalName("alpha2Cos");
        context["alpha2Sin"].SetExternalName("alpha2Sin");

        context["beta1Cos"].SetExternalName("beta1Cos");
        context["beta1Sin"].SetExternalName("beta1Sin");

        context["beta2Cos"].SetExternalName("beta2Cos");
        context["beta2Sin"].SetExternalName("beta2Sin");

        pvP1[0].SetAsOutput("p1X");
        pvP1[1].SetAsOutput("p1Y");

        pvP2[0].SetAsOutput("p2X");
        pvP2[1].SetAsOutput("p2Y");

        // Stage 4: Optimize symbolic computations in the meta-programming context
        context.OptimizeContext();

        context = MetaContextGeneticOptimizer.Process(
            new McGOptParameters()
            {
                GenerationCount = 300,
                CodeFilePath = @"D:\Projects\Study\Surveying\Hansen Problem\CGACode"
            },
            context
        );

        // Define code generated variable names for intermediate variables
        context.SetComputedExternalNamesByOrder(index => $"temp{index}");

        // Stage 6: Define a C# code composer with AngouriMath symbolic expressions converter
        var contextCodeComposer = context.CreateContextCodeComposer(
            GaFuLLanguageServerBase.CSharpFloat64()
        );

        contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = false;

        // Stage 7: Generate the final C# code
        var code = contextCodeComposer.Generate();

        Console.WriteLine("Generated Code:");
        Console.WriteLine(code);
        Console.WriteLine();


        //var dotCode = contextCodeComposer.GenerateGraphVizCode();

        //Console.WriteLine("GraphViz Code:");
        //Console.WriteLine(dotCode);
        //Console.WriteLine();
    }

    public static void GenerateCGaCode1()
    {
        // Stage 1: Define the meta-programming context
        // The meta-programming context is a special kind
        // of symbolic processor for code generation
        var context =
            new MetaContext()
            {
                MergeExpressions = true,
                ContextOptions =
                {
                    ContextName = "CGA",
                    AllowGenerateComments = true,
                    PropagateConstants = true,
                    //ReUseIntermediateVariables = false
                }
            };

        // Use this if you want Wolfram Mathematica symbolic processor
        // instead of the default AngouriMath symbolic processor
        context.AttachMathematicaExpressionEvaluator();

        // Define a Euclidean multivectors processor for the context
        var processor = context.CreateConformalXGaProcessor();


        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as
        // scalar parts of multivectors and used for later
        // processing to compute some outputs

        // Define constant CGA basis blades
        var en = processor.VectorTerm(0);
        var ep = processor.VectorTerm(1);
        var e1 = processor.VectorTerm(2);
        var e2 = processor.VectorTerm(3);
        var eo = (en + ep) / 2;
        var ei = en - ep;
        var e12 = e1.Op(e2);
        var eoi = eo.Op(ei);
        var cgaI = e1.Op(e2).Op(ep).Op(en);
        var cgaIi = cgaI.Inverse();

        // Define input 2D position vectors of points A,B
        var xa = context[-1]; //context["Ax"];
        var ya = context[5]; //context["Ay"];
        var xb = context[6]; //context["Bx"];
        var yb = context[4]; //context["By"];

        var pvA = xa * e1 + ya * e2;
        var pvB = xb * e1 + yb * e2;

        // Define input angles
        var alpha1 =
            context.CreatePolarAngleFromDegrees(44.53);
        //context.CreatePolarAngle(
        //    context["alpha1Cos"],
        //    context["alpha1Sin"]
        //);

        var alpha2 =
            context.CreatePolarAngleFromDegrees(32.47);
        //context.CreatePolarAngle(
        //    context["alpha2Cos"],
        //    context["alpha2Sin"]
        //);


        var beta1 =
            context.CreatePolarAngleFromDegrees(93.07);
        //context.CreatePolarAngle(
        //    context["beta1Cos"],
        //    context["beta1Sin"]
        //);

        var beta2 =
            context.CreatePolarAngleFromDegrees(69.34);
        //context.CreatePolarAngle(
        //    context["beta2Cos"],
        //    context["beta2Sin"]
        //);

        //var alpha1m = alpha1;
        //var beta1m = -beta1;
        //var alpha2m = alpha2;
        //var beta2m = -beta2;

        // Stage 3: Perform algebraic computations on input parameters
        // A symbolic expression tree for elementary operations on scalar
        // components is created automatically in this stage
        var a = eo + pvA + pvA.NormSquared() / 2 * ei;
        var b = eo + pvB + pvB.NormSquared() / 2 * ei;

        var l = a.Op(b).Op(ei).Gp(cgaIi);
        var v = (l + l.Sp(eo).Gp(ei)).GetVectorPart();

        var aa1 = alpha1.HalfPolarAngle().NegativeAngle();
        var ab1 = beta1.HalfPolarAngle().NegativeAngle();
        var aa2 = alpha2.HalfPolarAngle().NegativeAngle();
        var ab2 = beta2.HalfPolarAngle().NegativeAngle();

        var ra1 = aa1.Cos() + aa1.Sin() * e12;
        var rb1 = ab1.Cos() + ab1.Sin() * e12;
        var ra2 = aa2.Cos() + aa2.Sin() * e12;
        var rb2 = ab2.Cos() + ab2.Sin() * e12;

        var va1 = ra1.Gp(v).Gp(ra1.Reverse()).GetVectorPart();
        var vb1 = rb1.Gp(v).Gp(rb1.Reverse()).GetVectorPart();
        var va2 = ra2.Gp(v).Gp(ra2.Reverse()).GetVectorPart();
        var vb2 = rb2.Gp(v).Gp(rb2.Reverse()).GetVectorPart();

        var la1 = va1.Gp(e12) - a.Op(va1).Gp(e12.Gp(ei));
        var lb1 = vb1.Gp(e12) - b.Op(vb1).Gp(e12.Gp(ei));
        var la2 = va2.Gp(e12) - a.Op(va2).Gp(e12.Gp(ei));
        var lb2 = vb2.Gp(e12) - b.Op(vb2).Gp(e12.Gp(ei));

        var flatE = la1.Op(lb1).Gp(cgaIi).GetBivectorPart();
        var flatF = la2.Op(lb2).Gp(cgaIi).GetBivectorPart();
        var e = -flatE.Op(eo).Fdp(eoi);
        var f = -flatF.Op(eo).Fdp(eoi);

        var c1 = a.Op(b).Op(e);
        var c2 = a.Op(b).Op(f);

        var lef = e.Op(f).Op(ei);

        var pp1 = lef.Gp(cgaIi).Op(c1.Gp(cgaIi)).Gp(cgaIi);
        var pp2 = lef.Gp(cgaIi).Op(c2.Gp(cgaIi)).Gp(cgaIi);

        var m1 = (1 + pp1 / pp1.Sp(pp1.Reverse()).Sqrt()) / 2;
        var m2 = (1 + pp2 / pp2.Sp(pp1.Reverse()).Sqrt()) / 2;

        var p12 = m1.Gp(pp1.Fdp(ei)).Gp(m1.Reverse()).GetVectorPart();
        var p22 = m2.Gp(pp2.Fdp(ei)).Gp(m2.Reverse()).GetVectorPart();

        var pvP1 = p12.GetVectorPart(i => i >= 2);
        var pvP2 = p22.GetVectorPart(i => i >= 2);


        // Stage 5: Assign code generated variable names for all variables
        // Define code generated variable names for input variables
        context["Ax"].SetExternalName("data.Ax");
        context["Ay"].SetExternalName("data.Ay");

        context["Bx"].SetExternalName("data.Bx");
        context["By"].SetExternalName("data.By");

        context["alpha1Cos"].SetExternalName("alpha1Cos");
        context["alpha1Sin"].SetExternalName("alpha1Sin");

        context["alpha2Cos"].SetExternalName("alpha2Cos");
        context["alpha2Sin"].SetExternalName("alpha2Sin");

        context["beta1Cos"].SetExternalName("beta1Cos");
        context["beta1Sin"].SetExternalName("beta1Sin");

        context["beta2Cos"].SetExternalName("beta2Cos");
        context["beta2Sin"].SetExternalName("beta2Sin");

        pvP1[2].SetAsOutput("p1X");
        pvP1[3].SetAsOutput("p1Y");

        pvP2[2].SetAsOutput("p2X");
        pvP2[3].SetAsOutput("p2Y");

        // Stage 4: Optimize symbolic computations in the meta-programming context
        context.OptimizeContext();

        // Define code generated variable names for intermediate variables
        context.SetComputedExternalNamesByOrder(index => $"temp{index}");


        // Stage 6: Define a C# code composer with AngouriMath symbolic expressions converter
        var contextCodeComposer = context.CreateContextCodeComposer(
            GaFuLLanguageServerBase.CSharpFloat64()
        );

        contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = false;

        // Stage 7: Generate the final C# code
        var code = contextCodeComposer.Generate();

        Console.WriteLine("Generated Code:");
        Console.WriteLine(code);
        Console.WriteLine();


        var dotCode = contextCodeComposer.GenerateGraphVizCode();

        Console.WriteLine("GraphViz Code:");
        Console.WriteLine(dotCode);
        Console.WriteLine();
    }

    private static void TestMutations(MetaContext context)
    {
        var parameters = new McGOptParameters();

        var cost = McGOptCostFunction.ComputationsCount.ComputeCost(context);

        Console.WriteLine(cost);

        for (var i = 0; i < 25; i++)
        {
            var newContext = McGOptMutation.Simple.ApplyMutation(parameters, context);

            Console.WriteLine(newContext.ComputationsCount);
        }
    }

    public static void GenerateCGaCode()
    {
        // Stage 1: Define the meta-programming context
        // The meta-programming context is a special kind
        // of symbolic processor for code generation
        var context =
            new MetaContext()
            {
                MergeExpressions = false,
                ContextOptions =
                {
                    ContextName = "CGA",
                    AllowGenerateComments = true,
                    PropagateConstants = true,
                    //ReUseIntermediateVariables = false
                }
            };

        // Use this if you want Wolfram Mathematica symbolic processor
        // instead of the default AngouriMath symbolic processor
        context.AttachMathematicaExpressionEvaluator();

        // Define a Euclidean multivectors processor for the context
        var cgaSpace = context.CreateCGaGeometricSpace4D();
        var cgaProcessor = cgaSpace.ConformalProcessor;
        var scalarProcessor = cgaSpace.ScalarProcessor;

        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as
        // scalar parts of multivectors and used for later
        // processing to compute some outputs

        // Define input 2D position vectors of points A,B
        var xa = context["Ax"]; //context[-4];
        var ya = context["Ay"]; //context[2];
        var xb = context["Bx"]; //context[10];
        var yb = context["By"]; //context[6];

        // 2D Euclidean Position vector for points A,B
        var pvA = scalarProcessor.Vector2D(xa.ScalarValue, ya.ScalarValue).BreakMerge();
        var pvB = scalarProcessor.Vector2D(xb.ScalarValue, yb.ScalarValue).BreakMerge();

        // Define input angles
        var alpha1 =
            context.CreatePolarAngle(
                context["alpha1Cos"],
                context["alpha1Sin"]
            ).BreakMerge();
        //context.CreatePolarAngleFromDegrees(311.63);

        var alpha2 =
            context.CreatePolarAngle(
                context["alpha2Cos"],
                context["alpha2Sin"]
            ).BreakMerge();
        //context.CreatePolarAngleFromDegrees(66.8);

        var beta1 =
            context.CreatePolarAngle(
                context["beta1Cos"],
                context["beta1Sin"]
            ).BreakMerge();
        //context.CreatePolarAngleFromDegrees(46.4);

        var beta2 =
            context.CreatePolarAngle(
                context["beta2Cos"],
                context["beta2Sin"]
            ).BreakMerge();
        //context.CreatePolarAngleFromDegrees(305.84);


        // Encode points A, B as IPNS point null vectors a,b
        var a =
            cgaSpace.Encode.Point(xa, ya).BreakMerge();

        var b =
            cgaSpace.Encode.Point(xb, yb).BreakMerge();

        // Encode line passing through A,B as CGA flat line blade l
        var l =
            cgaSpace.Encode.IpnsFlat.LineFromPoints(pvA, pvB).BreakMerge();

        var bvA =
            cgaSpace.EncodeIpnsFlat.Point(pvA).InternalBivector.BreakMerge();

        var bvB =
            cgaSpace.EncodeIpnsFlat.Point(pvB).InternalBivector.BreakMerge();


        var (halfAlpha1Cos, halfAlpha1Sin) =
            (-alpha1).HalfPolarAngle().BreakMerge();

        var rA1 =
            (halfAlpha1Cos + halfAlpha1Sin / bvA.Norm() * bvA).ToConformalCGaRotor(cgaSpace).BreakMerge();
        //cgaSpace.EncodeCGaRotation(-alpha1, pvA);


        var (halfBeta1Cos, halfBeta1Sin) =
            (-beta1).HalfPolarAngle().BreakMerge();

        var rB1 =
            (halfBeta1Cos + halfBeta1Sin / bvB.Norm() * bvB).ToConformalCGaRotor(cgaSpace).BreakMerge();
        //cgaSpace.EncodeCGaRotation(-beta1, pvB);


        var (halfAlpha2Cos, halfAlpha2Sin) =
            alpha2.HalfPolarAngle().BreakMerge();

        var rB2 =
            (halfAlpha2Cos + halfAlpha2Sin / bvB.Norm() * bvB).ToConformalCGaRotor(cgaSpace).BreakMerge();
        //cgaSpace.EncodeCGaRotation(alpha2, pvB);


        var (halfBeta2Cos, halfBeta2Sin) =
            beta2.HalfPolarAngle().BreakMerge();

        var rA2 =
            (halfBeta2Cos + halfBeta2Sin / bvA.Norm() * bvA).ToConformalCGaRotor(cgaSpace).BreakMerge();
        //cgaSpace.EncodeCGaRotation(beta2, pvA);

        //context.MergeExpressions = false;

        // Rotate line l around points A,B with angles -alpha1, -beta1
        var lA1 = rA1.MapBlade(l).BreakMerge();
        var lB1 = rB1.MapBlade(l).BreakMerge();

        // Rotate line l around points A, B with angles beta2, alpha2
        var lA2 = rA2.MapBlade(l).BreakMerge();
        var lB2 = rB2.MapBlade(l).BreakMerge();

        // Point e is the intersection of lA1 and lB1
        var e =
            cgaSpace.EncodeIpnsRound.Point(
                lA1.MeetIpns(lB1).Decode.IpnsFlat.VGaPosition().Decode.VGaDirection.Vector2D().BreakMerge()
            ).BreakMerge();

        // Point f is the intersection of lA2 and lB2
        var f =
            cgaSpace.EncodeIpnsRound.Point(
                lA2.MeetIpns(lB2).Decode.IpnsFlat.VGaPosition().Decode.VGaDirection.Vector2D().BreakMerge()
            ).BreakMerge();

        //context.MergeExpressions = true;

        // Line lef passing through points e,f
        var lef = e.Op(f).Op(cgaSpace.Ei).BreakMerge();

        // Circle c1 passing through a,b,e
        var c1 = a.Op(b).Op(e).BreakMerge();

        // Circle c2 passing through a,b,f
        var c2 = a.Op(b).Op(f).BreakMerge();

        // Intersect c1 with lef to get a point pair blade
        var pp1 = c1.MeetOpns(lef).BreakMerge();

        // Intersect c2 with lef to get a point pair blade
        var pp2 = c2.MeetOpns(lef).BreakMerge();

        var m1 =
            pp1.Decode.IpnsRound.VGaCenter().Decode.VGaDirection.Vector2D().BreakMerge();

        var m2 =
            pp2.Decode.IpnsRound.VGaCenter().Decode.VGaDirection.Vector2D().BreakMerge();

        var pvP1 = (2 * m1 - e.Decode.IpnsRound.CircleVGaCenter2D()).BreakMerge();
        var pvP2 = (2 * m2 - f.Decode.IpnsRound.CircleVGaCenter2D()).BreakMerge();


        // Stage 5: Assign code generated variable names for all variables
        // Define code generated variable names for input variables
        context["Ax"].SetExternalName("data.Ax");
        context["Ay"].SetExternalName("data.Ay");

        context["Bx"].SetExternalName("data.Bx");
        context["By"].SetExternalName("data.By");

        context["alpha1Cos"].SetExternalName("alpha1Cos");
        context["alpha1Sin"].SetExternalName("alpha1Sin");

        context["alpha2Cos"].SetExternalName("alpha2Cos");
        context["alpha2Sin"].SetExternalName("alpha2Sin");

        context["beta1Cos"].SetExternalName("beta1Cos");
        context["beta1Sin"].SetExternalName("beta1Sin");

        context["beta2Cos"].SetExternalName("beta2Cos");
        context["beta2Sin"].SetExternalName("beta2Sin");

        pvP1.SetAsOutput("p1X", "p1Y");
        pvP2.SetAsOutput("p2X", "p2Y");

        halfAlpha1Cos.SetAsOutput("halfAlpha1Cos");
        halfAlpha1Sin.SetAsOutput("halfAlpha1Sin");
        halfAlpha2Cos.SetAsOutput("halfAlpha2Cos");
        halfAlpha2Sin.SetAsOutput("halfAlpha2Sin");

        halfBeta1Cos.SetAsOutput("halfBeta1Cos");
        halfBeta1Sin.SetAsOutput("halfBeta1Sin");
        halfBeta2Cos.SetAsOutput("halfBeta2Cos");
        halfBeta2Sin.SetAsOutput("halfBeta2Sin");

        context.UpdateDependencyData(true);
        Console.WriteLine(context.ToString());
        Console.WriteLine();

        // Stage 4: Optimize symbolic computations in the meta-programming context
        context.OptimizeContext();

        //context = MetaContextGeneticOptimizer.Process(
        //    new McGOptParameters()
        //    {
        //        GenerationCount = 300,
        //        CodeFilePath = @"D:\Projects\Study\Surveying\Hansen Problem\CGACode"
        //    },
        //    context
        //);

        // Define code generated variable names for intermediate variables
        context.SetComputedExternalNamesByOrder(index => $"temp{index}");


        // Stage 6: Define a C# code composer with AngouriMath symbolic expressions converter
        var contextCodeComposer = context.CreateContextCodeComposer(
            GaFuLLanguageServerBase.CSharpFloat64()
        );

        contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = false;

        // Stage 7: Generate the final C# code
        var code = contextCodeComposer.Generate();

        Console.WriteLine(code);
        Console.WriteLine();


        //var dotCode = contextCodeComposer.GenerateGraphVizCode();

        //Console.WriteLine("GraphViz Code:");
        //Console.WriteLine(dotCode);
        //Console.WriteLine();
    }
    
    public static void GenerateCGaCodeOpt()
    {
        // Stage 1: Define the meta-programming context
        // The meta-programming context is a special kind
        // of symbolic processor for code generation
        var context =
            new MetaContext()
            {
                MergeExpressions = false,
                ContextOptions =
                {
                    ContextName = "CGA",
                    AllowGenerateComments = true,
                    PropagateConstants = true,
                    //ReUseIntermediateVariables = false
                }
            };

        // Use this if you want Wolfram Mathematica symbolic processor
        // instead of the default AngouriMath symbolic processor
        context.AttachMathematicaExpressionEvaluator();

        // Define a Euclidean multivectors processor for the context
        var cgaSpace = context.CreateCGaGeometricSpace4D();
        var cgaProcessor = cgaSpace.ConformalProcessor;
        var scalarProcessor = cgaSpace.ScalarProcessor;

        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as
        // scalar parts of multivectors and used for later
        // processing to compute some outputs

        // Define input 2D position vectors of points A,B
        var xa = context["Ax"]; //context[-4];
        var ya = context["Ay"]; //context[2];
        var xb = context["Bx"]; //context[10];
        var yb = context["By"]; //context[6];

        // 2D Euclidean Position vector for points A,B
        var pvA = scalarProcessor.Vector2D(xa.ScalarValue, ya.ScalarValue);
        var pvB = scalarProcessor.Vector2D(xb.ScalarValue, yb.ScalarValue);

        // Define input angles
        var alpha1 =
            context.CreatePolarAngle(
                context["alpha1Cos"],
                context["alpha1Sin"]
            );
        //context.CreatePolarAngleFromDegrees(311.63);

        var alpha2 =
            context.CreatePolarAngle(
                context["alpha2Cos"],
                context["alpha2Sin"]
            );
        //context.CreatePolarAngleFromDegrees(66.8);

        var beta1 =
            context.CreatePolarAngle(
                context["beta1Cos"],
                context["beta1Sin"]
            );
        //context.CreatePolarAngleFromDegrees(46.4);

        var beta2 =
            context.CreatePolarAngle(
                context["beta2Cos"],
                context["beta2Sin"]
            );
        //context.CreatePolarAngleFromDegrees(305.84);


        // Encode points A, B as IPNS point null vectors a,b
        var a =
            cgaSpace.Encode.Point(xa, ya);

        var b =
            cgaSpace.Encode.Point(xb, yb);

        // Encode line passing through A,B as CGA flat line blade l
        var l =
            cgaSpace.Encode.IpnsFlat.LineFromPoints(pvA, pvB);

        var bvA =
            cgaSpace.EncodeIpnsFlat.Point(pvA).InternalBivector;

        var bvB =
            cgaSpace.EncodeIpnsFlat.Point(pvB).InternalBivector;


        var (halfAlpha1Cos, halfAlpha1Sin) =
            (-alpha1).HalfPolarAngle();

        var rA1 =
            (halfAlpha1Cos + halfAlpha1Sin / bvA.Norm() * bvA).ToConformalCGaRotor(cgaSpace);
        //cgaSpace.EncodeCGaRotation(-alpha1, pvA);


        var (halfBeta1Cos, halfBeta1Sin) =
            (-beta1).HalfPolarAngle();

        var rB1 =
            (halfBeta1Cos + halfBeta1Sin / bvB.Norm() * bvB).ToConformalCGaRotor(cgaSpace);
        //cgaSpace.EncodeCGaRotation(-beta1, pvB);


        var (halfAlpha2Cos, halfAlpha2Sin) =
            alpha2.HalfPolarAngle();

        var rB2 =
            (halfAlpha2Cos + halfAlpha2Sin / bvB.Norm() * bvB).ToConformalCGaRotor(cgaSpace);
        //cgaSpace.EncodeCGaRotation(alpha2, pvB);


        var (halfBeta2Cos, halfBeta2Sin) =
            beta2.HalfPolarAngle();

        var rA2 =
            (halfBeta2Cos + halfBeta2Sin / bvA.Norm() * bvA).ToConformalCGaRotor(cgaSpace);
        //cgaSpace.EncodeCGaRotation(beta2, pvA);

        //context.MergeExpressions = false;

        // Rotate line l around points A,B with angles -alpha1, -beta1
        var lA1 = rA1.MapBlade(l);
        var lB1 = rB1.MapBlade(l);

        // Rotate line l around points A, B with angles beta2, alpha2
        var lA2 = rA2.MapBlade(l);
        var lB2 = rB2.MapBlade(l);

        // Point e is the intersection of lA1 and lB1
        var e =
            cgaSpace.EncodeIpnsRound.Point(
                lA1.MeetIpns(lB1).Decode.IpnsFlat.VGaPosition().Decode.VGaDirection.Vector2D()
            );

        // Point f is the intersection of lA2 and lB2
        var f =
            cgaSpace.EncodeIpnsRound.Point(
                lA2.MeetIpns(lB2).Decode.IpnsFlat.VGaPosition().Decode.VGaDirection.Vector2D()
            );

        //context.MergeExpressions = true;

        // Line lef passing through points e,f
        var lef = e.Op(f).Op(cgaSpace.Ei);

        // Circle c1 passing through a,b,e
        var c1 = a.Op(b).Op(e);

        // Circle c2 passing through a,b,f
        var c2 = a.Op(b).Op(f);

        // Intersect c1 with lef to get a point pair blade
        var pp1 = c1.MeetOpns(lef);

        // Intersect c2 with lef to get a point pair blade
        var pp2 = c2.MeetOpns(lef);

        var m1 =
            pp1.Decode.IpnsRound.VGaCenter().Decode.VGaDirection.Vector2D();

        var m2 =
            pp2.Decode.IpnsRound.VGaCenter().Decode.VGaDirection.Vector2D();

        var pvP1 = (2 * m1 - e.Decode.IpnsRound.CircleVGaCenter2D());
        var pvP2 = (2 * m2 - f.Decode.IpnsRound.CircleVGaCenter2D());


        // Stage 5: Assign code generated variable names for all variables
        // Define code generated variable names for input variables
        context["Ax"].SetExternalName("data.Ax");
        context["Ay"].SetExternalName("data.Ay");

        context["Bx"].SetExternalName("data.Bx");
        context["By"].SetExternalName("data.By");

        context["alpha1Cos"].SetExternalName("alpha1Cos");
        context["alpha1Sin"].SetExternalName("alpha1Sin");

        context["alpha2Cos"].SetExternalName("alpha2Cos");
        context["alpha2Sin"].SetExternalName("alpha2Sin");

        context["beta1Cos"].SetExternalName("beta1Cos");
        context["beta1Sin"].SetExternalName("beta1Sin");

        context["beta2Cos"].SetExternalName("beta2Cos");
        context["beta2Sin"].SetExternalName("beta2Sin");

        pvP1.SetAsOutput("p1X", "p1Y");
        pvP2.SetAsOutput("p2X", "p2Y");

        halfAlpha1Cos.SetAsOutput("halfAlpha1Cos");
        halfAlpha1Sin.SetAsOutput("halfAlpha1Sin");
        halfAlpha2Cos.SetAsOutput("halfAlpha2Cos");
        halfAlpha2Sin.SetAsOutput("halfAlpha2Sin");

        halfBeta1Cos.SetAsOutput("halfBeta1Cos");
        halfBeta1Sin.SetAsOutput("halfBeta1Sin");
        halfBeta2Cos.SetAsOutput("halfBeta2Cos");
        halfBeta2Sin.SetAsOutput("halfBeta2Sin");

        //context.UpdateDependencyData(true);
        //Console.WriteLine(context.ToString());
        //Console.WriteLine();

        // Stage 4: Optimize symbolic computations in the meta-programming context
        context.OptimizeContext();

        //for (var i = 0; i < 10; i++)
        //{
        //    var oldCost = context.ComputationsCount;

        //    var costVarIndexDictionary = context.OptimizeContextLoop(20);

        //    context = context.GetContextCopy();

        //    var intermediateVars = 
        //        context.GetIntermediateVariablesList();

        //    var intermediateVarIndexSet = 
        //        intermediateVars.GetIntermediateDependencyIndexSet(costVarIndexDictionary, 2);
            
        //    context.RemoveIntermediateVariables(intermediateVarIndexSet);

        //    context.OptimizeContext();

        //    var newCost = context.ComputationsCount;

        //    var intermediateVarIndexSetText = 
        //        intermediateVarIndexSet
        //            .Select(j => j.ToString())
        //            .Concatenate(", ", "[", "]");

        //    Console.WriteLine();
        //    Console.WriteLine("Removing intermediate variables: " + intermediateVarIndexSetText);
        //    Console.WriteLine("   Cost    = " + newCost);
        //    Console.WriteLine("   Fitness = " + (oldCost - newCost));
        //    Console.WriteLine();
        //}

        //context = context.OptimizeContextGenetic();

        context = MetaContextGeneticOptimizer.Process(
            new McGOptParameters()
            {
                GenerationCount = 1000,
                CodeFilePath = @"D:\Projects\Study\Surveying\Hansen Problem\CGACode"
            },
            context
        );

        // Define code generated variable names for intermediate variables
        context.SetComputedExternalNamesByOrder(index => $"temp{index}");


        // Stage 6: Define a C# code composer with AngouriMath symbolic expressions converter
        var contextCodeComposer = context.CreateContextCodeComposer(
            GaFuLLanguageServerBase.CSharpFloat64()
        );

        contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = false;

        // Stage 7: Generate the final C# code
        var code = contextCodeComposer.Generate();

        Console.WriteLine(code);
        Console.WriteLine();


        //var dotCode = contextCodeComposer.GenerateGraphVizCode();

        //Console.WriteLine("GraphViz Code:");
        //Console.WriteLine(dotCode);
        //Console.WriteLine();
    }
}