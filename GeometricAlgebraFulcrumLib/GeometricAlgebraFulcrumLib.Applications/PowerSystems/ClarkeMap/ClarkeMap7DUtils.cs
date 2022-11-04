namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.ClarkeMap
{
    public static partial class ClarkeMapUtils
    {
        public static double[] ClarkeRotate7D(this double[] uVector)
        {
            const int n = 7;

            var vVector = new double[n];

            //Begin GA-FuL MetaContext Code Generation, 2022-11-01T17:20:46.5983104+02:00
            //MetaContext: Clarke Transformation
            var temp0 = 6 * Math.PI;
            temp0 = 0.14285714285714285 * temp0;
            var temp1 = Math.Cos(temp0);
            var temp2 = Math.Sqrt(0.2857142857142857);
            temp1 *= temp2;
            var temp3 = temp1 * uVector[3];
            var temp4 = temp2 * uVector[0];
            var temp5 = 2 * Math.PI;
            temp5 = 0.14285714285714285 * temp5;
            var temp6 = Math.Cos(temp5);
            temp6 = temp2 * temp6;
            temp6 *= uVector[1];
            temp6 = temp4 + temp6;
            var temp7 = 4 * Math.PI;
            temp7 = 0.14285714285714285 * temp7;
            var temp8 = Math.Cos(temp7);
            temp8 = temp2 * temp8;
            var temp9 = temp8 * uVector[2];
            temp6 += temp9;
            temp3 += temp6;
            temp6 = 8 * Math.PI;
            temp6 = 0.14285714285714285 * temp6;
            temp9 = Math.Cos(temp6);
            temp9 = temp2 * temp9;
            var temp10 = temp9 * uVector[4];
            temp3 += temp10;
            temp10 = 10 * Math.PI;
            temp10 = 0.14285714285714285 * temp10;
            var temp11 = Math.Cos(temp10);
            temp11 = temp2 * temp11;
            temp11 *= uVector[5];
            temp3 += temp11;
            temp11 = 12 * Math.PI;
            temp11 = 0.14285714285714285 * temp11;
            var temp12 = Math.Cos(temp11);
            temp12 = temp2 * temp12;
            var temp13 = temp12 * uVector[6];
            vVector[0] = temp3 + temp13;
            
            temp3 = Math.Sin(temp7);
            temp3 = temp2 * temp3;
            temp7 = temp3 * uVector[2];
            temp5 = Math.Sin(temp5);
            temp5 = temp2 * temp5;
            temp5 *= uVector[1];
            temp5 = temp7 + temp5;
            temp0 = Math.Sin(temp0);
            temp0 = temp2 * temp0;
            temp7 = temp0 * uVector[3];
            temp5 += temp7;
            temp6 = Math.Sin(temp6);
            temp6 = temp2 * temp6;
            temp7 = temp6 * uVector[4];
            temp5 += temp7;
            temp7 = Math.Sin(temp10);
            temp7 = temp2 * temp7;
            temp7 *= uVector[5];
            temp5 += temp7;
            temp7 = Math.Sin(temp11);
            temp7 = temp2 * temp7;
            temp10 = temp7 * uVector[6];
            vVector[1] = temp5 + temp10;
            
            temp5 = temp9 * uVector[2];
            temp8 *= uVector[1];
            temp8 = temp4 + temp8;
            temp5 += temp8;
            temp8 = temp12 * uVector[3];
            temp5 += temp8;
            temp8 = 16 * Math.PI;
            temp8 = 0.14285714285714285 * temp8;
            temp9 = Math.Cos(temp8);
            temp9 = temp2 * temp9;
            temp9 *= uVector[4];
            temp5 += temp9;
            temp9 = 20 * Math.PI;
            temp9 = 0.14285714285714285 * temp9;
            temp10 = Math.Cos(temp9);
            temp10 = temp2 * temp10;
            temp10 *= uVector[5];
            temp5 += temp10;
            temp10 = 24 * Math.PI;
            temp10 = 0.14285714285714285 * temp10;
            temp11 = Math.Cos(temp10);
            temp11 = temp2 * temp11;
            temp13 = temp11 * uVector[6];
            vVector[2] = temp5 + temp13;
            
            temp5 = temp6 * uVector[2];
            temp3 *= uVector[1];
            temp3 = temp5 + temp3;
            temp5 = temp7 * uVector[3];
            temp3 += temp5;
            temp5 = Math.Sin(temp8);
            temp5 = temp2 * temp5;
            temp5 *= uVector[4];
            temp3 += temp5;
            temp5 = Math.Sin(temp9);
            temp5 = temp2 * temp5;
            temp5 *= uVector[5];
            temp3 += temp5;
            temp5 = Math.Sin(temp10);
            temp5 = temp2 * temp5;
            temp6 = temp5 * uVector[6];
            vVector[3] = temp3 + temp6;
            
            temp3 = temp12 * uVector[2];
            temp1 *= uVector[1];
            temp1 = temp4 + temp1;
            temp1 = temp3 + temp1;
            temp3 = 18 * Math.PI;
            temp3 = 0.14285714285714285 * temp3;
            temp4 = Math.Cos(temp3);
            temp4 = temp2 * temp4;
            temp4 *= uVector[3];
            temp1 += temp4;
            temp4 = temp11 * uVector[4];
            temp1 += temp4;
            temp4 = 30 * Math.PI;
            temp4 = 0.14285714285714285 * temp4;
            temp6 = Math.Cos(temp4);
            temp6 = temp2 * temp6;
            temp6 *= uVector[5];
            temp1 += temp6;
            temp6 = 36 * Math.PI;
            temp6 = 0.14285714285714285 * temp6;
            temp8 = Math.Cos(temp6);
            temp8 = temp2 * temp8;
            temp8 *= uVector[6];
            vVector[4] = temp1 + temp8;
            
            temp1 = temp7 * uVector[2];
            temp0 *= uVector[1];
            temp0 = temp1 + temp0;
            temp1 = Math.Sin(temp3);
            temp1 = temp2 * temp1;
            temp1 *= uVector[3];
            temp0 += temp1;
            temp1 = temp5 * uVector[4];
            temp0 += temp1;
            temp1 = Math.Sin(temp4);
            temp1 = temp2 * temp1;
            temp1 *= uVector[5];
            temp0 += temp1;
            temp1 = Math.Sin(temp6);
            temp1 = temp2 * temp1;
            temp1 *= uVector[6];
            vVector[5] = temp0 + temp1;
            
            temp0 = Math.Sqrt(7);
            temp0 = 1 / temp0;
            temp1 = temp0 * uVector[2];
            temp2 = temp0 * uVector[0];
            temp3 = temp0 * uVector[1];
            temp2 += temp3;
            temp1 += temp2;
            temp2 = temp0 * uVector[3];
            temp1 += temp2;
            temp2 = temp0 * uVector[4];
            temp1 += temp2;
            temp2 = temp0 * uVector[5];
            temp1 += temp2;
            temp0 *= uVector[6];
            vVector[6] = temp1 + temp0;
            
            //Finish GA-FuL MetaContext Code Generation, 2022-11-01T17:20:46.5984741+02:00
            

            return vVector;
        }
    }
}