using System.Collections.Immutable;
using System.Runtime.InteropServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using OpenQA.Selenium;

namespace GeometricAlgebraFulcrumLib.Optimization.SVM.LibSVM
{
    public static class SvmUtils
    {
        public static string LibSvmVersion 
            => LibSvmInterop.Version;

        public delegate void SvmPrintFunction(string output);

        public static bool IsEqual(IReadOnlyList<SvmFeature> featureList1, double y1, IReadOnlyList<SvmFeature> featureList2, double y2)
        {
            if (!y1.Equals(y2) || featureList1.Count != featureList2.Count) return false;
            
            for (var i = 0; i < featureList1.Count; i++)
                if (!featureList1[i].Equals(featureList2[i])) return false;
            
            return true;
        }
        
        public static IReadOnlyList<double> ToFeatureValueArray(this IReadOnlyList<SvmFeature> featureList, int featureCount)
        {
            var valueArray = new double[featureCount];

            foreach (var feature in featureList)
                valueArray[feature.Index] = feature.Value;

            return valueArray;
        }

        public static IReadOnlyList<SvmFeature> Normalize(this IReadOnlyList<SvmFeature> featureList, SvmNormType type)
        {
            var normL = (double)(int)type;
            var norm = featureList.Sum(a => Math.Pow(Math.Abs(a.Value), normL));
            
            norm = Math.Pow(norm, 1 / normL);
            
            var y = featureList.Select(
                f => new SvmFeature(f.Index, f.Value / norm)
            ).ToArray();
            
            return y;
        }
        
        public static IReadOnlyList<SvmFeature> Normalize(this IReadOnlyList<SvmFeature> featureList, IReadOnlyList<double> translationValues, IReadOnlyList<double> scalingFactors)
        {
            return featureList.Select(
                f =>
                {
                    var i = f.Index;
                    var v = (f.Value + translationValues[i]) * scalingFactors[i];

                    return new SvmFeature(i, v);
                }).ToImmutableArray();
        }

        /// <summary>
        /// This function constructs and returns an SVM model according to the given training data and parameters.
        /// </summary>
        /// <param name="dataSet">Training data.</param>
        /// <param name="parameters">Parameter set.</param>
        /// <returns>SVM model according to the given training data and parameters.</returns>
        public static SvmModel Train(this SvmDataSet dataSet, SvmParameters parameters)
        {
            if (dataSet == null)
                throw new ArgumentNullException(nameof(dataSet));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var dataSetStructPtr = dataSet.AllocateDataSetStruct();
            var parametersStructPtr = parameters.AllocateParametersStruct();

            var modelStructPtr = LibSvmInterop.svm_train(dataSetStructPtr, parametersStructPtr);
            var model = SvmModel.CreateFromModelStructPtr(modelStructPtr);

            LibSvmInterop.FreeDataSetStruct(dataSetStructPtr);
            LibSvmInterop.FreeParametersStruct(parametersStructPtr);
            LibSvmInterop.svm_free_model_content(modelStructPtr);

            return model;
        }

        public static void CrossValidation(this SvmDataSet dataSet, SvmParameters parameters, int nFolds, out double[] target)
        {
            if (dataSet == null)
                throw new ArgumentNullException(nameof(dataSet));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (nFolds < 2)
                throw new ArgumentOutOfRangeException(nameof(nFolds));

            var ptrTarget = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)) * dataSet.SampleCount);
            var dataSetStructPtr = dataSet.AllocateDataSetStruct();
            var parametersStructPtr = parameters.AllocateParametersStruct();

            LibSvmInterop.svm_cross_validation(dataSetStructPtr, parametersStructPtr, nFolds, ptrTarget);

            target = new double[dataSet.SampleCount];
            Marshal.Copy(ptrTarget, target, 0, target.Length);

            LibSvmInterop.FreeDataSetStruct(dataSetStructPtr);
            LibSvmInterop.FreeParametersStruct(parametersStructPtr);
            Marshal.FreeHGlobal(ptrTarget);
        }

        public static bool SaveModel(this SvmModel model, string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                return false;
            }

            var modelStructPtr = model.AllocateModelStruct();
            var ptrFilename = Marshal.StringToHGlobalAnsi(filename);

            var success = LibSvmInterop.svm_save_model(ptrFilename, modelStructPtr) == 0;

            Marshal.FreeHGlobal(ptrFilename);

            return success;
        }
        
        public static SvmModel LoadModel(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename) || !File.Exists(filename))
                throw new FileNotFoundException();

            var ptrFilename = Marshal.StringToHGlobalAnsi(filename);

            var modelStructPtr = LibSvmInterop.svm_load_model(ptrFilename);

            Marshal.FreeHGlobal(ptrFilename);

            if (modelStructPtr == IntPtr.Zero)
                throw new NotFoundException();

            var model = SvmModel.CreateFromModelStructPtr(modelStructPtr);

            // There is a little memory leakage here !!!
            LibSvmInterop.svm_free_model_content(modelStructPtr);

            return model;
        }
        

        public static double PredictValues(this SvmModel model, IReadOnlyList<SvmFeature> featureList, out double[] values)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (featureList == null)
                throw new ArgumentNullException(nameof(featureList));

            var modelStructPtr = model.AllocateModelStruct();
            
            var result = PredictValues(modelStructPtr, featureList, out values);
            
            LibSvmInterop.FreeModelStruct(modelStructPtr);
            
            return result;
        }
        
        public static double PredictValues(IntPtr modelStructPtr, IReadOnlyList<SvmFeature> featureList, out double[] values)
        {
            if (modelStructPtr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(modelStructPtr));

            if (featureList == null)
                throw new ArgumentNullException(nameof(featureList));

            var classCount = LibSvmInterop.svm_get_nr_class(modelStructPtr);
            var size = (int)(classCount * (classCount - 1) * 0.5);
            var ptrValues = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)) * size);

            var featureStructArrayPtr = 
                featureList
                    .ToArray()
                    .AllocateFeatureStructArray();

            var result = LibSvmInterop.svm_predict_values(modelStructPtr, featureStructArrayPtr, ptrValues);

            values = new double[size];
            Marshal.Copy(ptrValues, values, 0, values.Length);

            LibSvmInterop.FreeFeatureStructArray(featureStructArrayPtr);
            Marshal.FreeHGlobal(ptrValues);

            return result;
        }
        
        public static double PredictValues(this IReadOnlyList<SvmFeature> featureList, IntPtr modelStructPtr, out double[] values)
        {
            return PredictValues(modelStructPtr, featureList, out values);
        }
        
        public static double[] PredictValues(this SvmDataSet dataSet, SvmModel model, out List<double[]> valuesList)
        {
            var modelStructPtr = model.AllocateModelStruct();

            var temp = new List<double[]>();
            var target = dataSet.InputFeatureList.Select(x =>
            {
                var y = x.PredictProbability(modelStructPtr, out var estimations);
                temp.Add(estimations);
                return y;
            }).ToArray();

            LibSvmInterop.FreeModelStruct(modelStructPtr);

            valuesList = temp;
            return target;
        }

        
        /// <summary>
        /// This function does classification or regression on a test vector x given a model.
        /// </summary>
        /// <param name="model">SVM model.</param>
        /// <param name="featureArray">Test vectors. Each row in the array is one data record</param>
        /// <returns>For a classification model, the predicted class for x is returned.
        /// For a regression model, the function value of x calculated using the model is returned. 
        /// For a one-class model, +1 or -1 is returned.</returns>
        public static IReadOnlyList<double> Predict(this SvmModel model, double[,] featureArray)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var modelStructPtr = model.AllocateModelStruct();

            var sampleCount = featureArray.GetLength(0);
            var resultArray = new double[sampleCount];

            for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
            {
                var featureStructArrayPtr = featureArray.AllocateFeatureStructArray(sampleIndex);
                
                resultArray[sampleIndex] = LibSvmInterop.svm_predict(modelStructPtr, featureStructArrayPtr);

                LibSvmInterop.FreeFeatureStructArray(featureStructArrayPtr);
            }
            
            LibSvmInterop.FreeModelStruct(modelStructPtr);
            
            return resultArray;
        }

        /// <summary>
        /// This function does classification or regression on a test vector x given a model.
        /// </summary>
        /// <param name="model">SVM model.</param>
        /// <param name="featureList">Test vector.</param>
        /// <returns>For a classification model, the predicted class for x is returned.
        /// For a regression model, the function value of x calculated using the model is returned. 
        /// For a one-class model, +1 or -1 is returned.</returns>
        public static double Predict(this SvmModel model, IReadOnlyList<SvmFeature> featureList)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (featureList == null)
                throw new ArgumentNullException(nameof(featureList));

            var modelStructPtr = model.AllocateModelStruct();
            var result = Predict(modelStructPtr, featureList);
            LibSvmInterop.FreeModelStruct(modelStructPtr);
            return result;
        }
        
        public static double Predict(IntPtr modelStructPtr, IReadOnlyList<SvmFeature> featureList)
        {
            if (modelStructPtr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(modelStructPtr));

            if (featureList == null)
                throw new ArgumentNullException(nameof(featureList));

            var featureStructArrayPtr = 
                featureList
                    .ToArray()
                    .AllocateFeatureStructArray();

            var result = LibSvmInterop.svm_predict(modelStructPtr, featureStructArrayPtr);

            LibSvmInterop.FreeFeatureStructArray(featureStructArrayPtr);

            return result;
        }
        
        public static double Predict(this IReadOnlyList<SvmFeature> featureList, IntPtr modelStructPtr)
        {
            return Predict(modelStructPtr, featureList);
        }
        
        public static double[] Predict(this SvmDataSet dataSet, SvmModel model)
        {
            var modelStructPtr = model.AllocateModelStruct();
            var target = dataSet.InputFeatureList.Select(x => x.Predict(modelStructPtr)).ToArray();
            LibSvmInterop.FreeModelStruct(modelStructPtr);
            return target;
        }


        public static double PredictProbability(this SvmModel model, IReadOnlyList<SvmFeature> featureList, out double[] estimations)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (featureList == null)
                throw new ArgumentNullException(nameof(featureList));

            var modelStructPtr = model.AllocateModelStruct();

            var result = PredictProbability(modelStructPtr, featureList, out estimations);
            
            LibSvmInterop.FreeModelStruct(modelStructPtr);
            
            return result;
        }
        
        public static double PredictProbability(IntPtr modelStructPtr, IReadOnlyList<SvmFeature> featureList, out double[] estimations)
        {
            if (modelStructPtr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(modelStructPtr));

            if (featureList == null)
                throw new ArgumentNullException(nameof(featureList));

            var isProbabilityModel = 
                LibSvmInterop.svm_check_probability_model(modelStructPtr);

            if (!isProbabilityModel)
            {
                LibSvmInterop.FreeModelStruct(modelStructPtr);
                estimations = [];
                return -1;
            }

            var classCount = LibSvmInterop.svm_get_nr_class(modelStructPtr);

            var ptrEstimations = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)) * classCount);
            
            var featureStructArrayPtr = 
                featureList
                    .ToArray()
                    .AllocateFeatureStructArray();

            var result = LibSvmInterop.svm_predict_probability(modelStructPtr, featureStructArrayPtr, ptrEstimations);

            estimations = new double[classCount];
            Marshal.Copy(ptrEstimations, estimations, 0, estimations.Length);

            LibSvmInterop.FreeFeatureStructArray(featureStructArrayPtr);
            Marshal.FreeHGlobal(ptrEstimations);

            return result;
        }
        
        public static double PredictProbability(this IReadOnlyList<SvmFeature> featureList, IntPtr modelStructPtr, out double[] estimations)
        {
            return PredictProbability(modelStructPtr, featureList, out estimations);
        }
        
        public static double[] PredictProbability(this SvmDataSet dataSet, SvmModel model, out List<double[]> estimationsList)
        {
            var modelStructPtr = model.AllocateModelStruct();

            var temp = new List<double[]>();
            var target = dataSet.InputFeatureList.Select(x =>
            {
                var y = x.PredictProbability(modelStructPtr, out var estimations);
                temp.Add(estimations);
                return y;
            }).ToArray();

            LibSvmInterop.FreeModelStruct(modelStructPtr);

            estimationsList = temp;
            return target;
        }

        public static string CheckParameter(this SvmDataSet dataSet, SvmParameters parameters)
        {
            if (dataSet == null)
            {
                throw new ArgumentNullException(nameof(dataSet));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var dataSetStructPtr = dataSet.AllocateDataSetStruct();
            var parametersStructPtr = parameters.AllocateParametersStruct();

            var ptrOutput = LibSvmInterop.svm_check_parameter(dataSetStructPtr, parametersStructPtr);

            LibSvmInterop.FreeDataSetStruct(dataSetStructPtr);
            LibSvmInterop.FreeParametersStruct(parametersStructPtr);

            var output = Marshal.PtrToStringAnsi(ptrOutput) ?? string.Empty;
            Marshal.FreeHGlobal(ptrOutput);

            return output;
        }

        public static bool CheckProbabilityModel(this SvmModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var modelStructPtr = model.AllocateModelStruct();
            var success = LibSvmInterop.svm_check_probability_model(modelStructPtr);
            LibSvmInterop.FreeModelStruct(modelStructPtr);
            return success;
        }

        public static void SetPrintStringFunction(SvmPrintFunction function)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));

            var ptrFunction = Marshal.GetFunctionPointerForDelegate(function);
            LibSvmInterop.svm_set_print_string_function(ptrFunction);
        }

        public static double EvaluateClassificationProblem(this SvmDataSet testSet, double[] target)
        {
            if (testSet.SampleCount != target.Length)
                return -1;

            var totalCorrect = 0;
            for (var i = 0; i < testSet.SampleCount; i++)
            {
                var y = (int)testSet.OutputValueList[i];
                var v = (int)target[i];

                if (y == v) ++totalCorrect;
            }

            return totalCorrect / (double)testSet.SampleCount;
        }

        public static double GetAverageAccuracyPerClass(this SvmDataSet testSet, double[] target)
        {
            return testSet
                .EvaluateClassificationProblemPerClass(target)
                .Select(p => p.Value)
                .Average();
        }

        public static IEnumerable<KeyValuePair<double, double>> EvaluateClassificationProblemPerClass(this SvmDataSet testSet, double[] target)
        {
            if (testSet.SampleCount != target.Length)
                throw new InvalidOperationException();

            var classLabelSet = testSet.GetClassLabels();
            var classCount = classLabelSet.Count;
            var sampleCount = new int[classCount];
            var correctCount = new int[classCount];

            for (var i = 0; i < testSet.SampleCount; i++)
            {
                var actualClassLabel = (int)testSet.OutputValueList[i];
                var predictedClassLabel = (int)target[i];
                var classIndex = classLabelSet.SelectFirstIndexWhere(v => v == actualClassLabel);

                sampleCount[classIndex]++;

                if (actualClassLabel == predictedClassLabel) 
                    correctCount[classIndex]++;
            }

            for (var classIndex = 0; classIndex < classCount; classIndex++)
            {
                var classLabel = classLabelSet[classIndex];
                var classAccuracy = correctCount[classIndex] / (double)sampleCount[classIndex];

                yield return new KeyValuePair<double, double>(
                    classLabel,
                    classAccuracy
                );
            }
        }

        public static double EvaluateClassificationProblem(this SvmDataSet testSet, double[] target, int[] labels, out int[,] confusionMatrix)
        {
            if (testSet.SampleCount != target.Length)
            {
                confusionMatrix = new int[,]{};
                return -1;
            }

            var indexes = new Dictionary<int, int>();
            for (var i = 0; i < labels.Length; i++)
            {
                indexes.Add(labels[i], i);
            }
            
            confusionMatrix = new int[labels.Length, labels.Length];

            var totalCorrect = 0;
            for (var i = 0; i < testSet.SampleCount; i++)
            {
                var y = (int)testSet.OutputValueList[i];
                var v = (int)target[i];

                confusionMatrix[indexes[y], indexes[v]]++;

                if (y == v)
                {
                    ++totalCorrect;
                }
            }

            return totalCorrect / (double)testSet.SampleCount;
        }
        
        public static double EvaluateRegressionProblem(this SvmDataSet testSet, double[] target, out double correlationCoef)
        {
            var totalError = 0d;
            var sumV = 0d;
            var sumy = 0d;
            var sumVv = 0d;
            var sumYy = 0d;
            var sumVy = 0d;

            for (var i = 0; i < testSet.SampleCount; i++)
            {
                var y = testSet.OutputValueList[i];
                var v = target[i];
                totalError += (v - y) * (v - y);
                sumV += v;
                sumy += y;
                sumVv += v * v;
                sumYy += y * y;
                sumVy += v * y;
            }

            var meanSquaredError = totalError / testSet.SampleCount;
            correlationCoef =
                ((testSet.SampleCount * sumVy - sumV * sumy) * (testSet.SampleCount * sumVy - sumV * sumy)) /
                ((testSet.SampleCount * sumVv - sumV * sumV) * (testSet.SampleCount * sumYy - sumy * sumy));

            return meanSquaredError;
        }
    }
}
