using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.SymbolicApplications.Samples.Geometry
{
    public static class SnelliusPothenotProblemSample
    {
        public static void SymbolicCGaCassini()
        {
            var scalarProcessor = 
                ScalarProcessorOfWolframExpr.DefaultProcessor;
            
            var geometricProcessor = 
                scalarProcessor.CreateConformalXGaProcessor();

            var latexComposer = 
                LaTeXComposerExpr.DefaultComposer;

            var halfPi = scalarProcessor.ScalarPiOver2;

            // Define constant CGA basis blades
            var en = 
                geometricProcessor.CreateTermVector(0);

            var ep = 
                geometricProcessor.CreateTermVector(1);

            var e1 = 
                geometricProcessor.CreateTermVector(2);

            var e2 = 
                geometricProcessor.CreateTermVector(3);

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
                geometricProcessor.CreateVector(Expr.INT_ZERO, Expr.INT_ZERO, (-4d).ToExpr(), 2d.ToExpr());
                
            var pvB =
                geometricProcessor.CreateVector(Expr.INT_ZERO, Expr.INT_ZERO, 0d.ToExpr(), 10d.ToExpr());

            var pvC =
                geometricProcessor.CreateVector(Expr.INT_ZERO, Expr.INT_ZERO, 10d.ToExpr(), 8d.ToExpr());

            // Define input angles
            var alpha =
                geometricProcessor.CreateScalar("75.964 * Pi / 180".ToExpr());

            var beta =
                geometricProcessor.CreateScalar("67.166 * Pi / 180".ToExpr());

            // Stage 3: Define computations on inputs
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

            var ra = angle1.Cos().Add(angle1.Sin() * e12);
            var rc = angle2.Cos().Add(angle2.Sin() * e12);
            
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
            var c1 = (o1 - (r1 * r1) * ni / 2).DivideByNorm();
            var c2 = (o2 - (r2 * r2) * ni / 2).DivideByNorm();

            // Intersect two circles to find OPNS point-pair
            var p = c1.Op(c2).UnDual(cgaI);

            // Find two final individual points inside point-pair, one of them must be B
            // and the other is P
            var s1 = p.SpSquared().Sqrt();
            var s2 = ni.Lcp(p).Inverse();

            var p1 = p.Add(s1).Gp(s2);
            var p2 = p.Subtract(s1).Gp(s2);

            var pvP1 = p1.GetVectorPart().GetVectorPart(i => i is 2 or 3).Negative();
            var pvP2 = p2.GetVectorPart().GetVectorPart(i => i is 2 or 3).Negative();
            
            Console.WriteLine($"$ r1 = {latexComposer.GetMultivectorText(r1)} $");
            Console.WriteLine($"$ r2 = {latexComposer.GetMultivectorText(r2)} $");
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
            // The meta-programming context is a special kind of symbolic linear processor for code generation
            var context = 
                new MetaContext()
                {
                    MergeExpressions = false,
                    ContextOptions =
                    {
                        ContextName = "VGA", 
                        AllowGenerateComments = true,
                        PropagateConstants = true
                    }
                };

            // Use this if you want Wolfram Mathematica symbolic processor
            // instead of the default AngouriMath symbolic processor
            context.AttachMathematicaExpressionEvaluator();

            // Define a Euclidean multivectors processor for the context
            var processor = context.CreateEuclideanXGaProcessor();


            // Stage 2: Define the input parameters of the context
            // The input parameters are named variables created as scalar parts of multivectors
            // and used for later processing to compute some outputs

            // Define constant EGA basis blades
            var e1 = 
                processor.CreateTermVector(0);

            var e2 = 
                processor.CreateTermVector(1);
            
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
                context["alpha"];

            var beta =
                context["beta"];


            // Stage 3: Define computations on inputs
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

            // Define the final outputs for the computations for proper code generation
            dSig.SetIsOutput(true);
            pvP.SetIsOutput(true);


            // Stage 4: Optimize symbolic computations in the meta-programming context
            context.OptimizeContext();


            // Stage 5: Assign code generated variable names for all variables
            // Define code generated variable names for input variables
            context["Ax"].SetExternalName("A.X.Value");
            context["Ay"].SetExternalName("A.Y.Value");
            
            context["Bx"].SetExternalName("B.X.Value");
            context["By"].SetExternalName("B.Y.Value");
            
            context["Cx"].SetExternalName("C.X.Value");
            context["Cy"].SetExternalName("C.Y.Value");
            
            alpha.SetExternalName("Alpha.Radians.Value");
            beta.SetExternalName("Beta.Radians.Value");

            pvP[0].SetExternalName("var pX");
            pvP[1].SetExternalName("var pY");

            dSig.SetExternalName("dSig");

            // Define code generated variable names for intermediate variables
            context.SetIntermediateExternalNamesByNameIndex(index => $"temp{index}");


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
            // The meta-programming context is a special kind of symbolic linear processor for code generation
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
            // The input parameters are named variables created as scalar parts of multivectors
            // and used for later processing to compute some outputs

            // Define constant scalars
            var halfPi = context.ScalarPiOver2;

            // Define constant CGA basis blades
            var en = 
                processor.CreateTermVector(0);

            var ep = 
                processor.CreateTermVector(1);

            var e1 = 
                processor.CreateTermVector(2);

            var e2 = 
                processor.CreateTermVector(3);

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
                context["alpha"];

            var beta =
                context["beta"];

            // Stage 3: Define computations on inputs
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
            var c1 = (o1 - (r1 * r1) * ni / 2).DivideByNorm();
            var c2 = (o2 - (r2 * r2) * ni / 2).DivideByNorm();

            // Intersect two circles to find OPNS point-pair
            var p = c1.Op(c2).UnDual(cgaI);

            // Find two final individual points inside point-pair, one of them must be B
            // and the other is P
            var s1 = p.SpSquared().Sqrt();
            var s2 = ni.Lcp(p).Inverse();

            var p1 = p.Add(s1).Gp(s2);
            var p2 = p.Subtract(s1).Gp(s2);

            var pvP1 = p1.GetVectorPart().GetVectorPart(i => i is 2 or 3).Negative();
            var pvP2 = p2.GetVectorPart().GetVectorPart(i => i is 2 or 3).Negative();
            
            // Define the final outputs for the computations for proper code generation
            pvP1.SetIsOutput(true);
            pvP2.SetIsOutput(true);

            //// For debugging purposes only
            //pvO1.SetIsOutput(true);
            //pvO2.SetIsOutput(true);
            //r1.SetIsOutput(true);
            //r2.SetIsOutput(true);
            

            // Stage 4: Optimize symbolic computations in the meta-programming context
            context.OptimizeContext();

            // Stage 5: Assign code generated variable names for all variables
            // Define code generated variable names for input variables
            context["Ax"].SetExternalName("A.X.Value");
            context["Ay"].SetExternalName("A.Y.Value");
            
            context["Bx"].SetExternalName("B.X.Value");
            context["By"].SetExternalName("B.Y.Value");
            
            context["Cx"].SetExternalName("C.X.Value");
            context["Cy"].SetExternalName("C.Y.Value");
            
            alpha.SetExternalName("Alpha.Radians.Value");
            beta.SetExternalName("Beta.Radians.Value");

            pvP1[2].SetExternalName("var p1X");
            pvP1[3].SetExternalName("var p1Y");
            
            pvP2[2].SetExternalName("var p2X");
            pvP2[3].SetExternalName("var p2Y");
            
            //pvP2.SetExternalNamesByTermIndex(i => i == 2 ? "var p2X" : "var p2Y");


            //// For debugging purposes only
            //r1.SetExternalNamesByTermIndex(i => "r1");
            //r2.SetExternalNamesByTermIndex(i => "r2");
            //pvO1.SetExternalNamesByTermIndex(i => i == 0 ? "o1X" : "o1Y");
            //pvO2.SetExternalNamesByTermIndex(i => i == 0 ? "o2X" : "o2Y");


            // Define code generated variable names for intermediate variables
            context.SetIntermediateExternalNamesByNameIndex(index => $"temp{index}");

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
        
        public static void GenerateCGaCollinsCode()
        {
            // Stage 1: Define the meta-programming context
            // The meta-programming context is a special kind of symbolic linear processor for code generation
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
            // The input parameters are named variables created as scalar parts of multivectors
            // and used for later processing to compute some outputs

            // Define constant CGA basis blades
            var en = 
                processor.CreateTermVector(0);

            var ep = 
                processor.CreateTermVector(1);

            var e1 = 
                processor.CreateTermVector(2);

            var e2 = 
                processor.CreateTermVector(3);

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
                context["alpha"];

            var beta =
                context["beta"];


            // Stage 3: Define computations on inputs
            // Compute IPNS points from A,B,C
            var a = no + pvA + pvA.NormSquared() * ni / 2;
            var b = no + pvB + pvB.NormSquared() * ni / 2;
            var c = no + pvC + pvC.NormSquared() * ni / 2;
            
            // Compute OPNS line AC
            var lac = a.Op(c).Op(ni);

            // Create two rotors around A and C
            var angle1 = -beta / 2;
            var angle2 = alpha / 2;
            
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

            var p1 = p.Add(s1).Gp(s2);
            var p2 = p.Subtract(s1).Gp(s2);

            var pvP1 = p1.GetVectorPart().GetVectorPart(i => i is 2 or 3).Negative();
            var pvP2 = p2.GetVectorPart().GetVectorPart(i => i is 2 or 3).Negative();
            
            // Define the final outputs for the computations for proper code generation
            pvP1.SetIsOutput(true);
            pvP2.SetIsOutput(true);
            pvE.SetIsOutput(true);


            // Stage 4: Optimize symbolic computations in the meta-programming context
            context.OptimizeContext();


            // Stage 5: Assign code generated variable names for all variables
            // Define code generated variable names for input variables
            context["Ax"].SetExternalName("A.X.Value");
            context["Ay"].SetExternalName("A.Y.Value");
            
            context["Bx"].SetExternalName("B.X.Value");
            context["By"].SetExternalName("B.Y.Value");
            
            context["Cx"].SetExternalName("C.X.Value");
            context["Cy"].SetExternalName("C.Y.Value");
            
            alpha.SetExternalName("Alpha.Radians.Value");
            beta.SetExternalName("Beta.Radians.Value");

            pvP1[2].SetExternalName("var p1X");
            pvP1[3].SetExternalName("var p1Y");
            
            pvP2[2].SetExternalName("var p2X");
            pvP2[3].SetExternalName("var p2Y");
            
            pvE[2].SetExternalName("var eX");
            pvE[3].SetExternalName("var eY");

            // Define code generated variable names for intermediate variables
            context.SetIntermediateExternalNamesByNameIndex(index => $"temp{index}");


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
            // The meta-programming context is a special kind of symbolic linear processor for code generation
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
            // The input parameters are named variables created as scalar parts of multivectors
            // and used for later processing to compute some outputs

            // Define constant CGA basis blades
            var en = 
                processor.CreateTermVector(0);

            var ep = 
                processor.CreateTermVector(1);

            var e1 = 
                processor.CreateTermVector(2);

            var e2 = 
                processor.CreateTermVector(3);

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
                context["alpha"];

            var beta =
                context["beta"];


            // Stage 3: Define computations on inputs
            // Compute IPNS points from A,B,C
            var a = no + pvA + pvA.NormSquared() * ni / 2;
            var b = no + pvB + pvB.NormSquared() * ni / 2;
            var c = no + pvC + pvC.NormSquared() * ni / 2;
            
            // Compute OPNS line AC
            var lac = a.Op(c).Op(ni);

            // Create two rotors around A and C
            var angle1 = -beta / 2;
            
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
            
            // Define the final outputs for the computations for proper code generation
            pvP.SetIsOutput(true);
            

            // Stage 4: Optimize symbolic computations in the meta-programming context
            context.OptimizeContext();


            // Stage 5: Assign code generated variable names for all variables
            // Define code generated variable names for input variables
            context["Ax"].SetExternalName("A.X.Value");
            context["Ay"].SetExternalName("A.Y.Value");
            
            context["Bx"].SetExternalName("B.X.Value");
            context["By"].SetExternalName("B.Y.Value");
            
            context["Cx"].SetExternalName("C.X.Value");
            context["Cy"].SetExternalName("C.Y.Value");
            
            alpha.SetExternalName("Alpha.Radians.Value");
            beta.SetExternalName("Beta.Radians.Value");

            pvP[2].SetExternalName("var pX");
            pvP[3].SetExternalName("var pY");

            // Define code generated variable names for intermediate variables
            context.SetIntermediateExternalNamesByNameIndex(index => $"temp{index}");


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
