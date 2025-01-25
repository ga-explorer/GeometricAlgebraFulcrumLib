namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;

public sealed class SdlPigmentMapItem
{
    public double Value { get; set; }

    public IGrPovRayPigment Pigment { get; set; }
}