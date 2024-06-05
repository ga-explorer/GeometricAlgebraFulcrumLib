using System;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float32;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Numeric;

public static class PothenotGa
{
    
    public static ScalarProcessorOfFloat64 ScalarProcessor { get; } = ScalarProcessorOfFloat64.Instance;

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
        var a = new float[2]{-4, 3};
        var b = new float[2]{0, 5};
        var c = new float[2]{7, 4};
        var p = new float[2]{1, 0};

        var result = new float[3];
            
        result = GetAngles(a, b, c, p);
        VGAMethod_SP(a, b, c, result[0], result[1], result[2]);

    }
    public static float[] VGAMethod_SP(float[] a, float[] b, float[] c, float alpha, float beta, float origin)
    {
        //// This is a pre-defined scalar processor for the standard
        //// 64-bit floating point scalars
        //var scalarProcessor = ScalarProcessorOfFloat64.Instance;

        //// Create a 2-dimensional Euclidean geometric algebra processor based on the
        //// selected scalar processor
        //var geometricProcessor = RGaFloat64Processor.Euclidean;

        //// This is a pre-defined text generator for displaying multivectors
        //// with 64-bit floating point scalars
        //var textComposer = TextComposerFloat64.DefaultComposer;


        var e1 = GeometricProcessor.VectorTerm(0);
        var e2 = GeometricProcessor.VectorTerm(1);

        //select the central beacon depending on the origin
        var center = new float[2];
            
            
        var v1 = GeometricProcessor.Vector(a[0]-b[0], a[1]-b[1]);
        var v2 = GeometricProcessor.Vector(c[0]-b[0], c[1]-b[1]);
        center = b;

        if (origin == 1){
            v1 = GeometricProcessor.Vector(b[0]-a[0], b[1]-a[1]);
            v2 = GeometricProcessor.Vector(c[0]-a[0], c[1]-a[1]);
            center = a;

        }else if (origin == 3){
            v1 = GeometricProcessor.Vector(a[0]-c[0], a[1]-c[1]);
            v2 = GeometricProcessor.Vector(b[0]-c[0], b[1]-c[1]);
            center = c;

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
        var globalPos = new float[2]{ center[0] + (float)p.Scalar(0), center[1] + (float)p.Scalar(1) };

        return globalPos;
        //Console.WriteLine();
        //Console.WriteLine(value: $"Vector p = {TextComposer.GetMultivectorText(p)}");
        //Console.WriteLine($"Global position of P = {(global_pos[0],global_pos[1])}");
    }

    static float[] GetAngles(float[] a, float[] b, float[] c, float[] p)
    {
        float thetaUa, thetaUb, thetaUc, alpha, beta, origin;
      
        var ua = new float[2]{a[0]-p[0], a[1]-p[1]};
        var ub = new float[2]{b[0]-p[0], b[1]-p[1]};
        var uc = new float[2]{c[0]-p[0], c[1]-p[1]};
        
        //Angles at which P sees the beacons
        thetaUa = ua[0].ArcTan2(ua[1]);
        thetaUb = ub[0].ArcTan2(ub[1]);
        thetaUc = uc[0].ArcTan2(uc[1]);

        alpha =  thetaUa-thetaUb;
        beta =  thetaUb-thetaUc;
        origin = 2; //2 = B

        // If beta or alpha are null we mest change the origin
        if(beta == 0){
            alpha =  thetaUb-thetaUa;
            beta =  thetaUa-thetaUc;
            origin = 1; //1=A
        }else if(alpha == 0){
            alpha =  thetaUa-thetaUc;
            beta =  thetaUc-thetaUb;
            origin = 3; //3=C
        }

        var result = new float[]{alpha, beta, origin};

        //Console.WriteLine();
        //Console.WriteLine($"Angles from P  -----> A:{theta_ua*180/Math.PI}, B:{theta_ub*180/Math.PI}, C:{theta_uc*180/Math.PI}");
        //Console.WriteLine($"alpha: {alpha*180/Math.PI}");
        //Console.WriteLine($"beta: {beta*180/Math.PI}");
        //Console.WriteLine($"origin: {origin}");
        //Console.WriteLine();

        return result;
    }
        
        
}