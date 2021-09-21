using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicMath.Tuples.Mutable;
using EuclideanGeometryLib.GraphicsGeometry;
using EuclideanGeometryLib.GraphicsGeometry.Constants;
using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Camera
{
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

        public MutableTuple3D EyePoint { get; }
            = new MutableTuple3D(XeoglCameraDefaults.EyePoint);

        public MutableTuple3D LookAtPoint { get; }
            = new MutableTuple3D(XeoglCameraDefaults.LookAtPoint);

        public MutableTuple3D WorldRightDirection { get; }
            = new MutableTuple3D(1, 0, 0);

        public MutableTuple3D WorldUpDirection { get; }
            = new MutableTuple3D(0, 1, 0);

        public MutableTuple3D WorldForwardDirection { get; }
            = new MutableTuple3D(0, 0, 1);


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

            EyePoint.SetTuple(0, 0, 10);
            LookAtPoint.SetTuple(0, 0, 0);

            WorldRightDirection.SetTuple(0, 1, 0);
            WorldUpDirection.SetTuple(0, 1, 0);
            WorldForwardDirection.SetTuple(0, 0, 1);

            GimbalLock = true;

            return this;
        }

        public XeoglCamera SetPosition(ITuple3D eyePoint, ITuple3D lookAtPoint)
        {
            EyePoint.SetTuple(eyePoint);
            LookAtPoint.SetTuple(lookAtPoint);

            return this;
        }

        public XeoglCamera SetOrientation(GraphicsAxis3D rightAxis, GraphicsAxis3D upAxis, GraphicsAxis3D forwardAxis)
        {
            WorldRightDirection.SetTuple(rightAxis.GetVector3D());
            WorldUpDirection.SetTuple(upAxis.GetVector3D());
            WorldForwardDirection.SetTuple(forwardAxis.GetVector3D());

            return this;
        }

        public XeoglCamera SetOrientation(ITuple3D rightAxis, ITuple3D upAxis, ITuple3D forwardAxis)
        {
            WorldRightDirection.SetTuple(rightAxis);
            WorldUpDirection.SetTuple(upAxis);
            WorldForwardDirection.SetTuple(forwardAxis);

            return this;
        }

        public XeoglCamera SetOrientationUpY()
        {
            WorldRightDirection.SetTuple(new Tuple3D(1, 0, 0));
            WorldUpDirection.SetTuple(new Tuple3D(0, 1, 0));
            WorldForwardDirection.SetTuple(new Tuple3D(0, 0, -1));

            return this;
        }

        public XeoglCamera SetOrientationUpZ()
        {
            WorldRightDirection.SetTuple(new Tuple3D(1, 0, 0));
            WorldUpDirection.SetTuple(new Tuple3D(0, 0, 1));
            WorldForwardDirection.SetTuple(new Tuple3D(0, -1, 0));

            return this;
        }

        public double[] GetWorldAxisArray()
        {
            return new[]
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


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("projection", ProjectionType, XeoglCameraProjectionType.Perspective)
                .SetAttributeValue("eye", EyePoint, XeoglCameraDefaults.EyePoint)
                .SetAttributeValue("look", LookAtPoint, XeoglCameraDefaults.LookAtPoint)
                .SetAttributeValue("up", WorldUpDirection, XeoglCameraDefaults.WorldUpDirection)
                .SetAttributeValue("worldAxis", GetWorldAxisArray(), XeoglCameraDefaults.WorldAxisArray)
                .SetAttributeValue("gimbalLock", GimbalLock, true);

            Projection.UpdateAttributesComposer(composer);
        }

        public override string ToString()
        {
            var composer = new XeoglCodeComposer();

            UpdateAttributesComposer(composer);

            composer
                .AppendAttributesSetCode("camera")
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

            return composer.ToString();
        }
    }
}
