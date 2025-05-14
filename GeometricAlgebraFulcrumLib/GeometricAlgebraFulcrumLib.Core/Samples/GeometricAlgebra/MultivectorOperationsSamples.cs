using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.Core.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Core.Samples.GeometricAlgebra
{
    public static class MultivectorOperationsSamples
    {
        private static Random RandomGenerator { get; } = new Random(10);

        public static XGaFloat64EuclideanProcessor Processor { get; }
            = XGaFloat64EuclideanProcessor.Instance;

        public static LaTeXComposerFloat64 LaTeXComposer { get; }
            = LaTeXComposerFloat64.DefaultComposer;


        private static XGaFloat64Vector GetRandomVector(int zeroCount, int termCount)
        {
            var scalars = Enumerable.Repeat(0d, zeroCount).Concat(
                RandomGenerator.GetFloat64Array1D(termCount, -1, 1)
            ).ToArray();

            return Processor.Vector(scalars);
        }
        
        public static void Example1()
        {
            const int n1 = 0;
            const int n = 2000;
            
            var v1 = GetRandomVector(n1, n).DivideByNorm();
            var v2 = GetRandomVector(n1, n).DivideByNorm();

            var t1 = DateTime.Now;

            var v12Sp = v1.Sp(v2);
            var v12Op = v1.Op(v2);
            var v12Gp = v1.Gp(v2);
            
            var diff = v12Gp - v12Sp.Add(v12Op);

            var t2 = DateTime.Now;
            Console.WriteLine($"Finished in {(t2 - t1).TotalSeconds:G} seconds");
            Console.WriteLine();

            Debug.Assert(diff.IsNearZero());

            if (n <= 10)
            {
                Console.WriteLine($@"$\boldsymbol{{v}}_{{1}} = {LaTeXComposer.GetMultivectorText(v1)}$");
                Console.WriteLine($@"$\boldsymbol{{v}}_{{2}} = {LaTeXComposer.GetMultivectorText(v2)}$");
                Console.WriteLine();

                Console.WriteLine($@"$\boldsymbol{{v}}_{{1}} \cdot \boldsymbol{{v}}_{{2}} = {LaTeXComposer.GetMultivectorText(v12Sp)}$");
                Console.WriteLine();

                Console.WriteLine($@"$\boldsymbol{{v}}_{{1}} \wedge \boldsymbol{{v}}_{{2}} = {LaTeXComposer.GetMultivectorText(v12Op)}$");
                Console.WriteLine();

                Console.WriteLine($@"$\boldsymbol{{v}}_{{1}} \boldsymbol{{v}}_{{2}} = {LaTeXComposer.GetMultivectorText(v12Gp)}$");
                Console.WriteLine();

                Console.WriteLine($@"$D = {LaTeXComposer.GetMultivectorText(diff)}$");
                Console.WriteLine();
            }
            
        }

        public static void Example2()
        {
            const int n1 = 0;
            const int n = 20;
            
            var v1 = GetRandomVector(n1, n).DivideByNorm();
            var v2 = GetRandomVector(n1, n).DivideByNorm();

            var a1 = v1.GetEuclideanAngle(v2, true);

            var t1 = DateTime.Now;

            //var v12Sp = v1.Sp(v2);
            //var v12Op = v1.Op(v2);
            var v12Gp = v1.Gp(v2);
            var v12Op = v12Gp.GetBivectorPart();

            //var diff = v12Gp - v12Sp.Add(v12Op);

            var u1 = GetRandomVector(n1, n);
            var u2 = v12Gp.Gp(u1).Gp(v12Gp.Reverse()).GetVectorPart();

            var a2 = u1.ProjectOnBivector(v12Op).GetEuclideanAngle(u2.ProjectOnBivector(v12Op));

            var t2 = DateTime.Now;
            Console.WriteLine($"Finished in {(t2 - t1).TotalSeconds:G} seconds");
            Console.WriteLine();

            //Debug.Assert(diff.IsNearZero());

            Console.WriteLine($"a1 = {a1.DegreesValue:G}");
            Console.WriteLine($"a2 = {a2.DegreesValue:G}");

            if (n <= 10)
            {
                Console.WriteLine($@"$\boldsymbol{{v}}_{{1}} = {LaTeXComposer.GetMultivectorText(v1)}$");
                Console.WriteLine($@"$\boldsymbol{{v}}_{{2}} = {LaTeXComposer.GetMultivectorText(v2)}$");
                Console.WriteLine();

                //Console.WriteLine($@"$\boldsymbol{{v}}_{{1}} \cdot \boldsymbol{{v}}_{{2}} = {LaTeXComposer.GetMultivectorText(v12Sp)}$");
                //Console.WriteLine();

                //Console.WriteLine($@"$\boldsymbol{{v}}_{{1}} \wedge \boldsymbol{{v}}_{{2}} = {LaTeXComposer.GetMultivectorText(v12Op)}$");
                //Console.WriteLine();

                Console.WriteLine($@"$\boldsymbol{{v}}_{{1}} \boldsymbol{{v}}_{{2}} = {LaTeXComposer.GetMultivectorText(v12Gp)}$");
                Console.WriteLine();

                //Console.WriteLine($@"$D = {LaTeXComposer.GetMultivectorText(diff)}$");
                //Console.WriteLine();
            }
            
        }
    }
}
