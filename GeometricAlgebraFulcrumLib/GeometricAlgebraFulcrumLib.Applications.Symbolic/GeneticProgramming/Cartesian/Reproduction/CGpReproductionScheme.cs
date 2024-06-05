namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Reproduction
{
    public abstract class CGpReproductionScheme
    {
        public static CGpMutateRandomParentReproductionScheme MutateRandomParent { get; }
            = new CGpMutateRandomParentReproductionScheme();


        public abstract string Name { get; }

        public abstract void Execute(CGpChromosome[] parents, CGpChromosome[] children, bool parametric);
    }
}
