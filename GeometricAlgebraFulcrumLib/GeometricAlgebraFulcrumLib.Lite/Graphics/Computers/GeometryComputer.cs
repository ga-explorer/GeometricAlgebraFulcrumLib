using DataStructuresLib.Statistics;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Computers
{
    public abstract class GeometryComputer : IGeometryComputer
    {
        public EventSummaryCollection EventCounters { get; private set; }

        public bool HasEventCountersCollection => !ReferenceEquals(EventCounters, null);


        protected GeometryComputer()
        {
            EventCounters = GeometryComputersUtils.GlobalEventCounters;
        }

        protected GeometryComputer(EventSummaryCollection eventCounters)
        {
            EventCounters = eventCounters ?? GeometryComputersUtils.GlobalEventCounters;
        }
    }
}
