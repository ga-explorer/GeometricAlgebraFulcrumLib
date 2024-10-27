using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression.DataSets;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression.Cost;

public class CGpRegressionCostFunctionRms :
    CGpRegressionCostFunction
{
    public override string Name
        => "RMS Supervised Learning Regression Cost Function";


    internal CGpRegressionCostFunctionRms()
    {
    }


    public override double ComputeCost(CGpChromosome chromosome, CGpRegressionDataSet dataSet)
    {
        double error = 0;

        /* error checking */
        if (chromosome.Parameters.InputSize != dataSet.InputSize)
            throw new InvalidOperationException(
                "Error: the number of chromosome inputs must match the number of inputs specified the dataSet."
            );

        if (chromosome.Parameters.OutputSize != dataSet.OutputSize)
            throw new InvalidOperationException(
                "Error: the number of chromosome outputs must match the number of outputs specified the dataSet."
            );

        /* for each sample data */
        for (var i = 0; i < dataSet.Count; i++)
        {
            /* calculate the chromosome outputs for the set of inputs  */
            chromosome.ExecuteChromosome(
                dataSet.GetSampleInput(i)
            );

            var correctOutputVector = dataSet.GetSampleOutput(i);

            /* for each chromosome output */
            for (var j = 0; j < chromosome.Parameters.OutputSize; j++)
            {
                var correctOutput = correctOutputVector[j];
                var predictedOutput = chromosome.OutputValues[j];

                error += (correctOutput - predictedOutput).Square();
            }
        }

        var n = chromosome.Parameters.OutputSize * dataSet.SampleCount;

        return (error / n).Sqrt();
    }


}