using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression.Cost;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression.DataSets;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression
{
    public static class CGpRegressionUtils
    {
        public static CGpChromosomeRegressionEvaluator CreateEvaluator(this CGpRegressionDataSet dataSet, CGpRegressionCostFunction costFunction)
        {
            return new CGpChromosomeRegressionEvaluator(dataSet, costFunction);
        }

        public static CGpChromosomeRegressionEvaluator CreateEvaluator(this CGpRegressionCostFunction costFunction, CGpRegressionDataSet dataSet)
        {
            return new CGpChromosomeRegressionEvaluator(dataSet, costFunction);
        }
    }
}
