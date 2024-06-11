using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Cameras;

/// <summary>
/// A Camera defines viewing and projection transforms for its Scene.
/// </summary>
/// <remarks>
///     One Camera per Scene
///     Controls viewing and projection transforms
///     Has methods to pan, zoom and orbit (or first-person rotation)
///     Dynamically configurable World-space "up" direction
///     Switchable between perspective, frustum and orthographic projections
///     Switchable gimbals lock
///     Can be "flown" to look at targets using a CameraFlightAnimation
///     Can be animated along a path using a CameraPathAnimation
///     Can follow a target using a CameraFollowAnimation
///
/// See Also: http://xeogl.org/docs/classes/Camera.html
/// </remarks>
public sealed class XeoglCamera : XeoglComponent
{
    public override string JavaScriptClassName 
        => "Camera";

    public XeoglCameraProjection Projection { get; private set; }
        = new XeoglPerspectiveProjection();

    public XeoglCameraProjectionType ProjectionType
        => Projection.ProjectionType;

    public LinFloat64Vector3DComposer EyePoint { get; }
        = LinFloat64Vector3DComposer.Create(XeoglCameraDefaults.EyePoint);

    public LinFloat64Vector3DComposer LookAtPoint { get; }
        = LinFloat64Vector3DComposer.Create(XeoglCameraDefaults.LookAtPoint);

    public LinFloat64Vector3DComposer WorldRightDirection { get; }
        = LinFloat64Vector3DComposer.Create(1, 0, 0);

    public LinFloat64Vector3DComposer WorldUpDirection { get; }
        = LinFloat64Vector3DComposer.Create(0, 1, 0);

    public LinFloat64Vector3DComposer WorldForwardDirection { get; }
        = LinFloat64Vector3DComposer.Create(0, 0, 1);


    /// <summary>
    /// If true:  Yaw rotation now happens about World's up axis
    /// If false: Yaw rotation now happens about Camera's local Y-axis
    /// </summary>
    public bool GimbalLock { get; set; } 
        = true;

    public XeoglCameraControl CameraControl { get; }
        = new XeoglCameraControl();

    public XeoglCameraOrbit CameraOrbit { get; }
        = new XeoglCameraOrbit();

    public XeoglCameraAxisHelper AxisHelper { get;} 
        = new XeoglCameraAxisHelper();


    public XeoglCamera Reset()
    {
        Projection = new XeoglPerspectiveProjection();

        EyePoint.SetVector(0, 0, 10);
        LookAtPoint.SetVector(0, 0, 0);

        WorldRightDirection.SetVector(0, 1, 0);
        WorldUpDirection.SetVector(0, 1, 0);
        WorldForwardDirection.SetVector(0, 0, 1);

        GimbalLock = true;

        return this;
    }

    public XeoglCamera SetPosition(ILinFloat64Vector3D eyePoint, ILinFloat64Vector3D lookAtPoint)
    {
        EyePoint.SetVector(eyePoint);
        LookAtPoint.SetVector(lookAtPoint);

        return this;
    }

    public XeoglCamera SetOrientation(LinUnitBasisVector3D rightAxis, LinUnitBasisVector3D upAxis, LinUnitBasisVector3D forwardAxis)
    {
        WorldRightDirection.SetVector(rightAxis.ToLinVector3D());
        WorldUpDirection.SetVector(upAxis.ToLinVector3D());
        WorldForwardDirection.SetVector(forwardAxis.ToLinVector3D());

        return this;
    }

    public XeoglCamera SetOrientation(ILinFloat64Vector3D rightAxis, ILinFloat64Vector3D upAxis, ILinFloat64Vector3D forwardAxis)
    {
        WorldRightDirection.SetVector(rightAxis);
        WorldUpDirection.SetVector(upAxis);
        WorldForwardDirection.SetVector(forwardAxis);

        return this;
    }

    public XeoglCamera SetOrientationUpY()
    {
        WorldRightDirection.SetVector(LinFloat64Vector3D.Create(1, 0, 0));
        WorldUpDirection.SetVector(LinFloat64Vector3D.Create(0, 1, 0));
        WorldForwardDirection.SetVector(LinFloat64Vector3D.Create(0, 0, -1));

        return this;
    }

    public XeoglCamera SetOrientationUpZ()
    {
        WorldRightDirection.SetVector(LinFloat64Vector3D.Create(1, 0, 0));
        WorldUpDirection.SetVector(LinFloat64Vector3D.Create(0, 0, 1));
        WorldForwardDirection.SetVector(LinFloat64Vector3D.Create(0, -1, 0));

        return this;
    }

    public double[] GetWorldAxisArray()
    {
        return new double[]
        {
            WorldRightDirection.X,
            WorldRightDirection.Y,
            WorldRightDirection.Z,

            WorldUpDirection.X,
            WorldUpDirection.Y,
            WorldUpDirection.Z,

            WorldForwardDirection.X,
            WorldForwardDirection.Y,
            WorldForwardDirection.Z
        };
    }


    public XeoglCamera SetProjection(XeoglCameraProjection projection)
    {
        if (!ReferenceEquals(projection, null))
            Projection = projection;

        return this;
    }

    public XeoglCamera SetProjectionPerspective()
    {
        Projection = new XeoglPerspectiveProjection();

        return this;
    }

    public XeoglCamera SetProjectionOrthographic(double scale = 1, double nearZ = 0.1d, double farZ = 10000)
    {
        Projection = new XeoglOrthographicProjection()
        {
            Scale = scale,
            NearZ = nearZ,
            FarZ = farZ
        };

        return this;
    }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("projection", ProjectionType, XeoglCameraProjectionType.Perspective)
            .SetNumbersArrayValue("eye", EyePoint, XeoglCameraDefaults.EyePoint)
            .SetNumbersArrayValue("look", LookAtPoint, XeoglCameraDefaults.LookAtPoint)
            .SetNumbersArrayValue("up", WorldUpDirection, XeoglCameraDefaults.WorldUpDirection)
            .SetNumbersArrayValue("worldAxis", GetWorldAxisArray(), XeoglCameraDefaults.WorldAxisArray)
            .SetValue("gimbalLock", GimbalLock, true);

        Projection.UpdateConstructorAttributes(composer);
    }

    protected override void GenerateAdditionalCode(LinearTextComposer composer)
    {
        composer
            .AppendLine()
            .AppendLine()
            .AppendLine(CameraControl.ToString());

        if (CameraOrbit.Enabled)
            composer
                .AppendLine()
                .AppendLine(@"scene.on(""tick"", function () {")
                .IncreaseIndentation()
                .AppendLine($"{JavaScriptVariableName}.orbitYaw({CameraOrbit.YawDelta});")
                .AppendLine($"{JavaScriptVariableName}.orbitPitch({CameraOrbit.PitchDelta});")
                .DecreaseIndentation()
                .AppendLine(@"});");

        if (AxisHelper.Enabled)
            composer
                .AppendLine()
                .AppendLineAtNewLine("new xeogl.AxisHelper({")
                .IncreaseIndentation()
                .AppendLine("camera: scene.camera,")
                .AppendLine("visible: true,")
                .Append("size: [")
                .Append(AxisHelper.PixelsSize)
                .Append(", ")
                .Append(AxisHelper.PixelsSize)
                .AppendLine("]")
                .DecreaseIndentation()
                .AppendLine("});");
    }
}