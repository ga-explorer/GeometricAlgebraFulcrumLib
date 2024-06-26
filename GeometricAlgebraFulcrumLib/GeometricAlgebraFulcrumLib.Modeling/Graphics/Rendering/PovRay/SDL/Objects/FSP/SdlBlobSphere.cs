﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Objects.FSP;

public class SdlBlobSphere : SdlObject, ISdlBlobComponent
{
    public ISdlScalarValue Strength { get; set; }

    public ISdlVectorValue Center { get; set; }

    public ISdlScalarValue Radius { get; set; }


    public SdlBlobSphere()
    {
        Strength = Strength = SdlScalarLiteral.One;
    }

    public SdlBlobSphere(ISdlScalarValue strength)
    {
        Strength = strength;
    }
}