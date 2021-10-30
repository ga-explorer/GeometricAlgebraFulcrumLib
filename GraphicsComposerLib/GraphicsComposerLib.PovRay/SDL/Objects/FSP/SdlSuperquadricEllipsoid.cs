using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Objects.FSP
{
    public class SdlSuperquadricEllipsoid : SdlObject, ISdlFspObject
    {
        public ISdlScalarValue EastWestExponent { get; set; }

        public ISdlScalarValue NorthSouthExponent { get; set; }
    }
}
