using System.Collections.Immutable;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Selection;

public class McGOptSortByCostSelectionScheme :
    McGOptSelectionScheme
{
    private static IReadOnlyList<McGOptChromosome> SortChromosomesByCost(IEnumerable<McGOptChromosome> chromosomeArray)
    {
        return chromosomeArray
            .OrderBy(c => c.Cost)
            .ThenByDescending(c => c.Generation)
            .ToImmutableArray();

        //var count = chromosomeArray.Count;

        //for (var i = 0; i < count; i++)
        //for (var j = i; j < count; j++)
        //{
        //    if (chromosomeArray[i].Cost > chromosomeArray[j].Cost) 
        //        (chromosomeArray[i], chromosomeArray[j]) = (chromosomeArray[j], chromosomeArray[i]);
        //}
    }


    internal McGOptSortByCostSelectionScheme()
    {
    }


    public override string Name 
        => "Sort By Cost Selection Scheme";


    public override void Execute(McGOptChromosome[] parentChromosomes, McGOptChromosome[] candidateChromosomes)
    {
        var chromosomeList = SortChromosomesByCost(candidateChromosomes);

        for (var i = 0; i < parentChromosomes.Length; i++)
            chromosomeList[i].CopyToChromosome(parentChromosomes[i]);
    }
}