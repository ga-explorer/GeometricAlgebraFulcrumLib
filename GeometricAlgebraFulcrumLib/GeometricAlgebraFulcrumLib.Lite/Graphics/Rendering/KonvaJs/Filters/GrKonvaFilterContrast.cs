﻿using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterContrast :
    GrKonvaFilter
{
    public override string FilterName 
        => "Contrast";

    
    public GrKonvaJsFloat32Value? Contrast
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Contrast");
        set => ParentStyle.SetAttributeValue("Contrast", value);
    }


    internal GrKonvaFilterContrast(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}