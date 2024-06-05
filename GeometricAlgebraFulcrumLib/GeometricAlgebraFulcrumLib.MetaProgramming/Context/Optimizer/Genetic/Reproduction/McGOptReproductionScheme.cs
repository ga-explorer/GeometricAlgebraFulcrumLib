namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Reproduction
{
    public abstract class McGOptReproductionScheme
    {
        public static McGOptMutateRandomParentReproductionScheme MutateRandomParent { get; }
            = new McGOptMutateRandomParentReproductionScheme();


        public abstract string Name { get; }

        public abstract void Execute(McGOptChromosome[] parents, McGOptChromosome[] children);
    }
}
