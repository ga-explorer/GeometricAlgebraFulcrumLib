﻿using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Constants;
using TextComposerLib.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Cameras;

public sealed class XeoglPerspectiveProjection : XeoglCameraProjection
{
    public override string JavaScriptClassName 
        => "Perspective";

    public override XeoglCameraProjectionType ProjectionType
        => XeoglCameraProjectionType.Perspective;

    public double FieldOfViewAngle { get; set; } 
        = 60;

    public XeoglPerspectiveFieldOfViewAxis FieldOfViewAxis { get; set; }
        = XeoglPerspectiveFieldOfViewAxis.Min;

    public double NearZ { get; set; } 
        = 0.1d;

    public double FarZ { get; set; } 
        = 10000;


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("fov", FieldOfViewAngle, 60)
            .SetValue("fovAxis", FieldOfViewAxis, XeoglPerspectiveFieldOfViewAxis.Min)
            .SetValue("near", NearZ, 0.1d)
            .SetValue("far", FarZ, 10000);
    }
}