using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression.Cost;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression.DataSets;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression;

public sealed class CGpChromosomeRegressionEvaluator :
    CGpChromosomeEvaluator
{
    public CGpRegressionDataSet DataSet { get; private set; }

    public CGpRegressionCostFunction CostFunction { get; }


    internal CGpChromosomeRegressionEvaluator(CGpRegressionDataSet dataSet, CGpRegressionCostFunction costFunction)
    {
        DataSet = dataSet;
        CostFunction = costFunction;
    }


    public override double ComputeCost(CGpChromosome chromosome)
    {
        return CostFunction.ComputeCost(chromosome, DataSet);
    }

    public CGpChromosomeRegressionEvaluator UseDataSet(CGpRegressionDataSet dataSet)
    {
        DataSet = dataSet;

        return this;
    }
}