namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.ClarkeMap;

public static partial class ClarkeMapUtils
{
    public static double[] ClarkeRotate8D(this double[] uVector)
    {
        const int n = 8;

        var vVector = new double[n];

        //Begin GA-FuL MetaContext Code Generation, 2022-11-01T17:20:46.7768751+02:00
        //MetaContext: Clarke Transformation
        var temp0 = Math.Tau;
        temp0 = 0.125 * temp0;
        var temp1 = Math.Cos(temp0);
        temp1 = 0.5 * temp1;
        temp1 *= uVector[1];
        var temp2 = 0.5 * uVector[0];
        temp1 += temp2;
        var temp3 = 4 * Math.PI;
        temp3 = 0.125 * temp3;
        var temp4 = Math.Cos(temp3);
        temp4 = 0.5 * temp4;
        var temp5 = temp4 * uVector[2];
        temp1 += temp5;
        temp5 = 6 * Math.PI;
        temp5 = 0.125 * temp5;
        var temp6 = Math.Cos(temp5);
        temp6 = 0.5 * temp6;
        var temp7 = temp6 * uVector[3];
        temp1 += temp7;
        temp7 = 8 * Math.PI;
        temp7 = 0.125 * temp7;
        var temp8 = Math.Cos(temp7);
        temp8 = 0.5 * temp8;
        var temp9 = temp8 * uVector[4];
        temp1 += temp9;
        temp9 = 10 * Math.PI;
        temp9 = 0.125 * temp9;
        var temp10 = Math.Cos(temp9);
        temp10 = 0.5 * temp10;
        temp10 *= uVector[5];
        temp1 += temp10;
        temp10 = 12 * Math.PI;
        temp10 = 0.125 * temp10;
        var temp11 = Math.Cos(temp10);
        temp11 = 0.5 * temp11;
        var temp12 = temp11 * uVector[6];
        temp1 += temp12;
        temp12 = 14 * Math.PI;
        temp12 = 0.125 * temp12;
        var temp13 = Math.Cos(temp12);
        temp13 = 0.5 * temp13;
        temp13 *= uVector[7];
        vVector[0] = temp1 + temp13;
            
        temp0 = Math.Sin(temp0);
        temp0 = 0.5 * temp0;
        temp0 *= uVector[1];
        temp1 = Math.Sin(temp3);
        temp1 = 0.5 * temp1;
        temp3 = temp1 * uVector[2];
        temp0 += temp3;
        temp3 = Math.Sin(temp5);
        temp3 = 0.5 * temp3;
        temp5 = temp3 * uVector[3];
        temp0 += temp5;
        temp5 = Math.Sin(temp7);
        temp5 = 0.5 * temp5;
        temp7 = temp5 * uVector[4];
        temp0 += temp7;
        temp7 = Math.Sin(temp9);
        temp7 = 0.5 * temp7;
        temp7 *= uVector[5];
        temp0 += temp7;
        temp7 = Math.Sin(temp10);
        temp7 = 0.5 * temp7;
        temp9 = temp7 * uVector[6];
        temp0 += temp9;
        temp9 = Math.Sin(temp12);
        temp9 = 0.5 * temp9;
        temp9 *= uVector[7];
        vVector[1] = temp0 + temp9;
            
        temp0 = temp4 * uVector[1];
        temp0 = temp2 + temp0;
        temp4 = temp8 * uVector[2];
        temp0 += temp4;
        temp4 = temp11 * uVector[3];
        temp0 += temp4;
        temp4 = 16 * Math.PI;
        temp4 = 0.125 * temp4;
        temp8 = Math.Cos(temp4);
        temp8 = 0.5 * temp8;
        temp8 *= uVector[4];
        temp0 += temp8;
        temp8 = 20 * Math.PI;
        temp8 = 0.125 * temp8;
        temp9 = Math.Cos(temp8);
        temp9 = 0.5 * temp9;
        temp9 *= uVector[5];
        temp0 += temp9;
        temp9 = 24 * Math.PI;
        temp9 = 0.125 * temp9;
        temp10 = Math.Cos(temp9);
        temp10 = 0.5 * temp10;
        temp12 = temp10 * uVector[6];
        temp0 += temp12;
        temp12 = 28 * Math.PI;
        temp12 = 0.125 * temp12;
        temp13 = Math.Cos(temp12);
        temp13 = 0.5 * temp13;
        temp13 *= uVector[7];
        vVector[2] = temp0 + temp13;
            
        temp0 = temp1 * uVector[1];
        temp1 = temp5 * uVector[2];
        temp0 += temp1;
        temp1 = temp7 * uVector[3];
        temp0 += temp1;
        temp1 = Math.Sin(temp4);
        temp1 = 0.5 * temp1;
        temp1 *= uVector[4];
        temp0 += temp1;
        temp1 = Math.Sin(temp8);
        temp1 = 0.5 * temp1;
        temp1 *= uVector[5];
        temp0 += temp1;
        temp1 = Math.Sin(temp9);
        temp1 = 0.5 * temp1;
        temp4 = temp1 * uVector[6];
        temp0 += temp4;
        temp4 = Math.Sin(temp12);
        temp4 = 0.5 * temp4;
        temp4 *= uVector[7];
        vVector[3] = temp0 + temp4;
            
        temp0 = temp6 * uVector[1];
        temp0 = temp2 + temp0;
        temp2 = temp11 * uVector[2];
        temp0 += temp2;
        temp2 = 18 * Math.PI;
        temp2 = 0.125 * temp2;
        temp4 = Math.Cos(temp2);
        temp4 = 0.5 * temp4;
        temp4 *= uVector[3];
        temp0 += temp4;
        temp4 = temp10 * uVector[4];
        temp0 += temp4;
        temp4 = 30 * Math.PI;
        temp4 = 0.125 * temp4;
        temp5 = Math.Cos(temp4);
        temp5 = 0.5 * temp5;
        temp5 *= uVector[5];
        temp0 += temp5;
        temp5 = 36 * Math.PI;
        temp5 = 0.125 * temp5;
        temp6 = Math.Cos(temp5);
        temp6 = 0.5 * temp6;
        temp6 *= uVector[6];
        temp0 += temp6;
        temp6 = 42 * Math.PI;
        temp6 = 0.125 * temp6;
        temp8 = Math.Cos(temp6);
        temp8 = 0.5 * temp8;
        temp8 *= uVector[7];
        vVector[4] = temp0 + temp8;
            
        temp0 = temp3 * uVector[1];
        temp3 = temp7 * uVector[2];
        temp0 += temp3;
        temp2 = Math.Sin(temp2);
        temp2 = 0.5 * temp2;
        temp2 *= uVector[3];
        temp0 += temp2;
        temp1 *= uVector[4];
        temp0 += temp1;
        temp1 = Math.Sin(temp4);
        temp1 = 0.5 * temp1;
        temp1 *= uVector[5];
        temp0 += temp1;
        temp1 = Math.Sin(temp5);
        temp1 = 0.5 * temp1;
        temp1 *= uVector[6];
        temp0 += temp1;
        temp1 = Math.Sin(temp6);
        temp1 = 0.5 * temp1;
        temp1 *= uVector[7];
        vVector[5] = temp0 + temp1;
            
        temp0 = Math.Sqrt(2);
        temp0 = 2 * temp0;
        temp1 = 1 / temp0;
        temp2 = temp1 * uVector[0];
        temp0 = 1 / temp0;
        temp0 = -temp0;
        temp3 = temp0 * uVector[1];
        temp3 = temp2 + temp3;
        temp4 = temp1 * uVector[2];
        temp3 += temp4;
        temp5 = temp0 * uVector[3];
        temp3 += temp5;
        temp5 = temp1 * uVector[4];
        temp3 += temp5;
        temp6 = temp0 * uVector[5];
        temp3 += temp6;
        temp6 = temp1 * uVector[6];
        temp3 += temp6;
        temp0 *= uVector[7];
        vVector[6] = temp3 + temp0;
            
        temp0 = temp1 * uVector[1];
        temp0 = temp2 + temp0;
        temp0 = temp4 + temp0;
        temp2 = temp1 * uVector[3];
        temp0 += temp2;
        temp0 = temp5 + temp0;
        temp2 = temp1 * uVector[5];
        temp0 += temp2;
        temp0 = temp6 + temp0;
        temp1 *= uVector[7];
        vVector[7] = temp0 + temp1;
            
        //Finish GA-FuL MetaContext Code Generation, 2022-11-01T17:20:46.7770878+02:00
            

        return vVector;
    }
}