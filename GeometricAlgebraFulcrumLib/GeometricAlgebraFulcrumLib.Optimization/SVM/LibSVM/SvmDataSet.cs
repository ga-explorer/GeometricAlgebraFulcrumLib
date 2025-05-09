using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Lists;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Optimization.SVM.LibSVM
{
    public class SvmDataSet : 
        IReadOnlyList<SvmDataSetRecord>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static SvmDataSet LoadFromFile(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename) || !File.Exists(filename))
                throw new FileNotFoundException();

            var provider = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            var problem = new SvmDataSet();
            using var sr = new StreamReader(filename);

            while (true)
            {
                var line = sr.ReadLine();
                if (line == null)
                    break;

                var list = line.Trim().Split(' ');

                var y = Convert.ToDouble(list[0].Trim(), provider);

                var features = new List<SvmFeature>();
                for (var i = 1; i < list.Length; i++)
                {
                    var temp = list[i].Split(':');
                    var feature = new SvmFeature(
                        index : Convert.ToInt32(temp[0].Trim()) - 1,
                        value : Convert.ToDouble(temp[1].Trim(), provider)
                    );
                    features.Add(feature);
                }

                problem.Add(features.ToImmutableArray(), y);
            }

            return problem;
        }

        internal static SvmDataSet CreateFromStruct(LibSvmInterop.DataSetStruct x)
        {
            var yArray = new double[x.l];
            Marshal.Copy(x.y, yArray, 0, yArray.Length);

            var xArray = new List<IReadOnlyList<SvmFeature>>(x.l);
            var iPtrX = x.x;
            for (var i = 0; i < x.l; i++)
            {
                var featureStructArrayPtr = (IntPtr)Marshal.PtrToStructure(iPtrX, typeof(IntPtr))!;
                var features = SvmFeature.CreateFromStructArrayPtr(featureStructArrayPtr);
                xArray.Add(features);
                iPtrX = IntPtr.Add(iPtrX, Marshal.SizeOf(typeof(IntPtr)));
            }

            var y = new SvmDataSet();
            for (var i = 0; i < x.l; i++)
            {
                y.Add(xArray[i], yArray[i]);
            }

            return y;
        }

        internal static SvmDataSet CreateFromStructPtr(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new NullReferenceException();

            var x = (LibSvmInterop.DataSetStruct)Marshal.PtrToStructure(ptr, typeof(LibSvmInterop.DataSetStruct))!;
            return CreateFromStruct(x);
        }

        
        public int Count 
            => OutputValueList.Count;

        public int SampleCount 
            => OutputValueList.Count;

        private int _featureCount = -1;
        public int FeatureCount
        {
            get
            {
                if (_featureCount < 0)
                    _featureCount = GetInputFeatureCountFromData();

                return _featureCount;
            }
        }

        public List<double> OutputValueList { get; } = [];

        public List<IReadOnlyList<SvmFeature>> InputFeatureList { get; } = [];

        public SvmDataSetRecord this[int index] 
            => new SvmDataSetRecord(
                InputFeatureList[index].ToFeatureValueArray(FeatureCount),
                OutputValueList[index]
            );


        public SvmDataSet()
        {

        }

        public SvmDataSet(int featureCount)
        {
            if (featureCount < 2)
                throw new ArgumentOutOfRangeException(nameof(featureCount));

            _featureCount = featureCount;
        }


        private int GetInputFeatureCountFromData()
        {
            return InputFeatureList.SelectMany(fl =>
                fl.Select(f => f.Index)
            ).Max() + 1;
        }
        
        public int GetClassCount()
        {
            return OutputValueList.Distinct().Count();
        }

        public IReadOnlyList<double> GetClassLabels()
        {
            return OutputValueList.ToImmutableSortedSet();
        }

        public void Add(IReadOnlyList<double> inputFeatures, double outputValue)
        {
            if (inputFeatures.Count == 0) return;

            var features = 
                inputFeatures.Select(
                    (value, index) => new SvmFeature(index, value)
                ).ToImmutableArray();

            InputFeatureList.Add(features);
            OutputValueList.Add(outputValue);
        }

        public void Add(IReadOnlyList<SvmFeature> inputFeatures, double outputValue)
        {
            if (inputFeatures.Count == 0) return;

            var features = 
                inputFeatures.OrderBy(a => a.Index).ToImmutableArray();

            InputFeatureList.Add(features);
            OutputValueList.Add(outputValue);
        }
        
        internal void AddOrdered(IReadOnlyList<SvmFeature> inputFeatures, double outputValue)
        {
            if (inputFeatures.Count == 0) return;

            InputFeatureList.Add(inputFeatures);
            OutputValueList.Add(outputValue);
        }

        public void Insert(int index, IReadOnlyList<double> inputFeatures, double outputValue)
        {
            if (inputFeatures.Count <= 0) return;
            
            var features = 
                inputFeatures.Select(
                    (value, index1) => new SvmFeature(index1, value)
                ).ToImmutableArray();

            InputFeatureList.Insert(index, features);
            OutputValueList.Insert(index, outputValue);
        }

        public void Insert(int index, IReadOnlyList<SvmFeature> inputFeatures, double outputValue)
        {
            if (inputFeatures.Count <= 0) return;

            var features = 
                inputFeatures.OrderBy(a => a.Index).ToImmutableArray();

            InputFeatureList.Insert(index, features);
            OutputValueList.Insert(index, outputValue);
        }
        
        internal void InsertOrdered(int index, IReadOnlyList<SvmFeature> inputFeatures, double outputValue)
        {
            if (inputFeatures.Count <= 0) return;

            InputFeatureList.Insert(index, inputFeatures);
            OutputValueList.Insert(index, outputValue);
        }

        public void RemoveAt(int index)
        {
            if (index >= SampleCount) return;

            OutputValueList.RemoveAt(index);
            InputFeatureList.RemoveAt(index);
        }

        public SvmDataSet GetCopy()
        {
            var dataSet = new SvmDataSet();

            for (var i = 0; i < SampleCount; i++)
            {
                dataSet.AddOrdered(
                    InputFeatureList[i].ToImmutableArray(), 
                    OutputValueList[i]
                );
            }

            return dataSet;
        }

        public SvmDataSet GetCopyNoDuplicates()
        {
            var dataSet = new SvmDataSet();

            for (var i = 0; i < SampleCount; i++)
            {
                var same = false;
                for (var j = i + 1; j < SampleCount; j++)
                {
                    same |= SvmUtils.IsEqual(InputFeatureList[i], OutputValueList[i], InputFeatureList[j], OutputValueList[j]);

                    if (same) break;
                }

                if (!same) 
                    dataSet.AddOrdered(InputFeatureList[i], OutputValueList[i]);
            }

            return dataSet;
        }

        public SvmDataSet GetCopyNormalized(SvmNormType type)
        {
            var dataSet = new SvmDataSet();
            
            for (var i = 0; i < SampleCount; i++)
            {
                dataSet.AddOrdered(
                    InputFeatureList[i].Normalize(type), 
                    OutputValueList[i]
                );
            }

            return dataSet;
        }

        public Pair<IReadOnlyList<double>> GetUnitCubeNormalizationFactors()
        {
            var (minCorner, maxCorner) = GetRange();

            var n = minCorner.Count;
            var translationValues = new double[n];
            var scalingFactors = new double[n];

            Debug.Assert(n == FeatureCount);

            for (var i = 0; i < n; i++)
            {
                var minValue = minCorner[i];
                var maxValue = maxCorner[i];
                var featureRangeSize = maxValue - minValue;

                translationValues[i] = -(maxValue + minValue) * 0.5d;

                scalingFactors[i] = featureRangeSize < 1e-12 
                    ? 1 
                    : 2 / featureRangeSize;
            }

            return new Pair<IReadOnlyList<double>>(
                translationValues,
                scalingFactors
            );
        }
        
        public SvmDataSet GetCopyUnitCube(IReadOnlyList<double> translationValues, IReadOnlyList<double> scalingFactors)
        {
            var dataSet = new SvmDataSet();

            for (var i = 0; i < SampleCount; i++)
            {
                dataSet.AddOrdered(
                    InputFeatureList[i].Normalize(translationValues, scalingFactors), 
                    OutputValueList[i]
                );
            }

            return dataSet;
        }

        public SvmDataSet GetCopyUnitCube()
        {
            var (translationValues, scalingFactors) = 
                GetUnitCubeNormalizationFactors();

            return GetCopyUnitCube(translationValues, scalingFactors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<double, int> GetLabelsCount()
        {
            var dic = new Dictionary<double, int>();
            for (var i = 0; i < SampleCount; i++)
            {
                if (!dic.ContainsKey(OutputValueList[i]))
                {
                    dic.Add(OutputValueList[i], 1);
                }
                else
                {
                    dic[OutputValueList[i]]++;
                }
            }
            return dic;
        }
        
        public ImmutableSortedSet<int> GetFeatureIndexSet()
        {
            return InputFeatureList.SelectMany(
                featureList => featureList.Select(f => f.Index)
            ).ToImmutableSortedSet();
        }

        public Pair<int> GetFeatureIndexRange()
        {
            var minIndex = int.MaxValue;
            var maxIndex = int.MinValue;

            var indexList = 
                InputFeatureList.SelectMany(
                    featureList => featureList.Select(f => f.Index)
                );

            foreach (var index in indexList)
            {
                if (minIndex > index) minIndex = index;
                if (maxIndex < index) maxIndex = index;
            }

            return new Pair<int>(minIndex, maxIndex);
        }

        public Pair<IReadOnlyList<double>> GetRange()
        {
            var minFeatureList = new SparseList<double>(0);
            var maxFeatureList = new SparseList<double>(0);

            foreach (var sampleInput in InputFeatureList)
            {
                foreach (var (index, value) in sampleInput)
                {
                    var minValue = minFeatureList[index];
                    var maxValue = maxFeatureList[index];

                    if (minValue > value) minFeatureList[index] = value;
                    if (maxValue < value) maxFeatureList[index] = value;
                }
            }

            return new Pair<IReadOnlyList<double>>(
                minFeatureList,
                maxFeatureList
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool SaveToFile(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename) || SampleCount == 0)
            {
                return false;
            }

            var provider = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            using var sw = new StreamWriter(filename);

            for (var i = 0; i < SampleCount; i++)
            {
                sw.Write(OutputValueList[i]);

                if (InputFeatureList[i].Count > 0)
                {
                    sw.Write(" ");

                    for (var j = 0; j < InputFeatureList[i].Count; j++)
                    {
                        sw.Write(InputFeatureList[i][j].IndexPlus1);
                        sw.Write(":");
                        sw.Write(InputFeatureList[i][j].Value.ToString(provider));

                        if (j < InputFeatureList[i].Count - 1)
                        {
                            sw.Write(" ");
                        }
                    }
                }

                sw.Write("\n");
            }

            return true;
        }

        public IEnumerator<SvmDataSetRecord> GetEnumerator()
        {
            var n = FeatureCount;

            for (var i = 0; i < SampleCount; i++)
            {
                yield return new SvmDataSetRecord(
                    InputFeatureList[i].ToFeatureValueArray(n),
                    OutputValueList[i]
                );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
    }
}
