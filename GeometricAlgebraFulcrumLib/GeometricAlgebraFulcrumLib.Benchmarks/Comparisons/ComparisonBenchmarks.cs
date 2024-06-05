using System;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Comparisons
{
    [SimpleJob]
    public class ComparisonBenchmarks
    {
        public static void GetSphereCenterGaFuL(LinFloat64Vector3D p1, LinFloat64Vector3D p2, LinFloat64Vector3D p3, LinFloat64Vector3D p4, double[] center)
        {
            var p1X = p1.X.ScalarValue;
            var p1Y = p1.Y.ScalarValue;
            var p1Z = p1.Z.ScalarValue;
            var p1I = (p1X * p1X + p1Y * p1Y + p1Z * p1Z) / 2;

            var p2X = p2.X.ScalarValue;
            var p2Y = p2.Y.ScalarValue;
            var p2Z = p2.Z.ScalarValue;
            var p2I = (p1X * p1X + p1Y * p1Y + p1Z * p1Z) / 2;

            var p3X = p3.X.ScalarValue;
            var p3Y = p3.Y.ScalarValue;
            var p3Z = p3.Z.ScalarValue;
            var p3I = (p1X * p1X + p1Y * p1Y + p1Z * p1Z) / 2;

            var p4X = p4.X.ScalarValue;
            var p4Y = p4.Y.ScalarValue;
            var p4Z = p4.Z.ScalarValue;
            var p4I = (p1X * p1X + p1Y * p1Y + p1Z * p1Z) / 2;

            //Begin GA-FuL MetaContext Code Generation, 2024-05-24T18:58:36.3929014+03:00
            //MetaContext: CGA
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 79 sub-expressions, 0 generated temps, 79 total.
            //Target Temp Variables: 79 total.
            //Output Variables: 3 total.
            //Computations: 1.4634146341463414 average, 120 total.
            //Memory Reads: 2.3536585365853657 average, 193 total.
            //Memory Writes: 82 total.
            //
            //MetaContext Binding Data:
            //   -1 = constant: '-1'
            //   0.5 = constant: '0.5'
            //   1 = constant: '1'
            //   l1 = parameter: p1I
            //   x1 = parameter: p1X
            //   y1 = parameter: p1Y
            //   z1 = parameter: p1Z
            //   l2 = parameter: p2I
            //   x2 = parameter: p2X
            //   y2 = parameter: p2Y
            //   z2 = parameter: p2Z
            //   l3 = parameter: p3I
            //   x3 = parameter: p3X
            //   y3 = parameter: p3Y
            //   z3 = parameter: p3Z
            //   l4 = parameter: p4I
            //   x4 = parameter: p4X
            //   y4 = parameter: p4Y
            //   z4 = parameter: p4Z

            var temp0 = 0.5 + p3I;
            var temp1 = 0.5 - p1I;
            var temp2 = 0.5 - p2I;
            var temp3 = temp2 * p1X;
            var temp4 = temp1 * p2X - temp3;
            var temp5 = 0.5 + p1I;
            var temp6 = 0.5 + p2I;
            var temp7 = temp1 * temp6;
            var temp8 = temp2 * temp5 - temp7;
            var temp9 = temp0 * temp4 + temp8 * p3X;
            var temp10 = 0.5 - p3I;
            var temp11 = temp6 * p1X;
            var temp12 = temp5 * p2X - temp11;
            var temp13 = temp10 * temp12;
            var temp14 = temp9 - temp13;
            var temp15 = temp2 * p1Y;
            var temp16 = temp1 * p2Y - temp15;
            var temp17 = temp0 * temp16 + temp8 * p3Y;
            var temp18 = temp6 * p1Y;
            var temp19 = temp5 * p2Y - temp18;
            var temp20 = temp10 * temp19;
            var temp21 = temp17 - temp20;
            var temp22 = temp21 * p4X;
            var temp23 = temp14 * p4Y - temp22;
            var temp24 = p2X * p1Y;
            var temp25 = p1X * p2Y - temp24;
            var temp26 = temp0 * temp25 + temp12 * p3Y;
            var temp27 = temp19 * p3X;
            var temp28 = temp26 - temp27;
            var temp29 = 0.5 - p4I;
            var temp30 = temp23 + temp28 * temp29;
            var temp31 = temp16 * p3X;
            var temp32 = temp4 * p3Y - temp31;
            var temp33 = temp10 * temp25 + temp32;
            var temp34 = 0.5 + p4I;
            var temp35 = temp33 * temp34;
            var temp36 = temp30 - temp35;
            var temp37 = temp33 * p4Z;
            var temp38 = temp2 * p1Z;
            var temp39 = temp1 * p2Z - temp38;
            var temp40 = temp39 * p3X;
            var temp41 = temp4 * p3Z - temp40;
            var temp42 = p2X * p1Z;
            var temp43 = p1X * p2Z - temp42;
            var temp44 = temp41 + temp10 * temp43;
            var temp45 = temp44 * p4Y - temp37;
            var temp46 = temp39 * p3Y;
            var temp47 = temp16 * p3Z - temp46;
            var temp48 = p2Y * p1Z;
            var temp49 = p1Y * p2Z - temp48;
            var temp50 = temp47 + temp10 * temp49;
            var temp51 = temp50 * p4X;
            var temp52 = temp45 - temp51;
            var temp53 = temp43 * p3Y;
            var temp54 = temp25 * p3Z - temp53;
            var temp55 = temp54 + temp49 * p3X;
            var temp56 = temp52 + temp29 * temp55;
            var temp57 = 1 / temp56;
            center[3] = temp36 * temp57;

            var temp59 = temp14 * p4Z;
            var temp60 = temp6 * p1Z;
            var temp61 = temp5 * p2Z - temp60;
            var temp62 = temp10 * temp61;
            var temp63 = temp8 * p3Z - temp62;
            var temp64 = temp0 * temp39 + temp63;
            var temp65 = temp64 * p4X - temp59;
            var temp66 = temp61 * p3X;
            var temp67 = temp12 * p3Z - temp66;
            var temp68 = temp0 * temp43 + temp67;
            var temp69 = temp29 * temp68;
            var temp70 = temp65 - temp69;
            var temp71 = temp34 * temp44 + temp70;
            center[2] = temp57 * temp71;

            var temp73 = temp64 * p4Y;
            var temp74 = temp21 * p4Z - temp73;
            var temp75 = temp61 * p3Y;
            var temp76 = temp19 * p3Z - temp75;
            var temp77 = temp0 * temp49 + temp76;
            var temp78 = temp74 + temp29 * temp77;
            var temp79 = temp34 * temp50;
            var temp80 = temp78 - temp79;
            center[1] = temp57 * temp80;

            //Finish GA-FuL MetaContext Code Generation, 2024-05-24T18:58:36.4861315+03:00
        }

        public static void GetSphereCenterGaalop(LinFloat64Vector3D p1, LinFloat64Vector3D p2, LinFloat64Vector3D p3, LinFloat64Vector3D p4, double[] center)
        {
            var p1X = p1.X.ScalarValue;
            var p1Y = p1.Y.ScalarValue;
            var p1Z = p1.Z.ScalarValue;
            var p1I = (p1X * p1X + p1Y * p1Y + p1Z * p1Z) / 2;

            var p2X = p2.X.ScalarValue;
            var p2Y = p2.Y.ScalarValue;
            var p2Z = p2.Z.ScalarValue;
            var p2I = (p2X * p2X + p2Y * p2Y + p2Z * p2Z) / 2;

            var p3X = p3.X.ScalarValue;
            var p3Y = p3.Y.ScalarValue;
            var p3Z = p3.Z.ScalarValue;
            var p3I = (p3X * p3X + p3Y * p3Y + p3Z * p3Z) / 2;

            var p4X = p4.X.ScalarValue;
            var p4Y = p4.Y.ScalarValue;
            var p4Z = p4.Z.ScalarValue;
            var p4I = (p4X * p4X + p4Y * p4Y + p4Z * p4Z) / 2;

            var tempGcse1 = p1I * p2Z;
            var tempGcse3 = p1I * p2Y;
            var tempGcse4 = p2X - p1X;
            var tempGcse5 = tempGcse4 * p3Y + (p1Y - p2Y) * p3X;
            var tempGcse6 = (-tempGcse4) * p3Z + (p2Z - p1Z) * p3X - p1X * p2Z + p1Z * p2X;
            var tempGcse8 = p1X * p2Z - p1Z * p2X;
            var tempGcse9 = (tempGcse5 + p1X * p2Y - p1Y * p2X) * p4Z + tempGcse6 * p4Y + ((p2Y - p1Y) * p3Z + (p1Z - p2Z) * p3Y + p1Y * p2Z - p1Z * p2Y) * p4X + (p1Y * p2X - p1X * p2Y) * p3Z + tempGcse8 * p3Y + (p1Z * p2Y - p1Y * p2Z) * p3X;
            var tempGcse10 = (tempGcse5 + p1X * p2Y - p1Y * p2X) * p4Z + tempGcse6 * p4Y + ((p2Y - p1Y) * p3Z + (p1Z - p2Z) * p3Y + p1Y * p2Z - p1Z * p2Y) * p4X + (p1Y * p2X - p1X * p2Y) * p3Z;
            var tempGcse11 = (p1I - p2I) * p3Z + (p2Z - p1Z) * p3I - tempGcse1 + p1Z * p2I;
            var tempGcse12 = tempGcse1 - p1Z * p2I;
            var tempGcse13 = (tempGcse5 + p1X * p2Y - p1Y * p2X) * p4Z + tempGcse6 * p4Y + ((p2Y - p1Y) * p3Z + (p1Z - p2Z) * p3Y + p1Y * p2Z - p1Z * p2Y) * p4X + (p1Y * p2X - p1X * p2Y) * p3Z + tempGcse8 * p3Y;
            var tempGcse14 = (p2Z - p1Z) * p3I - tempGcse1;
            var tempGcse15 = p1Y * p2X;
            var tempGcse16 = ((p2Y - p1Y) * p3Z + (p1Z - p2Z) * p3Y + p1Y * p2Z - p1Z * p2Y) * p4X;
            var tempGcse17 = p1Y * p2Z;
            var tempGcse19 = p2I - p1I;
            var tempGcse20 = p1Y - p2Y;
            var tempGcse21 = p1Y * p2I;
            var tempGcse22 = (p2Z - p1Z) * p3X - p1X * p2Z;
            var tempGcse23 = tempGcse8 * p3Y;
            var tempGcse24 = (p1Z * p2Y - p1Y * p2Z) * p3X;
            var tempGcse25 = (p2Z - p1Z) * p3I;
            var tempGcse26 = (p1I - p2I) * p3Z + (p2Z - p1Z) * p3I - tempGcse1;
            var tempGcse27 = tempGcse19 * p3Y;
            var tempGcse28 = (p1I - p2I) * p3Z;
            var tempGcse29 = (-tempGcse20) * p3Z + (p1Z - p2Z) * p3Y + tempGcse17 - p1Z * p2Y;
            var tempGcse30 = (-tempGcse4) * p3Z + (p2Z - p1Z) * p3X - p1X * p2Z;
            var tempGcse32 = p1Z * p2I;
            var tempGcse33 = (tempGcse5 + p1X * p2Y - p1Y * p2X) * p4Z + tempGcse6 * p4Y;
            var tempGcse34 = p1Z * p2Y;
            var tempGcse35 = tempGcse3 - tempGcse21;
            var tempGcse36 = tempGcse17 - p1Z * p2Y;
            var tempGcse37 = p1Z * p2X;
            var tempGcse38 = (-tempGcse20) * p3Z + (p1Z - p2Z) * p3Y;
            var tempGcse39 = (p1Z - p2Z) * p3Y;
            var tempGcse40 = (p2Z - p1Z) * p3X;
            var tempGcse41 = (p1Y * p2X - p1X * p2Y) * p3Z;
            var tempGcse42 = p1X * p2Y - p1Y * p2X;
            var tempGcse43 = p1X * p2Y;
            var tempGcse44 = p1X * p2Z;
            var tempGcse45 = tempGcse20 * p3I;
            var tempGcse46 = (-tempGcse4) * p3Z;
            var tempGcse47 = tempGcse4 * p3Y;
            var tempGcse48 = (tempGcse5 + p1X * p2Y - p1Y * p2X) * p4Z;
            var tempGcse49 = tempGcse5 + p1X * p2Y - p1Y * p2X;
            var tempGcse50 = (-tempGcse20) * p3Z;
            var tempGcse53 = (p1Y - p2Y) * p3X;
            var tempGcse54 = p2Z - p1Z;
            var tempGcse55 = tempGcse27 + tempGcse45 + tempGcse35;
            var tempGcse56 = (tempGcse5 + p1X * p2Y - p1Y * p2X) * p4Z + tempGcse6 * p4Y + ((p2Y - p1Y) * p3Z + (p1Z - p2Z) * p3Y + p1Y * p2Z - p1Z * p2Y) * p4X;
            var tempGcse57 = tempGcse6 * p4Y;
            center[1] = (-(tempGcse55 * p4Z + tempGcse11 * p4Y + tempGcse29 * p4I + (-tempGcse35) * p3Z + tempGcse12 * p3Y + (-tempGcse36) * p3I)) / tempGcse9; // e1
            var tempGcse2 = p1I * p2X;
            var tempGcse7 = (p2I - p1I) * p3X + (-tempGcse4) * p3I + tempGcse2 - p1X * p2I;
            var tempGcse18 = (p2I - p1I) * p3X + (-tempGcse4) * p3I;
            var tempGcse31 = (p2I - p1I) * p3X;
            var tempGcse51 = (-tempGcse4) * p3I;
            var tempGcse52 = tempGcse2 - p1X * p2I;
            var tempGcse58 = p1X * p2I;
            center[2] = (tempGcse7 * p4Z + tempGcse11 * p4X + (-tempGcse6) * p4I + (-tempGcse52) * p3Z + tempGcse12 * p3X + (-tempGcse8) * p3I) / tempGcse9; // e2
            center[3] = (-(tempGcse7 * p4Y + (-tempGcse55) * p4X + tempGcse49 * p4I + (-tempGcse52) * p3Y + tempGcse35 * p3X + (-tempGcse42) * p3I)) / tempGcse9; // e3
        }


        public static void Get3SphereIntersectionGaFuL(LinFloat64Vector3D p1, LinFloat64Vector3D p2, LinFloat64Vector3D p3, double d14, double d24, double d34, double[] x1, double[] S1, double[] S2, double[] S3, double[] PP4, double[] DualPP4, double[] x4a, double[] x4b)
        {
            var a1 = p1.X.ScalarValue;
            var a2 = p1.Y.ScalarValue;
            var a3 = p1.Z.ScalarValue;

            var b1 = p2.X.ScalarValue;
            var b2 = p2.Y.ScalarValue;
            var b3 = p2.Z.ScalarValue;

            var c1 = p3.X.ScalarValue;
            var c2 = p3.Y.ScalarValue;
            var c3 = p3.Z.ScalarValue;

            //Begin GA-FuL MetaContext Code Generation, 2024-05-24T18:16:58.6624231+03:00
            //MetaContext:
            var tmpVar19306 = a1 * a1 + a3 * a3;
            var tmpVar19308 = a2 * a2 + tmpVar19306;
            var tmpVar10 = 0.5 + 0.5 * tmpVar19308;
            x1[1] = tmpVar10;

            var tmpVar11 = 0.5 + -0.5 * tmpVar19308;
            x1[2] = tmpVar11;

            var tmpVar19311 = d14 * d14;
            var tmpVar39 = tmpVar10 + -0.5 * tmpVar19311;
            S1[1] = tmpVar39;

            var tmpVar40 = tmpVar11 + 0.5 * tmpVar19311;
            S1[2] = tmpVar40;

            var tmpVar19316 = b1 * b1 + b3 * b3;
            var tmpVar19318 = b2 * b2 + tmpVar19316;
            var tmpVar19321 = d24 * d24;
            var tmpVar46 = 0.5 + 0.5 * tmpVar19318 + -0.5 * tmpVar19321;
            S2[1] = tmpVar46;

            var tmpVar47 = 0.5 + -0.5 * tmpVar19318 + 0.5 * tmpVar19321;
            S2[2] = tmpVar47;

            var tmpVar19328 = c1 * c1 + c2 * c2;
            var tmpVar19330 = c3 * c3 + tmpVar19328;
            var tmpVar19333 = d34 * d34;
            var tmpVar53 = 0.5 + 0.5 * tmpVar19330 + -0.5 * tmpVar19333;
            S3[1] = tmpVar53;

            var tmpVar54 = 0.5 + -0.5 * tmpVar19330 + 0.5 * tmpVar19333;
            S3[2] = tmpVar54;

            var tmpVar19338 = tmpVar40 * tmpVar46;
            var tmpVar19341 = -tmpVar19338 + tmpVar39 * tmpVar47;
            var tmpVar19343 = a1 * tmpVar46;
            var tmpVar19346 = -tmpVar19343 + b1 * tmpVar39;
            var tmpVar19347 = tmpVar19346 * tmpVar54;
            var tmpVar19349 = c1 * tmpVar19341 + -tmpVar19347;
            var tmpVar19350 = a1 * tmpVar47;
            var tmpVar19353 = -tmpVar19350 + b1 * tmpVar40;
            var tmpVar120 = tmpVar19349 + tmpVar19353 * tmpVar53;
            PP4[7] = tmpVar120;

            var tmpVar19356 = a2 * tmpVar46;
            var tmpVar19359 = -tmpVar19356 + b2 * tmpVar39;
            var tmpVar19360 = tmpVar19359 * tmpVar54;
            var tmpVar19362 = c2 * tmpVar19341 + -tmpVar19360;
            var tmpVar19363 = a2 * tmpVar47;
            var tmpVar19366 = -tmpVar19363 + b2 * tmpVar40;
            var tmpVar124 = tmpVar19362 + tmpVar19366 * tmpVar53;
            PP4[11] = tmpVar124;

            var tmpVar19369 = a3 * tmpVar46;
            var tmpVar19372 = -tmpVar19369 + b3 * tmpVar39;
            var tmpVar19373 = tmpVar19372 * tmpVar54;
            var tmpVar19375 = c3 * tmpVar19341 + -tmpVar19373;
            var tmpVar19376 = a3 * tmpVar47;
            var tmpVar19379 = -tmpVar19376 + b3 * tmpVar40;
            var tmpVar130 = tmpVar19375 + tmpVar19379 * tmpVar53;
            PP4[19] = tmpVar130;

            var tmpVar19382 = c1 * tmpVar19359;
            var tmpVar19384 = c2 * tmpVar19346 + -tmpVar19382;
            var tmpVar19386 = a2 * b1;
            var tmpVar19388 = a1 * b2 + -tmpVar19386;
            var tmpVar138 = tmpVar19384 + tmpVar19388 * tmpVar53;
            PP4[13] = tmpVar138;

            var tmpVar19391 = c1 * tmpVar19366;
            var tmpVar19393 = c2 * tmpVar19353 + -tmpVar19391;
            var tmpVar140 = tmpVar19393 + tmpVar19388 * tmpVar54;
            PP4[14] = tmpVar140;

            var tmpVar19396 = c1 * tmpVar19372;
            var tmpVar19398 = c3 * tmpVar19346 + -tmpVar19396;
            var tmpVar19400 = a3 * b1;
            var tmpVar19402 = a1 * b3 + -tmpVar19400;
            var tmpVar143 = tmpVar19398 + tmpVar19402 * tmpVar53;
            PP4[21] = tmpVar143;

            var tmpVar19405 = -1 * c1 * tmpVar19379;
            var tmpVar19407 = c3 * tmpVar19353 + tmpVar19405;
            var tmpVar145 = tmpVar19407 + tmpVar19402 * tmpVar54;
            PP4[22] = tmpVar145;

            var tmpVar19410 = -c2;
            var tmpVar19412 = c3 * tmpVar19359 + tmpVar19372 * tmpVar19410;
            var tmpVar19414 = a3 * b2;
            var tmpVar19416 = a2 * b3 + -tmpVar19414;
            var tmpVar150 = tmpVar19412 + tmpVar19416 * tmpVar53;
            PP4[25] = tmpVar150;

            var tmpVar19420 = c3 * tmpVar19366 + tmpVar19379 * tmpVar19410;
            var tmpVar152 = tmpVar19420 + tmpVar19416 * tmpVar54;
            PP4[26] = tmpVar152;

            var tmpVar19424 = c3 * tmpVar19388 + c1 * tmpVar19416;
            var tmpVar154 = tmpVar19402 * tmpVar19410 + tmpVar19424;
            PP4[28] = tmpVar154;

            var tmpVar155 = -tmpVar120;
            DualPP4[24] = tmpVar155;

            var tmpVar157 = tmpVar124;
            DualPP4[20] = tmpVar157;

            var tmpVar158 = -tmpVar130;
            DualPP4[12] = tmpVar158;

            var tmpVar159 = -tmpVar138;
            DualPP4[18] = tmpVar159;

            var tmpVar161 = tmpVar143;
            DualPP4[10] = tmpVar161;

            var tmpVar162 = -tmpVar150;
            DualPP4[6] = tmpVar162;

            var tmpVar163 = -tmpVar140;
            DualPP4[17] = tmpVar163;

            var tmpVar165 = tmpVar145;
            DualPP4[9] = tmpVar165;

            var tmpVar166 = -tmpVar152;
            DualPP4[5] = tmpVar166;

            var tmpVar168 = tmpVar154;
            DualPP4[3] = tmpVar168;

            var tmpVar19427 = -tmpVar163;
            var tmpVar19428 = -tmpVar159 + tmpVar19427;
            var tmpVar19431 = -tmpVar165;
            var tmpVar19432 = -tmpVar161 + tmpVar19431;
            var tmpVar19434 = tmpVar19428 * tmpVar19428 + tmpVar19432 * tmpVar19432;
            var tmpVar19437 = -tmpVar162 + -tmpVar166;
            var tmpVar19439 = tmpVar19434 + tmpVar19437 * tmpVar19437;
            var tmpVar19440 = 1 / tmpVar19439;
            var tmpVar19441 = tmpVar19428 * tmpVar19440;
            var tmpVar19442 = tmpVar155 * tmpVar19441;
            var tmpVar19444 = tmpVar19437 * tmpVar19440;
            var tmpVar19446 = -tmpVar19442 + tmpVar158 * tmpVar19444;
            var tmpVar19448 = -1 * tmpVar168 * tmpVar19440;
            var tmpVar19450 = tmpVar19446 + tmpVar161 * tmpVar19448;
            var tmpVar19451 = tmpVar168 * tmpVar19440;
            var tmpVar19452 = tmpVar165 * tmpVar19451;
            var tmpVar19454 = tmpVar19450 + -tmpVar19452;
            var tmpVar19457 = tmpVar163 * tmpVar163 + tmpVar168 * tmpVar168;
            var tmpVar19459 = tmpVar165 * tmpVar165 + tmpVar19457;
            var tmpVar19461 = tmpVar166 * tmpVar166 + tmpVar19459;
            var tmpVar19466 = -(tmpVar161 * tmpVar161) + -(tmpVar162 * tmpVar162);
            var tmpVar19467 = tmpVar19461 + tmpVar19466;
            var tmpVar19472 = -(tmpVar158 * tmpVar158) + -(tmpVar159 * tmpVar159);
            var tmpVar19473 = tmpVar19467 + tmpVar19472;
            var tmpVar19478 = -(tmpVar155 * tmpVar155) + -(tmpVar157 * tmpVar157);
            var tmpVar19479 = tmpVar19473 + tmpVar19478;
            var tmpVar19480 = Math.Sqrt(tmpVar19479);
            var tmpVar19482 = tmpVar19432 * tmpVar19440;
            var tmpVar19483 = -1 * tmpVar19480 * tmpVar19482;
            x4a[8] = tmpVar19454 + -tmpVar19483;

            var tmpVar19487 = tmpVar157 * tmpVar19444 + tmpVar159 * tmpVar19448;
            var tmpVar19489 = tmpVar155 * tmpVar19482 + tmpVar19487;
            var tmpVar19490 = tmpVar163 * tmpVar19451;
            var tmpVar19492 = tmpVar19489 + -tmpVar19490;
            var tmpVar19493 = -tmpVar19441;
            var tmpVar19494 = tmpVar19480 * tmpVar19493;
            x4a[16] = tmpVar19492 + -tmpVar19494;

            var tmpVar19498 = tmpVar162 * tmpVar19448 + tmpVar157 * tmpVar19493;
            var tmpVar19500 = -1 * tmpVar158 * tmpVar19482;
            var tmpVar19501 = tmpVar19498 + tmpVar19500;
            var tmpVar19502 = tmpVar166 * tmpVar19451;
            var tmpVar19504 = tmpVar19501 + -tmpVar19502;
            var tmpVar19506 = -1 * tmpVar19444 * tmpVar19480;
            x4a[4] = tmpVar19504 + -tmpVar19506;

            var tmpVar19509 = tmpVar161 * tmpVar19482;
            var tmpVar19511 = tmpVar159 * tmpVar19493 + -tmpVar19509;
            var tmpVar19512 = tmpVar162 * tmpVar19444;
            var tmpVar19514 = tmpVar19511 + -tmpVar19512;
            var tmpVar19515 = tmpVar168 * tmpVar19451;
            var tmpVar19517 = tmpVar19514 + -tmpVar19515;
            var tmpVar19519 = -1 * tmpVar19448 * tmpVar19480;
            x4a[2] = tmpVar19517 + -tmpVar19519;

            var tmpVar19523 = tmpVar19427 * tmpVar19441 + tmpVar19431 * tmpVar19482;
            var tmpVar19524 = tmpVar166 * tmpVar19444;
            var tmpVar19526 = tmpVar19523 + -tmpVar19524;
            var tmpVar19527 = tmpVar168 * tmpVar19448;
            var tmpVar19529 = tmpVar19526 + -tmpVar19527;
            var tmpVar19531 = -1 * tmpVar19451 * tmpVar19480;
            x4a[1] = tmpVar19529 + -tmpVar19531;

            x4b[8] = tmpVar19454 + tmpVar19483;

            x4b[16] = tmpVar19492 + tmpVar19494;

            x4b[4] = tmpVar19504 + tmpVar19506;

            x4b[2] = tmpVar19517 + tmpVar19519;

            x4b[1] = tmpVar19529 + tmpVar19531;

            x1[4] = a1;

            x1[8] = a2;

            x1[16] = a3;

            S1[4] = a1;

            S1[8] = a2;

            S1[16] = a3;

            S2[4] = b1;

            S2[8] = b2;

            S2[16] = b3;

            S3[4] = c1;

            S3[8] = c2;

            S3[16] = c3;

            //Finish GA-FuL MetaContext Code Generation, 2024-05-24T18:16:58.6626433+03:00

        }

        public static void Get3SphereIntersectionGaalop(LinFloat64Vector3D p1, LinFloat64Vector3D p2, LinFloat64Vector3D p3, double d14, double d24, double d34, double[] x1, double[] S1, double[] S2, double[] S3, double[] PP4, double[] DualPP4, double[] x4a, double[] x4b)
        {
            var a1 = p1.X.ScalarValue;
            var a2 = p1.Y.ScalarValue;
            var a3 = p1.Z.ScalarValue;

            var b1 = p2.X.ScalarValue;
            var b2 = p2.Y.ScalarValue;
            var b3 = p2.Z.ScalarValue;

            var c1 = p3.X.ScalarValue;
            var c2 = p3.Y.ScalarValue;
            var c3 = p3.Z.ScalarValue;


            double temp_gcse_1, temp_gcse_10, temp_gcse_11, temp_gcse_12, temp_gcse_13, temp_gcse_14, temp_gcse_15, temp_gcse_17, temp_gcse_18, temp_gcse_19, temp_gcse_2, temp_gcse_21, temp_gcse_22, temp_gcse_24, temp_gcse_25, temp_gcse_26, temp_gcse_27, temp_gcse_28, temp_gcse_29, temp_gcse_3, temp_gcse_30, temp_gcse_31, temp_gcse_32, temp_gcse_33, temp_gcse_34, temp_gcse_35, temp_gcse_36, temp_gcse_37, temp_gcse_38, temp_gcse_39, temp_gcse_4, temp_gcse_40, temp_gcse_41, temp_gcse_42, temp_gcse_43, temp_gcse_44, temp_gcse_45, temp_gcse_47, temp_gcse_48, temp_gcse_49, temp_gcse_5, temp_gcse_50, temp_gcse_51, temp_gcse_52, temp_gcse_53, temp_gcse_54, temp_gcse_55, temp_gcse_56, temp_gcse_58, temp_gcse_59, temp_gcse_6, temp_gcse_61, temp_gcse_62, temp_gcse_64, temp_gcse_65, temp_gcse_66, temp_gcse_67, temp_gcse_69, temp_gcse_7, temp_gcse_70, temp_gcse_72, temp_gcse_74, temp_gcse_75, temp_gcse_76, temp_gcse_77, temp_gcse_78, temp_gcse_8, temp_gcse_80, temp_gcse_81, temp_gcse_82, temp_gcse_84, temp_gcse_9;
            x1[1] = a1; // e1
            x1[2] = a2; // e2
            x1[3] = a3; // e3
            x1[4] = (a3 * a3) / 2.0 + (a2 * a2) / 2.0 + (a1 * a1) / 2.0; // einf
            x1[5] = 1.0; // e0
            S1[1] = x1[1]; // e1
            S1[2] = x1[2]; // e2
            S1[3] = x1[3]; // e3
            S1[4] = x1[4] - (d14 * d14) / 2.0; // einf
            S1[5] = 1.0; // e0
            S2[1] = b1; // e1
            S2[2] = b2; // e2
            S2[3] = b3; // e3
            S2[4] = (-((d24 * d24) / 2.0)) + (b3 * b3) / 2.0 + (b2 * b2) / 2.0 + (b1 * b1) / 2.0; // einf
            S2[5] = 1.0; // e0
            S3[1] = c1; // e1
            S3[2] = c2; // e2
            S3[3] = c3; // e3
            S3[4] = (-((d34 * d34) / 2.0)) + (c3 * c3) / 2.0 + (c2 * c2) / 2.0 + (c1 * c1) / 2.0; // einf
            S3[5] = 1.0; // e0
            temp_gcse_7 = S1[3] * S2[1] - S1[1] * S2[3];
            temp_gcse_22 = S1[1] * S2[2];
            temp_gcse_26 = temp_gcse_22 - S1[2] * S2[1];
            temp_gcse_29 = S1[1] * S2[3];
            temp_gcse_30 = S1[2] * S2[3];
            temp_gcse_49 = S1[2] * S2[1];
            temp_gcse_50 = temp_gcse_30 - S1[3] * S2[2];
            temp_gcse_75 = S1[3] * S2[1];
            temp_gcse_82 = S1[3] * S2[2];
            PP4[16] = temp_gcse_26 * S3[3] + temp_gcse_7 * S3[2] + temp_gcse_50 * S3[1]; // e1 ^ (e2 ^ e3)
            temp_gcse_10 = S1[4] * S2[1] - S1[1] * S2[4];
            temp_gcse_28 = S1[2] * S2[4] - S1[4] * S2[2];
            temp_gcse_33 = S1[2] * S2[4];
            temp_gcse_40 = S1[1] * S2[4];
            temp_gcse_61 = S1[4] * S2[1];
            temp_gcse_62 = S1[4] * S2[2];
            PP4[17] = temp_gcse_26 * S3[4] + temp_gcse_10 * S3[2] + temp_gcse_28 * S3[1]; // e1 ^ (e2 ^ einf)
            temp_gcse_15 = S2[1] - S1[1];
            temp_gcse_72 = S1[2] - S2[2];
            PP4[18] = temp_gcse_15 * S3[2] + temp_gcse_72 * S3[1] + temp_gcse_26; // e1 ^ (e2 ^ e0)
            temp_gcse_36 = S1[3] * S2[4] - S1[4] * S2[3];
            temp_gcse_42 = S1[4] * S2[3];
            temp_gcse_69 = S1[3] * S2[4];
            PP4[19] = (-temp_gcse_7) * S3[4] + temp_gcse_10 * S3[3] + temp_gcse_36 * S3[1]; // e1 ^ (e3 ^ einf)
            temp_gcse_34 = S1[3] - S2[3];
            PP4[20] = temp_gcse_15 * S3[3] + temp_gcse_34 * S3[1] + (-temp_gcse_7); // e1 ^ (e3 ^ e0)
            temp_gcse_84 = S1[4] - S2[4];
            PP4[21] = temp_gcse_15 * S3[4] + temp_gcse_84 * S3[1] + (-temp_gcse_10); // e1 ^ (einf ^ e0)
            PP4[22] = temp_gcse_50 * S3[4] + (-temp_gcse_28) * S3[3] + temp_gcse_36 * S3[2]; // e2 ^ (e3 ^ einf)
            PP4[23] = (-temp_gcse_72) * S3[3] + temp_gcse_34 * S3[2] + temp_gcse_50; // e2 ^ (e3 ^ e0)
            PP4[24] = (-temp_gcse_72) * S3[4] + temp_gcse_84 * S3[2] + temp_gcse_28; // e2 ^ (einf ^ e0)
            PP4[25] = (-temp_gcse_34) * S3[4] + temp_gcse_84 * S3[3] + temp_gcse_36; // e3 ^ (einf ^ e0)
            DualPP4[6] = (-PP4[25]); // e1 ^ e2
            DualPP4[7] = PP4[24]; // e1 ^ e3
            DualPP4[8] = (-PP4[22]); // e1 ^ einf
            DualPP4[9] = PP4[23]; // e1 ^ e0
            DualPP4[10] = (-PP4[21]); // e2 ^ e3
            DualPP4[11] = PP4[19]; // e2 ^ einf
            DualPP4[12] = (-PP4[20]); // e2 ^ e0
            DualPP4[13] = (-PP4[17]); // e3 ^ einf
            DualPP4[14] = PP4[18]; // e3 ^ e0
            DualPP4[15] = PP4[16]; // einf ^ e0
            temp_gcse_4 = DualPP4[10] * DualPP4[10];
            temp_gcse_11 = DualPP4[15] * DualPP4[9];
            temp_gcse_24 = 2.0 * DualPP4[8] * DualPP4[9] - DualPP4[7] * DualPP4[7];
            temp_gcse_25 = DualPP4[6] * DualPP4[6];
            temp_gcse_27 = DualPP4[14] * DualPP4[7];
            temp_gcse_32 = DualPP4[14] * DualPP4[14];
            temp_gcse_37 = temp_gcse_24 - temp_gcse_25 + DualPP4[15] * DualPP4[15];
            temp_gcse_38 = DualPP4[7] * DualPP4[7];
            temp_gcse_39 = DualPP4[12] * DualPP4[6];
            temp_gcse_41 = temp_gcse_37 + 2.0 * DualPP4[13] * DualPP4[14];
            temp_gcse_44 = 2.0 * DualPP4[8] * DualPP4[9];
            temp_gcse_45 = 2.0 * DualPP4[13] * DualPP4[14];
            temp_gcse_47 = Math.Sqrt(temp_gcse_41 + 2.0 * DualPP4[11] * DualPP4[12] - temp_gcse_4);
            temp_gcse_48 = DualPP4[9] * DualPP4[9] + temp_gcse_32;
            temp_gcse_53 = DualPP4[9] * DualPP4[9];
            temp_gcse_55 = 2.0 * DualPP4[8];
            temp_gcse_56 = DualPP4[9] * temp_gcse_47;
            temp_gcse_58 = 2.0 * DualPP4[11];
            temp_gcse_59 = temp_gcse_41 + 2.0 * DualPP4[11] * DualPP4[12] - temp_gcse_4;
            temp_gcse_64 = temp_gcse_48 + DualPP4[12] * DualPP4[12];
            temp_gcse_65 = DualPP4[15] * DualPP4[15];
            temp_gcse_67 = temp_gcse_24 - temp_gcse_25;
            temp_gcse_70 = 2.0 * DualPP4[11] * DualPP4[12] - temp_gcse_4;
            temp_gcse_74 = 2.0 * DualPP4[11] * DualPP4[12];
            temp_gcse_76 = DualPP4[12] * DualPP4[12];
            temp_gcse_80 = 2.0 * DualPP4[13];
            x4a[1] = (temp_gcse_56 + temp_gcse_11 - temp_gcse_27 - temp_gcse_39) / temp_gcse_64; // e1
            temp_gcse_6 = DualPP4[12] * Math.Sqrt(2.0 * DualPP4[8] * DualPP4[9] - DualPP4[7] * DualPP4[7] - DualPP4[6] * DualPP4[6] + DualPP4[15] * DualPP4[15] + 2.0 * DualPP4[13] * DualPP4[14] + 2.0 * DualPP4[11] * DualPP4[12] - temp_gcse_4);
            temp_gcse_8 = DualPP4[12] * DualPP4[15];
            temp_gcse_35 = DualPP4[10] * DualPP4[14];
            temp_gcse_77 = DualPP4[6] * DualPP4[9];
            x4a[2] = (temp_gcse_6 + temp_gcse_77 + temp_gcse_8 - temp_gcse_35) / temp_gcse_64; // e2
            temp_gcse_31 = DualPP4[14] * DualPP4[15];
            temp_gcse_51 = DualPP4[10] * DualPP4[12];
            temp_gcse_54 = DualPP4[7] * DualPP4[9];
            temp_gcse_78 = DualPP4[14] * temp_gcse_47;
            x4a[3] = (temp_gcse_78 + temp_gcse_54 + temp_gcse_31 + temp_gcse_51) / temp_gcse_64; // e3
            temp_gcse_5 = DualPP4[11] * DualPP4[12];
            temp_gcse_9 = DualPP4[15] * Math.Sqrt(2.0 * DualPP4[8] * DualPP4[9] - DualPP4[7] * DualPP4[7] - DualPP4[6] * DualPP4[6] + DualPP4[15] * DualPP4[15] + 2.0 * DualPP4[13] * DualPP4[14] + 2.0 * DualPP4[11] * DualPP4[12] - temp_gcse_4);
            temp_gcse_17 = DualPP4[13] * DualPP4[14];
            temp_gcse_81 = DualPP4[8] * DualPP4[9];
            x4a[4] = (temp_gcse_9 + temp_gcse_81 + temp_gcse_65 + temp_gcse_17 + temp_gcse_5) / temp_gcse_64; // einf
            x4a[5] = 1.0; // e0
            temp_gcse_2 = DualPP4[10] * DualPP4[9];
            temp_gcse_14 = DualPP4[12] * DualPP4[7];
            temp_gcse_19 = DualPP4[14] * DualPP4[6];
            temp_gcse_52 = (-(temp_gcse_2 - temp_gcse_14 + temp_gcse_19));
            x4a[16] = temp_gcse_52 / (temp_gcse_48 + DualPP4[12] * DualPP4[12]); // e1 ^ (e2 ^ e3)
            temp_gcse_3 = DualPP4[11] * DualPP4[9];
            x4a[17] = (-(temp_gcse_3 - DualPP4[12] * DualPP4[8] + DualPP4[15] * DualPP4[6])) / (DualPP4[9] * DualPP4[9] + DualPP4[14] * DualPP4[14] + DualPP4[12] * DualPP4[12]); // e1 ^ (e2 ^ einf)
            temp_gcse_1 = DualPP4[13] * DualPP4[9];
            temp_gcse_12 = DualPP4[15] * DualPP4[7];
            temp_gcse_21 = DualPP4[14] * DualPP4[8];
            temp_gcse_43 = temp_gcse_1 - temp_gcse_21;
            temp_gcse_66 = temp_gcse_43 + temp_gcse_12;
            x4a[19] = (-temp_gcse_66) / temp_gcse_64; // e1 ^ (e3 ^ einf)
            temp_gcse_13 = DualPP4[10] * DualPP4[15] - DualPP4[11] * DualPP4[14];
            temp_gcse_18 = DualPP4[11] * DualPP4[14];
            x4a[22] = (-(temp_gcse_13 + DualPP4[12] * DualPP4[13])) / (DualPP4[9] * DualPP4[9] + temp_gcse_32 + DualPP4[12] * DualPP4[12]); // e2 ^ (e3 ^ einf)
            x4b[1] = (-(temp_gcse_56 - temp_gcse_11 + temp_gcse_27 + temp_gcse_39)) / temp_gcse_64; // e1
            x4b[2] = (-(temp_gcse_6 - temp_gcse_77 - temp_gcse_8 + temp_gcse_35)) / temp_gcse_64; // e2
            x4b[3] = (-(temp_gcse_78 - temp_gcse_54 - temp_gcse_31 - temp_gcse_51)) / temp_gcse_64; // e3
            x4b[4] = (-(temp_gcse_9 - temp_gcse_81 - temp_gcse_65 - temp_gcse_17 - temp_gcse_5)) / temp_gcse_64; // einf
            x4b[5] = 1.0; // e0
            x4b[16] = x4a[16]; // e1 ^ (e2 ^ e3)
            x4b[17] = x4a[17]; // e1 ^ (e2 ^ einf)
            x4b[19] = x4a[19]; // e1 ^ (e3 ^ einf)
            x4b[22] = x4a[22]; // e2 ^ (e3 ^ einf)
        }


        public int Count => 1000;

        public LinFloat64Vector3D[] Point1Array { get; set; }

        public LinFloat64Vector3D[] Point2Array { get; set; }

        public LinFloat64Vector3D[] Point3Array { get; set; }

        public LinFloat64Vector3D[] Point4Array { get; set; }

        public double[] D14 { get; set; }

        public double[] D24 { get; set; }

        public double[] D34 { get; set; }


        public double[] Center { get; } = new double[32];

        public double[] X1 { get; } = new double[32];

        public double[] S1 { get; } = new double[32];

        public double[] S2 { get; } = new double[32];

        public double[] S3 { get; } = new double[32];

        public double[] PP4 { get; } = new double[32];

        public double[] DualPP4 { get; } = new double[32];

        public double[] X4a { get; } = new double[32];

        public double[] X4b { get; } = new double[32];


        [GlobalSetup]
        public void Setup()
        {
            var randGen = new Random(10);

            Point1Array = new LinFloat64Vector3D[Count];
            Point2Array = new LinFloat64Vector3D[Count];
            Point3Array = new LinFloat64Vector3D[Count];
            Point4Array = new LinFloat64Vector3D[Count];
            D14 = new double[Count];
            D24 = new double[Count];
            D34 = new double[Count];

            for (var i = 0; i < Count; i++)
            {
                Point1Array[i] = randGen.GetLinVector3D();
                Point2Array[i] = randGen.GetLinVector3D();
                Point3Array[i] = randGen.GetLinVector3D();
                Point4Array[i] = randGen.GetLinVector3D();

                D14[i] = randGen.GetNumber(1, 10);
                D24[i] = randGen.GetNumber(1, 10);
                D34[i] = randGen.GetNumber(1, 10);
            }
        }


        [Benchmark]
        public void GetSphereCenterGaFuL()
        {
            for (var i = 0; i < Count; i++)
            {
                GetSphereCenterGaFuL(
                    Point1Array[i],
                    Point2Array[i],
                    Point3Array[i],
                    Point4Array[i],
                    Center
                );
            }
        }

        [Benchmark]
        public void GetSphereCenterGaalop()
        {
            for (var i = 0; i < Count; i++)
            {
                GetSphereCenterGaalop(
                    Point1Array[i],
                    Point2Array[i],
                    Point3Array[i],
                    Point4Array[i],
                    Center
                );
            }
        }


        [Benchmark]
        public void Get3SphereIntersectionGaFuL()
        {
            for (var i = 0; i < Count; i++)
            {
                Get3SphereIntersectionGaFuL(
                    Point1Array[i],
                    Point2Array[i],
                    Point3Array[i],
                    D14[i],
                    D24[i],
                    D34[i],
                    X1,
                    S1,
                    S2,
                    S3,
                    PP4,
                    DualPP4,
                    X4a,
                    X4b
                );
            }
        }

        [Benchmark]
        public void Get3SphereIntersectionGaalop()
        {
            for (var i = 0; i < Count; i++)
            {
                Get3SphereIntersectionGaalop(
                    Point1Array[i],
                    Point2Array[i],
                    Point3Array[i],
                    D14[i],
                    D24[i],
                    D34[i],
                    X1,
                    S1,
                    S2,
                    S3,
                    PP4,
                    DualPP4,
                    X4a,
                    X4b
                );
            }
        }

    }

}

