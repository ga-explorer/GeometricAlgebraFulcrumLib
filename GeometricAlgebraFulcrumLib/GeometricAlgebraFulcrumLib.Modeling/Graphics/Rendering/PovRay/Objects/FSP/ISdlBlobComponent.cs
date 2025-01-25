using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public interface ISdlBlobComponent : IGrPovRayFiniteSolidObject
{
    GrPovRayFloat32Value Strength { get; set; }
}