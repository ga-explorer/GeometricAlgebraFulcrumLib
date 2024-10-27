namespace GeometricAlgebraFulcrumLib.Optimization.SVM.LibSVM;

public sealed record SvmDataSetRecord
{
    public IReadOnlyList<double> InputFeatures { get; }

    public double OutputValue { get; }


    internal SvmDataSetRecord(IReadOnlyList<double> inputFeatures, double outputValue)
    {
        InputFeatures = inputFeatures;
        OutputValue = outputValue;
    }
}