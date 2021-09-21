﻿using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Objects.FSP
{
    public class SdlText : SdlObject, ISdlFspObject
    {
        public string FontName { get; set; }

        public string Text { get; set; }

        public ISdlScalarValue Thickness { get; set; }

        public ISdlVectorValue Offset { get; set; }

    }
}
