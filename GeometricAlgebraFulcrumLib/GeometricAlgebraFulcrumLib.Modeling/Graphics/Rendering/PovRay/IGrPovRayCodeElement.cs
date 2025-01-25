namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay;

public interface IGrPovRayCodeElement
{
    bool IsEmptyCodeElement();

    string GetPovRayCode();
}