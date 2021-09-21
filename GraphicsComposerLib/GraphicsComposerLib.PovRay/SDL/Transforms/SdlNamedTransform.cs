﻿namespace GraphicsComposerLib.POVRay.SDL.Transforms
{
    public sealed class SdlNamedTransform : SdlTransform
    {
        public string Identifier { get; set; }


        internal SdlNamedTransform(string ident)
        {
            Identifier = ident;
        }
    }
}
