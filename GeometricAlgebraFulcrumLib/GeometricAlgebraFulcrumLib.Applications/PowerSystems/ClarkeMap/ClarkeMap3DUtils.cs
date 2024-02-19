namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.ClarkeMap;

public static partial class ClarkeMapUtils
{
    public static double[] ClarkeRotate3D(this double[] uVector)
    {
        const int n = 3;

        var vVector = new double[n];

        //Begin GA-FuL MetaContext Code Generation, 2022-11-01T17:20:45.7396545+02:00
        //MetaContext: Clarke Transformation
        var temp0 = Math.Sqrt(0.6666666666666666);
        var temp1 = temp0 * uVector[0];
        var temp2 = 2 * Math.PI;
        temp2 = 0.3333333333333333 * temp2;
        var temp3 = Math.Cos(temp2);
        temp3 = temp0 * temp3;
        temp3 *= uVector[1];
        temp1 += temp3;
        temp3 = 4 * Math.PI;
        temp3 = 0.3333333333333333 * temp3;
        var temp4 = Math.Cos(temp3);
        temp4 = temp0 * temp4;
        temp4 *= uVector[2];
        vVector[0] = temp1 + temp4;
            
        temp1 = Math.Sin(temp2);
        temp1 = temp0 * temp1;
        temp1 *= uVector[1];
        temp2 = Math.Sin(temp3);
        temp0 *= temp2;
        temp0 *= uVector[2];
        vVector[1] = temp1 + temp0;
            
        temp0 = Math.Sqrt(3);
        temp0 = 1 / temp0;
        temp1 = temp0 * uVector[0];
        temp2 = temp0 * uVector[1];
        temp1 += temp2;
        temp0 *= uVector[2];
        vVector[2] = temp1 + temp0;
            
        //Finish GA-FuL MetaContext Code Generation, 2022-11-01T17:20:46.1393551+02:00
            

        return vVector;
    }
}