﻿using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Transforms;

public sealed class SdlScaleTransform : SdlTransform
{
    public ISdlVectorValue FactorVector { get; private set; }


    internal SdlScaleTransform(ISdlVectorValue factorVector)
    {
        FactorVector = factorVector;
    }
}