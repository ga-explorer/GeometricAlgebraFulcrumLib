using TextComposerLib.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Cameras;

/// <summary>
/// Camera that uses perspective projection.
/// This projection mode is designed to mimic the way the human eye sees.
/// It is the most common projection mode used for rendering a 3D scene.
/// https://threejs.org/docs/#api/en/cameras/PerspectiveCamera
/// </summary>
public class TjPerspectiveCamera :
    TjCamera
{
    public override string JavaScriptClassName 
        => "PerspectiveCamera";


    public double VerticalFieldOfView { get; set; }

    public double AspectRatio { get; set; }

    public double NearPlane { get; set; }

    public double FarPlane { get; set; }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("fov", VerticalFieldOfView, 50d)
            .SetValue("aspect", AspectRatio, 1d)
            .SetValue("near", NearPlane, 0.1d)
            .SetValue("far", FarPlane, 2000d);
    }
}