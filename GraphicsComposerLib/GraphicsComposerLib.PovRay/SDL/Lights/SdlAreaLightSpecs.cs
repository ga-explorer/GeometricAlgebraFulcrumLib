﻿using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Lights
{
    public class SdlAreaLightSpecs
    {
        public ISdlVectorValue Axis1 { get; set; }

        public ISdlVectorValue Axis2 { get; set; }

        public int Size1 { get; set; }

        public int Size2 { get; set; }

        public ISdlScalarValue Adaptive { get; set; }

        public ISdlBooleanValue AreaIllumination { get; set; }

        public bool Jitter { get; set; }

        public bool Circular { get; set; }

        public bool Orient { get; set; }
    }
}
