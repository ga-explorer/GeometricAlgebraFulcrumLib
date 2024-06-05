namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Compositions.Grids;

public sealed class SvgGcSquareGridIDs : ISvgGeometryComposerIDs
{
    public string ElementId { get; set; } = "squareGrid";

    public string BackgroundAreaId
        => ElementId + ".backgroundArea";

    public string UnitLinesId
        => ElementId + ".unitLines";

    public string SubUnitLinesId
        => ElementId + ".subUnitLines";

    public string SubSubUnitLinesId
        => ElementId + ".subSubUnitLines";

    public string AxisLinesId
        => ElementId + ".axisLines";

    public string BorderLinesId
        => ElementId + ".borderLines";


    public override string ToString()
    {
        return ElementId;
    }
}