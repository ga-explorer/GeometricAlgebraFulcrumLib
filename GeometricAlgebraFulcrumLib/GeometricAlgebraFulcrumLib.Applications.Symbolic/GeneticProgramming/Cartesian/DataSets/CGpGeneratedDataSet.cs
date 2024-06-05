namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.DataSets;

public class CGpGeneratedDataSet :
    CGpDataSet
{
    public override int SampleCount { get; }

    public Func<int, IReadOnlyList<double>> InputFunc { get; }

    public Func<int, IReadOnlyList<double>> OutputFunc { get; }


    public CGpGeneratedDataSet(int inputSize, int outputSize, int sampleCount, Func<int, IReadOnlyList<double>> inputFunc, Func<int, IReadOnlyList<double>> outputFunc)
        : base(inputSize, outputSize)
    {
        if (sampleCount < 1)
            throw new ArgumentException(nameof(sampleCount));

        SampleCount = sampleCount;
        InputFunc = inputFunc;
        OutputFunc = outputFunc;
    }

    
    public override CGpDataSetSample GetSample(int sampleIndex)
    {
        if (sampleIndex < 0 || sampleIndex > SampleCount)
            throw new IndexOutOfRangeException();

        var input = InputFunc(sampleIndex);

        if (input.Count != InputSize)
            throw new InvalidOperationException();

        var output = OutputFunc(sampleIndex);

        if (output.Count != OutputSize)
            throw new InvalidOperationException();

        return new CGpDataSetSample(input, output);
    }
}