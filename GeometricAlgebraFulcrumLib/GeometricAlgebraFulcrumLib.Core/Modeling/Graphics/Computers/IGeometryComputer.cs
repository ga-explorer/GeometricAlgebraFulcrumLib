using GeometricAlgebraFulcrumLib.Utilities.Structures.Statistics;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Computers;

public interface IGeometryComputer
{
    EventSummaryCollection EventCounters { get; }
}