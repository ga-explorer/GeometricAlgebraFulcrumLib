using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Encoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Operations;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.MetaProgramming;

public static class GaalopComparisonsSamples
{
    /*
       P1 =  P1x*e1 + P1y*e2 + P1z*e3 + P1i*einf +e0;
       P2 =  P2x*e1 + P2y*e2 + P2z*e3 + P2i*einf +e0;
       P3 =  P3x*e1 + P3y*e2 + P3z*e3 + P3i*einf +e0;
       P4 =  P4x*e1 + P4y*e2 + P4z*e3 + P4i*einf +e0;

       S = P1^P2^P3^P4;

       //#pragma output C e1 e2 e3
       ?C = Normalize(S);    
    */
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
        var cgaSpace = context.CreateCGaGeometricSpace5D();
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

    /*
       //creating the CGA points;
       ?x1=createPoint(a1,a2,a3); 
       x2=createPoint(b1,b2,b3);
       x3=createPoint(c1,c2,c3);
       
       // creating the spheres;
       ?S1=x1-0.5*(d14*d14)*einf;
       ?S2=x2-0.5*(d24*d24)*einf;
       ?S3=x3-0.5*(d34*d34)*einf;
       
       // The PointPair in the intersection;
       ?PP4=S1^S2^S3;
       ?DualPP4=*PP4;
       
       // Extraction of the two points;
       ?x4a=-(-sqrt(DualPP4.DualPP4)+DualPP4)/(einf.DualPP4);
       ?x4b=-(sqrt(DualPP4.DualPP4)+DualPP4)/(einf.DualPP4);     
    */
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
        var cgaSpace = context.CreateCGaGeometricSpace5D();
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
            cgaSpace.EncodeIpnsRound.Point(a1, a2, a3).InternalVector;

        var x2 =
            cgaSpace.EncodeIpnsRound.Point(b1, b2, b3).InternalVector;

        var x3 =
            cgaSpace.EncodeIpnsRound.Point(c1, c2, c3).InternalVector;

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

    /*
       p0 = createPoint(0,0);
       p1 = createPoint(1, 1);
       p2 = createPoint(1,-1);
       p3 = createPoint(-1, -1);
       p4 = createPoint(-1, 1);
       
       angle = TIME
       M = cos(angle/2) + sin(angle / 2) * p0;
       
       ?p5 = M*p1*~M;
       ?p6 = M*p2*~M;
       ?p7 = M*p3*~M;
       ?p8 = M*p4*~M;
    */
    public static void RotatingRectangle()
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
                    ContextName = "PGA",
                    AllowGenerateComments = true,
                    PropagateConstants = true
                }
            };

        // Use this if you want Wolfram Mathematica symbolic processor
        // instead of the default AngouriMath symbolic processor
        context.AttachMathematicaExpressionEvaluator();

        // Define a Euclidean multivectors processor for the context
        var pgaSpace = context.CreatePGaGeometricSpace3D();
        var scalarProcessor = pgaSpace.ScalarProcessor;

        // PGA Basis vectors
        var eo = pgaSpace.Eo;
        var e1 = pgaSpace.E1;
        var e2 = pgaSpace.E2;

        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as
        // scalar parts of multivectors and used for later
        // processing to compute some outputs
        var angle = context["angle"];

        var cos = (angle / 2).Cos();
        var sin = (angle / 2).Sin();

        var p0 = pgaSpace.EncodePGaPoint(0, 0);
        var p1 = pgaSpace.EncodePGaPoint(1, 1);
        var p2 = pgaSpace.EncodePGaPoint(1, -1);
        var p3 = pgaSpace.EncodePGaPoint(-1, -1);
        var p4 = pgaSpace.EncodePGaPoint(-1, 1);

        var m =
            (sin * p0).InternalBivector.Add(cos);

        var p5 = Rotate(m, p1);
        var p6 = Rotate(m, p2);
        var p7 = Rotate(m, p3);
        var p8 = Rotate(m, p4);


        // Stage 4: Assign target variable names for final
        // code generation and optimize internal expression tree

        // Define code generated variable names for input scalar variables
        angle.SetExternalName("angle");

        // Define code generated variable names for output scalar variables
        p5.InternalKVector.SetAsOutputByTermId(id => $"P5[{id}]");
        p6.InternalKVector.SetAsOutputByTermId(id => $"P6[{id}]");
        p7.InternalKVector.SetAsOutputByTermId(id => $"P7[{id}]");
        p8.InternalKVector.SetAsOutputByTermId(id => $"P8[{id}]");

        context.OptimizeContext();

        //context = context.OptimizeContext(
        //    new McGOptParameters()
        //    {
        //        GenerationCount = 250,
        //        CodeFilePath = @"D:\Projects\Study\Surveying\Hansen Problem\CGACode"
        //    }
        //);

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

        return;


        // Rotate a given blade
        PGaBlade<IMetaExpressionAtomic> Rotate(XGaMultivector<IMetaExpressionAtomic> rotor, PGaBlade<IMetaExpressionAtomic> blade)
        {
            return pgaSpace.ToBlade(rotor.Gp(blade).Gp(rotor.Reverse()).GetKVectorPart(blade.Grade));
        }

    }

    /*
       //Reconstructing a Motor from Exact Point Correspondences
       //according to Sect. 6.8 of the tutorial
       //A Guided Tour to the Plane-Based Geometric Algebra PGA
       //by Leo Dorst, University of Amsterdam
       ExpApprox = {1 + _P(1) + _P(1)*_P(1)/2 + _P(1)*_P(1)*_P(1)/6 +_P(1)*_P(1)*_P(1)*_P(1)/24}
       
       Motor = {
           // computes the motor between two points, lines or planes
           // as the sqrt of them
           !temp = 1+_P(1)/_P(2);
           !abstemp = abs(temp);
           temp/abstemp
       }
       
       // original points
       A1 = createPoint(ax, ay, az);
       B1 = createPoint(bx, by, bz);
       C1 = createPoint(cx, cy, cz);
       
       // arbitrary transformation
       !GT = ExpApprox(0.3*(e1^e3) + 0.2*(e2^e0));

       // corresponding target points
       ?At = GT * A1 * ~GT;
       ?Bt = GT * B1 * ~GT;
       ?Ct = GT * C1 * ~GT;
       
       // Transformation from A1 to At
       // (translation)
       !VA = Motor(At, A1);
       !A2 = VA * A1 * ~VA;
       !B2 = VA * B1 * ~VA;
       !C2 = VA * C1 * ~VA;
       
       // Transformation from B2 to Bt
       // based on the rotation from the line L2 to L1
       !L1 = *(*At ^ *Bt);
       !L2 = *(*At ^ *B2);
       !VB = Motor(L1, L2);
       !B3 = VB * B2 * ~VB;
       !C3 = VB * C2 * ~VB;
       
       // Transformation from C3 to Ct
       // based on the rotation of two planes
       !P1 = *(*L1 ^*Ct);
       !P2 = *(*L1 ^*C3);
       !VC = Motor(P1,P2);
       
       // complete transformation
       !V = VC * VB * VA;

       // interpolation motor
       lerp = 1 * (1-t) + V * t;

       // interpolated points
       ?AI = lerp * A1 * ~lerp;
       ?BI = lerp * B1 * ~lerp;
       ?CI = lerp * C1 * ~lerp;    
    */
    public static void TriangleInterpolationExample()
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
                    ContextName = "PGA",
                    AllowGenerateComments = true,
                    PropagateConstants = true
                }
            };

        // Use this if you want Wolfram Mathematica symbolic processor
        // instead of the default AngouriMath symbolic processor
        context.AttachMathematicaExpressionEvaluator();

        // Define a Euclidean multivectors processor for the context
        var pgaSpace = context.CreatePGaGeometricSpace4D();
        //var scalarProcessor = pgaSpace.ScalarProcessor;

        // PGA Basis vectors
        var eo = pgaSpace.Eo;
        var e1 = pgaSpace.E1;
        var e2 = pgaSpace.E2;
        var e3 = pgaSpace.E3;

        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as
        // scalar parts of multivectors and used for later
        // processing to compute some outputs

        // Define input 3D position vectors of points a,b,c
        var ax = context["ax"];
        var ay = context["ay"];
        var az = context["az"];

        var bx = context["bx"];
        var by = context["by"];
        var bz = context["bz"];

        var cx = context["cx"];
        var cy = context["cy"];
        var cz = context["cz"];

        var t = context["t"];

        // Stage 3: Perform algebraic computations on input parameters
        // A symbolic expression tree for elementary operations on scalar
        // components is created automatically in this stage

        // original points
        var a1 = pgaSpace.EncodePGaPoint(ax, ay, az);
        var b1 = pgaSpace.EncodePGaPoint(bx, by, bz);
        var c1 = pgaSpace.EncodePGaPoint(cx, cy, cz);

        // arbitrary transformation
        var gt = ExpApprox(
            0.3 * e1.Op(e3) + 0.2 * e2.Op(eo)
        );

        // corresponding target points
        var at = Rotate(gt, a1);
        var bt = Rotate(gt, b1);
        var ct = Rotate(gt, c1);

        // Transformation from A1 to At (translation)
        var va = Motor(at, a1);

        var a2 = Rotate(va, a1);
        var b2 = Rotate(va, b1);
        var c2 = Rotate(va, c1);

        // Transformation from B2 to Bt
        // based on the rotation from the line L2 to L1
        var l1 = at.Join(bt);
        var l2 = at.Join(b2);
        var vb = Motor(l1, l2);
        var b3 = Rotate(vb, b2);
        var c3 = Rotate(vb, c2);

        // Transformation from C3 to Ct
        // based on the rotation of two planes
        var p1 = l1.Join(ct);
        var p2 = l1.Join(c3);
        var vc = Motor(p1, p2);

        // complete transformation
        var v = vc.Gp(vb).Gp(va);

        // interpolation motor
        var lerp = 1 * (1 - t) + v * t;

        // interpolated points
        var ai = Rotate(lerp, a1);
        var bi = Rotate(lerp, b1);
        var ci = Rotate(lerp, c1);


        // Stage 4: Assign target variable names for final
        // code generation and optimize internal expression tree

        // Define code generated variable names for input scalar variables
        ax.SetExternalName("ax");
        ay.SetExternalName("ay");
        az.SetExternalName("az");

        bx.SetExternalName("bx");
        by.SetExternalName("by");
        bz.SetExternalName("bz");

        cx.SetExternalName("cx");
        cy.SetExternalName("cy");
        cz.SetExternalName("cz");

        t.SetExternalName("t");

        // Define code generated variable names for output scalar variables
        ai.InternalKVector.SetAsOutputByTermId(id => $"AI[{id}]");
        at.InternalKVector.SetAsOutputByTermId(id => $"At[{id}]");

        bi.InternalKVector.SetAsOutputByTermId(id => $"BI[{id}]");
        bt.InternalKVector.SetAsOutputByTermId(id => $"Bt[{id}]");

        ci.InternalKVector.SetAsOutputByTermId(id => $"CI[{id}]");
        ct.InternalKVector.SetAsOutputByTermId(id => $"Ct[{id}]");

        context.OptimizeContext();

        //context = context.OptimizeContext(
        //    new McGOptParameters()
        //    {
        //        GenerationCount = 250,
        //        CodeFilePath = @"D:\Projects\Study\Surveying\Hansen Problem\CGACode"
        //    }
        //);

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

        return;

        // Rotate a given blade
        PGaBlade<IMetaExpressionAtomic> Rotate(XGaMultivector<IMetaExpressionAtomic> rotor, PGaBlade<IMetaExpressionAtomic> blade)
        {
            return pgaSpace.ToBlade(rotor.Gp(blade).Gp(rotor.Reverse()).GetKVectorPart(blade.Grade));
        }

        // Reconstructing a Motor from Exact Point Correspondences
        // according to Sect. 6.8 of the tutorial
        // A Guided Tour to the Plane-Based Geometric Algebra PGA
        // by Leo Dorst, University of Amsterdam
        XGaMultivector<IMetaExpressionAtomic> ExpApprox(PGaBlade<IMetaExpressionAtomic> blade)
        {
            var bv = blade.InternalBivector;

            return 1 + bv + bv.Gp(bv) / 2 + bv.Gp(bv).Gp(bv) / 6 + bv.Gp(bv).Gp(bv).Gp(bv) / 24;
        }

        // computes the motor between two points, lines or planes
        // as the sqrt of them
        XGaMultivector<IMetaExpressionAtomic> Motor(PGaBlade<IMetaExpressionAtomic> blade1, PGaBlade<IMetaExpressionAtomic> blade2)
        {
            return (1 + blade1.Gp(blade2.Inverse())).DivideByNorm();
        }
    }

    /*
       cgaI = e0^e1^e2^einf;
       cgaIi = 1 / cgaI;
       
       // Define 3 points
       pvA = Ax * e1 + Ay * e2;
       pvB = Bx * e1 + By * e2;
       pvC = Cx * e1 + Cy * e2;
       
       a = e0 + pvA + 0.5 * abs(pvA) * einf;
       b = e0 + pvB + 0.5 * abs(pvB) * einf;
       c = e0 + pvC + 0.5 * abs(pvC) * einf;
       
       // Define lines AB and CB
       lab = a^b^einf;
       lcb = c^b^einf;
       
       // Define perpendicular bisections of AB and CB to get OPNS lines MAB and MCB
       midAb1 = (b - a).cgaI;
       midAb = midAb1 / abs(midAb1);
       
       midCb1 = (b - c).cgaI;
       midCb = midCb1 / abs(midCb1);
       
       // Create two rotors around A and C
       angle1 = (1.57079633 - alpha) / 2;
       angle2 = (beta - 1.57079633) / 2;
       
       ra = cos(angle1) + sin(angle1) * e1^e2;
       rc = cos(angle2) + sin(angle2) * e1^e2;
       
       ta = 1 + einf * pvA / 2;
       tc = 1 + einf * pvC / 2;
       
       da = ta * ra * ~ta;
       dc = tc * rc * ~tc;
       
       // Rotate AB around A to get OPNS line LAO, and CB around C to get OPNS line LCO
       lao = da * lab * ~da;
       lco = dc * lcb * ~dc;
       
       // Intersect LAO with MAB to get center of circle PAB
       ss11 = ((lao.cgaIi)^(midAb.cgaIi)).cgaI;
       ss1 = ss11 / abs(ss11);
       O1 = e0.ss1;
       pvO1 = (O1.e1) * e1 + (O1.e2) * e2;
       
       // Intersect LCO with MCB to get center of circle PCB
       ss21 = ((lco.cgaIi)^(midCb.cgaIi)).cgaI;
       ss2 = ss21 / abs(ss21);
       O2 = e0.ss2;
       pvO2 = (O2.e1) * e1 + (O2.e2) * e2;
       
       o1 = e0 + pvO1 + 0.5 * abs(pvO1) * einf;
       o2 = e0 + pvO2 + 0.5 * abs(pvO2) * einf;
       
       // Find radii of two circles
       r1 = sqrt(-2 * o1.b);
       r2 = sqrt(-2 * o2.b);
       
       // Construct IPNS blades of two circles
       c11 = o1 - 0.5 * r1 * r1 * einf;
       c1 = c11 / abs(c11);
       
       c21 = o2 - 0.5 * r2 * r2 * einf;
       c2 = c21 / abs(c21);
       
       // Intersect two circles to find OPNS point-pair
       p = (c1^c2).cgaI;
       
       // Find two final individual points inside point-pair, one of them must be B
       // and the other is P
       s1 = sqrt(p.p);
       s2 = 1 / (einf.p);
       
       //#pragma output p1 e1 e2
       ?p1 = (p + s1) * s2;
       
       //#pragma output p2 e1 e2
       ?p2 = (p - s1) * s2;
    */
    public static void SnelliusPothenotProblemCassiniExample()
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
                    ContextName = "CGA_Cassini",
                    AllowGenerateComments = true,
                    PropagateConstants = true
                }
            };

        // Use this if you want Wolfram Mathematica symbolic processor
        // instead of the default AngouriMath symbolic processor
        context.AttachMathematicaExpressionEvaluator();

        // Define a Conformal multivectors processor for the context
        var processor = context.CreateConformalXGaProcessor();

        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as
        // scalar parts of multivectors and used for later
        // processing to compute some outputs

        // Define constant scalars
        var halfPi = context.PiOver2Value;

        // Define constant CGA basis blades
        var en =
            processor.VectorTerm(0);

        var ep =
            processor.VectorTerm(1);

        var e1 =
            processor.VectorTerm(2);

        var e2 =
            processor.VectorTerm(3);

        var no =
            (en - ep) / 2;

        var ni =
            en + ep;

        var e12 =
            e1.Op(e2);

        var cgaI =
            en.Op(ep).Op(e1).Op(e2);

        // Define input 2D position vectors of points A,B,C
        var pvA =
            context["Ax"] * e1 + context["Ay"] * e2;

        var pvB =
            context["Bx"] * e1 + context["By"] * e2;

        var pvC =
            context["Cx"] * e1 + context["Cy"] * e2;

        // Define input angles
        var alpha =
            context.CreatePolarAngle(
                context["alphaCos"],
                context["alphaSin"]
            );

        var beta =
            context.CreatePolarAngle(
                context["betaCos"],
                context["betaSin"]
            );

        // Stage 3: Perform algebraic computations on input parameters
        // A symbolic expression tree for elementary operations on scalar
        // components is created automatically in this stage

        // Compute IPNS points from A,B,C
        var a = no + pvA + pvA.NormSquared() * ni / 2;
        var b = no + pvB + pvB.NormSquared() * ni / 2;
        var c = no + pvC + pvC.NormSquared() * ni / 2;

        // Compute OPNS lines AB and CB
        var lab = a.Op(b).Op(ni);
        var lcb = c.Op(b).Op(ni);

        // Define perpendicular bisections of AB and CB to get OPNS lines MAB and MCB
        var midAb = (b - a).UnDual(cgaI).DivideByNorm();
        var midCb = (b - c).UnDual(cgaI).DivideByNorm();

        // Create two rotors around A and C
        var angle1 = (halfPi - alpha).HalfPolarAngle();
        var angle2 = (beta - halfPi).HalfPolarAngle();

        var ra = angle1.Cos() + angle1.Sin() * e12;
        var rc = angle2.Cos() + angle2.Sin() * e12;

        var ta = 1 + ni.Gp(pvA) / 2;
        var tc = 1 + ni.Gp(pvC) / 2;

        var da = ta.Gp(ra).Gp(ta.Reverse());
        var dc = tc.Gp(rc).Gp(tc.Reverse());

        // Rotate AB around A to get OPNS line LAO, and CB around C to get OPNS line LCO
        var lao = da.Gp(lab).Gp(da.Reverse());
        var lco = dc.Gp(lcb).Gp(dc.Reverse());

        // Intersect LAO with MAB to get center of circle PAB
        var pvO1 = no.Lcp(
            lao.Dual(cgaI).Op(midAb.Dual(cgaI)).UnDual(cgaI).DivideByNorm()
        ).GetVectorPart().GetVectorPart(i => i is 2 or 3);

        // Intersect LCO with MCB to get center of circle PCB
        var pvO2 = no.Lcp(
            lco.Dual(cgaI).Op(midCb.Dual(cgaI)).UnDual(cgaI).DivideByNorm()
        ).GetVectorPart().GetVectorPart(i => i is 2 or 3);

        var o1 = no + pvO1 + pvO1.NormSquared() * ni / 2;
        var o2 = no + pvO2 + pvO2.NormSquared() * ni / 2;

        // Find radii of two circles
        var r1 = (-2 * o1.Lcp(b)).Sqrt();
        var r2 = (-2 * o2.Lcp(b)).Sqrt();

        // Construct IPNS blades of two circles
        var c1 = (o1 - r1 * r1 * ni / 2).DivideByNorm();
        var c2 = (o2 - r2 * r2 * ni / 2).DivideByNorm();

        // Intersect two circles to find OPNS point-pair
        var p = c1.Op(c2).UnDual(cgaI);

        // Find two final individual points inside point-pair, one of them must be B
        // and the other is P
        var s1 = p.SpSquared().Sqrt();
        var s2 = ni.Lcp(p).Inverse();

        var p1 = (p + s1).Gp(s2);
        var p2 = (p - s1).Gp(s2);

        var pvP1 = p1.GetVectorPart().GetVectorPart(i => i is 2 or 3).Negative();
        var pvP2 = p2.GetVectorPart().GetVectorPart(i => i is 2 or 3).Negative();

        // Stage 4: Assign target variable names for final
        // code generation and optimize internal expression tree

        // Define code generated variable names for input scalar variables
        context["Ax"].SetExternalName("Ax");
        context["Ay"].SetExternalName("Ay");

        context["Bx"].SetExternalName("Bx");
        context["By"].SetExternalName("By");

        context["Cx"].SetExternalName("Cx");
        context["Cy"].SetExternalName("Cy");

        context["alphaCos"].SetExternalName("alphaCos");
        context["alphaSin"].SetExternalName("alphaSin");

        context["betaCos"].SetExternalName("betaCos");
        context["betaSin"].SetExternalName("betaSin");

        pvP1[2].SetAsOutput("var p1X");
        pvP1[3].SetAsOutput("var p1Y");

        pvP2[2].SetAsOutput("var p2X");
        pvP2[3].SetAsOutput("var p2Y");

        context.OptimizeContext();

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
    }
}