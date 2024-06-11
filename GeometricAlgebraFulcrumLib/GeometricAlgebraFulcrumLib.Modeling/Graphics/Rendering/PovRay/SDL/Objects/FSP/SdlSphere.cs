﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Objects.FSP;

public class SdlSphere : SdlObject, ISdlFspObject
{
    public ISdlVectorValue Center { get; set; }

    public ISdlScalarValue Radius { get; set; }
}