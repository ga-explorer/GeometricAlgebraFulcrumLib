namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.ClarkeMap;

public static partial class ClarkeMapUtils
{
    public static double[] ClarkeRotate4D(this double[] uVector)
    {
        const int n = 4;

        var vVector = new double[n];

        //Begin GA-FuL MetaContext Code Generation, 2022-11-01T17:20:46.2459386+02:00
        //MetaContext: Clarke Transformation
        var temp0 = 1 / Math.Sqrt(2);
        var temp1 = temp0 * uVector[0];
        var temp2 = Math.Tau;
        temp2 = 0.25 * temp2;
        var temp3 = Math.Cos(temp2);
        temp3 = temp0 * temp3;
        temp3 *= uVector[1];
        temp1 += temp3;
        temp3 = 4 * Math.PI;
        temp3 = 0.25 * temp3;
        var temp4 = Math.Cos(temp3);
        temp4 = temp0 * temp4;
        temp4 *= uVector[2];
        temp1 += temp4;
        temp4 = 6 * Math.PI;
        temp4 = 0.25 * temp4;
        var temp5 = Math.Cos(temp4);
        temp5 = temp0 * temp5;
        temp5 *= uVector[3];
        vVector[0] = temp1 + temp5;
            
        temp1 = Math.Sin(temp2);
        temp1 = temp0 * temp1;
        temp1 *= uVector[1];
        temp2 = Math.Sin(temp3);
        temp2 = temp0 * temp2;
        temp2 *= uVector[2];
        temp1 += temp2;
        temp2 = Math.Sin(temp4);
        temp0 *= temp2;
        temp0 *= uVector[3];
        vVector[1] = temp1 + temp0;
            
        temp0 = 0.5 * uVector[0];
        temp1 = -0.5 * uVector[1];
        temp1 = temp0 + temp1;
        temp2 = 0.5 * uVector[2];
        temp1 += temp2;
        temp3 = -0.5 * uVector[3];
        vVector[2] = temp1 + temp3;
            
        temp1 = 0.5 * uVector[1];
        temp0 += temp1;
        temp0 = temp2 + temp0;
        temp1 = 0.5 * uVector[3];
        vVector[3] = temp0 + temp1;
            
        //Finish GA-FuL MetaContext Code Generation, 2022-11-01T17:20:46.2461589+02:00
            

        return vVector;
    }
}