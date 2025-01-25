namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;

public interface IGrPovRayCamera :
    IGrPovRayStatement,
    IGrPovRayTransformableCodeElement
{
    string CameraType { get; }
}