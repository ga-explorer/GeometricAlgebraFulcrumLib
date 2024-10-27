using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression.Cost;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression.DataSets;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Functions;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Mutation;

namespace GeometricAlgebraFulcrumLib.Optimization.Samples
{
    public static class CGpSamples
    {
        public static void Example1()
        {
            var functionSet = new CGpFunctionSet();

            functionSet.AddFunctions(
                CGpFloat64Function.Identity,
                CGpFloat64Function.Negative,
                CGpFloat64Function.Inverse,
                CGpFloat64Function.Plus,
                CGpFloat64Function.Subtract,
                CGpFloat64Function.Times,
                CGpFloat64Function.Divide,
                CGpFloat64Function.Sin,
                CGpFloat64Function.Cos
            );

            var parameters = new CGpParameters(functionSet, 4, 2, 5, 4, 2)
            {
                MaxLevelsBack = 2,
                MaxLevelsForward = 2,
                Mu = 1,
                Lambda = 4,
                NodeInputWeightRange = 0,
                ParametricNodeInputWeightRatio = 0,
                TargetCost = 0.03,
                UpdateFrequency = 100,
                //CostFunction = CGpRegressionCostFunction.Abs
            };

            var recurrentConnectionsAllowed = true;
            for (var j = 0; j < parameters.GridColumns; j++)
            {
                Console.WriteLine($"Column {j}");

                for (var i = 0; i < parameters.GridRows; i++)
                {
                    var index = parameters.GetNodeIndex(i, j);

                    var (row, col) = parameters.GetNodeGridPosition(index);

                    Debug.Assert(row == i & col == j);

                    Console.WriteLine($"   Node {index:00} at ({i:0}, {j:0})");

                    var (i1, i2) =
                        parameters.GetNodeInputNodesIndexRange(
                            index,
                            recurrentConnectionsAllowed
                        );

                    var count = parameters.GetNodeMaxInputNodesCount(
                        index,
                        recurrentConnectionsAllowed
                    );

                    if (i1 < 0)
                    {
                        Console.WriteLine($"      Input node range: None => {count:0} input nodes");
                    }
                    else
                    {
                        var (r1, c1) = parameters.GetNodeGridPosition(i1);
                        var (r2, c2) = parameters.GetNodeGridPosition(i2);

                        Console.WriteLine($"      Input node range: ({r1:0}, {c1:0})-({r2:0}, {c2:0}) => {count:0} input nodes");
                    }
                }

                Console.WriteLine();
            }
        }

        public static void Example2()
        {
            var functionSet = new CGpFunctionSet();

            functionSet.AddFunctions(
                CGpFloat64Function.Constant(1),
                CGpFloat64Function.Identity,
                CGpFloat64Function.Negative,
                CGpFloat64Function.Inverse,
                CGpFloat64Function.Log10,
                CGpFloat64Function.Abs,
                CGpFloat64Function.Step,
                CGpFloat64Function.Plus,
                CGpFloat64Function.Subtract,
                CGpFloat64Function.Times,
                CGpFloat64Function.Divide,
                CGpFloat64Function.Sin,
                CGpFloat64Function.Cos,
                CGpFloat64Function.Tan
            );

            var parameters = new CGpParameters(functionSet, 1, 1, 10, 10, 5)
            {
                Mu = 2,
                Lambda = 8,
                MutationRate = 0.2,
                NodeInputWeightRange = 5,
                ParametricNodeInputWeightRatio = 1,
                RecurrentConnectionProbability = 0,
                TargetCost = 0.001,
                UpdateFrequency = 25,
                //CostFunction = CGpRegressionCostFunction.Rms,
                MutationType = CGpMutation.Probabilistic
            };

            const int sampleCount = 101;
            var inputValues = 0.0001d.GetLinearRange(5 * Math.PI, sampleCount, false).ToArray();
            var outputValues = inputValues.Select(t => Math.Sin(t) / t).ToArray();

            var dataSet = CGpRegressionDataSetStored.Create(1, 1);

            for (var i = 0; i < sampleCount; i++)
                dataSet.AddSample(
                    new[] { inputValues[i] },
                    new[] { outputValues[i] }
                );

            //var dataSet = CGpRegressionDataSetStored.CreateFromFile(
            //    @"D:\Projects\Study\Genetic Programming\DataSets\sin2saw.data"
            //);

            var evaluator = 
                CGpRegressionCostFunction.Rms.CreateEvaluator(dataSet);

            var context =
                CGpContext.Create(parameters, evaluator);

            var chromosome = CGpChromosome.Create(context);

            //context.DataSet = dataSet;

            var solution =
                context.RunCGp(10000);

            Console.WriteLine(solution.ToString());
        }

        public static void Example3()
        {
            var functionSet = new CGpFunctionSet();

            functionSet.AddFunctions(
                CGpFloat64Function.Constant(1),
                CGpFloat64Function.Identity,
                CGpFloat64Function.Negative,
                CGpFloat64Function.Inverse,
                CGpFloat64Function.Log10,
                CGpFloat64Function.Abs,
                CGpFloat64Function.Step,
                CGpFloat64Function.Plus,
                CGpFloat64Function.Subtract,
                CGpFloat64Function.Times,
                CGpFloat64Function.Divide,
                CGpFloat64Function.Sin,
                CGpFloat64Function.Cos,
                CGpFloat64Function.Tan
            );

            var parameters = new CGpParameters(functionSet, 1, 1, 10, 10, 5)
            {
                Mu = 2,
                Lambda = 8,
                MutationRate = 0.2,
                NodeInputWeightRange = 5,
                ParametricNodeInputWeightRatio = 1,
                RecurrentConnectionProbability = 0,
                TargetCost = 0.001,
                UpdateFrequency = 25,
                //CostFunction = CGpRegressionCostFunction.Rms,
                MutationType = CGpMutation.Probabilistic
            };

            //const int sampleCount = 101;
            //var inputValues = 0.0001d.GetLinearRange(5 * Math.PI, sampleCount, false).ToArray();
            //var outputValues = inputValues.Select(t => Math.Sin(t) / t).ToArray();

            //var dataSet = CGpStoredDataSet.Create(1, 1);

            //for (var i = 0; i < sampleCount; i++)
            //    dataSet.AddSample(
            //        new[] { inputValues[i] },
            //        new[] { outputValues[i] }
            //    );

            var dataSet = CGpRegressionDataSetStored.CreateFromFile(
                @"D:\Projects\Study\Genetic Programming\DataSets\sin2saw.data"
            );

            var evaluator = 
                CGpRegressionCostFunction.Rms.CreateEvaluator(dataSet);

            var context =
                CGpContext.Create(parameters, evaluator);

            //context.DataSet = dataSet;

            var solution =
                context.RunCGp(10000);

            Console.WriteLine(solution.ToString());
        }
    }
}
