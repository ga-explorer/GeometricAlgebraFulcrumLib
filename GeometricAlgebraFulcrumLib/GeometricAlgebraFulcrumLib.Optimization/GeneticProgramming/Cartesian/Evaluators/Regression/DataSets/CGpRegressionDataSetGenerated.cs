namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators.Regression.DataSets;

public class CGpRegressionDataSetGenerated :
    CGpRegressionDataSet
{
    public override int SampleCount { get; }

    public Func<int, IReadOnlyList<double>> InputFunc { get; }

    public Func<int, IReadOnlyList<double>> OutputFunc { get; }


    public CGpRegressionDataSetGenerated(int inputSize, int outputSize, int sampleCount, Func<int, IReadOnlyList<double>> inputFunc, Func<int, IReadOnlyList<double>> outputFunc)
        : base(inputSize, outputSize)
    {
        if (sampleCount < 1)
            throw new ArgumentException(nameof(sampleCount));

        SampleCount = sampleCount;
        InputFunc = inputFunc;
        OutputFunc = outputFunc;
    }


    public override CGpRegressionDataSetSample GetSample(int sampleIndex)
    {
        if (sampleIndex < 0 || sampleIndex > SampleCount)
            throw new IndexOutOfRangeException();

        var input = InputFunc(sampleIndex);

        if (input.Count != InputSize)
            throw new InvalidOperationException();

        var output = OutputFunc(sampleIndex);

        if (output.Count != OutputSize)
            throw new InvalidOperationException();

        return new CGpRegressionDataSetSample(input, output);
    }
}