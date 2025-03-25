namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;

/// <summary>
/// https://gery.casiez.net/1euro/
/// </summary>
public sealed class Float64OneEuroFilter(double minCutoff, double beta)
{
    public static IEnumerable<double> Filter(double minCutoff, double beta, IEnumerable<double> signal)
    {
        var filter = new Float64OneEuroFilter(minCutoff, beta);

        return signal.Select(x => filter.Filter(x, 1));
    }


    public sealed class LowPassFilter
    {
        private bool _firstTime = true;
        private double _hatXPrev;

        public double Last()
        {
            return _hatXPrev;
        }

        public double Filter(double x, double alpha)
        {
            double hatX;
            if (_firstTime)
            {
                _firstTime = false;
                hatX = x;
            }
            else
                hatX = alpha * x + (1 - alpha) * _hatXPrev;

            _hatXPrev = hatX;

            return hatX;
        }
    }


    private static double Alpha(double rate, double cutoff)
    {
        //var tau = 1.0 / (2 * Math.PI * cutoff);
        //var te = 1.0 / rate;
        //return 1.0 / (1.0 + tau / te);

        return 1.0 / (1.0 + rate / (2 * Math.PI * cutoff));
    }


    private bool _firstTime = true;
    private readonly LowPassFilter _xFilter = new();
    private readonly LowPassFilter _dxFilter = new();
    private const double _dCutOff = 1;

    public double MinCutoff { get; } = minCutoff;

    public double Beta { get; } = beta;


    public double Filter(double x, double rate)
    {
        var dx = _firstTime ? 0 : (x - _xFilter.Last()) * rate;
        if (_firstTime) 
            _firstTime = false;

        var edx = _dxFilter.Filter(dx, Alpha(rate, _dCutOff));
        var cutoff = MinCutoff + Beta * Math.Abs(edx);

        return _xFilter.Filter(x, Alpha(rate, cutoff));
    }
}