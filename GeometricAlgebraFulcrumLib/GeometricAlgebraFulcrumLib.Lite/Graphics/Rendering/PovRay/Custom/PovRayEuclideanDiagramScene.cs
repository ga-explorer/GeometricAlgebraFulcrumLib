using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Cameras;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Objects;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Objects.FSP;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.Custom;

public class PovRayEuclideanDiagramScene : SdlScene
{
    public SdlOrthographicCamera DefaultCamera()
    {
        var camera = new SdlOrthographicCamera()
        {
            Location = SdlUtils.SdlVector(300, 100, 200),
            LookAt = SdlUtils.SdlVector(0)
        };

        return camera;
    }

    public SdlCsgObject Vector(double ox, double oy, double oz, double dx, double dy, double dz, double thickness)
    {
        var thicknessScalar = thickness.SdlScalar();
        var vectorLength = dx*dx + dy*dy + dz*dz;
        var arrowLength = thickness * 24;
        var axisLength = vectorLength - arrowLength;
        var axisToVectorRatio = axisLength/vectorLength;
        var headToVectorRatio = arrowLength / vectorLength;

        var axisStartPoint = SdlUtils.SdlVector(ox, oy, oz);
        var axisEndPoint = axisStartPoint + axisToVectorRatio * SdlUtils.SdlVector(dx, dy, dz);
        var arrowEndPoint = axisStartPoint + SdlUtils.SdlVector(dx, dy, dz);

        var axis =
            new SdlCylinder()
            {
                BasePoint = axisStartPoint,
                CapPoint = axisEndPoint,
                Radius = thicknessScalar
            };

        var axisStartSphere =
            new SdlSphere()
            {
                Center = axisStartPoint,
                Radius = thicknessScalar
            };

        var axisEndSphere =
            new SdlSphere()
            {
                Center = axisEndPoint,
                Radius = thicknessScalar
            };

        var arrow =
            new SdlCone()
            {
                BasePoint = axisEndPoint,
                BaseRadius = (arrowLength / 6).SdlScalar(),
                CapPoint = arrowEndPoint,
                CapRadius = SdlUtils.SdlScalar(0)
            };

        var vector = new SdlCsgObject()
        {
            CsgOperation = SdlCsgOperation.Union
        };

        vector.ChildObjects.Add(axis, axisStartSphere, axisEndSphere, arrow);

        return vector;
    }
}