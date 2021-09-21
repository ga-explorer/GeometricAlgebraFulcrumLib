namespace GraphicsComposerLib.Xeogl.Geometry
{
    public interface IXeoglGeometry
    {
        bool Quantized { get; set; }

        bool Combined { get; set; }

        double EdgeThreshold { get; set; }
    }
}