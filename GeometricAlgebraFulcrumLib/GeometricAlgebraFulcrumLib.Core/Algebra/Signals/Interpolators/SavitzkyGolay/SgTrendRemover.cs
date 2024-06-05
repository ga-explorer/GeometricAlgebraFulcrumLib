namespace GeometricAlgebraFulcrumLib.Core.Algebra.Signals.Interpolators.SavitzkyGolay;

/**
 * De-trends data by setting straight line between the first and the last point
 * and subtracting it from data. Having applied filters to data you should
 * reverse detrending by using {@link TrendRemover#retrend(double[], double[])}
 *
 * @author Marcin Rzeźnicki
 *
 */
public class SgTrendRemover : ISgPreprocessor
{


    public void Apply(double[] data)
    {
        // de-trend data so to avoid boundary distortion
        // we will achieve this by setting straight line from end to beginning
        // and subtracting it from the trend
        var n = data.Length;
        if (n <= 2)
            return;
        var y0 = data[0];
        var slope = (data[n - 1] - y0) / (n - 1);
        for (var x = 0; x < n; x++)
        {
            data[x] -= slope * x + y0;
        }
    }

    /**
         * Reverses the effect of {@link #apply(double[])} by modifying {@code
         * newData}
         *
         * @param newData
         *            processed data
         * @param data
         *            original data
         */
    public void Retrend(double[] newData, double[] data)
    {
        var n = data.Length;
        var y0 = data[0];
        var slope = (data[n - 1] - y0) / (n - 1);
        for (var x = 0; x < n; x++)
        {
            newData[x] += slope * x + y0;
        }
    }

}