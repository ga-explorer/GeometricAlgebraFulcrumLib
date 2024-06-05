using GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.DataSets;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Cost
{
    public abstract class CGpCostFunction
    {
        public static CGpAbsSupervisedLearningCostFunction AbsSupervisedLearning { get; }
            = new CGpAbsSupervisedLearningCostFunction();

        public static CGpRmsSupervisedLearningCostFunction RmsSupervisedLearning { get; }
            = new CGpRmsSupervisedLearningCostFunction();



        public abstract string Name { get; }

        public abstract double ComputeCost(CGpChromosome chromosome, CGpDataSet dataSet);
    }
}
