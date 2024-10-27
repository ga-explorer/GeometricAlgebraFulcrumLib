using GeneticSharp;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic2
{
    public class McOptGaFitness : 
        IFitness
    {
        public MetaContext OriginalContext { get; }

        public int IntermediateVariableCount { get; }

        public int OriginalComputationsCount { get; }


        public McOptGaFitness(MetaContext originalContext)
        {
            OriginalContext = originalContext;
            IntermediateVariableCount = originalContext.GetIntermediateVariables().Count();
            OriginalComputationsCount = originalContext.ComputationsCount;
        }


        public MetaContext GetMetaContext(McOptGaChromosome chromosome)
        {
            var indexSet = new SortedSet<int>(
                chromosome
                    .GetGenes()
                    .SelectIndexWhere(gene => (bool) gene.Value)
            );

            return OriginalContext.GetContextCopyWithMerge(indexSet).OptimizeContext();
        }

        public double Evaluate(IChromosome chromosome)
        {
            var newContext = GetMetaContext((McOptGaChromosome) chromosome);

            //var fitness = -Math.Log2(1d / newContext.ComputationsCount);
            
            var fitness = Math.Exp(
                OriginalComputationsCount - newContext.ComputationsCount
            );

            Console.WriteLine(fitness);

            return fitness;
        }
    }
}
