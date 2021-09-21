namespace GraphicsComposerLib.Xeogl.Camera
{
    public sealed class XeoglCameraOrbit
    {
        public bool Enabled { get; set; }
            = true;

        public double YawDelta { get; set; }
            = 0;

        public double PitchDelta { get; set; }
            = 0;
    }
}
