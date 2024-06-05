namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Reproduction;

public class McGOptMutateRandomParentReproductionScheme :
    McGOptReproductionScheme
{
    public override string Name 
        => "Mutate Random Parent Reproduction Scheme";


    internal McGOptMutateRandomParentReproductionScheme()
    {
    }

    
    public override void Execute(McGOptChromosome[] parents, McGOptChromosome[] children)
    {
        var parentGeneration = parents[0].Generation;

        foreach (var child in children)
        {
            // select random parent
            var index = parents[0].Parameters.GetRandomIndex(parents.Length);

            // set child as clone of random parent
            parents[index].CopyToChromosome(child);

            // mutate newly cloned child
            child.MutateChromosome();

            child.Generation = parentGeneration + 1;
        }
    }
}