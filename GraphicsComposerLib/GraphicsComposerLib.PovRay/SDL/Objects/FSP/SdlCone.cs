using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Objects.FSP
{
    public class SdlCone : SdlObject, ISdlFspObject
    {
        public ISdlVectorValue BasePoint { get; set; }

        public ISdlVectorValue CapPoint { get; set; }

        public ISdlScalarValue BaseRadius { get; set; }

        public ISdlScalarValue CapRadius { get; set; }

        public bool Open { get; set; }

    }
}
