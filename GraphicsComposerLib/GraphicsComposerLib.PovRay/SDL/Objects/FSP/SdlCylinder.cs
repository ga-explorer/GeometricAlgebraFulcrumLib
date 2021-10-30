using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Objects.FSP
{
    public class SdlCylinder : SdlObject, ISdlFspObject
    {
        public ISdlVectorValue BasePoint { get; set; }

        public ISdlVectorValue CapPoint { get; set; }

        public ISdlScalarValue Radius { get; set; }

        public bool Open { get; set; }




    }
}
