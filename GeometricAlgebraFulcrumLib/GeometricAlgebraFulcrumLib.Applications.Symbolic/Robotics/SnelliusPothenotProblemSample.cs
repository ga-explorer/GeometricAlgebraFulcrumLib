using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.Robotics
{
    public static class SnelliusPothenotProblemSample
    {
        public static void SymbolicCGaCassini()
        {
            var scalarProcessor =
                ScalarProcessorOfWolframExpr.Instance;

            var geometricProcessor =
                scalarProcessor.CreateConformalXGaProcessor();

            var latexComposer =
                LaTeXComposerOfWolframExpr.DefaultComposer;

            var halfPi = scalarProcessor.PiOver2Value;

            // Define constant CGA basis blades
            var en =
                geometricProcessor.VectorTerm(0);

            var ep =
                geometricProcessor.VectorTerm(1);

            var e1 =
                geometricProcessor.VectorTerm(2);

            var e2 =
                geometricProcessor.VectorTerm(3);

            var no =
                (en + ep) / 2;

            var ni =
                en - ep;

            var e12 =
                e1.Op(e2);

            var cgaI =
                en.Op(ep).Op(e1).Op(e2);

            // Define input 2D position vectors of points A,B,C
            var pvA =
                geometricProcessor.Vector(Expr.INT_ZERO, Expr.INT_ZERO, (-4d).ToExpr(), 2d.ToExpr());

            var pvB =
                geometricProcessor.Vector(Expr.INT_ZERO, Expr.INT_ZERO, 0d.ToExpr(), 10d.ToExpr());

            var pvC =
                geometricProcessor.Vector(Expr.INT_ZERO, Expr.INT_ZERO, 10d.ToExpr(), 8d.ToExpr());

            // Define input angles
            var alpha =
                geometricProcessor.Scalar("75.964 * Pi / 180".ToExpr());

            var beta =
                geometricProcessor.Scalar("67.166 * Pi / 180".ToExpr());

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
            var angle1 = (halfPi - alpha) / 2;
            var angle2 = (beta - halfPi) / 2;

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

            Console.WriteLine($"$ r1 = {latexComposer.GetScalarText(r1)} $");
            Console.WriteLine($"$ r2 = {latexComposer.GetScalarText(r2)} $");
            Console.WriteLine();

            Console.WriteLine($"$ c1 = {latexComposer.GetMultivectorText(pvO1)} $");
            Console.WriteLine($"$ c2 = {latexComposer.GetMultivectorText(pvO2)} $");
            Console.WriteLine();

            Console.WriteLine($"$ p1 = {latexComposer.GetMultivectorText(pvP1)} $");
            Console.WriteLine($"$ p2 = {latexComposer.GetMultivectorText(pvP2)} $");
            Console.WriteLine();
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
                        ContextName = "VGA",
                        AllowGenerateComments = true,
                        PropagateConstants = true,
                        //ReUseIntermediateVariables = true
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
            var e1 =
                processor.VectorTerm(0);

            var e2 =
                processor.VectorTerm(1);

            var e12 =
                e1.Op(e2);

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
            var v1 = pvA - pvB;
            var v2 = pvC - pvB;

            var d1 = v1 + (v1 / alpha.Tan()).Gp(e12);
            var d2 = v2 - (v2 / beta.Tan()).Gp(e12);
            var d = d2 - d1;

            var dSig = d.SpSquared();
            var dInv = d / dSig;

            var pvP =
                (pvB + d1.Op(d).Rcp(dInv)).GetVectorPart();


            // Stage 5: Assign code generated variable names for all variables
            // Define code generated variable names for input variables
            context["Ax"].SetExternalName("data.Ax");
            context["Ay"].SetExternalName("data.Ay");

            context["Bx"].SetExternalName("data.Bx");
            context["By"].SetExternalName("data.By");

            context["Cx"].SetExternalName("data.Cx");
            context["Cy"].SetExternalName("data.Cy");

            context["alphaCos"].SetExternalName("alphaCos");
            context["alphaSin"].SetExternalName("alphaSin");

            context["betaCos"].SetExternalName("betaCos");
            context["betaSin"].SetExternalName("betaSin");

            pvP[0].SetAsOutput("pX");
            pvP[1].SetAsOutput("pY");

            dSig.SetAsOutput("dSig");

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


            //var dotCode = contextCodeComposer.GenerateGraphVizCode();

            //Console.WriteLine("GraphViz Code:");
            //Console.WriteLine(dotCode);
            //Console.WriteLine();
        }

        public static void GeneratePGaPacoCode()
        {
            const int n = 4;

            // Stage 1: Define the meta-programming context
            // The meta-programming context is a special kind
            // of symbolic processor for code generation
            var context =
                new MetaContext()
                {
                    MergeExpressions = false,
                    ContextOptions =
                    {
                        ContextName = "PGA_Paco",
                        AllowGenerateComments = true,
                        PropagateConstants = true
                    }
                };

            // Use this if you want Wolfram Mathematica symbolic processor
            // instead of the default AngouriMath symbolic processor
            context.AttachMathematicaExpressionEvaluator();

            // Define a Conformal multivectors processor for the context
            var cga =
                context.CreateConformalXGaProcessor();

            // Stage 2: Define the input parameters of the context
            // The input parameters are named variables created as
            // scalar parts of multivectors and used for later
            // processing to compute some outputs

            // Define constant scalars
            var halfPi = context.PiOver2Value;

            // Define input angles
            var alpha =
                context.CreatePolarAngleFromDegrees(76);
            //context.CreatePolarAngle(
            //    context["alphaCos"], 
            //    context["alphaSin"]
            //);

            var beta =
                context.CreatePolarAngleFromDegrees(67.2);
            //context.CreatePolarAngle(
            //    context["betaCos"], 
            //    context["betaSin"]
            //);

            // Stage 3: Perform algebraic computations on input parameters
            // A symbolic expression tree for elementary operations on scalar
            // components is created automatically in this stage

            //var (ax, ay) = (context["Ax"], context["Ay"]);
            //var (bx, by) = (context["Bx"], context["By"]);
            //var (cx, cy) = (context["Cx"], context["Cy"]);
            var (ax, ay) = (context[-4], context[2]);
            var (bx, by) = (context[0], context[10]);
            var (cx, cy) = (context[10], context[8]);

            // Compute HGA points from A,B,C
            var hgaA =
                cga.EncodeHGaPoint(ax, ay);

            var pgaA =
                cga.PGaDual(hgaA, n);

            var hgaB =
                cga.EncodeHGaPoint(bx, by);

            var pgaB =
                cga.PGaDual(hgaB, n);

            var hgaC =
                cga.EncodeHGaPoint(cx, cy);

            var pgaC =
                cga.PGaDual(hgaC, n);

            // Compute PGA lines AB and CB
            var lab =
                cga.PGaDual(hgaA.Op(hgaB), n);

            var lcb =
                cga.PGaDual(hgaC.Op(hgaB), n);

            // Define perpendicular bisections of AB and CB to get OPNS lines MAB and MCB
            var midAb =
                (pgaA + pgaB).Gp(lab).GetVectorPart();

            var midCb =
                (pgaC + pgaB).Gp(lcb).GetVectorPart();

            // Create two rotors around A and C
            var angle1 =
                (halfPi - alpha).HalfPolarAngle();

            var angle2 =
                (beta - halfPi).HalfPolarAngle();

            var ra =
                angle1.Cos() + angle1.Sin() * pgaA;

            var rc =
                angle2.Cos() + angle2.Sin() * pgaC;

            // Rotate AB around A to get OPNS line LAO, and CB around C to get OPNS line LCO
            var lao =
                ra.Gp(lab).Gp(ra.Reverse()).GetVectorPart();

            var lco =
                rc.Gp(lcb).Gp(rc.Reverse()).GetVectorPart();

            // Intersect LAO with MAB to get center of circle PAB
            var pgaO1 =
                lao.Op(midAb);

            var hgaO1 =
                cga.PGaDual(pgaO1, n);

            // Intersect LCO with MCB to get center of circle PCB
            var pgaO2 =
                lco.Op(midCb);

            var hgaO2 =
                cga.PGaDual(pgaO2, n);

            // Construct PGA line connecting circle centers
            var l12 =
                cga.PGaDual(hgaO1.Op(hgaO2), n);

            // Reflect point B in line L12
            var pvP =
                cga.DecodePGaPoint(
                    l12.Gp(pgaB).Gp(l12.Inverse()).GetBivectorPart(),
                    n
                );


            // Stage 5: Assign code generated variable names for all variables
            // Define code generated variable names for input variables
            context["Ax"].SetExternalName("data.Ax");
            context["Ay"].SetExternalName("data.Ay");

            context["Bx"].SetExternalName("data.Bx");
            context["By"].SetExternalName("data.By");

            context["Cx"].SetExternalName("data.Cx");
            context["Cy"].SetExternalName("data.Cy");

            context["alphaCos"].SetExternalName("alphaCos");
            context["alphaSin"].SetExternalName("alphaSin");

            context["betaCos"].SetExternalName("betaCos");
            context["betaSin"].SetExternalName("betaSin");

            pvP[0].SetAsOutput("pX");
            pvP[1].SetAsOutput("pY");

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


            //var dotCode = contextCodeComposer.GenerateGraphVizCode();

            //Console.WriteLine("GraphViz Code:");
            //Console.WriteLine(dotCode);
            //Console.WriteLine();
        }

        public static void GenerateCGaPacoCode()
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
                        ContextName = "CGA_Paco",
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
            var halfPi =
                context.CreatePolarAngleFromRadians(context.PiOver2Value);

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

            // Construct OPNS line connecting circle centers
            var l12 = o1.Op(o2).Op(ni);

            // Reflect point B in line L12
            var pvP =
                l12.Gp(b).Gp(l12.Inverse()).GetVectorPart().GetVectorPart(i => i is 2 or 3);


            // Stage 5: Assign code generated variable names for all variables
            // Define code generated variable names for input variables
            context["Ax"].SetExternalName("data.Ax");
            context["Ay"].SetExternalName("data.Ay");

            context["Bx"].SetExternalName("data.Bx");
            context["By"].SetExternalName("data.By");

            context["Cx"].SetExternalName("data.Cx");
            context["Cy"].SetExternalName("data.Cy");

            context["alphaCos"].SetExternalName("alphaCos");
            context["alphaSin"].SetExternalName("alphaSin");

            context["betaCos"].SetExternalName("betaCos");
            context["betaSin"].SetExternalName("betaSin");

            pvP[2].SetAsOutput("pX");
            pvP[3].SetAsOutput("pY");

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

        }

        public static void GenerateCGaCassiniCode()
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

            //// For debugging purposes only
            //pvO1.SetIsOutput(true);
            //pvO2.SetIsOutput(true);
            //r1.SetIsOutput(true);
            //r2.SetIsOutput(true);

            // Stage 4: Assign target variable names for final
            // code generation and optimize internal expression tree

            // Define code generated variable names for input scalar variables
            context["Ax"].SetExternalName("data.Ax");
            context["Ay"].SetExternalName("data.Ay");

            context["Bx"].SetExternalName("data.Bx");
            context["By"].SetExternalName("data.By");

            context["Cx"].SetExternalName("data.Cx");
            context["Cy"].SetExternalName("data.Cy");

            context["alphaCos"].SetExternalName("alphaCos");
            context["alphaSin"].SetExternalName("alphaSin");

            context["betaCos"].SetExternalName("betaCos");
            context["betaSin"].SetExternalName("betaSin");

            pvP1[2].SetAsOutput("var p1X");
            pvP1[3].SetAsOutput("var p1Y");

            pvP2[2].SetAsOutput("var p2X");
            pvP2[3].SetAsOutput("var p2Y");

            context.OptimizeContext();

            //pvP2.SetExternalNamesByTermIndex(i => i == 2 ? "var p2X" : "var p2Y");


            //// For debugging purposes only
            //r1.SetExternalNamesByTermIndex(i => "r1");
            //r2.SetExternalNamesByTermIndex(i => "r2");
            //pvO1.SetExternalNamesByTermIndex(i => i == 0 ? "o1X" : "o1Y");
            //pvO2.SetExternalNamesByTermIndex(i => i == 0 ? "o2X" : "o2Y");


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

        public static void GenerateCGaCollinsCode()
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
                        ContextName = "CGA_Collins",
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

            // Compute OPNS line AC
            var lac = a.Op(c).Op(ni);

            // Create two rotors around A and C
            var angle1 = -beta.HalfPolarAngle();
            var angle2 = alpha.HalfPolarAngle();

            var r1 = angle1.Cos() + angle1.Sin() * e12;
            var r2 = angle2.Cos() + angle2.Sin() * e12;

            var t1 = 1 + ni.Gp(pvA) / 2;
            var t2 = 1 + ni.Gp(pvC) / 2;

            var d1 = t1.Gp(r1).Gp(t1.Reverse());
            var d2 = t2.Gp(r2).Gp(t2.Reverse());

            // Rotate AC around A to get OPNS line L1, and AC around C to get OPNS line L2
            var l1 = d1.Gp(lac).Gp(d1.Reverse());
            var l2 = d2.Gp(lac).Gp(d2.Reverse());

            // Intersect L1 with L2 to get point E
            var pvE = no.Lcp(
                l1.Dual(cgaI).Op(l2.Dual(cgaI)).UnDual(cgaI).DivideByNorm()
            ).GetVectorPart().GetVectorPart(i => i is 2 or 3);

            var e = no + pvE + pvE.NormSquared() * ni / 2;

            // Construct OPNS circle passing through A,C,E
            var c1 = a.Op(c).Op(e);

            // Construct OPNS line passing through E,B
            var leb = e.Op(b).Op(ni);

            // Intersect circle and line to find OPNS point-pair
            var p =
                c1.Dual(cgaI).Op(leb.Dual(cgaI)).UnDual(cgaI).DivideByNorm();

            // Find two final individual points inside point-pair, one of them must be E
            // and the other is P
            var s1 = p.SpSquared().Sqrt();
            var s2 = ni.Lcp(p).Inverse();

            var p1 = (p + s1).Gp(s2);
            var p2 = (p - s1).Gp(s2);

            var pvP1 = p1.GetVectorPart().GetVectorPart(i => i is 2 or 3).Negative();
            var pvP2 = p2.GetVectorPart().GetVectorPart(i => i is 2 or 3).Negative();


            // Stage 5: Assign code generated variable names for all variables
            // Define code generated variable names for input variables
            context["Ax"].SetExternalName("data.Ax");
            context["Ay"].SetExternalName("data.Ay");

            context["Bx"].SetExternalName("data.Bx");
            context["By"].SetExternalName("data.By");

            context["Cx"].SetExternalName("data.Cx");
            context["Cy"].SetExternalName("data.Cy");

            context["alphaCos"].SetExternalName("alphaCos");
            context["alphaSin"].SetExternalName("alphaSin");

            context["betaCos"].SetExternalName("betaCos");
            context["betaSin"].SetExternalName("betaSin");

            pvP1[2].SetAsOutput("var p1X");
            pvP1[3].SetAsOutput("var p1Y");

            pvP2[2].SetAsOutput("var p2X");
            pvP2[3].SetAsOutput("var p2Y");

            pvE[2].SetAsOutput("eX");
            pvE[3].SetAsOutput("eY");

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

        }

        public static void GenerateCGaCollinsParallelCode()
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
                        ContextName = "CGA_Collins_Parallel",
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

            // Compute OPNS line AC
            var lac = a.Op(c).Op(ni);

            // Create two rotors around A and C
            var angle1 = beta.HalfPolarAngle().NegativeAngle();

            var r1 = angle1.Cos() + angle1.Sin() * e12;

            var t1 = 1 + ni.Gp(pvA) / 2;

            var d1 = t1.Gp(r1).Gp(t1.Reverse());

            // Rotate AC around A to get OPNS line L1, and AC around C to get OPNS line L2
            var l1 = d1.Gp(lac).Gp(d1.Reverse());

            var leb =
                b.Op(ni.Lcp(l1));

            var f =
                lac.Dual(cgaI).Op(leb.Dual(cgaI)).UnDual(cgaI).DivideByNorm();

            var pvP =
                no.Lcp(f).GetVectorPart().GetVectorPart(i => i is 2 or 3);


            // Stage 5: Assign code generated variable names for all variables
            // Define code generated variable names for input variables
            context["Ax"].SetExternalName("data.Ax");
            context["Ay"].SetExternalName("data.Ay");

            context["Bx"].SetExternalName("data.Bx");
            context["By"].SetExternalName("data.By");

            context["Cx"].SetExternalName("data.Cx");
            context["Cy"].SetExternalName("data.Cy");

            context["alphaCos"].SetExternalName("alphaCos");
            context["alphaSin"].SetExternalName("alphaSin");

            context["betaCos"].SetExternalName("betaCos");
            context["betaSin"].SetExternalName("betaSin");

            pvP[2].SetAsOutput("pX");
            pvP[3].SetAsOutput("pY");

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
        }
    }
}
