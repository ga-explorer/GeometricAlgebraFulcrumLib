using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Operations;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.Robotics
{
    public static class InverseKinematics6RSamples
    {
        public static void NumericExample()
        {
            var cga = CGaFloat64GeometricSpace5D.Instance;

            // CGA null basis vectors
            var eo = cga.Eo;
            var ei = cga.Ei;

            // define the Staübli robot link lengths
            const double d1 = 480d;
            const double a3 = 425d;
            const double d4 = 425d;

            // CGA null vector of the desired position of end-effector
            var cgaP = cga.Encode.IpnsRound.Point(561.8479, 262.7685, 455.0104);

            // point at the origin
            var cgaP0 = eo;

            // compute p1 (translation of p0)
            var cgaP1 = cgaP0.TranslateBy(0, 0, d1);

            // plane passing by p0, p1 and desired position of end-effector
            var plane = cgaP0.Op(cgaP1).Op(cgaP).Op(ei);

            // sphere center = p1 and radius = a3
            var cgaSphere1 = cgaP1 - 0.5 * a3.Square() * ei;

            // sphere center = position and radius = d4
            var cgaSphere2 = cgaP - 0.5 * d4.Square() * ei;

            // intersection of the two spheres results in a circle
            var cgaCircle = cgaSphere1.Op(cgaSphere2); //.RemoveNearZeroTerms();

            // intersection of the plane and circle results in a pair of points
            var cgaPointPair = plane.CGaDual().Op(cgaCircle);

            // extract one point from point pair, the other is completely analogous
            var cgaP2 = cgaPointPair.DecodeIpnsRound.PointPairIpnsPoint1();

            // normal vector to plane
            var normal = plane.DecodeOpnsFlat.VGaNormalVector3D();

            // first joint angle
            var theta1 = normal.GetAngleWithE1() - LinFloat64PolarAngle.Angle90;

            // auxiliary lines l1, l2, l3
            var cgaLine1 = cgaP0.Op(cgaP1).Op(ei);
            var cgaLine2 = cgaP1.Op(cgaP2).Op(ei);
            var cgaLine3 = cgaP2.Op(cgaP).Op(ei);

            // square roots of modules of auxiliary lines l1, l2, l3
            var l11 = cgaLine1.SpSquared().Sqrt();
            var l22 = cgaLine2.SpSquared().Sqrt();
            var l33 = cgaLine3.SpSquared().Sqrt();

            // cos angle between lines l1 and l2
            var theta2Cos = cgaLine2.Sp(cgaLine1) / (l11 * l22);

            // cos angle between lines l2 and l3
            var theta3Cos = cgaLine2.Sp(cgaLine3) / (l22 * l33);

            // second and third joint angles
            var theta2 = LinFloat64PolarAngle.CreateFromCos(theta2Cos);
            var theta3 = LinFloat64PolarAngle.CreateFromCos(theta3Cos);

            Console.WriteLine(theta1.RadiansValue); // should equal 0.4375 radians
            Console.WriteLine(theta2.RadiansValue); // should equal 0.8590 radians
            Console.WriteLine(theta3.RadiansValue); // should equal 1.5040 radians
        }

        public static void SymbolicExample()
        {
            var sp = ScalarProcessorOfWolframExpr.Instance;
            var cga = CGaGeometricSpace5D<Expr>.Create(sp);

            // CGA null basis vectors
            var eo = cga.Eo;
            var ei = cga.Ei;

            // define the Staübli robot link lengths
            const double d1 = 480d;
            const double a3 = 425d;
            const double d4 = 425d;

            // CGA null vector of the desired position of end-effector
            var cgaP = cga.Encode.IpnsRound.Point(561.8479, 262.7685, 455.0104);
            //var cgaP = cga.Encode.IpnsRound.Point("Px", "Py", "Pz");

            // point at the origin
            var cgaP0 = eo;

            // compute p1 (translation of p0)
            var cgaP1 = cgaP0.TranslateBy(0, 0, d1);

            // plane passing by p0, p1 and desired position of end-effector
            var plane = cgaP0.Op(cgaP1).Op(cgaP).Op(ei);

            // sphere center = p1 and radius = a3
            var cgaSphere1 = cgaP1 - 0.5 * a3.Square() * ei;

            // sphere center = position and radius = d4
            var cgaSphere2 = cgaP - 0.5 * d4.Square() * ei;

            // intersection of the two spheres results in a circle
            var cgaCircle = cgaSphere1.Op(cgaSphere2);

            // intersection of the plane and circle results in a pair of points
            var cgaPointPair = plane.CGaDual().Op(cgaCircle);

            // extract one point from point pair, the other is completely analogous
            var cgaP2 = cgaPointPair.Decode.IpnsRound.PointPairIpnsPoint1();

            // normal vector to plane
            var normal = plane.Decode.OpnsFlat.VGaNormalAsVector3D();

            // first joint angle
            var theta1Cos = normal.GetAngleCosWithE1();

            // auxiliary lines l1, l2, l3
            var cgaLine1 = cgaP0.Op(cgaP1).Op(ei);
            var cgaLine2 = cgaP1.Op(cgaP2).Op(ei);
            var cgaLine3 = cgaP2.Op(cgaP).Op(ei);

            // square roots of modules of auxiliary lines l1, l2, l3
            var l11 = cgaLine1.SpSquared().Sqrt();
            var l22 = cgaLine2.SpSquared().Sqrt();
            var l33 = cgaLine3.SpSquared().Sqrt();

            // cos angle between lines l1 and l2
            var theta2Cos = cgaLine2.Sp(cgaLine1) / (l11 * l22);

            // cos angle between lines l2 and l3
            var theta3Cos = cgaLine2.Sp(cgaLine3) / (l22 * l33);

            //Console.WriteLine(theta1Cos);
            //Console.WriteLine();

            //Console.WriteLine(theta2Cos);
            //Console.WriteLine();

            //Console.WriteLine(theta3Cos);
            //Console.WriteLine();


            // joint angles
            var theta1 = LinPolarAngle<Expr>.CreateFromCos(theta1Cos) - LinPolarAngle<Expr>.Angle90(sp);
            var theta2 = LinPolarAngle<Expr>.CreateFromCos(theta2Cos);
            var theta3 = LinPolarAngle<Expr>.CreateFromCos(theta3Cos);

            Console.WriteLine(theta1.RadiansValue); // should equal 0.4375 radians
            Console.WriteLine();

            Console.WriteLine(theta2.RadiansValue); // should equal 0.8590 radians
            Console.WriteLine();

            Console.WriteLine(theta3.RadiansValue); // should equal 1.5040 radians
            Console.WriteLine();
        }

        public static void MetaprogrammingExample1()
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
            var cga =
                context.CreateCGaGeometricSpace5D();

            var sp = cga.ScalarProcessor;

            // Stage 2: Define the input parameters of the context
            // The input parameters are named variables created as
            // scalar parts of multivectors and used for later
            // processing to compute some outputs
            
            // CGA null basis vectors
            var eo = cga.Eo;
            var ei = cga.Ei;
            
            // define the Staübli robot link lengths
            var d1 = context["d1"];
            var a3 = context["a3"];
            var d4 = context["d4"];

            // desired position of end-effector
            var pX = context["Px"];
            var pY = context["Py"];
            var pZ = context["Pz"];

            // CGA null vector of position of end-effector
            var cgaP = cga.Encode.IpnsRound.Point(pX, pY, pZ);
            //cgaP.DisableMerge();

            // point at the origin
            var cgaP0 = eo;

            // compute p1 (translation of p0)
            var cgaP1 = cgaP0.TranslateBy(sp.Zero, sp.Zero, d1);
            //cgaP1.DisableMerge();

            // plane passing by p0, p1 and p
            var plane = cgaP0.Op(cgaP1).Op(cgaP).Op(ei).BreakMerge();

            // sphere with center at p1 and radius a3
            var cgaSphere1 = context.BreakMerge(cgaP1 - 0.5 * a3.Square() * ei);

            // sphere with center at p and radius d4
            var cgaSphere2 = (cgaP - 0.5 * d4.Square() * ei).BreakMerge();

            // intersection of the two spheres resulting in a circle
            var cgaCircle = cgaSphere1.Op(cgaSphere2).BreakMerge();

            // intersection of the plane and circle resulting in a pair of points
            var cgaPointPair = plane.CGaDual().Op(cgaCircle).BreakMerge();

            // extract one point from point pair, the other is completely analogous
            var cgaP2 = cgaPointPair.Decode.IpnsRound.PointPairIpnsPoint1().BreakMerge();

            // normal vector to plane
            var normal = plane.Decode.OpnsFlat.VGaNormalAsVector3D().BreakMerge();

            // cos of first joint angle theta1
            var theta1Cos = normal.GetAngleCosWithE1();

            // auxiliary lines l1, l2, l3
            var cgaLine1 = cgaP0.Op(cgaP1).Op(ei).BreakMerge();
            var cgaLine2 = cgaP1.Op(cgaP2).Op(ei).BreakMerge();
            var cgaLine3 = cgaP2.Op(cgaP).Op(ei).BreakMerge();

            // square roots of modules of auxiliary lines l1, l2, l3
            var l11 = cgaLine1.SpSquared().Sqrt();
            var l22 = cgaLine2.SpSquared().Sqrt();
            var l33 = cgaLine3.SpSquared().Sqrt();

            // cos angle between lines l1 and l2
            var theta2Cos = (cgaLine2.Sp(cgaLine1) / (l11 * l22)).BreakMerge();

            // cos angle between lines l2 and l3
            var theta3Cos = (cgaLine2.Sp(cgaLine3) / (l22 * l33)).BreakMerge();

            d1.SetExternalName("d1");
            a3.SetExternalName("a3");
            d4.SetExternalName("d4");

            pX.SetExternalName("px");
            pY.SetExternalName("py");
            pZ.SetExternalName("pz");

            theta1Cos.SetAsOutput("theta1Cos");
            theta2Cos.SetAsOutput("theta2Cos");
            theta3Cos.SetAsOutput("theta3Cos");

            // Stage 4: Optimize symbolic computations in the meta-programming context
            context = context.OptimizeContext();

            //context = context.OptimizeContext(
            //    new McGOptParameters()
            //    {
            //        GenerationCount = 300,
            //        CodeFilePath = @"D:\Projects\Study\Surveying\Hansen Problem\CGACode"
            //    }
            //);

            // Define code generated variable names for intermediate variables
            context.SetComputedExternalNamesByOrder(index => $"temp{index}");


            // Stage 5: Define a MATLAB code composer with AngouriMath symbolic expressions converter
            var contextCodeComposer = context.CreateContextCodeComposer(
                GaFuLLanguageServerBase.MatlabFloat64()
            );

            contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = false;

            // Stage 6: Generate the final code
            var code = contextCodeComposer.Generate();

            Console.WriteLine("Generated Code:");
            Console.WriteLine(code);
            Console.WriteLine();
        }
        
        public static void MetaprogrammingExample2()
        {
            // Stage 1: Define the meta-programming context
            // The meta-programming context is a special kind
            // of symbolic processor for code generation
            var context = new MetaContext
            {
                MergeExpressions = true
            };

            // Use this if you want Wolfram Mathematica symbolic processor
            // instead of the default AngouriMath symbolic processor
            context.AttachMathematicaExpressionEvaluator();

            // Define a Euclidean multivectors processor for the context
            var cga =
                context.CreateCGaGeometricSpace5D();

            var sp = cga.ScalarProcessor;

            // Stage 2: Define the input parameters of the context
            // The input parameters are named variables created as
            // scalar parts of multivectors and used for later
            // processing to compute some outputs
            
            // define the Staübli robot link lengths
            var d1 = context["d1"];
            var a3 = context["a3"];
            var d4 = context["d4"];

            // desired position of end-effector
            var pX = context["Px"];
            var pY = context["Py"];
            var pZ = context["Pz"];

            var p = sp.Vector3D(pX, pY, pZ);
            var p0 = sp.ZeroVector3D();
            var p1 = sp.E3Vector3D(d1);

            // plane passing by p0, p1 and p
            var cgaPlane = cga.EncodeIpnsFlat.PlaneFromPoints(p0, p1, p).BreakMerge();
            
            // sphere with center at p1 and radius a3
            var cgaSphere1 = cga.Encode.IpnsRound.RealSphere(a3, p1).BreakMerge();
            
            // sphere with center at p and radius d4
            var cgaSphere2 = cga.Encode.IpnsRound.RealSphere(d4, p).BreakMerge();
            
            // intersection of the plane and circle resulting in a pair of points
            var cgaPointPair = cgaSphere2.MeetIpns(cgaSphere1, cgaPlane).BreakMerge();
            
            // extract one point from point pair, the other is completely analogous
            var p2 = cgaPointPair.Decode.IpnsRound.PointPairVGaPoint1AsVector3D().BreakMerge();
            
            // normal vector to plane
            var normal = cgaPlane.Decode.IpnsFlat.VGaNormalAsVector3D().BreakMerge();

            // cos of first joint angle theta1
            var theta1Cos = normal.GetAngleCosWithE1().BreakMerge();

            // auxiliary lines l1, l2, l3
            var cgaLine1 = cga.EncodeOpnsFlat.LineFromPoints(p0, p1).BreakMerge();
            var cgaLine2 = cga.EncodeOpnsFlat.LineFromPoints(p1, p2).BreakMerge();
            var cgaLine3 = cga.EncodeOpnsFlat.LineFromPoints(p2, p).BreakMerge();

            // square roots of modules of auxiliary lines l1, l2, l3
            var l11 = cgaLine1.SpSquared().Sqrt();
            var l22 = cgaLine2.SpSquared().Sqrt();
            var l33 = cgaLine3.SpSquared().Sqrt();

            // cos angle between lines l1 and l2
            var theta2Cos = (cgaLine2.Sp(cgaLine1) / (l11 * l22)).BreakMerge();

            // cos angle between lines l2 and l3
            var theta3Cos = (cgaLine2.Sp(cgaLine3) / (l22 * l33)).BreakMerge();

            d1.SetExternalName("d1");
            a3.SetExternalName("a3");
            d4.SetExternalName("d4");

            pX.SetExternalName("px");
            pY.SetExternalName("py");
            pZ.SetExternalName("pz");

            theta1Cos.SetAsOutput("theta1Cos");
            theta2Cos.SetAsOutput("theta2Cos");
            theta3Cos.SetAsOutput("theta3Cos");
            
            context.UpdateDependencyData(true);
            Console.WriteLine(context.ToString());
            Console.WriteLine();

            // Stage 4: Optimize symbolic computations in the meta-programming context
            context = context.OptimizeContext();

            //context = context.OptimizeContext(
            //    new McGOptParameters()
            //    {
            //        GenerationCount = 250,
            //        CodeFilePath = @"D:\Projects\Study\Surveying\Hansen Problem\CGACode"
            //    }
            //);

            // Define code generated variable names for intermediate variables
            context.SetComputedExternalNamesByOrder(index => $"temp{index}");


            // Stage 5: Define a MATLAB code composer
            var contextCodeComposer = context.CreateContextCodeComposer(
                GaFuLLanguageServerBase.MatlabFloat64()
            );

            contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = true;

            // Stage 6: Generate the final code
            var code = contextCodeComposer.Generate();

            Console.WriteLine(code);
            Console.WriteLine();
        }
        
        public static void CodeGenExample()
        {
            const double d1 = 480d;
            const double a3 = 425d;
            const double d4 = 425d;

            // CGA null vector of the desired position of end-effector
            var (px, py, pz) = (561.8479, 262.7685, 455.0104);

            //Begin GA-FuL MetaContext Code Generation, 2024-06-16T17:13:00.4197747+03:00
            //MetaContext: CGA
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 238 sub-expressions, 0 generated temps, 238 total.
            //Target Temp Variables: 238 total.
            //Output Variables: 3 total.
            //Computations: 1.2655601659751037 average, 305 total.
            //Memory Reads: 1.983402489626556 average, 478 total.
            //Memory Writes: 241 total.
            //
            //MetaContext Binding Data:
            //   -1 = constant: '-1'
            //   2 = constant: '2'
            //   0.5 = constant: '0.5'
            //   -0.5 = constant: '-0.5'
            //   Rational[1, 2] = constant: 'Rational[1, 2]'
            //   1 = constant: '1'
            //   -2 = constant: '-2'
            //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
            //   d1 = parameter: d1
            //   a3 = parameter: a3
            //   d4 = parameter: d4
            //   Px = parameter: px
            //   Py = parameter: py
            //   Pz = parameter: pz

            var temp0 = 0.5 * d1;
            var temp1 = py * temp0;
            var temp2 = -2 * temp1;
            var temp3 = px * temp0;
            var temp4 = -2 * temp3;
            var temp5 = temp2 * temp2 + temp4 * temp4;
            var temp6 = 1 / (Math.Sqrt(Math.Abs(temp5)));
            var temp7 = temp2 * temp6;
            var temp8 = temp4 * temp6;
            var temp9 = -(temp7 * temp7 + temp8 * temp8);
            var temp10 = 1 / temp9;
            var temp11 = temp7 * temp10;
            var temp12 = -temp11;
            var temp13 = temp8 * temp10;
            var temp14 = temp12 * temp12 + temp13 * temp13;
            var theta1Cos = temp12 * 1 / (Math.Sqrt(temp14));

            var temp16 = -0.5 * d1;
            var temp17 = temp0 * temp16;
            var temp18 = -temp17;
            var temp19 = 0.5 + 2 * temp18;
            var temp20 = a3 * a3;
            var temp21 = temp19 + -0.5 * temp20;
            var temp22 = px * temp21;
            var temp23 = temp4 * temp22;
            var temp24 = 2 * temp1;
            var temp25 = py * temp21;
            var temp26 = temp24 * temp25;
            var temp27 = temp23 - temp26;
            var temp28 = temp26 - temp23;
            var temp29 = -(temp0 * temp0);
            var temp30 = 0.5 + 0.5 * temp20 + temp29;
            var temp31 = -(temp16 * temp16);
            var temp32 = temp30 + temp31;
            var temp33 = px * temp32;
            var temp34 = temp4 * temp33;
            var temp35 = py * temp32;
            var temp36 = temp24 * temp35;
            var temp37 = -temp36;
            var temp38 = temp34 + temp37;
            var temp39 = -temp34;
            var temp40 = temp36 + temp39;
            var temp41 = temp38 * temp40;
            var temp42 = temp27 * temp28 - temp41;
            var temp43 = px * px;
            var temp44 = py * py;
            var temp45 = 1 + temp43 + temp44;
            var temp46 = pz * pz;
            var temp47 = temp45 + temp46;
            var temp48 = 0.5 * temp47;
            var temp49 = d4 * d4;
            var temp50 = temp48 + -0.5 * temp49;
            var temp51 = d1 * temp50;
            var temp52 = pz * temp21 - temp51;
            var temp53 = temp24 * temp52;
            var temp54 = -temp53;
            var temp55 = temp42 + temp53 * temp54;
            var temp56 = 0.5 + -0.5 * temp43 + -0.5 * temp44;
            var temp57 = -0.5 * temp46 + temp56;
            var temp58 = 0.5 * temp49 + temp57;
            var temp59 = d1 * temp58;
            var temp60 = pz * temp32 - temp59;
            var temp61 = temp24 * temp60;
            var temp62 = -temp61;
            var temp63 = temp61 * temp62;
            var temp64 = temp55 - temp63;
            var temp65 = temp32 * temp50;
            var temp66 = temp21 * temp58 - temp65;
            var temp67 = temp24 * temp66;
            var temp68 = -temp67;
            var temp69 = temp64 + temp67 * temp68;
            var temp70 = temp4 * temp52;
            var temp71 = -temp70;
            var temp72 = temp69 + temp70 * temp71;
            var temp73 = temp4 * temp60;
            var temp74 = -temp73;
            var temp75 = temp73 * temp74;
            var temp76 = temp72 - temp75;
            var temp77 = d1 * px;
            var temp78 = -temp4 * temp77;
            var temp79 = d1 * py;
            var temp80 = -temp24 * temp79;
            var temp81 = temp80 - temp78;
            var temp82 = temp78 - temp80;
            var temp83 = temp81 * temp82;
            var temp84 = temp76 - temp83;
            var temp85 = temp4 * temp66;
            var temp86 = -temp85;
            var temp87 = temp84 + temp85 * temp86;
            var temp88 = temp70 + temp73;
            var temp89 = temp71 + temp74;
            var temp90 = temp85 * temp86 + temp88 * temp89;
            var temp91 = temp28 + temp39;
            var temp92 = temp36 + temp91;
            var temp93 = temp27 + temp34;
            var temp94 = temp37 + temp93;
            var temp95 = temp90 + temp92 * temp94;
            var temp96 = temp53 + temp61;
            var temp97 = temp54 + temp62;
            var temp98 = temp95 + temp96 * temp97;
            var temp99 = temp67 * temp68 + temp98;
            var temp100 = temp85 * temp85 + temp99;
            var temp101 = temp67 * temp67 + temp100;
            var temp102 = 1 / temp101;
            var temp103 = temp87 * temp102;
            var temp104 = Math.Sqrt(Math.Abs(temp103));
            var temp105 = temp89 * temp89 + temp94 * temp94;
            var temp106 = -temp97;
            var temp107 = temp105 + temp106 * temp106;
            var temp108 = 1 / (Math.Sqrt(Math.Abs(temp107)));
            var temp109 = temp94 * temp108;
            var temp110 = temp104 * temp109;
            var temp111 = temp85 * temp102;
            var temp112 = -temp111;
            var temp113 = temp86 * temp102;
            var temp114 = -temp113;
            var temp115 = temp74 * temp114;
            var temp116 = temp71 * temp112 - temp115;
            var temp117 = temp92 * temp102;
            var temp118 = -temp117;
            var temp119 = temp81 * temp118;
            var temp120 = temp116 - temp119;
            var temp121 = temp67 * temp102;
            var temp122 = -temp121;
            var temp123 = temp120 + temp54 * temp122;
            var temp124 = temp68 * temp102;
            var temp125 = -temp124;
            var temp126 = temp62 * temp125;
            var temp127 = temp123 - temp126;
            var temp128 = temp88 * temp102;
            var temp129 = -temp128;
            var temp130 = temp71 * temp129;
            var temp131 = temp85 * temp114;
            var temp132 = -(temp130 + temp131);
            var temp133 = temp27 * temp118;
            var temp134 = temp132 - temp133;
            var temp135 = temp96 * temp102;
            var temp136 = -temp135;
            var temp137 = temp54 * temp136;
            var temp138 = temp134 - temp137;
            var temp139 = temp67 * temp125;
            var temp140 = temp138 - temp139;
            var temp141 = temp74 * temp129;
            var temp142 = temp85 * temp112;
            var temp143 = -(temp141 + temp142);
            var temp144 = temp140 + temp143;
            var temp145 = temp38 * temp118;
            var temp146 = temp144 - temp145;
            var temp147 = temp62 * temp136;
            var temp148 = temp146 - temp147;
            var temp149 = temp67 * temp122;
            var temp150 = temp148 - temp149;
            var temp151 = 1 / temp150;
            var temp152 = temp127 * temp151 - temp110;
            var temp153 = temp152 * temp152;
            var temp154 = temp81 * temp129;
            var temp155 = temp27 * temp112;
            var temp156 = -(temp154 + temp155);
            var temp157 = temp38 * temp114 + temp156;
            var temp158 = temp89 * temp108;
            var temp159 = temp104 * temp158;
            var temp160 = temp151 * temp157 - temp159;
            var temp161 = temp160 * temp160;
            var temp162 = 1 + temp153 + temp161;
            var temp163 = temp27 * temp122 + temp81 * temp136;
            var temp164 = temp38 * temp125;
            var temp165 = temp163 - temp164;
            var temp166 = temp106 * temp108;
            var temp167 = temp104 * temp166;
            var temp168 = temp151 * temp165 - temp167;
            var temp169 = temp168 * temp168;
            var temp170 = temp162 + temp169;
            var temp171 = 0.5 * temp170;
            var temp172 = d1 * temp171;
            var temp173 = temp19 * temp152 - temp172;
            var temp174 = 0.5 + -0.5 * temp153 + -0.5 * temp161;
            var temp175 = -0.5 * temp169 + temp174;
            var temp176 = d1 * temp175;
            var temp177 = temp173 - temp176;
            var temp178 = 0.5 + temp29 + temp31;
            var temp179 = temp177 + temp152 * temp178;
            var temp180 = d1 * temp179;
            var temp181 = d1 * temp160;
            var temp182 = -temp181;
            var temp183 = temp179 * temp179 + temp182 * temp182;
            var temp184 = temp181 * temp182 + temp183;
            var temp185 = d1 * temp168;
            var temp186 = -temp185;
            var temp187 = temp184 + temp186 * temp186;
            var temp188 = temp185 * temp186 + temp187;
            var temp189 = temp19 * temp160 + temp160 * temp178;
            var temp190 = temp188 + temp189 * temp189;
            var temp191 = temp19 * temp168 + temp168 * temp178;
            var temp192 = temp190 + temp191 * temp191;
            var temp193 = Math.Sqrt(temp192);
            var temp194 = (Math.Sqrt(d1 * d1)) * temp193;
            var theta2Cos = temp180 * 1 / temp194;

            var temp196 = temp48 * temp152;
            var temp197 = pz * temp171 - temp196;
            var temp198 = pz * temp175 + temp197;
            var temp199 = temp57 * temp152;
            var temp200 = temp198 - temp199;
            var temp201 = px * temp152;
            var temp202 = pz * temp160;
            var temp203 = temp202 - temp201;
            var temp204 = temp179 * temp200 + temp182 * temp203;
            var temp205 = temp201 - temp202;
            var temp206 = temp181 * temp205;
            var temp207 = temp204 - temp206;
            var temp208 = py * temp152;
            var temp209 = pz * temp168;
            var temp210 = temp209 - temp208;
            var temp211 = temp207 + temp186 * temp210;
            var temp212 = temp208 - temp209;
            var temp213 = temp185 * temp212;
            var temp214 = temp211 - temp213;
            var temp215 = temp48 * temp160;
            var temp216 = px * temp171 - temp215;
            var temp217 = px * temp175 + temp216;
            var temp218 = temp57 * temp160;
            var temp219 = temp217 - temp218;
            var temp220 = temp214 + temp189 * temp219;
            var temp221 = temp48 * temp168;
            var temp222 = py * temp171 - temp221;
            var temp223 = py * temp175 + temp222;
            var temp224 = temp57 * temp168;
            var temp225 = temp223 - temp224;
            var temp226 = temp220 + temp191 * temp225;
            var temp227 = temp219 * temp219 + temp225 * temp225;
            var temp228 = temp200 * temp200 + temp227;
            var temp229 = temp203 * temp203 + temp228;
            var temp230 = temp203 * temp205 + temp229;
            var temp231 = temp210 * temp210 + temp230;
            var temp232 = temp210 * temp212 + temp231;
            var temp233 = py * temp160;
            var temp234 = px * temp168;
            var temp235 = temp233 - temp234;
            var temp236 = temp232 + temp235 * temp235;
            var temp237 = temp234 - temp233;
            var temp238 = temp236 + temp235 * temp237;
            var temp239 = temp193 * (Math.Sqrt(temp238));
            var theta3Cos = temp226 * 1 / temp239;

            //Finish GA-FuL MetaContext Code Generation, 2024-06-16T17:13:00.5511484+03:00

            var theta1 = Math.Acos(theta1Cos) - Math.PI / 2;
            var theta2 = Math.Acos(theta2Cos);
            var theta3 = Math.Acos(theta3Cos);

            Console.WriteLine(theta1); // should equal 0.4375 radians
            Console.WriteLine();

            Console.WriteLine(theta2); // should equal 0.8590 radians
            Console.WriteLine();

            Console.WriteLine(theta3); // should equal 1.5040 radians
            Console.WriteLine();
        }
    }
}
