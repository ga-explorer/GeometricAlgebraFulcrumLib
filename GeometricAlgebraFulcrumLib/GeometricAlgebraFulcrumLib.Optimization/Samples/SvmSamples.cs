using System.Collections.Immutable;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Functions;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Mutation;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using VectSharp;
using VectSharp.Raster;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression.DataSets;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression.Cost;
using GeometricAlgebraFulcrumLib.Optimization.SVM.LibSVM;

namespace GeometricAlgebraFulcrumLib.Optimization.Samples
{
    public static class SvmSamples
    {
        private static void PrintFunction(string output)
        {
            Console.Write(output);
        }

        public static void VisualizeModel(SvmModel model, SvmDataSet dataSet, int imageSize = 2048)
        {
            Debug.Assert(dataSet.FeatureCount == 2);

            const double x1 = -1.1;
            const double x2 = 1.1;

            const double y1 = -1.1;
            const double y2 = 1.1;

            var xIndexList =
                imageSize.GetRange().ToImmutableArray();

            var yIndexList =
                imageSize.GetRange().ToImmutableArray();

            var xValueList =
                xIndexList.Select(ix =>
                    (ix + 0.5) * (x2 - x1) / imageSize + x1
                ).ToImmutableArray();

            var yValueList =
                yIndexList.Select(iy =>
                    (iy + 0.5) * (y1 - y2) / imageSize + x2
                ).ToImmutableArray();

            var sampleCount = imageSize * imageSize;
            var featureArray = new double[sampleCount, 2];

            var sampleIndex = 0;
            for (var ix = 0; ix < imageSize; ix++)
            {
                var x = xValueList[ix];

                for (var iy = 0; iy < imageSize; iy++)
                {
                    var y = yValueList[iy];

                    featureArray[sampleIndex, 0] = x;
                    featureArray[sampleIndex, 1] = y;

                    sampleIndex++;
                }
            }

            var resultArray = model.Predict(featureArray);

            var width = imageSize;
            var height = imageSize;
            var pixelByteArray = new byte[width * height * 3];

            sampleIndex = 0;
            var pixelByteIndex = 0;
            for (var ix = 0; ix < imageSize; ix++)
            {
                for (var iy = 0; iy < imageSize; iy++)
                {
                    var sampleOutput = resultArray[sampleIndex];

                    var color = sampleOutput == 1d
                        ? Colours.LightBlue
                        : Colours.DarkRed;

                    pixelByteArray[pixelByteIndex++] = (byte)(color.R * 255);
                    pixelByteArray[pixelByteIndex++] = (byte)(color.G * 255);
                    pixelByteArray[pixelByteIndex++] = (byte)(color.B * 255);

                    sampleIndex++;
                }
            }

            var rasterImage = new RasterImage(
                pixelByteArray,
                width,
                height,
                PixelFormats.RGB,
                false
            );

            var image = new Page(imageSize, imageSize);
            var imageGraphics = image.Graphics;

            imageGraphics.DrawRasterImage(0, 0, rasterImage);

            const double dotSize = 5d;
            foreach (var dataRecord in dataSet)
            {
                var x = dataRecord.InputFeatures[0];
                var y = dataRecord.InputFeatures[1];
                var color = dataRecord.OutputValue == 1d
                    ? Colours.LightBlue
                    : Colours.DarkRed;

                var ix = width * (x - x1) / (x2 - x1) - 0.5;
                var iy = height * (y - y2) / (y1 - y2) - 0.5;

                imageGraphics.FillRectangle(
                    ix - dotSize,
                    iy - dotSize,
                    1 + dotSize * 2,
                    1 + dotSize * 2,
                    new SolidColourBrush(color)
                );

                imageGraphics.StrokeRectangle(
                    ix - dotSize,
                    iy - dotSize,
                    1 + dotSize * 2,
                    1 + dotSize * 2,
                    Colours.Black,
                    1
                );
            }

            image.SaveAsPNG(@"D:\SvmModel.png");
        }

        public static void Sample1()
        {
            const string mainPath =
                @"D:\Projects\Study\Machine Learning\Support Vector Machines\Implementation\Datasets";

            // Set print function for accessing output logs
            // Please do not call this function more than a couple of times since it may cause memory leak !
            SvmUtils.SetPrintStringFunction(PrintFunction);

            // Load the datasets: In this example I use the same datasets for training and testing which is not recommended
            var trainingSet = SvmDataSet.LoadFromFile(Path.Combine(mainPath, "wine.txt"));
            var testSet = SvmDataSet.LoadFromFile(Path.Combine(mainPath, "wine.txt"));

            // Normalize the datasets if you want: L2 Norm => x / ||x||
            var (translationValues, scalingFactors) =
                trainingSet.GetUnitCubeNormalizationFactors();

            trainingSet = trainingSet.GetCopyUnitCube(translationValues, scalingFactors);
            testSet = testSet.GetCopyUnitCube(translationValues, scalingFactors);

            //trainingSet = trainingSet.GetCopyNormalized(SvmNormType.L2);
            //testSet = testSet.GetCopyNormalized(SvmNormType.L2);

            // Select the parameter set
            var parameter = new SvmParameters
            {
                Type = SvmTaskType.CSvc,
                Kernel = SvmKernelType.Rbf,
                C = 1,
                Gamma = 1
            };

            // Do cross validation to check this parameter set is correct for the dataset or not
            const int nFold = 5;
            trainingSet.CrossValidation(parameter, nFold, out var crossValidationResults);

            // Evaluate the cross validation result
            // If it is not good enough, select the parameter set again
            var crossValidationAccuracy = trainingSet.EvaluateClassificationProblem(crossValidationResults);

            // Train the model, If your parameter set gives good result on cross validation
            var model = trainingSet.Train(parameter);

            // Save the model
            model.SaveModel(@"Model\wine_model.txt");

            // Predict the instances in the test set
            var testResults = testSet.Predict(model);

            // Evaluate the test results
            var testAccuracy = testSet.EvaluateClassificationProblem(testResults, model.Labels, out var confusionMatrix);

            // Print the results
            Console.WriteLine("\n\nCross validation accuracy: " + crossValidationAccuracy);
            Console.WriteLine("\nTest accuracy: " + testAccuracy);
            Console.WriteLine("\nConfusion matrix:\n");

            // Print formatted confusion matrix
            Console.Write($"{"",6}");

            foreach (var label in model.Labels)
                Console.Write($"{"(" + label + ")",5}");

            Console.WriteLine();

            for (var i = 0; i < confusionMatrix.GetLength(0); i++)
            {
                Console.Write($"{"(" + model.Labels[i] + ")",5}");
                for (var j = 0; j < confusionMatrix.GetLength(1); j++)
                    Console.Write($"{confusionMatrix[i, j],5}");
                Console.WriteLine();
            }
        }

        public static void Sample2()
        {
            const string mainPath =
                @"D:\Projects\Study\Machine Learning\Support Vector Machines\Implementation\Datasets";

            // Set print function for accessing output logs
            // Please do not call this function more than a couple of times since it may cause memory leak !
            SvmUtils.SetPrintStringFunction(PrintFunction);

            // Load the datasets: In this example I use the same datasets for training and testing which is not recommended
            var trainingSet = GetDataSet2(1000);
            var testSet = GetDataSet2(1000);

            // Normalize the datasets if you want: L2 Norm => x / ||x||
            var (translationValues, scalingFactors) =
                trainingSet.GetUnitCubeNormalizationFactors();

            trainingSet = trainingSet.GetCopyUnitCube(translationValues, scalingFactors);
            testSet = testSet.GetCopyUnitCube(translationValues, scalingFactors);

            var (minCorner, maxCorner) =
                trainingSet.GetRange();

            //trainingSet = trainingSet.GetCopyNormalized(SvmNormType.L2);
            //testSet = testSet.GetCopyNormalized(SvmNormType.L2);

            // Select the parameter set
            var parameter = new SvmParameters
            {
                Type = SvmTaskType.CSvc,
                Kernel = SvmKernelType.Rbf,
                C = 1,
                Gamma = 1
            };

            //Do cross validation to check this parameter set is correct for the dataset or not
            const int nFold = 5;
            trainingSet.CrossValidation(parameter, nFold, out var crossValidationResults);

            // Evaluate the cross validation result
            // If it is not good enough, select the parameter set again
            var crossValidationAccuracy =
                trainingSet.EvaluateClassificationProblem(crossValidationResults);

            var crossValidationAccuracyPerClass =
                trainingSet.EvaluateClassificationProblemPerClass(crossValidationResults).ToImmutableArray();

            var crossValidationAccuracyAverage =
                crossValidationAccuracyPerClass
                    .Select(p => p.Value)
                    .Average();

            Console.WriteLine();
            Console.WriteLine("Cross validation accuracy:");
            Console.WriteLine($"   Total  : {crossValidationAccuracy:P2}");
            Console.WriteLine($"   Average: {crossValidationAccuracyAverage:P2}");

            foreach (var (classLabel, classAccuracy) in crossValidationAccuracyPerClass)
                Console.WriteLine($"   Class {classLabel}: {classAccuracy:P2}");

            Console.WriteLine();

            // Train the model, If your parameter set gives good result on cross validation
            var model = trainingSet.Train(parameter);

            // Save the model
            model.SaveModel(@"Model\wine_model.txt");

            //VisualizeModel(model, testSet);

            // Predict the instances in the test set
            var testResults = testSet.Predict(model);

            // Evaluate the test results
            var testAccuracy = testSet.EvaluateClassificationProblem(testResults, model.Labels, out var confusionMatrix);

            // Print the results

            Console.WriteLine("\nTest accuracy: " + testAccuracy);
            Console.WriteLine("\nConfusion matrix:\n");

            // Print formatted confusion matrix
            Console.Write($"{"",6}");

            foreach (var label in model.Labels)
                Console.Write($"{"(" + label + ")",5}");

            Console.WriteLine();

            for (var i = 0; i < confusionMatrix.GetLength(0); i++)
            {
                Console.Write($"{"(" + model.Labels[i] + ")",5}");
                for (var j = 0; j < confusionMatrix.GetLength(1); j++)
                    Console.Write($"{confusionMatrix[i, j],5}");
                Console.WriteLine();
            }

            return;

            SvmDataSet GetDataSet1(int sampleCount)
            {
                const double a = 3;
                const double b = 2;
                const double c = 10;

                var rand = new Random();
                var dataSet = new SvmDataSet();

                for (var i = 0; i < sampleCount; i++)
                {
                    var tolerance = rand.NextDouble() * 16 - 8;

                    var feature = new double[]
                    {
                        rand.NextDouble() * 100 - 50,
                        rand.NextDouble() * 100 - 50
                    };

                    var classValue = a * feature[0] + b * feature[1] + c < tolerance
                        ? 1d : 2d;

                    dataSet.Add(feature, classValue);
                }

                return dataSet;
            }

            SvmDataSet GetDataSet2(int sampleCount)
            {
                var rand = new Random();
                var dataSet = new SvmDataSet();

                for (var i = 0; i < sampleCount; i++)
                {
                    var tolerance = 0;// rand.NextDouble() * 8 - 4;

                    var x = rand.NextDouble() * 100 - 50;
                    var y = rand.NextDouble() * 100 - 50;
                    //var z = rand.NextDouble() * 100 - 50;
                    //var feature = new double[] {x, y, z};
                    //var classValue = Math.Sqrt(x * x + y * y + z * z) - 35 < tolerance ? 1d : 2d;

                    var feature = new double[] { x, y };
                    var classValue = Math.Sqrt(x * x + y * y) - 35 < tolerance ? 1d : 2d;

                    dataSet.Add(feature, classValue);
                }

                return dataSet;
            }
        }

        public static void Sample3()
        {
            var functionSet = new CGpFunctionSet();

            functionSet.AddFunctions(
                CGpFloat64Function.UnitRange.T1,
                CGpFloat64Function.UnitRange.T2,
                CGpFloat64Function.UnitRange.T3,
                CGpFloat64Function.UnitRange.T4,
                CGpFloat64Function.UnitRange.T5,
                CGpFloat64Function.UnitRange.UNegative,
                CGpFloat64Function.UnitRange.UReciprocal,
                CGpFloat64Function.UnitRange.UAbs,
                CGpFloat64Function.UnitRange.UCos,
                CGpFloat64Function.UnitRange.USin,
                CGpFloat64Function.UnitRange.UTan,
                CGpFloat64Function.UnitRange.USquare,
                CGpFloat64Function.UnitRange.UCube,
                CGpFloat64Function.UnitRange.USqrt,
                CGpFloat64Function.UnitRange.UCbrt,
                CGpFloat64Function.UnitRange.UMean,
                CGpFloat64Function.UnitRange.UTimes
            );

            var parameters = new CGpParameters(functionSet, 2, 10, 10, 10, 5)
            {
                Mu = 2,
                Lambda = 8,
                MutationRate = 0.2,
                NodeInputWeightRange = 5,
                ParametricNodeInputWeightRatio = 0.5,
                RecurrentConnectionProbability = 0,
                TargetCost = 0.001,
                UpdateFrequency = 100,
                //CostFunction = CGpRegressionCostFunction.Rms,
                MutationType = CGpMutation.ProbabilisticActiveNodes
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
