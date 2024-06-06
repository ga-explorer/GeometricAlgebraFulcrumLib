using System.Text;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Applications.Robotics;

public readonly struct HansenProblemData2D
{
    public static HansenProblemData2D Create(LinFloat64Vector2D pointA, LinFloat64Vector2D pointB, LinFloat64Vector2D pointP1, LinFloat64Vector2D pointP2)
    {
        var alpha1 = pointP2.GetDirectedAngleFromPoints(pointP1, pointB);
        var beta1 = pointP2.GetDirectedAngleFromPoints(pointP1, pointA);

        var alpha2 = pointA.GetDirectedAngleFromPoints(pointP2, pointP1);
        var beta2 = pointB.GetDirectedAngleFromPoints(pointP2, pointP1);

        return new HansenProblemData2D
        {
            Ax = pointA.X.ScalarValue,
            Ay = pointA.Y.ScalarValue,
            Bx = pointB.X.ScalarValue,
            By = pointB.Y.ScalarValue,
            Alpha1 = alpha1.RadiansValue,
            Alpha2 = alpha2.RadiansValue,
            Beta1 = beta1.RadiansValue,
            Beta2 = beta2.RadiansValue
        };
    }


    public static void SolveUsingNone(HansenProblemData2D data, out double p1X, out double p1Y, out double p2X, out double p2Y)
    {
        p1X = 0;
        p1Y = 0;
        p2X = 0;
        p2Y = 0;
    }

    public static void SolveUsingTrig(HansenProblemData2D data, out double p1X, out double p1Y, out double p2X, out double p2Y)
    {
        var alpha1Sin = Math.Sin(data.Alpha1);
        var alpha2Sin = Math.Sin(data.Alpha2);
        var beta1Sin = Math.Sin(data.Beta1);
        var beta2Sin = Math.Sin(data.Beta2);

        var alpha1Cos = Math.Sqrt(1 - alpha1Sin * alpha1Sin);
        var alpha2Cos = Math.Sqrt(1 - alpha2Sin * alpha2Sin);
        var beta1Cos = Math.Sqrt(1 - beta1Sin * beta1Sin);
        var beta2Cos = Math.Sqrt(1 - beta2Sin * beta2Sin);

        //alpha1Cos = ((int)Math.Floor(2 * data.Alpha1 / Math.PI) % 4 + 4) % 4 is 1 or 2 ? -alpha1Cos : alpha1Cos;
        //alpha2Cos = ((int)Math.Floor(2 * data.Alpha2 / Math.PI) % 4 + 4) % 4 is 1 or 2 ? -alpha2Cos : alpha2Cos;
        //beta1Cos = ((int)Math.Floor(2 * data.Beta1 / Math.PI) % 4 + 4) % 4 is 1 or 2 ? -beta1Cos : beta1Cos;
        //beta2Cos = ((int)Math.Floor(2 * data.Beta2 / Math.PI) % 4 + 4) % 4 is 1 or 2 ? -beta2Cos : beta2Cos;

        if (((int)Math.Floor(2 * data.Alpha1 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha1Cos = -alpha1Cos;
        if (((int)Math.Floor(2 * data.Alpha2 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha2Cos = -alpha2Cos;
        if (((int)Math.Floor(2 * data.Beta1 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta1Cos = -beta1Cos;
        if (((int)Math.Floor(2 * data.Beta2 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta2Cos = -beta2Cos;

        //Begin GA-FuL MetaContext Code Generation, 2024-03-18T02:35:35.8687062+02:00
        //MetaContext: Hansen-Trig
        //Input Variables: 12 used, 0 not used, 12 total.
        //Temp Variables: 70 sub-expressions, 0 generated temps, 70 total.
        //Target Temp Variables: 16 total.
        //Output Variables: 4 total.
        //Computations: 1.662162162162162 average, 123 total.
        //Memory Reads: 2.2972972972972974 average, 170 total.
        //Memory Writes: 74 total.
        //
        //MetaContext Binding Data:
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   Rational[1, 2] = constant: 'Rational[1, 2]'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   Ax = parameter: data.Ax
        //   Ay = parameter: data.Ay
        //   Bx = parameter: data.Bx
        //   By = parameter: data.By
        //   alpha1Cos = parameter: alpha1Cos
        //   alpha1Sin = parameter: alpha1Sin
        //   alpha2Cos = parameter: alpha2Cos
        //   alpha2Sin = parameter: alpha2Sin
        //   beta1Cos = parameter: beta1Cos
        //   beta1Sin = parameter: beta1Sin
        //   beta2Cos = parameter: beta2Cos
        //   beta2Sin = parameter: beta2Sin

        var temp0 = alpha1Sin * beta1Cos;
        temp0 = alpha1Cos * beta1Sin - temp0;
        var temp1 = alpha2Cos * beta2Cos + alpha2Sin * beta2Sin;
        var temp2 = alpha2Sin * beta2Cos;
        temp2 = alpha2Cos * beta2Sin - temp2;
        var temp3 = alpha2Sin * temp1 + alpha2Cos * temp2;
        var temp4 = alpha2Sin * temp2;
        temp4 = alpha2Cos * temp1 - temp4;
        temp4 = alpha1Cos * temp3 + alpha1Sin * temp4;
        temp4 = alpha2Sin * temp4;
        var temp5 = alpha1Cos * beta1Cos;
        var temp6 = alpha1Sin * beta1Sin;
        var temp7 = temp5 + temp6;
        var temp8 = alpha1Cos * temp0 + alpha1Sin * temp7;
        var temp9 = alpha2Cos * temp8;
        var temp10 = alpha1Sin * temp0;
        temp10 = alpha1Cos * temp7 - temp10;
        var temp11 = alpha2Sin * temp10;
        var temp12 = temp9 + temp11;
        temp3 *= temp12;
        temp3 = temp4 * 1 / temp3;
        temp4 = 1 + temp5 + temp6;
        temp4 = 1 / Math.Sqrt(2) * Math.Sqrt(temp4) * Math.Sign(temp7);
        temp5 = -(temp5 + temp6);
        temp6 = 1 / Math.Sqrt(2) * Math.Sqrt(1 + temp5) * Math.Sign(temp0);
        temp4 = 1 / temp4 * temp6;
        temp3 = 1 / (temp4 + temp3 * temp4) + -temp3 * 1 / (temp4 + temp3 * temp4);
        temp3 = 2 * Math.Atan(temp3);
        temp4 = Math.Cos(temp3);
        temp6 = temp0 * temp4;
        temp3 = Math.Sin(temp3);
        temp7 = temp5 * temp3;
        var temp13 = temp6 - temp7;
        temp4 = temp5 * temp4;
        temp5 = -temp4;
        temp3 = temp0 * temp3;
        var temp14 = -temp3;
        var temp15 = temp5 + temp14;
        temp13 = 1 / Math.Sqrt(2) * Math.Sqrt(1 + temp15) * Math.Sign(temp13);
        temp0 = 1 / temp0 * temp13;
        temp13 = data.Bx - data.Ax;
        temp15 = temp4 + temp14;
        temp4 = 1 + temp4 + temp14;
        temp4 = 1 / Math.Sqrt(2) * Math.Sqrt(temp4) * Math.Sign(temp15);
        temp14 = data.By - data.Ay;
        temp6 += temp7;
        temp3 = 1 + temp5 + temp3;
        temp3 = 1 / Math.Sqrt(2) * Math.Sqrt(temp3) * Math.Sign(temp6);
        temp5 = temp14 * temp3;
        temp5 = temp13 * temp4 - temp5;
        p1X = data.Ax + temp0 * temp5;

        temp5 = temp4 * temp14 + temp13 * temp3;
        p1Y = data.Ay + temp0 * temp5;

        temp0 = alpha2Cos * temp10;
        temp5 = alpha2Sin * temp8;
        temp6 = temp0 - temp5;
        temp7 = temp2 * temp12;
        temp7 = temp1 * temp6 - temp7;
        temp1 = temp1 * temp12 + temp2 * temp6;
        temp1 = temp4 * temp1;
        temp1 = temp3 * temp7 - temp1;
        temp1 = 1 / temp2 * temp1;
        temp0 = temp5 - temp0;
        temp2 = -temp3;
        temp3 = -(temp9 + temp11);
        temp3 = -temp3;
        temp5 = temp4 * temp0 + temp2 * temp3;
        temp3 = temp4 * temp3;
        temp0 = temp0 * temp2 - temp3;
        temp2 = temp13 * temp5 + temp14 * temp0;
        p2X = data.Ax + temp1 * temp2;

        temp0 = temp13 * temp0;
        temp0 = temp14 * temp5 - temp0;
        p2Y = data.Ay + temp1 * temp0;

        //Finish GA-FuL MetaContext Code Generation, 2024-03-18T02:35:35.9583096+02:00
    }

    public static void SolveUsingTrigOpt(HansenProblemData2D data, out double p1X, out double p1Y, out double p2X, out double p2Y)
    {
        var alpha1Sin = Math.Sin(data.Alpha1);
        var alpha2Sin = Math.Sin(data.Alpha2);
        var beta1Sin = Math.Sin(data.Beta1);
        var beta2Sin = Math.Sin(data.Beta2);

        var alpha1Cos = Math.Sqrt(1 - alpha1Sin * alpha1Sin);
        var alpha2Cos = Math.Sqrt(1 - alpha2Sin * alpha2Sin);
        var beta1Cos = Math.Sqrt(1 - beta1Sin * beta1Sin);
        var beta2Cos = Math.Sqrt(1 - beta2Sin * beta2Sin);

        //alpha1Cos = ((int)Math.Floor(2 * data.Alpha1 / Math.PI) % 4 + 4) % 4 is 1 or 2 ? -alpha1Cos : alpha1Cos;
        //alpha2Cos = ((int)Math.Floor(2 * data.Alpha2 / Math.PI) % 4 + 4) % 4 is 1 or 2 ? -alpha2Cos : alpha2Cos;
        //beta1Cos = ((int)Math.Floor(2 * data.Beta1 / Math.PI) % 4 + 4) % 4 is 1 or 2 ? -beta1Cos : beta1Cos;
        //beta2Cos = ((int)Math.Floor(2 * data.Beta2 / Math.PI) % 4 + 4) % 4 is 1 or 2 ? -beta2Cos : beta2Cos;

        if (((int)Math.Floor(2 * data.Alpha1 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha1Cos = -alpha1Cos;
        if (((int)Math.Floor(2 * data.Alpha2 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha2Cos = -alpha2Cos;
        if (((int)Math.Floor(2 * data.Beta1 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta1Cos = -beta1Cos;
        if (((int)Math.Floor(2 * data.Beta2 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta2Cos = -beta2Cos;

        //Begin GA-FuL MetaContext Code Generation, 2024-03-18T02:35:35.8687062+02:00
        //MetaContext: Hansen-Trig
        //Input Variables: 12 used, 0 not used, 12 total.
        //Temp Variables: 70 sub-expressions, 0 generated temps, 70 total.
        //Target Temp Variables: 16 total.
        //Output Variables: 4 total.
        //Computations: 1.662162162162162 average, 123 total.
        //Memory Reads: 2.2972972972972974 average, 170 total.
        //Memory Writes: 74 total.
        //
        //MetaContext Binding Data:
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   Rational[1, 2] = constant: 'Rational[1, 2]'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   Ax = parameter: data.Ax
        //   Ay = parameter: data.Ay
        //   Bx = parameter: data.Bx
        //   By = parameter: data.By
        //   alpha1Cos = parameter: alpha1Cos
        //   alpha1Sin = parameter: alpha1Sin
        //   alpha2Cos = parameter: alpha2Cos
        //   alpha2Sin = parameter: alpha2Sin
        //   beta1Cos = parameter: beta1Cos
        //   beta1Sin = parameter: beta1Sin
        //   beta2Cos = parameter: beta2Cos
        //   beta2Sin = parameter: beta2Sin

        var tmpVar153345 = alpha1Sin * beta1Cos;
        var tmpVar153347 = alpha1Cos * beta1Sin + -tmpVar153345;
        var tmpVar153349 = alpha1Cos * beta1Cos;
        var tmpVar153351 = alpha1Sin * beta1Sin;
        var tmpVar153353 = -tmpVar153349 + -tmpVar153351;
        var tmpVar153355 = tmpVar153349 + tmpVar153351;
        var tmpVar153357 = alpha1Cos * tmpVar153347 + alpha1Sin * tmpVar153355;
        var tmpVar153361 = -1 * alpha1Sin * tmpVar153347;
        var tmpVar153362 = alpha1Cos * tmpVar153355 + tmpVar153361;
        var tmpVar153364 = alpha2Cos * tmpVar153357 + alpha2Sin * tmpVar153362;
        var tmpVar153367 = alpha2Cos * beta2Cos + alpha2Sin * beta2Sin;
        var tmpVar153370 = alpha2Sin * beta2Cos;
        var tmpVar153372 = alpha2Cos * beta2Sin + -tmpVar153370;
        var tmpVar153374 = alpha2Sin * tmpVar153367 + alpha2Cos * tmpVar153372;
        var tmpVar153375 = tmpVar153364 * tmpVar153374;
        var tmpVar153377 = alpha2Sin * 1 / tmpVar153375;
        var tmpVar153380 = alpha2Sin * tmpVar153372;
        var tmpVar153382 = alpha2Cos * tmpVar153367 + -tmpVar153380;
        var tmpVar153384 = alpha1Cos * tmpVar153374 + alpha1Sin * tmpVar153382;
        var tmpVar153385 = tmpVar153377 * tmpVar153384;
        var tmpVar153392 = 1 + tmpVar153349 + tmpVar153351;
        var tmpVar153394 = Math.Sqrt(1 + tmpVar153353) * 1 / Math.Sqrt(tmpVar153392);
        var tmpVar153395 = (1 + tmpVar153385) * tmpVar153394;
        var tmpVar153397 = (1 + -tmpVar153385) * 1 / tmpVar153395;
        var tmpVar153398 = tmpVar153397 * tmpVar153397;
        var tmpVar153400 = 1 / (1 + tmpVar153398);
        var tmpVar153402 = tmpVar153400 * Math.Sign(tmpVar153397);
        var tmpVar153406 = tmpVar153353 * (-1 + 2 * tmpVar153402 * tmpVar153402);
        var tmpVar153408 = 1 + -tmpVar153406;
        var tmpVar153411 = 2 * tmpVar153347 * tmpVar153398;
        var tmpVar153412 = tmpVar153400 * tmpVar153411;
        var tmpVar153413 = -1 * tmpVar153402 * tmpVar153412;
        var tmpVar153414 = tmpVar153408 + tmpVar153413;
        var tmpVar153416 = 1 / tmpVar153347 * Math.Sqrt(tmpVar153414);
        var tmpVar153417 = 1 / Math.Sqrt(2);
        var tmpVar153418 = tmpVar153416 * tmpVar153417;
        var tmpVar153420 = -data.Ax + data.Bx;
        var tmpVar153422 = 1 + tmpVar153406 + tmpVar153413;
        var tmpVar153424 = tmpVar153417 * Math.Sqrt(tmpVar153422);
        var tmpVar153427 = tmpVar153408 + tmpVar153402 * tmpVar153412;
        var tmpVar153428 = Math.Sqrt(tmpVar153427);
        var tmpVar153429 = tmpVar153417 * tmpVar153428;
        var tmpVar153431 = -data.Ay + data.By;
        var tmpVar153432 = tmpVar153429 * tmpVar153431;
        var tmpVar153434 = tmpVar153420 * tmpVar153424 + -tmpVar153432;
        p1X = data.Ax + tmpVar153418 * tmpVar153434;

        var tmpVar153438 = tmpVar153420 * tmpVar153429 + tmpVar153424 * tmpVar153431;
        p1Y = data.Ay + tmpVar153418 * tmpVar153438;

        var tmpVar153440 = tmpVar153364 * tmpVar153424;
        var tmpVar153443 = -1 * tmpVar153417 * tmpVar153428;
        var tmpVar153444 = alpha2Cos * tmpVar153362;
        var tmpVar153446 = alpha2Sin * tmpVar153357;
        var tmpVar153447 = -tmpVar153444 + tmpVar153446;
        var tmpVar153449 = -tmpVar153440 + tmpVar153443 * tmpVar153447;
        var tmpVar153453 = tmpVar153364 * tmpVar153443 + tmpVar153424 * tmpVar153447;
        var tmpVar153455 = tmpVar153431 * tmpVar153449 + tmpVar153420 * tmpVar153453;
        var tmpVar153460 = tmpVar153444 + -tmpVar153446;
        var tmpVar153462 = tmpVar153364 * tmpVar153367 + tmpVar153372 * tmpVar153460;
        var tmpVar153463 = -1 * tmpVar153424 * tmpVar153462;
        var tmpVar153465 = tmpVar153364 * tmpVar153372;
        var tmpVar153467 = tmpVar153367 * tmpVar153460 + -tmpVar153465;
        var tmpVar153469 = tmpVar153463 + tmpVar153429 * tmpVar153467;
        var tmpVar153470 = 1 / tmpVar153372 * tmpVar153469;
        p2X = data.Ax + tmpVar153455 * tmpVar153470;

        var tmpVar153473 = tmpVar153420 * tmpVar153449;
        var tmpVar153475 = tmpVar153431 * tmpVar153453 + -tmpVar153473;
        p2Y = data.Ay + tmpVar153470 * tmpVar153475;

        //Finish GA-FuL MetaContext Code Generation, 2024-03-18T02:35:35.9583096+02:00
    }

    public static void SolveUsingComplex(HansenProblemData2D data, out double p1X, out double p1Y, out double p2X, out double p2Y)
    {
        var alpha1Sin = Math.Sin(data.Alpha1);
        var alpha2Sin = Math.Sin(data.Alpha2);
        var beta1Sin = Math.Sin(data.Beta1);
        var beta2Sin = Math.Sin(data.Beta2);

        var alpha1Cos = Math.Sqrt(1 - alpha1Sin * alpha1Sin);
        var alpha2Cos = Math.Sqrt(1 - alpha2Sin * alpha2Sin);
        var beta1Cos = Math.Sqrt(1 - beta1Sin * beta1Sin);
        var beta2Cos = Math.Sqrt(1 - beta2Sin * beta2Sin);

        if (((int)Math.Floor(2 * data.Alpha1 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha1Cos = -alpha1Cos;
        if (((int)Math.Floor(2 * data.Alpha2 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha2Cos = -alpha2Cos;
        if (((int)Math.Floor(2 * data.Beta1 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta1Cos = -beta1Cos;
        if (((int)Math.Floor(2 * data.Beta2 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta2Cos = -beta2Cos;

        //Begin GA-FuL MetaContext Code Generation, 2024-03-16T23:45:49.2405082+02:00
        //MetaContext: Hansen-Complex
        //Input Variables: 12 used, 0 not used, 12 total.
        //Temp Variables: 73 sub-expressions, 0 generated temps, 73 total.
        //Target Temp Variables: 13 total.
        //Output Variables: 4 total.
        //Computations: 1.4025974025974026 average, 108 total.
        //Memory Reads: 2.103896103896104 average, 162 total.
        //Memory Writes: 77 total.
        //
        //MetaContext Binding Data:
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   -2 = constant: '-2'
        //   Ax = parameter: data.Ax
        //   Ay = parameter: data.Ay
        //   Bx = parameter: data.Bx
        //   By = parameter: data.By
        //   alpha1Cos = parameter: alpha1Cos
        //   alpha1Sin = parameter: alpha1Sin
        //   alpha2Cos = parameter: alpha2Cos
        //   alpha2Sin = parameter: alpha2Sin
        //   beta1Cos = parameter: beta1Cos
        //   beta1Sin = parameter: beta1Sin
        //   beta2Cos = parameter: beta2Cos
        //   beta2Sin = parameter: beta2Sin

        var temp0 = -beta2Sin;
        var temp1 = 2 * temp0 * temp0;
        var temp2 = -2 * alpha1Sin * alpha1Sin + temp1;
        var temp3 = alpha1Cos * alpha1Sin;
        temp0 = beta2Cos * temp0;
        temp0 = 2 * temp0;
        temp3 = -2 * temp3 + temp0;
        var temp4 = temp2 * temp2 + temp3 * temp3;
        temp4 = 1 / temp4;
        var temp5 = temp1 * temp2 + temp0 * temp3;
        temp5 = temp4 * temp5;
        var temp6 = -alpha2Sin;
        var temp7 = temp6 * temp6;
        var temp8 = temp7 - beta1Sin * beta1Sin;
        temp8 = 2 * temp8;
        temp6 = alpha2Cos * temp6;
        var temp9 = beta1Cos * beta1Sin;
        temp9 = temp6 - temp9;
        temp9 = 2 * temp9;
        var temp10 = temp8 * temp8 + temp9 * temp9;
        temp10 = 1 / temp10;
        temp7 = 2 * temp7;
        temp6 = 2 * temp6;
        var temp11 = temp8 * temp7 + temp9 * temp6;
        temp11 = temp10 * temp11;
        var temp12 = 1 - temp11;
        temp1 *= temp3;
        temp0 = temp2 * temp0 - temp1;
        temp0 = temp4 * temp0;
        temp1 = temp9 * temp7;
        temp1 = temp8 * temp6 - temp1;
        temp1 = temp10 * temp1;
        temp2 = -temp1;
        temp3 = temp0 * temp2;
        temp3 = temp5 * temp12 - temp3;
        temp4 = 1 - temp5;
        temp6 = temp11 * temp4;
        temp3 -= temp6;
        temp6 = -temp0;
        temp3 += temp1 * temp6;
        temp7 = temp11 * temp6;
        temp8 = temp1 * temp4;
        temp7 = -(temp7 + temp8);
        temp7 = temp12 * temp0 + temp7;
        temp7 = temp5 * temp2 + temp7;
        temp8 = temp3 * temp3 + temp7 * temp7;
        temp8 = 1 / temp8;
        temp9 = data.Ay * temp0;
        temp9 = data.Ax * temp5 - temp9;
        temp10 = data.Bx * temp11;
        temp9 -= temp10;
        temp9 = data.By * temp1 + temp9;
        temp0 = data.Ay * temp5 + data.Ax * temp0;
        temp5 = data.By * temp11;
        temp1 = data.Bx * temp1;
        temp1 = -(temp5 + temp1);
        temp0 += temp1;
        temp1 = temp3 * temp9 + temp7 * temp0;
        p1X = temp8 * temp1;

        temp1 = temp7 * temp9;
        temp0 = temp3 * temp0 - temp1;
        p1Y = temp8 * temp0;

        temp0 = data.By * temp2;
        temp0 = data.Bx * temp12 - temp0;
        temp1 = data.Ax * temp4;
        temp0 -= temp1;
        temp0 = data.Ay * temp6 + temp0;
        temp1 = data.By * temp12 + data.Bx * temp2;
        temp2 = data.Ax * temp6;
        temp4 = data.Ay * temp4;
        temp2 = -(temp2 + temp4);
        temp1 += temp2;
        temp2 = temp3 * temp0 + temp7 * temp1;
        p2X = temp8 * temp2;

        temp0 = temp7 * temp0;
        temp0 = temp3 * temp1 - temp0;
        p2Y = temp8 * temp0;

        //Finish GA-FuL MetaContext Code Generation, 2024-03-16T23:45:49.2410952+02:00
    }

    public static void SolveUsingComplexOpt(HansenProblemData2D data, out double p1X, out double p1Y, out double p2X, out double p2Y)
    {
        var alpha1Sin = Math.Sin(data.Alpha1);
        var alpha2Sin = Math.Sin(data.Alpha2);
        var beta1Sin = Math.Sin(data.Beta1);
        var beta2Sin = Math.Sin(data.Beta2);

        var alpha1Cos = Math.Sqrt(1 - alpha1Sin * alpha1Sin);
        var alpha2Cos = Math.Sqrt(1 - alpha2Sin * alpha2Sin);
        var beta1Cos = Math.Sqrt(1 - beta1Sin * beta1Sin);
        var beta2Cos = Math.Sqrt(1 - beta2Sin * beta2Sin);

        if (((int)Math.Floor(2 * data.Alpha1 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha1Cos = -alpha1Cos;
        if (((int)Math.Floor(2 * data.Alpha2 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha2Cos = -alpha2Cos;
        if (((int)Math.Floor(2 * data.Beta1 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta1Cos = -beta1Cos;
        if (((int)Math.Floor(2 * data.Beta2 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta2Cos = -beta2Cos;

        //Begin GA-FuL MetaContext Code Generation, 2024-03-16T23:45:49.2405082+02:00
        //MetaContext: Hansen-Complex
        //Input Variables: 12 used, 0 not used, 12 total.
        //Temp Variables: 73 sub-expressions, 0 generated temps, 73 total.
        //Target Temp Variables: 13 total.
        //Output Variables: 4 total.
        //Computations: 1.4025974025974026 average, 108 total.
        //Memory Reads: 2.103896103896104 average, 162 total.
        //Memory Writes: 77 total.
        //
        //MetaContext Binding Data:
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   -2 = constant: '-2'
        //   Ax = parameter: data.Ax
        //   Ay = parameter: data.Ay
        //   Bx = parameter: data.Bx
        //   By = parameter: data.By
        //   alpha1Cos = parameter: alpha1Cos
        //   alpha1Sin = parameter: alpha1Sin
        //   alpha2Cos = parameter: alpha2Cos
        //   alpha2Sin = parameter: alpha2Sin
        //   beta1Cos = parameter: beta1Cos
        //   beta1Sin = parameter: beta1Sin
        //   beta2Cos = parameter: beta2Cos
        //   beta2Sin = parameter: beta2Sin

        var tmpVar205544 = -1 * alpha1Cos * alpha1Sin;
        var tmpVar205546 = -1 * beta2Cos * beta2Sin;
        var tmpVar205547 = tmpVar205544 + tmpVar205546;
        var tmpVar205548 = 2 * tmpVar205547;
        var tmpVar205551 = beta2Cos * beta2Cos;
        var tmpVar205553 = alpha1Cos * alpha1Cos + -tmpVar205551;
        var tmpVar205554 = 2 * tmpVar205553;
        var tmpVar205556 = tmpVar205548 * tmpVar205548 + tmpVar205554 * tmpVar205554;
        var tmpVar205557 = 1 / tmpVar205556;
        var tmpVar205558 = 2 * tmpVar205546;
        var tmpVar205561 = 2 + -2 * tmpVar205551;
        var tmpVar205563 = tmpVar205548 * tmpVar205558 + tmpVar205554 * tmpVar205561;
        var tmpVar205564 = tmpVar205557 * tmpVar205563;
        var tmpVar205566 = alpha2Cos * alpha2Cos;
        var tmpVar205568 = beta1Cos * beta1Cos + -tmpVar205566;
        var tmpVar205569 = 2 * tmpVar205568;
        var tmpVar205572 = -1 * beta1Cos * beta1Sin;
        var tmpVar205574 = -1 * alpha2Cos * alpha2Sin;
        var tmpVar205575 = tmpVar205572 + tmpVar205574;
        var tmpVar205576 = 2 * tmpVar205575;
        var tmpVar205578 = tmpVar205569 * tmpVar205569 + tmpVar205576 * tmpVar205576;
        var tmpVar205579 = 1 / tmpVar205578;
        var tmpVar205581 = 2 + -2 * tmpVar205566;
        var tmpVar205583 = 2 * tmpVar205574;
        var tmpVar205585 = tmpVar205569 * tmpVar205581 + tmpVar205576 * tmpVar205583;
        var tmpVar205586 = tmpVar205579 * tmpVar205585;
        var tmpVar205587 = -tmpVar205586;
        var tmpVar205588 = 1 + tmpVar205587;
        var tmpVar205591 = 1 + -tmpVar205564;
        var tmpVar205593 = tmpVar205564 * tmpVar205588 + tmpVar205587 * tmpVar205591;
        var tmpVar205596 = tmpVar205548 * tmpVar205561;
        var tmpVar205598 = tmpVar205554 * tmpVar205558 + -tmpVar205596;
        var tmpVar205599 = tmpVar205557 * tmpVar205598;
        var tmpVar205601 = tmpVar205576 * tmpVar205581;
        var tmpVar205603 = tmpVar205569 * tmpVar205583 + -tmpVar205601;
        var tmpVar205604 = tmpVar205579 * tmpVar205603;
        var tmpVar205606 = tmpVar205599 + -tmpVar205604;
        var tmpVar205608 = tmpVar205593 * tmpVar205593 + tmpVar205606 * tmpVar205606;
        var tmpVar205609 = 1 / tmpVar205608;
        var tmpVar205611 = data.By * tmpVar205604;
        var tmpVar205612 = data.Ax * tmpVar205564 + tmpVar205611;
        var tmpVar205613 = -data.Bx;
        var tmpVar205615 = tmpVar205612 + tmpVar205586 * tmpVar205613;
        var tmpVar205616 = data.Ay * tmpVar205599;
        var tmpVar205618 = tmpVar205615 + -tmpVar205616;
        var tmpVar205622 = -1 * data.By * tmpVar205586;
        var tmpVar205623 = data.Ay * tmpVar205564 + tmpVar205622;
        var tmpVar205624 = data.Ax * tmpVar205599;
        var tmpVar205625 = tmpVar205623 + tmpVar205624;
        var tmpVar205626 = data.Bx * tmpVar205604;
        var tmpVar205628 = tmpVar205625 + -tmpVar205626;
        var tmpVar205630 = tmpVar205593 * tmpVar205618 + tmpVar205606 * tmpVar205628;
        p1X = tmpVar205609 * tmpVar205630;

        var tmpVar205632 = tmpVar205606 * tmpVar205618;
        var tmpVar205634 = tmpVar205593 * tmpVar205628 + -tmpVar205632;
        p1Y = tmpVar205609 * tmpVar205634;

        var tmpVar205636 = data.Bx * tmpVar205588 + tmpVar205611;
        var tmpVar205638 = -1 * data.Ay * tmpVar205599;
        var tmpVar205639 = tmpVar205636 + tmpVar205638;
        var tmpVar205640 = data.Ax * tmpVar205591;
        var tmpVar205642 = tmpVar205639 + -tmpVar205640;
        var tmpVar205645 = tmpVar205604 * tmpVar205613 + tmpVar205624;
        var tmpVar205647 = data.By * tmpVar205588 + tmpVar205645;
        var tmpVar205648 = data.Ay * tmpVar205591;
        var tmpVar205650 = tmpVar205647 + -tmpVar205648;
        var tmpVar205652 = tmpVar205593 * tmpVar205642 + tmpVar205606 * tmpVar205650;
        p2X = tmpVar205609 * tmpVar205652;

        var tmpVar205654 = tmpVar205606 * tmpVar205642;
        var tmpVar205656 = tmpVar205593 * tmpVar205650 + -tmpVar205654;
        p2Y = tmpVar205609 * tmpVar205656;

        //Finish GA-FuL MetaContext Code Generation, 2024-03-16T23:45:49.2410952+02:00
    }

    public static void SolveUsingVGa(HansenProblemData2D data, out double p1X, out double p1Y, out double p2X, out double p2Y)
    {
        var alpha1Sin = Math.Sin(data.Alpha1);
        var alpha2Sin = Math.Sin(data.Alpha2);
        var beta1Sin = Math.Sin(data.Beta1);
        var beta2Sin = Math.Sin(data.Beta2);

        var alpha1Cos = Math.Sqrt(1 - alpha1Sin * alpha1Sin);
        var alpha2Cos = Math.Sqrt(1 - alpha2Sin * alpha2Sin);
        var beta1Cos = Math.Sqrt(1 - beta1Sin * beta1Sin);
        var beta2Cos = Math.Sqrt(1 - beta2Sin * beta2Sin);

        if (((int)Math.Floor(2 * data.Alpha1 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha1Cos = -alpha1Cos;
        if (((int)Math.Floor(2 * data.Alpha2 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha2Cos = -alpha2Cos;
        if (((int)Math.Floor(2 * data.Beta1 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta1Cos = -beta1Cos;
        if (((int)Math.Floor(2 * data.Beta2 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta2Cos = -beta2Cos;

        //Begin GA-FuL MetaContext Code Generation, 2024-03-16T23:46:00.5213033+02:00
        //MetaContext: Hansen-VGA
        //Input Variables: 12 used, 0 not used, 12 total.
        //Temp Variables: 36 sub-expressions, 0 generated temps, 36 total.
        //Target Temp Variables: 10 total.
        //Output Variables: 4 total.
        //Computations: 1.625 average, 65 total.
        //Memory Reads: 2.475 average, 99 total.
        //Memory Writes: 40 total.
        //
        //MetaContext Binding Data:
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   1 = constant: '1'
        //   Ax = parameter: data.Ax
        //   Ay = parameter: data.Ay
        //   Bx = parameter: data.Bx
        //   By = parameter: data.By
        //   alpha1Cos = parameter: alpha1Cos
        //   alpha1Sin = parameter: alpha1Sin
        //   alpha2Cos = parameter: alpha2Cos
        //   alpha2Sin = parameter: alpha2Sin
        //   beta1Cos = parameter: beta1Cos
        //   beta1Sin = parameter: beta1Sin
        //   beta2Cos = parameter: beta2Cos
        //   beta2Sin = parameter: beta2Sin

        var temp0 = alpha2Sin * beta1Cos;
        var temp1 = alpha2Cos * beta1Sin;
        var temp2 = temp0 + temp1;
        temp2 = 1 / temp2;
        var temp3 = alpha2Sin * temp2;
        temp2 = beta1Sin * temp2;
        var temp4 = alpha1Sin * beta2Cos + alpha1Cos * beta2Sin;
        temp4 = alpha1Sin * 1 / temp4;
        var temp5 = temp2 * temp2 + temp4 * temp4;
        var temp6 = 2 * temp2 * temp4;
        var temp7 = alpha2Cos * beta2Cos + alpha2Sin * beta2Sin;
        temp6 *= temp7;
        temp5 -= temp6;
        temp5 = 1 / Math.Sqrt(temp5);
        temp3 *= temp5;
        temp6 = data.Bx - data.Ax;
        var temp8 = alpha2Cos * beta2Sin;
        temp8 = alpha2Sin * beta2Cos - temp8;
        var temp9 = temp4 * temp5;
        temp8 *= temp9;
        temp9 = alpha2Cos * beta1Cos;
        temp9 = alpha2Sin * beta1Sin - temp9;
        temp4 *= temp7;
        temp4 = temp2 - temp4;
        temp4 = temp5 * temp4;
        temp0 = -(temp0 + temp1);
        temp1 = temp8 * temp9 + temp4 * temp0;
        temp0 = temp8 * temp0;
        temp0 = temp9 * temp4 - temp0;
        temp7 = data.By - data.Ay;
        temp9 = temp6 * temp1 + temp0 * temp7;
        p1Y = data.Ay + temp3 * temp9;

        temp9 = data.Ay - data.By;
        temp0 = temp6 * temp0 + temp1 * temp9;
        p1X = data.Ax + temp3 * temp0;

        temp0 = temp2 * temp5;
        temp1 = temp6 * temp8 + temp4 * temp7;
        p2Y = data.Ay + temp0 * temp1;

        temp1 = temp6 * temp4 + temp8 * temp9;
        p2X = data.Ax + temp0 * temp1;

        //Finish GA-FuL MetaContext Code Generation, 2024-03-16T23:46:00.5216721+02:00
    }

    public static void SolveUsingVGaOpt(HansenProblemData2D data, out double p1X, out double p1Y, out double p2X, out double p2Y)
    {
        var alpha1Sin = Math.Sin(data.Alpha1);
        var alpha2Sin = Math.Sin(data.Alpha2);
        var beta1Sin = Math.Sin(data.Beta1);
        var beta2Sin = Math.Sin(data.Beta2);

        var alpha1Cos = Math.Sqrt(1 - alpha1Sin * alpha1Sin);
        var alpha2Cos = Math.Sqrt(1 - alpha2Sin * alpha2Sin);
        var beta1Cos = Math.Sqrt(1 - beta1Sin * beta1Sin);
        var beta2Cos = Math.Sqrt(1 - beta2Sin * beta2Sin);

        if (((int)Math.Floor(2 * data.Alpha1 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha1Cos = -alpha1Cos;
        if (((int)Math.Floor(2 * data.Alpha2 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha2Cos = -alpha2Cos;
        if (((int)Math.Floor(2 * data.Beta1 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta1Cos = -beta1Cos;
        if (((int)Math.Floor(2 * data.Beta2 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta2Cos = -beta2Cos;

        //Begin GA-FuL MetaContext Code Generation, 2024-03-16T23:46:00.5213033+02:00
        //MetaContext: Hansen-VGA
        //Input Variables: 12 used, 0 not used, 12 total.
        //Temp Variables: 36 sub-expressions, 0 generated temps, 36 total.
        //Target Temp Variables: 10 total.
        //Output Variables: 4 total.
        //Computations: 1.625 average, 65 total.
        //Memory Reads: 2.475 average, 99 total.
        //Memory Writes: 40 total.
        //
        //MetaContext Binding Data:
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   1 = constant: '1'
        //   Ax = parameter: data.Ax
        //   Ay = parameter: data.Ay
        //   Bx = parameter: data.Bx
        //   By = parameter: data.By
        //   alpha1Cos = parameter: alpha1Cos
        //   alpha1Sin = parameter: alpha1Sin
        //   alpha2Cos = parameter: alpha2Cos
        //   alpha2Sin = parameter: alpha2Sin
        //   beta1Cos = parameter: beta1Cos
        //   beta1Sin = parameter: beta1Sin
        //   beta2Cos = parameter: beta2Cos
        //   beta2Sin = parameter: beta2Sin

        var tmpVar114126 = alpha2Cos * beta1Sin;
        var tmpVar114127 = alpha2Sin * beta1Cos;
        var tmpVar114128 = tmpVar114126 + tmpVar114127;
        var tmpVar114129 = 1 / tmpVar114128;
        var tmpVar114130 = alpha2Sin * tmpVar114129;
        var tmpVar114133 = alpha1Sin * beta2Cos + alpha1Cos * beta2Sin;
        var tmpVar114135 = alpha1Sin * 1 / tmpVar114133;
        var tmpVar114137 = beta1Sin * tmpVar114129;
        var tmpVar114141 = alpha2Cos * beta2Cos + alpha2Sin * beta2Sin;
        var tmpVar114142 = -2 * tmpVar114135 * tmpVar114141;
        var tmpVar114143 = tmpVar114137 + tmpVar114142;
        var tmpVar114145 = tmpVar114135 * tmpVar114135 + tmpVar114137 * tmpVar114143;
        var tmpVar114146 = 1 / Math.Sqrt(tmpVar114145);
        var tmpVar114147 = tmpVar114130 * tmpVar114146;
        var tmpVar114149 = -data.Ax + data.Bx;
        var tmpVar114152 = -1 * alpha2Cos * beta2Sin;
        var tmpVar114153 = alpha2Sin * beta2Cos + tmpVar114152;
        var tmpVar114154 = tmpVar114135 * tmpVar114146;
        var tmpVar114155 = tmpVar114153 * tmpVar114154;
        var tmpVar114157 = alpha2Cos * beta1Cos;
        var tmpVar114159 = alpha2Sin * beta1Sin + -tmpVar114157;
        var tmpVar114163 = -tmpVar114126 + -tmpVar114127;
        var tmpVar114165 = -1 * tmpVar114135 * tmpVar114141;
        var tmpVar114166 = tmpVar114137 + tmpVar114165;
        var tmpVar114167 = tmpVar114146 * tmpVar114166;
        var tmpVar114169 = tmpVar114155 * tmpVar114159 + tmpVar114163 * tmpVar114167;
        var tmpVar114172 = -data.Ay + data.By;
        var tmpVar114174 = tmpVar114155 * tmpVar114163;
        var tmpVar114176 = tmpVar114159 * tmpVar114167 + -tmpVar114174;
        var tmpVar114178 = tmpVar114149 * tmpVar114169 + tmpVar114172 * tmpVar114176;
        p1Y = data.Ay + tmpVar114147 * tmpVar114178;

        var tmpVar114181 = tmpVar114169 * tmpVar114172;
        var tmpVar114183 = tmpVar114149 * tmpVar114176 + -tmpVar114181;
        p1X = data.Ax + tmpVar114147 * tmpVar114183;

        var tmpVar114185 = tmpVar114137 * tmpVar114146;
        var tmpVar114188 = tmpVar114149 * tmpVar114155 + tmpVar114167 * tmpVar114172;
        p2Y = data.Ay + tmpVar114185 * tmpVar114188;

        var tmpVar114191 = tmpVar114155 * tmpVar114172;
        var tmpVar114193 = tmpVar114149 * tmpVar114167 + -tmpVar114191;
        p2X = data.Ax + tmpVar114185 * tmpVar114193;

        //Finish GA-FuL MetaContext Code Generation, 2024-03-16T23:46:00.5216721+02:00
    }

    public static void SolveUsingCGa(HansenProblemData2D data, out double p1X, out double p1Y, out double p2X, out double p2Y)
    {
        var alpha1Sin = Math.Sin(data.Alpha1);
        var alpha2Sin = Math.Sin(data.Alpha2);
        var beta1Sin = Math.Sin(data.Beta1);
        var beta2Sin = Math.Sin(data.Beta2);

        var alpha1Cos = Math.Sqrt(1 - alpha1Sin * alpha1Sin);
        var alpha2Cos = Math.Sqrt(1 - alpha2Sin * alpha2Sin);
        var beta1Cos = Math.Sqrt(1 - beta1Sin * beta1Sin);
        var beta2Cos = Math.Sqrt(1 - beta2Sin * beta2Sin);

        if (((int)Math.Floor(2 * data.Alpha1 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha1Cos = -alpha1Cos;
        if (((int)Math.Floor(2 * data.Alpha2 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha2Cos = -alpha2Cos;
        if (((int)Math.Floor(2 * data.Beta1 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta1Cos = -beta1Cos;
        if (((int)Math.Floor(2 * data.Beta2 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta2Cos = -beta2Cos;

        var halfAlpha1Cos = 1 / Math.Sqrt(2) * Math.Sqrt(1 + alpha1Cos);
        var halfAlpha1Sin = 1 / Math.Sqrt(2) * Math.Sqrt(1 - alpha1Cos);
        if (alpha1Sin > 0) halfAlpha1Cos = -halfAlpha1Cos;

        var halfBeta1Cos = 1 / Math.Sqrt(2) * Math.Sqrt(1 + beta1Cos);
        var halfBeta1Sin = 1 / Math.Sqrt(2) * Math.Sqrt(1 - beta1Cos);
        if (beta1Sin > 0) halfBeta1Cos = -halfBeta1Cos;

        var halfAlpha2Cos = 1 / Math.Sqrt(2) * Math.Sqrt(1 + alpha2Cos);
        var halfAlpha2Sin = 1 / Math.Sqrt(2) * Math.Sqrt(1 - alpha2Cos);
        if (alpha2Sin < 0) halfAlpha2Cos = -halfAlpha2Cos;

        var halfBeta2Cos = 1 / Math.Sqrt(2) * Math.Sqrt(1 + beta2Cos);
        var halfBeta2Sin = 1 / Math.Sqrt(2) * Math.Sqrt(1 - beta2Cos);
        if (beta2Sin < 0) halfBeta2Cos = -halfBeta2Cos;

        //Begin GA-FuL MetaContext Code Generation, 2024-04-19T01:05:17.3369930+02:00
        //MetaContext: CGA
        //Input Variables: 8 used, 4 not used, 12 total.
        //Temp Variables: 703 sub-expressions, 0 generated temps, 703 total.
        //Target Temp Variables: 61 total.
        //Output Variables: 12 total.
        //Computations: 1.393006993006993 average, 996 total.
        //Memory Reads: 2.2097902097902096 average, 1580 total.
        //Memory Writes: 715 total.
        //
        //MetaContext Binding Data:
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   0.5 = constant: '0.5'
        //   Rational[1, 2] = constant: 'Rational[1, 2]'
        //   -0.5 = constant: '-0.5'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   Ax = parameter: data.Ax
        //   Ay = parameter: data.Ay
        //   Bx = parameter: data.Bx
        //   By = parameter: data.By
        //   alpha1Cos = parameter: alpha1Cos
        //   alpha2Cos = parameter: alpha2Cos
        //   beta1Cos = parameter: beta1Cos
        //   beta2Cos = parameter: beta2Cos
        //   alpha1Sin = parameter: alpha1Sin
        //   alpha2Sin = parameter: alpha2Sin
        //   beta1Sin = parameter: beta1Sin
        //   beta2Sin = parameter: beta2Sin

        var temp0 = -data.Ax;
        var temp1 = -0.5 * data.Ax;
        var temp2 = -0.5 * data.Ay;
        var temp3 = 0.5 * data.Ay;
        var temp4 = -1 + -(temp1 * temp1) + temp2 * temp3;
        var temp5 = 0.5 * data.Ax;
        var temp6 = temp4 + temp5 * temp5;
        var temp7 = temp2 * temp3;
        var temp8 = temp6 - temp7;
        var temp9 = data.Ax * temp0 + temp8 * temp8;
        var temp10 = temp1 * temp3;
        var temp11 = temp3 * temp5;
        var temp12 = -(temp10 + temp11);
        var temp13 = temp2 * temp5;
        var temp14 = temp12 + temp13;
        var temp15 = temp1 * temp2;
        var temp16 = temp14 + temp15;
        var temp17 = -(temp12 + temp13);
        var temp18 = temp17 - temp15;
        var temp19 = temp9 + temp16 * temp18;
        var temp20 = -data.Ay;
        var temp21 = temp19 + data.Ay * temp20;
        var temp22 = data.Ax * data.Ax;
        var temp23 = temp21 + temp22;
        var temp24 = temp20 * temp20 + temp23;
        var temp25 = 1 / Math.Sqrt(Math.Abs(temp24));
        var temp26 = halfAlpha1Sin * temp25;
        var temp27 = data.Ay * temp26;
        var temp28 = -temp27;
        var temp29 = temp0 * temp26;
        var temp30 = -temp29;
        var temp31 = temp8 * temp26;
        var temp32 = temp29 * temp30 + temp31 * temp31;
        var temp33 = temp16 * temp26;
        var temp34 = -temp33;
        var temp35 = temp32 + temp33 * temp34;
        var temp36 = temp27 * temp28 + temp35;
        var temp37 = data.Ax * temp26;
        var temp38 = temp36 + temp37 * temp37;
        var temp39 = temp20 * temp26;
        var temp40 = temp38 + temp39 * temp39;
        var temp41 = halfAlpha1Cos * halfAlpha1Cos + temp40;
        var temp42 = 1 / temp41;
        var temp43 = temp28 * temp42;
        var temp44 = data.Bx + temp0;
        var temp45 = data.By + temp20;
        var temp46 = temp44 * temp44 + temp45 * temp45;
        var temp47 = 1 / Math.Sqrt(Math.Abs(temp46));
        var temp48 = temp44 * temp47;
        var temp49 = temp45 * temp47;
        var temp50 = -temp49;
        var temp51 = temp3 * temp50;
        var temp52 = temp5 * temp48 - temp51;
        var temp53 = temp48 + temp1 * temp52;
        var temp54 = temp2 * temp50;
        var temp55 = temp1 * temp48 - temp54;
        var temp56 = temp5 * temp55;
        var temp57 = temp53 - temp56;
        var temp58 = temp5 * temp50;
        var temp59 = temp3 * temp48;
        var temp60 = temp58 + temp59;
        var temp61 = temp2 * temp60;
        var temp62 = temp57 - temp61;
        var temp63 = temp1 * temp50;
        var temp64 = temp2 * temp48;
        var temp65 = temp63 + temp64;
        var temp66 = temp62 + temp3 * temp65;
        var temp67 = temp65 - temp59;
        var temp68 = temp67 - temp58;
        var temp69 = temp29 * temp66 + temp33 * temp68;
        var temp70 = temp50 + temp5 * temp65;
        var temp71 = temp2 * temp52;
        var temp72 = temp70 - temp71;
        var temp73 = temp3 * temp55 + temp72;
        var temp74 = temp1 * temp60;
        var temp75 = temp73 - temp74;
        var temp76 = temp69 + temp27 * temp75;
        var temp77 = temp60 - temp64;
        var temp78 = temp77 - temp63;
        var temp79 = temp76 + halfAlpha1Cos * temp78;
        var temp80 = temp43 * temp79;
        var temp81 = -temp31 * temp42;
        var temp82 = temp31 * temp75;
        var temp83 = temp29 * temp78 - temp82;
        var temp84 = temp37 * temp68;
        var temp85 = temp83 - temp84;
        var temp86 = halfAlpha1Cos * temp66 + temp85;
        var temp87 = temp81 * temp86;
        var temp88 = -(temp80 + temp87);
        var temp89 = halfAlpha1Cos * temp42;
        var temp90 = temp31 * temp66 + temp27 * temp78;
        var temp91 = temp39 * temp68;
        var temp92 = temp90 - temp91;
        var temp93 = halfAlpha1Cos * temp75 + temp92;
        var temp94 = temp88 + temp89 * temp93;
        var temp95 = -temp39 * temp42;
        var temp96 = temp37 * temp66 + temp33 * temp78;
        var temp97 = temp39 * temp75 + temp96;
        var temp98 = halfAlpha1Cos * temp68 + temp97;
        var temp99 = temp94 + temp95 * temp98;
        var temp100 = temp30 * temp42;
        var temp101 = temp29 * temp75;
        var temp102 = temp31 * temp78 - temp101;
        var temp103 = temp27 * temp66 + temp102;
        var temp104 = temp100 * temp103;
        var temp105 = temp99 - temp104;
        var temp106 = -temp37 * temp42;
        var temp107 = temp37 * temp75;
        var temp108 = temp31 * temp68 - temp107;
        var temp109 = temp39 * temp66 + temp108;
        var temp110 = temp105 + temp106 * temp109;
        var temp111 = temp34 * temp42;
        var temp112 = temp27 * temp68;
        var temp113 = temp33 * temp75 - temp112;
        var temp114 = temp39 * temp78 + temp113;
        var temp115 = temp110 + temp111 * temp114;
        var temp116 = -data.Bx;
        var temp117 = -0.5 * data.Bx;
        var temp118 = -0.5 * data.By;
        var temp119 = 0.5 * data.By;
        var temp120 = -1 + -(temp117 * temp117) + temp118 * temp119;
        var temp121 = 0.5 * data.Bx;
        var temp122 = temp120 + temp121 * temp121;
        var temp123 = temp118 * temp119;
        var temp124 = temp122 - temp123;
        var temp125 = data.Bx * temp116 + temp124 * temp124;
        var temp126 = temp117 * temp119;
        var temp127 = temp119 * temp121;
        var temp128 = -(temp126 + temp127);
        var temp129 = temp118 * temp121;
        var temp130 = temp128 + temp129;
        var temp131 = temp117 * temp118;
        var temp132 = temp130 + temp131;
        var temp133 = -(temp128 + temp129);
        var temp134 = temp133 - temp131;
        var temp135 = temp125 + temp132 * temp134;
        var temp136 = -data.By;
        var temp137 = temp135 + data.By * temp136;
        var temp138 = data.Bx * data.Bx;
        var temp139 = temp137 + temp138;
        var temp140 = temp136 * temp136 + temp139;
        var temp141 = 1 / Math.Sqrt(Math.Abs(temp140));
        var temp142 = temp141 * halfBeta1Sin;
        var temp143 = temp116 * temp142;
        var temp144 = -temp143;
        var temp145 = temp124 * temp142;
        var temp146 = temp143 * temp144 + temp145 * temp145;
        var temp147 = temp132 * temp142;
        var temp148 = -temp147;
        var temp149 = temp146 + temp147 * temp148;
        var temp150 = data.By * temp142;
        var temp151 = -temp150;
        var temp152 = temp149 + temp150 * temp151;
        var temp153 = data.Bx * temp142;
        var temp154 = temp152 + temp153 * temp153;
        var temp155 = temp136 * temp142;
        var temp156 = temp154 + temp155 * temp155;
        var temp157 = temp156 + halfBeta1Cos * halfBeta1Cos;
        var temp158 = 1 / temp157;
        var temp159 = temp144 * temp158;
        var temp160 = temp66 * temp143 + temp68 * temp147;
        var temp161 = temp75 * temp150 + temp160;
        var temp162 = temp161 + temp78 * halfBeta1Cos;
        var temp163 = temp159 * temp162;
        var temp164 = temp158 * halfBeta1Cos;
        var temp165 = temp75 * temp145;
        var temp166 = temp78 * temp143 - temp165;
        var temp167 = temp68 * temp153;
        var temp168 = temp166 - temp167;
        var temp169 = temp168 + temp66 * halfBeta1Cos;
        var temp170 = temp164 * temp169 - temp163;
        var temp171 = -temp145 * temp158;
        var temp172 = temp66 * temp145 + temp78 * temp150;
        var temp173 = temp68 * temp155;
        var temp174 = temp172 - temp173;
        var temp175 = temp174 + temp75 * halfBeta1Cos;
        var temp176 = temp170 + temp171 * temp175;
        var temp177 = -temp153 * temp158;
        var temp178 = temp78 * temp147 + temp66 * temp153;
        var temp179 = temp75 * temp155 + temp178;
        var temp180 = temp179 + temp68 * halfBeta1Cos;
        var temp181 = temp176 + temp177 * temp180;
        var temp182 = temp151 * temp158;
        var temp183 = temp75 * temp143;
        var temp184 = temp78 * temp145 - temp183;
        var temp185 = temp66 * temp150 + temp184;
        var temp186 = temp181 + temp182 * temp185;
        var temp187 = temp148 * temp158;
        var temp188 = temp68 * temp143;
        var temp189 = temp66 * temp147 - temp188;
        var temp190 = temp78 * temp153 + temp189;
        var temp191 = temp186 + temp187 * temp190;
        var temp192 = -temp155 * temp158;
        var temp193 = temp75 * temp153;
        var temp194 = temp68 * temp145 - temp193;
        var temp195 = temp66 * temp155 + temp194;
        var temp196 = temp192 * temp195;
        var temp197 = temp191 - temp196;
        var temp198 = temp115 * temp197;
        var temp199 = temp79 * temp100;
        var temp200 = temp86 * temp89 - temp199;
        var temp201 = temp81 * temp93 + temp200;
        var temp202 = temp98 * temp106 + temp201;
        var temp203 = temp43 * temp103 + temp202;
        var temp204 = temp29 * temp68;
        var temp205 = temp33 * temp66 - temp204;
        var temp206 = temp37 * temp78 + temp205;
        var temp207 = temp203 + temp111 * temp206;
        var temp208 = temp95 * temp109;
        var temp209 = temp207 - temp208;
        var temp210 = temp162 * temp182;
        var temp211 = temp169 * temp171;
        var temp212 = -(temp210 + temp211);
        var temp213 = temp164 * temp175 + temp212;
        var temp214 = temp180 * temp192 + temp213;
        var temp215 = temp159 * temp185;
        var temp216 = temp214 - temp215;
        var temp217 = temp177 * temp195 + temp216;
        var temp218 = temp68 * temp150;
        var temp219 = temp75 * temp147 - temp218;
        var temp220 = temp78 * temp155 + temp219;
        var temp221 = temp217 + temp187 * temp220;
        var temp222 = temp209 * temp221;
        var temp223 = temp222 - temp198;
        var temp224 = 0.5 * temp223;
        var temp225 = temp162 * temp187;
        var temp226 = temp169 * temp177;
        var temp227 = -(temp225 + temp226);
        var temp228 = temp175 * temp192;
        var temp229 = temp227 - temp228;
        var temp230 = temp164 * temp180 + temp229;
        var temp231 = temp159 * temp190;
        var temp232 = temp230 - temp231;
        var temp233 = temp171 * temp195;
        var temp234 = temp232 - temp233;
        var temp235 = temp182 * temp220;
        var temp236 = temp234 - temp235;
        var temp237 = temp115 * temp236;
        var temp238 = temp79 * temp111;
        var temp239 = temp86 * temp106;
        var temp240 = -(temp238 + temp239);
        var temp241 = temp93 * temp95;
        var temp242 = temp240 - temp241;
        var temp243 = temp89 * temp98 + temp242;
        var temp244 = temp100 * temp206;
        var temp245 = temp243 - temp244;
        var temp246 = temp81 * temp109;
        var temp247 = temp245 - temp246;
        var temp248 = temp43 * temp114;
        var temp249 = temp247 - temp248;
        var temp250 = temp221 * temp249;
        var temp251 = temp237 - temp250;
        var temp252 = temp197 * temp249;
        var temp253 = temp209 * temp236;
        var temp254 = temp253 - temp252;
        var temp255 = temp252 - temp253;
        var temp256 = temp198 - temp222;
        var temp257 = temp254 * temp255 + temp223 * temp256;
        var temp258 = temp86 * temp100;
        var temp259 = temp79 * temp89 - temp258;
        var temp260 = temp43 * temp93;
        var temp261 = temp259 - temp260;
        var temp262 = temp98 * temp111;
        var temp263 = temp261 - temp262;
        var temp264 = temp81 * temp103;
        var temp265 = temp263 - temp264;
        var temp266 = temp106 * temp206;
        var temp267 = temp265 - temp266;
        var temp268 = temp95 * temp114;
        var temp269 = temp267 - temp268;
        var temp270 = temp197 * temp269;
        var temp271 = temp159 * temp169;
        var temp272 = temp162 * temp164 - temp271;
        var temp273 = temp175 * temp182;
        var temp274 = temp272 - temp273;
        var temp275 = temp180 * temp187;
        var temp276 = temp274 - temp275;
        var temp277 = temp171 * temp185;
        var temp278 = temp276 - temp277;
        var temp279 = temp177 * temp190;
        var temp280 = temp278 - temp279;
        var temp281 = temp192 * temp220;
        var temp282 = temp280 - temp281;
        var temp283 = temp209 * temp282;
        var temp284 = temp283 - temp270;
        var temp285 = temp257 + temp284 * temp284;
        var temp286 = temp250 - temp237;
        var temp287 = temp285 + temp251 * temp286;
        var temp288 = temp236 * temp269;
        var temp289 = temp249 * temp282 - temp288;
        var temp290 = temp287 + temp289 * temp289;
        var temp291 = temp221 * temp269;
        var temp292 = temp115 * temp282;
        var temp293 = temp292 - temp291;
        var temp294 = temp290 + temp293 * temp293;
        var temp295 = 1 / temp294;
        var temp296 = temp251 * temp295;
        var temp297 = temp224 * temp296;
        var temp298 = -0.5 * temp252 + 0.5 * temp253;
        var temp299 = 0.5 * temp270 + temp298;
        var temp300 = -0.5 * temp283 + temp299;
        var temp301 = temp289 * temp295;
        var temp302 = temp300 * temp301 - temp297;
        var temp303 = temp293 * temp295;
        var temp304 = temp302 + temp224 * temp303;
        var temp305 = data.Ay * data.Ay + temp22;
        var temp306 = 0.5 + 0.5 * temp305;
        var temp307 = data.By * data.By + temp138;
        var temp308 = 0.5 + 0.5 * temp307;
        var temp309 = data.Ax * temp308;
        var temp310 = data.Bx * temp306 - temp309;
        var temp311 = temp254 * temp295;
        var temp312 = temp284 * temp295;
        var temp313 = temp224 * temp312;
        var temp314 = temp224 * temp311 - temp313;
        var temp315 = 0.5 * temp237 + -0.5 * temp250;
        var temp316 = 0.5 * temp291 + temp315;
        var temp317 = -0.5 * temp292 + temp316;
        var temp318 = temp314 + temp301 * temp317;
        var temp319 = data.Ay * temp308;
        var temp320 = data.By * temp306 - temp319;
        var temp321 = temp318 * temp320;
        var temp322 = temp304 * temp310 - temp321;
        var temp323 = temp304 * temp304 + temp318 * temp318;
        var temp324 = 0.5 + 0.5 * temp323;
        var temp325 = data.Ay * data.Bx;
        var temp326 = data.Ax * data.By - temp325;
        var temp327 = temp322 + temp324 * temp326;
        var temp328 = temp25 * halfBeta2Sin;
        var temp329 = temp20 * temp328;
        var temp330 = temp0 * temp328;
        var temp331 = -temp330;
        var temp332 = temp8 * temp328;
        var temp333 = temp330 * temp331 + temp332 * temp332;
        var temp334 = temp16 * temp328;
        var temp335 = -temp334;
        var temp336 = temp333 + temp334 * temp335;
        var temp337 = data.Ay * temp328;
        var temp338 = -temp337;
        var temp339 = temp336 + temp337 * temp338;
        var temp340 = data.Ax * temp328;
        var temp341 = temp339 + temp340 * temp340;
        var temp342 = temp329 * temp329 + temp341;
        var temp343 = temp342 + halfBeta2Cos * halfBeta2Cos;
        var temp344 = 1 / temp343;
        var temp345 = -temp329 * temp344;
        var temp346 = temp78 * temp334 + temp66 * temp340;
        var temp347 = temp75 * temp329 + temp346;
        var temp348 = temp347 + temp68 * halfBeta2Cos;
        var temp349 = temp338 * temp344;
        var temp350 = temp66 * temp330 + temp68 * temp334;
        var temp351 = temp75 * temp337 + temp350;
        var temp352 = temp351 + temp78 * halfBeta2Cos;
        var temp353 = temp349 * temp352;
        var temp354 = -temp332 * temp344;
        var temp355 = temp75 * temp332;
        var temp356 = temp78 * temp330 - temp355;
        var temp357 = temp68 * temp340;
        var temp358 = temp356 - temp357;
        var temp359 = temp358 + temp66 * halfBeta2Cos;
        var temp360 = temp354 * temp359;
        var temp361 = -(temp353 + temp360);
        var temp362 = temp345 * temp348 + temp361;
        var temp363 = temp344 * halfBeta2Cos;
        var temp364 = temp66 * temp332 + temp78 * temp337;
        var temp365 = temp68 * temp329;
        var temp366 = temp364 - temp365;
        var temp367 = temp366 + temp75 * halfBeta2Cos;
        var temp368 = temp362 + temp363 * temp367;
        var temp369 = temp331 * temp344;
        var temp370 = temp75 * temp330;
        var temp371 = temp78 * temp332 - temp370;
        var temp372 = temp66 * temp337 + temp371;
        var temp373 = temp369 * temp372;
        var temp374 = temp368 - temp373;
        var temp375 = -temp340 * temp344;
        var temp376 = temp75 * temp340;
        var temp377 = temp68 * temp332 - temp376;
        var temp378 = temp66 * temp329 + temp377;
        var temp379 = temp374 + temp375 * temp378;
        var temp380 = temp335 * temp344;
        var temp381 = temp68 * temp337;
        var temp382 = temp75 * temp334 - temp381;
        var temp383 = temp78 * temp329 + temp382;
        var temp384 = temp379 + temp380 * temp383;
        var temp385 = temp141 * halfAlpha2Sin;
        var temp386 = temp116 * temp385;
        var temp387 = temp132 * temp385;
        var temp388 = temp66 * temp386 + temp68 * temp387;
        var temp389 = data.By * temp385;
        var temp390 = temp388 + temp75 * temp389;
        var temp391 = temp390 + temp78 * halfAlpha2Cos;
        var temp392 = -temp386;
        var temp393 = temp124 * temp385;
        var temp394 = temp386 * temp392 + temp393 * temp393;
        var temp395 = -temp387;
        var temp396 = temp394 + temp387 * temp395;
        var temp397 = -temp389;
        var temp398 = temp396 + temp389 * temp397;
        var temp399 = data.Bx * temp385;
        var temp400 = temp398 + temp399 * temp399;
        var temp401 = temp136 * temp385;
        var temp402 = temp400 + temp401 * temp401;
        var temp403 = temp402 + halfAlpha2Cos * halfAlpha2Cos;
        var temp404 = 1 / temp403;
        var temp405 = temp392 * temp404;
        var temp406 = temp391 * temp405;
        var temp407 = temp75 * temp393;
        var temp408 = temp78 * temp386 - temp407;
        var temp409 = temp68 * temp399;
        var temp410 = temp408 - temp409;
        var temp411 = temp410 + temp66 * halfAlpha2Cos;
        var temp412 = temp404 * halfAlpha2Cos;
        var temp413 = temp411 * temp412 - temp406;
        var temp414 = temp78 * temp389 + temp66 * temp393;
        var temp415 = temp68 * temp401;
        var temp416 = temp414 - temp415;
        var temp417 = temp416 + temp75 * halfAlpha2Cos;
        var temp418 = -temp393 * temp404;
        var temp419 = temp413 + temp417 * temp418;
        var temp420 = temp78 * temp387 + temp66 * temp399;
        var temp421 = temp75 * temp401 + temp420;
        var temp422 = temp421 + temp68 * halfAlpha2Cos;
        var temp423 = -temp399 * temp404;
        var temp424 = temp419 + temp422 * temp423;
        var temp425 = temp75 * temp386;
        var temp426 = temp78 * temp393 - temp425;
        var temp427 = temp66 * temp389 + temp426;
        var temp428 = temp397 * temp404;
        var temp429 = temp424 + temp427 * temp428;
        var temp430 = temp68 * temp386;
        var temp431 = temp66 * temp387 - temp430;
        var temp432 = temp78 * temp399 + temp431;
        var temp433 = temp395 * temp404;
        var temp434 = temp429 + temp432 * temp433;
        var temp435 = temp75 * temp399;
        var temp436 = temp68 * temp393 - temp435;
        var temp437 = temp66 * temp401 + temp436;
        var temp438 = -temp401 * temp404;
        var temp439 = temp437 * temp438;
        var temp440 = temp434 - temp439;
        var temp441 = temp384 * temp440;
        var temp442 = temp352 * temp369;
        var temp443 = temp348 * temp375 - temp442;
        var temp444 = temp359 * temp363 + temp443;
        var temp445 = temp354 * temp367 + temp444;
        var temp446 = temp349 * temp372 + temp445;
        var temp447 = temp68 * temp330;
        var temp448 = temp66 * temp334 - temp447;
        var temp449 = temp78 * temp340 + temp448;
        var temp450 = temp446 + temp380 * temp449;
        var temp451 = temp345 * temp378;
        var temp452 = temp450 - temp451;
        var temp453 = temp391 * temp428;
        var temp454 = temp411 * temp418;
        var temp455 = -(temp453 + temp454);
        var temp456 = temp412 * temp417 + temp455;
        var temp457 = temp422 * temp438 + temp456;
        var temp458 = temp405 * temp427;
        var temp459 = temp457 - temp458;
        var temp460 = temp423 * temp437 + temp459;
        var temp461 = temp68 * temp389;
        var temp462 = temp75 * temp387 - temp461;
        var temp463 = temp78 * temp401 + temp462;
        var temp464 = temp460 + temp433 * temp463;
        var temp465 = temp452 * temp464;
        var temp466 = temp465 - temp441;
        var temp467 = 0.5 * temp466;
        var temp468 = temp352 * temp380;
        var temp469 = temp359 * temp375;
        var temp470 = -(temp468 + temp469);
        var temp471 = temp348 * temp363 + temp470;
        var temp472 = temp345 * temp367;
        var temp473 = temp471 - temp472;
        var temp474 = temp369 * temp449;
        var temp475 = temp473 - temp474;
        var temp476 = temp354 * temp378;
        var temp477 = temp475 - temp476;
        var temp478 = temp349 * temp383;
        var temp479 = temp477 - temp478;
        var temp480 = temp440 * temp479;
        var temp481 = temp391 * temp433;
        var temp482 = temp411 * temp423;
        var temp483 = -(temp481 + temp482);
        var temp484 = temp417 * temp438;
        var temp485 = temp483 - temp484;
        var temp486 = temp412 * temp422 + temp485;
        var temp487 = temp405 * temp432;
        var temp488 = temp486 - temp487;
        var temp489 = temp418 * temp437;
        var temp490 = temp488 - temp489;
        var temp491 = temp428 * temp463;
        var temp492 = temp490 - temp491;
        var temp493 = temp452 * temp492;
        var temp494 = temp493 - temp480;
        var temp495 = temp480 - temp493;
        var temp496 = temp441 - temp465;
        var temp497 = temp494 * temp495 + temp466 * temp496;
        var temp498 = temp359 * temp369;
        var temp499 = temp352 * temp363 - temp498;
        var temp500 = temp349 * temp367;
        var temp501 = temp499 - temp500;
        var temp502 = temp348 * temp380;
        var temp503 = temp501 - temp502;
        var temp504 = temp354 * temp372;
        var temp505 = temp503 - temp504;
        var temp506 = temp375 * temp449;
        var temp507 = temp505 - temp506;
        var temp508 = temp345 * temp383;
        var temp509 = temp507 - temp508;
        var temp510 = temp440 * temp509;
        var temp511 = temp405 * temp411;
        var temp512 = temp391 * temp412 - temp511;
        var temp513 = temp417 * temp428;
        var temp514 = temp512 - temp513;
        var temp515 = temp422 * temp433;
        var temp516 = temp514 - temp515;
        var temp517 = temp418 * temp427;
        var temp518 = temp516 - temp517;
        var temp519 = temp423 * temp432;
        var temp520 = temp518 - temp519;
        var temp521 = temp438 * temp463;
        var temp522 = temp520 - temp521;
        var temp523 = temp452 * temp522;
        var temp524 = temp523 - temp510;
        var temp525 = temp497 + temp524 * temp524;
        var temp526 = temp384 * temp492;
        var temp527 = temp464 * temp479;
        var temp528 = temp526 - temp527;
        var temp529 = temp527 - temp526;
        var temp530 = temp525 + temp528 * temp529;
        var temp531 = temp492 * temp509;
        var temp532 = temp479 * temp522 - temp531;
        var temp533 = temp530 + temp532 * temp532;
        var temp534 = temp464 * temp509;
        var temp535 = temp384 * temp522;
        var temp536 = temp535 - temp534;
        var temp537 = temp533 + temp536 * temp536;
        var temp538 = 1 / temp537;
        var temp539 = temp494 * temp538;
        var temp540 = temp524 * temp538;
        var temp541 = temp467 * temp540;
        var temp542 = temp467 * temp539 - temp541;
        var temp543 = 0.5 * temp526 + -0.5 * temp527;
        var temp544 = 0.5 * temp534 + temp543;
        var temp545 = -0.5 * temp535 + temp544;
        var temp546 = temp532 * temp538;
        var temp547 = temp542 + temp545 * temp546;
        var temp548 = temp324 * temp547;
        var temp549 = temp528 * temp538;
        var temp550 = temp467 * temp549;
        var temp551 = -0.5 * temp480 + 0.5 * temp493;
        var temp552 = 0.5 * temp510 + temp551;
        var temp553 = -0.5 * temp523 + temp552;
        var temp554 = temp546 * temp553 - temp550;
        var temp555 = temp536 * temp538;
        var temp556 = temp554 + temp467 * temp555;
        var temp557 = temp547 * temp547 + temp556 * temp556;
        var temp558 = 0.5 + 0.5 * temp557;
        var temp559 = temp318 * temp558 - temp548;
        var temp560 = 0.5 + -0.5 * temp323;
        var temp561 = temp547 * temp560;
        var temp562 = temp559 - temp561;
        var temp563 = 0.5 + -0.5 * temp557;
        var temp564 = temp562 + temp318 * temp563;
        var temp565 = temp327 * temp564;
        var temp566 = 0.5 + -0.5 * temp307;
        var temp567 = 0.5 + -0.5 * temp305;
        var temp568 = temp308 * temp567;
        var temp569 = temp306 * temp566 - temp568;
        var temp570 = temp310 * temp560;
        var temp571 = temp318 * temp569 - temp570;
        var temp572 = data.Ax * temp566;
        var temp573 = data.Bx * temp567 - temp572;
        var temp574 = temp571 + temp324 * temp573;
        var temp575 = temp318 * temp556;
        var temp576 = temp304 * temp547;
        var temp577 = temp576 - temp575;
        var temp578 = temp574 * temp577;
        var temp579 = temp565 - temp578;
        var temp580 = temp320 * temp560;
        var temp581 = temp304 * temp569 - temp580;
        var temp582 = data.Ay * temp566;
        var temp583 = data.By * temp567 - temp582;
        var temp584 = temp581 + temp324 * temp583;
        var temp585 = temp564 * temp584;
        var temp586 = temp304 * temp558;
        var temp587 = temp324 * temp556 - temp586;
        var temp588 = temp556 * temp560 + temp587;
        var temp589 = temp304 * temp563;
        var temp590 = temp588 - temp589;
        var temp591 = temp574 * temp590;
        var temp592 = -(temp585 + temp591);
        var temp593 = -temp592;
        var temp594 = temp578 - temp565;
        var temp595 = temp318 * temp583;
        var temp596 = temp304 * temp573 - temp595;
        var temp597 = temp326 * temp560 + temp596;
        var temp598 = temp564 * temp597;
        var temp599 = temp594 - temp598;
        var temp600 = temp575 - temp576;
        var temp601 = temp574 * temp600;
        var temp602 = temp599 + temp601;
        var temp603 = temp592 * temp592 + temp602 * temp602;
        var temp604 = temp327 * temp590;
        var temp605 = temp577 * temp584;
        var temp606 = -(temp604 + temp605);
        var temp607 = temp590 * temp597;
        var temp608 = temp584 * temp600;
        var temp609 = -(temp607 + temp608);
        var temp610 = -(temp606 + temp609);
        var temp611 = temp603 + temp610 * temp610;
        var temp612 = temp592 * temp593 + temp611;
        var temp613 = 1 / temp612;
        var temp614 = temp593 * temp613;
        var temp615 = -temp614;
        var temp616 = temp598 - temp601;
        var temp617 = temp592 * temp613;
        var temp618 = -temp617;
        var temp619 = temp616 * temp618;
        var temp620 = temp579 * temp615 - temp619;
        var temp621 = temp327 * temp600;
        var temp622 = temp577 * temp597 - temp621;
        var temp623 = temp610 * temp613;
        var temp624 = -temp623;
        var temp625 = temp620 + temp622 * temp624;
        var temp626 = temp585 + temp591;
        var temp627 = temp602 * temp613;
        var temp628 = -temp627;
        var temp629 = temp618 * temp626 + temp579 * temp628;
        var temp630 = temp606 * temp624 + temp629;
        var temp631 = temp615 * temp626 + temp630;
        var temp632 = temp616 * temp628 + temp631;
        var temp633 = temp609 * temp624 + temp632;
        var temp634 = 1 / temp633;
        var temp635 = temp625 * temp634;
        p1X = 2 * temp635 - temp318;

        var temp636 = temp609 * temp618;
        var temp637 = temp606 * temp615 - temp636;
        var temp638 = temp622 * temp628;
        var temp639 = temp637 - temp638;
        var temp640 = temp634 * temp639;
        p1Y = 2 * temp640 - temp304;

        var temp641 = temp320 * temp547;
        var temp642 = temp310 * temp556 - temp641;
        var temp643 = temp326 * temp558 + temp642;
        var temp644 = temp564 * temp643;
        var temp645 = temp310 * temp563;
        var temp646 = temp547 * temp569 - temp645;
        var temp647 = temp558 * temp573 + temp646;
        var temp648 = temp577 * temp647;
        var temp649 = temp644 - temp648;
        var temp650 = temp320 * temp563;
        var temp651 = temp556 * temp569 - temp650;
        var temp652 = temp558 * temp583 + temp651;
        var temp653 = temp564 * temp652;
        var temp654 = temp590 * temp647;
        var temp655 = -(temp653 + temp654);
        var temp656 = -temp655;
        var temp657 = temp648 - temp644;
        var temp658 = temp547 * temp583;
        var temp659 = temp556 * temp573 - temp658;
        var temp660 = temp326 * temp563 + temp659;
        var temp661 = temp564 * temp660;
        var temp662 = temp657 - temp661;
        var temp663 = temp600 * temp647;
        var temp664 = temp662 + temp663;
        var temp665 = temp655 * temp655 + temp664 * temp664;
        var temp666 = temp590 * temp643;
        var temp667 = temp577 * temp652;
        var temp668 = -(temp666 + temp667);
        var temp669 = temp590 * temp660;
        var temp670 = temp600 * temp652;
        var temp671 = -(temp669 + temp670);
        var temp672 = -(temp668 + temp671);
        var temp673 = temp665 + temp672 * temp672;
        var temp674 = temp655 * temp656 + temp673;
        var temp675 = 1 / temp674;
        var temp676 = temp656 * temp675;
        var temp677 = -temp676;
        var temp678 = temp661 - temp663;
        var temp679 = temp655 * temp675;
        var temp680 = -temp679;
        var temp681 = temp678 * temp680;
        var temp682 = temp649 * temp677 - temp681;
        var temp683 = temp600 * temp643;
        var temp684 = temp577 * temp660 - temp683;
        var temp685 = temp672 * temp675;
        var temp686 = -temp685;
        var temp687 = temp682 + temp684 * temp686;
        var temp688 = temp653 + temp654;
        var temp689 = temp664 * temp675;
        var temp690 = -temp689;
        var temp691 = temp680 * temp688 + temp649 * temp690;
        var temp692 = temp668 * temp686 + temp691;
        var temp693 = temp677 * temp688 + temp692;
        var temp694 = temp678 * temp690 + temp693;
        var temp695 = temp671 * temp686 + temp694;
        var temp696 = 1 / temp695;
        var temp697 = temp687 * temp696;
        p2X = 2 * temp697 - temp547;

        var temp698 = temp671 * temp680;
        var temp699 = temp668 * temp677 - temp698;
        var temp700 = temp684 * temp690;
        var temp701 = temp699 - temp700;
        var temp702 = temp696 * temp701;
        p2Y = 2 * temp702 - temp556;

        //Finish GA-FuL MetaContext Code Generation, 2024-04-19T01:05:17.4209873+02:00
    }

    public static void SolveUsingCGaOpt(HansenProblemData2D data, out double p1X, out double p1Y, out double p2X, out double p2Y)
    {
        var alpha1Sin = Math.Sin(data.Alpha1);
        var alpha2Sin = Math.Sin(data.Alpha2);
        var beta1Sin = Math.Sin(data.Beta1);
        var beta2Sin = Math.Sin(data.Beta2);

        var alpha1Cos = Math.Sqrt(1 - alpha1Sin * alpha1Sin);
        var alpha2Cos = Math.Sqrt(1 - alpha2Sin * alpha2Sin);
        var beta1Cos = Math.Sqrt(1 - beta1Sin * beta1Sin);
        var beta2Cos = Math.Sqrt(1 - beta2Sin * beta2Sin);

        if (((int)Math.Floor(2 * data.Alpha1 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha1Cos = -alpha1Cos;
        if (((int)Math.Floor(2 * data.Alpha2 / Math.PI) % 4 + 4) % 4 is 1 or 2) alpha2Cos = -alpha2Cos;
        if (((int)Math.Floor(2 * data.Beta1 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta1Cos = -beta1Cos;
        if (((int)Math.Floor(2 * data.Beta2 / Math.PI) % 4 + 4) % 4 is 1 or 2) beta2Cos = -beta2Cos;

        var halfAlpha1Cos = 1 / Math.Sqrt(2) * Math.Sqrt(1 + alpha1Cos);
        var halfAlpha1Sin = 1 / Math.Sqrt(2) * Math.Sqrt(1 - alpha1Cos);
        if (alpha1Sin > 0) halfAlpha1Cos = -halfAlpha1Cos;

        var halfBeta1Cos = 1 / Math.Sqrt(2) * Math.Sqrt(1 + beta1Cos);
        var halfBeta1Sin = 1 / Math.Sqrt(2) * Math.Sqrt(1 - beta1Cos);
        if (beta1Sin > 0) halfBeta1Cos = -halfBeta1Cos;

        var halfAlpha2Cos = 1 / Math.Sqrt(2) * Math.Sqrt(1 + alpha2Cos);
        var halfAlpha2Sin = 1 / Math.Sqrt(2) * Math.Sqrt(1 - alpha2Cos);
        if (alpha2Sin < 0) halfAlpha2Cos = -halfAlpha2Cos;

        var halfBeta2Cos = 1 / Math.Sqrt(2) * Math.Sqrt(1 + beta2Cos);
        var halfBeta2Sin = 1 / Math.Sqrt(2) * Math.Sqrt(1 - beta2Cos);
        if (beta2Sin < 0) halfBeta2Cos = -halfBeta2Cos;

        //Begin GA-FuL MetaContext Code Generation, 2024-04-19T01:05:17.3369930+02:00
        //MetaContext: CGA
        //Input Variables: 8 used, 4 not used, 12 total.
        //Temp Variables: 703 sub-expressions, 0 generated temps, 703 total.
        //Target Temp Variables: 61 total.
        //Output Variables: 12 total.
        //Computations: 1.393006993006993 average, 996 total.
        //Memory Reads: 2.2097902097902096 average, 1580 total.
        //Memory Writes: 715 total.
        //
        //MetaContext Binding Data:
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   0.5 = constant: '0.5'
        //   Rational[1, 2] = constant: 'Rational[1, 2]'
        //   -0.5 = constant: '-0.5'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   Ax = parameter: data.Ax
        //   Ay = parameter: data.Ay
        //   Bx = parameter: data.Bx
        //   By = parameter: data.By
        //   alpha1Cos = parameter: alpha1Cos
        //   alpha2Cos = parameter: alpha2Cos
        //   beta1Cos = parameter: beta1Cos
        //   beta2Cos = parameter: beta2Cos
        //   alpha1Sin = parameter: alpha1Sin
        //   alpha2Sin = parameter: alpha2Sin
        //   beta1Sin = parameter: beta1Sin
        //   beta2Sin = parameter: beta2Sin

        var tmpVar998369 = -data.Ax;
        var tmpVar998370 = halfAlpha1Sin * tmpVar998369;
        var tmpVar998372 = halfAlpha1Sin * halfAlpha1Sin;
        var tmpVar998373 = data.Ax * data.Ax;
        var tmpVar998375 = halfAlpha1Cos * halfAlpha1Cos + tmpVar998372 * tmpVar998373;
        var tmpVar998378 = data.Ay * data.Ay + tmpVar998373;
        var tmpVar998379 = -1 * tmpVar998372 * tmpVar998378;
        var tmpVar998380 = tmpVar998375 + tmpVar998379;
        var tmpVar998381 = -halfAlpha1Sin;
        var tmpVar998383 = tmpVar998380 + tmpVar998381 * tmpVar998381;
        var tmpVar998384 = -data.Ay;
        var tmpVar998385 = halfAlpha1Sin * tmpVar998384;
        var tmpVar998387 = tmpVar998383 + tmpVar998385 * tmpVar998385;
        var tmpVar998388 = 1 / tmpVar998387;
        var tmpVar998389 = tmpVar998370 * tmpVar998388;
        var tmpVar998390 = data.By + tmpVar998384;
        var tmpVar998392 = -data.Bx;
        var tmpVar998393 = data.Ax + tmpVar998392;
        var tmpVar998395 = tmpVar998390 * tmpVar998390 + tmpVar998393 * tmpVar998393;
        var tmpVar998397 = 1 / Math.Sqrt(Math.Abs(tmpVar998395));
        var tmpVar998400 = 0.5 * data.Ay;
        var tmpVar998401 = 2 * tmpVar998393 * tmpVar998400;
        var tmpVar998402 = data.Ax * tmpVar998390 + tmpVar998401;
        var tmpVar998403 = tmpVar998397 * tmpVar998402;
        var tmpVar998404 = tmpVar998381 * tmpVar998403;
        var tmpVar998405 = tmpVar998390 * tmpVar998397;
        var tmpVar998406 = tmpVar998400 * tmpVar998405;
        var tmpVar998407 = -0.5 * data.Ax;
        var tmpVar998409 = data.Bx + tmpVar998369;
        var tmpVar998410 = tmpVar998397 * tmpVar998409;
        var tmpVar998411 = tmpVar998406 * tmpVar998407 + tmpVar998410;
        var tmpVar998413 = 0.5 * data.Ax * tmpVar998410;
        var tmpVar998415 = tmpVar998411 + tmpVar998407 * tmpVar998413;
        var tmpVar998417 = -data.By;
        var tmpVar998418 = data.Ay + tmpVar998417;
        var tmpVar998419 = tmpVar998397 * tmpVar998418;
        var tmpVar998421 = tmpVar998407 * tmpVar998410 + tmpVar998400 * tmpVar998419;
        var tmpVar998423 = tmpVar998415 + tmpVar998407 * tmpVar998421;
        var tmpVar998424 = tmpVar998385 * tmpVar998423;
        var tmpVar998425 = tmpVar998404 + tmpVar998424;
        var tmpVar998426 = tmpVar998370 * tmpVar998419;
        var tmpVar998427 = tmpVar998425 + tmpVar998426;
        var tmpVar998429 = halfAlpha1Cos * tmpVar998388;
        var tmpVar998432 = halfAlpha1Cos * tmpVar998419 + tmpVar998381 * tmpVar998423;
        var tmpVar998433 = tmpVar998385 * tmpVar998403;
        var tmpVar998435 = tmpVar998432 + -tmpVar998433;
        var tmpVar998438 = tmpVar998405 * tmpVar998407 + tmpVar998400 * tmpVar998410;
        var tmpVar998439 = 2 * tmpVar998438;
        var tmpVar998440 = data.Ay * halfAlpha1Sin;
        var tmpVar998442 = tmpVar998435 + tmpVar998439 * tmpVar998440;
        var tmpVar998444 = tmpVar998389 * tmpVar998427 + tmpVar998429 * tmpVar998442;
        var tmpVar998447 = halfAlpha1Cos * tmpVar998403 + tmpVar998385 * tmpVar998419;
        var tmpVar998448 = data.Ax * halfAlpha1Sin;
        var tmpVar998450 = tmpVar998447 + tmpVar998423 * tmpVar998448;
        var tmpVar998452 = -1 * tmpVar998385 * tmpVar998388;
        var tmpVar998454 = tmpVar998444 + tmpVar998450 * tmpVar998452;
        var tmpVar998457 = tmpVar998370 * tmpVar998423 + halfAlpha1Cos * tmpVar998439;
        var tmpVar998459 = tmpVar998419 * tmpVar998440 + tmpVar998457;
        var tmpVar998461 = tmpVar998454 + tmpVar998452 * tmpVar998459;
        var tmpVar998464 = halfAlpha1Cos * tmpVar998423 + tmpVar998370 * tmpVar998439;
        var tmpVar998465 = tmpVar998370 * tmpVar998403;
        var tmpVar998466 = tmpVar998464 + tmpVar998465;
        var tmpVar998467 = tmpVar998381 * tmpVar998419;
        var tmpVar998469 = tmpVar998466 + -tmpVar998467;
        var tmpVar998470 = halfAlpha1Sin * tmpVar998388;
        var tmpVar998471 = tmpVar998469 * tmpVar998470;
        var tmpVar998473 = tmpVar998461 + -tmpVar998471;
        var tmpVar998474 = tmpVar998388 * tmpVar998448;
        var tmpVar998476 = -tmpVar998426;
        var tmpVar998478 = tmpVar998423 * tmpVar998440 + tmpVar998476;
        var tmpVar998480 = tmpVar998381 * tmpVar998439 + tmpVar998478;
        var tmpVar998481 = -1 * tmpVar998474 * tmpVar998480;
        var tmpVar998482 = tmpVar998473 + tmpVar998481;
        var tmpVar998484 = halfBeta1Sin * halfBeta1Sin;
        var tmpVar998485 = halfBeta1Cos * halfBeta1Cos + tmpVar998484;
        var tmpVar998486 = data.By * data.By;
        var tmpVar998488 = tmpVar998485 + tmpVar998484 * tmpVar998486;
        var tmpVar998489 = data.By * halfBeta1Sin;
        var tmpVar998492 = tmpVar998488 + -(tmpVar998489 * tmpVar998489);
        var tmpVar998493 = 1 / tmpVar998492;
        var tmpVar998494 = halfBeta1Sin * tmpVar998392;
        var tmpVar998497 = tmpVar998417 * tmpVar998419;
        var tmpVar998498 = data.Bx * tmpVar998423 + tmpVar998497;
        var tmpVar998500 = halfBeta1Cos * tmpVar998403 + halfBeta1Sin * tmpVar998498;
        var tmpVar998504 = -halfBeta1Sin;
        var tmpVar998506 = tmpVar998392 * tmpVar998419 + tmpVar998439;
        var tmpVar998508 = tmpVar998423 * tmpVar998489 + tmpVar998504 * tmpVar998506;
        var tmpVar998509 = -1 * tmpVar998489 * tmpVar998508;
        var tmpVar998510 = tmpVar998494 * tmpVar998500 + tmpVar998509;
        var tmpVar998513 = halfBeta1Sin * tmpVar998419 + halfBeta1Cos * tmpVar998423;
        var tmpVar998515 = tmpVar998439 * tmpVar998494 + tmpVar998513;
        var tmpVar998516 = tmpVar998403 * tmpVar998494;
        var tmpVar998517 = tmpVar998515 + tmpVar998516;
        var tmpVar998519 = tmpVar998510 + halfBeta1Cos * tmpVar998517;
        var tmpVar998521 = halfBeta1Sin * tmpVar998493;
        var tmpVar998524 = halfBeta1Cos * tmpVar998419 + tmpVar998423 * tmpVar998504;
        var tmpVar998526 = tmpVar998439 * tmpVar998489 + tmpVar998524;
        var tmpVar998527 = tmpVar998403 * tmpVar998489;
        var tmpVar998528 = tmpVar998526 + tmpVar998527;
        var tmpVar998530 = tmpVar998493 * tmpVar998519 + tmpVar998521 * tmpVar998528;
        var tmpVar998531 = tmpVar998493 * tmpVar998494;
        var tmpVar998532 = halfBeta1Cos * tmpVar998439;
        var tmpVar998533 = tmpVar998419 * tmpVar998489;
        var tmpVar998534 = tmpVar998532 + tmpVar998533;
        var tmpVar998535 = tmpVar998423 * tmpVar998494;
        var tmpVar998536 = tmpVar998534 + tmpVar998535;
        var tmpVar998538 = tmpVar998530 + tmpVar998531 * tmpVar998536;
        var tmpVar998539 = tmpVar998489 * tmpVar998493;
        var tmpVar998541 = tmpVar998419 * tmpVar998494;
        var tmpVar998542 = tmpVar998403 * tmpVar998504;
        var tmpVar998543 = tmpVar998541 + tmpVar998542;
        var tmpVar998544 = tmpVar998417 * tmpVar998423;
        var tmpVar998545 = halfBeta1Sin * tmpVar998544;
        var tmpVar998546 = tmpVar998543 + tmpVar998545;
        var tmpVar998547 = -1 * tmpVar998539 * tmpVar998546;
        var tmpVar998548 = tmpVar998538 + tmpVar998547;
        var tmpVar998549 = tmpVar998482 * tmpVar998548;
        var tmpVar998551 = tmpVar998500 + tmpVar998532;
        var tmpVar998552 = tmpVar998533 + tmpVar998551;
        var tmpVar998553 = tmpVar998535 + tmpVar998552;
        var tmpVar998555 = halfBeta1Cos * tmpVar998493;
        var tmpVar998557 = tmpVar998539 * tmpVar998553 + tmpVar998528 * tmpVar998555;
        var tmpVar998558 = tmpVar998508 + tmpVar998541;
        var tmpVar998559 = tmpVar998542 + tmpVar998558;
        var tmpVar998560 = tmpVar998545 + tmpVar998559;
        var tmpVar998562 = tmpVar998557 + tmpVar998531 * tmpVar998560;
        var tmpVar998563 = tmpVar998517 * tmpVar998521;
        var tmpVar998565 = tmpVar998562 + -tmpVar998563;
        var tmpVar998568 = tmpVar998389 * tmpVar998450 + tmpVar998442 * tmpVar998470;
        var tmpVar998570 = tmpVar998429 * tmpVar998469 + tmpVar998568;
        var tmpVar998573 = -tmpVar998404 + -tmpVar998424;
        var tmpVar998574 = tmpVar998476 + tmpVar998573;
        var tmpVar998576 = tmpVar998570 + tmpVar998452 * tmpVar998574;
        var tmpVar998577 = tmpVar998385 * tmpVar998388;
        var tmpVar998579 = tmpVar998576 + tmpVar998480 * tmpVar998577;
        var tmpVar998580 = tmpVar998459 * tmpVar998474;
        var tmpVar998582 = tmpVar998579 + -tmpVar998580;
        var tmpVar998583 = tmpVar998565 * tmpVar998582;
        var tmpVar998584 = -tmpVar998549 + tmpVar998583;
        var tmpVar998585 = 0.5 * tmpVar998584;
        var tmpVar998587 = -tmpVar998470;
        var tmpVar998589 = tmpVar998429 * tmpVar998450 + tmpVar998427 * tmpVar998587;
        var tmpVar998591 = tmpVar998433 + tmpVar998385 * tmpVar998439;
        var tmpVar998592 = tmpVar998452 * tmpVar998591;
        var tmpVar998593 = tmpVar998589 + tmpVar998592;
        var tmpVar998594 = tmpVar998469 * tmpVar998474;
        var tmpVar998595 = tmpVar998593 + tmpVar998594;
        var tmpVar998596 = tmpVar998442 * tmpVar998452;
        var tmpVar998598 = tmpVar998595 + -tmpVar998596;
        var tmpVar998601 = tmpVar998439 * tmpVar998448 + -tmpVar998465;
        var tmpVar998602 = tmpVar998474 * tmpVar998601;
        var tmpVar998604 = tmpVar998598 + -tmpVar998602;
        var tmpVar998605 = tmpVar998548 * tmpVar998604;
        var tmpVar998608 = -1 * tmpVar998521 * tmpVar998546;
        var tmpVar998610 = data.Bx * halfBeta1Sin;
        var tmpVar998612 = halfBeta1Cos * tmpVar998500 + tmpVar998517 * tmpVar998610;
        var tmpVar998614 = tmpVar998608 + tmpVar998493 * tmpVar998612;
        var tmpVar998615 = tmpVar998528 * tmpVar998539;
        var tmpVar998617 = tmpVar998614 + -tmpVar998615;
        var tmpVar998620 = -tmpVar998516 + tmpVar998439 * tmpVar998610;
        var tmpVar998621 = tmpVar998531 * tmpVar998620;
        var tmpVar998622 = tmpVar998617 + tmpVar998621;
        var tmpVar998624 = tmpVar998417 * tmpVar998439;
        var tmpVar998626 = -tmpVar998527 + halfBeta1Sin * tmpVar998624;
        var tmpVar998627 = tmpVar998539 * tmpVar998626;
        var tmpVar998628 = tmpVar998622 + tmpVar998627;
        var tmpVar998629 = tmpVar998582 * tmpVar998628;
        var tmpVar998630 = -tmpVar998605 + tmpVar998629;
        var tmpVar998631 = tmpVar998585 * tmpVar998630;
        var tmpVar998633 = tmpVar998517 * tmpVar998531 + tmpVar998615;
        var tmpVar998635 = tmpVar998536 * tmpVar998555 + tmpVar998633;
        var tmpVar998638 = -tmpVar998621 + -tmpVar998627;
        var tmpVar998639 = tmpVar998635 + tmpVar998638;
        var tmpVar998640 = tmpVar998508 * tmpVar998521;
        var tmpVar998642 = tmpVar998639 + -tmpVar998640;
        var tmpVar998644 = tmpVar998596 + tmpVar998602;
        var tmpVar998646 = tmpVar998480 * tmpVar998587 + tmpVar998644;
        var tmpVar998648 = tmpVar998429 * tmpVar998459 + tmpVar998646;
        var tmpVar998651 = -tmpVar998592 + -tmpVar998594;
        var tmpVar998652 = tmpVar998648 + tmpVar998651;
        var tmpVar998653 = tmpVar998628 * tmpVar998652;
        var tmpVar998655 = tmpVar998604 * tmpVar998642 + -tmpVar998653;
        var tmpVar998658 = tmpVar998605 + -tmpVar998629;
        var tmpVar998660 = tmpVar998655 * tmpVar998655 + tmpVar998630 * tmpVar998658;
        var tmpVar998662 = tmpVar998549 + -tmpVar998583;
        var tmpVar998664 = tmpVar998660 + tmpVar998584 * tmpVar998662;
        var tmpVar998665 = tmpVar998548 * tmpVar998652;
        var tmpVar998667 = tmpVar998582 * tmpVar998642;
        var tmpVar998668 = -tmpVar998665 + tmpVar998667;
        var tmpVar998670 = tmpVar998664 + tmpVar998668 * tmpVar998668;
        var tmpVar998671 = tmpVar998482 * tmpVar998628;
        var tmpVar998672 = tmpVar998565 * tmpVar998604;
        var tmpVar998674 = tmpVar998671 + -tmpVar998672;
        var tmpVar998676 = -tmpVar998671 + tmpVar998672;
        var tmpVar998678 = tmpVar998670 + tmpVar998674 * tmpVar998676;
        var tmpVar998679 = tmpVar998565 * tmpVar998652;
        var tmpVar998681 = tmpVar998482 * tmpVar998642;
        var tmpVar998682 = -tmpVar998679 + tmpVar998681;
        var tmpVar998684 = tmpVar998678 + tmpVar998682 * tmpVar998682;
        var tmpVar998685 = 1 / tmpVar998684;
        var tmpVar998686 = tmpVar998631 * tmpVar998685;
        var tmpVar998688 = tmpVar998655 * tmpVar998685;
        var tmpVar998691 = 0.5 * tmpVar998671 + -0.5 * tmpVar998672;
        var tmpVar998693 = 0.5 * tmpVar998679 + tmpVar998691;
        var tmpVar998695 = -0.5 * tmpVar998681 + tmpVar998693;
        var tmpVar998696 = tmpVar998688 * tmpVar998695;
        var tmpVar998698 = -tmpVar998686 + -tmpVar998696;
        var tmpVar998699 = tmpVar998585 * tmpVar998668;
        var tmpVar998700 = tmpVar998685 * tmpVar998699;
        var tmpVar998701 = tmpVar998698 + tmpVar998700;
        var tmpVar998702 = tmpVar998686 + tmpVar998696;
        var tmpVar998704 = -tmpVar998700 + tmpVar998702;
        var tmpVar998705 = data.Ay * halfBeta2Sin;
        var tmpVar998706 = -tmpVar998705;
        var tmpVar998709 = halfBeta2Cos * halfBeta2Cos + tmpVar998705 * tmpVar998706;
        var tmpVar998712 = -1 * halfBeta2Sin * halfBeta2Sin * tmpVar998373;
        var tmpVar998713 = tmpVar998709 + tmpVar998712;
        var tmpVar998714 = halfBeta2Sin * tmpVar998384;
        var tmpVar998716 = tmpVar998713 + tmpVar998714 * tmpVar998714;
        var tmpVar998717 = -halfBeta2Sin;
        var tmpVar998719 = tmpVar998716 + tmpVar998717 * tmpVar998717;
        var tmpVar998720 = data.Ax * halfBeta2Sin;
        var tmpVar998722 = tmpVar998719 + tmpVar998720 * tmpVar998720;
        var tmpVar998723 = 1 / tmpVar998722;
        var tmpVar998724 = tmpVar998706 * tmpVar998723;
        var tmpVar998728 = halfBeta2Cos * tmpVar998439 + tmpVar998419 * tmpVar998705;
        var tmpVar998729 = tmpVar998369 * tmpVar998423;
        var tmpVar998731 = tmpVar998728 + halfBeta2Sin * tmpVar998729;
        var tmpVar998732 = -1 * tmpVar998724 * tmpVar998731;
        var tmpVar998733 = halfBeta2Sin * tmpVar998723;
        var tmpVar998734 = -tmpVar998733;
        var tmpVar998736 = halfBeta2Sin * tmpVar998369;
        var tmpVar998738 = halfBeta2Cos * tmpVar998423 + tmpVar998439 * tmpVar998736;
        var tmpVar998739 = tmpVar998419 * tmpVar998717;
        var tmpVar998741 = tmpVar998738 + -tmpVar998739;
        var tmpVar998742 = tmpVar998403 * tmpVar998720;
        var tmpVar998744 = tmpVar998741 + -tmpVar998742;
        var tmpVar998746 = tmpVar998732 + tmpVar998734 * tmpVar998744;
        var tmpVar998748 = -1 * tmpVar998714 * tmpVar998723;
        var tmpVar998751 = halfBeta2Cos * tmpVar998403 + tmpVar998419 * tmpVar998714;
        var tmpVar998753 = tmpVar998423 * tmpVar998720 + tmpVar998751;
        var tmpVar998755 = tmpVar998746 + tmpVar998748 * tmpVar998753;
        var tmpVar998756 = halfBeta2Cos * tmpVar998723;
        var tmpVar998759 = halfBeta2Cos * tmpVar998419 + tmpVar998439 * tmpVar998705;
        var tmpVar998761 = tmpVar998423 * tmpVar998717 + tmpVar998759;
        var tmpVar998762 = tmpVar998403 * tmpVar998714;
        var tmpVar998764 = tmpVar998761 + -tmpVar998762;
        var tmpVar998766 = tmpVar998755 + tmpVar998756 * tmpVar998764;
        var tmpVar998767 = tmpVar998723 * tmpVar998736;
        var tmpVar998770 = tmpVar998423 * tmpVar998705 + tmpVar998439 * tmpVar998717;
        var tmpVar998771 = tmpVar998369 * tmpVar998419;
        var tmpVar998773 = tmpVar998770 + tmpVar998717 * tmpVar998771;
        var tmpVar998775 = tmpVar998766 + tmpVar998767 * tmpVar998773;
        var tmpVar998777 = -1 * tmpVar998720 * tmpVar998723;
        var tmpVar998780 = tmpVar998423 * tmpVar998714 + tmpVar998403 * tmpVar998717;
        var tmpVar998781 = tmpVar998419 * tmpVar998720;
        var tmpVar998783 = tmpVar998780 + -tmpVar998781;
        var tmpVar998785 = tmpVar998775 + tmpVar998777 * tmpVar998783;
        var tmpVar998787 = -halfAlpha2Sin;
        var tmpVar998789 = halfAlpha2Sin * tmpVar998544 + tmpVar998403 * tmpVar998787;
        var tmpVar998790 = data.Bx * halfAlpha2Sin;
        var tmpVar998791 = tmpVar998419 * tmpVar998790;
        var tmpVar998793 = tmpVar998789 + -tmpVar998791;
        var tmpVar998794 = data.By * halfAlpha2Sin;
        var tmpVar998797 = halfAlpha2Cos * halfAlpha2Cos + tmpVar998787 * tmpVar998787;
        var tmpVar998799 = tmpVar998790 * tmpVar998790 + tmpVar998797;
        var tmpVar998802 = -(tmpVar998794 * tmpVar998794) + tmpVar998799;
        var tmpVar998804 = data.Bx * data.Bx;
        var tmpVar998806 = tmpVar998486 + -tmpVar998804;
        var tmpVar998807 = halfAlpha2Sin * halfAlpha2Sin * tmpVar998806;
        var tmpVar998808 = tmpVar998802 + tmpVar998807;
        var tmpVar998809 = 1 / tmpVar998808;
        var tmpVar998810 = tmpVar998794 * tmpVar998809;
        var tmpVar998811 = tmpVar998793 * tmpVar998810;
        var tmpVar998814 = -1 * tmpVar998790 * tmpVar998809;
        var tmpVar998817 = halfAlpha2Cos * tmpVar998439 + tmpVar998419 * tmpVar998794;
        var tmpVar998818 = halfAlpha2Sin * tmpVar998392;
        var tmpVar998820 = tmpVar998817 + tmpVar998423 * tmpVar998818;
        var tmpVar998822 = -tmpVar998811 + tmpVar998814 * tmpVar998820;
        var tmpVar998825 = halfAlpha2Cos * tmpVar998423 + tmpVar998439 * tmpVar998818;
        var tmpVar998826 = tmpVar998419 * tmpVar998787;
        var tmpVar998828 = tmpVar998825 + -tmpVar998826;
        var tmpVar998829 = tmpVar998403 * tmpVar998790;
        var tmpVar998831 = tmpVar998828 + -tmpVar998829;
        var tmpVar998832 = halfAlpha2Cos * tmpVar998809;
        var tmpVar998834 = tmpVar998822 + tmpVar998831 * tmpVar998832;
        var tmpVar998837 = halfAlpha2Cos * tmpVar998419 + tmpVar998439 * tmpVar998794;
        var tmpVar998839 = tmpVar998423 * tmpVar998787 + tmpVar998837;
        var tmpVar998840 = tmpVar998403 * tmpVar998794;
        var tmpVar998841 = tmpVar998839 + tmpVar998840;
        var tmpVar998842 = halfAlpha2Sin * tmpVar998809;
        var tmpVar998844 = tmpVar998834 + tmpVar998841 * tmpVar998842;
        var tmpVar998845 = halfAlpha2Sin * tmpVar998497;
        var tmpVar998846 = halfAlpha2Cos * tmpVar998403;
        var tmpVar998847 = tmpVar998845 + tmpVar998846;
        var tmpVar998848 = tmpVar998423 * tmpVar998790;
        var tmpVar998849 = tmpVar998847 + tmpVar998848;
        var tmpVar998851 = tmpVar998844 + tmpVar998814 * tmpVar998849;
        var tmpVar998853 = -1 * tmpVar998794 * tmpVar998809;
        var tmpVar998854 = tmpVar998439 * tmpVar998787;
        var tmpVar998855 = tmpVar998423 * tmpVar998794;
        var tmpVar998856 = tmpVar998854 + tmpVar998855;
        var tmpVar998857 = tmpVar998392 * tmpVar998826;
        var tmpVar998858 = tmpVar998856 + tmpVar998857;
        var tmpVar998860 = tmpVar998851 + tmpVar998853 * tmpVar998858;
        var tmpVar998861 = tmpVar998785 * tmpVar998860;
        var tmpVar998865 = tmpVar998744 * tmpVar998756 + tmpVar998733 * tmpVar998764;
        var tmpVar998867 = tmpVar998753 * tmpVar998777 + tmpVar998865;
        var tmpVar998869 = tmpVar998731 * tmpVar998767 + tmpVar998867;
        var tmpVar998871 = tmpVar998724 * tmpVar998773 + tmpVar998869;
        var tmpVar998873 = -1 * tmpVar998748 * tmpVar998783;
        var tmpVar998874 = tmpVar998871 + tmpVar998873;
        var tmpVar998876 = tmpVar998820 + tmpVar998845;
        var tmpVar998877 = tmpVar998846 + tmpVar998876;
        var tmpVar998878 = tmpVar998848 + tmpVar998877;
        var tmpVar998880 = tmpVar998832 * tmpVar998841 + tmpVar998810 * tmpVar998878;
        var tmpVar998881 = tmpVar998793 + tmpVar998854;
        var tmpVar998882 = tmpVar998855 + tmpVar998881;
        var tmpVar998883 = tmpVar998857 + tmpVar998882;
        var tmpVar998885 = tmpVar998880 + tmpVar998814 * tmpVar998883;
        var tmpVar998886 = tmpVar998831 * tmpVar998842;
        var tmpVar998888 = tmpVar998885 + -tmpVar998886;
        var tmpVar998889 = tmpVar998874 * tmpVar998888;
        var tmpVar998890 = -tmpVar998861 + tmpVar998889;
        var tmpVar998891 = 0.5 * tmpVar998890;
        var tmpVar998894 = tmpVar998753 * tmpVar998756 + tmpVar998734 * tmpVar998783;
        var tmpVar998896 = tmpVar998369 * tmpVar998717;
        var tmpVar998898 = tmpVar998439 * tmpVar998720 + tmpVar998403 * tmpVar998896;
        var tmpVar998900 = tmpVar998894 + tmpVar998767 * tmpVar998898;
        var tmpVar998901 = tmpVar998744 * tmpVar998777;
        var tmpVar998903 = tmpVar998900 + -tmpVar998901;
        var tmpVar998904 = tmpVar998748 * tmpVar998764;
        var tmpVar998906 = tmpVar998903 + -tmpVar998904;
        var tmpVar998909 = -1 * tmpVar998403 * tmpVar998705;
        var tmpVar998910 = tmpVar998439 * tmpVar998714 + tmpVar998909;
        var tmpVar998911 = tmpVar998724 * tmpVar998910;
        var tmpVar998913 = tmpVar998906 + -tmpVar998911;
        var tmpVar998914 = tmpVar998860 * tmpVar998913;
        var tmpVar998917 = tmpVar998814 * tmpVar998831;
        var tmpVar998919 = tmpVar998832 * tmpVar998849 + -tmpVar998917;
        var tmpVar998920 = tmpVar998810 * tmpVar998841;
        var tmpVar998922 = tmpVar998919 + -tmpVar998920;
        var tmpVar998924 = tmpVar998392 * tmpVar998403;
        var tmpVar998926 = tmpVar998439 * tmpVar998790 + tmpVar998787 * tmpVar998924;
        var tmpVar998928 = tmpVar998922 + tmpVar998814 * tmpVar998926;
        var tmpVar998929 = tmpVar998793 * tmpVar998842;
        var tmpVar998931 = tmpVar998928 + -tmpVar998929;
        var tmpVar998934 = halfAlpha2Sin * tmpVar998624 + -tmpVar998840;
        var tmpVar998935 = tmpVar998810 * tmpVar998934;
        var tmpVar998936 = tmpVar998931 + tmpVar998935;
        var tmpVar998937 = tmpVar998874 * tmpVar998936;
        var tmpVar998938 = -tmpVar998914 + tmpVar998937;
        var tmpVar998939 = tmpVar998891 * tmpVar998938;
        var tmpVar998941 = tmpVar998914 + -tmpVar998937;
        var tmpVar998944 = tmpVar998861 + -tmpVar998889;
        var tmpVar998946 = tmpVar998938 * tmpVar998941 + tmpVar998890 * tmpVar998944;
        var tmpVar998949 = tmpVar998731 * tmpVar998756 + tmpVar998734 * tmpVar998773;
        var tmpVar998951 = tmpVar998744 * tmpVar998767 + tmpVar998949;
        var tmpVar998953 = -1 * tmpVar998777 * tmpVar998898;
        var tmpVar998954 = tmpVar998951 + tmpVar998953;
        var tmpVar998955 = tmpVar998724 * tmpVar998764;
        var tmpVar998957 = tmpVar998954 + -tmpVar998955;
        var tmpVar998958 = tmpVar998748 * tmpVar998910;
        var tmpVar998960 = tmpVar998957 + -tmpVar998958;
        var tmpVar998961 = tmpVar998860 * tmpVar998960;
        var tmpVar998963 = tmpVar998917 + tmpVar998920;
        var tmpVar998965 = -tmpVar998935 + tmpVar998963;
        var tmpVar998967 = tmpVar998820 * tmpVar998832 + tmpVar998965;
        var tmpVar998969 = -1 * tmpVar998842 * tmpVar998858;
        var tmpVar998970 = tmpVar998967 + tmpVar998969;
        var tmpVar998972 = -1 * tmpVar998814 * tmpVar998926;
        var tmpVar998973 = tmpVar998970 + tmpVar998972;
        var tmpVar998974 = tmpVar998874 * tmpVar998973;
        var tmpVar998975 = -tmpVar998961 + tmpVar998974;
        var tmpVar998977 = tmpVar998946 + tmpVar998975 * tmpVar998975;
        var tmpVar998978 = tmpVar998785 * tmpVar998936;
        var tmpVar998979 = tmpVar998888 * tmpVar998913;
        var tmpVar998981 = tmpVar998978 + -tmpVar998979;
        var tmpVar998983 = -tmpVar998978 + tmpVar998979;
        var tmpVar998985 = tmpVar998977 + tmpVar998981 * tmpVar998983;
        var tmpVar998987 = tmpVar998936 * tmpVar998960;
        var tmpVar998989 = tmpVar998913 * tmpVar998973 + -tmpVar998987;
        var tmpVar998991 = tmpVar998985 + tmpVar998989 * tmpVar998989;
        var tmpVar998992 = tmpVar998888 * tmpVar998960;
        var tmpVar998994 = tmpVar998785 * tmpVar998973;
        var tmpVar998995 = -tmpVar998992 + tmpVar998994;
        var tmpVar998997 = tmpVar998991 + tmpVar998995 * tmpVar998995;
        var tmpVar998998 = 1 / tmpVar998997;
        var tmpVar998999 = tmpVar998939 * tmpVar998998;
        var tmpVar999000 = -tmpVar998999;
        var tmpVar999001 = tmpVar998704 + tmpVar999000;
        var tmpVar999002 = tmpVar998891 * tmpVar998975;
        var tmpVar999003 = tmpVar998998 * tmpVar999002;
        var tmpVar999004 = tmpVar999001 + tmpVar999003;
        var tmpVar999005 = tmpVar998989 * tmpVar998998;
        var tmpVar999008 = 0.5 * tmpVar998978 + -0.5 * tmpVar998979;
        var tmpVar999010 = 0.5 * tmpVar998992 + tmpVar999008;
        var tmpVar999012 = -0.5 * tmpVar998994 + tmpVar999010;
        var tmpVar999013 = tmpVar999005 * tmpVar999012;
        var tmpVar999014 = -tmpVar999013;
        var tmpVar999015 = tmpVar999004 + tmpVar999014;
        var tmpVar999016 = tmpVar998585 * tmpVar998674;
        var tmpVar999017 = tmpVar998685 * tmpVar999016;
        var tmpVar999021 = -0.5 * tmpVar998605 + 0.5 * tmpVar998629;
        var tmpVar999023 = 0.5 * tmpVar998665 + tmpVar999021;
        var tmpVar999025 = -0.5 * tmpVar998667 + tmpVar999023;
        var tmpVar999026 = tmpVar998688 * tmpVar999025;
        var tmpVar999027 = -tmpVar999017 + tmpVar999026;
        var tmpVar999028 = tmpVar998585 * tmpVar998682;
        var tmpVar999029 = tmpVar998685 * tmpVar999028;
        var tmpVar999030 = tmpVar999027 + tmpVar999029;
        var tmpVar999032 = 0.5 + 0.5 * tmpVar998378;
        var tmpVar999034 = tmpVar998486 + tmpVar998804;
        var tmpVar999035 = -0.5 * tmpVar999034;
        var tmpVar999036 = -0.5 + tmpVar999035;
        var tmpVar999038 = data.Bx * tmpVar999032 + data.Ax * tmpVar999036;
        var tmpVar999042 = data.By * tmpVar999032 + data.Ay * tmpVar999036;
        var tmpVar999044 = tmpVar999030 * tmpVar999038 + tmpVar998701 * tmpVar999042;
        var tmpVar999047 = tmpVar998704 * tmpVar998704 + tmpVar999030 * tmpVar999030;
        var tmpVar999049 = 0.5 + 0.5 * tmpVar999047;
        var tmpVar999051 = data.Ay * data.Bx;
        var tmpVar999053 = data.Ax * data.By + -tmpVar999051;
        var tmpVar999055 = tmpVar999044 + tmpVar999049 * tmpVar999053;
        var tmpVar999056 = tmpVar999015 * tmpVar999055;
        var tmpVar999058 = 0.5 + tmpVar999035;
        var tmpVar999061 = 0.5 + -0.5 * tmpVar998378;
        var tmpVar999063 = tmpVar999032 * tmpVar999058 + tmpVar999036 * tmpVar999061;
        var tmpVar999066 = 0.5 + -0.5 * tmpVar999047;
        var tmpVar999067 = tmpVar999038 * tmpVar999066;
        var tmpVar999069 = tmpVar998704 * tmpVar999063 + -tmpVar999067;
        var tmpVar999071 = data.Ax * tmpVar999058;
        var tmpVar999073 = data.Bx * tmpVar999061 + -tmpVar999071;
        var tmpVar999075 = tmpVar999069 + tmpVar999049 * tmpVar999073;
        var tmpVar999076 = tmpVar998891 * tmpVar998981;
        var tmpVar999077 = tmpVar998998 * tmpVar999076;
        var tmpVar999078 = -tmpVar999077;
        var tmpVar999079 = tmpVar998891 * tmpVar998995;
        var tmpVar999080 = tmpVar998998 * tmpVar999079;
        var tmpVar999081 = tmpVar999078 + tmpVar999080;
        var tmpVar999084 = -0.5 * tmpVar998914 + 0.5 * tmpVar998937;
        var tmpVar999086 = 0.5 * tmpVar998961 + tmpVar999084;
        var tmpVar999088 = -0.5 * tmpVar998974 + tmpVar999086;
        var tmpVar999089 = tmpVar999005 * tmpVar999088;
        var tmpVar999090 = tmpVar999081 + tmpVar999089;
        var tmpVar999091 = tmpVar998704 * tmpVar999090;
        var tmpVar999094 = tmpVar998999 + -tmpVar999003;
        var tmpVar999095 = tmpVar999013 + tmpVar999094;
        var tmpVar999096 = tmpVar999030 * tmpVar999095;
        var tmpVar999097 = -tmpVar999091 + tmpVar999096;
        var tmpVar999099 = -tmpVar999056 + tmpVar999075 * tmpVar999097;
        var tmpVar999102 = tmpVar999053 * tmpVar999066 + tmpVar999030 * tmpVar999073;
        var tmpVar999104 = data.Ay * tmpVar999058;
        var tmpVar999106 = data.By * tmpVar999061 + -tmpVar999104;
        var tmpVar999107 = tmpVar998704 * tmpVar999106;
        var tmpVar999109 = tmpVar999102 + -tmpVar999107;
        var tmpVar999110 = tmpVar999015 * tmpVar999109;
        var tmpVar999112 = tmpVar999099 + -tmpVar999110;
        var tmpVar999114 = tmpVar999091 + -tmpVar999096;
        var tmpVar999116 = tmpVar999112 + tmpVar999075 * tmpVar999114;
        var tmpVar999119 = tmpVar999017 + -tmpVar999026;
        var tmpVar999121 = -tmpVar999029 + tmpVar999119;
        var tmpVar999122 = tmpVar999078 + tmpVar999121;
        var tmpVar999123 = tmpVar999080 + tmpVar999122;
        var tmpVar999124 = tmpVar999089 + tmpVar999123;
        var tmpVar999125 = tmpVar999055 * tmpVar999124;
        var tmpVar999129 = tmpVar999030 * tmpVar999063 + tmpVar999049 * tmpVar999106;
        var tmpVar999130 = tmpVar999042 * tmpVar999066;
        var tmpVar999132 = tmpVar999129 + -tmpVar999130;
        var tmpVar999133 = tmpVar999097 * tmpVar999132;
        var tmpVar999135 = -tmpVar999125 + -tmpVar999133;
        var tmpVar999136 = tmpVar999109 * tmpVar999124;
        var tmpVar999138 = tmpVar999114 * tmpVar999132;
        var tmpVar999140 = -tmpVar999136 + -tmpVar999138;
        var tmpVar999141 = tmpVar999135 + tmpVar999140;
        var tmpVar999143 = tmpVar999116 * tmpVar999116 + tmpVar999141 * tmpVar999141;
        var tmpVar999145 = 2 * 1 / tmpVar999143;
        var tmpVar999148 = tmpVar999075 * tmpVar999124 + tmpVar999015 * tmpVar999132;
        var tmpVar999151 = tmpVar999055 * tmpVar999114;
        var tmpVar999153 = tmpVar999097 * tmpVar999109 + -tmpVar999151;
        var tmpVar999155 = tmpVar999116 * tmpVar999148 + tmpVar999141 * tmpVar999153;
        p1X = tmpVar998701 + tmpVar999145 * tmpVar999155;

        var tmpVar999159 = -1 * tmpVar999135 * tmpVar999148;
        var tmpVar999160 = tmpVar999116 * tmpVar999153 + tmpVar999159;
        var tmpVar999162 = -1 * tmpVar999140 * tmpVar999148;
        var tmpVar999163 = tmpVar999160 + tmpVar999162;
        p1Y = tmpVar999121 + tmpVar999145 * tmpVar999163;

        var tmpVar999165 = tmpVar999000 + tmpVar999003;
        var tmpVar999166 = tmpVar999014 + tmpVar999165;
        var tmpVar999169 = tmpVar999038 * tmpVar999090 + tmpVar999042 * tmpVar999166;
        var tmpVar999172 = tmpVar999090 * tmpVar999090 + tmpVar999095 * tmpVar999095;
        var tmpVar999174 = 0.5 + 0.5 * tmpVar999172;
        var tmpVar999176 = tmpVar999169 + tmpVar999053 * tmpVar999174;
        var tmpVar999177 = tmpVar999015 * tmpVar999176;
        var tmpVar999181 = 0.5 + -0.5 * tmpVar999172;
        var tmpVar999183 = tmpVar999073 * tmpVar999090 + tmpVar999053 * tmpVar999181;
        var tmpVar999184 = tmpVar999095 * tmpVar999106;
        var tmpVar999186 = tmpVar999183 + -tmpVar999184;
        var tmpVar999187 = tmpVar999015 * tmpVar999186;
        var tmpVar999189 = -tmpVar999177 + -tmpVar999187;
        var tmpVar999192 = tmpVar999063 * tmpVar999095 + tmpVar999073 * tmpVar999174;
        var tmpVar999193 = tmpVar999038 * tmpVar999181;
        var tmpVar999195 = tmpVar999192 + -tmpVar999193;
        var tmpVar999199 = tmpVar999063 * tmpVar999090 + tmpVar999106 * tmpVar999174;
        var tmpVar999200 = tmpVar999042 * tmpVar999181;
        var tmpVar999202 = tmpVar999199 + -tmpVar999200;
        var tmpVar999204 = tmpVar999124 * tmpVar999195 + tmpVar999015 * tmpVar999202;
        var tmpVar999206 = tmpVar999124 * tmpVar999176;
        var tmpVar999208 = tmpVar999097 * tmpVar999202;
        var tmpVar999210 = -tmpVar999206 + -tmpVar999208;
        var tmpVar999211 = tmpVar999124 * tmpVar999186;
        var tmpVar999213 = tmpVar999114 * tmpVar999202;
        var tmpVar999215 = -tmpVar999211 + -tmpVar999213;
        var tmpVar999216 = tmpVar999210 + tmpVar999215;
        var tmpVar999218 = tmpVar999189 * tmpVar999189 + tmpVar999216 * tmpVar999216;
        var tmpVar999219 = 1 / tmpVar999218;
        var tmpVar999220 = tmpVar999204 * tmpVar999219;
        var tmpVar999222 = tmpVar999216 * tmpVar999219;
        var tmpVar999224 = tmpVar999114 * tmpVar999176;
        var tmpVar999226 = tmpVar999097 * tmpVar999186 + -tmpVar999224;
        var tmpVar999228 = tmpVar999189 * tmpVar999220 + tmpVar999222 * tmpVar999226;
        p2X = tmpVar999166 + 2 * tmpVar999228;

        var tmpVar999231 = tmpVar999077 + -tmpVar999080;
        var tmpVar999233 = -tmpVar999089 + tmpVar999231;
        var tmpVar999234 = tmpVar999189 * tmpVar999219;
        var tmpVar999237 = -1 * tmpVar999210 * tmpVar999220;
        var tmpVar999238 = tmpVar999226 * tmpVar999234 + tmpVar999237;
        var tmpVar999239 = tmpVar999204 * tmpVar999215;
        var tmpVar999240 = tmpVar999219 * tmpVar999239;
        var tmpVar999242 = tmpVar999238 + -tmpVar999240;
        p2Y = tmpVar999233 + 2 * tmpVar999242;

        //Finish GA-FuL MetaContext Code Generation, 2024-04-19T01:05:17.4209873+02:00
    }


    public double Ax { get; init; }

    public double Ay { get; init; }

    public double Bx { get; init; }

    public double By { get; init; }

    public double Alpha1 { get; init; }

    public double Alpha2 { get; init; }

    public double Beta1 { get; init; }

    public double Beta2 { get; init; }


    public Pair<LinFloat64Vector2D> SolveUsingNone()
    {
        SolveUsingNone(
            this,
            out var p1X, out var p1Y,
            out var p2X, out var p2Y
        );

        return new Pair<LinFloat64Vector2D>(
            LinFloat64Vector2D.Create(p1X, p1Y),
            LinFloat64Vector2D.Create(p2X, p2Y)
        );
    }

    public Pair<LinFloat64Vector2D> SolveUsingTrig()
    {
        SolveUsingTrig(
            this,
            out var p1X, out var p1Y,
            out var p2X, out var p2Y
        );

        return new Pair<LinFloat64Vector2D>(
            LinFloat64Vector2D.Create(p1X, p1Y),
            LinFloat64Vector2D.Create(p2X, p2Y)
        );
    }

    public Pair<LinFloat64Vector2D> SolveUsingTrigOpt()
    {
        SolveUsingTrigOpt(
            this,
            out var p1X, out var p1Y,
            out var p2X, out var p2Y
        );

        return new Pair<LinFloat64Vector2D>(
            LinFloat64Vector2D.Create(p1X, p1Y),
            LinFloat64Vector2D.Create(p2X, p2Y)
        );
    }

    public Pair<LinFloat64Vector2D> SolveUsingComplex()
    {
        SolveUsingComplex(
            this,
            out var p1X, out var p1Y,
            out var p2X, out var p2Y
        );

        return new Pair<LinFloat64Vector2D>(
            LinFloat64Vector2D.Create(p1X, p1Y),
            LinFloat64Vector2D.Create(p2X, p2Y)
        );
    }

    public Pair<LinFloat64Vector2D> SolveUsingComplexOpt()
    {
        SolveUsingComplexOpt(
            this,
            out var p1X, out var p1Y,
            out var p2X, out var p2Y
        );

        return new Pair<LinFloat64Vector2D>(
            LinFloat64Vector2D.Create(p1X, p1Y),
            LinFloat64Vector2D.Create(p2X, p2Y)
        );
    }

    public Pair<LinFloat64Vector2D> SolveUsingVGa()
    {
        SolveUsingVGa(
            this,
            out var p1X, out var p1Y,
            out var p2X, out var p2Y
        );

        return new Pair<LinFloat64Vector2D>(
            LinFloat64Vector2D.Create(p1X, p1Y),
            LinFloat64Vector2D.Create(p2X, p2Y)
        );
    }

    public Pair<LinFloat64Vector2D> SolveUsingVGaOpt()
    {
        SolveUsingVGaOpt(
            this,
            out var p1X, out var p1Y,
            out var p2X, out var p2Y
        );

        return new Pair<LinFloat64Vector2D>(
            LinFloat64Vector2D.Create(p1X, p1Y),
            LinFloat64Vector2D.Create(p2X, p2Y)
        );
    }

    public Pair<LinFloat64Vector2D> SolveUsingCGa()
    {
        SolveUsingCGa(
            this,
            out var p1X, out var p1Y,
            out var p2X, out var p2Y
        );

        return new Pair<LinFloat64Vector2D>(
            LinFloat64Vector2D.Create(p1X, p1Y),
            LinFloat64Vector2D.Create(p2X, p2Y)
        );
    }

    public Pair<LinFloat64Vector2D> SolveUsingCGaOpt()
    {
        SolveUsingCGaOpt(
            this,
            out var p1X, out var p1Y,
            out var p2X, out var p2Y
        );

        return new Pair<LinFloat64Vector2D>(
            LinFloat64Vector2D.Create(p1X, p1Y),
            LinFloat64Vector2D.Create(p2X, p2Y)
        );
    }


    public override string ToString()
    {
        var composer = new StringBuilder();

        var a = LinFloat64Vector2D.Create(Ax, Ay);
        var b = LinFloat64Vector2D.Create(Bx, By);

        composer
            .AppendLine("2D Hansen Problem Data")
            .AppendLine($"   A     : {a.ToTupleString()}")
            .AppendLine($"   B     : {b.ToTupleString()}")
            .AppendLine($"   alpha1: {Alpha1}")
            .AppendLine($"   beta1 : {Beta1}")
            .AppendLine($"   alpha2: {Alpha2}")
            .AppendLine($"   beta2 : {Beta2}")
            .AppendLine();

        var (p1, p2) = SolveUsingTrig();

        composer
            .AppendLine($"Trig: {{{p1.ToTupleString()}, {p2.ToTupleString()}}}")
            .AppendLine();

        (p1, p2) = SolveUsingComplex();

        composer
            .AppendLine($"Complex: {{{p1.ToTupleString()}, {p2.ToTupleString()}}}")
            .AppendLine();

        (p1, p2) = SolveUsingVGa();

        composer
            .AppendLine($"VGA: {{{p1.ToTupleString()}, {p2.ToTupleString()}}}")
            .AppendLine();

        (p1, p2) = SolveUsingCGa();

        composer
            .AppendLine($"CGA: {{{p1.ToTupleString()}, {p2.ToTupleString()}}}")
            .AppendLine();

        return composer.ToString();
    }
}