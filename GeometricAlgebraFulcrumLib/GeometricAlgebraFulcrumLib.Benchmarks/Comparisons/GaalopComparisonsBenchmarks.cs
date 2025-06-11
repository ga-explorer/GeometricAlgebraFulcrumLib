using System;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Comparisons
{
    [SimpleJob]
    public class GaalopComparisonsBenchmarks
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
            var tempGcse6 = -tempGcse4 * p3Z + (p2Z - p1Z) * p3X - p1X * p2Z + p1Z * p2X;
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
            var tempGcse29 = -tempGcse20 * p3Z + (p1Z - p2Z) * p3Y + tempGcse17 - p1Z * p2Y;
            var tempGcse30 = -tempGcse4 * p3Z + (p2Z - p1Z) * p3X - p1X * p2Z;
            var tempGcse32 = p1Z * p2I;
            var tempGcse33 = (tempGcse5 + p1X * p2Y - p1Y * p2X) * p4Z + tempGcse6 * p4Y;
            var tempGcse34 = p1Z * p2Y;
            var tempGcse35 = tempGcse3 - tempGcse21;
            var tempGcse36 = tempGcse17 - p1Z * p2Y;
            var tempGcse37 = p1Z * p2X;
            var tempGcse38 = -tempGcse20 * p3Z + (p1Z - p2Z) * p3Y;
            var tempGcse39 = (p1Z - p2Z) * p3Y;
            var tempGcse40 = (p2Z - p1Z) * p3X;
            var tempGcse41 = (p1Y * p2X - p1X * p2Y) * p3Z;
            var tempGcse42 = p1X * p2Y - p1Y * p2X;
            var tempGcse43 = p1X * p2Y;
            var tempGcse44 = p1X * p2Z;
            var tempGcse45 = tempGcse20 * p3I;
            var tempGcse46 = -tempGcse4 * p3Z;
            var tempGcse47 = tempGcse4 * p3Y;
            var tempGcse48 = (tempGcse5 + p1X * p2Y - p1Y * p2X) * p4Z;
            var tempGcse49 = tempGcse5 + p1X * p2Y - p1Y * p2X;
            var tempGcse50 = -tempGcse20 * p3Z;
            var tempGcse53 = (p1Y - p2Y) * p3X;
            var tempGcse54 = p2Z - p1Z;
            var tempGcse55 = tempGcse27 + tempGcse45 + tempGcse35;
            var tempGcse56 = (tempGcse5 + p1X * p2Y - p1Y * p2X) * p4Z + tempGcse6 * p4Y + ((p2Y - p1Y) * p3Z + (p1Z - p2Z) * p3Y + p1Y * p2Z - p1Z * p2Y) * p4X;
            var tempGcse57 = tempGcse6 * p4Y;
            center[1] = -(tempGcse55 * p4Z + tempGcse11 * p4Y + tempGcse29 * p4I + -tempGcse35 * p3Z + tempGcse12 * p3Y + -tempGcse36 * p3I) / tempGcse9; // e1
            var tempGcse2 = p1I * p2X;
            var tempGcse7 = (p2I - p1I) * p3X + -tempGcse4 * p3I + tempGcse2 - p1X * p2I;
            var tempGcse18 = (p2I - p1I) * p3X + -tempGcse4 * p3I;
            var tempGcse31 = (p2I - p1I) * p3X;
            var tempGcse51 = -tempGcse4 * p3I;
            var tempGcse52 = tempGcse2 - p1X * p2I;
            var tempGcse58 = p1X * p2I;
            center[2] = (tempGcse7 * p4Z + tempGcse11 * p4X + -tempGcse6 * p4I + -tempGcse52 * p3Z + tempGcse12 * p3X + -tempGcse8 * p3I) / tempGcse9; // e2
            center[3] = -(tempGcse7 * p4Y + -tempGcse55 * p4X + tempGcse49 * p4I + -tempGcse52 * p3Y + tempGcse35 * p3X + -tempGcse42 * p3I) / tempGcse9; // e3
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
            x1[4] = a3 * a3 / 2.0 + a2 * a2 / 2.0 + a1 * a1 / 2.0; // einf
            x1[5] = 1.0; // e0
            S1[1] = x1[1]; // e1
            S1[2] = x1[2]; // e2
            S1[3] = x1[3]; // e3
            S1[4] = x1[4] - d14 * d14 / 2.0; // einf
            S1[5] = 1.0; // e0
            S2[1] = b1; // e1
            S2[2] = b2; // e2
            S2[3] = b3; // e3
            S2[4] = -(d24 * d24 / 2.0) + b3 * b3 / 2.0 + b2 * b2 / 2.0 + b1 * b1 / 2.0; // einf
            S2[5] = 1.0; // e0
            S3[1] = c1; // e1
            S3[2] = c2; // e2
            S3[3] = c3; // e3
            S3[4] = -(d34 * d34 / 2.0) + c3 * c3 / 2.0 + c2 * c2 / 2.0 + c1 * c1 / 2.0; // einf
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
            PP4[19] = -temp_gcse_7 * S3[4] + temp_gcse_10 * S3[3] + temp_gcse_36 * S3[1]; // e1 ^ (e3 ^ einf)
            temp_gcse_34 = S1[3] - S2[3];
            PP4[20] = temp_gcse_15 * S3[3] + temp_gcse_34 * S3[1] + -temp_gcse_7; // e1 ^ (e3 ^ e0)
            temp_gcse_84 = S1[4] - S2[4];
            PP4[21] = temp_gcse_15 * S3[4] + temp_gcse_84 * S3[1] + -temp_gcse_10; // e1 ^ (einf ^ e0)
            PP4[22] = temp_gcse_50 * S3[4] + -temp_gcse_28 * S3[3] + temp_gcse_36 * S3[2]; // e2 ^ (e3 ^ einf)
            PP4[23] = -temp_gcse_72 * S3[3] + temp_gcse_34 * S3[2] + temp_gcse_50; // e2 ^ (e3 ^ e0)
            PP4[24] = -temp_gcse_72 * S3[4] + temp_gcse_84 * S3[2] + temp_gcse_28; // e2 ^ (einf ^ e0)
            PP4[25] = -temp_gcse_34 * S3[4] + temp_gcse_84 * S3[3] + temp_gcse_36; // e3 ^ (einf ^ e0)
            DualPP4[6] = -PP4[25]; // e1 ^ e2
            DualPP4[7] = PP4[24]; // e1 ^ e3
            DualPP4[8] = -PP4[22]; // e1 ^ einf
            DualPP4[9] = PP4[23]; // e1 ^ e0
            DualPP4[10] = -PP4[21]; // e2 ^ e3
            DualPP4[11] = PP4[19]; // e2 ^ einf
            DualPP4[12] = -PP4[20]; // e2 ^ e0
            DualPP4[13] = -PP4[17]; // e3 ^ einf
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
            temp_gcse_52 = -(temp_gcse_2 - temp_gcse_14 + temp_gcse_19);
            x4a[16] = temp_gcse_52 / (temp_gcse_48 + DualPP4[12] * DualPP4[12]); // e1 ^ (e2 ^ e3)
            temp_gcse_3 = DualPP4[11] * DualPP4[9];
            x4a[17] = -(temp_gcse_3 - DualPP4[12] * DualPP4[8] + DualPP4[15] * DualPP4[6]) / (DualPP4[9] * DualPP4[9] + DualPP4[14] * DualPP4[14] + DualPP4[12] * DualPP4[12]); // e1 ^ (e2 ^ einf)
            temp_gcse_1 = DualPP4[13] * DualPP4[9];
            temp_gcse_12 = DualPP4[15] * DualPP4[7];
            temp_gcse_21 = DualPP4[14] * DualPP4[8];
            temp_gcse_43 = temp_gcse_1 - temp_gcse_21;
            temp_gcse_66 = temp_gcse_43 + temp_gcse_12;
            x4a[19] = -temp_gcse_66 / temp_gcse_64; // e1 ^ (e3 ^ einf)
            temp_gcse_13 = DualPP4[10] * DualPP4[15] - DualPP4[11] * DualPP4[14];
            temp_gcse_18 = DualPP4[11] * DualPP4[14];
            x4a[22] = -(temp_gcse_13 + DualPP4[12] * DualPP4[13]) / (DualPP4[9] * DualPP4[9] + temp_gcse_32 + DualPP4[12] * DualPP4[12]); // e2 ^ (e3 ^ einf)
            x4b[1] = -(temp_gcse_56 - temp_gcse_11 + temp_gcse_27 + temp_gcse_39) / temp_gcse_64; // e1
            x4b[2] = -(temp_gcse_6 - temp_gcse_77 - temp_gcse_8 + temp_gcse_35) / temp_gcse_64; // e2
            x4b[3] = -(temp_gcse_78 - temp_gcse_54 - temp_gcse_31 - temp_gcse_51) / temp_gcse_64; // e3
            x4b[4] = -(temp_gcse_9 - temp_gcse_81 - temp_gcse_65 - temp_gcse_17 - temp_gcse_5) / temp_gcse_64; // einf
            x4b[5] = 1.0; // e0
            x4b[16] = x4a[16]; // e1 ^ (e2 ^ e3)
            x4b[17] = x4a[17]; // e1 ^ (e2 ^ einf)
            x4b[19] = x4a[19]; // e1 ^ (e3 ^ einf)
            x4b[22] = x4a[22]; // e2 ^ (e3 ^ einf)
        }


        public static void RotatingRectangleGaFuL(double angle, double[] P5, double[] P6, double[] P7, double[] P8)
        {
            //Begin GA-FuL MetaContext Code Generation, 2024-06-09T02:56:34.1411259+03:00
            //MetaContext: PGA
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 17 sub-expressions, 0 generated temps, 17 total.
            //Target Temp Variables: 17 total.
            //Output Variables: 9 total.
            //Computations: 0.9230769230769231 average, 24 total.
            //Memory Reads: 1.8076923076923077 average, 47 total.
            //Memory Writes: 26 total.
            //
            //MetaContext Binding Data:
            //   -1 = constant: '-1'
            //   Rational[1, 2] = constant: 'Rational[1, 2]'
            //   2 = constant: '2'
            //   angle = parameter: angle

            var temp0 = 0.5 * angle;
            var temp1 = Math.Cos(temp0);
            var temp2 = Math.Sin(temp0);
            var temp3 = -temp2;
            var temp4 = temp1 + temp3;
            var temp5 = temp1 * temp4;
            var temp6 = -temp1;
            var temp7 = temp3 + temp6;
            var temp8 = temp3 * temp7;
            P5[3] = temp5 - temp8;

            var temp10 = temp3 * temp4;
            var temp11 = temp1 * temp7;
            P5[5] = temp10 + temp11;

            P5[6] = temp1 * temp1 + temp3 * temp3;

            var temp14 = temp2 + temp6;
            var temp15 = temp14 * temp3;
            P6[3] = temp11 - temp15;

            var temp17 = temp14 * temp1;
            P6[5] = temp17 + temp8;

            P6[6] = P5[6];

            var temp19 = temp1 + temp2;
            var temp20 = temp19 * temp3;
            P7[3] = temp17 - temp20;

            var temp22 = temp19 * temp1;
            P7[5] = temp15 + temp22;

            P7[6] = P5[6];

            P8[3] = temp22 - temp10;

            P8[5] = temp20 + temp5;

            P8[6] = P5[6];

            //Finish GA-FuL MetaContext Code Generation, 2024-06-09T02:56:34.2361862+03:00
        }

        public static void RotatingRectangleGaalop(double angle, double[] p5, double[] p6, double[] p7, double[] p8)
        {
            double temp_gcse_1, temp_gcse_10, temp_gcse_11, temp_gcse_2, temp_gcse_3, temp_gcse_4, temp_gcse_5, temp_gcse_7, temp_gcse_8, temp_gcse_9;
            temp_gcse_2 = Math.Sin(angle / 2.0) * Math.Sin(angle / 2.0);
            temp_gcse_3 = Math.Cos(angle / 2.0);
            temp_gcse_4 = temp_gcse_3 * temp_gcse_3;
            temp_gcse_5 = Math.Sin(angle / 2.0);
            temp_gcse_7 = (-temp_gcse_2) - 2.0 * temp_gcse_3 * temp_gcse_5;
            temp_gcse_8 = angle / 2.0;
            temp_gcse_9 = (-temp_gcse_2);
            temp_gcse_10 = 2.0 * temp_gcse_3;
            temp_gcse_11 = 2.0 * temp_gcse_3 * temp_gcse_5;
            p5[4] = temp_gcse_7 + temp_gcse_4; // e0 ^ e1
            temp_gcse_1 = Math.Sin(angle / 2.0) * Math.Sin(angle / 2.0) - 2.0 * Math.Cos(angle / 2.0) * Math.Sin(angle / 2.0);
            p5[5] = temp_gcse_1 - temp_gcse_4; // e0 ^ e2
            p5[6] = temp_gcse_2 + temp_gcse_3 * temp_gcse_3; // e1 ^ e2
            p6[4] = p5[5]; // e0 ^ e1
            p6[5] = temp_gcse_2 + 2.0 * temp_gcse_3 * Math.Sin(angle / 2.0) - temp_gcse_3 * temp_gcse_3; // e0 ^ e2
            p6[6] = p5[6]; // e1 ^ e2
            p7[4] = p6[5]; // e0 ^ e1
            p7[5] = (-(Math.Sin(angle / 2.0) * Math.Sin(angle / 2.0))) + 2.0 * Math.Cos(angle / 2.0) * Math.Sin(angle / 2.0) + Math.Cos(angle / 2.0) * Math.Cos(angle / 2.0); // e0 ^ e2
            p7[6] = p5[6]; // e1 ^ e2
            p8[4] = p7[5]; // e0 ^ e1
            p8[5] = p5[4]; // e0 ^ e2
            p8[6] = p5[6]; // e1 ^ e2
        }


        public static void ReconstructMotorGaFuL(LinFloat64Vector3D p1, LinFloat64Vector3D p2, LinFloat64Vector3D p3, double t, double[] AI, double[] At, double[] BI, double[] Bt, double[] CI, double[] Ct)
        {
            var ax = p1.X.ScalarValue;
            var ay = p1.Y.ScalarValue;
            var az = p1.Z.ScalarValue;

            var bx = p2.X.ScalarValue;
            var by = p2.Y.ScalarValue;
            var bz = p2.Z.ScalarValue;

            var cx = p3.X.ScalarValue;
            var cy = p3.Y.ScalarValue;
            var cz = p3.Z.ScalarValue;

            //Begin GA-FuL MetaContext Code Generation, 2024-06-09T00:41:17.1509230+03:00
            //MetaContext: PGA
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 445 sub-expressions, 0 generated temps, 445 total.
            //Target Temp Variables: 445 total.
            //Output Variables: 19 total.
            //Computations: 1.415948275862069 average, 657 total.
            //Memory Reads: 2.2176724137931036 average, 1029 total.
            //Memory Writes: 464 total.
            //
            //MetaContext Binding Data:
            //   1 = constant: '1'
            //   -1 = constant: '-1'
            //   0.05399999999999999 = constant: '0.05399999999999999'
            //   0.016199999999999996 = constant: '0.016199999999999996'
            //   -0.016199999999999996 = constant: '-0.016199999999999996'
            //   -0.021599999999999994 = constant: '-0.021599999999999994'
            //   0.00033749999999999996 = constant: '0.00033749999999999996'
            //   -0.0008999999999999998 = constant: '-0.0008999999999999998'
            //   -0.08732024999999999 = constant: '-0.08732024999999999'
            //   0.08732024999999999 = constant: '0.08732024999999999'
            //   0.18246946249999998 = constant: '0.18246946249999998'
            //   0.9126697389062499 = constant: '0.9126697389062499'
            //   0.9999899889062499 = constant: '0.9999899889062499'
            //   -0.9999899889062499 = constant: '-0.9999899889062499'
            //   1.99998998890625 = constant: '1.99998998890625'
            //   3.999959955725222 = constant: '3.999959955725222'
            //   0.5000025027859654 = constant: '0.5000025027859654'
            //   2 = constant: '2'
            //   -0.9126697389062499 = constant: '-0.9126697389062499'
            //   0.5646044625 = constant: '0.5646044625'
            //   -0.8253494889062499 = constant: '-0.8253494889062499'
            //   -0.5646044625 = constant: '-0.5646044625'
            //   0.21739756249999997 = constant: '0.21739756249999997'
            //   0.39986702499999993 = constant: '0.39986702499999993'
            //   -0.5000025027859654 = constant: '-0.5000025027859654'
            //   0.4999974972140347 = constant: '0.4999974972140347'
            //   0.9999949944280694 = constant: '0.9999949944280694'
            //   1.0000050055719307 = constant: '1.0000050055719307'
            //   -0.9999949944280694 = constant: '-0.9999949944280694'
            //   -1.0000050055719307 = constant: '-1.0000050055719307'
            //   -0.9999849833844305 = constant: '-0.9999849833844305'
            //   5.005571930616348e-06 = constant: '5.005571930616348e-06'
            //   0.9999849833844305 = constant: '0.9999849833844305'
            //   -5.005571930616348e-06 = constant: '-5.005571930616348e-06'
            //   -0.4999974972140347 = constant: '-0.4999974972140347'
            //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
            //   ax = parameter: ax
            //   ay = parameter: ay
            //   az = parameter: az
            //   bx = parameter: bx
            //   by = parameter: by
            //   bz = parameter: bz
            //   cx = parameter: cx
            //   cy = parameter: cy
            //   cz = parameter: cz
            //   t = parameter: t

            var temp0 = 0.5646044625 * ax + -0.8253494889062499 * az;
            At[7] = temp0;

            var temp1 = -0.8253494889062499 * ax + -0.5646044625 * az;
            At[13] = temp1;

            var temp2 = 0.39986702499999993 + 0.9999899889062499 * ay;
            At[11] = temp2;

            var temp3 = 0.5646044625 * bx + -0.8253494889062499 * bz;
            Bt[7] = temp3;

            var temp4 = -0.8253494889062499 * bx + -0.5646044625 * bz;
            Bt[13] = temp4;

            var temp5 = 0.9999899889062499 * by;
            var temp6 = 0.39986702499999993 + temp5;
            Bt[11] = temp6;

            var temp7 = 0.5646044625 * cx + -0.8253494889062499 * cz;
            Ct[7] = temp7;

            var temp8 = -0.8253494889062499 * cx + -0.5646044625 * cz;
            Ct[13] = temp8;

            var temp9 = 0.39986702499999993 + 0.9999899889062499 * cy;
            Ct[11] = temp9;

            var temp10 = -0.9999899889062499 * temp2 + 0.9999899889062499 * temp6;
            var temp11 = 0.9999849833844305 * az + -0.9999899889062499 * bz;
            var temp12 = temp11 + -5.005571930616348e-06 * temp0;
            var temp13 = 0.9999849833844305 * ax + -0.9999899889062499 * bx;
            var temp14 = temp13 + -5.005571930616348e-06 * temp1;
            var temp15 = -0.9999849833844305 * ax + 0.9999899889062499 * bx;
            var temp16 = temp15 + 5.005571930616348e-06 * temp1;
            var temp17 = -0.9999849833844305 * ay + temp5;
            var temp18 = temp17 + -5.005571930616348e-06 * temp2;
            var temp19 = 0.9999849833844305 * ay + -0.9999899889062499 * by;
            var temp20 = temp19 + 5.005571930616348e-06 * temp2;
            var temp21 = temp14 * temp16 + temp18 * temp20;
            var temp22 = -0.9999849833844305 * az + 0.9999899889062499 * bz;
            var temp23 = temp22 + 5.005571930616348e-06 * temp0;
            var temp24 = temp21 + temp12 * temp23;
            var temp25 = 1 / temp24;
            var temp26 = temp12 * temp25;
            var temp27 = -0.9999899889062499 * temp0 + 0.9999899889062499 * temp3;
            var temp28 = temp18 * temp25;
            var temp29 = temp27 * temp28;
            var temp30 = temp10 * temp26 - temp29;
            var temp31 = temp14 * temp25;
            var temp32 = temp10 * temp31;
            var temp33 = -0.9999899889062499 * temp1 + 0.9999899889062499 * temp4;
            var temp34 = temp28 * temp33 - temp32;
            var temp35 = temp30 * temp30 + temp34 * temp34;
            var temp36 = temp26 * temp33;
            var temp37 = temp27 * temp31 - temp36;
            var temp38 = temp35 + temp37 * temp37;
            var temp39 = temp10 * temp28;
            var temp40 = temp31 * temp33;
            var temp41 = -(temp39 + temp40);
            var temp42 = temp26 * temp27;
            var temp43 = 1 + temp41 + -temp42;
            var temp44 = temp38 + temp43 * temp43;
            var temp45 = 1 / Math.Sqrt(Math.Abs(temp44));
            var temp46 = temp30 * temp45;
            var temp47 = -temp8;
            var temp48 = 0.9999899889062499 * temp1 + -0.9999899889062499 * temp4;
            var temp49 = temp9 * temp48;
            var temp50 = temp10 * temp47 - temp49;
            var temp51 = -temp4;
            var temp52 = temp51 * temp2;
            var temp53 = temp50 + 0.9999899889062499 * temp52;
            var temp54 = -temp1;
            var temp55 = temp54 * temp6;
            var temp56 = temp53 + -0.9999899889062499 * temp55;
            var temp57 = temp43 * temp45;
            var temp58 = -0.9999949944280694 * ax + bx;
            var temp59 = temp58 + -1.0000050055719307 * temp1;
            var temp60 = -0.9999949944280694 * ay;
            var temp61 = by + temp60;
            var temp62 = 1.0000050055719307 * temp2;
            var temp63 = temp61 + temp62;
            var temp64 = temp54 * temp63;
            var temp65 = temp59 * temp2 - temp64;
            var temp66 = temp25 * temp65;
            var temp67 = -0.9999949944280694 * az + bz;
            var temp68 = temp67 + -1.0000050055719307 * temp0;
            var temp69 = -temp0;
            var temp70 = temp59 * temp69;
            var temp71 = temp54 * temp68 - temp70;
            var temp72 = temp25 * temp71;
            var temp73 = temp10 * temp66 + temp27 * temp72;
            var temp74 = temp52 - temp55;
            var temp75 = temp28 * temp74;
            var temp76 = temp73 - temp75;
            var temp77 = -temp3;
            var temp78 = temp54 * temp77;
            var temp79 = temp51 * temp69;
            var temp80 = temp78 - temp79;
            var temp81 = temp26 * temp80;
            var temp82 = temp76 - temp81;
            var temp83 = temp45 * temp82;
            var temp84 = -temp83;
            var temp85 = temp37 * temp45;
            var temp86 = temp68 * temp2;
            var temp87 = temp63 * temp69 - temp86;
            var temp88 = temp25 * temp87;
            var temp89 = temp10 * temp88;
            var temp90 = temp33 * temp72;
            var temp91 = -(temp89 + temp90);
            var temp92 = temp77 * temp2;
            var temp93 = temp69 * temp6;
            var temp94 = temp93 - temp92;
            var temp95 = temp91 + temp28 * temp94;
            var temp96 = temp31 * temp80 + temp95;
            var temp97 = temp45 * temp96;
            var temp98 = -temp97;
            var temp99 = temp57 * temp84 + temp85 * temp98;
            var temp100 = temp10 * temp72;
            var temp101 = temp33 * temp88 - temp100;
            var temp102 = temp27 * temp66 + temp101;
            var temp103 = temp26 * temp74 + temp102;
            var temp104 = temp31 * temp94 + temp103;
            var temp105 = temp28 * temp80;
            var temp106 = temp104 - temp105;
            var temp107 = temp45 * temp106;
            var temp108 = -temp46;
            var temp109 = temp99 + temp107 * temp108;
            var temp110 = temp34 * temp45;
            var temp111 = -temp110;
            var temp112 = temp27 * temp88;
            var temp113 = temp33 * temp66 - temp112;
            var temp114 = temp31 * temp74;
            var temp115 = temp113 - temp114;
            var temp116 = temp26 * temp94 + temp115;
            var temp117 = temp45 * temp116;
            var temp118 = -temp117;
            var temp119 = temp111 * temp118;
            var temp120 = temp109 - temp119;
            var temp121 = -cz;
            var temp122 = 0.9999949944280694 * az + temp121;
            var temp123 = temp122 + 1.0000050055719307 * temp0;
            var temp124 = temp110 * temp123;
            var temp125 = -cx;
            var temp126 = 0.9999949944280694 * ax + temp125;
            var temp127 = temp126 + 1.0000050055719307 * temp1;
            var temp128 = temp46 * temp127;
            var temp129 = -(temp124 + temp128);
            var temp130 = temp129 - temp107;
            var temp131 = cy + temp60;
            var temp132 = temp62 + temp131;
            var temp133 = temp85 * temp132;
            var temp134 = temp130 - temp133;
            var temp135 = temp108 * temp134;
            var temp136 = temp120 - temp135;
            var temp137 = temp118 + temp110 * temp127;
            var temp138 = temp46 * temp123;
            var temp139 = temp137 - temp138;
            var temp140 = temp57 * temp132 + temp139;
            var temp141 = temp111 * temp140;
            var temp142 = temp136 - temp141;
            var temp143 = temp110 * temp132;
            var temp144 = temp83 - temp143;
            var temp145 = temp85 * temp123 + temp144;
            var temp146 = temp57 * temp127 + temp145;
            var temp147 = temp57 * temp146;
            var temp148 = temp142 - temp147;
            var temp149 = -temp85;
            var temp150 = temp97 + temp46 * temp132;
            var temp151 = temp85 * temp127;
            var temp152 = temp150 - temp151;
            var temp153 = temp57 * temp123 + temp152;
            var temp154 = temp148 + temp149 * temp153;
            var temp155 = temp85 * temp107 + temp57 * temp118;
            var temp156 = temp98 * temp108;
            var temp157 = temp155 - temp156;
            var temp158 = temp84 * temp111 + temp157;
            var temp159 = temp134 * temp149 + temp158;
            var temp160 = temp57 * temp140 + temp159;
            var temp161 = temp111 * temp146;
            var temp162 = temp160 - temp161;
            var temp163 = temp108 * temp153 + temp162;
            var temp164 = temp48 * temp163;
            var temp165 = temp10 * temp154 - temp164;
            var temp166 = temp55 - temp52;
            var temp167 = temp108 * temp108 + temp111 * temp111;
            var temp168 = temp85 * temp149;
            var temp169 = temp167 - temp168;
            var temp170 = temp57 * temp57 + temp169;
            var temp171 = temp166 * temp170;
            var temp172 = temp165 - temp171;
            var temp173 = temp84 * temp85;
            var temp174 = temp57 * temp98 - temp173;
            var temp175 = temp108 * temp118 + temp174;
            var temp176 = temp107 * temp111 + temp175;
            var temp177 = temp111 * temp134;
            var temp178 = temp176 - temp177;
            var temp179 = temp108 * temp140 + temp178;
            var temp180 = temp146 * temp149;
            var temp181 = temp179 - temp180;
            var temp182 = temp57 * temp153;
            var temp183 = temp181 - temp182;
            var temp184 = temp10 * temp183;
            var temp185 = 0.9999899889062499 * temp0 + -0.9999899889062499 * temp3;
            var temp186 = temp163 * temp185 - temp184;
            var temp187 = temp92 - temp93;
            var temp188 = temp170 * temp187;
            var temp189 = temp186 - temp188;
            var temp190 = temp172 * temp172 + temp189 * temp189;
            var temp191 = temp154 * temp185;
            var temp192 = temp48 * temp183 - temp191;
            var temp193 = temp80 * temp170 + temp192;
            var temp194 = temp190 + temp193 * temp193;
            var temp195 = 1 / temp194;
            var temp196 = temp172 * temp195;
            var temp197 = temp56 * temp196;
            var temp198 = -0.9999899889062499 * temp92 + 0.9999899889062499 * temp93;
            var temp199 = -temp7;
            var temp200 = temp10 * temp199;
            var temp201 = temp198 - temp200;
            var temp202 = temp9 * temp185 + temp201;
            var temp203 = temp189 * temp195;
            var temp204 = 1 + temp197 + temp202 * temp203;
            var temp205 = 0.9999899889062499 * temp78 + -0.9999899889062499 * temp79;
            var temp206 = temp48 * temp199 + temp205;
            var temp207 = temp47 * temp185;
            var temp208 = temp206 - temp207;
            var temp209 = temp193 * temp195;
            var temp210 = temp204 + temp208 * temp209;
            var temp211 = temp56 * temp203;
            var temp212 = temp196 * temp202 - temp211;
            var temp213 = temp210 * temp210 + temp212 * temp212;
            var temp214 = temp56 * temp209;
            var temp215 = temp196 * temp208 - temp214;
            var temp216 = temp213 + temp215 * temp215;
            var temp217 = temp203 * temp208;
            var temp218 = temp202 * temp209 - temp217;
            var temp219 = temp216 + temp218 * temp218;
            var temp220 = 1 / Math.Sqrt(Math.Abs(temp219));
            var temp221 = temp210 * temp220;
            var temp222 = temp212 * temp220;
            var temp223 = temp46 * temp221 + temp110 * temp222;
            var temp224 = temp215 * temp220;
            var temp225 = temp223 + temp57 * temp224;
            var temp226 = temp218 * temp220;
            var temp227 = temp85 * temp226;
            var temp228 = temp225 - temp227;
            var temp229 = t * temp228;
            var temp230 = -temp229;
            var temp231 = 0.4999974972140347 * ay + -0.5000025027859654 * temp2;
            var temp232 = temp228 * temp231;
            var temp233 = temp97 * temp221 - temp232;
            var temp234 = temp83 * temp222;
            var temp235 = temp233 - temp234;
            var temp236 = temp117 * temp224;
            var temp237 = temp235 - temp236;
            var temp238 = temp166 * temp183 + temp154 * temp187;
            var temp239 = temp80 * temp163;
            var temp240 = temp238 - temp239;
            var temp241 = temp195 * temp240;
            var temp242 = temp56 * temp241;
            var temp243 = temp47 * temp187 + temp166 * temp199;
            var temp244 = temp9 * temp80;
            var temp245 = temp243 - temp244;
            var temp246 = temp196 * temp245 - temp242;
            var temp247 = temp220 * temp246;
            var temp248 = temp237 + temp57 * temp247;
            var temp249 = temp107 * temp226;
            var temp250 = temp248 - temp249;
            var temp251 = temp202 * temp241;
            var temp252 = temp203 * temp245 - temp251;
            var temp253 = temp220 * temp252;
            var temp254 = temp250 + temp85 * temp253;
            var temp255 = temp208 * temp241;
            var temp256 = temp209 * temp245 - temp255;
            var temp257 = temp220 * temp256;
            var temp258 = temp254 + temp46 * temp257;
            var temp259 = 0.9999899889062499 * ax + temp1;
            var temp260 = 0.5000025027859654 * temp259;
            var temp261 = temp85 * temp221 + temp57 * temp222;
            var temp262 = temp110 * temp224;
            var temp263 = temp261 - temp262;
            var temp264 = temp46 * temp226 + temp263;
            var temp265 = temp260 * temp264;
            var temp266 = temp258 - temp265;
            var temp267 = 0.9999899889062499 * az + temp0;
            var temp268 = 0.5000025027859654 * temp267;
            var temp269 = temp85 * temp222;
            var temp270 = temp57 * temp221 - temp269;
            var temp271 = temp46 * temp224;
            var temp272 = temp270 - temp271;
            var temp273 = temp110 * temp226;
            var temp274 = temp272 - temp273;
            var temp275 = temp266 + temp268 * temp274;
            var temp276 = t * temp275;
            var temp277 = -temp276;
            var temp278 = temp230 * temp277;
            var temp279 = temp46 * temp222;
            var temp280 = temp110 * temp221 - temp279;
            var temp281 = temp85 * temp224 + temp280;
            var temp282 = temp57 * temp226 + temp281;
            var temp283 = t * temp282;
            var temp284 = -temp283;
            var temp285 = temp83 * temp221 + temp231 * temp282;
            var temp286 = temp97 * temp222 + temp285;
            var temp287 = temp107 * temp224;
            var temp288 = temp286 - temp287;
            var temp289 = temp85 * temp247;
            var temp290 = temp288 - temp289;
            var temp291 = temp117 * temp226 + temp290;
            var temp292 = temp57 * temp253 + temp291;
            var temp293 = temp110 * temp257;
            var temp294 = temp292 - temp293;
            var temp295 = temp264 * temp268 + temp294;
            var temp296 = temp260 * temp274 + temp295;
            var temp297 = t * temp296;
            var temp298 = -temp297;
            var temp299 = temp284 * temp298 - temp278;
            var temp300 = -az;
            var temp301 = temp283 * temp300;
            var temp302 = -ax;
            var temp303 = temp229 * temp302;
            var temp304 = -(temp301 + temp303);
            var temp305 = t * temp264;
            var temp306 = ay * temp305;
            var temp307 = temp304 - temp306;
            var temp308 = temp228 * temp260 + temp268 * temp282;
            var temp309 = temp231 * temp264;
            var temp310 = temp308 - temp309;
            var temp311 = temp107 * temp221 + temp310;
            var temp312 = temp117 * temp222;
            var temp313 = temp311 - temp312;
            var temp314 = temp83 * temp224 + temp313;
            var temp315 = temp110 * temp247 + temp314;
            var temp316 = temp97 * temp226 + temp315;
            var temp317 = temp46 * temp253 + temp316;
            var temp318 = temp85 * temp257;
            var temp319 = temp317 - temp318;
            var temp320 = t * temp319;
            var temp321 = -temp320;
            var temp322 = temp307 + temp321;
            var temp323 = -temp305;
            var temp324 = temp299 + temp322 * temp323;
            var temp325 = temp305 * temp320;
            var temp326 = temp324 + temp325;
            var temp327 = 1 + -t + t * temp274;
            var temp328 = temp260 * temp282;
            var temp329 = temp228 * temp268 - temp328;
            var temp330 = temp117 * temp221 + temp329;
            var temp331 = temp107 * temp222 + temp330;
            var temp332 = temp97 * temp224 + temp331;
            var temp333 = temp46 * temp247;
            var temp334 = temp332 - temp333;
            var temp335 = temp83 * temp226;
            var temp336 = temp334 - temp335;
            var temp337 = temp110 * temp253 + temp336;
            var temp338 = temp57 * temp257 + temp337;
            var temp339 = temp231 * temp274 + temp338;
            var temp340 = t * temp339;
            var temp341 = -temp340;
            var temp342 = temp283 * temp302 + temp341;
            var temp343 = temp229 * temp300;
            var temp344 = temp342 - temp343;
            var temp345 = ay * temp327 + temp344;
            var temp346 = temp326 + temp327 * temp345;
            var temp347 = ay * temp283;
            var temp348 = temp297 - temp347;
            var temp349 = temp300 * temp305 + temp348;
            var temp350 = temp302 * temp327 + temp349;
            var temp351 = temp284 * temp350;
            var temp352 = temp346 - temp351;
            var temp353 = ay * temp229 + temp276;
            var temp354 = temp302 * temp305;
            var temp355 = temp353 - temp354;
            var temp356 = temp300 * temp327 + temp355;
            var temp357 = temp352 + temp230 * temp356;
            var temp358 = temp327 * temp341;
            AI[11] = temp357 + temp358;

            var temp360 = temp230 * temp320;
            var temp361 = temp284 * temp341 - temp360;
            var temp362 = temp230 * temp322 + temp361;
            var temp363 = temp277 * temp305;
            var temp364 = -temp363;
            var temp365 = temp362 + temp364;
            var temp366 = temp284 * temp345 + temp365;
            var temp367 = temp327 * temp350 + temp366;
            var temp368 = temp323 * temp356;
            var temp369 = temp367 - temp368;
            var temp370 = temp298 * temp327;
            var temp371 = -temp370;
            AI[13] = temp369 + temp371;

            var temp373 = temp284 * temp320;
            var temp374 = temp298 * temp305 - temp373;
            var temp375 = temp284 * temp322 + temp374;
            var temp376 = temp230 * temp341;
            var temp377 = -temp376;
            var temp378 = temp375 + temp377;
            var temp379 = temp230 * temp345;
            var temp380 = temp378 - temp379;
            var temp381 = temp323 * temp350 + temp380;
            var temp382 = temp327 * temp356 + temp381;
            var temp383 = temp277 * temp327;
            var temp384 = -temp383;
            AI[7] = temp382 + temp384;

            var temp386 = temp305 * temp323;
            var temp387 = temp284 * temp284 - temp386;
            var temp388 = temp230 * temp230 + temp387;
            AI[14] = temp327 * temp327 + temp388;

            var temp390 = temp299 + temp325;
            var temp391 = temp358 + temp390;
            var temp392 = -bz;
            var temp393 = temp283 * temp392;
            var temp394 = -bx;
            var temp395 = temp229 * temp394;
            var temp396 = -(temp393 + temp395);
            var temp397 = temp321 + temp396;
            var temp398 = by * temp305;
            var temp399 = temp397 - temp398;
            var temp400 = temp391 + temp323 * temp399;
            var temp401 = temp341 + temp283 * temp394;
            var temp402 = temp229 * temp392;
            var temp403 = temp401 - temp402;
            var temp404 = by * temp327 + temp403;
            var temp405 = temp400 + temp327 * temp404;
            var temp406 = by * temp283;
            var temp407 = temp297 - temp406;
            var temp408 = temp305 * temp392 + temp407;
            var temp409 = temp327 * temp394 + temp408;
            var temp410 = temp284 * temp409;
            var temp411 = temp405 - temp410;
            var temp412 = by * temp229 + temp276;
            var temp413 = temp305 * temp394;
            var temp414 = temp412 - temp413;
            var temp415 = temp327 * temp392 + temp414;
            BI[11] = temp411 + temp230 * temp415;

            var temp417 = temp361 + temp364;
            var temp418 = temp371 + temp417;
            var temp419 = temp230 * temp399 + temp418;
            var temp420 = temp284 * temp404 + temp419;
            var temp421 = temp327 * temp409 + temp420;
            var temp422 = temp323 * temp415;
            BI[13] = temp421 - temp422;

            var temp424 = temp374 + temp377;
            var temp425 = temp384 + temp424;
            var temp426 = temp284 * temp399 + temp425;
            var temp427 = temp230 * temp404;
            var temp428 = temp426 - temp427;
            var temp429 = temp323 * temp409 + temp428;
            BI[7] = temp327 * temp415 + temp429;

            var temp431 = temp121 * temp283;
            var temp432 = temp125 * temp229;
            var temp433 = -(temp431 + temp432);
            var temp434 = temp321 + temp433;
            var temp435 = cy * temp305;
            var temp436 = temp434 - temp435;
            var temp437 = temp391 + temp323 * temp436;
            var temp438 = temp125 * temp283 + temp341;
            var temp439 = temp121 * temp229;
            var temp440 = temp438 - temp439;
            var temp441 = cy * temp327 + temp440;
            var temp442 = temp437 + temp327 * temp441;
            var temp443 = cy * temp283;
            var temp444 = temp297 - temp443;
            var temp445 = temp121 * temp305 + temp444;
            var temp446 = temp125 * temp327 + temp445;
            var temp447 = temp284 * temp446;
            var temp448 = temp442 - temp447;
            var temp449 = cy * temp229 + temp276;
            var temp450 = temp125 * temp305;
            var temp451 = temp449 - temp450;
            var temp452 = temp121 * temp327 + temp451;
            CI[11] = temp448 + temp230 * temp452;

            var temp454 = temp418 + temp230 * temp436;
            var temp455 = temp284 * temp441 + temp454;
            var temp456 = temp327 * temp446 + temp455;
            var temp457 = temp323 * temp452;
            CI[13] = temp456 - temp457;

            var temp459 = temp425 + temp284 * temp436;
            var temp460 = temp230 * temp441;
            var temp461 = temp459 - temp460;
            var temp462 = temp323 * temp446 + temp461;
            CI[7] = temp327 * temp452 + temp462;

            //Finish GA-FuL MetaContext Code Generation, 2024-06-09T00:41:28.5577464+03:00


        }

        public static void ReconstructMotorGaalop(LinFloat64Vector3D p1, LinFloat64Vector3D p2, LinFloat64Vector3D p3, double t, double[] AI, double[] At, double[] BI, double[] Bt, double[] CI, double[] Ct)
        {
            var ax = p1.X.ScalarValue;
            var ay = p1.Y.ScalarValue;
            var az = p1.Z.ScalarValue;

            var bx = p2.X.ScalarValue;
            var by = p2.Y.ScalarValue;
            var bz = p2.Z.ScalarValue;

            var cx = p3.X.ScalarValue;
            var cy = p3.Y.ScalarValue;
            var cz = p3.Z.ScalarValue;

            var B2 = new double[16];
            var C2 = new double[16];
            var C3 = new double[16];
            var L1 = new double[16];
            var L2 = new double[16];
            var macro_Motor_abstemp1 = new double[16];
            var macro_Motor_abstemp2 = new double[16];
            var macro_Motor_temp = new double[16];
            var macro_Motor_temp1 = new double[16];
            var macro_Motor_temp2 = new double[16];
            var P1 = new double[16];
            var P2 = new double[16];
            var V = new double[16];
            var VA = new double[16];
            var VB = new double[16];
            var VC = new double[16];

            double temp_gcse_1, temp_gcse_10, temp_gcse_100, temp_gcse_101, temp_gcse_102, temp_gcse_103, temp_gcse_104, temp_gcse_105, temp_gcse_106, temp_gcse_107, temp_gcse_108, temp_gcse_109, temp_gcse_11, temp_gcse_110, temp_gcse_111, temp_gcse_112, temp_gcse_113, temp_gcse_114, temp_gcse_115, temp_gcse_116, temp_gcse_117, temp_gcse_118, temp_gcse_119, temp_gcse_12, temp_gcse_120, temp_gcse_121, temp_gcse_122, temp_gcse_123, temp_gcse_124, temp_gcse_125, temp_gcse_126, temp_gcse_127, temp_gcse_128, temp_gcse_129, temp_gcse_13, temp_gcse_130, temp_gcse_131, temp_gcse_132, temp_gcse_133, temp_gcse_134, temp_gcse_135, temp_gcse_136, temp_gcse_137, temp_gcse_138, temp_gcse_139, temp_gcse_14, temp_gcse_140, temp_gcse_141, temp_gcse_142, temp_gcse_143, temp_gcse_144, temp_gcse_145, temp_gcse_146, temp_gcse_147, temp_gcse_148, temp_gcse_149, temp_gcse_15, temp_gcse_150, temp_gcse_151, temp_gcse_152, temp_gcse_153, temp_gcse_154, temp_gcse_155, temp_gcse_156, temp_gcse_157, temp_gcse_158, temp_gcse_159, temp_gcse_16, temp_gcse_160, temp_gcse_161, temp_gcse_162, temp_gcse_163, temp_gcse_164, temp_gcse_165, temp_gcse_167, temp_gcse_168, temp_gcse_169, temp_gcse_17, temp_gcse_170, temp_gcse_172, temp_gcse_173, temp_gcse_174, temp_gcse_175, temp_gcse_176, temp_gcse_177, temp_gcse_178, temp_gcse_179, temp_gcse_18, temp_gcse_180, temp_gcse_19, temp_gcse_2, temp_gcse_20, temp_gcse_21, temp_gcse_22, temp_gcse_23, temp_gcse_24, temp_gcse_25, temp_gcse_26, temp_gcse_27, temp_gcse_28, temp_gcse_29, temp_gcse_3, temp_gcse_30, temp_gcse_31, temp_gcse_32, temp_gcse_33, temp_gcse_34, temp_gcse_35, temp_gcse_36, temp_gcse_37, temp_gcse_38, temp_gcse_39, temp_gcse_4, temp_gcse_40, temp_gcse_41, temp_gcse_42, temp_gcse_43, temp_gcse_44, temp_gcse_45, temp_gcse_46, temp_gcse_47, temp_gcse_48, temp_gcse_49, temp_gcse_5, temp_gcse_50, temp_gcse_51, temp_gcse_52, temp_gcse_53, temp_gcse_54, temp_gcse_55, temp_gcse_56, temp_gcse_57, temp_gcse_58, temp_gcse_59, temp_gcse_6, temp_gcse_60, temp_gcse_61, temp_gcse_62, temp_gcse_63, temp_gcse_64, temp_gcse_65, temp_gcse_66, temp_gcse_67, temp_gcse_68, temp_gcse_69, temp_gcse_7, temp_gcse_70, temp_gcse_71, temp_gcse_72, temp_gcse_73, temp_gcse_74, temp_gcse_75, temp_gcse_76, temp_gcse_77, temp_gcse_78, temp_gcse_79, temp_gcse_8, temp_gcse_80, temp_gcse_81, temp_gcse_82, temp_gcse_83, temp_gcse_84, temp_gcse_85, temp_gcse_86, temp_gcse_87, temp_gcse_88, temp_gcse_89, temp_gcse_9, temp_gcse_90, temp_gcse_91, temp_gcse_92, temp_gcse_93, temp_gcse_94, temp_gcse_95, temp_gcse_96, temp_gcse_97, temp_gcse_98, temp_gcse_99;
            At[11] = 0.5646044625 * ax - 0.8253494889062499 * az; // e0 ^ (e1 ^ e2)
            temp_gcse_77 = 0.9999899889062499 * ay;
            At[12] = temp_gcse_77 + 0.399867025; // e0 ^ (e1 ^ e3)
            At[13] = -(0.5646044625 * az) - 0.8253494889062499 * ax; // e0 ^ (e2 ^ e3)
            At[14] = 0.9999899889062499; // e1 ^ (e2 ^ e3)
            Bt[11] = 0.5646044625 * bx - 0.8253494889062499 * bz; // e0 ^ (e1 ^ e2)
            Bt[12] = 0.9999899889062499 * by + 0.399867025; // e0 ^ (e1 ^ e3)
            Bt[13] = -(0.5646044625 * bz) - 0.8253494889062499 * bx; // e0 ^ (e2 ^ e3)
            Bt[14] = 0.9999899889062499; // e1 ^ (e2 ^ e3)
            Ct[11] = 0.5646044625 * cx - 0.8253494889062499 * cz; // e0 ^ (e1 ^ e2)
            Ct[12] = 0.9999899889062499 * cy + 0.399867025; // e0 ^ (e1 ^ e3)
            Ct[13] = -(0.5646044625 * cz) - 0.8253494889062499 * cx; // e0 ^ (e2 ^ e3)
            Ct[14] = 0.9999899889062499; // e1 ^ (e2 ^ e3)
            macro_Motor_temp[5] = 0.9999899889062499 * ax + At[13]; // e0 ^ e1
            macro_Motor_temp[6] = temp_gcse_77 - At[12]; // e0 ^ e2
            macro_Motor_temp[7] = 0.9999899889062499 * az + At[11]; // e0 ^ e3
            VA[5] = 0.5000025027859653 * macro_Motor_temp[5]; // e0 ^ e1
            VA[6] = 0.5000025027859653 * macro_Motor_temp[6]; // e0 ^ e2
            VA[7] = 0.5000025027859653 * macro_Motor_temp[7]; // e0 ^ e3
            temp_gcse_118 = 2.0 * VA[7];
            B2[11] = temp_gcse_118 - bz; // e0 ^ (e1 ^ e2)
            temp_gcse_114 = 2.0 * VA[6];
            B2[12] = by - temp_gcse_114; // e0 ^ (e1 ^ e3)
            temp_gcse_110 = 2.0 * VA[5];
            B2[13] = temp_gcse_110 - bx; // e0 ^ (e2 ^ e3)
            C2[11] = temp_gcse_118 - cz; // e0 ^ (e1 ^ e2)
            C2[12] = cy - temp_gcse_114; // e0 ^ (e1 ^ e3)
            C2[13] = temp_gcse_110 - cx; // e0 ^ (e2 ^ e3)
            L1[5] = At[11] * Bt[12] - At[12] * Bt[11]; // e0 ^ e1
            L1[6] = At[11] * Bt[13] - At[13] * Bt[11]; // e0 ^ e2
            L1[7] = At[12] * Bt[13] - At[13] * Bt[12]; // e0 ^ e3
            L1[8] = 0.9999899889062499 * At[11] - 0.9999899889062499 * Bt[11]; // e1 ^ e2
            L1[9] = 0.9999899889062499 * At[12] - 0.9999899889062499 * Bt[12]; // e1 ^ e3
            L1[10] = 0.9999899889062499 * At[13] - 0.9999899889062499 * Bt[13]; // e2 ^ e3
            L2[5] = At[11] * B2[12] - At[12] * B2[11]; // e0 ^ e1
            L2[6] = At[11] * B2[13] - At[13] * B2[11]; // e0 ^ e2
            L2[7] = At[12] * B2[13] - At[13] * B2[12]; // e0 ^ e3
            L2[8] = At[11] - 0.9999899889062499 * B2[11]; // e1 ^ e2
            L2[9] = At[12] - 0.9999899889062499 * B2[12]; // e1 ^ e3
            L2[10] = At[13] - 0.9999899889062499 * B2[13]; // e2 ^ e3
            temp_gcse_21 = L2[8] * L2[8] * L2[8];
            temp_gcse_24 = 6.0 * Math.Pow(L2[8], 4.0);
            temp_gcse_25 = 4.0 * Math.Pow(L2[8], 6.0);
            temp_gcse_35 = 4.0 * L2[10] * L2[10];
            temp_gcse_37 = 3.0 * L1[9] * L2[10] * L2[10];
            temp_gcse_40 = 6.0 * Math.Pow(L2[10], 4.0);
            temp_gcse_41 = 4.0 * Math.Pow(L2[10], 6.0);
            temp_gcse_45 = Math.Pow(L2[10], 6.0);
            temp_gcse_46 = Math.Pow(L2[10], 4.0);
            temp_gcse_48 = L2[8] * L2[8];
            temp_gcse_56 = Math.Pow(L2[8], 5.0);
            temp_gcse_61 = 3.0 * L1[8] * L2[10] * L2[10];
            temp_gcse_63 = 3.0 * L1[9] * temp_gcse_46;
            temp_gcse_71 = L2[10] * L2[10] * L2[10];
            temp_gcse_78 = Math.Pow(L2[8], 4.0);
            temp_gcse_81 = 4.0 * temp_gcse_48;
            temp_gcse_101 = Math.Pow(L2[8], 8.0);
            temp_gcse_111 = Math.Pow(L2[9], 4.0);
            temp_gcse_115 = 3.0 * L1[10] * Math.Pow(L2[10], 5.0);
            temp_gcse_116 = 3.0 * L1[10] * L2[10];
            temp_gcse_121 = L2[10] * L2[10];
            temp_gcse_122 = Math.Pow(L2[9], 8.0);
            temp_gcse_124 = 3.0 * L1[8] * temp_gcse_46;
            temp_gcse_131 = 12.0 * temp_gcse_46;
            temp_gcse_133 = Math.Pow(L2[10], 5.0);
            temp_gcse_135 = 3.0 * L1[10];
            temp_gcse_136 = 12.0 * temp_gcse_121 + temp_gcse_116;
            temp_gcse_138 = Math.Pow(L2[10], 8.0);
            temp_gcse_141 = L1[10] * L2[10];
            temp_gcse_144 = 12.0 * temp_gcse_121;
            temp_gcse_154 = temp_gcse_135 * temp_gcse_71;
            temp_gcse_162 = L2[9] * L2[9];
            temp_gcse_174 = Math.Pow(L2[8], 6.0);
            temp_gcse_175 = 3.0 * L1[8];
            temp_gcse_176 = Math.Pow(L2[9], 6.0);
            temp_gcse_179 = 3.0 * L1[9];
            macro_Motor_temp1[0] = (temp_gcse_122 + L1[9] * Math.Pow(L2[9], 7.0) + (temp_gcse_81 + L1[8] * L2[8] + temp_gcse_35 + temp_gcse_141) * temp_gcse_176 + (temp_gcse_179 * temp_gcse_48 + temp_gcse_37) * Math.Pow(L2[9], 5.0) + (temp_gcse_24 + temp_gcse_175 * temp_gcse_21 + temp_gcse_136 * temp_gcse_48 + temp_gcse_61 * L2[8] + temp_gcse_40 + temp_gcse_154) * temp_gcse_111 + (temp_gcse_179 * temp_gcse_78 + 6.0 * L1[9] * temp_gcse_121 * temp_gcse_48 + temp_gcse_63) * temp_gcse_162 * L2[9] + (temp_gcse_25 + temp_gcse_175 * temp_gcse_56 + temp_gcse_136 * temp_gcse_78 + 6.0 * L1[8] * temp_gcse_121 * temp_gcse_21 + (temp_gcse_131 + 6.0 * L1[10] * temp_gcse_71) * temp_gcse_48 + temp_gcse_124 * L2[8] + temp_gcse_41 + temp_gcse_115) * temp_gcse_162 + (L1[9] * temp_gcse_174 + temp_gcse_37 * temp_gcse_78 + temp_gcse_63 * temp_gcse_48 + L1[9] * temp_gcse_45) * L2[9] + temp_gcse_101 + L1[8] * Math.Pow(L2[8], 7.0) + (temp_gcse_35 + temp_gcse_141) * temp_gcse_174 + temp_gcse_61 * temp_gcse_56 + (temp_gcse_40 + temp_gcse_154) * temp_gcse_78 + temp_gcse_124 * temp_gcse_21 + (temp_gcse_41 + temp_gcse_115) * temp_gcse_48 + L1[8] * temp_gcse_45 * L2[8] + temp_gcse_138 + L1[10] * Math.Pow(L2[10], 7.0)) / (temp_gcse_122 + (temp_gcse_81 + temp_gcse_35) * temp_gcse_176 + (temp_gcse_24 + temp_gcse_144 * temp_gcse_48 + temp_gcse_40) * temp_gcse_111 + (temp_gcse_25 + temp_gcse_144 * temp_gcse_78 + temp_gcse_131 * temp_gcse_48 + temp_gcse_41) * temp_gcse_162 + temp_gcse_101 + temp_gcse_35 * temp_gcse_174 + temp_gcse_40 * temp_gcse_78 + temp_gcse_41 * temp_gcse_48 + temp_gcse_138); // 1.0
            temp_gcse_14 = L2[9] * L2[9] + L2[8] * L2[8];
            temp_gcse_139 = temp_gcse_14 + temp_gcse_121;
            macro_Motor_temp1[5] = (L1[7] * L2[9] + L1[6] * L2[8] - L1[9] * L2[7] - L1[8] * L2[6]) / temp_gcse_139; // e0 ^ e1
            macro_Motor_temp1[6] = -(L1[5] * L2[8] + L1[10] * L2[7] + -(L1[8] * L2[5]) - L1[7] * L2[10]) / temp_gcse_139; // e0 ^ e2
            macro_Motor_temp1[7] = -(L1[5] * L2[9] + -(L1[10] * L2[6]) - L1[9] * L2[5] + L1[6] * L2[10]) / temp_gcse_139; // e0 ^ e3
            macro_Motor_temp1[8] = -(L1[10] * L2[9] + -(L1[9] * L2[10])) / temp_gcse_139; // e1 ^ e2
            macro_Motor_temp1[9] = -(-(L1[10] * L2[8]) + L1[8] * L2[10]) / temp_gcse_139; // e1 ^ e3
            macro_Motor_temp1[10] = (L1[8] * L2[9] - L1[9] * L2[8]) / temp_gcse_139; // e2 ^ e3
            macro_Motor_temp1[15] = (L1[6] * L2[9] - L1[7] * L2[8] - L1[8] * L2[7] + L1[9] * L2[6] - L1[10] * L2[5] - L1[5] * L2[10]) / temp_gcse_139; // e0 ^ (e1 ^ (e2 ^ e3))
            macro_Motor_abstemp1[0] = Math.Sqrt(macro_Motor_temp1[9] * macro_Motor_temp1[9] + macro_Motor_temp1[8] * macro_Motor_temp1[8] + macro_Motor_temp1[10] * macro_Motor_temp1[10] + macro_Motor_temp1[0] * macro_Motor_temp1[0]); // 1.0
            VB[0] = macro_Motor_temp1[0] / macro_Motor_abstemp1[0]; // 1.0
            VB[5] = macro_Motor_temp1[5] / macro_Motor_abstemp1[0]; // e0 ^ e1
            VB[6] = macro_Motor_temp1[6] / macro_Motor_abstemp1[0]; // e0 ^ e2
            VB[7] = macro_Motor_temp1[7] / macro_Motor_abstemp1[0]; // e0 ^ e3
            VB[8] = macro_Motor_temp1[8] / macro_Motor_abstemp1[0]; // e1 ^ e2
            VB[9] = macro_Motor_temp1[9] / macro_Motor_abstemp1[0]; // e1 ^ e3
            VB[10] = macro_Motor_temp1[10] / macro_Motor_abstemp1[0]; // e2 ^ e3
            VB[15] = macro_Motor_temp1[15] / macro_Motor_abstemp1[0]; // e0 ^ (e1 ^ (e2 ^ e3))
            temp_gcse_27 = 2.0 * VB[0];
            temp_gcse_34 = 2.0 * VB[5];
            temp_gcse_43 = 2.0 * VB[15];
            temp_gcse_62 = 2.0 * C2[12] * VB[0];
            temp_gcse_66 = VB[9] * VB[9];
            temp_gcse_76 = 2.0 * VB[10];
            temp_gcse_99 = VB[10] * VB[10];
            temp_gcse_106 = 2.0 * C2[13] * VB[10];
            temp_gcse_128 = VB[8] * VB[8];
            temp_gcse_130 = 2.0 * C2[13] * VB[0];
            temp_gcse_147 = 2.0 * C2[12];
            temp_gcse_149 = VB[0] * VB[0];
            temp_gcse_159 = 2.0 * C2[13];
            C3[11] = -(C2[11] * temp_gcse_66) + (temp_gcse_147 * VB[8] - temp_gcse_34 - temp_gcse_130) * VB[9] + C2[11] * temp_gcse_128 + (temp_gcse_43 + temp_gcse_106) * VB[8] + temp_gcse_27 * VB[7] - temp_gcse_76 * VB[6] - C2[11] * temp_gcse_99 + temp_gcse_62 * VB[10] + C2[11] * temp_gcse_149; // e0 ^ (e1 ^ e2)
            temp_gcse_4 = 2.0 * C2[11] * VB[0];
            temp_gcse_152 = 2.0 * C2[11];
            C3[12] = C2[12] * temp_gcse_66 + (temp_gcse_152 * VB[8] + temp_gcse_43 + temp_gcse_106) * VB[9] - C2[12] * temp_gcse_128 + (temp_gcse_34 + temp_gcse_130) * VB[8] - temp_gcse_76 * VB[7] - temp_gcse_27 * VB[6] - C2[12] * temp_gcse_99 - temp_gcse_4 * VB[10] + C2[12] * temp_gcse_149; // e0 ^ (e1 ^ e3)
            C3[13] = -(C2[13] * temp_gcse_66) + (2.0 * VB[7] + temp_gcse_147 * VB[10] + temp_gcse_4) * VB[9] - C2[13] * temp_gcse_128 + (2.0 * VB[6] + temp_gcse_152 * VB[10] - temp_gcse_62) * VB[8] + temp_gcse_27 * VB[5] + temp_gcse_76 * VB[15] + C2[13] * temp_gcse_99 + C2[13] * temp_gcse_149; // e0 ^ (e2 ^ e3)
            C3[14] = temp_gcse_66 + temp_gcse_128 + temp_gcse_99 + temp_gcse_149; // e1 ^ (e2 ^ e3)
            P1[1] = -(Ct[11] * L1[7]) + Ct[12] * L1[6] - Ct[13] * L1[5]; // e0
            P1[2] = -(Ct[11] * L1[9]) + Ct[12] * L1[8] - 0.9999899889062499 * L1[5]; // e1
            P1[3] = Ct[13] * L1[8] - 0.9999899889062499 * L1[6] - Ct[11] * L1[10]; // e2
            P1[4] = Ct[13] * L1[9] - 0.9999899889062499 * L1[7] - Ct[12] * L1[10]; // e3
            P2[1] = -(C3[11] * L1[7]) + C3[12] * L1[6] - C3[13] * L1[5]; // e0
            P2[2] = -(C3[11] * L1[9]) + C3[12] * L1[8] - C3[14] * L1[5]; // e1
            P2[3] = C3[13] * L1[8] - C3[14] * L1[6] - C3[11] * L1[10]; // e2
            P2[4] = C3[13] * L1[9] - C3[14] * L1[7] - C3[12] * L1[10]; // e3
            temp_gcse_5 = 3.0 * P2[3] * P2[3];
            temp_gcse_6 = 3.0 * Math.Pow(P2[3], 4.0);
            temp_gcse_22 = 3.0 * Math.Pow(P2[2], 4.0);
            temp_gcse_23 = 2.0 * P1[4];
            temp_gcse_26 = P1[2] * P2[2];
            temp_gcse_29 = Math.Pow(P2[2], 6.0);
            temp_gcse_32 = 2.0 * P1[3];
            temp_gcse_36 = 2.0 * P1[2];
            temp_gcse_38 = Math.Pow(P2[3], 6.0);
            temp_gcse_52 = temp_gcse_23 * P2[2] * P2[2];
            temp_gcse_53 = Math.Pow(P2[4], 6.0);
            temp_gcse_55 = temp_gcse_36 * P2[2] * P2[2] * P2[2];
            temp_gcse_57 = Math.Pow(P2[3], 4.0);
            temp_gcse_58 = P2[4] * P2[4];
            temp_gcse_60 = Math.Pow(P2[4], 4.0);
            temp_gcse_67 = Math.Pow(P2[2], 4.0);
            temp_gcse_82 = P2[2] * P2[2] * P2[2];
            temp_gcse_90 = P2[2] * P2[2];
            temp_gcse_94 = 6.0 * temp_gcse_90;
            temp_gcse_117 = temp_gcse_32 * temp_gcse_90;
            temp_gcse_167 = 3.0 * temp_gcse_90;
            temp_gcse_169 = P2[3] * P2[3] * P2[3];
            temp_gcse_173 = P2[3] * P2[3];
            macro_Motor_temp2[0] = (temp_gcse_53 + P1[4] * Math.Pow(P2[4], 5.0) + (temp_gcse_5 + P1[3] * P2[3] + temp_gcse_167 + temp_gcse_26) * temp_gcse_60 + (temp_gcse_23 * temp_gcse_173 + temp_gcse_52) * temp_gcse_58 * P2[4] + (temp_gcse_6 + temp_gcse_32 * temp_gcse_169 + (temp_gcse_94 + temp_gcse_36 * P2[2]) * temp_gcse_173 + temp_gcse_117 * P2[3] + temp_gcse_22 + temp_gcse_55) * temp_gcse_58 + (P1[4] * temp_gcse_57 + temp_gcse_52 * temp_gcse_173 + P1[4] * temp_gcse_67) * P2[4] + temp_gcse_38 + P1[3] * Math.Pow(P2[3], 5.0) + (temp_gcse_167 + temp_gcse_26) * temp_gcse_57 + temp_gcse_117 * temp_gcse_169 + (temp_gcse_22 + temp_gcse_55) * temp_gcse_173 + P1[3] * temp_gcse_67 * P2[3] + temp_gcse_29 + P1[2] * Math.Pow(P2[2], 5.0)) / (temp_gcse_53 + (temp_gcse_5 + temp_gcse_167) * temp_gcse_60 + (temp_gcse_6 + temp_gcse_94 * temp_gcse_173 + temp_gcse_22) * temp_gcse_58 + temp_gcse_38 + temp_gcse_167 * temp_gcse_57 + temp_gcse_22 * temp_gcse_173 + temp_gcse_29); // 1.0
            temp_gcse_75 = temp_gcse_58 + P2[3] * P2[3] + P2[2] * P2[2];
            temp_gcse_89 = temp_gcse_58 + P2[3] * P2[3];
            macro_Motor_temp2[5] = (P1[1] * P2[2] - P1[2] * P2[1]) / temp_gcse_75; // e0 ^ e1
            macro_Motor_temp2[6] = (P1[1] * P2[3] - P1[3] * P2[1]) / temp_gcse_75; // e0 ^ e2
            macro_Motor_temp2[7] = (P1[1] * P2[4] - P1[4] * P2[1]) / temp_gcse_75; // e0 ^ e3
            macro_Motor_temp2[8] = (P1[2] * P2[3] - P1[3] * P2[2]) / temp_gcse_75; // e1 ^ e2
            macro_Motor_temp2[9] = (P1[2] * P2[4] - P1[4] * P2[2]) / temp_gcse_75; // e1 ^ e3
            macro_Motor_temp2[10] = (P1[3] * P2[4] - P1[4] * P2[3]) / temp_gcse_75; // e2 ^ e3
            macro_Motor_abstemp2[0] = Math.Sqrt(macro_Motor_temp2[9] * macro_Motor_temp2[9] + macro_Motor_temp2[8] * macro_Motor_temp2[8] + macro_Motor_temp2[10] * macro_Motor_temp2[10] + macro_Motor_temp2[0] * macro_Motor_temp2[0]); // 1.0
            VC[0] = macro_Motor_temp2[0] / macro_Motor_abstemp2[0]; // 1.0
            VC[5] = macro_Motor_temp2[5] / macro_Motor_abstemp2[0]; // e0 ^ e1
            VC[6] = macro_Motor_temp2[6] / macro_Motor_abstemp2[0]; // e0 ^ e2
            VC[7] = macro_Motor_temp2[7] / macro_Motor_abstemp2[0]; // e0 ^ e3
            VC[8] = macro_Motor_temp2[8] / macro_Motor_abstemp2[0]; // e1 ^ e2
            VC[9] = macro_Motor_temp2[9] / macro_Motor_abstemp2[0]; // e1 ^ e3
            VC[10] = macro_Motor_temp2[10] / macro_Motor_abstemp2[0]; // e2 ^ e3
            V[0] = -(VB[9] * VC[9]) - VB[8] * VC[8] - VB[10] * VC[10] + VB[0] * VC[0]; // 1.0
            temp_gcse_2 = -(VA[5] * VB[8]) + VB[6];
            temp_gcse_3 = VA[6] * VB[10];
            temp_gcse_12 = VA[7] * VB[9] + VA[6] * VB[8];
            temp_gcse_18 = VA[6] * VB[9];
            temp_gcse_20 = VA[6] * VB[8];
            temp_gcse_42 = VA[5] * VB[8];
            temp_gcse_47 = VA[5] * VB[9];
            temp_gcse_64 = VA[5] * VB[10];
            temp_gcse_68 = VA[5] * VB[0];
            temp_gcse_69 = VA[7] * VB[10];
            temp_gcse_70 = temp_gcse_2 + temp_gcse_69;
            temp_gcse_74 = temp_gcse_12 + VB[5];
            temp_gcse_79 = VA[7] * VB[8];
            temp_gcse_85 = -temp_gcse_47 + VB[7] - temp_gcse_3 + VA[7] * VB[0];
            temp_gcse_93 = VA[7] * VB[0];
            temp_gcse_98 = VB[7] - temp_gcse_3;
            temp_gcse_119 = temp_gcse_18 - temp_gcse_79 - VB[15];
            temp_gcse_120 = VA[7] * VB[9];
            temp_gcse_132 = -temp_gcse_47;
            temp_gcse_137 = -(VA[5] * VB[8]);
            temp_gcse_143 = temp_gcse_74 + temp_gcse_68;
            temp_gcse_156 = temp_gcse_70 + VA[6] * VB[0];
            temp_gcse_165 = -temp_gcse_47 + VB[7] - temp_gcse_3;
            temp_gcse_172 = temp_gcse_119 - temp_gcse_64;
            temp_gcse_177 = VA[6] * VB[0];
            temp_gcse_178 = temp_gcse_18 - temp_gcse_79;
            V[5] = temp_gcse_85 * VC[9] + temp_gcse_156 * VC[8] - VB[9] * VC[7] - VB[8] * VC[6] + VB[0] * VC[5] + temp_gcse_172 * VC[10] + temp_gcse_143 * VC[0]; // e0 ^ e1
            temp_gcse_11 = -(VA[6] * VB[9]) + VA[7] * VB[8];
            temp_gcse_54 = -(VA[7] * VB[9]) - temp_gcse_20;
            temp_gcse_72 = -(VA[6] * VB[9]);
            temp_gcse_83 = temp_gcse_11 + VB[15] + temp_gcse_64;
            temp_gcse_105 = temp_gcse_11 + VB[15];
            temp_gcse_108 = -temp_gcse_68;
            temp_gcse_126 = temp_gcse_54 - VB[5] + temp_gcse_108;
            temp_gcse_161 = temp_gcse_54 - VB[5];
            temp_gcse_180 = -(VA[7] * VB[9]);
            V[6] = temp_gcse_83 * VC[9] + temp_gcse_126 * VC[8] - VB[10] * VC[7] + VB[0] * VC[6] + VB[8] * VC[5] + temp_gcse_85 * VC[10] + temp_gcse_156 * VC[0]; // e0 ^ e2
            temp_gcse_13 = VA[5] * VB[8] - VB[6];
            temp_gcse_84 = temp_gcse_13 - temp_gcse_69 - VA[6] * VB[0];
            temp_gcse_158 = temp_gcse_13 - temp_gcse_69;
            V[7] = temp_gcse_126 * VC[9] + temp_gcse_172 * VC[8] + VB[0] * VC[7] + VB[10] * VC[6] + VB[9] * VC[5] + temp_gcse_84 * VC[10] + temp_gcse_85 * VC[0]; // e0 ^ e3
            V[8] = -(VB[10] * VC[9]) + VB[0] * VC[8] + VB[9] * VC[10] + VB[8] * VC[0]; // e1 ^ e2
            V[9] = VB[0] * VC[9] + VB[10] * VC[8] - VB[8] * VC[10] + VB[9] * VC[0]; // e1 ^ e3
            V[10] = VB[8] * VC[9] - VB[9] * VC[8] + VB[0] * VC[10] + VB[10] * VC[0]; // e2 ^ e3
            V[15] = temp_gcse_84 * VC[9] + temp_gcse_85 * VC[8] + VB[8] * VC[7] - VB[9] * VC[6] + VB[10] * VC[5] + temp_gcse_143 * VC[10] + temp_gcse_83 * VC[0]; // e0 ^ (e1 ^ (e2 ^ e3))
            temp_gcse_1 = 2.0 * V[5] * V[9];
            temp_gcse_7 = (2.0 * V[0] - 2.0) * V[9] - 2.0 * V[10] * V[8];
            temp_gcse_15 = V[8] * V[8];
            temp_gcse_16 = 2.0 * V[8] * V[9] + (2.0 * V[0] - 2.0) * V[10];
            temp_gcse_28 = 2.0 * V[15];
            temp_gcse_39 = V[10] * V[10] - V[0] * V[0];
            temp_gcse_44 = 2.0 * V[0] - 2.0;
            temp_gcse_50 = V[9] * V[9];
            temp_gcse_80 = 2.0 * V[0] - 1.0;
            temp_gcse_86 = temp_gcse_50 - temp_gcse_15 + temp_gcse_39 + temp_gcse_80;
            temp_gcse_91 = temp_gcse_50 - temp_gcse_15 + temp_gcse_39;
            temp_gcse_95 = V[0] * V[0];
            temp_gcse_97 = temp_gcse_50 - temp_gcse_15;
            temp_gcse_103 = V[10] * V[10];
            temp_gcse_107 = 2.0 * V[8] * V[9];
            temp_gcse_113 = temp_gcse_44 * V[7];
            temp_gcse_123 = (2.0 * V[0] - 2.0) * V[9];
            temp_gcse_125 = temp_gcse_113 - 2.0 * V[10] * V[6];
            temp_gcse_127 = temp_gcse_28 * V[8];
            temp_gcse_140 = 2.0 * V[8];
            temp_gcse_145 = 2.0 * V[7];
            temp_gcse_146 = 2.0 * V[10] * V[8];
            temp_gcse_148 = t * t;
            temp_gcse_150 = 2.0 * V[10];
            temp_gcse_151 = 2.0 * V[9];
            temp_gcse_153 = 2.0 * V[10] * V[6];
            temp_gcse_160 = (2.0 * V[0] - 2.0) * V[10];
            temp_gcse_168 = 2.0 * V[5];
            temp_gcse_170 = 2.0 * V[0];
            AI[11] = (temp_gcse_86 * az + temp_gcse_16 * ay + temp_gcse_7 * ax - temp_gcse_1 + temp_gcse_127 + temp_gcse_125) * temp_gcse_148 + (-temp_gcse_44 * az + temp_gcse_150 * ay + temp_gcse_151 * ax + temp_gcse_145) * t - az; // e0 ^ (e1 ^ e2)
            temp_gcse_8 = (2.0 * V[0] - 2.0) * V[10] - 2.0 * V[8] * V[9];
            temp_gcse_9 = (2.0 - 2.0 * V[0]) * V[8] - 2.0 * V[10] * V[9];
            temp_gcse_19 = 2.0 * V[5] * V[8] - 2.0 * V[10] * V[7];
            temp_gcse_33 = 2.0 * V[5] * V[8];
            temp_gcse_65 = temp_gcse_50 - temp_gcse_15 - V[10] * V[10] + V[0] * V[0] - 2.0 * V[0];
            temp_gcse_73 = V[0] * V[0] - 2.0 * V[0];
            temp_gcse_87 = temp_gcse_65 + 1.0;
            temp_gcse_100 = -temp_gcse_44 * V[6];
            temp_gcse_109 = (2.0 - 2.0 * V[0]) * V[8];
            temp_gcse_112 = temp_gcse_28 * V[9];
            temp_gcse_134 = 2.0 * V[10] * V[9];
            temp_gcse_142 = 2.0 * V[10] * V[7];
            temp_gcse_157 = temp_gcse_50 - temp_gcse_15 - V[10] * V[10];
            temp_gcse_163 = 2.0 * V[6];
            AI[12] = (temp_gcse_8 * az + temp_gcse_87 * ay + temp_gcse_9 * ax + temp_gcse_112 + temp_gcse_19 + temp_gcse_100) * temp_gcse_148 + (temp_gcse_150 * az + temp_gcse_44 * ay - temp_gcse_140 * ax - temp_gcse_163) * t + ay; // e0 ^ (e1 ^ e3)
            temp_gcse_10 = (2.0 - 2.0 * V[0]) * V[9] - 2.0 * V[10] * V[8];
            temp_gcse_17 = 2.0 * V[10] * V[9] + (2.0 - 2.0 * V[0]) * V[8];
            temp_gcse_31 = 2.0 * V[6] * V[8];
            temp_gcse_88 = temp_gcse_50 + temp_gcse_15 - V[10] * V[10] - V[0] * V[0] + temp_gcse_80;
            temp_gcse_92 = temp_gcse_50 + temp_gcse_15 - V[10] * V[10] - V[0] * V[0];
            temp_gcse_102 = 2.0 * V[7] * V[9];
            temp_gcse_104 = temp_gcse_44 * V[5];
            temp_gcse_129 = temp_gcse_15 - V[10] * V[10] - V[0] * V[0];
            temp_gcse_155 = temp_gcse_15 - V[10] * V[10];
            temp_gcse_164 = temp_gcse_150 * V[15];
            AI[13] = (temp_gcse_10 * az + temp_gcse_17 * ay + temp_gcse_88 * ax + temp_gcse_102 + temp_gcse_31 + temp_gcse_104 + temp_gcse_164) * temp_gcse_148 + (-(temp_gcse_151 * az) - temp_gcse_140 * ay + -temp_gcse_44 * ax + temp_gcse_168) * t - ax; // e0 ^ (e2 ^ e3)
            temp_gcse_30 = (2.0 * V[0] - 2.0) * t;
            temp_gcse_49 = (V[9] * V[9] + temp_gcse_15 + V[10] * V[10] + V[0] * V[0] - 2.0 * V[0] + 1.0) * t * t + temp_gcse_30;
            temp_gcse_51 = (V[9] * V[9] + temp_gcse_15 + V[10] * V[10] + V[0] * V[0] - 2.0 * V[0] + 1.0) * t * t;
            temp_gcse_59 = V[9] * V[9] + temp_gcse_15 + V[10] * V[10] + V[0] * V[0] - 2.0 * V[0];
            temp_gcse_96 = V[9] * V[9] + temp_gcse_15;
            AI[14] = temp_gcse_49 + 1.0; // e1 ^ (e2 ^ e3)
            BI[11] = (temp_gcse_86 * bz + temp_gcse_16 * by + temp_gcse_7 * bx - temp_gcse_1 + temp_gcse_127 + temp_gcse_125) * temp_gcse_148 + (-temp_gcse_44 * bz + temp_gcse_150 * by + temp_gcse_151 * bx + temp_gcse_145) * t - bz; // e0 ^ (e1 ^ e2)
            BI[12] = (temp_gcse_8 * bz + temp_gcse_87 * by + temp_gcse_9 * bx + temp_gcse_112 + temp_gcse_19 + temp_gcse_100) * temp_gcse_148 + (temp_gcse_150 * bz + temp_gcse_44 * by - temp_gcse_140 * bx - temp_gcse_163) * t + by; // e0 ^ (e1 ^ e3)
            BI[13] = (temp_gcse_10 * bz + temp_gcse_17 * by + temp_gcse_88 * bx + temp_gcse_102 + temp_gcse_31 + temp_gcse_104 + temp_gcse_164) * temp_gcse_148 + (-(temp_gcse_151 * bz) - temp_gcse_140 * by + -temp_gcse_44 * bx + temp_gcse_168) * t - bx; // e0 ^ (e2 ^ e3)
            BI[14] = AI[14]; // e1 ^ (e2 ^ e3)
            CI[11] = (temp_gcse_86 * cz + temp_gcse_16 * cy + temp_gcse_7 * cx - temp_gcse_1 + temp_gcse_127 + temp_gcse_125) * temp_gcse_148 + (-temp_gcse_44 * cz + temp_gcse_150 * cy + temp_gcse_151 * cx + temp_gcse_145) * t - cz; // e0 ^ (e1 ^ e2)
            CI[12] = (temp_gcse_8 * cz + temp_gcse_87 * cy + temp_gcse_9 * cx + temp_gcse_112 + temp_gcse_19 + temp_gcse_100) * temp_gcse_148 + (temp_gcse_150 * cz + temp_gcse_44 * cy - temp_gcse_140 * cx - temp_gcse_163) * t + cy; // e0 ^ (e1 ^ e3)
            CI[13] = (temp_gcse_10 * cz + temp_gcse_17 * cy + temp_gcse_88 * cx + temp_gcse_102 + temp_gcse_31 + temp_gcse_104 + temp_gcse_164) * temp_gcse_148 + (-(temp_gcse_151 * cz) - temp_gcse_140 * cy + -temp_gcse_44 * cx + temp_gcse_168) * t - cx; // e0 ^ (e2 ^ e3)
            CI[14] = AI[14]; // e1 ^ (e2 ^ e3)
        }


        public static void SnelliusPothenotProblemCassiniGaFuL(LinFloat64Vector3D pointA, LinFloat64Vector3D pointB, LinFloat64Vector3D pointC, double alpha, double beta, double[] p1, double[] p2)
        {
            var Ax = pointA.X.ScalarValue;
            var Ay = pointA.Y.ScalarValue;
            var Bx = pointB.X.ScalarValue;
            var By = pointB.Y.ScalarValue;
            var Cx = pointC.X.ScalarValue;
            var Cy = pointC.Y.ScalarValue;

            //var alphaCos = Math.Cos(alpha);
            var alphaSin = Math.Sin(alpha);
            
            //var betaCos = Math.Cos(beta);
            var betaSin = Math.Sin(beta);

            //Begin GA-FuL MetaContext Code Generation, 2024-06-10T01:24:50.8270568+03:00
            //MetaContext: CGA_Cassini
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 425 sub-expressions, 0 generated temps, 425 total.
            //Target Temp Variables: 425 total.
            //Output Variables: 4 total.
            //Computations: 1.205128205128205 average, 517 total.
            //Memory Reads: 1.9836829836829837 average, 851 total.
            //Memory Writes: 429 total.
            //
            //MetaContext Binding Data:
            //   1 = constant: '1'
            //   -1 = constant: '-1'
            //   2 = constant: '2'
            //   -2 = constant: '-2'
            //   0.5 = constant: '0.5'
            //   -0.5 = constant: '-0.5'
            //   Rational[1, 2] = constant: 'Rational[1, 2]'
            //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
            //   Ax = parameter: Ax
            //   Ay = parameter: Ay
            //   Bx = parameter: Bx
            //   By = parameter: By
            //   Cx = parameter: Cx
            //   Cy = parameter: Cy
            //   alphaSin = parameter: alphaSin
            //   betaSin = parameter: betaSin

            var temp0 = Ay * Bx;
            var temp1 = Ax * By - temp0;
            var temp2 = 1 / (Math.Sqrt(2)) * (Math.Sqrt(1 + alphaSin));
            var temp3 = 0.5 * Ax;
            var temp4 = temp2 * temp3;
            var temp5 = 1 / (Math.Sqrt(2)) * (Math.Sqrt(1 - alphaSin));
            var temp6 = 0.5 * Ay;
            var temp7 = temp5 * temp6;
            var temp8 = temp4 - temp7;
            var temp9 = -0.5 * Ay;
            var temp10 = temp5 * temp9;
            var temp11 = temp8 + temp10;
            var temp12 = -0.5 * Ax;
            var temp13 = temp2 * temp12;
            var temp14 = temp11 + temp13;
            var temp15 = temp1 * temp14;
            var temp16 = Ax * Ax;
            var temp17 = Ay * Ay;
            var temp18 = 1 + temp16 + temp17;
            var temp19 = 0.5 * temp18;
            var temp20 = Bx * temp19;
            var temp21 = Bx * Bx;
            var temp22 = By * By;
            var temp23 = 1 + temp21 + temp22;
            var temp24 = 0.5 * temp23;
            var temp25 = Ax * temp24 - temp20;
            var temp26 = -1 + temp16 + temp17;
            var temp27 = 0.5 * temp26;
            var temp28 = temp25 + Bx * temp27;
            var temp29 = -1 + temp21 + temp22;
            var temp30 = 0.5 * temp29;
            var temp31 = Ax * temp30;
            var temp32 = temp28 - temp31;
            var temp33 = temp5 * temp32;
            var temp34 = -(temp15 + temp33);
            var temp35 = temp15 + temp34;
            var temp36 = By * temp19;
            var temp37 = Ay * temp24 - temp36;
            var temp38 = By * temp27 + temp37;
            var temp39 = Ay * temp30;
            var temp40 = temp38 - temp39;
            var temp41 = temp35 + temp2 * temp40;
            var temp42 = -temp5;
            var temp43 = temp2 * temp32 + temp5 * temp40;
            var temp44 = temp2 * temp43;
            var temp45 = temp41 * temp42 - temp44;
            var temp46 = temp8 * temp12;
            var temp47 = temp3 * temp5;
            var temp48 = temp2 * temp6;
            var temp49 = temp47 + temp48;
            var temp50 = temp9 * temp49;
            var temp51 = temp46 + temp50;
            var temp52 = -(temp46 + temp50);
            var temp53 = temp51 + temp52;
            var temp54 = temp32 * temp53;
            var temp55 = temp45;
            var temp56 = -0.5 * temp29;
            var temp57 = temp27 + temp56;
            var temp58 = -0.5 * temp23;
            var temp59 = temp19 + temp58;
            var temp60 = -0.5 * temp26 + temp30;
            var temp61 = temp59 * temp59 + temp57 * temp60;
            var temp62 = Bx - Ax;
            var temp63 = -Bx;
            var temp64 = Ax + temp63;
            var temp65 = temp61 + temp62 * temp64;
            var temp66 = -By;
            var temp67 = Ay + temp66;
            var temp68 = By - Ay;
            var temp69 = temp65 + temp67 * temp68;
            var temp70 = 1 / (Math.Sqrt(Math.Abs(temp69)));
            var temp71 = temp57 * temp70;
            var temp72 = -temp71;
            var temp73 = temp55 * temp72;
            var temp74 = temp7 - temp4;
            var temp75 = temp74 - temp10;
            var temp76 = temp75 - temp13;
            var temp77 = temp41 * temp76;
            var temp78 = -(temp47 + temp48);
            var temp79 = temp5 * temp12;
            var temp80 = temp78 + temp79;
            var temp81 = temp2 * temp9;
            var temp82 = temp80 - temp81;
            var temp83 = temp43 * temp82 - temp77;
            var temp84 = temp49 - temp79;
            var temp85 = temp81 + temp84;
            var temp86 = temp1 * temp53 + temp32 * temp85;
            var temp87 = temp14 * temp40;
            var temp88 = temp86 - temp87;
            var temp89 = temp1 * temp2 + temp88;
            var temp90 = temp83;
            var temp91 = temp2 * temp89;
            var temp92 = temp90 - temp91;
            var temp93 = temp1 * temp5;
            var temp94 = temp40 * temp85;
            var temp95 = -(temp93 + temp94);
            var temp96 = temp14 * temp32;
            var temp97 = temp95 - temp96;
            var temp98 = temp42 * temp97;
            var temp99 = temp92 - temp98;
            var temp100 = temp54 * temp82 + temp99;
            var temp101 = temp40 * temp53;
            var temp102 = temp76 * temp101;
            var temp103 = temp100 - temp102;
            var temp104 = temp67 * temp70;
            var temp105 = -temp104;
            var temp106 = temp103 * temp105;
            var temp107 = temp106 - temp73;
            var temp108 = temp59 * temp70;
            var temp109 = -temp108;
            var temp110 = temp55 * temp109;
            var temp111 = temp103 * temp105 - temp110;
            var temp112 = temp73 - temp106;
            var temp113 = temp111 * temp111 + temp107 * temp112;
            var temp114 = temp62 * temp70;
            var temp115 = temp55 * temp114;
            var temp116 = temp2 * temp41;
            var temp117 = temp42 * temp43;
            var temp118 = -(temp116 + temp117);
            var temp119 = temp118;
            var temp120 = -temp119;
            var temp121 = temp105 * temp120;
            var temp122 = temp115 - temp121;
            var temp123 = temp121 - temp115;
            var temp124 = temp113 + temp122 * temp123;
            var temp125 = temp103 * temp109;
            var temp126 = temp72 * temp103 - temp125;
            var temp127 = temp124 + temp126 * temp126;
            var temp128 = temp103 * temp114;
            var temp129 = -temp128;
            var temp130 = temp109 * temp120 + temp129;
            var temp131 = temp127 + temp130 * temp130;
            var temp132 = temp72 * temp120;
            var temp133 = temp129 + temp132;
            var temp134 = temp128 - temp132;
            var temp135 = temp131 + temp133 * temp134;
            var temp136 = temp42 * temp54;
            var temp137 = -temp136;
            var temp138 = temp2 * temp101;
            var temp139 = temp137 - temp138;
            var temp140 = -temp105 * temp139;
            var temp141 = temp41 * temp82;
            var temp142 = temp43 * temp76;
            var temp143 = -(temp141 + temp142);
            var temp144 = temp42 * temp89;
            var temp145 = temp143 - temp144;
            var temp146 = temp145;
            var temp147 = temp2 * temp97 + temp146;
            var temp148 = temp54 * temp76;
            var temp149 = temp147 - temp148;
            var temp150 = temp82 * temp101;
            var temp151 = temp149 - temp150;
            var temp152 = temp72 * temp151;
            var temp153 = -(temp140 + temp152);
            var temp154 = temp109 * temp151 + temp153;
            var temp155 = temp2 * temp54;
            var temp156 = -temp155;
            var temp157 = temp42 * temp101 + temp156;
            var temp158 = temp154 + temp114 * temp157;
            var temp159 = temp135 + temp158 * temp158;
            var temp160 = 1 / (Math.Sqrt(Math.Abs(temp159)));
            var temp161 = temp107 * temp160;
            var temp162 = temp111 * temp160;
            var temp163 = -0.5 * temp161 + -0.5 * temp162;
            var temp164 = temp163 * temp163;
            var temp165 = temp133 * temp160;
            var temp166 = temp130 * temp160;
            var temp167 = -0.5 * temp165 + -0.5 * temp166;
            var temp168 = temp167 * temp167;
            var temp169 = 1 + temp164 + temp168;
            var temp170 = 0.5 * temp169;
            var temp171 = temp24 * temp170;
            var temp172 = -1 + temp164 + temp168;
            var temp173 = 0.5 * temp172;
            var temp174 = temp30 * temp173 - temp171;
            var temp175 = Bx * temp163 + temp174;
            var temp176 = By * temp167 + temp175;
            var temp177 = -2 * temp176;
            var temp178 = -0.5 * temp177;
            var temp179 = temp170 + temp178;
            var temp180 = temp164 + temp168;
            var temp181 = -0.5 * temp169 + 0.5 * temp177;
            var temp182 = temp180 + temp179 * temp181;
            var temp183 = temp173 + temp178;
            var temp184 = temp182 + temp183 * temp183;
            var temp185 = 1 / (Math.Sqrt(Math.Abs(temp184)));
            var temp186 = temp179 * temp185;
            var temp187 = Bx * Cy;
            var temp188 = By * Cx - temp187;
            var temp189 = 1 / (Math.Sqrt(2)) * (Math.Sqrt(1 + betaSin));
            var temp190 = 0.5 * Cx;
            var temp191 = temp189 * temp190;
            var temp192 = 1 / (Math.Sqrt(2)) * (Math.Sqrt(1 - betaSin));
            var temp193 = 0.5 * Cy;
            var temp194 = temp192 * temp193;
            var temp195 = temp191 - temp194;
            var temp196 = -0.5 * Cy;
            var temp197 = temp192 * temp196;
            var temp198 = temp195 + temp197;
            var temp199 = -0.5 * Cx;
            var temp200 = temp189 * temp199;
            var temp201 = temp198 + temp200;
            var temp202 = temp188 * temp201;
            var temp203 = Cx * Cx;
            var temp204 = Cy * Cy;
            var temp205 = 1 + temp203 + temp204;
            var temp206 = 0.5 * temp205;
            var temp207 = Bx * temp206;
            var temp208 = Cx * temp24 - temp207;
            var temp209 = -1 + temp203 + temp204;
            var temp210 = 0.5 * temp209;
            var temp211 = temp208 + Bx * temp210;
            var temp212 = Cx * temp30;
            var temp213 = temp211 - temp212;
            var temp214 = temp192 * temp213;
            var temp215 = -(temp202 + temp214);
            var temp216 = temp202 + temp215;
            var temp217 = By * temp206;
            var temp218 = Cy * temp24 - temp217;
            var temp219 = Cy * temp30;
            var temp220 = temp218 - temp219;
            var temp221 = By * temp210 + temp220;
            var temp222 = temp216 + temp189 * temp221;
            var temp223 = temp194 - temp191;
            var temp224 = temp223 - temp197;
            var temp225 = temp224 - temp200;
            var temp226 = temp222 * temp225;
            var temp227 = temp189 * temp213 + temp192 * temp221;
            var temp228 = temp190 * temp192;
            var temp229 = temp189 * temp193;
            var temp230 = -(temp228 + temp229);
            var temp231 = temp192 * temp199;
            var temp232 = temp230 + temp231;
            var temp233 = temp189 * temp196;
            var temp234 = temp232 - temp233;
            var temp235 = temp227 * temp234 - temp226;
            var temp236 = temp195 * temp199;
            var temp237 = temp228 + temp229;
            var temp238 = temp196 * temp237;
            var temp239 = temp236 + temp238;
            var temp240 = -(temp236 + temp238);
            var temp241 = temp239 + temp240;
            var temp242 = temp237 - temp231;
            var temp243 = temp233 + temp242;
            var temp244 = temp188 * temp241 + temp213 * temp243;
            var temp245 = temp201 * temp221;
            var temp246 = temp244 - temp245;
            var temp247 = temp188 * temp189 + temp246;
            var temp248 = temp235;
            var temp249 = temp189 * temp247;
            var temp250 = temp248 - temp249;
            var temp251 = temp188 * temp192;
            var temp252 = temp221 * temp243;
            var temp253 = -(temp251 + temp252);
            var temp254 = temp201 * temp213;
            var temp255 = temp253 - temp254;
            var temp256 = -temp192;
            var temp257 = temp255 * temp256;
            var temp258 = temp250 - temp257;
            var temp259 = temp213 * temp241;
            var temp260 = temp258 + temp234 * temp259;
            var temp261 = temp221 * temp241;
            var temp262 = temp225 * temp261;
            var temp263 = temp260 - temp262;
            var temp264 = Bx - Cx;
            var temp265 = temp58 + temp206;
            var temp266 = temp56 + temp210;
            var temp267 = temp30 + -0.5 * temp209;
            var temp268 = temp265 * temp265 + temp266 * temp267;
            var temp269 = Cx + temp63;
            var temp270 = temp268 + temp264 * temp269;
            var temp271 = Cy + temp66;
            var temp272 = By - Cy;
            var temp273 = temp270 + temp271 * temp272;
            var temp274 = 1 / (Math.Sqrt(Math.Abs(temp273)));
            var temp275 = temp264 * temp274;
            var temp276 = temp263 * temp275;
            var temp277 = temp189 * temp222;
            var temp278 = temp227 * temp256;
            var temp279 = -(temp277 + temp278);
            var temp280 = temp279;
            var temp281 = -temp280;
            var temp282 = temp266 * temp274;
            var temp283 = -temp282;
            var temp284 = temp281 * temp283;
            var temp285 = temp284 - temp276;
            var temp286 = temp189 * temp227;
            var temp287 = temp222 * temp256 - temp286;
            var temp288 = temp287;
            var temp289 = temp265 * temp274;
            var temp290 = -temp289;
            var temp291 = temp288 * temp290;
            var temp292 = temp271 * temp274;
            var temp293 = -temp292;
            var temp294 = temp263 * temp293;
            var temp295 = temp294 - temp291;
            var temp296 = temp283 * temp288;
            var temp297 = temp294 - temp296;
            var temp298 = temp296 - temp294;
            var temp299 = temp295 * temp295 + temp297 * temp298;
            var temp300 = temp275 * temp288;
            var temp301 = temp281 * temp293;
            var temp302 = temp300 - temp301;
            var temp303 = temp301 - temp300;
            var temp304 = temp299 + temp302 * temp303;
            var temp305 = temp263 * temp290;
            var temp306 = temp263 * temp283 - temp305;
            var temp307 = temp304 + temp306 * temp306;
            var temp308 = temp263 * temp275;
            var temp309 = temp281 * temp290 - temp308;
            var temp310 = temp307 + temp309 * temp309;
            var temp311 = temp276 - temp284;
            var temp312 = temp310 + temp285 * temp311;
            var temp313 = temp256 * temp259;
            var temp314 = -temp313;
            var temp315 = temp189 * temp261;
            var temp316 = temp314 - temp315;
            var temp317 = -temp293 * temp316;
            var temp318 = temp222 * temp234;
            var temp319 = temp225 * temp227;
            var temp320 = -(temp318 + temp319);
            var temp321 = temp247 * temp256;
            var temp322 = temp320 - temp321;
            var temp323 = temp322;
            var temp324 = temp189 * temp255 + temp323;
            var temp325 = temp225 * temp259;
            var temp326 = temp324 - temp325;
            var temp327 = temp234 * temp261;
            var temp328 = temp326 - temp327;
            var temp329 = temp283 * temp328;
            var temp330 = -(temp317 + temp329);
            var temp331 = temp290 * temp328 + temp330;
            var temp332 = temp189 * temp259;
            var temp333 = -temp332;
            var temp334 = temp256 * temp261 + temp333;
            var temp335 = temp331 + temp275 * temp334;
            var temp336 = temp312 + temp335 * temp335;
            var temp337 = 1 / (Math.Sqrt(Math.Abs(temp336)));
            var temp338 = temp285 * temp337;
            var temp339 = temp309 * temp337;
            var temp340 = -0.5 * temp338 + -0.5 * temp339;
            var temp341 = temp340 * temp340;
            var temp342 = temp297 * temp337;
            var temp343 = temp295 * temp337;
            var temp344 = -0.5 * temp342 + -0.5 * temp343;
            var temp345 = temp344 * temp344;
            var temp346 = temp341 + temp345;
            var temp347 = 1 + temp341 + temp345;
            var temp348 = 0.5 * temp347;
            var temp349 = temp24 * temp348;
            var temp350 = -1 + temp341 + temp345;
            var temp351 = 0.5 * temp350;
            var temp352 = temp30 * temp351 - temp349;
            var temp353 = Bx * temp344 + temp352;
            var temp354 = By * temp340 + temp353;
            var temp355 = -2 * temp354;
            var temp356 = -0.5 * temp355;
            var temp357 = temp348 + temp356;
            var temp358 = -0.5 * temp347 + 0.5 * temp355;
            var temp359 = temp346 + temp357 * temp358;
            var temp360 = temp351 + temp356;
            var temp361 = temp359 + temp360 * temp360;
            var temp362 = 1 / (Math.Sqrt(Math.Abs(temp361)));
            var temp363 = temp340 * temp362;
            var temp364 = temp186 * temp363;
            var temp365 = temp167 * temp185;
            var temp366 = temp357 * temp362;
            var temp367 = temp365 * temp366;
            var temp368 = temp364 - temp367;
            var temp369 = temp163 * temp185;
            var temp370 = temp363 * temp369;
            var temp371 = temp344 * temp362;
            var temp372 = temp365 * temp371;
            var temp373 = temp370 - temp372;
            var temp374 = temp183 * temp185;
            var temp375 = temp371 * temp374;
            var temp376 = temp360 * temp362;
            var temp377 = temp369 * temp376;
            var temp378 = temp375 - temp377;
            var temp379 = temp186 * temp371;
            var temp380 = -temp379;
            var temp381 = temp378 + temp380;
            var temp382 = temp366 * temp369;
            var temp383 = temp381 + temp382;
            var temp384 = temp363 * temp374;
            var temp385 = temp368 - temp384;
            var temp386 = temp365 * temp376;
            var temp387 = temp385 + temp386;
            var temp388 = temp383 * temp383 + temp387 * temp387;
            var temp389 = temp373 * temp373 + temp388;
            var temp390 = temp372 - temp370;
            var temp391 = temp389 + temp373 * temp390;
            var temp392 = 1 / temp391;
            var temp393 = temp373 * temp392;
            var temp394 = temp186 * temp376;
            var temp395 = temp366 * temp374;
            var temp396 = temp394 - temp395;
            var temp397 = temp383 * temp392;
            var temp398 = temp396 * temp397;
            var temp399 = temp368 * temp393 - temp398;
            var temp400 = temp384 - temp386;
            var temp401 = temp393 * temp400;
            var temp402 = temp399 - temp401;
            var temp403 = temp395 - temp394;
            var temp404 = temp380 + temp382;
            var temp405 = temp379 - temp382;
            var temp406 = temp396 * temp403 + temp404 * temp405;
            var temp407 = temp367 - temp364;
            var temp408 = temp406 + temp368 * temp407;
            var temp409 = temp377 - temp375;
            var temp410 = temp408 + temp409 * temp409;
            var temp411 = temp400 * temp400 + temp410;
            var temp412 = temp390 * temp390 + temp411;
            var temp413 = Math.Sqrt(temp412);
            var temp414 = temp387 * temp392;
            var temp415 = temp413 * temp414;
            var p1X = temp402 - temp415;

            var temp417 = temp393 * temp404;
            var temp418 = temp396 * temp414;
            var temp419 = -(temp417 + temp418);
            var temp420 = temp393 * temp409;
            var temp421 = -(temp419 + temp420);
            var temp422 = temp397 * temp413;
            var p1Y = temp421 - temp422;

            var temp424 = -temp413;
            var temp425 = temp414 * temp424;
            var p2X = temp402 - temp425;

            var temp427 = temp397 * temp424;
            var p2Y = temp421 - temp427;

            //Finish GA-FuL MetaContext Code Generation, 2024-06-10T01:24:50.9857844+03:00

            p1[1] = p1X;
            p1[2] = p1Y;

            p2[1] = p2X;
            p2[2] = p2Y;
        }

        public static void SnelliusPothenotProblemCassiniGaalop(LinFloat64Vector3D pointA, LinFloat64Vector3D pointB, LinFloat64Vector3D pointC, double alpha, double beta, double[] p1, double[] p2)
        {
            var Ax = pointA.X.ScalarValue;
            var Ay = pointA.Y.ScalarValue;
            var Bx = pointB.X.ScalarValue;
            var By = pointB.Y.ScalarValue;
            var Cx = pointC.X.ScalarValue;
            var Cy = pointC.Y.ScalarValue;

            var a = new double[32];
            var angle1 = new double[32];
            var angle2 = new double[32];
            var b = new double[32];
            var c = new double[32];
            var c1 = new double[32];
            var c11 = new double[32];
            var c2 = new double[32];
            var c21 = new double[32];
            var da = new double[32];
            var dc = new double[32];
            var lab = new double[32];
            var lao = new double[32];
            var lcb = new double[32];
            var lco = new double[32];
            var midAb = new double[32];
            var midAb1 = new double[32];
            var midCb = new double[32];
            var midCb1 = new double[32];
            var o1 = new double[32];
            var o2 = new double[32];
            var p = new double[32];
            var r1 = new double[32];
            var r2 = new double[32];
            var ra = new double[32];
            var rc = new double[32];
            var s1 = new double[32];
            var s2 = new double[32];
            var ss1 = new double[32];
            var ss11 = new double[32];
            var ss2 = new double[32];
            var ss21 = new double[32];
            var ta = new double[32];
            var tc = new double[32];
            double temp_gcse_1, temp_gcse_10, temp_gcse_100, temp_gcse_101, temp_gcse_102, temp_gcse_103, temp_gcse_104, temp_gcse_11, temp_gcse_12, temp_gcse_13, temp_gcse_14, temp_gcse_15, temp_gcse_16, temp_gcse_17, temp_gcse_18, temp_gcse_19, temp_gcse_2, temp_gcse_20, temp_gcse_21, temp_gcse_22, temp_gcse_23, temp_gcse_24, temp_gcse_25, temp_gcse_26, temp_gcse_27, temp_gcse_28, temp_gcse_29, temp_gcse_3, temp_gcse_30, temp_gcse_31, temp_gcse_32, temp_gcse_33, temp_gcse_34, temp_gcse_35, temp_gcse_36, temp_gcse_37, temp_gcse_38, temp_gcse_39, temp_gcse_4, temp_gcse_40, temp_gcse_41, temp_gcse_42, temp_gcse_43, temp_gcse_44, temp_gcse_45, temp_gcse_46, temp_gcse_47, temp_gcse_48, temp_gcse_49, temp_gcse_5, temp_gcse_50, temp_gcse_51, temp_gcse_52, temp_gcse_53, temp_gcse_54, temp_gcse_55, temp_gcse_56, temp_gcse_57, temp_gcse_58, temp_gcse_59, temp_gcse_6, temp_gcse_60, temp_gcse_61, temp_gcse_62, temp_gcse_63, temp_gcse_64, temp_gcse_65, temp_gcse_66, temp_gcse_67, temp_gcse_68, temp_gcse_69, temp_gcse_7, temp_gcse_70, temp_gcse_71, temp_gcse_72, temp_gcse_73, temp_gcse_74, temp_gcse_75, temp_gcse_76, temp_gcse_77, temp_gcse_78, temp_gcse_79, temp_gcse_8, temp_gcse_80, temp_gcse_81, temp_gcse_82, temp_gcse_83, temp_gcse_84, temp_gcse_85, temp_gcse_86, temp_gcse_87, temp_gcse_88, temp_gcse_89, temp_gcse_9, temp_gcse_90, temp_gcse_91, temp_gcse_92, temp_gcse_93, temp_gcse_94, temp_gcse_95, temp_gcse_96, temp_gcse_97, temp_gcse_98, temp_gcse_99;
            a[4] = Math.Sqrt(Math.Abs(Ax * Ax + Ay * Ay)) / 2.0; // einf
            b[4] = Math.Sqrt(Math.Abs(Bx * Bx + By * By)) / 2.0; // einf
            c[4] = Math.Sqrt(Math.Abs(Cx * Cx + Cy * Cy)) / 2.0; // einf
            lab[17] = Ax * By + (-(Ay * Bx)); // e1 ^ (e2 ^ einf)
            temp_gcse_18 = (-Bx);
            lab[21] = (-(Ax + temp_gcse_18)); // e1 ^ (einf ^ e0)
            temp_gcse_95 = (-By);
            lab[24] = (-(Ay + temp_gcse_95)); // e2 ^ (einf ^ e0)
            lcb[17] = Cx * By + (-(Cy * Bx)); // e1 ^ (e2 ^ einf)
            lcb[21] = (-(Cx + temp_gcse_18)); // e1 ^ (einf ^ e0)
            lcb[24] = (-(Cy + temp_gcse_95)); // e2 ^ (einf ^ e0)
            midAb1[17] = (-(b[4] - a[4])); // e1 ^ (e2 ^ einf)
            midAb1[21] = By - Ay; // e1 ^ (einf ^ e0)
            midAb1[24] = (-(Bx - Ax)); // e2 ^ (einf ^ e0)
            temp_gcse_5 = (-midAb1[21]);
            temp_gcse_8 = (midAb1[21] * temp_gcse_5 + midAb1[24] * (-midAb1[24])) * (midAb1[21] * temp_gcse_5 + midAb1[24] * (-midAb1[24]));
            temp_gcse_14 = Math.Sqrt(Math.Abs(midAb1[21] * temp_gcse_5 + midAb1[24] * (-midAb1[24]))) / Math.Sqrt(Math.Abs(temp_gcse_8));
            temp_gcse_28 = Math.Sqrt(Math.Abs(midAb1[21] * temp_gcse_5 + midAb1[24] * (-midAb1[24])));
            temp_gcse_32 = midAb1[21] * temp_gcse_5;
            temp_gcse_34 = Math.Abs(temp_gcse_8);
            temp_gcse_43 = Math.Abs(midAb1[21] * temp_gcse_5 + midAb1[24] * (-midAb1[24]));
            temp_gcse_44 = (-midAb1[24]);
            temp_gcse_48 = midAb1[21] * temp_gcse_5 + midAb1[24] * (-midAb1[24]);
            temp_gcse_53 = Math.Sqrt(Math.Abs(temp_gcse_8));
            temp_gcse_92 = midAb1[24] * (-midAb1[24]);
            midAb[17] = midAb1[17] * temp_gcse_14; // e1 ^ (e2 ^ einf)
            midAb[21] = midAb1[21] * temp_gcse_14; // e1 ^ (einf ^ e0)
            midAb[24] = midAb1[24] * temp_gcse_14; // e2 ^ (einf ^ e0)
            midCb1[17] = (-(b[4] - c[4])); // e1 ^ (e2 ^ einf)
            midCb1[21] = By - Cy; // e1 ^ (einf ^ e0)
            midCb1[24] = (-(Bx - Cx)); // e2 ^ (einf ^ e0)
            temp_gcse_13 = (midCb1[21] * (-midCb1[21]) + midCb1[24] * (-midCb1[24])) * (midCb1[21] * (-midCb1[21]) + midCb1[24] * (-midCb1[24]));
            temp_gcse_40 = (-midCb1[21]);
            temp_gcse_51 = Math.Abs(temp_gcse_13);
            temp_gcse_63 = (-midCb1[24]);
            temp_gcse_69 = midCb1[21] * temp_gcse_40;
            temp_gcse_74 = Math.Sqrt(Math.Abs(temp_gcse_69 + midCb1[24] * temp_gcse_63)) / Math.Sqrt(temp_gcse_51);
            temp_gcse_77 = midCb1[24] * temp_gcse_63;
            temp_gcse_82 = Math.Abs(temp_gcse_69 + midCb1[24] * temp_gcse_63);
            temp_gcse_84 = Math.Sqrt(temp_gcse_51);
            temp_gcse_89 = temp_gcse_69 + midCb1[24] * temp_gcse_63;
            temp_gcse_91 = Math.Sqrt(Math.Abs(temp_gcse_69 + midCb1[24] * temp_gcse_63));
            midCb[17] = midCb1[17] * temp_gcse_74; // e1 ^ (e2 ^ einf)
            midCb[21] = midCb1[21] * temp_gcse_74; // e1 ^ (einf ^ e0)
            midCb[24] = midCb1[24] * temp_gcse_74; // e2 ^ (einf ^ e0)
            angle1[0] = (1.57079633 - alpha) / 2.0; // 1.0
            angle2[0] = (beta - 1.57079633) / 2.0; // 1.0
            ra[0] = Math.Cos(angle1[0]); // 1.0
            ra[6] = Math.Sin(angle1[0]); // e1 ^ e2
            rc[0] = Math.Cos(angle2[0]); // 1.0
            rc[6] = Math.Sin(angle2[0]); // e1 ^ e2
            ta[8] = (-(Ax / 2.0)); // e1 ^ einf
            ta[11] = (-(Ay / 2.0)); // e2 ^ einf
            tc[8] = (-(Cx / 2.0)); // e1 ^ einf
            tc[11] = (-(Cy / 2.0)); // e2 ^ einf
            temp_gcse_41 = (-ta[11]);
            temp_gcse_104 = (-ta[8]);
            da[8] = ra[0] * temp_gcse_104 + ra[6] * temp_gcse_41 + ta[8] * ra[0] + (-(ta[11] * ra[6])); // e1 ^ einf
            da[11] = ra[0] * temp_gcse_41 + (-(ra[6] * temp_gcse_104)) + ta[8] * ra[6] + ta[11] * ra[0]; // e2 ^ einf
            temp_gcse_35 = (-tc[8]);
            temp_gcse_87 = (-tc[11]);
            dc[8] = rc[0] * temp_gcse_35 + rc[6] * temp_gcse_87 + tc[8] * rc[0] + (-(tc[11] * rc[6])); // e1 ^ einf
            dc[11] = rc[0] * temp_gcse_87 + (-(rc[6] * temp_gcse_35)) + tc[8] * rc[6] + tc[11] * rc[0]; // e2 ^ einf
            temp_gcse_4 = ra[6] * lab[21];
            temp_gcse_16 = ra[0] * lab[24];
            temp_gcse_26 = temp_gcse_16 + (-temp_gcse_4);
            temp_gcse_47 = ra[0] * lab[21] + ra[6] * lab[24];
            temp_gcse_54 = ra[6] * lab[24];
            temp_gcse_59 = (-temp_gcse_4);
            temp_gcse_70 = (-ra[6]);
            temp_gcse_71 = ra[0] * lab[21];
            lao[17] = ((-(ra[6] * lab[17])) + (-(da[8] * lab[21])) + (-(da[11] * lab[24]))) * temp_gcse_70 + (ra[0] * lab[17] + (-(da[8] * lab[24])) + da[11] * lab[21]) * ra[0] + (-(temp_gcse_47 * (-da[11]))) + temp_gcse_26 * (-da[8]); // e1 ^ (e2 ^ einf)
            lao[21] = temp_gcse_47 * ra[0] + (-(temp_gcse_26 * temp_gcse_70)); // e1 ^ (einf ^ e0)
            lao[24] = temp_gcse_47 * temp_gcse_70 + temp_gcse_26 * ra[0]; // e2 ^ (einf ^ e0)
            temp_gcse_22 = rc[0] * lcb[24];
            temp_gcse_29 = (-rc[6]);
            temp_gcse_45 = rc[6] * lcb[21];
            temp_gcse_50 = (-temp_gcse_45);
            temp_gcse_58 = rc[6] * lcb[24];
            temp_gcse_85 = rc[0] * lcb[21];
            temp_gcse_86 = temp_gcse_22 + temp_gcse_50;
            temp_gcse_88 = temp_gcse_85 + temp_gcse_58;
            lco[17] = ((-(rc[6] * lcb[17])) + (-(dc[8] * lcb[21])) + (-(dc[11] * lcb[24]))) * temp_gcse_29 + (rc[0] * lcb[17] + (-(dc[8] * lcb[24])) + dc[11] * lcb[21]) * rc[0] + (-(temp_gcse_88 * (-dc[11]))) + temp_gcse_86 * (-dc[8]); // e1 ^ (e2 ^ einf)
            lco[21] = temp_gcse_88 * rc[0] + (-(temp_gcse_86 * temp_gcse_29)); // e1 ^ (einf ^ e0)
            lco[24] = temp_gcse_88 * temp_gcse_29 + temp_gcse_86 * rc[0]; // e2 ^ (einf ^ e0)
            temp_gcse_12 = (-midAb[17]);
            temp_gcse_52 = (-lao[17]);
            ss11[8] = lao[21] * temp_gcse_12 + (-(temp_gcse_52 * midAb[21])); // e1 ^ einf
            temp_gcse_19 = (-lao[24]);
            temp_gcse_60 = (-midAb[24]);
            ss11[11] = (-(temp_gcse_19 * temp_gcse_12 + (-(temp_gcse_52 * temp_gcse_60)))); // e2 ^ einf
            ss11[15] = temp_gcse_19 * midAb[21] + (-(lao[21] * temp_gcse_60)); // einf ^ e0
            temp_gcse_1 = Math.Sqrt(Math.Abs(ss11[15] * (-ss11[15]) * ss11[15] * (-ss11[15])));
            temp_gcse_11 = (-ss11[15]);
            temp_gcse_21 = Math.Abs(ss11[15] * temp_gcse_11);
            temp_gcse_39 = ss11[15] * temp_gcse_11;
            temp_gcse_42 = Math.Sqrt(temp_gcse_21);
            temp_gcse_55 = temp_gcse_42 / temp_gcse_1;
            temp_gcse_67 = Math.Abs(ss11[15] * (-ss11[15]) * ss11[15] * (-ss11[15]));
            temp_gcse_78 = ss11[15] * (-ss11[15]) * ss11[15] * (-ss11[15]);
            ss1[8] = ss11[8] * temp_gcse_55; // e1 ^ einf
            ss1[11] = ss11[11] * temp_gcse_55; // e2 ^ einf
            temp_gcse_57 = (-lco[17]);
            temp_gcse_97 = (-midCb[17]);
            ss21[8] = lco[21] * temp_gcse_97 + (-(temp_gcse_57 * midCb[21])); // e1 ^ einf
            temp_gcse_36 = (-lco[24]);
            temp_gcse_49 = (-midCb[24]);
            ss21[11] = (-(temp_gcse_36 * temp_gcse_97 + (-(temp_gcse_57 * temp_gcse_49)))); // e2 ^ einf
            ss21[15] = temp_gcse_36 * midCb[21] + (-(lco[21] * temp_gcse_49)); // einf ^ e0
            temp_gcse_20 = Math.Abs(ss21[15] * (-ss21[15]) * ss21[15] * (-ss21[15]));
            temp_gcse_24 = ss21[15] * (-ss21[15]);
            temp_gcse_30 = Math.Sqrt(Math.Abs(temp_gcse_24)) / Math.Sqrt(temp_gcse_20);
            temp_gcse_37 = ss21[15] * (-ss21[15]) * ss21[15] * (-ss21[15]);
            temp_gcse_66 = Math.Sqrt(Math.Abs(temp_gcse_24));
            temp_gcse_76 = Math.Abs(temp_gcse_24);
            temp_gcse_90 = Math.Sqrt(temp_gcse_20);
            temp_gcse_100 = (-ss21[15]);
            ss2[8] = ss21[8] * temp_gcse_30; // e1 ^ einf
            ss2[11] = ss21[11] * temp_gcse_30; // e2 ^ einf
            temp_gcse_6 = ss1[11] * ss1[11];
            temp_gcse_25 = ss1[8] * ss1[8] + temp_gcse_6;
            temp_gcse_94 = ss1[8] * ss1[8];
            o1[4] = Math.Sqrt(Math.Abs(temp_gcse_25)) / 2.0; // einf
            temp_gcse_17 = ss2[8] * ss2[8] + ss2[11] * ss2[11];
            temp_gcse_31 = ss2[11] * ss2[11];
            temp_gcse_96 = ss2[8] * ss2[8];
            o2[4] = Math.Sqrt(Math.Abs(temp_gcse_17)) / 2.0; // einf
            temp_gcse_61 = (-b[4]);
            r1[0] = Math.Sqrt(-2.0 * (ss1[8] * Bx + ss1[11] * By + (-o1[4]) + temp_gcse_61)); // 1.0
            r2[0] = Math.Sqrt(-2.0 * (ss2[8] * Bx + ss2[11] * By + (-o2[4]) + temp_gcse_61)); // 1.0
            c11[4] = o1[4] - r1[0] / 2.0 * r1[0]; // einf
            temp_gcse_2 = Math.Sqrt(Math.Abs(ss1[8] * ss1[8] + ss1[11] * ss1[11] + (-c11[4]) + (-c11[4]))) / Math.Sqrt(Math.Abs((ss1[8] * ss1[8] + ss1[11] * ss1[11] + (-c11[4]) + (-c11[4])) * (ss1[8] * ss1[8] + ss1[11] * ss1[11] + (-c11[4]) + (-c11[4]))));
            temp_gcse_7 = ss1[8] * ss1[8] + ss1[11] * ss1[11] + (-c11[4]) + (-c11[4]);
            temp_gcse_23 = Math.Abs((ss1[8] * ss1[8] + ss1[11] * ss1[11] + (-c11[4]) + (-c11[4])) * (ss1[8] * ss1[8] + ss1[11] * ss1[11] + (-c11[4]) + (-c11[4])));
            temp_gcse_38 = (-c11[4]);
            temp_gcse_62 = ss1[8] * ss1[8] + ss1[11] * ss1[11] + (-c11[4]);
            temp_gcse_64 = Math.Abs(ss1[8] * ss1[8] + ss1[11] * ss1[11] + (-c11[4]) + (-c11[4]));
            temp_gcse_93 = (ss1[8] * ss1[8] + ss1[11] * ss1[11] + (-c11[4]) + (-c11[4])) * (ss1[8] * ss1[8] + ss1[11] * ss1[11] + (-c11[4]) + (-c11[4]));
            temp_gcse_99 = Math.Sqrt(Math.Abs((ss1[8] * ss1[8] + ss1[11] * ss1[11] + (-c11[4]) + (-c11[4])) * (ss1[8] * ss1[8] + ss1[11] * ss1[11] + (-c11[4]) + (-c11[4]))));
            temp_gcse_102 = Math.Sqrt(Math.Abs(ss1[8] * ss1[8] + ss1[11] * ss1[11] + (-c11[4]) + (-c11[4])));
            c1[1] = ss1[8] * temp_gcse_2; // e1
            c1[2] = ss1[11] * temp_gcse_2; // e2
            c1[4] = c11[4] * temp_gcse_2; // einf
            c1[5] = temp_gcse_2; // e0
            c21[4] = o2[4] - r2[0] / 2.0 * r2[0]; // einf
            temp_gcse_15 = Math.Abs((ss2[8] * ss2[8] + ss2[11] * ss2[11] + (-c21[4]) + (-c21[4])) * (ss2[8] * ss2[8] + ss2[11] * ss2[11] + (-c21[4]) + (-c21[4])));
            temp_gcse_27 = temp_gcse_17 + (-c21[4]) + (-c21[4]);
            temp_gcse_68 = Math.Sqrt(Math.Abs(temp_gcse_27));
            temp_gcse_72 = temp_gcse_68 / Math.Sqrt(temp_gcse_15);
            temp_gcse_75 = (-c21[4]);
            temp_gcse_79 = Math.Abs(temp_gcse_27);
            temp_gcse_83 = temp_gcse_17 + (-c21[4]);
            temp_gcse_98 = Math.Sqrt(temp_gcse_15);
            temp_gcse_101 = (ss2[8] * ss2[8] + ss2[11] * ss2[11] + (-c21[4]) + (-c21[4])) * (ss2[8] * ss2[8] + ss2[11] * ss2[11] + (-c21[4]) + (-c21[4]));
            c2[1] = ss2[8] * temp_gcse_72; // e1
            c2[2] = ss2[11] * temp_gcse_72; // e2
            c2[4] = c21[4] * temp_gcse_72; // einf
            c2[5] = temp_gcse_72; // e0
            p[6] = (-(c1[4] * c2[5] + (-(c1[5] * c2[4])))); // e1 ^ e2
            p[8] = c1[2] * c2[4] + (-(c1[4] * c2[2])); // e1 ^ einf
            p[9] = (-(c1[2] * c2[5] + (-(c1[5] * c2[2])))); // e1 ^ e0
            p[11] = (-(c1[1] * c2[4] + (-(c1[4] * c2[1])))); // e2 ^ einf
            p[12] = c1[1] * c2[5] + (-(c1[5] * c2[1])); // e2 ^ e0
            p[15] = c1[1] * c2[2] + (-(c1[2] * c2[1])); // einf ^ e0
            s1[0] = Math.Sqrt((-(p[6] * p[6])) + p[8] * p[9] + p[9] * p[8] + p[11] * p[12] + p[12] * p[11] + p[15] * p[15]); // 1.0
            temp_gcse_33 = p[9] * p[9] + p[12] * p[12];
            temp_gcse_65 = p[9] * p[9];
            temp_gcse_73 = p[12] * p[12];
            s2[1] = p[9] / temp_gcse_33; // e1
            s2[2] = p[12] / temp_gcse_33; // e2
            s2[4] = p[15] / temp_gcse_33; // einf
            temp_gcse_9 = p[9] * s2[4];
            temp_gcse_56 = (-temp_gcse_9);
            temp_gcse_81 = p[6] * s2[2];
            p1[1] = s1[0] * s2[1] + temp_gcse_81 + temp_gcse_56; // e1
            temp_gcse_3 = p[12] * s2[4];
            temp_gcse_46 = (-temp_gcse_3);
            temp_gcse_80 = p[6] * s2[1];
            temp_gcse_103 = (-temp_gcse_80);
            p1[2] = s1[0] * s2[2] + temp_gcse_103 + temp_gcse_46; // e2
            temp_gcse_10 = (-s1[0]);
            p2[1] = temp_gcse_10 * s2[1] + temp_gcse_81 + temp_gcse_56; // e1
            p2[2] = temp_gcse_10 * s2[2] + temp_gcse_103 + temp_gcse_46; // e2
        }


        public int Count
            => 1000;

        public LinFloat64Vector3D[] Point1Array { get; set; }

        public LinFloat64Vector3D[] Point2Array { get; set; }

        public LinFloat64Vector3D[] Point3Array { get; set; }

        public LinFloat64Vector3D[] Point4Array { get; set; }


        public double[] tArray { get; set; }

        public double[] D14 { get; set; }

        public double[] D24 { get; set; }

        public double[] D34 { get; set; }
        
        public double[] alpha { get; set; }

        public double[] beta { get; set; }


        public double[] Center { get; } = new double[32];

        public double[] X1 { get; } = new double[32];

        public double[] S1 { get; } = new double[32];

        public double[] S2 { get; } = new double[32];

        public double[] S3 { get; } = new double[32];

        public double[] PP4 { get; } = new double[32];

        public double[] DualPP4 { get; } = new double[32];

        public double[] X4a { get; } = new double[32];

        public double[] X4b { get; } = new double[32];

        public double[] AI { get; } = new double[16];

        public double[] At { get; } = new double[16];

        public double[] BI { get; } = new double[16];

        public double[] Bt { get; } = new double[16];

        public double[] CI { get; } = new double[16];

        public double[] Ct { get; } = new double[16];


        [GlobalSetup]
        public void Setup()
        {
            var randGen = new Random(10);

            Point1Array = new LinFloat64Vector3D[Count];
            Point2Array = new LinFloat64Vector3D[Count];
            Point3Array = new LinFloat64Vector3D[Count];
            Point4Array = new LinFloat64Vector3D[Count];

            tArray = new double[Count];

            D14 = new double[Count];
            D24 = new double[Count];
            D34 = new double[Count];

            alpha = new double[Count];
            beta = new double[Count];

            for (var i = 0; i < Count; i++)
            {
                Point1Array[i] = randGen.GetLinVector3D();
                Point2Array[i] = randGen.GetLinVector3D();
                Point3Array[i] = randGen.GetLinVector3D();
                Point4Array[i] = randGen.GetLinVector3D();

                tArray[i] = randGen.GetFloat64(0, 1);

                D14[i] = randGen.GetFloat64(1, 10);
                D24[i] = randGen.GetFloat64(1, 10);
                D34[i] = randGen.GetFloat64(1, 10);

                alpha[i] = randGen.GetFloat64(0, Math.Tau);
                beta[i] = randGen.GetFloat64(0, Math.Tau);
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
        

        [Benchmark]
        public void RotatingRectangleGaFuL()
        {
            for (var i = 0; i < Count; i++)
            {
                RotatingRectangleGaFuL(
                    tArray[i],
                    AI,
                    At,
                    BI,
                    Bt
                );
            }
        }

        [Benchmark]
        public void RotatingRectangleGaalop()
        {
            for (var i = 0; i < Count; i++)
            {
                RotatingRectangleGaalop(
                    tArray[i],
                    AI,
                    At,
                    BI,
                    Bt
                );
            }
        }
        

        [Benchmark]
        public void SnelliusPothenotProblemCassiniGaFuL()
        {
            for (var i = 0; i < Count; i++)
            {
                SnelliusPothenotProblemCassiniGaFuL(
                    Point1Array[i],
                    Point2Array[i],
                    Point3Array[i],
                    alpha[i],
                    beta[i],
                    At,
                    Bt
                );
            }
        }

        [Benchmark]
        public void SnelliusPothenotProblemCassiniGaalop()
        {
            for (var i = 0; i < Count; i++)
            {
                SnelliusPothenotProblemCassiniGaalop(
                    Point1Array[i],
                    Point2Array[i],
                    Point3Array[i],
                    alpha[i],
                    beta[i],
                    At,
                    Bt
                );
            }
        }


        [Benchmark]
        public void ReconstructMotorGaFuL()
        {
            for (var i = 0; i < Count; i++)
            {
                ReconstructMotorGaFuL(
                    Point1Array[i],
                    Point2Array[i],
                    Point3Array[i],
                    tArray[i],
                    AI,
                    At,
                    BI,
                    Bt,
                    CI,
                    Ct
                );
            }
        }

        [Benchmark]
        public void ReconstructMotorGaalop()
        {
            for (var i = 0; i < Count; i++)
            {
                ReconstructMotorGaalop(
                    Point1Array[i],
                    Point2Array[i],
                    Point3Array[i],
                    tArray[i],
                    AI,
                    At,
                    BI,
                    Bt,
                    CI,
                    Ct
                );
            }
        }


    }

}

