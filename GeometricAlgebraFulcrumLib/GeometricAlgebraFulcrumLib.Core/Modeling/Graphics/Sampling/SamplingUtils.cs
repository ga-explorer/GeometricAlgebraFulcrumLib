namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Sampling;

public static class SamplingUtils
{
    //TODO: Get classes from PhD Ray Tracer

    public static IEnumerable<double> GetUniformSamplesWithStep(double minValue, double maxValue, double step)
    {
        var count = 1 + (int)Math.Floor((maxValue - minValue) / step);

        var iMax = count - 1;
        var iMaxInv = 1d / iMax;
        minValue *= iMaxInv;
        maxValue *= iMaxInv;

        return Enumerable
            .Range(0, count)
            .Select(i => (iMax - i) * minValue + i * maxValue);
    }

    public static IEnumerable<double> GetUniformSamplesWithCount(double minValue, double maxValue, int count)
    {
        var iMax = count - 1;
        var iMaxInv = 1d / iMax;
        minValue *= iMaxInv;
        maxValue *= iMaxInv;

        return Enumerable
            .Range(0, count)
            .Select(i => (iMax - i) * minValue + i * maxValue);
    }
}