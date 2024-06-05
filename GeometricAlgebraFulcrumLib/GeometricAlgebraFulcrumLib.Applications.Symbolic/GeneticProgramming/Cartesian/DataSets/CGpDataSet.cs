using System.Collections;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.DataSets;

public abstract class CGpDataSet :
    IReadOnlyList<CGpDataSetSample>
{
    public int InputSize { get; }

    public int OutputSize { get; }
    
    public abstract int SampleCount { get; }

    public int Count 
        => SampleCount;

    public CGpDataSetSample this[int index] 
        => GetSample(index);


    protected CGpDataSet(int inputSize, int outputSize)
    {
        if (inputSize < 1)
            throw new ArgumentException(nameof(inputSize));

        if (outputSize < 1)
            throw new ArgumentException(nameof(outputSize));

        InputSize = inputSize;
        OutputSize = outputSize;
    }


    public abstract CGpDataSetSample GetSample(int sampleIndex);

    public virtual IReadOnlyList<double> GetSampleInput(int sampleIndex)
    {
        return GetSample(sampleIndex).Input;
    }

    public virtual IReadOnlyList<double> GetSampleOutput(int sampleIndex)
    {
        return GetSample(sampleIndex).Output;
    }

    public virtual double GetSampleInputItem(int sampleIndex, int itemIndex)
    {
        return GetSampleInput(sampleIndex)[itemIndex];
    }

    public virtual double GetSampleOutputItem(int sampleIndex, int itemIndex)
    {
        return GetSampleOutput(sampleIndex)[itemIndex];
    }

    public virtual IReadOnlyList<double> GetInputItems(int itemIndex)
    {
        return this.Select(s => s.Input[itemIndex]).ToImmutableArray();
    }

    public virtual IReadOnlyList<double> GetOutputItems(int itemIndex)
    {
        return this.Select(s => s.Output[itemIndex]).ToImmutableArray();
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
    public virtual double[,] GetOutputsArray()
    {
        var inputsArray = new double[SampleCount, OutputSize];

        for (var sampleIndex = 0; sampleIndex < Count; sampleIndex++)
        {
            var sample = GetSample(sampleIndex);

            for (var itemIndex = 0; itemIndex < sample.InputSize; itemIndex++)
                inputsArray[sampleIndex, itemIndex] = sample.Output[itemIndex];
        }

        return inputsArray;
    }

    public virtual IEnumerator<CGpDataSetSample> GetEnumerator()
    {
        return Count.GetRange(GetSample).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}