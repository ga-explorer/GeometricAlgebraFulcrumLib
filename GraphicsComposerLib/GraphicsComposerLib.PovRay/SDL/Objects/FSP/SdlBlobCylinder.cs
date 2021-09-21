﻿using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Objects.FSP
{
    public class SdlBlobCylinder : SdlObject, ISdlBlobComponent
    {
        public ISdlScalarValue Strength { get; set; }

        public ISdlVectorValue BasePoint { get; set; }

        public ISdlVectorValue CapPoint { get; set; }

        public ISdlScalarValue Radius { get; set; }


        public SdlBlobCylinder()
        {
            Strength = Strength = SdlScalarLiteral.One;
        }

        public SdlBlobCylinder(ISdlScalarValue strength)
        {
            Strength = strength;
        }
    }
}
