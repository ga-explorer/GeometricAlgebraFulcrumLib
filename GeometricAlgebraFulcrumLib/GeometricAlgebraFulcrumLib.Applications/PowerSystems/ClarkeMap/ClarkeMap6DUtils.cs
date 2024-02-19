namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.ClarkeMap;

public static partial class ClarkeMapUtils
{
    public static double[] ClarkeRotate6D(this double[] uVector)
    {
        const int n = 6;

        var vVector = new double[n];

        //Begin GA-FuL MetaContext Code Generation, 2022-11-01T17:20:46.4437273+02:00
        //MetaContext: Clarke Transformation
        var temp0 = 0.16666666666666666 * 10 * Math.PI;
        var temp1 = Math.Cos(temp0);
        var temp2 = 1 / Math.Sqrt(3);
        temp1 *= temp2;
        temp1 *= uVector[5];
        var temp3 = temp2 * uVector[0];
        var temp4 = 0.16666666666666666 * 2 * Math.PI;
        var temp5 = Math.Cos(temp4);
        temp5 = temp2 * temp5;
        temp5 *= uVector[1];
        temp5 = temp3 + temp5;
        var temp6 = 4 * Math.PI;
        temp6 = 0.16666666666666666 * temp6;
        var temp7 = Math.Cos(temp6);
        temp7 = temp2 * temp7;
        var temp8 = temp7 * uVector[2];
        temp5 += temp8;
        temp8 = 6 * Math.PI;
        temp8 = 0.16666666666666666 * temp8;
        var temp9 = Math.Cos(temp8);
        temp9 = temp2 * temp9;
        temp9 *= uVector[3];
        temp5 += temp9;
        temp9 = 8 * Math.PI;
        temp9 = 0.16666666666666666 * temp9;
        var temp10 = Math.Cos(temp9);
        temp10 = temp2 * temp10;
        var temp11 = temp10 * uVector[4];
        temp5 += temp11;
        vVector[0] = temp1 + temp5;
            
        temp0 = Math.Sin(temp0);
        temp0 = temp2 * temp0;
        temp0 *= uVector[5];
        temp1 = Math.Sin(temp4);
        temp1 = temp2 * temp1;
        temp1 *= uVector[1];
        temp4 = Math.Sin(temp6);
        temp4 = temp2 * temp4;
        temp5 = temp4 * uVector[2];
        temp1 += temp5;
        temp5 = Math.Sin(temp8);
        temp5 = temp2 * temp5;
        temp5 *= uVector[3];
        temp1 += temp5;
        temp5 = Math.Sin(temp9);
        temp5 = temp2 * temp5;
        temp6 = temp5 * uVector[4];
        temp1 += temp6;
        vVector[1] = temp0 + temp1;
            
        temp0 = 16 * Math.PI;
        temp0 = 0.16666666666666666 * temp0;
        temp1 = Math.Cos(temp0);
        temp1 = temp2 * temp1;
        temp1 *= uVector[4];
        temp6 = temp7 * uVector[1];
        temp3 += temp6;
        temp6 = temp10 * uVector[2];
        temp3 += temp6;
        temp6 = 12 * Math.PI;
        temp6 = 0.16666666666666666 * temp6;
        temp7 = Math.Cos(temp6);
        temp7 = temp2 * temp7;
        temp7 *= uVector[3];
        temp3 += temp7;
        temp1 += temp3;
        temp3 = 20 * Math.PI;
        temp3 = 0.16666666666666666 * temp3;
        temp7 = Math.Cos(temp3);
        temp7 = temp2 * temp7;
        temp7 *= uVector[5];
        vVector[2] = temp1 + temp7;
            
        temp0 = Math.Sin(temp0);
        temp0 = temp2 * temp0;
        temp0 *= uVector[4];
        temp1 = temp4 * uVector[1];
        temp4 = temp5 * uVector[2];
        temp1 += temp4;
        temp4 = Math.Sin(temp6);
        temp4 = temp2 * temp4;
        temp4 *= uVector[3];
        temp1 += temp4;
        temp0 += temp1;
        temp1 = Math.Sin(temp3);
        temp1 = temp2 * temp1;
        temp1 *= uVector[5];
        vVector[3] = temp0 + temp1;
            
        temp0 = Math.Sqrt(6);
        temp1 = 1 / temp0;
        temp2 = temp1 * uVector[4];
        temp3 = temp1 * uVector[0];
        temp0 = 1 / temp0;
        temp0 = -temp0;
        temp4 = temp0 * uVector[1];
        temp4 = temp3 + temp4;
        temp5 = temp1 * uVector[2];
        temp4 += temp5;
        temp6 = temp0 * uVector[3];
        temp4 += temp6;
        temp4 = temp2 + temp4;
        temp0 *= uVector[5];
        vVector[4] = temp4 + temp0;
            
        temp0 = temp1 * uVector[1];
        temp0 = temp3 + temp0;
        temp0 = temp5 + temp0;
        temp3 = temp1 * uVector[3];
        temp0 += temp3;
        temp0 = temp2 + temp0;
        temp1 *= uVector[5];
        vVector[5] = temp0 + temp1;
            
        //Finish GA-FuL MetaContext Code Generation, 2022-11-01T17:20:46.4439535+02:00
            

        return vVector;
    }
}