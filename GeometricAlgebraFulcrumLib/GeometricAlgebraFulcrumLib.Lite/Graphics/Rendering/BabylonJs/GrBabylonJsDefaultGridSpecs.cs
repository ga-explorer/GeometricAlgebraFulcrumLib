namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs;

public sealed class GrBabylonJsDefaultGridSpecs
{
    public static GrBabylonJsDefaultGridSpecs CreateXy(int gridUnitCount, double unitSize = 1, double zValue = 0, double opacity = 0.25)
    {
        return new GrBabylonJsDefaultGridSpecs
        {
            ShowXyGrid = true,
            ShowYzGrid = false,
            ShowZxGrid = false,
            UnitCount = gridUnitCount,
            UnitSize = unitSize,
            Opacity = opacity,
            XyGridZValue = zValue,
            YzGridXValue = 0,
            ZxGridYValue = 0
        };
    }
    
    public static GrBabylonJsDefaultGridSpecs CreateYz(int gridUnitCount, double unitSize = 1, double xValue = 0, double opacity = 0.25)
    {
        return new GrBabylonJsDefaultGridSpecs
        {
            ShowXyGrid = false,
            ShowYzGrid = true,
            ShowZxGrid = false,
            UnitCount = gridUnitCount,
            UnitSize = unitSize,
            Opacity = opacity,
            XyGridZValue = 0,
            YzGridXValue = xValue,
            ZxGridYValue = 0
        };
    }

    public static GrBabylonJsDefaultGridSpecs CreateZx(int gridUnitCount, double unitSize = 1, double yValue = 0, double opacity = 0.25)
    {
        return new GrBabylonJsDefaultGridSpecs
        {
            ShowXyGrid = false,
            ShowYzGrid = false,
            ShowZxGrid = true,
            UnitCount = gridUnitCount,
            UnitSize = unitSize,
            Opacity = opacity,
            XyGridZValue = 0,
            YzGridXValue = 0,
            ZxGridYValue = yValue
        };
    }

    public static GrBabylonJsDefaultGridSpecs CreateXyz(int gridUnitCount, double unitSize = 1, double opacity = 0.25)
    {
        return new GrBabylonJsDefaultGridSpecs
        {
            ShowXyGrid = true,
            ShowYzGrid = true,
            ShowZxGrid = true,
            UnitCount = gridUnitCount,
            UnitSize = unitSize,
            Opacity = opacity,
            XyGridZValue = -0.5 * unitSize * gridUnitCount,
            YzGridXValue = -0.5 * unitSize * gridUnitCount,
            ZxGridYValue = -0.5 * unitSize * gridUnitCount
        };
    }


    public int UnitCount { get; init; }

    public double UnitSize { get; init; }

    public double Opacity { get; init; }

    public bool ShowXyGrid { get; init; }

    public bool ShowYzGrid { get; init; }

    public bool ShowZxGrid { get; init; }

    public double XyGridZValue {get; init; }

    public double YzGridXValue {get; init; }

    public double ZxGridYValue {get; init; }
}