namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;

public abstract class GrPovRayCameraProperties : 
    GrPovRayAttributeSet
{
    public override string ToString()
    {
        return GetPovRayCode();
    }
}