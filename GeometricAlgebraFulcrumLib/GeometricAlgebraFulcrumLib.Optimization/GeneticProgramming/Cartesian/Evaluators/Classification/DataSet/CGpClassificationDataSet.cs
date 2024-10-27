using System.Collections;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Classification.DataSet;

public abstract class CGpClassificationDataSet :
    IReadOnlyList<CGpClassificationDataSetSample>
{
    public int InputSize { get; }

    public int ClassCount { get; }

    public abstract int SampleCount { get; }

    public int Count
        => SampleCount;

    public CGpClassificationDataSetSample this[int index]
        => GetSample(index);


    protected CGpClassificationDataSet(int inputSize, int classCount)
    {
        if (inputSize < 1)
            throw new ArgumentException(nameof(inputSize));

        if (classCount < 1)
            throw new ArgumentException(nameof(classCount));

        InputSize = inputSize;
        ClassCount = classCount;
    }


    public abstract CGpClassificationDataSetSample GetSample(int sampleIndex);

    public virtual IReadOnlyList<double> GetSampleInput(int sampleIndex)
    {
        return GetSample(sampleIndex).Input;
    }

    public virtual int GetSampleClassIndex(int sampleIndex)
    {
        return GetSample(sampleIndex).ClassIndex;
    }

    public virtual double GetSampleInputItem(int sampleIndex, int itemIndex)
    {
        return GetSampleInput(sampleIndex)[itemIndex];
    }

    public virtual IReadOnlyList<double> GetInputItems(int itemIndex)
    {
        return this.Select(s => s.Input[itemIndex]).ToImmutableArray();
    }

    /// <summary>
    /// Each row contains the input items of a sample
    /// </summary>
    /// <returns></returns>
    public virtual double[,] GetInputsArray()
    {
        var inputsArray = new double[SampleCount, InputSize];

        for (var sampleIndex = 0; sampleIndex < Count; sampleIndex++)
        {
            var sample = GetSample(sampleIndex);

            for (var itemIndex = 0; itemIndex < sample.InputSize; itemIndex++)
                inputsArray[sampleIndex, itemIndex] = sample.Input[itemIndex];
        }

        return inputsArray;
    }

    /// <summary>
    /// Each row contains the output items of a sample
    /// </summary>
    /// <returns></returns>
    public virtual int[] GetClassIndexArray()
    {
        var classIndexArray = new int[SampleCount];

        for (var sampleIndex = 0; sampleIndex < Count; sampleIndex++)
        {
            var sample = GetSample(sampleIndex);

            for (var itemIndex = 0; itemIndex < sample.InputSize; itemIndex++)
                classIndexArray[sampleIndex] = sample.ClassIndex;
        }

        return classIndexArray;
    }

    public virtual IEnumerator<CGpClassificationDataSetSample> GetEnumerator()
    {
        return Count.GetRange(GetSample).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}