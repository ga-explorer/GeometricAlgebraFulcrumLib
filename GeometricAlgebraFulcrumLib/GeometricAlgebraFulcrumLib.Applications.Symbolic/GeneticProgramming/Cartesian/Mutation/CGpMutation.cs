namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Mutation
{
    public abstract class CGpMutation
    {
        public static CGpSingleMutation Single { get; }
            = new CGpSingleMutation();
        
        public static CGpPointMutation Point { get; }
            = new CGpPointMutation(false);

        public static CGpPointMutation PointAnn { get; }
            = new CGpPointMutation(true);

        public static CGpProbabilisticMutation Probabilistic { get; }
            = new CGpProbabilisticMutation(false);

        public static CGpProbabilisticMutation ProbabilisticActiveNodes { get; }
            = new CGpProbabilisticMutation(true);


        public abstract string Name { get; }

        public abstract void ApplyMutation(CGpChromosome chromosome, bool parametric);


        public override string ToString()
        {
            return Name;
        }
    }
}
