using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression.DataSets;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression.Cost
{
    public abstract class CGpRegressionCostFunction
    {
        public static CGpRegressionCostFunctionAbs Abs { get; }
            = new CGpRegressionCostFunctionAbs();

        public static CGpRegressionCostFunctionRms Rms { get; }
            = new CGpRegressionCostFunctionRms();

        
        public abstract string Name { get; }

        public abstract double ComputeCost(CGpChromosome chromosome, CGpRegressionDataSet dataSet);
    }
}
