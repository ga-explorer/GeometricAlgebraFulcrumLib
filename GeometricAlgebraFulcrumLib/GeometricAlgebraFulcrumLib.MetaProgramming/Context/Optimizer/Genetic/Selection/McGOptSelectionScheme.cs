namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Selection
{
    public abstract class McGOptSelectionScheme
    {
        public static McGOptSortByCostSelectionScheme SortByCost { get; }
            = new McGOptSortByCostSelectionScheme();


        public abstract string Name { get; }

        public abstract void Execute(McGOptChromosome[] parentChromosomes, McGOptChromosome[] candidateChromosomes);
    }
}
