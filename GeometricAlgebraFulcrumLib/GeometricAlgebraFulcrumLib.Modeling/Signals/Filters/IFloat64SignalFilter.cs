namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Filters
{
    public interface IFloat64SignalFilter
    {
        IEnumerable<double> Filter(IEnumerable<double> signal);
    }
}
