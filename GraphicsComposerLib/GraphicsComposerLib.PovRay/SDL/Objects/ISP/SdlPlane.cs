﻿using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Objects.ISP
{
    public class SdlPlane : SdlObject, ISdlIspObject
    {
        public ISdlVectorValue Normal { get; set; }

        public ISdlScalarValue Distance { get; set; }
    }
}
