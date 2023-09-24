using System;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Numeric;

public static class Pothenot_GA
{
    
    public static ScalarProcessorOfFloat64 ScalarProcessor { get; } = ScalarProcessorOfFloat64.DefaultProcessor;

    //public static int VSpaceDimensions => 2;

    public static RGaFloat64Processor GeometricProcessor { get; } = RGaFloat64Processor.Euclidean;

    public static TextComposerFloat64 TextComposer { get; } = TextComposerFloat64.DefaultComposer;


    public static void Benchmark()
    {
        // Create initial lookup tables for GA-FuL takes some time,
        // so we make one call to force building them before actual benchmark
        test_algoritm();

        var time1 = DateTime.Now;
        for (var i = 0; i < 1000000; i++)
            test_algoritm();
        var time2 = DateTime.Now;

        Console.WriteLine(time2 - time1);
    }

    public static void test_algoritm(){

        // Exmaple points locations to test
        float[] A = new float[2]{-4, 3};
        float[] B = new float[2]{0, 5};
        float[] C = new float[2]{7, 4};
        float[] P = new float[2]{1, 0};

        float[] result = new float[3];
            
        result = GetAngles(A, B, C, P);
        VGAMethod_SP(A, B, C, result[0], result[1], result[2]);

    }
    public static float[] VGAMethod_SP(float[] A, float[] B, float[] C, float alpha, float beta, float origin)
    {
        //// This is a pre-defined scalar processor for the standard
        //// 64-bit floating point scalars
        //var scalarProcessor = ScalarProcessorOfFloat64.DefaultProcessor;

        //// Create a 2-dimensional Euclidean geometric algebra processor based on the
        //// selected scalar processor
        //var geometricProcessor = RGaFloat64Processor.Euclidean;

        //// This is a pre-defined text generator for displaying multivectors
        //// with 64-bit floating point scalars
        //var textComposer = TextComposerFloat64.DefaultComposer;


        var e1 = GeometricProcessor.CreateTermVector(0);
        var e2 = GeometricProcessor.CreateTermVector(1);

        //select the central beacon depending on the origin
        float[] center = new float[2];
            
            
        var v1 = GeometricProcessor.CreateVector(A[0]-B[0], A[1]-B[1]);
        var v2 = GeometricProcessor.CreateVector(C[0]-B[0], C[1]-B[1]);
        center = B;

        if (origin == 1){
            v1 = GeometricProcessor.CreateVector(B[0]-A[0], B[1]-A[1]);
            v2 = GeometricProcessor.CreateVector(C[0]-A[0], C[1]-A[1]);
            center = A;

        }else if (origin == 3){
            v1 = GeometricProcessor.CreateVector(A[0]-C[0], A[1]-C[1]);
            v2 = GeometricProcessor.CreateVector(B[0]-C[0], B[1]-C[1]);
            center = C;

        }

        //Calculate the vector p
        var d1 = v1 + (v1/Math.Tan(alpha)).Gp(e1.Op(e2));
        var d2 = v2 - (v2/Math.Tan(beta)).Gp(e1.Op(e2));

        var d = d2 - d1;
        var inv = d1.Inverse();

        //If the inverse of d does not exist, we are in the forbidden circle.
        try
        {
            inv = d.Inverse();

                
        }catch (Exception ex)
        {
            Console.WriteLine("Forbidden circle.   ----->    Excepción: " + ex);
        }


        var p = (d1.Op(d)).Gp(d.Inverse());
        float[] global_pos = new float[2]{ center[0] + (float)p.Scalar(0), center[1] + (float)p.Scalar(1) };

        return global_pos;
        //Console.WriteLine();
        //Console.WriteLine(value: $"Vector p = {TextComposer.GetMultivectorText(p)}");
        //Console.WriteLine($"Global position of P = {(global_pos[0],global_pos[1])}");
    }

    static float[] GetAngles(float[] A, float[] B, float[] C, float[] P)
    {
        float theta_ua, theta_ub, theta_uc, alpha, beta, origin;
      
        float[] ua = new float[2]{A[0]-P[0], A[1]-P[1]};
        float[] ub = new float[2]{B[0]-P[0], B[1]-P[1]};
        float[] uc = new float[2]{C[0]-P[0], C[1]-P[1]};

        //Angles at which P sees the beacons
        theta_ua = (float)Math.Atan2(ua[1], ua[0]);
        theta_ub = (float)Math.Atan2(ub[1], ub[0]);
        theta_uc = (float)Math.Atan2(uc[1], uc[0]);

        alpha =  theta_ua-theta_ub;
        beta =  theta_ub-theta_uc;
        origin = 2; //2 = B

        // If beta or alpha are null we mest change the origin
        if(beta == 0){
            alpha =  theta_ub-theta_ua;
            beta =  theta_ua-theta_uc;
            origin = 1; //1=A
        }else if(alpha == 0){
            alpha =  theta_ua-theta_uc;
            beta =  theta_uc-theta_ub;
            origin = 3; //3=C
        }

        float[] result = new float[]{alpha, beta, origin};

        //Console.WriteLine();
        //Console.WriteLine($"Angles from P  -----> A:{theta_ua*180/Math.PI}, B:{theta_ub*180/Math.PI}, C:{theta_uc*180/Math.PI}");
        //Console.WriteLine($"alpha: {alpha*180/Math.PI}");
        //Console.WriteLine($"beta: {beta*180/Math.PI}");
        //Console.WriteLine($"origin: {origin}");
        //Console.WriteLine();

        return result;
    }
        
        
}