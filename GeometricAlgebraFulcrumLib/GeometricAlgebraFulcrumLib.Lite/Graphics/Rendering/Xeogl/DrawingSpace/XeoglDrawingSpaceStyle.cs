using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Materials;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.DrawingSpace;

public sealed class XeoglDrawingSpaceStyle
{
    public XeoglDrawingSpace ParentDrawingSpace { get; }

    public XeoglMaterial PointMaterial { get; set; }

    public XeoglMaterial LineMaterial { get; set; }

    public XeoglMaterial SurfaceMaterial { get; set; }


    internal XeoglDrawingSpaceStyle(XeoglDrawingSpace parentDrawingSpace)
    {
        ParentDrawingSpace = parentDrawingSpace;
    }
}