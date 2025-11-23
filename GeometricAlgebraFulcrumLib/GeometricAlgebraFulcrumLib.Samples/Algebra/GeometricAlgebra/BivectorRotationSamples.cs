using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Subspaces;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;

namespace GeometricAlgebraFulcrumLib.Samples.Algebra.GeometricAlgebra
{
    public static class BivectorRotationSamples
    {
        public static void MultivectorFunctionsExample1()
        {
            const int n = 4;
            var rg = new Random(10);
            var ga = XGaFloat64EuclideanProcessor.Instance;

            var bv = ga.Bivector(
                rg.GetFloat64Array1D(n * (n - 1) / 2, -1, 1)
            ).DivideByNorm() * 0.5;

            var bvExp = 
                bv.ExpByTaylor(1e-16).DivideByNorm();
            
            var dExp = 
                (bvExp.Gp(bv).Gp(bvExp.Reverse()) - bv).RemoveSmallTerms();

            Console.WriteLine("Exp:");
            Console.WriteLine(bv);
            Console.WriteLine(bv.Norm());
            Console.WriteLine(bvExp);
            Console.WriteLine(bvExp.Norm());
            Console.WriteLine(dExp);
            Console.WriteLine();

            Console.WriteLine("Sin:");
            var bvSin = bv.SinByTaylor(1e-16);
            Console.WriteLine(bvSin);
            Console.WriteLine(bvSin.Norm());
            Console.WriteLine();

            Console.WriteLine("Cos:");
            var bvCos = bv.CosByTaylor(1e-16);
            Console.WriteLine(bvCos);
            Console.WriteLine(bvCos.Norm());
            Console.WriteLine();
            
            Console.WriteLine("Sinh:");
            var bvSinh = bv.SinhByTaylor(1e-16);
            Console.WriteLine(bvSinh);
            Console.WriteLine(bvSinh.Norm());
            Console.WriteLine();

            Console.WriteLine("Cosh:");
            var bvCosh = bv.CoshByTaylor(1e-16);
            Console.WriteLine(bvCosh);
            Console.WriteLine(bvCosh.Norm());
            Console.WriteLine();

            var mv = ga.Multivector(
                rg.GetFloat64Array1D(1 << n, -1, 1)
            ).DivideByNorm() * 0.5 + 3;

            //var bvInv = 
            //    ga.Multivector(
            //        bv.LeftGpMapArray(n).FirstColumnOfInverse()
            //    ).RemoveSmallTerms();
            
            var mvInv = 
                mv.InverseByNeumann(1e-16);

            var dInv = 
                (mv.Gp(mvInv) - 1).RemoveSmallTerms();

            Console.WriteLine("Inverse:");
            Console.WriteLine(mv);
            Console.WriteLine(mv.Norm());
            Console.WriteLine(mvInv);
            Console.WriteLine(mvInv.Norm());
            Console.WriteLine(dInv);
            Console.WriteLine();
        }

        public static void MinimalSubspaceExample1()
        {
            var ga = XGaFloat64EuclideanProcessor.Instance;
            
            var e1234 = ga.PseudoScalar(4);

            var b1 = ga.BivectorTerm(0, 1);
            var b2 = ga.BivectorTerm(2, 3);

            Console.WriteLine($"I = {e1234}");
            Console.WriteLine($"B1 = {b1}");
            Console.WriteLine($"B2 = {b2}");
            Console.WriteLine();
            
            var b1P = (1 + e1234).Gp(b1).Divide(2).RemoveSmallTerms();
            var b1N = (1 - e1234).Gp(b1).Divide(2).RemoveSmallTerms();
            
            var b2P = (1 + e1234).Gp(b2).Divide(2).RemoveSmallTerms();
            var b2N = (1 - e1234).Gp(b2).Divide(2).RemoveSmallTerms();
            
            Console.WriteLine($"B1+ = {b1P}");
            Console.WriteLine($"*B1+ = {e1234.Gp(b1P)}");
            Console.WriteLine($"B1- = {b1N}");
            Console.WriteLine($"*B1- = {e1234.Gp(b1N)}");
            Console.WriteLine();

            Console.WriteLine($"B2+ = {b2P}");
            Console.WriteLine($"*B2+ = {e1234.Gp(b2P)}");
            Console.WriteLine($"B2- = {b2N}");
            Console.WriteLine($"*B2- = {e1234.Gp(b2N)}");
            Console.WriteLine();

            Console.WriteLine($"B1 - (B1+ + B1-) = {b1 - (b1P + b1N)}");
            Console.WriteLine($"B2 - (B2+ + B2-) = {b2 - (b2P + b2N)}");
            Console.WriteLine();
            
            var rP = (1 - b2P.DivideByNorm().Gp(b1P.DivideByNorm())).RemoveSmallTerms().DivideByNorm();
            var rN = (1 - b2N.DivideByNorm().Gp(b1N.DivideByNorm())).RemoveSmallTerms().DivideByNorm();
            var r = rP.Gp(rN);

            Console.WriteLine($"R+ = {rP}");
            Console.WriteLine($"R- = {rN}");
            Console.WriteLine($"R  = {r}");
            Console.WriteLine($"R~ = {r.VersorInverse()}");
            Console.WriteLine();

            Console.WriteLine($"B2P = {b2P}");
            Console.WriteLine($"R+ B1+ R+~ = {rP.Gp(b1P).Gp(rP.VersorInverse())}");
            Console.WriteLine();

            Console.WriteLine($"B2N = {b2N}");
            Console.WriteLine($"R- B1- R-~ = {rN.Gp(b1N).Gp(rN.VersorInverse())}");
            Console.WriteLine();
        }

        public static void MinimalSubspaceExample2()
        {
            const int n = 10;

            var randGen = new Random(10);
            var ga = XGaFloat64EuclideanProcessor.Instance;
            
            // Create two random 2-blades in n-dimensional GA
            var u1 = ga.Vector(
                randGen.GetFloat64Array1D(n, -1, 1)
            );
            
            var u2 = ga.Vector(
                randGen.GetFloat64Array1D(n, -1, 1)
            );

            var u = u1.Op(u2).DivideByNorm();

            var v1 = ga.Vector(
                randGen.GetFloat64Array1D(n, -1, 1)
            );
            
            var v2 = ga.Vector(
                randGen.GetFloat64Array1D(n, -1, 1)
            );

            var v = v1.Op(v2).DivideByNorm();

            // Find common basis for the minimal space containing the two 2-blades
            var composer = new XGaFloat64VectorListOrthonormalizer();

            var result = composer.Orthonormalize([u1, u2, v1, v2]);

            Console.WriteLine($"Found {result.Rank} basis vectors:");
            for (var i = 0; i < result.Rank; i++)
            {
                Console.WriteLine($"   e{i + 1} = {result.OrthonormalBasis[i]}");
            }
            Console.WriteLine();

            var cu1 = ga.Vector(
                u1.GetMetricProjectionCoefficients(result.OrthonormalBasis)
            ).RemoveSmallTerms();

            var cu2 = ga.Vector(
                u2.GetMetricProjectionCoefficients(result.OrthonormalBasis)
            ).RemoveSmallTerms();

            var cu = cu1.Op(cu2).RemoveSmallTerms().DivideByNorm();

            var cv1 = ga.Vector(
                v1.GetMetricProjectionCoefficients(result.OrthonormalBasis)
            ).RemoveSmallTerms();

            var cv2 = ga.Vector(
                v2.GetMetricProjectionCoefficients(result.OrthonormalBasis)
            ).RemoveSmallTerms();
            
            var cv = cv1.Op(cv2).RemoveSmallTerms().DivideByNorm();

            Console.WriteLine($"Reduced 4D vectors:");
            Console.WriteLine($"   cu1 = {cu1}");
            Console.WriteLine($"   cu2 = {cu2}");
            Console.WriteLine($"   cv1 = {cv1}");
            Console.WriteLine($"   cv2 = {cv2}");
            Console.WriteLine();

            Console.WriteLine($"norm(cu) = {cu.Norm()}");
            Console.WriteLine($"norm(cv) = {cv.Norm()}");
            Console.WriteLine();
            
            var i4 = ga.PseudoScalar(4);

            var cuP = (cu + cu.Gp(i4)).RemoveSmallTerms().DivideByNorm();
            var cuN = (cu - cu.Gp(i4)).RemoveSmallTerms().DivideByNorm();
            
            var cvP = (cv + cv.Gp(i4)).RemoveSmallTerms().DivideByNorm();
            var cvN = (cv - cv.Gp(i4)).RemoveSmallTerms().DivideByNorm();
            
            Console.WriteLine($"cu+ = {cuP}");
            Console.WriteLine($"cu- = {cuN}");
            Console.WriteLine();

            Console.WriteLine($"cv+ = {cvP}");
            Console.WriteLine($"cv- = {cvN}");
            Console.WriteLine();

            var rP = (1 - cvP.Gp(cuP)).RemoveSmallTerms().DivideByNorm();
            var rN = (1 - cvN.Gp(cuN)).RemoveSmallTerms().DivideByNorm();
            var r = rP.Gp(rN);

            Console.WriteLine($"R+ = {rP}");
            Console.WriteLine($"R- = {rN}");
            Console.WriteLine($"R  = {r}");
            Console.WriteLine();
        }
    }
}
