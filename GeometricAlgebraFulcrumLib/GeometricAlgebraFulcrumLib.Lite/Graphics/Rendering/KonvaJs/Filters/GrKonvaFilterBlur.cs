﻿using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterBlur :
    GrKonvaFilter
{
    public override string FilterName 
        => "Blur";

    public GrKonvaJsFloat32Value? BlurRadius
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("BlurRadius");
        set => ParentStyle.SetAttributeValue("BlurRadius", value);
    }


    internal GrKonvaFilterBlur(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}