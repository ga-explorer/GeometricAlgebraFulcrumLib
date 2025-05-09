namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.ClarkeMap;

public static partial class ClarkeMapUtils
{
    public static double[] ClarkeRotate19D(this double[] uVector)
    {
        const int n = 19;

        var vVector = new double[n];

        //Begin GA-FuL MetaContext Code Generation, 2022-11-01T17:20:52.6384131+02:00
        //MetaContext: Clarke Transformation
        var temp0 = Math.Sqrt(0.10526315789473684);
        var temp1 = 30 * Math.PI;
        temp1 = 0.05263157894736842 * temp1;
        var temp2 = Math.Cos(temp1);
        temp2 = temp0 * temp2;
        var temp3 = temp2 * uVector[15];
        var temp4 = temp0 * uVector[0];
        var temp5 = Math.Tau;
        temp5 = 0.05263157894736842 * temp5;
        var temp6 = Math.Cos(temp5);
        temp6 = temp0 * temp6;
        temp6 *= uVector[1];
        temp6 = temp4 + temp6;
        var temp7 = 4 * Math.PI;
        temp7 = 0.05263157894736842 * temp7;
        var temp8 = Math.Cos(temp7);
        temp8 = temp0 * temp8;
        var temp9 = temp8 * uVector[2];
        temp6 += temp9;
        temp9 = 6 * Math.PI;
        temp9 = 0.05263157894736842 * temp9;
        var temp10 = Math.Cos(temp9);
        temp10 = temp0 * temp10;
        var temp11 = temp10 * uVector[3];
        temp6 += temp11;
        temp11 = 8 * Math.PI;
        temp11 = 0.05263157894736842 * temp11;
        var temp12 = Math.Cos(temp11);
        temp12 = temp0 * temp12;
        var temp13 = temp12 * uVector[4];
        temp6 += temp13;
        temp13 = 10 * Math.PI;
        temp13 = 0.05263157894736842 * temp13;
        var temp14 = Math.Cos(temp13);
        temp14 = temp0 * temp14;
        var temp15 = temp14 * uVector[5];
        temp6 += temp15;
        temp15 = 12 * Math.PI;
        temp15 = 0.05263157894736842 * temp15;
        var temp16 = Math.Cos(temp15);
        temp16 = temp0 * temp16;
        var temp17 = temp16 * uVector[6];
        temp6 += temp17;
        temp17 = 14 * Math.PI;
        temp17 = 0.05263157894736842 * temp17;
        var temp18 = Math.Cos(temp17);
        temp18 = temp0 * temp18;
        var temp19 = temp18 * uVector[7];
        temp6 += temp19;
        temp19 = 16 * Math.PI;
        temp19 = 0.05263157894736842 * temp19;
        var temp20 = Math.Cos(temp19);
        temp20 = temp0 * temp20;
        var temp21 = temp20 * uVector[8];
        temp6 += temp21;
        temp21 = 18 * Math.PI;
        temp21 = 0.05263157894736842 * temp21;
        var temp22 = Math.Cos(temp21);
        temp22 = temp0 * temp22;
        var temp23 = temp22 * uVector[9];
        temp6 += temp23;
        temp23 = 20 * Math.PI;
        temp23 = 0.05263157894736842 * temp23;
        var temp24 = Math.Cos(temp23);
        temp24 = temp0 * temp24;
        var temp25 = temp24 * uVector[10];
        temp6 += temp25;
        temp25 = 22 * Math.PI;
        temp25 = 0.05263157894736842 * temp25;
        var temp26 = Math.Cos(temp25);
        temp26 = temp0 * temp26;
        temp26 *= uVector[11];
        temp6 += temp26;
        temp26 = 24 * Math.PI;
        temp26 = 0.05263157894736842 * temp26;
        var temp27 = Math.Cos(temp26);
        temp27 = temp0 * temp27;
        var temp28 = temp27 * uVector[12];
        temp6 += temp28;
        temp28 = 26 * Math.PI;
        temp28 = 0.05263157894736842 * temp28;
        var temp29 = Math.Cos(temp28);
        temp29 = temp0 * temp29;
        temp29 *= uVector[13];
        temp6 += temp29;
        temp29 = 28 * Math.PI;
        temp29 = 0.05263157894736842 * temp29;
        var temp30 = Math.Cos(temp29);
        temp30 = temp0 * temp30;
        var temp31 = temp30 * uVector[14];
        temp6 += temp31;
        temp3 += temp6;
        temp6 = 32 * Math.PI;
        temp6 = 0.05263157894736842 * temp6;
        temp31 = Math.Cos(temp6);
        temp31 = temp0 * temp31;
        var temp32 = temp31 * uVector[16];
        temp3 += temp32;
        temp32 = 34 * Math.PI;
        temp32 = 0.05263157894736842 * temp32;
        var temp33 = Math.Cos(temp32);
        temp33 = temp0 * temp33;
        temp33 *= uVector[17];
        temp3 += temp33;
        temp33 = 36 * Math.PI;
        temp33 = 0.05263157894736842 * temp33;
        var temp34 = Math.Cos(temp33);
        temp34 = temp0 * temp34;
        var temp35 = temp34 * uVector[18];
        vVector[0] = temp3 + temp35;
            
        temp1 = Math.Sin(temp1);
        temp1 = temp0 * temp1;
        temp3 = temp1 * uVector[15];
        temp5 = Math.Sin(temp5);
        temp5 = temp0 * temp5;
        temp5 *= uVector[1];
        temp7 = Math.Sin(temp7);
        temp7 = temp0 * temp7;
        temp35 = temp7 * uVector[2];
        temp5 += temp35;
        temp9 = Math.Sin(temp9);
        temp9 = temp0 * temp9;
        temp35 = temp9 * uVector[3];
        temp5 += temp35;
        temp11 = Math.Sin(temp11);
        temp11 = temp0 * temp11;
        temp35 = temp11 * uVector[4];
        temp5 += temp35;
        temp13 = Math.Sin(temp13);
        temp13 = temp0 * temp13;
        temp35 = temp13 * uVector[5];
        temp5 += temp35;
        temp15 = Math.Sin(temp15);
        temp15 = temp0 * temp15;
        temp35 = temp15 * uVector[6];
        temp5 += temp35;
        temp17 = Math.Sin(temp17);
        temp17 = temp0 * temp17;
        temp35 = temp17 * uVector[7];
        temp5 += temp35;
        temp19 = Math.Sin(temp19);
        temp19 = temp0 * temp19;
        temp35 = temp19 * uVector[8];
        temp5 += temp35;
        temp21 = Math.Sin(temp21);
        temp21 = temp0 * temp21;
        temp35 = temp21 * uVector[9];
        temp5 += temp35;
        temp23 = Math.Sin(temp23);
        temp23 = temp0 * temp23;
        temp35 = temp23 * uVector[10];
        temp5 += temp35;
        temp25 = Math.Sin(temp25);
        temp25 = temp0 * temp25;
        temp25 *= uVector[11];
        temp5 += temp25;
        temp25 = Math.Sin(temp26);
        temp25 = temp0 * temp25;
        temp26 = temp25 * uVector[12];
        temp5 += temp26;
        temp26 = Math.Sin(temp28);
        temp26 = temp0 * temp26;
        temp26 *= uVector[13];
        temp5 += temp26;
        temp26 = Math.Sin(temp29);
        temp26 = temp0 * temp26;
        temp28 = temp26 * uVector[14];
        temp5 += temp28;
        temp3 += temp5;
        temp5 = Math.Sin(temp6);
        temp5 = temp0 * temp5;
        temp6 = temp5 * uVector[16];
        temp3 += temp6;
        temp6 = Math.Sin(temp32);
        temp6 = temp0 * temp6;
        temp6 *= uVector[17];
        temp3 += temp6;
        temp6 = Math.Sin(temp33);
        temp6 = temp0 * temp6;
        temp28 = temp6 * uVector[18];
        vVector[1] = temp3 + temp28;
            
        temp3 = 60 * Math.PI;
        temp3 = 0.05263157894736842 * temp3;
        temp28 = Math.Cos(temp3);
        temp28 = temp0 * temp28;
        temp29 = temp28 * uVector[15];
        temp8 *= uVector[1];
        temp8 = temp4 + temp8;
        temp32 = temp12 * uVector[2];
        temp8 += temp32;
        temp32 = temp16 * uVector[3];
        temp8 += temp32;
        temp32 = temp20 * uVector[4];
        temp8 += temp32;
        temp32 = temp24 * uVector[5];
        temp8 += temp32;
        temp32 = temp27 * uVector[6];
        temp8 += temp32;
        temp32 = temp30 * uVector[7];
        temp8 += temp32;
        temp32 = temp31 * uVector[8];
        temp8 += temp32;
        temp32 = temp34 * uVector[9];
        temp8 += temp32;
        temp32 = 40 * Math.PI;
        temp32 = 0.05263157894736842 * temp32;
        temp33 = Math.Cos(temp32);
        temp33 = temp0 * temp33;
        temp35 = temp33 * uVector[10];
        temp8 += temp35;
        temp35 = 44 * Math.PI;
        temp35 = 0.05263157894736842 * temp35;
        var temp36 = Math.Cos(temp35);
        temp36 = temp0 * temp36;
        temp36 *= uVector[11];
        temp8 += temp36;
        temp36 = 48 * Math.PI;
        temp36 = 0.05263157894736842 * temp36;
        var temp37 = Math.Cos(temp36);
        temp37 = temp0 * temp37;
        var temp38 = temp37 * uVector[12];
        temp8 += temp38;
        temp38 = 52 * Math.PI;
        temp38 = 0.05263157894736842 * temp38;
        var temp39 = Math.Cos(temp38);
        temp39 = temp0 * temp39;
        temp39 *= uVector[13];
        temp8 += temp39;
        temp39 = 56 * Math.PI;
        temp39 = 0.05263157894736842 * temp39;
        var temp40 = Math.Cos(temp39);
        temp40 = temp0 * temp40;
        var temp41 = temp40 * uVector[14];
        temp8 += temp41;
        temp8 = temp29 + temp8;
        temp29 = 64 * Math.PI;
        temp29 = 0.05263157894736842 * temp29;
        temp41 = Math.Cos(temp29);
        temp41 = temp0 * temp41;
        var temp42 = temp41 * uVector[16];
        temp8 += temp42;
        temp42 = 68 * Math.PI;
        temp42 = 0.05263157894736842 * temp42;
        var temp43 = Math.Cos(temp42);
        temp43 = temp0 * temp43;
        temp43 *= uVector[17];
        temp8 += temp43;
        temp43 = 72 * Math.PI;
        temp43 = 0.05263157894736842 * temp43;
        var temp44 = Math.Cos(temp43);
        temp44 = temp0 * temp44;
        var temp45 = temp44 * uVector[18];
        vVector[2] = temp8 + temp45;
            
        temp3 = Math.Sin(temp3);
        temp3 = temp0 * temp3;
        temp8 = temp3 * uVector[15];
        temp7 *= uVector[1];
        temp45 = temp11 * uVector[2];
        temp7 += temp45;
        temp45 = temp15 * uVector[3];
        temp7 += temp45;
        temp45 = temp19 * uVector[4];
        temp7 += temp45;
        temp45 = temp23 * uVector[5];
        temp7 += temp45;
        temp45 = temp25 * uVector[6];
        temp7 += temp45;
        temp45 = temp26 * uVector[7];
        temp7 += temp45;
        temp45 = temp5 * uVector[8];
        temp7 += temp45;
        temp45 = temp6 * uVector[9];
        temp7 += temp45;
        temp32 = Math.Sin(temp32);
        temp32 = temp0 * temp32;
        temp45 = temp32 * uVector[10];
        temp7 += temp45;
        temp35 = Math.Sin(temp35);
        temp35 = temp0 * temp35;
        temp35 *= uVector[11];
        temp7 += temp35;
        temp35 = Math.Sin(temp36);
        temp35 = temp0 * temp35;
        temp36 = temp35 * uVector[12];
        temp7 += temp36;
        temp36 = Math.Sin(temp38);
        temp36 = temp0 * temp36;
        temp36 *= uVector[13];
        temp7 += temp36;
        temp36 = Math.Sin(temp39);
        temp36 = temp0 * temp36;
        temp38 = temp36 * uVector[14];
        temp7 += temp38;
        temp7 = temp8 + temp7;
        temp8 = Math.Sin(temp29);
        temp8 = temp0 * temp8;
        temp29 = temp8 * uVector[16];
        temp7 += temp29;
        temp29 = Math.Sin(temp42);
        temp29 = temp0 * temp29;
        temp29 *= uVector[17];
        temp7 += temp29;
        temp29 = Math.Sin(temp43);
        temp29 = temp0 * temp29;
        temp38 = temp29 * uVector[18];
        vVector[3] = temp7 + temp38;
            
        temp7 = 84 * Math.PI;
        temp7 = 0.05263157894736842 * temp7;
        temp38 = Math.Cos(temp7);
        temp38 = temp0 * temp38;
        temp39 = temp38 * uVector[14];
        temp10 *= uVector[1];
        temp10 = temp4 + temp10;
        temp42 = temp16 * uVector[2];
        temp10 += temp42;
        temp42 = temp22 * uVector[3];
        temp10 += temp42;
        temp42 = temp27 * uVector[4];
        temp10 += temp42;
        temp42 = temp2 * uVector[5];
        temp10 += temp42;
        temp42 = temp34 * uVector[6];
        temp10 += temp42;
        temp42 = 42 * Math.PI;
        temp42 = 0.05263157894736842 * temp42;
        temp43 = Math.Cos(temp42);
        temp43 = temp0 * temp43;
        temp45 = temp43 * uVector[7];
        temp10 += temp45;
        temp45 = temp37 * uVector[8];
        temp10 += temp45;
        temp45 = 54 * Math.PI;
        temp45 = 0.05263157894736842 * temp45;
        var temp46 = Math.Cos(temp45);
        temp46 = temp0 * temp46;
        var temp47 = temp46 * uVector[9];
        temp10 += temp47;
        temp47 = temp28 * uVector[10];
        temp10 += temp47;
        temp47 = 66 * Math.PI;
        temp47 = 0.05263157894736842 * temp47;
        var temp48 = Math.Cos(temp47);
        temp48 = temp0 * temp48;
        temp48 *= uVector[11];
        temp10 += temp48;
        temp48 = temp44 * uVector[12];
        temp10 += temp48;
        temp48 = 78 * Math.PI;
        temp48 = 0.05263157894736842 * temp48;
        var temp49 = Math.Cos(temp48);
        temp49 = temp0 * temp49;
        temp49 *= uVector[13];
        temp10 += temp49;
        temp10 = temp39 + temp10;
        temp39 = 90 * Math.PI;
        temp39 = 0.05263157894736842 * temp39;
        temp49 = Math.Cos(temp39);
        temp49 = temp0 * temp49;
        var temp50 = temp49 * uVector[15];
        temp10 += temp50;
        temp50 = 96 * Math.PI;
        temp50 = 0.05263157894736842 * temp50;
        var temp51 = Math.Cos(temp50);
        temp51 = temp0 * temp51;
        var temp52 = temp51 * uVector[16];
        temp10 += temp52;
        temp52 = 102 * Math.PI;
        temp52 = 0.05263157894736842 * temp52;
        var temp53 = Math.Cos(temp52);
        temp53 = temp0 * temp53;
        temp53 *= uVector[17];
        temp10 += temp53;
        temp53 = 108 * Math.PI;
        temp53 = 0.05263157894736842 * temp53;
        var temp54 = Math.Cos(temp53);
        temp54 = temp0 * temp54;
        var temp55 = temp54 * uVector[18];
        vVector[4] = temp10 + temp55;
            
        temp7 = Math.Sin(temp7);
        temp7 = temp0 * temp7;
        temp10 = temp7 * uVector[14];
        temp9 *= uVector[1];
        temp55 = temp15 * uVector[2];
        temp9 += temp55;
        temp55 = temp21 * uVector[3];
        temp9 += temp55;
        temp55 = temp25 * uVector[4];
        temp9 += temp55;
        temp55 = temp1 * uVector[5];
        temp9 += temp55;
        temp55 = temp6 * uVector[6];
        temp9 += temp55;
        temp42 = Math.Sin(temp42);
        temp42 = temp0 * temp42;
        temp55 = temp42 * uVector[7];
        temp9 += temp55;
        temp55 = temp35 * uVector[8];
        temp9 += temp55;
        temp45 = Math.Sin(temp45);
        temp45 = temp0 * temp45;
        temp55 = temp45 * uVector[9];
        temp9 += temp55;
        temp55 = temp3 * uVector[10];
        temp9 += temp55;
        temp47 = Math.Sin(temp47);
        temp47 = temp0 * temp47;
        temp47 *= uVector[11];
        temp9 += temp47;
        temp47 = temp29 * uVector[12];
        temp9 += temp47;
        temp47 = Math.Sin(temp48);
        temp47 = temp0 * temp47;
        temp47 *= uVector[13];
        temp9 += temp47;
        temp9 = temp10 + temp9;
        temp10 = Math.Sin(temp39);
        temp10 = temp0 * temp10;
        temp39 = temp10 * uVector[15];
        temp9 += temp39;
        temp39 = Math.Sin(temp50);
        temp39 = temp0 * temp39;
        temp47 = temp39 * uVector[16];
        temp9 += temp47;
        temp47 = Math.Sin(temp52);
        temp47 = temp0 * temp47;
        temp47 *= uVector[17];
        temp9 += temp47;
        temp47 = Math.Sin(temp53);
        temp47 = temp0 * temp47;
        temp48 = temp47 * uVector[18];
        vVector[5] = temp9 + temp48;
            
        temp9 = 112 * Math.PI;
        temp9 = 0.05263157894736842 * temp9;
        temp48 = Math.Cos(temp9);
        temp48 = temp0 * temp48;
        temp50 = temp48 * uVector[14];
        temp12 *= uVector[1];
        temp12 = temp4 + temp12;
        temp52 = temp20 * uVector[2];
        temp12 += temp52;
        temp52 = temp27 * uVector[3];
        temp12 += temp52;
        temp52 = temp31 * uVector[4];
        temp12 += temp52;
        temp52 = temp33 * uVector[5];
        temp12 += temp52;
        temp52 = temp37 * uVector[6];
        temp12 += temp52;
        temp52 = temp40 * uVector[7];
        temp12 += temp52;
        temp52 = temp41 * uVector[8];
        temp12 += temp52;
        temp52 = temp44 * uVector[9];
        temp12 += temp52;
        temp52 = 80 * Math.PI;
        temp52 = 0.05263157894736842 * temp52;
        temp53 = Math.Cos(temp52);
        temp53 = temp0 * temp53;
        temp55 = temp53 * uVector[10];
        temp12 += temp55;
        temp55 = 88 * Math.PI;
        temp55 = 0.05263157894736842 * temp55;
        var temp56 = Math.Cos(temp55);
        temp56 = temp0 * temp56;
        temp56 *= uVector[11];
        temp12 += temp56;
        temp56 = temp51 * uVector[12];
        temp12 += temp56;
        temp56 = 104 * Math.PI;
        temp56 = 0.05263157894736842 * temp56;
        var temp57 = Math.Cos(temp56);
        temp57 = temp0 * temp57;
        temp57 *= uVector[13];
        temp12 += temp57;
        temp12 = temp50 + temp12;
        temp50 = 120 * Math.PI;
        temp50 = 0.05263157894736842 * temp50;
        temp57 = Math.Cos(temp50);
        temp57 = temp0 * temp57;
        var temp58 = temp57 * uVector[15];
        temp12 += temp58;
        temp58 = 128 * Math.PI;
        temp58 = 0.05263157894736842 * temp58;
        var temp59 = Math.Cos(temp58);
        temp59 = temp0 * temp59;
        var temp60 = temp59 * uVector[16];
        temp12 += temp60;
        temp60 = 136 * Math.PI;
        temp60 = 0.05263157894736842 * temp60;
        var temp61 = Math.Cos(temp60);
        temp61 = temp0 * temp61;
        temp61 *= uVector[17];
        temp12 += temp61;
        temp61 = 144 * Math.PI;
        temp61 = 0.05263157894736842 * temp61;
        var temp62 = Math.Cos(temp61);
        temp62 = temp0 * temp62;
        var temp63 = temp62 * uVector[18];
        vVector[6] = temp12 + temp63;
            
        temp9 = Math.Sin(temp9);
        temp9 = temp0 * temp9;
        temp12 = temp9 * uVector[14];
        temp11 *= uVector[1];
        temp63 = temp19 * uVector[2];
        temp11 += temp63;
        temp63 = temp25 * uVector[3];
        temp11 += temp63;
        temp63 = temp5 * uVector[4];
        temp11 += temp63;
        temp63 = temp32 * uVector[5];
        temp11 += temp63;
        temp63 = temp35 * uVector[6];
        temp11 += temp63;
        temp63 = temp36 * uVector[7];
        temp11 += temp63;
        temp63 = temp8 * uVector[8];
        temp11 += temp63;
        temp63 = temp29 * uVector[9];
        temp11 += temp63;
        temp52 = Math.Sin(temp52);
        temp52 = temp0 * temp52;
        temp63 = temp52 * uVector[10];
        temp11 += temp63;
        temp55 = Math.Sin(temp55);
        temp55 = temp0 * temp55;
        temp55 *= uVector[11];
        temp11 += temp55;
        temp55 = temp39 * uVector[12];
        temp11 += temp55;
        temp55 = Math.Sin(temp56);
        temp55 = temp0 * temp55;
        temp55 *= uVector[13];
        temp11 += temp55;
        temp11 = temp12 + temp11;
        temp12 = Math.Sin(temp50);
        temp12 = temp0 * temp12;
        temp50 = temp12 * uVector[15];
        temp11 += temp50;
        temp50 = Math.Sin(temp58);
        temp50 = temp0 * temp50;
        temp55 = temp50 * uVector[16];
        temp11 += temp55;
        temp55 = Math.Sin(temp60);
        temp55 = temp0 * temp55;
        temp55 *= uVector[17];
        temp11 += temp55;
        temp55 = Math.Sin(temp61);
        temp55 = temp0 * temp55;
        temp56 = temp55 * uVector[18];
        vVector[7] = temp11 + temp56;
            
        temp11 = 140 * Math.PI;
        temp11 = 0.05263157894736842 * temp11;
        temp56 = Math.Cos(temp11);
        temp56 = temp0 * temp56;
        temp58 = temp56 * uVector[14];
        temp14 *= uVector[1];
        temp14 = temp4 + temp14;
        temp24 *= uVector[2];
        temp14 += temp24;
        temp2 *= uVector[3];
        temp2 = temp14 + temp2;
        temp14 = temp33 * uVector[4];
        temp2 += temp14;
        temp14 = 50 * Math.PI;
        temp14 = 0.05263157894736842 * temp14;
        temp24 = Math.Cos(temp14);
        temp24 = temp0 * temp24;
        temp24 *= uVector[5];
        temp2 += temp24;
        temp24 = temp28 * uVector[6];
        temp2 += temp24;
        temp24 = 70 * Math.PI;
        temp24 = 0.05263157894736842 * temp24;
        temp33 = Math.Cos(temp24);
        temp33 = temp0 * temp33;
        temp60 = temp33 * uVector[7];
        temp2 += temp60;
        temp60 = temp53 * uVector[8];
        temp2 += temp60;
        temp60 = temp49 * uVector[9];
        temp2 += temp60;
        temp60 = 100 * Math.PI;
        temp60 = 0.05263157894736842 * temp60;
        temp61 = Math.Cos(temp60);
        temp61 = temp0 * temp61;
        temp61 *= uVector[10];
        temp2 += temp61;
        temp61 = 110 * Math.PI;
        temp61 = 0.05263157894736842 * temp61;
        temp63 = Math.Cos(temp61);
        temp63 = temp0 * temp63;
        temp63 *= uVector[11];
        temp2 += temp63;
        temp63 = temp57 * uVector[12];
        temp2 += temp63;
        temp63 = 130 * Math.PI;
        temp63 = 0.05263157894736842 * temp63;
        var temp64 = Math.Cos(temp63);
        temp64 = temp0 * temp64;
        temp64 *= uVector[13];
        temp2 += temp64;
        temp2 = temp58 + temp2;
        temp58 = 150 * Math.PI;
        temp58 = 0.05263157894736842 * temp58;
        temp64 = Math.Cos(temp58);
        temp64 = temp0 * temp64;
        temp64 *= uVector[15];
        temp2 += temp64;
        temp64 = 160 * Math.PI;
        temp64 = 0.05263157894736842 * temp64;
        var temp65 = Math.Cos(temp64);
        temp65 = temp0 * temp65;
        var temp66 = temp65 * uVector[16];
        temp2 += temp66;
        temp66 = 170 * Math.PI;
        temp66 = 0.05263157894736842 * temp66;
        var temp67 = Math.Cos(temp66);
        temp67 = temp0 * temp67;
        temp67 *= uVector[17];
        temp2 += temp67;
        temp67 = 180 * Math.PI;
        temp67 = 0.05263157894736842 * temp67;
        var temp68 = Math.Cos(temp67);
        temp68 = temp0 * temp68;
        var temp69 = temp68 * uVector[18];
        vVector[8] = temp2 + temp69;
            
        temp2 = Math.Sin(temp11);
        temp2 = temp0 * temp2;
        temp11 = temp2 * uVector[14];
        temp13 *= uVector[1];
        temp23 *= uVector[2];
        temp13 += temp23;
        temp1 *= uVector[3];
        temp1 = temp13 + temp1;
        temp13 = temp32 * uVector[4];
        temp1 += temp13;
        temp13 = Math.Sin(temp14);
        temp13 = temp0 * temp13;
        temp13 *= uVector[5];
        temp1 += temp13;
        temp13 = temp3 * uVector[6];
        temp1 += temp13;
        temp13 = Math.Sin(temp24);
        temp13 = temp0 * temp13;
        temp14 = temp13 * uVector[7];
        temp1 += temp14;
        temp14 = temp52 * uVector[8];
        temp1 += temp14;
        temp14 = temp10 * uVector[9];
        temp1 += temp14;
        temp14 = Math.Sin(temp60);
        temp14 = temp0 * temp14;
        temp14 *= uVector[10];
        temp1 += temp14;
        temp14 = Math.Sin(temp61);
        temp14 = temp0 * temp14;
        temp14 *= uVector[11];
        temp1 += temp14;
        temp14 = temp12 * uVector[12];
        temp1 += temp14;
        temp14 = Math.Sin(temp63);
        temp14 = temp0 * temp14;
        temp14 *= uVector[13];
        temp1 += temp14;
        temp1 = temp11 + temp1;
        temp11 = Math.Sin(temp58);
        temp11 = temp0 * temp11;
        temp11 *= uVector[15];
        temp1 += temp11;
        temp11 = Math.Sin(temp64);
        temp11 = temp0 * temp11;
        temp14 = temp11 * uVector[16];
        temp1 += temp14;
        temp14 = Math.Sin(temp66);
        temp14 = temp0 * temp14;
        temp14 *= uVector[17];
        temp1 += temp14;
        temp14 = Math.Sin(temp67);
        temp14 = temp0 * temp14;
        temp23 = temp14 * uVector[18];
        vVector[9] = temp1 + temp23;
            
        temp1 = 168 * Math.PI;
        temp1 = 0.05263157894736842 * temp1;
        temp23 = Math.Cos(temp1);
        temp23 = temp0 * temp23;
        temp24 = temp23 * uVector[14];
        temp16 *= uVector[1];
        temp16 = temp4 + temp16;
        temp27 *= uVector[2];
        temp16 += temp27;
        temp27 = temp34 * uVector[3];
        temp16 += temp27;
        temp27 = temp37 * uVector[4];
        temp16 += temp27;
        temp27 = temp28 * uVector[5];
        temp16 += temp27;
        temp27 = temp44 * uVector[6];
        temp16 += temp27;
        temp27 = temp38 * uVector[7];
        temp16 += temp27;
        temp27 = temp51 * uVector[8];
        temp16 += temp27;
        temp27 = temp54 * uVector[9];
        temp16 += temp27;
        temp27 = temp57 * uVector[10];
        temp16 += temp27;
        temp27 = 132 * Math.PI;
        temp27 = 0.05263157894736842 * temp27;
        temp28 = Math.Cos(temp27);
        temp28 = temp0 * temp28;
        temp28 *= uVector[11];
        temp16 += temp28;
        temp28 = temp62 * uVector[12];
        temp16 += temp28;
        temp28 = 156 * Math.PI;
        temp28 = 0.05263157894736842 * temp28;
        temp32 = Math.Cos(temp28);
        temp32 = temp0 * temp32;
        temp32 *= uVector[13];
        temp16 += temp32;
        temp16 = temp24 + temp16;
        temp24 = temp68 * uVector[15];
        temp16 += temp24;
        temp24 = 192 * Math.PI;
        temp24 = 0.05263157894736842 * temp24;
        temp32 = Math.Cos(temp24);
        temp32 = temp0 * temp32;
        temp57 = temp32 * uVector[16];
        temp16 += temp57;
        temp57 = 204 * Math.PI;
        temp57 = 0.05263157894736842 * temp57;
        temp58 = Math.Cos(temp57);
        temp58 = temp0 * temp58;
        temp58 *= uVector[17];
        temp16 += temp58;
        temp58 = 216 * Math.PI;
        temp58 = 0.05263157894736842 * temp58;
        temp60 = Math.Cos(temp58);
        temp60 = temp0 * temp60;
        temp61 = temp60 * uVector[18];
        vVector[10] = temp16 + temp61;
            
        temp1 = Math.Sin(temp1);
        temp1 = temp0 * temp1;
        temp16 = temp1 * uVector[14];
        temp15 *= uVector[1];
        temp25 *= uVector[2];
        temp15 += temp25;
        temp25 = temp6 * uVector[3];
        temp15 += temp25;
        temp25 = temp35 * uVector[4];
        temp15 += temp25;
        temp3 *= uVector[5];
        temp3 = temp15 + temp3;
        temp15 = temp29 * uVector[6];
        temp3 += temp15;
        temp15 = temp7 * uVector[7];
        temp3 += temp15;
        temp15 = temp39 * uVector[8];
        temp3 += temp15;
        temp15 = temp47 * uVector[9];
        temp3 += temp15;
        temp12 *= uVector[10];
        temp3 += temp12;
        temp12 = Math.Sin(temp27);
        temp12 = temp0 * temp12;
        temp12 *= uVector[11];
        temp3 += temp12;
        temp12 = temp55 * uVector[12];
        temp3 += temp12;
        temp12 = Math.Sin(temp28);
        temp12 = temp0 * temp12;
        temp12 *= uVector[13];
        temp3 += temp12;
        temp3 = temp16 + temp3;
        temp12 = temp14 * uVector[15];
        temp3 += temp12;
        temp12 = Math.Sin(temp24);
        temp12 = temp0 * temp12;
        temp15 = temp12 * uVector[16];
        temp3 += temp15;
        temp15 = Math.Sin(temp57);
        temp15 = temp0 * temp15;
        temp15 *= uVector[17];
        temp3 += temp15;
        temp15 = Math.Sin(temp58);
        temp15 = temp0 * temp15;
        temp16 = temp15 * uVector[18];
        vVector[11] = temp3 + temp16;
            
        temp3 = 196 * Math.PI;
        temp3 = 0.05263157894736842 * temp3;
        temp16 = Math.Cos(temp3);
        temp16 = temp0 * temp16;
        temp16 *= uVector[14];
        temp18 *= uVector[1];
        temp18 = temp4 + temp18;
        temp24 = temp30 * uVector[2];
        temp18 += temp24;
        temp24 = temp43 * uVector[3];
        temp18 += temp24;
        temp24 = temp40 * uVector[4];
        temp18 += temp24;
        temp24 = temp33 * uVector[5];
        temp18 += temp24;
        temp24 = temp38 * uVector[6];
        temp18 += temp24;
        temp24 = 98 * Math.PI;
        temp24 = 0.05263157894736842 * temp24;
        temp25 = Math.Cos(temp24);
        temp25 = temp0 * temp25;
        temp25 *= uVector[7];
        temp18 += temp25;
        temp25 = temp48 * uVector[8];
        temp18 += temp25;
        temp25 = 126 * Math.PI;
        temp25 = 0.05263157894736842 * temp25;
        temp27 = Math.Cos(temp25);
        temp27 = temp0 * temp27;
        temp28 = temp27 * uVector[9];
        temp18 += temp28;
        temp28 = temp56 * uVector[10];
        temp18 += temp28;
        temp28 = 154 * Math.PI;
        temp28 = 0.05263157894736842 * temp28;
        temp30 = Math.Cos(temp28);
        temp30 = temp0 * temp30;
        temp30 *= uVector[11];
        temp18 += temp30;
        temp23 *= uVector[12];
        temp18 += temp23;
        temp23 = 182 * Math.PI;
        temp23 = 0.05263157894736842 * temp23;
        temp30 = Math.Cos(temp23);
        temp30 = temp0 * temp30;
        temp30 *= uVector[13];
        temp18 += temp30;
        temp16 += temp18;
        temp18 = 210 * Math.PI;
        temp18 = 0.05263157894736842 * temp18;
        temp30 = Math.Cos(temp18);
        temp30 = temp0 * temp30;
        temp30 *= uVector[15];
        temp16 += temp30;
        temp30 = 224 * Math.PI;
        temp30 = 0.05263157894736842 * temp30;
        temp33 = Math.Cos(temp30);
        temp33 = temp0 * temp33;
        temp38 = temp33 * uVector[16];
        temp16 += temp38;
        temp38 = 238 * Math.PI;
        temp38 = 0.05263157894736842 * temp38;
        temp40 = Math.Cos(temp38);
        temp40 = temp0 * temp40;
        temp40 *= uVector[17];
        temp16 += temp40;
        temp40 = 252 * Math.PI;
        temp40 = 0.05263157894736842 * temp40;
        temp43 = Math.Cos(temp40);
        temp43 = temp0 * temp43;
        temp56 = temp43 * uVector[18];
        vVector[12] = temp16 + temp56;
            
        temp3 = Math.Sin(temp3);
        temp3 = temp0 * temp3;
        temp3 *= uVector[14];
        temp16 = temp17 * uVector[1];
        temp17 = temp26 * uVector[2];
        temp16 += temp17;
        temp17 = temp42 * uVector[3];
        temp16 += temp17;
        temp17 = temp36 * uVector[4];
        temp16 += temp17;
        temp13 *= uVector[5];
        temp13 = temp16 + temp13;
        temp7 *= uVector[6];
        temp7 = temp13 + temp7;
        temp13 = Math.Sin(temp24);
        temp13 = temp0 * temp13;
        temp13 *= uVector[7];
        temp7 += temp13;
        temp13 = temp9 * uVector[8];
        temp7 += temp13;
        temp13 = Math.Sin(temp25);
        temp13 = temp0 * temp13;
        temp16 = temp13 * uVector[9];
        temp7 += temp16;
        temp2 *= uVector[10];
        temp2 = temp7 + temp2;
        temp7 = Math.Sin(temp28);
        temp7 = temp0 * temp7;
        temp7 *= uVector[11];
        temp2 += temp7;
        temp1 *= uVector[12];
        temp1 = temp2 + temp1;
        temp2 = Math.Sin(temp23);
        temp2 = temp0 * temp2;
        temp2 *= uVector[13];
        temp1 += temp2;
        temp1 = temp3 + temp1;
        temp2 = Math.Sin(temp18);
        temp2 = temp0 * temp2;
        temp2 *= uVector[15];
        temp1 += temp2;
        temp2 = Math.Sin(temp30);
        temp2 = temp0 * temp2;
        temp3 = temp2 * uVector[16];
        temp1 += temp3;
        temp3 = Math.Sin(temp38);
        temp3 = temp0 * temp3;
        temp3 *= uVector[17];
        temp1 += temp3;
        temp3 = Math.Sin(temp40);
        temp3 = temp0 * temp3;
        temp7 = temp3 * uVector[18];
        vVector[13] = temp1 + temp7;
            
        temp1 = temp33 * uVector[14];
        temp7 = temp20 * uVector[1];
        temp7 = temp4 + temp7;
        temp16 = temp31 * uVector[2];
        temp7 += temp16;
        temp16 = temp37 * uVector[3];
        temp7 += temp16;
        temp16 = temp41 * uVector[4];
        temp7 += temp16;
        temp16 = temp53 * uVector[5];
        temp7 += temp16;
        temp16 = temp51 * uVector[6];
        temp7 += temp16;
        temp16 = temp48 * uVector[7];
        temp7 += temp16;
        temp16 = temp59 * uVector[8];
        temp7 += temp16;
        temp16 = temp62 * uVector[9];
        temp7 += temp16;
        temp16 = temp65 * uVector[10];
        temp7 += temp16;
        temp16 = 176 * Math.PI;
        temp16 = 0.05263157894736842 * temp16;
        temp17 = Math.Cos(temp16);
        temp17 = temp0 * temp17;
        temp17 *= uVector[11];
        temp7 += temp17;
        temp17 = temp32 * uVector[12];
        temp7 += temp17;
        temp17 = 208 * Math.PI;
        temp17 = 0.05263157894736842 * temp17;
        temp18 = Math.Cos(temp17);
        temp18 = temp0 * temp18;
        temp18 *= uVector[13];
        temp7 += temp18;
        temp1 += temp7;
        temp7 = 240 * Math.PI;
        temp7 = 0.05263157894736842 * temp7;
        temp18 = Math.Cos(temp7);
        temp18 = temp0 * temp18;
        temp18 *= uVector[15];
        temp1 += temp18;
        temp18 = 256 * Math.PI;
        temp18 = 0.05263157894736842 * temp18;
        temp20 = Math.Cos(temp18);
        temp20 = temp0 * temp20;
        temp20 *= uVector[16];
        temp1 += temp20;
        temp20 = 272 * Math.PI;
        temp20 = 0.05263157894736842 * temp20;
        temp23 = Math.Cos(temp20);
        temp23 = temp0 * temp23;
        temp23 *= uVector[17];
        temp1 += temp23;
        temp23 = 288 * Math.PI;
        temp23 = 0.05263157894736842 * temp23;
        temp24 = Math.Cos(temp23);
        temp24 = temp0 * temp24;
        temp25 = temp24 * uVector[18];
        vVector[14] = temp1 + temp25;
            
        temp1 = temp2 * uVector[14];
        temp2 = temp19 * uVector[1];
        temp5 *= uVector[2];
        temp2 += temp5;
        temp5 = temp35 * uVector[3];
        temp2 += temp5;
        temp5 = temp8 * uVector[4];
        temp2 += temp5;
        temp5 = temp52 * uVector[5];
        temp2 += temp5;
        temp5 = temp39 * uVector[6];
        temp2 += temp5;
        temp5 = temp9 * uVector[7];
        temp2 += temp5;
        temp5 = temp50 * uVector[8];
        temp2 += temp5;
        temp5 = temp55 * uVector[9];
        temp2 += temp5;
        temp5 = temp11 * uVector[10];
        temp2 += temp5;
        temp5 = Math.Sin(temp16);
        temp5 = temp0 * temp5;
        temp5 *= uVector[11];
        temp2 += temp5;
        temp5 = temp12 * uVector[12];
        temp2 += temp5;
        temp5 = Math.Sin(temp17);
        temp5 = temp0 * temp5;
        temp5 *= uVector[13];
        temp2 += temp5;
        temp1 += temp2;
        temp2 = Math.Sin(temp7);
        temp2 = temp0 * temp2;
        temp2 *= uVector[15];
        temp1 += temp2;
        temp2 = Math.Sin(temp18);
        temp2 = temp0 * temp2;
        temp2 *= uVector[16];
        temp1 += temp2;
        temp2 = Math.Sin(temp20);
        temp2 = temp0 * temp2;
        temp2 *= uVector[17];
        temp1 += temp2;
        temp2 = Math.Sin(temp23);
        temp2 = temp0 * temp2;
        temp5 = temp2 * uVector[18];
        vVector[15] = temp1 + temp5;
            
        temp1 = temp43 * uVector[14];
        temp5 = temp22 * uVector[1];
        temp4 += temp5;
        temp5 = temp34 * uVector[2];
        temp4 += temp5;
        temp5 = temp46 * uVector[3];
        temp4 += temp5;
        temp5 = temp44 * uVector[4];
        temp4 += temp5;
        temp5 = temp49 * uVector[5];
        temp4 += temp5;
        temp5 = temp54 * uVector[6];
        temp4 += temp5;
        temp5 = temp27 * uVector[7];
        temp4 += temp5;
        temp5 = temp62 * uVector[8];
        temp4 += temp5;
        temp5 = 162 * Math.PI;
        temp5 = 0.05263157894736842 * temp5;
        temp7 = Math.Cos(temp5);
        temp7 = temp0 * temp7;
        temp7 *= uVector[9];
        temp4 += temp7;
        temp7 = temp68 * uVector[10];
        temp4 += temp7;
        temp7 = 198 * Math.PI;
        temp7 = 0.05263157894736842 * temp7;
        temp8 = Math.Cos(temp7);
        temp8 = temp0 * temp8;
        temp8 *= uVector[11];
        temp4 += temp8;
        temp8 = temp60 * uVector[12];
        temp4 += temp8;
        temp8 = 234 * Math.PI;
        temp8 = 0.05263157894736842 * temp8;
        temp9 = Math.Cos(temp8);
        temp9 = temp0 * temp9;
        temp9 *= uVector[13];
        temp4 += temp9;
        temp1 += temp4;
        temp4 = 270 * Math.PI;
        temp4 = 0.05263157894736842 * temp4;
        temp9 = Math.Cos(temp4);
        temp9 = temp0 * temp9;
        temp9 *= uVector[15];
        temp1 += temp9;
        temp9 = temp24 * uVector[16];
        temp1 += temp9;
        temp9 = 306 * Math.PI;
        temp9 = 0.05263157894736842 * temp9;
        temp11 = Math.Cos(temp9);
        temp11 = temp0 * temp11;
        temp11 *= uVector[17];
        temp1 += temp11;
        temp11 = 324 * Math.PI;
        temp11 = 0.05263157894736842 * temp11;
        temp12 = Math.Cos(temp11);
        temp12 = temp0 * temp12;
        temp12 *= uVector[18];
        vVector[16] = temp1 + temp12;
            
        temp1 = temp3 * uVector[14];
        temp3 = temp21 * uVector[1];
        temp6 *= uVector[2];
        temp3 += temp6;
        temp6 = temp45 * uVector[3];
        temp3 += temp6;
        temp6 = temp29 * uVector[4];
        temp3 += temp6;
        temp6 = temp10 * uVector[5];
        temp3 += temp6;
        temp6 = temp47 * uVector[6];
        temp3 += temp6;
        temp6 = temp13 * uVector[7];
        temp3 += temp6;
        temp6 = temp55 * uVector[8];
        temp3 += temp6;
        temp5 = Math.Sin(temp5);
        temp5 = temp0 * temp5;
        temp5 *= uVector[9];
        temp3 += temp5;
        temp5 = temp14 * uVector[10];
        temp3 += temp5;
        temp5 = Math.Sin(temp7);
        temp5 = temp0 * temp5;
        temp5 *= uVector[11];
        temp3 += temp5;
        temp5 = temp15 * uVector[12];
        temp3 += temp5;
        temp5 = Math.Sin(temp8);
        temp5 = temp0 * temp5;
        temp5 *= uVector[13];
        temp3 += temp5;
        temp1 += temp3;
        temp3 = Math.Sin(temp4);
        temp3 = temp0 * temp3;
        temp3 *= uVector[15];
        temp1 += temp3;
        temp2 *= uVector[16];
        temp1 += temp2;
        temp2 = Math.Sin(temp9);
        temp2 = temp0 * temp2;
        temp2 *= uVector[17];
        temp1 += temp2;
        temp2 = Math.Sin(temp11);
        temp0 *= temp2;
        temp0 *= uVector[18];
        vVector[17] = temp1 + temp0;
            
        temp0 = Math.Sqrt(19);
        temp0 = 1 / temp0;
        temp1 = temp0 * uVector[14];
        temp2 = temp0 * uVector[0];
        temp3 = temp0 * uVector[1];
        temp2 += temp3;
        temp3 = temp0 * uVector[2];
        temp2 += temp3;
        temp3 = temp0 * uVector[3];
        temp2 += temp3;
        temp3 = temp0 * uVector[4];
        temp2 += temp3;
        temp3 = temp0 * uVector[5];
        temp2 += temp3;
        temp3 = temp0 * uVector[6];
        temp2 += temp3;
        temp3 = temp0 * uVector[7];
        temp2 += temp3;
        temp3 = temp0 * uVector[8];
        temp2 += temp3;
        temp3 = temp0 * uVector[9];
        temp2 += temp3;
        temp3 = temp0 * uVector[10];
        temp2 += temp3;
        temp3 = temp0 * uVector[11];
        temp2 += temp3;
        temp3 = temp0 * uVector[12];
        temp2 += temp3;
        temp3 = temp0 * uVector[13];
        temp2 += temp3;
        temp1 += temp2;
        temp2 = temp0 * uVector[15];
        temp1 += temp2;
        temp2 = temp0 * uVector[16];
        temp1 += temp2;
        temp2 = temp0 * uVector[17];
        temp1 += temp2;
        temp0 *= uVector[18];
        vVector[18] = temp1 + temp0;
            
        //Finish GA-FuL MetaContext Code Generation, 2022-11-01T17:20:52.6397775+02:00
            

        return vVector;
    }
}