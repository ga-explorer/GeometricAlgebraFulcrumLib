using GeometricAlgebraFulcrumLib.Utilities.Structures.Statistics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Computers;

public interface IGeometryComputer
{
    EventSummaryCollection EventCounters { get; }
}