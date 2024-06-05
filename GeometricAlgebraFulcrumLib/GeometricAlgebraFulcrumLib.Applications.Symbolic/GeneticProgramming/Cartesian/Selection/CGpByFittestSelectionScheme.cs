namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Selection;

public class CGpByFittestSelectionScheme :
    CGpSelectionScheme
{
    private static void SortByCost(IList<CGpChromosome> chromosomeArray)
    {
        var count = chromosomeArray.Count;

        for (var i = 0; i < count; i++)
        for (var j = i; j < count; j++)
        {
            if (chromosomeArray[i].Cost > chromosomeArray[j].Cost) 
                (chromosomeArray[i], chromosomeArray[j]) = (chromosomeArray[j], chromosomeArray[i]);
        }
    }


    internal CGpByFittestSelectionScheme()
    {
    }


    public override string Name 
        => "By Fittest Selection Scheme";


    public override void Execute(CGpChromosome[] parentChromosomes, CGpChromosome[] candidateChromosomes)
    {
        SortByCost(candidateChromosomes);

        for (var i = 0; i < parentChromosomes.Length; i++)
            candidateChromosomes[i].CopyToChromosome(parentChromosomes[i]);
    }
}