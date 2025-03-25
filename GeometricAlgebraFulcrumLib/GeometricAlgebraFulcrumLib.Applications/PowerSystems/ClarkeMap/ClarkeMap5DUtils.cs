namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.ClarkeMap;

public static partial class ClarkeMapUtils
{
    public static double[] ClarkeRotate5D(this double[] uVector)
    {
        const int n = 5;

        var vVector = new double[n];

        //Begin GA-FuL MetaContext Code Generation, 2022-11-01T17:20:46.3369335+02:00
        //MetaContext: Clarke Transformation
        var temp0 = Math.Sqrt(0.4);
        var temp1 = temp0 * uVector[0];
        var temp2 = Math.Tau;
        temp2 = 0.2 * temp2;
        var temp3 = Math.Cos(temp2);
        temp3 = temp0 * temp3;
        temp3 *= uVector[1];
        temp3 = temp1 + temp3;
        var temp4 = 4 * Math.PI;
        temp4 = 0.2 * temp4;
        var temp5 = Math.Cos(temp4);
        temp5 = temp0 * temp5;
        var temp6 = temp5 * uVector[2];
        temp3 += temp6;
        temp6 = 6 * Math.PI;
        temp6 = 0.2 * temp6;
        var temp7 = Math.Cos(temp6);
        temp7 *= temp0;
        temp7 *= uVector[3];
        temp3 += temp7;
        temp7 = 8 * Math.PI;
        temp7 = 0.2 * temp7;
        var temp8 = Math.Cos(temp7);
        temp8 *= temp0;
        var temp9 = temp8 * uVector[4];
        vVector[0] = temp3 + temp9;
            
        temp2 = Math.Sin(temp2);
        temp2 *= temp0;
        temp2 *= uVector[1];
        temp3 = Math.Sin(temp4);
        temp3 *= temp0;
        temp4 = temp3 * uVector[2];
        temp2 += temp4;
        temp4 = Math.Sin(temp6);
        temp4 *= temp0;
        temp4 *= uVector[3];
        temp2 += temp4;
        temp4 = Math.Sin(temp7);
        temp4 *= temp0;
        temp6 = temp4 * uVector[4];
        vVector[1] = temp2 + temp6;
            
        temp2 = temp5 * uVector[1];
        temp1 = temp2 + temp1;
        temp2 = temp8 * uVector[2];
        temp1 += temp2;
        temp2 = 12 * Math.PI;
        temp2 = 0.2 * temp2;
        temp5 = Math.Cos(temp2);
        temp5 *= temp0;
        temp5 *= uVector[3];
        temp1 += temp5;
        temp5 = 16 * Math.PI;
        temp5 = 0.2 * temp5;
        temp6 = Math.Cos(temp5);
        temp6 *= temp0;
        temp6 *= uVector[4];
        vVector[2] = temp1 + temp6;
            
        temp1 = temp3 * uVector[1];
        temp3 = temp4 * uVector[2];
        temp1 += temp3;
        temp2 = Math.Sin(temp2);
        temp2 *= temp0;
        temp2 *= uVector[3];
        temp1 += temp2;
        temp2 = Math.Sin(temp5);
        temp0 = temp2 * temp0;
        temp0 *= uVector[4];
        vVector[3] = temp1 + temp0;
            
        temp0 = Math.Sqrt(5);
        temp0 = 1 / temp0;
        temp1 = temp0 * uVector[0];
        temp2 = temp0 * uVector[1];
        temp1 += temp2;
        temp2 = temp0 * uVector[2];
        temp1 += temp2;
        temp2 = temp0 * uVector[3];
        temp1 += temp2;
        temp0 *= uVector[4];
        vVector[4] = temp1 + temp0;
            
        //Finish GA-FuL MetaContext Code Generation, 2022-11-01T17:20:46.3370385+02:00
            

        return vVector;
    }
}