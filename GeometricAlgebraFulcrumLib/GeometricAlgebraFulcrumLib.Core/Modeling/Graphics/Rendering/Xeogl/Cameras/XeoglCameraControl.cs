using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Cameras;

/// <summary>
/// Rotates, pans and zooms the Scene's Camera with keyboard, mouse and
/// touch input.
/// http://xeogl.org/docs/classes/CameraControl.html
/// </summary>
/// <remarks>
/// CameraControl fires these events:
///     "hover" - Hover enters a new object
///     "hoverSurface" - Hover continues over an object surface - fired continuously as mouse moves over an object
///     "hoverLeave" - Hover has left the last object we were hovering over
///     "hoverOff" - Hover continues over empty space - fired continuously as mouse moves over nothing
///     "picked" - Clicked or tapped object
///     "pickedSurface" - Clicked or tapped object, with event containing surface intersection details
///     "doublePicked" - Double-clicked or double-tapped object
///     "doublePickedSurface" - Double-clicked or double-tapped object, with event containing surface intersection details
///     "pickedNothing" - Clicked or tapped, but not on any objects
///     "doublePickedNothing" - Double-clicked or double-tapped, but not on any objects
///     CameraControl only fires "hover" events when the mouse is up.
/// 
/// For efficiency, CameraControl only does surface intersection picking when
/// you subscribe to "doublePicked" and "doublePickedSurface" events.
/// Therefore, only subscribe to those when you're OK with the overhead
/// incurred by the surface intersection tests.
/// 
/// See Also: http://xeogl.org/docs/classes/CameraControl.html    /// </remarks>
public sealed class XeoglCameraControl : XeoglComponent
{
    public override string JavaScriptClassName 
        => "CameraControl";

    public bool FirstPerson { get; set; }

    public bool Walking { get; set; }

    public bool DoublePickFlyTo { get; set; } 
        = true;

    public bool Active { get; set; } 
        = true;

    public bool Pivoting { get; set; }

    public bool PanToPointer { get; set; }

    public double Inertia { get; set; } 
        = 0.5;


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("firstPerson", FirstPerson, false)
            .SetValue("walking", Walking, false)
            .SetValue("doublePickFlyTo", DoublePickFlyTo, true)
            .SetValue("active", Active, true)
            .SetValue("pivoting", Pivoting, false)
            .SetValue("panToPointer", PanToPointer, false)
            .SetValue("inertia", Inertia, 0.5);
    }

    //public override string ToString()
    //{
    //    var composer = new XeoglAttributesTextComposer();

    //    UpdateAttributesComposer(composer);

    //    return composer
    //        .AppendXeoglConstructorCall(this)
    //        .ToString();
    //}
}