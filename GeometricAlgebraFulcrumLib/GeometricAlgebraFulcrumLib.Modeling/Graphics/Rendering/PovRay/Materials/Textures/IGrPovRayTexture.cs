namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Textures;

public interface IGrPovRayTexture :
    IGrPovRayTransformableCodeElement
{
    string GetPovRayCode(bool isInterior);
}