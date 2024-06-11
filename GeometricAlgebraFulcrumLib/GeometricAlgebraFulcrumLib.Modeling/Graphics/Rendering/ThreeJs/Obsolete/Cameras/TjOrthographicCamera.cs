using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Cameras;

/// <summary>
/// Camera that uses orthographic projection.
/// In this projection mode, an object's size in the rendered image
/// stays constant regardless of its distance from the camera.
/// This can be useful for rendering 2D scenes and UI elements, amongst other things.
/// https://threejs.org/docs/#api/en/cameras/OrthographicCamera
/// </summary>
public class TjOrthographicCamera :
    TjCamera
{
    public override string JavaScriptClassName 
        => "OrthographicCamera";


    public double LeftPlane { get; set; }

    public double RightPlane { get; set; }

    public double TopPlane { get; set; }

    public double BottomPlane { get; set; }

    public double NearPlane { get; set; }

    public double FarPlane { get; set; }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("left", LeftPlane, 0d)
            .SetValue("right", RightPlane, 0d)
            .SetValue("top", TopPlane, 0d)
            .SetValue("bottom", BottomPlane, 0d)
            .SetValue("near", NearPlane, 0.1d)
            .SetValue("far", FarPlane, 2000d);
    }
}