using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga6;

public sealed partial class Ga6Multivector
{
    public static Ga6Multivector Zero { get; } = Create(new double[64]);
    
    public static Ga6Multivector E => Create(Ga6KVector0.E);
    
    public static Ga6Multivector E1 => Create(Ga6KVector1.E1);
    
    public static Ga6Multivector E2 => Create(Ga6KVector1.E2);
    
    public static Ga6Multivector E3 => Create(Ga6KVector1.E3);
    
    public static Ga6Multivector E4 => Create(Ga6KVector1.E4);
    
    public static Ga6Multivector E5 => Create(Ga6KVector1.E5);
    
    public static Ga6Multivector E6 => Create(Ga6KVector1.E6);
    
    public static Ga6Multivector E12 => Create(Ga6KVector2.E12);
    
    public static Ga6Multivector E13 => Create(Ga6KVector2.E13);
    
    public static Ga6Multivector E23 => Create(Ga6KVector2.E23);
    
    public static Ga6Multivector E14 => Create(Ga6KVector2.E14);
    
    public static Ga6Multivector E24 => Create(Ga6KVector2.E24);
    
    public static Ga6Multivector E34 => Create(Ga6KVector2.E34);
    
    public static Ga6Multivector E15 => Create(Ga6KVector2.E15);
    
    public static Ga6Multivector E25 => Create(Ga6KVector2.E25);
    
    public static Ga6Multivector E35 => Create(Ga6KVector2.E35);
    
    public static Ga6Multivector E45 => Create(Ga6KVector2.E45);
    
    public static Ga6Multivector E16 => Create(Ga6KVector2.E16);
    
    public static Ga6Multivector E26 => Create(Ga6KVector2.E26);
    
    public static Ga6Multivector E36 => Create(Ga6KVector2.E36);
    
    public static Ga6Multivector E46 => Create(Ga6KVector2.E46);
    
    public static Ga6Multivector E56 => Create(Ga6KVector2.E56);
    
    public static Ga6Multivector E123 => Create(Ga6KVector3.E123);
    
    public static Ga6Multivector E124 => Create(Ga6KVector3.E124);
    
    public static Ga6Multivector E134 => Create(Ga6KVector3.E134);
    
    public static Ga6Multivector E234 => Create(Ga6KVector3.E234);
    
    public static Ga6Multivector E125 => Create(Ga6KVector3.E125);
    
    public static Ga6Multivector E135 => Create(Ga6KVector3.E135);
    
    public static Ga6Multivector E235 => Create(Ga6KVector3.E235);
    
    public static Ga6Multivector E145 => Create(Ga6KVector3.E145);
    
    public static Ga6Multivector E245 => Create(Ga6KVector3.E245);
    
    public static Ga6Multivector E345 => Create(Ga6KVector3.E345);
    
    public static Ga6Multivector E126 => Create(Ga6KVector3.E126);
    
    public static Ga6Multivector E136 => Create(Ga6KVector3.E136);
    
    public static Ga6Multivector E236 => Create(Ga6KVector3.E236);
    
    public static Ga6Multivector E146 => Create(Ga6KVector3.E146);
    
    public static Ga6Multivector E246 => Create(Ga6KVector3.E246);
    
    public static Ga6Multivector E346 => Create(Ga6KVector3.E346);
    
    public static Ga6Multivector E156 => Create(Ga6KVector3.E156);
    
    public static Ga6Multivector E256 => Create(Ga6KVector3.E256);
    
    public static Ga6Multivector E356 => Create(Ga6KVector3.E356);
    
    public static Ga6Multivector E456 => Create(Ga6KVector3.E456);
    
    public static Ga6Multivector E1234 => Create(Ga6KVector4.E1234);
    
    public static Ga6Multivector E1235 => Create(Ga6KVector4.E1235);
    
    public static Ga6Multivector E1245 => Create(Ga6KVector4.E1245);
    
    public static Ga6Multivector E1345 => Create(Ga6KVector4.E1345);
    
    public static Ga6Multivector E2345 => Create(Ga6KVector4.E2345);
    
    public static Ga6Multivector E1236 => Create(Ga6KVector4.E1236);
    
    public static Ga6Multivector E1246 => Create(Ga6KVector4.E1246);
    
    public static Ga6Multivector E1346 => Create(Ga6KVector4.E1346);
    
    public static Ga6Multivector E2346 => Create(Ga6KVector4.E2346);
    
    public static Ga6Multivector E1256 => Create(Ga6KVector4.E1256);
    
    public static Ga6Multivector E1356 => Create(Ga6KVector4.E1356);
    
    public static Ga6Multivector E2356 => Create(Ga6KVector4.E2356);
    
    public static Ga6Multivector E1456 => Create(Ga6KVector4.E1456);
    
    public static Ga6Multivector E2456 => Create(Ga6KVector4.E2456);
    
    public static Ga6Multivector E3456 => Create(Ga6KVector4.E3456);
    
    public static Ga6Multivector E12345 => Create(Ga6KVector5.E12345);
    
    public static Ga6Multivector E12346 => Create(Ga6KVector5.E12346);
    
    public static Ga6Multivector E12356 => Create(Ga6KVector5.E12356);
    
    public static Ga6Multivector E12456 => Create(Ga6KVector5.E12456);
    
    public static Ga6Multivector E13456 => Create(Ga6KVector5.E13456);
    
    public static Ga6Multivector E23456 => Create(Ga6KVector5.E23456);
    
    public static Ga6Multivector E123456 => Create(Ga6KVector6.E123456);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector0 kVector)
    {
        return new Ga6Multivector(
            kVector,
            Ga6KVector1.Zero,
            Ga6KVector2.Zero,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector0 kVector, double scalar)
    {
        return new Ga6Multivector(
            new Ga6KVector0 { Scalar = kVector.Scalar + scalar },
            Ga6KVector1.Zero,
            Ga6KVector2.Zero,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector1 kVector)
    {
        return new Ga6Multivector(
            Ga6KVector0.Zero,
            kVector,
            Ga6KVector2.Zero,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector1 kVector, double scalar)
    {
        return new Ga6Multivector(
            new Ga6KVector0 { Scalar = scalar },
            kVector,
            Ga6KVector2.Zero,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector2 kVector)
    {
        return new Ga6Multivector(
            Ga6KVector0.Zero,
            Ga6KVector1.Zero,
            kVector,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector2 kVector, double scalar)
    {
        return new Ga6Multivector(
            new Ga6KVector0 { Scalar = scalar },
            Ga6KVector1.Zero,
            kVector,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector3 kVector)
    {
        return new Ga6Multivector(
            Ga6KVector0.Zero,
            Ga6KVector1.Zero,
            Ga6KVector2.Zero,
            kVector,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector3 kVector, double scalar)
    {
        return new Ga6Multivector(
            new Ga6KVector0 { Scalar = scalar },
            Ga6KVector1.Zero,
            Ga6KVector2.Zero,
            kVector,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector4 kVector)
    {
        return new Ga6Multivector(
            Ga6KVector0.Zero,
            Ga6KVector1.Zero,
            Ga6KVector2.Zero,
            Ga6KVector3.Zero,
            kVector,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector4 kVector, double scalar)
    {
        return new Ga6Multivector(
            new Ga6KVector0 { Scalar = scalar },
            Ga6KVector1.Zero,
            Ga6KVector2.Zero,
            Ga6KVector3.Zero,
            kVector,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector5 kVector)
    {
        return new Ga6Multivector(
            Ga6KVector0.Zero,
            Ga6KVector1.Zero,
            Ga6KVector2.Zero,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            kVector,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector5 kVector, double scalar)
    {
        return new Ga6Multivector(
            new Ga6KVector0 { Scalar = scalar },
            Ga6KVector1.Zero,
            Ga6KVector2.Zero,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            kVector,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector6 kVector)
    {
        return new Ga6Multivector(
            Ga6KVector0.Zero,
            Ga6KVector1.Zero,
            Ga6KVector2.Zero,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            kVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector6 kVector, double scalar)
    {
        return new Ga6Multivector(
            new Ga6KVector0 { Scalar = scalar },
            Ga6KVector1.Zero,
            Ga6KVector2.Zero,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            kVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6Multivector mv, double scalar)
    {
        return Create(
            new Ga6KVector0 { Scalar = mv.KVector0.Scalar + scalar },
            mv.KVector1,
            mv.KVector2,
            mv.KVector3,
            mv.KVector4,
            mv.KVector5,
            mv.KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector Create(Ga6KVector0 kVector0, Ga6KVector1 kVector1, Ga6KVector2 kVector2, Ga6KVector3 kVector3, Ga6KVector4 kVector4, Ga6KVector5 kVector5, Ga6KVector6 kVector6)
    {
        return new Ga6Multivector(
            kVector0,
            kVector1,
            kVector2,
            kVector3,
            kVector4,
            kVector5,
            kVector6
        );
    }
    
    public static Ga6Multivector Create(params double[] scalarArray)
    {
        var kVector0 = new Ga6KVector0()
        {
            Scalar = scalarArray[0]
        };
        
        var kVector1 = new Ga6KVector1()
        {
            Scalar1 = scalarArray[1],
            Scalar2 = scalarArray[2],
            Scalar3 = scalarArray[4],
            Scalar4 = scalarArray[8],
            Scalar5 = scalarArray[16],
            Scalar6 = scalarArray[32]
        };
        
        var kVector2 = new Ga6KVector2()
        {
            Scalar12 = scalarArray[3],
            Scalar13 = scalarArray[5],
            Scalar23 = scalarArray[6],
            Scalar14 = scalarArray[9],
            Scalar24 = scalarArray[10],
            Scalar34 = scalarArray[12],
            Scalar15 = scalarArray[17],
            Scalar25 = scalarArray[18],
            Scalar35 = scalarArray[20],
            Scalar45 = scalarArray[24],
            Scalar16 = scalarArray[33],
            Scalar26 = scalarArray[34],
            Scalar36 = scalarArray[36],
            Scalar46 = scalarArray[40],
            Scalar56 = scalarArray[48]
        };
        
        var kVector3 = new Ga6KVector3()
        {
            Scalar123 = scalarArray[7],
            Scalar124 = scalarArray[11],
            Scalar134 = scalarArray[13],
            Scalar234 = scalarArray[14],
            Scalar125 = scalarArray[19],
            Scalar135 = scalarArray[21],
            Scalar235 = scalarArray[22],
            Scalar145 = scalarArray[25],
            Scalar245 = scalarArray[26],
            Scalar345 = scalarArray[28],
            Scalar126 = scalarArray[35],
            Scalar136 = scalarArray[37],
            Scalar236 = scalarArray[38],
            Scalar146 = scalarArray[41],
            Scalar246 = scalarArray[42],
            Scalar346 = scalarArray[44],
            Scalar156 = scalarArray[49],
            Scalar256 = scalarArray[50],
            Scalar356 = scalarArray[52],
            Scalar456 = scalarArray[56]
        };
        
        var kVector4 = new Ga6KVector4()
        {
            Scalar1234 = scalarArray[15],
            Scalar1235 = scalarArray[23],
            Scalar1245 = scalarArray[27],
            Scalar1345 = scalarArray[29],
            Scalar2345 = scalarArray[30],
            Scalar1236 = scalarArray[39],
            Scalar1246 = scalarArray[43],
            Scalar1346 = scalarArray[45],
            Scalar2346 = scalarArray[46],
            Scalar1256 = scalarArray[51],
            Scalar1356 = scalarArray[53],
            Scalar2356 = scalarArray[54],
            Scalar1456 = scalarArray[57],
            Scalar2456 = scalarArray[58],
            Scalar3456 = scalarArray[60]
        };
        
        var kVector5 = new Ga6KVector5()
        {
            Scalar12345 = scalarArray[31],
            Scalar12346 = scalarArray[47],
            Scalar12356 = scalarArray[55],
            Scalar12456 = scalarArray[59],
            Scalar13456 = scalarArray[61],
            Scalar23456 = scalarArray[62]
        };
        
        var kVector6 = new Ga6KVector6()
        {
            Scalar123456 = scalarArray[63]
        };
        
        return new Ga6Multivector(
            kVector0,
            kVector1,
            kVector2,
            kVector3,
            kVector4,
            kVector5,
            kVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6Multivector mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6Multivector mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator *(Ga6Multivector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator *(double mv1, Ga6Multivector mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator /(Ga6Multivector mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6Multivector mv1, Ga6KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6Multivector mv1, Ga6KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6Multivector mv1, Ga6KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6Multivector mv1, Ga6KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6Multivector mv1, Ga6KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6Multivector mv1, Ga6KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6Multivector mv1, Ga6KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6Multivector mv1, Ga6KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6Multivector mv1, Ga6KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6Multivector mv1, Ga6KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6Multivector mv1, Ga6KVector5 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6Multivector mv1, Ga6KVector5 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6Multivector mv1, Ga6Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6Multivector mv1, Ga6Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6Multivector mv1, double mv2)
    {
        return Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(double mv1, Ga6Multivector mv2)
    {
        return Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6Multivector mv1, double mv2)
    {
        return Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(double mv1, Ga6Multivector mv2)
    {
        return Create(mv2.Negative(), mv1);
    }
    
    public Ga6KVector0 KVector0 { get; }
    
    public Ga6KVector1 KVector1 { get; }
    
    public Ga6KVector2 KVector2 { get; }
    
    public Ga6KVector3 KVector3 { get; }
    
    public Ga6KVector4 KVector4 { get; }
    
    public Ga6KVector5 KVector5 { get; }
    
    public Ga6KVector6 KVector6 { get; }
    
    public double Scalar => KVector0.Scalar;
    
    public double Scalar1 => KVector1.Scalar1;
    
    public double Scalar2 => KVector1.Scalar2;
    
    public double Scalar3 => KVector1.Scalar3;
    
    public double Scalar4 => KVector1.Scalar4;
    
    public double Scalar5 => KVector1.Scalar5;
    
    public double Scalar6 => KVector1.Scalar6;
    
    public double Scalar12 => KVector2.Scalar12;
    
    public double Scalar13 => KVector2.Scalar13;
    
    public double Scalar23 => KVector2.Scalar23;
    
    public double Scalar14 => KVector2.Scalar14;
    
    public double Scalar24 => KVector2.Scalar24;
    
    public double Scalar34 => KVector2.Scalar34;
    
    public double Scalar15 => KVector2.Scalar15;
    
    public double Scalar25 => KVector2.Scalar25;
    
    public double Scalar35 => KVector2.Scalar35;
    
    public double Scalar45 => KVector2.Scalar45;
    
    public double Scalar16 => KVector2.Scalar16;
    
    public double Scalar26 => KVector2.Scalar26;
    
    public double Scalar36 => KVector2.Scalar36;
    
    public double Scalar46 => KVector2.Scalar46;
    
    public double Scalar56 => KVector2.Scalar56;
    
    public double Scalar123 => KVector3.Scalar123;
    
    public double Scalar124 => KVector3.Scalar124;
    
    public double Scalar134 => KVector3.Scalar134;
    
    public double Scalar234 => KVector3.Scalar234;
    
    public double Scalar125 => KVector3.Scalar125;
    
    public double Scalar135 => KVector3.Scalar135;
    
    public double Scalar235 => KVector3.Scalar235;
    
    public double Scalar145 => KVector3.Scalar145;
    
    public double Scalar245 => KVector3.Scalar245;
    
    public double Scalar345 => KVector3.Scalar345;
    
    public double Scalar126 => KVector3.Scalar126;
    
    public double Scalar136 => KVector3.Scalar136;
    
    public double Scalar236 => KVector3.Scalar236;
    
    public double Scalar146 => KVector3.Scalar146;
    
    public double Scalar246 => KVector3.Scalar246;
    
    public double Scalar346 => KVector3.Scalar346;
    
    public double Scalar156 => KVector3.Scalar156;
    
    public double Scalar256 => KVector3.Scalar256;
    
    public double Scalar356 => KVector3.Scalar356;
    
    public double Scalar456 => KVector3.Scalar456;
    
    public double Scalar1234 => KVector4.Scalar1234;
    
    public double Scalar1235 => KVector4.Scalar1235;
    
    public double Scalar1245 => KVector4.Scalar1245;
    
    public double Scalar1345 => KVector4.Scalar1345;
    
    public double Scalar2345 => KVector4.Scalar2345;
    
    public double Scalar1236 => KVector4.Scalar1236;
    
    public double Scalar1246 => KVector4.Scalar1246;
    
    public double Scalar1346 => KVector4.Scalar1346;
    
    public double Scalar2346 => KVector4.Scalar2346;
    
    public double Scalar1256 => KVector4.Scalar1256;
    
    public double Scalar1356 => KVector4.Scalar1356;
    
    public double Scalar2356 => KVector4.Scalar2356;
    
    public double Scalar1456 => KVector4.Scalar1456;
    
    public double Scalar2456 => KVector4.Scalar2456;
    
    public double Scalar3456 => KVector4.Scalar3456;
    
    public double Scalar12345 => KVector5.Scalar12345;
    
    public double Scalar12346 => KVector5.Scalar12346;
    
    public double Scalar12356 => KVector5.Scalar12356;
    
    public double Scalar12456 => KVector5.Scalar12456;
    
    public double Scalar13456 => KVector5.Scalar13456;
    
    public double Scalar23456 => KVector5.Scalar23456;
    
    public double Scalar123456 => KVector6.Scalar123456;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Ga6Multivector(Ga6KVector0 kVector0, Ga6KVector1 kVector1, Ga6KVector2 kVector2, Ga6KVector3 kVector3, Ga6KVector4 kVector4, Ga6KVector5 kVector5, Ga6KVector6 kVector6)
    {
        KVector0 = kVector0;
        KVector1 = kVector1;
        KVector2 = kVector2;
        KVector3 = kVector3;
        KVector4 = kVector4;
        KVector5 = kVector5;
        KVector6 = kVector6;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            KVector0.IsValid() &&
            KVector1.IsValid() &&
            KVector2.IsValid() &&
            KVector3.IsValid() &&
            KVector4.IsValid() &&
            KVector5.IsValid() &&
            KVector6.IsValid();
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            KVector0.IsZero() &&
            KVector1.IsZero() &&
            KVector2.IsZero() &&
            KVector3.IsZero() &&
            KVector4.IsZero() &&
            KVector5.IsZero() &&
            KVector6.IsZero();
    
        return _isZero.Value;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double zeroEpsilon = 1e-12d)
    {
        var norm = this.Norm();
    
        return norm > -zeroEpsilon && norm < zeroEpsilon;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetMultivectorArray()
    {
        return new double[]
        {
            Scalar,
            Scalar1,
            Scalar2,
            Scalar12,
            Scalar3,
            Scalar13,
            Scalar23,
            Scalar123,
            Scalar4,
            Scalar14,
            Scalar24,
            Scalar124,
            Scalar34,
            Scalar134,
            Scalar234,
            Scalar1234,
            Scalar5,
            Scalar15,
            Scalar25,
            Scalar125,
            Scalar35,
            Scalar135,
            Scalar235,
            Scalar1235,
            Scalar45,
            Scalar145,
            Scalar245,
            Scalar1245,
            Scalar345,
            Scalar1345,
            Scalar2345,
            Scalar12345,
            Scalar6,
            Scalar16,
            Scalar26,
            Scalar126,
            Scalar36,
            Scalar136,
            Scalar236,
            Scalar1236,
            Scalar46,
            Scalar146,
            Scalar246,
            Scalar1246,
            Scalar346,
            Scalar1346,
            Scalar2346,
            Scalar12346,
            Scalar56,
            Scalar156,
            Scalar256,
            Scalar1256,
            Scalar356,
            Scalar1356,
            Scalar2356,
            Scalar12356,
            Scalar456,
            Scalar1456,
            Scalar2456,
            Scalar12456,
            Scalar3456,
            Scalar13456,
            Scalar23456,
            Scalar123456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][] GetKVectorArrays()
    {
        return new double[][]
        {
            KVector0.GetKVectorArray(),
            KVector1.GetKVectorArray(),
            KVector2.GetKVectorArray(),
            KVector3.GetKVectorArray(),
            KVector4.GetKVectorArray(),
            KVector5.GetKVectorArray(),
            KVector6.GetKVectorArray()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Times(double mv2)
    {
        return new Ga6Multivector(
            KVector0 * mv2,
            KVector1 * mv2,
            KVector2 * mv2,
            KVector3 * mv2,
            KVector4 * mv2,
            KVector5 * mv2,
            KVector6 * mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Negative()
    {
        return new Ga6Multivector(
            KVector0.Negative(),
            KVector1.Negative(),
            KVector2.Negative(),
            KVector3.Negative(),
            KVector4.Negative(),
            KVector5.Negative(),
            KVector6.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Reverse()
    {
        return new Ga6Multivector(
            KVector0,
            KVector1,
            KVector2.Negative(),
            KVector3.Negative(),
            KVector4,
            KVector5,
            KVector6.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector GradeInvolution()
    {
        return new Ga6Multivector(
            KVector0,
            KVector1.Negative(),
            KVector2,
            KVector3.Negative(),
            KVector4,
            KVector5.Negative(),
            KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector CliffordConjugate()
    {
        return new Ga6Multivector(
            KVector0,
            KVector1.Negative(),
            KVector2.Negative(),
            KVector3,
            KVector4,
            KVector5.Negative(),
            KVector6.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Inverse()
    {
        var mvReverse = Reverse();
    
        return mvReverse.Times(
            1d / mvReverse.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector InverseTimes(double mv2)
    {
        var mvReverse = Reverse();
    
        return mvReverse.Times(
            mv2 / mvReverse.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Conjugate()
    {
        return new Ga6Multivector(
            KVector0.Conjugate(),
            KVector1.Conjugate(),
            KVector2.Conjugate(),
            KVector3.Conjugate(),
            KVector4.Conjugate(),
            KVector5.Conjugate(),
            KVector6.Conjugate()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector0 Dual(Ga6KVector0 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector0 UnDual(Ga6KVector0 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Dual(Ga6KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector UnDual(Ga6KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Dual(Ga6KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector UnDual(Ga6KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Dual(Ga6KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector UnDual(Ga6KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Dual(Ga6KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector UnDual(Ga6KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Dual(Ga6KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector UnDual(Ga6KVector5 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Dual(Ga6KVector6 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector UnDual(Ga6KVector6 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0.Add(mv2),
            KVector1,
            KVector2,
            KVector3,
            KVector4,
            KVector5,
            KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1.Add(mv2),
            KVector2,
            KVector3,
            KVector4,
            KVector5,
            KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2.Add(mv2),
            KVector3,
            KVector4,
            KVector5,
            KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3.Add(mv2),
            KVector4,
            KVector5,
            KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3,
            KVector4.Add(mv2),
            KVector5,
            KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6KVector5 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3,
            KVector4,
            KVector5.Add(mv2),
            KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6KVector6 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3,
            KVector4,
            KVector5,
            KVector6.Add(mv2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6Multivector mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return Create(
            KVector0.Add(mv2.KVector0),
            KVector1.Add(mv2.KVector1),
            KVector2.Add(mv2.KVector2),
            KVector3.Add(mv2.KVector3),
            KVector4.Add(mv2.KVector4),
            KVector5.Add(mv2.KVector5),
            KVector6.Add(mv2.KVector6)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0.Subtract(mv2),
            KVector1,
            KVector2,
            KVector3,
            KVector4,
            KVector5,
            KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1.Subtract(mv2),
            KVector2,
            KVector3,
            KVector4,
            KVector5,
            KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2.Subtract(mv2),
            KVector3,
            KVector4,
            KVector5,
            KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3.Subtract(mv2),
            KVector4,
            KVector5,
            KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3,
            KVector4.Subtract(mv2),
            KVector5,
            KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6KVector5 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3,
            KVector4,
            KVector5.Subtract(mv2),
            KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6KVector6 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3,
            KVector4,
            KVector5,
            KVector6.Subtract(mv2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6Multivector mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return Create(
            KVector0.Subtract(mv2.KVector0),
            KVector1.Subtract(mv2.KVector1),
            KVector2.Subtract(mv2.KVector2),
            KVector3.Subtract(mv2.KVector3),
            KVector4.Subtract(mv2.KVector4),
            KVector5.Subtract(mv2.KVector5),
            KVector6.Subtract(mv2.KVector6)
        );
    }
    
}
