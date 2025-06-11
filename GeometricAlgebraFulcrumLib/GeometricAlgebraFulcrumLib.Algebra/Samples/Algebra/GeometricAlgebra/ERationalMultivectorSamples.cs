using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Algebra.Samples.Algebra.GeometricAlgebra
{
    public  class ERationalMultivectorSamples
    {
        public static XGaProcessor<ERational> Processor { get; }
            = ScalarProcessorOfERational.Instance.CreateEuclideanXGaProcessor();

        public static IScalarProcessor<ERational> ScalarProcessor
            => Processor.ScalarProcessor;

        public static LaTeXComposer<ERational> LaTeXComposer { get; }
            = Processor.ScalarProcessor.CreateLaTeXComposer();

        public static Random RandGen { get; } 
            = new Random(11);


        public static void Example1()
        {
            // Generate two random vectors
            var v1 = RandGen.GetXGaVector(Processor, 10);
            var v2 = RandGen.GetXGaVector(Processor, 10);
            
            //var indexSet1 = RandGen.GetIndexSet(5, 10).GetUnitSubsets();
            //var indexSet2 = RandGen.GetIndexSet(5, 10).GetUnitSubsets();

            //var v1 = Processor.Vector(
            //    indexSet1.ToDictionary(
            //        id => id,
            //        _ => RandGen.GetERational()
            //    )
            //);
            
            //var v2 = Processor.Vector(
            //    indexSet2.ToDictionary(
            //        id => id,
            //        _ => RandGen.GetERational()
            //    )
            //);

            var mv1 = v1.Gp(v2);
            var mv2 = v1.Sp(v2) + v1.Op(v2);

            var mvDiff = mv1 - mv2;

            Console.WriteLine($"$v1 = {LaTeXComposer.GetMultivectorText(v1)}$");
            Console.WriteLine($"$v2 = {LaTeXComposer.GetMultivectorText(v2)}$");
            Console.WriteLine($"$mv1 = {LaTeXComposer.GetMultivectorText(mv1)}$");
            Console.WriteLine($"$mv2 = {LaTeXComposer.GetMultivectorText(mv2)}$");
            Console.WriteLine($"$mv2 - mv1 = {LaTeXComposer.GetMultivectorText(mvDiff)}$");
        }
    }
}
