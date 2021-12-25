using DataStructuresLib.Statistics;

namespace NumericalGeometryLib.Computers
{
    public interface IGeometryComputer
    {
        EventSummaryCollection EventCounters { get; }
    }
}