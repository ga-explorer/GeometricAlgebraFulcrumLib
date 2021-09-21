using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Objects.FSP
{
    public class SdlSuperquadricEllipsoid : SdlObject, ISdlFspObject
    {
        public ISdlScalarValue EastWestExponent { get; set; }

        public ISdlScalarValue NorthSouthExponent { get; set; }
    }
}
