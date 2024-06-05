namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Selection
{
    public abstract class CGpSelectionScheme
    {
        public static CGpByFittestSelectionScheme ByFittest { get; }
            = new CGpByFittestSelectionScheme();


        public abstract string Name { get; }

        public abstract void Execute(CGpChromosome[] parentChromosomes, CGpChromosome[] candidateChromosomes);
    }
}
