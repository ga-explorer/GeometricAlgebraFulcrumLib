namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.EllipseFitting;

public sealed record EvdResult
{
    public double[] EigenValues { get; }

    public double[,] EigenVectors { get; }

    public EvdResult(double[] eigenValues, double[,] eigenVectors)
    {
        EigenValues = eigenValues;
        EigenVectors = eigenVectors;
    }
}