using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.MetaProgramming;

public static class GaalopComparisonSamples
{
    public static void GetSphereCenterExample()
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
                    PropagateConstants = true
                }
            };

        // Use this if you want Wolfram Mathematica symbolic processor
        // instead of the default AngouriMath symbolic processor
        context.AttachMathematicaExpressionEvaluator();

        // Define a Euclidean multivectors processor for the context
        var cgaSpace = context.CreateConformalXGaSpace5D();
        var scalarProcessor = cgaSpace.ScalarProcessor;

        // CGA Basis vectors
        var eo = cgaSpace.Eo;
        var e1 = cgaSpace.E1;
        var e2 = cgaSpace.E2;
        var e3 = cgaSpace.E3;
        var ei = cgaSpace.Ei;

        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as
        // scalar parts of multivectors and used for later
        // processing to compute some outputs

        // Define input 3D position vectors of points 1,2,3,4
        var l1 = context["l1"];
        var x1 = context["x1"];
        var y1 = context["y1"];
        var z1 = context["z1"];

        var l2 = context["l2"];
        var x2 = context["x2"];
        var y2 = context["y2"];
        var z2 = context["z2"];

        var l3 = context["l3"];
        var x3 = context["x3"];
        var y3 = context["y3"];
        var z3 = context["z3"];

        var l4 = context["l4"];
        var x4 = context["x4"];
        var y4 = context["y4"];
        var z4 = context["z4"];

        
        // Stage 3: Perform algebraic computations on input parameters
        // A symbolic expression tree for elementary operations on scalar
        // components is created automatically in this stage

        // Encode points as IPNS null vectors
        var p1 =
            eo + x1 * e1 + y1 * e2 + z1 * e3 + l1 * ei;

        var p2 =
            eo + x2 * e1 + y2 * e2 + z2 * e3 + l2 * ei;

        var p3 =
            eo + x3 * e1 + y3 * e2 + z3 * e3 + l3 * ei;

        var p4 =
            eo + x4 * e1 + y4 * e2 + z4 * e3 + l4 * ei;
        
        // Encode IPNS sphere passing through the 4 points
        var ipnsSphere =
            p1.Op(p2).Op(p3).Op(p4).CGaDual();

        ipnsSphere /= ipnsSphere[0];

        // Decode center of sphere as a 3D vector
        var center =
            scalarProcessor.Vector3D(
                ipnsSphere[2].ScalarValue,
                ipnsSphere[3].ScalarValue,
                ipnsSphere[4].ScalarValue
            );


        // Stage 4: Assign target variable names for final
        // code generation and optimize internal expression tree

        // Define code generated variable names for input scalar variables
        context["l1"].SetExternalName("p1I");
        context["x1"].SetExternalName("p1X");
        context["y1"].SetExternalName("p1Y");
        context["z1"].SetExternalName("p1Z");

        context["l2"].SetExternalName("p2I");
        context["x2"].SetExternalName("p2X");
        context["y2"].SetExternalName("p2Y");
        context["z2"].SetExternalName("p2Z");

        context["l3"].SetExternalName("p3I");
        context["x3"].SetExternalName("p3X");
        context["y3"].SetExternalName("p3Y");
        context["z3"].SetExternalName("p3Z");

        context["l4"].SetExternalName("p4I");
        context["x4"].SetExternalName("p4X");
        context["y4"].SetExternalName("p4Y");
        context["z4"].SetExternalName("p4Z");

        // Define code generated variable names for output scalar variables
        center.SetAsOutput(
            "center[1]",
            "center[2]",
            "center[3]"
        );

        // Optimize symbolic computations in the meta-programming context
        context.OptimizeContext();

        // You can also use genetic programming for more optimization
        //context = context.OptimizeContext(
        //    new McGOptParameters()
        //    {
        //        GenerationCount = 50,
        //        CodeFilePath = @"D:\Projects\Study\Surveying\Hansen Problem\CGACode"
        //    }
        //);
        
        // Define code generated variable names for intermediate scalar variables
        context.SetComputedExternalNamesByOrder(index => $"temp{index}");


        // Stage 5: Define a target language code composer and generate final code
        var contextCodeComposer = context.CreateContextCodeComposer(
            GaFuLLanguageServerBase.CSharpFloat64()
        );

        contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = false;

        var code = contextCodeComposer.Generate();

        Console.WriteLine("Generated Code:");
        Console.WriteLine(code);
        Console.WriteLine();


        //var dotCode = contextCodeComposer.GenerateGraphVizCode();

        //Console.WriteLine("GraphViz Code:");
        //Console.WriteLine(dotCode);
        //Console.WriteLine();
    }

    public static void Get3SphereIntersectionExample()
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
        var cgaSpace = context.CreateConformalXGaSpace5D();
        var cgaProcessor = cgaSpace.ConformalProcessor;
        var scalarProcessor = cgaSpace.ScalarProcessor;

        var eo = cgaSpace.Eo.InternalVector;
        var e1 = cgaSpace.E1.InternalVector;
        var e2 = cgaSpace.E2.InternalVector;
        var e3 = cgaSpace.E3.InternalVector;
        var ei = cgaSpace.Ei.InternalVector;

        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as
        // scalar parts of multivectors and used for later
        // processing to compute some outputs

        // Define input 3D position vectors of points 1,2,3,4
        var a1 = context["a1"];
        var a2 = context["a2"];
        var a3 = context["a3"];
        var d14 = context["d14"];

        var b1 = context["b1"];
        var b2 = context["b2"];
        var b3 = context["b3"];
        var d24 = context["d24"];

        var c1 = context["c1"];
        var c2 = context["c2"];
        var c3 = context["c3"];
        var d34 = context["d34"];


        // Encode points as IPNS null vectors
        var x1 =
            cgaSpace.EncodeIpnsRoundPoint(a1, a2, a3).InternalVector;

        var x2 =
            cgaSpace.EncodeIpnsRoundPoint(b1, b2, b3).InternalVector;

        var x3 =
            cgaSpace.EncodeIpnsRoundPoint(c1, c2, c3).InternalVector;

        var s1 = x1 - 0.5 * (d14 * d14) * ei;
        var s2 = x2 - 0.5 * (d24 * d24) * ei;
        var s3 = x3 - 0.5 * (d34 * d34) * ei;

        var pp4 = s1.Op(s2).Op(s3);
        var pp4Dual = pp4.Lcp(cgaSpace.IcInv.InternalKVector).GetBivectorPart();

        var x4A = -(pp4Dual - pp4Dual.Sp(pp4Dual).Sqrt()).Gp(ei.Lcp(pp4Dual).Inverse()).GetVectorPart();
        var x4B = -(pp4Dual + pp4Dual.Sp(pp4Dual).Sqrt()).Gp(ei.Lcp(pp4Dual).Inverse()).GetVectorPart();


        // Stage 4: Assign target variable names for final
        // code generation and optimize internal expression tree
        
        // Define code generated variable names for input scalar variables
        a1.SetExternalName("a1");
        a2.SetExternalName("a2");
        a3.SetExternalName("a3");

        b1.SetExternalName("b1");
        b2.SetExternalName("b2");
        b3.SetExternalName("b3");

        c1.SetExternalName("c1");
        c2.SetExternalName("c2");
        c3.SetExternalName("c3");

        // Define code generated variable names for output scalar variables
        x1.SetAsOutputByTermId(id => $"x1[{id}]");

        s1.SetAsOutputByTermId(id => $"S1[{id}]");
        s2.SetAsOutputByTermId(id => $"S2[{id}]");
        s3.SetAsOutputByTermId(id => $"S3[{id}]");

        pp4.SetAsOutputByTermId(id => $"PP4[{id}]");
        pp4Dual.SetAsOutputByTermId(id => $"DualPP4[{id}]");

        x4A.SetAsOutputByTermId(id => $"x4a[{id}]");
        x4B.SetAsOutputByTermId(id => $"x4b[{id}]");

        
        //context.OptimizeContext();

        context = context.OptimizeContext(
            new McGOptParameters()
            {
                GenerationCount = 40,
                CodeFilePath = @"D:\Projects\Study\Surveying\Hansen Problem\CGACode"
            }
        );

        // Define code generated variable names for intermediate scalar variables
        context.SetComputedExternalNamesByOrder(index => $"temp{index}");


        // Stage 5: Define a C# code composer and generate final code
        var contextCodeComposer = context.CreateContextCodeComposer(
            GaFuLLanguageServerBase.CSharpFloat64()
        );

        contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = false;

        var code = contextCodeComposer.Generate();

        Console.WriteLine("Generated Code:");
        Console.WriteLine(code);
        Console.WriteLine();


        //var dotCode = contextCodeComposer.GenerateGraphVizCode();

        //Console.WriteLine("GraphViz Code:");
        //Console.WriteLine(dotCode);
        //Console.WriteLine();
    }
}