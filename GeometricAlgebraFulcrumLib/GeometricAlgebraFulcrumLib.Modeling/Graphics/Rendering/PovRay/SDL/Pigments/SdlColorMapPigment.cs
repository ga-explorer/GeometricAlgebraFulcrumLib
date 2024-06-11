﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Patterns;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Pigments;

public sealed class SdlColorMapItem
{
    public double Value { get; set; }

    public ISdlColorValue Color { get; set; }
}

public sealed class SdlColorMapPigment : SdlPigment
{
    public ISdlPattern Pattern { get; set; }

    public List<SdlColorMapItem> ColorItems { get; private set; }


    public SdlColorMapPigment()
    {
        ColorItems = new List<SdlColorMapItem>();
    }
}