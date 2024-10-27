namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Classification.DataSet;

public class CGpClassificationDataSetGenerated :
    CGpClassificationDataSet
{
    public override int SampleCount { get; }

    public Func<int, IReadOnlyList<double>> InputFunc { get; }

    public Func<int, int> ClassIndexFunc { get; }


    public CGpClassificationDataSetGenerated(int inputSize, int classCount, int sampleCount, Func<int, IReadOnlyList<double>> inputFunc, Func<int, int> classIndexFunc)
        : base(inputSize, classCount)
    {
        if (sampleCount < 1)
            throw new ArgumentException(nameof(sampleCount));

        SampleCount = sampleCount;
        InputFunc = inputFunc;
        ClassIndexFunc = classIndexFunc;
    }


    public override CGpClassificationDataSetSample GetSample(int sampleIndex)
    {
        if (sampleIndex < 0 || sampleIndex > SampleCount)
            throw new IndexOutOfRangeException();

        var input = InputFunc(sampleIndex);

        if (input.Count != InputSize)
            throw new InvalidOperationException();

        var output = ClassIndexFunc(sampleIndex);

        if (output < 0 || output >= ClassCount)
            throw new InvalidOperationException();

        return new CGpClassificationDataSetSample(input, output);
    }
}