using System.Text;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Samples.Modeling.Geometry;

public readonly struct SnelliusPothenotProblemData2D
{
    public static SnelliusPothenotProblemData2D Create(LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3, LinFloat64Vector2D pointP)
    {
        var pointArray = new[] { point1, point2, point3 };

        var angleArray = new[]
        {
            (point1 - pointP).GetPolarAngle().DegreesValue,
            (point2 - pointP).GetPolarAngle().DegreesValue,
            (point3 - pointP).GetPolarAngle().DegreesValue
        };

        Array.Sort(angleArray, pointArray);

        var angleDiffArray = new[]
        {
            pointArray[0].GetDirectedAngleFromPoints(pointP, pointArray[1]).DegreesValue,
            pointArray[1].GetDirectedAngleFromPoints(pointP, pointArray[2]).DegreesValue,
            pointArray[2].GetDirectedAngleFromPoints(pointP, pointArray[0]).DegreesValue
        };

        var i = 0;
        var maxAngleDiff = angleDiffArray[0];

        if (angleDiffArray[1] > maxAngleDiff)
        {
            maxAngleDiff = angleDiffArray[1];
            i = 1;
        }

        if (angleDiffArray[2] > maxAngleDiff)
        {
            maxAngleDiff = angleDiffArray[2];
            i = 2;
        }

        //if (maxAngleDiff < 180)
        //    i = 2;

        var i0 = (i + 1) % 3;
        var i1 = (i + 2) % 3;
        var i2 = (i + 3) % 3;

        var pointC = pointArray[i0];
        var pointB = pointArray[i1];
        var pointA = pointArray[i2];

        var alpha = pointB.GetDirectedAngleFromPoints(pointP, pointA);
        var beta = pointC.GetDirectedAngleFromPoints(pointP, pointB);
        var gamma = point1.GetDirectedAngleFromPoints(pointP, point3);

        return new SnelliusPothenotProblemData2D()
        {
            Ax = pointA.X.ScalarValue,
            Ay = pointA.Y.ScalarValue,
            Bx = pointB.X.ScalarValue,
            By = pointB.Y.ScalarValue,
            Cx = pointC.X.ScalarValue,
            Cy = pointC.Y.ScalarValue,
            Alpha = alpha.RadiansValue,
            Beta = beta.RadiansValue
        };
    }

    //public static SnelliusPothenotProblemData2D Create(Float64Vector2D pointA, Float64Vector2D pointB, Float64Vector2D pointC, Float64Vector2D pointP)
    //{
    //    var ua = pointA - pointP;
    //    var ub = pointB - pointP;
    //    var uc = pointC - pointP;

    //    var thetaUa = ua.GetPolarAngle();
    //    var thetaUb = ub.GetPolarAngle();
    //    var thetaUc = uc.GetPolarAngle();

    //    var alpha = thetaUa - thetaUb;
    //    var beta = thetaUb - thetaUc;

    //    // If P is aligned with A and B or B and C, a new assignment of
    //    // the points is necessary.
    //    if (beta.IsZeroOrFullRotation())
    //    {
    //        alpha = thetaUb - thetaUa;
    //        beta = thetaUa - thetaUc;

    //        //Console.WriteLine(
    //        //    "The angle beta = 0 ---> The position of A and B will be interchanged."
    //        //);

    //        (pointA, pointB) = (pointB, pointA);
    //    }

    //    if (alpha.IsZeroOrFullRotation())
    //    {
    //        alpha = thetaUa - thetaUc;
    //        beta = thetaUc - thetaUb;

    //        //Console.WriteLine(
    //        //    "The angle alpha = 0 ---> The position of C and B will be interchanged."
    //        //);

    //        (pointC, pointB) = (pointB, pointC);
    //    }

    //    return new SnelliusPothenotProblemData2D()
    //    {
    //        Ax = pointA.X.Value,
    //        Ay = pointA.Y.Value,
    //        Bx = pointB.X.Value,
    //        By = pointB.Y.Value,
    //        Cx = pointC.X.Value,
    //        Cy = pointC.Y.Value,
    //        Alpha = alpha.Radians.Value,
    //        Beta = beta.Radians.Value
    //    };
    //}


    public static void SolveUsingNone(SnelliusPothenotProblemData2D data, out double pX, out double pY)
    {
        pX = 0;
        pY = 0;
    }

    /// <summary>
    /// Code-generated implementation, very fast to use
    /// </summary>
    /// <returns></returns>
    public static double SolveUsingVGa(SnelliusPothenotProblemData2D data, out double pX, out double pY)
    {
        //var alphaSin = Math.Sin(data.Alpha);
        //var betaSin = Math.Sin(data.Beta);

        //var alphaCos = Math.Sqrt(1 - alphaSin * alphaSin);
        //var betaCos = Math.Sqrt(1 - betaSin * betaSin);

        //if (((int)Math.Floor(2 * data.Alpha / Math.PI) % 4 + 4) % 4 is 1 or 2) alphaCos = -alphaCos;
        //if (((int)Math.Floor(2 * data.Beta / Math.PI) % 4 + 4) % 4 is 1 or 2) betaCos = -betaCos;

        //Begin GA-FuL MetaContext Code Generation, 2024-03-17T00:30:20.2733652+02:00
        //MetaContext: VGA
        //Input Variables: 10 used, 0 not used, 10 total.
        //Temp Variables: 29 sub-expressions, 0 generated temps, 29 total.
        //Target Temp Variables: 8 total.
        //Output Variables: 3 total.
        //Computations: 1.125 average, 36 total.
        //Memory Reads: 1.90625 average, 61 total.
        //Memory Writes: 32 total.
        //
        //MetaContext Binding Data:
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   1 = constant: '1'
        //   Ax = parameter: data.Ax
        //   Ay = parameter: data.Ay
        //   Bx = parameter: data.Bx
        //   By = parameter: data.By
        //   Cx = parameter: data.Cx
        //   Cy = parameter: data.Cy
        //   alphaCos = parameter: alphaCos
        //   alphaSin = parameter: alphaSin
        //   betaCos = parameter: betaCos
        //   betaSin = parameter: betaSin

        var temp0 = data.Cx - data.Ax;
        var temp1 = -data.By;
        var temp2 = data.Ay + temp1;
        var temp3 = 1 / Math.Tan(data.Alpha);
        temp2 *= temp3;
        temp0 += temp2;
        temp1 = data.Cy + temp1;
        var temp4 = 1 / Math.Tan(data.Beta);
        temp0 += temp4 * temp1;
        temp1 = -data.Ay;
        var temp5 = data.Cy + temp1;
        var temp6 = -data.Bx;
        var temp7 = data.Ax + temp6;
        temp3 = -temp7 * temp3;
        temp5 += temp3;
        temp6 = data.Cx + temp6;
        temp4 *= temp6;
        temp4 = temp5 - temp4;

        var dSig = temp0 * temp0 + temp4 * temp4;

        temp5 = 1 / dSig;
        temp6 = temp0 * temp5;
        temp0 = temp4 * (temp7 - temp2) + temp0 * (temp3 + data.By + temp1);

        pX = data.Bx + temp0 * temp4 * temp5;
        pY = data.By - temp6 * temp0;

        //Finish GA-FuL MetaContext Code Generation, 2024-03-17T00:30:20.3631106+02:00

        return dSig;
    }

    /// <summary>
    /// Code-generated implementation, very fast to use
    /// </summary>
    /// <returns></returns>
    public static void SolveUsingPGaPaco(SnelliusPothenotProblemData2D data, out double pX, out double pY)
    {
        var alphaSin = Math.Sin(data.Alpha);
        var betaSin = Math.Sin(data.Beta);

        var alphaCos = Math.Sqrt(1 - alphaSin * alphaSin);
        var betaCos = Math.Sqrt(1 - betaSin * betaSin);

        if (((int)Math.Floor(2 * data.Alpha / Math.PI) % 4 + 4) % 4 is 1 or 2) alphaCos = -alphaCos;
        if (((int)Math.Floor(2 * data.Beta / Math.PI) % 4 + 4) % 4 is 1 or 2) betaCos = -betaCos;

        //Begin GA-FuL MetaContext Code Generation, 2024-03-17T00:30:23.4928663+02:00
        //MetaContext: PGA_Paco
        //Input Variables: 10 used, 0 not used, 10 total.
        //Temp Variables: 329 sub-expressions, 0 generated temps, 329 total.
        //Target Temp Variables: 57 total.
        //Output Variables: 2 total.
        //Computations: 1.1570996978851964 average, 383 total.
        //Memory Reads: 2.0181268882175227 average, 668 total.
        //Memory Writes: 331 total.
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
        //   alphaCos = parameter: alphaCos
        //   alphaSin = parameter: alphaSin
        //   betaCos = parameter: betaCos
        //   betaSin = parameter: betaSin
        //   Ax = parameter: data.Ax
        //   Ay = parameter: data.Ay
        //   Bx = parameter: data.Bx
        //   By = parameter: data.By
        //   Cx = parameter: data.Cx
        //   Cy = parameter: data.Cy

        var temp0 = -1 * 1 / Math.Sqrt(2) * Math.Sqrt(1 - betaSin) * Math.Sign(betaCos);
        var temp1 = data.By * data.Cx;
        var temp2 = -data.Bx;
        var temp3 = data.Cy * temp2;
        temp1 = -(temp1 + temp3);
        temp3 = 0.5 * temp1;
        var temp4 = temp0 * temp3;
        var temp5 = -temp4;
        var temp6 = data.Cy - data.By;
        var temp7 = -0.5 * data.Cx;
        var temp8 = temp0 * temp7;
        var temp9 = temp6 * temp8;
        var temp10 = temp5 - temp9;
        var temp11 = data.Cx + temp2;
        var temp12 = 0.5 * data.Cy;
        var temp13 = temp0 * temp12;
        var temp14 = temp11 * temp13;
        temp10 -= temp14;
        var temp15 = -temp13;
        var temp16 = temp0 * temp11;
        var temp17 = temp0 * temp12;
        var temp18 = temp3 * temp17;
        var temp19 = temp16 + temp18;
        temp1 = -0.5 * temp1;
        var temp20 = temp13 * temp1;
        temp19 += temp20;
        var temp21 = data.By - data.Cy;
        var temp22 = 1 / Math.Sqrt(2) * Math.Sqrt(1 + betaSin) * Math.Sign(betaSin);
        var temp23 = temp21 * temp22;
        temp19 += temp23;
        var temp24 = -temp0;
        var temp25 = temp10 * temp15 + temp19 * temp24;
        var temp26 = temp0 * temp6;
        temp0 *= temp7;
        var temp27 = temp3 * temp0;
        var temp28 = temp26 + temp27;
        var temp29 = temp8 * temp1;
        temp28 += temp29;
        var temp30 = temp11 * temp22;
        temp28 += temp30;
        temp25 += temp22 * temp28;
        temp28 = temp11 * temp0;
        var temp31 = temp17 * temp21;
        var temp32 = -(temp28 + temp31);
        var temp33 = temp3 * temp22;
        temp32 -= temp33;
        var temp34 = -temp0;
        temp25 += temp32 * temp34;
        var temp35 = temp8 * temp11 + temp13 * temp21;
        temp35 = temp33 + temp35;
        var temp36 = -temp8;
        temp25 += temp35 * temp36;
        temp6 *= temp0;
        var temp37 = temp4 + temp6;
        var temp38 = temp11 * temp17;
        temp37 += temp38;
        var temp39 = -temp17;
        temp25 += temp37 * temp39;
        temp37 = -0.5 * data.Bx;
        temp7 += temp37;
        var temp40 = temp11 * temp7;
        var temp41 = 0.5 * data.By;
        temp12 += temp41;
        var temp42 = temp21 * temp12;
        temp40 = -(temp40 + temp42);
        temp42 = -2 * data.By;
        var temp43 = 2 * data.Cy + temp42;
        temp43 = temp3 * temp7 + temp43;
        temp43 = temp1 * temp7 + temp43;
        temp16 = -(temp16 + temp18);
        temp16 -= temp20;
        temp16 -= temp23;
        temp18 = -(temp26 + temp27);
        temp18 -= temp29;
        temp18 -= temp30;
        temp20 = temp39 * temp16 + temp34 * temp18;
        temp23 = temp28 + temp31;
        temp23 = temp33 + temp23;
        temp20 += temp22 * temp23;
        temp5 -= temp6;
        temp5 -= temp38;
        temp6 = temp20 + temp24 * temp5;
        temp0 = temp1 * temp0;
        temp8 = temp3 * temp8;
        temp0 = -(temp0 + temp8);
        temp6 += temp36 * temp0;
        temp8 = temp17 * temp1;
        temp13 = temp3 * temp13;
        temp8 = -(temp8 + temp13);
        temp6 += temp15 * temp8;
        temp13 = temp25 * temp40 + temp43 * temp6;
        temp7 = temp11 * temp7;
        temp11 = temp21 * temp12;
        temp7 = -(temp7 + temp11);
        temp11 = temp13 + temp25 * temp7;
        temp13 = temp15 * temp16 + temp36 * temp18;
        temp13 = temp22 * temp35 + temp13;
        temp10 = temp10 * temp24 + temp13;
        temp0 = temp34 * temp0 + temp10;
        temp0 = temp39 * temp8 + temp0;
        temp8 = temp11 + temp43 * temp0;
        temp10 = 2 * data.Bx;
        temp11 = -2 * data.Ax + temp10;
        temp13 = 0.5 * data.Ay;
        temp16 = temp41 + temp13;
        temp17 = data.Ax * data.By;
        temp20 = -data.Ay;
        temp21 = data.Bx * temp20;
        temp17 = -(temp17 + temp21);
        temp17 = 0.5 * temp17;
        temp21 = temp16 * temp17;
        temp11 -= temp21;
        temp23 = -0.5 * data.By;
        temp26 = -0.5 * data.Ay + temp23;
        temp26 = temp17 * temp26;
        temp11 -= temp26;
        temp2 = data.Ax + temp2;
        temp27 = 1 / Math.Sqrt(2) * Math.Sqrt(1 - alphaSin) * Math.Sign(alphaCos);
        temp28 = temp2 * temp27;
        temp13 *= temp27;
        temp29 = temp17 * temp13;
        temp30 = temp28 + temp29;
        temp31 = -temp13;
        temp33 = temp17 * temp31;
        temp30 += temp33;
        temp38 = 1 / Math.Sqrt(2) * Math.Sqrt(1 + alphaSin) * Math.Sign(alphaSin);
        temp20 = data.By + temp20;
        var temp44 = temp38 * temp20;
        temp30 += temp44;
        var temp45 = -temp27;
        var temp46 = temp20 * temp45;
        var temp47 = -0.5 * data.Ax;
        var temp48 = temp27 * temp47;
        var temp49 = temp17 * temp48;
        var temp50 = temp46 + temp49;
        var temp51 = -temp48;
        var temp52 = temp17 * temp51;
        temp50 += temp52;
        var temp53 = temp2 * temp38;
        temp50 += temp53;
        temp50 = temp30 * temp45 + temp38 * temp50;
        temp48 = temp2 * temp48;
        var temp54 = temp13 * temp20;
        var temp55 = -(temp48 + temp54);
        var temp56 = temp17 * temp38;
        temp55 -= temp56;
        temp50 += temp51 * temp55;
        temp48 += temp54;
        temp48 = temp56 + temp48;
        temp50 += temp51 * temp48;
        temp27 = temp17 * temp27;
        temp54 = temp20 * temp51;
        temp56 = temp27 + temp54;
        temp13 = temp2 * temp13;
        temp56 += temp13;
        temp50 += temp31 * temp56;
        temp27 = -(temp27 + temp54);
        temp13 = temp27 - temp13;
        temp27 = temp50 + temp31 * temp13;
        temp11 *= temp27;
        temp42 = 2 * data.Ay + temp42;
        temp47 = temp37 + temp47;
        temp42 += temp17 * temp47;
        temp50 = 0.5 * data.Bx;
        temp54 = 0.5 * data.Ax + temp50;
        temp17 = temp42 + temp17 * temp54;
        temp42 = -temp49;
        temp46 = temp42 - temp46;
        temp49 = -temp52;
        temp46 += temp49;
        temp46 -= temp53;
        temp30 = temp38 * temp30 + temp45 * temp46;
        temp30 = temp31 * temp55 + temp30;
        temp30 = temp31 * temp48 + temp30;
        temp30 = temp51 * temp13 + temp30;
        temp30 = temp51 * temp56 + temp30;
        temp52 = temp17 * temp30;
        temp11 = -(temp11 + temp52);
        temp11 = 0.5 * temp11;
        temp52 = temp8 * temp11;
        temp52 = -temp52;
        temp2 *= temp47;
        temp16 *= temp20;
        temp2 = -(temp2 + temp16);
        temp16 = temp27 * temp2;
        temp20 = -temp16;
        temp27 = -temp29;
        temp28 = temp27 - temp28;
        temp29 = -temp33;
        temp28 += temp29;
        temp28 -= temp44;
        temp28 = temp51 * temp46 + temp31 * temp28;
        temp28 = temp38 * temp48 + temp28;
        temp13 = temp45 * temp13 + temp28;
        temp28 = temp42 + temp49;
        temp13 += temp51 * temp28;
        temp27 += temp29;
        temp13 += temp31 * temp27;
        temp17 *= temp13;
        temp27 = -temp17;
        temp28 = temp20 + temp27;
        temp20 += temp28;
        temp20 = temp27 + temp20;
        temp10 = -2 * data.Cx + temp10;
        temp3 *= temp12;
        temp10 -= temp3;
        temp1 *= temp12;
        temp10 -= temp1;
        temp10 = temp25 * temp10;
        temp12 = temp22 * temp19 + temp24 * temp18;
        temp12 = temp32 * temp39 + temp12;
        temp12 = temp15 * temp35 + temp12;
        temp5 = temp34 * temp5 + temp12;
        temp4 += temp9;
        temp4 = temp14 + temp4;
        temp4 = temp5 + temp36 * temp4;
        temp5 = temp43 * temp4;
        temp5 = -(temp10 + temp5);
        temp5 = 0.5 * temp5;
        temp9 = temp20 * temp5;
        temp9 = -temp9;
        temp10 = temp52 + temp9;
        temp10 = temp52 + temp10;
        temp9 += temp10;
        temp10 = temp16 + temp17;
        temp10 = temp16 + temp10;
        temp10 = temp17 + temp10;
        temp12 = -2 * data.Bx;
        temp14 = 2 * data.Cx + temp12;
        temp3 += temp14;
        temp1 += temp3;
        temp3 = temp6 * temp1;
        temp6 = temp40 * temp4;
        temp3 = -(temp3 + temp6);
        temp0 *= temp1;
        temp0 = temp3 - temp0;
        temp1 = temp7 * temp4;
        temp0 -= temp1;
        temp1 = temp10 * temp0;
        temp3 = 2 * data.Ax + temp12;
        temp3 = temp21 + temp3;
        temp3 = temp26 + temp3;
        temp3 = temp13 * temp3;
        temp3 = -temp3;
        temp2 = temp30 * temp2;
        temp2 = -temp2;
        temp4 = temp3 + temp2;
        temp3 += temp4;
        temp2 += temp3;
        temp2 = -temp2;
        temp3 = temp8 * temp2;
        temp1 = -(temp1 + temp3);
        temp3 = 0.5 * temp1;
        temp4 = temp23 * temp3;
        temp6 = -temp4;
        temp7 = temp9 + temp6;
        temp8 = temp41 * temp3;
        temp10 = -temp8;
        temp7 += temp10;
        temp12 = temp11 * temp0;
        temp2 = temp5 * temp2;
        temp5 = temp12 + temp2;
        temp0 = temp11 * temp0;
        temp5 += temp0;
        temp5 = temp2 + temp5;
        temp11 = temp9 * temp9 + temp5 * temp5;
        temp13 = temp3 * temp3;
        temp11 -= temp13;
        temp11 = temp13 + temp11;
        temp11 = 1 / temp11;
        temp13 = temp3 * temp11;
        temp7 *= temp13;
        temp7 = -temp7;
        temp14 = temp50 * temp9;
        temp15 = temp23 * temp5 + temp14;
        temp16 = temp5 * temp11;
        temp17 = temp15 * temp16;
        temp17 = temp7 - temp17;
        temp18 = temp41 * temp9;
        temp19 = temp3 + temp18;
        temp20 = temp50 * temp5;
        temp19 += temp20;
        temp11 = temp9 * temp11;
        temp21 = temp19 * temp11;
        temp17 -= temp21;
        temp6 += temp10;
        temp10 = temp13 * temp6;
        temp10 = temp17 - temp10;
        temp7 += temp10;
        temp5 = temp23 * temp5 + temp14;
        temp10 = temp16 * temp5;
        temp7 -= temp10;
        temp10 = temp19 * temp11;
        temp7 -= temp10;
        temp6 = temp13 * temp6;
        temp6 = temp7 - temp6;
        temp4 -= temp9;
        temp4 = temp8 + temp4;
        temp4 = temp11 * temp4;
        temp2 = -temp2;
        temp7 = temp2 - temp12;
        temp0 = temp7 - temp0;
        temp0 = temp2 + temp0;
        temp2 = temp50 * temp3;
        temp2 = -temp2;
        temp0 += temp2;
        temp3 = temp37 * temp3;
        temp3 = -temp3;
        temp0 += temp3;
        temp7 = temp16 * temp0;
        temp4 = -(temp4 + temp7);
        temp1 = -0.5 * temp1 - temp18;
        temp1 -= temp20;
        temp7 = temp13 * temp1;
        temp4 -= temp7;
        temp7 = temp13 * temp19;
        temp4 -= temp7;
        temp4 = 1 / temp4;
        pY = temp6 * temp4;

        temp0 = temp13 * temp0;
        temp6 = temp15 * temp11 + temp0;
        temp1 = temp16 * temp1;
        temp6 += temp1;
        temp2 += temp3;
        temp2 = temp13 * temp2;
        temp3 = temp6 + temp2;
        temp3 = temp11 * temp5 + temp3;
        temp0 += temp3;
        temp0 = temp1 + temp0;
        temp0 = temp2 + temp0;
        pX = temp4 * temp0;

        //Finish GA-FuL MetaContext Code Generation, 2024-03-17T00:30:23.4944107+02:00
    }

    /// <summary>
    /// Code-generated implementation, very fast to use
    /// </summary>
    /// <returns></returns>
    public static void SolveUsingCGaPaco(SnelliusPothenotProblemData2D data, out double pX, out double pY)
    {
        var alphaSin = Math.Sin(data.Alpha);
        var betaSin = Math.Sin(data.Beta);

        var alphaCos = Math.Sqrt(1 - alphaSin * alphaSin);
        var betaCos = Math.Sqrt(1 - betaSin * betaSin);

        if (((int)Math.Floor(2 * data.Alpha / Math.PI) % 4 + 4) % 4 is 1 or 2) alphaCos = -alphaCos;
        if (((int)Math.Floor(2 * data.Beta / Math.PI) % 4 + 4) % 4 is 1 or 2) betaCos = -betaCos;

        //Begin GA-FuL MetaContext Code Generation, 2024-03-17T00:30:26.9088958+02:00
        //MetaContext: CGA_Paco
        //Input Variables: 10 used, 0 not used, 10 total.
        //Temp Variables: 637 sub-expressions, 0 generated temps, 637 total.
        //Target Temp Variables: 52 total.
        //Output Variables: 2 total.
        //Computations: 1.1017214397496087 average, 704 total.
        //Memory Reads: 1.949921752738654 average, 1246 total.
        //Memory Writes: 639 total.
        //
        //MetaContext Binding Data:
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   0.5 = constant: '0.5'
        //   -0.5 = constant: '-0.5'
        //   Rational[1, 2] = constant: 'Rational[1, 2]'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   Ax = parameter: data.Ax
        //   Ay = parameter: data.Ay
        //   Bx = parameter: data.Bx
        //   By = parameter: data.By
        //   Cx = parameter: data.Cx
        //   Cy = parameter: data.Cy
        //   alphaCos = parameter: alphaCos
        //   alphaSin = parameter: alphaSin
        //   betaCos = parameter: betaCos
        //   betaSin = parameter: betaSin

        var temp0 = 1 / Math.Sqrt(2) * Math.Sqrt(1 + betaSin) * Math.Sign(betaSin);
        var temp1 = 0.5 * data.Cx;
        var temp2 = temp0 * temp1;
        var temp3 = -1 * 1 / Math.Sqrt(2) * Math.Sqrt(1 - betaSin) * Math.Sign(betaCos);
        var temp4 = -temp3;
        var temp5 = 0.5 * data.Cy;
        var temp6 = temp4 * temp5;
        var temp7 = -(temp2 + temp6);
        var temp8 = -0.5 * data.Cy;
        var temp9 = temp3 * temp8;
        var temp10 = temp7 - temp9;
        var temp11 = -0.5 * data.Cx;
        var temp12 = temp0 * temp11;
        temp10 -= temp12;
        var temp13 = -data.Cy;
        var temp14 = data.By * data.Cx + data.Bx * temp13;
        temp2 += temp6;
        temp6 = temp9 + temp2;
        temp6 = temp12 + temp6;
        temp6 = temp14 * temp6;
        temp9 = temp10 * temp14;
        temp12 = temp6 + temp9;
        var temp15 = data.Cx * data.Cx;
        var temp16 = data.Cy * data.Cy;
        var temp17 = 1 + temp15 + temp16;
        temp17 = 0.5 * temp17;
        var temp18 = data.By * temp17;
        var temp19 = data.Bx * data.Bx;
        var temp20 = data.By * data.By;
        var temp21 = 1 + temp19 + temp20;
        var temp22 = 0.5 * temp21;
        var temp23 = temp13 * temp22;
        temp18 = -(temp18 + temp23);
        temp15 = -1 + temp15 + temp16;
        temp15 = 0.5 * temp15;
        temp16 = temp18 + data.By * temp15;
        temp18 = -1 + temp19 + temp20;
        temp19 = 0.5 * temp18;
        temp13 = temp16 + temp13 * temp19;
        temp16 = temp7 * temp11;
        temp1 *= temp3;
        temp5 = temp0 * temp5;
        temp20 = -(temp1 + temp5);
        temp23 = temp8 * temp20;
        var temp24 = temp16 + temp23;
        temp1 += temp5;
        temp5 = temp8 * temp1;
        temp24 += temp5;
        var temp25 = temp11 * temp2;
        temp24 += temp25;
        var temp26 = temp13 * temp24;
        temp12 += temp26;
        var temp27 = data.Bx * temp17;
        var temp28 = -data.Cx;
        var temp29 = temp22 * temp28;
        temp27 = -(temp27 + temp29);
        temp27 = data.Bx * temp15 + temp27;
        temp27 = temp19 * temp28 + temp27;
        temp7 *= temp8;
        temp29 = -temp7;
        var temp30 = temp11 * temp1;
        var temp31 = -temp30;
        var temp32 = temp29 + temp31;
        var temp33 = temp11 * temp20;
        var temp34 = -temp33;
        temp32 += temp34;
        temp2 = temp8 * temp2;
        var temp35 = -temp2;
        temp32 += temp35;
        var temp36 = temp27 * temp32;
        temp12 += temp36;
        var temp37 = temp10 * temp12;
        temp29 = temp4 + temp29;
        temp29 = temp31 + temp29;
        temp29 = temp34 + temp29;
        temp29 = temp35 + temp29;
        temp31 = temp27 * temp29;
        temp34 = temp6 + temp31;
        temp34 = temp9 + temp34;
        temp35 = temp0 + temp16;
        temp35 = temp23 + temp35;
        temp35 = temp5 + temp35;
        temp35 = temp25 + temp35;
        var temp38 = temp13 * temp35;
        temp34 += temp38;
        var temp39 = temp10 * temp34;
        temp37 = -(temp37 + temp39);
        temp4 *= temp11;
        temp11 = temp20 - temp4;
        temp0 *= temp8;
        temp8 = temp11 - temp0;
        temp11 = temp14 * temp8;
        temp20 = -temp11;
        temp3 += temp7;
        temp3 = temp30 + temp3;
        temp3 = temp33 + temp3;
        temp3 = temp2 + temp3;
        temp3 = temp13 * temp3;
        temp39 = temp20 - temp3;
        temp1 += temp4;
        temp0 += temp1;
        temp1 = temp14 * temp0;
        temp4 = -temp1;
        temp39 += temp4;
        var temp40 = temp27 * temp35;
        temp39 -= temp40;
        var temp41 = temp8 * temp39;
        temp37 -= temp41;
        temp41 = temp14 * temp24;
        temp0 = temp27 * temp0;
        var temp42 = -(temp41 + temp0);
        var temp43 = temp10 * temp13;
        temp42 -= temp43;
        var temp44 = temp14 * temp35;
        temp42 -= temp44;
        temp16 = -(temp16 + temp23);
        temp5 = temp16 - temp5;
        temp5 -= temp25;
        temp16 = temp42 * temp5;
        temp16 = temp37 - temp16;
        temp0 = temp41 + temp0;
        temp0 = temp43 + temp0;
        temp0 = temp44 + temp0;
        temp23 = temp35 * temp0;
        temp16 -= temp23;
        temp23 = temp14 * temp29;
        temp25 = temp13 * temp8;
        temp37 = temp23 + temp25;
        temp41 = temp10 * temp27;
        temp37 += temp41;
        temp14 *= temp32;
        temp37 += temp14;
        temp43 = temp29 * temp37;
        temp16 -= temp43;
        temp43 = temp32 * temp37;
        temp16 -= temp43;
        temp4 = temp20 + temp4;
        temp20 = temp24 * temp27;
        temp4 -= temp20;
        temp7 += temp30;
        temp7 = temp33 + temp7;
        temp2 += temp7;
        temp7 = temp13 * temp2;
        temp4 -= temp7;
        temp13 = temp8 * temp4;
        temp13 = temp16 - temp13;
        temp16 = -data.By;
        temp24 = data.Cy + temp16;
        temp21 = -0.5 * temp21;
        temp17 += temp21;
        temp18 = -0.5 * temp18;
        temp15 += temp18;
        temp27 = temp17 * temp17 - temp15 * temp15;
        temp28 = data.Bx + temp28;
        temp27 -= temp28 * temp28;
        temp27 -= temp24 * temp24;
        temp27 = 1 / Math.Sqrt(Math.Abs(temp27));
        temp24 *= temp27;
        temp24 = -temp24;
        temp23 = -(temp23 + temp25);
        temp23 -= temp41;
        temp14 = temp23 - temp14;
        temp23 = temp10 * temp14;
        temp23 = -temp23;
        temp6 = -temp6;
        temp25 = temp6 - temp31;
        temp9 = -temp9;
        temp25 += temp9;
        temp25 -= temp38;
        temp30 = temp29 * temp25;
        temp30 = temp23 - temp30;
        temp3 = temp11 + temp3;
        temp3 = temp1 + temp3;
        temp3 = temp40 + temp3;
        temp31 = temp35 * temp3;
        temp30 -= temp31;
        temp31 = temp8 * temp42;
        temp31 = -temp31;
        temp30 += temp31;
        temp33 = temp8 * temp0;
        temp33 = -temp33;
        temp30 += temp33;
        temp38 = temp10 * temp37;
        temp38 = -temp38;
        temp30 += temp38;
        temp1 = temp11 + temp1;
        temp1 = temp20 + temp1;
        temp1 = temp7 + temp1;
        temp7 = temp5 * temp1;
        temp7 = temp30 - temp7;
        temp11 = temp32 * temp12;
        temp7 -= temp11;
        temp7 = -temp7;
        temp11 = temp15 * temp27;
        temp11 = -temp11;
        temp15 = temp13 * temp24 + temp7 * temp11;
        temp17 *= temp27;
        temp17 = -temp17;
        temp20 = temp13 * temp24 + temp7 * temp17;
        temp30 = temp20 * temp20 - temp15 * temp15;
        temp27 = temp28 * temp27;
        temp7 *= temp27;
        temp28 = temp8 * temp14;
        temp40 = temp35 * temp34;
        temp41 = temp28 + temp40;
        temp43 = temp29 * temp3;
        temp41 += temp43;
        temp44 = temp10 * temp0;
        temp41 += temp44;
        var temp45 = temp10 * temp42;
        temp41 += temp45;
        var temp46 = temp8 * temp37;
        temp41 += temp46;
        var temp47 = temp2 * temp1;
        temp41 += temp47;
        var temp48 = temp12 * temp5;
        temp41 += temp48;
        temp41 = temp24 * temp41;
        temp7 = -(temp7 + temp41);
        temp7 = temp30 - temp7 * temp7;
        temp30 = -temp13 * temp17;
        temp30 = temp13 * temp11 + temp30;
        temp7 += temp30 * temp30;
        temp13 *= temp27;
        temp13 = -temp13;
        temp30 = -(temp28 + temp40);
        temp30 -= temp43;
        temp30 -= temp44;
        temp30 -= temp45;
        temp30 -= temp46;
        temp30 -= temp47;
        temp30 -= temp48;
        temp40 = temp17 * temp30;
        temp40 = temp13 - temp40;
        temp7 += temp40 * temp40;
        temp30 = temp11 * temp30;
        temp13 -= temp30;
        temp7 -= temp13 * temp13;
        temp28 += temp44;
        temp28 = temp45 + temp28;
        temp28 = temp46 + temp28;
        temp28 = temp34 * temp5 + temp28;
        temp28 = temp2 * temp3 + temp28;
        temp28 = temp29 * temp1 + temp28;
        temp12 = temp12 * temp35 + temp28;
        temp12 = temp24 * temp12;
        temp24 = temp8 * temp25;
        temp25 = temp10 * temp39;
        temp28 = temp24 + temp25;
        temp30 = temp29 * temp42;
        temp28 += temp30;
        temp0 *= temp2;
        temp2 = temp28 + temp0;
        temp14 = temp5 * temp14;
        temp2 += temp14;
        temp28 = temp35 * temp37;
        temp2 += temp28;
        temp4 = temp10 * temp4;
        temp2 += temp4;
        temp6 += temp9;
        temp6 -= temp26;
        temp6 -= temp36;
        temp8 *= temp6;
        temp2 += temp8;
        temp2 = temp11 * temp2;
        temp2 = -(temp12 + temp2);
        temp9 = -(temp24 + temp25);
        temp9 -= temp30;
        temp0 = temp9 - temp0;
        temp0 -= temp14;
        temp0 -= temp28;
        temp0 -= temp4;
        temp0 -= temp8;
        temp0 = temp17 * temp0;
        temp0 = temp2 - temp0;
        temp2 = temp32 * temp34;
        temp3 = temp5 * temp3;
        temp2 = -(temp2 + temp3);
        temp1 = temp35 * temp1;
        temp1 = temp2 - temp1;
        temp2 = temp29 * temp6;
        temp1 -= temp2;
        temp1 = temp23 + temp1;
        temp1 = temp31 + temp1;
        temp1 = temp33 + temp1;
        temp1 = temp38 + temp1;
        temp1 = -temp27 * temp1;
        temp0 -= temp1;
        temp0 = temp7 + temp0 * temp0;
        temp0 = 1 / Math.Sqrt(Math.Abs(temp0));
        temp1 = temp15 * temp0;
        temp2 = temp20 * temp0;
        temp1 = -0.5 * temp1 + -0.5 * temp2;
        temp2 = 1 / Math.Sqrt(2) * Math.Sqrt(1 + alphaSin) * Math.Sign(alphaSin);
        temp3 = 0.5 * data.Ax;
        temp4 = temp2 * temp3;
        temp5 = 1 / Math.Sqrt(2) * Math.Sqrt(1 - alphaSin) * Math.Sign(alphaCos);
        temp6 = -temp5;
        temp7 = 0.5 * data.Ay;
        temp8 = temp6 * temp7;
        temp9 = -(temp4 + temp8);
        temp10 = -0.5 * data.Ay;
        temp11 = temp5 * temp10;
        temp12 = temp9 - temp11;
        temp14 = -0.5 * data.Ax;
        temp15 = temp2 * temp14;
        temp12 -= temp15;
        temp17 = temp9 * temp10;
        temp20 = -temp17;
        temp23 = temp6 + temp20;
        temp3 *= temp5;
        temp7 = temp2 * temp7;
        temp24 = temp3 + temp7;
        temp25 = temp14 * temp24;
        temp26 = -temp25;
        temp23 += temp26;
        temp3 = -(temp3 + temp7);
        temp7 = temp14 * temp3;
        temp27 = -temp7;
        temp23 += temp27;
        temp4 += temp8;
        temp8 = temp10 * temp4;
        temp28 = -temp8;
        temp23 += temp28;
        temp29 = -data.Ay * data.Bx;
        temp29 = data.Ax * data.By + temp29;
        temp30 = temp23 * temp29;
        temp6 *= temp14;
        temp31 = temp3 - temp6;
        temp32 = temp2 * temp10;
        temp31 -= temp32;
        temp33 = data.Ax * data.Ax;
        temp34 = data.Ay * data.Ay;
        temp35 = 1 + temp33 + temp34;
        temp35 = 0.5 * temp35;
        temp36 = data.By * temp35;
        temp37 = data.Ay * temp21;
        temp36 = -(temp36 + temp37);
        temp33 = -1 + temp33 + temp34;
        temp33 = 0.5 * temp33;
        temp34 = temp36 + data.By * temp33;
        temp34 = data.Ay * temp18 + temp34;
        temp36 = temp31 * temp34;
        temp37 = -(temp30 + temp36);
        temp38 = data.Bx * temp35;
        temp39 = -data.Ax;
        temp22 *= temp39;
        temp22 = -(temp38 + temp22);
        temp22 = data.Bx * temp33 + temp22;
        temp22 = temp19 * temp39 + temp22;
        temp38 = temp12 * temp22;
        temp37 -= temp38;
        temp20 += temp26;
        temp20 = temp27 + temp20;
        temp20 = temp28 + temp20;
        temp26 = temp29 * temp20;
        temp27 = temp37 - temp26;
        temp28 = temp12 * temp27;
        temp28 = -temp28;
        temp11 += temp4;
        temp11 = temp15 + temp11;
        temp11 = temp29 * temp11;
        temp15 = -temp11;
        temp37 = temp23 * temp22;
        temp41 = temp15 - temp37;
        temp42 = temp12 * temp29;
        temp43 = -temp42;
        temp41 += temp43;
        temp9 *= temp14;
        temp2 += temp9;
        temp3 = temp10 * temp3;
        temp2 += temp3;
        temp10 *= temp24;
        temp2 += temp10;
        temp4 = temp14 * temp4;
        temp2 += temp4;
        temp14 = temp34 * temp2;
        temp41 -= temp14;
        temp44 = temp23 * temp41;
        temp44 = temp28 - temp44;
        temp45 = temp29 * temp31;
        temp5 += temp17;
        temp5 = temp25 + temp5;
        temp5 = temp7 + temp5;
        temp5 = temp8 + temp5;
        temp5 = temp34 * temp5;
        temp46 = temp45 + temp5;
        temp6 = temp24 + temp6;
        temp6 = temp32 + temp6;
        temp24 = temp29 * temp6;
        temp32 = temp46 + temp24;
        temp46 = temp22 * temp2;
        temp32 += temp46;
        temp47 = temp2 * temp32;
        temp44 -= temp47;
        temp47 = temp9 + temp3;
        temp47 = temp10 + temp47;
        temp47 = temp4 + temp47;
        temp48 = temp29 * temp47;
        temp6 = temp22 * temp6;
        var temp49 = -(temp48 + temp6);
        var temp50 = temp12 * temp34;
        temp49 -= temp50;
        temp29 *= temp2;
        temp49 -= temp29;
        var temp51 = temp31 * temp49;
        temp51 = -temp51;
        temp44 += temp51;
        temp6 = temp48 + temp6;
        temp6 = temp50 + temp6;
        temp6 = temp29 + temp6;
        temp29 = temp31 * temp6;
        temp29 = -temp29;
        temp44 += temp29;
        temp30 += temp36;
        temp30 = temp38 + temp30;
        temp26 += temp30;
        temp30 = temp12 * temp26;
        temp30 = -temp30;
        temp36 = temp44 + temp30;
        temp38 = temp45 + temp24;
        temp44 = temp22 * temp47;
        temp38 += temp44;
        temp17 += temp25;
        temp7 += temp17;
        temp7 = temp8 + temp7;
        temp8 = temp34 * temp7;
        temp17 = temp38 + temp8;
        temp3 = -(temp9 + temp3);
        temp3 -= temp10;
        temp3 -= temp4;
        temp4 = temp17 * temp3;
        temp4 = temp36 - temp4;
        temp9 = temp11 + temp42;
        temp10 = temp34 * temp47;
        temp9 += temp10;
        temp22 *= temp20;
        temp9 += temp22;
        temp25 = temp20 * temp9;
        temp4 -= temp25;
        temp4 = -temp4;
        temp25 = temp18 + temp33;
        temp33 = temp21 + temp35;
        temp34 = temp33 * temp33 - temp25 * temp25;
        temp35 = data.Bx + temp39;
        temp34 -= temp35 * temp35;
        temp16 = data.Ay + temp16;
        temp34 -= temp16 * temp16;
        temp34 = 1 / Math.Sqrt(Math.Abs(temp34));
        temp25 *= temp34;
        temp25 = -temp25;
        temp11 += temp37;
        temp11 = temp42 + temp11;
        temp11 = temp14 + temp11;
        temp14 = temp12 * temp11;
        temp36 = -temp45;
        temp5 = temp36 - temp5;
        temp24 = -temp24;
        temp5 += temp24;
        temp5 -= temp46;
        temp37 = temp31 * temp5;
        temp14 = -(temp14 + temp37);
        temp37 = temp49 * temp3;
        temp14 -= temp37;
        temp37 = temp2 * temp6;
        temp14 -= temp37;
        temp37 = temp23 * temp26;
        temp14 -= temp37;
        temp37 = temp20 * temp26;
        temp14 -= temp37;
        temp24 = temp36 + temp24;
        temp24 -= temp44;
        temp8 = temp24 - temp8;
        temp24 = temp31 * temp8;
        temp14 -= temp24;
        temp24 = temp12 * temp9;
        temp14 -= temp24;
        temp16 *= temp34;
        temp16 = -temp16;
        temp24 = temp4 * temp25 + temp14 * temp16;
        temp33 *= temp34;
        temp33 = -temp33;
        temp36 = temp14 * temp16 + temp4 * temp33;
        temp37 = temp36 * temp36 - temp24 * temp24;
        temp34 = temp35 * temp34;
        temp4 *= temp34;
        temp35 = temp31 * temp27;
        temp38 = temp2 * temp11;
        temp39 = temp35 + temp38;
        temp42 = temp23 * temp32;
        temp39 += temp42;
        temp44 = temp12 * temp6;
        temp39 += temp44;
        temp45 = temp12 * temp49;
        temp39 += temp45;
        temp46 = temp31 * temp26;
        temp39 += temp46;
        temp47 = temp7 * temp17;
        temp39 += temp47;
        temp48 = temp3 * temp9;
        temp39 += temp48;
        temp39 = temp16 * temp39;
        temp4 = -(temp4 + temp39);
        temp4 = temp37 - temp4 * temp4;
        temp37 = -temp14 * temp33;
        temp37 = temp25 * temp14 + temp37;
        temp4 += temp37 * temp37;
        temp14 *= temp34;
        temp14 = -temp14;
        temp37 = -(temp35 + temp38);
        temp37 -= temp42;
        temp37 -= temp44;
        temp37 -= temp45;
        temp37 -= temp46;
        temp37 -= temp47;
        temp37 -= temp48;
        temp38 = temp33 * temp37;
        temp38 = temp14 - temp38;
        temp4 += temp38 * temp38;
        temp37 = temp25 * temp37;
        temp14 -= temp37;
        temp4 -= temp14 * temp14;
        temp35 += temp44;
        temp35 = temp45 + temp35;
        temp35 = temp46 + temp35;
        temp35 = temp3 * temp11 + temp35;
        temp35 = temp32 * temp7 + temp35;
        temp35 = temp23 * temp17 + temp35;
        temp9 = temp2 * temp9 + temp35;
        temp9 = temp16 * temp9;
        temp16 = temp31 * temp41;
        temp5 = temp12 * temp5;
        temp35 = temp16 + temp5;
        temp37 = temp23 * temp49;
        temp35 += temp37;
        temp6 *= temp7;
        temp7 = temp35 + temp6;
        temp27 *= temp3;
        temp7 += temp27;
        temp26 = temp2 * temp26;
        temp7 += temp26;
        temp8 = temp12 * temp8;
        temp7 += temp8;
        temp12 = temp15 + temp43;
        temp10 = temp12 - temp10;
        temp10 -= temp22;
        temp12 = temp31 * temp10;
        temp7 += temp12;
        temp7 = temp25 * temp7;
        temp7 = -(temp9 + temp7);
        temp5 = -(temp16 + temp5);
        temp5 -= temp37;
        temp5 -= temp6;
        temp5 -= temp27;
        temp5 -= temp26;
        temp5 -= temp8;
        temp5 -= temp12;
        temp5 = temp33 * temp5;
        temp5 = temp7 - temp5;
        temp6 = temp28 + temp51;
        temp6 = temp29 + temp6;
        temp6 = temp30 + temp6;
        temp7 = temp20 * temp11;
        temp6 -= temp7;
        temp3 = temp32 * temp3;
        temp3 = temp6 - temp3;
        temp2 *= temp17;
        temp2 = temp3 - temp2;
        temp3 = temp23 * temp10;
        temp2 -= temp3;
        temp2 = -temp34 * temp2;
        temp2 = temp5 - temp2;
        temp2 = temp4 + temp2 * temp2;
        temp2 = 1 / Math.Sqrt(Math.Abs(temp2));
        temp3 = temp24 * temp2;
        temp4 = temp36 * temp2;
        temp5 = -0.5 * temp3 + -0.5 * temp4;
        temp6 = temp5 * temp5;
        temp7 = temp14 * temp2;
        temp2 = temp38 * temp2;
        temp8 = -0.5 * temp7 + -0.5 * temp2;
        temp8 *= temp8;
        temp9 = 1 + temp6 + temp8;
        temp9 = 0.5 * temp9;
        temp10 = temp1 * temp9;
        temp3 = 0.5 * temp3 + 0.5 * temp4;
        temp4 = temp1 * temp1;
        temp11 = temp13 * temp0;
        temp0 = temp40 * temp0;
        temp0 = -0.5 * temp11 + -0.5 * temp0;
        temp11 = temp0 * temp0;
        temp12 = 1 + temp4 + temp11;
        temp12 = 0.5 * temp12;
        temp13 = temp3 * temp12;
        temp10 = -(temp10 + temp13);
        temp6 = -1 + temp6 + temp8;
        temp6 = 0.5 * temp6;
        temp8 = temp10 + temp1 * temp6;
        temp4 = -1 + temp4 + temp11;
        temp4 = 0.5 * temp4;
        temp3 = temp8 + temp3 * temp4;
        temp8 = temp21 * temp3;
        temp2 = 0.5 * temp7 + 0.5 * temp2;
        temp1 = temp5 * temp0 + temp1 * temp2;
        temp5 = data.By * temp1;
        temp7 = -(temp8 + temp5);
        temp8 = temp9 * temp0;
        temp9 = temp12 * temp2;
        temp8 = -(temp8 + temp9);
        temp0 = temp0 * temp6 + temp8;
        temp0 = temp4 * temp2 + temp0;
        temp2 = temp3 * temp3 + temp0 * temp0;
        temp2 = 1 / temp2;
        temp4 = temp1 * temp2;
        temp5 = temp18 * temp3 + temp5;
        temp5 = temp7 * temp4 + temp4 * temp5;
        temp6 = data.Bx * temp3 + data.By * temp0;
        temp7 = temp0 * temp2;
        temp5 += temp6 * temp7;
        temp8 = temp21 * temp1;
        temp9 = data.By * temp3;
        temp10 = -(temp8 + temp9);
        temp11 = -data.Bx;
        temp12 = temp0 * temp11;
        temp10 -= temp12;
        temp13 = temp19 * temp1;
        temp10 -= temp13;
        temp2 = temp3 * temp2;
        pY = temp5 + temp10 * temp2;

        temp1 *= temp11;
        temp3 = temp21 * temp0 + temp1;
        temp2 = temp6 * temp2 + temp4 * temp3;
        temp0 = temp18 * temp0;
        temp0 = -(temp1 + temp0);
        temp0 = temp2 + temp4 * temp0;
        temp1 = temp8 + temp9;
        temp1 = temp12 + temp1;
        temp1 = temp13 + temp1;
        pX = temp0 + temp7 * temp1;

        //Finish GA-FuL MetaContext Code Generation, 2024-03-17T00:30:26.9107471+02:00
    }

    /// <summary>
    /// Code-generated implementation, very fast to use
    /// </summary>
    /// <returns></returns>
    public static void SolveUsingCGaCollinsParallel(SnelliusPothenotProblemData2D data, out double pX, out double pY)
    {
        var alphaSin = Math.Sin(data.Alpha);
        var betaSin = Math.Sin(data.Beta);

        var alphaCos = Math.Sqrt(1 - alphaSin * alphaSin);
        var betaCos = Math.Sqrt(1 - betaSin * betaSin);

        if (((int)Math.Floor(2 * data.Alpha / Math.PI) % 4 + 4) % 4 is 1 or 2) alphaCos = -alphaCos;
        if (((int)Math.Floor(2 * data.Beta / Math.PI) % 4 + 4) % 4 is 1 or 2) betaCos = -betaCos;

        //Begin GA-FuL MetaContext Code Generation, 2024-03-17T00:30:28.4478332+02:00
        //MetaContext: CGA_Collins_Parallel
        //Input Variables: 8 used, 2 not used, 10 total.
        //Temp Variables: 290 sub-expressions, 0 generated temps, 290 total.
        //Target Temp Variables: 46 total.
        //Output Variables: 2 total.
        //Computations: 1.0582191780821917 average, 309 total.
        //Memory Reads: 1.9109589041095891 average, 558 total.
        //Memory Writes: 292 total.
        //
        //MetaContext Binding Data:
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   0.5 = constant: '0.5'
        //   -0.5 = constant: '-0.5'
        //   Rational[1, 2] = constant: 'Rational[1, 2]'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   Ax = parameter: data.Ax
        //   Ay = parameter: data.Ay
        //   Bx = parameter: data.Bx
        //   By = parameter: data.By
        //   Cx = parameter: data.Cx
        //   Cy = parameter: data.Cy
        //   betaCos = parameter: betaCos
        //   betaSin = parameter: betaSin
        //   alphaCos = parameter: alphaCos
        //   alphaSin = parameter: alphaSin

        var temp0 = data.Ax * data.Ax;
        var temp1 = data.Ay * data.Ay;
        var temp2 = 1 + temp0 + temp1;
        temp2 = 0.5 * temp2;
        var temp3 = data.Cx * temp2;
        var temp4 = -data.Ax;
        var temp5 = data.Cx * data.Cx;
        var temp6 = data.Cy * data.Cy;
        var temp7 = 1 + temp5 + temp6;
        var temp8 = 0.5 * temp4 * temp7;
        temp3 = -(temp3 + temp8);
        temp0 = -1 + temp0 + temp1;
        temp0 = 0.5 * temp0;
        temp1 = data.Cx * temp0;
        temp8 = -(temp3 + temp1);
        temp5 = -1 + temp5 + temp6;
        temp4 = 0.5 * temp4 * temp5;
        temp6 = temp8 - temp4;
        temp6 = -temp6;
        temp8 = 1 / Math.Sqrt(2) * Math.Sqrt(1 + betaCos) * Math.Sign(betaCos);
        var temp9 = 0.5 * data.Ax;
        var temp10 = temp8 * temp9;
        var temp11 = 1 / Math.Sqrt(2) * Math.Sqrt(1 - betaCos) * Math.Sign(betaSin);
        var temp12 = 0.5 * data.Ay;
        var temp13 = temp11 * temp12;
        var temp14 = -(temp10 + temp13);
        var temp15 = -temp11;
        var temp16 = -0.5 * data.Ay;
        var temp17 = temp15 * temp16;
        var temp18 = temp14 - temp17;
        var temp19 = -0.5 * data.Ax;
        var temp20 = temp8 * temp19;
        temp18 -= temp20;
        temp10 += temp13;
        temp13 = temp17 + temp10;
        temp13 = temp20 + temp13;
        temp17 = data.Ax * data.Cy;
        temp20 = -data.Ay * data.Cx;
        var temp21 = temp17 + temp20;
        temp13 *= temp21;
        temp1 = temp3 + temp1;
        temp1 = temp4 + temp1;
        temp3 = temp14 * temp16;
        temp4 = -temp3;
        var temp22 = temp11 + temp4;
        temp9 *= temp15;
        temp12 = temp8 * temp12;
        var temp23 = -(temp9 + temp12);
        var temp24 = temp19 * temp23;
        var temp25 = -temp24;
        temp22 += temp25;
        var temp26 = temp16 * temp10;
        var temp27 = -temp26;
        temp22 += temp27;
        temp9 += temp12;
        temp12 = temp19 * temp9;
        var temp28 = -temp12;
        temp22 += temp28;
        var temp29 = temp1 * temp22;
        var temp30 = temp13 + temp29;
        var temp31 = temp18 * temp21;
        temp30 += temp31;
        temp14 *= temp19;
        var temp32 = temp8 + temp14;
        var temp33 = temp16 * temp23;
        temp32 += temp33;
        var temp34 = temp16 * temp9;
        temp32 += temp34;
        temp10 = temp19 * temp10;
        temp32 += temp10;
        temp2 = data.Cy * temp2;
        temp7 = -0.5 * data.Ay * temp7;
        temp2 = -(temp2 + temp7);
        temp0 = data.Cy * temp0;
        temp7 = temp2 + temp0;
        temp5 = -0.5 * data.Ay * temp5;
        temp7 += temp5;
        var temp35 = temp32 * temp7;
        temp30 += temp35;
        var temp36 = temp18 * temp30;
        temp11 *= temp19;
        temp19 = temp23 - temp11;
        temp8 *= temp16;
        temp16 = temp19 - temp8;
        temp19 = temp21 * temp16;
        temp23 = -temp19;
        temp15 += temp3;
        temp15 = temp24 + temp15;
        temp15 = temp26 + temp15;
        temp15 = temp12 + temp15;
        temp15 = temp7 * temp15;
        var temp37 = temp23 - temp15;
        temp9 += temp11;
        temp8 += temp9;
        temp9 = temp21 * temp8;
        temp11 = -temp9;
        temp37 += temp11;
        var temp38 = temp1 * temp32;
        temp37 -= temp38;
        var temp39 = temp16 * temp37;
        var temp40 = temp36 + temp39;
        var temp41 = temp14 + temp33;
        temp41 = temp34 + temp41;
        temp41 = temp10 + temp41;
        var temp42 = temp21 * temp41;
        temp8 = temp1 * temp8;
        var temp43 = -(temp42 + temp8);
        var temp44 = temp18 * temp7;
        temp43 -= temp44;
        var temp45 = temp21 * temp32;
        temp43 -= temp45;
        temp14 = -(temp14 + temp33);
        temp14 -= temp34;
        temp10 = temp14 - temp10;
        temp14 = temp43 * temp10;
        temp33 = temp40 + temp14;
        temp8 = temp42 + temp8;
        temp8 = temp44 + temp8;
        temp8 = temp45 + temp8;
        temp34 = temp32 * temp8;
        temp33 += temp34;
        temp40 = temp21 * temp22;
        temp42 = temp7 * temp16;
        temp44 = temp40 + temp42;
        temp45 = temp18 * temp1;
        temp44 += temp45;
        temp4 += temp25;
        temp4 = temp27 + temp4;
        temp4 = temp28 + temp4;
        temp21 *= temp4;
        temp25 = temp44 + temp21;
        temp27 = temp22 * temp25;
        temp28 = temp33 + temp27;
        temp33 = temp4 * temp25;
        temp28 += temp33;
        temp11 = temp23 + temp11;
        temp23 = temp1 * temp41;
        temp11 -= temp23;
        temp3 += temp24;
        temp3 = temp26 + temp3;
        temp3 = temp12 + temp3;
        temp12 = temp7 * temp3;
        temp11 -= temp12;
        temp24 = temp16 * temp11;
        temp26 = temp28 + temp24;
        temp28 = temp13 + temp31;
        temp41 = temp7 * temp41;
        temp28 += temp41;
        temp1 *= temp4;
        temp28 += temp1;
        temp44 = temp18 * temp28;
        temp26 += temp44;
        temp36 = -(temp36 + temp39);
        temp14 = temp36 - temp14;
        temp14 -= temp34;
        temp14 -= temp27;
        temp14 -= temp33;
        temp14 -= temp24;
        temp14 -= temp44;
        temp14 = temp26 + temp14;
        temp24 = data.Bx * data.Bx;
        temp26 = data.By * data.By;
        temp27 = 1 + temp24 + temp26;
        temp27 = 0.5 * temp27;
        temp33 = temp14 * temp27;
        temp34 = -(temp40 + temp42);
        temp34 -= temp45;
        temp21 = temp34 - temp21;
        temp34 = temp16 * temp21;
        temp30 = temp32 * temp30;
        temp30 = -(temp34 + temp30);
        temp15 = temp19 + temp15;
        temp15 = temp9 + temp15;
        temp15 = temp38 + temp15;
        temp34 = temp22 * temp15;
        temp30 -= temp34;
        temp34 = temp18 * temp8;
        temp30 -= temp34;
        temp34 = temp18 * temp43;
        temp30 -= temp34;
        temp34 = temp16 * temp25;
        temp30 -= temp34;
        temp9 = temp19 + temp9;
        temp9 = temp23 + temp9;
        temp9 = temp12 + temp9;
        temp12 = temp3 * temp9;
        temp12 = temp30 - temp12;
        temp19 = temp10 * temp28;
        temp12 -= temp19;
        temp19 = -temp12;
        temp23 = data.Bx * temp19;
        temp23 = -temp23;
        temp30 = temp23 - temp33;
        temp33 = temp18 * temp21;
        temp13 = -temp13;
        temp29 = temp13 - temp29;
        temp31 = -temp31;
        temp29 += temp31;
        temp29 -= temp35;
        temp34 = temp22 * temp29;
        temp33 = -(temp33 + temp34);
        temp15 = temp32 * temp15;
        temp15 = temp33 - temp15;
        temp33 = temp16 * temp43;
        temp15 -= temp33;
        temp33 = temp16 * temp8;
        temp15 -= temp33;
        temp33 = temp18 * temp25;
        temp15 -= temp33;
        temp9 = temp10 * temp9;
        temp9 = temp15 - temp9;
        temp4 *= temp28;
        temp4 = temp9 - temp4;
        temp9 = data.By * temp4;
        temp9 = -temp9;
        temp15 = temp30 + temp9;
        temp17 = -(temp17 + temp20);
        temp20 = temp27 * temp4;
        temp24 = -1 + temp24 + temp26;
        temp24 = 0.5 * temp24;
        temp4 = -temp4 * temp24;
        temp4 = -(temp20 + temp4);
        temp20 = temp17 * temp4;
        temp26 = temp6 * temp15 + temp20;
        temp12 = temp27 * temp12 + temp19 * temp24;
        temp19 = temp6 * temp12;
        temp4 = temp7 * temp4;
        temp4 = -(temp19 + temp4);
        temp4 = -(temp26 * temp26 + temp4 * temp4);
        temp9 = temp23 + temp9;
        temp14 *= temp24;
        temp9 -= temp14;
        temp14 = temp20 + temp6 * temp9;
        temp4 += temp14 * temp14;
        temp0 = -(temp2 + temp0);
        temp0 -= temp5;
        temp2 = temp15 * temp0;
        temp5 = temp17 * temp12;
        temp5 = -temp5;
        temp2 = temp5 - temp2;
        temp4 -= temp2 * temp2;
        temp0 = temp9 * temp0;
        temp0 = temp5 - temp0;
        temp4 += temp0 * temp0;
        temp5 = -temp17;
        temp9 = temp15 * temp17 + temp5 * temp9;
        temp4 += temp9 * temp9;
        temp9 = temp10 * temp21;
        temp10 = temp22 * temp43;
        temp12 = temp9 + temp10;
        temp15 = temp16 * temp29;
        temp12 += temp15;
        temp19 = temp18 * temp37;
        temp12 += temp19;
        temp3 = temp8 * temp3;
        temp8 = temp12 + temp3;
        temp12 = temp32 * temp25;
        temp8 += temp12;
        temp11 = temp18 * temp11;
        temp8 += temp11;
        temp13 += temp31;
        temp13 -= temp41;
        temp1 = temp13 - temp1;
        temp1 *= temp16;
        temp8 += temp1;
        temp9 = -(temp9 + temp10);
        temp9 -= temp15;
        temp9 -= temp19;
        temp3 = temp9 - temp3;
        temp3 -= temp12;
        temp3 -= temp11;
        temp1 = temp3 - temp1;
        temp1 = temp8 + temp1;
        temp3 = data.By * temp1;
        temp3 *= temp6;
        temp6 = data.Bx * temp1;
        temp6 = -temp6 * temp7;
        temp3 = -(temp3 + temp6);
        temp6 = temp1 * temp24;
        temp5 *= temp6;
        temp3 -= temp5;
        temp1 *= temp27;
        temp1 *= temp17;
        temp1 = temp3 - temp1;
        temp1 = temp4 + temp1 * temp1;
        temp1 = 1 / Math.Sqrt(Math.Abs(temp1));
        temp3 = temp1 * temp26;
        temp4 = temp1 * temp14;
        pX = -0.5 * temp3 + -0.5 * temp4;

        temp2 *= temp1;
        temp0 *= temp1;
        pY = -0.5 * temp2 + -0.5 * temp0;

        //Finish GA-FuL MetaContext Code Generation, 2024-03-17T00:30:28.4485051+02:00
    }

    /// <summary>
    /// Code-generated implementation, very fast to use
    /// </summary>
    /// <returns></returns>
    public static void SolveUsingCGaCollins(SnelliusPothenotProblemData2D data, out double pX, out double pY)
    {
        if ((data.Alpha + data.Beta - Math.PI).IsNearZero())
        {
            SolveUsingCGaCollinsParallel(data, out pX, out pY);

            return;
        }

        var alphaSin = Math.Sin(data.Alpha);
        var betaSin = Math.Sin(data.Beta);

        var alphaCos = Math.Sqrt(1 - alphaSin * alphaSin);
        var betaCos = Math.Sqrt(1 - betaSin * betaSin);

        if (((int)Math.Floor(2 * data.Alpha / Math.PI) % 4 + 4) % 4 is 1 or 2) alphaCos = -alphaCos;
        if (((int)Math.Floor(2 * data.Beta / Math.PI) % 4 + 4) % 4 is 1 or 2) betaCos = -betaCos;

        //Begin GA-FuL MetaContext Code Generation, 2024-03-17T00:30:31.3064995+02:00
        //MetaContext: CGA_Collins
        //Input Variables: 10 used, 0 not used, 10 total.
        //Temp Variables: 588 sub-expressions, 0 generated temps, 588 total.
        //Target Temp Variables: 88 total.
        //Output Variables: 6 total.
        //Computations: 1.069023569023569 average, 635 total.
        //Memory Reads: 1.9427609427609427 average, 1154 total.
        //Memory Writes: 594 total.
        //
        //MetaContext Binding Data:
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   0.5 = constant: '0.5'
        //   -0.5 = constant: '-0.5'
        //   Rational[1, 2] = constant: 'Rational[1, 2]'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   Ax = parameter: data.Ax
        //   Ay = parameter: data.Ay
        //   Bx = parameter: data.Bx
        //   By = parameter: data.By
        //   Cx = parameter: data.Cx
        //   Cy = parameter: data.Cy
        //   alphaCos = parameter: alphaCos
        //   alphaSin = parameter: alphaSin
        //   betaCos = parameter: betaCos
        //   betaSin = parameter: betaSin

        var temp0 = data.Ax * data.Ax;
        var temp1 = data.Ay * data.Ay;
        var temp2 = 1 + temp0 + temp1;
        temp2 = 0.5 * temp2;
        var temp3 = data.Cx * temp2;
        var temp4 = data.Cx * data.Cx;
        var temp5 = data.Cy * data.Cy;
        var temp6 = 1 + temp4 + temp5;
        var temp7 = 0.5 * temp6;
        var temp8 = -data.Ax;
        var temp9 = temp7 * temp8;
        var temp10 = -(temp3 + temp9);
        temp0 = -1 + temp0 + temp1;
        temp1 = 0.5 * temp0;
        var temp11 = data.Cx * temp1;
        var temp12 = temp10 + temp11;
        temp4 = -1 + temp4 + temp5;
        temp5 = 0.5 * temp4;
        temp8 *= temp5;
        temp12 += temp8;
        var temp13 = -0.5 * data.Cy;
        var temp14 = 1 / Math.Sqrt(2) * Math.Sqrt(1 + alphaCos) * Math.Sign(alphaCos);
        var temp15 = 0.5 * data.Cx;
        var temp16 = temp14 * temp15;
        var temp17 = 1 / Math.Sqrt(2) * Math.Sqrt(1 - alphaCos) * Math.Sign(alphaSin);
        var temp18 = -temp17;
        var temp19 = 0.5 * data.Cy;
        var temp20 = temp18 * temp19;
        var temp21 = -(temp16 + temp20);
        var temp22 = temp13 * temp21;
        var temp23 = -temp22;
        var temp24 = -0.5 * data.Cx;
        temp15 *= temp17;
        temp19 = temp14 * temp19;
        var temp25 = temp15 + temp19;
        var temp26 = temp24 * temp25;
        var temp27 = -temp26;
        var temp28 = temp23 + temp27;
        temp15 = -(temp15 + temp19);
        temp19 = temp24 * temp15;
        var temp29 = -temp19;
        temp28 += temp29;
        temp16 += temp20;
        temp20 = temp13 * temp16;
        var temp30 = -temp20;
        temp28 += temp30;
        var temp31 = temp12 * temp28;
        var temp32 = -data.Ay * data.Cx;
        temp32 = data.Ax * data.Cy + temp32;
        var temp33 = temp13 * temp17;
        var temp34 = temp16 + temp33;
        var temp35 = temp14 * temp24;
        temp34 += temp35;
        temp34 = temp32 * temp34;
        var temp36 = temp31 + temp34;
        temp33 = temp21 - temp33;
        temp33 -= temp35;
        temp35 = temp32 * temp33;
        temp36 += temp35;
        var temp37 = data.Cy * temp2;
        temp6 = -0.5 * data.Ay * temp6;
        temp6 = -(temp37 + temp6);
        temp1 = data.Cy * temp1;
        temp37 = temp6 + temp1;
        temp4 = -0.5 * data.Ay * temp4;
        temp37 += temp4;
        temp21 *= temp24;
        var temp38 = temp13 * temp15;
        var temp39 = temp21 + temp38;
        var temp40 = temp13 * temp25;
        temp39 += temp40;
        temp16 = temp24 * temp16;
        temp39 += temp16;
        var temp41 = temp37 * temp39;
        temp36 += temp41;
        var temp42 = temp33 * temp36;
        temp23 = temp18 + temp23;
        temp23 = temp27 + temp23;
        temp23 = temp29 + temp23;
        temp23 = temp30 + temp23;
        temp27 = temp12 * temp23;
        temp29 = temp34 + temp27;
        temp29 = temp35 + temp29;
        temp30 = temp14 + temp21;
        temp30 = temp38 + temp30;
        temp30 = temp40 + temp30;
        temp30 = temp16 + temp30;
        var temp43 = temp37 * temp30;
        temp29 += temp43;
        var temp44 = temp33 * temp29;
        temp42 = -(temp42 + temp44);
        temp18 *= temp24;
        temp15 -= temp18;
        temp13 *= temp14;
        temp14 = temp15 - temp13;
        temp15 = temp32 * temp14;
        temp24 = -temp15;
        temp17 += temp22;
        temp17 = temp26 + temp17;
        temp17 = temp19 + temp17;
        temp17 = temp20 + temp17;
        temp17 = temp37 * temp17;
        temp44 = temp24 - temp17;
        temp18 = temp25 + temp18;
        temp13 += temp18;
        temp18 = temp32 * temp13;
        temp25 = -temp18;
        temp44 += temp25;
        var temp45 = temp12 * temp30;
        temp44 -= temp45;
        var temp46 = temp14 * temp44;
        temp42 -= temp46;
        temp46 = temp32 * temp39;
        temp13 = temp12 * temp13;
        var temp47 = -(temp46 + temp13);
        var temp48 = temp33 * temp37;
        temp47 -= temp48;
        var temp49 = temp32 * temp30;
        temp47 -= temp49;
        temp21 = -(temp21 + temp38);
        temp21 -= temp40;
        temp16 = temp21 - temp16;
        temp21 = temp47 * temp16;
        temp21 = temp42 - temp21;
        temp13 = temp46 + temp13;
        temp13 = temp48 + temp13;
        temp13 = temp49 + temp13;
        temp38 = temp30 * temp13;
        temp21 -= temp38;
        temp38 = temp32 * temp23;
        temp40 = temp37 * temp14;
        temp42 = temp38 + temp40;
        temp46 = temp12 * temp33;
        temp42 += temp46;
        temp48 = temp28 * temp32;
        temp42 += temp48;
        temp49 = temp23 * temp42;
        temp21 -= temp49;
        temp49 = temp28 * temp42;
        temp21 -= temp49;
        temp24 += temp25;
        temp25 = temp12 * temp39;
        temp24 -= temp25;
        temp22 += temp26;
        temp19 += temp22;
        temp19 = temp20 + temp19;
        temp20 = temp37 * temp19;
        temp22 = temp24 - temp20;
        temp24 = temp14 * temp22;
        temp21 -= temp24;
        temp24 = 1 / Math.Sqrt(2) * Math.Sqrt(1 - betaCos) * Math.Sign(betaSin);
        temp26 = 0.5 * data.Ay;
        temp39 = temp24 * temp26;
        temp49 = 1 / Math.Sqrt(2) * Math.Sqrt(1 + betaCos) * Math.Sign(betaCos);
        var temp50 = 0.5 * data.Ax;
        var temp51 = temp49 * temp50;
        var temp52 = -(temp39 + temp51);
        var temp53 = -temp24;
        var temp54 = -0.5 * data.Ay;
        var temp55 = temp53 * temp54;
        var temp56 = temp52 - temp55;
        var temp57 = -0.5 * data.Ax;
        var temp58 = temp49 * temp57;
        temp56 -= temp58;
        var temp59 = temp52 * temp54;
        var temp60 = -temp59;
        var temp61 = temp24 + temp60;
        temp26 *= temp49;
        temp50 *= temp53;
        var temp62 = temp26 + temp50;
        var temp63 = temp57 * temp62;
        var temp64 = -temp63;
        temp61 += temp64;
        temp26 = -(temp26 + temp50);
        temp50 = temp57 * temp26;
        var temp65 = -temp50;
        temp61 += temp65;
        temp39 += temp51;
        temp51 = temp54 * temp39;
        var temp66 = -temp51;
        temp61 += temp66;
        var temp67 = temp32 * temp61;
        temp24 *= temp57;
        var temp68 = temp26 - temp24;
        var temp69 = temp49 * temp54;
        temp68 -= temp69;
        var temp70 = temp37 * temp68;
        var temp71 = -(temp67 + temp70);
        var temp72 = temp12 * temp56;
        temp71 -= temp72;
        temp60 += temp64;
        temp60 = temp65 + temp60;
        temp60 = temp66 + temp60;
        temp64 = temp32 * temp60;
        temp65 = temp71 - temp64;
        temp66 = temp56 * temp65;
        temp66 = -temp66;
        temp55 += temp39;
        temp55 = temp58 + temp55;
        temp55 = temp32 * temp55;
        temp58 = -temp55;
        temp71 = temp12 * temp61;
        var temp73 = temp58 - temp71;
        var temp74 = temp32 * temp56;
        var temp75 = -temp74;
        temp73 += temp75;
        temp52 *= temp57;
        temp49 += temp52;
        temp26 = temp54 * temp26;
        temp49 += temp26;
        temp54 *= temp62;
        temp49 += temp54;
        temp39 = temp57 * temp39;
        temp49 += temp39;
        temp57 = temp37 * temp49;
        temp73 -= temp57;
        var temp76 = temp61 * temp73;
        temp76 = temp66 - temp76;
        var temp77 = temp32 * temp68;
        temp53 += temp59;
        temp53 = temp63 + temp53;
        temp53 = temp50 + temp53;
        temp53 = temp51 + temp53;
        temp53 = temp37 * temp53;
        var temp78 = temp77 + temp53;
        temp24 = temp62 + temp24;
        temp24 = temp69 + temp24;
        temp62 = temp32 * temp24;
        temp69 = temp78 + temp62;
        temp78 = temp12 * temp49;
        temp69 += temp78;
        var temp79 = temp49 * temp69;
        temp76 -= temp79;
        temp79 = temp52 + temp26;
        temp79 = temp54 + temp79;
        temp79 = temp39 + temp79;
        var temp80 = temp32 * temp79;
        temp24 = temp12 * temp24;
        var temp81 = -(temp80 + temp24);
        var temp82 = temp37 * temp56;
        temp81 -= temp82;
        var temp83 = temp32 * temp49;
        temp81 -= temp83;
        var temp84 = temp68 * temp81;
        temp84 = -temp84;
        temp76 += temp84;
        temp24 = temp80 + temp24;
        temp24 = temp82 + temp24;
        temp24 = temp83 + temp24;
        temp80 = temp68 * temp24;
        temp80 = -temp80;
        temp76 += temp80;
        temp67 += temp70;
        temp67 = temp72 + temp67;
        temp64 += temp67;
        temp67 = temp56 * temp64;
        temp67 = -temp67;
        temp70 = temp76 + temp67;
        temp72 = temp77 + temp62;
        temp76 = temp12 * temp79;
        temp72 += temp76;
        temp59 += temp63;
        temp50 += temp59;
        temp50 = temp51 + temp50;
        temp51 = temp37 * temp50;
        temp59 = temp72 + temp51;
        temp26 = -(temp52 + temp26);
        temp26 -= temp54;
        temp26 -= temp39;
        temp39 = temp59 * temp26;
        temp39 = temp70 - temp39;
        temp52 = temp55 + temp74;
        temp37 *= temp79;
        temp52 += temp37;
        temp12 *= temp60;
        temp52 += temp12;
        temp54 = temp60 * temp52;
        temp39 -= temp54;
        temp39 = -temp39;
        temp54 = temp21 * temp39;
        temp55 += temp71;
        temp55 = temp74 + temp55;
        temp55 = temp57 + temp55;
        temp57 = temp56 * temp55;
        temp63 = -temp77;
        temp53 = temp63 - temp53;
        temp62 = -temp62;
        temp53 += temp62;
        temp53 -= temp78;
        temp70 = temp68 * temp53;
        temp57 = -(temp57 + temp70);
        temp70 = temp81 * temp26;
        temp57 -= temp70;
        temp70 = temp49 * temp24;
        temp57 -= temp70;
        temp70 = temp61 * temp64;
        temp57 -= temp70;
        temp70 = temp60 * temp64;
        temp57 -= temp70;
        temp62 = temp63 + temp62;
        temp62 -= temp76;
        temp51 = temp62 - temp51;
        temp62 = temp68 * temp51;
        temp57 -= temp62;
        temp62 = temp56 * temp52;
        temp57 -= temp62;
        temp38 = -(temp38 + temp40);
        temp38 -= temp46;
        temp38 -= temp48;
        temp40 = temp33 * temp38;
        temp40 = -temp40;
        temp34 = -temp34;
        temp27 = temp34 - temp27;
        temp35 = -temp35;
        temp27 += temp35;
        temp27 -= temp43;
        temp43 = temp23 * temp27;
        temp43 = temp40 - temp43;
        temp17 = temp15 + temp17;
        temp17 = temp18 + temp17;
        temp17 = temp45 + temp17;
        temp45 = temp30 * temp17;
        temp43 -= temp45;
        temp45 = temp14 * temp47;
        temp45 = -temp45;
        temp43 += temp45;
        temp46 = temp14 * temp13;
        temp46 = -temp46;
        temp43 += temp46;
        temp48 = temp33 * temp42;
        temp48 = -temp48;
        temp43 += temp48;
        temp15 += temp18;
        temp15 = temp25 + temp15;
        temp15 = temp20 + temp15;
        temp18 = temp16 * temp15;
        temp18 = temp43 - temp18;
        temp20 = temp28 * temp36;
        temp18 -= temp20;
        temp20 = temp54 + temp57 * temp18;
        temp25 = temp54 + temp57 * temp18;
        temp43 = temp25 * temp25 - temp20 * temp20;
        temp54 = temp14 * temp38;
        temp62 = temp30 * temp29 + temp54;
        temp62 = temp23 * temp17 + temp62;
        temp63 = temp33 * temp13;
        temp62 += temp63;
        temp70 = temp33 * temp47;
        temp62 += temp70;
        temp71 = temp14 * temp42;
        temp62 += temp71;
        temp62 = temp19 * temp15 + temp62;
        temp62 = temp36 * temp16 + temp62;
        temp72 = temp39 * temp62;
        temp74 = temp68 * temp65;
        temp76 = temp49 * temp55;
        temp77 = temp74 + temp76;
        temp78 = temp61 * temp69;
        temp77 += temp78;
        temp79 = temp56 * temp24;
        temp77 += temp79;
        temp82 = temp56 * temp81;
        temp77 += temp82;
        temp83 = temp68 * temp64;
        temp77 += temp83;
        var temp85 = temp50 * temp59;
        temp77 += temp85;
        var temp86 = temp26 * temp52;
        temp77 += temp86;
        var temp87 = temp18 * temp77;
        temp72 = -(temp72 + temp87);
        temp43 -= temp72 * temp72;
        temp72 = -temp57;
        temp87 = temp21 * temp57 + temp21 * temp72;
        temp43 += temp87 * temp87;
        temp87 = temp57 * temp62;
        temp76 = -(temp74 + temp76);
        temp76 -= temp78;
        temp76 -= temp79;
        temp76 -= temp82;
        temp76 -= temp83;
        temp76 -= temp85;
        temp76 -= temp86;
        temp76 = temp21 * temp76;
        temp76 = -(temp87 + temp76);
        temp78 = temp76 * temp76;
        temp43 += temp78;
        temp43 -= temp78;
        temp54 += temp63;
        temp54 = temp70 + temp54;
        temp54 = temp71 + temp54;
        temp54 = temp29 * temp16 + temp54;
        temp54 = temp19 * temp17 + temp54;
        temp54 = temp23 * temp15 + temp54;
        temp36 = temp36 * temp30 + temp54;
        temp36 = temp39 * temp36;
        temp38 = temp23 * temp47 + temp16 * temp38;
        temp27 = temp14 * temp27 + temp38;
        temp27 = temp33 * temp44 + temp27;
        temp13 = temp13 * temp19 + temp27;
        temp13 = temp30 * temp42 + temp13;
        temp13 = temp33 * temp22 + temp13;
        temp19 = temp34 - temp31;
        temp19 = temp35 + temp19;
        temp19 -= temp41;
        temp13 += temp14 * temp19;
        temp14 = temp57 * temp13;
        temp14 = -(temp36 + temp14);
        temp13 = temp72 * temp13;
        temp13 = temp14 - temp13;
        temp14 = temp40 + temp45;
        temp14 = temp46 + temp14;
        temp14 = temp48 + temp14;
        temp22 = temp28 * temp29;
        temp14 -= temp22;
        temp16 *= temp17;
        temp14 -= temp16;
        temp15 = temp30 * temp15;
        temp14 -= temp15;
        temp15 = temp23 * temp19;
        temp14 -= temp15;
        temp14 = temp77 * temp14;
        temp13 -= temp14;
        temp14 = temp74 + temp79;
        temp14 = temp82 + temp14;
        temp14 = temp83 + temp14;
        temp14 = temp26 * temp55 + temp14;
        temp14 = temp69 * temp50 + temp14;
        temp14 = temp61 * temp59 + temp14;
        temp14 = temp49 * temp52 + temp14;
        temp14 = temp18 * temp14;
        temp13 -= temp14;
        temp14 = temp68 * temp73;
        temp15 = temp56 * temp53;
        temp16 = temp14 + temp15;
        temp17 = temp61 * temp81;
        temp16 += temp17;
        temp18 = temp24 * temp50;
        temp16 += temp18;
        temp19 = temp65 * temp26;
        temp16 += temp19;
        temp22 = temp49 * temp64;
        temp16 += temp22;
        temp23 = temp56 * temp51;
        temp16 += temp23;
        temp24 = temp58 + temp75;
        temp24 -= temp37;
        temp12 = temp24 - temp12;
        temp24 = temp68 * temp12;
        temp16 += temp24;
        temp16 = temp21 * temp16;
        temp13 -= temp16;
        temp14 = -(temp14 + temp15);
        temp14 -= temp17;
        temp14 -= temp18;
        temp14 -= temp19;
        temp14 -= temp22;
        temp14 -= temp23;
        temp14 -= temp24;
        temp14 = temp21 * temp14;
        temp13 -= temp14;
        temp14 = temp60 * temp55;
        temp15 = temp69 * temp26;
        temp14 = -(temp14 + temp15);
        temp15 = temp49 * temp59;
        temp14 -= temp15;
        temp12 = temp61 * temp12;
        temp12 = temp14 - temp12;
        temp12 = temp66 + temp12;
        temp12 = temp84 + temp12;
        temp12 = temp80 + temp12;
        temp12 = temp67 + temp12;
        temp12 = -temp62 * temp12;
        temp12 = temp13 - temp12;
        temp12 = temp43 + temp12 * temp12;
        temp12 = 1 / Math.Sqrt(Math.Abs(temp12));
        temp13 = temp20 * temp12;
        temp14 = temp25 * temp12;
        var eX = -0.5 * temp13 + -0.5 * temp14;

        temp12 = temp76 * temp12;
        var eY = -temp12;

        temp0 = -0.5 * temp7 * temp0;
        temp0 = temp2 * temp5 + temp0;
        temp2 = temp0 * eX;
        temp5 = eX * eX;
        temp7 = eY * eY;
        temp12 = -1 + temp5 + temp7;
        temp12 = 0.5 * temp12;
        temp10 *= temp12;
        temp2 = -(temp2 + temp10);
        temp5 = 1 + temp5 + temp7;
        temp5 = 0.5 * temp5;
        temp7 = temp11 + temp8;
        temp8 = temp5 * temp7;
        temp2 -= temp8;
        temp2 = -temp2;
        temp8 = data.By * eX;
        temp10 = -data.Bx * eY;
        temp8 = -(temp8 + temp10);
        temp10 = temp2 * temp8;
        temp3 += temp9;
        temp3 *= eY;
        temp9 = temp6 * eX;
        temp3 = -(temp3 + temp9);
        temp9 = temp32 * temp5;
        temp3 -= temp9;
        temp9 = data.Bx * temp5;
        temp11 = data.Bx * data.Bx;
        temp13 = data.By * data.By;
        temp14 = 1 + temp11 + temp13;
        temp14 = -0.5 * temp14;
        temp15 = temp14 * eX;
        temp9 = -(temp9 + temp15);
        temp15 = data.Bx * temp12;
        temp9 = -(temp9 + temp15);
        temp11 = -1 + temp11 + temp13;
        temp11 = -0.5 * temp11;
        temp13 = temp11 * eX;
        temp9 -= temp13;
        temp13 = temp10 + temp3 * temp9;
        temp15 = data.By * temp5;
        temp14 *= eY;
        temp14 = -(temp15 + temp14);
        temp14 = data.By * temp12 + temp14;
        temp11 = temp14 + temp11 * eY;
        temp2 *= temp11;
        temp0 *= eY;
        temp6 *= temp12;
        temp14 = temp0 + temp6;
        temp1 += temp4;
        temp4 = temp5 * temp1;
        temp5 = temp14 + temp4;
        temp5 = temp9 * temp5;
        temp2 = -(temp2 + temp5);
        temp5 = -(temp13 * temp13 + temp2 * temp2);
        temp7 *= eY;
        temp1 = -temp1 * eX;
        temp1 = -(temp7 + temp1);
        temp7 = temp32 * temp12;
        temp1 -= temp7;
        temp7 = temp10 + temp9 * temp1;
        temp5 += temp7 * temp7;
        temp0 = -(temp0 + temp6);
        temp0 -= temp4;
        temp0 = temp8 * temp0;
        temp0 = -temp0;
        temp4 = temp3 * temp11;
        temp4 = temp0 - temp4;
        temp5 -= temp4 * temp4;
        temp6 = temp11 * temp1;
        temp0 -= temp6;
        temp5 += temp0 * temp0;
        temp3 = -temp8 * temp3;
        temp1 = temp8 * temp1 + temp3;
        temp3 = temp5 + temp1 * temp1;
        temp3 = 1 / Math.Sqrt(Math.Abs(temp3));
        temp5 = temp13 * temp3;
        temp2 *= temp3;
        temp6 = temp7 * temp3;
        temp7 = temp6 - temp5;
        temp4 *= temp3;
        temp0 *= temp3;
        temp8 = temp0 - temp4;
        temp9 = temp7 * temp7 + temp8 * temp8;
        temp9 = 1 / temp9;
        temp10 = -temp2 * temp9;
        temp11 = temp5 * temp10;
        temp12 = -temp6 * temp10;
        temp11 = -(temp11 + temp12);
        temp1 *= temp3;
        temp3 = temp8 * temp9;
        temp8 = temp1 * temp3;
        temp8 = temp11 - temp8;
        temp2 = temp5 * temp5 + temp2 * temp2;
        temp2 -= temp6 * temp6;
        temp2 = temp4 * temp4 + temp2;
        temp2 -= temp0 * temp0;
        temp2 -= temp1 * temp1;
        temp2 = Math.Sqrt(temp2);
        temp5 = temp7 * temp9;
        temp6 = temp2 * temp5;
        var p1X = temp8 - temp6;

        temp4 *= temp10;
        temp0 = -temp0 * temp10;
        temp0 = -(temp4 + temp0);
        temp1 = -temp1 * temp5;
        temp0 -= temp1;
        temp1 = temp3 * temp2;
        var p1Y = temp0 - temp1;

        temp1 = -temp2;
        temp2 = temp5 * temp1;
        var p2X = temp8 - temp2;

        temp1 = temp3 * temp1;
        var p2Y = temp0 - temp1;

        //Finish GA-FuL MetaContext Code Generation, 2024-03-17T00:30:31.3076691+02:00

        var d =
            Math.Abs(p1X - eX) +
            Math.Abs(p1Y - eY);

        if (d.IsNaN() || d.IsNearZero())
        {
            pX = p2X;
            pY = p2Y;
        }
        else
        {
            pX = p1X;
            pY = p1Y;
        }
    }

    /// <summary>
    /// Code-generated implementation, very fast to use
    /// </summary>
    /// <returns></returns>
    public static void SolveUsingCGaCassini(SnelliusPothenotProblemData2D data, out double pX, out double pY)
    {
        var alphaSin = Math.Sin(data.Alpha);
        var betaSin = Math.Sin(data.Beta);

        var alphaCos = Math.Sqrt(1 - alphaSin * alphaSin);
        var betaCos = Math.Sqrt(1 - betaSin * betaSin);

        if (((int)Math.Floor(2 * data.Alpha / Math.PI) % 4 + 4) % 4 is 1 or 2) alphaCos = -alphaCos;
        if (((int)Math.Floor(2 * data.Beta / Math.PI) % 4 + 4) % 4 is 1 or 2) betaCos = -betaCos;

        //Begin GA-FuL MetaContext Code Generation, 2024-03-17T00:30:35.1356470+02:00
        //MetaContext: CGA_Cassini
        //Input Variables: 10 used, 0 not used, 10 total.
        //Temp Variables: 670 sub-expressions, 0 generated temps, 670 total.
        //Target Temp Variables: 53 total.
        //Output Variables: 4 total.
        //Computations: 1.0875370919881306 average, 733 total.
        //Memory Reads: 1.9332344213649852 average, 1303 total.
        //Memory Writes: 674 total.
        //
        //MetaContext Binding Data:
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   0.5 = constant: '0.5'
        //   -0.5 = constant: '-0.5'
        //   Rational[1, 2] = constant: 'Rational[1, 2]'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   Ax = parameter: data.Ax
        //   Ay = parameter: data.Ay
        //   Bx = parameter: data.Bx
        //   By = parameter: data.By
        //   Cx = parameter: data.Cx
        //   Cy = parameter: data.Cy
        //   alphaCos = parameter: alphaCos
        //   alphaSin = parameter: alphaSin
        //   betaCos = parameter: betaCos
        //   betaSin = parameter: betaSin

        var temp0 = -data.Ay * data.Bx;
        temp0 = data.Ax * data.By + temp0;
        var temp1 = 1 / Math.Sqrt(2) * Math.Sqrt(1 + alphaSin) * Math.Sign(alphaSin);
        var temp2 = 0.5 * data.Ax;
        var temp3 = temp1 * temp2;
        var temp4 = 1 / Math.Sqrt(2) * Math.Sqrt(1 - alphaSin) * Math.Sign(alphaCos);
        var temp5 = -temp4;
        var temp6 = 0.5 * data.Ay;
        var temp7 = temp5 * temp6;
        var temp8 = -(temp3 + temp7);
        var temp9 = -0.5 * data.Ax;
        var temp10 = temp8 * temp9;
        var temp11 = temp1 + temp10;
        var temp12 = -0.5 * data.Ay;
        temp2 *= temp4;
        temp6 = temp1 * temp6;
        var temp13 = -(temp2 + temp6);
        var temp14 = temp12 * temp13;
        temp11 += temp14;
        temp2 += temp6;
        temp6 = temp12 * temp2;
        temp11 += temp6;
        temp3 += temp7;
        temp7 = temp9 * temp3;
        temp11 += temp7;
        var temp15 = temp0 * temp11;
        var temp16 = temp10 + temp14;
        temp16 = temp6 + temp16;
        temp16 = temp7 + temp16;
        var temp17 = temp0 * temp16;
        var temp18 = -(temp15 + temp17);
        var temp19 = temp5 * temp9;
        var temp20 = temp2 + temp19;
        var temp21 = temp1 * temp12;
        temp20 += temp21;
        var temp22 = data.Ax * data.Ax;
        var temp23 = data.Ay * data.Ay;
        var temp24 = 1 + temp22 + temp23;
        temp24 = 0.5 * temp24;
        var temp25 = data.Bx * temp24;
        var temp26 = data.Bx * data.Bx;
        var temp27 = data.By * data.By;
        var temp28 = 1 + temp26 + temp27;
        var temp29 = 0.5 * temp28;
        var temp30 = -data.Ax;
        var temp31 = temp29 * temp30;
        temp25 = -(temp25 + temp31);
        temp22 = -1 + temp22 + temp23;
        temp22 = 0.5 * temp22;
        temp23 = temp25 + data.Bx * temp22;
        temp25 = -1 + temp26 + temp27;
        temp26 = 0.5 * temp25;
        temp23 += temp30 * temp26;
        temp27 = temp20 * temp23;
        temp18 -= temp27;
        temp31 = data.By * temp24;
        temp28 = -0.5 * temp28;
        var temp32 = data.Ay * temp28;
        temp31 = -(temp31 + temp32);
        temp31 = data.By * temp22 + temp31;
        temp25 = -0.5 * temp25;
        temp31 += data.Ay * temp25;
        temp32 = temp4 * temp12;
        var temp33 = temp8 - temp32;
        temp1 *= temp9;
        temp33 -= temp1;
        var temp34 = temp31 * temp33;
        temp18 -= temp34;
        temp19 = temp13 - temp19;
        temp19 -= temp21;
        temp21 = temp18 * temp19;
        temp15 += temp17;
        temp15 = temp27 + temp15;
        temp15 = temp34 + temp15;
        temp17 = temp19 * temp15;
        temp17 = -(temp21 + temp17);
        temp8 *= temp12;
        temp21 = -temp8;
        temp5 += temp21;
        temp2 = temp9 * temp2;
        temp27 = -temp2;
        temp5 += temp27;
        temp9 *= temp13;
        temp13 = -temp9;
        temp5 += temp13;
        temp12 *= temp3;
        temp34 = -temp12;
        temp5 += temp34;
        var temp35 = temp0 * temp5;
        var temp36 = temp31 * temp19;
        var temp37 = temp35 + temp36;
        var temp38 = temp23 * temp33;
        temp37 += temp38;
        temp21 += temp27;
        temp13 += temp21;
        temp13 = temp34 + temp13;
        temp21 = temp0 * temp13;
        temp27 = temp37 + temp21;
        temp34 = temp33 * temp27;
        temp17 -= temp34;
        temp34 = temp16 * temp23;
        temp37 = temp8 + temp2;
        temp37 = temp9 + temp37;
        temp37 = temp12 + temp37;
        var temp39 = temp31 * temp37;
        var temp40 = temp34 + temp39;
        var temp41 = temp0 * temp19;
        temp40 += temp41;
        temp20 = temp0 * temp20;
        temp40 += temp20;
        temp10 = -(temp10 + temp14);
        temp6 = temp10 - temp6;
        temp6 -= temp7;
        temp7 = temp40 * temp6;
        temp7 = temp17 - temp7;
        temp10 = temp16 * temp31;
        temp14 = temp23 * temp13;
        temp16 = temp10 + temp14;
        temp3 += temp32;
        temp1 += temp3;
        temp1 = temp0 * temp1;
        temp3 = temp16 + temp1;
        temp0 *= temp33;
        temp3 += temp0;
        temp16 = temp13 * temp3;
        temp7 -= temp16;
        temp16 = -(temp35 + temp36);
        temp16 -= temp38;
        temp16 -= temp21;
        temp21 = temp33 * temp16;
        temp21 = -temp21;
        temp7 += temp21;
        temp32 = -temp1;
        temp35 = temp23 * temp5;
        temp36 = temp32 - temp35;
        temp38 = -temp0;
        temp36 += temp38;
        var temp42 = temp11 * temp31;
        temp36 -= temp42;
        var temp43 = temp5 * temp36;
        temp7 -= temp43;
        temp4 += temp8;
        temp2 += temp4;
        temp2 = temp9 + temp2;
        temp2 = temp12 + temp2;
        temp2 = temp31 * temp2;
        temp4 = temp41 + temp2;
        temp4 = temp20 + temp4;
        temp8 = temp11 * temp23;
        temp4 += temp8;
        temp9 = temp11 * temp4;
        temp7 -= temp9;
        temp7 = -temp7;
        temp9 = temp22 + temp25;
        temp12 = temp24 + temp28;
        temp22 = temp12 * temp12 - temp9 * temp9;
        temp23 = data.Bx + temp30;
        temp22 -= temp23 * temp23;
        temp24 = -data.By;
        temp30 = data.Ay + temp24;
        temp22 -= temp30 * temp30;
        temp22 = 1 / Math.Sqrt(Math.Abs(temp22));
        temp9 *= temp22;
        temp9 = -temp9;
        temp1 += temp35;
        temp0 += temp1;
        temp0 = temp42 + temp0;
        temp1 = temp33 * temp0;
        temp31 = -temp41;
        temp2 = temp31 - temp2;
        temp20 = -temp20;
        temp2 += temp20;
        temp2 -= temp8;
        temp8 = temp19 * temp2;
        temp1 = -(temp1 + temp8);
        temp8 = temp18 * temp6;
        temp1 -= temp8;
        temp8 = temp11 * temp15;
        temp1 -= temp8;
        temp8 = temp5 * temp27;
        temp1 -= temp8;
        temp8 = temp13 * temp27;
        temp1 -= temp8;
        temp8 = -(temp34 + temp39);
        temp8 = temp31 + temp8;
        temp8 = temp20 + temp8;
        temp20 = temp19 * temp8;
        temp1 -= temp20;
        temp20 = temp33 * temp3;
        temp1 -= temp20;
        temp20 = temp30 * temp22;
        temp20 = -temp20;
        temp30 = temp7 * temp9 + temp1 * temp20;
        temp12 *= temp22;
        temp12 = -temp12;
        temp31 = temp1 * temp20 + temp7 * temp12;
        temp34 = temp31 * temp31 - temp30 * temp30;
        temp22 = temp23 * temp22;
        temp7 *= temp22;
        temp23 = temp19 * temp16;
        temp35 = temp11 * temp0;
        temp39 = temp23 + temp35;
        temp41 = temp5 * temp4;
        temp39 += temp41;
        temp42 = temp33 * temp15;
        temp39 += temp42;
        temp43 = temp33 * temp18;
        temp39 += temp43;
        var temp44 = temp19 * temp27;
        temp39 += temp44;
        var temp45 = temp37 * temp40;
        temp39 += temp45;
        var temp46 = temp6 * temp3;
        temp39 += temp46;
        temp39 = temp20 * temp39;
        temp7 = -(temp7 + temp39);
        temp7 = temp34 - temp7 * temp7;
        temp34 = -temp1 * temp12;
        temp34 = temp9 * temp1 + temp34;
        temp7 += temp34 * temp34;
        temp1 *= temp22;
        temp1 = -temp1;
        temp34 = -(temp23 + temp35);
        temp34 -= temp41;
        temp34 -= temp42;
        temp34 -= temp43;
        temp34 -= temp44;
        temp34 -= temp45;
        temp34 -= temp46;
        temp35 = temp12 * temp34;
        temp35 = temp1 - temp35;
        temp7 += temp35 * temp35;
        temp34 = temp9 * temp34;
        temp1 -= temp34;
        temp7 -= temp1 * temp1;
        temp23 += temp42;
        temp23 = temp43 + temp23;
        temp23 = temp44 + temp23;
        temp23 = temp6 * temp0 + temp23;
        temp23 = temp37 * temp4 + temp23;
        temp23 = temp5 * temp40 + temp23;
        temp3 = temp11 * temp3 + temp23;
        temp3 = temp20 * temp3;
        temp20 = temp19 * temp36;
        temp2 = temp33 * temp2;
        temp23 = temp20 + temp2;
        temp18 *= temp5;
        temp23 += temp18;
        temp15 *= temp37;
        temp23 += temp15;
        temp16 = temp6 * temp16;
        temp23 += temp16;
        temp27 = temp11 * temp27;
        temp23 += temp27;
        temp8 = temp33 * temp8;
        temp23 += temp8;
        temp10 = -(temp10 + temp14);
        temp10 = temp32 + temp10;
        temp10 = temp38 + temp10;
        temp14 = temp19 * temp10;
        temp19 = temp23 + temp14;
        temp9 *= temp19;
        temp3 = -(temp3 + temp9);
        temp2 = -(temp20 + temp2);
        temp2 -= temp18;
        temp2 -= temp15;
        temp2 -= temp16;
        temp2 -= temp27;
        temp2 -= temp8;
        temp2 -= temp14;
        temp2 = temp12 * temp2;
        temp2 = temp3 - temp2;
        temp0 = temp13 * temp0;
        temp0 = temp17 - temp0;
        temp3 = temp6 * temp4;
        temp0 -= temp3;
        temp3 = temp11 * temp40;
        temp0 -= temp3;
        temp3 = temp5 * temp10;
        temp0 -= temp3;
        temp0 = temp21 + temp0;
        temp0 = -temp22 * temp0;
        temp0 = temp2 - temp0;
        temp0 = temp7 + temp0 * temp0;
        temp0 = 1 / Math.Sqrt(Math.Abs(temp0));
        temp2 = temp30 * temp0;
        temp3 = temp31 * temp0;
        temp2 = -0.5 * temp2 + -0.5 * temp3;
        temp3 = temp2 * temp2;
        temp1 *= temp0;
        temp0 = temp35 * temp0;
        temp0 = -0.5 * temp1 + -0.5 * temp0;
        temp1 = temp0 * temp0;
        temp4 = 1 + temp3 + temp1;
        temp4 = 0.5 * temp4;
        temp5 = -1 + temp3 + temp1;
        temp5 = 0.5 * temp5;
        temp6 = temp28 * temp4 + temp26 * temp5;
        temp6 = data.Bx * temp2 + temp6;
        temp6 = data.By * temp0 + temp6;
        temp6 = temp6;
        temp4 += temp6;
        temp1 = temp3 + temp1;
        temp1 -= temp4 * temp4;
        temp3 = temp5 + temp6;
        temp1 += temp3 * temp3;
        temp1 = 1 / Math.Sqrt(Math.Abs(temp1));
        temp4 *= temp1;
        temp5 = 1 / Math.Sqrt(2) * Math.Sqrt(1 + betaSin) * Math.Sign(betaSin);
        temp6 = 0.5 * data.Cx;
        temp7 = temp5 * temp6;
        temp8 = -1 * 1 / Math.Sqrt(2) * Math.Sqrt(1 - betaSin) * Math.Sign(betaCos);
        temp9 = -temp8;
        temp10 = 0.5 * data.Cy;
        temp11 = temp9 * temp10;
        temp12 = -(temp7 + temp11);
        temp13 = -0.5 * data.Cy;
        temp14 = temp8 * temp13;
        temp15 = temp12 - temp14;
        temp16 = -0.5 * data.Cx;
        temp17 = temp5 * temp16;
        temp15 -= temp17;
        temp18 = temp12 * temp13;
        temp19 = -temp18;
        temp20 = temp9 + temp19;
        temp6 *= temp8;
        temp10 = temp5 * temp10;
        temp21 = temp6 + temp10;
        temp22 = temp16 * temp21;
        temp23 = -temp22;
        temp20 += temp23;
        temp6 = -(temp6 + temp10);
        temp10 = temp16 * temp6;
        temp27 = -temp10;
        temp20 += temp27;
        temp7 += temp11;
        temp11 = temp13 * temp7;
        temp30 = -temp11;
        temp20 += temp30;
        temp31 = -data.Cy;
        temp32 = data.By * data.Cx + data.Bx * temp31;
        temp33 = temp20 * temp32;
        temp9 *= temp16;
        temp34 = temp6 - temp9;
        temp35 = temp5 * temp13;
        temp34 -= temp35;
        temp36 = data.Cx * data.Cx;
        temp37 = data.Cy * data.Cy;
        temp38 = 1 + temp36 + temp37;
        temp38 = 0.5 * temp38;
        temp39 = data.By * temp38;
        temp40 = temp29 * temp31;
        temp39 = -(temp39 + temp40);
        temp36 = -1 + temp36 + temp37;
        temp36 = 0.5 * temp36;
        temp37 = temp39 + data.By * temp36;
        temp31 = temp26 * temp31 + temp37;
        temp37 = temp34 * temp31;
        temp39 = -(temp33 + temp37);
        temp40 = data.Bx * temp38;
        temp41 = -data.Cx;
        temp29 *= temp41;
        temp29 = -(temp40 + temp29);
        temp29 = data.Bx * temp36 + temp29;
        temp29 = temp26 * temp41 + temp29;
        temp40 = temp15 * temp29;
        temp39 -= temp40;
        temp19 += temp23;
        temp19 = temp27 + temp19;
        temp19 = temp30 + temp19;
        temp23 = temp32 * temp19;
        temp27 = temp39 - temp23;
        temp30 = temp15 * temp27;
        temp30 = -temp30;
        temp14 += temp7;
        temp14 = temp17 + temp14;
        temp14 = temp32 * temp14;
        temp17 = -temp14;
        temp39 = temp20 * temp29;
        temp42 = temp17 - temp39;
        temp43 = temp15 * temp32;
        temp44 = -temp43;
        temp42 += temp44;
        temp12 *= temp16;
        temp5 += temp12;
        temp6 = temp13 * temp6;
        temp5 += temp6;
        temp13 *= temp21;
        temp5 += temp13;
        temp7 = temp16 * temp7;
        temp5 += temp7;
        temp16 = temp31 * temp5;
        temp42 -= temp16;
        temp45 = temp20 * temp42;
        temp45 = temp30 - temp45;
        temp46 = temp32 * temp34;
        temp8 += temp18;
        temp8 = temp22 + temp8;
        temp8 = temp10 + temp8;
        temp8 = temp11 + temp8;
        temp8 = temp31 * temp8;
        var temp47 = temp46 + temp8;
        temp9 = temp21 + temp9;
        temp9 = temp35 + temp9;
        temp21 = temp32 * temp9;
        temp35 = temp47 + temp21;
        temp47 = temp29 * temp5;
        temp35 += temp47;
        var temp48 = temp5 * temp35;
        temp45 -= temp48;
        temp48 = temp12 + temp6;
        temp48 = temp13 + temp48;
        temp48 = temp7 + temp48;
        var temp49 = temp32 * temp48;
        temp9 = temp29 * temp9;
        var temp50 = -(temp49 + temp9);
        var temp51 = temp15 * temp31;
        temp50 -= temp51;
        temp32 *= temp5;
        temp50 -= temp32;
        var temp52 = temp34 * temp50;
        temp52 = -temp52;
        temp45 += temp52;
        temp9 = temp49 + temp9;
        temp9 = temp51 + temp9;
        temp9 = temp32 + temp9;
        temp32 = temp34 * temp9;
        temp32 = -temp32;
        temp45 += temp32;
        temp33 += temp37;
        temp33 = temp40 + temp33;
        temp23 += temp33;
        temp33 = temp15 * temp23;
        temp33 = -temp33;
        temp37 = temp45 + temp33;
        temp40 = temp46 + temp21;
        temp45 = temp29 * temp48;
        temp40 += temp45;
        temp18 += temp22;
        temp10 += temp18;
        temp10 = temp11 + temp10;
        temp11 = temp31 * temp10;
        temp18 = temp40 + temp11;
        temp6 = -(temp12 + temp6);
        temp6 -= temp13;
        temp6 -= temp7;
        temp7 = temp18 * temp6;
        temp7 = temp37 - temp7;
        temp12 = temp14 + temp43;
        temp13 = temp31 * temp48;
        temp12 += temp13;
        temp22 = temp29 * temp19;
        temp12 += temp22;
        temp29 = temp19 * temp12;
        temp7 -= temp29;
        temp7 = -temp7;
        temp25 += temp36;
        temp29 = temp28 + temp38;
        temp31 = temp29 * temp29 - temp25 * temp25;
        temp36 = data.Bx + temp41;
        temp31 -= temp36 * temp36;
        temp24 = data.Cy + temp24;
        temp31 -= temp24 * temp24;
        temp31 = 1 / Math.Sqrt(Math.Abs(temp31));
        temp25 *= temp31;
        temp25 = -temp25;
        temp14 += temp39;
        temp14 = temp43 + temp14;
        temp14 = temp16 + temp14;
        temp16 = temp15 * temp14;
        temp37 = -temp46;
        temp8 = temp37 - temp8;
        temp21 = -temp21;
        temp8 += temp21;
        temp8 -= temp47;
        temp38 = temp34 * temp8;
        temp16 = -(temp16 + temp38);
        temp38 = temp50 * temp6;
        temp16 -= temp38;
        temp38 = temp5 * temp9;
        temp16 -= temp38;
        temp38 = temp20 * temp23;
        temp16 -= temp38;
        temp38 = temp19 * temp23;
        temp16 -= temp38;
        temp21 = temp37 + temp21;
        temp21 -= temp45;
        temp11 = temp21 - temp11;
        temp21 = temp34 * temp11;
        temp16 -= temp21;
        temp21 = temp15 * temp12;
        temp16 -= temp21;
        temp21 = temp24 * temp31;
        temp21 = -temp21;
        temp24 = temp7 * temp25 + temp16 * temp21;
        temp29 *= temp31;
        temp29 = -temp29;
        temp37 = temp16 * temp21 + temp7 * temp29;
        temp38 = temp37 * temp37 - temp24 * temp24;
        temp31 = temp36 * temp31;
        temp7 *= temp31;
        temp36 = temp34 * temp27;
        temp39 = temp5 * temp14;
        temp40 = temp36 + temp39;
        temp41 = temp20 * temp35;
        temp40 += temp41;
        temp43 = temp15 * temp9;
        temp40 += temp43;
        temp45 = temp15 * temp50;
        temp40 += temp45;
        temp46 = temp34 * temp23;
        temp40 += temp46;
        temp47 = temp10 * temp18;
        temp40 += temp47;
        temp48 = temp6 * temp12;
        temp40 += temp48;
        temp40 = temp21 * temp40;
        temp7 = -(temp7 + temp40);
        temp7 = temp38 - temp7 * temp7;
        temp38 = -temp16 * temp29;
        temp38 = temp25 * temp16 + temp38;
        temp7 += temp38 * temp38;
        temp16 *= temp31;
        temp16 = -temp16;
        temp38 = -(temp36 + temp39);
        temp38 -= temp41;
        temp38 -= temp43;
        temp38 -= temp45;
        temp38 -= temp46;
        temp38 -= temp47;
        temp38 -= temp48;
        temp39 = temp29 * temp38;
        temp39 = temp16 - temp39;
        temp7 += temp39 * temp39;
        temp38 = temp25 * temp38;
        temp16 -= temp38;
        temp7 -= temp16 * temp16;
        temp36 += temp43;
        temp36 = temp45 + temp36;
        temp36 = temp46 + temp36;
        temp36 = temp6 * temp14 + temp36;
        temp36 = temp35 * temp10 + temp36;
        temp36 = temp20 * temp18 + temp36;
        temp12 = temp5 * temp12 + temp36;
        temp12 = temp21 * temp12;
        temp21 = temp34 * temp42;
        temp8 = temp15 * temp8;
        temp36 = temp21 + temp8;
        temp38 = temp20 * temp50;
        temp36 += temp38;
        temp9 *= temp10;
        temp10 = temp36 + temp9;
        temp27 *= temp6;
        temp10 += temp27;
        temp23 = temp5 * temp23;
        temp10 += temp23;
        temp11 = temp15 * temp11;
        temp10 += temp11;
        temp15 = temp17 + temp44;
        temp13 = temp15 - temp13;
        temp13 -= temp22;
        temp15 = temp34 * temp13;
        temp10 += temp15;
        temp10 = temp25 * temp10;
        temp10 = -(temp12 + temp10);
        temp8 = -(temp21 + temp8);
        temp8 -= temp38;
        temp8 -= temp9;
        temp8 -= temp27;
        temp8 -= temp23;
        temp8 -= temp11;
        temp8 -= temp15;
        temp8 = temp29 * temp8;
        temp8 = temp10 - temp8;
        temp9 = temp30 + temp52;
        temp9 = temp32 + temp9;
        temp9 = temp33 + temp9;
        temp10 = temp19 * temp14;
        temp9 -= temp10;
        temp6 = temp35 * temp6;
        temp6 = temp9 - temp6;
        temp5 *= temp18;
        temp5 = temp6 - temp5;
        temp6 = temp20 * temp13;
        temp5 -= temp6;
        temp5 = -temp31 * temp5;
        temp5 = temp8 - temp5;
        temp5 = temp7 + temp5 * temp5;
        temp5 = 1 / Math.Sqrt(Math.Abs(temp5));
        temp6 = temp24 * temp5;
        temp7 = temp37 * temp5;
        temp6 = -0.5 * temp6 + -0.5 * temp7;
        temp7 = temp6 * temp6;
        temp8 = temp16 * temp5;
        temp5 = temp39 * temp5;
        temp5 = -0.5 * temp8 + -0.5 * temp5;
        temp8 = temp5 * temp5;
        temp9 = -1 + temp7 + temp8;
        temp9 = 0.5 * temp9;
        temp10 = 1 + temp7 + temp8;
        temp10 = 0.5 * temp10;
        temp11 = temp26 * temp9 + temp28 * temp10;
        temp11 = data.Bx * temp6 + temp11;
        temp11 = data.By * temp5 + temp11;
        temp11 = temp11;
        temp9 += temp11;
        temp7 += temp8;
        temp8 = temp10 + temp11;
        temp7 -= temp8 * temp8;
        temp7 = temp9 * temp9 + temp7;
        temp7 = 1 / Math.Sqrt(Math.Abs(temp7));
        temp9 *= temp7;
        temp10 = temp4 * temp9;
        temp3 *= temp1;
        temp8 *= temp7;
        temp11 = -temp3 * temp8;
        temp12 = temp10 + temp11;
        temp6 *= temp7;
        temp13 = temp3 * temp6;
        temp2 *= temp1;
        temp14 = -temp2;
        temp15 = temp9 * temp14;
        temp13 = -(temp13 + temp15);
        temp15 = temp4 * temp6;
        temp14 = temp8 * temp14;
        temp14 = -(temp15 + temp14);
        temp15 = temp14 - temp13;
        temp5 *= temp7;
        temp3 *= temp5;
        temp0 *= temp1;
        temp0 = -temp0;
        temp1 = temp9 * temp0;
        temp7 = -(temp3 + temp1);
        temp4 *= temp5;
        temp7 += temp4;
        temp8 *= temp0;
        temp7 += temp8;
        temp9 = temp15 * temp15 + temp7 * temp7;
        temp9 = 1 / temp9;
        temp15 *= temp9;
        temp16 = temp12 * temp15;
        temp17 = -(temp4 + temp8);
        temp2 *= temp5;
        temp0 = temp6 * temp0;
        temp0 = -(temp2 + temp0);
        temp2 = -temp9 * temp0;
        temp5 = temp17 * temp2;
        temp5 = -(temp16 + temp5);
        temp1 = temp3 + temp1;
        temp3 = temp2 * temp1;
        temp3 = temp5 - temp3;
        temp5 = -(temp12 * temp12 + temp14 * temp14);
        temp4 += temp8;
        temp4 = temp5 - temp4 * temp4;
        temp4 = temp13 * temp13 + temp4;
        temp1 = temp1 * temp1 + temp4;
        temp0 = temp0 * temp0 + temp1;
        temp0 = Math.Sqrt(temp0);
        temp1 = temp7 * temp9;
        temp4 = temp0 * temp1;
        var p1X = temp3 - temp4;

        temp4 = -(temp10 + temp11);
        temp4 = temp1 * temp4;
        temp5 = -temp14 * temp2;
        temp4 = -(temp4 + temp5);
        temp2 = temp13 * temp2;
        temp2 = temp4 - temp2;
        temp4 = temp15 * temp0;
        var p1Y = temp2 - temp4;

        temp0 = -temp0;
        temp1 *= temp0;
        var p2X = temp3 - temp1;

        temp0 = temp15 * temp0;
        var p2Y = temp2 - temp0;

        //Finish GA-FuL MetaContext Code Generation, 2024-03-17T00:30:35.1376123+02:00

        var d =
            Math.Abs(p1X - data.Bx) +
            Math.Abs(p1Y - data.By);

        if (d.IsNaN() || d.IsNearZero())
        {
            pX = p2X;
            pY = p2Y;
        }
        else
        {
            pX = p1X;
            pY = p1Y;
        }
    }


    private static double AdjustValueToBounds(double value, double max)
        => value > max
            ? max
            : value < -max ? -max : value;

    public static double SolveUsingPierlot(SnelliusPothenotProblemData2D data, out double pX, out double pY)
    {
        const double cotMax = 100000000;

        var cot12 = 1 / Math.Tan(data.Beta); //Cot(alpha2 - alpha1);
        var cot23 = 1 / Math.Tan(data.Alpha); //Cot(alpha3 - alpha2);
        cot12 = AdjustValueToBounds(cot12, cotMax);
        cot23 = AdjustValueToBounds(cot23, cotMax);
        var cot31 = (1.0 - cot12 * cot23) / (cot12 + cot23);
        cot31 = AdjustValueToBounds(cot31, cotMax);

        var x1 = data.Cx - data.Bx;
        var y1 = data.Cy - data.By;
        var x3 = data.Cx - data.Bx;
        var y3 = data.Cy - data.By;

        var c12X = x1 + cot12 * y1;
        var c12Y = y1 - cot12 * x1;

        var c23X = x3 - cot23 * y3;
        var c23Y = y3 + cot23 * x3;

        var c31X = x3 + x1 + cot31 * (y3 - y1);
        var c31Y = y3 + y1 - cot31 * (x3 - x1);
        var k31 = x3 * x1 + y3 * y1 + cot31 * (y3 * x1 - x3 * y1);

        var d = (c12X - c23X) * (c23Y - c31Y) - (c23X - c31X) * (c12Y - c23Y);
        var invD = 1.0 / d;
        var k = k31 * invD;

        pX = k * (c12Y - c23Y) + data.Bx;
        pY = k * (c23X - c12X) + data.By;

        return invD; // return 1/D
    }

    public static double SolveUsingPierlot2(SnelliusPothenotProblemData2D data, out double pX, out double pY)
    {
        var alpha12 = data.Beta;  // alpha2 - alpha1;
        var alpha23 = data.Alpha; // alpha3 - alpha2;
        var alpha31 = -(alpha12 + alpha23); // alpha1 - alpha3;

        double x1, y1, x2, y2, x3, y3;

        double cot12, cot23, cot31, c12X, c12Y, c23X, c23Y, c31X, c31Y, k, d, invD, kInvD;

        if (alpha12 is Math.PI or 0.0)
        {
            x1 = data.Cx - data.Cx;
            y1 = data.Cy - data.Cy;
            x2 = data.Bx - data.Cx;
            y2 = data.By - data.Cy;

            cot23 = 1 / Math.Tan(alpha23);

            c12X = y1 - y2;
            c12Y = x2 - x1;
            k = y1 * x2 - x1 * y2;

            c23X = x2 + cot23 * y2;
            c23Y = y2 - cot23 * x2;

            c31X = x1 + cot23 * y1;
            c31Y = y1 - cot23 * x1;

            d = (c31X - c23X) * c12Y + c12X * (c23Y - c31Y);
            invD = 1.0 / d;
            kInvD = k * invD;

            pX = kInvD * (c23Y - c31Y) + data.Cx;
            pY = kInvD * (c31X - c23X) + data.Cy;

            return invD;
        }

        if (alpha23 is Math.PI or 0.0)
        {
            x2 = data.Bx - data.Cx;
            y2 = data.By - data.Cy;
            x3 = data.Cx - data.Cx;
            y3 = data.Cy - data.Cy;

            cot31 = 1 / Math.Tan(alpha31);

            c12X = x2 + cot31 * y2;
            c12Y = y2 - cot31 * x2;

            c23X = y2 - y3;
            c23Y = x3 - x2;
            k = y2 * x3 - x2 * y3;

            c31X = x3 + cot31 * y3;
            c31Y = y3 - cot31 * x3;

            d = (c12X - c31X) * c23Y + c23X * (c31Y - c12Y);
            invD = 1.0 / d;
            kInvD = k * invD;

            pX = kInvD * (c31Y - c12Y) + data.Cx;
            pY = kInvD * (c12X - c31X) + data.Cy;

            return invD;
        }

        if (alpha31 is Math.PI or 0.0)
        {
            x1 = data.Cx - data.Bx;
            y1 = data.Cy - data.By;
            x3 = data.Cx - data.Bx;
            y3 = data.Cy - data.By;

            cot12 = 1 / Math.Tan(alpha12);

            c12X = x1 + cot12 * y1;
            c12Y = y1 - cot12 * x1;

            c23X = x3 + cot12 * y3;
            c23Y = y3 - cot12 * x3;

            c31X = y3 - y1;
            c31Y = x1 - x3;
            k = y3 * x1 - x3 * y1;

            d = (c23X - c12X) * c31Y + c31X * (c12Y - c23Y);
            invD = 1.0 / d;
            kInvD = k * invD;

            pX = kInvD * (c12Y - c23Y) + data.Bx;
            pY = kInvD * (c23X - c12X) + data.By;

            return invD;
        }

        x1 = data.Cx - data.Bx;
        y1 = data.Cy - data.By;
        x3 = data.Cx - data.Bx;
        y3 = data.Cy - data.By;

        cot12 = 1 / Math.Tan(alpha12);
        cot23 = 1 / Math.Tan(alpha23);
        cot31 = (1.0 - cot12 * cot23) / (cot12 + cot23);

        c12X = x1 + cot12 * y1;
        c12Y = y1 - cot12 * x1;

        c23X = x3 - cot23 * y3;
        c23Y = y3 + cot23 * x3;

        c31X = x3 + x1 + cot31 * (y3 - y1);
        c31Y = y3 + y1 - cot31 * (x3 - x1);
        k = x3 * x1 + y3 * y1 + cot31 * (y3 * x1 - x3 * y1);

        d = (c12X - c23X) * (c23Y - c31Y) - (c23X - c31X) * (c12Y - c23Y);
        invD = 1.0 / d;
        kInvD = k * invD;

        pX = kInvD * (c12Y - c23Y) + data.Bx;
        pY = kInvD * (c23X - c12X) + data.By;

        return invD; // return 1/D
    }


    public double Ax { get; init; }

    public double Ay { get; init; }

    public double Bx { get; init; }

    public double By { get; init; }

    public double Cx { get; init; }

    public double Cy { get; init; }

    public double Alpha { get; init; }

    public double Beta { get; init; }


    public LinFloat64Vector2D SolveUsingVGa()
    {
        SolveUsingVGa(
            this,
            out var pX,
            out var pY
        );

        if (double.IsNaN(pX) || double.IsNaN(pY))
            return LinFloat64Vector2D.Create(double.MaxValue, double.MaxValue);

        return LinFloat64Vector2D.Create(pX, pY);
    }

    public LinFloat64Vector2D SolveUsingPGaPaco()
    {
        SolveUsingPGaPaco(
            this,
            out var pX, out var pY
        );

        if (double.IsNaN(pX) || double.IsNaN(pY))
            return LinFloat64Vector2D.Create(double.MaxValue, double.MaxValue);

        return LinFloat64Vector2D.Create(pX, pY);
    }

    public LinFloat64Vector2D SolveUsingCGaPaco()
    {
        SolveUsingCGaPaco(
            this,
            out var pX, out var pY
        );

        if (double.IsNaN(pX) || double.IsNaN(pY))
            return LinFloat64Vector2D.Create(double.MaxValue, double.MaxValue);

        return LinFloat64Vector2D.Create(pX, pY);
    }

    public LinFloat64Vector2D SolveUsingCGaCollins()
    {
        SolveUsingCGaCollins(
            this,
            out var pX, out var pY
        );

        if (double.IsNaN(pX) || double.IsNaN(pY))
            return LinFloat64Vector2D.Create(double.MaxValue, double.MaxValue);

        return LinFloat64Vector2D.Create(pX, pY);
    }

    public LinFloat64Vector2D SolveUsingCGaCassini()
    {
        SolveUsingCGaCassini(
            this,
            out var pX, out var pY
        );

        if (double.IsNaN(pX) || double.IsNaN(pY))
            return LinFloat64Vector2D.Create(double.MaxValue, double.MaxValue);

        return LinFloat64Vector2D.Create(pX, pY);
    }


    public override string ToString()
    {
        var composer = new StringBuilder();

        var a = LinFloat64Vector2D.Create(Ax, Ay);
        var b = LinFloat64Vector2D.Create(Bx, By);
        var c = LinFloat64Vector2D.Create(Cx, Cy);

        composer
            .AppendLine("2D Hansen Problem Data")
            .AppendLine($"   A     : {a.ToTupleString()}")
            .AppendLine($"   B     : {b.ToTupleString()}")
            .AppendLine($"   C     : {c.ToTupleString()}")
            .AppendLine($"   alpha : {Alpha}")
            .AppendLine($"   beta  : {Beta}")
            .AppendLine();

        composer
            .AppendLine($"VGA: {SolveUsingVGa().ToTupleString()}")
            .AppendLine();

        composer
            .AppendLine($"PGA-Paco: {SolveUsingPGaPaco().ToTupleString()}")
            .AppendLine();

        composer
            .AppendLine($"CGA-Paco: {SolveUsingCGaPaco().ToTupleString()}")
            .AppendLine();

        composer
            .AppendLine($"CGA-Collins: {SolveUsingCGaCollins().ToTupleString()}")
            .AppendLine();

        composer
            .AppendLine($"CGA-Cassini: {SolveUsingCGaCassini().ToTupleString()}")
            .AppendLine();

        return composer.ToString();
    }
}