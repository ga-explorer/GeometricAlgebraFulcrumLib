namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Reproduction;

public class CGpMutateRandomParentReproductionScheme :
    CGpReproductionScheme
{
    public override string Name 
        => "Mutate Random Parent Reproduction Scheme";


    internal CGpMutateRandomParentReproductionScheme()
    {
    }

    
    public override void Execute(CGpChromosome[] parents, CGpChromosome[] children, bool parametric)
    {
        /* for each child */
        foreach (var child in children)
        {
            /* select random parent */
            var index = CGpParameters.GetRandomIndex(parents.Length);

            /* set child as clone of random parent */
            parents[index].CopyToChromosome(child);

            /* mutate newly cloned child */
            child.MutateChromosome(parametric);
        }
    }
}