﻿using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Objects.ISP;

public class SdlPlane : SdlObject, ISdlIspObject
{
    public ISdlVectorValue Normal { get; set; }

    public ISdlScalarValue Distance { get; set; }
}