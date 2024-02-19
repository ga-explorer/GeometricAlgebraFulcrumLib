namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Pigments;

public sealed class SdlPigmentListPigment : SdlPigment
{
    public SdlColorListPigmentPattern Pattern { get; set; }

    public ISdlPigment Pigment1 { get; set; }

    public ISdlPigment Pigment2 { get; set; }

    public ISdlPigment Pigment3 { get; set; }

}