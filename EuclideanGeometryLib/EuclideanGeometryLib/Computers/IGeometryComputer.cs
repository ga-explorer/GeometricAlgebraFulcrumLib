using DataStructuresLib.Statistics;

namespace EuclideanGeometryLib.Computers
{
    public interface IGeometryComputer
    {
        EventSummaryCollection EventCounters { get; }
    }
}